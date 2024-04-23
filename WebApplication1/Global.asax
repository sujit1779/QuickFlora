<%@ Application Language="VB" %>
<%@ Import Namespace="EnterpriseClient.Core" %>
<%@ Import Namespace="EnterpriseCommon.Configuration" %>
<%@ Import Namespace="EnterpriseASPClient.Core" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.IO" %>



<%@ Import Namespace="System.Text" %>
<%@ Import  Namespace="Amazon" %>
<%@ Import  Namespace="Amazon.EC2" %>
<%@ Import  Namespace="Amazon.EC2.Model" %>
<%@ Import  Namespace="Amazon.SimpleDB" %>
<%@ Import  Namespace="Amazon.SimpleDB.Model" %>
<%@ Import  Namespace="Amazon.S3" %>
<%@ Import  Namespace="Amazon.S3.Model" %>
<%@ Import  Namespace="Amazon.SimpleEmail" %>
<%@ Import  Namespace="Amazon.SimpleEmail.Model" %>


<script RunAt="server">

    Public Sub EmailSendingWithoutBcc(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String)
        ''
        'REMOVE EXIT SUB FROM FUNCTION BEFORE USING IT
        ''
        'Exit Sub
        ''
        'REMOVE EXIT SUB FROM FUNCTION BEFORE USING IT
        ''

        Dim mMailMessage As New MailMessage()

        ' Set the sender address of the mail message
        mMailMessage.From = New MailAddress(FromAddress)
        ' Set the recepient address of the mail message


        Try

        Catch ex As Exception

        End Try
        mMailMessage.To.Add(New MailAddress(ToAddress))
        mMailMessage.To.Add(New MailAddress("imy@quickflora.com"))
        mMailMessage.To.Add(New MailAddress("gaurav@quickflora.com"))
        mMailMessage.To.Add(New MailAddress("mo@quickflora.com"))

        ' Set the subject of the mail message
        mMailMessage.Subject = OrderPlacedSubject.ToString()

        ' Set the body of the mail message
        mMailMessage.Body = OrderPlacedContent.ToString()

        ' Set the format of the mail message body as HTML
        mMailMessage.IsBodyHtml = True

        ' Set the priority of the mail message to normal
        mMailMessage.Priority = MailPriority.Normal

        ' Instantiate a new instance of SmtpClient
        Dim smtp As New System.Net.Mail.SmtpClient()
        smtp.Host = "8.3.16.126"

        Try
            'smtp.Send(mMailMessage)
            newmailsending(mMailMessage)
        Catch ex As Exception

        End Try





    End Sub





    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub


    Public Sub UpdateErrorIPDB(ByVal IPAdd As String, ByVal VisitURL As String, ByVal Errors As String)
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("AddErrorIPtoDB", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim PIPAdd As New SqlParameter("@IPAdd", Data.SqlDbType.NVarChar)
        PIPAdd.Value = IPAdd
        myCommand.Parameters.Add(PIPAdd)

        Dim PVisitURL As New SqlParameter("@VisitURL", Data.SqlDbType.NVarChar)
        PVisitURL.Value = VisitURL
        myCommand.Parameters.Add(PVisitURL)

        Dim PjsonResponse As New SqlParameter("@Error", Data.SqlDbType.NVarChar)
        PjsonResponse.Value = Errors
        myCommand.Parameters.Add(PjsonResponse)


        myCon.Open()
        myCommand.ExecuteNonQuery()
        myCon.Close()
    End Sub




    Public Function GETErrorIPDB(ByVal IPAdd As String) As DataTable
        Dim ConnectionString As String = ""

        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim dt As New DataTable
        Try


            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("GETErrorIPDB", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim PIPAdd As New SqlParameter("@IPAdd", Data.SqlDbType.NVarChar)
            PIPAdd.Value = IPAdd
            myCommand.Parameters.Add(PIPAdd)

            Dim adapter As New SqlDataAdapter(myCommand)

            adapter.Fill(dt)
            'ConString.Close()


        Catch ex As Exception
            EmailSendingWithoutBcc("Error IP Load global config", ex.Message & "<br><br><br>" & Context.Request.Url.ToString(), "support@quickflora.com", "gaurav@quickflora.com")
        Finally
            'CheckConn(ConString)
        End Try
        Return dt
    End Function



    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)

        Dim context As HttpContext = HttpContext.Current

        Dim IP As String = Request.ServerVariables("REMOTE_ADDR")
        Dim dtIPnew As New DataTable
        dtIPnew = GETErrorIPDB(IP)

        Dim n As Integer = 0
        For n = 0 To dtIPnew.Rows.Count - 1
            ' Use a tab to indent each line of the file.
            If IP = dtIPnew.Rows(n)("IPAdd") Then
                Dim subject As String = ""
                Dim EmailContent As String = ""
                Dim nurl As String = Request.Url.ToString()
                EmailContent = nurl
                subject = "Rejected IP IP From POM | " & IP '& "|  " & CompanyID.Trim()
                EmailSendingWithoutBcc(subject, EmailContent, "errors@quickflora.com", "errors@quickflora.com")
                ' Throw New System.Exception(String.Format("Rejected IP ({0})  URL ({1}).", IP, nurl))
                'GBCHECK = False
            End If

        Next

        Dim lines2 As String() = System.IO.File.ReadAllLines("D:\Webapps\QuickFloraFrontEnd\keywords.txt")
        ' Display the file contents by using a foreach loop.
        Dim n2 As Integer = 1
        Dim queryString As String = context.Request.ServerVariables("QUERY_STRING")
        Dim GBCHECK As Boolean = True
        Dim country As String = ""
        Dim jsonResponse As String = ""



        If String.IsNullOrEmpty(queryString) = False Then
            If queryString.Length > 500 And context.Request.Url.ToString().IndexOf("ExcelBatchPO.aspx") = (-1) Then
                'Throw New SQLInjectionException(String.Format("Unexpected 'QUERY_STRING' length ({0}).", queryString))
                Dim Address As String = Request.ServerVariables("REMOTE_ADDR")
                Dim subject As String = ""
                Dim EmailContent As String = ""
                Dim nurl As String = Request.Url.ToString()
                EmailContent = nurl
                subject = "Greater than 300 IP | " & Address '& "|  " & CompanyID.Trim()
                EmailSendingWithoutBcc(subject, EmailContent, "errors@quickflora.com", "errors@quickflora.com")
                UpdateErrorIPDB(Address, nurl, "Greater than 300  'QUERY_STRING'")
                Throw New System.Exception(String.Format("Unexpected Error ({0}).", ""))
            End If
        End If


        Dim lines3 As String() = System.IO.File.ReadAllLines("D:\Webapps\QuickFloraFrontEnd\keywordsExcp.txt")
        Dim n3 As Integer = 1

        Dim excp As Boolean = False

        For Each line As String In lines2
            excp = False

            For Each _line As String In lines3
                queryString = queryString.ToLower
                _line = _line.ToLower
                'Throw New System.Exception(String.Format("Unexpected 'QUERY_STRING' length ({0}) , ({1}).", _line, queryString))
                If queryString.IndexOf(_line) <> (-1) Then
                    excp = True
                    Exit For
                End If
            Next

            If excp Then
                Exit For
            End If

            ' Throw New System.Exception(String.Format("Unexpected 'QUERY_STRING' length ({0}).", queryString))     
            ' Throw New System.Exception(String.Format("Unexpected 'QUERY_STRING' length ({0}).", line))
            queryString = queryString.ToLower
            line = line.ToLower
            If context.Request.Url.ToString().IndexOf("ajax") <> (-1) Then
                Continue For
            End If
            If context.Request.Url.ToString().IndexOf("ExcelBatchPO.aspx") <> (-1) Then
                Continue For
            End If

            If queryString.IndexOf("ajax") <> (-1) Then
                Continue For
            End If


            If line = "exec" Then
                If queryString.IndexOf("executivebaskets77057") <> (-1) Then
                    Continue For
                End If
            End If
            If line = "update" Then
                If queryString.IndexOf("ajaxitemsupdate") <> (-1) Then
                    Continue For
                End If
            End If
            If line = "update" Then
                If queryString.IndexOf("ajaxdeliverydateupdate") <> (-1) Then
                    Continue For
                End If
            End If
            If line = "(" Or line = ")" Then

                Continue For

            End If

            If queryString.IndexOf(line) <> (-1) Then

                'Throw New SQLInjectionException(String.Format("Unexpected T-SQL keyword ('{0}') has been detected ({1})", keyword, queryString))
                Dim Address As String = context.Request.ServerVariables("REMOTE_ADDR")
                Dim subject As String = ""
                Dim EmailContent As String = ""
                Dim nurl As String = context.Request.Url.ToString()
                EmailContent = nurl & "<br>" & "<br>" & "<br><b>" & String.Format("Unexpected T-SQL keyword ('{0}') has been detected ({1})", line, queryString) & "</b><br>" & "<br>" & jsonResponse & "<br>"
                subject = "Unexpected 'QUERY_STRING' T-SQL keyword IP | " & Address '& "|  " & CompanyID.Trim()
                EmailSendingWithoutBcc(subject, EmailContent, "errors@quickflora.com", "errors@quickflora.com")
                UpdateErrorIPDB(Address, nurl, "Unexpected 'QUERY_STRING' T-SQL keyword IP | " & Address)
                Throw New System.Exception(String.Format("Unexpected error ('{0}') has been detected ({1})", Address, nurl))
            End If

        Next


    End Sub

    Public Sub Application_End()
        Dim runtime As HttpRuntime = CType(GetType(System.Web.HttpRuntime).InvokeMember( _
            "_theRuntime", BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.GetField, _
            Nothing, Nothing, Nothing), HttpRuntime)
        If runtime Is Nothing Then
            Return
        End If
        Dim shutDownMessage As String = CType(runtime.GetType.InvokeMember( _
            "_shutDownMessage", BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField, _
            Nothing, runtime, Nothing), String)
        Dim shutDownStack As String = CType(runtime.GetType.InvokeMember( _
            "_shutDownStack", BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField, _
            Nothing, runtime, Nothing), String)
        If Not EventLog.SourceExists("EnterpriseASP") Then
            EventLog.CreateEventSource("EnterpriseASP", "Application")
        End If
        Dim log As EventLog = New EventLog
        log.Source = "EnterpriseASP"
        log.WriteEntry(String.Format( _
            vbCrLf & vbCrLf & "_shutDownMessage={0}" & _
            vbCrLf & vbCrLf & "_shutDownStack={1}", shutDownMessage, shutDownStack), _
            EventLogEntryType.Error)
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)



        Try





            Dim CompanyID As String = ""
            Dim DivisionID As String = ""
            Dim DepartmentID As String = ""
            Dim EmployeeID As String = ""


            Dim filters As EnterpriseCommon.Core.FilterSet
            filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
            CompanyID = filters!CompanyID
            DivisionID = filters!DivisionID
            DepartmentID = filters!DepartmentID
            EmployeeID = filters!EmployeeID







            Dim objCompany As New DAL.CustomOrder()
            Dim CompanyName As String = ""
            Dim CompanyAddress1 As String = ""
            Dim CompanyCity As String = ""
            Dim CompanyState As String = ""
            Dim CompanyZip As String = ""
            Dim CompanyCountry As String = ""
            Dim CompanyPhone As String = ""
            Dim CompanyFax As String = ""
            Dim CompanyEmail As String = ""
            Dim rs As SqlDataReader

            rs = objCompany.PopulateCompanyLogo(CompanyID, DepartmentID, DivisionID)

            If rs.HasRows = True Then

                While rs.Read()
                    CompanyName = rs("CompanyName").ToString()
                    CompanyAddress1 = rs("CompanyAddress1").ToString()
                    CompanyCity = rs("CompanyCity").ToString()
                    CompanyState = rs("CompanyState").ToString()

                    CompanyZip = rs("CompanyZip").ToString()
                    CompanyCountry = rs("CompanyCountry").ToString()
                    CompanyPhone = rs("CompanyPhone").ToString()
                    CompanyFax = rs("CompanyFax").ToString()
                    CompanyEmail = rs("CompanyEmail").ToString()

                End While
            End If

            Dim Address As String = Request.ServerVariables("REMOTE_ADDR")
            Dim dt As String = DateTime.Now.ToString()




            ' New Code Added 24- Sep-2007-----------------------------------------
            'Get exception details
            Dim ex As Exception = HttpContext.Current.Server.GetLastError()

            If TypeOf ex Is HttpUnhandledException AndAlso ex.InnerException IsNot Nothing Then
                ex = ex.InnerException
            End If


            If ex IsNot Nothing Then
                Try
                    'Email the administrator with information that an error has occurred
                    '!!! UPDATE THIS VALUE TO YOUR EMAIL ADDRESS
                    Const ToAndFromAddress As String = "errors@sunflowertechnologies.com"


                    '(1) Create the MailMessage instance
                    Dim mm As New System.Net.Mail.MailMessage(ToAndFromAddress, ToAndFromAddress)
                    ''mm.Bcc.Add("errors@sunflowertechnologies.com")
                    mm.Bcc.Add("alex@quickflora.com")

                    Dim Errtest As String = "<b>An error occurred in " & CompanyName & vbCrLf & " <br>Please contact " & _
                                        "the adminstrator with the following information:   </b><br><br>" & CompanyName & vbCrLf & "<br>" & CompanyAddress1 & vbCrLf & "<br>" & CompanyCity & " " & CompanyState & " " & CompanyZip & " " & CompanyCountry & "<br> Phone: " & CompanyPhone & "<br>Fax: " & CompanyFax & "<br>Email: " & CompanyEmail & "<br>Employee ID: " & EmployeeID & " <br><br>IP Address: " & Address & "<br><br>Date: " & dt & "<br>"

                    Dim IP As String = ConfigurationManager.AppSettings("ipaddress").ToString()
                    Dim IPAdd As String = IP
                    Dim newlinestr As String = "<br><br>"

                    '(2) Assign the MailMessage's properties
                    mm.Subject = "An exception occurred! on Production Server "
                    mm.Body = String.Format("Message: {1}{0}<b>Message: </b>{2}{0} <b>Stack Trace</b>({4}):{0}{3}", newlinestr, Errtest, ex.Message, ex.StackTrace, IPAdd)
                    mm.IsBodyHtml = True


                    '(3) Create the SmtpClient object
                    Dim smtp As New System.Net.Mail.SmtpClient

                    '(4) Send the MailMessage (will use the Web.config settings)
                    'smtp.Send(mm)

                    newmailsending(mm)

                Catch ex1 As Exception
                    'Whoops, some problem sending email!
                    'Just send the user onto CustomErrorPage.aspx...


                End Try
            End If

        Catch ex2 As Exception

            'senderrormail()



        End Try


    End Sub


    Public Sub senderrormail()

        ' New Code Added 24- Sep-2007-----------------------------------------
        'Get exception details
        Dim ex As Exception = System.Web.HttpContext.Current.Server.GetLastError()

        If TypeOf ex Is HttpUnhandledException AndAlso ex.InnerException IsNot Nothing Then
            ex = ex.InnerException
        End If


        If ex IsNot Nothing Then
            Try
                'Email the administrator with information that an error has occurred
                '!!! UPDATE THIS VALUE TO YOUR EMAIL ADDRESS
                Const ToAndFromAddress As String = "errors@sunflowertechnologies.com"


                '(1) Create the MailMessage instance
                Dim mm As New System.Net.Mail.MailMessage(ToAndFromAddress, ToAndFromAddress)
                ''mm.Bcc.Add("errors@sunflowertechnologies.com")
                mm.Bcc.Add("alex@quickflora.com")

                Dim Errtest As String = "Company Information not Present" '<b>An error occurred in " & CompanyName & vbCrLf & " <br>Please contact " & _
                '"the adminstrator with the following information:   </b><br><br>" & CompanyName & vbCrLf & "<br>" & CompanyAddress1 & vbCrLf & "<br>" & CompanyCity & " " & CompanyState & " " & CompanyZip & " " & CompanyCountry & "<br> Phone: " & CompanyPhone & "<br>Fax: " & CompanyFax & "<br>Email: " & CompanyEmail & "<br>Employee ID: " & EmployeeID & " <br><br>IP Address: " & Address & "<br><br>Date: " & dt & "<br>"

                Dim IP As String = ConfigurationManager.AppSettings("ipaddress").ToString()
                Dim IPAdd As String = IP
                Dim newlinestr As String = "<br><br>"

                '(2) Assign the MailMessage's properties
                mm.Subject = "An exception occurred! on Production Server "
                mm.Body = String.Format("Message: {1}{0}<b>Message: </b>{2}{0} <b>Stack Trace</b>({4}):{0}{3}", newlinestr, Errtest, ex.Message, ex.StackTrace, IPAdd)
                mm.IsBodyHtml = True


                '(3) Create the SmtpClient object
                Dim smtp As New System.Net.Mail.SmtpClient

                '(4) Send the MailMessage (will use the Web.config settings)
                'smtp.Send(mm)

                newmailsending(mm)

            Catch ex1 As Exception
                'Whoops, some problem sending email!
                'Just send the user onto CustomErrorPage.aspx...


            End Try
        End If

    End Sub


    Public Sub newmailsending(ByVal Email As MailMessage)
        Dim QFmail As New com.quickflora.qfscheduler.QFPrintService
        QFmail.newmailsending(Email.From.ToString, Email.To.ToString, Email.CC.ToString, Email.Bcc.ToString, Email.Subject.ToString, Email.Body.ToString, "DEFAULT", "DEFAULT", "DEFAULT")

        Exit Sub

        Dim lblerrortestmail As New TextBox

        Dim obj_InsertOutGoingMailDetails As New clsMailServer
        obj_InsertOutGoingMailDetails.CompanyID = "DEFAULT"
        obj_InsertOutGoingMailDetails.DivisionID = "DEFAULT"
        obj_InsertOutGoingMailDetails.DepartmentID = "DEFAULT"

        Try

            obj_InsertOutGoingMailDetails.From_Email = Email.From.ToString
            obj_InsertOutGoingMailDetails.To_Email = Email.To.ToString
            obj_InsertOutGoingMailDetails.CC_Email = Email.CC.ToString
            obj_InsertOutGoingMailDetails.Email_Subject = Email.Subject.ToString
            obj_InsertOutGoingMailDetails.Email_Body = Email.Body.ToString


            Dim Host As String = ""
            Dim Port As String = ""

            Dim NetworkCredential_username As String = ""
            Dim NetworkCredential_password As String = ""

            Dim Host2 As String = ""
            Dim Port2 As String = ""

            Dim NetworkCredential_username2 As String = ""
            Dim NetworkCredential_password2 As String = ""


            Dim obj As New clsMailServer
            obj.CompanyID = "DEFAULT"
            obj.DivisionID = "DEFAULT"
            obj.DepartmentID = "DEFAULT"
            Dim dt As New Data.DataTable
            dt = obj.FillDetails

            If dt.Rows.Count <> 0 Then

                Host = "mail.authsmtp.com" ' dt.Rows(0)("MailServer1")
                Port = "25" 'dt.Rows(0)("MailServerPort1")
                NetworkCredential_username = "afcsfp" ' dt.Rows(0)("MailServerUserName1")
                NetworkCredential_password = "st079786" 'dt.Rows(0)("MailServerPassword1")


                Host2 = dt.Rows(0)("MailServer2")
                Port2 = dt.Rows(0)("MailServerPort2")
                NetworkCredential_username2 = dt.Rows(0)("MailServerUserName2")
                NetworkCredential_password2 = dt.Rows(0)("MailServerPassword2")



                ''New code going to put
                Dim AccessKeyId As String = ""
                Dim SecrectAccesskey As String = ""
                Dim chkAmazonmail As Boolean = False
                Try
                    AccessKeyId = dt.Rows(0)("AccessKeyId")
                    SecrectAccesskey = dt.Rows(0)("SecrectAccesskey")
                    chkAmazonmail = dt.Rows(0)("chkAmazonmail")
                Catch ex As Exception

                End Try

                If chkAmazonmail Then

                    Dim body As String =  Email.Body & "check" 'txtMessage.Text
                    Dim subject As String =Email.Subject ' txtSubject.Text


                    Dim client As New AmazonSimpleEmailServiceClient(AccessKeyId, SecrectAccesskey)
                    Dim sesemail As New Amazon.SimpleEmail.Model.SendEmailRequest()


                    sesemail.Message = New Amazon.SimpleEmail.Model.Message()
                    sesemail.Message.Body = New Amazon.SimpleEmail.Model.Body()
                    sesemail.Message.Body.Html = New Amazon.SimpleEmail.Model.Content(body)
                    sesemail.Message.Subject = New Amazon.SimpleEmail.Model.Content(subject)

                    Dim dst As New Destination()
                    Dim ToAddresses() As String = {"errors@sunflowertechnologies.com"}
                    Dim tolst As New System.Collections.Generic.List(Of String)(ToAddresses)

                    dst.ToAddresses = tolst
                    sesemail.WithDestination(dst)
                    sesemail.WithSource("errors@sunflowertechnologies.com")
                    sesemail.WithReturnPath("errors@sunflowertechnologies.com")
                    Dim resp As New Amazon.SimpleEmail.Model.SendEmailResponse

                    Try
                        resp = client.SendEmail(sesemail)
                        lblerrortestmail.Text = "Mail Sent With Amazon Mail Services Details"
                        Exit Sub
                    Catch ex As Exception
                        lblerrortestmail.Text = "Error occured while send email by Amazon Mail Services :" & ex.Message
                        ' Exit Sub
                    End Try

                End If

                '''



                If Host.Trim <> "" Then
                    Dim mailClient As New System.Net.Mail.SmtpClient()

                    'This object stores the authentication values

                    If NetworkCredential_username.Trim <> "" And NetworkCredential_password.Trim <> "" Then
                        Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username.Trim, NetworkCredential_password.Trim)
                        'mailClient.UseDefaultCredentials = False
                        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                        mailClient.Credentials = basicAuthenticationInfo
                    Else
                        mailClient.UseDefaultCredentials = True
                    End If

                    'Put your own, or your ISPs, mail server name onthis next line


                    mailClient.Host = Host.Trim
                    If Port.Trim <> "" Then
                        mailClient.Port = Port.Trim
                    End If

                    Try
                        mailClient.Send(Email)

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        lblerrortestmail.Text = "Mail Sent With Primary SMTP Details"

                    Catch ex As SmtpException
                        lblerrortestmail.Text = "Error From PRIMARY SMTP:" & ex.Message
                        If Host2.Trim <> "" Then
                            mailClient = New System.Net.Mail.SmtpClient()
                            'This object stores the authentication values

                            If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                                'mailClient.UseDefaultCredentials = False
                                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                                mailClient.Credentials = basicAuthenticationInfo
                            Else
                                mailClient.UseDefaultCredentials = True
                            End If

                            'Put your own, or your ISPs, mail server name onthis next line


                            mailClient.Host = Host2.Trim
                            If Port2.Trim <> "" Then
                                mailClient.Port = Port2.Trim
                            End If

                            mailClient.Send(Email)
                            '''''Email Details storing''''
                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                            '''''Email Details storing''''
                            lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondary SMTP Details"
                        End If

                    End Try



                Else
                    If Host2.Trim <> "" Then
                        Dim mailClient As New System.Net.Mail.SmtpClient()
                        'This object stores the authentication values

                        If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                            Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                            mailClient.UseDefaultCredentials = False
                            mailClient.Credentials = basicAuthenticationInfo
                        Else
                            mailClient.UseDefaultCredentials = True
                        End If

                        'Put your own, or your ISPs, mail server name onthis next line


                        mailClient.Host = Host2.Trim
                        If Port2.Trim <> "" Then
                            mailClient.Port = Port2.Trim
                        End If

                        mailClient.Send(Email)
                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondary SMTP Details"

                    Else

                        'Dim mailClient As New System.Net.Mail.SmtpClient()
                        ''This object stores the authentication values
                        ''mailClient.UseDefaultCredentials = True
                        ''Put your own, or your ISPs, mail server name onthis next line
                        ''mailClient.Host = "8.3.16.126"
                        ''mailClient.Port = "25"
                        'mailClient.Send(Email)
                        ''''''Email Details storing''''
                        'obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        'obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        'obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

                    End If
                End If



            Else

                'Dim mailClient As New System.Net.Mail.SmtpClient()
                ''This object stores the authentication values
                ''mailClient.UseDefaultCredentials = True
                ''Put your own, or your ISPs, mail server name onthis next line
                ''mailClient.Host = "8.3.16.126"
                ''mailClient.Port = "25"
                'mailClient.Send(Email)
                ''''''Email Details storing''''
                'obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                'obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                'obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                ''''''Email Details storing''''
                lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

            End If

        Catch ex As FormatException

            lblerrortestmail.Text = ("Format Exception: " & ex.Message)

        Catch ex As SmtpException

            lblerrortestmail.Text = ("SMTP Exception:  " & ex.Message)

        Catch ex As Exception

            lblerrortestmail.Text = ("General Exception:  " & ex.Message)

        End Try
    End Sub



    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Session.Timeout=480
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

    Protected Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As System.EventArgs)
        'If HttpContext.Current.Session("UserDetails") <> Nothing Then
        '    Dim strCacheKey As String = HttpContext.Current.Session("UserDetails").ToString()
        '    Dim strUser As String = HttpContext.Current.Cache(strCacheKey).ToString()
        'End If
    End Sub
</script>

