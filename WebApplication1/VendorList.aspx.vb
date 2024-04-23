Option Strict Off

Imports EnterpriseASPClient.Core
'Imports EnterpriseClient.Core
Imports System.Data
Imports System.Data.SqlClient

Partial Class VendorList
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Dim SortDirection As String

    Dim obj As New clsInventoryTransfer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CType(SessionKey("CompanyID"), String)
        DivisionID = CType(SessionKey("DivisionID"), String)
        DepartmentID = CType(SessionKey("DepartmentID"), String)



        txtcustomersearch.Attributes.Add("placeholder", "SEARCH")
        txtcustomersearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")

        Dim dt As New DataTable

        If Not Page.IsPostBack Then

            dt = GetVendorList(CompanyID, DivisionID, DepartmentID)

            If dt.Rows.Count > 0 Then
                InventoryTransferGrid.DataSource = dt
                InventoryTransferGrid.DataBind()
            End If

        End If

    End Sub

    Protected Sub InventoryTransferGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles InventoryTransferGrid.PageIndexChanging

        InventoryTransferGrid.PageIndex = e.NewPageIndex

        Dim dt As New DataTable

        dt = GetVendorList(CompanyID, DivisionID, DepartmentID)

        If dt.Rows.Count > 0 Then
            InventoryTransferGrid.DataSource = dt
            InventoryTransferGrid.DataBind()
        End If

    End Sub



    Protected Sub InventoryTransferGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles InventoryTransferGrid.RowEditing

        Dim VendorID As String = InventoryTransferGrid.DataKeys(e.NewEditIndex).Values(0).ToString()

        If VendorID <> "" Then
            Response.Redirect(String.Format("VendorDetails.aspx?VendorID={0}", VendorID))
        End If

    End Sub

    Protected Sub InventoryTransferGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles InventoryTransferGrid.Sorting

        Dim dt As New DataTable

        dt = GetVendorList(CompanyID, DivisionID, DepartmentID)

        Dim dv As DataView = dt.DefaultView

        If hdSortDirection.Value = "" Or hdSortDirection.Value = "DESC" Then
            hdSortDirection.Value = "ASC"
        Else
            hdSortDirection.Value = "DESC"
        End If

        dv.Sort = e.SortExpression & " " & hdSortDirection.Value

        If dt.Rows.Count > 0 Then
            InventoryTransferGrid.DataSource = dv
            InventoryTransferGrid.DataBind()
        End If

    End Sub



    Protected Sub btncustsearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btncustsearch.Click
        Dim TempCustomerID As String = txtcustomersearch.Text
        lblsearchcustomermsg.ForeColor = Drawing.Color.Black

        If txtcustomersearch.Text.Trim = "" Then
            Exit Sub

        End If

        If TempCustomerID.IndexOf("[") > -1 Then
            Dim st, ed As Integer
            st = TempCustomerID.IndexOf("[")
            ed = TempCustomerID.IndexOf("]")

            If st = -1 Then
                Exit Sub
            End If

            If (ed - st) - 1 = -1 Then
                Exit Sub
            End If

            TempCustomerID = TempCustomerID.Substring(st + 1, (ed - st) - 1)
        End If


        txtcustomersearch.Text = TempCustomerID

        'BindOrderHeaderList()

        Dim dt As New DataTable

        dt = GetVendorList(CompanyID, DivisionID, DepartmentID, TempCustomerID)

        If dt.Rows.Count > 0 Then
            InventoryTransferGrid.DataSource = dt
            InventoryTransferGrid.DataBind()
        End If


        ' txtcustomersearch.Text = ""

    End Sub



    Public Function GetVendorList(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
                                            Optional ByVal VendorID As String = "") As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetVendorInformationList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("VendorID", VendorID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function

End Class

