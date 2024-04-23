Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Public Class clsGiftCardNew

    Public Function GenerateRandomAuthKey(ByVal length As Int16) As String

        Dim legalChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
        Dim str As New StringBuilder
        Dim ch As Char
        Dim random As New Random

        For i As Integer = 0 To length - 1
            ch = legalChars(random.Next(0, legalChars.Length))
            str.Append(ch)
        Next

        Return str.ToString

    End Function

    Public Function GetGiftCardList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                    Optional ByVal GiftCardNumber As String = "", Optional ByVal Amount As String = "", Optional ByVal CustomerId As String = "", _
                                    Optional ByVal OrderNumber As String = "", Optional ByVal IssueDate As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GiftCardList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)

                Command.Parameters.AddWithValue("Amount", Amount)
                Command.Parameters.AddWithValue("CustomerId", CustomerId)
                Command.Parameters.AddWithValue("OrderNumber", OrderNumber)
                Command.Parameters.AddWithValue("IssueDate", IssueDate)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try
            End Using
        End Using

        Return dt

    End Function

    Public Function GetGiftCardItemsList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, Optional ByVal GiftCardItemID As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GiftCardItemsList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("GiftCardItemID", GiftCardItemID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try
            End Using
        End Using

        Return dt

    End Function

    Public Function CreateGiftCard(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal GiftCardNumber As String, _
                                        ByVal GiftCardItemID As String, ByVal Amount As String, ByVal LocationID As String, ByVal AuthKey As String, _
                                        Optional ByVal CustomerID As String = "", Optional ByVal OrderNumber As String = "") As String
        'Public Function CreateGiftCard(CompanyID As String, DivisionID As String, DepartmentID As String, GiftCardNumber As String, _
        '                                        Amount As String, LocationID As String, AuthKey As String, _
        '                                        CustomerID As String, OrderNumber As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CreateGiftCard]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)

                'Command.Parameters.AddWithValue("GiftCardItemID", GiftCardItemID)
                Command.Parameters.AddWithValue("Amount", Amount)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("AuthKey", AuthKey)

                Command.Parameters.AddWithValue("CustomerID", CustomerID)
                Command.Parameters.AddWithValue("OrderNumber", OrderNumber)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    Return "OK"

                Catch ex As Exception

                    Return ex.Message

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function SuspendGiftCard(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal GiftCardNumber As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[SuspendGiftCard]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)

                Try
                    Dim count As Int16
                    Command.Connection.Open()
                    count = CInt(Command.ExecuteScalar())

                    If count = 1 Then
                        Return "OK"
                    Else
                        Return "Gift Card can not be suspend as its not in active state."
                    End If

                Catch ex As Exception

                    Return ex.Message

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpdateGiftCard(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal GiftCardNumber As String, _
                                            ByVal Suspend As Boolean, ByVal Active As Boolean, Optional ByVal AuthKey As String = "", Optional ByVal CustomerID As String = "", _
                                            Optional ByVal OrdeRNumber As String = "") As Boolean

        'Amount As String, LocationID As String, AuthKey As String, _
        '    CustomerID As String, OrderNumber As String, _
        '    , IssueDate As String, ExpiryDate As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateGiftCard]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)

                'Command.Parameters.AddWithValue("Amount", Amount)
                'Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("AuthKey", AuthKey)

                Command.Parameters.AddWithValue("CustomerID", CustomerID)
                Command.Parameters.AddWithValue("OrderNumber", OrdeRNumber)

                Command.Parameters.AddWithValue("Suspend", Suspend)
                Command.Parameters.AddWithValue("Active", Active)
                'Command.Parameters.AddWithValue("IssueDate", IssueDate)
                'Command.Parameters.AddWithValue("ExpiryDate", ExpiryDate)

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    Return True

                Catch ex As Exception

                    Return False

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    'Public Function DeleteGiftCard(CompanyID As String, DivisionID As String, DepartmentID As String, GiftCardNumber As String, EmployeeID As String) As Boolean

    '    Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using Command As New SqlCommand("[enterprise].[DeleteGiftCard]", Connection)
    '            Command.CommandType = CommandType.StoredProcedure

    '            Command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            Command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    '            Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)
    '            Command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    '            Try

    '                Command.Connection.Open()
    '                Command.ExecuteNonQuery()

    '                Return True

    '            Catch ex As Exception

    '                Return False

    '            Finally
    '                Command.Connection.Close()
    '            End Try

    '        End Using
    '    End Using

    'End Function

    Public Function CheckGiftCardBalanceAmount(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal GiftCardNumber As String, ByVal AuthKey As String, ByRef BalanceAmount As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CheckGiftCardBalance]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)
                Command.Parameters.AddWithValue("AuthKey", AuthKey)

                Try
                    Command.Connection.Open()
                    BalanceAmount = Command.ExecuteScalar()

                    Return "OK"

                Catch ex As Exception

                    Return ex.Message

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Private Function GenerateForgetGiftCardTemporaryKey(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal GiftCardNumber As String, _
                                                        ByRef CustomerName As String, ByVal CustomerEmail As String, ByVal TemporaryAuthKey As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GenerateForgetGiftCardTemporaryKey]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)
                Command.Parameters.AddWithValue("CustomerEmail", CustomerEmail)
                Command.Parameters.AddWithValue("TemporaryAuthKey", TemporaryAuthKey)

                Try

                    Command.Connection.Open()
                    CustomerName = Command.ExecuteScalar()

                    Return "OK"

                Catch ex As Exception

                    Return ex.Message

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function ForgetGiftCardAuthKey(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal GiftCardNumber As String, ByVal CustomerEmail As String) As String

        Dim NewAuthKey As String = GenerateRandomAuthKey(50)
        Dim CustomerName As String = ""
        Dim ForgetAuthKey As String = GenerateForgetGiftCardTemporaryKey(CompanyID, DivisionID, DepartmentID, GiftCardNumber, CustomerName, CustomerEmail, NewAuthKey)

        If CustomerName = "FAILED" Then
            Return "Failed"
            Exit Function
        End If

        Dim ds As New DataSet
        ds = GetCompanyInformation(CompanyID, DivisionID, DepartmentID)

        Dim row As DataRow

        If ds.Tables(0).Rows.Count > 0 Then
            row = ds.Tables(0).Rows(0)
        Else
            Return "Invalid Company Details"
            Exit Function
        End If

        Dim CompanyAddress As String = row("CompanyAddress1").ToString + " " + row("CompanyAddress2").ToString + " " + row("CompanyAddress3").ToString + " " + row("CompanyCity").ToString + " " + row("CompanyState").ToString + " " + row("CompanyZip").ToString

        If ForgetAuthKey = "OK" Then
            SendForgetAuthKeyEmail(CompanyID, DivisionID, DepartmentID, GiftCardNumber, row("CompanyName").ToString(), row("CompanyEmail").ToString(), _
                                   row("CompanyPhone").ToString(), CompanyAddress, row("FrontEndURL"), CustomerName, CustomerEmail, NewAuthKey)
            Return "OK"
        ElseIf ForgetAuthKey = "FAILED" Then
            Return "Failed"
        Else
            Return ForgetAuthKey
        End If

    End Function

    Private Function SendForgetAuthKeyEmail(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal GiftCardNumber As String, _
                                           ByVal CompanyName As String, ByVal CompanyEmail As String, ByVal CompanyPhone As String, ByVal CompanyAddress As String, ByVal CompanyFrontEndURL As String, _
                                           ByVal CustomerName As String, ByVal CustomerEmail As String, ByVal NewAuthKey As String) As Boolean


        Try
            Dim strEmailBody As New StringBuilder

            strEmailBody.Append("<strong>Dear " + CustomerName.Trim + "</strong>,")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("You recently initiated an authentication key reset for your Gift Card # <strong> " + GiftCardNumber + " </strong>. To complete the process, use the temporary key # and click the link below.")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<div style='width:50%; text-align:center; background-color:#EEEEEE; border-radius:10px; font-family:Arial;'>")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Temporary Key</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span>" + NewAuthKey + "</span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Reset Link</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span><a href='" + CompanyFrontEndURL + "/ResetGiftCardAuthentictionKey.aspx?gc=" + GiftCardNumber + "'>Click here</a></span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("</div>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("Social media addict?")
            strEmailBody.Append("<br />")

            strEmailBody.Append("Like us on <a href='#'> Facebook </a> and follow us on <a href='#'> Twitter </a>, <a href='#'> Instagram </a> and <a href='#'> Pinterest </a>")
            strEmailBody.Append("<br />")

            'strEmailBody.Append("Enjoy your class")
            'strEmailBody.Append("<br />")

            strEmailBody.Append("<strong>" + CompanyName + " Crew</strong>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<div style='border: 0px currentcolor; color: rgb(153,153,153); font-family: Arial; font-size: 11px'>")
            strEmailBody.Append("<div>")
            strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/balloon.png' />")
            strEmailBody.Append(CompanyAddress + "&nbsp;")
            strEmailBody.Append("<a href='http://maps.google.com/maps?q=" + CompanyAddress.Replace(" ", "+") + "' style='color: rgb(0,174,239); text-decoration: none' target='_blank'>View Map</a>")
            strEmailBody.Append("</div>")

            strEmailBody.Append("<div>")
            strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/call.png' />")
            strEmailBody.Append("<span style='color: rgb(34,34,34);'>" + CompanyPhone + "</span>")
            strEmailBody.Append("</div>")

            strEmailBody.Append("<div>")
            strEmailBody.Append("<a href='#' style='color: rgb(0,174,239); text-decoration: none' target='_blank'>")
            strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/globe.png' />Visit our Website")
            strEmailBody.Append("</a>")
            strEmailBody.Append("</div>")

            'strEmailBody.Append("<div>")
            'strEmailBody.Append("<a href='#' style='color: rgb(0,174,239); text-decoration: none' target='_blank'>")
            'strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/preview-page.png' />View our Cancellation Policy")
            'strEmailBody.Append("</a>")
            'strEmailBody.Append("</div>")
            strEmailBody.Append("</div>")

            Dim Subject As String = "Forget Authentication Key Request - " + CompanyName

            EmailSending(CompanyID, DivisionID, DepartmentID, Subject, strEmailBody.ToString(), CompanyEmail, CustomerEmail)

            Return True

        Catch ex As Exception

            Return False

        End Try


    End Function

    Public Function UpdateGiftCardAuthKey(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                          ByVal GiftCardNumber As String, ByVal CustomerEmail As String, ByVal NewAuthKey As String, ByVal TemporaryAuthKey As String) As String

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateGiftCardAuthKey]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("GiftCardNumber", GiftCardNumber)
                Command.Parameters.AddWithValue("CustomerEmail", CustomerEmail)
                Command.Parameters.AddWithValue("NewAuthKey", NewAuthKey)
                Command.Parameters.AddWithValue("TemporaryAuthKey", TemporaryAuthKey)

                Try
                    Dim OP As String = ""
                    Command.Connection.Open()
                    OP = Command.ExecuteScalar()

                    If OP = "OK" Then
                        Return "OK"
                    Else
                        Return OP
                    End If

                Catch ex As Exception

                    Return ex.Message

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function SendGiftCardBookingEmail(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, _
                                             ByVal GiftCardNumber As String, ByVal AuthKey As String, ByVal Amount As String, ByVal IssueDate As String, ByVal ExpirytDate As String, _
                                             ByVal CustomerID As String, ByVal CustomerName As String, ByVal CustomerEmail As String) As Boolean


        '---code for trace
        Dim subjectt As String = "Giftcard Put coming for Ordernumber " & OrderNumber & " CompanyID = " & CompanyID
        Dim mailcontent As String = "Out put comes GiftCardNumber=" & GiftCardNumber
        Dim frommail As String = "imtiyaz@sunflowertechnologies.com"
        Dim tomail As String = "imtiyaz@sunflowertechnologies.com"
        EmailSending(CompanyID, DivisionID, DepartmentID, subjectt, mailcontent, frommail, tomail)
        '---code for trace

        Dim CompanyName As String, CompanyEmail As String, CompanyPhone As String, CompanyAddress As String
        Dim ds As New DataSet
        ds = GetCompanyInformation(CompanyID, DivisionID, DepartmentID)

        Dim row As DataRow

        If ds.Tables(0).Rows.Count > 0 Then
            row = ds.Tables(0).Rows(0)
        Else
            Return "Invalid Company Details"
            Exit Function
        End If

        CompanyAddress = row("CompanyAddress1").ToString + " " + row("CompanyAddress2").ToString + " " + row("CompanyAddress3").ToString + " " + row("CompanyCity").ToString + " " + row("CompanyState").ToString + " " + row("CompanyZip").ToString
        CompanyName = row("CompanyName").ToString
        CompanyEmail = row("CompanyEmail").ToString
        CompanyPhone = row("CompanyPhone").ToString

        Try
            Dim strEmailBody As New StringBuilder

            strEmailBody.Append("<strong>Dear " + CustomerName + "</strong>,")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("Thank you for purchasing " + CompanyName + " gift card. We look forward to seeing you soon.")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<div style='width:50%; text-align:center; background-color:#EEEEEE; border-radius:10px; font-family:Arial;'>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Order #</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span>" + OrderNumber + "</span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Gift Card #</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span>" + GiftCardNumber + "</span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Authentication Key</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span>" + AuthKey + "</span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Amount</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span>" + String.Format("${0:N2}", Convert.ToDecimal(Amount)) + "</span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Issue Date</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span>" + Convert.ToDateTime(IssueDate).ToShortDateString + "</span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<span style='font-weight:bold;font-size:20px'>Expiry Date</span>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<span>" + Convert.ToDateTime(ExpirytDate).ToShortDateString + "</span>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("</div>")

            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("Social media addict?")
            strEmailBody.Append("<br />")

            strEmailBody.Append("Like us on <a href='#'> Facebook </a> and follow us on <a href='#'> Twitter </a>, <a href='#'> Instagram </a> and <a href='#'> Pinterest </a>")
            strEmailBody.Append("<br />")

            'strEmailBody.Append("Enjoy your class")
            'strEmailBody.Append("<br />")

            strEmailBody.Append("<strong>" + CompanyName + " Crew</strong>")
            strEmailBody.Append("<br />")
            strEmailBody.Append("<br />")

            strEmailBody.Append("<div style='border: 0px currentcolor; color: rgb(153,153,153); font-family: Arial; font-size: 11px'>")
            strEmailBody.Append("<div>")
            strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/balloon.png' />")
            strEmailBody.Append(CompanyAddress + "&nbsp;")
            strEmailBody.Append("<a href='http://maps.google.com/maps?q=" + CompanyAddress.Replace(" ", "+") + "' style='color: rgb(0,174,239); text-decoration: none' target='_blank'>View Map</a>")
            strEmailBody.Append("</div>")

            strEmailBody.Append("<div>")
            strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/call.png' />")
            strEmailBody.Append("<span style='color: rgb(34,34,34);'>" + CompanyPhone + "</span>")
            strEmailBody.Append("</div>")

            strEmailBody.Append("<div>")
            strEmailBody.Append("<a href='#' style='color: rgb(0,174,239); text-decoration: none' target='_blank'>")
            strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/globe.png' />Visit our Website")
            strEmailBody.Append("</a>")
            strEmailBody.Append("</div>")

            'strEmailBody.Append("<div>")
            'strEmailBody.Append("<a href='#' style='color: rgb(0,174,239); text-decoration: none' target='_blank'>")
            'strEmailBody.Append("<img src='http://reservationsystem.quickflora.com/Images/preview-page.png' />View our Cancellation Policy")
            'strEmailBody.Append("</a>")
            'strEmailBody.Append("</div>")
            strEmailBody.Append("</div>")

            Dim Subject As String = "New Gift Card Notification - " + CompanyName

            EmailSending(CompanyID, DivisionID, DepartmentID, Subject, strEmailBody.ToString(), CompanyEmail, CustomerEmail)

            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function EmailSending(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                            ByVal EmailSubject As String, ByVal EmailContent As String, ByVal FromAddress As String, ByVal ToAddress As String) As Boolean

        Dim mMailMessage As New MailMessage()

        mMailMessage.From = New MailAddress(FromAddress)
        mMailMessage.To.Add(New MailAddress(ToAddress))
        'mMailMessage.CC.Add(New MailAddress("gauravg@quickflora.com"))

        mMailMessage.Subject = EmailSubject.ToString()
        mMailMessage.Body = EmailContent.ToString()

        mMailMessage.IsBodyHtml = True
        mMailMessage.Priority = MailPriority.Normal

        Dim smtp As New System.Net.Mail.SmtpClient()
        'smtp.Host = ConfigurationManager.AppSettings("SystemSMTPServer")

        Try
            'smtp.Send(mMailMessage)
            NewEmailSending(CompanyID, DivisionID, DepartmentID, mMailMessage)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Sub NewEmailSending(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Email As MailMessage)

        Dim lblerrortestmail As New TextBox

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

    Public Function GetCompanyInformation(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet

        Dim dt As New DataSet()

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("enterprise.GetCompanyInformation", Connection)
                Command.CommandType = CommandType.StoredProcedure
                Try
                    Command.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
                    Command.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
                    Command.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID

                    Dim da As New SqlDataAdapter
                    da.SelectCommand = Command
                    da.Fill(dt)
                    'Return dt

                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    HttpContext.Current.Response.Write(msg)
                    'Return dt
                End Try
            End Using
        End Using

        Return dt

    End Function

End Class
