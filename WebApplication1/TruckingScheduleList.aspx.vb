Option Strict Off

Imports EnterpriseASPClient.Core
'Imports EnterpriseClient.Core
Imports System.Data
Imports System.Data.SqlClient

Partial Class TruckingScheduleList
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""

    Dim SortDirection As String

    Dim obj As New clsTruckingSchedule




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CType(SessionKey("CompanyID"), String)
        DivisionID = CType(SessionKey("DivisionID"), String)
        DepartmentID = CType(SessionKey("DepartmentID"), String)

        Dim dt As New DataTable

        If Not Page.IsPostBack Then

            dt = obj.GetTruckingSchedules(CompanyID, DivisionID, DepartmentID)

            If dt.Rows.Count > 0 Then

                Dim n As Integer = 0
                For n = 0 To dt.Rows.Count - 1
                    Dim lstOrigin As New ListItem
                    Dim lstLocation As New ListItem
                    Dim lstShipMethod As New ListItem

                    lstOrigin.Text = dt.Rows(n)("OriginName")
                    lstOrigin.Value = dt.Rows(n)("OriginName")
                    If drpOrigin.Items.IndexOf(lstOrigin) = -1 Then
                        drpOrigin.Items.Add(lstOrigin)
                    End If

                    lstLocation.Text = dt.Rows(n)("LocationID")
                    lstLocation.Value = dt.Rows(n)("LocationID")
                    If cmblocationid.Items.IndexOf(lstLocation) = -1 Then
                        cmblocationid.Items.Add(lstLocation)
                    End If

                    lstShipMethod.Text = dt.Rows(n)("ShipMethodDescription")
                    lstShipMethod.Value = dt.Rows(n)("ShipMethodDescription")
                    If drpShipMethod.Items.IndexOf(lstShipMethod) = -1 Then
                        drpShipMethod.Items.Add(lstShipMethod)
                    End If

                Next

                TruckingScheduleGrid.DataSource = dt
                TruckingScheduleGrid.DataBind()
            End If
        End If
    End Sub



    Private Sub drpOrigin_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpOrigin.SelectedIndexChanged, drpShipMethod.SelectedIndexChanged, cmblocationid.SelectedIndexChanged
        Dim dt As New DataTable
        dt = obj.GetTruckingSchedules(CompanyID, DivisionID, DepartmentID)
        If dt.Rows.Count > 0 Then
            TruckingScheduleGrid.DataSource = dt
            TruckingScheduleGrid.DataBind()
        End If

    End Sub

    Private Sub TruckingScheduleGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles TruckingScheduleGrid.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lblLocationID As New Label
            Dim lblShipMethodDescription As New Label
            Dim lblOrigin As New Label

            lblLocationID = e.Row.FindControl("lblLocationID")
            lblShipMethodDescription = e.Row.FindControl("lblShipMethodDescription")
            lblOrigin = e.Row.FindControl("lblOrigin")

            If drpOrigin.SelectedValue <> "" Then
                If lblOrigin.Text <> drpOrigin.SelectedValue Then
                    e.Row.Visible = False
                End If
            End If

            If cmblocationid.SelectedValue <> "" Then
                If lblLocationID.Text <> cmblocationid.SelectedValue Then
                    e.Row.Visible = False
                End If
            End If

            If drpShipMethod.SelectedValue <> "" Then
                If lblShipMethodDescription.Text <> drpShipMethod.SelectedValue Then
                    e.Row.Visible = False
                End If
            End If

        End If


    End Sub



    Protected Sub TruckingScheduleGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles TruckingScheduleGrid.PageIndexChanging

        TruckingScheduleGrid.PageIndex = e.NewPageIndex

        Dim dt As New DataTable

        dt = obj.GetTruckingSchedules(CompanyID, DivisionID, DepartmentID)

        If dt.Rows.Count > 0 Then
            TruckingScheduleGrid.DataSource = dt
            TruckingScheduleGrid.DataBind()
        End If

    End Sub




    Protected Sub TruckingScheduleGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles TruckingScheduleGrid.RowDeleting

        Dim ScheduleID As String = TruckingScheduleGrid.DataKeys(e.RowIndex).Values(0).ToString()    '    
        obj.DeleteTruckingSchedule(CompanyID, DivisionID, DepartmentID, ScheduleID)
        Response.Redirect("TruckingScheduleList.aspx")

    End Sub

    Protected Sub TruckingScheduleGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles TruckingScheduleGrid.RowEditing

        Dim ScheduleID As String = TruckingScheduleGrid.DataKeys(e.NewEditIndex).Values(0).ToString()

        If ScheduleID <> "" Then
            Response.Redirect(String.Format("TruckingScheduleDetail.aspx?ScheduleID={0}", ScheduleID))
        End If

    End Sub

    Protected Sub TruckingScheduleGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles TruckingScheduleGrid.Sorting

        Dim dt As New DataTable

        dt = obj.GetTruckingSchedules(CompanyID, DivisionID, DepartmentID)

        Dim dv As DataView = dt.DefaultView

        If hdSortDirection.Value = "" Or hdSortDirection.Value = "DESC" Then
            hdSortDirection.Value = "ASC"
        Else
            hdSortDirection.Value = "DESC"
        End If

        dv.Sort = e.SortExpression & " " & hdSortDirection.Value

        If dt.Rows.Count > 0 Then
            TruckingScheduleGrid.DataSource = dv
            TruckingScheduleGrid.DataBind()
        End If

    End Sub



    'Protected Sub btncustsearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btncustsearch.Click
    '    Dim TempCustomerID As String = txtcustomersearch.Text
    '    lblsearchcustomermsg.ForeColor = Drawing.Color.Black

    '    If txtcustomersearch.Text.Trim = "" Then
    '        Exit Sub

    '    End If

    '    If TempCustomerID.IndexOf("[") > -1 Then
    '        Dim st, ed As Integer
    '        st = TempCustomerID.IndexOf("[")
    '        ed = TempCustomerID.IndexOf("]")

    '        If st = -1 Then
    '            Exit Sub
    '        End If

    '        If (ed - st) - 1 = -1 Then
    '            Exit Sub
    '        End If

    '        TempCustomerID = TempCustomerID.Substring(st + 1, (ed - st) - 1)
    '    End If


    '    txtcustomersearch.Text = TempCustomerID

    '    'BindOrderHeaderList()


    '    Dim dt As New DataTable

    '    dt = obj.GetInventoryTransferList(CompanyID, DivisionID, DepartmentID, TempCustomerID)

    '    If dt.Rows.Count > 0 Then
    '        InventoryTransferGrid.DataSource = dt
    '        InventoryTransferGrid.DataBind()
    '    End If

    '    txtcustomersearch.Text = "" '"dt.Rows.Count=" & dt.Rows.Count

    'End Sub


    'Protected Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
    '    Dim dt As New DataTable


    '    If rdAllDates.Checked Then
    '        dt = GetInventoryTransferList(CompanyID, DivisionID, DepartmentID)

    '    ElseIf rdOrderDates.Checked Then
    '        dt = GetInventoryTransferListbyTransferDate(CompanyID, DivisionID, DepartmentID)

    '    ElseIf rdDeliveryDates.Checked Then

    '        dt = GetInventoryTransferListbyReceiveDate(CompanyID, DivisionID, DepartmentID)

    '    End If



    '    InventoryTransferGrid.DataSource = dt
    '    InventoryTransferGrid.DataBind()


    'End Sub


   






End Class
