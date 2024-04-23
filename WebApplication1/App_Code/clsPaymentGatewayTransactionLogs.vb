Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Data

Public Class clsPaymentGatewayTransactionLogs

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    Public CustomerID As String
    Public InLineNumber As String
    Public PaymentGateway As String

    Public ReferenceNumber As String
    Public RefrenaceType As String
    Public ProcessDate As Date

    Public ProcessAmount As Double
    Public ProcessType As String
    Public ProcessDetails As String

    Public PPIOrderID As String
    Public PayPalPNREF As String
    Public PPIApprovalNumber As String

    Public CreditCardNumber As String
    Public CreditCardExpDate As String
    Public CreditCardCSVNumber As String

    Public ResponseMessage As String
    Public ResponseNumber As String
    Public SecondryResponseMessage As String

    'CompanyID, DivisionID, DepartmentID, CustomerID, InLineNumber, PaymentGateway, ReferenceNumber, RefrenaceType, ProcessDate, 
    'ProcessAmount, ProcessType, ProcessDetails, PPIOrderID, PayPalPNREF, PPIApprovalNumber, CreditCardNumber, CreditCardExpDate, 
    'CreditCardCSVNumber, ResponseMessage, ResponseNumber, SecondryResponseMessage
    'FROM  PaymentGatewayTransactionLogs

    '(<CompanyID, nvarchar(36),>
    ',<DivisionID, nvarchar(36),>
    ',<DepartmentID, nvarchar(36),>
    ',<CustomerID, nvarchar(36),>
    ',<PaymentGateway, nvarchar(36),>
    ',<ReferenceNumber, nvarchar(36),>
    ',<RefrenaceType, nvarchar(36),>
    ',<ProcessDate, datetime,>
    ',<ProcessAmount, money,>
    ',<ProcessType, nvarchar(100),>
    ',<ProcessDetails, nvarchar(100),>
    ',<PPIOrderID, nvarchar(100),>
    ',<PayPalPNREF, nvarchar(100),>
    ',<PPIApprovalNumber, nvarchar(100),>
    ',<CreditCardNumber, nvarchar(20),>
    ',<CreditCardExpDate, nvarchar(50),>
    ',<CreditCardCSVNumber, nvarchar(5),>
    ',<ResponseMessage, nvarchar(1000),>
    ',<ResponseNumber, nchar(10),>
    ',<SecondryResponseMessage, nvarchar(1000),>)


    Public Function InsertPaymentGatewayTransactionLogs() As Boolean

        Me.InLineNumber = Date.Now.Year & Date.Now.Month & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO PaymentGatewayTransactionLogs ( CompanyID, DivisionID, DepartmentID, CustomerID, InLineNumber, PaymentGateway, ReferenceNumber, RefrenaceType, ProcessDate, "
        qry = qry & " ProcessAmount, ProcessType, ProcessDetails, PPIOrderID, PayPalPNREF, PPIApprovalNumber, CreditCardNumber, CreditCardExpDate, "
        qry = qry & " ResponseMessage, ResponseNumber, SecondryResponseMessage )"
        qry = qry & " VALUES(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13,@f14,@f15,@f16,@f17,@f19,@f20,@f21)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.CustomerID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.BigInt)).Value = Me.InLineNumber
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = Me.PaymentGateway
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 36)).Value = Me.ReferenceNumber
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 36)).Value = Me.RefrenaceType
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.DateTime)).Value = Me.ProcessDate
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.Money)).Value = Me.ProcessAmount
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.NVarChar, 100)).Value = Me.ProcessType
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.NVarChar, 100)).Value = Me.ProcessDetails
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 100)).Value = Me.PPIOrderID
            com.Parameters.Add(New SqlParameter("@f14", SqlDbType.NVarChar, 100)).Value = Me.PayPalPNREF
            com.Parameters.Add(New SqlParameter("@f15", SqlDbType.NVarChar, 100)).Value = Me.PPIApprovalNumber

            Dim AES_cardnumber As String = ""
            AES_cardnumber = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(Me.CreditCardNumber, Me.CustomerID.Trim)

            com.Parameters.Add(New SqlParameter("@f16", SqlDbType.NVarChar, 50)).Value = AES_cardnumber

            com.Parameters.Add(New SqlParameter("@f17", SqlDbType.NVarChar, 50)).Value = Me.CreditCardExpDate
            'com.Parameters.Add(New SqlParameter("@f18", SqlDbType.NVarChar, 5)).Value = Me.CreditCardCSVNumber
            com.Parameters.Add(New SqlParameter("@f19", SqlDbType.NVarChar, 1000)).Value = Me.ResponseMessage
            com.Parameters.Add(New SqlParameter("@f20", SqlDbType.NVarChar, 10)).Value = Me.ResponseNumber
            com.Parameters.Add(New SqlParameter("@f21", SqlDbType.NVarChar, 1000)).Value = Me.SecondryResponseMessage


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



    Public Function UpdatePaymentGatewayTransactionLogsTdata(ByVal Tdata As String, ByVal transactionConditionCode As String) As String
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PaymentGatewayTransactionLogs set Tdata=@f5,transactionConditionCode=@f6  Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And InLineNumber=@f4  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.BigInt)).Value = Me.InLineNumber


            Dim AES_Tdata As String = ""
            AES_Tdata = CryptographyRijndael.EncryptionRijndael.RijndaelEncode(Tdata, Me.InLineNumber)

            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar)).Value = AES_Tdata

            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 36)).Value = transactionConditionCode

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return msg
        End Try
    End Function



    Public Function UpdatePaymentGatewayTransactionLogsOnResponseReferenceNumber() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PaymentGatewayTransactionLogs set ReferenceNumber=@f5  Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And InLineNumber=@f4  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.BigInt)).Value = Me.InLineNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.ReferenceNumber

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




    Public Function UpdatePaymentGatewayTransactionLogsOnResponseTrue() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PaymentGatewayTransactionLogs set ResponseMessage=@f5,ResponseNumber=@f6,SecondryResponseMessage=@f7,PPIOrderID=@f8,PayPalPNREF=@f9,PPIApprovalNumber=@f10   Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And InLineNumber=@f4  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.BigInt)).Value = Me.InLineNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 1000)).Value = Me.ResponseMessage
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 10)).Value = Me.ResponseNumber
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 1000)).Value = Me.SecondryResponseMessage
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 100)).Value = Me.PPIOrderID
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 100)).Value = Me.PayPalPNREF
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 100)).Value = Me.PPIApprovalNumber



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




    Public Function UpdatePaymentGatewayTransactionLogsOnResponseFalse() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PaymentGatewayTransactionLogs set ResponseMessage=@f5,ResponseNumber=@f6,SecondryResponseMessage=@f7  Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And InLineNumber=@f4  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.BigInt)).Value = Me.InLineNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 1000)).Value = Me.ResponseMessage
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 10)).Value = Me.ResponseNumber
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 1000)).Value = Me.SecondryResponseMessage



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




    Public Function GetDetailsPaymentGatewayTransactionLogs() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from PaymentGatewayTransactionLogs where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
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
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try


    End Function



    Public Function GetDetailsPaymentGatewayTransactionLogsByInLineNumber() As DataTable


        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from PaymentGatewayTransactionLogs where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and InLineNumber=@InLineNumber"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = Me.InLineNumber

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




    Public Function PaymentGatewayTransactionLogsSearchList(ByVal Condition As String, ByVal fieldName As String, ByVal fieldexpression As String, ByVal FromDate As String, ByVal ToDate As String, ByVal AllDate As Integer, ByVal SortField As String, ByVal SortDirection As String) As DataSet


        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("PaymentGatewayTransactionLogsSearchList", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = Me.CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = Me.DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = Me.DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterCondition As New SqlParameter("@Condition", Data.SqlDbType.NVarChar)
        parameterCondition.Value = Condition
        myCommand.Parameters.Add(parameterCondition)


        Dim parameterfieldName As New SqlParameter("@fieldName", Data.SqlDbType.NVarChar)
        parameterfieldName.Value = fieldName
        myCommand.Parameters.Add(parameterfieldName)

        Dim parameterfieldexpression As New SqlParameter("@fieldexpression", Data.SqlDbType.NVarChar)
        parameterfieldexpression.Value = fieldexpression
        myCommand.Parameters.Add(parameterfieldexpression)


        Dim parameterFromDate As New SqlParameter("@FromDate", Data.SqlDbType.NVarChar)
        parameterFromDate.Value = FromDate
        myCommand.Parameters.Add(parameterFromDate)

        Dim parameterToDate As New SqlParameter("@ToDate", Data.SqlDbType.NVarChar)
        parameterToDate.Value = ToDate
        myCommand.Parameters.Add(parameterToDate)


        Dim parameterAllDate As New SqlParameter("@AllDate", Data.SqlDbType.Int)
        parameterAllDate.Value = AllDate
        myCommand.Parameters.Add(parameterAllDate)


        Dim parameterSortField As New SqlParameter("@SortField", Data.SqlDbType.NVarChar)
        parameterSortField.Value = SortField
        myCommand.Parameters.Add(parameterSortField)

        Dim parameterSortDirection As New SqlParameter("@SortDirection", Data.SqlDbType.NVarChar)
        parameterSortDirection.Value = SortDirection
        myCommand.Parameters.Add(parameterSortDirection)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataSet()
        adapter.Fill(ds)
        conString.Close()

        Return ds


    End Function


End Class
