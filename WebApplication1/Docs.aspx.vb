
Partial Class Docs
    Inherits System.Web.UI.Page
    Public css As String = "none"
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""

    Private Sub Docs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim filters As EnterpriseCommon.Core.FilterSet

        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        If CompanyID.ToLower = "QuickfloraDemo".ToLower Or CompanyID.ToLower = "FMW".ToLower Then
        Else
            li1.Visible = False
            li2.Visible = False
            li3.Visible = False
        End If
    End Sub
End Class
