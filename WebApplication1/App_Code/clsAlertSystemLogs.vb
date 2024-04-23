Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsAlertSystemLogs


    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public LocationID As String
    Public AlertID As Integer
    Public AlertName As String
    Public AlertMessage As String
    Public AlertType As String
    Public OrderStatus As String
    Public OrderNumber As String

    Public Inlinenumber As Integer

    '  CREATE TABLE [dbo].[AlertInformationLogs](
    '[CompanyID] [nvarchar](50) NOT NULL,
    '[DivisionID] [nvarchar](50) NOT NULL,
    '[DepartmentID] [nvarchar](50) NOT NULL,
    '[LocationID] [nvarchar](50) NOT NULL,
    '[AlertID] [int] NOT NULL,
    '[AlertName] [nvarchar](255) NULL,
    '[AlertMessage] [nvarchar](max) NULL,
    '[AlertType] [nvarchar](255) NULL,
    '[OrderStatus] [nvarchar](50) NULL,
    '[SendAt] [datetime] NULL,
    '[Inlinenumber] [int] IDENTITY(1,1) NOT NULL,
    '[OrderNumber] [nvarchar](50) NOT NULL
    ') ON [PRIMARY]



    '    CREATE TABLE dbo.EmployeeAlertLogs(
    ' CompanyID nvarchar(50) NOT NULL,
    ' DivisionID nvarchar(50) NOT NULL,
    ' DepartmentID nvarchar(50) NOT NULL,
    ' DetailInlinenumber int IDENTITY(1,1) PRIMARY KEY ,
    ' Inlinenumber int NOT NULL,
    ' EmployeeID nvarchar(50)  NULL,
    ' EmployeePhone nvarchar(50)  NULL,
    ' EmployeeEmail nvarchar(255)  NULL, 
    ')

    Public EmployeeEmail As String
    Public EmployeePhone As String

    Public Function InsertAlertLogsDetails() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  [EmployeeAlertLogs] ([CompanyID],[DivisionID],[DepartmentID],Inlinenumber,EmployeeID,EmployeePhone,EmployeeEmail,[AlertID] " _
        & " ) values(@f1,@f2,@f3,@Inlinenumber,@EmployeeID,@EmployeePhone,@EmployeeEmail,@AlertID)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            com.Parameters.Add(New SqlParameter("@Inlinenumber", SqlDbType.Int)).Value = Me.Inlinenumber
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@EmployeePhone", SqlDbType.NVarChar)).Value = Me.EmployeePhone
            com.Parameters.Add(New SqlParameter("@EmployeeEmail", SqlDbType.NVarChar)).Value = Me.EmployeeEmail

            com.Parameters.Add(New SqlParameter("@AlertID", SqlDbType.Int)).Value = Me.AlertID


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function




    Public Function InsertAlertLogs() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  [AlertInformationLogs] ([CompanyID],[DivisionID],[DepartmentID],[LocationID],AlertID,AlertName,AlertMessage,AlertType,OrderStatus,OrderNumber " _
        & " ) values(@f1,@f2,@f3,@f4,@AlertID,@AlertName,@AlertMessage,@AlertType,@OrderStatus,@OrderNumber)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.LocationID
            com.Parameters.Add(New SqlParameter("@AlertID", SqlDbType.Int)).Value = Me.AlertID
            com.Parameters.Add(New SqlParameter("@AlertName", SqlDbType.NVarChar)).Value = Me.AlertName
            com.Parameters.Add(New SqlParameter("@AlertMessage", SqlDbType.NVarChar)).Value = Me.AlertMessage
            com.Parameters.Add(New SqlParameter("@AlertType", SqlDbType.NVarChar)).Value = Me.AlertType
            com.Parameters.Add(New SqlParameter("@OrderStatus", SqlDbType.NVarChar)).Value = Me.OrderStatus
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar)).Value = Me.OrderNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Public Function AlertInformationLogscount() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from AlertInformationLogs where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and AlertID=@AlertID and OrderNumber=@OrderNumber  Order by [SendAt] Desc "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@AlertID", SqlDbType.Int)).Value = Me.AlertID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar)).Value = Me.OrderNumber

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function



    Public Function AlertNotificationList() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select EmployeeID, SendSMS, SendEmail from AlertNotificationList where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and AlertID=@AlertID"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@AlertID", SqlDbType.Int)).Value = Me.AlertID


            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function

    '    CREATE TABLE AlertNotificationList (
    '	CompanyID nvarchar(50) NOT NULL,
    '	DivisionID nvarchar(50) NOT NULL,
    '	DepartmentID nvarchar(50) NOT NULL,
    '	LocationID nvarchar(50) NOT NULL,
    '	RowID int IDENTITY(1,1) PRIMARY KEY,
    '	AlertID nvarchar(50),
    '	EmployeeID nvarchar(50),
    '	SendSMS bit,
    '	SendEmail bit,
    '	CONSTRAINT [UK_AlertNotificationList] UNIQUE CLUSTERED (CompanyID ASC,DivisionID ASC,DepartmentID ASC,LocationID ASC, AlertID, EmployeeID)
    ')

    Dim EmployeeID As String

    Public Function PayrollEmployees() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select [EmployeeID],[EmployeePhone],[EmployeeEmailAddress]  from PayrollEmployees where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and EmployeeID=@EmployeeID"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@EmployeeID", SqlDbType.NVarChar)).Value = Me.EmployeeID
            da.SelectCommand = com
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function
    '  SELECT [CompanyID]
    '    ,[DivisionID]
    '    ,[DepartmentID]
    '    ,[EmployeeID]

    '    ,[EmployeePhone]

    '    ,[EmployeeEmailAddress] 
    'FROM [Enterprise].[dbo].[PayrollEmployees]
    Public smsmessage As String = ""
    Public emailsubject As String = ""

    Public Function sendalert(ByVal message As String, Optional ByVal assignedto As String = "", Optional ByVal salesperson As String = "")

        Try

            Dim dtCompanyEmailAddress As New DataTable
            dtCompanyEmailAddress = GetCompanyEmailAddress()

            If dtCompanyEmailAddress.Rows.Count <> 0 Then
                CompanyEmailAddress = dtCompanyEmailAddress.Rows(0)("CompanyEmail")
            End If
        Catch ex As Exception

        End Try

        Dim dt_CheckAlertAlertAcknowledg As New DataTable
        dt_CheckAlertAlertAcknowledg = CheckAlertAlertAcknowledg()

        If dt_CheckAlertAlertAcknowledg.Rows.Count <> 0 Then
            Return False
        End If


        Dim MobileNumber As String
        Dim obj As New clsSMSsentLogs
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        obj.CustomerID = ""
        obj.ProcessAmount = 0

        Dim dt_AlertNotificationList As New DataTable
        dt_AlertNotificationList = AlertNotificationList()

        If assignedto <> "" Then
            Dim dr As DataRow
            dr = dt_AlertNotificationList.NewRow()
            dr("EmployeeID") = assignedto
            dr("SendSMS") = True
            dr("SendEmail") = True
            dt_AlertNotificationList.Rows.Add(dr)
        End If

        If salesperson <> "" Then
            Dim dr As DataRow
            dr = dt_AlertNotificationList.NewRow()
            dr("EmployeeID") = salesperson
            dr("SendSMS") = True
            dr("SendEmail") = True
            dt_AlertNotificationList.Rows.Add(dr)
        End If

        If dt_AlertNotificationList.Rows.Count <> 0 Then
            Dim n As Integer

            For n = 0 To dt_AlertNotificationList.Rows.Count - 1
                EmployeeID = ""
                Dim SendSMS As Boolean = False
                Dim SendEmail As Boolean = False

                Try
                    EmployeeID = dt_AlertNotificationList.Rows(n)("EmployeeID")
                Catch ex As Exception

                End Try
                Try
                    SendSMS = dt_AlertNotificationList.Rows(n)("SendSMS")
                Catch ex As Exception

                End Try
                Try
                    SendEmail = dt_AlertNotificationList.Rows(n)("SendEmail")
                Catch ex As Exception

                End Try

                ' Dim EmployeePhone As String = ""
                Dim EmployeeEmailAddress As String = ""

                Dim dt_Employee As New DataTable
                dt_Employee = PayrollEmployees()

                If dt_Employee.Rows.Count <> 0 Then
                    Try
                        EmployeePhone = dt_Employee.Rows(0)("EmployeePhone")
                    Catch ex As Exception

                    End Try
                    Try
                        EmployeeEmailAddress = dt_Employee.Rows(0)("EmployeeEmailAddress")
                    Catch ex As Exception

                    End Try

                    If SendSMS Then

                        Dim dt_CheckSMSAlertcount As New DataTable
                        dt_CheckSMSAlertcount = CheckSMSAlertcount()

                        If dt_CheckSMSAlertcount.Rows.Count = 0 Then
                            obj.ProcessType = "SMS sending for Order Alert on Mobile No=" & EmployeePhone
                            obj.ProcessDetails = smsmessage
                            obj.InsertSMSLogs()
                            clsSMSGT.SendSMS(smsmessage, EmployeePhone)

                            Me.EmployeePhone = EmployeePhone
                            Me.EmployeeEmail = ""
                            Me.InsertAlertLogsDetails()
                        End If

                    End If

                    If SendEmail And EmployeeEmailAddress.Trim <> "" Then

                        message = message.Replace("@", "<a href='https://secure.quickflora.com/EnterpriseASP/scripts/MarkAlertAcknowledge.aspx?InlineNumber=" & Me.Inlinenumber & "&EmployeeID=" & EmployeeID & "' > Click here to acknowledge receiving this alert.</a>")

                        Dim subject As String = emailsubject
                        Dim mailcontent As String = message
                        Dim frommail As String = ""
                        frommail = CompanyEmailAddress
                        Dim tomail As String = EmployeeEmailAddress
                        Dim objmail As New clsCreditCardChargeOnRebook
                        objmail.CompanyID = Me.CompanyID
                        objmail.DivisionID = Me.DivisionID
                        objmail.DepartmentID = Me.DepartmentID

                        objmail.EmailSending(subject, mailcontent, frommail, tomail)


                        Me.EmployeePhone = ""
                        Me.EmployeeEmail = EmployeeEmailAddress
                        Me.InsertAlertLogsDetails()

                    End If

                End If

            Next

        End If





    End Function


    Public Function CheckSMSAlertcount() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select AlertID from [EmployeeAlertLogs] where  CompanyID='" & Me.CompanyID & "' And DivisionID='" & Me.DivisionID & "' And DepartmentID='" & Me.DepartmentID & "' and  AlertID =" & Me.AlertID & " and EmployeePhone ='" & Me.EmployeePhone & "'"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function




    Public Function CheckAlertAlertAcknowledg() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select AlertID from [AlertInformationLogs] where  CompanyID='" & Me.CompanyID & "' And DivisionID='" & Me.DivisionID & "' And DepartmentID='" & Me.DepartmentID & "' And OrderNumber='" & Me.OrderNumber & "'  and  AlertID =" & Me.AlertID & " and isnull([AlertAcknowledgeby],'') <> '' "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function


    Dim CompanyEmailAddress As String = "support@quickflora.com"

    Public Function GetCompanyEmailAddress() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "Select CompanyEmail from companies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            da.SelectCommand = com
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function


End Class
