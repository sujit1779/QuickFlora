
Imports System.Data

Partial Class InventoryTransferListsExcel
    Inherits System.Web.UI.Page

    Private Sub InventoryTransferListsExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dt As New DataTable
        dt = Session("InventoryTransferLists")

        If dt.Rows.Count <> 0 Then

            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "InventoryTransferLists.xls"))
            Response.ContentType = "application/ms-excel"

            Dim str As String = String.Empty
            For Each dtcol As DataColumn In dt.Columns
                Response.Write(str + dtcol.ColumnName)
                str = vbTab
            Next
            Response.Write(vbLf)
            For Each dr As DataRow In dt.Rows
                str = ""
                For j As Integer = 0 To dt.Columns.Count - 1
                    Response.Write(str & Convert.ToString(dr(j)))
                    str = vbTab
                Next
                Response.Write(vbLf)
            Next
            Response.[End]()

        Else
            Response.Clear()
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Dim stringWrite As New System.IO.StringWriter
            Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
            ' grid.RenderControl(htmlWrite)
            Response.Write(stringWrite.ToString)

            Response.End()

        End If
    End Sub
End Class
