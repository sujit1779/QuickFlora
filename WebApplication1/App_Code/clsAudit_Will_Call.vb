Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic
Imports System
Imports System.Net
Imports System.Net.Mail

Public Class clsAudit_Will_Call


    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String



    'INSERT INTO [Enterprise].[dbo].[Audit_Will_Call]
    '([CompanyID]
    ',[DivisionID]
    ',[DepartmentID]
    ',[OrderNumber]
    ',[PaymentMethodID]
    ',[Update_Date])
    'VALUES
    '(<CompanyID, nvarchar(36),>
    ',<DivisionID, nvarchar(36),>
    ',<DepartmentID, nvarchar(36),>
    ',<OrderNumber, nvarchar(36),>
    ',<PaymentMethodID, nvarchar(36),>
    ',<Update_Date, nchar(10),>)

    Public OrderNumber As String
    Public PaymentMethodID As String


    Public Function Insert() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into Audit_Will_Call( CompanyID, DivisionID, DepartmentID, OrderNumber,PaymentMethodID) " _
             & " values(@f0,@f1,@f2,@f3,@f4)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.PaymentMethodID

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




    Public Function FillDetailsAudit_Will_Call() As DataTable
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from Audit_Will_Call where OrderNumber='" & Me.OrderNumber & "' and  CompanyID='" & Me.CompanyID & "' and  DivisionID ='" & Me.DivisionID & "' and  DepartmentID='" & Me.DepartmentID & "' Order BY Linenumber Desc"
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function


    Public Function UpdateOrderHeader() As Boolean
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update OrderHeader set  Orderdate=@f4 " _
        & " where  CompanyID=@f0 and  DivisionID =@f1 and  DepartmentID=@f2 and Ordernumber=@f3"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.DateTime)).Value = Date.Now


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
