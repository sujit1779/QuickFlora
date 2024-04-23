Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsOrderfrmPrintingException

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public OrderfrmType As String ' Order type is POS or BACK
    Public PrintType As String 'WorkTicket or Card
    Public Method As String 'Payment or Card
    Public Value As String 'Value for selected set

    'SELECT [CompanyID]
    '      ,[DivisionID]
    '      ,[DepartmentID]
    '      ,[OrderfrmType]
    '      ,[PrintType]
    '      ,[Method]
    '      ,[Value]
    '  FROM [Enterprise].[dbo].[OrderfrmPrintingException]

#Region "Functions"

    Public Function Insert() As String

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into OrderfrmPrintingException( CompanyID, DivisionID, DepartmentID, OrderfrmType, PrintType, Method, Value) " _
            & " values(@f0,@f1,@f2,@f3,@f4,@f5,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 50)).Value = Me.OrderfrmType
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 50)).Value = Me.PrintType
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 50)).Value = Me.Method
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 50)).Value = Me.Value

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return "ok"
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return msg

        End Try
    End Function

    Public Function FillDetails() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderfrmPrintingException where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function FillDetails_Order_Print_Type() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderfrmPrintingException where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and OrderfrmType='" & Me.OrderfrmType & "' and  PrintType ='" & Me.PrintType & "' and  Method='" & Me.Method & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function FillDetails_Order_Print_Type_value() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderfrmPrintingException where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and OrderfrmType='" & Me.OrderfrmType & "' and  PrintType ='" & Me.PrintType & "' and  Method='" & Me.Method & "' and Value='" & Me.Value & "'"
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

    Public Function Delete_all_old() As Boolean
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "delete from OrderfrmPrintingException where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"

        Dim con As New SqlConnection(constr)
        Dim cmd As New SqlCommand(ssql, con)
        Dim da As New SqlDataAdapter

        Try

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function


    

#End Region



End Class
