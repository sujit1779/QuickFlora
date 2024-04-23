Imports System.Data
Imports System.Data.SqlClient
Imports DAL
Partial Class MonthlyReceivingReportExcel
    Inherits System.Web.UI.Page

    Public CompanyID As String = ""
    Public DivisionID As String = ""
    Public DepartmentID As String = ""
    Public EmployeeID As String = ""

    Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        EmployeeID = Session("EmployeeID")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""

        ssql = "[dbo].[ReceivingreportbyMonth]"

        Dim dt As New DataTable
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)


        com.CommandType = CommandType.StoredProcedure
        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@fromDate", SqlDbType.DateTime)).Value = Session("txtDeliveryDate")
        com.Parameters.Add(New SqlParameter("@todate", SqlDbType.DateTime)).Value = Session("txtDeliveryDateTO")
        com.Parameters.Add(New SqlParameter("@VendorCode", SqlDbType.NVarChar, 36)).Value = Session("VendorCode")
        com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar, 36)).Value = Session("PurchaseNumber")
        com.Parameters.Add(New SqlParameter("@bydate", SqlDbType.NVarChar, 36)).Value = Session("bydate")
        'Session("txtDeliveryDate") = txtDeliveryDate.Text
        'Session("txtDeliveryDateTO") = txtDeliveryDateTO.Text
        'Session("VendorCode") = cmblocationid.SelectedValue
        'Session("PurchaseNumber") = ""

        da.SelectCommand = com
        da.Fill(dt)
        'Session("txtDeliveryDate") = txtDeliveryDate.Text
        'Session("txtDeliveryDateTO") = txtDeliveryDateTO.Text
        ''Session("cmblocationid") = cmblocationid.SelectedValue
        'lblmsg.Text = lblmsg.Text & " <br><br>txtDeliveryDate" & Session("txtDeliveryDate")
        'lblmsg.Text = lblmsg.Text & "<br><br>txtDeliveryDateTO" & Session("txtDeliveryDateTO")
        'lblmsg.Text = lblmsg.Text & "<br><br>cmblocationid" & Session("cmblocationid")
        'lblmsg.Text = lblmsg.Text & "<br><br>PurchaseNumber" & Session("PurchaseNumber")

        If dt.Rows.Count <> 0 Then

            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "MonthlyReceivingReport.xls"))
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
            lblmsg.Text = lblmsg.Text & "No Result Found <br><br>"

        End If
    End Sub




End Class
