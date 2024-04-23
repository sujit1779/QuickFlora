
Partial Class ErrorBoard
    Inherits System.Web.UI.Page

    Public errormessage As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            errormessage = Request.QueryString("aspxerrorpath")

            errormessage = "<br/><br/>Please contact administrator for more information <br/><br/> Error occurred on page <br/>" & errormessage
        Catch ex As Exception

        End Try

    End Sub
End Class
