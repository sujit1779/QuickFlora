
Partial Class AdjustReceivedPO
    Inherits System.Web.UI.Page
    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public PO As String = ""

    Private Sub AdjustReceivedPO_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            PO = Request.QueryString("PO")
        Catch ex As Exception
            PO = ""
        End Try

        If Session("CompanyID") Is Nothing Then
            Response.Redirect("loginform.aspx")
        End If

        'Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        '' get the connection ready
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
    End Sub
End Class
