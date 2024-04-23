Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsOrderMailSendForTrace
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String

 
    Public OrderNumber As String

    Public Mailcontent As String

    Public Card As String = ""
    Public ExpirationDate As String = ""
    Public CSV As String = ""


    Public Function Insert_OrderMailSendForTrace() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  OrderMailSendForTrace (CompanyID,DivisionID,DepartmentID,OrderNumber,Mailcontent) values(@f1,@f2,@f3,@f4,@f5)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NText)).Value = Me.Mailcontent 

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

    Public Function Details_OrderMailSendForTraceTemplates() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from OrderMailSendForTraceTemplates"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try



            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function


    Public Function UpdateOrderMailSendForTraceTemplates() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderMailSendForTraceTemplates set MailSubjectTemplate=@f1"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NText)).Value = Me.Mailcontent


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
