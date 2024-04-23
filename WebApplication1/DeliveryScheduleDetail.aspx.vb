
Partial Class DeliveryScheduleDetail
    Inherits System.Web.UI.Page
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Public ScheduleID As String = ""

    Private Sub DeliveryScheduleDetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        ScheduleID = ""
        Try
            ScheduleID = Request.QueryString("ScheduleID")
        Catch ex As Exception

        End Try
    End Sub
End Class
