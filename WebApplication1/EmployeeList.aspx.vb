Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration


Partial Class EmployeeList
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    'Dim IsAdmin As Boolean = False
    Dim objEmployee As New clsEmployee

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("CompanyID") Is Nothing Then
            Response.Redirect("loginform.aspx")
        End If
        'Response.Redirect("Home.aspx")
        'Home

        'Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        '' get the connection ready
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        If Not Page.IsPostBack Then
            GetEmployeeList()
        End If

    End Sub

    Private Function CheckIsEmployeeAdmin() As Boolean

        Return objEmployee.CheckIsEmployeeAdmin(CompanyID, DivisionID, DepartmentID, EmployeeID)

    End Function

    Private Sub GetEmployeeList()

        Dim dt As New DataTable

        If CheckIsEmployeeAdmin() Then
            If EmployeeID = "Admin" Then
                dt = objEmployee.GetEmployeeList(CompanyID, DivisionID, DepartmentID, EmployeeID)
            Else
                dt = objEmployee.GetEmployeeList(CompanyID, DivisionID, DepartmentID, "")
            End If

        Else
            dt = objEmployee.GetEmployeeList(CompanyID, DivisionID, DepartmentID, EmployeeID)
        End If

        If dt.Rows.Count > 0 Then

            EmployeeGrid.DataSource = dt
            EmployeeGrid.DataBind()

            If Not CheckIsEmployeeAdmin() Then
                EmployeeGrid.Columns(0).Visible = False
                EmployeeGrid.Columns(1).Visible = False
            End If

        End If

    End Sub

    Protected Sub EmployeeGrid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles EmployeeGrid.PageIndexChanging

        EmployeeGrid.PageIndex = e.NewPageIndex
        GetEmployeeList()

    End Sub

    Protected Sub EmployeeGrid_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles EmployeeGrid.RowDeleting

        Dim EmpID As String = EmployeeGrid.DataKeys(e.RowIndex).Values(0).ToString()
        If objEmployee.DeleteEmployeeDetail(CompanyID, DivisionID, DepartmentID, EmpID) Then
            Response.Redirect("EmployeeList.aspx")
        End If

    End Sub

    Protected Sub EmployeeGrid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles EmployeeGrid.Sorting

        Dim dt As New DataTable

        If CheckIsEmployeeAdmin() Then
            dt = objEmployee.GetEmployeeList(CompanyID, DivisionID, DepartmentID, "")
            'Else
            '    dt = GetEmployeeList(CompanyID, DivisionID, DepartmentID, EmployeeID)
        End If

        Dim dv As DataView = dt.DefaultView

        If gvSortDirection.Value = "" Or gvSortDirection.Value = "DESC" Then
            gvSortDirection.Value = "ASC"
        Else
            gvSortDirection.Value = "DESC"
        End If

        dv.Sort = e.SortExpression & " " & gvSortDirection.Value

        If dt.Rows.Count > 0 Then
            EmployeeGrid.DataSource = dv
            EmployeeGrid.DataBind()
        End If

    End Sub

#Region "Class Functions"

    ''Public Function GetEmployeeList(CompanyID As String, DivisionID As String, DepartmentID As String, Optional EmployeeID As String = "") As DataTable

    ''    Dim dt As New DataTable

    ''    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    ''        Using command As New SqlCommand("[enterprise].[GetEmployeeList]", connection)
    ''            command.CommandType = CommandType.StoredProcedure

    ''            command.Parameters.AddWithValue("CompanyID", CompanyID)
    ''            command.Parameters.AddWithValue("DivisionID", DivisionID)
    ''            command.Parameters.AddWithValue("DepartmentID", DepartmentID)

    ''            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    ''            Try
    ''                Dim da As New SqlDataAdapter(command)
    ''                da.Fill(dt)
    ''            Catch ex As Exception

    ''            End Try

    ''        End Using
    ''    End Using

    ''    Return dt

    ''End Function

    ''Public Function CheckIsEmployeeAdmin(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String) As Boolean

    ''    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    ''        Using command As New SqlCommand("[enterprise].[CheckIsEmployeeAdmin]", connection)
    ''            command.CommandType = CommandType.StoredProcedure

    ''            command.Parameters.AddWithValue("CompanyID", CompanyID)
    ''            command.Parameters.AddWithValue("DivisionID", DivisionID)
    ''            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    ''            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    ''            Try
    ''                command.Connection.Open()
    ''                Return Convert.ToBoolean(command.ExecuteScalar)
    ''            Catch ex As Exception
    ''                Return False
    ''            Finally
    ''                command.Connection.Close()
    ''            End Try

    ''        End Using
    ''    End Using

    ''End Function

    ''Public Function CheckIsMasterEmployee(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String) As Boolean

    ''    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    ''        Using command As New SqlCommand("[enterprise].[CheckIsMasterEmployee]", connection)
    ''            command.CommandType = CommandType.StoredProcedure

    ''            command.Parameters.AddWithValue("CompanyID", CompanyID)
    ''            command.Parameters.AddWithValue("DivisionID", DivisionID)
    ''            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    ''            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    ''            Try
    ''                command.Connection.Open()
    ''                Return Convert.ToBoolean(command.ExecuteScalar)
    ''            Catch ex As Exception
    ''                Return False
    ''            Finally
    ''                command.Connection.Close()
    ''            End Try

    ''        End Using
    ''    End Using

    ''End Function

    ''Public Function DeleteEmployeeDetail(CompanyID As String, DivisionID As String, DepartmentID As String, EmployeeID As String) As Boolean

    ''    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    ''        Using command As New SqlCommand("[enterprise].[DeleteEmployeeDetail]", connection)
    ''            command.CommandType = CommandType.StoredProcedure

    ''            command.Parameters.AddWithValue("CompanyID", CompanyID)
    ''            command.Parameters.AddWithValue("DivisionID", DivisionID)
    ''            command.Parameters.AddWithValue("DepartmentID", DepartmentID)
    ''            command.Parameters.AddWithValue("EmployeeID", EmployeeID)

    ''            Try
    ''                command.Connection.Open()
    ''                command.ExecuteNonQuery()
    ''                Return True
    ''            Catch ex As Exception
    ''                Return False
    ''            Finally
    ''                command.Connection.Close()
    ''            End Try

    ''        End Using
    ''    End Using

    ''End Function

#End Region

End Class
