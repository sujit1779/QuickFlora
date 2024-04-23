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

Partial Class PO
    Inherits System.Web.UI.Page

    

    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim PurchaseOrderNumber As String = ""

    Dim Allowed As Boolean = False
    Dim rs As SqlDataReader

    Public Bill_to_Customer_ID As String = "collapse"
    Public Ship_To As String = "collapse"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        rs = PopulateEmployees(CompanyID, DepartmentID, DivisionID, "PO")

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

        txtcustomersearch.Attributes.Add("placeholder", "SEARCH")
        txtcustomersearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")

        txtitemsearch.Attributes.Add("placeholder", "SEARCH")
        txtitemsearch.Attributes.Add("onKeyUp", "SendQuery2(this.value)")

        txtQty.Attributes.Add("onKeyUp", "onblurtxtQty()")
        txtuom.Attributes.Add("onchange", "onblurtxtQty()")

        txtitemsearchprice.Attributes.Add("onKeyUp", "Processuomtotal()")
        txtunits.Attributes.Add("onKeyUp", "Processuomtotal()")
        txtPack.Attributes.Add("onKeyUp", "Processuomwithpacktotal()")
        ' txtitemsearchprice.Attributes.Add("onKeyUp", "Processuomtotal()")
        txtTaxPercent.Attributes.Add("onKeyUp", "Processtotal()")
        txtDelivery.Attributes.Add("onKeyUp", "Processtotal()")


        txtitemTotal.Attributes.Add("onKeyUp", "Processuomtotalchange()")


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
            txtArrivalDate.Text = DateTime.Now.Date.Month & "/" & DateTime.Now.Date.Day & "/" & DateTime.Now.Date.Year
            txtArrivalDate.Attributes.Add("data-date-format", "mm/dd/yyyy")
            txtArrivalDate.Attributes.Add("data-date-viewmode", "years")
            txtArrivalDate.Attributes.Add("data-date", "01/01/2012")

            txtshipdate.Text = DateTime.Now.Date.Month & "/" & DateTime.Now.Date.Day & "/" & DateTime.Now.Date.Year
            txtshipdate.Attributes.Add("data-date-format", "mm/dd/yyyy")
            txtshipdate.Attributes.Add("data-date-viewmode", "years")
            txtshipdate.Attributes.Add("data-date", "01/01/2012")

            txtOrderNumber.Text = lblPurchaseOrderNumberData.Text



            PurchaseOrderNumber = ""
            Try
                PurchaseOrderNumber = Request.QueryString("PurchaseOrderNumber")
            Catch ex As Exception

            End Try

            If CompanyID.ToUpper = "FarmDirect".ToUpper Or CompanyID.ToUpper = "FarmDirectTS".ToUpper Then

                If Not IsNothing(Request.QueryString("PurchaseOrderNumber")) Then
                    Try
                        PurchaseOrderNumber = Request.QueryString("PurchaseOrderNumber")
                        PurchaseOrderNumber = PurchaseOrderNumber.Trim
                    Catch ex As Exception

                    End Try
                End If
                If PurchaseOrderNumber <> "" Then
                    Response.Redirect("POOrder.aspx?PurchaseOrderNumber=" & PurchaseOrderNumber)
                Else
                    Response.Redirect("POOrder.aspx")
                End If


            End If


            If PurchaseOrderNumber <> "" Then
                Label1.Text = Label1.Text & "BindCall"

                Try
                    BindCustomerDetails(PurchaseOrderNumber)


                    BindGrid()
                Catch ex As Exception
                    Label1.Text = Label1.Text & ex.Message


                End Try

                txtOrderNumber.Text = lblPurchaseOrderNumberData.Text
            End If

            If CompanyID.ToUpper() = "JWF" Or CompanyID.ToUpper = "NEWWF" Then
                'Response.Redirect("Home.aspx")
                drpShiplocation.SelectedIndex = drpShiplocation.Items.IndexOf(drpShiplocation.Items.FindByValue("DEFAULT"))
            End If


        End If


    End Sub

    Public Function POHeaderDETAILS(ByVal PurchaseNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM PurchaseHeader Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [PurchaseNumber] ='" & PurchaseNumber & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function


    Sub BindGrid()

        'Label1.Text = "Work"

        If lblPurchaseOrderNumberData.Text.Trim() = "(New)" Then

            Exit Sub
        End If

        Dim PartialReceived As Boolean = False
        Dim dt3 As New DataTable
        dt3 = POHeaderDETAILS(lblPurchaseOrderNumberData.Text)
        If dt3.Rows.Count <> 0 Then
            Try
                PartialReceived = dt3.Rows(0)("PartialReceived")
            Catch ex As Exception
            End Try
        End If





        Dim FillItemDetailGrid As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = GetPurchaseDetail_list(lblPurchaseOrderNumberData.Text)



        Dim tr As String = ""
        Dim n As Integer = 0
        Label1.Text = Label1.Text & ds.Tables(0).Rows.Count.ToString()


        For n = 0 To ds.Tables(0).Rows.Count - 1
            Label1.Text = Label1.Text & n.ToString()

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
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("Color") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("VendorQTY") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("ItemUOM") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("VendorPacksize") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("OrderQty") & "</td>"
            tr = tr & "<td>" & Format(ds.Tables(0).Rows(n)("ItemUnitPrice"), "0.00") & "</td>"
            tr = tr & "<td>" & Format(ds.Tables(0).Rows(n)("Total"), "0.00") & "</td>"
            If PartialReceived Then
                tr = tr & "<td></td><td></td>"
            Else
                tr = tr & "<td><a class='edit btn btn-info btn-block btn-xs' href=''>Edit</a></td><td><a class='delete btn btn-danger btn-block btn-xs' href=''>Delete</a></td>"
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

            Dim ArrivalDate As DateTime

            Try
                ArrivalDate = dt.Rows(0)("ArrivalDate")
            Catch ex As Exception
                ArrivalDate = DateTime.Now.Date
            End Try
            txtArrivalDate.Text = ArrivalDate.Date

            Dim ShippDate As DateTime
            Try
                ShippDate = dt.Rows(0)("OrderShipDate")
            Catch ex As Exception
                ShippDate = DateTime.Now.Date
            End Try
            Try
                txtshipdate.Text = ShippDate.Date
            Catch ex As Exception

            End Try


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
            ' Shiplocation(drpShiplocation.SelectedValue)
            ShiplocationWithPOdt(drpShiplocation.SelectedValue, dt)
            drpPaymentType.SelectedValue = dt.Rows(0)("PaymentMethodID").ToString()
            txtInternalNotes.Text = dt.Rows(0)("InternalNotes")

            chkPosted.Checked = dt.Rows(0)("Posted")

            Dim objtm As New clsCompanyLocalTime
            Dim gmtdt As New DateTime


            Dim posteddate As Date
            Try
                posteddate = dt.Rows(0)("PostedDate")
            Catch ex As Exception

            End Try

            gmtdt = posteddate
            gmtdt = objtm.populateCMPTime(CompanyID, DivisionID, DepartmentID, gmtdt)


            Try
                txtPostedDate.Text = gmtdt.ToShortDateString
                txtPostedTime.Text = gmtdt.ToShortTimeString
            Catch ex As Exception

            End Try


            Try
                chkApproved.Checked = dt.Rows(0)("Approved")
            Catch ex As Exception

            End Try


            Dim Approveddate As Date
            Try
                Approveddate = dt.Rows(0)("ApprovedDate")
            Catch ex As Exception

            End Try


            gmtdt = Approveddate
            gmtdt = objtm.populateCMPTime(CompanyID, DivisionID, DepartmentID, gmtdt)


            Try
                txtApprovedDate.Text = gmtdt.ToShortDateString
                txtApprovedTime.Text = gmtdt.ToShortTimeString
            Catch ex As Exception

            End Try

            Try
                chkReceived.Checked = dt.Rows(0)("Received")
            Catch ex As Exception

            End Try


            Dim Receiveddate As Date
            Try
                Receiveddate = dt.Rows(0)("ReceivedDate")
            Catch ex As Exception

            End Try



            gmtdt = Receiveddate
            gmtdt = objtm.populateCMPTime(CompanyID, DivisionID, DepartmentID, gmtdt)


            Try
                txtReceivedDate.Text = gmtdt.ToShortDateString
                txtReceivedTime.Text = gmtdt.ToShortTimeString
            Catch ex As Exception

            End Try



        End If

    End Sub


    Sub ShiplocationWithPOdt(ByVal LocationID As String, ByVal dt As DataTable)



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
                ' txtState.Text = dtnew.Rows(0)("State")
                txtLCZip.Text = dtnew.Rows(0)("ZipCode")
                'txtCountry.Text = dtnew.Rows(0)("Country")
                txtLCFax.Text = dtnew.Rows(0)("Fax")
                txtLCEmail.Text = dtnew.Rows(0)("Email")


                txtLCPhone.Text = dtnew.Rows(0)("Phone")
            Catch ex As Exception

            End Try


            Try
                txtLCAddress.Text = dtnew.Rows(0)("Add1")
                txtLCAddress.Text = txtLCAddress.Text.Replace("Flower Market ", "")
            Catch ex As Exception

            End Try



        End If

        ',[ShippingAddress1]
        ',[ShippingAddress2]
        ',[ShippingAddress3]
        ',[ShippingCity]
        ',[ShippingState]
        ',[ShippingZip]
        ',[ShippingCountry]
        ',[ShippingEmail]
        ',[ShippingPhone]
        ',[ShippingFax]

        If dt.Rows.Count > 0 Then
            Try
                txtLCCity.Text = dt.Rows(0)("ShippingCity")
            Catch ex As Exception

            End Try

            ' txtState.Text = dtnew.Rows(0)("ShippingState")
            Try
                txtLCstate.Text = dt.Rows(0)("ShippingState")
            Catch ex As Exception

            End Try

            Try
                txtLCZip.Text = dt.Rows(0)("ShippingZip")
            Catch ex As Exception

            End Try

            'txtCountry.Text = dtnew.Rows(0)("Country")
            Try
                txtLCFax.Text = dt.Rows(0)("ShippingFax")
            Catch ex As Exception

            End Try

            Try
                txtLCEmail.Text = dt.Rows(0)("ShippingEmail")
            Catch ex As Exception

            End Try


            Try
                txtLCPhone.Text = dt.Rows(0)("ShippingPhone")
            Catch ex As Exception

            End Try
            Try
                txtLCAddress.Text = dt.Rows(0)("ShippingAddress1")
            Catch ex As Exception

            End Try
            Try
                txtLCAddress2.Text = dt.Rows(0)("ShippingAddress2")
            Catch ex As Exception

            End Try
            Try
                txtLCAddress3.Text = dt.Rows(0)("ShippingAddress3")
            Catch ex As Exception

            End Try

        End If
    End Sub

    Public Function Update_PurchaseOrderNumber(ByVal _PurchaseOrderNumber As String) As Boolean
        '        FROM(PurchaseHeader)
        'WHERE     (PurchaseNumber = '2168')

        ',[ShippingAddress1]
        ',[ShippingAddress2]
        ',[ShippingAddress3]
        ',[ShippingCity]
        ',[ShippingState]
        ',[ShippingZip]
        ',[ShippingCountry]
        ',[ShippingEmail]
        ',[ShippingPhone]
        ',[ShippingFax]

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE PurchaseHeader set ShippingFax=@ShippingFax,ShippingZip=@ShippingZip,ShippingEmail=@ShippingEmail,ShippingPhone=@ShippingPhone,ShippingAddress1=@ShippingAddress1,ShippingAddress2=@ShippingAddress2,ShippingAddress3=@fShippingAddress3 Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And PurchaseNumber=@f5 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = _PurchaseOrderNumber
            com.Parameters.Add(New SqlParameter("@ShippingAddress1", SqlDbType.NVarChar)).Value = txtLCAddress.Text
            com.Parameters.Add(New SqlParameter("@ShippingAddress2", SqlDbType.NVarChar)).Value = txtLCAddress2.Text
            com.Parameters.Add(New SqlParameter("@ShippingAddress3", SqlDbType.NVarChar)).Value = txtLCAddress3.Text
            com.Parameters.Add(New SqlParameter("@ShippingPhone", SqlDbType.NVarChar)).Value = txtLCPhone.Text
            com.Parameters.Add(New SqlParameter("@ShippingEmail", SqlDbType.NVarChar)).Value = txtLCEmail.Text
            com.Parameters.Add(New SqlParameter("@ShippingZip", SqlDbType.NVarChar)).Value = txtLCZip.Text
            com.Parameters.Add(New SqlParameter("@ShippingFax", SqlDbType.NVarChar)).Value = txtLCFax.Text
            'txtLCPhone.Text = dt.Rows(0)("ShippingPhone")
            'txtLCEmail.Text = dt.Rows(0)("ShippingEmail")
            'txtLCZip.Text = dt.Rows(0)("ShippingZip")
            'txtLCCity.Text = dt.Rows(0)("ShippingCity")
            'txtLCFax.Text = dt.Rows(0)("ShippingFax")

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            '' HttpContext.Current.Response.Write(msg)
            Return False
        End Try
    End Function


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


        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) Then
            Dim objsave As New PurchaseModuleUI.PurchaseOrder
            Dim _PurchaseOrderNumber As String = ""
            Try
                _PurchaseOrderNumber = Check_PurchaseHeader_PurchaseNumber(lblPurchaseOrderNumberData.Text.Trim)
            Catch ex As Exception

            End Try
            CalculationPart()

            If _PurchaseOrderNumber = "" Then
                If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                End If
            Else
                If objsave.UpdatePurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    '  Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                End If
            End If
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

        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) Then
            Dim objsave As New PurchaseModuleUI.PurchaseOrder
            Dim _PurchaseOrderNumber As String = ""
            Try
                _PurchaseOrderNumber = Check_PurchaseHeader_PurchaseNumber(lblPurchaseOrderNumberData.Text.Trim)
            Catch ex As Exception

            End Try
            CalculationPart()

            If _PurchaseOrderNumber = "" Then
                If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                End If
            Else
                If objsave.UpdatePurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                End If
            End If
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
            Catch ex As Exception

            End Try
            Try
                txtAttention.Text = dtCST.Rows(0)("Attention").ToString()
            Catch ex As Exception

            End Try
            Try
                txtVendorAddress1.Text = dtCST.Rows(0)("VendorAddress1").ToString()
                'txtVendorAddress1.Text = txtVendorAddress1.Text.Replace("Flower Market ", "")
            Catch ex As Exception

            End Try
            Try
                txtVendorAddress2.Text = dtCST.Rows(0)("VendorAddress2").ToString()
            Catch ex As Exception

            End Try

            ' txtVendorAddress3.Text = dtCST.Rows(0)("VendorAddress3").ToString()
            Try
                txtVendorCity.Text = dtCST.Rows(0)("VendorCity").ToString()
            Catch ex As Exception

            End Try
            Try
                txtVendorFax.Text = dtCST.Rows(0)("VendorFax").ToString()
            Catch ex As Exception

            End Try

            Try
                txtVendorPhone.Text = dtCST.Rows(0)("VendorPhone").ToString()
            Catch ex As Exception

            End Try

            Try
                txtVendorEmail.Text = dtCST.Rows(0)("VendorEmail").ToString()
            Catch ex As Exception

            End Try

            Try
                txtVendorZip.Text = dtCST.Rows(0)("VendorZip").ToString()
            Catch ex As Exception

            End Try

            Try
                txtVendorCountry.Text = dtCST.Rows(0)("VendorCountry").ToString()
            Catch ex As Exception

            End Try

            Try
                txtVendorState.Text = dtCST.Rows(0)("VendorState").ToString()
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

        '  Session("OrderLocationid") = cmblocationid.SelectedValue
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


            Dim locationid As String = "Davie"
            If locationid <> "" Then
                drpShiplocation.SelectedIndex = drpShiplocation.Items.IndexOf(drpShiplocation.Items.FindByValue(locationid))
            End If

            drpShiplocation.Items.Remove("Wholesale")
            Shiplocation(drpShiplocation.SelectedValue)
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

        Dim rs1 As SqlDataReader
        rs1 = PopulatePaymentTypes(CompanyID, DepartmentID, DivisionID)

        drpPaymentType.DataTextField = "PaymentMethodDescription"
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



    Public Function PopulatePaymentTypes(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String) As SqlDataReader
        Dim ConnectionString As String = ""

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString
        Dim sqlStr As String = "SELECT * FROM VendorPaymentMethods WHERE CompanyID='" & CompanyID & "' AND DepartmentID='" & DeptID & "' AND DivisionID='" & DivID & "' AND Active=1  "
        Dim Cmd As New SqlCommand
        Cmd.CommandText = sqlStr
        Cmd.Connection = ConString
        Dim rs As SqlDataReader
        ConString.Open()
        rs = Cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)

        Return rs
    End Function



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
            Dim objsave As New PurchaseModuleUI.PurchaseOrder
            Dim _PurchaseOrderNumber As String = ""
            Try
                _PurchaseOrderNumber = Check_PurchaseHeader_PurchaseNumber(lblPurchaseOrderNumberData.Text.Trim)
            Catch ex As Exception

            End Try
            CalculationPart()

            If _PurchaseOrderNumber = "" Then
                If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OrderShipDate", txtshipdate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "ArrivalDate", txtArrivalDate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "VendorInvoiceNumber", invoiveno.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "MABWNumber", MABWNO.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OcassionCodeID", drpOccasionCode.SelectedValue)
                    'Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                    Dim MVC As String = ""
                    Try
                        MVC = Request.QueryString("MVC")
                    Catch ex As Exception

                    End Try

                    If MVC = "1" Then
                        Dim POURL As String = ""
                        If Me.CompanyID.ToLower() = "mccarthyg".ToLower() Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        If Me.CompanyID = "QuickfloraDemo" Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)

                    Else
                        Response.Redirect("PO.aspx")
                    End If
                End If
            Else
                If objsave.UpdatePurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OrderShipDate", txtshipdate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "ArrivalDate", txtArrivalDate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "VendorInvoiceNumber", invoiveno.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OcassionCodeID", drpOccasionCode.SelectedValue)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "MABWNumber", MABWNO.Text)
                    ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                    Dim MVC As String = ""
                    Try
                        MVC = Request.QueryString("MVC")
                    Catch ex As Exception

                    End Try

                    If MVC = "1" Then
                        Dim POURL As String = ""
                        If Me.CompanyID.ToLower() = "mccarthyg".ToLower() Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        If Me.CompanyID = "QuickfloraDemo" Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)

                    Else
                        Response.Redirect("PO.aspx")
                    End If
                End If
            End If
        End If




    End Sub


    Protected Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) Then
            Dim objsave As New PurchaseModuleUI.PurchaseOrder
            Dim _PurchaseOrderNumber As String = ""
            Try
                _PurchaseOrderNumber = Check_PurchaseHeader_PurchaseNumber(lblPurchaseOrderNumberData.Text.Trim)
            Catch ex As Exception

            End Try
            CalculationPart()

            If _PurchaseOrderNumber = "" Then
                If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    PurchaseNumberPost(lblPurchaseOrderNumberData.Text.Trim)
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    POExportoExcel(lblPurchaseOrderNumberData.Text.Trim)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OrderShipDate", txtshipdate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "ArrivalDate", txtArrivalDate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "VendorInvoiceNumber", invoiveno.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OcassionCodeID", drpOccasionCode.SelectedValue)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "MABWNumber", MABWNO.Text)
                    Dim MVC As String = ""
                    Try
                        MVC = Request.QueryString("MVC")
                    Catch ex As Exception

                    End Try

                    If MVC = "1" Then
                        Dim POURL As String = ""
                        If Me.CompanyID.ToLower() = "mccarthyg".ToLower() Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        If Me.CompanyID = "QuickfloraDemo" Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                            Response.Redirect("http://bpom2.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)
                        Else
                            Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)
                        End If
                    Else
                        Response.Redirect("PO.aspx")
                    End If
                    '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                End If
            Else
                If objsave.UpdatePurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    PurchaseNumberPost(lblPurchaseOrderNumberData.Text.Trim)
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    POExportoExcel(lblPurchaseOrderNumberData.Text.Trim)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OrderShipDate", txtshipdate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "ArrivalDate", txtArrivalDate.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "VendorInvoiceNumber", invoiveno.Text)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "OcassionCodeID", drpOccasionCode.SelectedValue)
                    Update_PurchaseHeader(lblPurchaseOrderNumberData.Text.Trim, "MABWNumber", MABWNO.Text)
                    Dim MVC As String = ""
                    Try
                        MVC = Request.QueryString("MVC")
                    Catch ex As Exception

                    End Try

                    If MVC = "1" Then
                        Dim POURL As String = ""
                        If Me.CompanyID.ToLower() = "mccarthyg".ToLower() Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        If Me.CompanyID = "QuickfloraDemo" Then
                            POURL = "https://secureapps.quickflora.com/POM/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID
                            Response.Redirect(POURL)
                        End If
                        ' Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)
                        If Me.CompanyID = "DierbergsMarkets,Inc63017" Or Me.CompanyID = "QuickfloraDemo" Then
                            Response.Redirect("http://bpom2.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)
                        Else
                            Response.Redirect("http://bpom.quickflora.com/Web/BPO.aspx?CompanyID=" & CompanyID & "&DivisionID=" & DivisionID & "&DepartmentID=" & DepartmentID & "&TerminalID=" & "DEFAULT" & "&locationid=" & Session("Locationid") & "&EmployeeID=" & EmployeeID & "&Session_SessionID=" & Session.SessionID)
                        End If
                    Else
                        Response.Redirect("PO.aspx")
                    End If
                End If
            End If



        End If
      


    End Sub

    Public Function PODETAILSEmail(ByVal PurchaseNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim LocationName As String = ""
        Dim dt As New DataTable

        'From [Enterprise].[dbo].[PurchaseDetail]
        'Order By [PurchaseLineNumber] DESC

        ssql = ssql & " SELECT  *  "
        ssql = ssql & " FROM [POExportoExcel] Where CompanyID ='" & Me.CompanyID & "' AND DivisionID ='" & Me.DivisionID & "'  AND DepartmentID ='" & Me.DepartmentID & "' "
        ssql = ssql & " AND [PONumber] ='" & PurchaseNumber & "'   "
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        da.SelectCommand = com
        da.Fill(dt)


        Return dt
    End Function

    Public Function POExportoExcel(ByVal PONumber As String) As Boolean
        Dim objemail As New clsemailtovendor
        'objemail.CompanyID = Me.CompanyID
        'objemail.DivisionID = Me.DivisionID
        'objemail.DepartmentID = Me.DepartmentID
        ''  objemail.ConnectionString = Me.ConnectionString
        'objemail.EmailNotifications(PONumber)

        'objemail.txtfrom.Text = "support@quickflora.com"

        'EmailSendingWithhCC(objemail.txtemailsubject.Text, objemail.divEmailContent.Text, objemail.txtfrom.Text, objemail.txtto.Text, objemail.txtcc.Text, objemail._vendorid)
        If Me.CompanyID = "FarmDirect" Or Me.CompanyID = "FarmDirectTS" Then
            Exit Function
        End If

        Dim ds As New DataTable
        ds = PODETAILSEmail(PONumber)
        If ds.Rows.Count > 0 Then
        Else
            Dim connec As New SqlConnection(ConnectionString)
            Dim qry As String
            qry = "insert into POExportoExcel( CompanyID, DivisionID, DepartmentID, [PONumber],[Done] ) " _
             & " values(@f0,@f1,@f2,@PONumber,1)"

            Dim com As SqlCommand
            com = New SqlCommand(qry, connec)
            Try

                com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
                com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
                com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
                com.Parameters.Add(New SqlParameter("@PONumber", SqlDbType.NVarChar, 255)).Value = PONumber

                com.Connection.Open()
                com.ExecuteNonQuery()
                com.Connection.Close()

                Return True

            Catch ex As Exception
                Dim msg As String
                msg = ex.Message
                '' HttpContext.Current.Response.Write(msg)
                Return False

            End Try

        End If

        Return True
    End Function



    Public Function Update_PurchaseHeader(ByVal PurchaseNumber As String, ByVal name As String, ByVal value As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        Dim com As SqlCommand


        qry = "Update PurchaseHeader SET  " & name & " =@value Where [PurchaseNumber] = @PurchaseNumber  AND CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID  "
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.AddWithValue("@value", value)
            com.Parameters.AddWithValue("@PurchaseNumber", PurchaseNumber)
            com.Parameters.AddWithValue("@CompanyID", Me.CompanyID)
            com.Parameters.AddWithValue("@DivisionID", Me.DivisionID)
            com.Parameters.AddWithValue("@DepartmentID", Me.DepartmentID)

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


    Protected Sub txtDelivery_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelivery.TextChanged, txtTaxPercent.TextChanged

        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) Then
            Dim objsave As New PurchaseModuleUI.PurchaseOrder
            Dim _PurchaseOrderNumber As String = ""
            Try
                _PurchaseOrderNumber = Check_PurchaseHeader_PurchaseNumber(lblPurchaseOrderNumberData.Text.Trim)
            Catch ex As Exception

            End Try
            CalculationPart()

            If _PurchaseOrderNumber = "" Then
                If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    ' PurchaseNumberPost(lblPurchaseOrderNumberData.Text.Trim)
                    ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                End If
            Else
                If objsave.UpdatePurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                    Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                    ' PurchaseNumberPost(lblPurchaseOrderNumberData.Text.Trim)
                    ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                End If
            End If



        End If

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



        Shiplocation(drpShiplocation.SelectedValue)
        If IsNumeric(lblPurchaseOrderNumberData.Text.Trim) Then
            Dim objsave As New PurchaseModuleUI.PurchaseOrder
            Dim _PurchaseOrderNumber As String = ""
            Try
                _PurchaseOrderNumber = Check_PurchaseHeader_PurchaseNumber(lblPurchaseOrderNumberData.Text.Trim)
            Catch ex As Exception

            End Try
            CalculationPart()
            Try
                If _PurchaseOrderNumber = "" Then

                    If objsave.AddPurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                        Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                        ' PurchaseNumberPost(lblPurchaseOrderNumberData.Text.Trim)
                        ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                    End If
                Else
                    If objsave.UpdatePurchseHeader(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblPurchaseOrderNumberData.Text.Trim, 0, drpPurchaseTransaction.SelectedValue, lblPurchaseOrderDate.Text, txtArrivalDate.Text, drpEmployeeID.SelectedValue, "", "DEFAULT", txtVendorTemp.Text, "USD", 1, txtSubtotal.Text, 0, 0, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtSubtotal.Text, txtDelivery.Text, 0, txtTotal.Text, drpShipMethod.SelectedValue, chkPosted.Checked, Date.Now, cmblocationid.SelectedValue, drpShiplocation.SelectedValue, txtInternalNotes.Text, drpPaymentType.SelectedValue, txtTrackingno.Text, txtOrderno.Text) Then
                        Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
                        'PurchaseNumberPost(lblPurchaseOrderNumberData.Text.Trim)
                        ' Response.Redirect("PO.aspx") '?PO=" & lblPurchaseOrderNumberData.Text.Trim)
                    End If
                End If
            Catch ex As Exception

            End Try

            Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)


        End If

    End Sub

    Sub Shiplocation(ByVal LocationID As String)
        Dim obj As New clsOrder_Location
        Dim dtnew As New System.Data.DataTable()
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID
        obj.LocationID = LocationID

        dtnew = obj.DetailsOrder_Location()
        'lblpo.Text = lblpo.Text & LocationID
        'lblpo.Text = lblpo.Text & dtnew.Rows.Count
        If dtnew.Rows.Count <> 0 Then


            txtLCCity.Text = dtnew.Rows(0)("City")
            ' txtState.Text = dtnew.Rows(0)("State")
            txtLCZip.Text = dtnew.Rows(0)("ZipCode")
            'txtCountry.Text = dtnew.Rows(0)("Country")
            txtLCFax.Text = dtnew.Rows(0)("Fax")
            txtLCEmail.Text = dtnew.Rows(0)("Email")

            Try
                txtLCPhone.Text = dtnew.Rows(0)("Phone")
            Catch ex As Exception

            End Try


            Try
                txtLCAddress.Text = dtnew.Rows(0)("Add1")
                txtLCAddress.Text = txtLCAddress.Text.Replace("Flower Market ", "")
            Catch ex As Exception

            End Try



        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Update_PurchaseOrderNumber(lblPurchaseOrderNumberData.Text.Trim)
    End Sub

    Private Sub PO_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

    End Sub
End Class
