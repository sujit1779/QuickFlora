Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class InventoryFormAdd
    Inherits System.Web.UI.Page
    Private obj As New clsItems

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""



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


        Dim locationid As String = ""
        Try
            locationid = Session("Locationid")
        Catch ex As Exception

        End Try
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
                    locationid_true = False
                    Exit For
                End If
            Next


        Catch ex As Exception

        End Try

        If locationid_true Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
            cmblocationid.Enabled = False
        Else
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
        End If
        ' Session("OrderLocationid") = cmblocationid.SelectedValue
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        txtitemsearch.Attributes.Add("placeholder", "SEARCH")
        txtitemsearch.Attributes.Add("onKeyUp", "SendQuery2(this.value)")

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))
        EmployeeID = CStr(SessionKey("EmployeeID"))

        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        fillHomePageManagement()

        myCalendarn = myCalendarn + 1
        myCalendar = "myCalendars" & myCalendarn
        strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

        strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & lblShipDate.ClientID & """]);" & vbCrLf
        strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf


        If Not Page.IsPostBack Then
            SetLocationIDdropdown()

            GetInventoryItemsList()


            Dim dt As New DataTable()
            dt = GetVendorShipMethods(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
            drpshipemthod.Items.Clear()
            If dt.Rows.Count <> 0 Then
                drpshipemthod.DataSource = dt
                drpshipemthod.DataTextField = "ShipMethodDescription"
                drpshipemthod.DataValueField = "ShipMethodID"
                drpshipemthod.DataBind()
                Try
                    drpshipemthod.SelectedIndex = drpshipemthod.Items.IndexOf(drpshipemthod.Items.FindByValue("LocalTruck"))
                Catch ex As Exception

                End Try
            End If

            Dim tm As DateTime
            tm = DateTime.Now
            txtDateTo.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            txtstart.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            tm = tm.AddDays(7)
            txtend.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
        End If
        ' cmblocationid.SelectedValue = "" Or
        If chkexculea.Checked Then
            '  btnAccept.Enabled = False
        Else
            btnAccept.Enabled = True
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

    Public Function priceformat(ByVal pr As String) As String
        Dim price As Decimal = 0
        Try
            price = pr
            pr = Format(price, "0.00")
        Catch ex As Exception

        End Try

        Return pr
    End Function

    Public Function GetInventoryItemsListNew() As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryItemsListAvailbilityListBetaNewPage2]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@Location", cmblocationid.SelectedValue)
                Session("p1") = cmblocationid.SelectedValue
                Command.Parameters.AddWithValue("@ArrivalDate", txtDateTo.Text)
                Session("p2") = txtDateTo.Text
                Command.Parameters.AddWithValue("@startavailabledate", txtstart.Text)
                Session("p3") = txtstart.Text
                Command.Parameters.AddWithValue("@endavailabledate", txtend.Text)
                Session("p4") = txtend.Text
                Command.Parameters.AddWithValue("@ExcludeArrivalDate", chkexculea.Checked)
                Session("p5") = chkexculea.Checked
                ',@Condition NVARCHAR (36)=NULL, @fieldName NVARCHAR (36)=NULL, @fieldexpression NVARCHAR (400)=NULL
                Command.Parameters.AddWithValue("@Condition", drpSearchCondition.SelectedValue)
                Session("p6") = drpSearchCondition.SelectedValue
                Command.Parameters.AddWithValue("@fieldName", drpSearchFor.SelectedValue)
                Session("p7") = drpSearchFor.SelectedValue
                Command.Parameters.AddWithValue("@fieldexpression", txtSearchValue.Text)
                Session("p8") = txtSearchValue.Text
                ', @ArrivalDate datetime = NULL 
                ', @startavailabledate datetime = NULL 
                ', @endavailabledate datetime = NULL 

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    lblInfo.Text = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function


    Public Function GetInventoryItemsListNewWay1() As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryItemsListAvailbilityListAjaxBeta]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@Location", cmblocationid.SelectedValue)


                Command.Parameters.AddWithValue("@ArrivalDate", DateTime.Now)
                Command.Parameters.AddWithValue("@ExcludeArrivalDate", True)
                ',@Condition NVARCHAR (36)=NULL, @fieldName NVARCHAR (36)=NULL, @fieldexpression NVARCHAR (400)=NULL
                Command.Parameters.AddWithValue("@Condition", "")
                Command.Parameters.AddWithValue("@fieldName", "")
                'Command.Parameters.AddWithValue("@fieldexpression", keyword)
                Command.Parameters.AddWithValue("@fieldexpression", txtSearchValue.Text)
                ', @ArrivalDate datetime = NULL 
                ', @startavailabledate datetime = NULL 
                ', @endavailabledate datetime = NULL 
                Session("p1") = cmblocationid.SelectedValue
                Session("p2") = txtDateTo.Text
                Session("p3") = txtstart.Text
                Session("p4") = txtend.Text
                Session("p5") = chkexculea.Checked
                Session("p6") = drpSearchCondition.SelectedValue
                Session("p7") = drpSearchFor.SelectedValue
                Session("p8") = txtSearchValue.Text

                Try
                    Dim ds As New DataSet
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)

                    If ds.Tables(0).Rows.Count >= 0 Then
                        Return ds
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    lblInfo.Text = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function


    Private Sub GetInventoryItemsListNeway()

        Dim ds As New DataSet
        ds = GetInventoryItemsListNewWay1()
        gvItemsList.DataSource = ds
        gvItemsList.DataBind()
        Try
            If ds.Tables(0).Rows.Count > 0 Then

                lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
            Else
                lblInfo.Text = "0 records found"
            End If
        Catch ex As Exception

        End Try

        'If ds.Tables.Count > 0 Then

        '    If ds.Tables(0).Rows.Count > 0 Then

        '        lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
        '    Else
        '        lblInfo.Text = "0 records found"
        '    End If
        'Else
        '    lblInfo.Text = "0 records found"
        'End If


    End Sub



    Private Sub GetInventoryItemsList()

        Dim ds As New DataSet
        ds = GetInventoryItemsListNew()
        gvItemsList.DataSource = ds
        gvItemsList.DataBind()
        Try
            If ds.Tables(0).Rows.Count > 0 Then

                lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
            Else
                lblInfo.Text = "0 records found"
            End If
        Catch ex As Exception

        End Try

        'If ds.Tables.Count > 0 Then

        '    If ds.Tables(0).Rows.Count > 0 Then

        '        lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
        '    Else
        '        lblInfo.Text = "0 records found"
        '    End If
        'Else
        '    lblInfo.Text = "0 records found"
        'End If


    End Sub

    Protected Sub gvItemsList_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvItemsList.PageIndexChanging

        gvItemsList.PageIndex = e.NewPageIndex
        GetInventoryItemsList()

        'txtDateTo 
        'txtstart 
        'txtend 


    End Sub

    Dim strJS1 As String = ""
    Dim strJS2 As String = ""
    Dim strJS3 As String = ""
    Dim strJs5 As String = ""
    Dim myCalendar As String
    Dim myCalendarn As Integer = 0

    Protected Sub gvItemsList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvItemsList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            'Dim lblShipDate As New TextBox
            'lblShipDate = e.Row.FindControl("lblShipDate")
            'myCalendarn = myCalendarn + 1
            'myCalendar = "myCalendar" & myCalendarn
            'strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

            'strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & lblShipDate.ClientID & """]);" & vbCrLf
            'strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf

            ' Dim txtItemID As Label = e.Row.FindControl("txtItemID")
            Dim chkOrderNumber As CheckBox = e.Row.FindControl("chkOrderNumber")

            Dim txtstem As Label = e.Row.FindControl("txtstem")
            Dim txtrecevied As TextBox = e.Row.FindControl("txtrecevied")
            Dim txtBalance As TextBox = e.Row.FindControl("txtBalance")
            Dim txtQtyAccepted As Label = e.Row.FindControl("txtQtyAccepted")

            Try
                txtBalance.Text = txtstem.Text - txtQtyAccepted.Text
            Catch ex As Exception

            End Try

            If txtstem.Text = txtQtyAccepted.Text Then
                chkOrderNumber.Enabled = False
            End If

            txtrecevied.Attributes.Add("onKeyUp", "allnumeric(this)")
            'txtprice.Attributes.Add("onKeyUp", "allnumeric2(this)")

            txtrecevied.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtrecevied.Attributes.Add("onblur", "Qtybalance(this,'" & txtstem.Text & "','QtyOnHand','" & txtBalance.ClientID & "','" & txtQtyAccepted.Text & "')")

            'txtprice.Attributes.Add("onfocus", "myFocusFunction(this)")
            'txtprice.Attributes.Add("onblur", "Saveitem(this,'" & txtItemID.Text & "','Price','" & txtprice.Text & "')")

            'Dim dt As New DataTable()
            'dt = GetVendorShipMethods(Me.CompanyID, Me.DivisionID, Me.DepartmentID)

            'Dim drpshipemthod As New DropDownList
            'drpshipemthod = e.Row.FindControl("drpshipemthod")

            'drpshipemthod.Items.Clear()
            'If dt.Rows.Count <> 0 Then
            '    drpshipemthod.DataSource = dt
            '    drpshipemthod.DataTextField = "ShipMethodDescription"
            '    drpshipemthod.DataValueField = "ShipMethodID"
            '    drpshipemthod.DataBind()
            'End If



            'If dt.Rows.Count = 1 Then
            '    'Dim o As New Object
            '    'Dim e As New EventArgs
            '    'drpshipemthod_SelectedIndexChanged(o, e)
            'Else
            '    Dim lst As New ListItem
            '    lst.Value = ""
            '    lst.Text = "Select Ship Method"
            '    drpshipemthod.Items.Insert(0, lst)
            'End If

        End If
    End Sub



    Public Function GetVendorShipMethods(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetVendorShipMethods]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function


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

        'Command.Parameters.AddWithValue("@Condition", drpSearchCondition.SelectedValue)
        'Command.Parameters.AddWithValue("@fieldName", drpSearchFor.SelectedValue)
        'Command.Parameters.AddWithValue("@fieldexpression", txtSearchValue.Text)

        If txtitemsearch.Text.Trim <> "" Then
            drpSearchCondition.SelectedValue = "="
            drpSearchFor.SelectedValue = "ItemID"
            txtSearchValue.Text = txtitemsearch.Text.Trim
            txtitemsearch.Text = ""

            GetInventoryItemsListNeway()

        Else
            GetInventoryItemsList()
        End If



    End Sub

    'Protected Sub btnSUbmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSUbmit.Click
    '    Response.Redirect("InventoryForm.aspx")
    'End Sub

    Private Sub cmblocationid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmblocationid.SelectedIndexChanged
        txtSearchValue.Text = ""
        GetInventoryItemsList()

        'Dim strJS1 As String = ""
        'Dim strJS2 As String = ""
        'Dim myCalendar As String = ""

        'myCalendar = "myCalendarto"
        'strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

        'strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & txtDateTo.ClientID & """]);" & vbCrLf
        'strJS2 = strJS2 & "" & myCalendar & "alert(myCalendarto); " & vbCrLf
        'strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf
        ''myCalendar3.setSensitiveRange("3/20/2017", null);

        ''ctl00_ContentPlaceHolder_btnarrivedate

        'Dim onloadScript As String = ""
        'onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        'onloadScript = onloadScript & "  " & "doOnLoadNew();" & " " & vbCrLf
        'onloadScript = onloadScript & "  " & strJS1 & " " & vbCrLf
        'onloadScript = onloadScript & "  " & "function doOnLoadNew() {" & " " & vbCrLf
        'onloadScript = onloadScript & "  " & strJS2 & " " & vbCrLf
        'onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        'onloadScript = onloadScript & "<" & "/" & "script>"
        'Me.ClientScript.RegisterStartupScript(Me.GetType(), "doOnLoadNew", onloadScript.ToString())
    End Sub


    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn.Click
        txtSearchValue.Text = ""
        GetInventoryItemsList()

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Response.Redirect("InventoryFormAdd.aspx")
    End Sub

    Private Sub gvItemsList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvItemsList.RowCommand
        'If e.CommandName = "Accept" Then
        '    Dim itemid As String = ""
        '    itemid = e.CommandArgument
        '    Dim gvrow As GridViewRow

        '    'gvrow = CType(CType((e.CommandSource), Control), GridViewRow)
        '    Dim txtstem As New Label
        '    txtstem = CType(gvrow.FindControl("txtstem"), Label)
        '    lblmsg.Text = txtstem.Text

        '    Dim txtrecevied As New Label
        '    txtrecevied = CType(gvrow.FindControl("txtrecevied"), Label)
        '    lblmsg.Text = lblmsg.Text & " " & txtrecevied.Text

        '    lblmsg.Text = lblmsg.Text & " " & itemid

        'End If
        'gvItemsList 


    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click

        For Each row As GridViewRow In gvItemsList.Rows
            Dim chk As CheckBox = row.FindControl("chkOrderNumber")
            If chk.Checked Then
                'Dim lblShipDate As TextBox
                'lblShipDate = row.FindControl("lblShipDate")

                Try
                    Dim shdate As New DateTime
                    shdate = lblShipDate.Text

                Catch ex As Exception

                End Try

                'Dim drpshipemthod As New DropDownList
                'drpshipemthod = row.FindControl("drpshipemthod")

                If lblShipDate.Text.Trim = "" Or drpshipemthod.SelectedValue = "" Then
                    Dim onloadScript1 As String = ""
                    onloadScript1 = onloadScript1 & "<script type='text/javascript'>" & vbCrLf
                    onloadScript1 = onloadScript1 & " alert('" & "Please select Ship Method and Ship date for each row and try again to Accpet." & "');" & vbCrLf
                    onloadScript1 = onloadScript1 & "<" & "/" & "script>"
                    ' Register script with page 
                    ' strJs5 = onloadScript
                    Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript1.ToString())
                    Exit Sub
                End If

            End If
        Next



        Dim OrderNo As String = ""
        Try
            EmployeeID = Session("EmployeeID")
        Catch ex As Exception

        End Try
        Dim ArrivalDate As String = ""
        Dim ShipDate As String = ""
        Dim GroupCode As String = ""
        Dim Location As String = ""
        Dim Total As Decimal = 0

        Dim InLineNumberRO As String = ""
        InLineNumberRO = Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond
        OrderNo = InLineNumberRO
        For Each row As GridViewRow In gvItemsList.Rows
            Dim ItemID As String = (gvItemsList.DataKeys(row.RowIndex).Value)
            Dim chk As CheckBox = row.FindControl("chkOrderNumber")

            If chk.Checked Then
                'st = st & "OrderNumber=" & OrderNumber & "<br> and its InvoiceNumber=" & Invoice_CreateFromOrder(OrderNumber) & "<br>"
                ' AjaxOrderInvoice(OrderNumber)
                Dim ItemDescription As String = ""
                Dim lblItemDescription As Label
                lblItemDescription = row.FindControl("lblItemDescription")
                ItemDescription = (lblItemDescription.Text)
                lblmsg.Text = lblmsg.Text & " " & lblItemDescription.Text

                Dim ItemColor As String = ""
                Dim lblItemColor As Label
                lblItemColor = row.FindControl("lblItemColor")
                ItemColor = (lblItemColor.Text)
                lblmsg.Text = lblmsg.Text & " " & lblItemColor.Text


                Dim lblGroupCode As Label
                lblGroupCode = row.FindControl("lblGroupCode")
                GroupCode = (lblGroupCode.Text)
                lblmsg.Text = lblmsg.Text & " " & lblGroupCode.Text



                'Dim lblShipDate As TextBox
                'lblShipDate = row.FindControl("lblShipDate")

                Try
                    Dim shdate As New DateTime
                    shdate = lblShipDate.Text
                    ShipDate = shdate.ToShortDateString
                Catch ex As Exception
                    ShipDate = ""
                End Try

                Dim LBLLocation As Label
                LBLLocation = row.FindControl("LBLLocation")
                Location = (LBLLocation.Text)
                lblmsg.Text = lblmsg.Text & " " & LBLLocation.Text


                ' Dim ItemID As String = ""
                Dim txtItemID As Label
                txtItemID = row.FindControl("txtItemID")
                ItemID = (txtItemID.Text)
                lblmsg.Text = lblmsg.Text & " " & txtItemID.Text

                Dim ItemQty As Integer
                Dim txtstem As Label
                txtstem = row.FindControl("txtstem")
                ItemQty = CInt(txtstem.Text)
                lblmsg.Text = lblmsg.Text & " " & txtstem.Text

                Dim ItemQtyRecevied As Integer
                Dim txtrecevied As TextBox
                txtrecevied = row.FindControl("txtrecevied")
                Try
                    ItemQtyRecevied = CInt(txtrecevied.Text)
                Catch ex As Exception
                    ItemQtyRecevied = 0
                End Try

                lblmsg.Text = lblmsg.Text & " " & ItemQtyRecevied & " "

                Dim price As Decimal = 0
                Dim txtprice As Label
                txtprice = row.FindControl("txtprice")
                Try
                    price = (txtprice.Text)
                Catch ex As Exception

                End Try
                lblmsg.Text = lblmsg.Text & " " & price & " "

                Dim lblinlineNumber As Label
                lblinlineNumber = row.FindControl("lblinlineNumber")




                'Dim drpshipemthod As New DropDownList
                'drpshipemthod = row.FindControl("drpshipemthod")

                If OrderNo = "" Then
                    'Dim rs As SqlDataReader
                    'Dim PopOrderNo As New DAL.CustomOrder()
                    'rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextRequisitionNumber")
                    'While rs.Read()
                    '    OrderNo = rs("NextNumberValue")
                    'End While
                    'rs.Close()
                End If

                ' lblmsg.Text = lblmsg.Text & " OrderNo:" & OrderNo & " "

                Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
                Dim connec As New SqlConnection(constr)


                Dim qry1 As String
                qry1 = "insert into PO_Requisition_InvLogs ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[OrderNo] ,[ItemQtyRecevied] ,[ShipDate] ,[shipemthod] ,[ItemID]  )" _
                     & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@OrderNo ,@ItemQtyRecevied ,@ShipDate ,@shipemthod ,@ItemID) "
                Dim com1 As SqlCommand
                com1 = New SqlCommand(qry1, connec)
                com1.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com1.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
                com1.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
                com1.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = OrderNo
                com1.Parameters.Add(New SqlParameter("@ItemQtyRecevied", SqlDbType.NVarChar)).Value = ItemQtyRecevied
                com1.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = ShipDate
                com1.Parameters.Add(New SqlParameter("@shipemthod", SqlDbType.NVarChar)).Value = drpshipemthod.SelectedValue
                com1.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar)).Value = ItemID

                com1.Connection.Open()
                com1.ExecuteNonQuery()
                com1.Connection.Close()

                Dim qry As String

                If OrderNo <> "" And ItemQtyRecevied > 0 And ShipDate <> "" And drpshipemthod.SelectedValue <> "" Then
                    qry = "insert into PO_Requisition_Details ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE],[PONO],HDStatus,ShipDate,Location,Type,ProductName,Vendor_Remarks)" _
                     & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@OrderNo ,@Product ,@QOH ,@DUMP ,@Q_REQ ,@PRESOLD ,@COLOR_VARIETY ,@REMARKS ,@Q_ORD ,@PACK ,@COST ,@Ext_COSt ,@Vendor_Code ,@Buyer ,@Status ,@Q_Recv ,@ISSUE,'','Entry Completed',@ShipDate,@Location,@Type,@ProductName,@Vendor_Remarks) "

                    Dim com As SqlCommand
                    com = New SqlCommand(qry, connec)
                    '@ShipDate
                    'ArrivalDate
                    'Location
                    'Type
                    com.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar)).Value = ItemID
                    com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = GroupCode
                    com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = "Corporate"
                    com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = ShipDate

                    com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                    com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
                    com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
                    com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = OrderNo
                    com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = ItemID
                    com.Parameters.Add(New SqlParameter("@QOH", SqlDbType.NVarChar)).Value = "0"
                    com.Parameters.Add(New SqlParameter("@DUMP", SqlDbType.NVarChar)).Value = "0"
                    com.Parameters.Add(New SqlParameter("@Q_REQ", SqlDbType.NVarChar)).Value = ItemQtyRecevied
                    com.Parameters.Add(New SqlParameter("@PRESOLD", SqlDbType.NVarChar)).Value = "0"
                    com.Parameters.Add(New SqlParameter("@COLOR_VARIETY", SqlDbType.NVarChar)).Value = ItemColor
                    Try
                        If ItemDescription.Length > 49 Then
                            ItemDescription = ItemDescription.Substring(0, 49)
                        End If
                    Catch ex As Exception

                    End Try
                    com.Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = ItemDescription
                    com.Parameters.Add(New SqlParameter("@Q_ORD", SqlDbType.NVarChar)).Value = ItemQtyRecevied
                    com.Parameters.Add(New SqlParameter("@PACK", SqlDbType.NVarChar)).Value = "1"
                    com.Parameters.Add(New SqlParameter("@COST", SqlDbType.NVarChar)).Value = price
                    com.Parameters.Add(New SqlParameter("@Ext_COSt", SqlDbType.NVarChar)).Value = (ItemQtyRecevied * price)
                    com.Parameters.Add(New SqlParameter("@Vendor_Code", SqlDbType.NVarChar)).Value = Location
                    com.Parameters.Add(New SqlParameter("@Buyer", SqlDbType.NVarChar)).Value = EmployeeID
                    com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = "No Action"
                    com.Parameters.Add(New SqlParameter("@Q_Recv", SqlDbType.NVarChar)).Value = "0"
                    com.Parameters.Add(New SqlParameter("@ISSUE", SqlDbType.NVarChar)).Value = "0"
                    com.Parameters.Add(New SqlParameter("@Vendor_Remarks", SqlDbType.NVarChar)).Value = drpshipemthod.SelectedValue
                    'Vendor_Remarks

                    com.Connection.Open()
                    com.ExecuteNonQuery()
                    com.Connection.Close()

                    Total = Total + (ItemQty * price)

                    Try
                        UpdateInventoryByGrowerAvailability(lblinlineNumber.Text, ItemQtyRecevied)
                        lblmsg.Text = lblmsg.Text & " lblinlineNumber:" & lblinlineNumber.Text & " "
                        lblmsg.Text = lblmsg.Text & " ItemQtyRecevied:" & ItemQtyRecevied & " "
                    Catch ex As Exception
                        '' Response.Write(ex.Message)
                    End Try
                    ' Response.Redirect("RequisitionOrderList.aspx")


                    Dim PO_Requisition_Details_InLineNumber As String = ""
                    Try
                        qry = "select PO_Requisition_Details.InLineNumber  from PO_Requisition_Details  where       " _
                    & " [CompanyID] = @CompanyID AND [DivisionID] = @DivisionID AND [DepartmentID]  = @DepartmentID AND [OrderNo] = @OrderNo AND [Product] = @Product  AND [Q_REQ]= @Q_REQ AND  ShipDate =@ShipDate   Order by PO_Requisition_Details.InLineNumber  DESC "

                        com = New SqlCommand(qry, connec)


                        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
                        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
                        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = OrderNo
                        com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = ItemID
                        com.Parameters.Add(New SqlParameter("@Q_REQ", SqlDbType.NVarChar)).Value = ItemQtyRecevied
                        com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = ShipDate

                        Dim da_1 As New SqlDataAdapter(com)
                        Dim dt_1 As New DataTable
                        da_1.Fill(dt_1)

                        If dt_1.Rows.Count > 0 Then
                            PO_Requisition_Details_InLineNumber = dt_1.Rows(0)(0)
                        End If

                        If PO_Requisition_Details_InLineNumber <> "" Then

                            Dim qry2 As String
                            qry2 = "insert into [PO_Requisition_ByGrowerAvailabilityLogs] ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[PO_Requisition_inlinenumber] ,[ItemQtyRecevied] ,[ShipDate] ,[shipemthod] ,[ItemID] ,GrowerAvailability_inlinenumber )" _
                                 & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@PO_Requisition_inlinenumber ,@ItemQtyRecevied ,@ShipDate ,@shipemthod ,@ItemID ,@GrowerAvailability_inlinenumber) "

                            com1 = New SqlCommand(qry2, connec)
                            com1.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                            com1.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
                            com1.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
                            com1.Parameters.Add(New SqlParameter("@PO_Requisition_inlinenumber", SqlDbType.NVarChar)).Value = PO_Requisition_Details_InLineNumber
                            com1.Parameters.Add(New SqlParameter("@ItemQtyRecevied", SqlDbType.NVarChar)).Value = ItemQtyRecevied
                            com1.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = ShipDate
                            com1.Parameters.Add(New SqlParameter("@shipemthod", SqlDbType.NVarChar)).Value = drpshipemthod.SelectedValue
                            com1.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar)).Value = ItemID
                            com1.Parameters.Add(New SqlParameter("@GrowerAvailability_inlinenumber", SqlDbType.NVarChar)).Value = lblinlineNumber.Text
                            'GrowerAvailability_inlinenumber
                            com1.Connection.Open()
                            com1.ExecuteNonQuery()
                            com1.Connection.Close()
                        End If


                    Catch ex As Exception

                    End Try


                End If


            End If

        Next

        'If OrderNo <> "" Then
        '    InsertIntoHeader(OrderNo, cmblocationid.SelectedValue, GroupCode, Total, ShipDate, ArrivalDate, EmployeeID)
        '    sendponew(False, Location, OrderNo)
        'End If


        OrderNo = ""

        Dim dt2 As New DataTable
        dt2 = RODETAILSBYInLineNumber(InLineNumberRO)
        If dt2.Rows.Count <> 0 Then
            Dim fn As Integer = 0
            For fn = 0 To dt2.Rows.Count - 1
                Dim VendorID As String = ""
                VendorID = dt2.Rows(fn)(0)
                'Vendor_Code,ShipDate,Vendor_Remarks
                '  Dim ShipDate As String = ""
                ShipDate = dt2.Rows(fn)(1)
                Dim ShipMethod As String = ""
                ShipMethod = dt2.Rows(fn)(2)

                Dim dt3 As New DataTable
                dt3 = RODETAILSBYInLineNumberAndVendor(InLineNumberRO, VendorID, ShipDate, ShipMethod)
                Dim rs As SqlDataReader
                Dim PopOrderNo As New DAL.CustomOrder()
                rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextRequisitionNumber")
                While rs.Read()
                    OrderNo = rs("NextNumberValue")
                End While
                rs.Close()
                Dim fm As Integer = 0
                Dim _total As Decimal = 0
                For fm = 0 To dt3.Rows.Count - 1
                    Dim ROLineNumber As Integer
                    ROLineNumber = dt3.Rows(fm)("InLineNumber")
                    RODETAILSPurchaseNumberUPdate(ROLineNumber, OrderNo)
                    'PONOUpdate2(InLineNumber, _PurchaseOrderNumber, VendorID)
                    _total = _total + dt3.Rows(fm)("Ext_COSt")
                    ' _LocationID = dt3.Rows(fm)("LocationID")
                    'Try
                    '    Dim shdate As New DateTime
                    '    shdate = dt3.Rows(fm)("ShipDate")
                    '    ShipDate = shdate.ToShortDateString
                    '    Dim ardate As New DateTime
                    '    ardate = shdate.AddDays(+1)
                    '    ArrivalDate = ardate.ToShortDateString()

                    '    '''''''''
                    '    '''''''''
                    'Catch ex As Exception

                    'End Try
                Next

                Dim dt22 As New DataTable
                dt22 = GetArrivalDayForTruckingDay(Me.CompanyID, Me.DivisionID, Me.DepartmentID, VendorID, ShipMethod, "Corporate", ShipDate)
                Dim dtnow As New DateTime
                Try
                    Dim shdate As New DateTime
                    shdate = ShipDate
                    Dim ardate As New DateTime
                    ardate = shdate.AddDays(+1)
                    dtnow = ardate
                Catch ex As Exception

                End Try
                If dt22.Rows.Count <> 0 Then
                    Try
                        dtnow = dt22.Rows(0)(0)
                    Catch ex As Exception

                    End Try
                End If

                ArrivalDate = dtnow.Date.ToShortDateString
                'Create Header and Create PO for that RO
                InsertIntoHeader(OrderNo, VendorID, "Flowers", _total, ShipDate, ArrivalDate, EmployeeID, ShipMethod)
                sendponew(True, VendorID, OrderNo)
            Next
        End If

        All_PurchaseOrderNumber = All_PurchaseOrderNumber & " has generated."

        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & " alert('" & All_PurchaseOrderNumber & "');" & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        ' strJs5 = onloadScript
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())

        GetInventoryItemsList()
    End Sub



    Public Function GetArrivalDayForTruckingDay(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal ShipFromLocation As String, ByVal ShipMethodID As String, ByVal ShipToLocation As String, ByVal TruckingDate As String) As DataTable

        Dim ds As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetArrivalDayForTruckingDay]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ShipFromLocation", ShipFromLocation)
                Command.Parameters.AddWithValue("@ShipMethodID", ShipMethodID)
                Command.Parameters.AddWithValue("@ShipToLocation", ShipToLocation)
                Command.Parameters.AddWithValue("@TruckingDate", TruckingDate)

                Try
                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(ds)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return ds

    End Function


    Public Function RODETAILSBYInLineNumber(ByVal InLineNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  Vendor_Code,ShipDate,Vendor_Remarks  "
        ssql = ssql & " FROM PO_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [OrderNo] ='" & InLineNumber & "'  Group by Vendor_Code,ShipDate,Vendor_Remarks  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Public Function RODETAILSBYInLineNumberAndVendor(ByVal InLineNumber As String, ByVal Vendor As String, ByVal ShipDate As String, ByVal ShipMethod As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM PO_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [OrderNo] ='" & InLineNumber & "' AND [Vendor_Code] ='" & Vendor & "'    AND [ShipDate] ='" & ShipDate & "'    AND [Vendor_Remarks] ='" & ShipMethod & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Public Function RODETAILSPurchaseNumberUPdate(ByVal PurchaseLineNumber As Integer, ByVal PurchaseNumber As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PO_Requisition_Details set OrderNo=@PurchaseNumber Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And InLineNumber=@PurchaseLineNumber  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar, 36)).Value = PurchaseNumber
            com.Parameters.Add(New SqlParameter("@PurchaseLineNumber", SqlDbType.BigInt)).Value = PurchaseLineNumber


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return msg
        End Try

        Return True
    End Function


    Public Function UpdateInventoryByGrowerAvailability(ByVal inlineNumber As Integer, ByVal qty As Integer) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " Update InventoryByGrowerAvailability   "
        ssql = ssql & " SET QtyAccepted  = ISNULL(QtyAccepted,0) + " & qty & "  "
        ssql = ssql & "  Where  inlineNumber =" & inlineNumber

        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Return ""
    End Function

    Public Function BatchPOSampleRowInLineNumbernew(ByVal OrderNo As Integer) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " SELECT  InLineNumber  "
        ssql = ssql & " FROM PO_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND  OrderNo =" & OrderNo
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Dim All_PurchaseOrderNumber As String = "PO # "


    Public Sub sendponew(ByVal sendemail As Boolean, ByVal __LocationID As String, ByVal OrderNo As Integer)

        _LocationID = __LocationID

        Dim _PurchaseOrderNumber As String = ""
        Dim InLineNumber As String = ""



        InLineNumber = Date.Now.Year & Date.Now.Month & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond

        Dim _total As Decimal = 0
        Dim dtr As New DataTable
        dtr = BatchPOSampleRowInLineNumbernew(OrderNo)

        Dim nf As Integer

        For nf = 0 To dtr.Rows.Count - 1
            Dim RowNumber As Integer
            RowNumber = dtr.Rows(nf)(0)

            lblmsg.Text = lblmsg.Text & " RowNumber:" & RowNumber & " "
            If True Then
                Dim lblLocationID As New Label
                lblLocationID.Text = _LocationID
                'LBLMSG.Text = LBLMSG.Text & " Reached in checked:" & RowNumber

                Dim dt As New DataTable
                dt = BatchPOSampleRow(RowNumber)
                PurchaseNumber = InLineNumber

                ',[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE]

                Dim PONO As String = ""

                If dt.Rows.Count <> 0 Then
                    Try
                        ItemID = dt.Rows(0)("Product")
                    Catch ex As Exception

                    End Try

                    Try
                        PONO = dt.Rows(0)("PONO")
                    Catch ex As Exception
                        PONO = ""
                    End Try

                    Try
                        Comments = dt.Rows(0)("REMARKS")
                    Catch ex As Exception

                    End Try
                    Description = ""
                    Try
                        Description = dt.Rows(0)("Vendor_Remarks")
                    Catch ex As Exception
                        Description = ""
                    End Try
                    Try
                        Color = dt.Rows(0)("COLOR_VARIETY")
                    Catch ex As Exception

                    End Try
                    Try
                        OrderQty = dt.Rows(0)("Q_ORD").ToString.Trim()
                    Catch ex As Exception

                    End Try
                    Try
                        ItemUOM = "EA"
                    Catch ex As Exception

                    End Try
                    Try
                        Pack = dt.Rows(0)("PACK").ToString.Trim()
                    Catch ex As Exception

                    End Try
                    Try
                        Units = "1"
                    Catch ex As Exception

                    End Try
                    Try
                        ItemUnitPrice = dt.Rows(0)("COST").ToString.Trim()
                    Catch ex As Exception

                    End Try
                    Try
                        Total = dt.Rows(0)("Ext_COSt").ToString.Trim()
                    Catch ex As Exception

                    End Try

                    If Total <> (Pack * ItemUnitPrice * OrderQty) Then
                        Total = Pack * ItemUnitPrice * OrderQty
                        Update_PO_Requisition_Details(RowNumber, "Ext_COSt", Total)
                    End If


                    Try
                        _Vendor = dt.Rows(0)("Vendor_Code")
                    Catch ex As Exception

                    End Try

                    Try
                        _LocationID = lblLocationID.Text
                    Catch ex As Exception

                    End Try

                    Try
                        _Buyer = dt.Rows(0)("Buyer")
                    Catch ex As Exception

                    End Try

                End If
                ''[LocationID],[Product] ,[Type] ,[PONumber] ,[QOH] ,[PreSold] ,[Requested] ,
                '[ColorVerity] ,[PoNotes] ,[VendorNotes] ,[ShipDate] ,[QtyOrdered] ,[Pack] ,
                '[Total] ,[Cost] ,[Vendor] ,[BuyStatus] ,[Employee] ,[RowNumber]
                '
                SubTotal = Total

                '_total = _total + SubTotal

                If _Buyer = "" Then
                    EmployeeID = Session("EmployeeID")
                    _Buyer = EmployeeID
                End If

                If _Vendor.Trim <> "" And PONO = "" Then
                    Dim OrderLineNumber As Integer = AddItemDetailsCustomisedGrid(RowNumber)
                    PONOUpdate(InLineNumber, RowNumber, _Buyer, sendemail)
                End If


            End If



        Next


        Dim dt2 As New DataTable
        dt2 = PODETAILSBYInLineNumber(InLineNumber)
        If dt2.Rows.Count <> 0 Then

            Dim fn As Integer = 0

            For fn = 0 To dt2.Rows.Count - 1
                Dim VendorID As String = ""



                VendorID = dt2.Rows(fn)(0)

                Dim dt3 As New DataTable
                dt3 = PODETAILSBYInLineNumberAndVendor(InLineNumber, VendorID)

                Dim PopOrderNo As New DAL.CustomOrder()
                Dim rs As SqlDataReader
                rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextPurchaseOrderNumber")
                While rs.Read()
                    _PurchaseOrderNumber = rs("NextNumberValue")
                End While
                rs.Close()
                lblmsg.Text = lblmsg.Text & " _PurchaseOrderNumber:" & _PurchaseOrderNumber & " "
                Dim fm As Integer = 0
                _total = 0

                For fm = 0 To dt3.Rows.Count - 1
                    Dim PurchaseLineNumber As Integer
                    PurchaseLineNumber = dt3.Rows(fm)("PurchaseLineNumber")
                    PODETAILSPurchaseNumberUPdate(PurchaseLineNumber, _PurchaseOrderNumber)
                    PONOUpdate2(InLineNumber, _PurchaseOrderNumber, VendorID)

                    _total = _total + dt3.Rows(fm)("Total")
                    _LocationID = dt3.Rows(fm)("LocationID")
                Next

                Dim objsave As New PurchaseModuleUI.PurchaseOrder

                Try
                    EmployeeID = Session("EmployeeID")
                Catch ex As Exception
                    EmployeeID = "Admin"
                End Try

                Dim drpPurchaseTransaction As String = "POF"
                Dim lblPurchaseOrderDate As String = Date.Now
                Dim txtArrivalDate As String = Date.Now
                Dim drpEmployeeID As String = EmployeeID
                Dim txtVendorTemp As String = VendorID
                Dim txtSubtotal As String = _total
                Dim txtTaxPercent As String = "0.00"
                Dim txtTax As String = "0.00"

                Dim txtDelivery As String = "0.00"
                Dim txtTotal As String = _total
                Dim drpShipMethod As String = "Ship"
                Dim chkPosted As Boolean = True
                Dim cmblocationid As String = _LocationID
                Dim drpShiplocation As String = _LocationID
                Dim txtInternalNotes As String = ""
                Dim drpPaymentType As String = "Check"
                Dim txtTrackingno As String = ""
                Dim txtOrderno As String = ""

                If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, _PurchaseOrderNumber, 0, drpPurchaseTransaction, lblPurchaseOrderDate, txtArrivalDate, drpEmployeeID, "", "DEFAULT", txtVendorTemp, "USD", 1, txtSubtotal, 0, 0, txtTaxPercent.Replace("%", ""), txtTax, txtSubtotal, txtDelivery, 0, txtTotal, drpShipMethod, chkPosted, Date.Now, cmblocationid, drpShiplocation, txtInternalNotes, drpPaymentType, txtTrackingno, txtOrderno) Then
                    'Response.Redirect("PO.aspx?PurchaseOrderNumber=" & _PurchaseOrderNumber)
                    All_PurchaseOrderNumber = All_PurchaseOrderNumber & " :  " & _PurchaseOrderNumber & "  "
                    Dim objemail As New clsemailtovendor
                    objemail.CompanyID = Me.CompanyID
                    objemail.DivisionID = Me.DivisionID
                    objemail.DepartmentID = Me.DepartmentID

                    If sendemail Then
                        objemail.EmailNotifications(_PurchaseOrderNumber)
                        objemail.txtfrom.Text = "info@quickflora.com"
                        EmailSendingWithhCC(objemail.txtemailsubject.Text, objemail.divEmailContent.Text, objemail.txtfrom.Text, objemail.txtto.Text, objemail.txtcc.Text, objemail._vendorid)

                    End If



                End If

            Next

        End If


        If IsNumeric(_PurchaseOrderNumber) = True Then
        End If

        lblmsg.Text = lblmsg.Text & "and  PO Generated: " & _PurchaseOrderNumber & " "
        ''All_PurchaseOrderNumber






    End Sub





    Private Sub gvItemsList_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvItemsList.RowEditing
        ' gvItemsList.EditIndex = e.NewEditIndex

        'lblGroupCode
        'lblItemColor
        'lblItemDescription
        Dim ItemDescription As String = ""
        Dim lblItemDescription As Label
        lblItemDescription = gvItemsList.Rows(e.NewEditIndex).FindControl("lblItemDescription")
        ItemDescription = (lblItemDescription.Text)
        lblmsg.Text = lblmsg.Text & " " & lblItemDescription.Text

        Dim ItemColor As String = ""
        Dim lblItemColor As Label
        lblItemColor = gvItemsList.Rows(e.NewEditIndex).FindControl("lblItemColor")
        ItemColor = (lblItemColor.Text)
        lblmsg.Text = lblmsg.Text & " " & lblItemColor.Text


        Dim GroupCode As String = ""
        Dim lblGroupCode As Label
        lblGroupCode = gvItemsList.Rows(e.NewEditIndex).FindControl("lblGroupCode")
        GroupCode = (lblGroupCode.Text)
        lblmsg.Text = lblmsg.Text & " " & lblGroupCode.Text


        Dim ArrivalDate As String = ""
        Dim lblArrivalDate As Label
        lblArrivalDate = gvItemsList.Rows(e.NewEditIndex).FindControl("lblArrivalDate")
        ArrivalDate = (lblArrivalDate.Text)
        lblmsg.Text = lblmsg.Text & " " & lblArrivalDate.Text

        Dim Location As String = ""
        Dim LBLLocation As Label
        LBLLocation = gvItemsList.Rows(e.NewEditIndex).FindControl("LBLLocation")
        Location = (LBLLocation.Text)
        lblmsg.Text = lblmsg.Text & " " & LBLLocation.Text


        Dim ItemID As String = ""
        Dim txtItemID As Label
        txtItemID = gvItemsList.Rows(e.NewEditIndex).FindControl("txtItemID")
        ItemID = (txtItemID.Text)
        lblmsg.Text = lblmsg.Text & " " & txtItemID.Text

        Dim ItemQty As Integer
        Dim txtstem As Label
        txtstem = gvItemsList.Rows(e.NewEditIndex).FindControl("txtstem")
        ItemQty = CInt(txtstem.Text)
        lblmsg.Text = lblmsg.Text & " " & txtstem.Text

        Dim ItemQtyRecevied As Integer
        Dim txtrecevied As TextBox
        txtrecevied = gvItemsList.Rows(e.NewEditIndex).FindControl("txtrecevied")
        ItemQtyRecevied = CInt(txtrecevied.Text)
        lblmsg.Text = lblmsg.Text & " " & ItemQtyRecevied & " "

        Dim price As Decimal = 0
        Dim txtprice As Label
        txtprice = gvItemsList.Rows(e.NewEditIndex).FindControl("txtprice")
        Try
            price = (txtprice.Text)
        Catch ex As Exception

        End Try
        lblmsg.Text = lblmsg.Text & " " & price & " "

        Dim rs As SqlDataReader
        Dim OrderNo As String = ""
        Try
            EmployeeID = Session("EmployeeID")
        Catch ex As Exception

        End Try

        Dim PopOrderNo As New DAL.CustomOrder()
        rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextRequisitionNumber")
        While rs.Read()
            OrderNo = rs("NextNumberValue")
        End While
        rs.Close()

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String

        If OrderNo <> "" Then
            qry = "insert into PO_Requisition_Details ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE],[PONO],HDStatus,ShipDate,Location,Type,ProductName,Vendor_Remarks)" _
             & " values(@CompanyID ,@DivisionID ,@DepartmentID ,@OrderNo ,@Product ,@QOH ,@DUMP ,@Q_REQ ,@PRESOLD ,@COLOR_VARIETY ,@REMARKS ,@Q_ORD ,@PACK ,@COST ,@Ext_COSt ,@Vendor_Code ,@Buyer ,@Status ,@Q_Recv ,@ISSUE,'','Entry Completed',@ShipDate,@Location,@Type,@ProductName,'') "

            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)
            '@ShipDate
            'ArrivalDate
            'Location
            'Type
            com.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar)).Value = ItemID
            com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = GroupCode
            com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = Location
            com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = ArrivalDate
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = OrderNo
            com.Parameters.Add(New SqlParameter("@Product", SqlDbType.NVarChar)).Value = ItemID
            com.Parameters.Add(New SqlParameter("@QOH", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@DUMP", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@Q_REQ", SqlDbType.NVarChar)).Value = ItemQtyRecevied
            com.Parameters.Add(New SqlParameter("@PRESOLD", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@COLOR_VARIETY", SqlDbType.NVarChar)).Value = ItemColor
            com.Parameters.Add(New SqlParameter("@REMARKS", SqlDbType.NVarChar)).Value = ItemDescription
            com.Parameters.Add(New SqlParameter("@Q_ORD", SqlDbType.NVarChar)).Value = ItemQtyRecevied
            com.Parameters.Add(New SqlParameter("@PACK", SqlDbType.NVarChar)).Value = "1"
            com.Parameters.Add(New SqlParameter("@COST", SqlDbType.NVarChar)).Value = price
            com.Parameters.Add(New SqlParameter("@Ext_COSt", SqlDbType.NVarChar)).Value = (ItemQtyRecevied * price)
            com.Parameters.Add(New SqlParameter("@Vendor_Code", SqlDbType.NVarChar)).Value = Location
            com.Parameters.Add(New SqlParameter("@Buyer", SqlDbType.NVarChar)).Value = EmployeeID
            com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = "No Action"
            com.Parameters.Add(New SqlParameter("@Q_Recv", SqlDbType.NVarChar)).Value = "0"
            com.Parameters.Add(New SqlParameter("@ISSUE", SqlDbType.NVarChar)).Value = ""

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()



            InsertIntoHeader(OrderNo, Location, GroupCode, (ItemQty * price), ArrivalDate, ArrivalDate, EmployeeID, "Local_Truck")

            lblmsg.Text = "Requisition Generated: " & OrderNo & " "
            'Response.Redirect("RequisitionOrderList.aspx")

            Dim InLineNumber As Integer = 0
            InLineNumber = BatchPOSampleRowInLineNumber(OrderNo)
            sendpo(False, Location, InLineNumber)
            ' Response.Redirect("RequisitionOrderList.aspx")
        End If


    End Sub



    Public Function BatchPOSampleRowInLineNumber(ByVal OrderNo As Integer) As Integer
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " SELECT  InLineNumber  "
        ssql = ssql & " FROM PO_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND  OrderNo =" & OrderNo
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)(0)
        End If

        Return 0
    End Function



    Public Function InsertIntoHeader(ByVal OrderNo As String, ByVal cmblocationid As String, ByVal drpType As String, ByVal txttotal As String, ByVal txtshipdate As String, ByVal txtarrivedate As String, ByVal txtorderby As String, ByVal ShipMethodID As String) As String
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into PO_Requisition_Header(ShipMethodID,InventoryOrigin, CompanyID, DivisionID, DepartmentID, OrderNo,Location,Remarks,Status,Type,LastChangeDateTime,LastChangeBy,TotalAmount,ShipDate,ArriveDate,OrderPlacedDate,ReceivedOnDate,OrderBy,ReceivedBy) " _
             & " values(@ShipMethodID,@InventoryOrigin,@CompanyID, @DivisionID, @DepartmentID, @OrderNo,@Location,@Remarks,@Status,@Type,@LastChangeDateTime,@LastChangeBy,@TotalAmount,@ShipDate,@ArriveDate,@OrderPlacedDate,@ReceivedOnDate,@OrderBy,@ReceivedBy)"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@ShipMethodID", SqlDbType.NVarChar)).Value = ShipMethodID
        com.Parameters.Add(New SqlParameter("@InventoryOrigin", SqlDbType.NVarChar)).Value = cmblocationid
        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@OrderNo", SqlDbType.NVarChar)).Value = OrderNo
        com.Parameters.Add(New SqlParameter("@Location", SqlDbType.NVarChar)).Value = "Corporate"
        com.Parameters.Add(New SqlParameter("@Remarks", SqlDbType.NVarChar)).Value = "Request ON Accept"
        com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = "Entry Completed"
        com.Parameters.Add(New SqlParameter("@Type", SqlDbType.NVarChar)).Value = drpType
        com.Parameters.Add(New SqlParameter("@LastChangeDateTime", SqlDbType.NVarChar)).Value = DateTime.Now.ToString()
        com.Parameters.Add(New SqlParameter("@LastChangeBy", SqlDbType.NVarChar)).Value = ""
        com.Parameters.Add(New SqlParameter("@TotalAmount", SqlDbType.NVarChar)).Value = txttotal
        com.Parameters.Add(New SqlParameter("@ShipDate", SqlDbType.NVarChar)).Value = txtshipdate
        com.Parameters.Add(New SqlParameter("@ArriveDate", SqlDbType.NVarChar)).Value = txtarrivedate
        com.Parameters.Add(New SqlParameter("@OrderPlacedDate", SqlDbType.NVarChar)).Value = DateTime.Now.ToString()
        com.Parameters.Add(New SqlParameter("@ReceivedOnDate", SqlDbType.NVarChar)).Value = DateTime.Now.ToString()
        com.Parameters.Add(New SqlParameter("@OrderBy", SqlDbType.NVarChar)).Value = txtorderby
        com.Parameters.Add(New SqlParameter("@ReceivedBy", SqlDbType.NVarChar)).Value = txtorderby

        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()




        Return ""
    End Function

    Public Function getitemname(ByVal id As String) As String
        Dim itemname As String = ""
        Dim sql As String = "select ItemName from [InventoryItems] where  ItemID=@id AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand(sql, myCon)
        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)
        myCommand.Parameters.AddWithValue("@id", id)

        Dim da As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable
        da.Fill(dt)
        If dt.Rows.Count <> 0 Then
            Try
                itemname = dt.Rows(0)("ItemName").ToString()
            Catch ex As Exception

            End Try

        End If


        Return itemname
    End Function


    Public Function BatchPOSampleRow(ByVal RowNumber As Integer) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM PO_Requisition_Details Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND InLineNumber =" & RowNumber
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Dim PurchaseNumber As String = ""
    Dim ItemID As String = ""
    Dim Description As String = ""
    Dim Color As String = ""

    Dim OrderQty As Integer = 1
    Dim ItemUOM As String = ""
    Dim Comments As String = ""
    Dim Pack As Integer = 1
    Dim Units As Integer = 1

    Dim ItemUnitPrice As Decimal = 0
    Dim SubTotal As Decimal = 0
    Dim Total As Decimal = 0

    Dim _Vendor As String = ""
    Dim _LocationID As String = ""
    Dim _Buyer As String = ""

    Public Sub sendpo(ByVal sendemail As Boolean, ByVal __LocationID As String, ByVal RowNumber As Integer)

        _LocationID = __LocationID

        Dim _PurchaseOrderNumber As String = ""
        Dim InLineNumber As String = ""

        Dim All_PurchaseOrderNumber As String = "PO # "

        InLineNumber = Date.Now.Year & Date.Now.Month & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond

        Dim _total As Decimal = 0

        If True Then
            Dim lblLocationID As New Label
            lblLocationID.Text = _LocationID
            'LBLMSG.Text = LBLMSG.Text & " Reached in checked:" & RowNumber

            Dim dt As New DataTable
            dt = BatchPOSampleRow(RowNumber)
            PurchaseNumber = InLineNumber

            ',[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE]

            Dim PONO As String = ""

            If dt.Rows.Count <> 0 Then
                Try
                    ItemID = dt.Rows(0)("Product")
                Catch ex As Exception

                End Try

                Try
                    PONO = dt.Rows(0)("PONO")
                Catch ex As Exception
                    PONO = ""
                End Try

                Try
                    Comments = dt.Rows(0)("REMARKS")
                Catch ex As Exception

                End Try
                Description = ""
                Try
                    Description = dt.Rows(0)("Vendor_Remarks")
                Catch ex As Exception
                    Description = ""
                End Try
                Try
                    Color = dt.Rows(0)("COLOR_VARIETY")
                Catch ex As Exception

                End Try
                Try
                    OrderQty = dt.Rows(0)("Q_ORD").ToString.Trim()
                Catch ex As Exception

                End Try
                Try
                    ItemUOM = "EA"
                Catch ex As Exception

                End Try
                Try
                    Pack = dt.Rows(0)("PACK").ToString.Trim()
                Catch ex As Exception

                End Try
                Try
                    Units = "1"
                Catch ex As Exception

                End Try
                Try
                    ItemUnitPrice = dt.Rows(0)("COST").ToString.Trim()
                Catch ex As Exception

                End Try
                Try
                    Total = dt.Rows(0)("Ext_COSt").ToString.Trim()
                Catch ex As Exception

                End Try

                If Total <> (Pack * ItemUnitPrice * OrderQty) Then
                    Total = Pack * ItemUnitPrice * OrderQty
                    Update_PO_Requisition_Details(RowNumber, "Ext_COSt", Total)
                End If


                Try
                    _Vendor = dt.Rows(0)("Vendor_Code")
                Catch ex As Exception

                End Try

                Try
                    _LocationID = lblLocationID.Text
                Catch ex As Exception

                End Try

                Try
                    _Buyer = dt.Rows(0)("Buyer")
                Catch ex As Exception

                End Try

            End If
            ''[LocationID],[Product] ,[Type] ,[PONumber] ,[QOH] ,[PreSold] ,[Requested] ,
            '[ColorVerity] ,[PoNotes] ,[VendorNotes] ,[ShipDate] ,[QtyOrdered] ,[Pack] ,
            '[Total] ,[Cost] ,[Vendor] ,[BuyStatus] ,[Employee] ,[RowNumber]
            '
            SubTotal = Total

            '_total = _total + SubTotal

            If _Buyer = "" Then
                EmployeeID = Session("EmployeeID")
                _Buyer = EmployeeID
            End If

            If _Vendor.Trim <> "" And PONO = "" Then
                Dim OrderLineNumber As Integer = AddItemDetailsCustomisedGrid(RowNumber)
                PONOUpdate(InLineNumber, RowNumber, _Buyer, sendemail)
            End If


        End If



        Dim dt2 As New DataTable
        dt2 = PODETAILSBYInLineNumber(InLineNumber)
        If dt2.Rows.Count <> 0 Then

            Dim fn As Integer = 0

            For fn = 0 To dt2.Rows.Count - 1
                Dim VendorID As String = ""



                VendorID = dt2.Rows(fn)(0)

                Dim dt3 As New DataTable
                dt3 = PODETAILSBYInLineNumberAndVendor(InLineNumber, VendorID)

                Dim PopOrderNo As New DAL.CustomOrder()
                Dim rs As SqlDataReader
                rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextPurchaseOrderNumber")
                While rs.Read()
                    _PurchaseOrderNumber = rs("NextNumberValue")
                End While
                rs.Close()

                Dim fm As Integer = 0
                _total = 0

                For fm = 0 To dt3.Rows.Count - 1
                    Dim PurchaseLineNumber As Integer
                    PurchaseLineNumber = dt3.Rows(fm)("PurchaseLineNumber")
                    PODETAILSPurchaseNumberUPdate(PurchaseLineNumber, _PurchaseOrderNumber)
                    PONOUpdate2(InLineNumber, _PurchaseOrderNumber, VendorID)

                    _total = _total + dt3.Rows(fm)("Total")
                    _LocationID = dt3.Rows(fm)("LocationID")
                Next

                Dim objsave As New PurchaseModuleUI.PurchaseOrder

                Try
                    EmployeeID = Session("EmployeeID")
                Catch ex As Exception
                    EmployeeID = "Admin"
                End Try

                Dim drpPurchaseTransaction As String = "POH"
                Dim lblPurchaseOrderDate As String = Date.Now
                Dim txtArrivalDate As String = Date.Now
                Dim drpEmployeeID As String = EmployeeID
                Dim txtVendorTemp As String = VendorID
                Dim txtSubtotal As String = _total
                Dim txtTaxPercent As String = "0.00"
                Dim txtTax As String = "0.00"

                Dim txtDelivery As String = "0.00"
                Dim txtTotal As String = _total
                Dim drpShipMethod As String = "Ship"
                Dim chkPosted As Boolean = True
                Dim cmblocationid As String = _LocationID
                Dim drpShiplocation As String = _LocationID
                Dim txtInternalNotes As String = ""
                Dim drpPaymentType As String = ""
                Dim txtTrackingno As String = ""
                Dim txtOrderno As String = ""

                If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, _PurchaseOrderNumber, 0, drpPurchaseTransaction, lblPurchaseOrderDate, txtArrivalDate, drpEmployeeID, "", "DEFAULT", txtVendorTemp, "USD", 1, txtSubtotal, 0, 0, txtTaxPercent.Replace("%", ""), txtTax, txtSubtotal, txtDelivery, 0, txtTotal, drpShipMethod, chkPosted, Date.Now, cmblocationid, drpShiplocation, txtInternalNotes, drpPaymentType, txtTrackingno, txtOrderno) Then
                    'Response.Redirect("PO.aspx?PurchaseOrderNumber=" & _PurchaseOrderNumber)
                    All_PurchaseOrderNumber = All_PurchaseOrderNumber & " :  " & _PurchaseOrderNumber & "  "
                    'Dim objemail As New clsemailtovendor
                    'objemail.CompanyID = Me.CompanyID
                    'objemail.DivisionID = Me.DivisionID
                    'objemail.DepartmentID = Me.DepartmentID

                    'If sendemail Then
                    '    objemail.EmailNotifications(_PurchaseOrderNumber)
                    '    objemail.txtfrom.Text = "claire@mccarthygroupflorists.com"
                    '    EmailSendingWithhCC(objemail.txtemailsubject.Text, objemail.divEmailContent.Text, objemail.txtfrom.Text, objemail.txtto.Text, objemail.txtcc.Text, objemail._vendorid)

                    'End If



                End If

            Next

        End If


        If IsNumeric(_PurchaseOrderNumber) = True Then
        End If

        lblmsg.Text = lblmsg.Text & "and  PO Generated: " & _PurchaseOrderNumber & " "
        ''All_PurchaseOrderNumber

        All_PurchaseOrderNumber = All_PurchaseOrderNumber & " has generated."

        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & " alert('" & All_PurchaseOrderNumber & "');" & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        ' strJs5 = onloadScript
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())




    End Sub



    Public Function AddItemDetailsCustomisedGrid(ByVal RowNumber As String) As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[AddPurchaseItemDetailsBatchPO]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure


        Dim P_RowNumber As New SqlParameter("@RowNumber", Data.SqlDbType.NVarChar)
        P_RowNumber.Value = RowNumber
        myCommand.Parameters.Add(P_RowNumber)

        Dim P_Buyer As New SqlParameter("@Buyer", Data.SqlDbType.NVarChar)
        P_Buyer.Value = _Buyer
        myCommand.Parameters.Add(P_Buyer)

        Dim p_Vendor As New SqlParameter("@Vendor", Data.SqlDbType.NVarChar)
        p_Vendor.Value = _Vendor
        myCommand.Parameters.Add(p_Vendor)

        Dim P_LocationID As New SqlParameter("@LocationID", Data.SqlDbType.NVarChar)
        P_LocationID.Value = _LocationID
        myCommand.Parameters.Add(P_LocationID)


        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)


        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        'Dim parameterOrderLineNumber As New SqlParameter("@OrderLineNumber ", Data.SqlDbType.Int)
        'parameterOrderLineNumber.Value = OLineNumber
        'myCommand.Parameters.Add(parameterOrderLineNumber)

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = PurchaseNumber
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim parameterItemID As New SqlParameter("@ItemID", Data.SqlDbType.NVarChar)
        parameterItemID.Value = ItemID
        myCommand.Parameters.Add(parameterItemID)

        Dim parameterDescription As New SqlParameter("@Description", Data.SqlDbType.NVarChar)
        parameterDescription.Value = Description
        myCommand.Parameters.Add(parameterDescription)

        Dim pComments As New SqlParameter("@Comments", Data.SqlDbType.NVarChar)
        pComments.Value = Comments
        myCommand.Parameters.Add(pComments)

        'Color
        Dim pColor As New SqlParameter("@Color", Data.SqlDbType.NVarChar)
        pColor.Value = Color
        myCommand.Parameters.Add(pColor)

        Dim parameterOrderQty As New SqlParameter("@OrderQty", Data.SqlDbType.Float)
        parameterOrderQty.Value = OrderQty
        myCommand.Parameters.Add(parameterOrderQty)

        Dim parameterItemUOM As New SqlParameter("@ItemUOM", Data.SqlDbType.NVarChar)
        parameterItemUOM.Value = ItemUOM
        myCommand.Parameters.Add(parameterItemUOM)




        Dim pPack As New SqlParameter("@Pack", Data.SqlDbType.Int)
        pPack.Value = Pack
        myCommand.Parameters.Add(pPack)

        Dim pUnits As New SqlParameter("@Units", Data.SqlDbType.Int)
        pUnits.Value = Units
        myCommand.Parameters.Add(pUnits)

        Dim parameterItemUnitPrice As New SqlParameter("@ItemUnitPrice", Data.SqlDbType.Money)
        parameterItemUnitPrice.Value = ItemUnitPrice
        myCommand.Parameters.Add(parameterItemUnitPrice)



        Dim pSubTotal As New SqlParameter("@SubTotal", Data.SqlDbType.Money)
        pSubTotal.Value = SubTotal
        myCommand.Parameters.Add(pSubTotal)

        Dim parameterSubTotal As New SqlParameter("@Total", Data.SqlDbType.Money)
        parameterSubTotal.Value = Total
        myCommand.Parameters.Add(parameterSubTotal)



        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramReturnValue)

        myCon.Open()

        myCommand.ExecuteNonQuery()


        Dim OutPutValue As Integer
        OutPutValue = Convert.ToInt32(paramReturnValue.Value)
        myCon.Close()
        Return OutPutValue

    End Function


    Public Function Update_PO_Requisition_Details(ByVal RowNumber As Integer, ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Update PO_Requisition_Details SET  " & name & " =@value   Where InLineNumber = @RowNumber"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@value", SqlDbType.NVarChar)).Value = value
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar)).Value = RowNumber
            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function PODETAILSBYInLineNumber(ByVal InLineNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  Vendor  "
        ssql = ssql & " FROM Enterprise.dbo.PurchaseDetail Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [PurchaseNumber] ='" & InLineNumber & "'  Group by Vendor  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Public Function PODETAILSBYInLineNumberAndVendor(ByVal InLineNumber As String, ByVal Vendor As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM Enterprise.dbo.PurchaseDetail Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [PurchaseNumber] ='" & InLineNumber & "' AND [Vendor] ='" & Vendor & "' "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Public Function PODETAILSPurchaseNumberUPdate(ByVal PurchaseLineNumber As Integer, ByVal PurchaseNumber As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE Enterprise.dbo.PurchaseDetail set PurchaseNumber=@PurchaseNumber Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And PurchaseLineNumber=@PurchaseLineNumber  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar, 36)).Value = PurchaseNumber
            com.Parameters.Add(New SqlParameter("@PurchaseLineNumber", SqlDbType.BigInt)).Value = PurchaseLineNumber


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return msg
        End Try

        Return True
    End Function

    ''(InLineNumber, RowNumber)

    Public Function PONOUpdate(ByVal InLineNumber As String, ByVal RowNumber As String, ByVal _Buyer As String, ByVal sendemail As Boolean) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PO_Requisition_Details set Buyer=@Buyer, PONO=@InLineNumber,Status=@Status,TransmissionBy=@TransmissionBy Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And InLineNumber=@RowNumber  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.NVarChar, 36)).Value = InLineNumber
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar, 36)).Value = RowNumber
            com.Parameters.Add(New SqlParameter("@Buyer", SqlDbType.NVarChar)).Value = _Buyer

            If sendemail Then
                com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = "Pending Email"
                com.Parameters.Add(New SqlParameter("@TransmissionBy", SqlDbType.NVarChar)).Value = ""
            Else
                com.Parameters.Add(New SqlParameter("@Status", SqlDbType.NVarChar)).Value = "Bought"
                com.Parameters.Add(New SqlParameter("@TransmissionBy", SqlDbType.NVarChar)).Value = "Accept Button"
            End If


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return msg
        End Try

        Return True
    End Function

    Dim ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")


    Public Function PONOUpdate2(ByVal InLineNumber As String, ByVal PONO As String, ByVal Venor As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PO_Requisition_Details set PONO=@PONO Where  PONO=@InLineNumber AND Vendor_Code=@Venor AND CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.NVarChar)).Value = InLineNumber
        com.Parameters.Add(New SqlParameter("@PONO", SqlDbType.NVarChar)).Value = PONO
        com.Parameters.Add(New SqlParameter("@Venor", SqlDbType.NVarChar)).Value = Venor

        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()



        Return True
    End Function

    Private Sub InventoryFormAdd_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender


        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "  " & "doOnLoad();doOnLoadtypeheade();" & " " & vbCrLf
        'onloadScript = onloadScript & "  " & " $('.sticky-header').floatThead();" & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS1 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "function doOnLoad() {" & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS2 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        onloadScript = onloadScript & "  " & "function doOnLoadtypeheade() {" & " " & vbCrLf
        ''onloadScript = onloadScript & "  " & "alert('hi 2');" & " " & vbCrLf
        'onloadScript = onloadScript & "  " & strJS3 & " " & vbCrLf
        'onloadScript = onloadScript & "  " & strJs5 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCallnew", onloadScript.ToString())
    End Sub



    Public Function EmaillAddd() As DataTable
        Dim constr As String = ConnectionString
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "select  FromEmail, CCEmail, BCCEmail  from [HomePageManagement]   where   CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(com)
        da.Fill(dt)
        Return dt
    End Function



    Public Function FillDetailsVendor(ByVal VendorID As String) As DataTable
        Dim constr As String = ConnectionString
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from VendorInformation where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and VendorID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 50)).Value = VendorID

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            '' HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function



    Public Function EmployeeEmailAddress() As String

        Try
            '' EmployeeID = Session("EmployeeID")
        Catch ex As Exception
            EmployeeID = "Admin"
        End Try

        Dim email As String = ""

        Dim dt As New DataTable
        Dim constr As String = ConnectionString
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select [EmployeeEmailAddress],ISNULL(SendPoCopy,0) from   [PayrollEmployees] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [EmployeeID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = EmployeeID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            If dt.Rows.Count <> 0 Then

                Try
                    email = dt.Rows(0)(0)
                Catch ex As Exception

                End Try

                Dim SendPoCopy As Boolean = False
                Try

                    Try
                        SendPoCopy = dt.Rows(0)(1)
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try

                If SendPoCopy Then
                Else
                    email = ""
                End If

            End If



        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            '' HttpContext.Current.Response.Write(msg)
            Return email
        End Try
        Return email
    End Function




    Public Sub EmailSendingWithhCC(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String, ByVal CCAddress As String, ByVal _vendorid As String)
        'Exit Sub
        Dim vendoremail As String
        Dim vendoremail2 As String = ""
        Dim vendoremail3 As String = ""

        If _vendorid <> "" Then
            Dim dtvendor As DataTable
            dtvendor = FillDetailsVendor(_vendorid)
            vendoremail = ""
            If dtvendor.Rows.Count <> 0 Then
                Try
                    vendoremail = dtvendor.Rows(0)("VendorEmail")
                Catch ex As Exception
                    'vendoremail = "imtiyazsir@gmail.com"
                End Try

                Try
                    vendoremail2 = dtvendor.Rows(0)("VendorEmail2")
                Catch ex As Exception
                    'vendoremail = "imtiyazsir@gmail.com"
                End Try

                Try
                    vendoremail3 = dtvendor.Rows(0)("VendorEmail3")
                Catch ex As Exception
                    'vendoremail = "imtiyazsir@gmail.com"
                End Try

            End If
        End If


        Dim _EmployeeEmailAddress As String = ""
        _EmployeeEmailAddress = EmployeeEmailAddress()

        lblerrortestmail.Text = "In Functon reached"
        lblerrortestmail.Visible = False


        Dim BCCAddress As String = ""
        Dim dtemail As New DataTable
        dtemail = EmaillAddd()
        If dtemail.Rows.Count <> 0 Then
            Try
                FromAddress = dtemail.Rows(0)("FromEmail")
            Catch ex As Exception

            End Try
            Try
                CCAddress = dtemail.Rows(0)("CCEmail")
            Catch ex As Exception

            End Try
            Try
                BCCAddress = dtemail.Rows(0)("BCCEmail")
            Catch ex As Exception

            End Try
        End If

        Dim mMailMessage As New MailMessage()


        ' Set the sender address of the mail message

        If FromAddress.Trim <> "" Then
            mMailMessage.From = New MailAddress(FromAddress)
        Else
            mMailMessage.From = New MailAddress("support@quickflora.com")
        End If
        ' Set the recepient address of the mail message
        If ToAddress.Trim <> "" Then
            mMailMessage.To.Add(New MailAddress(ToAddress))
        Else
            ''Exit Sub
            mMailMessage.To.Add(New MailAddress(FromAddress))
        End If

        If vendoremail2.Trim <> "" Then
            mMailMessage.To.Add(New MailAddress(vendoremail2))
        End If

        If vendoremail3.Trim <> "" Then
            mMailMessage.To.Add(New MailAddress(vendoremail3))
        End If


        If CCAddress.Trim <> "" Then
            mMailMessage.CC.Add(New MailAddress(CCAddress))
        End If

        If _EmployeeEmailAddress.Trim <> "" Then
            mMailMessage.CC.Add(New MailAddress(_EmployeeEmailAddress))
        End If


        'If Me.CompanyID.ToLower() = "mccarthyg" Then
        '    mMailMessage.CC.Add(New MailAddress("pat@mccarthygroupflorists.com"))
        'End If

        If BCCAddress.Trim <> "" Then
            mMailMessage.Bcc.Add(New MailAddress(BCCAddress))
        End If


        ''mMailMessage.Bcc.Add(New MailAddress("gaurav@quickflora.com"))
        ''mMailMessage.Bcc.Add(New MailAddress("alex@quickflora.com"))
        ''mMailMessage.Bcc.Add(New MailAddress("imy@quickflora.com"))


        'mMailMessage.Bcc.Add(New MailAddress("qfclientorders@sunflowertechnologies.com"))
        'Set the subject of the mail message
        mMailMessage.Subject = OrderPlacedSubject.ToString()
        ' Set the body of the mail message
        mMailMessage.Body = OrderPlacedContent.ToString()

        ' Set the format of the mail message body as HTML
        mMailMessage.IsBodyHtml = True


        ' Set the priority of the mail message to normal
        mMailMessage.Priority = MailPriority.Normal

        ' Instantiate a new instance of SmtpClient


        newmailsending(mMailMessage)

        lblerrortestmail.Text = "In Functon complete"

    End Sub


    Dim lblerrortestmail As New Label

    Public Sub newmailsending(ByVal Email As MailMessage)

        Dim QFmail As New com.quickflora.qfscheduler.QFPrintService
        QFmail.newmailsending(Email.From.ToString, Email.To.ToString, Email.CC.ToString, "", Email.Subject.ToString, Email.Body.ToString, CompanyID, DivisionID, DepartmentID)

        Exit Sub
    End Sub

End Class
