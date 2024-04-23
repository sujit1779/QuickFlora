
Partial Class VendorEmailList
    Inherits System.Web.UI.Page

    Public CustomerEmail As String = ""
    Public CustomerID As String = ""
    Public CustomerName As String = ""

    Private Sub VendorEmailList_Load(sender As Object, e As EventArgs) Handles Me.Load
        CustomerID = Request.QueryString("VendorID")
        'CustomerEmail = Request.QueryString("CustomerEmail")
        CustomerName = Request.QueryString("VendorName")

    End Sub
End Class
