
Imports System.Data
Imports System.Data.SqlClient

Partial Class InventoryCategoriesDetails
    Inherits System.Web.UI.Page

    Public CompanyID As String, DivisionID As String, DepartmentID As String

    Dim EmployeeID As String = ""

    Private Sub InventoryFamiliesDetails_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))
        EmployeeID = CStr(SessionKey("EmployeeID"))

        If Not Page.IsPostBack Then

            Dim dtf As New Data.DataTable
            dtf = FillDetailsfamily()
            txtItemFamilyID.DataSource = dtf
            txtItemFamilyID.DataTextField = "FamilyName"
            txtItemFamilyID.DataValueField = "ItemFamilyID"
            txtItemFamilyID.DataBind()

            If Not Request.QueryString("ItemFamilyID") = Nothing And Not Request.QueryString("ItemCategoryID") = Nothing Then
                fillItemFamilyID()
            End If

        End If


    End Sub
    Dim ItemFamilyID As String = ""
    Dim ItemCategoryID As String = ""

    Private Sub fillItemFamilyID()

        ItemFamilyID = Request.QueryString("ItemFamilyID")
        ItemCategoryID = Request.QueryString("ItemCategoryID")

        Dim dt As New DataTable
        dt = FillDetailscategorybyid()


        If dt.Rows.Count <> 0 Then

            Try
                txtItemFamilyID.Text = dt.Rows(0)("ItemFamilyID")
                ' txtItemFamilyID.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtItemCategoryID.Text = dt.Rows(0)("ItemCategoryID")
                txtItemCategoryID.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtCategoryName.Text = dt.Rows(0)("CategoryName")
            Catch ex As Exception

            End Try
            Try
                txtCategoryDescription.Text = dt.Rows(0)("CategoryDescription")
            Catch ex As Exception

            End Try
            Try
                txtCategoryLongDescription.Text = dt.Rows(0)("CategoryLongDescription")
            Catch ex As Exception

            End Try

            Try
                txtCategoryPictureURL.Text = dt.Rows(0)("CategoryPictureURL")
            Catch ex As Exception

            End Try

            Try
                txtSortOrder.Text = dt.Rows(0)("SortOrder")
            Catch ex As Exception

            End Try
            Try
                chkEnableItemCategoryID.Checked = dt.Rows(0)("EnableItemCategoryID")
            Catch ex As Exception

            End Try
            Try
                txtMetaKeywords.Text = dt.Rows(0)("MetaKeywords")
            Catch ex As Exception

            End Try
            Try
                txtMetadescription.Text = dt.Rows(0)("Metadescription")
            Catch ex As Exception

            End Try
            Try
                txtSEOTitle.Text = dt.Rows(0)("SEOTitle")
            Catch ex As Exception

            End Try
            Try
                txtContainerType.Text = dt.Rows(0)("ContainerType")
            Catch ex As Exception

            End Try
            Try
                txtCategoryUrlLink.Text = dt.Rows(0)("CategoryUrlLink")
            Catch ex As Exception

            End Try


        End If


    End Sub


    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")


    Public Function FillDetailscategorybyid() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryCategories where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3 and ItemCategoryID=@f4"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = Me.ItemCategoryID

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


    Private Sub btnTab1Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab1Save.Click
        savedata()
    End Sub

    Private Sub btnTab1SaveClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTab1SaveClose.Click
        savedata()
        Response.Redirect("ItemsCategories.aspx")
    End Sub

    Private Sub savedata()
        ',ItemCategoryID
        ',ItemFamilyID
        ',CategoryName
        ',CategoryDescription
        ',CategoryLongDescription
        ',CategoryPictureURL
        ',EnableItemCategoryID
        ',ContainerType
        ',CategoryUrlLink
        ',SortOrder
        ',MetaKeywords
        ',Metadescription
        ',SEOTitle

        Me.ItemFamilyID = txtItemFamilyID.Text
        Me.ItemCategoryID = txtItemCategoryID.Text

        Update_ItemFamilyID(txtItemFamilyID.Text, "CategoryName", txtCategoryName.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "CategoryDescription", txtCategoryDescription.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "CategoryLongDescription", txtCategoryLongDescription.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "CategoryPictureURL", txtCategoryPictureURL.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "SortOrder", txtSortOrder.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "EnableItemCategoryID", chkEnableItemCategoryID.Checked)
        Update_ItemFamilyID(txtItemFamilyID.Text, "MetaKeywords", txtMetaKeywords.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "Metadescription", txtMetadescription.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "SEOTitle", txtSEOTitle.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "ContainerType", txtContainerType.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "CategoryUrlLink", txtCategoryUrlLink.Text)


    End Sub


    Public Function Update_ItemFamilyID(ByVal _ItemFamilyID As String, ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim com As SqlCommand



        Dim ds As New DataTable
        ds = FillDetailscategorybyid()
        If ds.Rows.Count > 0 Then
        Else
            qry = "Insert into [InventoryCategories] ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[ItemFamilyID],ItemCategoryID) values(@CompanyID,@DivisionID,@DepartmentID,@ItemFamilyID,@ItemCategoryID)"
            com = New SqlCommand(qry, connec)
            Try
                com.Parameters.AddWithValue("@ItemCategoryID", Me.ItemCategoryID)
                com.Parameters.AddWithValue("@ItemFamilyID", Me.ItemFamilyID)
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


        qry = "Update InventoryCategories SET  " & name & " =@value Where  ItemCategoryID = @ItemCategoryID  AND  ItemFamilyID = @ItemFamilyID  AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.AddWithValue("@value", value)
            com.Parameters.AddWithValue("@ItemCategoryID", Me.ItemCategoryID)
            com.Parameters.AddWithValue("@ItemFamilyID", Me.ItemFamilyID)
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


    Public Function FillDetailsfamily() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
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


End Class
