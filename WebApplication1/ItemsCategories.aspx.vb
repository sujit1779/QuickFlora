Option Strict Off

Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Drawing
Imports System.IO
Imports System

Partial Class ItemsCategories
    Inherits System.Web.UI.Page

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")

        If Not Page.IsPostBack Then
            PopulateContent()

        End If
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PopulateContent()
    End Sub

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Public Function FillDetailsCategories() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [InventoryCategories] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        If txtSearchValue.Text.Trim <> "" Then
            ssql = ssql & " and " & drpSearchFor.SelectedValue & " like " & "'%" & txtSearchValue.Text & "%'"
        End If


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            Web.HttpContext.Current.Response.Write(msg)
            Return dt

        End Try


    End Function



    Sub PopulateContent()
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        ''EmployeeID = Session("EmployeeID")
        Dim ds As DataTable

        ds = FillDetailsCategories()
        grdContentList.DataSource = ds
        grdContentList.DataBind()


    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAdd.Click
        Dim ContentID As String = "Add"
        Response.Redirect("InventoryCategoriesDetails.aspx?Add=" & ContentID)

    End Sub

    Protected Sub grdContentList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdContentList.RowDeleting
        'Dim ContentID As Integer = Convert.ToInt32(grdContentList.DataKeys(e.RowIndex).Value)
        Dim ItemCategoryID As String = grdContentList.DataKeys(e.RowIndex).Values(0)
        Dim ItemFamilyID As String = grdContentList.DataKeys(e.RowIndex).Values(1)

        DeleteDetailsfamilybyid(ItemCategoryID, ItemFamilyID)
        PopulateContent()
    End Sub

    Public Function DeleteDetailsfamilybyid(ByVal ItemCategoryID As String, ByVal ItemFamilyID As String) As Boolean
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "delete from [InventoryCategories] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  and ItemCategoryID=@ItemCategoryID and ItemFamilyID=@ItemFamilyID"

        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@ItemFamilyID", SqlDbType.NVarChar, 36)).Value = ItemFamilyID
            com.Parameters.Add(New SqlParameter("@ItemCategoryID", SqlDbType.NVarChar, 36)).Value = ItemCategoryID

            com.CommandText = ssql
            com.CommandType = CommandType.Text
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try

        Return True
    End Function


    Protected Sub grdContentList_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdContentList.PageIndexChanging
        grdContentList.PageIndex = e.NewPageIndex
        PopulateContent()

    End Sub

    Protected Sub grdContentList_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdContentList.RowEditing


        Dim ItemCategoryID As String = grdContentList.DataKeys(e.NewEditIndex).Values(0)
        Dim ItemFamilyID As String = grdContentList.DataKeys(e.NewEditIndex).Values(1)

        Response.Redirect("InventoryCategoriesDetails.aspx?ItemFamilyID=" & ItemFamilyID & "&ItemCategoryID=" & ItemCategoryID)

    End Sub

    Function getDisplay(ByVal ob As String)

        Dim retvalue As String

        If ob.ToString() = "True" Then

            retvalue = "yes"

        Else
            retvalue = "No"

        End If

        Return retvalue


    End Function

End Class
