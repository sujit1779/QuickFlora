
Partial Class POAdjustmentLog
    Inherits System.Web.UI.Page
    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""

    Private Sub POAdjustmentLog_Load(sender As Object, e As EventArgs) Handles Me.Load
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
