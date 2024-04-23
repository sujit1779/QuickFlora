Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsMacAccessPermissions
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public RequestMessage As String
    Public MACAddress As String
    Public TerminalName As String
    Public Active As Boolean

    'INSERT INTO [Enterprise].[dbo].[MacAccessPermissions]
    '       (<CompanyID, nvarchar(36),>
    '       ,<DivisionID, nvarchar(36),>
    '       ,<DepartmentID, nvarchar(36),>
    '       ,<MACAddress, nvarchar(50),>
    '       ,<Active, bit,>)

    Public Function CheckMACAddress() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from  MacAccessPermissions where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and MACAddress=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 50)).Value = Me.MACAddress

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
        Return dt
    End Function



    Public Function GetMACAddress() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from  MacAccessPermissions where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
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
        Return dt
    End Function

    Public Function InsertMACAddress() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  [MacAccessPermissions] ([CompanyID],[DivisionID],[DepartmentID],[MACAddress],RequestMessage, Active ,TerminalName " _
        & " ) values(@f1,@f2,@f3,@f4,@f5,0,@f6)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 50)).Value = Me.MACAddress
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 500)).Value = Me.RequestMessage
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 500)).Value = Me.TerminalName

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


    Public Function UpdateMacAccessPermissions(ByVal Inlinenumber As String) As String

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "Update MacAccessPermissions set Active=@Active,TerminalName=@TerminalName   where Inlinenumber=@Inlinenumber"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@Inlinenumber", SqlDbType.NVarChar, 36)).Value = Inlinenumber
            com.Parameters.Add(New SqlParameter("@Active", SqlDbType.Bit)).Value = Me.Active
            com.Parameters.Add(New SqlParameter("@TerminalName", SqlDbType.NVarChar, 500)).Value = Me.TerminalName

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return "0"
        End Try
        Return "0"
    End Function



    Public Function GetCompaniesMacAccessPermissions() As DataTable

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from  Companies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
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
        Return dt
    End Function

    'HomepageContentID
    Public Function UpdateCompaniesMacAccessPermissions(ByVal MacAccessPermissions As Boolean) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "Update Companies Set MacAccessPermissions=@f3  where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.Bit)).Value = MacAccessPermissions

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function
    'HomepageContentID


End Class
