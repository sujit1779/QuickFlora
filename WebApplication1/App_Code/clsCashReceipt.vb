Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsCashReceipt
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    Public CompanyId As String
    Public DivisionId As String
    Public DepartmentId As String

    Public Function FillReceiptTypes() As SqlDataReader

        Dim ConString As New SqlConnection
        ConString.ConnectionString = constr
        Dim sqlStr As String = "select ReceiptTypeID,ReceiptTypeDescription from Receipttypes where  CompanyID='" & Me.CompanyId & "' and  DivisionID ='" & Me.DivisionId & "' and  DepartmentID='" & Me.DepartmentId & "'"
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        Return rs
    End Function

    Public Function FillCurrencyTypes() As SqlDataReader

        Dim ConString As New SqlConnection
        ConString.ConnectionString = constr
        Dim sqlStr As String = "select CurrencyID,Currencytype,currencyID + ', ' + CurrencyType as Currencydescription from Currencytypes where  CompanyID='" & Me.CompanyId & "' and  DivisionID ='" & Me.DivisionId & "' and  DepartmentID='" & Me.DepartmentId & "'"
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        Return rs
    End Function

    Public Function FillCustomerID() As SqlDataReader

        Dim ConString As New SqlConnection
        ConString.ConnectionString = constr
        'Dim sqlStr As String = "select CustomerID,CustomerID + ', ' + CustomerfirstName + ' ' + CustomerLastName as CustomerName from CustomerInformation where  CompanyID='" & Me.CompanyId & "' and  DivisionID ='" & Me.DivisionId & "' and  DepartmentID='" & Me.DepartmentId & "'"
        Dim sqlStr As String = "select Top 50 CustomerID,ISNULL(CustomerID,'') + ', ' + ISNULL(CustomerfirstName,'') + ' ' + ISNULL(CustomerLastName,'') as CustomerName from CustomerInformation where  CompanyID='" & Me.CompanyId & "' and  DivisionID ='" & Me.DivisionId & "' and  DepartmentID='" & Me.DepartmentId & "'"
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        Return rs
    End Function
    Public Function UpdateCashReceipt(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal Amount As String, ByVal ProjectID As String, ByVal ReceiptTypeID As String, ByVal CheckNumber As String, ByVal CustomerID As String, ByVal CurrencyID As String, ByVal CurrencyExchangeRate As String, ByVal TransactionDate As String, ByVal DueToDate As String, ByVal OrderDate As String, ByVal ReceiptClassID As String) As String
        Try
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("enterprise.Receipt_CreateFromInvoice_New", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyId As New SqlParameter("@CompanyId", Data.SqlDbType.NVarChar)
            parameterCompanyId.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyId)

            Dim parameterDepartmentId As New SqlParameter("@DepartmentId", Data.SqlDbType.NVarChar)
            parameterDepartmentId.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentId)

            Dim parameterDivisionId As New SqlParameter("@DivisionId", Data.SqlDbType.NVarChar)
            parameterDivisionId.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionId)

            Dim parameterAmount As New SqlParameter("@Amount", Data.SqlDbType.SmallMoney)
            parameterAmount.Value = Amount
            myCommand.Parameters.Add(parameterAmount)

            Dim parameterProjectID As New SqlParameter("@ProjectID", Data.SqlDbType.NVarChar)
            parameterProjectID.Value = ProjectID
            myCommand.Parameters.Add(parameterProjectID)

            Dim parameterReceiptTypeID As New SqlParameter("@ReceiptTypeID", Data.SqlDbType.NVarChar)
            parameterReceiptTypeID.Value = ReceiptTypeID
            myCommand.Parameters.Add(parameterReceiptTypeID)

            Dim parametercheckNumber As New SqlParameter("@checkNumber", Data.SqlDbType.NVarChar)
            parametercheckNumber.Value = CheckNumber
            myCommand.Parameters.Add(parametercheckNumber)

            Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar)
            parameterCustomerID.Value = CustomerID
            myCommand.Parameters.Add(parameterCustomerID)

            Dim parameterCurrencyID As New SqlParameter("@CurrencyID", Data.SqlDbType.NVarChar)
            parameterCurrencyID.Value = CurrencyID
            myCommand.Parameters.Add(parameterCurrencyID)

            Dim parameterCurrencyExchangeRate As New SqlParameter("@CurrencyExchangeRate", Data.SqlDbType.NVarChar)
            parameterCurrencyExchangeRate.Value = CurrencyExchangeRate
            myCommand.Parameters.Add(parameterCurrencyExchangeRate)


            Dim parameterTransactionDate As New SqlParameter("@TransactionDate", Data.SqlDbType.SmallDateTime)
            parameterTransactionDate.Value = TransactionDate
            myCommand.Parameters.Add(parameterTransactionDate)

            Dim parameterDueToDate As New SqlParameter("@DueToDate", Data.SqlDbType.SmallDateTime)
            parameterDueToDate.Value = DueToDate
            myCommand.Parameters.Add(parameterDueToDate)

            Dim parameterOrderDate As New SqlParameter("@OrderDate", Data.SqlDbType.SmallDateTime)
            parameterOrderDate.Value = OrderDate
            myCommand.Parameters.Add(parameterOrderDate)

            Dim parameterReceiptClassID As New SqlParameter("@ReceiptClassID", Data.SqlDbType.NVarChar)
            parameterReceiptClassID.Value = ReceiptClassID
            myCommand.Parameters.Add(parameterReceiptClassID)

            Dim parameterReceiptId As New SqlParameter("@ReceiptID", Data.SqlDbType.NVarChar)
            parameterReceiptId.Direction = ParameterDirection.Output
            parameterReceiptId.Size = 36
            myCommand.Parameters.Add(parameterReceiptId)


            'Dim receiptID1 As String

            myCon.Open()
            'receiptID1 = 
            myCommand.ExecuteNonQuery()
            myCon.Close()

            Return parameterReceiptId.Value
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)


        End Try
    End Function


    Public Sub PostCashReceipt(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ReceiptID As String)
        Try
            Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("enterprise.Receipt_Post", myCon)
            myCommand.CommandType = Data.CommandType.StoredProcedure

            Dim parameterCompanyId As New SqlParameter("@CompanyId", Data.SqlDbType.NVarChar)
            parameterCompanyId.Value = CompanyID
            myCommand.Parameters.Add(parameterCompanyId)

            Dim parameterDepartmentId As New SqlParameter("@DepartmentId", Data.SqlDbType.NVarChar)
            parameterDepartmentId.Value = DepartmentID
            myCommand.Parameters.Add(parameterDepartmentId)

            Dim parameterDivisionId As New SqlParameter("@DivisionId", Data.SqlDbType.NVarChar)
            parameterDivisionId.Value = DivisionID
            myCommand.Parameters.Add(parameterDivisionId)


            Dim parameterReceiptId As New SqlParameter("@ReceiptID", Data.SqlDbType.NVarChar)
            parameterReceiptId.Value = ReceiptID
            myCommand.Parameters.Add(parameterReceiptId)


            myCon.Open()
            myCommand.ExecuteNonQuery()
            myCon.Close()


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)


        End Try
    End Sub


End Class
