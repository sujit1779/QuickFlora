Imports System.Data
Imports System.Data.SqlClient

Partial Class ItemListByPrice
    Inherits System.Web.UI.Page

    Private obj As New clsItems

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))
        EmployeeID = CStr(SessionKey("EmployeeID"))

        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        fillHomePageManagement()
        If Not Page.IsPostBack Then
            ' GetInventoryItemsList()
        End If

    End Sub


    Public LeftMenuFooterText As String = ""

    Public Function fillHomePageManagement() As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT ConsultationRequestTitle,ConsultationRequestText,LeftMenuFooterText,MostPopular,SubscribeEmail,MenuFamilyGroupName,HomeLink1URL,HomeLink2URL,[HomeLink1],[HomeLink2] ,NewWebsitePhoneText ,NewWebsitePhoneNumber, FooterMessageHead ,FooterMessage , HomeSingleItemID,txtConentHeading,txtConentMessage,txtConentLinkName,txtConentLinkURL  FROM HomePageManagement where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            da.SelectCommand = com
            da.Fill(dt)


            If dt.Rows.Count > 0 Then
                Try
                    LeftMenuFooterText = dt.Rows(0)("LeftMenuFooterText")
                Catch ex As Exception

                End Try



            End If

            Return ""
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return ""
        End Try

    End Function



    Private Sub GetInventoryItemsList()

        Dim ds As New DataSet
        ds = obj.GetInventoryItemsList()

        If ds.Tables(0).Rows.Count > 0 Then
            gvItemsList.DataSource = ds
            gvItemsList.DataBind()
            lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
        Else
            lblInfo.Text = "0 records found"
        End If

    End Sub

    Protected Sub gvItemsList_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvItemsList.PageIndexChanging

        gvItemsList.PageIndex = e.NewPageIndex
        GetInventoryItemsList()


    End Sub

    Protected Sub gvItemsList_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles gvItemsList.Sorting

        Dim ds As New DataSet

        ds = obj.GetInventoryItemsList()

        Dim dv As DataView = ds.Tables(0).DefaultView

        If gvSortDirection.Value = "" Or gvSortDirection.Value = "DESC" Then
            gvSortDirection.Value = "ASC"
        Else
            gvSortDirection.Value = "DESC"
        End If

        dv.Sort = e.SortExpression & " " & gvSortDirection.Value

        If ds.Tables(0).Rows.Count > 0 Then
            gvItemsList.DataSource = dv
            gvItemsList.DataBind()
        End If

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim ds As New DataSet
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT * FROM [InventoryItems] where  "
        If drpSearchCondition.SelectedValue = "Like" Then
            ssql = ssql & "   " & drpSearchFor.SelectedValue & " LIKE '%" & txtSearchValue.Text.Trim & "%'"
        End If
        If drpSearchCondition.SelectedValue = "=" Then
            ssql = ssql & "   " & drpSearchFor.SelectedValue & " = '" & txtSearchValue.Text.Trim & "'"
        End If

        ssql = ssql & " and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by ItemID ASC "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        'com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar, 36)).Value = txtSearchValue.Text.Trim

        da.SelectCommand = com
        da.Fill(dt)
        'If drpSearchFor.SelectedValue = "ItemID" Then
        '    Session("1") = txtSearchValue.Text.Trim
        '    ' Session("1") = ""
        '    Session("2") = ""
        '    Session("3") = ""
        '    Session("4") = ""
        '    Session("5") = ""

        '    ds = obj.GetInventoryItemsList(txtSearchValue.Text.Trim, "", "", "", "")
        'ElseIf drpSearchFor.SelectedValue = "ItemName" Then
        '    Session("2") = txtSearchValue.Text.Trim
        '    Session("1") = ""
        '    'Session("2") = ""
        '    Session("3") = ""
        '    Session("4") = ""
        '    Session("5") = ""
        '    ds = obj.GetInventoryItemsList("", txtSearchValue.Text.Trim, "", "", "")
        'ElseIf drpSearchFor.SelectedValue = "ItemType" Then
        '    Session("3") = txtSearchValue.Text.Trim
        '    Session("1") = ""
        '    Session("2") = ""
        '    'Session("3") = ""
        '    Session("4") = ""
        '    Session("5") = ""
        '    ds = obj.GetInventoryItemsList("", "", txtSearchValue.Text.Trim, "", "")
        'ElseIf drpSearchFor.SelectedValue = "Location" Then
        '    Session("4") = txtSearchValue.Text.Trim
        '    Session("1") = ""
        '    Session("2") = ""
        '    Session("3") = ""
        '    'Session("4") = ""
        '    Session("5") = ""
        '    ds = obj.GetInventoryItemsList("", "", "", txtSearchValue.Text.Trim, "")
        'ElseIf drpSearchFor.SelectedValue = "VendorID" Then
        '    Session("5") = txtSearchValue.Text.Trim
        '    Session("1") = ""
        '    Session("2") = ""
        '    Session("3") = ""
        '    Session("4") = ""
        '    'Session("5") = ""
        '    ds = obj.GetInventoryItemsList("", "", "", "", txtSearchValue.Text.Trim)
        'End If

        gvItemsList.DataSource = dt
        gvItemsList.DataBind()

        If dt.Rows.Count > 0 Then
            lblInfo.Text = dt.Rows.Count.ToString + " records found"
        Else
            lblInfo.Text = "0 records found"
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Response.Redirect("ItemListByPrice.aspx")
    End Sub
End Class
