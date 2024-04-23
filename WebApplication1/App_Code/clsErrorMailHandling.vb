Imports System.Data
Imports System.Data.SqlClient
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
Imports AuthorizeNet


Public Class clsErrorMailHandling


    Public CompanyID As String = "DEFAULT"
    Public DivisionID As String = "DEFAULT"
    Public DepartmentID As String = "DEFAULT"

    Public OrderNumber As String = ""

    Public Function ErrorMailHandling(ByVal Mailsubject As String, ByVal mailcontent As String, ByVal mailFrom As String) As String


        '---code for trace
        Dim subject As String = mailFrom & "--" & Mailsubject & " [ " & OrderNumber & "]"
        'Dim mailcontent As String = "Out put comes is =" & output & " also Error message is =" & Me.lblerrormessag.Text
        Dim frommail As String = "imy@quickflora.com"
        Dim tomail As String = "imy@quickflora.com"
        EmailSending(subject, mailcontent, frommail, tomail)
        '---code for trace

        Return ""
    End Function


    Public Sub EmailSending(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String)

        'Exit Sub

        Dim mMailMessage As New MailMessage()

        ' Set the sender address of the mail message
        mMailMessage.From = New MailAddress(FromAddress)
        ' Set the recepient address of the mail message


        mMailMessage.To.Add(New MailAddress(ToAddress))
        'mMailMessage.To.Add(New MailAddress("gaurav@sunflowertechnologies.com"))
        mMailMessage.CC.Add(New MailAddress("gaurav@quickflora.com"))


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
        smtp.Host = ConfigurationManager.AppSettings("SystemSMTPServer")

        Try
            'smtp.Send(mMailMessage)
            newmailsending(mMailMessage)
        Catch ex As Exception

        End Try



    End Sub


    Public Sub newmailsending(ByVal Email As MailMessage)

        Dim lblerrortestmail As New TextBox

        CompanyID = Me.CompanyID
        DivisionID = Me.DivisionID
        DepartmentID = Me.DepartmentID

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





End Class
