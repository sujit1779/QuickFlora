Imports System.Xml
Imports System.Data

Imports Microsoft.VisualBasic

Public Class clsMercury

    Public Function CreditSaleXML() As String

        'set Values of above variable here
        Dim returnXML As String = CreateMercuryXML("Credit", "Sale")
        Return returnXML

    End Function

    Public Function CreditPreAuthXML() As String

        'set Values of above variables here
        Dim returnXML As String = CreateMercuryXML("Credit", "PreAuth")
        Return returnXML

    End Function

    Public Function CreditPreAuthCaptureXML() As String

        'set Values of above variables here
        Dim returnXML As String = "" 'CreateMercuryXMLPreAuthCapture("Credit", "PreAuthCapture")
        Return returnXML

    End Function

    Public Function CreditReturnXML() As String

        'set Values of above variables here
        Dim returnXML As String = CreateMercuryXML("Credit", "Return")
        Return returnXML

    End Function

    Public Function DebitSaleXML() As String

        'set Values of above variables here
        Dim returnXML As String = CreateMercuryXML("Debit", "Sale")
        Return returnXML

    End Function

    Public Function DebitReturnXML() As String

        'set Values of above variables here
        Dim returnXML As String = CreateMercuryXML("Debit", "Return")
        Return returnXML

    End Function


    Private Function CreateMercuryXML(ByVal TranType As String, ByVal TranCode As String) As String

        ' OR set Values of above variables here

        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<Memo>" + Memo + "</Memo>")
        'sb.Append("<Frequency>" + Frequency + "</Frequency>")
        'sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")
        sb.Append("<PartialAuth>" + PartialAuth + "</PartialAuth>")

        sb.Append("<Account>")

        ''sb.Append("<Track2>" + Track2 + "</Track2>")                    'In a standard non encrypted swiped request
        ''sb.Append("<Name>" + Name + "</Name>")

        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")                ''In a non encrypted manual request
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")

        'THESE WILL BE USED WHEN WE ARE SENDING ENCRYPTED ACCOUNT INFORMATION
        'sb.Append("<EncryptedFormat>" + EncryptedFormat + "</EncryptedFormat>")         ' for encrypted account data - recommanded
        'sb.Append("<AccountSource>" + AccountSource + "</AccountSource>")
        'sb.Append("<EncryptedBlock>" + EncryptedBlock + "</EncryptedBlock>")
        'sb.Append("<EncryptedKey>" + EncryptedKey + "</EncryptedKey>")

        sb.Append("</Account>")

        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        If TranType = "Debit" And Not TranCode = "Return" Then
            sb.Append("<CashBack>" + CashBack + "</CashBack>")
        End If

        If TranType = "Credit" And (TranCode = "PreAuth" Or TranCode = "PreAuthCapture") Then
            sb.Append("<Authorize>" + Authorize + "</Authorize>")
        End If

        If TranType = "Credit" And TranCode = "PreAuthCapture" Then
            sb.Append("<Gratutiy>" + Gratutiy + "</Gratutiy>")
        End If

        sb.Append("</Amount>")

        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")

        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function

    Public Function CreateMercuryXMLWithRecordNo(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")

        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")
        sb.Append("<Frequency>" + Frequency + "</Frequency>")
        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<Account>")

        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")

        sb.Append("</Account>")

        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("</Amount>")

        If zipcode.Trim() <> "" Then
            sb.Append("<AVS>")
            sb.Append("<Zip>" & zipcode & "</Zip>")
            sb.Append("</AVS>")
        End If

        If cvv.Trim() <> "" Then
            sb.Append("<CVVData>" & cvv & "</CVVData>")
        End If

        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function




    Public Function CreateMercuryXMLVoidSaleByRecordNo(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")


        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")

        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")



        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")
        sb.Append("<Frequency>" + Frequency + "</Frequency>")

        'sb.Append("<Account>")
        'sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        'sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")
        'sb.Append("</Account>")


        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("</Amount>")


        sb.Append("<TranInfo>")

        sb.Append("<AcqRefData>" + ACQRefData + "</AcqRefData>")
        sb.Append("<ProcessData>" + ProcessData + "</ProcessData>")
        sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")

        sb.Append("</TranInfo>")

        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function



    Public Function CreateMercuryXMLReturnByRecordNo(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")


        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<PartialAuth>" + PartialAUth + "</PartialAuth>")

        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")


        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")
        sb.Append("<Frequency>" + Frequency + "</Frequency>")

        'sb.Append("<Account>")
        'sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        'sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")
        'sb.Append("</Account>")


        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("</Amount>")


 
        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function




    Public Function CreateMercuryXMLWithRecordNoafterGenrate(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<PartialAuth>" + PartialAUth + "</PartialAuth>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")
        sb.Append("<Frequency>" + Frequency + "</Frequency>")
        sb.Append("<Memo>" + Memo + "</Memo>")

        'sb.Append("<Account>")
        'sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        'sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")
        'sb.Append("</Account>")


        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("</Amount>")


        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function


    Public Function CreateMercuryPreAuthXMLWithRecordNo(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")

        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")
        sb.Append("<Frequency>" + Frequency + "</Frequency>")
        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<Account>")

        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")

        sb.Append("</Account>")

        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")
        sb.Append("<Authorize>" + Authorize + "</Authorize>")
        sb.Append("</Amount>")


        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function



    Public Function CreateMercuryPreAuthXMLWithExistingRecordNo(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<PartialAuth>" + PartialAUth + "</PartialAuth>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")
        sb.Append("<Frequency>" + Frequency + "</Frequency>")
        sb.Append("<Memo>" + Memo + "</Memo>")


        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")
        sb.Append("<Authorize>" + Authorize + "</Authorize>")
        sb.Append("</Amount>")


        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function




    Public Function CreateMercuryXMLPreAuthCaptureWithRecordNo(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<Frequency>" + Frequency + "</Frequency>")
        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")

        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("<Authorize>" + Authorize + "</Authorize>")
         
        sb.Append("</Amount>")

        sb.Append("<TranInfo>")

        sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")
        sb.Append("<AcqRefData>" + ACQRefData + "</AcqRefData>")

        sb.Append("</TranInfo>")


        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function




    Public Function CreateMercuryXMLSaleAdjustWithRecordNo(ByVal TranType As String, ByVal TranCode As String) As String



        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")


        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")


        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<Frequency>" + Frequency + "</Frequency>")
        sb.Append("<RecordNo>" + RecordNo + "</RecordNo>")

        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")
        sb.Append("<Authorize>" + Authorize + "</Authorize>")
        sb.Append("<Gratuity>" + Gratutiy + "</Gratuity>")

        sb.Append("</Amount>")

        sb.Append("<TranInfo>")

        sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")

        sb.Append("</TranInfo>")

        sb.Append("</Transaction>")
        sb.Append("</TStream>")

 
        Return sb.ToString

    End Function



    Public Sub ExtractResponse(ByVal XMLResponseString As String)

        Dim dsResponse As New DataSet
        Dim dtCmdResponse As New DataTable
        Dim dtTranResponse As New DataTable

        Try
            Dim xDoc = New XmlDocument
            xDoc.LoadXml(XMLResponseString)
            dsResponse.ReadXml(New XmlNodeReader(xDoc))
            If Not (dsResponse Is Nothing) Then
                dtCmdResponse = dsResponse.Tables(0)
                dtTranResponse = dsResponse.Tables(1)
            Else
                dtCmdResponse = Nothing
                dtTranResponse = Nothing
            End If
        Catch ex As Exception

        End Try

        ''Iterate dtResponse and SettingAttribute properties

        'Dim strResponseOrigin As String = ""
        For Each row As DataRow In dtCmdResponse.Rows
            Try
                m_responseOrigin = row("ResponseOrigin").ToString().Replace("'", " ")
                m_dsiXReturnCode = row("DSIXReturnCode").ToString().Replace("'", " ")
                m_cmdStatus = row("CmdStatus").ToString().Replace("'", " ")
                m_textResponse = row("TextResponse").ToString().Replace("'", " ")
            Catch ex As Exception
            End Try
        Next

        'Dim strCaptureStatus As String = ""
        For Each row As DataRow In dtTranResponse.Rows
            Try
                m_captureStatus = row("CaptureStatus").ToString().Replace("'", " ")
                m_cardType = row("CardType").ToString().Replace("'", " ")
                m_authCode = row("AuthCode").ToString().Replace("'", " ")
                m_acqRefData = row("AcqRefData").ToString().Replace("'", " ")
                m_recordNo = row("RecordNo").ToString().Replace("'", " ")
                m_processData = row("ProcessData").ToString().Replace("'", " ")
Response_RefNo = (row("RefNo").ToString().Replace("'", " "))

            Catch ex As Exception
            End Try
        Next

    End Sub

    Public Response_RefNo As String

    'Response Filds with some of Request fileds
    Public m_responseOrigin As String
    Public m_dsiXReturnCode As String
    Public m_cmdStatus As String
    Public m_textResponse As String

    Public m_MerchantID As String
    Public m_acctNo As String
    Public m_expDate As String
    Public m_cardType As String
    Public m_tranCode As String
    Public m_authCode As String
    Public m_captureStatus As String
    Public m_refNo As String
    Public m_invoiceNo As String
    Public m_operatorID As String = "QF"
    Public m_memo As String
    Public m_purchase As String
    Public m_authorize As String
    Public m_acqRefData As String
    Public m_recordNo As String
    Public m_processData As String

    Public m_shiftID As String = "1"
    Public m_terminalName As String = "QF"
    Public m_gratutiy As String
    Public m_cashBack As String
    Public m_partialAuth As String
    Public m_frequency As String

    Public Property ResponseOrigin As String
        Get
            Return m_responseOrigin
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property DSIReturnCode As String
        Get
            Return m_dsiXReturnCode
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property CMDStatus As String
        Get
            Return m_cmdStatus
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property TextResponse As String
        Get
            Return m_textResponse
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property MerchantID() As String
        Get
            Return m_MerchantID
        End Get
        Set(ByVal value As String)
            m_MerchantID = value
        End Set
    End Property

    Public Property AcctNo As String
        Get
            Return m_acctNo
        End Get
        Set(ByVal value As String)
            m_acctNo = value
        End Set
    End Property

    Public Property ExpDate As String
        Get
            Return m_expDate
        End Get
        Set(ByVal value As String)
            m_expDate = value
        End Set
    End Property

    Public Property CardType As String
        Get
            Return m_cardType
        End Get
        Set(ByVal value As String)
            m_cardType = value
        End Set
    End Property

    Public Property TranCode As String
        Get
            Return m_tranCode
        End Get
        Set(ByVal value As String)
            m_tranCode = value
        End Set
    End Property

    Public Property AuthCode As String
        Get
            Return m_authCode
        End Get
        Set(ByVal value As String)
            m_authCode = value
        End Set
    End Property

    Public Property CaptureStatus As String
        Get
            Return m_captureStatus
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property RefNo As String
        Get
            Return m_refNo
        End Get
        Set(ByVal value As String)
            m_refNo = value
        End Set
    End Property

    Public Property InvoiceNo As String
        Get
            Return m_invoiceNo
        End Get
        Set(ByVal value As String)
            m_invoiceNo = value
        End Set
    End Property

    Public Property OperatorID As String
        Get
            Return m_operatorID
        End Get
        Set(ByVal value As String)
            m_operatorID = value
        End Set
    End Property

    Public Property Memo As String
        Get
            Return m_memo
        End Get
        Set(ByVal value As String)
            m_memo = value
        End Set
    End Property

    Public Property Purchase As String
        Get
            Return m_purchase
        End Get
        Set(ByVal value As String)
            m_purchase = value
        End Set
    End Property

    Public Property Authorize As String
        Get
            Return m_authorize
        End Get
        Set(ByVal value As String)
            m_authorize = value
        End Set
    End Property

    Public Property ACQRefData As String
        Get
            Return m_acqRefData
        End Get
        Set(ByVal value As String)
            m_acqRefData = value
        End Set
    End Property

    Public Property RecordNo As String
        Get
            Return m_recordNo
        End Get
        Set(ByVal value As String)
            m_recordNo = value
        End Set
    End Property

    Public Property ProcessData As String
        Get
            Return m_processData
        End Get
        Set(ByVal value As String)
            m_processData = value
        End Set
    End Property

    Public Property PartialAUth As String
        Get
            Return m_partialAuth
        End Get
        Set(ByVal value As String)
            m_partialAuth = value
        End Set
    End Property

    Public Property Frequency As String
        Get
            Return m_frequency
        End Get
        Set(ByVal value As String)
            m_frequency = value
        End Set
    End Property

    Public Property CashBack As String
        Get
            Return m_cashBack
        End Get
        Set(ByVal value As String)
            m_cashBack = value
        End Set
    End Property

    Public Property ShiftID As String
        Get
            Return m_shiftID
        End Get
        Set(ByVal value As String)
            m_shiftID = value
        End Set
    End Property

    Public Property TerminalName As String
        Get
            Return m_terminalName
        End Get
        Set(ByVal value As String)
            m_terminalName = value
        End Set
    End Property

    Public Property Gratutiy As String
        Get
            Return m_gratutiy
        End Get
        Set(ByVal value As String)
            m_gratutiy = value
        End Set
    End Property



    Public Function CreateMercuryXMLCreditSale(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")


        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        
        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<Account>")

        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")

        sb.Append("</Account>")

       
        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        If Authorize.Trim <> "" Then
            sb.Append("<Authorize>" + Authorize + "</Authorize>")
        End If

        If Gratutiy.Trim <> "" Then
            sb.Append("<Gratutiy>" + Gratutiy + "</Gratutiy>")
        End If


        sb.Append("</Amount>")


        sb.Append("<AVS>")

        sb.Append("<Zip>" & zipcode & "</Zip>")
        sb.Append("</AVS>")

        sb.Append("<CVVData>" & cvv & "</CVVData>")


        If AuthCode.Trim <> "" Then
            sb.Append("<TranInfo>")
            sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")
            sb.Append("</TranInfo>")
        End If
        
        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function

    Public cvv As String = ""
    Public Address As String = ""
    Public zipcode As String = ""

    Public Function CreateMercuryXMLCreditAdjust(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")


        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        
        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<Account>")

        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")

        sb.Append("</Account>")

        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")
        sb.Append("<Authorize>" + Authorize + "</Authorize>")
        sb.Append("<Gratuity>" + Gratutiy + "</Gratuity>")

        sb.Append("</Amount>")

        sb.Append("<TranInfo>")

        sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")

        sb.Append("</TranInfo>")

        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function


    Public Function CreateMercuryXMLVoidSale(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")
        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<Memo>" + Memo + "</Memo>")
        sb.Append("<Account>")
        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")
        sb.Append("</Account>")
        sb.Append("<Amount>")
        sb.Append("<Purchase>" + Purchase + "</Purchase>")
        sb.Append("</Amount>")
        sb.Append("<TranInfo>")
        sb.Append("<AcqRefData>" + ACQRefData + "</AcqRefData>")
        sb.Append("<ProcessData>" + ProcessData + "</ProcessData>")
        sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")
        sb.Append("</TranInfo>")
        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function


    Public Function CreateMercuryXMLVoidSaleNonReversal(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")
        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<Memo>" + Memo + "</Memo>")
        sb.Append("<Account>")
        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")
        sb.Append("</Account>")
        sb.Append("<Amount>")
        sb.Append("<Purchase>" + Purchase + "</Purchase>")
        sb.Append("</Amount>")
        sb.Append("<TranInfo>")
         
        sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")
        sb.Append("</TranInfo>")
        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function





    Public Function CreateMercuryPreAuth(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")

        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")

        
        sb.Append("<Memo>" + Memo + "</Memo>")

        sb.Append("<Account>")

        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")

        sb.Append("</Account>")

        sb.Append("<Amount>")
         
        sb.Append("<Authorize>" + Purchase + "</Authorize>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("</Amount>")


        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function



    Public Function CreateMercuryXMLPreAuthCapture(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")
        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")
        sb.Append("<Memo>" + Memo + "</Memo>")


        sb.Append("<Account>")

        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")

        sb.Append("</Account>")

         
        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("<Authorize>" + Authorize + "</Authorize>")

        If Gratutiy.Trim <> "" Then
            sb.Append("<Gratutiy>" + Gratutiy + "</Gratutiy>")
        End If


        sb.Append("</Amount>")

        sb.Append("<TranInfo>")

        sb.Append("<AuthCode>" + AuthCode + "</AuthCode>")

        If ACQRefData.Trim <> "" Then
            sb.Append("<AcqRefData>" + ACQRefData + "</AcqRefData>")
        End If


        sb.Append("</TranInfo>")


        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function



    Public Function CreateMercuryXMLReturn(ByVal TranType As String, ByVal TranCode As String) As String


        Dim sb As New StringBuilder
        sb.Append("<?xml version=""1.0""?>")
        sb.Append("<TStream>")
        sb.Append("<Transaction>")

        sb.Append("<MerchantID>" + MerchantID + "</MerchantID>")


        sb.Append("<OperatorID>" + OperatorID + "</OperatorID>")
        sb.Append("<TerminalName>" + TerminalName + "</TerminalName>")
        sb.Append("<ShiftID>" + ShiftID + "</ShiftID>")

        sb.Append("<TranType>" + TranType + "</TranType>")
        sb.Append("<TranCode>" + TranCode + "</TranCode>")
         
        sb.Append("<InvoiceNo>" + InvoiceNo + "</InvoiceNo>")
        sb.Append("<RefNo>" + RefNo + "</RefNo>")


        sb.Append("<Memo>" + Memo + "</Memo>")
         
        sb.Append("<Account>")
        sb.Append("<AcctNo>" + AcctNo + "</AcctNo>")
        sb.Append("<ExpDate>" + ExpDate + "</ExpDate>")
        sb.Append("</Account>")


        sb.Append("<Amount>")

        sb.Append("<Purchase>" + Purchase + "</Purchase>")

        sb.Append("</Amount>")



        sb.Append("</Transaction>")
        sb.Append("</TStream>")


        Return sb.ToString

    End Function



End Class
