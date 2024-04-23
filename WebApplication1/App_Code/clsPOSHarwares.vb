Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsPOSHarwares

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

    'INSERT INTO [Enterprise].[dbo].[POSHardware]
    '       ([CompanyID]
    '       ,[DivisionID]
    '       ,[DepartmentID]
    '       ,[CardReader])

    Public CardReader As String


    Public Function FillDetails() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from POSHardware where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Public Function Insert() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into POSHardware( CompanyID, DivisionID, DepartmentID, CardReader) " _
             & " values(@f0,@f1,@f2,@f3)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 100)).Value = Me.CardReader

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

    Public Function Update() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update POSHardware set  CardReader=@f3 " _
        & " where  CompanyID=@f0 and  DivisionID =@f1 and  DepartmentID=@f2"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 100)).Value = Me.CardReader

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


End Class
