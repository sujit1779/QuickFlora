Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient


Public Class clsCompanyLocalTime

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""


    Public Function populate(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As Date
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT Isnull(GMTOffset,'') as GMTOffset FROM Companies WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DepartmentID & "' AND DivisionID='" & DivisionID & "'"
        Dim Cmd As New SqlCommand(sqlStr, ConString)
        Dim da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        Dim GMTime As String = ""
        Dim HR As String = ""
        Dim MN As String = ""
        Dim sign As String = ""

        Dim datePST As New Date
        datePST = populatedefault("DEFAULT", "DEFAULT", "DEFAULT")

        If CompanyID = "DEFAULT" Then
            Return datePST
        End If



        If dt.Rows.Count <> 0 Then
            Dim GMToffset As String = ""
            Try
                GMToffset = dt.Rows(0)("GMTOffset")
            Catch ex As Exception

            End Try
            If GMToffset <> "" Then

                Try

                    sign = GMToffset.Substring(0, 1)


                    Dim TRACKarray As String()
                    TRACKarray = GMToffset.Split(":")
                    If TRACKarray.Length = 2 Then
                        HR = TRACKarray(0)
                        HR = HR.Substring(1, HR.Length - 1)
                        MN = TRACKarray(1)
                    End If

                Catch ex As Exception

                End Try

                GMTime = "sign " & sign & " HR " & HR & "  MN " & MN



                If sign = "+" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR
                    Dim MN_sign As Integer = 0
                    MN_sign = MN

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If

                If sign = "-" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR * -1
                    Dim MN_sign As Integer = 0
                    MN_sign = MN * -1

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If

                

            End If


        End If

        Return datePST

    End Function



    Public Function populatedefault(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String) As Date
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT Isnull(GMTOffset,'') as GMTOffset FROM Companies WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DepartmentID & "' AND DivisionID='" & DivisionID & "'"
        Dim Cmd As New SqlCommand(sqlStr, ConString)
        Dim da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        Dim GMTime As String = ""
        Dim HR As String = ""
        Dim MN As String = ""
        Dim sign As String = ""

        Dim datePST As New Date
        datePST = DateTime.Now

        If dt.Rows.Count <> 0 Then
            Dim GMToffset As String = ""
            Try
                GMToffset = dt.Rows(0)("GMTOffset")
            Catch ex As Exception

            End Try
            If GMToffset <> "" Then

                Try

                    sign = GMToffset.Substring(0, 1)


                    Dim TRACKarray As String()
                    TRACKarray = GMToffset.Split(":")
                    If TRACKarray.Length = 2 Then
                        HR = TRACKarray(0)
                        HR = HR.Substring(1, HR.Length - 1)
                        MN = TRACKarray(1)
                    End If

                Catch ex As Exception

                End Try

                GMTime = "sign " & sign & " HR " & HR & "  MN " & MN



                If sign = "-" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR
                    Dim MN_sign As Integer = 0
                    MN_sign = MN

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If

                If sign = "+" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR * -1
                    Dim MN_sign As Integer = 0
                    MN_sign = MN * -1

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If



            End If


        End If

        Return datePST

    End Function





    Public Function populateCMPTime(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal dte As Date) As Date
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT Isnull(GMTOffset,'') as GMTOffset FROM Companies WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DepartmentID & "' AND DivisionID='" & DivisionID & "'"
        Dim Cmd As New SqlCommand(sqlStr, ConString)
        Dim da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        Dim GMTime As String = ""
        Dim HR As String = ""
        Dim MN As String = ""
        Dim sign As String = ""

        Dim datePST As New Date
        datePST = populatedefaultCMPTime("DEFAULT", "DEFAULT", "DEFAULT", dte)

        If CompanyID = "DEFAULT" Then
            Return datePST
        End If



        If dt.Rows.Count <> 0 Then
            Dim GMToffset As String = ""
            Try
                GMToffset = dt.Rows(0)("GMTOffset")
            Catch ex As Exception

            End Try
            If GMToffset <> "" Then

                Try

                    sign = GMToffset.Substring(0, 1)


                    Dim TRACKarray As String()
                    TRACKarray = GMToffset.Split(":")
                    If TRACKarray.Length = 2 Then
                        HR = TRACKarray(0)
                        HR = HR.Substring(1, HR.Length - 1)
                        MN = TRACKarray(1)
                    End If

                Catch ex As Exception

                End Try

                GMTime = "sign " & sign & " HR " & HR & "  MN " & MN



                If sign = "+" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR
                    Dim MN_sign As Integer = 0
                    MN_sign = MN

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If

                If sign = "-" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR * -1
                    Dim MN_sign As Integer = 0
                    MN_sign = MN * -1

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If

            End If

        End If
        Return datePST
    End Function



    Public Function populatedefaultCMPTime(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal dte As Date) As Date
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT Isnull(GMTOffset,'') as GMTOffset FROM Companies WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DepartmentID & "' AND DivisionID='" & DivisionID & "'"
        Dim Cmd As New SqlCommand(sqlStr, ConString)
        Dim da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        Dim GMTime As String = ""
        Dim HR As String = ""
        Dim MN As String = ""
        Dim sign As String = ""

        Dim datePST As New Date
        datePST = dte ' DateTime.Now

        If dt.Rows.Count <> 0 Then
            Dim GMToffset As String = ""
            Try
                GMToffset = dt.Rows(0)("GMTOffset")
            Catch ex As Exception

            End Try
            If GMToffset <> "" Then

                Try

                    sign = GMToffset.Substring(0, 1)


                    Dim TRACKarray As String()
                    TRACKarray = GMToffset.Split(":")
                    If TRACKarray.Length = 2 Then
                        HR = TRACKarray(0)
                        HR = HR.Substring(1, HR.Length - 1)
                        MN = TRACKarray(1)
                    End If

                Catch ex As Exception

                End Try

                GMTime = "sign " & sign & " HR " & HR & "  MN " & MN



                If sign = "-" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR
                    Dim MN_sign As Integer = 0
                    MN_sign = MN

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If

                If sign = "+" Then
                    Dim HR_sign As Integer = 0
                    HR_sign = HR * -1
                    Dim MN_sign As Integer = 0
                    MN_sign = MN * -1

                    datePST = datePST.AddHours(HR_sign)
                    datePST = datePST.AddMinutes(MN_sign)


                End If



            End If


        End If

        Return datePST

    End Function





End Class
