Imports System.Data
Imports System.Data.SqlClient

Partial Class AjaxItemsSearchPOS
    Inherits System.Web.UI.Page
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim CustomerID As String = ""

    Public result As String = ""
    Public locationid As String = ""
    Dim ProductCategory As String = ""
    Dim drpProductFamily As String = ""
    Dim ProductColor As String = ""
    Dim ProductSize As String = ""
    Dim ProductGroup As String = ""
    Public currsymb As String = "$"

    Private Sub AjaxItemsSearchPOS_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim CustomerTypeID As String = ""
        Try
            CustomerTypeID = Session("CustomerTypeID")
        Catch ex As Exception
        End Try

        Try
            ' locationid = Session("OrderLocationid")
        Catch ex As Exception
        End Try

        Try
            ProductCategory = Request.QueryString("ProductCategory")
            ProductCategory = ProductCategory.Trim
        Catch ex As Exception
            ProductCategory = ""
        End Try
        Try
            drpProductFamily = Request.QueryString("drpProductFamily")
            drpProductFamily = drpProductFamily.Trim
        Catch ex As Exception
            drpProductFamily = ""
        End Try

        Try
            ProductColor = Request.QueryString("ProductColor")
            ProductColor = ProductColor.Trim
        Catch ex As Exception
            ProductColor = ""
        End Try

        Try
            ProductSize = Request.QueryString("ProductSize")
            ProductSize = ProductSize.Trim
        Catch ex As Exception
            ProductSize = ""
        End Try

        Try
            ProductGroup = Request.QueryString("ProductGroup")
            ProductGroup = ProductGroup.Trim
        Catch ex As Exception
            ProductGroup = ""
        End Try

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")
        If CompanyID = "Meliaforflowersandgifts33301" Then
            currsymb = "QR "
        End If

        Dim LocationID As String = Request.QueryString("locationid")
        Dim OrderDate As String = DateAndTime.Now.ToShortDateString  ' Request.QueryString("od")
        Dim DeliveryDate As String = Request.QueryString("sd")

        Me.locationid = LocationID
        If CompanyID.ToUpper = "HBFARM" Then
            Me.locationid = ""
        End If

        Dim dt_PopulateManageLiveInventory As New DataTable
        dt_PopulateManageLiveInventory = PopulateManageLiveInventory()

        Dim ManageLiveInventory As Boolean = False
        Dim ItemsColumn As Boolean = False

        If dt_PopulateManageLiveInventory.Rows.Count <> 0 Then

            Try
                ManageLiveInventory = dt_PopulateManageLiveInventory.Rows(0)(0)
            Catch ex As Exception

            End Try
            Try
                ItemsColumn = dt_PopulateManageLiveInventory.Rows(0)(1)
            Catch ex As Exception

            End Try
        End If

        If Not Request.QueryString("lc") = Nothing Then
            CustomerTypeID = Request.QueryString("lc")
        End If
        If Not Request.QueryString("k") = Nothing Then
            Dim keyword As String = ""
            keyword = Request.QueryString("k")

            If keyword <> "" Then

                Dim dt As New DataTable
                If Me.CompanyID.ToUpper = "FreytagsFlorist".ToUpper Or Me.CompanyID.ToUpper = "SAFC".ToUpper Then
                    ManageLiveInventory = False
                End If
                If Me.CompanyID.ToUpper = "PhillipsFlowersGifts60559".ToUpper Then
                    ManageLiveInventory = False
                End If
                If ManageLiveInventory Then
                    ''Response.Write("WithInventoryItemSearch")
                    dt = WithInventoryItemSearch(keyword, OrderDate, DeliveryDate)
                Else
                    ''Response.Write("WithoutInventoryItemSearch")
                    'dt = WithoutInventoryItemSearch(keyword)
                    If Me.CompanyID.ToUpper = "PhillipsFlowersGifts60559".ToUpper Or Me.CompanyID.ToUpper = "FreytagsFlorist".ToUpper Or Me.CompanyID.ToUpper = "SAFC".ToUpper Then
                        dt = WithoutInventoryItemSearch(keyword)
                    Else
                        dt = ExactInventoryItemSearch(keyword)
                        If dt.Rows.Count <> 1 Then
                            ''Response.Write("WithoutInventoryItemSearch")
                            dt = WithoutInventoryItemSearch(keyword)
                        End If
                    End If
                End If

                ''Response.Write("dt.Rows.Count:" & dt.Rows.Count)
                ''Response.Write("")

                ' Dim str As String = "<table width='100%'   class='table table-striped table-hover table-bordered' >"

                Dim str As String = "<table width='100%' id='display-table'  class='fixed_headers table table-bordered table-condensed table-hover' >"
                str = str & "<thead><tr  width='100%' > "
                str = str & "<th style='display:none;' width='0' ></th>"
                str = str & "<th align='left' >Item ID</th>"
                If CompanyID.ToUpper = "HBFARM" Then
                    str = str & " <th align='left' >Price </th>"
                Else
                    str = str & " <th align='left' >Item Name</th> <th align='left' >Price </th>"
                End If
                If CompanyID.ToUpper = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
                    str = str & " <th align='left' >UOM </th>"
                End If

                If ItemsColumn Or CompanyID = "ICON" Or CompanyID = "metroflowermarket" Or CompanyID = "SouthFloral" Or CompanyID = "SouthFloralsTraining" Or CompanyID = "JoseDiazWH" Or CompanyID = "FloricaWholesale" Or CompanyID = "wholesaledemo" Or CompanyID = "CowardandGlisson33955" Or CompanyID = "FloristSoftwareDemo" Or CompanyID = "McCarthyg" Or CompanyID = "FloraVina33301" Or CompanyID = "MarshallsWholesaleFlowers" Or CompanyID = "HBFARM" Or CompanyID = "MatrangaFloral" Or CompanyID = "DierbergsMarkets,Inc63017" Or CompanyID = "FreytagsFlorist" Or Me.CompanyID.ToUpper = "SAFC".ToUpper Then
                    str = str & "<th align='left' >QOH </th> <th align='left' >QTR</th> <th align='left'  >AvSale</th> <th align='left' >Variety</th>"
                    str = str & "<th align='left' >Start Date</th> <th align='left' >End Date</th> <th align='left' >Grower </th> "
                    If CompanyID.ToUpper = "HBFARM" Then
                        str = str & "<th align='left' >Description </th> "
                    End If
                Else
                    str = str & "<th align='left' >Description </th> "
                End If

                str = str & "</tr></thead><tbody style='height:345px;'>"
                If dt.Rows.Count <> 0 Then

                    Dim n As Integer = 0
                    Dim gridrow As Boolean = True
                    For Each row As DataRow In dt.Rows
                        n = n + 1
                        keyword = keyword.ToLower
                        Dim ItemDescription As String = row("ItemDescription").ToString()
                        ItemDescription = ItemDescription.ToLower
                        ItemDescription = ItemDescription.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                        Dim ItemTypeID As String = row("ItemTypeID").ToString()

                        Dim Variety As String = ""
                        Dim UOM As String = ""
                        Dim StartDateAvailable As String = ""
                        Dim EndDateAvailable As String = ""
                        Dim Grower As String = ""
                        Dim _QtyOnHand As String = ""

                        Try
                            UOM = row("ItemUOM").ToString()
                            If UOM.Trim = "" Then
                                UOM = "&nbsp;&nbsp;&nbsp;&nbsp;"
                            End If
                        Catch ex As Exception

                        End Try
                        Try
                            Variety = row("Variety").ToString()
                            If Variety.Trim = "" Then
                                Variety = "&nbsp;&nbsp;&nbsp;&nbsp;"
                            End If
                        Catch ex As Exception

                        End Try
                        Try
                            Grower = row("Grower").ToString()
                            If Grower.Trim = "" Then
                                Grower = "&nbsp;&nbsp;&nbsp;&nbsp;"
                            End If
                        Catch ex As Exception

                        End Try
                        Try
                            _QtyOnHand = row("QtyOnHand").ToString()
                            If _QtyOnHand.Trim = "" Then
                                _QtyOnHand = "0"
                            End If
                        Catch ex As Exception
                            _QtyOnHand = "0"
                        End Try
                        Try
                            Dim DT_StartDateAvailable As Date
                            DT_StartDateAvailable = row("StartDateAvailable").ToString()
                            StartDateAvailable = DT_StartDateAvailable.ToShortDateString
                        Catch ex As Exception
                            StartDateAvailable = "&nbsp;&nbsp;&nbsp;&nbsp;"
                        End Try
                        Try
                            Dim DT_EndDateAvailable As Date
                            DT_EndDateAvailable = row("EndDateAvailable").ToString()
                            EndDateAvailable = DT_EndDateAvailable.ToShortDateString
                        Catch ex As Exception
                            EndDateAvailable = "&nbsp;&nbsp;&nbsp;&nbsp;"
                        End Try

                        Dim ItemName As String = row("ItemName").ToString()
                        ItemName = ItemName.ToLower
                        ItemName = ItemName.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                        Dim Itemprice As Double = 0
                        Try
                            Itemprice = row("Price").ToString()
                        Catch ex As Exception

                        End Try

                        If CustomerTypeID = "WHO" Or CustomerTypeID = "WHOC" Or CustomerTypeID = "Wholesale" Then
                            Try
                                Itemprice = row("wholesalePrice").ToString()
                            Catch ex As Exception

                            End Try
                        End If

                        If ItemDescription.Trim = "" Then
                            '  ItemDescription = ItemName
                        End If

                        Dim ItemID As String = row("ItemID").ToString()
                        Dim S_ItemID As String = row("ItemID").ToString()
                        ItemID = ItemID.ToUpper
                        ItemID = ItemID.Replace(keyword.ToUpper, "<font color='red'>" + keyword.ToUpper + "</font>")

                        Dim IsAssembly As Boolean = row("IsAssembly")

                        Dim finalsend As String = ""
                        Dim pr As Decimal = 0

                        Try
                            pr = Itemprice
                        Catch ex As Exception

                        End Try

                        finalsend = finalsend & row("ItemID").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")
                        finalsend = finalsend & "~!"
                        Try
                            finalsend = finalsend & Format(pr, "0.00").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")
                        Catch ex As Exception

                        End Try
                        If CompanyID.ToUpper = "HBFARM" Then
                            finalsend = finalsend & "~!"
                            finalsend = finalsend & row("ItemDescription").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")

                        Else
                            finalsend = finalsend & "~!"
                            finalsend = finalsend & row("ItemName").ToString().Replace("'", "").Replace(";", "").Replace("~!", "").Replace("""", "").Replace("(", "").Replace(")", "")

                        End If

                        Dim QtyOnHand As Integer = -9999
                        Dim loc As String = "DEFAULT"
                        Try
                            ''loc = Session("Locationid")
                            loc = Request.QueryString("locationid")
                        Catch ex As Exception
                            ''QtyOnHand= 1
                        End Try

                        'If ManageLiveInventory and ItemTypeID = "Stock" Then
                        '    ''QtyOnHand= 2
                        '    Dim dt_PopulateQtyOnHand As New DataTable
                        '    dt_PopulateQtyOnHand = PopulateQtyOnHand(loc, S_ItemID)
                        '    If dt_PopulateQtyOnHand.Rows.Count <> 0 Then
                        '       '' QtyOnHand= 3
                        '        QtyOnHand = dt_PopulateQtyOnHand.Rows(0)(0)
                        '    else
                        '       '' QtyOnHand= 4
                        '    End If
                        'End If

                        If ManageLiveInventory And ItemTypeID = "Stock" Then
                            ''QtyOnHand= 2
                            QtyOnHand = PopulateQtyOnHand(loc, S_ItemID, IsAssembly)
                        End If

                        finalsend = finalsend & "~!"
                        finalsend = finalsend & QtyOnHand.ToString()

                        If ManageLiveInventory And ItemTypeID = "Stock" Then
                            'Qty to be received
                            finalsend = finalsend & "~!"
                            finalsend = finalsend & row("QtyToBeReceived").ToString()

                            'available qty
                            finalsend = finalsend & "~!"
                            finalsend = finalsend & row("AvailableQtyForSell").ToString()
                        End If

                        'grid-alternative-row
                        If gridrow Then
                            str = str & "<tr align='left'  style='cursor:pointer;' width='100%'   id='InventoryInfoGrid_tr" & n & "'  >"
                            gridrow = False
                        Else
                            str = str & "<tr align='left' style='cursor:pointer;'  width='100%'   id='InventoryInfoGrid_tr" & n & "'  >"
                            gridrow = True
                        End If

                        str = str & "<td style='display:none;' width='0' >" + finalsend + "</td>"
                        'If Me.CompanyID.ToLower() = "QuickfloraDemo".ToLower() Or Me.CompanyID.ToLower() = "JWF".ToLower() Then
                        '    str = str & "<td align='left' style='position: relative;'> <a href='javascript:void(0);' onmouseout='closeDesc(this)' onmouseover='openDesc(`" + S_ItemID + "`,this)'>" + ItemID + " </a><span class='preview_desc' onmouseout='closeDirectDesc(this)' onmouseover='openDirectDesc(this)'></span></td>"
                        'Else
                        str = str & "<td align='left'>" + ItemID + "</td>"
                        'End If
                        ' str = str & "<td align='left'><a  href=" & """" & "javascript:FillSearchtextBox2('" + finalsend + "')" + """" + " >" + ItemID + "</a></td>"
                        ' str = str & "<td >" & ItemName & "</td>"
                        If CompanyID.ToUpper = "HBFARM" Then
                            ' str = str & "<td >" & "" & "</td>"
                        Else
                            str = str & "<td >" & ItemName & "</td>"
                        End If
                        str = str & "<td align='left'>" & currsymb & Format(Itemprice, "0.00") & ""
                        str = str & "<div id='InventoryInfoGrid_tr" & n & "_gridPopup' style='display: none; z-index: 101; position: absolute;'><br />"
                        str = str & "<img border='0' src='" & returl(row("PictureURL").ToString()) & "'></div>"
                        str = str & "</td>"

                        If CompanyID.ToUpper = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
                            str = str & "<td title='Item UOM' >" & UOM & "</td>"
                        End If

                        If ItemsColumn Or CompanyID = "ICON" Or CompanyID = "metroflowermarket" Or CompanyID = "SouthFloral" Or CompanyID = "SouthFloralsTraining" Or CompanyID = "JoseDiazWH" Or CompanyID = "FloricaWholesale" Or CompanyID = "wholesaledemo" Or CompanyID = "CowardandGlisson33955" Or CompanyID = "FloristSoftwareDemo" Or CompanyID = "McCarthyg" Or CompanyID = "FloraVina33301" Or CompanyID = "MarshallsWholesaleFlowers" Or CompanyID = "HBFARM" Or CompanyID = "MatrangaFloral" Or CompanyID = "DierbergsMarkets,Inc63017" Or CompanyID = "FreytagsFlorist" Or CompanyID = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
                            str = str & "<td title='Qty On Hand' >" & _QtyOnHand & "</td>"

                            If ManageLiveInventory And ItemTypeID = "Stock" Then

                                Dim QtyToBeReceived As Integer = 0
                                Try
                                    QtyToBeReceived = row("QtyToBeReceived")
                                Catch ex As Exception

                                End Try

                                str = str & "<td title='Qty To Be Received' >" & QtyToBeReceived.ToString() & "</td>"
                                Dim aa As Integer = 0
                                Dim bb As Integer = 0
                                Try
                                    aa = _QtyOnHand
                                Catch ex As Exception

                                End Try
                                Try
                                    bb = row("QtyToBeReceived")
                                Catch ex As Exception

                                End Try
                                '' str = str & "<td title='Available Qty For Sell' >" & aa + bb & "</td>"
                                str = str & "<td title='Available Qty For Sell' >" & row("AvailableQtyForSell").ToString() & "</td>"
                            Else
                                str = str & "<td title='Qty To Be Received' >" & "0".ToString() & "</td>"
                                str = str & "<td title='Available Qty For Sell' >" & "0".ToString() & "</td>"
                            End If

                            str = str & "<td title='Variety' >" & Variety & "</td>"
                            str = str & "<td title='Start Date Available' >" & StartDateAvailable & "</td>"
                            str = str & "<td title='End Date Available' >" & EndDateAvailable & "</td>"
                            str = str & "<td  title='Grower '>" & Grower & "</td>"
                            If CompanyID.ToUpper = "HBFARM" Then
                                str = str & "<td >" & ItemDescription & "</td>"
                            End If

                        Else

                            str = str & "<td >" & ItemDescription & "</td>"
                        End If


                        str = str & "</tr>"
                        'Response.Write("<div style='text-decoration:none; border:dotted; border-width:1px; border-color:White; width:660px;'><a  href=" & """" & "javascript:FillSearchtextBox2('[" + row("ItemID").ToString() + "]')" + """" + " > <img  width='25' height='25' src='" + returl(row("PictureURL").ToString()) + "' /> <strong>" + ItemID + "</strong>  <i>" + ItemName + "</i> : " + ItemDescription + "</a></div>")

                    Next
                    str = str & "</tbody></table>"
                    Response.Write(str)
                    result = str
                    Response.End()
                Else
                    'Response.Write()
                    Response.Clear()
                    Response.Write(" ")
                    Response.End()
                End If

            End If

        End If

    End Sub


    Public Function PopulateManageLiveInventory() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""

        'ssql = "Select isnull(ManageLiveInventory, 0) as ManageLiveInventoryImmediateDelivery, isnull(ManageLiveInventoryFutureDelivery,0) as ManageLiveInventoryFutureDelivery  from companies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        ssql = "Select isnull(ManageLiveInventory, 0) as ManageLiveInventory,ISNULL(ItemsColumn,0) AS 'ItemsColumn' from companies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "

        Dim dt As New DataTable
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message

            Return dt
        End Try
        Return dt
    End Function




    Private Function ExactInventoryItemSearch(ByVal keyword As String) As DataTable

        Dim sql As String = "" 'select top 35 ItemID,ItemDescription,ItemName,Price,PictureURL,wholesalePrice,[ItemTypeID], Isnull(IsAssembly,0) as IsAssembly "
        sql = sql & " select top 35 InventoryItems.ItemID,InventoryItems.ItemDescription,InventoryItems.ItemName,InventoryItems.Price,InventoryItems.PictureURL,InventoryItems.wholesalePrice,InventoryItems.[ItemTypeID], Isnull(InventoryItems.IsAssembly,0) as IsAssembly "
        sql = sql & " ,InventoryItems.Variety, InventoryItems.StartDateAvailable ,InventoryItems.EndDateAvailable ,InventoryItems.Grower "
        sql = sql & " from [InventoryItems] "
        sql = sql & " where  [InventoryItems].ItemID <> 'DEFAULT' AND [InventoryItems].CompanyID=@CompanyID AND [InventoryItems].DivisionID=@DivisionID AND [InventoryItems].DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 and [InventoryItems].[ItemID]  = '" & keyword & "' "


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand(sql, myCon)
        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

        Dim da As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable

        'populate the data control and bind the data source

        da.Fill(dt)

        Return dt

    End Function



    Private Function WithoutInventoryItemSearch(ByVal keyword As String) As DataTable

        Dim sql As String = "" 'select top 35 ItemID,ItemDescription,ItemName,Price,PictureURL,wholesalePrice,[ItemTypeID], Isnull(IsAssembly,0) as IsAssembly "

        'sql = sql & " select top 35 InventoryItems.ItemID,InventoryItems.ItemDescription,InventoryItems.ItemName,InventoryItems.Price,InventoryItems.PictureURL,InventoryItems.wholesalePrice,InventoryItems.[ItemTypeID], Isnull(InventoryItems.IsAssembly,0) as IsAssembly "
        'sql = sql & " ,InventoryItems.Variety, InventoryItems.StartDateAvailable ,InventoryItems.EndDateAvailable ,InventoryItems.Grower ,[InventoryByWarehouse].QtyOnHand "
        'sql = sql & " from [InventoryItems] LEFT OUTER JOIN [InventoryByWarehouse] ON "
        'sql = sql & " [InventoryItems].CompanyID = [InventoryByWarehouse].CompanyID AND "
        'sql = sql & " [InventoryItems].DivisionID = [InventoryByWarehouse].DivisionID AND "
        'sql = sql & " [InventoryItems].DepartmentID = [InventoryByWarehouse].DepartmentID AND "
        'sql = sql & " [InventoryItems].ItemID = [InventoryByWarehouse].ItemID "
        'sql = sql & "   where  [InventoryItems].ItemID <> 'DEFAULT' AND [InventoryItems].CompanyID=@CompanyID AND [InventoryItems].DivisionID=@DivisionID AND [InventoryItems].DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 and ([InventoryItems].[ItemID]  like '%" + keyword.Trim().Replace("'", "''") + "%' Or [InventoryItems].[ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [InventoryItems].[ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "

        sql = sql & " select top 100 InventoryItems.ItemID,InventoryItems.ItemDescription,InventoryItems.ItemName,InventoryItems.Price,InventoryItems.PictureURL,InventoryItems.wholesalePrice,InventoryItems.[ItemTypeID], Isnull(InventoryItems.IsAssembly,0) as IsAssembly "
        sql = sql & " ,InventoryItems.Variety, InventoryItems.StartDateAvailable ,InventoryItems.EndDateAvailable ,InventoryItems.Grower "
        sql = sql & " from [InventoryItems] "
        sql = sql & " where  [InventoryItems].ItemID <> 'DEFAULT' AND [InventoryItems].CompanyID=@CompanyID AND [InventoryItems].DivisionID=@DivisionID AND [InventoryItems].DepartmentID=@DepartmentID AND  InventoryItems.IsActive=1 and InventoryItems.WireServiceIdAllowed = 1 and ([InventoryItems].[ItemID]  like '%" & keyword.Trim().Replace("'", "''") + "%' "
        If CompanyID = "CamelbackFlowershop85018" Or Me.CompanyID.ToUpper = "PhillipsFlowersGifts60559".ToUpper Then
            sql = sql & " Or [InventoryItems].[ItemDescription] like '%" + keyword.Trim().Replace("'", "''") + "%' "
        End If

        If CompanyID.ToLower = "FreytagsFlorist".ToLower Or Me.CompanyID.ToUpper = "SAFC".ToUpper Or Me.CompanyID.ToUpper = "JWF".ToUpper Or CompanyID.ToUpper = "NEWWF" Then
            Dim keywoordarray() As String
            keywoordarray = keyword.Split(" ")
            Dim nf As Integer

            If keywoordarray.Length = 1 Then
                sql = sql & " Or [InventoryItems].[ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [InventoryItems].[ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "
            Else
                sql = sql & "  Or [InventoryItems].[ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' Or ( "
                For nf = 0 To keywoordarray.Length - 1
                    If nf <> 0 Then
                        sql = sql & " and "
                    End If
                    sql = sql & "   [InventoryItems].[ItemName] like '%" + keywoordarray(nf).Trim().Replace("'", "''") + "%'  "
                Next
                sql = sql & " ) )"
            End If
            If ProductCategory <> "" Then
                sql = sql & "   AND ISNULL(ItemCategoryID ,'') = '" & ProductCategory & "'"
            End If
            'drpProductFamily
            If drpProductFamily <> "" Then
                sql = sql & "   AND ISNULL(ItemFamilyID ,'') = '" & drpProductFamily & "'"
            End If
            If ProductColor <> "" Then
                sql = sql & "   AND ISNULL(ItemColor ,'') Like '%" & ProductColor & "%'"
            End If
            If ProductSize <> "" Then
                sql = sql & "   AND ISNULL(ItemSize ,'') Like '%" & ProductSize & "%'"
            End If
        Else
            sql = sql & " Or [InventoryItems].[ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [InventoryItems].[ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "
        End If
        ' sql = sql & " Or [InventoryItems].[ItemName] like '%" + keyword.Trim().Replace("'", "''") + "%'  Or [InventoryItems].[ItemUPCCode] like '%" + keyword.Trim().Replace("'", "''") + "%' ) "

        If CompanyID = "DierbergsMarkets,Inc63017" Then
            ' sql = sql & " AND [InventoryItems].[ActiveForRecipe] = 0 "
        End If

        'Response.Write(sql)


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand(sql, myCon)
        myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
        myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
        myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

        Dim da As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable

        'populate the data control and bind the data source

        da.Fill(dt)

        Return dt

    End Function

    Private Function WithInventoryItemSearch(ByVal KeyWord As String, ByVal OrderDate As String, ByVal DeliveryDate As String) As DataTable

        Dim dt As New DataTable

        If CompanyID = "DierbergsMarkets,Inc63017" Then

            Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using command As New SqlCommand("[enterprise].[NewItemSearchWithInventoryDetailsDierbergsMarkets]", connection)
                    command.CommandType = CommandType.StoredProcedure

                    command.Parameters.AddWithValue("CompanyID", CompanyID)
                    command.Parameters.AddWithValue("DivisionID", DivisionID)
                    command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                    command.Parameters.AddWithValue("locationid", Me.locationid)
                    '@locationid
                    command.Parameters.AddWithValue("KeyWord", KeyWord.Trim().Replace("'", "''").Trim().Replace("'", "''").Trim().Replace("'", "''"))
                    command.Parameters.AddWithValue("OrderDate", OrderDate)
                    command.Parameters.AddWithValue("DeliveryDate", DeliveryDate)

                    Try
                        Dim da As New SqlDataAdapter(command)
                        da.Fill(dt)
                    Catch ex As Exception

                    End Try

                End Using

            End Using

        ElseIf CompanyID.ToLower = "JWF".ToLower Or CompanyID.ToUpper = "NEWWF" Then

            Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using command As New SqlCommand("[enterprise].[NewItemSearchWithInventoryDetails_JWF_]", connection)
                    command.CommandType = CommandType.StoredProcedure

                    command.Parameters.AddWithValue("CompanyID", CompanyID)
                    command.Parameters.AddWithValue("DivisionID", DivisionID)
                    command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                    command.Parameters.AddWithValue("locationid", Me.locationid)
                    command.Parameters.AddWithValue("family", drpProductFamily)
                    command.Parameters.AddWithValue("category", ProductCategory)
                    command.Parameters.AddWithValue("color", ProductColor)
                    command.Parameters.AddWithValue("size", ProductSize)
                    command.Parameters.AddWithValue("grp", ProductGroup)
                    '@locationid
                    command.Parameters.AddWithValue("KeyWord", KeyWord.Trim().Replace("'", "''").Trim().Replace("'", "''").Trim().Replace("'", "''"))
                    command.Parameters.AddWithValue("OrderDate", OrderDate)
                    command.Parameters.AddWithValue("DeliveryDate", DeliveryDate)

                    Try
                        Dim da As New SqlDataAdapter(command)
                        da.Fill(dt)
                    Catch ex As Exception

                    End Try

                End Using

            End Using

        Else

            Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Using command As New SqlCommand("[enterprise].[NewItemSearchWithInventoryDetails]", connection)
                    command.CommandType = CommandType.StoredProcedure

                    command.Parameters.AddWithValue("CompanyID", CompanyID)
                    command.Parameters.AddWithValue("DivisionID", DivisionID)
                    command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                    command.Parameters.AddWithValue("locationid", Me.locationid)
                    '@locationid
                    command.Parameters.AddWithValue("KeyWord", KeyWord.Trim().Replace("'", "''").Trim().Replace("'", "''").Trim().Replace("'", "''"))
                    command.Parameters.AddWithValue("OrderDate", OrderDate)
                    command.Parameters.AddWithValue("DeliveryDate", DeliveryDate)

                    Try
                        Dim da As New SqlDataAdapter(command)
                        da.Fill(dt)
                    Catch ex As Exception

                    End Try

                End Using

            End Using

        End If


        Return dt

    End Function

    'Public Function PopulateQtyOnHand(ByVal LocationID As String, ByVal ItemID As String) As DataTable
    '    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim connec As New SqlConnection(constr) 
    '    Dim ssql As String = ""

    '    ssql = "Select isnull(QtyOnHand, 0) from InventoryByWarehouse where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and LocationID=@f3 and ItemID=@f4 "
    '    Dim dt As New DataTable
    '    Dim com As SqlCommand
    '    com = New SqlCommand(ssql, connec)

    '        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
    '        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
    '        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
    '        com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = LocationID
    '        com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = ItemID

    '        Dim da As New SqlDataAdapter(com)

    '        da.Fill(dt)
    '        Return dt

    '    Return dt
    'End Function

    Public Function PopulateQtyOnHand(ByVal LocationID As String, ByVal ItemID As String, ByVal IsAssembly As Boolean) As Long

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[GetMinimumAvailableQtyForItem]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)

                command.Parameters.AddWithValue("LocationID", LocationID)
                command.Parameters.AddWithValue("ItemID", ItemID)
                command.Parameters.AddWithValue("IsAssembly", IsAssembly)

                'command.Parameters.AddWithValue("CompanyID", Me.CompanyID)

                Try
                    command.Connection.Open()
                    Return Convert.ToInt64(command.ExecuteScalar())
                Catch ex As Exception
                    Return 0
                Finally
                    command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


    Public Function returl(ByVal ob As String) As String

        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("InvPath")
        If (ImgName.Trim() = "") Then

            Return "itemimages/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "itemimages/" & ImgName.Trim()

            Else
                Return "itemimages/no_image.gif"
            End If




        End If


    End Function



End Class
