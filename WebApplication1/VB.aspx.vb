
Partial Class VB
    Inherits System.Web.UI.Page
    Protected Sub Submit(sender As Object, e As EventArgs)
        Dim message As String = ""
        For Each item As ListItem In lstFruits.Items
            If item.Selected Then
                message += item.Text + " " + item.Value + "\n"
            End If
        Next
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "alert('" & message & "');", True)
    End Sub
End Class
