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

Public Class SecondTimeBookPaymentCheck2nd


    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    'Dim OrderNumber As String = ""

   
    Dim txtCustomerFirstName As String = ""
    Dim txtCustomerLastName As String = ""
    Dim txtCustomerAddress1 As String = ""
    Dim txtCustomerAddress2 As String = ""
    Dim txtCustomerZip As String = ""
    Dim txtCustomerCity As String = ""
    Dim txtCustomerState As String = ""
    Dim drpCountry As String = ""

    Dim OrderID As String = ""
    Dim ReferenceID As String = ""
    Dim ApprovalNumber As String = ""
    Dim Address As String = ""

    Dim PayPalPNREF As String = ""




    Public Function CreditCardProcessingDetails2ndFirstEntry(ByVal Ordernumber As String) As Boolean

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("dbo.[StoreOrderHeaderCreditCardProcessingDetails2ndFirstEntry]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)



        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Ordernumber
        myCommand.Parameters.Add(parameterOrderNumber)



        myCon.Open()

        myCommand.ExecuteNonQuery()
        myCon.Close()


        Return True

    End Function


    Public Function ProcessRefund(ByVal OrderNumber As String, ByVal RefundamountPass As Decimal) As String
        Dim OrdNumber As String = ""
        OrdNumber = OrderNumber
        Dim objUser As New DAL.CustomOrder()
        Dim ApprovalNumber As String = ""
        Dim Address As String = ""
        Address = ""
        ApprovalNumber = ""

        BindCustomerDetails(Me.CompanyID, Me.DepartmentID, Me.DivisionID, OrderNumber)
        CreditCardProcessingDetails2ndFirstEntry(OrderNumber)

        Dim obj1 As New clsOrderAdjustments2nd
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        obj1.OrderNumber = OrderNumber

        Dim dt As New Data.DataTable
        dt = obj1.DetailsCreditCardPaymentSalesDetails()

        If dt.Rows.Count <> 0 Then

            Dim n As Integer = 0
            Dim runningamount As Decimal = RefundamountPass

            Dim golbalrs As String = ""

            For n = 0 To dt.Rows.Count - 1

                If runningamount = 0 Then
                    Exit For
                End If

                Dim inlinenumber As Integer
                Dim finalamount As Decimal = 0
                Dim RefundAmount As Decimal = 0
                Dim PaymentAmount As Decimal = 0
                Dim PassAmount As Decimal = 0

                inlinenumber = dt.Rows(n)("InLIneNUmber")
                Try
                    PaymentAmount = dt.Rows(n)("PaymentAmount")
                Catch ex As Exception

                End Try
                Try
                    RefundAmount = dt.Rows(n)("RefundAmount")
                Catch ex As Exception

                End Try

                finalamount = PaymentAmount - RefundAmount

                If finalamount >= runningamount Then
                    PassAmount = runningamount
                    runningamount = 0
                End If

                If finalamount < runningamount Then
                    PassAmount = finalamount
                    runningamount = runningamount - finalamount
                End If


                Dim dtpayment As New Data.DataTable
                dtpayment = obj1.DetailsCreditCardPaymentDetailsinlinenumber(inlinenumber)


                If dtpayment.Rows.Count > 0 Then
                    If dt.Rows(0)("PaymentGateway") = "PayPal" Then
                        Dim rs As String
                        'rs = PaymentDetails(OrderNumber, dt)

                    End If
                    If dt.Rows(0)("PaymentGateway") = "PPI" Then
                        Dim rs As String
                        rs = PaymentDetailsPPIRefundCharge(OrderNumber, dtpayment, PassAmount)
                        If rs = "Offline" Then
                            Return "Offline"
                        Else
                            'Return rs
                            golbalrs = "Approved"
                        End If
                    End If


                    If dt.Rows(0)("PaymentGateway") = "MerchantWARE" Then
                        Dim rs As String
                        'rs = PaymentMecrhantWareDetails(OrderNumber, dt)
                    End If

                    If dt.Rows(0)("PaymentGateway") = "AuthorizeNet" Then
                        Dim rs As String
                        'rs = PaymentAuthorizeNetDetails(OrderNumber, dt)
                    End If

                    If dt.Rows(0)("PaymentGateway") = "Mercury" Then
                        Dim rs As String
                        'rs = PaymentMercuryDetails(OrderNumber, dt)
                    End If

                End If

            Next

            Return golbalrs

        End If

        Return False
    End Function




    Public Function PaymentDetailsPPIRefundCharge(ByVal OrdNumber As String, ByVal dtpayment As DataTable, ByVal amount As Decimal) As String
        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "PPI"
        objclsPaymentGatewayTransactionLogs.RefrenaceType = "Order"
        objclsPaymentGatewayTransactionLogs.ProcessDate = Date.Now


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
 
        If CompanyID = "Greene and Greene" Or CompanyID = "Ovando Floral and Event Design-10065" Then

            dt = obj1.FillDetailsPaymentGatwayByOrder(OrdNumber)

            If (dt.Rows.Count = 0) Then
                dt = obj1.FillDetailsPPI(lblOrderType.Text, lblOrderLocation.Text)
            End If

        Else
            dt = obj1.FillDetails(lblOrderType.Text)
        End If


        If dt.Rows.Count <> 0 Then
            Dim activetoken As String
            activetoken = dt.Rows(0)("Active")

            If activetoken = "Live" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKEN")

            End If

            If activetoken = "Test" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKENTest")
            End If


            eComerceTransactions = dt.Rows(0)("eComerceTransactions")
            OrderEntryTransactions = dt.Rows(0)("OrderEntryTransactions")
            POSTransactions = dt.Rows(0)("POSTransactions")

        End If


        ccExp = Convert.ToDateTime(drpExpirationDate.Text).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If
         
        Dim Amt As String = amount

        CreditCardNumber = txtCard.Text
        CreditCardCSVNumber = txtCSV.Text

        Dim trackdata As String
        trackdata = ""  

        Dim Res As Boolean
        Dim result As Integer
          
        Dim processdate As Date
        Dim chkDay As Boolean = False
        Try
            processdate = dtpayment.Rows(0)("ProcessDate")
        Catch ex As Exception
            processdate = "11/1/1900"
        End Try
          

        Dim obj As New PPIPayMover
        obj.creditCardNumber = CreditCardNumber
        obj.creditCardVerificationNumber = CreditCardCSVNumber
        obj.expireMonth = CreditCardExpMonth
        obj.expireYear = CreditCardExpYear
        obj.chargeTotal = Amt
         
        obj.chargeType = "CREDIT"
        obj.industry = "RETAIL"
        obj.ACCOUNT_TOKEN = PPI_ACOUNT_TOKEN

        obj.orderID = dtpayment.Rows(0)("PPIOrderID")
        obj.referenceID = dtpayment.Rows(0)("PPIReferenceID")
         
        obj.transactionConditionCode = OrderEntryTransactions
        obj.Track1 = Track1
        obj.Track2 = Track2

        'Dim obj_mail As New clsErrorMailHandling
        'obj_mail.OrderNumber = OrdNumber
        'obj_mail.ErrorMailHandling("New CC process Check resp = false", OrdNumber, "obj.referenceID=" & obj.referenceID & "--obj.orderID=" & obj.orderID & "--PPI_ACOUNT_TOKEN=" & PPI_ACOUNT_TOKEN)


        obj.billFirstName = txtCustomerFirstName
        obj.billLastName = txtCustomerLastName
        obj.billAddressOne = txtCustomerAddress1
        obj.billAddressTwo = txtCustomerAddress2
        obj.billZipOrPostalCode = txtCustomerZip
        obj.billCity = txtCustomerCity
        obj.billStateOrProvince = txtCustomerState
        obj.billCountry = drpCountry

         
        objclsPaymentGatewayTransactionLogs.ProcessAmount = obj.chargeTotal
        objclsPaymentGatewayTransactionLogs.ProcessType = obj.chargeType
        objclsPaymentGatewayTransactionLogs.ProcessDetails = "Order refund adjustment amount change Process from POS"
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = obj.creditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = obj.expireMonth & "/" & obj.expireYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = obj.creditCardVerificationNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()
         
        Dim misc As String = ""

        Res = obj.Process_Transaction
        result = obj.resResponseCode

        If Res Then

            lblCCMessage.Visible = True
            Address = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")

            Dim chk As Boolean = False

            Select Case result
                Case 0
                    lblCCMessage.Text = " NONE."
                Case 1
                    lblCCMessage.Text = " Your Request has been placed successfully. "
                Case 2
                    lblCCMessage.Text = " Missing required request field."
                Case 3
                    lblCCMessage.Text = " Invalid request field."
                Case 4
                    lblCCMessage.Text = " Illegal transaction request."
                Case 5
                    lblCCMessage.Text = " Transaction server error."
                Case 6
                    lblCCMessage.Text = " Transaction not possible."
                Case 7
                    lblCCMessage.Text = " IInvalid version."
                Case 100
                    lblCCMessage.Text = " Credit card declined."
                Case 101
                    lblCCMessage.Text = " Acquirer gateway error."
                Case 102
                    lblCCMessage.Text = " Payment engine error."
            End Select



            If result = 1 Then
                OrderID = obj.resOrderID
                ReferenceID = obj.resReferenceID
                ApprovalNumber = obj.resBankApprovalCode
                Me.Address = Address


                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text & " and   ResponseCodeText = " & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.PPIOrderID = obj.resOrderID
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

                ''Storing Paypment Process details on new table for further Cancle it
                Try
                    Dim objpayment As New clsOrderAdjustments2nd
                    objpayment.CompanyID = CompanyID
                    objpayment.DivisionID = DivisionID
                    objpayment.DepartmentID = DepartmentID
                    objpayment.OrderNumber = OrdNumber
                    objpayment.PaymentGateway = "PPI"
                    objpayment.PayPalPNREF = ""
                    objpayment.PPIOrderID = obj.resOrderID
                    objpayment.PPIReferenceID = obj.resReferenceID
                    objpayment.PaymentAmount = amount

                    objpayment.ChargeTypes = obj.chargeType
                    objpayment.ChargeStatus = obj.chargeType

                    ''New Lines Added
                    objpayment.NewCreditCardNumber = txtCard.Text
                    objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.Text).Date
                    objpayment.NewCreditCardCSVNumber = "" ' txtCSV.Text
                    ''New Lines Added
                    objpayment.InsertCreditCardPaymentDetails()

                    objpayment.RefundAmount = amount
                    objpayment.UpdateCreditCardPaymentRefundAmount(dtpayment.Rows(0)("InLIneNUmber"))

                Catch ex As Exception
                End Try
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Return ApprovalNumber

            Else
                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text & " and   ResponseCodeText = " & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.PPIOrderID = obj.resOrderID
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

                Return "Offline"
            End If


        Else
            'Dim obj_mail As New clsErrorMailHandling
            'obj_mail.OrderNumber = OrdNumber
            'obj_mail.ErrorMailHandling("New CC process Check resp = false", OrdNumber, lblCCMessage.Text)


            lblCCMessage.ForeColor = Drawing.Color.Red
            Try
                lblCCMessage.Text = "General error:Payment engine error on PPI Error: number:" & obj.resResponseCode
            Catch ex As Exception
                lblCCMessage.Text = "General error:Payment engine error on PPI"
            End Try
            lblCCMessage.Text = lblCCMessage.Text & "<br>" & "Unable to process order this time."
            lblCCMessage.Visible = True

            objclsPaymentGatewayTransactionLogs.ResponseNumber = "False"
            objclsPaymentGatewayTransactionLogs.ResponseMessage = "Unable to process order this time."
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = lblCCMessage.Text
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

            Return "Offline"
        End If

        Return "Offline"
    End Function



    ''new function need to addd
    Public Function OrdernmberCUstomerID(ByVal OrdNumber As String) As String
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderHeader where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = OrdNumber
            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count <> 0 Then

                Try
                    Return dt.Rows(0)("CustomerID")
                Catch ex As Exception
                    Return "OrdNo:" & OrdNumber
                End Try
            Else
                Return "OrdNo:" & OrdNumber
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return "OrdNo:" & OrdNumber
        End Try
        Return "OrdNo:" & OrdNumber
    End Function


    Dim drpExpirationDate As New TextBox
    Dim txtCard As New TextBox
    Dim txtCSV As New TextBox

    Dim lblCustomerID As New TextBox
    Dim lblOrderType As New TextBox
    Dim lblEmployeeID As New TextBox
    Dim lblOrderLocation As New TextBox
    Public lblCCMessage As New TextBox

    Public Sub BindCustomerDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdN As String)

        Dim OrderRs As SqlDataReader
        Dim PopOrderNo As New CustomOrder()
        OrderRs = PopOrderNo.PopulateOrder(CompanyID, DepartmentID, DivisionID, OrdN)
        While OrderRs.Read()
             Dim exdat As String = ""
            Dim exdate As Date
            Try
                exdat = OrderRs("CreditCardExpDate").ToString()
                exdate = exdat
                exdat = exdate.ToString("MM/yyyy")
            Catch ex As Exception
            End Try
            drpExpirationDate.Text = exdat
            txtCSV.Text = OrderRs("CreditCardCSVNumber").ToString()
            Dim Decrypt As New Encryption
            If OrderRs("CreditCardNumber").ToString() <> "" Then
                Try
                    txtCard.Text = Decrypt.TripleDESDecode(OrderRs("CreditCardNumber").ToString(), OrderRs("CustomerID").ToString().ToUpper())
                Catch ex As Exception
                    Try
                        txtCard.Text = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(OrderRs("CreditCardNumber").ToString(), OrderRs("CustomerID").ToString().ToUpper())
                    Catch ex1 As Exception

                    End Try
                End Try
            Else
                txtCard.Text = ""
            End If


             lblCustomerID.Text = OrderRs("CustomerID").ToString()
            PopulateCustomerInfo(lblCustomerID.Text)
            'lblOrderDate.Text = Convert.ToDateTime(OrderRs("OrderDate").ToString()).ToShortDateString
            lblOrderType.Text = OrderRs("OrderTypeID")
            'lblTransactionType.Text = OrderRs("TransactionTypeID") '
            ' lblPaymentMethod.Text = OrderRs("PaymentMethodID")
            'lblShipdate.Text = Convert.ToDateTime(OrderRs("OrderShipDate").ToString()).ToShortDateString
            lblEmployeeID.Text = OrderRs("EmployeeID").ToString()
            'lblDeliveryMethod.Text = OrderRs("ShipMethodID").ToString()
            'lblOrderLocation.Text = status(Posted, Picked, Shipped, invoiced, Delivered)
            lblOrderLocation.Text = OrderRs("LocationID").ToString()
              
        End While

    End Sub


    Public Sub PopulateCustomerInfo(ByVal CustID As String)
         
        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderType.PopulateCustomerDetails(CompanyID, DepartmentID, DivisionID, CustID)
        While rs.Read()
            Try
                'lblCustomerName.Text = rs("CustomerSalutation").ToString() & " " & rs("CustomerFirstName").ToString() & " " & rs("CustomerLastName").ToString()
                'txttxtCard.Text = rs("CustomerAddress1").ToString()
                'lblCustomerCity.Text = rs("CustomerCity").ToString()
                'lblCustomerState.Text = (rs("CustomerState").ToString())

                txtCustomerFirstName = rs("CustomerFirstName").ToString()
                txtCustomerLastName = rs("CustomerLastName").ToString()
                txtCustomerAddress1 = rs("CustomerAddress1").ToString()
                txtCustomerAddress2 = rs("CustomerAddress2").ToString()
                txtCustomerCity = rs("CustomerCity").ToString()
                txtCustomerState = rs("CustomerState").ToString()
                txtCustomerZip = rs("CustomerZip").ToString()
                drpCountry = rs("CustomerCountry").ToString()

            Catch ex As Exception

            End Try
        End While
        rs.Close()
    End Sub


End Class
