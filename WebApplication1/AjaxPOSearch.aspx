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
            
          
            
            If keyword <> "" Then
                
                  Dim sql As String = "select top 50 PurchaseHeader.[PurchaseNumber],[VendorInformation].[VendorName],PurchaseHeader.[VendorID],PurchaseHeader.[ShipMethodID],PurchaseHeader.[PaymentMethodID],PurchaseHeader.[TrackingNumber],PurchaseHeader.[OrderNumber],PurchaseHeader.InternalNotes,PurchaseHeader.Total,PurchaseHeader.TransactionTypeID from [PurchaseHeader] "
                sql = sql & "  LEFT JOIN "
                sql = sql & "  VendorInformation ON PurchaseHeader.CompanyID = VendorInformation.CompanyID AND "
                sql = sql & "  PurchaseHeader.DivisionID = VendorInformation.DivisionID AND PurchaseHeader.DepartmentID = VendorInformation.DepartmentID AND "
                sql = sql & "  PurchaseHeader.VendorID = VendorInformation.VendorID "
                sql = sql & "  where PurchaseHeader.VendorID <> 'DEFAULT' AND PurchaseHeader.CompanyID=@CompanyID AND PurchaseHeader.DivisionID=@DivisionID AND PurchaseHeader.DepartmentID=@DepartmentID AND ("
                sql = sql & "  PurchaseHeader.[PurchaseNumber] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or PurchaseHeader.[VendorID] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or PurchaseHeader.[ShipMethodID] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or PurchaseHeader.[PaymentMethodID] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or PurchaseHeader.[TrackingNumber] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or PurchaseHeader.[OrderNumber] like '%" + keyword.Trim().Replace("'", "''") + "%'  "
                sql = sql & "  Or PurchaseHeader.[VendorInvoiceNumber] like '%" + keyword.Trim().Replace("'", "''") + "%'  "
                
                sql = sql & "  Or PurchaseHeader.[InternalNotes] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or PurchaseHeader.[ShippingAddress1] like '%" + keyword.Trim().Replace("'", "''") + "%' "
                sql = sql & "  Or PurchaseHeader.[ShippingCity] like '%" + keyword.Trim().Replace("'", "''") + "%'  "
                sql = sql & "  Or PurchaseHeader.[TransactionTypeID] like '%" + keyword.Trim().Replace("'", "''") + "%'  "
                sql = sql & "  Or [VendorInformation].[VendorName] like '%" + keyword.Trim().Replace("'", "''") + "%'  "
                sql = sql & "  Or PurchaseHeader.[ShippingZip] like '%" + keyword.Trim().Replace("'", "''") + "%')"
                 
                
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
                        
                        Dim ShipMethodID As String = row("ShipMethodID").ToString()
                        ShipMethodID = ShipMethodID.ToLower
                        ShipMethodID = ShipMethodID.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        
                           Dim VendorName As String = row("VendorName").ToString()
                        VendorName = VendorName.ToLower
                        VendorName = VendorName.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                        
                        Dim TransactionTypeID As String = row("TransactionTypeID").ToString()
                        TransactionTypeID = TransactionTypeID.ToLower
                        TransactionTypeID = TransactionTypeID.Replace(keyword, "<font color='red'>" + keyword + "</font>")


                        Dim PaymentMethodID As String = row("PaymentMethodID").ToString()
                        PaymentMethodID = PaymentMethodID.ToLower
                        PaymentMethodID = PaymentMethodID.Replace(keyword, "<font color='red'>" + keyword + "</font>")

                      
                        
                        Dim VendorID As String = row("VendorID").ToString()
                        VendorID = VendorID.ToUpper
                        VendorID = VendorID.Replace(keyword.ToUpper, "<font color='red'>" + keyword.ToUpper + "</font>")
                        
                        Dim PurchaseNumber As String = row("PurchaseNumber").ToString()
                        PurchaseNumber = PurchaseNumber.ToUpper
                       ' PurchaseNumber = PurchaseNumber.Replace(keyword.ToUpper, "<font color='red'>" + keyword.ToUpper + "</font>")
                        
                        Dim Total As String = ""
                        
                        Try
                            Total = row("Total").ToString()
                            Total = Total.ToLower
                            Total = Total.Replace(keyword, "<font color='red'>" + keyword + "</font>")
                        Catch ex As Exception

                        End Try
                        
                                                
                        str = str & "<td align='left'><a  href=" + """" + "javascript:FillSearchtextBox('[" + PurchaseNumber + "]')" + """" + " ><strong>" + PurchaseNumber + "</strong> </a></td>"
                        'str = str & "<td align='left'>" + CustomerID + "</td>"
                        str = str & "<td >" & VendorID & "</a></td>"
                        str = str & "<td >" & VendorName & "</a></td>"
                        str = str & "<td >" & TransactionTypeID & "</a></td>"
                        str = str & "<td align='left'>"
                       
                        str = str & "" & ShipMethodID & "</td>"
                        str = str & "<td >" & Total & "</td>"
                        str = str & "<td align='left'>" & PaymentMethodID & "</td>"
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
    
    
    Public Function Custdetails(ByVal row As DataRow) As String
        
        Dim details As String = ""
        details = details & ""
        Return details
        Dim nchk As Boolean = True

        Try
            If row("CustomerSalutation").ToString.Trim <> "" Then
                details = details & row("CustomerSalutation").ToString.Trim & " "
            End If
        Catch ex As Exception
        End Try
        Dim cusfname As String = ""
        Dim cuslname As String = ""
        Try
            If row("CustomerFirstName").ToString.Trim <> "" Then
                cusfname = row("CustomerFirstName").ToString.Trim
            End If
        Catch ex As Exception
        End Try
        Try
            If row("CustomerLastName").ToString.Trim <> "" Then
                cuslname = row("CustomerLastName").ToString.Trim
            End If
        Catch ex As Exception
        End Try
        Dim fulname As String = ""
        fulname = cusfname & " " & cuslname
        
        If fulname.Trim() = "" Then
            Try
                If row("CustomerName").ToString.Trim <> "" Then
                    nchk = False
                    details = details & row("CustomerName").ToString.Trim
                End If
            Catch ex As Exception
            End Try
        Else
            nchk = False
            details = details & fulname
        End If
        

        Try
            If row("Attention").ToString.Trim <> "" Then
                nchk = False
                details = details & vbCrLf & row("Attention").ToString.Trim
            End If
        Catch ex As Exception

        End Try

        'CustomerCompany

        Try
            If row("CustomerCompany").ToString.Trim <> "" Then
                nchk = False
                details = details & vbCrLf & row("CustomerCompany").ToString.Trim
            End If
        Catch ex As Exception

        End Try
        

      
        Try
            If row("CustomerAddress1").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & row("CustomerAddress1").ToString.Trim
                Else
                    details = details & row("CustomerAddress1").ToString.Trim
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If row("CustomerAddress2").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & row("CustomerAddress2").ToString.Trim
                Else
                    details = details & row("CustomerAddress2").ToString.Trim
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If row("CustomerAddress3").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & row("CustomerAddress3").ToString.Trim
                Else
                    details = details & row("CustomerAddress3").ToString.Trim
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If row("CustomerCity").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & row("CustomerCity").ToString.Trim & " "
                Else
                    details = details & row("CustomerCity").ToString.Trim & " "
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If row("CustomerState").ToString.Trim <> "" Then
                details = details & row("CustomerState").ToString.Trim & " "
            End If
        Catch ex As Exception

        End Try

        Try
            If row("CustomerZip").ToString.Trim <> "" Then
                details = details & row("CustomerZip").ToString.Trim
            End If
        Catch ex As Exception

        End Try
        
        Try
            If row("CustomerEmail").ToString.Trim <> "" Then
                nchk = False
                details = details & vbCrLf & row("CustomerEmail").ToString.Trim
            End If
        Catch ex As Exception

        End Try
        
        details = details.Replace(vbCrLf, "<br>")
        
        Return details
        
    End Function
    
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