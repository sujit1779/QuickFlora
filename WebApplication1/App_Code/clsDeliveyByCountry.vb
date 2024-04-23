Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient


Public Class clsDeliveyByCountry

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public CountryName As String
    Public CountryID As String

    Public ZIPCODE As String

    'SELECT     CompanyID, DivisionID, DepartmentID, CountryName, CountryID
    'FROM         DeliveryByCountry


    Public Function FillCountry() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from Country "
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function FillDeliveryByZipCode() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from DeliveryByZip where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' and Zip='" & Me.ZIPCODE & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function FillDefaultDeliveryCharge() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from DeliveryCharge where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function FillDeliveryByCountryID() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from DeliveryByCountry where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' AND CountryID='" & Me.CountryID & "'"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function FillDeliveryByCountry() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from DeliveryByCountry where  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "'  Order By CountryName"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function DeleteDeliveryByCountry() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Delete from DeliveryByCountry  where CompanyID=@f0 AND  DivisionID=@f1 AND DepartmentID=@f2"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
             
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

    Public Function DeleteDeliveryByCountryBYId() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Delete from DeliveryByCountry  where CompanyID=@f0 AND  DivisionID=@f1 AND DepartmentID=@f2 AND CountryName=@f3 AND CountryID=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 50)).Value = Me.CountryName
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 2)).Value = Me.CountryID

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

    Public Function Insert_DeliveryByCountry() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into DeliveryByCountry( CompanyID, DivisionID, DepartmentID, CountryName, CountryID) " _
             & " values(@f0,@f1,@f2,@f3,@f4)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 50)).Value = Me.CountryName
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 2)).Value = Me.CountryID

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

End Class
