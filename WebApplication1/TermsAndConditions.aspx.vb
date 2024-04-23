Imports System.Data.SqlClient

Imports System.Data

Partial Class TermsAndConditions
    Inherits System.Web.UI.Page

    Protected Sub btnAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Dim AutoPostID As String = ""
        AutoPostID = Request.QueryString("AutoPostID")
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim myConnection As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ',[IP]
        ',[Employee]

        Dim Address As String = Context.Request.ServerVariables("REMOTE_ADDR")
        Dim Employee As String = ""

        Try
            Employee = Request.QueryString("EmployeeID")
        Catch ex As Exception

        End Try

        ssql = "Update [NCRSmall] SET ACCEPT_TERMS = 1,IP='" & Address & "',Employee='" & Employee & "',[AcceptOn] = GETDATE() where RequestID=" & AutoPostID
        Dim myCommand As New SqlCommand(ssql, myConnection)
        Dim workParam As New SqlParameter()
        myCommand.CommandType = CommandType.Text
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()

        Response.Redirect("Home.aspx?AutoPostID=" & AutoPostID)

    End Sub

    Protected Sub TermsAndConditions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDecline.Attributes.Add("style", "font-weight: 600; padding: 9px 40px !important;")
    End Sub


End Class
