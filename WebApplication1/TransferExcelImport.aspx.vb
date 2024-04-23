Imports System.Data
Imports System.Data.SqlClient
Imports DAL

Partial Class TransferExcelImport
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Private Sub ExportExcelROL_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        EmployeeID = Session("EmployeeID")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""

        ssql = "[enterprise].GetInventoryTransferListExcelPivotNewLC"

        Dim dt As New DataTable
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)


        com.CommandType = CommandType.StoredProcedure
        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@fromDate", SqlDbType.NVarChar, 36)).Value = Session("fromDate")
        'com.Parameters.Add(New SqlParameter("@todate", SqlDbType.NVarChar, 36)).Value = txtDeliveryDate.Text
        com.Parameters.Add(New SqlParameter("@FromLocaton", SqlDbType.NVarChar, 36)).Value = Session("drpTansferFromLocaton")
        com.Parameters.Add(New SqlParameter("@ToLocaton", SqlDbType.NVarChar, 36)).Value = Session("drpTransferToLocaton")


        da.SelectCommand = com
        da.Fill(dt)
        'Session("txtDeliveryDate") = txtDeliveryDate.Text
        'Session("txtDeliveryDateTO") = txtDeliveryDateTO.Text
        'Session("cmblocationid") = cmblocationid.SelectedValue

        If dt.Rows.Count <> 0 Then

            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "TransfersExportReport.xls"))
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
                    If j = 4 Or j = 2 Then
                        If IsNumeric(dr(j)) Then
                            Response.Write(str & "=TEXT(" & Convert.ToString(dr(j)) & ",""0"")")
                        Else
                            Response.Write(str & Convert.ToString(dr(j)))
                        End If
                    Else
                        Response.Write(str & Convert.ToString(dr(j)))
                    End If
                    'Response.Write(str & Convert.ToString(dr(j)))
                    str = vbTab
                Next
                Response.Write(vbLf)
            Next
            Response.[End]()

        Else
            'lblmsg.Text = "No Result Found <br><br>"

        End If
    End Sub


End Class
