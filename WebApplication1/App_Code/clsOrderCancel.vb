Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsOrderCancel
    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")


    Public CompanyID As String
    Public DivisionID As String
    Public DepartmentID As String
    Public OrderNumber As String
    Public EmployeeID As String
    Public EmployeeName As String
    Public Reason As String


    '[dbo].[OrdernumberCancelLogs](
    '[CompanyID] [nvarchar](36) NOT NULL,
    '[DivisionID] [nvarchar](36) NOT NULL,
    '[DepartmentID] [nvarchar](36) NOT NULL,
    '[ordernumber] [nvarchar](36) NULL,
    '[Linenumber] [bigint] NULL,
    '[EmployeeID] [nvarchar](36) NULL,
    '[EmployeeName] [nvarchar](255) NULL,
    '[Reason] [text] NULL,
    '[Datetime] [datetime] NULL,


    Public Function OrdernumberCancelLogs() As Boolean

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "INSERT INTO  OrdernumberCancelLogs (CompanyID,DivisionID,DepartmentID,OrderNumber,EmployeeID,EmployeeName,Reason,Datetime) values(@f1,@f2,@f3,@f4,@f5,@f6,@f7,@f8)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.OrderNumber
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = Me.EmployeeID
            com.Parameters.Add(New SqlParameter("@f6", SqlDbType.NVarChar, 255)).Value = Me.EmployeeName
            com.Parameters.Add(New SqlParameter("@f7", SqlDbType.Text)).Value = Me.Reason
            com.Parameters.Add(New SqlParameter("@f8", SqlDbType.DateTime)).Value = Date.Now

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
