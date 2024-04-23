Option Strict Off

Imports System.Data.SqlClient
Imports System.Data
Imports DAL
Imports EnterpriseCommon.Configuration
Imports EnterpriseASPClient.Core
Imports EnterpriseClient.Core
Imports System.Diagnostics
Imports PayPal.Payments.DataObjects
Imports PayPal.Payments.Common
Imports PayPal.Payments.Common.Utility
Imports PayPal.Payments.Transactions
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports AuthorizeNet

Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Collections.Generic

Partial Class ReceivePO
    Inherits System.Web.UI.Page



    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim PurchaseOrderNumber As String = ""

    Dim Allowed As Boolean = False
    Dim rs As SqlDataReader

    Public Bill_to_Customer_ID As String = "expand"
    Public Ship_To As String = "collapse"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnpost.Visible = False
        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        'If CompanyID.ToUpper = "JWF" Then
        '    Response.Redirect("Home.aspx")
        'End If

        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "REC")

        Dim securitycheck As Boolean = False

        While (rs.Read())

            If rs("EmployeeID").ToString() = EmployeeID Then
                securitycheck = True
                Exit While
            End If

        End While
        rs.Close()

        If securitycheck = False Then
            Response.Redirect("SecurityAcessPermission.aspx?MOD=PO")
        End If

        If CompanyID.ToUpper() = "JWF" Then
            'Response.Redirect("Home.aspx")
        End If

        ' txtcustomersearch.Attributes.Add("placeholder", "SEARCH")
        ' txtcustomersearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")

        ' txtitemsearch.Attributes.Add("placeholder", "SEARCH")
        ' txtitemsearch.Attributes.Add("onKeyUp", "SendQuery2(this.value)")

        txtQty.Attributes.Add("onKeyUp", "onblurtxtQty()")
        'txtitemsearchprice.Attributes.Add("onKeyUp", "Processuomtotal()")
        RQty.Attributes.Add("onKeyUp", "ProcessRecevieQtytotal()")
        RPack.Attributes.Add("onKeyUp", "ProcessRecevieQtytotal()")
        ' txtitemsearchprice.Attributes.Add("onKeyUp", "Processuomtotal()")

        txtitemsearch.Enabled = False
        txtcustomersearch.Enabled = False


        'Populating RetailerLogo
        Dim ImageTemp As String = ""

        Dim PopOrderType As New CustomOrder()
        rs = PopOrderType.PopulateCompanyLogo(CompanyID, DepartmentID, DivisionID)
        While (rs.Read())

            'Dim objNascheck As New clsNasImageCheck
            'ImgRetailerLogo.ImageUrl = objNascheck.retLogourl(rs("CompanyLogoUrl").ToString()) ' "~/images/" & rs("CompanyLogoUrl").ToString()

            ImgRetailerLogo.ImageUrl = "~" & returl(rs("CompanyLogoUrl").ToString())

        End While

        rs.Close()
        If Not IsPostBack Then

            PopulateDrops()
            lblPurchaseOrderDate.Text = DateTime.Now.ToShortDateString()
            txtArrivalDate.Text = DateTime.Now.ToShortDateString()
            txtArrivalDate.Attributes.Add("data-date-format", "mm/dd/yyyy")
            txtArrivalDate.Attributes.Add("data-date-viewmode", "years")
            txtArrivalDate.Attributes.Add("data-date", "01/01/2012")

            txtOrderNumber.Text = lblPurchaseOrderNumberData.Text



            PurchaseOrderNumber = ""
            Try
                PurchaseOrderNumber = Request.QueryString("PurchaseOrderNumber")
            Catch ex As Exception

            End Try


            If PurchaseOrderNumber <> "" Then
                BindCustomerDetails(PurchaseOrderNumber)
                BindGrid()
                txtOrderNumber.Text = lblPurchaseOrderNumberData.Text
            End If




        End If


    End Sub


    Sub BindGrid()


        If lblPurchaseOrderNumberData.Text.Trim() = "(New)" Then

            Exit Sub
        End If

        Dim FillItemDetailGrid As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = GetPurchaseDetail_list(lblPurchaseOrderNumberData.Text)



        Dim tr As String = ""
        Dim n As Integer = 0
        For n = 0 To ds.Tables(0).Rows.Count - 1

            'tr = tr & "<tr><td><input type='text' style='width:50px;' class='form-control' value='" & ds.Tables(0).Rows(n)("OrderQty") & "'></td>"
            'tr = tr & "<td><input type='text' style='width:80px;' class='form-control' value='" & ds.Tables(0).Rows(n)("ItemID") & "'></td>"
            'tr = tr & "<td><input type='text' style='width:80px;' class='form-control' value='" & ds.Tables(0).Rows(n)("ItemName") & "'></td>"
            'tr = tr & "<td><input type='text' style='width:80px;' class='form-control' value='" & ds.Tables(0).Rows(n)("Description") & "'></td>"
            'tr = tr & "<td><input type='text' style='width:80px;' class='form-control' value='" & ds.Tables(0).Rows(n)("ItemUnitPrice") & "'></td>"
            'tr = tr & "<td><input type='text' style='width:80px;' class='form-control' value='" & ds.Tables(0).Rows(n)("DiscountPerc") & "'></td>"
            'tr = tr & "<td><input type='text' style='width:80px;' class='form-control' value='" & ds.Tables(0).Rows(n)("DiscountFlatOrPercent") & "'></td>"
            'tr = tr & "<td><input type='text' style='width:80px;' class='form-control' value='" & ds.Tables(0).Rows(n)("SubTotal") & "'></td>"
            'tr = tr & "<td><a class='edit' href=''>Save</a></td><td><a class='cancel' href=''>Cancel</a></td>"
            'tr = tr & "<td><input type='text'  value='" & ds.Tables(0).Rows(n)("OrderLineNumber") & "'></td></tr>"

            ' ([CompanyID]  ,[DivisionID]  ,[DepartmentID]  ,PurchaseNumber
            ',ItemID ,Description  ,Color ,OrderQty  ,ItemUOM ,Pack ,units ,ItemUnitPrice  ,SubTotal ,Total,[PurchaseDetail].GLPurchaseAccount  )

            tr = tr & "<tr><td>" & ds.Tables(0).Rows(n)("ItemID") & "</td>"
            tr = tr & "<td>" & GetPurchaseDetail_ItemID_name(ds.Tables(0).Rows(n)("ItemID")) & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("Description") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("DetailMemo1") & "</td>"
            'tr = tr & "<td>" & ds.Tables(0).Rows(n)("Color") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("VendorQTY") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("ItemUOM") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("VendorPacksize") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("OrderQty") & "</td>"
            Dim OrderQty As Integer = 0
            Try
                OrderQty = ds.Tables(0).Rows(n)("OrderQty")
            Catch ex As Exception

            End Try
            Try
                tr = tr & "<td>" & ds.Tables(0).Rows(n)("ReceivedVenorQTY") & "</td>"
            Catch ex As Exception
                tr = tr & "<td>0</td>"
            End Try

            Try
                tr = tr & "<td>" & ds.Tables(0).Rows(n)("ReceivedPackSize") & "</td>"
            Catch ex As Exception
                tr = tr & "<td>0</td>"
            End Try



            Try
                tr = tr & "<td>" & ds.Tables(0).Rows(n)("ReceivedQty") & "</td>"
            Catch ex As Exception
                tr = tr & "<td>0</td>"
            End Try
            Dim ReceivedQty As Integer = 0
            Try
                ReceivedQty = ds.Tables(0).Rows(n)("ReceivedQty")
            Catch ex As Exception

            End Try
            If ReceivedQty = OrderQty Then
                tr = tr & "<td> <i class='fa fa-check'  style='color:green' aria-hidden='true'></i>   Received </td>"
            Else
                tr = tr & "<td><a class='edit btn btn-info btn-block btn-xs' href=''>Receive</a></td>"
            End If


            tr = tr & "<td><input type=""hidden"" value=""" & ds.Tables(0).Rows(n)("PurchaseLineNumber") & """></td></tr>"

            txtfirst.Text = 0

        Next
        tbody.InnerHtml = tr


    End Sub


    Public Function GetPurchaseDetail_ItemID_name(ByVal ItemID As String) As String

        Dim name As String = 0

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  ItemName  from [InventoryItems]   where [InventoryItems].ItemID =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = ItemID.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    name = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return name

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return name

    End Function


    Public Function GetPurchaseDetail_list(ByVal PO As String) As Data.DataSet

        Dim Total As Decimal = 0
        Dim dt As New Data.DataSet

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  *  from [PurchaseDetail]   where [PurchaseDetail].PurchaseNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = PO.Trim()


            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return dt

    End Function



    Public Sub BindCustomerDetails(ByVal PurchaseOrderNumber As String)


        Dim dt As New DataTable

        dt = PurchaseOrderNumber_Details(PurchaseOrderNumber)

        If (dt.Rows.Count <> 0) Then

            lblPurchaseOrderDate.Text = "in bind"

            'Total = dt.Rows(0)(0)
            'lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT",
            'txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""),
            'txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, True, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue

            lblPurchaseOrderNumberData.Text = PurchaseOrderNumber

            drpPurchaseTransaction.SelectedValue = dt.Rows(0)("TransactionTypeID").ToString()

            Dim PurchaseOrderDate As Date
            Try
                PurchaseOrderDate = dt.Rows(0)("PurchaseDate")
            Catch ex As Exception

            End Try
            lblPurchaseOrderDate.Text = PurchaseOrderDate.Date

            Try
                invoiveno.Text = dt.Rows(0)("VendorInvoiceNumber")
            Catch ex As Exception

            End Try
            Try
                MABWNO.Text = dt.Rows(0)("MABWNumber")
            Catch ex As Exception

            End Try

            Try
                txtOrderno.Text = dt.Rows(0)("OrderNumber")
            Catch ex As Exception

            End Try

            Try
                txtTrackingno.Text = dt.Rows(0)("TrackingNumber")
            Catch ex As Exception

            End Try

            Try
                drpReceiveby.SelectedValue = dt.Rows(0)("ReceivedBy")
            Catch ex As Exception

            End Try
            Try
                txtVendorInvoiceNumber.Text = dt.Rows(0)("VendorInvoiceNumber")
            Catch ex As Exception

            End Try


            Dim ArrivalDate As Date
            Try
                ArrivalDate = dt.Rows(0)("PurchaseDateRequested")
            Catch ex As Exception

            End Try
            txtArrivalDate.Text = ArrivalDate.Date


            drpEmployeeID.SelectedValue = dt.Rows(0)("OrderedBy").ToString()


            txtVendorTemp.Text = dt.Rows(0)("VendorID").ToString()
            PopulateVendorIDInfo(txtVendorTemp.Text)

            Dim Subtotal As Decimal = 0
            Try
                Subtotal = dt.Rows(0)("Subtotal").ToString()
            Catch ex As Exception

            End Try
            txtSubtotal.Text = Format(Subtotal, "0.00")

            Dim TaxPercent As Decimal = 0
            Try
                TaxPercent = dt.Rows(0)("TaxPercent").ToString()
            Catch ex As Exception

            End Try
            txtTaxPercent.Text = Format(TaxPercent, "0.00")

            Dim TaxAmount As Decimal = 0
            Try
                TaxAmount = dt.Rows(0)("TaxAmount").ToString()
            Catch ex As Exception

            End Try
            txtTax.Text = Format(TaxAmount, "0.00")
            Dim Freight As Decimal = 0
            Try
                Freight = dt.Rows(0)("Freight").ToString()
            Catch ex As Exception

            End Try

            txtDelivery.Text = Format(Freight, "0.00")
            Dim Total As Decimal = 0
            Try
                Total = dt.Rows(0)("Total").ToString()
            Catch ex As Exception

            End Try
            txtTotal.Text = Format(Total, "0.00")
            drpShipMethod.SelectedValue = dt.Rows(0)("ShipMethodID").ToString()
            drpOccasionCode.SelectedValue = dt.Rows(0)("OcassionCodeID").ToString()

            cmblocationid.SelectedValue = dt.Rows(0)("LocationID").ToString()
            drpShiplocation.SelectedValue = dt.Rows(0)("ShipLocationID").ToString()
            Shiplocation(drpShiplocation.SelectedValue)

            drpPaymentType.SelectedValue = dt.Rows(0)("PaymentMethodID").ToString()
            txtInternalNotes.Text = dt.Rows(0)("InternalNotes")

            chkPosted.Checked = dt.Rows(0)("Posted")

            Dim posteddate As Date
            Try
                posteddate = dt.Rows(0)("PostedDate")
            Catch ex As Exception

            End Try

            chkApproved.Checked = dt.Rows(0)("Approved")

            Dim Approveddate As Date
            Try
                Approveddate = dt.Rows(0)("ApprovedDate")
            Catch ex As Exception

            End Try

            txtApprovedDate.Text = Approveddate.Date
            txtApprovedTime.Text = Approveddate.Hour & ":" & Approveddate.Minute & ":" & Approveddate.Second

            Try
                chkReceived.Checked = dt.Rows(0)("Received")
            Catch ex As Exception

            End Try


            Dim Receiveddate As Date
            Try
                Receiveddate = dt.Rows(0)("ReceivedDate")
            Catch ex As Exception

            End Try

            txtReceivedDate.Text = Receiveddate.Date
            txtReceivedTime.Text = Receiveddate.Hour & ":" & Receiveddate.Minute & ":" & Receiveddate.Second


            If chkReceived.Checked Then
                btnpost.Enabled = False
            End If


            drpPurchaseTransaction.Enabled = False
            drpPaymentType.Enabled = False
            lblPurchaseOrderDate.Enabled = False
            txtArrivalDate.Enabled = False
            drpEmployeeID.Enabled = False
            txtVendorTemp.Enabled = False
            txtSubtotal.Enabled = False
            txtTaxPercent.Enabled = False
            txtTax.Enabled = False
            txtDelivery.Enabled = False
            txttotal.Enabled = False
            drpShipMethod.Enabled = False
            cmblocationid.Enabled = False
            drpShiplocation.Enabled = False

            txtPostedDate.Text = posteddate.Date
            txtPostedTime.Text = posteddate.Hour & ":" & posteddate.Minute & ":" & posteddate.Second
            Try
            Catch ex As Exception

            End Try

        End If

    End Sub


    Public Function PurchaseOrderNumber_Details(ByVal PO As String) As DataTable



        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim dt As New DataTable

        qry = "select  *  from [PurchaseHeader]   where [PurchaseHeader].PurchaseNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
        com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
        com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
        com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = PO.Trim()


        Dim da As New SqlDataAdapter(com)

        da.Fill(dt)



        Return dt
        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return dt

    End Function



    Public Function returl(ByVal ob As String) As String

        Dim DocumentDir As String = ""
        Dim img1 As String = ""
        Dim ImgName As String = ob.ToLower()
        DocumentDir = ConfigurationManager.AppSettings("DocPath")
        If (ImgName.Trim() = "") Then

            Return "/images/no_image.gif"

        Else

            If System.IO.File.Exists(DocumentDir & ImgName.Trim()) Then

                Return "/images/" & ImgName.Trim()

            Else
                Return "/images/no_image.gif"
            End If




        End If


    End Function


    Protected Sub ImgUpdateSearchitems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgUpdateSearchitems.Click

        'txtComments.Text = "In ord process"

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) = False Then
            Dim PopOrderNo As New CustomOrder()
            Dim rs As SqlDataReader
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextPurchaseOrderNumber")
            While rs.Read()
                PurchaseOrderNumber = rs("NextNumberValue")

            End While
            rs.Close()
            lblPurchaseOrderNumberData.Text = PurchaseOrderNumber
            Session("PurchaseOrderNumbe") = PurchaseOrderNumber
            txtOrderNumber.Text = lblPurchaseOrderNumberData.Text
            ' EmailSendingWithoutBcc(CompanyID & "- POS new PurchaseOrderNumbe" & PurchaseOrderNumbe & " - line number customer search 14224", "Existing order - " & lblOrderNumberData.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
        End If


    End Sub




    Protected Sub btncustsearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btncustsearch.Click
        Dim TempCustomerID As String = txtcustomersearch.Text
        lblsearchcustomermsg.ForeColor = Drawing.Color.Black

        If txtcustomersearch.Text.Trim = "" Then
            Exit Sub

        End If

        If TempCustomerID.IndexOf("[") > -1 Then
            Dim st, ed As Integer
            st = TempCustomerID.IndexOf("[")
            ed = TempCustomerID.IndexOf("]")

            If st = -1 Then
                Exit Sub
            End If

            If (ed - st) - 1 = -1 Then
                Exit Sub
            End If

            TempCustomerID = TempCustomerID.Substring(st + 1, (ed - st) - 1)
        End If

        Dim CustomerID As String = TempCustomerID


        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        Dim ReturnValue As Integer = CheckVendorExists(CompanyID, DepartmentID, DivisionID, CustomerID)
        If ReturnValue > 0 Then
            txtcustomersearch.Text = ""
            PopulateVendorIDInfo(CustomerID)
            txtVendorTemp.Text = CustomerID

            If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) = False Then
                Dim PopOrderNo As New CustomOrder()
                Dim rs As SqlDataReader
                rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextPurchaseOrderNumber")
                While rs.Read()
                    PurchaseOrderNumber = rs("NextNumberValue")

                End While
                rs.Close()
                lblPurchaseOrderNumberData.Text = PurchaseOrderNumber
                Session("PurchaseOrderNumbe") = PurchaseOrderNumber
                txtOrderNumber.Text = lblPurchaseOrderNumberData.Text
                ' EmailSendingWithoutBcc(CompanyID & "- POS new PurchaseOrderNumbe" & PurchaseOrderNumbe & " - line number customer search 14224", "Existing order - " & lblOrderNumberData.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
            End If

        Else
            lblsearchcustomermsg.Text = "Your search - <b>" & txtcustomersearch.Text.Trim & "</b> - did not match any customers."
            lblsearchcustomermsg.ForeColor = Drawing.Color.Red
            Exit Sub



        End If


        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "CcustomersearchcloseProcess();" & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())

    End Sub

    Public Sub PopulateVendorIDInfo(ByVal VendorID As String)
        Dim dtCST As New DataTable
        If VendorID.Trim <> "" Then
            dtCST = VendorInformation(VendorID)
        End If

        If dtCST.Rows.Count <> 0 Then

            Dim CustomerTypeID As String = ""

            Try
                txtVendorName.Text = dtCST.Rows(0)("VendorName")
                txtVendorName.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtAttention.Text = dtCST.Rows(0)("Attention").ToString()
                txtAttention.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtVendorAddress1.Text = dtCST.Rows(0)("VendorAddress1").ToString()
                txtVendorAddress1.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtVendorAddress2.Text = dtCST.Rows(0)("VendorAddress2").ToString()
                txtVendorAddress2.Enabled = False
            Catch ex As Exception

            End Try

            ' txtVendorAddress3.Text = dtCST.Rows(0)("VendorAddress3").ToString()
            Try
                txtVendorCity.Text = dtCST.Rows(0)("VendorCity").ToString()
                txtVendorCity.Enabled = False
            Catch ex As Exception

            End Try
            Try
                txtVendorFax.Text = dtCST.Rows(0)("VendorFax").ToString()
                txtVendorFax.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorPhone.Text = dtCST.Rows(0)("VendorPhone").ToString()
                txtVendorPhone.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorEmail.Text = dtCST.Rows(0)("VendorEmail").ToString()
                txtVendorEmail.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorZip.Text = dtCST.Rows(0)("VendorZip").ToString()
                txtVendorZip.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorCountry.Text = dtCST.Rows(0)("VendorCountry").ToString()
                txtVendorCountry.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtVendorState.Text = dtCST.Rows(0)("VendorState").ToString()
                txtVendorState.Enabled = False
            Catch ex As Exception

            End Try


        End If

    End Sub


    Public Function VendorInformation(ByVal VendorID As String) As DataTable
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select * from   [VendorInformation] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [VendorID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = VendorID

            com.CommandType = CommandType.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
        Return dt
    End Function



    Public Function CheckVendorExists(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal VendorID As String) As Integer

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[dbo].[CheckVendorExists]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterCustomerID As New SqlParameter("@VendorID", Data.SqlDbType.NVarChar)
        parameterCustomerID.Value = VendorID
        myCommand.Parameters.Add(parameterCustomerID)

        Dim paramReturnValue As New SqlParameter("@ReturnValue", Data.SqlDbType.Int)
        paramReturnValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramReturnValue)

        myCon.Open()
        myCommand.ExecuteNonQuery()

        Dim OutPutValue As Integer
        OutPutValue = Convert.ToInt32(paramReturnValue.Value)
        myCon.Close()
        Return OutPutValue

    End Function


    Public Sub Setdropdown()

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim locationid As String = "Davie"
        If locationid <> "" Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
        End If

        cmblocationid.Items.Remove("Wholesale")

        ' Session("OrderLocationid") = cmblocationid.SelectedValue
    End Sub


    Public Sub PopulateDrops()
        Dim CompanyID As String = ""
        Dim DivisionID As String = ""
        Dim DepartmentID As String = ""
        Dim EmpID As String = ""



        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        '''''''''''''''''
        Dim obj As New clsOrder_Location
        Dim dt As New Data.DataTable
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        dt = obj.FillLocation
        If dt.Rows.Count <> 0 Then
            cmblocationid.DataSource = dt
            cmblocationid.DataTextField = "LocationName"
            cmblocationid.DataValueField = "LocationID"
            cmblocationid.DataBind()
            Setdropdown()
        Else
            cmblocationid.Items.Clear()
            Dim item As New ListItem
            item.Text = "DEFAULT"
            item.Value = "DEFAULT"
            cmblocationid.Items.Add(item)
        End If
        ''''''''''''''''''''

        Dim dt1 As New Data.DataTable
        dt1 = obj.FillLocation
        If dt1.Rows.Count <> 0 Then
            drpShiplocation.DataSource = dt
            drpShiplocation.DataTextField = "LocationName"
            drpShiplocation.DataValueField = "LocationID"
            drpShiplocation.DataBind()
            Dim item As New ListItem
            item.Text = "--Select--"
            item.Value = ""
            drpShiplocation.Items.Insert(0, item)

        End If
        ''''''''''''''''''''

        Dim PopOrderType As New CustomOrder()
        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "PO")

        drpEmployeeID.DataTextField = "EmployeeName"
        drpEmployeeID.DataValueField = "EmployeeID"
        drpEmployeeID.DataSource = rs
        drpEmployeeID.DataBind()
        drpEmployeeID.SelectedValue = EmployeeID ' Session("EmployeeUserName")
        rs.Close()


        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "REC")

        drpReceiveby.DataTextField = "EmployeeName"
        drpReceiveby.DataValueField = "EmployeeID"
        drpReceiveby.DataSource = rs
        drpReceiveby.DataBind()
        drpReceiveby.SelectedValue = EmployeeID
        rs.Close()


        Dim rs1 As SqlDataReader
        rs1 = PopOrderType.PopulatePaymentTypes(CompanyID, DepartmentID, DivisionID)

        drpPaymentType.DataTextField = "PaymentMethodID"
        drpPaymentType.DataValueField = "PaymentMethodID"
        drpPaymentType.DataSource = rs1
        drpPaymentType.DataBind()
        drpPaymentType.Items.Insert(0, (New ListItem("-Select-", "0")))

        rs = PopOrderType.PopulateOccasionCodes(CompanyID, DepartmentID, DivisionID)
        drpOccasionCode.DataTextField = "OccasionDesc"
        drpOccasionCode.DataValueField = "OccasionDesc"
        drpOccasionCode.DataSource = rs
        drpOccasionCode.DataBind()
        drpOccasionCode.Items.Insert(0, (New ListItem("-Select-", "")))
        drpOccasionCode.SelectedIndex = drpOccasionCode.Items.IndexOf(drpOccasionCode.Items.FindByValue("0"))
        rs.Close()
    End Sub




    Public Function PopulateEmployees(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal MO As String) As SqlDataReader

        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()
        Dim myCommand As New SqlCommand("PopulateEmployeesByAccess", ConString)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim pModule As New SqlParameter("@Module", Data.SqlDbType.NVarChar, 36)
        pModule.Value = MO
        myCommand.Parameters.Add(pModule)

        Dim rs As SqlDataReader
        rs = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return rs



    End Function




    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click

        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) Then
            UpdatePurcahseHeaderAllReceivedStatus()
            updatePODone()
            Response.Redirect("ReceivePOList.aspx")
        End If




    End Sub



    Public Function updatePODone() As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update [PurchaseHeader] set  [Done] = 1 where CompanyID=@f0 and DivisionID =@f1 and DepartmentID=@f2 and [PurchaseNumber] =@PurchaseNumber "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar, 36)).Value = lblPurchaseOrderNumberData.Text

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function UpdatePurcahseHeaderAllReceivedStatus() As Boolean

        Using connection As New SqlConnection(ConnectionString)
            Using Command As New SqlCommand("[enterprise].[UpdatePurcahseHeaderAllReceivedStatus]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)
                Command.Parameters.AddWithValue("PurchaseNumber", lblPurchaseOrderNumberData.Text.Trim)
                Command.Parameters.AddWithValue("ReceivedDate", Date.Now)
                Command.Parameters.AddWithValue("ReceivingNumber", txtVendorInvoiceNumber.Text)
                Command.Parameters.AddWithValue("Result", True)
                Command.Connection.Open()
                Command.ExecuteNonQuery()
                Try

                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


    Protected Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) Then

            Dim FillItemDetailGrid As New CustomOrder()
            Dim ds As New Data.DataSet
            ds = GetPurchaseDetail_list(lblPurchaseOrderNumberData.Text)



            Dim tr As String = ""
            Dim n As Integer = 0
            For n = 0 To ds.Tables(0).Rows.Count - 1



                OrderLineNumber = ds.Tables(0).Rows(n)("PurchaseLineNumber")
                VRPack = ds.Tables(0).Rows(n)("VendorPacksize")
                VRQty = ds.Tables(0).Rows(n)("VendorQTY")
                VRTotalQty = ds.Tables(0).Rows(n)("OrderQty")

                updateItemDetailsCustomisedGrid()
                UpdatePurcahseHeaderReceivedStatus()
                updatePOReceiveby()

            Next

            UpdateInventoryReceivedTodayPO()
            UpdatePurcahseHeaderAllReceivedStatus()
            updatePODone()
            Response.Redirect("ReceivePOList.aspx")

        End If



    End Sub

#Region "Inventory"

    Private Sub UpdateInventoryReceivedTodayPO()

        'Find Today POs list
        'Add inventory into in hand
        'find delivery orders list (having backorder items) - order by delivery date, order number, itemid
        'if quantity is enough againast order then remove backorder for that item 

        UpdatePOItemsForReceiving(txtArrivalDate.Text.Trim, lblPurchaseOrderNumberData.Text)

        Dim dtPOList As New DataTable
        dtPOList = GetTodaysPOItemsList(txtArrivalDate.Text.Trim, lblPurchaseOrderNumberData.Text)

        If dtPOList.Rows.Count > 0 Then

            For Each row As DataRow In dtPOList.Rows

                UpdateItemInventoryByLocationForReceivedPO(row("LocationID"), row("ItemID"), row("ItemQty"), row("PurchaseLineNumber"), txtArrivalDate.Text.Trim)

                Dim dtOrdersList As New DataTable
                dtOrdersList = GetOrderItemsForReceivedPO(row("LocationID"), row("ItemID"), txtArrivalDate.Text.Trim)

                If dtOrdersList.Rows.Count > 0 Then

                    For Each rowItem As DataRow In dtOrdersList.Rows

                        UpateItemInventoryFutureDeliveryForBackOrderItems(row("LocationID"), rowItem("OrderLineNumber"), row("ItemID"), rowItem("BackOrderQty"), rowItem("OrderShipDate"))

                    Next

                End If


            Next

        End If

    End Sub

    Private Function UpdatePOItemsForReceiving(ByVal PODate As String, ByVal PONumber As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdatePOItemsForReceiving]", Connection)
                Command.CommandType = CommandType.StoredProcedure


                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("PONumber", PONumber)
                Command.Parameters.AddWithValue("PODate", PODate)

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Private Function GetTodaysPOItemsList(ByVal PODate As String, ByVal PONumber As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetTodaysPOItemsList]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("PONumber", PONumber)
                Command.Parameters.AddWithValue("PODate", PODate)

                Try

                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return dt

    End Function

    Private Function GetOrderItemsForReceivedPO(ByVal LocationID As String, ByVal ItemID As String, ByVal POReceivedDate As String) As DataTable

        Dim dt As New DataTable

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[GetOrderItemsForReceivedPO]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("POReceivedDate", POReceivedDate)

                Try

                    Dim da As New SqlDataAdapter(Command)
                    da.Fill(dt)

                Catch ex As Exception

                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

        Return dt

    End Function

    Private Function UpdateItemInventoryByLocationForReceivedPO(ByVal LocationID As String, ByVal ItemID As String, ByVal Qty As Integer, ByVal PurchaseLineMumber As String, ByVal PoDate As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpdateItemInventoryByLocationForReceivedPO]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)
                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("QtyOnHand", Qty)
                Command.Parameters.AddWithValue("POReceivedDate", PoDate)
                Command.Parameters.AddWithValue("PurchaseLineNumber", PurchaseLineMumber)

                Try

                    Command.Connection.Open()
                    Command.ExecuteNonQuery()
                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function

    Public Function UpateItemInventoryFutureDeliveryForBackOrderItems(ByVal LocationID As String, ByVal OrderLineNumber As String, _
                                                                      ByVal ItemID As String, ByVal Qty As Int16, ByVal OrderShipDate As String) As Boolean

        Using Connection As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Using Command As New SqlCommand("[enterprise].[UpateItemInventoryFutureDeliveryForBackOrderItems]", Connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", CompanyID)
                Command.Parameters.AddWithValue("DivisionID", DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", DepartmentID)

                Command.Parameters.AddWithValue("LocationID", LocationID)
                Command.Parameters.AddWithValue("ItemID", ItemID)
                Command.Parameters.AddWithValue("Qty", Qty)

                Command.Parameters.AddWithValue("OrderLineNumber", OrderLineNumber)
                Command.Parameters.AddWithValue("OrderShipDate", OrderShipDate)

                Try
                    Command.Connection.Open()
                    Command.ExecuteNonQuery()

                    Return True

                Catch ex As Exception

                    Return False

                Finally

                    Command.Connection.Close()

                End Try

            End Using
        End Using

    End Function

#End Region



    Public Function updatePOReceiveby() As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "update [PurchaseHeader] set [ReceivedBy] = @ReceivedBy, VendorInvoiceNumber =@VendorInvoiceNumber where CompanyID=@f0 and DivisionID =@f1 and DepartmentID=@f2 and [PurchaseNumber] =@PurchaseNumber "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@PurchaseNumber", SqlDbType.NVarChar, 36)).Value = lblPurchaseOrderNumberData.Text
            com.Parameters.Add(New SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 36)).Value = drpReceiveby.SelectedValue
            com.Parameters.Add(New SqlParameter("@VendorInvoiceNumber", SqlDbType.NVarChar, 36)).Value = txtVendorInvoiceNumber.Text

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function

    Public Function UpdatePurcahseHeaderReceivedStatus() As Boolean

        Using connection As New SqlConnection(ConnectionString)
            Using Command As New SqlCommand("[enterprise].[UpdatePurcahseHeaderReceivedStatus]", connection)
                Command.CommandType = CommandType.StoredProcedure

                Command.Parameters.AddWithValue("CompanyID", Me.CompanyID)
                Command.Parameters.AddWithValue("DivisionID", Me.DivisionID)
                Command.Parameters.AddWithValue("DepartmentID", Me.DepartmentID)
                Command.Parameters.AddWithValue("PurchaseNumber", lblPurchaseOrderNumberData.Text.Trim)
                Command.Parameters.AddWithValue("ReceivedDate", Date.Now)
                Command.Parameters.AddWithValue("ReceivingNumber", txtVendorInvoiceNumber.Text)
                Command.Parameters.AddWithValue("PurchaseLineNumber", OrderLineNumber)
                Command.Parameters.AddWithValue("LocationID", drpShiplocation.SelectedValue)
                Command.Parameters.AddWithValue("Result", True)
                Command.Connection.Open()
                Command.ExecuteNonQuery()
                Try

                    Return True
                Catch ex As Exception
                    Return False
                Finally
                    Command.Connection.Close()
                End Try

            End Using
        End Using

    End Function


    Dim OrderLineNumber As String = "'"

    Dim VRPack As String = "'"
    Public VRQty As String = ""
    Public VRTotalQty As String = ""


    Public Function updateItemDetailsCustomisedGrid() As Integer


        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[UpdatePurchaseDetailRecievedQty]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderLineNumber As New SqlParameter("@PurchaseLineNumber", Data.SqlDbType.Int)
        parameterOrderLineNumber.Value = OrderLineNumber
        myCommand.Parameters.Add(parameterOrderLineNumber)

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = lblPurchaseOrderNumberData.Text
        myCommand.Parameters.Add(parameterOrderNumber)

        Dim ReceivedQty As New SqlParameter("@ReceivedQty", Data.SqlDbType.NVarChar)
        ReceivedQty.Value = VRqty
        myCommand.Parameters.Add(ReceivedQty)

        Dim ReceivedPackSize As New SqlParameter("@ReceivedPackSize", Data.SqlDbType.NVarChar)
        ReceivedPackSize.Value = VRPack
        myCommand.Parameters.Add(ReceivedPackSize)

        Dim ReceivedQtyByPackSize As New SqlParameter("@ReceivedQtyByPackSize", Data.SqlDbType.NVarChar)
        ReceivedQtyByPackSize.Value = VRTotalQty
        myCommand.Parameters.Add(ReceivedQtyByPackSize)


        myCon.Open()

        myCommand.ExecuteNonQuery()



        myCon.Close()

        Return 1

    End Function


    Public Function PurchaseNumberPost(ByVal PurchaseNumber As String) As String
        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("[enterprise].[Purchase_Post]", myCon)
        myCommand.CommandType = Data.CommandType.StoredProcedure

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)

        Dim parameterOrderNumber As New SqlParameter("@PurchaseNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = PurchaseNumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim parameterPostingResult As New SqlParameter("@PostingResult", Data.SqlDbType.NVarChar, 200)
        parameterPostingResult.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(parameterPostingResult)
        Dim OutputValue As String = ""
        myCon.Open()

        myCommand.ExecuteNonQuery()

        OutputValue = parameterPostingResult.Value.ToString()

        myCon.Close()
        Return OutputValue

    End Function


    Public Function Check_PurchaseHeader_PurchaseNumber(ByVal PO As String) As String

        Dim PurchaseNumber As String = ""

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  PurchaseNumber  from [PurchaseHeader]   where [PurchaseHeader].PurchaseNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = PO.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    PurchaseNumber = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return PurchaseNumber

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return PurchaseNumber

    End Function


    Protected Sub txtDelivery_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelivery.TextChanged


        CalculationPart()

    End Sub


    Protected Sub CalculationPart()
        Dim SubTotalToDisplay As Decimal = 0

        SubTotalToDisplay = GetPurchaseDetail_Total(lblPurchaseOrderNumberData.Text)

        txtSubtotal.Text = Format(SubTotalToDisplay, "0.00")
        Dim Freight As Decimal

        Dim TaxAmount As Decimal = 0
        Dim TaxPercent As Decimal = 0

        Try
            TaxPercent = txtTaxPercent.Text.Replace("%", "")
        Catch ex As Exception

        End Try

        Try
            TaxAmount = Math.Round(((SubTotalToDisplay * TaxPercent) / 100), 2)

            txtTax.Text = Math.Round((TaxAmount), 2)
        Catch ex As Exception

        End Try

        Freight = txtDelivery.Text

        txtTotal.Text = Math.Round((SubTotalToDisplay + Freight + TaxAmount), 2)


    End Sub

    Public Function GetPurchaseDetail_Total(ByVal PO As String) As Decimal

        Dim Total As Decimal = 0

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "select  SUM(Total)  from [PurchaseDetail]   where [PurchaseDetail].PurchaseNumber =@f31 and CompanyID = @f0 AND DivisionID = @f1 AND DepartmentID = @f2 "
        'Values (@f00,@f0,@f1,@f2,@f3,@f31,@f32,@f311,@f322)
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f31", SqlDbType.NVarChar, 36)).Value = PO.Trim()

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)


            If (dt.Rows.Count <> 0) Then

                Try
                    Total = dt.Rows(0)(0)
                Catch ex As Exception
                    Dim msg As String
                    msg = ex.Message
                    'HttpContext.Current.Response.Write(msg)
                End Try

            End If

            Return Total

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)

        End Try

        Return Total

    End Function

    Protected Sub drpShiplocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpShiplocation.SelectedIndexChanged


        CalculationPart()
        Shiplocation(drpShiplocation.SelectedValue)


    End Sub

    Sub Shiplocation(ByVal LocationID As String)
        Dim obj As New clsOrder_Location
        Dim dtnew As New System.Data.DataTable()
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        obj.LocationID = LocationID

        dtnew = obj.DetailsOrder_Location()

        If dtnew.Rows.Count <> 0 Then

            Try
                txtLCCity.Text = dtnew.Rows(0)("City")
                txtLCCity.Enabled = False
            Catch ex As Exception

            End Try

            ' txtState.Text = dtnew.Rows(0)("State")
            Try
                txtLCZip.Text = dtnew.Rows(0)("ZipCode")
                txtLCZip.Enabled = False

            Catch ex As Exception

            End Try

            'txtCountry.Text = dtnew.Rows(0)("Country")
            Try
                txtLCFax.Text = dtnew.Rows(0)("Fax")
                txtLCFax.Enabled = False

            Catch ex As Exception

            End Try
            Try
                txtLCEmail.Text = dtnew.Rows(0)("Email")
                txtLCEmail.Enabled = False
            Catch ex As Exception

            End Try

            Try
                txtLCPhone.Text = dtnew.Rows(0)("Phone")
                txtLCPhone.Enabled = False
            Catch ex As Exception

            End Try


            Try
                txtLCAddress.Text = dtnew.Rows(0)("Add1")
                txtLCAddress.Enabled = False
            Catch ex As Exception

            End Try



        End If
    End Sub

End Class
