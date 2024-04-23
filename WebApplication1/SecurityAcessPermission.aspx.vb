
Partial Class SecurityAcessPermission
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim qr As String = ""
        Try
            qr = Request.QueryString("MOD")
        Catch ex As Exception

        End Try

        Dim comm As String = ""
        comm = ""

        If qr = "PO" Then
            comm = "purchase order"
        End If

        If qr = "REC" Then
            comm = "receive purchase"
        End If

        If qr = "IA" Then
            comm = "Inventory Adjustment"
        End If

        If qr = "AP" Then
            comm = "Accounts Payables"
        End If
        If qr = "AR" Then
            comm = "Accounts Recievables"
        End If
        If qr = "BatchPO" Then
            comm = "Batch Purchasing"
        End If
        If qr = "PurchaseRequest" Then
            comm = "Purchase Request"
        End If


        lblpermisssion.Text = "Your login not have Permission for " & comm & " please contact your manager."

    End Sub
End Class
