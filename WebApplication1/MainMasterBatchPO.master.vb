Imports System.Data.SqlClient

Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core


Partial Class MainMasterBatchPO
    Inherits System.Web.UI.MasterPage
    Public JsCompanyID As String = ""
    Public JSDivisionID As String = ""
    Public JSDepartmentID As String = ""
    Public JSUserName As String = ""

    Dim LastName As String
    Dim CompanyName As String
    Dim Email As String
    Dim Phone As String
    Dim StrSql As String

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Public EmployeeID As String = ""
    Public TerminalID As String = ""
    Public locationid As String = ""

    Public ShiftID As String = ""

    Public LoggOffURL As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        locationid = Session("Locationid")
        TerminalID = Session("TerminalID")

        JsCompanyID = CompanyID
        JSDivisionID = DivisionID
        JSDepartmentID = DepartmentID
        JSUserName = EmployeeID


        populateCompany()

        LoggOffURL = "QFPOSLogin.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & TerminalID & "&locationid=" & locationid & "&logoff=1"

        Try
            ShiftID = "Shift ID: " & Session("ShiftID")
        Catch ex As Exception

        End Try



    End Sub




    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim ConnectionString As String = ""
    Dim reader As SqlDataReader

    Sub populateCompany()


        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim CommandText As String = "enterprise.spCompanyInformation"

        ' get the connection ready
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID


        ' open the connection
        myConnection.Open()
        reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While reader.Read()
            lblCompany.Text = reader(3).ToString()
            lblCompany.DataBind()
        End While
    End Sub

End Class

