Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Text
Imports System.Security
Imports System.Security.Cryptography
Imports System.Web.Services
Imports System.Web.UI
Imports System.Data
Imports System.Data.SqlClient

Public Class Encryption
    Public Shared mbytKey(7) As Byte
    Public Shared mbytIV(7) As Byte

    ' -----------------------------------------------------
    Public Shared Function InitKey(ByVal strKey As String) As Boolean
        'Fonction de génération des clés pour les variables internes
        Try
            ' Conversion de la clé en Tableau d'Octets
            Dim bp(strKey.Length - 1) As Byte
            Dim aEnc As ASCIIEncoding = New ASCIIEncoding()
            aEnc.GetBytes(strKey, 0, strKey.Length, bp, 0)

            'Hashage de la clé en utilisant SHA1
            Dim sha As SHA1CryptoServiceProvider = New SHA1CryptoServiceProvider()
            Dim bpHash() As Byte = sha.ComputeHash(bp)

            Dim i As Integer
            ' Utilisation de la base 64-bits pour la valeur de la Clé
            For i = 0 To 7
                mbytKey(i) = bpHash(i)
            Next i

            For i = 8 To 15
                mbytIV(i - 8) = bpHash(i)
            Next
            Return True

        Catch e As Exception
            'Erreur durant les opérations 
            Return False
        End Try
    End Function

    ' -----------------------------------------------------
    Public Function EncryptData(ByVal strKey As String, ByVal strData As String) As String
        Dim strResult As String

        '1. La chaine de doit pas dépasser 90 Ko, sinon le Buffer sera dépassé. 
        'Voir la raison au point 3
        If strData.Length > 92160 Then
            strResult = "Erreur: Les données sont trop volumineuses. Ne pas dépasser 90Ko."
            Return strResult
        End If

        '2. Génération de la Clé
        If Not (InitKey(strKey)) Then
            strResult = "Erreur: Impossible de générer la clé de Cryptage."
            Return strResult
        End If

        '3. Préparation de la Chaine
        ' Les premiers 5 caractères de la Chaine sont formatés pour stocker l'actuelle
        ' longueur des Données. C'est une méthode simple pour conserver la taille originale
        ' des données, sans avoir à compliquer les calculs.
        strData = String.Format("{0,5:00000}" & strData, strData.Length)


        '4. Cryptage des Données
        Dim rbData(strData.Length - 1) As Byte
        Dim aEnc As New ASCIIEncoding()
        aEnc.GetBytes(strData, 0, strData.Length, rbData, 0)

        Dim descsp As DESCryptoServiceProvider = New DESCryptoServiceProvider()

        Dim desEncrypt As ICryptoTransform = descsp.CreateEncryptor(mbytKey, mbytIV)


        '5. Préparation des streams:
        ' mOut est le Stream de Sortie.
        ' mStream est le Stream d'Entrée.
        ' cs est la Stream de Transformation.
        Dim mStream As New MemoryStream(rbData)
        Dim cs As New CryptoStream(mStream, desEncrypt, CryptoStreamMode.Read)
        Dim mOut As New MemoryStream()

        '6. Début du Cryptage
        Dim bytesRead As Integer
        Dim output(1023) As Byte
        Do
            bytesRead = cs.Read(output, 0, 1024)
            If Not (bytesRead = 0) Then
                mOut.Write(output, 0, bytesRead)
            End If
        Loop While (bytesRead > 0)

        '7. Renvoie le résultat Crypté encodé en Base 64
        ' Dans ce cas, Le résultat Actuel est Converti en Base 64 et il peut être 
        ' transporté au travers du protocole HTTP sans déformation.
        If mOut.Length = 0 Then
            strResult = ""
        Else
            strResult = Convert.ToBase64String(mOut.GetBuffer(), 0, CInt(mOut.Length))
            '8. Modification de la chaine d'entrée pour récupérer les + 
            ' qui ne sont pas passables via Querystring
            strResult = strResult.Replace("+", "-||-||-")
        End If
        Return strResult

    End Function

    ' -----------------------------------------------------
    Public Function DecryptData(ByVal strKey As String, ByVal strData As String) As String

        Dim strResult As String

        '1. Modification de la chaine d'entrée pour récupérer les + 
        ' qui ne sont pas passables via Querystring
        strData = strData.Replace("-||-||-", "+")

        '2. Génération de la Clé
        If Not (InitKey(strKey)) Then
            strResult = "Erreur: Impossible de générer la clé de Cryptage."
            Return strResult
        End If

        '2. Initialisation du Provider
        Dim nReturn As Integer = 0
        Dim descsp As New DESCryptoServiceProvider()
        Dim desDecrypt As ICryptoTransform = descsp.CreateDecryptor(mbytKey, mbytIV)

        '3. Préparation des streams:
        ' mOut est le Stream de Sortie.
        ' cs est la Stream de Transformation.
        Dim mOut As New MemoryStream()
        Dim cs As New CryptoStream(mOut, desDecrypt, CryptoStreamMode.Write)

        '4. Mémorisation pour revenir de la Base 64 vers un tableau d'Octets pour récupérer le 
        'le Stream de base Crypté
        Dim bPlain(strData.Length - 1) As Byte
        Try
            bPlain = Convert.FromBase64CharArray(strData.ToCharArray(), 0, strData.Length)
        Catch e As Exception
            strResult = "Erreur: Les données en Entrée ne sont pas en base 64."
            Return strResult
        End Try

        Dim lRead As Long = 0
        Dim lReadNow As Long = 0
        Dim lTotal As Long = strData.Length

        Try
            '5. Réalise le Décryptage
            Do While (lTotal >= lRead)
                cs.Write(bPlain, 0, bPlain.Length)
                lReadNow = CLng(((bPlain.Length / descsp.BlockSize) * descsp.BlockSize))
                lRead = lReadNow + lRead
            Loop

            Dim aEnc As New ASCIIEncoding()
            strResult = aEnc.GetString(mOut.GetBuffer(), 0, CInt(mOut.Length))

            '6. Nettoie la chaine pour ne renvoyer que les données signicatives
            ' Rappelez-vous cela dans la fonction de chiffrage, les 5 premiers caractères
            ' permettent de conserver la taille originale de la chaine de caracteres
            ' C'est la méthode la plus simple de mémoriser la taille des données d'origine,
            ' sans passer par des calculs complexes.

            Dim strLen As String = strResult.Substring(0, 5)
            Dim nLen As Integer = CInt(strLen)
            strResult = strResult.Substring(5, nLen)
            nReturn = CInt(mOut.Length)

            Return strResult
        Catch e As Exception
            strResult = "Erreur: Impossible de décrypter. Eventuellement "
            strResult &= "à cause d'une clé non conforme ou de données corrompue."
        End Try
        Return strResult
    End Function

    ' -----------------------------------------------------

    Public Sub AllCreditCardEncryption()
        Dim ConnectionString As String = ""

        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        'Dim sqlStr As String = "SELECT CustomerID,CreditCardNumber,OrderNumber,CompanyID,DivisionID,DepartmentID FROM OrderHeader WHERE CreditCardNumber<>'' AND CreditCardNumber IS NOT NULL "
        Dim sqlStr As String = "SELECT  CustomerID, CreditCardNumber, OrderNumber, CompanyID, DivisionID, DepartmentID " _
            & "FROM  OrderHeader " _
            & "WHERE  (CreditCardNumber <> '') AND (CreditCardNumber IS NOT NULL) AND (CompanyID <> 'Demo6') OR " _
            & "(CreditCardNumber <> '') AND (CreditCardNumber IS NOT NULL) AND (CompanyID <> 'Demo7') OR " _
            & "(CreditCardNumber <> '') AND (CreditCardNumber IS NOT NULL) AND (CompanyID <> 'Demo8') OR " _
            & "(CreditCardNumber <> '') AND (CreditCardNumber IS NOT NULL) AND (CompanyID <> 'Demo9') "
        'Dim rs As SqlDataReader = SqlHelper.ExecuteReader(ConString, CommandType.Text, sqlStr)
        'Return rs
        Dim Cmd As New SqlCommand
        Dim dr As SqlDataReader
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        ConString.Open()
        dr = Cmd.ExecuteReader()
        Dim cmd1 As New SqlCommand
        While dr.Read()
            UpdateCrd(dr("CreditCardNumber").ToString(), dr("OrderNumber").ToString(), dr("CompanyID").ToString(), dr("DivisionID").ToString(), dr("DepartmentID").ToString(), dr("CustomerID").ToString())
        End While
        dr.Close()
        ConString.Close()
    End Sub

    Public Sub UpdateCrd(ByVal Endata As String, ByVal OrdNo As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CustID As String)
        'If OrdNo <> "8590" Then
        '    If OrdNo <> "8591" Then
        '        If OrdNo <> "8593" Then
        Dim ConnectionString As String = ""
        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim ConString As New SqlConnection
        Dim cmd1 As New SqlCommand

        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "UPDATE OrderHeader SET CreditCardNumber='" & TripleDESEncode(Endata, CustID) & "' " _
                    & " WHERE OrderNumber='" & OrdNo & "' AND CustomerID='" & CustID & "' AND CompanyID='" & CompanyID & "' " _
                    & " AND DivisionID='" & DivisionID & "' AND DepartmentID='" & DepartmentID & "'"
        cmd1.CommandText = sqlStr
        cmd1.Connection = ConString
        ConString.Open()
        cmd1.ExecuteNonQuery()
        ConString.Close()
        '        End If
        '    End If
        'End If


    End Sub

    Public Function TripleDESEncode(ByVal value As String, ByVal key As String) As String

        key = key.ToUpper()
        Dim des As New Security.Cryptography.TripleDESCryptoServiceProvider

        des.IV = New Byte(7) {}

        Dim pdb As New Security.Cryptography.PasswordDeriveBytes(key, New Byte(-1) {})

        des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, New Byte(7) {})

        Dim ms As New IO.MemoryStream((value.Length * 2) - 1)

        Dim encStream As New Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(), Security.Cryptography.CryptoStreamMode.Write)

        Dim plainBytes As Byte() = Text.Encoding.UTF8.GetBytes(value)

        encStream.Write(plainBytes, 0, plainBytes.Length)

        encStream.FlushFinalBlock()

        Dim encryptedBytes(CInt(ms.Length - 1)) As Byte

        ms.Position = 0

        ms.Read(encryptedBytes, 0, CInt(ms.Length))

        encStream.Close()

        Return Convert.ToBase64String(encryptedBytes)

    End Function



    Public Function TripleDESDecode(ByVal value As String, ByVal key As String) As String
        key = key.ToUpper()

        Dim des As New Security.Cryptography.TripleDESCryptoServiceProvider

        des.IV = New Byte(7) {}

        Dim pdb As New Security.Cryptography.PasswordDeriveBytes(key, New Byte(-1) {})

        des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, New Byte(7) {})

        Dim encryptedBytes As Byte() = Convert.FromBase64String(value)

        Dim ms As New IO.MemoryStream(value.Length)

        Dim decStream As New Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(), Security.Cryptography.CryptoStreamMode.Write)

        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)

        decStream.FlushFinalBlock()

        Dim plainBytes(CInt(ms.Length - 1)) As Byte

        ms.Position = 0

        ms.Read(plainBytes, 0, CInt(ms.Length))

        decStream.Close()

        Return Text.Encoding.UTF8.GetString(plainBytes)

    End Function


End Class