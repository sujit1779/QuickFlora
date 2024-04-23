Imports System.Data
Imports System.Data.SqlClient

Partial Class ItemsmarkArchived
    Inherits System.Web.UI.Page


    Dim obj As New clsItems
    Public CompanyID As String, DivisionID As String, DepartmentID As String

    Private Sub ItemsmarkArchived_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))
        ' EmployeeID = CStr(SessionKey("EmployeeID"))

        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        If Not Page.IsPostBack Then
            If Not Request.QueryString("ItemID") = Nothing Then
                FillItemDetail()
            End If
        End If

    End Sub

    Private Sub FillItemDetail()

        Dim ds As New DataSet
        ds = obj.GetInventoryItemDetail(Request.QueryString("ItemID"))

        If ds.Tables(0).Rows.Count > 0 Then
            Dim row As DataRow
            row = ds.Tables(0).Rows(0)



            'First Tab
            txtItemID.Text = row("ItemID").ToString
            txtItemID.Enabled = False
            Try
                drpItemType.SelectedValue = row("ItemTypeID").ToString
            Catch ex As Exception
                ' lbldebug.Text = lbldebug.Text & "<br>" & "drpItemType:" & ex.Message
            End Try


            txtItemName.Text = row("ItemName").ToString
            txtItemName.Enabled = False
            txtItemShortDescription.Text = row("ItemDescription").ToString
            txtItemShortDescription.Enabled = False
            txtItemLongDescription.Text = row("ItemLongDescription").ToString
            txtItemLongDescription.Enabled = False


        End If

    End Sub

    Private Sub btnTab1Save_Click(sender As Object, e As EventArgs) Handles btnTab1Save.Click
        savetab1()
        Response.Redirect("ItemList.aspx")
    End Sub

    Public Sub savetab1()

        Update_InventoryItems(txtItemID.Text, "IsActive", False)
        Update_InventoryItems(txtItemID.Text, "ActiveForEvents", False)
        Update_InventoryItems(txtItemID.Text, "ActiveForRecipe", False)
        Update_InventoryItems(txtItemID.Text, "ActiveForStore", False)
        Update_InventoryItems(txtItemID.Text, "ActiveForPOM", False)
        Update_InventoryItems(txtItemID.Text, "EnabledfrontEndItem", False)
        Update_InventoryItems(txtItemID.Text, "EnableItemPrice", False)
        Update_InventoryItems(txtItemID.Text, "EnableAddtoCart", False)
        Update_InventoryItems(txtItemID.Text, "WireServiceIDAllowed", False)
        Update_InventoryItems(txtItemID.Text, "archived", True)

    End Sub


    Public Function Update_InventoryItems(ByVal ItemID As String, ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim com As SqlCommand

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

    Private Sub btnTab1SaveClose_Click(sender As Object, e As EventArgs) Handles btnTab1SaveClose.Click
        Response.Redirect("ItemList.aspx")
    End Sub
End Class
