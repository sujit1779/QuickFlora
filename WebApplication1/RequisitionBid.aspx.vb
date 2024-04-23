Imports System.Data
Imports System.Data.SqlClient


Partial Class RequisitionBid
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
        If Not Page.IsPostBack Then

            SetLocationIDdropdown()

            Dim tm As DateTime
            tm = DateTime.Now
            txtDateTo.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            txtstart.Text = tm.Month & "/" & tm.Day & "/" & tm.Year
            tm = tm.AddDays(7)
            txtend.Text = tm.Month & "/" & tm.Day & "/" & tm.Year

            GetInventoryItemsList()

            Try
                If Session("Grower") = "True" Then
                    'HyperLink1.Visible = False
                End If
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub chkrequested_CheckedChanged(sender As Object, e As EventArgs) Handles chkrequested.CheckedChanged
        GetInventoryItemsList()
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

    Public Function frmt(ByVal mn As Decimal) As String
        Try
            Return Format(mn, "###0.00").ToString()
        Catch ex As Exception
            Return mn.ToString
        End Try


    End Function

    Public Function GetInventoryItemsListNew() As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetRequisitionBid]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("@ShipDate", txtstart.Text)

                '@Committed


                'ArrivalDate
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
                    ' Debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Public Function condate(ByVal s As String) As String
        Dim dt As DateTime
        Try
            dt = s
            s = dt.ToShortDateString
        Catch ex As Exception

        End Try
        If s = "1/1/1900" Then
            s = ""
        End If
        Return s
    End Function


    Public Function GetInventoryItemsListNewway1() As DataSet

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetInventoryItemsListAvailbilityAjaxBeta]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("@CompanyID", CompanyID)
                Command.Parameters.AddWithValue("@DivisionID", DivisionID)
                Command.Parameters.AddWithValue("@DepartmentID", DepartmentID)


                Command.Parameters.AddWithValue("@Location", cmblocationid.SelectedValue)
                Command.Parameters.AddWithValue("@ArrivalDate", txtDateTo.Text)
                Command.Parameters.AddWithValue("@ExcludeArrivalDate", True)
                ',@Condition NVARCHAR (36)=NULL, @fieldName NVARCHAR (36)=NULL, @fieldexpression NVARCHAR (400)=NULL
                Command.Parameters.AddWithValue("@Condition", "")
                Command.Parameters.AddWithValue("@fieldexpression", txtSearchValue.Text)

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
                    ' Debug = ex.Message
                    Return Nothing

                End Try

            End Using
        End Using

    End Function

    Private Sub GetInventoryItemsListNewway()

        Dim ds As New DataSet
        ds = GetInventoryItemsListNewway1()
        gvItemsList.DataSource = ds
        gvItemsList.DataBind()
        Try

        Catch ex As Exception

        End Try

        If ds.Tables(0).Rows.Count > 0 Then

            lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
        Else
            lblInfo.Text = "0 records found"
        End If

    End Sub



    Private Sub GetInventoryItemsList()

        Dim ds As New DataSet
        ds = GetInventoryItemsListNew()
        gvItemsList.DataSource = ds
        gvItemsList.DataBind()
        Try

        Catch ex As Exception

        End Try

        If ds.Tables(0).Rows.Count > 0 Then

            lblInfo.Text = ds.Tables(0).Rows.Count.ToString + " records found"
        Else
            lblInfo.Text = "0 records found"
        End If

    End Sub

    Protected Sub gvItemsList_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvItemsList.PageIndexChanging

        gvItemsList.PageIndex = e.NewPageIndex
        GetInventoryItemsList()

        'txtDateTo 
        'txtstart 
        'txtend 


    End Sub

    Protected Sub gvItemsList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvItemsList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim txtItemID As Label = e.Row.FindControl("txtItemID")

            Dim lblShipDate As Label = e.Row.FindControl("lblShipDate")

            Dim lblLocation As Label = e.Row.FindControl("lblLocation")



            Dim txtVendorBidQty As TextBox = e.Row.FindControl("txtVendorBidQty")
            Dim txtVendorBidPrice As TextBox = e.Row.FindControl("txtVendorBidPrice")
            txtVendorBidQty.Attributes.Add("onKeyUp", "allnumeric(this)")
            txtVendorBidPrice.Attributes.Add("onKeyUp", "allnumeric2(this)")

            txtVendorBidQty.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtVendorBidQty.Attributes.Add("onblur", "Saveitem(this,'" & txtItemID.Text & "','" & EmployeeID & "','" & lblLocation.Text & "','" & lblShipDate.Text & "','BidQty','" & txtVendorBidQty.Text & "')")

            txtVendorBidPrice.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtVendorBidPrice.Attributes.Add("onblur", "Saveitem(this,'" & txtItemID.Text & "','" & EmployeeID & "','" & lblLocation.Text & "','" & lblShipDate.Text & "','Price','" & txtVendorBidPrice.Text & "')")


        End If
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
        If txtitemsearch.Text.Trim <> "" Then
            drpSearchCondition.SelectedValue = "="
            drpSearchFor.SelectedValue = "ItemID"
            txtSearchValue.Text = txtitemsearch.Text.Trim
            txtitemsearch.Text = ""
            GetInventoryItemsListNewway()

        Else
            GetInventoryItemsList()
        End If



    End Sub

    'Protected Sub btnSUbmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSUbmit.Click
    '    Response.Redirect("InventoryForm.aspx")
    'End Sub

    Private Sub cmblocationid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmblocationid.SelectedIndexChanged
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

End Class

