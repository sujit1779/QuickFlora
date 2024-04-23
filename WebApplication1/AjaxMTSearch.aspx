<%@ Page Language="VB" EnableViewState="false"   %>

<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Data.SqlClient"%>
<%@Import Namespace="System.Configuration"%> 


 
<script runat="server">

    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim CustomerID As String = ""
    Public locationid As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)


        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        If Not Request.QueryString("k") = Nothing Then
            Dim keyword As String = ""
            keyword = Request.QueryString("k")

            '         UPDATE(InventoryTransferHeader)
            'SET TransferDate = @TransferDate,
            '	TansferFromLocation = @TansferFromLocation,
            '	TransferToLocation = @TransferToLocation,
            '	TransferByEmployee = @TransferByEmployee,
            '	ApprovedByEmployee = @ApprovedByEmployee,
            '	ApprovedTime = @ApprovedTime,
            '	TotalItemsTransfer = @TotalItemsTransfer


            If keyword <> "" Then

                Dim sql As String = "select top 50 InventoryTransferHeader.[TransferNumber],[PayrollEmployees].[EmployeeName],InventoryTransferHeader.[TransferByEmployee],InventoryTransferHeader.[TransferToLocation],InventoryTransferHeader.[TansferFromLocation],TotalItemsTransfer,ApprovedByEmployee from [InventoryTransferHeader] "
                sql = sql & "  LEFT JOIN "
                sql = sql & "  PayrollEmployees ON InventoryTransferHeader.CompanyID = PayrollEmployees.CompanyID AND "
                sql = sql & "  InventoryTransferHeader.DivisionID = PayrollEmployees.DivisionID AND InventoryTransferHeader.DepartmentID = PayrollEmployees.DepartmentID AND "
                sql = sql & "  InventoryTransferHeader.TransferByEmployee = PayrollEmployees.EmployeeID "
                sql = sql & "  where   InventoryTransferHeader.CompanyID=@CompanyID AND InventoryTransferHeader.DivisionID=@DivisionID AND InventoryTransferHeader.DepartmentID=@DepartmentID AND ("
                sql = sql & " CAST(InventoryTransferHeader.[TransferNumber] as varchar(10))  like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or InventoryTransferHeader.[TansferFromLocation] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or InventoryTransferHeader.[TransferToLocation] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or InventoryTransferHeader.[TransferByEmployee] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or InventoryTransferHeader.[ApprovedByEmployee] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or [PayrollEmployees].[EmployeeName] like '%" + keyword.Trim().Replace("'", "''") + "%'  )"

                Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
                Dim myCommand As New SqlCommand(sql, myCon)
                myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
                myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
                myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)

                Dim da As New SqlDataAdapter(myCommand)
                Dim dt As New DataTable

                'populate the data control and bind the data source

                da.Fill(dt)



                Dim str As String = "<table width='100%'   id='CustomerInfoGrid' class='table table-striped table-hover table-bordered'>"



                If dt.Rows.Count <> 0 Then


                    Dim n As Integer = 0
                    Dim gridrow As Boolean = True

                    For Each row As DataRow In dt.Rows
                        n = n + 1
                        keyword = keyword.ToLower

                        Dim TansferFromLocation As String = row("TansferFromLocation").ToString()
                        TansferFromLocation = TansferFromLocation.ToLower
                        TansferFromLocation = TansferFromLocation.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                        Dim TransferToLocation As String = row("TransferToLocation").ToString()
                        TransferToLocation = TransferToLocation.ToLower
                        TransferToLocation = TransferToLocation.Replace(keyword, "<font color='red'>" + keyword + "</font>")


                        Dim TransferByEmployee As String = row("TransferByEmployee").ToString()
                        TransferByEmployee = TransferByEmployee.ToLower
                        TransferByEmployee = TransferByEmployee.Replace(keyword, "<font color='red'>" + keyword + "</font>")


                        Dim ApprovedByEmployee As String = row("ApprovedByEmployee").ToString()
                        ApprovedByEmployee = ApprovedByEmployee.ToLower
                        ApprovedByEmployee = ApprovedByEmployee.Replace(keyword, "<font color='red'>" + keyword + "</font>")



                        Dim EmployeeName As String = row("EmployeeName").ToString()
                        EmployeeName = EmployeeName.ToUpper
                        EmployeeName = EmployeeName.Replace(keyword.ToUpper, "<font color='red'>" + keyword.ToUpper + "</font>")

                        Dim TransferNumber As String = row("TransferNumber").ToString()
                        Dim TransferNumber_ As String = row("TransferNumber").ToString()
                        TransferNumber = TransferNumber.ToUpper
                        TransferNumber = TransferNumber.Replace(keyword.ToUpper, "<font color='red'>" + keyword.ToUpper + "</font>")

                        Dim TotalItemsTransfer As String = ""

                        Try
                            TotalItemsTransfer = row("TotalItemsTransfer").ToString()
                            TotalItemsTransfer = TotalItemsTransfer.ToLower
                            TotalItemsTransfer = TotalItemsTransfer.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try


                        str = str & "<td align='left'><a  href=" + """" + "javascript:FillSearchtextBox('[" + TransferNumber_ + "]')" + """" + " ><strong>" + TransferNumber + "</strong> </a></td>"
                        str = str & "<td align='left'>From:" + TansferFromLocation + "</td>"
                        str = str & "<td >To:" & TransferToLocation & "</a></td>"
                        str = str & "<td >By:" & TransferByEmployee & "-" & EmployeeName & "</a></td>"
                        str = str & "<td >Approved:" & ApprovedByEmployee & "</a></td>"
                        str = str & "<td align='left'>"

                        str = str & "" & TotalItemsTransfer & "</td>"
                        str = str & "</tr>"

                    Next
                    str = str & "</table>"
                    Response.Write(str)
                    Response.End()
                Else
                    Response.End()
                    Response.Clear()
                    Response.Write("")
                End If

            End If

        End If


    End Sub


</script>
 
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
</head>
<body>
    <form id="form1" runat="server">
    
    </form>
</body>
</html>