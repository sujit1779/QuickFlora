Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsRefundProcess

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    Public CompanyId As String
    Public DivisionId As String
    Public DepartmentId As String
    Public OrderNumber As String
    Public ProcessID As String
    Public RefundAmount As Decimal
    Public Delivery As Decimal
    Public Service As Decimal
    Public Relay As Decimal
    Public HeaderTaxAmount As Decimal
    Public TaxGSTValue As Decimal
    Public TaxPSTValue As Decimal

    Public LocationID As String = ""
    Public CurrencyID As String = "USD"
    Public CurrencyExchangeRate As String = "1"
    Public RefundProcessDate As Date
    Public GLSalesAccount As String = ""
    Public CustomerID As String

    Private CreditMemosID As String = ""
    Public PaymentMethodID As String = ""
    Public RefundPaymentMethodID As String = ""

    Dim bStatus As Boolean = False
    Dim strResponse As String = "Success"

    Public EmployeeID As String
    Public ShiftID As String
    Public TerminalID As String
    Public DeliveryMethodID As String = ""




    Public Function ProcessRefundTransaction(ByRef strReturnResponse As String) As Boolean

        If PaymentMethodID.ToLower() <> RefundPaymentMethodID.ToLower() Then
            bStatus = ProcessRefundinCash()
        Else
            bStatus = ProcessRefund()
        End If

        UpdateOrderRefundProcessStatus(bStatus, strResponse)
        strReturnResponse = strResponse
        Return bStatus
    End Function

    Private Function ProcessRefund() As Boolean

        If PaymentMethodID.ToLower() = "credit card" Then

            'Post Credit Memo
            Dim CreditMemoNumber As String = ""
            CreditMemoNumber = PostCreditMemoForRefundedAmount()

            If CreditMemoNumber = "" Then
                strResponse = String.Format("Refund process Failed during Credit memo post.{0}", msg)
                Return False
            End If


            'Reverse GL for invoiced items, delivery, service, Relay, tax
            Dim bReversedGLTransactions As Boolean = ReverceInvoiceGLForRefund()
            Dim bReversedReceiptGLTransactions As Boolean = ReverseReceiptGLForRefund()

            If Not bReversedGLTransactions Or Not bReversedReceiptGLTransactions Then
                CreditMemoDelete(CreditMemoNumber)
                strResponse = String.Format("Refund process Failed during Reversal of GL transactions.")
                Return False
            End If



            Dim ApprovalNumber As String = ""
            Dim CCResponse As String = ""
            Dim clsRefuncCC As New clsCCreturn
            ApprovalNumber = clsRefuncCC.CCreturnprocess(CompanyId, DivisionId, DepartmentId, OrderNumber, RefundAmount, CCResponse)
            Dim obj_mail As New clsErrorMailHandling
            obj_mail.OrderNumber = OrderNumber
            obj_mail.ErrorMailHandling("Refund process start ", OrderNumber, CCResponse)

            If ApprovalNumber = "" Then
                strResponse = String.Format("Refund process successful except Credit card transaction. Payment Gateway Message: {1}. Please issue payment and process against credit memo #{0}", CreditMemoNumber, CCResponse)
                Return True
            End If

            strResponse = String.Format("Payment Approved with Approval number: {0}", ApprovalNumber)

            'Post payment and close Credit memo if closed invoice.
            Dim PaymentID As String = IssuePaymentForCreditMemo(CreditMemoNumber)
            If PaymentID = "" Then
                strResponse = String.Format("Refund process successful except payment posting for credit memo #{0}.", CreditMemoNumber)
                Return True
            End If

            strResponse = String.Format("Refund process completed with Credit memo #{0} and Approval #{1} and issued payment with ID: {2}.", CreditMemoNumber, ApprovalNumber, PaymentID)
            Return True

        ElseIf PaymentMethodID.ToLower() = "cash" Then

            'Post Credit Memo
            Dim CreditMemoNumber As String = ""
            CreditMemoNumber = PostCreditMemoForRefundedAmount()

            If CreditMemoNumber = "" Then
                strResponse = String.Format("Refund process Failed during Credit memo post.")
                Return False
            End If

            'Reverse GL for invoiced items, delivery, service, Relay, tax
            Dim bReversedGLTransactions As Boolean = ReverceInvoiceGLForRefund()
            Dim bReversedReceiptGLTransactions As Boolean = ReverseReceiptGLForRefund()

            If Not bReversedGLTransactions Or Not bReversedReceiptGLTransactions Then
                CreditMemoDelete(CreditMemoNumber)
                strResponse = String.Format("Refund process Failed during Reversal of GL transactions.")
                Return False
            End If

            HandlePOSShiftTransaction()

            'Post payment and close Credit memo if closed invoice.
            Dim PaymentID As String = IssuePaymentForCreditMemo(CreditMemoNumber)
            If PaymentID = "" Then
                strResponse = String.Format("Refund process successful except payment posting for credit memo #{0}.", CreditMemoNumber)
                Return True
            End If

            strResponse = String.Format("Refund process completed with Credit memo #{0} and issued payment with ID: {1}.", CreditMemoNumber, PaymentID)

            Return True

        ElseIf PaymentMethodID.ToLower() = "check" Then

            'Post Credit Memo
            Dim CreditMemoNumber As String = ""
            CreditMemoNumber = PostCreditMemoForRefundedAmount()

            If CreditMemoNumber = "" Then
                strResponse = String.Format("Refund process Failed during Credit memo post.")
                Return False
            End If

            'Reverse GL for invoiced items, delivery, service, Relay, tax
            Dim bReversedGLTransactions As Boolean = ReverceInvoiceGLForRefund()
            Dim bReversedReceiptGLTransactions As Boolean = ReverseReceiptGLForRefund()

            If Not bReversedGLTransactions Or Not bReversedReceiptGLTransactions Then
                CreditMemoDelete(CreditMemoNumber)
                strResponse = String.Format("Refund process Failed during Reversal of GL transactions.")
                Return False
            End If

            strResponse = String.Format("Refund process completed with Credit memo #{0}.", CreditMemoNumber)
            Return True

        ElseIf PaymentMethodID.ToLower() = "house account" Then

            'Post Credit Memo
            Dim CreditMemoNumber As String = ""
            CreditMemoNumber = PostCreditMemoForRefundedAmount()

            If CreditMemoNumber = "" Then
                strResponse = String.Format("Refund process Failed during Credit memo post.")
                Return False
            End If

            'Reverse GL for invoiced items, delivery, service, Relay, tax
            Dim bReversedGLTransactions As Boolean = ReverceInvoiceGLForRefund()


            If Not bReversedGLTransactions Then
                CreditMemoDelete(CreditMemoNumber)
                strResponse = String.Format("Refund process Failed during Reversal of GL transactions.")
                Return False
            End If

            ''Adjust against invoice if open invoice.
            'Dim bAdjustCreditMemoWithInvoice As Boolean = AdjustCreditMemoWithInvoice(CreditMemoNumber)
            'If Not bAdjustCreditMemoWithInvoice Then
            'strResponse = String.Format("Refund process Successful except Invoice#{0} and Credit memo# {1} adjustment.", OrderNumber, CreditMemoNumber)
            'Return True
            'End If

            strResponse = String.Format("Refund process completed with Credit memo #{0}.", CreditMemoNumber)
            Return True

        Else
            ''Payment method not supported for refund.
            strResponse = String.Format("Payment method not supported.")
            Return False
        End If

        Return False
    End Function

    Private Function ProcessRefundinCash() As Boolean


        If RefundPaymentMethodID.ToLower() = "cash" Then

            'Post Credit Memo
            Dim CreditMemoNumber As String = ""
            CreditMemoNumber = PostCreditMemoForRefundedAmount()

            If CreditMemoNumber = "" Then
                strResponse = String.Format("Refund process Failed during Credit memo post.")
                Return False
            End If

            'Reverse GL for invoiced items, delivery, service, Relay, tax
            Dim bReversedGLTransactions As Boolean = ReverceInvoiceGLForRefund()

            If Not bReversedGLTransactions Then
                CreditMemoDelete(CreditMemoNumber)
                strResponse = String.Format("Refund process Failed during Reversal of GL transactions.")
                Return False
            End If

            HandlePOSShiftTransaction()

            'Post payment and close Credit memo if closed invoice.
            Dim PaymentID As String = IssuePaymentForCreditMemo(CreditMemoNumber)
            If PaymentID = "" Then
                strResponse = String.Format("Refund process successful except payment posting for credit memo #{0}.", CreditMemoNumber)
                Return True
            End If

            Try
                Dim bCashPaymentGLTransactionPosted As Boolean = CreateCashPaymentGLTransaction()
                If Not bCashPaymentGLTransactionPosted Then
                    strResponse = String.Format("Refund process completed with Credit memo #{0} and issued payment with ID: {1}. But GL posting for payment in cash not done.", CreditMemoNumber, PaymentID)
                    Return True
                End If
            Catch ex As Exception
            End Try

            strResponse = String.Format("Refund process completed with Credit memo #{0} and issued payment with ID: {1}.", CreditMemoNumber, PaymentID)

            Return True
        Else
            ''Payment method not supported for refund.
            strResponse = String.Format("Payment method not supported.")
            Return False
        End If

        Return False
    End Function

    Private Function ReverceInvoiceGLForRefund() As Boolean
        Try


            Dim StoredProcedureToCall As String = "enterprise.InvoiceRefund_CreateGLTransaction"

            If PaymentMethodID.ToLower() = "wire_in" Or PaymentMethodID.ToLower() = "wire in" Then
                StoredProcedureToCall = "enterprise.InvoiceRefund_WireIn_CreateGLTransaction"
            End If

            If DeliveryMethodID.ToLower() = "wire_out" Or DeliveryMethodID.ToLower() = "wire out" Then
                StoredProcedureToCall = "enterprise.InvoiceRefund_WireOut_CreateGLTransaction"
            End If


            Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using Command As New SqlCommand(StoredProcedureToCall, Connection)
                    Command.CommandType = CommandType.StoredProcedure

                    Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                    Command.Parameters.AddWithValue("@DivisionID", DivisionId)
                    Command.Parameters.AddWithValue("@DepartmentID", DepartmentId)
                    Command.Parameters.AddWithValue("@OrderNumber", OrderNumber)
                    Command.Parameters.AddWithValue("@ProcessID", ProcessID)
                    Command.Parameters.AddWithValue("@Total", RefundAmount)
                    Command.Parameters.AddWithValue("@Delivery", Delivery)
                    Command.Parameters.AddWithValue("@Service", Service)
                    Command.Parameters.AddWithValue("@Relay", Relay)
                    Command.Parameters.AddWithValue("@HeaderTaxAmount", HeaderTaxAmount)
                    Command.Parameters.AddWithValue("@TaxGSTValue", TaxGSTValue)
                    Command.Parameters.AddWithValue("@TaxPSTValue", TaxPSTValue)


                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()
                    Return True
                End Using
            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function ReverseReceiptGLForRefund() As Boolean
        Try
            Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using Command As New SqlCommand("enterprise.InvoiceRefund_ReverseReceiptGLTransaction", Connection)
                    Command.CommandType = CommandType.StoredProcedure

                    Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                    Command.Parameters.AddWithValue("@DivisionID", DivisionId)
                    Command.Parameters.AddWithValue("@DepartmentID", DepartmentId)
                    Command.Parameters.AddWithValue("@OrderNumber", OrderNumber)
                    Command.Parameters.AddWithValue("@Total", RefundAmount)
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()
                    Return True
                End Using
            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public msg As String = ""

    Private Function PostCreditMemoForRefundedAmount() As String
        Try


            Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using Command As New SqlCommand("enterprise.CreateCreditMemoInRefundProcess", Connection)
                    Command.CommandType = CommandType.StoredProcedure


                    Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                    Command.Parameters.AddWithValue("@DivisionId", DivisionId)
                    Command.Parameters.AddWithValue("@DepartmentId", DepartmentId)
                    Command.Parameters.AddWithValue("@CustomerID", CustomerID)
                    Command.Parameters.AddWithValue("@Amount", RefundAmount)
                    Command.Parameters.AddWithValue("@CurrencyID", CurrencyID)
                    Command.Parameters.AddWithValue("@CurrencyExchangeRate", CurrencyExchangeRate)
                    Command.Parameters.AddWithValue("@CheckNumber", String.Format("INV-{0}", OrderNumber))
                    Command.Parameters.AddWithValue("@TransactionDate", DateTime.Now)
                    Command.Parameters.AddWithValue("@GLSalesAccount", Nothing)
                    Command.Parameters.AddWithValue("@Description", String.Format("Refund against order #{0}", OrderNumber))
                    Command.Parameters.AddWithValue("@LocationID", LocationID)
                    Command.Parameters.AddWithValue("@OrderNumber", OrderNumber)
                    Command.Parameters.AddWithValue("@RefundProcessID", ProcessID)
                    Dim parameterCreditMemosId As New SqlParameter("@CreditMemosID", Data.SqlDbType.NVarChar, 36)
                    parameterCreditMemosId.Direction = ParameterDirection.Output
                    Command.Parameters.Add(parameterCreditMemosId)

                    Command.Connection.Open()
                    'receiptID1 = 
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()

                    Return parameterCreditMemosId.Value
                End Using
            End Using
        Catch ex As Exception
            ''Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
        End Try

        Return ""
    End Function

    Private Function AdjustCreditMemoWithInvoice(ByVal CreditMemoNumber As String) As Boolean
        Try


            Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using Command As New SqlCommand("enterprise.AdjustCreditMemoWithInvoice_RefundProcess", Connection)
                    Command.CommandType = CommandType.StoredProcedure

                    Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                    Command.Parameters.AddWithValue("@DivisionID", DivisionId)
                    Command.Parameters.AddWithValue("@DepartmentID", DepartmentId)
                    Command.Parameters.AddWithValue("@OrderNumber", OrderNumber)
                    Command.Parameters.AddWithValue("@CreditMemoNumber", CreditMemoNumber)
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()
                    Return True
                End Using
            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function IssuePaymentForCreditMemo(ByVal CreditMemoNumber As String) As String
        Try


            Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using Command As New SqlCommand("enterprise.CreditMemo_RefundProcess_CreatePayment", Connection)
                    Command.CommandType = CommandType.StoredProcedure

                    Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                    Command.Parameters.AddWithValue("@DivisionID", DivisionId)
                    Command.Parameters.AddWithValue("@DepartmentID", DepartmentId)
                    Command.Parameters.AddWithValue("@InvoiceNumber", CreditMemoNumber)
                    Dim parameterPaymentID As New SqlParameter("@PaymentID", Data.SqlDbType.NVarChar, 36)
                    parameterPaymentID.Direction = ParameterDirection.Output
                    Command.Parameters.Add(parameterPaymentID)

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()
                    Return parameterPaymentID.Value
                End Using
            End Using

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Function CreditMemoDelete(ByVal CreditMemoNumber As String) As Boolean

        Try
            Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using Command As New SqlCommand("enterprise.Import_Invoice_Delete", Connection)
                    Command.CommandType = CommandType.StoredProcedure

                    Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                    Command.Parameters.AddWithValue("@DivisionID", DivisionId)
                    Command.Parameters.AddWithValue("@DepartmentID", DepartmentId)
                    Command.Parameters.AddWithValue("@InvoiceNumber", CreditMemoNumber)

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()
                End Using
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub UpdateOrderRefundProcessStatus(ByVal Status As Boolean, ByVal StatusDescription As String)

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("Enterprise.UpdateOrderRefundProcessStatus", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@ProcessID", ProcessID)
                Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                Command.Parameters.AddWithValue("@DivisionID", DivisionId)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentId)
                Command.Parameters.AddWithValue("@Status", Status)
                Command.Parameters.AddWithValue("@StatusDescription", StatusDescription)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                Catch ex As Exception
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Sub

    Private Sub HandlePOSShiftTransaction()

        Try


            Dim myCommand As SqlCommand
            ' Dim reader As SqlDataReader
            Dim cn As New SqlConnection(ConfigurationManager.AppSettings("COnnectionString"))

            myCommand = New SqlCommand( _
           " INSERT INTO POSShiftTransaction(CompanyID,DivisionID,DepartmentID,EmployeeID,OrderNumber," & _
           " ShiftID,TerminalID,CustomerID,TransDateTime,OrderAmount,TransType,PaymentMethodID)" & _
           " VALUES(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@OrderNumber," & _
           " @ShiftID,@TerminalID,@CustomerID,getdate(),@OrderAmount,@TransType,@PaymentMethodID)", cn)

            myCommand.Parameters.AddWithValue("@CompanyID", CompanyId)
            myCommand.Parameters.AddWithValue("@DivisionID", DivisionId)
            myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentId)
            myCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID)
            myCommand.Parameters.AddWithValue("@TransType", "OrderRefund")
            myCommand.Parameters.AddWithValue("@PaymentMethodID", "Cash")
            myCommand.Parameters.AddWithValue("@OrderNumber", OrderNumber)
            myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
            myCommand.Parameters.AddWithValue("@TerminalID", TerminalID)
            myCommand.Parameters.AddWithValue("@CustomerID", CustomerID)
            myCommand.Parameters.AddWithValue("@OrderAmount", (-1) * RefundAmount)
            cn.Open()
            myCommand.ExecuteNonQuery()
            cn.Close()

            'To Update the CurrentBalance in POSShiftMaster	

            'Dim CurrentBalance As Double = 0.0
            'myCommand = New SqlCommand( _
            '    "SELECT CurrentBalance FROM POSShiftMaster WHERE ShiftID=@ShiftID", cn)
            'myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
            'cn.Open()
            'If IsDBNull(myCommand.ExecuteScalar) Then
            '    CurrentBalance = 0.0
            'Else
            '    CurrentBalance = myCommand.ExecuteScalar
            'End If
            'cn.Close()

            'If drpRefundPaymentType.SelectedValue.ToLower = "cash" Or drpRefundPaymentType.SelectedValue.ToLower = "check" Then

            'CurrentBalance = CurrentBalance + RefundAmount

            myCommand = New SqlCommand( _
            "UPDATE POSShiftMaster SET CurrentBalance = isnull(CurrentBalance,0) - " + RefundAmount.ToString + "WHERE ShiftID=@ShiftID ", cn)
            myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
            'myCommand.Parameters.AddWithValue("@CurrentBalance", CurrentBalance)
            cn.Open()
            myCommand.ExecuteNonQuery()
            cn.Close()

            '  End If

            ''Code For trace all entry 
            ' If drpRefundPaymentType.SelectedValue <> "Will Call" Then

            myCommand = New SqlCommand( _
            " INSERT INTO POSTransactionTrace(CompanyID,DivisionID,DepartmentID,EmployeeID,OrderNumber," & _
            " ShiftID,TerminalID,CustomerID,TransDateTime,OrderAmount,TransType,PaymentMethodID)" & _
            " VALUES(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@OrderNumber," & _
            " @ShiftID,@TerminalID,@CustomerID,getdate(),@OrderAmount,@TransType,@PaymentMethodID)", cn)

            myCommand.Parameters.AddWithValue("@CompanyID", CompanyId)
            myCommand.Parameters.AddWithValue("@DivisionID", DivisionId)
            myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentId)
            myCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID)
            myCommand.Parameters.AddWithValue("@TransType", "OrderRefund")
            myCommand.Parameters.AddWithValue("@PaymentMethodID", "Cash")
            myCommand.Parameters.AddWithValue("@OrderNumber", OrderNumber)
            myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
            myCommand.Parameters.AddWithValue("@TerminalID", TerminalID)
            myCommand.Parameters.AddWithValue("@CustomerID", CustomerID)
            myCommand.Parameters.AddWithValue("@OrderAmount", (-1) * RefundAmount)
            cn.Open()
            myCommand.ExecuteNonQuery()
            cn.Close()

            ' End If
            ''Code For trace all entry 
        Catch ex As Exception

        End Try

    End Sub

    Public Function InsertRefundReceiptPrintRequest(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal TerminalName As String, ByVal RefundProcessID As String, ByVal PrintText As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[InsertRefundReceiptPrintRequest]", Connection)
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("TerminalName", TerminalName)
                Command.Parameters.AddWithValue("RefundProcessID", RefundProcessID)
                Command.Parameters.AddWithValue("PrintText", PrintText)
                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()
                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End Using
        End Using
    End Function

    Private Function CreateCashPaymentGLTransaction() As Boolean
        Try
            Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using Command As New SqlCommand("enterprise.InvoiceRefund_CreateCashPaymentGLTransaction", Connection)
                    Command.CommandType = CommandType.StoredProcedure

                    Command.Parameters.AddWithValue("@CompanyID", CompanyId)
                    Command.Parameters.AddWithValue("@DivisionID", DivisionId)
                    Command.Parameters.AddWithValue("@DepartmentID", DepartmentId)
                    Command.Parameters.AddWithValue("@OrderNumber", OrderNumber)
                    Command.Parameters.AddWithValue("@Total", RefundAmount)
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Command.Connection.Close()
                    Return True
                End Using
            End Using

        Catch ex As Exception
            Return False
        End Try

    End Function
End Class
