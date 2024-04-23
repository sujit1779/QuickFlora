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

Public Class clsCaptureCreditCardAmount
    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public OrderNumber As String = ""

    Public drpExpirationDate As String = ""
    Public txtCard As String = ""
    Public txtCSV As String = ""
    Dim txtCustomerFirstName As String = ""
    Dim txtCustomerLastName As String = ""
    Dim txtCustomerAddress1 As String = ""
    Dim txtCustomerAddress2 As String = ""
    Dim txtCustomerZip As String = ""
    Dim txtCustomerCity As String = ""
    Dim txtCustomerState As String = ""
    Dim drpCountry As String = ""

    Public OrderID As String = ""
    Public ReferenceID As String = ""
    Public ApprovalNumber As String = ""
    Public Address As String = ""

    Public PayPalPNREF As String = ""


    Public lblerrormessag As New TextBox
    Public REMOTE_ADDR As String
    Public AmttotalCharge As Decimal
    Public AmttotalRefund As Decimal
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public PaymentAmount As Decimal
    Public PaymentMethod As String



    Public Function DetailsOrderPaymentDetails() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderHeaderPaymentMethodProcessingDetails where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try

    End Function


    Public Sub BindCustomerDetails()

        Dim OrderRs As SqlDataReader
        Dim PopOrderNo As New CustomOrder()
        OrderRs = PopOrderNo.PopulateOrder(CompanyID, DepartmentID, DivisionID, OrderNumber)
        While OrderRs.Read()
            Dim exdat As String = ""
            Dim exdate As Date
            Try
                exdat = OrderRs("CreditCardExpDate").ToString()
                exdate = exdat
                exdat = exdate.ToString("MM/yyyy")
            Catch ex As Exception
            End Try
            drpExpirationDate = exdat
            txtCSV = OrderRs("CreditCardCSVNumber").ToString()
            Dim Decrypt As New Encryption
            If OrderRs("CreditCardNumber").ToString() <> "" Then
                txtCard = Decrypt.TripleDESDecode(OrderRs("CreditCardNumber").ToString(), OrderRs("CustomerID").ToString().ToUpper())
            Else
                txtCard = ""
            End If


        End While

    End Sub


    Public Function CaptureCreditCardCHarge() As String

        BindCustomerDetails()
        Dim output As String
        output = ChrageCreditCard()

        If output = "ERROR" Then
            Return output
        End If

        Dim obj As New clsOrderAdjustments
        obj.OrderNumber = OrderNumber
        obj.CompanyID = CompanyID
        obj.DepartmentID = DepartmentID
        obj.DivisionID = DivisionID
        obj.OrderID = OrderID
        obj.ReferenceID = ReferenceID
        obj.ApprovalNumber = ApprovalNumber
        obj.PayPalPNREF = PayPalPNREF
        obj.PaymentAmount = AmttotalCharge
        obj.UpdateOrderAdjustmentsApprovalNumber()

        Return ApprovalNumber

    End Function


    Public Sub EmailSending(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String)

        Exit Sub

        Dim mMailMessage As New MailMessage()

        ' Set the sender address of the mail message
        mMailMessage.From = New MailAddress(FromAddress)
        ' Set the recepient address of the mail message


        mMailMessage.To.Add(New MailAddress(ToAddress))
        mMailMessage.To.Add(New MailAddress("gaurav@sunflowertechnologies.com"))


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
            smtp.Send(mMailMessage)
        Catch ex As Exception

        End Try



    End Sub


  Public Function ChrageCreditCard() As String


        Dim obj1 As New clsOrderAdjustments
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        obj1.OrderNumber = OrderNumber

        Dim dt As New Data.DataTable
        dt = obj1.DetailsCreditCardPaymentDetails

        If dt.Rows.Count <> 0 Then
            'Try
            If dt.Rows(0)("PaymentGateway") = "PayPal" Then
                Dim rs As String
                rs = PaymentDetails(OrderNumber, dt)

                If rs = "ERROR" Then
                    Return "ERROR"
                End If

                Dim ChargeStatus As String
                Dim ChargeTypes As String
                Try
                    ChargeTypes = dt.Rows(0)("ChargeTypes")
                    ChargeStatus = dt.Rows(0)("ChargeStatus")
                Catch ex As Exception
                    ChargeTypes = "SALE"
                End Try
                If ChargeTypes <> "InvCancel" Then
                    rs = PaymentDetails_CheckCapture(OrderNumber, dt)
                    If rs = "ERROR" Then
                        Return "ERROR"
                    Else
                        If rs = "OK" Then
                            Return "OK"
                        Else
                            rs = PaymentDetails_Sale(OrderNumber, dt)
                            If rs = "OK" Then
                                Return "OK"
                            Else
                                Return "ERROR"
                            End If
                        End If
                    End If
                End If
            End If

            If dt.Rows(0)("PaymentGateway") = "PPI" Then
                Dim rs As String
                rs = PaymentDetailsPPI(OrderNumber, dt)
                If rs = "ERROR" Then
                    Return "ERROR"
                End If

                Dim ChargeStatus As String
                Dim ChargeTypes As String
                Try
                    ChargeTypes = dt.Rows(0)("ChargeTypes")
                    ChargeStatus = dt.Rows(0)("ChargeStatus")
                Catch ex As Exception
                    ChargeTypes = "SALE"
                End Try
                If ChargeTypes <> "InvCancel" Then
                    rs = PaymentDetailsPPI_CheckCapture(OrderNumber, dt)
                    If rs = "ERROR" Then
                        Return "ERROR"
                    Else
                        If rs = "OK" Then
                            Return "OK"
                        Else
                            rs = PaymentDetailsPPI_PutSaleTran(OrderNumber, dt)
                            If rs = "OK" Then
                                Return "OK"
                            Else
                                Return "ERROR"
                            End If
                        End If
                    End If
                End If

            End If

        End If


        Return ""
    End Function

    Public Function PaymentDetailsPPI(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim Track1 As String = ""
        Dim Track2 As String = ""

        ''New Feild ''
        Dim AVSCHECK As Boolean = False
        Dim AVSZIPCODECHECK As Boolean = False
        Dim CVSCHECK As String = False
        ''New Feild ''

        Dim PostOrder As New CustomOrder()
        Dim objUser As New DAL.CustomOrder()

        Dim eComerceTransactions, OrderEntryTransactions, POSTransactions As String

        Dim PaymentURL As String = ""

        Dim PPI_ACOUNT_TOKEN As String

        'CompanyID, DepartmentID, DivisionID
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails

        If dt.Rows.Count <> 0 Then
            Dim activetoken As String
            activetoken = dt.Rows(0)("Active")

            If activetoken = "Live" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKEN")

            End If

            If activetoken = "Test" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKENTest")
            End If


            ''New Data Is geting from here''
            Try
                AVSCHECK = CType(dt.Rows(0)("AVS"), Boolean)
            Catch ex As Exception
                AVSCHECK = False
            End Try
            Try
                CVSCHECK = CType(dt.Rows(0)("CVV"), Boolean)
            Catch ex As Exception
                CVSCHECK = False
            End Try
            Try
                AVSZIPCODECHECK = CType(dt.Rows(0)("AVSZIPCODE"), Boolean)
            Catch ex As Exception
                AVSZIPCODECHECK = False
            End Try
            ''New Data Is geting from here''

            eComerceTransactions = dt.Rows(0)("eComerceTransactions")
            OrderEntryTransactions = dt.Rows(0)("OrderEntryTransactions")
            POSTransactions = dt.Rows(0)("POSTransactions")


        End If


        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If


        'Dim Amt As String = txtAdjustmentValueTax.Text
        Dim Amt As String = AmttotalCharge

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        Dim trackdata As String
        trackdata = "" ' intxtrawcard.Value

        If trackdata <> "" Then
            Dim TRACKarray As String()
            TRACKarray = trackdata.Split(";")
            If TRACKarray.Length = 2 Then
                Track1 = TRACKarray(0)
                Track2 = ";" & TRACKarray(1)
            End If
        End If
        '' PPI variable

        Dim Res As Boolean
        Dim result As Integer
        Dim Result2 As Integer
        Dim resResponseCode As String
        Dim resSecondaryResponseCode As String
        Dim resCreditCardVerificationResponse As String
        Dim resAVSCode As String



        Dim obj As New PPIPayMover
        obj.creditCardNumber = CreditCardNumber
        obj.creditCardVerificationNumber = CreditCardCSVNumber
        obj.expireMonth = CreditCardExpMonth
        obj.expireYear = CreditCardExpYear


        obj.chargeTotal = Amt


        'new Code For Caputre The Amount
        Dim ChargeStatus As String
        Dim ChargeTypes As String
        Try
            ChargeTypes = dtpayment.Rows(0)("ChargeTypes")
            ChargeStatus = dtpayment.Rows(0)("ChargeStatus")
        Catch ex As Exception
            ChargeTypes = "SALE"
        End Try
        If ChargeTypes = "InvCancel" Then
            obj.chargeType = "SALE"
        Else
            obj.chargeType = "CAPTURE"
            obj.orderID = dtpayment.Rows(0)("PPIOrderID")
            obj.referenceID = dtpayment.Rows(0)("PPIReferenceID")

        End If
        'new Code For Caputre The Amount



        'CAPTURE
        'CAPTURE

        obj.industry = "RETAIL"
        obj.ACCOUNT_TOKEN = PPI_ACOUNT_TOKEN



        obj.transactionConditionCode = OrderEntryTransactions
        obj.Track1 = Track1
        obj.Track2 = Track2

        obj.billFirstName = txtCustomerFirstName
        obj.billLastName = txtCustomerLastName
        obj.billAddressOne = txtCustomerAddress1
        obj.billAddressTwo = txtCustomerAddress2
        obj.billZipOrPostalCode = txtCustomerZip
        obj.billCity = txtCustomerCity
        obj.billStateOrProvince = txtCustomerState
        obj.billCountry = drpCountry

        ''START'' set this to stop the extra checking for the Card processing
        AVSCHECK = False
        CVSCHECK = False
        AVSZIPCODECHECK = False
        ''STOP'' set this to stop the extra checking for the Card processing


        ''Res = obj.Process_Transaction
        If AVSCHECK = True Or CVSCHECK = True Or AVSZIPCODECHECK = True Then
            obj.chargeType = "AUTH"
            obj.chargeTotal = 1
            Res = obj.Process_Transaction

            If Res Then
                resResponseCode = obj.resResponseCode
                resSecondaryResponseCode = obj.resSecondaryResponseCode
                resCreditCardVerificationResponse = obj.resCreditCardVerificationResponse
                resAVSCode = obj.resAVSCode
                result = obj.resResponseCode
                Result2 = resSecondaryResponseCode

                If result = 1 Then

                    Dim chk As Boolean = True
                    'obj.chargeTotal = AmtPPI - 0.01
                    'Res = obj.Process_Transaction
                    If AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                            If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 4 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If AVSZIPCODECHECK Then
                        If IsNumeric(resAVSCode) Then
                            If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 5 And resAVSCode <> 6 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If CVSCHECK Then

                        If IsNumeric(resCreditCardVerificationResponse) Then
                            If resCreditCardVerificationResponse <> 1 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If chk Then
                        obj.chargeType = "SALE"
                        obj.chargeTotal = Amt
                        Res = obj.Process_Transaction
                    End If

                End If
            Else
                Res = obj.Process_Transaction
                result = obj.resResponseCode
            End If

        Else
            Res = obj.Process_Transaction
            result = obj.resResponseCode
        End If

        Dim misc As String = ""

        If Res Then

            lblerrormessag.Visible = True
            Address = REMOTE_ADDR 'Request.ServerVariables("REMOTE_ADDR")

            Dim chk As Boolean = False
            Select Case result

                Case 0
                    lblerrormessag.Text = " NONE."
                Case 1
                    lblerrormessag.Text = " Your Request has been placed successfully. "
                Case 2
                    lblerrormessag.Text = " Missing required request field."
                Case 3
                    lblerrormessag.Text = " Invalid request field."
                Case 4
                    lblerrormessag.Text = " Illegal transaction request."
                Case 5
                    lblerrormessag.Text = " Transaction server error."
                Case 6
                    lblerrormessag.Text = " Transaction not possible."
                Case 7
                    lblerrormessag.Text = " IInvalid version."
                Case 100
                    lblerrormessag.Text = " Credit card declined."
                Case 101
                    lblerrormessag.Text = " Acquirer gateway error."
                Case 102
                    lblerrormessag.Text = " Payment engine error."
            End Select



            If result = 1 Then
                OrderID = obj.resOrderID
                ReferenceID = obj.resReferenceID
                ApprovalNumber = obj.resBankApprovalCode
                Me.Address = Address
            Else
                If result = -1 Then
                    Dim restxt0 As String = ""
                    Dim restxt1 As String = ""
                    Dim restxt11 As String = ""
                    Dim restxt111 As String = ""
                    Dim restxt2 As String = ""
                    If AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If
                        If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 4 Then
                            Select Case resAVSCode
                                Case 1
                                    restxt0 = "Address do not match. "

                                Case 5
                                    restxt0 = "Address does not match. "
                                Case 6
                                    restxt0 = "Address does not match. "

                            End Select
                        End If
                    End If

                    If AVSZIPCODECHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If
                        If resAVSCode <> 2 And resAVSCode <> 3 Then
                            Select Case resAVSCode
                                Case 1
                                    restxt1 = " Postal Code do not match."
                                Case 4
                                    restxt1 = " Postal/Zip Code does not match."
                            End Select
                        End If
                    End If

                    If AVSZIPCODECHECK Or AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If

                        Select Case resAVSCode
                            Case 7
                                restxt11 = " AVS information is not available for this transaction."
                            Case 8
                                restxt11 = " No response from issuer."
                            Case 9
                                restxt11 = " AVS is not supported for this transaction."
                            Case 10
                                restxt11 = " AVS is not supported by the card issuer."
                            Case 11
                                restxt11 = " AVS is not supported for this card brand."
                            Case 12
                                restxt11 = " AVS information was not specified in the correct format."

                        End Select

                    End If

                    restxt111 = restxt0 & restxt1 & restxt11

                    If CVSCHECK Then
                        If IsNumeric(resCreditCardVerificationResponse) Then
                        Else
                            resCreditCardVerificationResponse = -1
                            restxt2 = "From CreditCard Verification No Response"
                        End If
                        If resCreditCardVerificationResponse <> 1 Then
                            Select Case resCreditCardVerificationResponse
                                Case 2
                                    restxt2 = " Credit Card Verification Not Match."
                                Case 3
                                    restxt2 = " Credit Card Verification Not processed."
                                Case 4
                                    restxt2 = " Credit Card Verification Should have been present."
                                Case 5
                                    restxt2 = " Credit Card Verification Not supported by issuer."
                                Case 6
                                    restxt2 = " Credit Card Verification No response from CVV system."
                            End Select
                        End If
                    End If
                    lblerrormessag.ForeColor = Drawing.Color.Red
                    lblerrormessag.Text = "Error Occured While Payment Processing : <b>" & restxt111 & "     " & restxt2 & " <b>"
                    'Response.Redirect("PaymentInfo.aspx?SessID=" & BwrID & "&Result1=" & "     " & restxt111 & "     " & restxt2, False)
                    Return "ERROR"
                End If

            End If


        Else
            lblerrormessag.ForeColor = Drawing.Color.Red
            Try
                lblerrormessag.Text = "General error:Payment engine error on PPI Error: number:" & obj.resResponseCode
            Catch ex As Exception
                lblerrormessag.Text = "General error:Payment engine error on PPI"
            End Try
            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"
        End If

        Return ""
    End Function


    Public Function PaymentDetails(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String

        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""

        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If
        CreditCardDateString = CreditCardExpMonth + CreditCardExpYear.Substring(2)

        Dim strRequestID As String
        strRequestID = PayflowUtility.RequestId
        Dim Inv As New Invoice()

        Dim objUser As New DAL.CustomOrder()

        Dim rs As SqlDataReader

        rs = objUser.PopulatePaymentAccounts(CompanyID, DivisionID, DepartmentID)

        While rs.Read()

            Partnername = rs("Partnername").ToString()
            Vendorname = rs("Vendorname").ToString()
            If rs("Username").ToString() = "" Then
                UserName = rs("Vendorname").ToString()
            Else
                UserName = rs("Username").ToString()
            End If


            Password = rs("Password").ToString()
            If rs("TestMode").ToString() = "True" Then
                PaymentURL = rs("Testurl").ToString()
            Else
                PaymentURL = rs("Liveurl").ToString()
            End If

        End While
        rs.Close()



        Dim User As New UserInfo(UserName, Vendorname, Partnername, Password)
        Dim Connection As New PayflowConnectionData(PaymentURL, 443, "", 0, "", "")


        Dim Amt As New Currency(AmttotalCharge)

        Inv.Amt = Amt
        Inv.ItemAmt = Amt
        Amt.NoOfDecimalDigits = 2
        Amt.Round = True

        Inv.PoNum = "PO12345"
        Inv.InvNum = "INV12345"
        Inv.CustRef = "CustRef1"
        Inv.Comment1 = CompanyID
        Inv.Comment2 = OrdNumber

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        Dim CC As New CreditCard(CreditCardNumber, CreditCardDateString)

        CC.Cvv2 = CreditCardCSVNumber

        'CC.Name = "Joe M Smith"
        Dim Card As New CardTender(CC)
        'Dim Trans As New SaleTransaction(User, Connection, Inv, Card, strRequestID)
        'Dim Trans As New CaptureTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, Card, strRequestID)
        'Dim Transnew As New VoidTransaction(
        Dim Resp As Response ' = Trans.SubmitTransaction()


        Dim ChargeStatus As String
        Dim ChargeTypes As String
        Try
            ChargeTypes = dtpayment.Rows(0)("ChargeTypes")
            ChargeStatus = dtpayment.Rows(0)("ChargeStatus")
        Catch ex As Exception
            ChargeTypes = "SALE"
        End Try
        If ChargeTypes = "InvCancel" Then
            Dim Trans As New SaleTransaction(User, Connection, Inv, Card, strRequestID)
            Resp = Trans.SubmitTransaction()
        Else
            Dim Trans As New CaptureTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
            Resp = Trans.SubmitTransaction()
        End If
        'new Code For Caputre The Amount


        If Not (Resp Is Nothing) Then

            lblerrormessag.Visible = True

            Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
            Dim Result As Integer = TrxnResponse.Result
            Dim RespMsg As String = TrxnResponse.RespMsg
            Dim AuthCode As String = TrxnResponse.AuthCode
            ApprovalNumber = TrxnResponse.Pnref
            PayPalPNREF = TrxnResponse.Pnref
            Dim Code As String = TrxnResponse.CVV2Match
            Address = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = PayflowUtility.GetStatus(Resp)
            If Not (TrxnResponse Is Nothing) Then
            End If

            Dim TransCtx As Context = Resp.TransactionContext
            Select Case Result
                Case -1
                    lblerrormessag.Text = " Failed to connect to Host."
                Case 0
                    lblerrormessag.Text = " Your order has been placed successfully ."
                Case 1
                    lblerrormessag.Text = " User Authentication Failed.  Please contact Customer Service. "
                Case 12
                    lblerrormessag.Text = " Your transaction was declined."
                Case 13
                    lblerrormessag.Text = " Your Transactions was declined."
                Case 4
                    lblerrormessag.Text = " Invalid Amount."
                Case 23
                    lblerrormessag.Text = " Invalid Account Number."
                Case 24
                    lblerrormessag.Text = " Invalid Credit Card Expiration Date."
                Case 124
                    lblerrormessag.Text = " Your Transactions has been declined. Contact Customer Service."
            End Select

            If Result = 0 Then
                'txtApproval.Text = ApprovalNumber
                'txtIpAddress.Text = Address
                'Code Added For Updating Credit Card Validation Code returned From Paypal
                'objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)
                'PostingOrder(OrdNumber)
            Else
                Return "ERROR"
            End If

        End If

        Return ""
    End Function

    Public Function PaymentDetailsPPI_CheckCapture(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim Track1 As String = ""
        Dim Track2 As String = ""

        ''New Feild ''
        Dim AVSCHECK As Boolean = False
        Dim AVSZIPCODECHECK As Boolean = False
        Dim CVSCHECK As String = False
        ''New Feild ''

        Dim PostOrder As New CustomOrder()
        Dim objUser As New DAL.CustomOrder()

        Dim eComerceTransactions, OrderEntryTransactions, POSTransactions As String

        Dim PaymentURL As String = ""

        Dim PPI_ACOUNT_TOKEN As String

        'CompanyID, DepartmentID, DivisionID
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails

        If dt.Rows.Count <> 0 Then
            Dim activetoken As String
            activetoken = dt.Rows(0)("Active")

            If activetoken = "Live" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKEN")

            End If

            If activetoken = "Test" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKENTest")
            End If


            ''New Data Is geting from here''
            Try
                AVSCHECK = CType(dt.Rows(0)("AVS"), Boolean)
            Catch ex As Exception
                AVSCHECK = False
            End Try
            Try
                CVSCHECK = CType(dt.Rows(0)("CVV"), Boolean)
            Catch ex As Exception
                CVSCHECK = False
            End Try
            Try
                AVSZIPCODECHECK = CType(dt.Rows(0)("AVSZIPCODE"), Boolean)
            Catch ex As Exception
                AVSZIPCODECHECK = False
            End Try
            ''New Data Is geting from here''

            eComerceTransactions = dt.Rows(0)("eComerceTransactions")
            OrderEntryTransactions = dt.Rows(0)("OrderEntryTransactions")
            POSTransactions = dt.Rows(0)("POSTransactions")


        End If


        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If


        'Dim Amt As String = txtAdjustmentValueTax.Text
        Dim Amt As String = AmttotalCharge

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        Dim trackdata As String
        trackdata = "" ' intxtrawcard.Value

        If trackdata <> "" Then
            Dim TRACKarray As String()
            TRACKarray = trackdata.Split(";")
            If TRACKarray.Length = 2 Then
                Track1 = TRACKarray(0)
                Track2 = ";" & TRACKarray(1)
            End If
        End If
        '' PPI variable

        Dim Res As Boolean
        Dim result As Integer
        Dim Result2 As Integer
        Dim resResponseCode As String
        Dim resSecondaryResponseCode As String
        Dim resCreditCardVerificationResponse As String
        Dim resAVSCode As String



        Dim obj As New PPIPayMover
        obj.creditCardNumber = CreditCardNumber
        obj.creditCardVerificationNumber = CreditCardCSVNumber
        obj.expireMonth = CreditCardExpMonth
        obj.expireYear = CreditCardExpYear


        obj.chargeTotal = Amt

        obj.chargeType = "QUERY_PAYMENT"

        obj.orderID = dtpayment.Rows(0)("PPIOrderID")
        obj.referenceID = dtpayment.Rows(0)("PPIReferenceID")

        obj.industry = "RETAIL"
        obj.ACCOUNT_TOKEN = PPI_ACOUNT_TOKEN



        obj.transactionConditionCode = OrderEntryTransactions
        obj.Track1 = Track1
        obj.Track2 = Track2

        obj.billFirstName = txtCustomerFirstName
        obj.billLastName = txtCustomerLastName
        obj.billAddressOne = txtCustomerAddress1
        obj.billAddressTwo = txtCustomerAddress2
        obj.billZipOrPostalCode = txtCustomerZip
        obj.billCity = txtCustomerCity
        obj.billStateOrProvince = txtCustomerState
        obj.billCountry = drpCountry

        Res = obj.Process_Transaction
        result = obj.resResponseCode

        Dim misc As String = ""

        If Res Then

            lblerrormessag.Visible = True
            Address = REMOTE_ADDR 'Request.ServerVariables("REMOTE_ADDR")

            Dim chk As Boolean = False
            Select Case result

                Case 0
                    lblerrormessag.Text = " NONE."
                Case 1
                    lblerrormessag.Text = " Your Request has been placed successfully. "
                Case 2
                    lblerrormessag.Text = " Missing required request field."
                Case 3
                    lblerrormessag.Text = " Invalid request field."
                Case 4
                    lblerrormessag.Text = " Illegal transaction request."
                Case 5
                    lblerrormessag.Text = " Transaction server error."
                Case 6
                    lblerrormessag.Text = " Transaction not possible."
                Case 7
                    lblerrormessag.Text = " IInvalid version."
                Case 100
                    lblerrormessag.Text = " Credit card declined."
                Case 101
                    lblerrormessag.Text = " Acquirer gateway error."
                Case 102
                    lblerrormessag.Text = " Payment engine error."
            End Select



            If result = 1 Then
                OrderID = obj.resOrderID
                ReferenceID = obj.resReferenceID
                ApprovalNumber = obj.resBankApprovalCode
                Me.Address = Address

                Dim state As String = ""
                state = obj.resState

                If state = "payment_deposited" Or state = "payment_closed" Then
                    Return "OK"
                Else
                    Return "FAIL"
                End If

            Else
                lblerrormessag.Text = "Error number from PPI :" & result
                Return "ERROR"
            End If


        Else
            lblerrormessag.ForeColor = Drawing.Color.Red
            Try
                lblerrormessag.Text = "General error:Payment engine error on PPI Error: number:" & obj.resResponseCode
            Catch ex As Exception
                lblerrormessag.Text = "General error:Payment engine error on PPI"
            End Try
            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"
        End If

        Return ""
    End Function

    Public Function PaymentDetailsPPI_PutSaleTran(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim Track1 As String = ""
        Dim Track2 As String = ""

        ''New Feild ''
        Dim AVSCHECK As Boolean = False
        Dim AVSZIPCODECHECK As Boolean = False
        Dim CVSCHECK As String = False
        ''New Feild ''

        Dim PostOrder As New CustomOrder()
        Dim objUser As New DAL.CustomOrder()

        Dim eComerceTransactions, OrderEntryTransactions, POSTransactions As String

        Dim PaymentURL As String = ""

        Dim PPI_ACOUNT_TOKEN As String

        'CompanyID, DepartmentID, DivisionID
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails

        If dt.Rows.Count <> 0 Then
            Dim activetoken As String
            activetoken = dt.Rows(0)("Active")

            If activetoken = "Live" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKEN")

            End If

            If activetoken = "Test" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKENTest")
            End If


            ''New Data Is geting from here''
            Try
                AVSCHECK = CType(dt.Rows(0)("AVS"), Boolean)
            Catch ex As Exception
                AVSCHECK = False
            End Try
            Try
                CVSCHECK = CType(dt.Rows(0)("CVV"), Boolean)
            Catch ex As Exception
                CVSCHECK = False
            End Try
            Try
                AVSZIPCODECHECK = CType(dt.Rows(0)("AVSZIPCODE"), Boolean)
            Catch ex As Exception
                AVSZIPCODECHECK = False
            End Try
            ''New Data Is geting from here''

            eComerceTransactions = dt.Rows(0)("eComerceTransactions")
            OrderEntryTransactions = dt.Rows(0)("OrderEntryTransactions")
            POSTransactions = dt.Rows(0)("POSTransactions")


        End If


        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If


        'Dim Amt As String = txtAdjustmentValueTax.Text
        Dim Amt As String = AmttotalCharge

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        Dim trackdata As String
        trackdata = "" ' intxtrawcard.Value

        If trackdata <> "" Then
            Dim TRACKarray As String()
            TRACKarray = trackdata.Split(";")
            If TRACKarray.Length = 2 Then
                Track1 = TRACKarray(0)
                Track2 = ";" & TRACKarray(1)
            End If
        End If
        '' PPI variable

        Dim Res As Boolean
        Dim result As Integer
        Dim Result2 As Integer
        Dim resResponseCode As String
        Dim resSecondaryResponseCode As String
        Dim resCreditCardVerificationResponse As String
        Dim resAVSCode As String



        Dim obj As New PPIPayMover
        obj.creditCardNumber = CreditCardNumber
        obj.creditCardVerificationNumber = CreditCardCSVNumber
        obj.expireMonth = CreditCardExpMonth
        obj.expireYear = CreditCardExpYear

        obj.chargeTotal = Amt

        obj.chargeType = "SALE"

        obj.industry = "RETAIL"
        obj.ACCOUNT_TOKEN = PPI_ACOUNT_TOKEN



        obj.transactionConditionCode = OrderEntryTransactions
        obj.Track1 = Track1
        obj.Track2 = Track2

        obj.billFirstName = txtCustomerFirstName
        obj.billLastName = txtCustomerLastName
        obj.billAddressOne = txtCustomerAddress1
        obj.billAddressTwo = txtCustomerAddress2
        obj.billZipOrPostalCode = txtCustomerZip
        obj.billCity = txtCustomerCity
        obj.billStateOrProvince = txtCustomerState
        obj.billCountry = drpCountry

        ''START'' set this to stop the extra checking for the Card processing
        AVSCHECK = False
        CVSCHECK = False
        AVSZIPCODECHECK = False
        ''STOP'' set this to stop the extra checking for the Card processing


        ''Res = obj.Process_Transaction
        If AVSCHECK = True Or CVSCHECK = True Or AVSZIPCODECHECK = True Then
            obj.chargeType = "AUTH"
            obj.chargeTotal = 1
            Res = obj.Process_Transaction

            If Res Then
                resResponseCode = obj.resResponseCode
                resSecondaryResponseCode = obj.resSecondaryResponseCode
                resCreditCardVerificationResponse = obj.resCreditCardVerificationResponse
                resAVSCode = obj.resAVSCode
                result = obj.resResponseCode
                Result2 = resSecondaryResponseCode

                If result = 1 Then

                    Dim chk As Boolean = True
                    'obj.chargeTotal = AmtPPI - 0.01
                    'Res = obj.Process_Transaction
                    If AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                            If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 4 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If AVSZIPCODECHECK Then
                        If IsNumeric(resAVSCode) Then
                            If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 5 And resAVSCode <> 6 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If CVSCHECK Then

                        If IsNumeric(resCreditCardVerificationResponse) Then
                            If resCreditCardVerificationResponse <> 1 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If chk Then
                        obj.chargeType = "SALE"
                        obj.chargeTotal = Amt
                        Res = obj.Process_Transaction
                    End If

                End If
            Else
                Res = obj.Process_Transaction
                result = obj.resResponseCode
            End If

        Else
            Res = obj.Process_Transaction
            result = obj.resResponseCode
        End If

        Dim misc As String = ""

        If Res Then

            lblerrormessag.Visible = True
            Address = REMOTE_ADDR 'Request.ServerVariables("REMOTE_ADDR")

            Dim chk As Boolean = False
            Select Case result

                Case 0
                    lblerrormessag.Text = " NONE."
                Case 1
                    lblerrormessag.Text = " Your Request has been placed successfully. "
                Case 2
                    lblerrormessag.Text = " Missing required request field."
                Case 3
                    lblerrormessag.Text = " Invalid request field."
                Case 4
                    lblerrormessag.Text = " Illegal transaction request."
                Case 5
                    lblerrormessag.Text = " Transaction server error."
                Case 6
                    lblerrormessag.Text = " Transaction not possible."
                Case 7
                    lblerrormessag.Text = " IInvalid version."
                Case 100
                    lblerrormessag.Text = " Credit card declined."
                Case 101
                    lblerrormessag.Text = " Acquirer gateway error."
                Case 102
                    lblerrormessag.Text = " Payment engine error."
            End Select



            If result = 1 Then
                OrderID = obj.resOrderID
                ReferenceID = obj.resReferenceID
                ApprovalNumber = obj.resBankApprovalCode
                Me.Address = Address
                Return "OK"
            Else
                If result = -1 Then
                    Dim restxt0 As String = ""
                    Dim restxt1 As String = ""
                    Dim restxt11 As String = ""
                    Dim restxt111 As String = ""
                    Dim restxt2 As String = ""
                    If AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If
                        If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 4 Then
                            Select Case resAVSCode
                                Case 1
                                    restxt0 = "Address do not match. "

                                Case 5
                                    restxt0 = "Address does not match. "
                                Case 6
                                    restxt0 = "Address does not match. "

                            End Select
                        End If
                    End If

                    If AVSZIPCODECHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If
                        If resAVSCode <> 2 And resAVSCode <> 3 Then
                            Select Case resAVSCode
                                Case 1
                                    restxt1 = " Postal Code do not match."
                                Case 4
                                    restxt1 = " Postal/Zip Code does not match."
                            End Select
                        End If
                    End If

                    If AVSZIPCODECHECK Or AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If

                        Select Case resAVSCode
                            Case 7
                                restxt11 = " AVS information is not available for this transaction."
                            Case 8
                                restxt11 = " No response from issuer."
                            Case 9
                                restxt11 = " AVS is not supported for this transaction."
                            Case 10
                                restxt11 = " AVS is not supported by the card issuer."
                            Case 11
                                restxt11 = " AVS is not supported for this card brand."
                            Case 12
                                restxt11 = " AVS information was not specified in the correct format."

                        End Select

                    End If

                    restxt111 = restxt0 & restxt1 & restxt11

                    If CVSCHECK Then
                        If IsNumeric(resCreditCardVerificationResponse) Then
                        Else
                            resCreditCardVerificationResponse = -1
                            restxt2 = "From CreditCard Verification No Response"
                        End If
                        If resCreditCardVerificationResponse <> 1 Then
                            Select Case resCreditCardVerificationResponse
                                Case 2
                                    restxt2 = " Credit Card Verification Not Match."
                                Case 3
                                    restxt2 = " Credit Card Verification Not processed."
                                Case 4
                                    restxt2 = " Credit Card Verification Should have been present."
                                Case 5
                                    restxt2 = " Credit Card Verification Not supported by issuer."
                                Case 6
                                    restxt2 = " Credit Card Verification No response from CVV system."
                            End Select
                        End If
                    End If
                    lblerrormessag.ForeColor = Drawing.Color.Red
                    lblerrormessag.Text = "Error Occured While Payment Processing : <b>" & restxt111 & "     " & restxt2 & " <b>"
                    'Response.Redirect("PaymentInfo.aspx?SessID=" & BwrID & "&Result1=" & "     " & restxt111 & "     " & restxt2, False)
                    Return "ERROR"
                End If

            End If


        Else
            lblerrormessag.ForeColor = Drawing.Color.Red
            Try
                lblerrormessag.Text = "General error:Payment engine error on PPI Error: number:" & obj.resResponseCode
            Catch ex As Exception
                lblerrormessag.Text = "General error:Payment engine error on PPI"
            End Try
            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"
        End If

        Return ""
    End Function


    Public Function PaymentDetails_CheckCapture(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String

        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""

        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If
        CreditCardDateString = CreditCardExpMonth + CreditCardExpYear.Substring(2)

        Dim strRequestID As String
        strRequestID = PayflowUtility.RequestId
        Dim Inv As New Invoice()

        Dim objUser As New DAL.CustomOrder()

        Dim rs As SqlDataReader

        rs = objUser.PopulatePaymentAccounts(CompanyID, DivisionID, DepartmentID)

        While rs.Read()

            Partnername = rs("Partnername").ToString()
            Vendorname = rs("Vendorname").ToString()
            If rs("Username").ToString() = "" Then
                UserName = rs("Vendorname").ToString()
            Else
                UserName = rs("Username").ToString()
            End If


            Password = rs("Password").ToString()
            If rs("TestMode").ToString() = "True" Then
                PaymentURL = rs("Testurl").ToString()
            Else
                PaymentURL = rs("Liveurl").ToString()
            End If

        End While
        rs.Close()



        Dim User As New UserInfo(UserName, Vendorname, Partnername, Password)
        Dim Connection As New PayflowConnectionData(PaymentURL, 443, "", 0, "", "")


        Dim Amt As New Currency(AmttotalCharge)

        Inv.Amt = Amt
        Inv.ItemAmt = Amt
        Amt.NoOfDecimalDigits = 2
        Amt.Round = True

        Inv.PoNum = "PO12345"
        Inv.InvNum = "INV12345"
        Inv.CustRef = "CustRef1"
        Inv.Comment1 = CompanyID
        Inv.Comment2 = OrdNumber

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        Dim CC As New CreditCard(CreditCardNumber, CreditCardDateString)

        CC.Cvv2 = CreditCardCSVNumber

        'CC.Name = "Joe M Smith"
        Dim Card As New CardTender(CC)
 
        Dim Resp As Response ' = Trans.SubmitTransaction()
        'Dim Trans As New CreditTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
        Dim Trans As New InquiryTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
        Trans.Verbosity = "MEDIUM"
        Resp = Trans.SubmitTransaction()


        If Not (Resp Is Nothing) Then

            lblerrormessag.Visible = True

            Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
            Dim Result As Integer = TrxnResponse.Result
            Dim RespMsg As String = TrxnResponse.RespMsg
            Dim AuthCode As String = TrxnResponse.AuthCode
            ApprovalNumber = TrxnResponse.Pnref
            PayPalPNREF = TrxnResponse.Pnref
            Dim Code As String = TrxnResponse.CVV2Match
            Address = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = PayflowUtility.GetStatus(Resp)
            If Not (TrxnResponse Is Nothing) Then
            End If

            Dim TransCtx As Context = Resp.TransactionContext
            Select Case Result
                Case -1
                    lblerrormessag.Text = " Failed to connect to Host."
                Case 0
                    lblerrormessag.Text = " Your order has been placed successfully ."
                Case 1
                    lblerrormessag.Text = " User Authentication Failed.  Please contact Customer Service. "
                Case 12
                    lblerrormessag.Text = " Your transaction was declined."
                Case 13
                    lblerrormessag.Text = " Your Transactions was declined."
                Case 4
                    lblerrormessag.Text = " Invalid Amount."
                Case 23
                    lblerrormessag.Text = " Invalid Account Number."
                Case 24
                    lblerrormessag.Text = " Invalid Credit Card Expiration Date."
                Case 124
                    lblerrormessag.Text = " Your Transactions has been declined. Contact Customer Service."
            End Select

            If Result = 0 Then
                'txtApproval.Text = ApprovalNumber
                'txtIpAddress.Text = Address
                'Code Added For Updating Credit Card Validation Code returned From Paypal
                'objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)
                'PostingOrder(OrdNumber)
                Dim state As String = ""
                state = TrxnResponse.TransState

                If state = "9" Or state = "7" Or state = "8" Then
                    Return "OK"
                Else
                    Return "FAIL"
                End If

            Else
                Return "ERROR"
            End If

        End If

        Return ""
    End Function


    Public Function PaymentDetails_Sale(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String

        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""

        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If
        CreditCardDateString = CreditCardExpMonth + CreditCardExpYear.Substring(2)

        Dim strRequestID As String
        strRequestID = PayflowUtility.RequestId
        Dim Inv As New Invoice()

        Dim objUser As New DAL.CustomOrder()

        Dim rs As SqlDataReader

        rs = objUser.PopulatePaymentAccounts(CompanyID, DivisionID, DepartmentID)

        While rs.Read()

            Partnername = rs("Partnername").ToString()
            Vendorname = rs("Vendorname").ToString()
            If rs("Username").ToString() = "" Then
                UserName = rs("Vendorname").ToString()
            Else
                UserName = rs("Username").ToString()
            End If


            Password = rs("Password").ToString()
            If rs("TestMode").ToString() = "True" Then
                PaymentURL = rs("Testurl").ToString()
            Else
                PaymentURL = rs("Liveurl").ToString()
            End If

        End While
        rs.Close()



        Dim User As New UserInfo(UserName, Vendorname, Partnername, Password)
        Dim Connection As New PayflowConnectionData(PaymentURL, 443, "", 0, "", "")


        Dim Amt As New Currency(AmttotalCharge)

        Inv.Amt = Amt
        Inv.ItemAmt = Amt
        Amt.NoOfDecimalDigits = 2
        Amt.Round = True

        Inv.PoNum = "PO12345"
        Inv.InvNum = "INV12345"
        Inv.CustRef = "CustRef1"
        Inv.Comment1 = CompanyID
        Inv.Comment2 = OrdNumber

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        Dim CC As New CreditCard(CreditCardNumber, CreditCardDateString)

        CC.Cvv2 = CreditCardCSVNumber

        'CC.Name = "Joe M Smith"
        Dim Card As New CardTender(CC)
        Dim Trans As New SaleTransaction(User, Connection, Inv, Card, strRequestID)
        Dim Resp As Response = Trans.SubmitTransaction()

        If Not (Resp Is Nothing) Then

            lblerrormessag.Visible = True

            Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
            Dim Result As Integer = TrxnResponse.Result
            Dim RespMsg As String = TrxnResponse.RespMsg
            Dim AuthCode As String = TrxnResponse.AuthCode
            ApprovalNumber = TrxnResponse.Pnref
            PayPalPNREF = TrxnResponse.Pnref
            Dim Code As String = TrxnResponse.CVV2Match
            Address = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = PayflowUtility.GetStatus(Resp)
            If Not (TrxnResponse Is Nothing) Then
            End If

            Dim TransCtx As Context = Resp.TransactionContext
            Select Case Result
                Case -1
                    lblerrormessag.Text = " Failed to connect to Host."
                Case 0
                    lblerrormessag.Text = " Your order has been placed successfully ."
                Case 1
                    lblerrormessag.Text = " User Authentication Failed.  Please contact Customer Service. "
                Case 12
                    lblerrormessag.Text = " Your transaction was declined."
                Case 13
                    lblerrormessag.Text = " Your Transactions was declined."
                Case 4
                    lblerrormessag.Text = " Invalid Amount."
                Case 23
                    lblerrormessag.Text = " Invalid Account Number."
                Case 24
                    lblerrormessag.Text = " Invalid Credit Card Expiration Date."
                Case 124
                    lblerrormessag.Text = " Your Transactions has been declined. Contact Customer Service."
            End Select

            If Result = 0 Then
                'txtApproval.Text = ApprovalNumber
                'txtIpAddress.Text = Address
                'Code Added For Updating Credit Card Validation Code returned From Paypal
                'objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)
                'PostingOrder(OrdNumber)
                Return "OK"
            Else
                Return "ERROR"
            End If

        End If

        Return ""
    End Function
End Class
