Imports System.Data
Imports System.Data.SqlClient
Imports DAL


Partial Class StandingROSchedular
    Inherits System.Web.UI.Page

    Public CompanyID As String = "QuickfloraDemo"
    Public DivisionID As String = "DEFAULT"
    Public DepartmentID As String = "DEFAULT"
    Public EmployeeID As String = "DEFAULT"

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Private Sub StandingROSchedular_Load(sender As Object, e As EventArgs) Handles Me.Load



        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))

        Dim myCommand As New SqlCommand("Standing_BatchPO_Schedular", ConString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        myCommand.Connection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()

    End Sub
End Class
