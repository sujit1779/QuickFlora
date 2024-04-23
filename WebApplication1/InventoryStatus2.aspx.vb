Option Strict Off

Imports EnterpriseASPClient.Core
'Imports EnterpriseClient.Core
Imports System.Data
Imports System.Data.SqlClient

Partial Class InventoryStatus2
    Inherits System.Web.UI.Page


    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Dim SortDirection As String

    'Dim obj As New clsInventoryTransfer

    Public Function FillLocationLocationName(ByVal LocationID As String) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT LocationName FROM [Order_Location] where  LocationID = @LocationID and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by DisplayOrder ASC"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@LocationID", SqlDbType.NVarChar, 36)).Value = LocationID
            da.SelectCommand = com
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            Else
                Return LocationID
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return LocationID
        End Try

    End Function


    Public Sub SetLocationIDdropdown()
        '''''''''''''''''
        Dim obj As New clsOrder_Location
        Dim dt As New Data.DataTable
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        dt = obj.FillLocation
        If dt.Rows.Count <> 0 Then
            cmblocationid.DataSource = dt
            cmblocationid.DataTextField = "LocationName"
            cmblocationid.DataValueField = "LocationID"
            cmblocationid.DataBind()
            'Setdropdown()
        Else
            cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            cmblocationid.Items.Add(item)
        End If
        ''''''''''''''''''''


        ''------------------''
        Dim locationid_chk As String = ""
        Dim locationid_true As Boolean = True

        Try
            Dim dt_new As New Data.DataTable
            dt_new = obj.FillLocationIsmaster()

            locationid_chk = Session("Locationid")

            Dim n As Integer
            For n = 0 To dt_new.Rows.Count - 1
                If locationid_chk = dt_new.Rows(n)("LocationID") Then
                    ' locationid_true = False
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        If locationid_true Then

            If CompanyID = "SouthFloral" Or CompanyID = "SouthFloralsTraining" Then
                cmblocationid.Items.Clear()
                Dim lst As New ListItem
                lst.Text = "--Select--"
                lst.Value = ""
                cmblocationid.Items.Add(lst)
                cmblocationid.Items.Add(locationid_chk)
                If locationid_chk <> "Corporate" Then
                    cmblocationid.Items.Add("Corporate")
                End If
            Else
                cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid_chk))
                cmblocationid.Enabled = False
            End If

        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CType(SessionKey("CompanyID"), String)
        DivisionID = CType(SessionKey("DivisionID"), String)
        DepartmentID = CType(SessionKey("DepartmentID"), String)



        'txtcustomersearch.Attributes.Add("placeholder", "SEARCH")
        'txtcustomersearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")

        If IsPostBack = False Then
            SetLocationIDdropdown()
            txtDateFrom.Text = DateTime.Now.Date.ToShortDateString
            ' txtDateTo.Text = DateTime.Now.Date.ToShortDateString
            'txtDateFrom.Text = DateTime.Now.Date.ToShortDateString
            'txtDateTo.Text = DateTime.Now.Date.ToShortDateString
        End If

        Dim strJS1 As String = ""
        Dim strJS2 As String = ""
        Dim myCalendar As String = ""

        myCalendar = "myCalendarShipDate"
        strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

        strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & txtDateFrom.ClientID & """]);" & vbCrLf
        strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf
        'myCalendar3.setSensitiveRange("3/20/2017", null);

        strJS2 = strJS2 & "" & myCalendar & ".setSensitiveRange('" & DateTime.Now.Date.ToShortDateString & "',null); " & vbCrLf
        'ctl00_ContentPlaceHolder_btnarrivedate
        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "  " & "doOnLoadShipDate();" & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS1 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "function doOnLoadShipDate() {" & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS2 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCallShipDate", onloadScript.ToString())


        Dim dt As New DataTable

        If Not Page.IsPostBack Or True Then

            dt = GetLiveInventoryStatusReport()

            If dt.Rows.Count > 0 Then
                InventoryStatusGrid.DataSource = dt
                InventoryStatusGrid.DataBind()
            End If

        End If

    End Sub


    Protected Sub InventoryStatusGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles InventoryStatusGrid.PageIndexChanging

        InventoryStatusGrid.PageIndex = e.NewPageIndex

        Dim dt As New DataTable

        dt = GetLiveInventoryStatusReport()

        If dt.Rows.Count > 0 Then
            InventoryStatusGrid.DataSource = dt
            InventoryStatusGrid.DataBind()
        End If

    End Sub

    Protected Sub InventoryStatusGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles InventoryStatusGrid.Sorting

        Dim dt As New DataTable

        dt = GetLiveInventoryStatusReport()

        Dim dv As DataView = dt.DefaultView

        If hdSortDirection.Value = "" Or hdSortDirection.Value = "DESC" Then
            hdSortDirection.Value = "ASC"
        Else
            hdSortDirection.Value = "DESC"
        End If

        dv.Sort = e.SortExpression & " " & hdSortDirection.Value

        If dt.Rows.Count > 0 Then
            InventoryStatusGrid.DataSource = dv
            InventoryStatusGrid.DataBind()
        End If

    End Sub

    Protected Sub btncustsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchExpression.Click, rdall.CheckedChanged, rdselected.CheckedChanged
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

        '    dt = GetVendorList(CompanyID, DivisionID, DepartmentID, TempCustomerID)

        '    If dt.Rows.Count > 0 Then
        '        InventoryTransferGrid.DataSource = dt
        '        InventoryTransferGrid.DataBind()
        '    End If


        '    ' txtcustomersearch.Text = ""

        Dim dt As New DataTable

        dt = GetLiveInventoryStatusReport()
        lblErr.Visible = True
        ' lblErr.Text = "Rows Found:" & dt.Rows.Count
        If dt.Rows.Count > 0 Or True Then
            InventoryStatusGrid.DataSource = dt
            InventoryStatusGrid.DataBind()
        End If

    End Sub

    Private Sub cmblocationid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmblocationid.SelectedIndexChanged
        Dim dt As New DataTable

        dt = GetLiveInventoryStatusReport()
        lblErr.Visible = True
        ' lblErr.Text = "Rows Found:" & dt.Rows.Count
        If dt.Rows.Count > 0 Or True Then
            InventoryStatusGrid.DataSource = dt
            InventoryStatusGrid.DataBind()
        End If
    End Sub

    Private Sub drpType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpType.SelectedIndexChanged
        Dim dt As New DataTable

        dt = GetLiveInventoryStatusReport()
        lblErr.Visible = True
        ' lblErr.Text = "Rows Found:" & dt.Rows.Count
        If dt.Rows.Count > 0 Or True Then
            InventoryStatusGrid.DataSource = dt
            InventoryStatusGrid.DataBind()
        End If
    End Sub

    'Public Function GetInventoryStatus(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, _
    '                                        Optional ByVal VendorID As String = "") As DataTable

    '    Return GetInventoryStatusReport()

    '    Exit Function

    '    Dim dt As New DataTable

    '    Dim SQLSTR As String = ""
    '    SQLSTR = SQLSTR & " SELECT * FROM ( "
    '    SQLSTR = SQLSTR & " SELECT i.CompanyID, i.DivisionID, i.DepartmentID, "
    '    SQLSTR = SQLSTR & " iw.LocationID, i.ItemTypeID, i.ItemCategoryID, "
    '    SQLSTR = SQLSTR & " i.ItemID, i.ItemName, i.ItemColor, i.Variety,  iw.VendorID, "
    '    SQLSTR = SQLSTR & " iw.QtyOnHand, 0 as QTYToReceive,  null AS PurchaseShipDate, "
    '    SQLSTR = SQLSTR & " i.ItemUOM, i.ItemPackSize, iw.QtyCommitted, iw.QtyOnOrder, iw.QtyOnBackorder, iw.ReOrderQty, "
    '    SQLSTR = SQLSTR & " i.AverageCost, i.Price          "
    '    SQLSTR = SQLSTR & " FROM   InventoryItems i "
    '    SQLSTR = SQLSTR & " INNER JOIN   InventoryByWarehouse iw ON   i.CompanyID = iw.CompanyID AND   i.DivisionID = iw.DivisionID AND   i.DepartmentID = iw.DepartmentID "
    '    SQLSTR = SQLSTR & " AND   i.ItemID = iw.ItemID "
    '    SQLSTR = SQLSTR & "  WHERE  i.CompanyID = @CompanyID AND  i.DivisionID = @DivisionID AND  i.DepartmentID = @DepartmentID "

    '    If drpType.SelectedValue <> "" Then
    '        SQLSTR = SQLSTR & " AND  i.ItemCategoryID  = '" & drpType.SelectedValue & "'"
    '    End If

    '    If cmblocationid.SelectedValue <> "" Then
    '        SQLSTR = SQLSTR & " AND  iw.LocationID = '" & cmblocationid.SelectedValue & "'"
    '    End If

    '    If txtSearchExpression.Text.Trim <> "" Then
    '        If drpCondition.SelectedValue = "=" Then
    '            If drpFieldName.SelectedValue = "VendorID" Then
    '                SQLSTR = SQLSTR & " AND  iw.VendorID   = '" & txtSearchExpression.Text & "'"
    '                ' And iw.VendorID = CASE isnull(@VendorID, '') WHEN '' THEN iw.VendorID ELSE @VendorID END
    '            End If
    '            If drpFieldName.SelectedValue = "ItemID" Then
    '                SQLSTR = SQLSTR & " AND  i.ItemID  = '" & txtSearchExpression.Text & "'"
    '                ' And i.ItemID = CASE isnull(@ItemID, '') WHEN '' THEN i.ItemID ELSE @ItemID END
    '            End If
    '            If drpFieldName.SelectedValue = "LocationID" Then
    '                SQLSTR = SQLSTR & " AND  iw.LocationID   = '" & txtSearchExpression.Text & "'"
    '                ' And iw.LocationID = CASE isnull(@LocationID, '') WHEN '' THEN iw.LocationID ELSE @LocationID END
    '            End If

    '        End If
    '        If drpCondition.SelectedValue = "Like" Then
    '            If drpFieldName.SelectedValue = "VendorID" Then
    '                SQLSTR = SQLSTR & " AND  iw.VendorID  Like '%" & txtSearchExpression.Text & "%'"
    '            End If
    '            If drpFieldName.SelectedValue = "ItemID" Then
    '                SQLSTR = SQLSTR & " AND  i.ItemID  Like '%" & txtSearchExpression.Text & "%'"
    '            End If
    '            If drpFieldName.SelectedValue = "LocationID" Then
    '                SQLSTR = SQLSTR & " AND  iw.LocationID  Like '%" & txtSearchExpression.Text & "%'"
    '            End If

    '        End If
    '    End If


    '    SQLSTR = SQLSTR & "  UNION ALL "
    '    SQLSTR = SQLSTR & "  SELECT H.CompanyID, H.DivisionID, H.DepartmentID, "
    '    SQLSTR = SQLSTR & "  H.LocationID, i.ItemTypeID, i.ItemCategoryID, "
    '    SQLSTR = SQLSTR & "  D.ItemID, i.ItemName, D.Color,  i.Variety,  H.VendorID, "
    '    SQLSTR = SQLSTR & "  max(0) as QtyOnHand, Sum(D.OrderQty) as QTYToReceive,  H.PurchaseShipDate, "
    '    SQLSTR = SQLSTR & "  D.ItemUOM, D.pack, max(0) as QtyCommitted, max(0) as QtyOnOrder, max(0) as QtyOnBackorder, max(0) as ReOrderQty, "
    '    SQLSTR = SQLSTR & "  max(i.AverageCost), i.Price         "
    '    SQLSTR = SQLSTR & "  FROM   PurchaseHeaderNew H "
    '    SQLSTR = SQLSTR & "  INNER JOIN   PurchaseDetailNew D ON   D.CompanyID = H.CompanyID AND   D.DivisionID = H.DivisionID AND   D.DepartmentID = H.DepartmentID AND   D.PurchaseNumber = H.PurchaseNumber "
    '    SQLSTR = SQLSTR & "  INNER JOIN   InventoryItems i ON   i.CompanyID = D.CompanyID AND   i.DivisionID = D.DivisionID AND   i.DepartmentID = D.DepartmentID AND   i.ItemID = D.ItemID "
    '    SQLSTR = SQLSTR & "  WHERE  H.CompanyID = @CompanyID AND  H.DivisionID = @DivisionID AND  H.DepartmentID = @DepartmentID AND  H.PurchaseShipDate > getdate() "

    '    If drpType.SelectedValue <> "" Then
    '        SQLSTR = SQLSTR & " AND  i.ItemCategoryID  = '" & drpType.SelectedValue & "'"
    '    End If

    '    If cmblocationid.SelectedValue <> "" Then
    '        SQLSTR = SQLSTR & " AND  H.LocationID = '" & cmblocationid.SelectedValue & "'"
    '    End If

    '    If txtSearchExpression.Text.Trim <> "" Then
    '        If drpCondition.SelectedValue = "=" Then
    '            If drpFieldName.SelectedValue = "VendorID" Then
    '                SQLSTR = SQLSTR & " AND  H.VendorID   = '" & txtSearchExpression.Text & "'"
    '                ' And H.VendorID = CASE isnull(@VendorID, '') WHEN '' THEN H.VendorID ELSE @VendorID END
    '            End If
    '            If drpFieldName.SelectedValue = "ItemID" Then
    '                SQLSTR = SQLSTR & " AND  i.ItemID  = '" & txtSearchExpression.Text & "'"
    '                ' And i.ItemID = CASE isnull(@ItemID, '') WHEN '' THEN i.ItemID ELSE @ItemID END
    '            End If
    '            If drpFieldName.SelectedValue = "LocationID" Then
    '                SQLSTR = SQLSTR & " AND  H.LocationID   = '" & txtSearchExpression.Text & "'"
    '                ' And H.LocationID = CASE isnull(@LocationID, '') WHEN '' THEN H.LocationID ELSE @LocationID END
    '            End If

    '        End If
    '        If drpCondition.SelectedValue = "Like" Then
    '            If drpFieldName.SelectedValue = "VendorID" Then
    '                SQLSTR = SQLSTR & " AND  H.VendorID  Like '%" & txtSearchExpression.Text & "%'"
    '            End If
    '            If drpFieldName.SelectedValue = "ItemID" Then
    '                SQLSTR = SQLSTR & " AND  i.ItemID  Like '%" & txtSearchExpression.Text & "%'"
    '            End If
    '            If drpFieldName.SelectedValue = "LocationID" Then
    '                SQLSTR = SQLSTR & " AND  H.LocationID  Like '%" & txtSearchExpression.Text & "%'"
    '            End If

    '        End If
    '    End If

    '    SQLSTR = SQLSTR & "  GROUP BY H.CompanyID, H.DivisionID, H.DepartmentID, "
    '    SQLSTR = SQLSTR & "  D.ItemID, i.ItemName, D.Color, D.pack, i.Variety, i.ItemTypeID, i.ItemCategoryID, H.VendorID, "
    '    SQLSTR = SQLSTR & "  i.Price, D.ItemUOM, H.LocationID, H.PurchaseShipDate "
    '    SQLSTR = SQLSTR & "  ) AS X "
    '    SQLSTR = SQLSTR & "  Order by X.CompanyID, X.DivisionID, X.DepartmentID,  X.LocationID, X.ItemCategoryID, X.ItemID, X.ItemName, X.ItemColor, X.Variety, X.VendorID, X.PurchaseShipDate "
    '    SQLSTR = SQLSTR & "    "

    '    If txtSearchExpression.Text.Trim <> "" Then
    '        Response.Write(SQLSTR)
    '    End If



    '    Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using Command As New SqlCommand(SQLSTR, Connection)
    '            Command.CommandType = CommandType.Text

    '            Command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            Command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

    '            Try
    '                Dim da As New SqlDataAdapter(Command)
    '                da.Fill(dt)
    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return dt

    'End Function

    'Private Function GetInventoryStatusReport() As DataTable

    '    Dim dt As New DataTable

    '    Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '        Using Command As New SqlCommand("[enterprise].[GetInventoryStatusReport]", connection)
    '            Command.CommandType = CommandType.StoredProcedure

    '            Command.Parameters.AddWithValue("CompanyID", CompanyID)
    '            Command.Parameters.AddWithValue("DivisionID", DivisionID)
    '            Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

    '            Command.Parameters.AddWithValue("ItemCategoryID", drpType.SelectedValue)
    '            Command.Parameters.AddWithValue("LocationID", cmblocationid.SelectedValue)

    '            If txtSearchExpression.Text.Trim <> "" Then
    '                If drpCondition.SelectedValue = "Like" Then

    '                    If drpFieldName.SelectedValue = "VendorID" Then
    '                        Command.Parameters.AddWithValue("VendorID", txtSearchExpression.Text)
    '                    End If
    '                    If drpFieldName.SelectedValue = "ItemID" Then
    '                        Command.Parameters.AddWithValue("ItemID", txtSearchExpression.Text)
    '                    End If

    '                End If
    '            End If

    '            Try
    '                Dim da As New SqlDataAdapter(Command)
    '                da.Fill(dt)

    '            Catch ex As Exception

    '            End Try

    '        End Using
    '    End Using

    '    Return dt

    'End Function

    Private Function GetLiveInventoryStatusReport() As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryStatusReport_Revised_New]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("ItemCategoryID", drpType.SelectedValue)
                Command.Parameters.AddWithValue("LocationID", cmblocationid.SelectedValue)

                If txtSearchExpression.Text.Trim <> "" Then
                    'If drpCondition.SelectedValue = "Like" Then

                    If drpFieldName.SelectedValue = "VendorID" Then
                        Command.Parameters.AddWithValue("VendorID", txtSearchExpression.Text)
                    End If
                    If drpFieldName.SelectedValue = "ItemID" Then
                        Command.Parameters.AddWithValue("ItemID", txtSearchExpression.Text)
                    End If
                    If rdselected.Checked Then
                        Command.Parameters.AddWithValue("@DeliveryDate", txtDateFrom.Text)
                    End If

                    'End If
                End If

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

