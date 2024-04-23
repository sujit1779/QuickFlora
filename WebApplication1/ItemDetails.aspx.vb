

Imports System.Data
Imports System.Data.SqlClient

Partial Class ItemDetails
    Inherits System.Web.UI.Page

    Dim obj As New clsItems
    Public CompanyID As String, DivisionID As String, DepartmentID As String

    Dim EmployeeID As String = ""


    Private Sub FillLedgerAccounts()

        Dim dsLedgerChartOfAccount As New DataSet

        dsLedgerChartOfAccount = obj.GetLedgerChartOfAccount()

        drpGLItemSalesAccount.DataValueField = "GLAccountNumber"
        drpGLItemSalesAccount.DataTextField = "GLAccountName"
        drpGLItemSalesAccount.DataSource = dsLedgerChartOfAccount
        drpGLItemSalesAccount.DataBind()
        drpGLItemSalesAccount.Items.Insert(0, "")

        drpGLItemCOGSAccount.DataValueField = "GLAccountNumber"
        drpGLItemCOGSAccount.DataTextField = "GLAccountName"
        drpGLItemCOGSAccount.DataSource = dsLedgerChartOfAccount
        drpGLItemCOGSAccount.DataBind()
        drpGLItemCOGSAccount.Items.Insert(0, "")

        drpGLItemInventoryAccount.DataValueField = "GLAccountNumber"
        drpGLItemInventoryAccount.DataTextField = "GLAccountName"
        drpGLItemInventoryAccount.DataSource = dsLedgerChartOfAccount
        drpGLItemInventoryAccount.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpGLItemInventoryAccount.Items.Insert(0, lst)

    End Sub

    Private Sub FillTaxGroups()

        Dim dsTaxGroups As New DataSet
        dsTaxGroups = obj.GetTaxGroups()

        drpTaxbleGroupID.DataTextField = "TaxGroupID"
        drpTaxbleGroupID.DataValueField = "TaxGroupID"
        drpTaxbleGroupID.DataSource = dsTaxGroups
        drpTaxbleGroupID.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpTaxbleGroupID.Items.Insert(0, lst)

    End Sub

    Private Sub FillVendor()

        Dim objVendor As New clsVendor
        Dim ds As New DataSet
        ds = objVendor.GetVendorList(CompanyID, DivisionID, DepartmentID)

        drpVendor.DataTextField = "VendorID"
        drpVendor.DataValueField = "VendorName"
        drpVendor.DataSource = ds
        drpVendor.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpVendor.Items.Insert(0, lst)

    End Sub

    Private Sub FillEmployee()

        Dim ds As New DataSet
        ds = obj.GetEmployeeList()

        drpEnteredBy.DataTextField = "EmployeeID"
        drpEnteredBy.DataValueField = "EmployeeID"
        drpEnteredBy.DataSource = ds
        drpEnteredBy.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpEnteredBy.Items.Insert(0, lst)

    End Sub

    Private Sub FillImageCopyrightHolders()

        Dim ds As New DataSet
        ds = obj.GetImageCopyrightholders()

        drpImageCopyrightHolder.DataTextField = "WireServiceID"
        drpImageCopyrightHolder.DataValueField = "WireServiceDescription"
        drpImageCopyrightHolder.DataSource = ds
        drpImageCopyrightHolder.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpImageCopyrightHolder.Items.Insert(0, lst)

    End Sub

    Public Function Fillfamily() As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 order by InventoryFamilies.FamilyName Asc "
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
            HttpContext.Current.Response.Write(msg)
            Return dt

        End Try


    End Function


    Public Function FillDetailscategory(ByVal familyid As String) As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryCategories where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3 "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = familyid

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt

        End Try

    End Function



    Private Sub FillItemFamilies()

        Dim dtfamily As New DataTable
        dtfamily = Fillfamily()

        drpItemFamilyID1.DataSource = dtfamily
        drpItemFamilyID1.DataTextField = "FamilyName"
        drpItemFamilyID1.DataValueField = "ItemFamilyID"
        drpItemFamilyID1.DataBind()

        drpItemFamilyID2.DataSource = dtfamily
        drpItemFamilyID2.DataTextField = "FamilyName"
        drpItemFamilyID2.DataValueField = "ItemFamilyID"
        drpItemFamilyID2.DataBind()


        drpItemFamilyID3.DataSource = dtfamily
        drpItemFamilyID3.DataTextField = "FamilyName"
        drpItemFamilyID3.DataValueField = "ItemFamilyID"
        drpItemFamilyID3.DataBind()

    End Sub

    Private Sub FillInventoryAssemblyList()

        Dim ds As New DataSet
        ds = obj.GetInventoryAssemblyList()

        drpItemAssembly.DataTextField = "AssemblyID"
        drpItemAssembly.DataValueField = "AssemblyID"
        drpItemAssembly.DataSource = ds
        drpItemAssembly.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpItemAssembly.Items.Insert(0, lst)

    End Sub

    Private Sub FillDefaultWarehousesList()

        Dim ds As New DataSet
        ds = obj.GetDefaultWarehousesList()

        drpDefaultWarehouse.DataTextField = "WarehouseName"
        drpDefaultWarehouse.DataValueField = "WarehouseID"
        drpDefaultWarehouse.DataSource = ds
        drpDefaultWarehouse.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpDefaultWarehouse.Items.Insert(0, lst)

    End Sub

    Private Sub FillDefaultWarehousesBinsList(ByVal WarehouseID As String)

        Dim ds As New DataSet
        ds = obj.GetDefaultWarehousesBinList(WarehouseID)

        drpDefaultWareHouseBin.DataTextField = "WarehouseBinName"
        drpDefaultWareHouseBin.DataValueField = "WarehouseBinID"
        drpDefaultWareHouseBin.DataSource = ds
        drpDefaultWareHouseBin.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Please Select"
        lst.Value = ""
        drpDefaultWareHouseBin.Items.Insert(0, lst)

    End Sub

    Private Sub HandleControls()

        If Request.QueryString("ItemID") = Nothing Then
            btnClear.Visible = True
            btnSubmit.Visible = True
            btnUpdate.Visible = False
            btnCancel.Visible = False

        Else
            btnClear.Visible = False
            btnSubmit.Visible = False
            btnUpdate.Visible = True
            btnCancel.Visible = True
            txtItemID.Enabled = False
        End If

    End Sub



    Private Sub BindCatData()

        '  Exit Sub

        Dim dt As New DataTable

        dt = fillCatDisplayData()

        grdProducts.DataSource = dt
        grdProducts.DataBind()

    End Sub


    Protected Sub grdProducts_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdProducts.RowDeleting


        Dim AssociationID As String = ""

        AssociationID = grdProducts.DataKeys(e.RowIndex).Values(0)

        deleteData(AssociationID)



        BindCatData()

    End Sub



    Public Function deleteData(ByVal AssociationID As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "Delete FROM dbo.[ItemFamilyCategoryAssociation] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and AssociationID=@AssociationID "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@AssociationID", SqlDbType.NVarChar, 36)).Value = AssociationID

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




    Public Function fillCatDisplayData() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        'ssql = "SELECT * FROM dbo.[ItemFamilyCategoryAssociation] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 Order by ItemID "

        ssql = ssql & " SELECT [ItemFamilyCategoryAssociation].[CompanyID] ,[ItemFamilyCategoryAssociation].[DivisionID] ,[ItemFamilyCategoryAssociation].[DepartmentID] "
        ssql = ssql & "       ,[ItemFamilyCategoryAssociation].[AssociationID] ,[ItemFamilyCategoryAssociation].[ItemID] ,[ItemFamilyCategoryAssociation].[ItemFamilyID] "
        ssql = ssql & "       ,[ItemFamilyCategoryAssociation].[ItemCategoryID] ,[ItemFamilyCategoryAssociation].[IsActive] ,[ItemFamilyCategoryAssociation].[CreatedOn] "
        ssql = ssql & "       ,InventoryItems.ItemName   "
        ssql = ssql & " FROM [Enterprise].[dbo].[ItemFamilyCategoryAssociation] Inner JOIN  InventoryItems  ON [ItemFamilyCategoryAssociation].CompanyID = InventoryItems.CompanyID AND  "
        ssql = ssql & " [ItemFamilyCategoryAssociation].DivisionID = InventoryItems.DivisionID  AND [ItemFamilyCategoryAssociation].DepartmentID = InventoryItems.DepartmentID AND  "
        ssql = ssql & " [ItemFamilyCategoryAssociation].ItemID = InventoryItems.ItemID "

        ssql = ssql & "  where ItemFamilyCategoryAssociation.CompanyID=@f0 and ItemFamilyCategoryAssociation.DivisionID=@f1 and ItemFamilyCategoryAssociation.DepartmentID=@f2 "

        ssql = ssql & " AND  [InventoryItems].[ItemID]  = '" & txtItemID.Text & "'"

        'If checkwebsite.Checked Then
        '    ssql = ssql & " AND ISNULL([InventoryItems].[EnabledfrontEndItem],0) =  1 "
        'End If

        'If chkpos.Checked Then
        '    ssql = ssql & " AND ISNULL([InventoryItems].[IsActive],0) =  1 "
        'End If

        'If chkfeatured.Checked Then
        '    ssql = ssql & " AND ISNULL([InventoryItems].[Featured],0) =  1 "
        'End If


        'If txtSearchValue.Text.Trim <> "" Then
        '    If drpSearchCondition.SelectedValue = "=" Then
        '        If drpSearchFor.SelectedValue = "ItemID" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemID]  = '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "ItemName" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemName]  = '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "ItemType" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemType]  = '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "Location" Then
        '            ssql = ssql & " AND  [InventoryItems].[Location]  = '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "VendorID" Then
        '            ssql = ssql & " AND  [InventoryItems].[VendorID]  = '" & txtSearchValue.Text & "'"
        '        End If
        '    End If
        '    If drpSearchCondition.SelectedValue = "<>" Then
        '        If drpSearchFor.SelectedValue = "ItemID" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemID]  <> '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "ItemName" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemName]  <> '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "ItemType" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemType]  <> '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "Location" Then
        '            ssql = ssql & " AND  [InventoryItems].[Location]  <> '" & txtSearchValue.Text & "'"
        '        End If
        '        If drpSearchFor.SelectedValue = "VendorID" Then
        '            ssql = ssql & " AND  [InventoryItems].[VendorID]  <> '" & txtSearchValue.Text & "'"
        '        End If
        '    End If
        '    If drpSearchCondition.SelectedValue = "Like" Then
        '        If drpSearchFor.SelectedValue = "ItemID" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemID]  Like '%" & txtSearchValue.Text & "%'"
        '        End If
        '        If drpSearchFor.SelectedValue = "ItemName" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemName]  Like '%" & txtSearchValue.Text & "%'"
        '        End If
        '        If drpSearchFor.SelectedValue = "ItemType" Then
        '            ssql = ssql & " AND  [InventoryItems].[ItemType]  Like '%" & txtSearchValue.Text & "%'"
        '        End If
        '        If drpSearchFor.SelectedValue = "Location" Then
        '            ssql = ssql & " AND  [InventoryItems].[Location]  Like '%" & txtSearchValue.Text & "%'"
        '        End If
        '        If drpSearchFor.SelectedValue = "VendorID" Then
        '            ssql = ssql & " AND  [InventoryItems].[VendorID]  Like '%" & txtSearchValue.Text & "%'"
        '        End If

        '    End If
        'End If


        'If drpItemFamilyID1.SelectedValue <> "" Then
        '    ssql = ssql & " AND  [ItemFamilyCategoryAssociation].[ItemFamilyID] = '" & drpItemFamilyID1.SelectedValue & "' "
        'End If

        'If drpItemCategoryID1.SelectedValue <> "" Then
        '    ssql = ssql & " AND   [ItemFamilyCategoryAssociation].[ItemCategoryID] = '" & drpItemCategoryID1.SelectedValue & "' "
        'End If


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID


        da.SelectCommand = com
        da.Fill(dt)

        'lblInfo.Text = dt.Rows.Count.ToString + " associations found"
        Return dt
        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt
        End Try

    End Function

    'DropDown_SelectedIndexChanged
    Protected Sub DropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)


        ''txtorder.Text = ""

        'DropDownList list = (DropDownList)sender;
        Dim listnone As New ListItem
        listnone.Text = "--Please Select Category--"
        listnone.Value = "---None---"
        Category1.Items.Clear()
        Try

            Dim obj As New clsViewallProductwithfc
            obj.CompanyID = CompanyID
            obj.DepartmentID = DepartmentID
            obj.DivisionID = DivisionID
            obj.ItemFamilyID = Family1.SelectedValue

            Dim dtcategory As New DataTable

            dtcategory = obj.FillDetailscategory()
            Category1.DataSource = dtcategory
            Category1.DataTextField = "CategoryName"
            Category1.DataValueField = "ItemCategoryID"
            Category1.DataBind()

            Category1.Items.Insert(0, listnone)

            Category1.SelectedIndex = 0
            ''code needed to update the dropdown values

        Catch ex As Exception

        End Try
        ' DropDown_SelectedIndexChanged2(sender, e)
    End Sub


    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click


        If txtItemID.Text.Trim <> "" Then
            SaveSortCat(True, Family1.SelectedValue, Category1.SelectedValue, txtItemID.Text)
        Else
            lblErr2.Text = "Provide Item ID to add."
        End If

        BindCatData()
        Dim listnone As New ListItem
        listnone.Text = "--Please Select Category--"
        listnone.Value = "---None---"
        Category1.Items.Clear()
        Category1.Items.Insert(0, listnone)
        Category1.SelectedIndex = 0


        txtitemsearch.Text = ""
        Family1.SelectedIndex = -1
        'Category1.SelectedIndex = -1

    End Sub


    Function SaveSortCat(ByVal Active As Boolean, ByVal fam As String, ByVal cat As String, ByVal itemid As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        'ssql = "SELECT * FROM dbo.[Companies] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        ssql = ssql & " INSERT INTO dbo.ItemFamilyCategoryAssociation (CompanyID, DivisionID, DepartmentID, ItemID, ItemFamilyID, ItemCategoryID, IsActive, CreatedOn) "
        ssql = ssql & " Values(@CompanyID,@DivisionID,@DepartmentID,@itemid,@Fam,@Cat,@Active,Getdate())"

        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@itemid", SqlDbType.NVarChar, 36)).Value = itemid
            com.Parameters.Add(New SqlParameter("@Fam", SqlDbType.NVarChar)).Value = fam
            com.Parameters.Add(New SqlParameter("@Cat", SqlDbType.NVarChar)).Value = cat
            com.Parameters.Add(New SqlParameter("@Active", SqlDbType.Bit)).Value = Active

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

        Return True
    End Function




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))
        EmployeeID = CStr(SessionKey("EmployeeID"))

        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        If Not Page.IsPostBack Then
            FillEmployee()
            FillLedgerAccounts()
            FillTaxGroups()
            ' FillVendor()
            txtVendor_Code.Attributes.Add("placeholder", "SEARCH VENDOR")
            txtVendor_Code.Attributes.Add("onKeyUp", "SendQuery(this.value,this,'v')")

            FillImageCopyrightHolders()
            FillItemFamilies()
            FillInventoryAssemblyList()
            FillDefaultWarehousesList()
            HandleControls()

            If Me.CompanyID.ToUpper = "FMW" Or Me.CompanyID.ToUpper = "QuickfloraDemo".ToUpper Then
                txtItemWholeSalePrice.Attributes.Add("onblur", "processmarkup()")
                txtMarkup.Attributes.Add("onblur", "processmarkup()")
            End If


            Dim obj As New clsViewallProductwithfc
            obj.CompanyID = CompanyID
            obj.DepartmentID = DepartmentID
            obj.DivisionID = DivisionID

            Dim dtf As New Data.DataTable
            dtf = obj.FillDetailsfamily
            Family1.DataSource = dtf
            Family1.DataTextField = "FamilyName"
            Family1.DataValueField = "ItemFamilyID"
            Family1.DataBind()

            'Dim dtcategory As New DataTable
            If Me.CompanyID = "DierbergsMarkets,Inc63017" Then
                pnldisable.Visible = True
            Else
                pnldisable.Visible = False
            End If
            'dtcategory = obj.FillDetailscategory()
            'Category1.DataSource = dtcategory
            'Category1.DataTextField = "CategoryName"
            'Category1.DataValueField = "ItemCategoryID"
            'Category1.DataBind()

            If Not Request.QueryString("ItemID") = Nothing Then
                If Me.CompanyID.ToUpper = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
                    Response.Redirect("ItemDetailsJWF.aspx?ItemID=" & Request.QueryString("ItemID"))
                End If
                FillItemDetail()
                BindCatData()
            Else
                If Me.CompanyID.ToUpper = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
                    Response.Redirect("ItemDetailsJWF.aspx")
                End If
            End If
        End If

    End Sub


    Public Function Update_InventoryWareHouse(ByVal ItemID As String) As Boolean

        If drpItemType.SelectedValue <> "Stock" Then
            Return True
        End If

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[CheckInventoryByWarehouseExist]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("LocationID", "DEFAULT")

                Try
                    connection.Open()
                    Command.ExecuteNonQuery()
                    connection.Close()

                Catch ex As Exception
                    Return True

                End Try

            End Using
        End Using

        Return True
    End Function

    Public Function Update_InventoryItems(ByVal ItemID As String, ByVal name As String, ByVal value As String) As Boolean


        Try
            Update_InventoryWareHouse(ItemID)
        Catch ex As Exception

        End Try

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim com As SqlCommand

        Dim ds As New DataSet
        ds = obj.GetInventoryItemDetail(ItemID)
        If ds.Tables(0).Rows.Count > 0 Then
        Else
            qry = "Insert into [InventoryItems] ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[ItemID]) values(@CompanyID,@DivisionID,@DepartmentID,@ItemID)"
            com = New SqlCommand(qry, connec)
            Try

                com.Parameters.AddWithValue("@ItemID", ItemID)
                com.Parameters.AddWithValue("@CompanyID", Me.CompanyID)
                com.Parameters.AddWithValue("@DivisionID", Me.DivisionID)
                com.Parameters.AddWithValue("@DepartmentID", Me.DepartmentID)

                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()
                ' Return True
            Catch ex As Exception
                Dim msg As String
                msg = ex.Message
                'HttpContext.Current.Response.Write(msg)
                'Return False
            End Try

        End If


        qry = "Update InventoryItems SET  " & name & " =@value Where ItemID = @ItemID  AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.AddWithValue("@value", value)
            com.Parameters.AddWithValue("@ItemID", ItemID)
            com.Parameters.AddWithValue("@CompanyID", Me.CompanyID)
            com.Parameters.AddWithValue("@DivisionID", Me.DivisionID)
            com.Parameters.AddWithValue("@DepartmentID", Me.DepartmentID)

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


    Public Sub savetab1()
        Update_InventoryItems(txtItemID.Text, "UnitsPerBox", txtUnitsPerBox.Text)

        Update_InventoryItems(txtItemID.Text, "ItemTypeID", drpItemType.SelectedValue)
        Update_InventoryItems(txtItemID.Text, "ItemName", txtItemName.Text)
        Update_InventoryItems(txtItemID.Text, "ItemDescription", txtItemShortDescription.Text)
        Update_InventoryItems(txtItemID.Text, "ItemLongDescription", txtItemLongDescription.Text)
        Update_InventoryItems(txtItemID.Text, "ItemCareInstructions", txtItemCareInstruction.Text)
        Update_InventoryItems(txtItemID.Text, "CareInstructions", txtItemCareInstruction.Text)
        Update_InventoryItems(txtItemID.Text, "AdditionalInformation", txtAdditionalInformation.Text)
        Update_InventoryItems(txtItemID.Text, "ItemUPCCode", txtItemUPCCode.Text)
        Update_InventoryItems(txtItemID.Text, "ItemColor", txtItemColor.Text)
        Update_InventoryItems(txtItemID.Text, "ItemUOM", txtItemUOM.Text)
        Update_InventoryItems(txtItemID.Text, "CurrencyID", txtCurrencyID.Text)
        Update_InventoryItems(txtItemID.Text, "CurrencyExchangeRate", txtCurrencyExchangeRate.Text)
        Update_InventoryItems(txtItemID.Text, "EnteredBy", drpEnteredBy.Text)
        Update_InventoryItems(txtItemID.Text, "ItemPricingCode", txtItemPricingCode.Text)
        Update_InventoryItems(txtItemID.Text, "PricingMethods", txtPricingMethod.Text)
        Update_InventoryItems(txtItemID.Text, "Taxable", chkItemTaxable.Checked)
        Update_InventoryItems(txtItemID.Text, "TaxGroupID", drpTaxbleGroupID.Text)
        Update_InventoryItems(txtItemID.Text, "GSTTAX", chkGSTTax.Checked)
        Update_InventoryItems(txtItemID.Text, "PSTTAX", chkPSTTax.Checked)
        Update_InventoryItems(txtItemID.Text, "VendorID", txtVendor_Code.Text)
        Update_InventoryItems(txtItemID.Text, "Is_TWOITEMS", chkIsTwoItems.Checked)
        Update_InventoryItems(txtItemID.Text, "Is_THREEITEMS", chkIsThreeItems.Checked)
        Update_InventoryItems(txtItemID.Text, "Bestselling", chkBestSelling.Checked)
        Update_InventoryItems(txtItemID.Text, "FlowerClassIDForSeries", drpFlowerClassForSeries.Text)
        Update_InventoryItems(txtItemID.Text, "FlowerClassUnitPrice", txtFlowerClassUnitPtrice.Text)
        Update_InventoryItems(txtItemID.Text, "SortOrder", txtSortOrder.Text)
        If chkMarkIfGiftCard.Checked = True Then
            ''row("ItemUsedAs").ToString = "GiftCardSale" 
            Update_InventoryItems(txtItemID.Text, "ItemUsedAs", "GiftCardSale")
        Else
            Update_InventoryItems(txtItemID.Text, "ItemUsedAs", "")
        End If
        Update_InventoryItems(txtItemID.Text, "IsActive", chkActiveForBackOffice.Checked)

        Update_InventoryItems(txtItemID.Text, "ActiveforRecipe", chkActiveforRecipe.Checked)
        Update_InventoryItems(txtItemID.Text, "ActiveforEvents", chkActiveforEvents.Checked)

        Update_InventoryItems(txtItemID.Text, "ActiveForStore", chkActiveForStore.Checked)
        Update_InventoryItems(txtItemID.Text, "ActiveForPOM", chkActiveForPOM.Checked)
        Update_InventoryItems(txtItemID.Text, "GroupCode", txtGroup.Text)
        Update_InventoryItems(txtItemID.Text, "Class", txtClass.Text)
        Update_InventoryItems(txtItemID.Text, "ProductVendor", txtProductVendor.Text)

        Update_InventoryItems(txtItemID.Text, "WireServiceProducts", chkWireServiceProduct.Checked)
        Update_InventoryItems(txtItemID.Text, "WireServiceID", drpImageCopyrightHolder.Text)
        Update_InventoryItems(txtItemID.Text, "SalesDescription", txtSalesDescription.Text)
        Update_InventoryItems(txtItemID.Text, "PurchaseDescription", txtPurchaseDescription.Text)
        Update_InventoryItems(txtItemID.Text, "GiftCardType", txtGiftCardType.Text)
        Update_InventoryItems(txtItemID.Text, "IsAssembly", chkIsAssembly.Text)
        Update_InventoryItems(txtItemID.Text, "ItemAssembly", drpItemAssembly.Text)
        Update_InventoryItems(txtItemID.Text, "SKU", txtSKU.Text)
        Update_InventoryItems(txtItemID.Text, "LeadTime", txtLeadTime.Text)
        Update_InventoryItems(txtItemID.Text, "ItemSize", txtItemSize.Text)
        Update_InventoryItems(txtItemID.Text, "ItemStyle", txtItemStyle.Text)
        Update_InventoryItems(txtItemID.Text, "ItemRFID", txtItemRFID.Text)
    End Sub

    Public Sub savetab2()
        'markup
        Update_InventoryItems(txtItemID.Text, "markup", txtMarkup.Text)
        Update_InventoryItems(txtItemID.Text, "Price", txtItemPrice.Text)
        Update_InventoryItems(txtItemID.Text, "DeluxePrice", txtItemDeluxePrice.Text)
        Update_InventoryItems(txtItemID.Text, "PremiumPrice", txtPremiumPrice.Text)
        Update_InventoryItems(txtItemID.Text, "HolidayEverydayPrice", txtItemHolidayPrice.Text)
        Update_InventoryItems(txtItemID.Text, "wholesalePrice", txtItemWholeSalePrice.Text)
        Update_InventoryItems(txtItemID.Text, "MTPrice", txtItemMTPrice.Text)
        Update_InventoryItems(txtItemID.Text, "CostWOFreight", txtItemCostWOFreightPrice.Text)
        Update_InventoryItems(txtItemID.Text, "LocalEverydayPrice", txtLocalEverydayPrice.Text)
        Update_InventoryItems(txtItemID.Text, "WireoutEverydayPrice", txtWireoutEverydayPrice.Text)
        Update_InventoryItems(txtItemID.Text, "WireoutHolidayPrice", txtWireoutHolidayPrice.Text)
        Update_InventoryItems(txtItemID.Text, "DropshipEverydayPrice", txtDropshipEverydayPrice.Text)
        Update_InventoryItems(txtItemID.Text, "DropshipHolidayPrice", txtDropshipHolidayPrice.Text)
        Update_InventoryItems(txtItemID.Text, "AverageCost", txtAverageCost.Text)
        Update_InventoryItems(txtItemID.Text, "AverageValue", txtAverageValue.Text)
        Update_InventoryItems(txtItemID.Text, "Commissionable", chkCommissionable.Checked)
        Update_InventoryItems(txtItemID.Text, "CommissionType", txtCommissionType.Text)
        Update_InventoryItems(txtItemID.Text, "CommissionPerc", txtCommissionPercent.Text)
    End Sub



    Public Sub savetab4()
        Update_InventoryItems(txtItemID.Text, "ItemFamilyID", drpItemFamilyID1.Text)
        Update_InventoryItems(txtItemID.Text, "ItemCategoryID", drpItemCategoryID1.Text)
        Update_InventoryItems(txtItemID.Text, "ItemFamilyID2", drpItemFamilyID2.Text)
        Update_InventoryItems(txtItemID.Text, "ItemCategoryID2", drpItemCategoryID2.Text)
        Update_InventoryItems(txtItemID.Text, "ItemFamilyID2IsActive", chkActiveItemFamilyID2.Checked)
        Update_InventoryItems(txtItemID.Text, "ItemFamilyID3", drpItemFamilyID3.Text)
        Update_InventoryItems(txtItemID.Text, "ItemCategoryID3", drpItemCategoryID3.Text)
        Update_InventoryItems(txtItemID.Text, "ItemFamilyID3IsActive", chkActiveItemFamilyID3.Checked)
    End Sub
    Public Sub savetab5()
        Update_InventoryItems(txtItemID.Text, "GLItemSalesAccount", drpGLItemSalesAccount.Text)
        Update_InventoryItems(txtItemID.Text, "GLItemCOGSAccount", drpGLItemCOGSAccount.Text)
        Update_InventoryItems(txtItemID.Text, "GLItemInventoryAccount", drpGLItemInventoryAccount.Text)

    End Sub
    Public Sub savetab6()
        Update_InventoryItems(txtItemID.Text, "SEOTitle", txtPageTitle.Text)
        Update_InventoryItems(txtItemID.Text, "MetaKeywords", txtMetaKeywords.Text)
        Update_InventoryItems(txtItemID.Text, "Metadescription", txtMetaDescription.Text)

    End Sub

    Public Sub savetab7()
        Update_InventoryItems(txtItemID.Text, "freeDelivery", chkItemFreeDelivery.Checked)
        ''txtDeliveryByItem.Text = obj.GetItemWiseDelivery(row("ItemID").ToString)
        If obj.InsertUpdateItemWiseDelivery(txtItemID.Text.Trim, txtDeliveryByItem.Text.Trim) Then
            ''Response.Redirect("ItemList.aspx")
        End If
        Update_InventoryItems(txtItemID.Text, "discountcode", txtDiscountCouponCode.Text)
    End Sub

    Public Sub savetab8()
        Update_InventoryItems(txtItemID.Text, "MSRP", txtItemMSRPStrikePrice.Text)
        Update_InventoryItems(txtItemID.Text, "SalePrice", txtSalesPrice.Text)
        Update_InventoryItems(txtItemID.Text, "SaleStartDate", txtSaleStartDate.Text)
        Update_InventoryItems(txtItemID.Text, "SaleEndDate", txtSaleEndDate.Text)

    End Sub

    Public Sub savetab9()
        Update_InventoryItems(txtItemID.Text, "LIFOCost", txtLIFOCost.Text)
        Update_InventoryItems(txtItemID.Text, "LIFOValue", txtLIFOValue.Text)
        Update_InventoryItems(txtItemID.Text, "FIFOCost", txtFIFOCost.Text)
        Update_InventoryItems(txtItemID.Text, "FIFOValue", txtFIFOValue.Text)
        Update_InventoryItems(txtItemID.Text, "ReOrderLevel", txtReOrderLevel.Text)
        Update_InventoryItems(txtItemID.Text, "ReOrderQty", txtReOrderQty.Text)
        Update_InventoryItems(txtItemID.Text, "ItemDefaultWarehouse", drpDefaultWarehouse.Text)
        Update_InventoryItems(txtItemID.Text, "ItemDefaultWarehouseBin", drpDefaultWareHouseBin.Text)
    End Sub

    Public Sub savetab10()
        Update_InventoryItems(txtItemID.Text, "ItemCommonName", txtItemCommonName.Text)
        Update_InventoryItems(txtItemID.Text, "ItemBotanicalName", txtItemBotanicalName.Text)
        Update_InventoryItems(txtItemID.Text, "ColorGroup", txtColorGroup.Text)
        Update_InventoryItems(txtItemID.Text, "FlowerType", txtFlowerType.Text)
        Update_InventoryItems(txtItemID.Text, "Variety", txtVariety.Text)
        Update_InventoryItems(txtItemID.Text, "Grade", txtGrade.Text)
        Update_InventoryItems(txtItemID.Text, "BoxSize", txtBoxSize.Text)
        Update_InventoryItems(txtItemID.Text, "ActualWeight", txtActualWeight.Text)
        Update_InventoryItems(txtItemID.Text, "DimensionalWeight", txtDimensionalWeight.Text)
        Update_InventoryItems(txtItemID.Text, "Origin", txtOrigin.Text)
        Update_InventoryItems(txtItemID.Text, "StartDateAvailable", txtStartDateAvailable.Text)
        Update_InventoryItems(txtItemID.Text, "EndDateAvailable", txtEndDateAvailable.Text)
        Update_InventoryItems(txtItemID.Text, "ShipMethodAllowed", chkShipMethodAllwed.Checked)
        Update_InventoryItems(txtItemID.Text, "PaymentMethodsAllowed", chkPaymentMethodAllowed.Checked)
        Update_InventoryItems(txtItemID.Text, "AgeIndays", txtAgeIndays.Text)
        Update_InventoryItems(txtItemID.Text, "ShipPreparation", txtShipPreparation.Text)

        Update_InventoryItems(txtItemID.Text, "BoxPrice", txtBoxPrice.Text)
        Update_InventoryItems(txtItemID.Text, "UnitPrice", txtUnitPrice.Text)
        Update_InventoryItems(txtItemID.Text, "UnitsperBunch", txtUnitsPerBunch.Text)
        Update_InventoryItems(txtItemID.Text, "StandingOrderPrice", txtStandingOrderPrice.Text)
        Update_InventoryItems(txtItemID.Text, "PrebookPrice", txtPreBookPrice.Text)
        Update_InventoryItems(txtItemID.Text, "CutOffTime", txtCutoffTime.Text)
        Update_InventoryItems(txtItemID.Text, "CutPoint", txtCutPoint.Text)
        Update_InventoryItems(txtItemID.Text, "StorageTemperature", txtStorageTemperature.Text)
        Update_InventoryItems(txtItemID.Text, "MiscInformation", txtMiscllenousinformation.Text)
        Update_InventoryItems(txtItemID.Text, "VarietyInformation", txtVarietyInformation.Text)
        Update_InventoryItems(txtItemID.Text, "Grower", txtGrower.Text)
        Update_InventoryItems(txtItemID.Text, "Flag", txtFlag.Text)
        Update_InventoryItems(txtItemID.Text, "AvailableNumberOfBoxes", txtAvailableNumberOfBoxes.Text)
        Update_InventoryItems(txtItemID.Text, "CountryOfOrigin", txtCountryOfOrigin.Text)
        Update_InventoryItems(txtItemID.Text, "Location", txtLocation.Text)
        Update_InventoryItems(txtItemID.Text, "BoxLength", txtBoxLength.Text)
        Update_InventoryItems(txtItemID.Text, "BoxWidth", txtBoxWidth.Text)
        Update_InventoryItems(txtItemID.Text, "BoxHeight", txtBoxHeight.Text)
        Update_InventoryItems(txtItemID.Text, "UOM", txtUOM.Text)
        Update_InventoryItems(txtItemID.Text, "OrigionalUnitPrice", txtOriginalUnitPrice.Text)
        Update_InventoryItems(txtItemID.Text, "ImportedAt", txtImportedAt.Text)
        Update_InventoryItems(txtItemID.Text, "ItemPackSize", txtItemPackSize.Text)
        Update_InventoryItems(txtItemID.Text, "VarietyID", txtVarietyID.Text)
        Update_InventoryItems(txtItemID.Text, "NotifyPrice", txtNotifyPrice.Text)
    End Sub


    Public ThumbnailImage As String = ""

    Private Sub FillItemDetail()

        Dim ds As New DataSet
        ds = obj.GetInventoryItemDetail(Request.QueryString("ItemID"))

        If ds.Tables(0).Rows.Count > 0 Then
            Dim row As DataRow
            row = ds.Tables(0).Rows(0)
            Try
                txtGroup.Text = row("GroupCode").ToString
            Catch ex As Exception

            End Try
            Try
                txtProductVendor.Text = row("ProductVendor").ToString
            Catch ex As Exception

            End Try
            Try
                txtClass.Text = row("Class").ToString
            Catch ex As Exception

            End Try
            Try
                chkActiveForPOM.Checked = row("ActiveForPOM").ToString
            Catch ex As Exception

            End Try
            Try
                chkActiveForStore.Checked = row("ActiveForStore").ToString
            Catch ex As Exception

            End Try

            'First Tab
            txtItemID.Text = row("ItemID").ToString
            Try
                drpItemType.SelectedValue = row("ItemTypeID").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemType:" & ex.Message
            End Try

            Try
                txtAgeIndays.Text = row("AgeIndays").ToString
            Catch ex As Exception

            End Try
            Try
                chkActiveforEvents.Checked = row("ActiveforEvents").ToString
            Catch ex As Exception

            End Try

            Try
                chkActiveforRecipe.Checked = row("ActiveforRecipe").ToString
            Catch ex As Exception

            End Try



            Try
                txtAgeIndays.Text = row("AgeIndays").ToString
            Catch ex As Exception

            End Try

            txtItemName.Text = row("ItemName").ToString
            txtItemShortDescription.Text = row("ItemDescription").ToString
            txtItemLongDescription.Text = row("ItemLongDescription").ToString
            txtItemCareInstruction.Text = row("ItemCareInstructions").ToString

            Try
                txtAdditionalInformation.Text = row("AdditionalInformation").ToString
            Catch ex As Exception

            End Try

            txtItemUPCCode.Text = row("ItemUPCCode").ToString
            txtItemColor.Text = row("ItemColor").ToString
            txtItemUOM.Text = row("ItemUOM").ToString
            txtCurrencyID.Text = row("CurrencyID").ToString
            txtCurrencyExchangeRate.Text = row("CurrencyExchangeRate").ToString

            Try
                drpEnteredBy.SelectedValue = row("EnteredBy").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpEnteredBy:" & ex.Message
            End Try


            txtItemPricingCode.Text = row("ItemPricingCode").ToString
            txtPricingMethod.Text = row("PricingMethods").ToString
            chkItemTaxable.Checked = row("Taxable").ToString

            Try
                drpTaxbleGroupID.SelectedValue = row("TaxGroupID").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpTaxbleGroupID:" & ex.Message
            End Try


            chkGSTTax.Checked = row("GSTTAX").ToString
            chkPSTTax.Checked = row("PSTTAX").ToString
            'drpVendor.SelectedValue = row("VendorID").ToString
            txtVendor_Code.Text = row("VendorID").ToString
            chkIsTwoItems.Checked = row("Is_TWOITEMS").ToString
            chkIsThreeItems.Checked = row("Is_THREEITEMS").ToString
            chkBestSelling.Checked = row("Bestselling").ToString
            ''drpFlowerClassForSeries.SelectedValue = row("FlowerClassIDForSeries").ToString

            Try
                drpFlowerClassForSeries.SelectedValue = row("FlowerClassIDForSeries").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpFlowerClassForSeries:" & ex.Message
            End Try

            Try
                txtFlowerClassUnitPtrice.Text = Format(row("FlowerClassUnitPrice"), "0.00")
            Catch ex As Exception

            End Try





            txtSortOrder.Text = row("SortOrder").ToString
            If row("ItemUsedAs").ToString = "GiftCardSale" Then
                chkMarkIfGiftCard.Checked = True
            Else
                chkMarkIfGiftCard.Checked = False
            End If
            chkActiveForBackOffice.Checked = row("IsActive").ToString
            chkWireServiceProduct.Checked = row("WireServiceProducts").ToString
            ''drpImageCopyrightHolder.SelectedValue = row("WireServiceID").ToString

            Try
                drpImageCopyrightHolder.SelectedValue = row("WireServiceID").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpImageCopyrightHolder:" & ex.Message
            End Try

            txtSalesDescription.Text = row("SalesDescription").ToString
            txtPurchaseDescription.Text = row("PurchaseDescription").ToString
            txtGiftCardType.Text = row("GiftCardType").ToString
            chkIsAssembly.Checked = row("IsAssembly").ToString
            ''drpItemAssembly.SelectedValue = row("ItemAssembly").ToString

            Try
                drpItemAssembly.SelectedValue = row("ItemAssembly").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemAssembly:" & ex.Message
            End Try

            txtSKU.Text = row("SKU").ToString
            txtLeadTime.Text = row("LeadTime").ToString
            txtItemSize.Text = row("ItemSize").ToString
            txtItemStyle.Text = row("ItemStyle").ToString
            txtItemRFID.Text = row("ItemRFID").ToString


            'markup
            'Second Tab
            Try
                txtItemPrice.Text = Format(row("Price"), "0.00")
            Catch ex As Exception

            End Try
            Try
                txtMarkup.Text = Format(row("markup"), "0.00")
            Catch ex As Exception

            End Try
            'txtItemPrice.Text = row("Price").ToString
            Try
                txtItemDeluxePrice.Text = Format(row("DeluxePrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtItemDeluxePrice.Text = row("DeluxePrice").ToString
            Try
                txtPremiumPrice.Text = Format(row("PremiumPrice"), "0.00")
            Catch ex As Exception

            End Try
            'txtPremiumPrice.Text = row("PremiumPrice").ToString

            'txtItemHolidayPrice.Text = row("HolidayEverydayPrice").ToString
            Try
                txtItemHolidayPrice.Text = Format(row("HolidayEverydayPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtItemWholeSalePrice.Text = row("wholesalePrice").ToString
            Try
                txtItemWholeSalePrice.Text = Format(row("wholesalePrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtItemMTPrice.Text = row("MTPrice").ToString
            Try
                txtItemMTPrice.Text = Format(row("MTPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtItemCostWOFreightPrice.Text = row("CostWOFreight").ToString
            Try
                txtItemCostWOFreightPrice.Text = Format(row("CostWOFreight"), "0.00")
            Catch ex As Exception

            End Try

            'txtLocalEverydayPrice.Text = row("LocalEverydayPrice").ToString
            Try
                txtLocalEverydayPrice.Text = Format(row("LocalEverydayPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtWireoutEverydayPrice.Text = row("WireoutEverydayPrice").ToString
            Try
                txtWireoutEverydayPrice.Text = Format(row("WireoutEverydayPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtWireoutHolidayPrice.Text = row("WireoutHolidayPrice").ToString
            Try
                txtWireoutHolidayPrice.Text = Format(row("WireoutHolidayPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtDropshipEverydayPrice.Text = row("DropshipEverydayPrice").ToString
            Try
                txtDropshipEverydayPrice.Text = Format(row("DropshipEverydayPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtDropshipHolidayPrice.Text = row("DropshipHolidayPrice").ToString
            Try
                txtDropshipHolidayPrice.Text = Format(row("DropshipHolidayPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtAverageCost.Text = row("AverageCost").ToString
            Try
                txtAverageCost.Text = Format(row("AverageCost"), "0.00")
            Catch ex As Exception

            End Try

            'txtAverageValue.Text = row("AverageValue").ToString
            Try
                txtAverageValue.Text = Format(row("AverageValue"), "0.00")
            Catch ex As Exception

            End Try

            chkCommissionable.Checked = row("Commissionable").ToString

            txtCommissionType.Text = row("CommissionType").ToString
            txtCommissionPercent.Text = row("CommissionPerc").ToString



            'Third Tab  
            'Small Image 100x100
            'ImgSmallImage.ImageUrl = "../../../" + ConfigurationManager.AppSettings("BackofficeImagePath") + row("PictureURL").ToString
            'Medium Image 200x200
            'Large image 400x400
            'Icon small image 
            'icon medium image
            'icon large image
            Try
                ThumbnailImage = returl(row("ThumbnailImage").ToString)
            Catch ex As Exception
                ThumbnailImage = returl("")
            End Try


            ImgSmallImage.ImageUrl = returl(row("PictureURL").ToString)
            ImgSmallImage.Width = 100
            ImgSmallImage.Height = 100
            ImgMediumImage.ImageUrl = returl(row("MediumPictureURL").ToString)
            ImgMediumImage.Width = 100
            ImgMediumImage.Height = 100
            ImgLargeImage.ImageUrl = returl(row("LargePictureURL").ToString)
            ImgLargeImage.Width = 100
            ImgLargeImage.Height = 100

            ''D:\WebApps\QuickFloraFrontEnd\itemimages\
            ',PictureURL
            ',MediumPictureURL
            ',LargePictureURL
            ',[IcomImageSmall]
            ',[IconImageMedium]
            ',[IconImageLarge]

            ImgIconSmallImage.ImageUrl = returl(row("IcomImageSmall").ToString)
            ImgIconSmallImage.Width = 100
            ImgIconSmallImage.Height = 100
            ImgIconMediumImage.ImageUrl = returl(row("IconImageMedium").ToString)
            ImgIconMediumImage.Width = 100
            ImgIconMediumImage.Height = 100
            ImgIconLargeImage.ImageUrl = returl(row("IconImageLarge").ToString)
            ImgIconLargeImage.Width = 100
            ImgIconLargeImage.Height = 100

            txtVideoURL.Text = row("VideoURL").ToString
            chkMarkIfSale.Checked = row("SALE").ToString
            chkMarkIfNew.Checked = row("NEW").ToString
            chkFeatured.Checked = row("Featured").ToString
            chkActiveForShoppingCart.Checked = row("EnabledfrontEndItem").ToString
            chkEnableItemPrice.Checked = row("EnableItemPrice").ToString
            chkEnableAddToCart.Checked = row("EnableAddtoCart").ToString
            chkMobileAppSpecial.Checked = row("bSpecialItem").ToString



            'Fourth Tab


            Try
                drpItemFamilyID1.SelectedIndex = -1
                drpItemFamilyID1.SelectedIndex = drpItemFamilyID1.Items.IndexOf(drpItemFamilyID1.Items.FindByValue(row("ItemFamilyID").ToString))
                ''drpItemFamilyID1.SelectedValue = row("ItemFamilyID").ToString

            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemFamilyID1:" & ex.Message
            End Try

            Dim sender As New Object
            Dim e As System.EventArgs

            drpItemFamilyID1_SelectedIndexChanged(sender, e)

            Try

                drpItemCategoryID1.SelectedValue = row("ItemCategoryID").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemCategoryID1:" & ex.Message
            End Try



            Try
                drpItemFamilyID2.SelectedValue = row("ItemFamilyID2").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemFamilyID2:" & ex.Message
            End Try

            drpItemFamilyID2_SelectedIndexChanged(sender, e)

            Try
                drpItemCategoryID2.SelectedValue = row("ItemCategoryID2").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemCategoryID2:" & ex.Message
            End Try

            chkActiveItemFamilyID2.Checked = row("ItemFamilyID2IsActive").ToString



            Try
                drpItemFamilyID3.SelectedValue = row("ItemFamilyID3").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemFamilyID3:" & ex.Message
            End Try


            drpItemFamilyID3_SelectedIndexChanged(sender, e)


            Try
                drpItemCategoryID3.SelectedValue = row("ItemCategoryID3").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpItemCategoryID3:" & ex.Message
            End Try

            chkActiveItemFamilyID3.Checked = row("ItemFamilyID3IsActive").ToString


            '5th Tab

            Try
                drpGLItemSalesAccount.SelectedValue = row("GLItemSalesAccount").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpGLItemSalesAccount:" & ex.Message
            End Try

            Try
                drpGLItemCOGSAccount.SelectedValue = row("GLItemCOGSAccount").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpGLItemCOGSAccount:" & ex.Message
            End Try

            Try
                drpGLItemInventoryAccount.SelectedValue = row("GLItemInventoryAccount").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpGLItemInventoryAccount:" & ex.Message
            End Try

            '6th Tab
            txtPageTitle.Text = row("SEOTitle").ToString
            txtMetaKeywords.Text = row("MetaKeywords").ToString
            txtMetaDescription.Text = row("Metadescription").ToString


            '7th Tab
            chkItemFreeDelivery.Checked = row("freeDelivery").ToString
            txtDeliveryByItem.Text = obj.GetItemWiseDelivery(row("ItemID").ToString)
            txtDiscountCouponCode.Text = row("discountcode").ToString



            '8th Tab
            Try
                txtItemMSRPStrikePrice.Text = Format(row("MSRP"), "0.00")
            Catch ex As Exception

            End Try
            'txtItemMSRPStrikePrice.Text = row("MSRP").ToString

            'txtSalesPrice.Text = row("SalePrice").ToString
            Try
                txtSalesPrice.Text = Format(row("SalePrice"), "0.00")
            Catch ex As Exception

            End Try

            txtSaleStartDate.Text = row("SaleStartDate").ToString
            txtSaleEndDate.Text = row("SaleEndDate").ToString


            '9th Tab
            'txtLIFOCost.Text = row("LIFOCost").ToString
            Try
                txtLIFOCost.Text = Format(row("LIFOCost"), "0.00")
            Catch ex As Exception

            End Try

            'txtLIFOValue.Text = row("LIFOValue").ToString
            Try
                txtLIFOValue.Text = Format(row("LIFOValue"), "0.00")
            Catch ex As Exception

            End Try

            'txtFIFOCost.Text = row("FIFOCost").ToString
            Try
                txtFIFOCost.Text = Format(row("FIFOCost"), "0.00")
            Catch ex As Exception

            End Try

            'txtFIFOValue.Text = row("FIFOValue").ToString
            Try
                txtFIFOValue.Text = Format(row("FIFOValue"), "0.00")
            Catch ex As Exception

            End Try

            txtReOrderLevel.Text = row("ReOrderLevel").ToString

            txtReOrderQty.Text = row("ReOrderQty").ToString


            Try
                drpDefaultWarehouse.SelectedValue = row("ItemDefaultWarehouse").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpDefaultWarehouse:" & ex.Message
            End Try
            Try
                drpDefaultWareHouseBin.SelectedValue = row("ItemDefaultWarehouseBin").ToString
            Catch ex As Exception
                lbldebug.Text = lbldebug.Text & "<br>" & "drpDefaultWareHouseBin:" & ex.Message
            End Try

            '10th Tab
            txtItemCommonName.Text = row("ItemCommonName").ToString
            txtItemBotanicalName.Text = row("ItemBotanicalName").ToString
            txtColorGroup.Text = row("ColorGroup").ToString
            txtFlowerType.Text = row("FlowerType").ToString
            txtVariety.Text = row("Variety").ToString
            txtGrade.Text = row("Grade").ToString
            txtBoxSize.Text = row("BoxSize").ToString
            txtActualWeight.Text = row("ActualWeight").ToString
            txtDimensionalWeight.Text = row("DimensionalWeight").ToString
            txtOrigin.Text = row("Origin").ToString
            txtStartDateAvailable.Text = row("StartDateAvailable").ToString
            txtEndDateAvailable.Text = row("EndDateAvailable").ToString
            chkShipMethodAllwed.Checked = row("ShipMethodAllowed").ToString
            chkPaymentMethodAllowed.Checked = row("PaymentMethodsAllowed").ToString



            txtShipPreparation.Text = row("ShipPreparation").ToString
            txtUnitsPerBox.Text = row("UnitsPerBox").ToString
            'txtBoxPrice.Text = row("BoxPrice").ToString
            Try
                txtBoxPrice.Text = Format(row("BoxPrice"), "0.00")
            Catch ex As Exception

            End Try

            'txtUnitPrice.Text = row("UnitPrice").ToString
            Try
                txtUnitPrice.Text = Format(row("UnitPrice"), "0.00")
            Catch ex As Exception

            End Try

            txtUnitsPerBunch.Text = row("UnitsperBunch").ToString
            'txtStandingOrderPrice.Text = row("StandingOrderPrice").ToString
            Try
                txtStandingOrderPrice.Text = Format(row("StandingOrderPrice"), "0.00")
            Catch ex As Exception

            End Try

            ''txtPreBookPrice.Text = row("PrebookPrice").ToString
            Try
                txtPreBookPrice.Text = Format(row("PrebookPrice"), "0.00")
            Catch ex As Exception

            End Try

            txtCutoffTime.Text = row("CutOffTime").ToString
            txtCutPoint.Text = row("CutPoint").ToString
            txtStorageTemperature.Text = row("StorageTemperature").ToString
            txtMiscllenousinformation.Text = row("MiscInformation").ToString
            txtVarietyInformation.Text = row("VarietyInformation").ToString
            txtGrower.Text = row("Grower").ToString
            txtFlag.Text = row("Flag").ToString




            txtAvailableNumberOfBoxes.Text = row("AvailableNumberOfBoxes").ToString
            txtCountryOfOrigin.Text = row("CountryOfOrigin").ToString
            txtLocation.Text = row("Location").ToString
            txtBoxWidth.Text = row("BoxWidth").ToString
            txtBoxLength.Text = row("BoxLength").ToString
            txtBoxHeight.Text = row("BoxHeight").ToString
            txtUOM.Text = row("UOM").ToString
            ''txtOriginalUnitPrice.Text = row("OrigionalUnitPrice").ToString
            Try
                txtOriginalUnitPrice.Text = Format(row("OrigionalUnitPrice"), "0.00")
            Catch ex As Exception

            End Try

            txtImportedAt.Text = row("ImportedAt").ToString
            txtItemPackSize.Text = row("ItemPackSize").ToString
            txtVarietyID.Text = row("VarietyID").ToString
            ''txtNotifyPrice.Text = row("NotifyPrice").ToString
            Try
                txtNotifyPrice.Text = Format(row("NotifyPrice"), "0.00")
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub InsertItemDetail()

        Dim SmallImageURL As String = UploadImage(fuSmallImage, "sm", 100)
        Dim MediumImageURL As String = UploadImage(fuSmallImage, "md", 200)
        Dim LargeImageURL As String = UploadImage(fuSmallImage, "lg", 300)

        Dim IconSmallImageURL As String = UploadImage(fuSmallImage, "sm", 100)
        Dim IconMediumImageURL As String = UploadImage(fuSmallImage, "md", 200)
        Dim IconLargeImageURL As String = UploadImage(fuSmallImage, "lg", 300)
        Try
            If SmallImageURL.ToString().Length = 0 Then
                SmallImageURL = ""
            End If
        Catch ex As Exception
            SmallImageURL = ""
        End Try

        Try
            If MediumImageURL.ToString().Length = 0 Then
                MediumImageURL = ""
            End If
        Catch ex As Exception
            MediumImageURL = ""
        End Try

        Try
            If LargeImageURL.ToString().Length = 0 Then
                LargeImageURL = ""
            End If
        Catch ex As Exception
            LargeImageURL = ""
        End Try

        Try
            If IconSmallImageURL.ToString().Length = 0 Then
                IconSmallImageURL = ""
            End If
        Catch ex As Exception
            IconSmallImageURL = ""
        End Try

        Try
            If IconMediumImageURL.ToString().Length = 0 Then
                IconMediumImageURL = ""
            End If
        Catch ex As Exception
            IconMediumImageURL = ""
        End Try

        Try
            If IconLargeImageURL.ToString().Length = 0 Then
                IconLargeImageURL = ""
            End If
        Catch ex As Exception
            IconLargeImageURL = ""
        End Try


        Dim ItemUsedAs As String = "", VarietyId As String = "", NotifyPrice As String = "", WholesalePrice As String = ""

        lbldebug.Text = lbldebug.Text & "<br>" & " Start"

        If Not obj.IsInventoryItemExist(txtItemID.Text) Then

            lbldebug.Text = lbldebug.Text & "<br>" & "level 1"

            If obj.InsertItemDetail(txtItemID.Text.Trim, _
             drpItemType.SelectedValue.Trim, txtItemName.Text.Trim, txtItemShortDescription.Text.Trim, txtItemLongDescription.Text.Trim, _
             txtItemCareInstruction.Text.Trim, txtItemUPCCode.Text.Trim, txtItemColor.Text.Trim, txtItemUOM.Text.Trim, txtCurrencyID.Text.Trim, _
             txtCurrencyExchangeRate.Text.Trim, drpEnteredBy.SelectedValue.Trim, txtItemPricingCode.Text.Trim, txtPricingMethod.Text.Trim, chkItemTaxable.Checked, _
             drpTaxbleGroupID.SelectedValue.Trim, chkGSTTax.Checked, chkPSTTax.Checked, txtVendor_Code.Text, chkIsTwoItems.Checked, chkIsThreeItems.Checked, _
             chkBestSelling.Checked, drpFlowerClassForSeries.SelectedValue.Trim, txtFlowerClassUnitPtrice.Text.Trim, txtSortOrder.Text.Trim, chkMarkIfGiftCard.Checked, _
             chkActiveForBackOffice.Checked, chkWireServiceProduct.Checked, drpImageCopyrightHolder.SelectedValue.Trim, txtSalesDescription.Text.Trim, _
             txtPurchaseDescription.Text.Trim, txtGiftCardType.Text.Trim, chkIsAssembly.Checked, drpItemAssembly.SelectedValue, txtSKU.Text.Trim, _
             txtLeadTime.Text.Trim, txtItemSize.Text.Trim, txtItemStyle.Text.Trim, txtItemRFID.Text.Trim, txtItemPrice.Text.Trim, txtItemDeluxePrice.Text.Trim, _
             txtPremiumPrice.Text.Trim, txtItemHolidayPrice.Text.Trim, txtItemMTPrice.Text.Trim, txtItemCostWOFreightPrice.Text.Trim, txtLocalEverydayPrice.Text.Trim, _
             txtWireoutEverydayPrice.Text.Trim, txtWireoutHolidayPrice.Text.Trim, txtDropshipEverydayPrice.Text.Trim, _
             txtDropshipHolidayPrice.Text.Trim, txtAverageCost.Text.Trim, txtAverageValue.Text.Trim, chkCommissionable.Checked, txtCommissionType.Text.Trim, _
             txtCommissionPercent.Text.Trim, SmallImageURL, MediumImageURL, LargeImageURL, IconSmallImageURL, IconMediumImageURL, IconLargeImageURL, txtVideoURL.Text.Trim, _
             chkMarkIfSale.Checked, chkMarkIfNew.Checked, chkFeatured.Checked, chkActiveForShoppingCart.Checked, chkEnableItemPrice.Checked, chkEnableAddToCart.Checked, _
             chkMobileAppSpecial.Checked, drpItemFamilyID1.SelectedValue.Trim, drpItemCategoryID1.SelectedValue.Trim, drpItemFamilyID2.SelectedValue, drpItemCategoryID2.SelectedValue, _
             chkActiveItemFamilyID2.Checked, drpItemFamilyID3.SelectedValue, drpItemCategoryID3.SelectedValue, chkActiveItemFamilyID3.Checked, _
             drpGLItemSalesAccount.SelectedValue, drpGLItemCOGSAccount.SelectedValue, drpGLItemInventoryAccount.SelectedValue, _
             txtPageTitle.Text.Trim, txtMetaKeywords.Text.Trim, txtMetaDescription.Text.Trim, chkItemFreeDelivery.Checked, _
             txtDiscountCouponCode.Text.Trim, txtItemMSRPStrikePrice.Text.Trim, txtSalesPrice.Text.Trim, txtSaleStartDate.Text.Trim, txtSaleEndDate.Text.Trim, _
             txtLIFOCost.Text.Trim, txtLIFOValue.Text.Trim, txtFIFOCost.Text.Trim, txtFIFOValue.Text.Trim, txtReOrderLevel.Text.Trim, txtReOrderQty.Text.Trim, _
             drpDefaultWarehouse.SelectedValue.Trim, drpDefaultWareHouseBin.SelectedValue.Trim, _
             txtItemCommonName.Text.Trim, txtItemBotanicalName.Text.Trim, txtColorGroup.Text.Trim, txtFlowerType.Text.Trim, txtVariety.Text.Trim, _
             txtGrade.Text.Trim, txtBoxSize.Text.Trim, txtActualWeight.Text.Trim, txtDimensionalWeight.Text.Trim, txtOrigin.Text.Trim, _
             txtStartDateAvailable.Text.Trim, txtEndDateAvailable.Text.Trim, chkShipMethodAllwed.Checked, chkPaymentMethodAllowed.Checked, _
             txtShipPreparation.Text.Trim, txtUnitsPerBox.Text.Trim, txtBoxPrice.Text.Trim, txtUnitPrice.Text.Trim, txtUnitsPerBunch.Text.Trim, txtStandingOrderPrice.Text.Trim, _
             txtPreBookPrice.Text.Trim, txtCutoffTime.Text.Trim, txtCutPoint.Text.Trim, txtStorageTemperature.Text.Trim, txtMiscllenousinformation.Text.Trim, _
             txtVarietyInformation.Text.Trim, txtGrower.Text.Trim, txtFlag.Text.Trim, txtAvailableNumberOfBoxes.Text.Trim, txtCountryOfOrigin.Text.Trim, _
             txtLocation.Text.Trim, txtBoxWidth.Text.Trim, txtBoxLength.Text.Trim, txtBoxHeight.Text.Trim, txtUOM.Text.Trim, txtOriginalUnitPrice.Text.Trim, _
             txtImportedFrom.Text.Trim, txtBoxSizeUOM.Text.Trim, txtImportedAt.Text.Trim, txtItemPackSize.Text.Trim, ItemUsedAs, VarietyId, txtNotifyPrice.Text.Trim, 99.99D) Then

                lbldebug.Text = lbldebug.Text & "<br>" & "level 2"

                If obj.InsertUpdateItemWiseDelivery(txtItemID.Text.Trim, txtDeliveryByItem.Text.Trim) Then

                    lbldebug.Text = lbldebug.Text & "<br>" & "level 3"

                    Response.Redirect("ItemList.aspx")
                End If

            End If
            lbldebug.Text = lbldebug.Text & "<br>" & "Error:" & obj.debug

        End If

    End Sub

    Private Sub UpdateItemDetail()

        Dim SmallImageURL As String = UploadImage(fuSmallImage, "sm", 100)
        Dim MediumImageURL As String = UploadImage(fuMediumImage, "md", 200)
        Dim LargeImageURL As String = UploadImage(fuLargeImage, "lg", 300)

        Dim IconSmallImageURL As String = UploadImage(fuIconSmallImage, "sm", 100)
        Dim IconMediumImageURL As String = UploadImage(fuIconMediumImage, "md", 200)
        Dim IconLargeImageURL As String = UploadImage(fuIconLargeImage, "lg", 300)

        Try
            If SmallImageURL.ToString().Length = 0 Then
                SmallImageURL = ""
            End If
        Catch ex As Exception
            SmallImageURL = ""
        End Try

        Try
            If MediumImageURL.ToString().Length = 0 Then
                MediumImageURL = ""
            End If
        Catch ex As Exception
            MediumImageURL = ""
        End Try

        Try
            If LargeImageURL.ToString().Length = 0 Then
                LargeImageURL = ""
            End If
        Catch ex As Exception
            LargeImageURL = ""
        End Try

        Try
            If IconSmallImageURL.ToString().Length = 0 Then
                IconSmallImageURL = ""
            End If
        Catch ex As Exception
            IconSmallImageURL = ""
        End Try

        Try
            If IconMediumImageURL.ToString().Length = 0 Then
                IconMediumImageURL = ""
            End If
        Catch ex As Exception
            IconMediumImageURL = ""
        End Try

        Try
            If IconLargeImageURL.ToString().Length = 0 Then
                IconLargeImageURL = ""
            End If
        Catch ex As Exception
            IconLargeImageURL = ""
        End Try

        Dim ItemUsedAs As String = "", VarietyId As String = "", NotifyPrice As String = "", WholesalePrice As String = ""

        If obj.UpdateItemDetail(txtItemID.Text.Trim, _
             drpItemType.SelectedValue.Trim, txtItemName.Text.Trim, txtItemShortDescription.Text.Trim, txtItemLongDescription.Text.Trim, _
             txtItemCareInstruction.Text.Trim, txtItemUPCCode.Text.Trim, txtItemColor.Text.Trim, txtItemUOM.Text.Trim, txtCurrencyID.Text.Trim, _
             txtCurrencyExchangeRate.Text.Trim, drpEnteredBy.SelectedValue.Trim, txtItemPricingCode.Text.Trim, txtPricingMethod.Text.Trim, chkItemTaxable.Checked, _
             drpTaxbleGroupID.SelectedValue.Trim, chkGSTTax.Checked, chkPSTTax.Checked, txtVendor_Code.Text, chkIsTwoItems.Checked, chkIsThreeItems.Checked, _
             chkBestSelling.Checked, drpFlowerClassForSeries.SelectedValue.Trim, txtFlowerClassUnitPtrice.Text.Trim, txtSortOrder.Text.Trim, chkMarkIfGiftCard.Checked, _
             chkActiveForBackOffice.Checked, chkWireServiceProduct.Checked, drpImageCopyrightHolder.SelectedValue.Trim, txtSalesDescription.Text.Trim, _
             txtPurchaseDescription.Text.Trim, txtGiftCardType.Text.Trim, chkIsAssembly.Checked, drpItemAssembly.SelectedValue, txtSKU.Text.Trim, _
             txtLeadTime.Text.Trim, txtItemSize.Text.Trim, txtItemStyle.Text.Trim, txtItemRFID.Text.Trim, txtItemPrice.Text.Trim, txtItemDeluxePrice.Text.Trim, _
             txtPremiumPrice.Text.Trim, txtItemHolidayPrice.Text.Trim, txtItemMTPrice.Text.Trim, txtItemCostWOFreightPrice.Text.Trim, txtLocalEverydayPrice.Text.Trim, _
             txtWireoutEverydayPrice.Text.Trim, txtWireoutHolidayPrice.Text.Trim, txtDropshipEverydayPrice.Text.Trim, _
             txtDropshipHolidayPrice.Text.Trim, txtAverageCost.Text.Trim, txtAverageValue.Text.Trim, chkCommissionable.Checked, txtCommissionType.Text.Trim, _
             txtCommissionPercent.Text.Trim, SmallImageURL, MediumImageURL, LargeImageURL, IconSmallImageURL, IconMediumImageURL, IconLargeImageURL, txtVideoURL.Text.Trim, _
             chkMarkIfSale.Checked, chkMarkIfNew.Checked, chkFeatured.Checked, chkActiveForShoppingCart.Checked, chkEnableItemPrice.Checked, chkEnableAddToCart.Checked, _
             chkMobileAppSpecial.Checked, drpItemFamilyID1.SelectedValue.Trim, drpItemCategoryID1.SelectedValue.Trim, drpItemFamilyID2.SelectedValue, drpItemCategoryID2.SelectedValue, _
             chkActiveItemFamilyID2.Checked, drpItemFamilyID3.SelectedValue, drpItemCategoryID3.SelectedValue, chkActiveItemFamilyID3.Checked, _
             drpGLItemSalesAccount.SelectedValue, drpGLItemCOGSAccount.SelectedValue, drpGLItemInventoryAccount.SelectedValue, _
             txtPageTitle.Text.Trim, txtMetaKeywords.Text.Trim, txtMetaDescription.Text.Trim, chkItemFreeDelivery.Checked, _
             txtDiscountCouponCode.Text.Trim, txtItemMSRPStrikePrice.Text.Trim, txtSalesPrice.Text.Trim, txtSaleStartDate.Text.Trim, txtSaleEndDate.Text.Trim, _
             txtLIFOCost.Text.Trim, txtLIFOValue.Text.Trim, txtFIFOCost.Text.Trim, txtFIFOValue.Text.Trim, txtReOrderLevel.Text.Trim, txtReOrderQty.Text.Trim, _
             drpDefaultWarehouse.SelectedValue.Trim, drpDefaultWareHouseBin.SelectedValue.Trim, _
             txtItemCommonName.Text.Trim, txtItemBotanicalName.Text.Trim, txtColorGroup.Text.Trim, txtFlowerType.Text.Trim, txtVariety.Text.Trim, _
             txtGrade.Text.Trim, txtBoxSize.Text.Trim, txtActualWeight.Text.Trim, txtDimensionalWeight.Text.Trim, txtOrigin.Text.Trim, _
             txtStartDateAvailable.Text.Trim, txtEndDateAvailable.Text.Trim, chkShipMethodAllwed.Checked, chkPaymentMethodAllowed.Checked, _
             txtShipPreparation.Text.Trim, txtUnitsPerBox.Text.Trim, txtBoxPrice.Text.Trim, txtUnitPrice.Text.Trim, txtUnitsPerBunch.Text.Trim, txtStandingOrderPrice.Text.Trim, _
             txtPreBookPrice.Text.Trim, txtCutoffTime.Text.Trim, txtCutPoint.Text.Trim, txtStorageTemperature.Text.Trim, txtMiscllenousinformation.Text.Trim, _
             txtVarietyInformation.Text.Trim, txtGrower.Text.Trim, txtFlag.Text.Trim, txtAvailableNumberOfBoxes.Text.Trim, txtCountryOfOrigin.Text.Trim, _
             txtLocation.Text.Trim, txtBoxWidth.Text.Trim, txtBoxLength.Text.Trim, txtBoxHeight.Text.Trim, txtUOM.Text.Trim, txtOriginalUnitPrice.Text.Trim, _
             txtImportedFrom.Text.Trim, txtBoxSizeUOM.Text.Trim, txtImportedAt.Text.Trim, txtItemPackSize.Text.Trim, ItemUsedAs, VarietyId, txtNotifyPrice.Text.Trim, 99.99D) Then

            If obj.InsertUpdateItemWiseDelivery(txtItemID.Text.Trim, txtDeliveryByItem.Text.Trim) Then

                Response.Redirect("ItemList.aspx")

            End If

        End If

    End Sub

    Protected Sub drpItemFamilyID1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpItemFamilyID1.SelectedIndexChanged

        Dim ds As New DataSet
        ds = obj.GetItemCategories(drpItemFamilyID1.SelectedValue)

        drpItemCategoryID1.DataSource = ds
        drpItemCategoryID1.DataTextField = "CategoryName"
        drpItemCategoryID1.DataValueField = "ItemCategoryID"
        drpItemCategoryID1.DataBind()

        drpItemCategoryID1.Items.Insert(0, "")

    End Sub

    Protected Sub drpItemFamilyID2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpItemFamilyID2.SelectedIndexChanged

        Dim ds As New DataSet
        ds = obj.GetItemCategories(drpItemFamilyID2.SelectedValue)

        drpItemCategoryID2.DataSource = ds
        drpItemCategoryID2.DataTextField = "CategoryName"
        drpItemCategoryID2.DataValueField = "ItemCategoryID"
        drpItemCategoryID2.DataBind()

        drpItemCategoryID2.Items.Insert(0, "")

    End Sub

    Protected Sub drpItemFamilyID3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpItemFamilyID3.SelectedIndexChanged

        Dim ds As New DataSet
        ds = obj.GetItemCategories(drpItemFamilyID3.SelectedValue)

        drpItemCategoryID3.DataSource = ds
        drpItemCategoryID3.DataTextField = "CategoryName"
        drpItemCategoryID3.DataValueField = "ItemCategoryID"
        drpItemCategoryID3.DataBind()

        drpItemCategoryID3.Items.Insert(0, "")

    End Sub

    Protected Sub drpDefaultWarehouse_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles drpDefaultWarehouse.SelectedIndexChanged

        FillDefaultWarehousesBinsList(drpDefaultWarehouse.SelectedValue)

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click

        'InsertItemDetail()
        saveall()
    End Sub

    Public Function returl(ByVal ob As String) As String

        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = "D:\WebApps\QuickFloraFrontEnd\itemimages\"
        ''


        If (ImgName.Trim() = "") Then

            Return "https://secure.quickflora.com/itemimages/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "https://secure.quickflora.com/itemimages/" & ImgName.Trim()

            Else
                Return "https://secure.quickflora.com/itemimages/no_image.gif"
            End If

        End If


    End Function

    Private Function UploadImage(ByVal fuImage As FileUpload, ByVal SizeType As String, ByVal Size As Integer) As String

        Dim BackofficeImagePath As String = ConfigurationManager.AppSettings("BackofficeImagePath")
        Dim POSImagePath As String = ConfigurationManager.AppSettings("POSImagePath")
        Dim WebSiteImagePath As String = ConfigurationManager.AppSettings("WebSiteImagePath")

        Dim imgPhoto As Drawing.Bitmap
        Dim imgPhotosave As Drawing.Image
        Dim keygen As New clsGetUniqueKey
        Dim sm_w As Integer
        Dim sm_h As Integer
        Dim sm_fh As Double
        Dim rt As Double
        Dim obj As New Tutorial.ImageResize
        Dim lg As String = ""
        Dim mg As String = ""
        Dim sg As String = ""
        Dim hrg As String = ""

        If fuImage.HasFile Then

            '1
            fuImage.SaveAs(BackofficeImagePath & fuImage.FileName)
            imgPhoto = New Drawing.Bitmap(BackofficeImagePath & fuImage.FileName)
            imgPhoto.SetResolution(72, 72)

            rt = imgPhoto.Width / imgPhoto.Height
            sm_fh = Size / rt
            imgPhotosave = obj.FixedSize(imgPhoto, Size, sm_fh)
            sg = keygen.GetUniqueKey & "_" + SizeType + ".jpg"
            imgPhotosave.Save(BackofficeImagePath + sg, Drawing.Imaging.ImageFormat.Jpeg)
            imgPhoto.Dispose()

            '2
            fuImage.SaveAs(POSImagePath & fuImage.FileName)
            imgPhoto = New Drawing.Bitmap(POSImagePath & fuImage.FileName)
            imgPhoto.SetResolution(72, 72)

            rt = imgPhoto.Width / imgPhoto.Height
            sm_fh = Size / rt
            imgPhotosave = obj.FixedSize(imgPhoto, Size, sm_fh)
            sg = keygen.GetUniqueKey & "_" + SizeType + ".jpg"
            imgPhotosave.Save(POSImagePath + sg, Drawing.Imaging.ImageFormat.Jpeg)
            imgPhoto.Dispose()

            '3
            fuImage.SaveAs(WebSiteImagePath & fuImage.FileName)
            imgPhoto = New Drawing.Bitmap(WebSiteImagePath & fuImage.FileName)
            imgPhoto.SetResolution(72, 72)


            rt = imgPhoto.Width / imgPhoto.Height
            sm_fh = Size / rt
            imgPhotosave = obj.FixedSize(imgPhoto, Size, sm_fh)
            sg = keygen.GetUniqueKey & "_" + SizeType + ".jpg"
            imgPhotosave.Save(WebSiteImagePath + sg, Drawing.Imaging.ImageFormat.Jpeg)

            imgPhotosave.Dispose()
            imgPhoto.Dispose()

            'UpdateSmallImage(txtItemID.Text.Trim, sg)
            Return sg
        Else

            Return Nothing

        End If

    End Function


    'Public Function UpdateSmallImage(ItemID As String, PictureURL As String) As Boolean

    '    Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
    '    Dim qry As String
    '    qry = "UPDATE InventoryItems  set  PictureURL=@PictureURL   where CompanyID=@CompanyID and DivisionID=@DivisionID and DepartmentID=@DepartmentID and ItemID=@ItemID"
    '    Dim com As SqlCommand
    '    com = New SqlCommand(qry, connec)
    '    Try

    '        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
    '        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
    '        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
    '        com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID

    '        com.Parameters.Add(New SqlParameter("@PictureURL", SqlDbType.NVarChar, 80)).Value = PictureURL

    '        com.Connection.Open()
    '        com.ExecuteNonQuery()
    '        com.Connection.Close()

    '        Return True

    '    Catch ex As Exception
    '        Dim msg As String
    '        msg = ex.Message
    '        HttpContext.Current.Response.Write(msg)
    '        Return False
    '    End Try

    'End Function

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click

        ' UpdateItemDetail()
        saveall()
    End Sub

    Public Sub saveall()
        savetab1()
        savetab2()
        savetab3()
        savetab4()
        savetab5()
        savetab6()
        savetab7()
        savetab8()
        savetab9()
        savetab10()
        savetabMobileApp()
        Response.Redirect("ItemList.aspx")
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click

        Response.Redirect("ItemList.aspx")

    End Sub

    Private Sub btnTab1Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab1Save.Click
        savetab1()
    End Sub

    Private Sub btnTab1SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab1SaveClose.Click
        savetab1()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnsavetab2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsavetab2.Click
        savetab2()
    End Sub

    Private Sub btnsaveandclosetab2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsaveandclosetab2.Click
        savetab2()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab3Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab3Save.Click
        savetab3()
    End Sub

    Private Sub btnTab3SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab3SaveClose.Click
        savetab3()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab4Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab4Save.Click
        savetab4()
    End Sub

    Private Sub btnTab4SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab4SaveClose.Click
        savetab4()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab5Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab5Save.Click
        savetab5()
    End Sub

    Private Sub btnTab5SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab5SaveClose.Click
        savetab5()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab6Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab6Save.Click
        savetab6()
    End Sub

    Private Sub btnTab6SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab6SaveClose.Click
        savetab6()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab7Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab7Save.Click
        savetab7()
    End Sub

    Private Sub btnTab7SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab7SaveClose.Click
        savetab7()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab8Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab8Save.Click
        savetab8()

    End Sub

    Private Sub btnTab8SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab8SaveClose.Click
        savetab8()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab9Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab9Save.Click
        savetab9()
    End Sub

    Private Sub btnTab9SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab9SaveClose.Click
        savetab9()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnTab10Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab10Save.Click
        savetab10()
    End Sub

    Private Sub btnTab10SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab10SaveClose.Click
        savetab10()
        Response.Redirect("ItemList.aspx")
    End Sub

    Private Sub btnMobileAppsave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMobileAppsave.Click
        savetabMobileApp()
    End Sub

    Private Sub btnMobileAppsaveclose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMobileAppsaveclose.Click
        savetabMobileApp()
        Response.Redirect("ItemList.aspx")
    End Sub



    Public Sub savetabMobileApp()

        ''D:\WebApps\QuickFloraFrontEnd\itemimages\
        ',PictureURL
        ',MediumPictureURL
        ',LargePictureURL
        ',[IcomImageSmall]
        ',[IconImageMedium]
        ',[IconImageLarge]
        ''txtVideoURL.Text = fuIconSmallImage.HasFile

        If fuIconSmallImage.HasFile Then
            If System.IO.File.Exists("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuIconSmallImage.FileName) Then
                Dim _fuIconSmallImage As String = ""
                _fuIconSmallImage = Me.CompanyID & "_" & Date.Now.Millisecond.ToString() & "_" & fuIconSmallImage.FileName
                fuIconSmallImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & _fuIconSmallImage)
                Update_InventoryItems(txtItemID.Text, "IcomImageSmall", _fuIconSmallImage)
                ImgIconSmallImage.ImageUrl = returl(_fuIconSmallImage)
                ImgIconSmallImage.Width = 100
                ImgIconSmallImage.Height = 100
            Else
                fuIconSmallImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuIconSmallImage.FileName)
                Update_InventoryItems(txtItemID.Text, "IcomImageSmall", Me.CompanyID & "_" & fuIconSmallImage.FileName)
                ImgIconSmallImage.ImageUrl = returl(Me.CompanyID & "_" & fuIconSmallImage.FileName)
                ImgIconSmallImage.Width = 100
                ImgIconSmallImage.Height = 100
            End If
        End If

        If fuIconMediumImage.HasFile Then
            If System.IO.File.Exists("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuIconMediumImage.FileName) Then
                Dim _fuIconMediumImage As String = ""
                _fuIconMediumImage = Me.CompanyID & "_" & Date.Now.Millisecond.ToString() & "_" & fuIconMediumImage.FileName
                fuIconMediumImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & _fuIconMediumImage)
                Update_InventoryItems(txtItemID.Text, "IconImageMedium", _fuIconMediumImage)
                ImgIconMediumImage.ImageUrl = returl(_fuIconMediumImage)
                ImgIconMediumImage.Width = 100
                ImgIconMediumImage.Height = 100
            Else
                fuIconMediumImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuIconMediumImage.FileName)
                Update_InventoryItems(txtItemID.Text, "IconImageMedium", Me.CompanyID & "_" & fuIconMediumImage.FileName)
                ImgIconMediumImage.ImageUrl = returl(Me.CompanyID & "_" & fuIconMediumImage.FileName)
                ImgIconMediumImage.Width = 100
                ImgIconMediumImage.Height = 100
            End If
        End If

        If fuIconLargeImage.HasFile Then
            If System.IO.File.Exists("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuIconLargeImage.FileName) Then
                Dim _fuIconLargeImage As String = ""
                _fuIconLargeImage = Me.CompanyID & "_" & Date.Now.Millisecond.ToString() & "_" & fuIconLargeImage.FileName
                fuIconLargeImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & _fuIconLargeImage)
                Update_InventoryItems(txtItemID.Text, "IconImageLarge", _fuIconLargeImage)
                ImgIconLargeImage.ImageUrl = returl(_fuIconLargeImage)
                ImgIconLargeImage.Width = 100
                ImgIconLargeImage.Height = 100
            Else
                fuIconLargeImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuIconLargeImage.FileName)
                Update_InventoryItems(txtItemID.Text, "IconImageLarge", Me.CompanyID & "_" & fuIconLargeImage.FileName)
                ImgIconLargeImage.ImageUrl = returl(Me.CompanyID & "_" & fuIconLargeImage.FileName)
                ImgIconLargeImage.Width = 100
                ImgIconLargeImage.Height = 100
            End If

        End If



        Update_InventoryItems(txtItemID.Text, "VideoURL", txtVideoURL.Text)
        Update_InventoryItems(txtItemID.Text, "bSpecialItem", chkMobileAppSpecial.Checked)

    End Sub

    Public Sub savetab3()
        ''With image upload
        Update_InventoryItems(txtItemID.Text, "SALE", chkMarkIfSale.Checked)
        Update_InventoryItems(txtItemID.Text, "NEW", chkMarkIfNew.Checked)
        Update_InventoryItems(txtItemID.Text, "Featured", chkFeatured.Checked)
        Update_InventoryItems(txtItemID.Text, "EnabledfrontEndItem", chkActiveForShoppingCart.Checked)
        Update_InventoryItems(txtItemID.Text, "EnableItemPrice", chkEnableItemPrice.Checked)
        Update_InventoryItems(txtItemID.Text, "EnableAddtoCart", chkEnableAddToCart.Checked)
        'lbldebug.Text = fuSmallImage.FileName
        If fuSmallImage.HasFile Then
            If System.IO.File.Exists("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuSmallImage.FileName) Then
                Dim _fuSmallImage As String = ""
                _fuSmallImage = Me.CompanyID & "_" & Date.Now.Millisecond.ToString() & "_" & fuSmallImage.FileName
                fuSmallImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & _fuSmallImage)
                Update_InventoryItems(txtItemID.Text, "PictureURL", _fuSmallImage)
                ImgSmallImage.ImageUrl = returl(_fuSmallImage)
                ImgSmallImage.Width = 100
                ImgSmallImage.Height = 100
                lbldebug.Text = Me.CompanyID & "_" & Date.Now.Millisecond.ToString() & "_" & fuSmallImage.FileName
            Else

                fuSmallImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuSmallImage.FileName)
                Update_InventoryItems(txtItemID.Text, "PictureURL", Me.CompanyID & "_" & fuSmallImage.FileName)
                ImgSmallImage.ImageUrl = returl(Me.CompanyID & "_" & fuSmallImage.FileName)
                ImgSmallImage.Width = 100
                ImgSmallImage.Height = 100
                lbldebug.Text = Me.CompanyID & "_" & fuSmallImage.FileName
            End If
        End If

        If fuMediumImage.HasFile Then
            If System.IO.File.Exists("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuMediumImage.FileName) Then
                Dim _fuMediumImage As String = ""
                _fuMediumImage = Me.CompanyID & "_" & Date.Now.Millisecond.ToString() & "_" & fuMediumImage.FileName
                fuMediumImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & _fuMediumImage)
                Update_InventoryItems(txtItemID.Text, "MediumPictureURL", _fuMediumImage)
                ImgMediumImage.ImageUrl = returl(_fuMediumImage)
                ImgMediumImage.Width = 100
                ImgMediumImage.Height = 100
            Else
                fuMediumImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuMediumImage.FileName)
                Update_InventoryItems(txtItemID.Text, "MediumPictureURL", Me.CompanyID & "_" & fuMediumImage.FileName)
                ImgMediumImage.ImageUrl = returl(Me.CompanyID & "_" & fuMediumImage.FileName)
                ImgMediumImage.Width = 100
                ImgMediumImage.Height = 100
            End If

        End If

        If fuLargeImage.HasFile Then
            If System.IO.File.Exists("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuLargeImage.FileName) Then
                Dim _fuLargeImage As String = ""
                _fuLargeImage = Me.CompanyID & "_" & Date.Now.Millisecond.ToString() & "_" & fuLargeImage.FileName
                fuLargeImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & _fuLargeImage)
                Update_InventoryItems(txtItemID.Text, "LargePictureURL", _fuLargeImage)
                ImgLargeImage.ImageUrl = returl(_fuLargeImage)
                ImgLargeImage.Width = 100
                ImgLargeImage.Height = 100
            Else
                fuLargeImage.SaveAs("D:\WebApps\QuickFloraFrontEnd\itemimages\" & Me.CompanyID & "_" & fuLargeImage.FileName)
                Update_InventoryItems(txtItemID.Text, "LargePictureURL", Me.CompanyID & "_" & fuLargeImage.FileName)
                ImgLargeImage.ImageUrl = returl(Me.CompanyID & "_" & fuLargeImage.FileName)
                ImgLargeImage.Width = 100
                ImgLargeImage.Height = 100
            End If
        End If


    End Sub
End Class
