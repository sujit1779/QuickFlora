Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class BPO
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Public EmployeeID As String = ""
    Public TerminalID As String = ""
    Public locationid As String = ""

    Public POURL As String = ""
    Public POURL2 As String = ""



    Dim rs As SqlDataReader


    Public Function PopulateEmployees(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal MO As String) As SqlDataReader

        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()
        Dim myCommand As New SqlCommand("PopulateEmployeesByAccess", ConString)
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

        Dim pModule As New SqlParameter("@Module", Data.SqlDbType.NVarChar, 36)
        pModule.Value = MO
        myCommand.Parameters.Add(pModule)

        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs



    End Function


    Private Sub BPO_Load(sender As Object, e As EventArgs) Handles Me.Load


        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "PurchaseRequest")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=PurchaseRequest")
        End If


        locationid = Session("Locationid")
        TerminalID = Session("TerminalID")

        POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
        POURL2 = "https://secureapps.quickflora.com/POM/Web/BPOR.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID

        If Me.CompanyID.ToLower() = "DierbergsMarkets,Inc63017".ToLower() Or Me.CompanyID = "QGFloralLandscape11357" Then

            POURL = "http://bpom2.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
            POURL2 = "http://bpom2.quickflora.com/Web/BPOR.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID

        End If

        If Me.CompanyID = "QuickfloraDemo" Then
            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
        End If

        If Me.CompanyID.ToLower() = "mccarthyg".ToLower() Then
            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
        End If

        Dim page As String = 0
        Try
            page = Request.QueryString("page")
            '  HttpContext.Current.Response.Write(endtdate)
        Catch ex As Exception

        End Try

        If page = "1" Then
            Response.Redirect(POURL)
        End If

        If page = "2" Then
            Response.Redirect(POURL2)
        End If


    End Sub
End Class
