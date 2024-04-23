
Partial Class RO_PreBookList
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public parameter As String = ""
    Public EmployeeID As String = ""

    Private Sub RO_PreBookList_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim company As String = CType(SessionKey("CompanyID"), String)
        Dim UserName As String = CType(SessionKey("EmployeeID"), String)
        Dim DivID As String = CType(SessionKey("DivisionID"), String)
        Dim DeptID As String = CType(SessionKey("DepartmentID"), String)
        CompanyID = company
        DivisionID = DivID
        DepartmentID = DeptID
        EmployeeID = UserName

    End Sub
End Class
