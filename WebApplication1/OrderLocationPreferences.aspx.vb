Imports System.Data
Imports System.Data.SqlClient

Partial Class OrderLocationPreferences
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        If Not IsPostBack Then
            fillgrid1s()
        End If

    End Sub
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""

    Public Sub fillgrid1s()
       

        Dim obj As New clsOrder_Location
        Dim dt As New DataTable
        dt.Clear()
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        dt = obj.FillLocation()
        Session("dt") = dt
        GridView1.DataSource = dt
        GridView1.DataBind()
        ' lblmsgOrderLocationDelete.Text = ""

    End Sub



    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        fillgrid1s()
    End Sub


    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Dim index As Integer = e.CommandArgument
        Dim id As String = GridView1.DataKeys(e.RowIndex).Value
        Dim obj As New clsOrder_Location
         
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        obj.LocationID = id

        If obj.DeleteOrder_Location() Then

            fillgrid1s()
            lblmsgOrderLocationDelete.Text = "Record is deleted"


        Else

            lblmsgOrderLocationDelete.Text = "Unable to delete the record"

        End If

    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        'Dim index As Integer = e.CommandArgument
        Dim id As String = GridView1.DataKeys(e.NewEditIndex).Value
        Response.Redirect("OrderLocationPreferencesDetails.aspx?ID=" & id)

    End Sub



End Class
