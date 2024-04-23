
Imports System.Data
Imports System.Data.SqlClient

Partial Class InventoryFamiliesDetails
    Inherits System.Web.UI.Page

    Public CompanyID As String, DivisionID As String, DepartmentID As String

    Dim EmployeeID As String = ""

    Private Sub InventoryFamiliesDetails_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))
        EmployeeID = CStr(SessionKey("EmployeeID"))

        If Not Page.IsPostBack Then

            If Not Request.QueryString("ItemFamilyID") = Nothing Then
                fillItemFamilyID()
            End If

        End If


    End Sub
    Dim ItemFamilyID As String = ""

    Private Sub fillItemFamilyID()

        ItemFamilyID = Request.QueryString("ItemFamilyID")

        Dim dt As New DataTable
        dt = FillDetailsfamilybyid()


        If dt.Rows.Count <> 0 Then

            Try
                txtItemFamilyID.Text = dt.Rows(0)("ItemFamilyID")
                txtItemFamilyID.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtFamilyName.Text = dt.Rows(0)("FamilyName")
            Catch ex As Exception

            End Try
            Try
                txtFamilyDescription.Text = dt.Rows(0)("FamilyDescription")
            Catch ex As Exception

            End Try
            Try
                txtFamilyLongDescription.Text = dt.Rows(0)("FamilyLongDescription")
            Catch ex As Exception

            End Try

            Try
                txtFamilyPictureURL.Text = dt.Rows(0)("FamilyPictureURL")
            Catch ex As Exception

            End Try

            Try
                txtSortOrder.Text = dt.Rows(0)("SortOrder")
            Catch ex As Exception

            End Try
            Try
                chkEnableItemFamilyID.Checked = dt.Rows(0)("EnableItemFamilyID")
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


        End If


    End Sub


    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
    Public Function FillDetailsfamilybyid() As DataTable
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from InventoryFamilies where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 and ItemFamilyID=@f3"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.ItemFamilyID

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


    Private Sub btnTab1Save_Click(sender As Object, e As EventArgs) Handles btnTab1Save.Click
        savedata()
    End Sub

    Private Sub btnTab1SaveClose_Click(sender As Object, e As EventArgs) Handles btnTab1SaveClose.Click
        savedata()
        Response.Redirect("IitemsFamily.aspx")
    End Sub

    Private Sub savedata()
        ',[ItemFamilyID]
        ',[FamilyName]
        ',[FamilyDescription]
        ',[FamilyLongDescription]
        ',[FamilyPictureURL]
        ' ,[SortOrder]
        ',[EnableItemFamilyID]
        ',[MetaKeywords]
        ',[Metadescription]
        ',[SEOTitle]
        Update_ItemFamilyID(txtItemFamilyID.Text, "FamilyName", txtFamilyName.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "FamilyDescription", txtFamilyDescription.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "FamilyLongDescription", txtFamilyLongDescription.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "FamilyPictureURL", txtFamilyPictureURL.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "SortOrder", txtSortOrder.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "EnableItemFamilyID", chkEnableItemFamilyID.Checked)
        Update_ItemFamilyID(txtItemFamilyID.Text, "MetaKeywords", txtMetaKeywords.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "Metadescription", txtMetadescription.Text)
        Update_ItemFamilyID(txtItemFamilyID.Text, "SEOTitle", txtSEOTitle.Text)

    End Sub


    Public Function Update_ItemFamilyID(ByVal _ItemFamilyID As String, ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim com As SqlCommand

        Me.ItemFamilyID = _ItemFamilyID

        Dim ds As New DataTable
        ds = FillDetailsfamilybyid()
        If ds.Rows.Count > 0 Then
        Else
            qry = "Insert into [InventoryFamilies] ([CompanyID] ,[DivisionID] ,[DepartmentID] ,[ItemFamilyID]) values(@CompanyID,@DivisionID,@DepartmentID,@ItemFamilyID)"
            com = New SqlCommand(qry, connec)
            Try

                com.Parameters.AddWithValue("@ItemFamilyID", _ItemFamilyID)
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


        qry = "Update InventoryFamilies SET  " & name & " =@value Where ItemFamilyID = @ItemFamilyID  AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.AddWithValue("@value", value)
            com.Parameters.AddWithValue("@ItemFamilyID", _ItemFamilyID)
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



End Class
