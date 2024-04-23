Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsOrderEntryForm

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public OrderNumber As String
    Public AmountPaid As Double
    Public BalanceDue As Double

    Public Function UpdateOrderAmountPaid() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set AmountPaid=@f4,BalanceDue=@f41  Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.Money)).Value = Me.AmountPaid
            com.Parameters.Add(New SqlParameter("@f41", SqlDbType.Money)).Value = Me.BalanceDue
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber

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
