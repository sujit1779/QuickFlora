Option Strict Off
Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common
Imports PayPal.Payments.Common.Utility
Imports PayPal.Payments.Transactions
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net


Imports System.Text

Imports Amazon
Imports Amazon.EC2
Imports Amazon.EC2.Model
Imports Amazon.SimpleDB
Imports Amazon.SimpleDB.Model
Imports Amazon.S3
Imports Amazon.S3.Model
Imports Amazon.SimpleEmail
Imports Amazon.SimpleEmail.Model



Partial Class BatchPO
    Inherits System.Web.UI.Page


    Dim ConnectionString As String = ""
    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim ShipMethod As String = ""
    Dim FromDate As String = ""
    Dim ToDate As String = ""
    Dim fieldName As String = ""
    Dim Condition As String = ""
    Dim fieldexpression As String = ""
    Dim AllDate As Integer


    Protected Sub OrderHeaderGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles OrderHeaderGrid.PageIndexChanging

        OrderHeaderGrid.PageIndex = e.NewPageIndex

        SortExpression = "DEFAULT"
        fillitems("1")
    End Sub

    Dim strJS1 As String = ""
    Dim strJS2 As String = ""
    Dim strJS3 As String = ""
    Dim strJs5 As String = ""
    Dim myCalendar As String



    Private Sub OrderHeaderGrid_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles OrderHeaderGrid.PreRender

        ' adds scope attribute
        OrderHeaderGrid.UseAccessibleHeader = True
        'adds <thead> and <tbody> elements
        Try
            OrderHeaderGrid.HeaderRow.TableSection = TableRowSection.TableHeader
            OrderHeaderGrid.HeaderRow.CssClass = "table-fixed-header"
        Catch ex As Exception

        End Try


        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "  " & "doOnLoad();doOnLoadtypeheade();" & " " & vbCrLf
        onloadScript = onloadScript & "  " & " $('.sticky-header').floatThead();" & " " & vbCrLf
        'onloadScript = onloadScript & "  " & strJS1 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "function doOnLoad() {" & " " & vbCrLf
        'onloadScript = onloadScript & "  " & strJS2 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        onloadScript = onloadScript & "  " & "function doOnLoadtypeheade() {" & " " & vbCrLf
        ''onloadScript = onloadScript & "  " & "alert('hi 2');" & " " & vbCrLf
        'onloadScript = onloadScript & "  " & strJS3 & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJs5 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCallnew", onloadScript.ToString())

    End Sub

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


    Public Function Updatetotal(ByVal RowNumber As Integer, ByVal Ext_COSt As Decimal) As Boolean
        'HttpContext.Current.Response.Write(Ext_COSt)
        'HttpContext.Current.Response.Write("-")
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Update PO_Requisition_Details SET   Ext_COSt=@Ext_COSt   Where  InLineNumber = @InLineNumber and CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@Ext_COSt", SqlDbType.Money)).Value = Ext_COSt
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@InLineNumber", SqlDbType.BigInt)).Value = RowNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Private Sub OrderHeaderGrid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles OrderHeaderGrid.RowDataBound

        Dim txtRowNumber As New TextBox

        Dim lblLocationID As New Label
        Dim lblProduct As New Label
        Dim lblProductName As New Label

        'Dim lblType As New Label
        Dim lblPONumber As New Label

        Dim txtQOH As New Label
        Dim txtPreSold As New Label
        Dim txtRequested As New Label

        Dim txtColorVerity As New TextBox
        Dim lblPoNotes As New TextBox
        Dim lblVendorNotes As New TextBox
        Dim lblShipDate As New TextBox
        Dim lblQtyOrdered As New TextBox
        Dim lblPack As New TextBox
        Dim lblCost As New TextBox
        Dim lblTotal As New TextBox
        Dim lblVendor As New TextBox
        Dim lblVendor_Remarks As New TextBox

        If e.Row.RowType = DataControlRowType.DataRow Then

            txtRowNumber = e.Row.FindControl("txtRowNumber")

            e.Row.Attributes("id") = txtRowNumber.Text

            lblProduct = e.Row.FindControl("lblProduct")
            'lblProductName = e.Row.FindControl("lblProductName")
            'lblProductName.Text = getitemname(lblProduct.Text.Trim)


            Dim lblBuyStatus As New Label
            Dim drpPOStatus As New DropDownList



            lblBuyStatus = e.Row.FindControl("lblBuyStatus")

            drpPOStatus = e.Row.FindControl("drpPOStatus")

            lblVendor_Remarks = e.Row.FindControl("lblVendor_Remarks")
            lblVendor_Remarks.Attributes.Add("onfocus", "myFocusFunction(this)")
            lblVendor_Remarks.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'Vendor_Remarks','" & lblVendor_Remarks.Text & "','" & drpPOStatus.ClientID & "')")
            lblVendor_Remarks.Attributes.Add("autocomplete", "off")



            txtColorVerity = e.Row.FindControl("txtColorVerity")
            txtColorVerity.Attributes.Add("onfocus", "myFocusFunction(this)")
            txtColorVerity.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'COLOR_VARIETY','" & txtColorVerity.Text & "','" & drpPOStatus.ClientID & "')")

            txtColorVerity.Attributes.Add("autocomplete", "off")

            lblPoNotes = e.Row.FindControl("lblPoNotes")
            lblPoNotes.Attributes.Add("onfocus", "myFocusFunction(this)")
            lblPoNotes.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'REMARKS','" & lblPoNotes.Text & "','" & drpPOStatus.ClientID & "')")

            lblPoNotes.Attributes.Add("autocomplete", "off")

            lblShipDate = e.Row.FindControl("lblShipDate")
            'lblShipDate.Attributes.Add("onfocus", "myFocusFunction(this)")
            'lblShipDate.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'ShipDate','" & lblShipDate.Text & "')")

            'myCalendar = "myCalendar" & txtRowNumber.Text
            'strJS1 = strJS1 & " var " & myCalendar & ";  " & vbCrLf

            'strJS2 = strJS2 & "" & myCalendar & " = new dhtmlXCalendarObject([""" & lblShipDate.ClientID & """]);" & vbCrLf
            'strJS2 = strJS2 & "" & myCalendar & ".setDateFormat('%m/%d/%Y'); " & vbCrLf

            lblQtyOrdered = e.Row.FindControl("lblQtyOrdered")
            lblPack = e.Row.FindControl("lblPack")
            lblCost = e.Row.FindControl("lblCost")
            lblTotal = e.Row.FindControl("lblTotal")

            lblQtyOrdered.Attributes.Add("onfocus", "myFocusFunctionNew(this)")
            'lblQtyOrdered.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'Q_ORD','" & lblQtyOrdered.Text & "')")
            lblQtyOrdered.Attributes.Add("onblur", "myFocusFunctiontotalnew(this,'" & lblQtyOrdered.Text & "','" & lblTotal.ClientID & "','" & txtRowNumber.Text & "','" & lblQtyOrdered.ClientID & "','" & lblPack.ClientID & "','" & lblCost.ClientID & "','" & drpPOStatus.ClientID & "')")
            lblQtyOrdered.Attributes.Add("onKeyUp", "javascript:checkNumber(this);")

            lblQtyOrdered.Attributes.Add("autocomplete", "off")

            lblPack.Attributes.Add("onfocus", "myFocusFunctionNew(this)")
            'lblPack.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'PACK','" & lblPack.Text & "')")
            lblPack.Attributes.Add("onblur", "myFocusFunctiontotalnew(this,'" & lblPack.Text & "','" & lblTotal.ClientID & "','" & txtRowNumber.Text & "','" & lblQtyOrdered.ClientID & "','" & lblPack.ClientID & "','" & lblCost.ClientID & "','" & drpPOStatus.ClientID & "')")
            lblPack.Attributes.Add("onKeyUp", "javascript:checkNumber(this);")
            lblPack.Attributes.Add("autocomplete", "off")

            lblCost.Attributes.Add("onfocus", "myFocusFunctionNew(this)")
            ' lblCost.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'COST','" & lblCost.Text & "')")
            lblCost.Attributes.Add("onblur", "myFocusFunctiontotalnew(this,'" & lblCost.Text & "','" & lblTotal.ClientID & "','" & txtRowNumber.Text & "','" & lblQtyOrdered.ClientID & "','" & lblPack.ClientID & "','" & lblCost.ClientID & "','" & drpPOStatus.ClientID & "')")
            lblCost.Attributes.Add("onKeyUp", "javascript:checkNumber(this);")
            lblCost.Attributes.Add("autocomplete", "off")

            lblTotal.Attributes.Add("onfocus", "myFocusFunctiontotal(this,'" & lblQtyOrdered.ClientID & "','" & lblPack.ClientID & "','" & lblCost.ClientID & "')")
            lblTotal.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'Ext_COSt','" & lblTotal.Text & "')")
            'lblTotal.Enabled = False
            'lblTotal.Attributes.Add("onkeydown", "javascript:return false;")
            lblTotal.Attributes.Add("onkeydown", "javascript:return AllowAlphabet(event)")
            'lblTotal.Attributes.Add("onkeydown", "javascript:document.getElementById(" & lblVendor.ClientID & ").focus(); document.getElementById(" & lblVendor.ClientID & ").click();")

            'Dim qty_ As Int16 = 0
            'Dim pack_ As Decimal = 0
            'Dim cost_ As Decimal = 0
            'Dim total_ As Decimal = 0
            'Try
            '    qty_ = lblQtyOrdered.Text.Trim
            'Catch ex As Exception

            'End Try
            'Try
            '    pack_ = lblPack.Text.Trim
            'Catch ex As Exception

            'End Try
            'Try
            '    cost_ = lblCost.Text.Trim
            'Catch ex As Exception

            'End Try

            'lblCost.Text = Format(cost_, "0.00")


            'total_ = qty_ * pack_ * cost_
            'Updatetotal(txtRowNumber.Text, total_)
            'lblTotal.Text = Format(total_, "0.00")


            lblVendor = e.Row.FindControl("lblVendor")
            lblVendor.Attributes.Add("onfocus", "myFocusFunction(this)")
            lblVendor.Attributes.Add("onblur", "Saveitem(this," & txtRowNumber.Text & ",'Vendor_Code','" & lblVendor.Text & "','" & drpPOStatus.ClientID & "')")

            lblVendor.Attributes.Add("autocomplete", "off")

            lblVendor.Attributes.Add("placeholder", "SEARCH VENDOR")

            'Dim typeahead As String = ""
            'typeahead = typeahead & ""

            'typeahead = typeahead & "  " & "var sc = document.createElement('script');" & " " & vbCrLf
            'typeahead = typeahead & "  " & "sc.setAttribute('src', 'https://secure.quickflora.com/POM/typeahead.bundle.js');" & " " & vbCrLf
            'typeahead = typeahead & "  " & "sc.setAttribute('type', 'text/javascript');" & " " & vbCrLf
            'typeahead = typeahead & "  " & "document.head.appendChild(sc);" & " " & vbCrLf
            'typeahead = typeahead & " try  {  " & "$('#scrollable-dropdown-menu" & txtRowNumber.Text & " .typeahead').typeahead('destroy');" & " } catch(err) { alert(err.message); }" & vbCrLf
            'typeahead = typeahead & "        $('#scrollable-dropdown-menu" & txtRowNumber.Text & " .typeahead').typeahead(null, { " & vbCrLf
            'typeahead = typeahead & "        name: 'VendorID', " & vbCrLf
            'typeahead = typeahead & "        display: 'VendorID', " & vbCrLf
            'typeahead = typeahead & "        limit: 25, " & vbCrLf
            'typeahead = typeahead & "        source: vendor, " & vbCrLf
            'typeahead = typeahead & "        templates:  { " & vbCrLf
            'typeahead = typeahead & "            empty: [ " & vbCrLf
            'typeahead = typeahead & "              '<div >', " & vbCrLf
            'typeahead = typeahead & "                'unable to find vendors that match your keywords', " & vbCrLf
            'typeahead = typeahead & "              '</div>' " & vbCrLf
            'typeahead = typeahead & "            ].join('\n'), " & vbCrLf
            'typeahead = typeahead & "            suggestion: Handlebars.compile('<div><strong>{{VendorID}}</strong> – {{VendorName}}</div>') " & vbCrLf
            'typeahead = typeahead & "        } " & vbCrLf
            'typeahead = typeahead & "    }); " & vbCrLf

            'strJS3 = strJS3 & vbCrLf & typeahead



            lblVendor.Attributes.Add("onKeyUp", "SendQuerynew(this.value,this," & txtRowNumber.Text & ")")


            '     qry = "Update PO_Requisition_Details   ,[COLOR_VARIETY]=@COLOR_VARIETY ,[REMARKS]=@REMARKS ,[Q_ORD]=@Q_ORD ,[PACK]=@PACK ,[COST]=@COST ,[Ext_COSt]=@Ext_COSt ,[Vendor_Code]=@Vendor_Code ,[Buyer]=@Buyer ,[Status]=@Status ,[Q_Recv]=@Q_Recv ,[ISSUE]=@ISSUE " _


            If lblBuyStatus.Text.Trim <> "" Then
                Try
                    drpPOStatus.SelectedValue = lblBuyStatus.Text
                Catch ex As Exception

                End Try
            End If

            If lblBuyStatus.Text.Trim = "Pending Email" Then
                e.Row.BackColor = Drawing.Color.White
            End If

            If lblBuyStatus.Text.Trim = "Pending-Stg Email" Then
                e.Row.BackColor = Drawing.Color.White
            End If

            If lblBuyStatus.Text.Trim = "Do Not Touch" Then
                e.Row.BackColor = Drawing.Color.FromArgb(204, 204, 255)
            End If

            If lblBuyStatus.Text.Trim = "Wed" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 204, 204)
            End If

            If lblBuyStatus.Text.Trim = "Not Avail" Then
                e.Row.BackColor = Drawing.Color.FromArgb(204, 204, 204)
            End If

            If lblBuyStatus.Text.Trim = "Pending" Then
                e.Row.BackColor = Drawing.Color.FromArgb(153, 255, 153)
            End If

            If lblBuyStatus.Text.Trim = "Bought Wed" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 204, 204)
            End If

            If lblBuyStatus.Text.Trim = "In Process" Then
                e.Row.BackColor = Drawing.Color.FromArgb(204, 255, 255)
            End If

            If lblBuyStatus.Text.Trim = "Bought" Then
                e.Row.BackColor = Drawing.Color.FromArgb(153, 255, 255)
            End If

            If lblBuyStatus.Text.Trim = "No Action" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 255, 153)
            End If

            If lblBuyStatus.Text.Trim = "Pending Auction" Then
                e.Row.BackColor = Drawing.Color.FromArgb(153, 255, 153)
            End If

            If lblBuyStatus.Text.Trim = "Wed In Process" Then
                e.Row.BackColor = Drawing.Color.FromArgb(255, 204, 204)
            End If

            drpPOStatus.Attributes.Add("onfocus", "myFocusFunction(this)")
            drpPOStatus.Attributes.Add("onchange", "Saveitem(this," & txtRowNumber.Text & ",'Status','" & lblBuyStatus.Text & "',this)")
            'myonblurFunction
            drpPOStatus.Attributes.Add("onblur", "myonblurFunction(this)")


            Dim lblBuyer As New Label
            Dim drpBuyer As New DropDownList

            lblBuyer = e.Row.FindControl("lblBuyer")
            drpBuyer = e.Row.FindControl("drpBuyer")

            'If Me.CompanyID <> "mccarthyg" Then
            '    drpBuyer.Items.Clear()
            '    Dim PopOrderType As New CustomOrder()
            '    Dim rs As SqlDataReader
            '    Try
            '        rs = PopOrderType.PopulateEmployees(CompanyID, DepartmentID, DivisionID)
            '        drpBuyer.DataTextField = "EmployeeName"
            '        drpBuyer.DataValueField = "EmployeeID"
            '        drpBuyer.DataSource = rs
            '        drpBuyer.DataBind()
            '        '' drpBuyer.SelectedValue = EmployeeID ' Session("EmployeeUserName")
            '        rs.Close()
            '    Catch ex As Exception

            '    End Try

            '    drpBuyer.Items.Insert(0, (New ListItem("-Select-", "0")))
            'End If


            If lblBuyer.Text.Trim <> "" Then
                Try
                    drpBuyer.SelectedValue = lblBuyer.Text
                Catch ex As Exception

                End Try
            End If

            drpBuyer.Attributes.Add("onfocus", "myFocusFunction(this)")
            drpBuyer.Attributes.Add("onchange", "Saveitem(this," & txtRowNumber.Text & ",'Buyer','" & lblBuyer.Text & "','" & drpPOStatus.ClientID & "')")
            'myonblurFunction
            drpBuyer.Attributes.Add("onblur", "myonblurFunction(this)")

        End If
    End Sub

    ''[CompanyID],[DivisionID],[DepartmentID],[LocationID],[Product] ,[Type] ,[PONumber] ,[QOH] ,[PreSold] ,[Requested] ,[ColorVerity] ,[PoNotes] ,[VendorNotes] ,[ShipDate] ,[QtyOrdered] ,[Pack] ,[Total] ,[Cost] ,[Vendor] ,[BuyStatus] ,[Employee] ,[RowNumber]

    'SET @Pageno = @Pageno * @drppagelimit + 1
    'Select  
    '	 PictureURL, MediumPictureURL,LargePictureURL,
    '	ItemName, ItemID, ItemCategoryID,  Price,EnableItemPrice ,RowRank, MSRP

    '	FROM (
    '	Select Case InventoryItems.PictureURL, InventoryItems.MediumPictureURL,InventoryItems.LargePictureURL,
    '	InventoryItems.ItemName, InventoryItems.ItemID, InventoryItems.ItemCategoryID,  InventoryItems.Price ,InventoryItems.EnableItemPrice 
    '	,ROW_NUMBER() OVER (ORDER BY   InventoryItems.ItemName  ASC) AS RowRank, MSRP
    '	FROM  InventoryItems
    '	WHERE (InventoryItems.CompanyID = @CompanyID) And (InventoryItems.DivisionID =  @DivisionID) And 
    '	(InventoryItems.DepartmentID = @DepartmentID)  
    '	And (InventoryItems.Price > 0) 
    '	And InventoryItems.WireServiceIDAllowed=1  
    '	And ((InventoryItems.ItemFamilyID= @CatID) Or (InventoryItems.ItemFamilyID2= @CatID )
    '	Or (InventoryItems.ItemFamilyID3= @CatID))
    '	And (InventoryItems.EnabledfrontEndItem = 1)

    '	) AS itemswithrownumber 

    '	WHERE RowRank >= @Pageno And RowRank < (@Pageno + @drppagelimit)
    '	Order by RowRank ASC


    Public Sub fillitems(ByVal pg As String)


        If pg <> "" Then
            Pageno = pg
        Else
            Pageno = "1"
        End If

        Dim dtcount As New DataTable
        dtcount = BindDetailsCount()



        Dim totalrows As Integer = 0

        Try
            totalrows = dtcount.Rows(0)(0)
        Catch ex As Exception

        End Try

        Dim numofpages As Integer = 0
        Dim numofitemsperpage As Integer = 0
        numofitemsperpage = drppagelimit.SelectedValue

        numofpages = totalrows / numofitemsperpage

        If totalrows > numofitemsperpage * numofpages Then
            numofpages = numofpages + 1
        End If



        Dim n As Integer = 0
        paging = ""
        For n = 1 To numofpages

            If n = Pageno Then
                paging = paging & "<li class='c-active'>"
            Else
                paging = paging & "<li>"
            End If
            paging = paging & "<a href='Javascript:;'  onclick=""javascript:paging('" & n & "')"">" & n & "</a>"
            paging = paging & "</li>"
        Next

        pagingleft = "javascript:paging('" & Pageno - 1 & "')"
        pagingright = "javascript:paging('" & Pageno + 1 & "')"


        BindDetails(Pageno - 1)

    End Sub

    Public Pageno As String = ""
    Public paging As String = ""
    Public pagingleft As String = ""
    Public pagingright As String = ""

    Public Function BindDetailsCount() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        ssql = ssql & " SELECT  Count([Enterprise].[dbo].[PO_Requisition_Details].[OrderNo] ) "
        ssql = ssql & "      FROM [Enterprise].[dbo].[PO_Requisition_Details] Left Outer Join [Enterprise].[dbo].[PO_Requisition_Header] "
        ssql = ssql & "      ON [Enterprise].[dbo].[PO_Requisition_Details].[CompanyID] = [Enterprise].[dbo].[PO_Requisition_Header].[CompanyID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DivisionID] =[Enterprise].[dbo].[PO_Requisition_Header].[DivisionID] And "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DepartmentID] = [Enterprise].[dbo].[PO_Requisition_Header].[DepartmentID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[OrderNo] = [Enterprise].[dbo].[PO_Requisition_Header].[OrderNo] "
        ssql = ssql & "  Where  convert(int, CASE WHEN  ISNUMERIC([PO_Requisition_Details].[Q_REQ])  = 1  THEN  [PO_Requisition_Details].[Q_REQ] ELSE '0' END)  <> 0 AND     ISNULL([PO_Requisition_Header].[canceled],0) <> 1 AND  [Enterprise].[dbo].[PO_Requisition_Details].CompanyID ='" & Me.CompanyID & "' AND [Enterprise].[dbo].[PO_Requisition_Details].DivisionID ='" & Me.DivisionID & "'  AND [Enterprise].[dbo].[PO_Requisition_Details].DepartmentID ='" & Me.DepartmentID & "' "

        ssql = ssql & " AND  ISDATE([PO_Requisition_Header].[ShipDate])  = 1 "
        ssql = ssql & " AND  [PO_Requisition_Details].[Product]  <> '' "

        If txtSearchExpression.Text.Trim <> "" Then
            If drpCondition.SelectedValue = "=" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  = '" & txtSearchExpression.Text.Trim & "'"
                End If
                If drpFieldName.SelectedValue = "PONO" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[PONO]  = '" & txtSearchExpression.Text.Trim & "'"
                End If
                If drpFieldName.SelectedValue = "Buyer" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[Buyer]  = '" & txtSearchExpression.Text.Trim & "'"
                End If
            End If
            If drpCondition.SelectedValue = "Like" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  Like '%" & txtSearchExpression.Text.Trim & "%'"
                End If
                If drpFieldName.SelectedValue = "PONO" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[PONO]  Like '%" & txtSearchExpression.Text.Trim & "%'"
                End If
                If drpFieldName.SelectedValue = "Buyer" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[Buyer]  Like '%" & txtSearchExpression.Text.Trim & "%'"
                End If
            End If
        End If


        Session("drpBuyStatus") = drpBuyStatus.SelectedValue
        Session("drpDefaultBuyStatus") = drpDefaultBuyStatus.SelectedValue
        'drpDefaultBuyStatus

        If drpBuyStatus.SelectedValue <> "" Then
            If drpBuyStatus.SelectedValue = "Not Bought" Then
                ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'Bought' "
            Else
                ssql = ssql & " AND  [PO_Requisition_Details].[Status]  = '" & drpBuyStatus.SelectedValue & "'"
            End If
        Else

            '"Entry In Process"
        End If

        Session("drpProductTypes") = drpProductTypes.SelectedValue

        If drpProductTypes.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Type] = '" & drpProductTypes.SelectedValue & "'"
        End If

        If cmblocationid.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Location] = '" & cmblocationid.SelectedValue & "'"
        End If

        Session("drpBuyerlist") = drpBuyerlist.SelectedValue

        If drpBuyerlist.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Details].[Buyer] = '" & drpBuyerlist.SelectedValue & "'"
        End If

        If vendor <> "" Then
            ssql = ssql & " AND [PO_Requisition_Details].[Vendor_Code] = '" & vendor & "'"
        End If

        'itemajaxsearch

        If itemajaxsearch <> "" Then
            ssql = ssql & " AND [PO_Requisition_Details].[Product] = '" & itemajaxsearch & "'"
        End If

        If rdselected.Checked Then
            ssql = ssql & " AND ISNULL([PO_Requisition_Header].[ShipDate],'') <> '' "
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) >= '" & txtDateFrom.Text & "'"
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) <= '" & txtDateTo.Text & "'"
        End If

        Session("txtDateFrom") = txtDateFrom.Text
        Session("txtDateTo") = txtDateTo.Text

        If chkincludeStanding.Checked Then
            If chkonlyStanding.Checked Then
                ssql = ssql & " AND [PO_Requisition_Header].[Type] = 'Standing Auto' "
            End If
        Else

            If chkonlyStanding.Checked Then
                ssql = ssql & " AND [PO_Requisition_Header].[Type] = 'Standing Auto' "
            Else
                ssql = ssql & " AND  [PO_Requisition_Header].[Status]  <> 'Entry In Process' "
                ssql = ssql & " AND [PO_Requisition_Header].[Type] <> 'Standing Auto' "
            End If
        End If



        If chkincludeBought.Checked Then

        Else
            ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'Bought' "
        End If

        If chkWithother.Checked Then

        Else
            ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'With-Other' "
        End If

        If chkNotavailable.Checked Then

        Else
            ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'Not Avail' "
        End If


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)


        da.SelectCommand = com
        da.Fill(dt)

        Return dt
    End Function

    Dim itemajaxsearch As String = ""

    Private Sub btnsearchbyitem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsearchbyitem.Click
        If txtitemsearchajax.Text.Trim <> "" Then
            itemajaxsearch = txtitemsearchajax.Text
            itemajaxsearch = itemajaxsearch.Replace("[", "")
            itemajaxsearch = itemajaxsearch.Replace("]", "")
            txtitemsearchajax.Text = ""

            SortExpression = "DEFAULT"
            fillitems("1")
        End If

    End Sub

    Protected Sub btnOrderSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrderSearch.Click

        If txtOrderSearch.Text.Trim <> "" Then
            ' Bindorders(txtOrderSearch.Text)
            vendor = txtOrderSearch.Text
            vendor = vendor.Replace("[", "")
            vendor = vendor.Replace("]", "")
            txtOrderSearch.Text = ""

            SortExpression = "DEFAULT"
            fillitems("1")
        End If


    End Sub

    Dim vendor As String = ""


    Dim SortExpression As String = "ShipDate"

    Private Sub OrderHeaderGrid_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles OrderHeaderGrid.Sorting
        Try
            If Session("SortDirection") = "Asc" Then
                Session("SortDirection") = "Desc"
            Else
                Session("SortDirection") = "Asc"
            End If
        Catch ex As Exception
            Session("SortDirection") = "Asc"
        End Try


        SortExpression = e.SortExpression

        fillitems(txtpageno.Text)

        ''e.SortDirection 
        ''e.SortExpression 

    End Sub


    Public Function BindDetails(ByVal Pageno As Integer) As DataSet


        Dim _drppagelimit As Integer = 0
        _drppagelimit = drppagelimit.SelectedValue
        Pageno = Pageno * _drppagelimit + 1

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataSet()


        ssql = ssql & "      Select ItemName,RowRank,[Location],[Product],[Type],[OrderNo],[PONO],[QOH],[PRESOLD],[Q_REQ],[COLOR_VARIETY],[REMARKS],[ShipDate],[Q_ORD],[PACK],[COST],[Ext_COSt],[Vendor_Code],Vendor_Remarks,[Status],Buyer,[InLineNumber],[OrderBy]  FROM ( "
        ssql = ssql & "      Select [Enterprise].[dbo].InventoryItems.ItemName   , [PO_Requisition_Details].Vendor_Remarks,[PO_Requisition_Details].[PONO],[PO_Requisition_Details].[Product],[PO_Requisition_Details].[QOH],[PO_Requisition_Details].[PRESOLD] ,[PO_Requisition_Details].[Q_REQ] ,[PO_Requisition_Details].[COLOR_VARIETY] ,[PO_Requisition_Details].[REMARKS] ,[PO_Requisition_Details].[Q_ORD] "
        ssql = ssql & "      ,[PO_Requisition_Details].[PACK] ,[PO_Requisition_Details].[COST] ,[PO_Requisition_Details].[Ext_COSt] ,[PO_Requisition_Details].[Vendor_Code] ,[PO_Requisition_Details].[Status] ,[PO_Requisition_Details].[Buyer] "
        ssql = ssql & "      ,[PO_Requisition_Details].[InLineNumber] ,[PO_Requisition_Header].[OrderNo] ,[PO_Requisition_Header].[Location] ,[PO_Requisition_Header].[Type] ,[PO_Requisition_Header].[ShipDate] ,[PO_Requisition_Header].[OrderBy] "

        'Session("SortDirection") = "DESC"
        'SortExpression = "ShipDate"
        Dim SortDirection As String = "ASC"

        Try
            SortDirection = Session("SortDirection")
        Catch ex As Exception

        End Try

        If SortExpression = "DEFAULT" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Details].[Product],[PO_Requisition_Header].[Location]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Product" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Details].[Product]   " & SortDirection & " ) As RowRank "
        End If


        If SortExpression = "Vendor_Code" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Details].[Vendor_Code]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Q_REQ" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Details].[Q_REQ]   " & SortDirection & " ) As RowRank "
        End If


        If SortExpression = "Q_ORD" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Details].[Q_ORD]   " & SortDirection & " ) As RowRank "
        End If


        If SortExpression = "QOH" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Details].[QOH]   " & SortDirection & " ) As RowRank "
        End If


        If SortExpression = "ShipDate" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY   convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101))  " & SortDirection & ") As RowRank "
        End If

        If SortExpression = "OrderNo" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[OrderNo]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Location" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[Location]   " & SortDirection & " ) As RowRank "
        End If

        If SortExpression = "Type" Then
            ssql = ssql & "      ,ROW_NUMBER() OVER (ORDER BY  [PO_Requisition_Header].[Type]   " & SortDirection & " ) As RowRank "
        End If

        ssql = ssql & "      FROM [Enterprise].[dbo].[PO_Requisition_Details] Left Outer Join [Enterprise].[dbo].[PO_Requisition_Header] "
        ssql = ssql & "      ON [Enterprise].[dbo].[PO_Requisition_Details].[CompanyID] = [Enterprise].[dbo].[PO_Requisition_Header].[CompanyID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DivisionID] =[Enterprise].[dbo].[PO_Requisition_Header].[DivisionID] And "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[DepartmentID] = [Enterprise].[dbo].[PO_Requisition_Header].[DepartmentID] AND "
        ssql = ssql & "      [Enterprise].[dbo].[PO_Requisition_Details].[OrderNo] = [Enterprise].[dbo].[PO_Requisition_Header].[OrderNo] "

        ssql = ssql & " Left Outer Join [Enterprise].[dbo].InventoryItems  "
        ssql = ssql & " ON [Enterprise].[dbo].[PO_Requisition_Details].[CompanyID] = [Enterprise].[dbo].InventoryItems.[CompanyID] "
        ssql = ssql & " AND [Enterprise].[dbo].[PO_Requisition_Details].[DivisionID] =[Enterprise].[dbo].InventoryItems.[DivisionID] "
        ssql = ssql & " And [Enterprise].[dbo].[PO_Requisition_Details].[DepartmentID] = [Enterprise].[dbo].InventoryItems.[DepartmentID]  "
        ssql = ssql & " AND [Enterprise].[dbo].[PO_Requisition_Details].[Product] = [Enterprise].[dbo].InventoryItems.ItemID   "

        ssql = ssql & "  Where   convert(int, CASE WHEN  ISNUMERIC([PO_Requisition_Details].[Q_REQ])  = 1  THEN  [PO_Requisition_Details].[Q_REQ] ELSE '0' END)  <> 0 AND    ISNULL([PO_Requisition_Header].[canceled],0) <> 1 AND [Enterprise].[dbo].[PO_Requisition_Details].CompanyID ='" & Me.CompanyID & "' AND [Enterprise].[dbo].[PO_Requisition_Details].DivisionID ='" & Me.DivisionID & "'  AND [Enterprise].[dbo].[PO_Requisition_Details].DepartmentID ='" & Me.DepartmentID & "' "
        ''  ssql = ssql & " AND  [PO_Requisition_Header].[Status]  <> 'Entry In Process' "
        ssql = ssql & " AND  ISDATE([PO_Requisition_Header].[ShipDate])  = 1 "
        ssql = ssql & " AND  [PO_Requisition_Details].[Product]  <> '' "

        If txtSearchExpression.Text.Trim <> "" Then
            If drpCondition.SelectedValue = "=" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  = '" & txtSearchExpression.Text.Trim & "'"
                End If
                If drpFieldName.SelectedValue = "PONO" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[PONO]  = '" & txtSearchExpression.Text.Trim & "'"
                End If
                If drpFieldName.SelectedValue = "Buyer" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[Buyer]  = '" & txtSearchExpression.Text.Trim & "'"
                End If
            End If
            If drpCondition.SelectedValue = "Like" Then
                If drpFieldName.SelectedValue = "OrderNo" Then
                    ssql = ssql & " AND  [PO_Requisition_Header].[OrderNo]  Like '%" & txtSearchExpression.Text.Trim & "%'"
                End If
                If drpFieldName.SelectedValue = "PONO" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[PONO]  Like '%" & txtSearchExpression.Text.Trim & "%'"
                End If
                If drpFieldName.SelectedValue = "Buyer" Then
                    ssql = ssql & " AND  [PO_Requisition_Details].[Buyer]  Like '%" & txtSearchExpression.Text.Trim & "%'"
                End If
            End If
        End If


        If drpBuyStatus.SelectedValue <> "" Then
            If drpBuyStatus.SelectedValue = "Not Bought" Then
                ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'Bought' "
            Else
                ssql = ssql & " AND  [PO_Requisition_Details].[Status]  = '" & drpBuyStatus.SelectedValue & "'"
            End If
        Else

        End If
        If drpProductTypes.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Type] = '" & drpProductTypes.SelectedValue & "'"
        End If
        If cmblocationid.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Header].[Location] = '" & cmblocationid.SelectedValue & "'"
        End If

        If drpBuyerlist.SelectedValue <> "" Then
            ssql = ssql & " AND [PO_Requisition_Details].[Buyer] = '" & drpBuyerlist.SelectedValue & "'"
        End If

        If vendor <> "" Then
            ssql = ssql & " AND [PO_Requisition_Details].[Vendor_Code] = '" & vendor & "'"
        End If


        If itemajaxsearch <> "" Then
            ssql = ssql & " AND [PO_Requisition_Details].[Product] = '" & itemajaxsearch & "'"
        End If

        If rdselected.Checked Then
            ssql = ssql & " AND ISNULL([PO_Requisition_Header].[ShipDate],'') <> '' "
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) >= '" & txtDateFrom.Text & "'"
            ssql = ssql & " AND convert(datetime, Convert(nvarchar(36),[PO_Requisition_Header].[ShipDate],101)) <= '" & txtDateTo.Text & "'"
        End If

        If chkincludeStanding.Checked Then
            If chkonlyStanding.Checked Then
                ssql = ssql & " AND [PO_Requisition_Header].[Type] = 'Standing Auto' "
            End If
        Else

            If chkonlyStanding.Checked Then
                ssql = ssql & " AND [PO_Requisition_Header].[Type] = 'Standing Auto' "
            Else
                ssql = ssql & " AND  [PO_Requisition_Header].[Status]  <> 'Entry In Process' "
                ssql = ssql & " AND [PO_Requisition_Header].[Type] <> 'Standing Auto' "
            End If
        End If


        If chkincludeBought.Checked Then

        Else
            ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'Bought' "
        End If

        If chkWithother.Checked Then

        Else
            ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'With-Other' "
        End If

        If chkNotavailable.Checked Then

        Else
            ssql = ssql & " AND [PO_Requisition_Details].[Status] <> 'Not Avail' "
        End If


        ssql = ssql & "      ) AS itemswithrownumber "

        Session("BatchPO") = ssql

        ssql = ssql & "	WHERE RowRank >= " & Pageno.ToString & " And RowRank < (" & (Pageno + _drppagelimit).ToString & ") "
        ssql = ssql & " Order by RowRank ASC "



        'SET @DateSql = 'and  convert(datetime, Convert(nvarchar(36),OrderHeader.OrderDate,101)) >=''' + @FromDate + '''  
        'And  convert(datetime,Convert(nvarchar(36),OrderHeader.OrderDate,101)) <=''' + @ToDate + '''';
        lblsql.Text = ssql
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)
        OrderHeaderGrid.DataSource = dt
        OrderHeaderGrid.DataBind()
        Return dt

        Try
        Catch ex As Exception

        End Try
        Return dt
    End Function


    Public Function GetItemTypes(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("GetItemTypes", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

        Dim adapter As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable()
        Try
            adapter.Fill(dt)
        Catch ex As Exception
        End Try

        conString.Close()

        Return dt
    End Function

    Public Function GetBuyStatus(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String) As DataTable

        Dim conString As New SqlConnection
        conString.ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim myCommand As New SqlCommand("GetBuyStatus", conString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

        Dim adapter As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable()
        Try
            adapter.Fill(dt)
        Catch ex As Exception
        End Try

        conString.Close()

        Return dt
    End Function

    Public Sub SetBuyerdropdown()
        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        Try
            rs = PopOrderType.PopulateEmployees(CompanyID, DepartmentID, DivisionID)
            drpBuyerlist.DataTextField = "EmployeeName"
            drpBuyerlist.DataValueField = "EmployeeID"
            drpBuyerlist.DataSource = rs
            drpBuyerlist.DataBind()
            '' drpBuyer.SelectedValue = EmployeeID ' Session("EmployeeUserName")
            rs.Close()
        Catch ex As Exception

        End Try


    End Sub

    Public Sub SetLocationIDdropdown()
        SetBuyerdropdown()
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
            'cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            cmblocationid.Items.Add(item)
        End If
        ''''''''''''''''''''

        Dim dt1 As New Data.DataTable
        dt1 = GetBuyStatus(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
        If dt1.Rows.Count <> 0 Then
            drpBuyStatus.DataSource = dt1
            drpBuyStatus.DataTextField = "BuyStatusDesc"
            drpBuyStatus.DataValueField = "BuyStatusID"
            drpBuyStatus.DataBind()
            'Setdropdown()
        Else
            'drpBuyStatus.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            drpBuyStatus.Items.Add(item)
        End If

        Dim dt2 As New Data.DataTable
        dt2 = GetItemTypes(Me.CompanyID, Me.DivisionID, Me.DepartmentID)
        If dt2.Rows.Count <> 0 Then
            drpProductTypes.DataSource = dt2
            drpProductTypes.DataTextField = "ItemTypeDescription"
            drpProductTypes.DataValueField = "ItemTypeID"
            drpProductTypes.DataBind()
            'Setdropdown()
        Else
            'drpProductTypes.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            drpProductTypes.Items.Add(item)
        End If



        Dim locationid As String = ""
        Try
            locationid = Session("Locationid")
        Catch ex As Exception

        End Try
        If locationid <> "Corporate" Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
            cmblocationid.Enabled = False
        End If
        ' Session("OrderLocationid") = cmblocationid.SelectedValue
    End Sub
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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        txtOrderSearch.Attributes.Add("placeholder", "SEARCH VENDOR")
        txtOrderSearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")

        'txtitemsearchajax
        txtitemsearchajax.Attributes.Add("placeholder", "SEARCH By ITEMS")
        txtitemsearchajax.Attributes.Add("onKeyUp", "SendQuery2(this.value,this,'1')")

        txtSearchExpression.Attributes.Add("autocomplete", "off")


        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "BatchPO")

        Dim securitycheck As Boolean = False

        While (rs.Read())
            'Response.Write(rs("EmployeeID").ToString())
            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=BatchPO")
        End If

        If Not (Page.IsPostBack) Then
            txtDateFrom.Text = DateTime.Now.Date.ToShortDateString
            txtDateTo.Text = DateTime.Now.Date.AddDays(7).ToShortDateString
            SetLocationIDdropdown()
            Session("SortDirection") = "ASC"
            SortExpression = "DEFAULT"


            Try
                If Session("txtDateFrom") <> "" Then
                    txtDateFrom.Text = Session("txtDateFrom")
                End If

            Catch ex As Exception

            End Try

            Try
                If Session("txtDateTo") <> "" Then
                    txtDateTo.Text = Session("txtDateTo")
                End If

            Catch ex As Exception

            End Try

            Try
                ''Response.Write("drpProductTypes: " & Session("drpProductTypes"))
                If Session("drpProductTypes") <> "" Then
                    drpProductTypes.SelectedValue = Session("drpProductTypes")
                End If

            Catch ex As Exception

            End Try

            Try
                If Session("drpBuyerlist") <> "" Then
                    drpBuyerlist.SelectedValue = Session("drpBuyerlist")
                End If

            Catch ex As Exception

            End Try

            Try
                If Session("drpBuyStatus") <> "" Then
                    drpBuyStatus.SelectedValue = Session("drpBuyStatus")
                End If

            Catch ex As Exception

            End Try

            Try
                ''Response.Write("drpDefaultBuyStatus: " & Session("drpDefaultBuyStatus"))

                If Session("drpDefaultBuyStatus") <> "" Then
                    drpDefaultBuyStatus.SelectedValue = Session("drpDefaultBuyStatus")
                End If

            Catch ex As Exception

            End Try

            fillitems("1")
            txtpageno.Text = "1"

        End If

        If IsPostBack Then

            Dim CtrlID As String = String.Empty
            Dim value As String = String.Empty



        End If

        vendor = ""
        Dim myCalendar2 As String = ""
        Dim strJS12 As String = ""
        Dim strJS22 As String = ""

        myCalendar2 = "CalFrom" & Date.Now.Hour.ToString & Date.Now.Minute.ToString & Date.Now.Second.ToString & Date.Now.Millisecond.ToString

        strJS12 = strJS12 & " var " & myCalendar2 & ";  " & vbCrLf
        strJS22 = strJS22 & "" & myCalendar2 & " = new dhtmlXCalendarObject([""" & txtDateFrom.ClientID & """]);" & vbCrLf
        strJS22 = strJS22 & "" & myCalendar2 & ".setDateFormat('%m/%d/%Y'); " & vbCrLf

        myCalendar2 = "Calto" & Date.Now.Hour.ToString & Date.Now.Minute.ToString & Date.Now.Second.ToString & Date.Now.Millisecond.ToString

        strJS12 = strJS12 & " var " & myCalendar2 & ";  " & vbCrLf
        strJS22 = strJS22 & "" & myCalendar2 & " = new dhtmlXCalendarObject([""" & txtDateTo.ClientID & """]);" & vbCrLf
        strJS22 = strJS22 & "" & myCalendar2 & ".setDateFormat('%m/%d/%Y'); " & vbCrLf



        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "  " & "doOnLoadNew2();" & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS12 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "function doOnLoadNew2() { " & " " & vbCrLf
        onloadScript = onloadScript & "  " & strJS22 & " " & vbCrLf
        onloadScript = onloadScript & "  " & "}" & " " & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "doOnLoadNew2", onloadScript.ToString())

    End Sub


    Private Sub btnSearchExpression_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchExpression.Click

        SortExpression = "DEFAULT"
        fillitems("1")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click, chkincludeStanding.CheckedChanged, rdall.CheckedChanged, rdselected.CheckedChanged, chkonlyStanding.CheckedChanged

        SortExpression = "DEFAULT"
        fillitems("1")
    End Sub

    Private Sub cmblocationid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmblocationid.SelectedIndexChanged, drpProductTypes.SelectedIndexChanged, drpBuyStatus.SelectedIndexChanged, drpBuyerlist.SelectedIndexChanged

        SortExpression = "DEFAULT"
        fillitems("1")

    End Sub
    Private Sub btnREFRESHPURCH_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnREFRESHPURCH.Click
        Try
            EmployeeID = Session("EmployeeID")
            drpBuyerlist.SelectedValue = EmployeeID
        Catch ex As Exception

        End Try
        SortExpression = "DEFAULT"
        fillitems("1")
    End Sub

    Protected Sub drppagelimit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drppagelimit.SelectedIndexChanged
        Session("drppagelimit") = drppagelimit.SelectedValue
        SortExpression = "DEFAULT"
        fillitems("1")

    End Sub

    Private Sub btnpageno_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnpageno.Click
        fillitems(txtpageno.Text)
    End Sub

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
                com.Parameters.Add(New SqlParameter("@TransmissionBy", SqlDbType.NVarChar)).Value = drpTransmission.SelectedValue
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

    Dim _Buyer As String = ""
    Private Sub btnSETTOBOUGHT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSETTOBOUGHT.Click
        For Each row1 As GridViewRow In OrderHeaderGrid.Rows
            Dim RowNumber As Integer = (OrderHeaderGrid.DataKeys(row1.RowIndex).Value)
            Dim chk As CheckBox = row1.FindControl("chkRow")
            If chk.Checked Then
                SETTOBOUGHTUpdate(RowNumber)
            End If
        Next
        SortExpression = "DEFAULT"
        fillitems("1")


    End Sub

    Public Function SETTOBOUGHTUpdate(ByVal RowNumber As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PO_Requisition_Details set  Status='Bought' Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And InLineNumber=@RowNumber  "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@RowNumber", SqlDbType.NVarChar, 36)).Value = RowNumber

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

    Private Sub btnManualBought_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnManualBought.Click
        sendpo(False)
        SortExpression = "DEFAULT"
        fillitems("1")
    End Sub

    Private Sub btnSENDPO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSENDPO.Click
        sendpo(True)
        SortExpression = "DEFAULT"
        fillitems("1")
    End Sub


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

    Public Sub sendpo(ByVal sendemail As Boolean)

        For Each row1 As GridViewRow In OrderHeaderGrid.Rows
            Dim RowNumber As Integer = (OrderHeaderGrid.DataKeys(row1.RowIndex).Value)
            Dim chk As CheckBox = row1.FindControl("chkRow")
            If chk.Checked Then
                Dim dt As New DataTable
                dt = BatchPOSampleRow(RowNumber)
                If dt.Rows.Count <> 0 Then

                    ',[OrderNo] ,[Product] ,[QOH] ,[DUMP] ,[Q_REQ] ,[PRESOLD] ,[COLOR_VARIETY] ,[REMARKS] ,[Q_ORD] ,[PACK] ,[COST] ,[Ext_COSt] ,[Vendor_Code] ,[Buyer] ,[Status] ,[Q_Recv] ,[ISSUE]

                    Dim _Product_ As String = ""
                    Dim _COST_ As Decimal = 0
                    Dim _Ext_COSt_ As Decimal = 0
                    Dim _Q_ORD_ As Integer = 0

                    Try
                        _Product_ = dt.Rows(0)("Product")
                    Catch ex As Exception

                    End Try
                    'getitemname

                    If _Product_ = "" Or getitemname(_Product_) = "" Then
                        Dim onloadScript1 As String = ""
                        ''onloadScript = onloadScript1 & "<script type='text/javascript'>" & vbCrLf
                        onloadScript1 = onloadScript1 & " alert('" & "Please verify that all selected requests have correct Item ID selected." & "');" & vbCrLf
                        ''onloadScript = onloadScript & "<" & "/" & "script>"
                        ' Register script with page 
                        strJs5 = onloadScript1
                        ''Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript1.ToString())

                        Exit Sub
                    End If

                    Try
                        _COST_ = dt.Rows(0)("COST")
                    Catch ex As Exception

                    End Try
                    Try
                        _Ext_COSt_ = dt.Rows(0)("Ext_COSt")
                    Catch ex As Exception

                    End Try
                    Try
                        _Q_ORD_ = dt.Rows(0)("Q_ORD")
                    Catch ex As Exception

                    End Try

                    If _Q_ORD_ = 0 Then
                        Dim onloadScript1 As String = ""
                        ''onloadScript = onloadScript1 & "<script type='text/javascript'>" & vbCrLf
                        onloadScript1 = onloadScript1 & " alert('" & "Please verify that all selected requests have correct non zero quantity ordered." & "');" & vbCrLf
                        ''onloadScript = onloadScript & "<" & "/" & "script>"
                        ' Register script with page 
                        strJs5 = onloadScript1
                        ''Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript1.ToString())

                        Exit Sub
                    End If

                    If _COST_ = 0 And chkvalidate.Checked Then
                        Dim onloadScript1 As String = ""
                        ''onloadScript = onloadScript1 & "<script type='text/javascript'>" & vbCrLf
                        onloadScript1 = onloadScript1 & " alert('" & "Please verify that all selected requests have non zero Item cost." & "');" & vbCrLf
                        ''onloadScript = onloadScript & "<" & "/" & "script>"
                        ' Register script with page 
                        strJs5 = onloadScript1
                        ''Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript1.ToString())

                        Exit Sub
                    End If

                    If _Ext_COSt_ = 0 And chkvalidate.Checked Then
                        Dim onloadScript1 As String = ""
                        ''onloadScript = onloadScript1 & "<script type='text/javascript'>" & vbCrLf
                        onloadScript1 = onloadScript1 & " alert('" & "Please verify that all selected requests have non zero Ext cost." & "');" & vbCrLf
                        ''onloadScript = onloadScript & "<" & "/" & "script>"
                        ' Register script with page 
                        strJs5 = onloadScript1
                        ''Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript1.ToString())

                        Exit Sub
                    End If

                    

                    Dim VVendor As String = ""
                    Dim bbuyer As String = ""
                    Try
                        VVendor = dt.Rows(0)("Vendor_Code")
                    Catch ex As Exception

                    End Try

                    Try
                        bbuyer = dt.Rows(0)("Buyer")
                    Catch ex As Exception

                    End Try

                    Dim dtvendor As DataTable
                    dtvendor = FillDetailsVendor(VVendor)

                    If VVendor = "" Or dtvendor.Rows.Count = 0 Then
                        Dim onloadScript1 As String = ""
                        ''onloadScript = onloadScript1 & "<script type='text/javascript'>" & vbCrLf
                        onloadScript1 = onloadScript1 & " alert('" & "Please verify that all selected requests have correct Vendor selected." & "');" & vbCrLf
                        ''onloadScript = onloadScript & "<" & "/" & "script>"
                        ' Register script with page 
                        strJs5 = onloadScript1
                        ''Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript1.ToString())

                        Exit Sub
                    End If

                End If
            End If
        Next

        Dim _PurchaseOrderNumber As String = ""
        Dim InLineNumber As String = ""

        Dim All_PurchaseOrderNumber As String = "PO # "

        InLineNumber = Date.Now.Year & Date.Now.Month & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond

        Dim _total As Decimal = 0

        For Each row As GridViewRow In OrderHeaderGrid.Rows
            Dim RowNumber As Integer = (OrderHeaderGrid.DataKeys(row.RowIndex).Value)
            Dim chk As CheckBox = row.FindControl("chkRow")
            If chk.Checked Then
                Dim lblLocationID As Label = row.FindControl("lblLocationID")
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

                Dim PopOrderNo As New CustomOrder()
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
                    Dim objemail As New clsemailtovendor
                    objemail.CompanyID = Me.CompanyID
                    objemail.DivisionID = Me.DivisionID
                    objemail.DepartmentID = Me.DepartmentID

                    If sendemail Then
                        objemail.EmailNotifications(_PurchaseOrderNumber)

                        objemail.txtfrom.Text = "claire@mccarthygroupflorists.com"

                        EmailSendingWithhCC(objemail.txtemailsubject.Text, objemail.divEmailContent.Text, objemail.txtfrom.Text, objemail.txtto.Text, objemail.txtcc.Text, objemail._vendorid)

                    End If



                End If

            Next

        End If


        If IsNumeric(_PurchaseOrderNumber) = True Then
        End If

        ''All_PurchaseOrderNumber

        All_PurchaseOrderNumber = All_PurchaseOrderNumber & " has generated."

        Dim onloadScript As String = ""
        ''onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & " alert('" & All_PurchaseOrderNumber & "');" & vbCrLf
        ''onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        strJs5 = onloadScript
        ''Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())

        


    End Sub


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




    Public Function Check_PurchaseHeader_PurchaseNumber(ByVal PO As String) As String

        Dim PurchaseNumber As String = ""

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  PurchaseNumber  from [PurchaseHeader]   where [PurchaseHeader].PurchaseNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = PO.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    PurchaseNumber = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return PurchaseNumber

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return PurchaseNumber

    End Function



    Public Function EmaillAddd() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
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
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
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
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function



    Public Function EmployeeEmailAddress() As String

        Try
            EmployeeID = Session("EmployeeID")
        Catch ex As Exception
            EmployeeID = "Admin"
        End Try

        Dim email As String = ""

        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
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
            HttpContext.Current.Response.Write(msg)
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
            mMailMessage.From = New MailAddress("claire@mccarthygroupflorists.com")
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


        If Me.CompanyID.ToLower() = "mccarthyg" Then
            mMailMessage.CC.Add(New MailAddress("pat@mccarthygroupflorists.com"))
        End If

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


    Public Sub newmailsending(ByVal Email As MailMessage)
        lblerrortestmail.Text = "In Functon newmailsending"
        'Dim lblerrortestmail As New TextBox


        Dim obj_InsertOutGoingMailDetails As New clsMailServer
        obj_InsertOutGoingMailDetails.CompanyID = CompanyID
        obj_InsertOutGoingMailDetails.DivisionID = DivisionID
        obj_InsertOutGoingMailDetails.DepartmentID = DepartmentID


        Try

            obj_InsertOutGoingMailDetails.From_Email = Email.From.ToString
            obj_InsertOutGoingMailDetails.To_Email = Email.To.ToString
            obj_InsertOutGoingMailDetails.CC_Email = Email.CC.ToString
            obj_InsertOutGoingMailDetails.Email_Subject = Email.Subject.ToString
            obj_InsertOutGoingMailDetails.Email_Body = Email.Body.ToString


            Dim Host As String = ""
            Dim Port As String = ""

            Dim NetworkCredential_username As String = ""
            Dim NetworkCredential_password As String = ""

            Dim Host2 As String = ""
            Dim Port2 As String = ""

            Dim NetworkCredential_username2 As String = ""
            Dim NetworkCredential_password2 As String = ""


            Dim obj As New clsMailServer
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            Dim dt As New Data.DataTable
            dt = obj.FillDetails

            If dt.Rows.Count <> 0 Then

                Host = dt.Rows(0)("MailServer1")
                Port = dt.Rows(0)("MailServerPort1")
                NetworkCredential_username = dt.Rows(0)("MailServerUserName1")
                NetworkCredential_password = dt.Rows(0)("MailServerPassword1")


                Host2 = dt.Rows(0)("MailServer2")
                Port2 = dt.Rows(0)("MailServerPort2")
                NetworkCredential_username2 = dt.Rows(0)("MailServerUserName2")
                NetworkCredential_password2 = dt.Rows(0)("MailServerPassword2")


                ''New code going to put
                Dim AccessKeyId As String = ""
                Dim SecrectAccesskey As String = ""
                Dim chkAmazonmail As Boolean = False
                Try
                    AccessKeyId = dt.Rows(0)("AccessKeyId")
                    SecrectAccesskey = dt.Rows(0)("SecrectAccesskey")
                    chkAmazonmail = dt.Rows(0)("chkAmazonmail")
                Catch ex As Exception

                End Try

                If chkAmazonmail Then

                    Dim body As String = Email.Body & "*" ' txtMessage.Text
                    Dim subject As String = Email.Subject 'txtSubject.Text


                    Dim client As New AmazonSimpleEmailServiceClient(AccessKeyId, SecrectAccesskey)
                    Dim sesemail As New Amazon.SimpleEmail.Model.SendEmailRequest()


                    sesemail.Message = New Amazon.SimpleEmail.Model.Message()
                    sesemail.Message.Body = New Amazon.SimpleEmail.Model.Body()
                    sesemail.Message.Body.Html = New Amazon.SimpleEmail.Model.Content(body)
                    sesemail.Message.Subject = New Amazon.SimpleEmail.Model.Content(subject)

                    Dim dst As New Destination()
                    Dim ToAddresses() As String = {Email.To(0).ToString}
                    Dim tolst As New System.Collections.Generic.List(Of String)(ToAddresses)

                    Try
                        If Email.CC(0).ToString.Trim <> "" Then
                            Dim CCAddresses() As String = {Email.CC(0).ToString}
                            Dim CClst As New System.Collections.Generic.List(Of String)(CCAddresses)
                            dst.CcAddresses = CClst
                        End If
                    Catch ex As Exception

                    End Try

                    dst.ToAddresses = tolst
                    sesemail.WithDestination(dst)
                    sesemail.WithSource(Email.From.ToString)
                    sesemail.WithReturnPath(Email.From.ToString)
                    Dim resp As New Amazon.SimpleEmail.Model.SendEmailResponse

                    Try
                        resp = client.SendEmail(sesemail)
                        lblerrortestmail.Text = "Mail Sent With Amazon Mail Services Details"
                        Exit Sub
                    Catch ex As Exception
                        lblerrortestmail.Text = "Error occured while send email by Amazon Mail Services :" & ex.Message
                        'Exit Sub
                    End Try

                End If

                '''


                If Host.Trim <> "" Then
                    Dim mailClient As New System.Net.Mail.SmtpClient()

                    'This object stores the authentication values

                    If NetworkCredential_username.Trim <> "" And NetworkCredential_password.Trim <> "" Then
                        Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username.Trim, NetworkCredential_password.Trim)
                        'mailClient.UseDefaultCredentials = False
                        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                        mailClient.Credentials = basicAuthenticationInfo
                    Else
                        mailClient.UseDefaultCredentials = True
                    End If

                    'Put your own, or your ISPs, mail server name onthis next line


                    mailClient.Host = Host.Trim
                    If Port.Trim <> "" Then
                        mailClient.Port = Port.Trim
                    End If

                    Try
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        lblerrortestmail.Text = "Mail Sent With Primary SMTP Details"

                    Catch ex As SmtpException
                        Dim Email_Subject1 As String = obj_InsertOutGoingMailDetails.Email_Subject

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        obj_InsertOutGoingMailDetails.Email_Subject = Email_Subject1

                        lblerrortestmail.Text = "Error From PRIMARY SMTP:" & ex.Message
                        If Host2.Trim <> "" Then
                            mailClient = New System.Net.Mail.SmtpClient()
                            'This object stores the authentication values

                            If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                                'mailClient.UseDefaultCredentials = False
                                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                                mailClient.Credentials = basicAuthenticationInfo
                            Else
                                mailClient.UseDefaultCredentials = True
                            End If

                            'Put your own, or your ISPs, mail server name onthis next line


                            mailClient.Host = Host2.Trim
                            If Port2.Trim <> "" Then
                                mailClient.Port = Port2.Trim
                            End If

                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                            mailClient.Send(Email)
                            '''''Email Details storing''''
                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                            '''''Email Details storing''''
                            lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"
                        End If

                    End Try



                Else
                    If Host2.Trim <> "" Then
                        Dim mailClient As New System.Net.Mail.SmtpClient()
                        'This object stores the authentication values

                        If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                            Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                            mailClient.UseDefaultCredentials = False
                            mailClient.Credentials = basicAuthenticationInfo
                        Else
                            mailClient.UseDefaultCredentials = True
                        End If

                        'Put your own, or your ISPs, mail server name onthis next line


                        mailClient.Host = Host2.Trim
                        If Port2.Trim <> "" Then
                            mailClient.Port = Port2.Trim
                        End If

                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)
                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"

                    Else

                        'Dim mailClient As New System.Net.Mail.SmtpClient()
                        ''This object stores the authentication values
                        ''mailClient.UseDefaultCredentials = True
                        ''Put your own, or your ISPs, mail server name onthis next line
                        ''mailClient.Host = "8.3.16.126"
                        ''mailClient.Port = "25"
                        'mailClient.Send(Email)
                        ''''''Email Details storing''''
                        'obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        'obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        'obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

                    End If
                End If



            Else

                Dim mailClient As New System.Net.Mail.SmtpClient()
                'This object stores the authentication values
                'mailClient.UseDefaultCredentials = True
                'Put your own, or your ISPs, mail server name onthis next line
                mailClient.Host = "8.3.16.126"
                mailClient.Port = "25"
                mailClient.Send(Email)
                '''''Email Details storing''''
                obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                ''''Email Details storing''''
                lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

            End If

        Catch ex As FormatException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Format Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("Format Exception: " & ex.Message)

        Catch ex As SmtpException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send SMTP Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''
            lblerrortestmail.Text = ("SMTP Exception:  " & ex.Message)

        Catch ex As Exception

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send General Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("General Exception:  " & ex.Message)

        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnreset.Click
        Response.Redirect("BatchPO.aspx")
    End Sub

    Private Sub OrderHeaderGrid_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles OrderHeaderGrid.RowCancelingEdit
        'Vendor_Remarks
    End Sub

    Private Sub drpDefaultBuyStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpDefaultBuyStatus.SelectedIndexChanged
        Session("drpDefaultBuyStatus") = drpDefaultBuyStatus.SelectedValue
        fillitems("1")
    End Sub

    Private Sub chkincludeBought_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkincludeBought.CheckedChanged, chkincludeStanding.CheckedChanged, chkNotavailable.CheckedChanged, chkonlyStanding.CheckedChanged, chkWithother.CheckedChanged
        fillitems("1")
    End Sub


End Class
