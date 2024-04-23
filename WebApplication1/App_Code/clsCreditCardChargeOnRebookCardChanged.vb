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

Public Class clsCreditCardChargeOnRebookCardChanged
    
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

    Public Function UpdateOrderPaymentDetails() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeaderPaymentMethodProcessingDetails set PaymentAmount=@f5, PaymentMethod=@f6 Where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@f4 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Money)).Value = Me.PaymentAmount
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = Me.PaymentMethod
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

    Public Function InsertOrderPaymentDetails() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  OrderHeaderPaymentMethodProcessingDetails (CompanyID,DivisionID,DepartmentID,OrderNumber,PaymentAmount,PaymentMethod) values(@f1,@f2,@f3,@f4,@f5,@f6)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Money)).Value = Me.PaymentAmount
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = Me.PaymentMethod
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

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

    Public processfrom As String = ""


    Public Function OrderTypeOfCreditCardPaymentDetails() As String
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select [OrderTypeID] from [OrderHeader]  where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
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

            If dt.Rows.Count > 0 Then
                Dim OrderTypeID As String = ""
                Try
                    OrderTypeID = dt.Rows(0)("OrderTypeID")
                Catch ex As Exception

                End Try
                Return OrderTypeID
            End If

            Return "Order"

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return "Order"
        End Try

    End Function


    Public Function OrderLocationidOfCreditCardPaymentDetails() As String
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select [LocationID] from [OrderHeader]  where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and OrderNumber=@f3"
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

            If dt.Rows.Count > 0 Then
                Dim OrderTypeID As String = ""
                Try
                    OrderTypeID = dt.Rows(0)("LocationID")
                Catch ex As Exception

                End Try
                Return OrderTypeID
            End If

            Return "Order"

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return "Order"
        End Try

    End Function



    Public PMG As String = ""
    Public LocationID As String = ""

    Public Function RebookCreditCardCHarge() As String

        PMG = OrderTypeOfCreditCardPaymentDetails()
        LocationID = OrderLocationidOfCreditCardPaymentDetails()

        BindCustomerDetails()
        Dim output As String
        output = ChrageCreditCard()

        '---code for trace
        Dim subject As String = "Checking Out Put coming for Ordernumber " & OrderNumber
        Dim mailcontent As String = "Out put comes is =" & output & " also Error message is =" & Me.lblerrormessag.Text
        Dim frommail As String = "imtiyaz@sunflowertechnologies.com"
        Dim tomail As String = "imtiyaz@sunflowertechnologies.com"
        EmailSending(subject, mailcontent, frommail, tomail)
        '---code for trace

        If output = "Offline" Then
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

        If AmttotalCharge <> 0 Then
            obj.UpdateOrderAdjustmentsApprovalNumber()
        End If


        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If
        'New Code For Two step chrage coding

        If Auth_Capture Then
            obj.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
        Else
            obj.UpdateCreditCardPaymentChargeType("SALE", "SALE")
        End If

        Return ApprovalNumber
    End Function


    Public Sub EmailSending(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String)

        'Exit Sub

        Dim mMailMessage As New MailMessage()

        ' Set the sender address of the mail message
        mMailMessage.From = New MailAddress(FromAddress)
        ' Set the recepient address of the mail message


        mMailMessage.To.Add(New MailAddress(ToAddress))
        mMailMessage.To.Add(New MailAddress("imtiyaz@sunflowertechnologies.com"))


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

        Dim ds As New Data.DataSet
        Dim PostOrder As New CustomOrder()
        Dim drApprovalNumber As SqlDataReader
        Dim ApprovalNumber As String = ""
        Dim PaymentMethodID As String = ""

        ' Check for PaymentMethodID
        ' If PaymentMethodID='Credit Card' and ApprovalNumber="" then 
        ' Pass the values to Paypal for Credit Card Processing

        drApprovalNumber = PostOrder.GetDetailsForCreditCardApproval(CompanyID, DivisionID, DepartmentID, OrderNumber)
        While drApprovalNumber.Read()
            ApprovalNumber = drApprovalNumber("CreditCardApprovalNumber").ToString()
            PaymentMethodID = drApprovalNumber("PaymentMethodID").ToString()
        End While


        'Added Code for Checking Credit Card Offline on 20/12/2007

        Dim CreditCardOffline As Boolean = False
        ds = PostOrder.CheckCreditCardOffline(CompanyID, DivisionID, DepartmentID, PMG)

        If ds.Tables(0).Rows.Count > 0 Then

            CreditCardOffline = ds.Tables(0).Rows(0)("CreditCardOffline").ToString()

        End If

        If CreditCardOffline = False Then

            'Dim obj1 As New clsPaymentGateWay
            'obj1.CompanyID = CompanyID
            'obj1.DivisionID = DivisionID
            'obj1.DepartmentID = DepartmentID
            'Dim dt As New Data.DataTable
            'dt = obj1.FillDetails
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
                        Return "Offline"
                    End If

                End If
                If dt.Rows(0)("PaymentGateway") = "PPI" Then
                    Dim rs As String
                    rs = PaymentDetailsPPI(OrderNumber, dt)
                    If rs = "ERROR" Then
                        Return "Offline"
                    End If
                End If

                If dt.Rows(0)("PaymentGateway") = "MerchantWARE" Then
                    Dim rs As String
                    rs = PaymentMecrhantWareDetails(OrderNumber, dt)
                    If rs = "ERROR" Then
                        Return "Offline"
                    End If
                End If


                If dt.Rows(0)("PaymentGateway") = "Mercury" Then
                    Dim rs As String
                    rs = PaymentMercuryDetails(OrderNumber, dt)
                    If rs = "ERROR" Then
                        Return "Offline"
                    End If
                End If




                If dt.Rows(0)("PaymentGateway") = "AuthorizeNet" Then
                    Dim rs As String
                    rs = PaymentAuthorizeNetDetails(OrderNumber, dt)
                    If rs = "ERROR" Then
                        Return "Offline"
                    End If
                End If

                'Catch ex As Exception
                '    PaymentDetails(OrdNumber)
                'End Try
            End If
        Else

            'lblerrormessag.Text = "Credit Card Offline So it Unable to Process."
            Return "Offline"

        End If

        Return ""
    End Function



    Public Function PaymentMercuryDetails(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "Mercury"
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
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""




        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If


        Dim rsfrom2fn As String

        rsfrom2fn = PaymentMercuryDetailsRefundBeforeAditionalcharge(OrderNumber, dtpayment)

 

        If rsfrom2fn = "ERROR" Then
            Return "ERROR"
        End If

        If AmttotalCharge = 0 Then
            Return ""
        End If

        Dim Amt As String = (AmttotalCharge)

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV



        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalCharge
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"

        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()



        Return ""

    End Function

    Public Function PaymentMercuryDetailsRefundBeforeAditionalcharge(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "Mercury"
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
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""




        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If




        If AmttotalRefund = 0 Then
            Return ""
        End If

        Dim Amt As String = (AmttotalRefund)

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        ''new code added
        Try
            ccExp = Convert.ToDateTime(dtpayment.Rows(0)("CreditCardExpDate")).Date
            CreditCardExpMonth = ccExp.Month()
            CreditCardExpYear = ccExp.Year()
            If CreditCardExpMonth.Trim().Length < 2 Then
                CreditCardExpMonth = "0" + CreditCardExpMonth
            End If
            CreditCardNumber = dtpayment.Rows(0)("CreditCardNumber")

            Try
                CreditCardNumber = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(CreditCardNumber, OrdNumber)
            Catch ex As Exception

            End Try

            CreditCardCSVNumber = dtpayment.Rows(0)("CreditCardCSVNumber")
        Catch ex As Exception

        End Try
        ''new code added


        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetailsMercury(PMG, LocationID)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If

        Dim processdate As Date
        Dim chkDay As Boolean = False
        Try
            processdate = dtpayment.Rows(0)("ProcessDate")
        Catch ex As Exception
            processdate = "11/1/1900"
        End Try

        If processdate.Year = Date.Now.Year Then
            If processdate.Month = Date.Now.Month Then
                If processdate.Day = Date.Now.Day Then
                    chkDay = True
                End If
            End If
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalRefund

        If chkDay Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "VOID"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "Return"
        End If


        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable

        dtMecrhant = obj1.FillDetailsMercury(PMG, LocationID)
        Dim APILoginID As String
        Dim TransactionKey As String


        If dt.Rows.Count <> 0 Then
            Try
                APILoginID = dtMecrhant.Rows(0)("MercuryMerchantID")
            Catch ex As Exception
            End Try

            Try
                TransactionKey = dtMecrhant.Rows(0)("MercuryWebPassword")
            Catch ex As Exception
            End Try



        End If
        'Dim CC As String = "4003000123456781"
        Dim expr As String = CreditCardExpMonth & CreditCardExpYear
        'Dim cvv As String = "123"
        Dim CSTaddress As String = "" 'txtCustomerAddress1.Text
        Dim zipcode As String = "" ' txtCustomerZip.Text
        Dim MercuryMerchantID As String = ""
        Dim MercuryWebPassword As String = ""
        Try
            MercuryMerchantID = APILoginID
        Catch ex As Exception
        End Try
        Try
            MercuryWebPassword = TransactionKey
        Catch ex As Exception
        End Try


        Dim MercuryData As New clsMercury
        Dim TransactionDataXML As String

        If chkDay Then
            'AuthorizeNetRequest = New AuthorizeNet.VoidRequest(dtpayment.Rows(0)("PPIReferenceID"))
            If "VoidSaleByRecordNo" = "VoidSaleByRecordNo" Then
                MercuryData.MerchantID = MercuryMerchantID
                MercuryData.InvoiceNo = dtpayment.Rows(0)("MercuryData_InvoiceNo")
                MercuryData.RefNo = dtpayment.Rows(0)("MercuryData_RefNo")
                MercuryData.Memo = "QuickFlora1.5"
                MercuryData.Frequency = "OneTime"
                MercuryData.RecordNo = dtpayment.Rows(0)("MercuryData_RecordNo")

                MercuryData.Purchase = Format(AmttotalRefund, "0.00")
                MercuryData.TerminalName = CompanyID
                MercuryData.OperatorID = "Admin"

                MercuryData.ACQRefData = dtpayment.Rows(0)("MercuryData_ACQRefData")
                MercuryData.AuthCode = dtpayment.Rows(0)("MercuryData_AuthCode")
                MercuryData.ProcessData = dtpayment.Rows(0)("MercuryData_ProcessData")
                TransactionDataXML = MercuryData.CreateMercuryXMLVoidSaleByRecordNo("Credit", "VoidSaleByRecordNo")
            End If
        Else
            'AuthorizeNetRequest = New AuthorizeNet.CreditRequest(dtpayment.Rows(0)("PPIReferenceID"), AmttotalRefund, txtCard)
            If "ReturnByRecordNo" = "ReturnByRecordNo" Then
                MercuryData.MerchantID = MercuryMerchantID
                MercuryData.InvoiceNo = dtpayment.Rows(0)("MercuryData_InvoiceNo")
                MercuryData.RefNo = dtpayment.Rows(0)("MercuryData_RefNo")
                MercuryData.Memo = "QuickFlora1.5"
                MercuryData.Frequency = "OneTime"
                MercuryData.RecordNo = dtpayment.Rows(0)("MercuryData_RecordNo")
                MercuryData.Purchase = Format(AmttotalRefund, "0.00")
                TransactionDataXML = MercuryData.CreateMercuryXMLReturnByRecordNo("Credit", "ReturnByRecordNo")
            End If

        End If



        'If "CreditSale" = "CreditSale" Then
        '    MercuryData.MerchantID = MercuryMerchantID
        '    MercuryData.InvoiceNo = objclsPaymentGatewayTransactionLogs.InLineNumber
        '    MercuryData.RefNo = objclsPaymentGatewayTransactionLogs.InLineNumber
        '    MercuryData.Memo = "QuickFlora1.5"
        '    MercuryData.Frequency = "OneTime"
        '    MercuryData.RecordNo = "RecordNumberRequested"

        '    MercuryData.AcctNo = CreditCardNumber
        '    MercuryData.ExpDate = expr
        '    MercuryData.Purchase = Amt

        '    MercuryData.TerminalName = CompanyID
        '    MercuryData.OperatorID = "Admin"

        '    MercuryData.cvv = CreditCardCSVNumber
        '    'MercuryData.Address = address
        '    MercuryData.zipcode = zipcode

        '    TransactionDataXML = MercuryData.CreateMercuryXMLWithRecordNo("Credit", "Sale")
        'End If

        Dim TransactionResponseXML As String = ""

        ''ToDO: Switch over Web 1 and web 2 based upon available connection:

        Dim Mercury_ws1 As New ws.ws
        Dim Mercury As New ws1.ws


        If MercuryMerchantID = "395347306=TOKEN" Then

            TransactionResponseXML = Mercury_ws1.CreditTransaction(TransactionDataXML, MercuryWebPassword)
        Else
            TransactionResponseXML = Mercury.CreditTransaction(TransactionDataXML, MercuryWebPassword)
        End If

        MercuryData.ExtractResponse(TransactionResponseXML)


        Dim response_obj As New Object

        If MercuryData.CMDStatus = "Approved" Then

            lblerrormessag.Visible = True


            Dim AuthCode As String = MercuryData.AuthCode
            Dim ApprovalNumber As String = MercuryData.Response_RefNo


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "Mercury"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = MercuryData.AuthCode
                objpayment.PPIReferenceID = MercuryData.Response_RefNo
                objpayment.PaymentAmount = AmttotalRefund
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = MercuryData.CMDStatus




            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If


            objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
            objclsPaymentGatewayTransactionLogs.ResponseMessage = MercuryData.CMDStatus '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "<!-- Response of CreditSale--><br>" & TransactionResponseXML 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = "" ' MercuryData.Response_RefNo
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = "" 'MercuryData.AuthCode
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


            '---code for trace
            Dim subject As String = "Checking TransactionDataXML for Ordernumber " & OrderNumber & " CompanyID = " & CompanyID & " MercuryData.CMDStatus= " & MercuryData.CMDStatus
            Dim mailcontent As String = "TransactionResponseXML =" & TransactionResponseXML
            Dim frommail As String = "imtiyaz@sunflowertechnologies.com"
            Dim tomail As String = "imtiyaz@sunflowertechnologies.com"
            EmailSending(subject, mailcontent, frommail, tomail)
            '---code for trace

            ' Code Added For Updating Credit Card Validation Code returned 
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "Mercury"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = MercuryData.AuthCode
                objpayment.PPIReferenceID = MercuryData.Response_RefNo
                objpayment.PaymentAmount = AmttotalRefund
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate).Date
                objpayment.NewCreditCardCSVNumber = txtCSV
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


                objpayment.MercuryData_InvoiceNo = MercuryData.InvoiceNo
                objpayment.MercuryData_RefNo = MercuryData.Response_RefNo
                objpayment.MercuryData_AuthCode = MercuryData.AuthCode
                objpayment.MercuryData_RecordNo = MercuryData.RecordNo
                objpayment.MercuryData_ACQRefData = MercuryData.ACQRefData
                objpayment.MercuryData_ProcessData = MercuryData.ProcessData
                objpayment.UpdateCreditCardPaymentMercury()

            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        Else
            lblerrormessag.Text = MercuryData.CMDStatus
            lblerrormessag.Visible = True

            objclsPaymentGatewayTransactionLogs.ResponseNumber = MercuryData.TextResponse
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text   '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ("<!-- Response of Refund-><br>" & TransactionResponseXML) 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "Mercury"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try

            lblerrormessag.ForeColor = Drawing.Color.Red

            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"

        End If

        Return ""

    End Function


    Public Function PaymentAuthorizeNetDetails(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "AuthorizeNet"
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
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""




        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If


        Dim rsfrom2fn As String

        rsfrom2fn = PaymentAuthorizeNetDetailsRefundBeforeAditionalcharge(OrderNumber, dtpayment)
 

        If rsfrom2fn = "ERROR" Then
            Return "ERROR"
        End If

        If AmttotalCharge = 0 Then
            Return ""
        End If

        Dim Amt As String = (AmttotalCharge)

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV



        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalCharge
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"

        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable

        dtMecrhant = obj1.FillDetails(PMG)
        Dim txtSiteID As String
        Dim txtKey As String
        Dim txtName As String

        If dt.Rows.Count <> 0 Then
            Try
                txtSiteID = dtMecrhant.Rows(0)("Merchant_SiteID")
            Catch ex As Exception
            End Try

            Try
                txtKey = dtMecrhant.Rows(0)("Merchant_Key")
            Catch ex As Exception
            End Try

            Try
                txtName = dtMecrhant.Rows(0)("Merchant_Name")
            Catch ex As Exception
            End Try
        End If


        Dim objTXRetail31 As New MerchantTest.TXRetail31
        Dim StatusInfo2 As New MerchantTest.RetailTransactionStatusInfo2

        StatusInfo2.ApprovalStatus = "Send for process"

        Try
            StatusInfo2 = objTXRetail31.IssueKeyedSale(txtName, txtSiteID, txtKey, OrdNumber, Amt, CreditCardNumber, CreditCardExpMonth & "" & CreditCardExpYear, "Imtiyaz AHmad", "", "", CreditCardCSVNumber, "True", "", objclsPaymentGatewayTransactionLogs.InLineNumber)
        Catch ex As Exception

        End Try



        If StatusInfo2.ApprovalStatus = "APPROVED" Then

            lblerrormessag.Visible = True


            Dim AuthCode As String = StatusInfo2.ReferenceID
            Dim ApprovalNumber As String = StatusInfo2.AuthCode


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = StatusInfo2.ApprovalStatus




            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If

            objclsPaymentGatewayTransactionLogs.ResponseNumber = StatusInfo2.ApprovalStatus
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = StatusInfo2.ReferenceID
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = StatusInfo2.AuthCode
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


            ' Code Added For Updating Credit Card Validation Code returned 
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate).Date
                objpayment.NewCreditCardCSVNumber = txtCSV
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        Else
            objclsPaymentGatewayTransactionLogs.ResponseNumber = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& "  ResponseCodeText" & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()


            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try

            lblerrormessag.ForeColor = Drawing.Color.Red

            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"

        End If

        Return ""

    End Function




    Public Function PaymentAuthorizeNetDetailsRefundBeforeAditionalcharge(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "AuthorizeNet"
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
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""




        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If




        If AmttotalRefund = 0 Then
            Return ""
        End If

        Dim Amt As String = (AmttotalRefund)

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV

        ''new code added
        Try
            ccExp = Convert.ToDateTime(dtpayment.Rows(0)("CreditCardExpDate")).Date
            CreditCardExpMonth = ccExp.Month()
            CreditCardExpYear = ccExp.Year()
            If CreditCardExpMonth.Trim().Length < 2 Then
                CreditCardExpMonth = "0" + CreditCardExpMonth
            End If
            CreditCardDateString = CreditCardExpMonth + CreditCardExpYear.Substring(2)


            CreditCardNumber = dtpayment.Rows(0)("CreditCardNumber")
            Try
                CreditCardNumber = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(CreditCardNumber, OrdNumber)
            Catch ex As Exception

            End Try


            CreditCardCSVNumber = dtpayment.Rows(0)("CreditCardCSVNumber")
        Catch ex As Exception

        End Try
        ''new code added


        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If

        Dim processdate As Date
        Dim chkDay As Boolean = False
        Try
            processdate = dtpayment.Rows(0)("ProcessDate")
        Catch ex As Exception
            processdate = "11/1/1900"
        End Try

        If processdate.Year = Date.Now.Year Then
            If processdate.Month = Date.Now.Month Then
                If processdate.Day = Date.Now.Day Then
                    chkDay = True
                End If
            End If
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalRefund

        If chkDay Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "VOID"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "Credit"
        End If


        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable

        dtMecrhant = obj1.FillDetails(PMG)
        Dim APILoginID As String
        Dim TransactionKey As String


        If dt.Rows.Count <> 0 Then
            Try
                APILoginID = dtMecrhant.Rows(0)("APILoginID")
            Catch ex As Exception
            End Try

            Try
                TransactionKey = dtMecrhant.Rows(0)("TransactionKey")
            Catch ex As Exception
            End Try



        End If

        Dim AuthorizeNetRequest As New Object

        If chkDay Then
            AuthorizeNetRequest = New AuthorizeNet.VoidRequest(dtpayment.Rows(0)("PPIReferenceID"))
        Else
            AuthorizeNetRequest = New AuthorizeNet.CreditRequest(dtpayment.Rows(0)("PPIReferenceID"), AmttotalRefund, CreditCardNumber)
        End If

        'StatusInfo2 = objTXRetail31.IssueVoid(txtName, txtSiteID, txtKey, dtpayment.Rows(0)("PPIOrderID"), "", "")
        'StatusInfo2 = objTXRetail31.IssueRefundByReference(txtName, txtSiteID, txtKey, OrdNumber, dtpayment.Rows(0)("PPIOrderID"), AmttotalRefund, "Imtiyaz AHmad", "", "")

        Dim rwresponse As String()

        Dim gate As New AuthorizeNet.Gateway(APILoginID, TransactionKey, True)

        gate.TestMode = False

        Dim response_obj As New AuthorizeNet.GatewayResponse(rwresponse)
        response_obj = gate.Send(AuthorizeNetRequest)

        If response_obj.Approved = True Then

            lblerrormessag.Visible = True


            Dim AuthCode As String = response_obj.AuthorizationCode
            Dim ApprovalNumber As String = response_obj.TransactionID


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "AuthorizeNet"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = response_obj.AuthorizationCode
                objpayment.PPIReferenceID = response_obj.TransactionID
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = response_obj.Message




            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If

            objclsPaymentGatewayTransactionLogs.ResponseNumber = response_obj.Code
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = response_obj.AuthorizationCode
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = response_obj.TransactionID
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


            ' Code Added For Updating Credit Card Validation Code returned 
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "AuthorizeNet"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = response_obj.AuthorizationCode
                objpayment.PPIReferenceID = response_obj.TransactionID
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate).Date
                objpayment.NewCreditCardCSVNumber = txtCSV
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        Else
            lblerrormessag.Text = response_obj.Message

            objclsPaymentGatewayTransactionLogs.ResponseNumber = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& "  ResponseCodeText" & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()


            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "AuthorizeNet"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try

            lblerrormessag.ForeColor = Drawing.Color.Red

            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"

        End If

        Return ""

    End Function



    Public Function PaymentMecrhantWareDetails(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "MerchantWARE"
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
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""

        


        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If


        Dim rsfrom2fn As String

        rsfrom2fn = PaymentMecrhantWareDetailsRefundBeforeAditionalcharge(OrderNumber, dtpayment)
 
        If rsfrom2fn = "ERROR" Then
            Return "ERROR"
        End If

        If AmttotalCharge = 0 Then
            Return ""
        End If

        Dim Amt As String = (AmttotalCharge)

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV



        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalCharge
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"

        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable

        dtMecrhant = obj1.FillDetails(PMG)
        Dim txtSiteID As String
        Dim txtKey As String
        Dim txtName As String

        If dt.Rows.Count <> 0 Then
            Try
                txtSiteID = dtMecrhant.Rows(0)("Merchant_SiteID")
            Catch ex As Exception
            End Try

            Try
                txtKey = dtMecrhant.Rows(0)("Merchant_Key")
            Catch ex As Exception
            End Try

            Try
                txtName = dtMecrhant.Rows(0)("Merchant_Name")
            Catch ex As Exception
            End Try
        End If


        Dim objTXRetail31 As New MerchantTest.TXRetail31
        Dim StatusInfo2 As New MerchantTest.RetailTransactionStatusInfo2

        StatusInfo2.ApprovalStatus = "Send for process"

        Try
            StatusInfo2 = objTXRetail31.IssueKeyedSale(txtName, txtSiteID, txtKey, OrdNumber, Amt, CreditCardNumber, CreditCardExpMonth & "" & CreditCardExpYear, "Imtiyaz AHmad", "", "", CreditCardCSVNumber, "True", "", objclsPaymentGatewayTransactionLogs.InLineNumber)
        Catch ex As Exception

        End Try



        If StatusInfo2.ApprovalStatus = "APPROVED" Then

            lblerrormessag.Visible = True


            Dim AuthCode As String = StatusInfo2.ReferenceID
            Dim ApprovalNumber As String = StatusInfo2.AuthCode


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = StatusInfo2.ApprovalStatus




            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If

            objclsPaymentGatewayTransactionLogs.ResponseNumber = StatusInfo2.ApprovalStatus
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = StatusInfo2.ReferenceID
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = StatusInfo2.AuthCode
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

            
            ' Code Added For Updating Credit Card Validation Code returned 
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate).Date
                objpayment.NewCreditCardCSVNumber = txtCSV
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        Else
            objclsPaymentGatewayTransactionLogs.ResponseNumber = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& "  ResponseCodeText" & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()


            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try

            lblerrormessag.ForeColor = Drawing.Color.Red

            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"

        End If

        Return ""

    End Function




    Public Function PaymentMecrhantWareDetailsRefundBeforeAditionalcharge(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String
        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "MerchantWARE"
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
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""




        ccExp = Convert.ToDateTime(drpExpirationDate).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If




        If AmttotalRefund = 0 Then
            Return ""
        End If

        Dim Amt As String = (AmttotalRefund)

        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV



        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If

        Dim processdate As Date
        Dim chkDay As Boolean = False
        Try
            processdate = dtpayment.Rows(0)("ProcessDate")
        Catch ex As Exception
            processdate = "11/1/1900"
        End Try

        If processdate.Year = Date.Now.Year Then
            If processdate.Month = Date.Now.Month Then
                If processdate.Day = Date.Now.Day Then
                    chkDay = True
                End If
            End If
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalRefund
       
        If chkDay Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "VOID"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "Credit"
        End If


        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable

        dtMecrhant = obj1.FillDetails(PMG)
        Dim txtSiteID As String
        Dim txtKey As String
        Dim txtName As String
        Dim Merchant_Mode As String = ""


        If dt.Rows.Count <> 0 Then
            Try
                txtSiteID = dtMecrhant.Rows(0)("Merchant_SiteID")
            Catch ex As Exception
            End Try

            Try
                txtKey = dtMecrhant.Rows(0)("Merchant_Key")
            Catch ex As Exception
            End Try

            Try
                txtName = dtMecrhant.Rows(0)("Merchant_Name")
            Catch ex As Exception
            End Try

            Try
                Merchant_Mode = dtMecrhant.Rows(0)("Merchant_Mode")
            Catch ex As Exception
            End Try

        End If


       

        'Dim objTXRetail31 As New MerchantTest.TXRetail31
        'Dim StatusInfo2 As New MerchantTest.RetailTransactionStatusInfo2

        Dim objTXRetail31 As Object
        Dim StatusInfo2 As Object


        If Merchant_Mode = "TEST" Then
            objTXRetail31 = New MerchantTest.TXRetail31
            StatusInfo2 = New MerchantTest.RetailTransactionStatusInfo2
        Else
            objTXRetail31 = New MerchantWARE31Services.TXRetail31
            StatusInfo2 = New MerchantWARE31Services.RetailTransactionStatusInfo2
        End If

        StatusInfo2.ApprovalStatus = "Send for process"

        Try
            'StatusInfo2 = objTXRetail31.IssueKeyedSale(txtName, txtSiteID, txtKey, OrdNumber, Amt, CreditCardNumber, CreditCardExpMonth & "" & CreditCardExpYear, "Imtiyaz Ahmad", "", "", CreditCardCSVNumber, "True", "", objclsPaymentGatewayTransactionLogs.InLineNumber)

            If chkDay Then
                StatusInfo2 = objTXRetail31.IssueVoid(txtName, txtSiteID, txtKey, dtpayment.Rows(0)("PPIOrderID"), "", "")
            Else
                StatusInfo2 = objTXRetail31.IssueRefundByReference(txtName, txtSiteID, txtKey, OrdNumber, dtpayment.Rows(0)("PPIOrderID"), AmttotalRefund, "Imtiyaz AHmad", "", "")
            End If

        Catch ex As Exception

        End Try



        If StatusInfo2.ApprovalStatus = "APPROVED" Then

            lblerrormessag.Visible = True


            Dim AuthCode As String = StatusInfo2.ReferenceID
            Dim ApprovalNumber As String = StatusInfo2.AuthCode


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

            lblerrormessag.Text = StatusInfo2.ApprovalStatus




            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If

            objclsPaymentGatewayTransactionLogs.ResponseNumber = StatusInfo2.ApprovalStatus
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = StatusInfo2.ReferenceID
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = StatusInfo2.AuthCode
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


            ' Code Added For Updating Credit Card Validation Code returned 
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate).Date
                objpayment.NewCreditCardCSVNumber = txtCSV
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        Else
            objclsPaymentGatewayTransactionLogs.ResponseNumber = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text '& "  ResponseCodeText" & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()


            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = AmttotalCharge
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try

            lblerrormessag.ForeColor = Drawing.Color.Red

            lblerrormessag.Text = lblerrormessag.Text & "<br>" & "Unable to process order this time."
            lblerrormessag.Visible = True
            Return "ERROR"

        End If

        Return ""

    End Function



    Public Function PaymentDetailsPPI(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String

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

        Dim Auth_Capture As Boolean = False

        'CompanyID, DepartmentID, DivisionID
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        'dt = obj1.FillDetails(PMG)
        If CompanyID = "Greene and Greene" Or CompanyID = "Ovando Floral and Event Design-10065" Then

            dt = obj1.FillDetailsPaymentGatwayByOrder(OrdNumber)

            If (dt.Rows.Count = 0) Then
                dt = obj1.FillDetailsPPI(PMG, LocationID)
            End If

        Else
            dt = obj1.FillDetails(PMG)
        End If


        If dt.Rows.Count <> 0 Then

            'new code added for capture
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
            'new code added for capture

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

        Dim rsfrom2fn As String

        rsfrom2fn = PaymentDetailsPPIRefundBeforeAditionalcharge(OrderNumber, dtpayment)


        If rsfrom2fn = "ERROR" Then
            Return "ERROR"
        End If


        If AmttotalCharge = 0 Then
            Return ""
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


        'obj.chargeType = "SALE"
        If Auth_Capture Then
            obj.chargeType = "AUTH"
        Else
            obj.chargeType = "SALE"
        End If

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


        objclsPaymentGatewayTransactionLogs.ProcessAmount = obj.chargeTotal
        objclsPaymentGatewayTransactionLogs.ProcessType = obj.chargeType
        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
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

                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & " and   ResponseCodeText = " & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.PPIOrderID = obj.resOrderID
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

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

                End If
                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & "  ResponseCodeText = " & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()


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

            objclsPaymentGatewayTransactionLogs.ResponseNumber = "False"
            objclsPaymentGatewayTransactionLogs.ResponseMessage = "Unable to process order this time."
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

            Return "ERROR"
        End If

        Return ""
    End Function

    Public Function PaymentDetailsPPIRefundBeforeAditionalcharge(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String

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
        dt = obj1.FillDetails(PMG)

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
        Dim Amt As String = AmttotalRefund


        CreditCardNumber = txtCard
        CreditCardCSVNumber = txtCSV


        ''new code added
        Try
            ccExp = Convert.ToDateTime(dtpayment.Rows(0)("CreditCardExpDate")).Date
            CreditCardExpMonth = ccExp.Month()
            CreditCardExpYear = ccExp.Year()
            If CreditCardExpMonth.Trim().Length < 2 Then
                CreditCardExpMonth = "0" + CreditCardExpMonth
            End If

            CreditCardNumber = dtpayment.Rows(0)("CreditCardNumber")
            Try
                CreditCardNumber = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(CreditCardNumber, OrdNumber)
            Catch ex As Exception

            End Try

            CreditCardCSVNumber = dtpayment.Rows(0)("CreditCardCSVNumber")
        Catch ex As Exception

        End Try
        ''new code added



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


        Dim processdate As Date
        Dim chkDay As Boolean = False
        Try
            processdate = dtpayment.Rows(0)("ProcessDate")
        Catch ex As Exception
            processdate = "11/1/1900"
        End Try

        If processdate.Year = Date.Now.Year Then
            If processdate.Month = Date.Now.Month Then
                If processdate.Day = Date.Now.Day Then
                    chkDay = True
                End If
            End If
        End If


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
        If ChargeTypes = "AUTH" Then
            If ChargeStatus = "AUTH" Then
                obj.chargeType = "VOID"
            Else
                If chkDay Then
                    obj.chargeType = "VOID"
                Else
                    obj.chargeType = "CREDIT"
                End If
            End If
        ElseIf ChargeTypes = "SALE" Then
            If chkDay Then
                obj.chargeType = "VOID"
            Else
                obj.chargeType = "CREDIT"
            End If
        ElseIf ChargeTypes = "InvCancel" Then
            Return ""
        End If
        'new Code For Caputre The Amount



        obj.industry = "RETAIL"
        obj.ACCOUNT_TOKEN = PPI_ACOUNT_TOKEN

        obj.orderID = dtpayment.Rows(0)("PPIOrderID")
        obj.referenceID = dtpayment.Rows(0)("PPIReferenceID")



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


        objclsPaymentGatewayTransactionLogs.ProcessAmount = obj.chargeTotal
        objclsPaymentGatewayTransactionLogs.ProcessType = obj.chargeType
        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
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
            Try
                result = obj.resResponseCode
                If obj.chargeType = "VOID" Then
                    If Res Then
                        If result = 1 Then
                            Res = obj.Process_Transaction
                            result = obj.resResponseCode
                        End If
                    End If
                End If
            Catch ex As Exception
                result = 0
            End Try

        End If

        Dim misc As String = ""

        Dim subject As String = ""
        Dim mailcontent As String = ""
        Dim frommail As String = "imtiyaz@sunflowertechnologies.com"
        Dim tomail As String = "imtiyaz@sunflowertechnologies.com"

        subject = "Adjustment Details Processing details Used <br>"
        mailcontent = mailcontent & "obj.chargeTotal  =" & obj.chargeTotal & " <br>"
        mailcontent = mailcontent & "obj.chargeType  =" & obj.chargeType & " <br>"
        mailcontent = mailcontent & "obj.orderID  =" & obj.orderID & " "
        mailcontent = mailcontent & "obj.referenceID  =" & obj.referenceID & " <br>"
        mailcontent = mailcontent & "obj.transactionConditionCode  =" & obj.transactionConditionCode & " <br>"
        mailcontent = mailcontent & "result  =" & result & " <br>"

        'EmailSending(subject, mailcontent, frommail, tomail)


        If Res Then

            lblerrormessag.Visible = True
            Address = REMOTE_ADDR ' Request.ServerVariables("REMOTE_ADDR")

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

                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & " and   ResponseCodeText = " & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.PPIOrderID = obj.resOrderID
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

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
                    'Return "ERROR"
                End If

                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & "  ResponseCodeText = " & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()
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

            objclsPaymentGatewayTransactionLogs.ResponseNumber = "False"
            objclsPaymentGatewayTransactionLogs.ResponseMessage = "Unable to process order this time."
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

            Return "ERROR"
        End If

        Return ""
    End Function



    Public Function OrdernmberCUstomerID(ByVal OrdNumber As String) As String
        Dim connec As New SqlConnection(constr)
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
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
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

    Public Function PaymentDetails(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String

        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "PayPal"
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

        rs = objUser.PopulatePaymentAccounts(CompanyID, DivisionID, DepartmentID, "POS")

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
        Dim Connection As New PayflowConnectionData(PaymentURL, 443, 40, "", 0, "", "")

        ''
        'canceling original order payment
        Dim rsreturn As String
        rsreturn = PaymentDetailsRefundBeforeAditionalcharge(OrderNumber, dtpayment)

        If rsreturn = "ERROR" Then
            Return "ERROR"
        End If

        ''

        If AmttotalCharge = 0 Then
            Return ""
        End If


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
        'Dim Transnew As New VoidTransaction(
        'Dim Resp As Response = Trans.SubmitTransaction()

        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If
        'New Code For Two step chrage coding
        Dim Resp As Response

        If Auth_Capture Then
            Dim Trans As New AuthorizationTransaction(User, Connection, Inv, Card, strRequestID)
            Resp = Trans.SubmitTransaction()
        Else
            Dim Trans As New SaleTransaction(User, Connection, Inv, Card, strRequestID)
            Resp = Trans.SubmitTransaction()
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalCharge
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


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


                objclsPaymentGatewayTransactionLogs.ResponseNumber = Result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & " and   ResponseCodeText = " & TrxnResponse.RespMsg
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
                objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


            Else

                objclsPaymentGatewayTransactionLogs.ResponseNumber = Result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & "  ResponseCodeText = " & TrxnResponse.RespMsg
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

                Return "ERROR"
            End If
        Else
            objclsPaymentGatewayTransactionLogs.ResponseNumber = "No Response"
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()
            Return "ERROR"
        End If

        Return ""
    End Function

    Public Function PaymentDetailsRefundBeforeAditionalcharge(ByVal OrdNumber As String, ByVal dtpayment As DataTable) As String

        Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs
        objclsPaymentGatewayTransactionLogs.CompanyID = Me.CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = Me.DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = Me.DepartmentID
        objclsPaymentGatewayTransactionLogs.CustomerID = OrdernmberCUstomerID(OrdNumber)
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "PayPal"
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

        rs = objUser.PopulatePaymentAccounts(CompanyID, DivisionID, DepartmentID, "POS")

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
        Dim Connection As New PayflowConnectionData(PaymentURL, 443, 40, "", 0, "", "")

        Dim Amt As New Currency(AmttotalRefund)
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


        ''new code added
        Try
            ccExp = Convert.ToDateTime(dtpayment.Rows(0)("CreditCardExpDate")).Date
            CreditCardExpMonth = ccExp.Month()
            CreditCardExpYear = ccExp.Year()
            If CreditCardExpMonth.Trim().Length < 2 Then
                CreditCardExpMonth = "0" + CreditCardExpMonth
            End If
            CreditCardDateString = CreditCardExpMonth + CreditCardExpYear.Substring(2)

            CreditCardNumber = dtpayment.Rows(0)("CreditCardNumber")
            Try
                CreditCardNumber = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(CreditCardNumber, OrdNumber)
            Catch ex As Exception

            End Try

            CreditCardCSVNumber = dtpayment.Rows(0)("CreditCardCSVNumber")
        Catch ex As Exception

        End Try
        ''new code added



        Dim CC As New CreditCard(CreditCardNumber, CreditCardDateString)

        CC.Cvv2 = CreditCardCSVNumber

        'CC.Name = "Joe M Smith"
        Dim Card As New CardTender(CC)
        'Dim Trans As New SaleTransaction(User, Connection, Inv, Card, strRequestID)
        Dim Resp As Response '= Trans.SubmitTransaction()

        'new Code For Caputre The Amount
        Dim processdate As Date
        Dim chkDay As Boolean = False
        Try
            processdate = dtpayment.Rows(0)("ProcessDate")
        Catch ex As Exception
            processdate = "11/1/1900"
        End Try

        If processdate.Year = Date.Now.Year Then
            If processdate.Month = Date.Now.Month Then
                If processdate.Day = Date.Now.Day Then
                    chkDay = True
                End If
            End If
        End If

        Dim ChargeStatus As String
        Dim ChargeTypes As String
        Try
            ChargeTypes = dtpayment.Rows(0)("ChargeTypes")
            ChargeStatus = dtpayment.Rows(0)("ChargeStatus")
        Catch ex As Exception
            ChargeTypes = "SALE"
        End Try
        If ChargeTypes = "AUTH" Then
            If ChargeStatus = "AUTH" Then
                Dim Trans As New VoidTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
                Resp = Trans.SubmitTransaction()
                objclsPaymentGatewayTransactionLogs.ProcessType = "VOID"
            Else
                If chkDay Then
                    Dim Trans As New VoidTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
                    Resp = Trans.SubmitTransaction()
                    objclsPaymentGatewayTransactionLogs.ProcessType = "VOID"
                Else
                    Dim Trans As New CreditTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
                    Resp = Trans.SubmitTransaction()
                    objclsPaymentGatewayTransactionLogs.ProcessType = "CREDIT"
                End If
            End If
        ElseIf ChargeTypes = "SALE" Then
            If chkDay Then
                Dim Trans As New VoidTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
                Resp = Trans.SubmitTransaction()
                objclsPaymentGatewayTransactionLogs.ProcessType = "VOID"
            Else
                Dim Trans As New CreditTransaction(dtpayment.Rows(0)("PayPalPNREF").ToString, User, Connection, Inv, strRequestID)
                Resp = Trans.SubmitTransaction()
                objclsPaymentGatewayTransactionLogs.ProcessType = "CREDIT"
            End If
        ElseIf ChargeTypes = "InvCancel" Then
            Return ""
        End If
        'new Code For Caputre The Amount

        objclsPaymentGatewayTransactionLogs.ProcessAmount = AmttotalRefund

        objclsPaymentGatewayTransactionLogs.ProcessDetails = Me.processfrom
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()




        If Not (Resp Is Nothing) Then

            lblerrormessag.Visible = True

            Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
            Dim Result As Integer = TrxnResponse.Result
            Dim RespMsg As String = TrxnResponse.RespMsg
            Dim AuthCode As String = TrxnResponse.AuthCode
            ApprovalNumber = TrxnResponse.Pnref
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

                objclsPaymentGatewayTransactionLogs.ResponseNumber = Result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & " and   ResponseCodeText = " & TrxnResponse.RespMsg
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
                objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

            Else
                objclsPaymentGatewayTransactionLogs.ResponseNumber = Result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text & "  ResponseCodeText = " & TrxnResponse.RespMsg
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

                Return "ERROR"

            End If
        Else

            objclsPaymentGatewayTransactionLogs.ResponseNumber = "No Response"
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblerrormessag.Text
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

            Return "ERROR"
        End If

        Return ""
    End Function




End Class

