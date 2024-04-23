
Partial Class RO_AllocateQTY
    Inherits System.Web.UI.Page
    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""
    Public ItemID As String = ""
    Public SDate As String = ""
    Public EDate As String = ""

    Private Sub RO_AllocateQTY_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim company As String = CType(SessionKey("CompanyID"), String)
        Dim UserName As String = CType(SessionKey("EmployeeID"), String)
        Dim DivID As String = CType(SessionKey("DivisionID"), String)
        Dim DeptID As String = CType(SessionKey("DepartmentID"), String)
        CompanyID = company
        DivisionID = DivID
        DepartmentID = DeptID
        EmployeeID = UserName

        If Request.QueryString("ItemID") <> "" Then
            ItemID = Request.QueryString("ItemID")
        Else
            Response.Redirect("PreBookList.aspx")
        End If

        If Request.QueryString("SDate") <> "" Then
            SDate = Request.QueryString("SDate")
        Else
            Response.Redirect("PreBookList.aspx")
        End If

        If Request.QueryString("EDate") <> "" Then
            EDate = Request.QueryString("EDate")
        Else
            Response.Redirect("PreBookList.aspx")
        End If
    End Sub
End Class
