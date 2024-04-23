Imports System.Data
Imports System.Data.SqlClient

Partial Class ItemList
    Inherits System.Web.UI.Page

    Private obj As New clsItems

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""

    Public Function Fillfamily() As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 Order by InventoryFamilies.FamilyName  "
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


    Public Function GetEmployeesModuleAccessByModule(ByVal AccessByModule As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal EmployeeID As String) As DataTable

        Dim dt As New DataTable

        Using connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using command As New SqlCommand("[enterprise].[GetEmployeesModuleAccessByModule]", connection)
                command.CommandType = CommandType.StoredProcedure

                command.Parameters.AddWithValue("CompanyID", CompanyID)
                command.Parameters.AddWithValue("DivisionID", DivisionID)
                command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                command.Parameters.AddWithValue("EmployeeID", EmployeeID)
                command.Parameters.AddWithValue("ModuleID", AccessByModule)

                Try
                    Dim da As New SqlDataAdapter(command)
                    da.Fill(dt)
                Catch ex As Exception

                End Try

            End Using
        End Using

        Return dt

    End Function


    Public Function returl(ByVal ob As Object) As String
        '''NasCheck(ob)


        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ""
        Try
            ImgName = ob.ToString()
        Catch ex As Exception

        End Try
        DocumentDir = "D:\WebApps\QuickFloraFrontEnd\itemimages\" ' ConfigurationManager.AppSettings("InvPath")

        If (ImgName.Trim() = "") Then

            Return "https://secure.quickflora.com/itemimages/noimagenew.png"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then
                Return "https://secure.quickflora.com/itemimages/" & ImgName.Trim()
                ''Return "../../images/products/" & ImgName.Trim()

            Else
                Return "https://secure.quickflora.com/itemimages/noimagenew.png"
            End If




        End If


    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))
        EmployeeID = CStr(SessionKey("EmployeeID"))


        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
            Dim objEmployee As New clsEmployee
            Dim dtEmployee As New DataTable
            dtEmployee = GetEmployeesModuleAccessByModule("IA", CompanyID, DivisionID, DepartmentID, EmployeeID)
            If dtEmployee.Rows.Count > 0 Then
                Dim CheckAccess As Boolean = False
                Try
                    CheckAccess = dtEmployee.Rows(0)("CheckAccess")
                Catch ex As Exception

                End Try

                If CheckAccess = False Then
                    Response.Redirect("SecurityAcessPermission.aspx?MOD=IA")
                End If
            Else
                Response.Redirect("SecurityAcessPermission.aspx?MOD=IA")
            End If

        End If

        If CompanyID.ToUpper = "JWF" Or CompanyID.ToUpper = "NEWWF" Then

            grpLabel.Visible = True
                drpItemGroup.Visible = True

            End If

            obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        fillHomePageManagement()
        If Not Page.IsPostBack Then

            Dim dtfamily As New DataTable
            dtfamily = Fillfamily()

            drpItemFamilyID1.DataSource = dtfamily
            drpItemFamilyID1.DataTextField = "FamilyName"
            drpItemFamilyID1.DataValueField = "ItemFamilyID"
            drpItemFamilyID1.DataBind()

            Dim ds As New DataSet
            ds = obj.GetItemCategories(drpItemFamilyID1.SelectedValue)

            drpItemCategoryID1.DataSource = ds
            drpItemCategoryID1.DataTextField = "CategoryName"
            drpItemCategoryID1.DataValueField = "ItemCategoryID"
            drpItemCategoryID1.DataBind()
            Dim lst As New ListItem
            lst.Text = "--Select--"
            lst.Value = ""
            drpItemCategoryID1.Items.Insert(0, lst)

            Dim dtItemGroup As New DataTable
            dtItemGroup = FillItemGroup(drpItemFamilyID1.SelectedValue, drpItemCategoryID1.SelectedValue)
            'drpItemGroup
            ' ,[ItemGroupID]
            ' ,[GroupName]
            drpItemGroup.DataSource = dtItemGroup
            drpItemGroup.DataTextField = "GroupName"
            drpItemGroup.DataValueField = "ItemGroupID"
            drpItemGroup.DataBind()
            Dim lst1 As New ListItem
            lst1.Text = "--Select--"
            lst1.Value = ""
            drpItemGroup.Items.Insert(0, lst1)
            GetInventoryItemsList()
        End If

    End Sub


    Public Function InventoryItemsName(ByVal ItemID As String) As String
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "SELECT   ItemName  FROM [InventoryItems] Where InventoryItems.CompanyID= @CompanyID  and InventoryItems.DivisionID=@DivisionID and InventoryItems.DepartmentID= @DepartmentID and InventoryItems.ItemID=@ItemID"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                If Me.CompanyID = "DierbergsMarkets,Inc63017" Then
                    Return "<a href='ReceipeXML.aspx?ItemID=" & ItemID & "&name=" & dt.Rows(0)(0) & "' target='_blank' >" & dt.Rows(0)(0) & "</a>"
                End If
                Dim obj As New clsInventoryAssembly
                Dim dtnew As New DataTable
                dtnew = obj.GetInventoryAssemblyListByItem(Me.CompanyID, Me.DivisionID, Me.DepartmentID, ItemID)
                Dim AssemblyID As String = ""

                If dtnew.Rows.Count > 0 Then
                    Try
                        AssemblyID = dtnew.Rows(0)("AssemblyID")
                    Catch ex As Exception

                    End Try
                End If

                If AssemblyID <> "" Then
                    Return "<a href='NewInventoryAssemblyPreview.aspx?AssemblyID=" & AssemblyID & "&PriceType=Best' target='_blank' >" & dt.Rows(0)(0) & "</a>"
                End If

                Try
                    Return dt.Rows(0)(0)
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
        Return ""
    End Function


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

    Private Function GetInventoryItemsListDT() As DataTable


        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim neworderentryform As Boolean = False
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT  ISNULL(InventoryItems.wholesalePrice ,0) AS 'WP'  ,ISNULL(archived,0) as 'ar',* FROM   InventoryItems   where  CompanyID='" & CompanyID & "' AND  [DivisionID]='" & DivisionID & "' AND  [DepartmentID]='" & DepartmentID & "' "
        If chkArchive.Checked Then
            ssql = ssql & " AND ISNULL(archived,0) = 1 "
        Else
            ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If
        If chkactive.Checked Then
            ssql = ssql & " AND ISNULL(IsActive,0) = 1 "
        Else
            'ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If
        If chkwebsite.Checked Then
            ssql = ssql & " AND ISNULL(EnabledfrontEndItem,0) = 1 "
        Else
            'ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If
        If chkEvents.Checked Then
            ssql = ssql & " AND ISNULL(ActiveForEvents,0) = 1 "
        Else
            'ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If
        If chkRecipe.Checked Then
            ssql = ssql & " AND ISNULL(ActiveForRecipe,0) = 1 "
        Else
            'ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If

        If chktaxable.Checked Then
            ssql = ssql & " AND ISNULL(Taxable,0) = 0 "
        Else
            'ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If

        If chkActiveForStore.Checked Then
            ssql = ssql & " AND ISNULL(ActiveForStore,0) = 1 "
        Else
            'ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If

        If chkActiveForPOM.Checked Then
            ssql = ssql & " AND ISNULL(ActiveForPOM,0) = 1 "
        Else
            'ssql = ssql & " AND ISNULL(archived,0) = 0 "
        End If

        If drpItemFamilyID1.SelectedValue <> "" Then
            ssql = ssql & " AND ISNULL(ItemFamilyID,0) = '" & drpItemFamilyID1.SelectedValue & "'"
        End If

        If drpItemCategoryID1.SelectedValue <> "" Then
            ssql = ssql & " AND ISNULL(ItemCategoryID,0) = '" & drpItemCategoryID1.SelectedValue & "'"
        End If


        If CompanyID.ToUpper = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
            If drpItemGroup.SelectedValue <> "" Then
                ssql = ssql & " AND ISNULL(GroupCode,0) = '" & drpItemGroup.SelectedValue & "'"
            End If
        End If

        If txtSearchValue.Text.Trim <> "" And drpSearchFor.SelectedValue <> "" Then
            If drpSearchCondition.SelectedValue = "Like" Then
                ssql = ssql & " AND  " & drpSearchFor.SelectedValue & " Like '%" & txtSearchValue.Text.Trim & "%'"
            End If
            If drpSearchCondition.SelectedValue = "=" Then
                ssql = ssql & " AND  " & drpSearchFor.SelectedValue & " = '" & txtSearchValue.Text.Trim & "'"
            End If
        End If


        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            da.SelectCommand = com
            da.Fill(dt)
            If dt.Rows.Count <> 0 Then

            End If
            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
    End Function

    Private Sub GetInventoryItemsList()

        Dim ds As New DataTable
        ds = GetInventoryItemsListDT()

        If ds.Rows.Count > 0 Then
            gvItemsList.DataSource = ds
            gvItemsList.DataBind()
            lblInfo.Text = ds.Rows.Count.ToString + " records found"
            gvItemsList.Visible = True
        Else
            lblInfo.Text = "0 records found"
            gvItemsList.Visible = False
        End If

    End Sub

    Public Function FillItemGroup(ByVal ItemFamilyID As String, ByVal ItemCategoryID As String) As DataTable
        Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from [InventoryGroups] where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemCategoryID=@ItemCategoryID and ItemFamilyID=@ItemFamilyID Order by GroupName  "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@ItemCategoryID", SqlDbType.NVarChar, 36)).Value = ItemCategoryID
            com.Parameters.Add(New SqlParameter("@ItemFamilyID", SqlDbType.NVarChar, 36)).Value = ItemFamilyID

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


    Protected Sub gvItemsList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvItemsList.PageIndexChanging

        gvItemsList.PageIndex = e.NewPageIndex
        GetInventoryItemsList()


    End Sub

    Protected Sub gvItemsList_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gvItemsList.Sorting

        Dim ds As New DataTable
        ds = GetInventoryItemsListDT()

        'If ds.Rows.Count > 0 Then
        '    gvItemsList.DataSource = ds
        '    gvItemsList.DataBind()
        '    lblInfo.Text = ds.Rows.Count.ToString + " records found"
        'Else
        '    lblInfo.Text = "0 records found"
        'End If

        Dim dv As DataView = ds.DefaultView

        If gvSortDirection.Value = "" Or gvSortDirection.Value = "DESC" Then
            gvSortDirection.Value = "ASC"
        Else
            gvSortDirection.Value = "DESC"
        End If

        dv.Sort = e.SortExpression & " " & gvSortDirection.Value

        If ds.Rows.Count > 0 Then
            gvItemsList.DataSource = dv
            gvItemsList.DataBind()
            lblInfo.Text = ds.Rows.Count.ToString + " records found"
        Else
            lblInfo.Text = "0 records found"
        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GetInventoryItemsList()

    End Sub

    Private Sub drpItemCategoryID1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpItemCategoryID1.SelectedIndexChanged
        Dim dtItemGroup As New DataTable
        dtItemGroup = FillItemGroup(drpItemFamilyID1.SelectedValue, drpItemCategoryID1.SelectedValue)
        'drpItemGroup
        ' ,[ItemGroupID]
        ' ,[GroupName]
        drpItemGroup.DataSource = dtItemGroup
        drpItemGroup.DataTextField = "GroupName"
        drpItemGroup.DataValueField = "ItemGroupID"
        drpItemGroup.DataBind()
        Dim lst1 As New ListItem
        lst1.Text = "--Select--"
        lst1.Value = ""
        drpItemGroup.Items.Insert(0, lst1)

        GetInventoryItemsList()
    End Sub

    Private Sub drpItemFamilyID1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpItemFamilyID1.SelectedIndexChanged
        Dim ds As New DataSet
        ds = obj.GetItemCategories(drpItemFamilyID1.SelectedValue)

        drpItemCategoryID1.DataSource = ds
        drpItemCategoryID1.DataTextField = "CategoryName"
        drpItemCategoryID1.DataValueField = "ItemCategoryID"
        drpItemCategoryID1.DataBind()
        Dim lst As New ListItem
        lst.Text = "--Select--"
        lst.Value = ""
        drpItemCategoryID1.Items.Insert(0, lst)

        GetInventoryItemsList()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, chkwebsite.CheckedChanged, chktaxable.CheckedChanged, chkRecipe.CheckedChanged, chkEvents.CheckedChanged, chkArchive.CheckedChanged, chkActiveForStore.CheckedChanged, chkActiveForPOM.CheckedChanged, chkactive.CheckedChanged
        GetInventoryItemsList()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Response.Redirect("itemlist.aspx")
    End Sub

    Private Sub gvItemsList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvItemsList.RowDataBound

    End Sub
    Public Function archived(ByVal ar As Boolean) As String
        If ar Then
            Return "none"
        Else

        End If
        Return "block"
    End Function

    Private Sub drpItemGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpItemGroup.SelectedIndexChanged
        GetInventoryItemsList()
    End Sub
End Class
