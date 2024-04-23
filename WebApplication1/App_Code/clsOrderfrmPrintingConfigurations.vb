Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsOrderfrmPrintingConfigurations
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public WorkTicket1PrinterName As String
    Public CardMessage1PrinterName As String
    Public WorkTicketPrinterProfileName As String ''Added By Vikas
    Public CardMessagePrinterProfilName As String ''Added By Vikas
    Public WorkTicket1bit As Boolean
    Public CardMessage1bit As Boolean
    Public WorkTicket2bit As Boolean
    Public CardMessage2bit As Boolean
    Public activexenable As Boolean



#Region "Functions"

    Public Function Insert() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit,WorkTicket2bit,CardMessage2bit) " _
            & " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 100)).Value = Me.WorkTicket1PrinterName
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 100)).Value = Me.CardMessage1PrinterName
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Bit)).Value = Me.WorkTicket1bit
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit)).Value = Me.CardMessage1bit
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Bit)).Value = Me.WorkTicket2bit
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.Bit)).Value = Me.CardMessage2bit

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

    Public Function InsertNew() As Boolean '' Changed By Vikas on 07/10/2008

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit,WorkTicket2bit,CardMessage2bit,WorkTicketPrinterProfileName,CardMessagePrinterProfilName) " _
            & " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8,@f9,@f10)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 100)).Value = Me.WorkTicket1PrinterName
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 100)).Value = Me.CardMessage1PrinterName
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.Bit)).Value = Me.WorkTicket1bit
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.Bit)).Value = Me.CardMessage1bit
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Bit)).Value = Me.WorkTicket2bit
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.Bit)).Value = Me.CardMessage2bit
            com.Parameters.Add(New SqlParameter("@f9", SqlDbType.NVarChar, 100)).Value = Me.WorkTicketPrinterProfileName
            com.Parameters.Add(New SqlParameter("@f10", SqlDbType.NVarChar, 100)).Value = Me.CardMessagePrinterProfilName

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


    Public Function FillDetails() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderfrmPrintingConfigurations where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function Returndatatable(ByVal querry As String) As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(querry, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function UpdatePrinterName() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update OrderfrmPrintingConfigurations set  WorkTicket1PrinterName=@f1, CardMessage1PrinterName=@f2 " _
        & " where    CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 100)).Value = Me.WorkTicket1PrinterName
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 100)).Value = Me.CardMessage1PrinterName
            
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

    Public Function UpdatePrinterNameNew() As Boolean  '' Changed By Vikas on 07/10/2008
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update OrderfrmPrintingConfigurations set  WorkTicket1PrinterName=@f1, CardMessage1PrinterName=@f2 ,WorkTicketPrinterProfileName=@f3, CardMessagePrinterProfileName=@f4 " _
        & " where    CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 100)).Value = Me.WorkTicket1PrinterName
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 100)).Value = Me.CardMessage1PrinterName
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 100)).Value = Me.WorkTicket1PrinterName
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 100)).Value = Me.CardMessage1PrinterName

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


    Public Function UpdateActiveXstatus() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim qry1 As String
        'uzair
        qry1 = "Select Activexstatus from OrderfrmPrintingConfigurations Where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        qry = "update OrderfrmPrintingConfigurations set  Activexstatus=@f1" _
        & " where    CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.Bit)).Value = Me.activexenable
            

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

   
    Public Function SelectActiveXstatus(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
        Dim ConnectionString As String = ""
        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT Activexstatus FROM OrderfrmPrintingConfigurations WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'"
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        Return rs
    End Function


    Public Function UpdateBit() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update OrderfrmPrintingConfigurations set  WorkTicket1bit=@f1, CardMessage1bit=@f2,WorkTicket2bit=@f3, CardMessage2bit=@f4 " _
        & " where      CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.Bit)).Value = Me.WorkTicket1bit
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.Bit)).Value = Me.CardMessage1bit
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.Bit)).Value = Me.WorkTicket2bit
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.Bit)).Value = Me.CardMessage2bit


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


    Public Function OrderDetailsDateWise(ByVal ordernumber As String) As DataTable
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "SELECT       OrderHeader.OrderNumber  , OrderHeader.CustomerID  , " _
            & "OrderHeader.Total  , OrderHeader.ShipMethodID  , OrderHeader.PaymentMethodID ,OrderHeader.OrderTypeID  " _
            & " FROM  OrderHeader " _
            & " where OrderHeader.CompanyID=@f1  and OrderHeader.DivisionID=@f2 and OrderHeader.DepartmentID=@f3 and OrderHeader.OrderNumber=@f4"


        'HttpContext.Current.Response.Write(qry)

        Dim com As SqlCommand
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 50)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 50)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 50)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 50)).Value = ordernumber

            da.SelectCommand = com
            da.Fill(dt)
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
        End Try
        Return dt
    End Function


    Public Function PopulatePaymentTypes(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT * FROM PaymentMethods WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'"
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        Return rs
    End Function

    Public Function SelectCardPrinttype(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
        Dim ConnectionString As String = ""
        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT CardPintType FROM OrderfrmPrintingConfigurations WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'"
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        Return rs
    End Function


#End Region



    Public OnCardWTPaymentMethod As Boolean = False

    Public Function UpdateOnCardWTPaymentMethod() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        
        qry = "update OrderfrmPrintingConfigurations set  OnCardWTPaymentMethod=@f1" _
        & " where    CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        'qry = "insert into OrderfrmPrintingConfigurations( CompanyID, DivisionID, DepartmentID, WorkTicket1PrinterName, CardMessage1PrinterName, WorkTicket1bit, CardMessage1bit) " _
        '& " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.VarChar)).Value = Me.OnCardWTPaymentMethod


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



    Public Function SelectOnCardWTPaymentMethod(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
        Dim ConnectionString As String = ""
        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT OnCardWTPaymentMethod FROM OrderfrmPrintingConfigurations WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "'"
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
        Return rs
    End Function




End Class
