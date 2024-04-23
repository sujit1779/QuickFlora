Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class Home
    Inherits System.Web.UI.Page

    Dim CompanyID As String
    Dim DivisionID As String
    Dim DepartmentID As String
    Dim EmployeeID As String
    Dim TerminalID As String
    Public locationid As String = ""
    Dim ShiftID As Integer = 0

    Public POURL As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        OldCommon.GetCompanyInfo(CompanyID, DivisionID, DepartmentID, EmployeeID)

        If Me.CompanyID = "JuliaTesta11221" Or Me.CompanyID = "QuickfloraDemo" Then
            Session("ShiftID") = "67653"
            Session("TerminalID") = "DEFAULT"
            If Me.CompanyID = "QuickfloraDemo" Then
                Session("ShiftID") = "67655"
            End If
        End If

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        locationid = Session("Locationid")
        TerminalID = Session("TerminalID")

        POURL = "http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & locationid & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID

    End Sub
End Class
