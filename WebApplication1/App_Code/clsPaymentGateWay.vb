Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsPaymentGateWay

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    Public Payment_Gateway As String
    Public PPI_TOKEN As String
    Public PPI_TOKENTest As String
    Public Active As String
    Public eComerceTransactions As String
    Public OrderEntryTransactions As String
    Public POSTransactions As String

    Public AVS As Boolean
    Public CVV As Boolean
    Public AVSZIPCODE As Boolean


    'CREATE TABLE [dbo].[Payment_Gateway](
    '[CompanyID] [nvarchar](36) NOT NULL,
    '[DivisionID] [nvarchar](36) NOT NULL,
    '[DepartmentID] [nvarchar](36) NOT NULL,
    '[Payment_Gateway] [nvarchar](36) NULL,
    '[PPI_TOKEN] [nvarchar](100) NULL,



    Public Function FillDetailsPPI(Optional ByVal UsedIn As String = "", Optional ByVal LocationID As String = "") As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from Payment_Gateway where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        If UsedIn <> String.Empty Then ssql = ssql + " and  UsedIn='" & UsedIn & "'"
        If LocationID <> String.Empty Then ssql = ssql + " and  LocationID='" & LocationID & "'"

        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function FillDetailsPaymentGatwayByOrder(ByVal Ordernumber As String) As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from Payment_Gateway_ByOrder where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and  [Ordernumber]='" & Ordernumber & "'   and [Payment_Type] ='Order' "
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function StorePaymentGatwayByOrder(ByVal UsedIn As String, ByVal LocationID As String, ByVal Ordernumber As String, Optional ByVal Payment_Type As String = "") As Boolean

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("dbo.[StorePaymentGatwayByOrder]", myCon)
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

        Dim parameterUsedIn As New SqlParameter("@UsedIn ", Data.SqlDbType.NVarChar)
        parameterUsedIn.Value = UsedIn
        myCommand.Parameters.Add(parameterUsedIn)

        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Ordernumber
        myCommand.Parameters.Add(parameterOrderNumber)

        '@Payment_Type
        Dim parameterPayment_Type As New SqlParameter("@Payment_Type", Data.SqlDbType.NVarChar)
        parameterPayment_Type.Value = Payment_Type
        myCommand.Parameters.Add(parameterPayment_Type)


        Dim parameterLocationID As New SqlParameter("@LocationID  ", Data.SqlDbType.NVarChar)
        parameterLocationID.Value = LocationID
        myCommand.Parameters.Add(parameterLocationID)


        myCon.Open()

        myCommand.ExecuteNonQuery()
        myCon.Close()


        Return True

    End Function




    Public Function FillDetailsMercury(Optional ByVal UsedIn As String = "", Optional ByVal LocationID As String = "") As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from Payment_Gateway where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        If UsedIn <> String.Empty Then ssql = ssql + " and  UsedIn='" & UsedIn & "'"
        If LocationID <> String.Empty Then ssql = ssql + " and  LocationID='" & LocationID & "'"

        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function FillDetails(Optional ByVal UsedIn As String = "") As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from Payment_Gateway where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        If UsedIn <> String.Empty Then ssql = ssql + " and  UsedIn='" & UsedIn & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

      Public Function Insert() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into Payment_Gateway( CompanyID, DivisionID, DepartmentID, Payment_Gateway, PPI_TOKEN,PPI_TOKENTest,Active,eComerceTransactions,OrderEntryTransactions,POSTransactions,AVS,CVV,AVSZIPCODE,POSSecondryTransactions) " _
             & " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10,@f11,@f12,@f13)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.Payment_Gateway
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 100)).Value = Me.PPI_TOKEN
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 100)).Value = ""
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 10)).Value = "Live"
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.NVarChar, 2)).Value = "5"
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.NVarChar, 2)).Value = "2"
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 2)).Value = "7"
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f11", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f12", SqlDbType.Bit)).Value = False
            com.Parameters.Add(New SqlParameter("@f13", SqlDbType.NVarChar, 2)).Value = "2"


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

    Public Function UpdatePayment_Gateway(ByVal UsedIn As String) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update Payment_Gateway set  Payment_Gateway = @f1 " _
        & " where      CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and  UsedIn='" & UsedIn & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.Payment_Gateway

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

    Public Function UpdatePayment_Gateway_Offline(ByVal cardoffline As Boolean, Optional ByVal UsedIn As String = "") As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update PaymentAccounts set  CreditCardOffline=@f1 " _
        & " where      CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        If UsedIn <> String.Empty Then qry = qry + " and  UsedIn='" & UsedIn & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.Bit)).Value = cardoffline

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

    Public POSSecondryTransactions As String

    Public Function UpdatePPI_TOKEN(ByVal UsedIn As String) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update Payment_Gateway set  PPI_TOKEN=@f1,PPI_TOKENTest=@f2,Active=@f3,eComerceTransactions=@f4,OrderEntryTransactions=@f5,POSTransactions=@f6,AVS=@f7,CVV=@f8,AVSZIPCODE=@f9,POSSecondryTransactions=@f10  " _
        & " where      CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and  UsedIn='" & UsedIn & "'"


        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 100)).Value = Me.PPI_TOKEN
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 100)).Value = Me.PPI_TOKENTest
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 10)).Value = Me.Active
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 2)).Value = Me.eComerceTransactions
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 2)).Value = Me.OrderEntryTransactions
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 2)).Value = Me.POSTransactions
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Bit)).Value = Me.AVS
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.Bit)).Value = Me.CVV
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.Bit)).Value = Me.AVSZIPCODE
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 2)).Value = Me.POSSecondryTransactions

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


     Public Merchant_SiteID As String
    Public Merchant_Key As String
    Public Merchant_Name As String
    Public Merchant_TestURL As String
    Public Merchant_LiveURL As String
    Public Merchant_Mode As String



    Public Function UpdateMerchant(ByVal UsedIn As String) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update Payment_Gateway set  Merchant_SiteID=@Merchant_SiteID,Merchant_Key=@Merchant_Key,Merchant_Name=@Merchant_Name,Merchant_TestURL=@Merchant_TestURL,Merchant_LiveURL=@Merchant_LiveURL,Merchant_Mode=@Merchant_Mode  " _
        & " where      CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and  UsedIn='" & UsedIn & "'"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@Merchant_SiteID", SqlDbType.NVarChar)).Value = Me.Merchant_SiteID
            com.Parameters.Add(New SqlParameter("@Merchant_Key", SqlDbType.NVarChar)).Value = Me.Merchant_Key
            com.Parameters.Add(New SqlParameter("@Merchant_Name", SqlDbType.NVarChar)).Value = Me.Merchant_Name
            com.Parameters.Add(New SqlParameter("@Merchant_TestURL", SqlDbType.NVarChar)).Value = Me.Merchant_TestURL
            com.Parameters.Add(New SqlParameter("@Merchant_LiveURL", SqlDbType.NVarChar)).Value = Me.Merchant_LiveURL
            com.Parameters.Add(New SqlParameter("@Merchant_Mode", SqlDbType.NVarChar)).Value = Me.Merchant_Mode

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


    Public Function UpdateAuth_Capture_Sale(ByVal val As Boolean, Optional ByVal UsedIn As String = "") As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "UPDATE Payment_Gateway set  Auth_Capture=@f1 " _
        & " WHERE CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        If UsedIn <> String.Empty Then qry = qry + " and  UsedIn='" & UsedIn & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.Bit)).Value = val

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


    Public APILoginID As String = ""
    Public TransactionKey As String = ""


    Public Function UpdateAuthorizeNet(ByVal UsedIn As String) As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update Payment_Gateway set  APILoginID=@APILoginID,TransactionKey=@TransactionKey  " _
        & " where      CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and  UsedIn ='" & UsedIn & "'"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        ' Try

        com.Parameters.Add(New SqlParameter("@APILoginID", SqlDbType.NVarChar)).Value = Me.APILoginID
        com.Parameters.Add(New SqlParameter("@TransactionKey", SqlDbType.NVarChar)).Value = Me.TransactionKey

        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()

        Return True

        '  Catch ex As Exception
        'Dim msg As String
        ' msg = ex.Message
        ' HttpContext.Current.Response.Write(msg)
        ' Return False

        '  End Try
    End Function

    Public Function GetPaymentGatwayList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataSet


        Dim ds As New DataSet

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetPaymentGatewayList]", Connection)
                Command.CommandType = CommandType.StoredProcedure
                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function

End Class
