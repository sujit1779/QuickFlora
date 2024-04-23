Option Strict Off
Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common
Imports PayPal.Payments.Common.Utility
Imports PayPal.Payments.Transactions
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net


'Imports System.Text

Imports Amazon
Imports Amazon.EC2
Imports Amazon.EC2.Model
Imports Amazon.SimpleDB
Imports Amazon.SimpleDB.Model
Imports Amazon.S3
Imports Amazon.S3.Model
Imports Amazon.SimpleEmail
Imports Amazon.SimpleEmail.Model


Partial Class emailAdjustInventory
    Inherits System.Web.UI.Page



    Dim TransferNumber As String
    Dim ConnectionString As String = ""

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim filters As EnterpriseCommon.Core.FilterSet

        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID

        If Not IsNothing(Request.QueryString("InventoryAdjustmentsNumber")) Then
            TransferNumber = Request.QueryString("InventoryAdjustmentsNumber")
            lblordernumber.Text = TransferNumber
            If Not IsPostBack Then
                EmailNotifications(TransferNumber)
            End If
        End If

    End Sub



    Public Function Employeename(ByVal EmployeeID As String) As String
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select [EmployeeName] from   [PayrollEmployees] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [EmployeeID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = EmployeeID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Dim Ename As String = ""

                Try
                    Ename = dt.Rows(0)(0)
                Catch ex As Exception

                End Try

                Return EmployeeID & "-" & Ename

                If Ename <> "" Then
                Else
                    Return EmployeeID
                End If
            Else
                Return EmployeeID
            End If



        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return EmployeeID
        End Try
        Return EmployeeID
    End Function



    Public Sub EmailNotifications(ByVal TransferNumber As String)

        Dim TransactionDate As String = ""

        Dim TransactionType As String = ""


        Dim LocationID As String = ""

        Dim EmployeeID As String = ""

        Dim InternalNotes As String = ""



        Dim dtPO As New DataTable

        dtPO = GetInventoryTransferList(TransferNumber)

        If (dtPO.Rows.Count <> 0) Then

            Try
                TransactionDate = dtPO.Rows(0)("TransactionDate")
            Catch ex As Exception

            End Try

            Try
                TransactionType = dtPO.Rows(0)("TransactionType")
            Catch ex As Exception

            End Try
            Try
                LocationID = dtPO.Rows(0)("LocationID")
            Catch ex As Exception

            End Try

            Try
                EmployeeID = Employeename(dtPO.Rows(0)("EmployeeID"))
            Catch ex As Exception

            End Try


            Try
                InternalNotes = dtPO.Rows(0)("InternalNotes")
            Catch ex As Exception

            End Try



        End If




        Dim StrBody As New StringBuilder()

        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='663' id='table1'>")
        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td colspan='2' ><b>   Inventory Transfer Details   </b></td>")


        StrBody.Append("</tr>")

        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td  ><b> Adjustment # </b></td>")

        StrBody.Append("<td  ><b> " & TransferNumber & " </b></td>")

        StrBody.Append("</tr>")

        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td  ><b> Adjustment Date </b></td>")

        StrBody.Append("<td  ><b> " & TransactionDate & " </b></td>")

        StrBody.Append("</tr>")

        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td  ><b> Adjustment Type  </b></td>")

        StrBody.Append("<td  ><b> " & TransactionType & " </b></td>")

        StrBody.Append("</tr>")

        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td  ><b> Location </b></td>")

        StrBody.Append("<td  ><b> " & LocationID & " </b></td>")

        StrBody.Append("</tr>")

        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td  ><b> Adjust by  </b></td>")

        StrBody.Append("<td  ><b> " & EmployeeID & " </b></td>")

        StrBody.Append("</tr>")

        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td  ><b> Internal Notes </b></td>")

        StrBody.Append("<td  ><b> " & InternalNotes & " </b></td>")



        StrBody.Append("</table>")

        StrBody.Append("<hr>")

        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='663' id='table1'>")

        StrBody.Append("<tr    align='center'>")
        StrBody.Append("<td  ><b>Item ID</b></td>")

        StrBody.Append("<td  ><b>Item Name</b></td>")

        StrBody.Append("<td ><b> Qty</b></td>")



        StrBody.Append("<td  ><b>Average Cost </b></td>")

        StrBody.Append("<td  ><b> Total </b></td>")


        StrBody.Append("</tr>")

        Dim FillItemDetailGrid As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = GetInventoryTransferItemsList(TransferNumber)

        '    SELECT [RowID]
        '      ,[CompanyID]
        '      ,[DivisionID]
        '      ,[DepartmentID]
        '      ,[TransactionNumber]
        '      ,[ItemID]
        '      ,[ItemName]
        '      ,[Qty]
        '      ,[AverageCost]
        '      ,[ItemTotal]
        '  FROM [Enterprise].[dbo].[InventoryAdjustmentDetail]
        'GO

        Dim tr As String = ""
        Dim n As Integer = 0
        For n = 0 To ds.Tables(0).Rows.Count - 1


            tr = tr & "<tr><td>" & ds.Tables(0).Rows(n)("ItemID") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("ItemName") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("Qty") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("AverageCost") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("ItemTotal") & "</td>"
            tr = tr & "<tr>"

        Next

        StrBody.Append(tr)

        StrBody.Append("</table>")


        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='663' id='table1'>")

        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td  ><b> Download PDF  </b></td>")

        StrBody.Append("<td  ><b> " & "<a  target='_blank'  href='https://reports.quickflora.com/reports/scripts/AdjustInventoryReport.aspx?CompanyID=" & Me.CompanyID & "&DivisionID=" & Me.DivisionID & "&DepartmentID=" & Me.DepartmentID & "&InventoryAdjustmentsNumber=" & TransferNumber & "' >Click to Open</a>" & " </b></td>")

        StrBody.Append("</tr>")


        StrBody.Append("<tr    align='center'>")

        StrBody.Append("<td colspan='2' ><b>  Thanks <br> Inventory Support Team  </b></td>")


        StrBody.Append("</tr>")


        StrBody.Append("</table>")



        Dim OrderPlacedSubject As String
        Dim OrderPlacedContent As String




        OrderPlacedSubject = "Adjust Inventory  # " & TransferNumber & " For Location " & LocationID
        OrderPlacedContent = StrBody.ToString


        Dim CompanyEmail As String = "customerservice@fieldofflowers.com"

        Dim ToAddress As String = ""
        Dim FromAddress As String = CompanyEmail


        OrderPlacedSubject = OrderPlacedSubject
        OrderPlacedContent = OrderPlacedContent


        txtfrom.Text = "" 'FromAddress
        txtto.Text = ToAddress
        txtcc.Text = ""
        txtemailsubject.Text = OrderPlacedSubject
        divEmailContent.InnerHtml = OrderPlacedContent

    End Sub


    Public Function GetInventoryTransferList(ByVal TransferNumber As String) As DataTable



        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim dt As New DataTable

        'qry = "select  *  from [InventoryTransferHeader]   where [InventoryTransferHeader].TransferNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'select  *  from  [InventoryAdjustmentHeader]   where [InventoryAdjustmentHeader].TransactionNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 
        qry = "select  *  from [InventoryAdjustmentHeader]   where [InventoryAdjustmentHeader].TransactionNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f31", SqlDbType.BigInt)).Value = TransferNumber.Trim()


        Dim da As New SqlDataAdapter(com)

        da.Fill(dt)



        Return dt
        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return dt

    End Function




    Public Function GetInventoryTransferItemsList(ByVal TransferNumber As String) As Data.DataSet

        Dim Total As Decimal = 0
        Dim dt As New Data.DataSet

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  *  from [InventoryAdjustmentDetail]   where [InventoryAdjustmentDetail].TransactionNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.BigInt)).Value = TransferNumber


            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return dt

    End Function

    Public Function GetPurchaseDetail_ItemID_name(ByVal ItemID As String) As String

        Dim name As String = 0

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  ItemName  from [InventoryItems]   where [InventoryItems].ItemID =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = ItemID.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    name = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return name

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return name

    End Function



    Dim EmployeeID As String = ""




    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click

        If txtemailsubject.Text <> "" And txtfrom.Text <> "" And txtto.Text <> "" Then

            'If checkOrdermodified.Checked Then
            '    txtemailsubject.Text = "[Modified] " & txtemailsubject.Text
            'Else
            '    txtemailsubject.Text = "[Duplicate] " & txtemailsubject.Text
            'End If

            EmailSendingWithhCC(txtemailsubject.Text, divEmailContent.InnerHtml, txtfrom.Text, txtto.Text, txtcc.Text)
            lblconfirmation.ForeColor = Drawing.Color.Green
            lblconfirmation.Text = "Order mail sent "
            drpEmailTypes.Enabled = False
            BtnSubmit.Visible = False
            Button1.Visible = False
            table1.Visible = False
            table3.Visible = True
        Else
            lblconfirmation.ForeColor = Drawing.Color.Red
            lblconfirmation.Text = "Email subject , Email From and Email To feild can not be blank"
        End If

    End Sub



    Public Sub EmailSendingWithhCC(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String, ByVal CCAddress As String)
        'Exit Sub
        Dim mMailMessage As New MailMessage()
        Try

            ' Set the sender address of the mail message
            mMailMessage.From = New MailAddress(FromAddress)
            ' Set the recepient address of the mail message
            mMailMessage.To.Add(New MailAddress(ToAddress))

            If CCAddress.Trim <> "" Then
                mMailMessage.CC.Add(New MailAddress(CCAddress))
            End If

            If txtcc2.Text.Trim <> "" Then
                mMailMessage.CC.Add(New MailAddress(txtcc2.Text.Trim))
            End If

            'mMailMessage.Bcc.Add(New MailAddress("qfclientorders@sunflowertechnologies.com"))
            'Set the subject of the mail message
            mMailMessage.Subject = OrderPlacedSubject.ToString()
            ' Set the body of the mail message
            mMailMessage.Body = OrderPlacedContent.ToString()

            ' Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = True


            ' Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal

            ' Instantiate a new instance of SmtpClient
            Dim smtp As New System.Net.Mail.SmtpClient()
            smtp.Host = ConfigurationManager.AppSettings("SystemSMTPServer")

            Try
                'smtp.Send(mMailMessage)
                newmailsending(mMailMessage)

            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try

    End Sub


    Public Sub newmailsending(ByVal Email As MailMessage)

        Dim lblerrortestmail As New TextBox
        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID

        Dim obj_InsertOutGoingMailDetails As New clsMailServer
        obj_InsertOutGoingMailDetails.CompanyID = CompanyID
        obj_InsertOutGoingMailDetails.DivisionID = DivisionID
        obj_InsertOutGoingMailDetails.DepartmentID = DepartmentID


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
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            Dim dt As New Data.DataTable
            dt = obj.FillDetails

            If dt.Rows.Count <> 0 Then

                Host = dt.Rows(0)("MailServer1")
                Port = dt.Rows(0)("MailServerPort1")
                NetworkCredential_username = dt.Rows(0)("MailServerUserName1")
                NetworkCredential_password = dt.Rows(0)("MailServerPassword1")


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

                    Dim body As String = Email.Body & "*" ' txtMessage.Text
                    Dim subject As String = Email.Subject 'txtSubject.Text


                    Dim client As New AmazonSimpleEmailServiceClient(AccessKeyId, SecrectAccesskey)
                    Dim sesemail As New Amazon.SimpleEmail.Model.SendEmailRequest()


                    sesemail.Message = New Amazon.SimpleEmail.Model.Message()
                    sesemail.Message.Body = New Amazon.SimpleEmail.Model.Body()
                    sesemail.Message.Body.Html = New Amazon.SimpleEmail.Model.Content(body)
                    sesemail.Message.Subject = New Amazon.SimpleEmail.Model.Content(subject)

                    Dim dst As New Destination()
                    Dim ToAddresses() As String = {Email.To(0).ToString}
                    Dim tolst As New System.Collections.Generic.List(Of String)(ToAddresses)

                    Try
                        If Email.CC(0).ToString.Trim <> "" Then
                            Dim CCAddresses() As String = {Email.CC(0).ToString}
                            Dim CClst As New System.Collections.Generic.List(Of String)(CCAddresses)
                            dst.CcAddresses = CClst
                        End If
                    Catch ex As Exception

                    End Try

                    dst.ToAddresses = tolst
                    sesemail.WithDestination(dst)
                    sesemail.WithSource(Email.From.ToString)
                    sesemail.WithReturnPath(Email.From.ToString)
                    Dim resp As New Amazon.SimpleEmail.Model.SendEmailResponse

                    Try
                        resp = client.SendEmail(sesemail)
                        lblerrortestmail.Text = "Mail Sent With Amazon Mail Services Details"
                        Exit Sub
                    Catch ex As Exception
                        lblerrortestmail.Text = "Error occured while send email by Amazon Mail Services :" & ex.Message
                        'Exit Sub
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
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        lblerrortestmail.Text = "Mail Sent With Primary SMTP Details"

                    Catch ex As SmtpException
                        Dim Email_Subject1 As String = obj_InsertOutGoingMailDetails.Email_Subject

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        obj_InsertOutGoingMailDetails.Email_Subject = Email_Subject1

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

                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                            mailClient.Send(Email)
                            '''''Email Details storing''''
                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                            '''''Email Details storing''''
                            lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"
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

                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)
                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"

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

                Dim mailClient As New System.Net.Mail.SmtpClient()
                'This object stores the authentication values
                'mailClient.UseDefaultCredentials = True
                'Put your own, or your ISPs, mail server name onthis next line
                mailClient.Host = "8.3.16.126"
                mailClient.Port = "25"
                mailClient.Send(Email)
                '''''Email Details storing''''
                obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                ''''Email Details storing''''
                lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

            End If

        Catch ex As FormatException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Format Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("Format Exception: " & ex.Message)

        Catch ex As SmtpException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send SMTP Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''
            lblerrortestmail.Text = ("SMTP Exception:  " & ex.Message)

        Catch ex As Exception

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send General Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("General Exception:  " & ex.Message)

        End Try
    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnback.Click
        Response.Redirect("InventoryAdjustmentsPOList.aspx")

    End Sub

    Protected Sub btnmore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmore.Click

        Response.Redirect("emailAdjustInventory.aspx?InventoryAdjustmentsNumber=" & Request.QueryString("InventoryAdjustmentsNumber"))
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("InventoryAdjustmentsPOList.aspx")

    End Sub
End Class
