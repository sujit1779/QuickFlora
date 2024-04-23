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


Partial Class POSOE
    Inherits System.Web.UI.Page

     

    Dim objclsPaymentGatewayTransactionLogs As New clsPaymentGatewayTransactionLogs

    Public Opt_Employee As Integer
    Public Opt_Priority As Integer      'Added by GG
    Public Opt_Source As Integer
    Public Opt_Occasion As Integer
    Dim objZone As New clsAddressZone
    Dim TypedSearchOntextChange As Boolean = False
    Dim TypedCitySerch As Boolean = False
    Dim ZoneChange As Boolean
    Dim PostalCodeDropDown As Boolean

#Region "Public Variables"
    Public addonQty As String = ""
    Public addonName As String = ""
    Public addonPrice As String = ""
    Public addonTotalPrice As Double = 0.0
    Public addonTotalstr As String = ""
    Public heading As String = ""
    Public Orderbind As Boolean = False

    Public ShowTodaysDate As String

    Public ONumber As String = ""
    Dim IPrice As Decimal


    Dim vendorID As String = ""
    Dim WireService As String = ""
    Dim WireOutCode As String = ""
    Dim WireOutOwner As String = ""
    Dim WireOutStatus As String = ""
    Dim WireOutNotes As String = ""
    Dim WireOutTransID As String = ""
    Dim WireOutTransMethod As String = ""
    Dim WireOutPriority As String = ""

    Dim VendorName As String = ""
    Dim VendorAddress As String = ""
    Dim VendorCity As String = ""
    Dim VendorCountry As String = ""
    Dim VendorZip As String = ""
    Dim VendorState As String = ""
    Dim VendorPhone As String = ""
    Dim VendorEmail As String = ""
    Dim VendorWebPage As String = ""
    Dim VendorFax As String = ""



    Dim TerminalID As String = ""
    Dim ShiftID As Integer = 0


    Public ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim Allowed As Boolean = False
    Dim rs As SqlDataReader
    Dim Cid As String = ""

    Public RetailerAddress As String
    Public RetailerCity As String
    Public RetailerState As String
    Public RetailerZip As String
    Public OrdNumber As String
    Public OrderNumberEdit As String
    Public ItemIDTemp As Integer = 0
    Public ItemIDFromTextBox As String
    Public Quantity As Decimal
    Public MacroValues As String = ""
    Public MacroCodes As String = ""
    Public SystemWMessage As String = ""
    Public Shared dsAdd As New Data.DataSet
    Public Shared CustIDTempCount As Integer = 0
    Public Delivery As String = ""

    Public DeliveryValue As String = ""

    Public service As Decimal
    Public relay As Decimal

    Public SalesTax As Decimal = 0.0

    Public Fkey As String = ""
    Public Action As String = ""

    Public AfterDelete As String = ""
    Public GridCount As Integer

    Public OID As String = ""

    Public result As String
    Public ItemPrice As String = ""


    Dim GTReader As SqlDataReader

#End Region

#Region "Existing Function of Order Entry form"
    Public Sub SessionClearDataInStart()
        Session("Oln") = Nothing
        Session("Resultvalue") = Nothing
        Session("PO") = Nothing
        Session("EditButton") = Nothing
        Session("TempOrderNumber") = Nothing
        Session("CustomerID") = Nothing
        Session("CntrlFrmdrpCountry") = Nothing
        Session("OrderNumberToItemSearch") = Nothing
        Session("OrderNumber") = Nothing
        Session("CustIDCreatedFrmGrid") = Nothing
        Session("CustIDNotFound") = Nothing
        Session("CustIDCreatedFrmItemSearch") = Nothing
        Session("SpecifyCustomer") = Nothing
        Session("CheckLinkCliked") = Nothing
        Session("DataPostedFrmItemSearch") = Nothing
        Session("CtrlFrmItemNotFound") = Nothing
        Session("CustomerIDFromItemSearch") = Nothing
        Session("NewCustomer") = Nothing
        Session("CntlFromPostBackTrue") = Nothing
        Session("CntrlFromItemSearch") = Nothing

        Session("CntlFromPostBackTrue") = Nothing
        Session("CtrlFrmItemNotFound") = Nothing

        Session("PO") = Nothing
        Session("Grid") = Nothing

        Session("PaymentType") = Nothing
        Session("CustomerType") = Nothing
        Session("OrdNumber") = Nothing
        Session("CustomerID") = Nothing
        Session("ShipMethod") = Nothing
        Session("AddEdit") = Nothing
        Session("OrderNumberToItemSearch") = Nothing
        Session("TempOrderNumber") = Nothing
        Session("DataSetCount") = Nothing
        Session("EditButton") = Nothing
        Session("OrderNumberToItemSearch") = Nothing
        Session("MessageID") = Nothing
        Session("TempOrderNumber") = Nothing
        Session("CustIDCreatedFrmGrid") = Nothing
        Session("EditButton") = Nothing
        Session("CustIDNotFound") = Nothing
        Session("CustIDCreatedFrmGrid") = Nothing
        Session("NewCustomer") = Nothing
        Session("RetailCustomer") = Nothing
        Session("SpecifyCustomer") = Nothing
        Session("TempOrderNumber") = Nothing
        Session("CheckLinkCliked") = Nothing
        Session("CustIDCreatedFrmItemSearch") = Nothing
        Session("DataPostedFrmItemSearch") = Nothing
        Session("DataFromAddOn") = Nothing
        Session("CntrlFrmdrpCountry") = Nothing

    End Sub

#End Region


    Public Sub BindDefaultOrderEntryPrompts()
        Dim ObjCustomer As New DAL.CustomOrder()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = CStr(SessionKey("CompanyID"))
        DivisionID = CStr(SessionKey("DivisionID"))
        DepartmentID = CStr(SessionKey("DepartmentID"))

        Dim dt As New DataTable

        dt = PopulateOrderEntryPrompts()


        txtCardMessageDesc.Attributes.Add("onChange", "Proper(this); return true;")


        If dt.Rows.Count <> 0 Then
            Try
                If dt.Rows(0)("customerfirstname").ToString = True Then
                    txtCustomerFirstName.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")




                End If
            Catch ex As Exception

            End Try
            Try
                If dt.Rows(0)("customerlastname").ToString = True Then
                    txtCustomerLastName.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
                End If
            Catch ex As Exception

            End Try
            Try
                If dt.Rows(0)("customercompany").ToString = True Then
                    txtCompany.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
                End If
            Catch ex As Exception

            End Try

            Try
                If dt.Rows(0)("customerphone").ToString = True Then
                    txtCustomerPhone.Attributes.Add("onkeydown", "javascript:backspacerDOWN(this,event);")
                    txtCustomerPhone.Attributes.Add("onkeyup", "javascript:backspacerUP(this,event);")

                    txtCustomerCell.Attributes.Add("onkeydown", "javascript:backspacerDOWN(this,event);")
                    txtCustomerCell.Attributes.Add("onkeyup", "javascript:backspacerUP(this,event);")

                    txtCustomerFax.Attributes.Add("onkeydown", "javascript:backspacerDOWN(this,event);")
                    txtCustomerFax.Attributes.Add("onkeyup", "javascript:backspacerUP(this,event);")


                End If
            Catch ex As Exception

            End Try

            Try
                If dt.Rows(0)("customercity").ToString = True Then
                    txtCustomerCity.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
                End If

            Catch ex As Exception

            End Try

            Try
                If dt.Rows(0)("shippingfirstname").ToString = True Then
                    txtShippingName.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
                End If
            Catch ex As Exception

            End Try

            Try
                If dt.Rows(0)("shippinglastname").ToString = True Then
                    txtShippingLastName.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
                End If
            Catch ex As Exception

            End Try

            Try
                If dt.Rows(0)("shippingcompany").ToString = True Then
                    txtShipCompany.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
                End If
            Catch ex As Exception

            End Try

            Try
                If dt.Rows(0)("shippingcity").ToString = True Then
                    txtShippingCity.Attributes.Add("onKeyUp", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
                End If
            Catch ex As Exception

            End Try

            Try
                If dt.Rows(0)("shippingphone").ToString = True Then
                    txtShipCustomerPhone.Attributes.Add("onkeydown", "javascript:backspacerDOWN(this,event);")
                    txtShipCustomerPhone.Attributes.Add("onkeyup", "javascript:backspacerUP(this,event);")

                    txtShipCustomerCell.Attributes.Add("onkeydown", "javascript:backspacerDOWN(this,event);")
                    txtShipCustomerCell.Attributes.Add("onkeyup", "javascript:backspacerUP(this,event);")


                    txtShipCustomerFax.Attributes.Add("onkeydown", "javascript:backspacerDOWN(this,event);")
                    txtShipCustomerFax.Attributes.Add("onkeyup", "javascript:backspacerUP(this,event);")

                End If
            Catch ex As Exception

            End Try


        End If


    End Sub



    Public Sub populatedefaultsettings()
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""

        ssql = "Select allowtoeditcreditlimit,defaultitemid,defaultcreditlimit from  OrderEntryPrompts where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        Dim dt As New DataTable
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
 
        End Try

        Dim chkcreditlimit As Boolean = False
        Dim txtitemid As String = ""
        Dim defaultcreditlimit As Double = 0

        If dt.Rows.Count <> 0 Then
            Try
                chkcreditlimit = dt.Rows(0)("allowtoeditcreditlimit").ToString
            Catch ex As Exception

            End Try

            Try
                defaultcreditlimit = dt.Rows(0)("defaultcreditlimit").ToString
            Catch ex As Exception

            End Try

            Try
                txtitemid = dt.Rows(0)("defaultitemid").ToString
            Catch ex As Exception

            End Try

            If chkcreditlimit Then
                txtCreditLimit.Enabled = True
                txtCreditLimit.ReadOnly = False
                txtCreditLimit.Text = Format(defaultcreditlimit, "0.00")
            Else
                txtCreditLimit.Enabled = False
                txtCreditLimit.ReadOnly = True
            End If

            If txtitemid <> "" Then
                Dim ItemID As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyItem"), TextBox)
                ItemID.Text = txtitemid

                'BindEmptyGrid()
            End If

        End If

    End Sub


    Public publicCVSCHECK As String = "true"

    Public Sub chkcsv()
        'CompanyID, DepartmentID, DivisionID
        Dim PMG As String = "POS"
        If drpOrderTypeIDData.SelectedValue = "Order" Then
            PMG = "Order"
        End If
        If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
            PMG = "CartOrder"
        End If
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        Dim CVSCHECK As Boolean = True

        If dt.Rows.Count <> 0 Then



            Try
                CVSCHECK = CType(dt.Rows(0)("CVV"), Boolean)
            Catch ex As Exception
                CVSCHECK = True
            End Try


        End If

        If CVSCHECK Then
            publicCVSCHECK = "true"
        Else
            publicCVSCHECK = "false"
        End If

    End Sub

    Public Bill_to_Customer_ID As String = "expand"
    Public Ship_To As String = "expand"

    'Public Ship_To As Boolean = False
    Protected Sub chkDeliveryOverride_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDeliveryOverride.CheckedChanged

        If chkDeliveryOverride.Checked Then
            txtDelivery.Enabled = True
        Else
            txtDelivery.Enabled = False
        End If

    End Sub

    Protected Sub chkRelayOverride_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRelayOverride.CheckedChanged
        If chkRelayOverride.Checked Then
            txtRelay.Enabled = True
        Else
            txtRelay.Enabled = False
        End If
    End Sub

    Protected Sub chkServiceOverride_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkServiceOverride.CheckedChanged
        If chkServiceOverride.Checked Then
            txtService.Enabled = True
        Else
            txtService.Enabled = False
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If IsPostBack Then

        '    Dim CtrlID As String = String.Empty
        '    If Request.Form("__EVENTTARGET") IsNot Nothing And _
        '     Request.Form("__EVENTTARGET") <> String.Empty Then
        '        CtrlID = Request.Form("__EVENTTARGET")
        '        CtrlID = CtrlID.Replace(":", "_")

        '        '     ClientScript.RegisterStartupScript(Me.GetType(), _
        '        '"sourceofpostback", _
        '        '"<script type='text/javascript'>" _
        '        '& "window.onload=new function(){" _
        '        '& "alert('Control ID " & CtrlID & " --" & txtDelivery.ClientID _
        '        '& " caused postback.');}" _
        '        '& "</script>")

        '    End If

        '    If CtrlID = txtDelivery.ClientID Then
        '        chkDeliveryOverride.Checked = True
        '    End If

        'End If

        If Not IsPostBack Then
            chkDeliveryOverride.ToolTip = "Select to Override Deliver"
            chkRelayOverride.ToolTip = "Select to Override Service"
            chkServiceOverride.ToolTip = "Select to Override Relay"

            txtDelivery.Enabled = False
            txtRelay.Enabled = False
            txtService.Enabled = False
            Session("CustomerTypeID") = ""
            'Session("OrderLocationid") = ""
        End If

        Me.Form.Attributes.Add("autocomplete", "off")

        If Session("CompanyID") Is Nothing Then
            Response.Redirect("loginform.aspx")
        End If

        If Session("ShiftID") Is Nothing Then
            Response.Redirect("StartShift.aspx")
        End If

        chkstoreCC.Attributes.Add("onclick", "javascript:drpstoredcc_Change();")
		drpstoredcc.Attributes.Add("onchange", "javascript:drpstoredcc_Change();")
        drpstoredcc.Attributes.Add("onclick", "javascript:drpstoredcc_Change();")

        txtcustomersearch.Attributes.Add("placeholder", "SEARCH")
        txtitemsearch.Attributes.Add("placeholder", "SEARCH")

        lblrefersh.Text = Convert.ToDateTime(Date.Now).ToShortTimeString

        If Not IsPostBack Then
            '	// Ajax refresh every 10 seconds
            ' MagicAjax.AjaxCallHelper.SetAjaxCallTimerInterval(30000)
            Session("OrderNumber") = ""
            Session("CustomerIDNew") = ""
        End If


        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "Demo Site-90210" Or CompanyID = "Greene and Greene" Or CompanyID = "PoppiesV8W1L8" Then


            drpShippingState.AutoPostBack = True

        End If

        ItemSearch.Attributes.Add("onclick", "Javascript:window.open('ItemsSearch.aspx','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=650,height=400,Left=350,top=275,screenX=0,screenY=275,alwaysRaised=yes');return false; ")
        lnkCustomerSearch.Attributes.Add("onclick", "Javascript:window.open('orderCustomerSearch.aspx','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=700,height=300,Left=350,top=275,screenX=0,screenY=275,alwaysRaised=yes');return false; ")

        'drpPaymentType.Attributes.Add("onchange", "Javascript:initInputHighlightScript();")

        btnTransmit.Attributes.Add("onclick", "javascript:jumptodiv();")
        drpShipMethod.Attributes.Add("onchange", "javascript:drpShipMethod_Change();")
        pnlPricerange.Visible = False
        PopulateFkeys()
        'Page.Form.Attributes.Add("onkeydown", "javascript: CallFkey()")
        lblPayPalType.Visible = False
        lblPaypalmethod.Visible = False




        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

		
        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "Demo Site-90210" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
            txtTaxPercentGST.Visible = True
            txtTaxPercentPST.Visible = True
            txtTaxPST.Visible = True
            txtTaxGST.Visible = True
            drpTaxesPST.Visible = True
            drpTaxesGSTHST.Visible = True

            drpTaxes.Visible = False
            txtTax.Visible = False
            txtTaxPercent.Visible = False

        Else

            txtTaxPercentGST.Visible = False
            txtTaxPercentPST.Visible = False
            txtTaxPST.Visible = False
            txtTaxGST.Visible = False
            drpTaxesPST.Visible = False
            drpTaxesGSTHST.Visible = False

            drpTaxes.Visible = True
            txtTax.Visible = True
            txtTaxPercent.Visible = True


        End If
		
		
        chkcsv()

        objclsPaymentGatewayTransactionLogs.CompanyID = CompanyID
        objclsPaymentGatewayTransactionLogs.DivisionID = DivisionID
        objclsPaymentGatewayTransactionLogs.DepartmentID = DepartmentID
        PopulateMacros()
        'AutoCompleteExtender1.ContextKey = CompanyID + "!" + DivisionID + "*" + DepartmentID + "~" + "ItemID"
        'AutoCompleteExtender2.ContextKey = CompanyID + "!" + DivisionID + "*" + DepartmentID + "~" + "CustomerID"

        txtcustomersearch.Attributes.Add("onKeyUp", "SendQuery(this.value)")
        txtitemsearch.Attributes.Add("onKeyUp", "SendQuery2(this.value)")

        PostalCodeDropDown = objZone.CheckPostalCodeDropDownFeature(CompanyID, DivisionID, DepartmentID)
        If PostalCodeDropDown And objZone.CheckAddressZoneVerification(CompanyID, DivisionID, DepartmentID) Then
            txtShippingZip.Attributes.Add("onKeyUp", "SendQuery3(this.value)")
            txtShippingZip.AutoPostBack = False
        End If

        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "BrownstheFloristLtdV8W1G9" Or CompanyID = "Greene and Greene" Or CompanyID = "TrainingBrownsTheFlorist" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "Quickflora-CanadaTest" Then
            ''advFeature.Visible = True
            cmblocationid.AutoPostBack = True
            HlkFindZip.NavigateUrl = "http://www.canadapost.ca/cpotools/apps/fpc/personal/findByCity?execution=e1s1"
            HlkFindZip.ToolTip = "Click to look up zip code on Canadapost website"
        ElseIf CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-USATest" Then
            cmblocationid.AutoPostBack = True
            HlkFindZip.NavigateUrl = "http://zip4.usps.com/zip4/welcome.jsp"
            HlkFindZip.ToolTip = "Click to look up zip code on USPS website"
        Else
            cmblocationid.AutoPostBack = True
            HlkFindZip.NavigateUrl = "http://zip4.usps.com/zip4/welcome.jsp"
            HlkFindZip.ToolTip = "Click to look up zip code on USPS website"
        End If

        If Not Page.IsPostBack Then

            If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
                drpNewsletterID.Items.Clear()
                Dim lst As New ListItem
                lst.Text = "Select One"
                lst.Value = ""
                drpNewsletterID.Items.Add(lst)
                drpNewsletterID.Items.Add("Yes")
                drpNewsletterID.Items.Add("No")

            End If


            ''GetZoneID(CompanyID, DivisionID, DepartmentID)
            Session("SupressEvent") = False

            If CompanyID = "The Garden Gate-92886" Then
                Try
                    drpCustomerID.SelectedIndex = 1
                Catch ex As Exception

                End Try
            End If

            BindDefaultOrderEntryPrompts()

            lblOrderNumberData.Text = "(New)"

            txtSubtotal.Text = "0.00"
            txtTotal.Text = "0.00"
            txtDelivery.Text = "0.00"
            txtService.Text = "0.00"
            txtRelay.Text = "0.00"
            txtDiscountAmount.Text = "0.00"


            drpOrderTypeIDData.Focus()
            SessionClearDataInStart()
            PopulateDeliveryDateandMethodDisplay()
            populateCustomerTypes()
            BtnEdit.Visible = False
            PopulateCardExpDate()
            PopulateCardExpDate()
            PopulateDrops()
            lblOrderDate.Text = DateTime.Now.ToShortDateString()
            txtCustomerFirstName.Attributes.Add("onblur", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
            txtCustomerLastName.Attributes.Add("onblur", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
            txtCustomerCity.Attributes.Add("onblur", "this.value=this.value.replace(this.value.substr(0,1),this.value.substr(0,1).toUpperCase());")
            txtInternal.Visible = False

            txtCardMessageDesc.Attributes.Add("onkeyup", "javascript:updateCount(this) ")
            txtCardMessageDesc.Attributes.Add("onChange", "javascript:updateCount(this) ")
            'txtDeliveryDate.Attributes.Add("onChange", "javascript: popdate1()")
            txtDriverRouteInfo.Attributes.Add("onkeyup", "javascript:updateCountdriverinfo(this) ")
            txtDriverRouteInfo.Attributes.Add("onChange", "javascript:updateCountdriverinfo(this) ")
            lnkDeliveryLocations.Attributes.Add("onclick", "Javascript:window.open('DeliveryLocations.aspx','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=900,height=400');return false; ")


            lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")
            lnkMacro.Attributes.Add("onclick", "Javascript:window.open('MacrosList.aspx?OccasionCode=" + drpOccasionCode.SelectedValue + "','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")
            lnkSuggestions.Attributes.Add("onclick", "Javascript:window.open('MessageSuggestions.aspx','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');")
            txtCardMessageDesc.Attributes.Add("onkeydown", "javascript: callme()")
            'txtDeliveryDate.Attributes.Add("onTextChanged", "javascript: popdate1()")
            'txtDeliveryDate.Attributes.Add("onBlur", "javascript: popdate1()")
            LinkButton1.Attributes.Add("onclick", "javascript: checkSpelling()")
            GetCompanyAddress()

            If Not IsNothing(Request.QueryString("OrderNumber")) Then
                OrderNumberEdit = Request.QueryString("OrderNumber")
                lblOrderNumberData.Text = OrderNumberEdit

                BindCustomerDetails(CompanyID, DepartmentID, DivisionID, OrderNumberEdit)
                BindGrid()
                lnkChangeHistory.Attributes.Add("onclick", "Javascript:window.open('ChangeHistory.aspx?CustomerID=" + OrderNumberEdit + "','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400,Left=350,top=275,screenX=0,screenY=275 ');return false; ")
                lnkChangeDelivery.Attributes.Add("onclick", "Javascript:window.open('DeliveryTrace.aspx?CustomerID=" + OrderNumberEdit + "','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400,Left=350,top=275,screenX=0,screenY=275 ');return false; ")
                lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")

            Else
                populatedefaultsettings()

            End If


        End If

 

        If txtCustomerFirstName.Text.Trim() = "" And txtCustomerLastName.Text.Trim() = "" And txtCustomerAddress1.Text.Trim() = "" And txtCustomerTemp.Text = "" Then
            lblsearchcustomermsg.Text = "Please search by any of the following for customer's Id, Name, Phone, Address, Zip Code, City, State, Company and Email.<br> After search if no list populates then please try some other values.(After Search click on green check button to get customer details)"
            lblsearchcustomermsg.ForeColor = Drawing.Color.Blue
        Else
            If txtcustomersearch.Text.Trim <> "" Then
                lblsearchcustomermsg.Text = ""
            End If
        End If

        If drpPaymentType.SelectedValue.Trim = "Wire In" Then
            lblsearchcustomermsg.Text = ""
        End If

        If txtSubtotal.Text.Trim = "0.00" Then
            If txtitemsearch.Text.Trim = "" Then
                lblitemsearchmsg.Text = "Please search by any of the following for Item's Id, Name, Description. After search if no list populates then please try some other values.(After Search click on green check button to get Item details)"
                lblitemsearchmsg.ForeColor = Drawing.Color.Blue
                'txtcustomersearch.Attributes.Add("", "")
            End If
        End If

        lblsearchcustomermsg.Visible = False
        txtcustomersearch.Attributes.Add("onblur", "CcustomersearchBlurProcess()")

        lblitemsearchmsg.Visible = False
        txtitemsearch.Attributes.Add("onblur", "itemsearchBlurProcess()")

        'txtShippingZip.Attributes.Add("onblur", "postalcodesearchBlurProcess()")
        ''
        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "itemrsearchcloseProcess();CcustomersearchcloseProcess();postalcodesearchcloseProcess();" & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 

        If Not IsPostBack Then
            If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "Busseys,Inc.30125" Or CompanyID = "LaneFlorist75205" Or CompanyID = "UniquleyChicFlorist93312" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "Fleurish"     Or CompanyID = "FlourishNY,LLC111249" Then
                Dim r As New Random
                'ClientScript.RegisterStartupScript(Me.GetType(), "loadfn" & r.Next(100001, 200001).ToString(), "clo_fun();clo_fun1()", True)
            Else
                'clo_All
                Dim r As New Random
                'ClientScript.RegisterStartupScript(Me.GetType(), "loadfn" & r.Next(100001, 200001).ToString(), "clo_All();", True)
            End If


        End If

        txtDeliveryDate.Attributes.Add("data-date-format", "mm-dd-yyyy")
        txtDeliveryDate.Attributes.Add("data-date-viewmode", "years")
        txtDeliveryDate.Attributes.Add("data-date", txtDeliveryDate.Text)


       


    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete



        If IsNumeric(lblOrderNumberData.Text.Trim) And IsPostBack Then
            Dim n As Integer = 0

            Try
                n = lblOrderNumberData.Text.Trim
            Catch ex As Exception
                Exit Sub
            End Try

            If n <> 0 Then

                If IsPostBack Then
                    BindGrid()
                    ShippingDeliveryCharge(txtShippingZip.Text)
                    PopulatingTaxPercent()
                End If

                AddEditItemDetails()
                AddCardMessages(n)
                ''Update Order Location Method''
                Dim objrderloaction As New clsOrder_Location
                objrderloaction.CompanyID = CompanyID
                objrderloaction.DivisionID = DivisionID
                objrderloaction.DepartmentID = DepartmentID
                objrderloaction.OrderNumber = n
                objrderloaction.LocationID = cmblocationid.SelectedValue
                objrderloaction.UpdateOrderLocationID()

            End If
        End If

        trTax.Visible = drpTaxes.Visible
        txtOrderNumber.Text = lblOrderNumberData.Text

        If drpTaxes.Visible Then
            trGSTTax.Visible = False
            trPSTTax.Visible = False
        End If

       

    End Sub

#Region "GetCompanyAddress"
    Sub GetCompanyAddress()
        ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim CommandText As String = "enterprise.spCompanyInformation"

        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)

        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        myCommand.CommandTimeout = ConfigSettings.SqlCommandTimeout
        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myConnection.Open()
        Dim myReader As SqlDataReader
        myReader = myCommand.ExecuteReader()
        While (myReader.Read())
            '.Text = myReader("CompanyAddress1").ToString()
            RetailerAddress = myReader("CompanyAddress1").ToString()
            RetailerCity = myReader("CompanyCity").ToString()
            RetailerState = myReader("CompanyState").ToString()
            RetailerZip = myReader("CompanyZip").ToString()
        End While
        myConnection.Close()

    End Sub
#End Region

#Region "PopulateFkeys"
    Public Sub PopulateFkeys()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")


        Dim objUser As New CustomOrder()
        Dim rs As SqlDataReader
        rs = objUser.PopulateFunctionalkeys(CompanyID, DepartmentID, DivisionID)
        While rs.Read()
            Fkey = Fkey + "~" + rs("Fkey").ToString().Trim().Replace(vbCrLf, "")
            Action = Action + "~" + rs("RelatedAction").ToString().Trim().Replace(vbCrLf, "")
        End While

        If Fkey <> "" Then
            Fkey = Fkey.Remove(0, 1)
        End If
        If Action <> "" Then
            Action = Action.Remove(0, 1)
        End If
        rs.Close()
    End Sub
#End Region

#Region "PopulateMacros"
    Public Sub PopulateMacros()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopMacro As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopMacro.PopulateMacroValues(CompanyID, DepartmentID, DivisionID)
        While rs.Read()
            MacroValues = MacroValues + "~" + rs("MessageDesc").ToString().Trim().Replace(vbCrLf, "")
            MacroCodes = MacroCodes + "~" + rs("MacroCode").ToString().Trim().Replace(vbCrLf, "")
        End While
        If MacroValues <> "" Then
            MacroValues = MacroValues.Remove(0, 1)
        End If
        If MacroCodes <> "" Then
            MacroCodes = MacroCodes.Remove(0, 1)
        End If
        rs.Close()
    End Sub
#End Region

#Region "Show Delivery Date and Delivery method Display"

    Public Sub PopulateDeliveryDateandMethodDisplay()

        Dim CompanyID As String = ""
        Dim DivisionID As String = ""
        Dim DepartmentID As String = ""
        Dim EmpID As String = ""

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim objUSer As New CustomOrder()
        Dim rs As SqlDataReader
        rs = objUSer.PopulateDeliveryDateandMethodDisplay(CompanyID, DivisionID, DepartmentID)

        While rs.Read()
            If rs("AllowDeliveryToday").ToString() = "True" Then
                ShowTodaysDate = "1"
                txtDeliveryDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy")
            Else
                ShowTodaysDate = "0"
                txtDeliveryDate.Text = DateTime.Now.Date.AddDays(1).ToString("MM/dd/yyyy")
            End If

        End While
        rs.Close()



    End Sub

#End Region

#Region "Show Populate Customer Types"
    Sub populateCustomerTypes()
        If drpPaymentType.SelectedValue <> "Wire In" Then

            If drpCustomerID.SelectedValue <> "New Customer" And drpCustomerID.SelectedValue <> "Retail Customer" Then
                lblCustomer.Text = "Bill to Customer ID"
                drpCustomerID.Items.Clear()
                'Retail Customer
                drpCustomerID.Items.Add("Retail Customer")
                drpCustomerID.Items.Add("New Customer")
            End If


            txtcustomersearch.Enabled = True
            btncustsearch.Enabled = True
            If drpCustomerID.Visible = False And txtCustomerTemp.Text.Trim <> "" Then
                txtSpecifyCustomer.Visible = False
                txtCustomerTemp.Visible = True
                lnkCustomerSearch.Visible = False
                lnkBackToOption.Visible = True
            Else
                txtCustomerTemp.Visible = False
                lnkCustomerSearch.Visible = True
                lnkBackToOption.Visible = False
                drpCustomerID.Visible = True
            End If


            'drpCustomerID.Items.Add("Search")
            'PopupvendorControl.Visible = False
            lkpCustomerID.Visible = True
            lblNewsletter.Visible = True
            drpNewsletterID.Visible = True
            lblPO.Visible = True
            txtPO.Visible = True
            lblSource.Visible = True
            drpSource.Visible = True
            pnlCustomer.Visible = True
            pnlVendor.Visible = False
            lblSalutation.Text = "Salutation"

            lblCustomerComments.Text = "Customer Comments"
            chkBillingAddress.Visible = True
            lnkShipAddress.Visible = True


        Else
            drpCustomerID.Visible = False
            txtSpecifyCustomer.Visible = False
            txtCustomerTemp.Visible = True
            lnkCustomerSearch.Visible = False
            lnkBackToOption.Visible = False
            txtcustomersearch.Enabled = False
            btncustsearch.Enabled = False
            lblCustomer.Text = "Wire Service"
            'drpCustomerID.Items.Clear()
            'Dim lst As New ListItem
            'lst.Text = "-Select-"
            'lst.Value = "0"
            'drpCustomerID.Items.Add(lst)
            'Dim objwireserive As New clsWireInProcessInOrderEntryForm
            'objwireserive.CompanyID = CompanyID
            'objwireserive.DivisionID = DivisionID
            'objwireserive.DepartmentID = DepartmentID
            'Dim wiredt As New DataTable
            'wiredt = objwireserive.GetWireSeriveCustomer()
            'If wiredt.Rows.Count <> 0 Then
            '    Dim nf As Integer = 0
            '    For nf = 0 To wiredt.Rows.Count - 1
            '        drpCustomerID.Items.Add(wiredt.Rows(nf)("CustomerID"))
            '    Next
            'End If

            'lkpCustomerID.Visible = False
            'PopupvendorControl.Visible = False
            lblNewsletter.Visible = False
            drpNewsletterID.Visible = False
            lblPO.Visible = False
            txtPO.Visible = False
            lblSource.Visible = False
            drpSource.Visible = False
            pnlCustomer.Visible = False
            pnlVendor.Visible = True
            lblSalutation.Text = "Wire Service"

            lblCustomerComments.Text = "Florist Comments"
            chkBillingAddress.Visible = False
            lnkShipAddress.Visible = False

        End If

    End Sub
#End Region

#Region "Populate Card Expiry Date"
    Sub PopulateCardExpDate()
        Dim i As Integer = 0
        For i = 0 To 120
            drpExpirationDate.Items.Add(DateTime.Now.Date.AddMonths(i).ToString("MM/yyyy"))
        Next

    End Sub


#End Region

#Region "PopulateDrops()"


    Public Sub Setdropdown()

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim locationid As String = CType(SessionKey("Locationid"), String)
        If locationid <> "" Then
            cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(locationid))
        End If

        Session("OrderLocationid") = cmblocationid.SelectedValue
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
        GetZoneID(CompanyID, DivisionID, DepartmentID, cmblocationid.SelectedValue)

        'Populating Order Types

        Dim PopOrderType As New CustomOrder()
        ' Dim rs As SqlDataReader
        rs = PopOrderType.PopulateOrderType(CompanyID, DepartmentID, DivisionID)
        drpOrderTypeIDData.DataTextField = "OrderTypeID"
        drpOrderTypeIDData.DataValueField = "OrderTypeID"
        drpOrderTypeIDData.DataSource = rs
        drpOrderTypeIDData.DataBind()
        drpOrderTypeIDData.SelectedIndex = drpOrderTypeIDData.Items.IndexOf(drpOrderTypeIDData.Items.FindByValue("POS"))
        rs.Close()

        'Populating Payment Type
        Dim rs1 As SqlDataReader
        rs1 = PopOrderType.PopulatePaymentTypes(CompanyID, DepartmentID, DivisionID)

        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
            drpPaymentType.DataTextField = "PaymentMethodDescription"
            drpPaymentType.DataValueField = "PaymentMethodID"
        Else
            drpPaymentType.DataTextField = "PaymentMethodID"
            drpPaymentType.DataValueField = "PaymentMethodID"
        End If

        drpPaymentType.DataSource = rs1
        drpPaymentType.DataBind()
        drpPaymentType.Items.Insert(0, (New ListItem("-Select-", "0")))

        Try
            If CompanyID <> "BrownsTheFloristV8Z5N6" And CompanyID <> "PoppiesV8W1L8" Then
                drpPaymentType.SelectedIndex = drpPaymentType.Items.IndexOf(drpPaymentType.Items.FindByValue("Cash"))

                If CompanyID = "Fleurish" Then
                    ' drpPaymentType.SelectedIndex = drpPaymentType.Items.IndexOf(drpPaymentType.Items.FindByValue("Credit Card"))
                End If

                Dim sender1 As New Object
                Dim e1 As System.EventArgs

                drpPaymentType_SelectedIndexChanged(sender1, e1)
            End If
        Catch ex As Exception
            drpPaymentType.SelectedIndex = drpPaymentType.Items.IndexOf(drpPaymentType.Items.FindByValue("0"))
        End Try



        rs1.Close()

        'Populating Customers IDs

        ' PopulateCustomerInfo("DEFAULT")
        BindGrid()
        'Populating Destination Types


        rs = PopOrderType.PopulateDestinationTypes(CompanyID, DepartmentID, DivisionID)
        drpDestinationType.DataTextField = "DestinationDesc"
        drpDestinationType.DataValueField = "DestinationID"
        drpDestinationType.DataSource = rs
        drpDestinationType.DataBind()

        drpDestinationType.Items.Insert(0, (New ListItem("-Select-", "0")))
        drpDestinationType.SelectedIndex = drpDestinationType.Items.IndexOf(drpDestinationType.Items.FindByValue("0"))
        rs.Close()

        'Populating EmployeeID



        rs = PopOrderType.PopulateEmployees(CompanyID, DepartmentID, DivisionID)

        drpEmployeeID.DataTextField = "EmployeeName"
        drpEmployeeID.DataValueField = "EmployeeID"
        drpEmployeeID.DataSource = rs
        drpEmployeeID.DataBind()
        drpEmployeeID.SelectedValue = EmployeeID ' Session("EmployeeUserName")
        rs.Close()


        'New code for drpAssignedto
        rs = PopOrderType.PopulateEmployees(CompanyID, DepartmentID, DivisionID)
        drpAssignedto.DataTextField = "EmployeeName"
        drpAssignedto.DataValueField = "EmployeeID"
        drpAssignedto.DataSource = rs
        drpAssignedto.DataBind()
        Dim lstdrpAssignedto As New ListItem
        lstdrpAssignedto.Text = "-Select-"
        lstdrpAssignedto.Value = ""
        drpAssignedto.Items.Insert(0, lstdrpAssignedto)
        rs.Close()
        'New code for drpAssignedto


        'Populating RetailerLogo
        Dim ImageTemp As String = ""

        rs = PopOrderType.PopulateCompanyLogo(CompanyID, DepartmentID, DivisionID)
        While (rs.Read())

            'Dim objNascheck As New clsNasImageCheck
            'ImgRetailerLogo.ImageUrl = objNascheck.retLogourl(rs("CompanyLogoUrl").ToString()) ' "~/images/" & rs("CompanyLogoUrl").ToString()

            ImgRetailerLogo.ImageUrl = "~" & returl(rs("CompanyLogoUrl").ToString())

        End While

        rs.Close()


        ' '' Populate Countries

        rs = PopOrderType.PopulateCountries(CompanyID, DepartmentID, DivisionID)

        drpCountry.DataTextField = "CountryDescription"
        drpCountry.DataValueField = "CountryID"


        drpCountry.DataSource = rs
        drpCountry.DataBind()

        rs.Close()


        rs = PopOrderType.PopulateCountries(CompanyID, DepartmentID, DivisionID)

        drpWireOutCountry.DataTextField = "CountryDescription"
        drpWireOutCountry.DataValueField = "CountryID"
        drpWireOutCountry.DataSource = rs
        drpWireOutCountry.DataBind()
        rs.Close()
        ''Populating local Florist Country as default country in dropdownlist

        'New code for wire out
        Dim lstWireOutCountry As New ListItem
        lstWireOutCountry.Text = "-Select-"
        lstWireOutCountry.Value = ""
        drpWireOutCountry.Items.Insert(0, lstWireOutCountry)
        'New code for wire out


        rs = PopOrderType.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)
        While rs.Read()

            Dim Countries As String = rs("CompanyCountry").ToString()

            drpCountry.SelectedIndex = drpCountry.Items.IndexOf(drpCountry.Items.FindByValue(rs("CompanyCountry").ToString()))
        End While

        rs.Close()

        rs = PopOrderType.PopulateCountries(CompanyID, DepartmentID, DivisionID)

        drpShipCountry.DataTextField = "CountryDescription"
        drpShipCountry.DataValueField = "CountryID"
        drpShipCountry.DataSource = rs
        drpShipCountry.DataBind()
        rs.Close()


        Dim rsShipCountry As SqlDataReader
        rsShipCountry = PopOrderType.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)
        While rsShipCountry.Read()
            drpShipCountry.SelectedIndex = drpShipCountry.Items.IndexOf(drpShipCountry.Items.FindByValue(rsShipCountry("CompanyCountry").ToString()))

            If rsShipCountry("CompanyCountry").ToString() = "US" Then
                drpState.Visible = True
                txtCustomerState.Visible = False
            ElseIf rsShipCountry("CompanyCountry").ToString() = "CD" Then
                drpState.Visible = True
                txtCustomerState.Visible = False
            Else
                drpState.Visible = False
                txtCustomerState.Visible = True
            End If

        End While
        rsShipCountry.Close()



        ''Populate States
        rs = PopOrderType.PopulateStates(CompanyID, DepartmentID, DivisionID)
        drpState.DataTextField = "StateID"
        drpState.DataValueField = "StateID"


        drpState.DataSource = rs
        drpState.DataBind()
        rs.Close()


        rs = PopOrderType.PopulateStates(CompanyID, DepartmentID, DivisionID)
        drpWireOutState.DataTextField = "StateID"
        drpWireOutState.DataValueField = "StateID"
        drpWireOutState.DataSource = rs
        drpWireOutState.DataBind()

        rs.Close()


        'New code for wire out
        Dim lst As New ListItem
        lst.Text = "-Select-"
        lst.Value = ""
        drpWireOutState.Items.Insert(0, lst)
        'New code for wire out


        ''New code added for Manuall value if needed
        Dim obj_IsAddressDefault As New clsDeliveyByCountry
        obj_IsAddressDefault.CompanyID = CompanyID
        obj_IsAddressDefault.DepartmentID = DepartmentID
        obj_IsAddressDefault.DivisionID = DivisionID
        Dim dtdefault As New DataTable
        dtdefault = obj_IsAddressDefault.FillDefaultDeliveryCharge
        Dim IsAddressDefault As Boolean = True
        Dim IsAddressDefaultCST As Boolean = True

        If dtdefault.Rows.Count <> 0 Then
            Try
                IsAddressDefault = dtdefault.Rows(0)("IsAddressDefault")
                IsAddressDefaultCST = dtdefault.Rows(0)("IsAddressDefaultCST")
            Catch ex As Exception
            End Try
        End If
        ''New code added for Manuall value if needed


        rs = PopOrderType.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)

        If CompanyID = "BranchingOutFloralL0E1E0" Or IsAddressDefaultCST Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then

            While rs.Read()

                If rs("CompanyCountry").ToString() = "US" Then
                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(rs("CompanyState").ToString()))

                ElseIf rs("CompanyCountry").ToString().ToLower() = "cd" Then

                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(rs("CompanyState").ToString()))

                Else
                    txtCustomerState.Text = rs("CompanyState").ToString()
                End If
            End While
        Else
            drpState.Items.Insert(0, (New ListItem("-Select-", "")))
            drpState.SelectedIndex = drpPriorirty.Items.IndexOf(drpPriorirty.Items.FindByValue("0"))
        End If

        rs.Close()

        rs = PopOrderType.PopulateStates(CompanyID, DepartmentID, DivisionID)
        drpShippingState.DataTextField = "StateID"
        drpShippingState.DataValueField = "StateID"
        drpShippingState.DataSource = rs
        drpShippingState.DataBind()
        rs.Close()

        Dim rsShipState As SqlDataReader
        rsShipState = PopOrderType.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)
        While rsShipState.Read()
            'drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(rsShipState("EmployeeState").ToString()))
            If rsShipState("CompanyCountry").ToString() = "US" Then
            ElseIf rsShipState("CompanyCountry").ToString() = "CD" Then
            Else
            End If
        End While
        rsShipState.Close()




        'rs = PopOrderType.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)
        'Me.CompanyID = "FieldOfFlowers" Or 

        If (Me.CompanyID = "FieldOfFlowers" Or Me.CompanyID = "FieldOfFlowersTraining") Then
            rs = PopOrderType.GettingRetailerCountryNameByLocation(CompanyID, DepartmentID, DivisionID, cmblocationid.SelectedValue)
        Else
            rs = PopOrderType.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)
        End If

        While rs.Read()

            If IsAddressDefaultCST Then
                txtCustomerCity.Text = rs("CompanyCity").ToString()
                txtCustomerZip.Text = rs("Companyzip").ToString()
            End If

            If IsAddressDefault Then
                txtShippingCity.Text = rs("CompanyCity").ToString()
                txtShippingZip.Text = rs("Companyzip").ToString()
            End If

            If rs("CompanyCountry").ToString() = "US" Then
                drpShippingState.Visible = True
                txtShippingState.Visible = False

                Dim state As String = rs("CompanyState").ToString()


                If IsAddressDefault Then
                    drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(rs("CompanyState").ToString()))
                Else
                    Dim list As New ListItem
                    list.Text = ""
                    list.Value = ""
                    drpShippingState.Items.Insert(0, list)
                    drpShippingState.SelectedIndex = 0
                End If

            ElseIf rs("CompanyCountry").ToString().ToLower() = "cd" Then
                drpShippingState.Visible = True
                txtShippingState.Visible = False

                If CompanyID = "BranchingOutFloralL0E1E0" Or IsAddressDefault Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
                    drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(rs("CompanyState").ToString()))
                Else
                    Dim list As New ListItem
                    list.Text = ""
                    list.Value = ""
                    drpShippingState.Items.Insert(0, list)
                    drpShippingState.SelectedIndex = 0
                End If

            Else
                drpShippingState.Visible = False
                txtShippingState.Visible = True

                If IsAddressDefault Then
                    txtShippingState.Text = rs("CompanyState").ToString()
                End If

            End If
        End While
        rs.Close()

        If (Me.CompanyID = "FieldOfFlowersTraining") Or (Me.CompanyID = "FieldOfFlowers") Then
            LoadLocationandZoneForZipDetails(txtShippingZip.Text)
        End If


        rs = PopOrderType.PopulateOccasionCodes(CompanyID, DepartmentID, DivisionID)
        drpOccasionCode.DataTextField = "OccasionDesc"
        drpOccasionCode.DataValueField = "OccasionDesc"
        drpOccasionCode.DataSource = rs
        drpOccasionCode.DataBind()
        drpOccasionCode.Items.Insert(0, (New ListItem("-Select-", "")))
        drpOccasionCode.SelectedIndex = drpOccasionCode.Items.IndexOf(drpOccasionCode.Items.FindByValue("0"))

        rs.Close()

        'Populate Priority
        rs = PopOrderType.PopulatePriority(CompanyID, DepartmentID, DivisionID)
        drpPriorirty.DataTextField = "PriorityDesc"
        drpPriorirty.DataValueField = "PriorityDesc"
        drpPriorirty.DataSource = rs
        drpPriorirty.DataBind()
        drpPriorirty.Items.Insert(0, (New ListItem("-Select-", "0")))
        drpPriorirty.SelectedIndex = drpPriorirty.Items.IndexOf(drpPriorirty.Items.FindByValue("0"))

        rs.Close()
        ' Populate DeliveryMethods

        rs = PopOrderType.PopulateDeliveryMethods(CompanyID, DepartmentID, DivisionID)

        drpShipMethod.DataTextField = "ShipMethodDescription"
        drpShipMethod.DataValueField = "ShipMethodID"
        drpShipMethod.DataSource = rs
        drpShipMethod.DataBind()
        drpShipMethod.Items.Insert(0, (New ListItem("-Select-", "0")))
        Try
            drpShipMethod.SelectedIndex = drpShipMethod.Items.IndexOf(drpShipMethod.Items.FindByValue("Taken"))
        Catch ex As Exception
            drpShipMethod.SelectedIndex = drpShipMethod.Items.IndexOf(drpShipMethod.Items.FindByValue("0"))
        End Try


        'Populate TransactionTypes
        rs = PopOrderType.PopulateTransactionTypes(CompanyID, DepartmentID, DivisionID)

        drpTransaction.DataTextField = "TransactionDescription"
        drpTransaction.DataValueField = "TransactionTypeID"
        drpTransaction.DataSource = rs
        drpTransaction.DataBind()

        drpTransaction.SelectedIndex = drpTransaction.Items.IndexOf(drpTransaction.Items.FindByValue("Order"))

        'Populating System Wide message
        SystemWideMessageDisplay()

        'Populating Macros
        PopulateMacros()

        'Populating Location (WareHouse)
        PopulateWareHouse()

        'Populating Projects
        PopulateProjects()

        'Populatate Salutations
        PopulateSalutations()

        'Populate Taxes

        PopulateTaxes()


        'Populate Internal Notes

        'PopulateInternalNotes()

        'Populate Wire Services

        PopulateWireServices()


        ' Populate Wire Service Status

        PopulateWireServicesStatus()

        ' Populate Wire Service  Priority
        PopulateWireServicesPriority()

        'Populate Wire Service TransmissionMethod

        PopulateWireServiceTransmissionMethod()
        ' PopulateServiceRelayCharges()


        PopulateOrderSourceCode()

        populateCreditCardTypes()


        Dim Sender As New Object
        Dim e As New System.EventArgs
        txtShippingZip_TextChanged(Sender, e)



        ''New code addded for Option for promt
        Dim dtOpt As New DataTable

        dtOpt = PopulateOrderEntryPrompts()

        If dtOpt.Rows.Count <> 0 Then

            Try
                If dtOpt.Rows(0)("EmployeeId").ToString = "True" Then
                    Opt_Employee = 1
                Else
                    Opt_Employee = 0
                End If
            Catch ex As Exception
                Opt_Employee = 0
            End Try

            'Added by GG
            Try
                If dtOpt.Rows(0)("Priority").ToString = "True" Then
                    Opt_Priority = 1
                Else
                    Opt_Priority = 0
                End If
            Catch ex As Exception
                Opt_Priority = 0
            End Try

            Try
                If dtOpt.Rows(0)("SourceCode").ToString = "True" Then
                    Opt_Source = 1
                Else
                    Opt_Source = 0
                End If

            Catch ex As Exception
                Opt_Source = 0
            End Try
            Try
                If dtOpt.Rows(0)("OccasionCode").ToString = "True" Then
                    Opt_Occasion = 1
                Else
                    Opt_Occasion = 0
                End If
            Catch ex As Exception
                Opt_Occasion = 0
            End Try

        Else
            Opt_Employee = 0
            Opt_Occasion = 0
            Opt_Source = 0
	    Opt_Priority = 0        'Added by GG
        End If
        ''New code addded for Option for promt

        If Opt_Employee = 1 Then
            drpEmployeeID.Items.Insert(0, (New ListItem("-Select-", "")))
            drpEmployeeID.SelectedIndex = 0
        End If



    End Sub


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


    Public Function PopulateOrderEntryPrompts() As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""

        ssql = "Select * from  OrderEntryPrompts where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2 "
        Dim dt As New DataTable
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message

            Return dt
        End Try
        Return dt
    End Function









#End Region



#Region "SystemWideMessageDisplay"
    Public Sub SystemWideMessageDisplay()
        Dim CurrDate As String = ""
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)

        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")
        EmployeeID = SessionKey("EmployeeID")
        CurrDate = Now.ToString()

        'Finding the Local time Starts here

        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim GMT As Integer
        Dim reader As SqlDataReader

        Dim commandText As String = "[enterprise].[GetRetailerCompanyGMT]"
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(commandText, myConnection)
        Dim workParam As New SqlParameter()
        myCommand.CommandType = CommandType.StoredProcedure

        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID

        myConnection.Open()
        reader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        If reader.HasRows() = True Then
            While reader.Read()
                GMT = Convert.ToInt16(reader("GMTOffset").ToString())
            End While
            CurrDate = DateTime.UtcNow.AddHours(GMT).ToString()

        Else
            CurrDate = DateTime.Now.ToString()
        End If
        reader.Close()
        myConnection.Close()

        'Finding the Local time Ends here

        BindGridSystemWideMsg(CurrDate)
    End Sub
#End Region

#Region "BindGridSystemWideMsg"
    Sub BindGridSystemWideMsg(ByVal CurrentDate As String)
        ' Load the name of the stored procedure where our data comes from here into commandtext
        ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
        Dim rs1 As SqlDataReader
        Dim CommandText As String = "enterprise.spCompanySystemWideMessage"

        ' get the connection ready
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        myCommand.CommandTimeout = ConfigSettings.SqlCommandTimeout
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@CurrDate", SqlDbType.NVarChar).Value = CurrentDate


        ' open the connection
        myConnection.Open()
        rs1 = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
        If rs1.HasRows = True Then
            While rs1.Read()
                SystemWMessage = rs1("SystemMessage").ToString()
                lblSystemWM.Text = SystemWMessage
            End While


        End If

        rs1.Close()
    End Sub
#End Region

#Region "populateCreditCardTypes"

    Public Sub populateCreditCardTypes()
        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderType.PopulateCreditCardTypes(CompanyID, DepartmentID, DivisionID)
        drpCardType.DataTextField = "CreditCardTypeID"
        drpCardType.DataValueField = "CreditCardTypeID"
        drpCardType.DataSource = rs
        drpCardType.DataBind()
        rs.Close()


        Try
            drpCardType.SelectedValue = "Visa"
        Catch ex As Exception

        End Try

    End Sub
#End Region


#Region "PopulateWareHouse"
    Public Sub PopulateWareHouse()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)

        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopWareHouse As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopWareHouse.PopulateWarehouseLocations(CompanyID, DepartmentID, DivisionID)
        drpLocation.DataValueField = "WareHouseID"
        drpLocation.DataTextField = "WareHouseID"
        drpLocation.DataSource = rs
        drpLocation.DataBind()
        drpLocation.SelectedIndex = drpLocation.Items.IndexOf(drpLocation.Items.FindByValue("Default"))
        rs.Close()
    End Sub
#End Region


#Region "PopulateProjects"
    Public Sub PopulateProjects()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopProjects As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopProjects.PopulateProjectsName(CompanyID, DepartmentID, DivisionID)
        drpProject.DataValueField = "ProjectID"
        drpProject.DataTextField = "ProjectName"
        drpProject.DataSource = rs
        drpProject.DataBind()

        drpProject.SelectedIndex = drpProject.Items.IndexOf(drpProject.Items.FindByValue("Default"))
        rs.Close()
    End Sub
#End Region

#Region "PopulateWireServices"
    Public Sub PopulateWireServices()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopWireServices As New CustomOrder()
        Dim ds As DataSet

        ds = PopWireServices.PopulateAllWireServices(CompanyID, DepartmentID, DivisionID)

        drpWireoutService.DataTextField = "WireServiceID"
        drpWireoutService.DataValueField = "WireServiceID"
        drpWireoutService.DataSource = ds

        drpWireoutService.DataBind()

        drpWireoutService.Items.Insert(0, (New ListItem("-Please Select-", "0")))
        drpWireoutService.SelectedIndex = drpWire.Items.IndexOf(drpWire.Items.FindByValue("0"))

        drpWire.DataTextField = "WireServiceID"
        drpWire.DataValueField = "WireServiceID"
        drpWire.DataSource = ds
        drpWire.DataBind()
        drpWire.Items.Insert(0, (New ListItem("-Select-", "0")))
        drpWire.SelectedIndex = drpWire.Items.IndexOf(drpWire.Items.FindByValue("0"))

    End Sub

#End Region

#Region "PopulateWireServicesStatus"
    Public Sub PopulateWireServicesStatus()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopWireServices As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopWireServices.PopulateWireServicesStatus(CompanyID, DepartmentID, DivisionID)

        drpWireoutStatus.DataTextField = "WireServiceStatusID"
        drpWireoutStatus.DataValueField = "WireServiceStatusID"
        drpWireoutStatus.DataSource = rs

        drpWireoutStatus.DataBind()
        rs.Close()

        'New code for wire out
        Dim lstWireoutStatus As New ListItem
        lstWireoutStatus.Text = "-Select-"
        lstWireoutStatus.Value = ""
        drpWireoutStatus.Items.Insert(0, lstWireoutStatus)
        'New code for wire out

    End Sub

#End Region

#Region "Populate Taxes"

    Public Sub PopulateTaxes()

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)

        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopSalutations As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopSalutations.BindingTaxes(CompanyID, DepartmentID, DivisionID)
        drpTaxes.DataTextField = "TaxID"
        drpTaxes.DataValueField = "TaxIDlower"
        drpTaxes.DataSource = rs

        drpTaxes.DataBind()

        Try
            drpTaxes.SelectedValue = "default"
        Catch ex As Exception

        End Try

        Try
            Dim locationid As String = CType(SessionKey("Locationid"), String)
            If locationid <> "" And CompanyID = "Busseys,Inc.30125" Then
                'txtInternalNotes.Text = drpTaxes.Items.IndexOf(drpTaxes.Items.FindByText(locationid))
                If drpTaxes.Items.IndexOf(drpTaxes.Items.FindByText(locationid)) <> -1 Then
                    drpTaxes.SelectedIndex = drpTaxes.Items.IndexOf(drpTaxes.Items.FindByText(locationid))
                End If

            End If

        Catch ex As Exception

        End Try

        PopulatingTaxPercent()

        rs.Close()
    End Sub

#End Region

#Region "PopulateSalutations"
    Public Sub PopulateSalutations()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")



        Dim objUser As New DAL.CustomOrder()
        Dim ds As DataSet

        ds = objUser.PopulateSalutations(CompanyID, DepartmentID, DivisionID)

        DropDownList1.DataTextField = "Salutation"
        DropDownList1.DataValueField = "Salutation"
        DropDownList1.DataSource = ds
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, (New ListItem("", "0")))
        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue("0"))

        drpShipCustomerSalutation.DataTextField = "Salutation"
        drpShipCustomerSalutation.DataValueField = "Salutation"
        drpShipCustomerSalutation.DataSource = ds
        drpShipCustomerSalutation.DataBind()
        drpShipCustomerSalutation.Items.Insert(0, (New ListItem("", "0")))
        drpShipCustomerSalutation.SelectedIndex = drpShipCustomerSalutation.Items.IndexOf(drpShipCustomerSalutation.Items.FindByValue("0"))

    End Sub
#End Region

#Region "PopulatingTaxPercent"


    Dim taxproduct_pst As Integer = 0
    Dim taxdelivery_pst As Integer = 0
    Dim taxserviceRelya_pst As Integer = 0

    Dim taxproduct_gst As Integer = 0
    Dim taxdelivery_gst As Integer = 0
    Dim taxserviceRelya_gst As Integer = 0

    Sub PopulatingTaxPercentValuesGSTPST()

        txtShippingState.Text = drpShippingState.SelectedValue

        'txtInternalNotes.Text = txtShippingState.Text

        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "Demo Site-90210" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then

            If txtPostedDate.Text.Trim <> "" Or True Then
                Dim dt As Date = "1/1/1900"

                Try
                    dt = txtPostedDate.Text
                Catch ex As Exception

                End Try


                If dt > "12/05/2013" Or txtPostedDate.Text.Trim = "" Then

                    ' txtInternalNotes.Text = "in date"

                    txtTaxPercentGST.Visible = True
                    txtTaxPercentPST.Visible = True
                    txtTaxPST.Visible = True
                    txtTaxGST.Visible = True
                    drpTaxesPST.Visible = True
                    drpTaxesGSTHST.Visible = True

                    If txtShippingState.Text = "BC" Then
                        txtTaxPercentPST.Text = "7%"
                        txtTaxPercentGST.Text = "5%"
                        'txtInternalNotes.Text = "in BC"

                        If drpShipMethod.SelectedValue = "Wire_Out" Then
                            taxproduct_pst = 7
                            taxdelivery_pst = 7
                            taxserviceRelya_pst = 0

                            taxproduct_gst = 5
                            taxdelivery_gst = 5
                            taxserviceRelya_gst = 5
                        Else
                            taxproduct_pst = 7
                            taxdelivery_pst = 0
                            taxserviceRelya_pst = 0

                            taxproduct_gst = 5
                            taxdelivery_gst = 5
                            taxserviceRelya_gst = 5
                        End If

                    Else
                        txtTaxPercentPST.Text = "0%"
                        txtTaxPercentGST.Text = "5%"

                        taxproduct_pst = 0
                        taxdelivery_pst = 0
                        taxserviceRelya_pst = 0

                        taxproduct_gst = 5
                        taxdelivery_gst = 5
                        taxserviceRelya_gst = 5
                    End If


                    If txtShippingState.Text = "AB" Or txtShippingState.Text = "MB" Or txtShippingState.Text = "YT" Or txtShippingState.Text = "SK" Or txtShippingState.Text = "NT" Or txtShippingState.Text = "PE" Then

                        txtTaxPercentPST.Text = "0%"
                        txtTaxPercentGST.Text = "5%"

                        taxdelivery_pst = 0
                        taxproduct_pst = 0
                        taxserviceRelya_pst = 0

                        taxdelivery_gst = 5
                        taxproduct_gst = 5
                        taxserviceRelya_gst = 5

                    End If

                    If txtShippingState.Text = "NB" Or txtShippingState.Text = "NL" Or txtShippingState.Text = "ON" Or txtShippingState.Text = "LB" Then

                        txtTaxPercentPST.Text = "0%"
                        txtTaxPercentGST.Text = "13%"

                        taxdelivery_pst = 0
                        taxproduct_pst = 0
                        taxserviceRelya_pst = 0

                        taxdelivery_gst = 13
                        taxproduct_gst = 13
                        taxserviceRelya_gst = 5

                    End If


                    If txtShippingState.Text = "NS" Then

                        txtTaxPercentPST.Text = "0%"
                        txtTaxPercentGST.Text = "15%"

                        taxdelivery_pst = 0
                        taxproduct_pst = 0
                        taxserviceRelya_pst = 0

                        taxdelivery_gst = 15
                        taxproduct_gst = 15
                        taxserviceRelya_gst = 5

                    End If

                    If drpShipCountry.SelectedValue <> "CD" Then
                        txtTaxPercentPST.Text = "0%"
                        txtTaxPercentGST.Text = "5%"

                        taxdelivery_pst = 0
                        taxproduct_pst = 0
                        taxserviceRelya_pst = 0

                        taxdelivery_gst = 0
                        taxproduct_gst = 0
                        taxserviceRelya_gst = 5

                    End If


                    If drpPaymentType.SelectedValue = "Wire In" Then
                        txtTaxPercentPST.Text = "0%"
                        txtTaxPercentGST.Text = "0%"

                        taxdelivery_pst = 0
                        taxproduct_pst = 0
                        taxserviceRelya_pst = 0

                        taxdelivery_gst = 0
                        taxproduct_gst = 0
                        taxserviceRelya_gst = 0

                    End If

                    If txtCustomerTemp.Text.Trim().ToLower = "c-27843" Or txtCustomerTemp.Text.Trim().ToLower = "b-4683" Or txtCustomerTemp.Text.Trim().ToLower = "b-14544" Or txtCustomerTemp.Text.Trim().ToLower = "b-4685" Or txtCustomerTemp.Text.Trim().ToLower = "b-4687" Or txtCustomerTemp.Text.Trim().ToLower = "b-4894" Or txtCustomerTemp.Text.Trim().ToLower = "b-4692" Or txtCustomerTemp.Text.Trim().ToLower = "b-4686" Or txtCustomerTemp.Text.Trim().ToLower = "b-4891" Then
                        txtTaxPercentPST.Text = "0%"

                        taxdelivery_pst = 0
                        taxproduct_pst = 0
                        taxserviceRelya_pst = 0

                    End If


                End If


            End If


            If CompanyID = "BranchingOutFloralL0E1E0" Then

                txtTaxPercentGST.Visible = True
                txtTaxPercentPST.Visible = True
                txtTaxPST.Visible = True
                txtTaxGST.Visible = True
                drpTaxesPST.Visible = True
                drpTaxesGSTHST.Visible = True

                If txtShippingState.Text = "AB" Then
                    txtTaxPercentPST.Text = "0%"
                    txtTaxPercentGST.Text = "5%"

                    taxproduct_pst = 0
                    taxdelivery_pst = 0
                    taxserviceRelya_pst = 0

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                End If

                If txtShippingState.Text = "BC" Then
                    txtTaxPercentPST.Text = "7%"
                    txtTaxPercentGST.Text = "5%"

                    taxproduct_pst = 7
                    taxdelivery_pst = 7
                    taxserviceRelya_pst = 7

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                End If


                If txtShippingState.Text = "MB" Then
                    txtTaxPercentPST.Text = "8%"
                    txtTaxPercentGST.Text = "5%"

                    taxproduct_pst = 8
                    taxdelivery_pst = 8
                    taxserviceRelya_pst = 8

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                End If

                If txtShippingState.Text = "NB" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "8%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 8
                    taxdelivery_pst = 8
                    taxserviceRelya_pst = 8


                End If

                If txtShippingState.Text = "NL" Or txtShippingState.Text = "LA" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "8%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 8
                    taxdelivery_pst = 8
                    taxserviceRelya_pst = 8


                End If

                If txtShippingState.Text = "NT" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "0%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 0
                    taxdelivery_pst = 0
                    taxserviceRelya_pst = 0

                End If

                If txtShippingState.Text = "NS" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "10%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 10
                    taxdelivery_pst = 10
                    taxserviceRelya_pst = 10

                End If

                If txtShippingState.Text = "NU" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "0%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 0
                    taxdelivery_pst = 0
                    taxserviceRelya_pst = 0

                End If

                If txtShippingState.Text = "ON" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "8%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 8
                    taxdelivery_pst = 8
                    taxserviceRelya_pst = 8

                End If

                If txtShippingState.Text = "PE" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "9%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 9
                    taxdelivery_pst = 9
                    taxserviceRelya_pst = 9

                End If

                If txtShippingState.Text = "QC" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "9.975%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 9.975
                    taxdelivery_pst = 9.975
                    taxserviceRelya_pst = 9.975

                End If

                If txtShippingState.Text = "SK" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "5%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 5
                    taxdelivery_pst = 5
                    taxserviceRelya_pst = 5

                End If

                If txtShippingState.Text = "YT" Then

                    txtTaxPercentGST.Text = "5%"
                    txtTaxPercentPST.Text = "0%"

                    taxproduct_gst = 5
                    taxdelivery_gst = 5
                    taxserviceRelya_gst = 5

                    taxproduct_pst = 0
                    taxdelivery_pst = 0
                    taxserviceRelya_pst = 0

                End If

                If drpShipCountry.SelectedValue <> "CD" Then
                    txtTaxPercentPST.Text = "0%"
                    txtTaxPercentGST.Text = "5%"

                    taxdelivery_pst = 0
                    taxproduct_pst = 0
                    taxserviceRelya_pst = 0

                    taxdelivery_gst = 0
                    taxproduct_gst = 0
                    taxserviceRelya_gst = 5

                End If


                If drpPaymentType.SelectedValue = "Wire In" Then
                    txtTaxPercentPST.Text = "0%"
                    txtTaxPercentGST.Text = "0%"

                    taxdelivery_pst = 0
                    taxproduct_pst = 0
                    taxserviceRelya_pst = 0

                    taxdelivery_gst = 0
                    taxproduct_gst = 0
                    taxserviceRelya_gst = 0

                End If


                Dim dt_txtCustomerTemp As New DataTable
                dt_txtCustomerTemp = SelectCustomerInformationDetail(txtCustomerTemp.Text.Trim)

                If dt_txtCustomerTemp.Rows.Count <> 0 Then
                    Dim PSTTAXOFF As Boolean = False

                    Try
                        PSTTAXOFF = dt_txtCustomerTemp.Rows(0)("PSTTAXOFF")
                    Catch ex As Exception

                    End Try

                    If PSTTAXOFF Then
                        txtTaxPercentPST.Text = "0%"

                        taxdelivery_pst = 0
                        taxproduct_pst = 0
                        taxserviceRelya_pst = 0


                    End If

                    Dim GSTTAXOFF As Boolean = False
                    Try
                        GSTTAXOFF = dt_txtCustomerTemp.Rows(0)("GSTTAXOFF")
                    Catch ex As Exception

                    End Try

                    If GSTTAXOFF Then
                        txtTaxPercentGST.Text = "0%"

                        taxdelivery_gst = 0
                        taxproduct_gst = 0
                        taxserviceRelya_gst = 0


                    End If


                End If

            End If



        End If

    End Sub


    Public Function SelectCustomerInformationDetail(ByVal cstid As String) As DataTable
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select * from   [CustomerInformation] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [CustomerID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = cstid.Trim

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



    Sub PopulatingTaxPercent()

        PopulatingTaxPercentValuesGSTPST()

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")
        Dim PopSalutations As New CustomOrder()
        Dim TaxPercent As Decimal = 0.0
        Dim TaxAmount As Decimal = 0.0

        Dim rs As SqlDataReader
        If drpPaymentType.SelectedValue <> "Wire In" Then
            rs = PopSalutations.BindingTaxPercent(CompanyID, DepartmentID, DivisionID, drpTaxes.SelectedValue)
            While rs.Read()
                txtTaxPercent.Text = rs("TaxPercent").ToString() + "%"
                TaxPercent = rs("TaxPercent")
            End While
        End If
        Dim ServiceCharge As Decimal
        Dim RelayCharge As Decimal
        Dim DeliveryCharge As Decimal

        If txtService.Text.Trim() <> "" Then
            If IsNumeric(txtService.Text) Then
                ServiceCharge = Convert.ToDecimal(txtService.Text)
            Else
                ServiceCharge = 0
            End If
        End If
        If txtRelay.Text.Trim() <> "" Then
            If IsNumeric(txtRelay.Text) Then
                RelayCharge = Convert.ToDecimal(txtRelay.Text)
            Else
                RelayCharge = 0
            End If


        End If
        If txtDelivery.Text.Trim() <> "" Then
            If IsNumeric(txtDelivery.Text.Trim()) Then
                DeliveryCharge = Convert.ToDecimal(txtDelivery.Text)
            Else
                DeliveryCharge = 0

            End If
        End If
        Dim objCustomer As New CustomOrder()
        Dim rs1 As SqlDataReader
        rs1 = objCustomer.CheckTaxable(CompanyID, DivisionID, DepartmentID)
        While rs1.Read()
            If rs1("DeliveryTaxable").ToString() <> "" Then


                If rs1("DeliveryTaxable").ToString() = True Then
                    SalesTax += DeliveryCharge
                End If
            End If
            If rs1("ServiceTaxable").ToString() <> "" Then


                If rs1("ServiceTaxable").ToString() = True Then
                    SalesTax += ServiceCharge
                End If
            End If
            If rs1("Internationaltaxable").ToString() <> "" Then

                If rs1("Internationaltaxable").ToString() = True Then
                    SalesTax += RelayCharge
                End If
            End If


        End While
        'Dim DiscountAmount As String = txtDiscountAmount.Text
        'Dim SubTotalString As String = txtSubtotal.Text
        'If SubTotalString <> "" Then
        '    TaxAmount = (TaxPercent * (Convert.ToDecimal(SubTotalString) + SalesTax) / 100)
        'End If

        Dim DiscountAmount As String = txtDiscountAmount.Text
        Dim SubTotalString As String = txtSubtotal.Text


        Dim SubTotalString1 As Double = 0

        Try
            SubTotalString1 = Convert.ToDecimal(SubTotalString)
        Catch ex As Exception
            SubTotalString1 = 0
        End Try

        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Fleurish" Or CompanyID = "Greene and Greene" Or CompanyID = "Busseys,Inc.30125" Or CompanyID = "ExecutiveBaskets77057" Or CompanyID = "Demo Site-90210" Or CompanyID = "ChocolatesandPosies93611" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "Four Seasons Flowers" Then
            ''New code for adding subtotal by tax
            Dim FillSubTotalString As New CustomOrder()
            Dim ds As New Data.DataTable
            ds = FillSubTotalString.PopulateItemDetails_SubtotalbyTaxable(CompanyID, DepartmentID, DivisionID, lblOrderNumberData.Text)

            'txtDriverRouteInfo.Text = ds.Rows.Count
            'txtDriverRouteInfo.Text = txtDriverRouteInfo.Text & lblOrderNumberData.Text & " "
            'txtDriverRouteInfo.Text = txtDriverRouteInfo.Text & CompanyID & " "
            'txtDriverRouteInfo.Text = txtDriverRouteInfo.Text & DepartmentID & " "
            'txtDriverRouteInfo.Text = txtDriverRouteInfo.Text & DivisionID & " "

            Try
                SubTotalString1 = ds.Rows(0)(0)
            Catch ex As Exception
                SubTotalString1 = 0
            End Try
            ''New code for adding subtotal by tax
        End If

        Dim gstpst As Boolean = False
        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "Demo Site-90210" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
            txtTaxPercentGST.Visible = True
            txtTaxPercentPST.Visible = True
            txtTaxPST.Visible = True
            txtTaxGST.Visible = True
            drpTaxesPST.Visible = True
            drpTaxesGSTHST.Visible = True

            txtTaxGST.Text = Format((Convert.ToDecimal(txtTaxPercentGST.Text.Replace("%", "")) * (Convert.ToDecimal(SubTotalString1) + SalesTax - DiscountAmount) / 100), "0.00")
            txtTaxPST.Text = Format((Convert.ToDecimal(txtTaxPercentPST.Text.Replace("%", "")) * (Convert.ToDecimal(SubTotalString1) + SalesTax - DiscountAmount) / 100), "0.00")

            If IsNumeric(txtTaxPercentGST.Text.Replace("%", "")) And IsNumeric(txtTaxPercentPST.Text.Replace("%", "")) Then
                TaxPercent = Convert.ToDecimal(txtTaxPercentGST.Text.Replace("%", "")) + Convert.ToDecimal(txtTaxPercentPST.Text.Replace("%", ""))
            End If

            If txtPostedDate.Text.Trim <> "" Or True Then
                Dim dt As Date = "1/1/1900"
                Try
                    dt = txtPostedDate.Text
                Catch ex As Exception

                End Try
                If dt > "12/05/2013" Or txtPostedDate.Text.Trim = "" Then
                    Dim taxgstvalue As Decimal = 0
                    Dim taxpstvalue As Decimal = 0

                    taxgstvalue = (((ServiceCharge + RelayCharge) * taxserviceRelya_gst) / 100) + ((DeliveryCharge * taxdelivery_gst) / 100) + ((SubTotalString1 * taxproduct_gst) / 100)
                    taxpstvalue = (((ServiceCharge + RelayCharge) * taxserviceRelya_pst) / 100) + ((DeliveryCharge * taxdelivery_pst) / 100) + ((SubTotalString1 * taxproduct_pst) / 100)


                    txtTaxGST.Text = Format(taxgstvalue, "0.00")
                    txtTaxPST.Text = Format(taxpstvalue, "0.00")

                End If
            End If


            Try
            Catch ex As Exception

            End Try

            If IsNumeric(lblOrderNumberData.Text.Trim()) Then
                InsertCaTaxDetail(lblOrderNumberData.Text, txtTaxPercentGST.Text.Replace("%", ""), txtTaxGST.Text, txtTaxPercentPST.Text.Replace("%", ""), txtTaxPST.Text)
            End If

            gstpst = True

            drpTaxes.Visible = False
            txtTax.Visible = False
            txtTaxPercent.Visible = False
            txtTaxPercent.Text = TaxPercent
        Else

            txtTaxPercentGST.Visible = False
            txtTaxPercentPST.Visible = False
            txtTaxPST.Visible = False
            txtTaxGST.Visible = False
            drpTaxesPST.Visible = False
            drpTaxesGSTHST.Visible = False

	If TaxPercent <> 0.0 Then
                '' New code for tax by zipcode
                Dim dttax As New DataTable
                If txtShippingZip.Text.Trim <> "" Then
                    dttax = SelectZipTaxDetail(txtShippingZip.Text)
                End If

                If dttax.Rows.Count <> 0 Then

                    Dim TaxPerc As Double = 0

                    Try
                        TaxPerc = dttax.Rows(0)("TaxPerc")
                    Catch ex As Exception

                    End Try


                    Try
                        drpTaxes.SelectedIndex = 1 'drpTaxes.Items.IndexOf(drpTaxes.Items.FindByValue("Miami Dade County"))
                        'txtInternalNotes.Text = drpTaxes.Items.IndexOf(drpTaxes.Items.FindByValue("Miami Dade County"))
                    Catch ex As Exception

                    End Try


                    txtTaxPercent.Text = Format(TaxPerc, "0.00") + "%"
                    TaxPercent = Format(TaxPerc, "0.00")
                End If
                '' New code for tax by zipcode
        End If


        drpTaxes.Visible = True
        txtTax.Visible = True
        txtTaxPercent.Visible = True

        End If
        If SubTotalString <> "" Then
            TaxAmount = (TaxPercent * (Convert.ToDecimal(SubTotalString1) + SalesTax - DiscountAmount) / 100)
        End If

        If gstpst Then
            Try
                TaxAmount = Convert.ToDecimal(txtTaxGST.Text) + Convert.ToDecimal(txtTaxPST.Text)
            Catch ex As Exception

            End Try
        End If

        ' txtTax.Text = Math.Round(TaxAmount, 2)
        txtTax.Text = Format(TaxAmount, "0.00")


        'Dim DiscountAmount As String = txtDiscountAmount.Text

        If SubTotalString <> "" Then

            Dim GrandTotal As Decimal = Convert.ToDecimal(SubTotalString) + TaxAmount + ServiceCharge + RelayCharge + DeliveryCharge - DiscountAmount

            txtTotal.Text = Format(GrandTotal, "0.00")

        End If
        SalesTax = 0.0
        rs1.Close()


        If drpTransaction.SelectedValue = "Wire In" Or drpTransaction.SelectedValue = "Wire_In" Then
            Dim obj As New clsOrderEntryFormWireIn
            obj.CompanyID = CompanyID
            obj.DepartmentID = DepartmentID
            obj.DivisionID = DivisionID
            obj.OrderNumber = lblOrderNumberData.Text

            If IsNumeric(txtDelivery.Text.Trim) Then
                obj.DeliveryAmount = txtDelivery.Text
            Else
                obj.DeliveryAmount = 0
            End If
            'obj.UpdateDiscountFromDeliveryItemWise()
        End If



    End Sub
#End Region



    Public Function SelectZipTaxDetail(ByVal zipcode As String) As DataTable
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select * from   TaxByZip Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND ZipCode=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = zipcode

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


    Public Function SelectCaTaxDetail(ByVal OrderNumber As String) As DataTable
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select * from   CATaxByOrderNumber Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND OrderNumber=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = OrderNumber

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


    Public Function InsertCaTaxDetail(ByVal OrderNumber As String, ByVal TaxGSTPerc As Decimal, ByVal TaxGSTValue As Decimal, ByVal TaxPSTPerc As Decimal, ByVal TaxPSTValue As Decimal) As Boolean




        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Update  [OrderHeader] SET  [ShippingZone]=@ShippingZone, [TaxGSTPerc] = @TaxGSTPerc " _
        & ",[TaxGSTValue]= @TaxGSTValue , [TaxPSTPerc] = @TaxPSTPerc , [TaxPSTValue] =@TaxPSTValue Where CompanyID = @CompanyID AND  DivisionID = @DivisionID AND  DepartmentID = @DepartmentID AND OrderNumber =@OrderNumber "

        'txtInternalNotes.Text = qry
        'txtInternalNotes.Text += txtInternalNotes.Text & Me.CompanyID
        'txtInternalNotes.Text += txtInternalNotes.Text & Me.DivisionID
        'txtInternalNotes.Text += txtInternalNotes.Text & Me.DepartmentID & " " & OrderNumber


        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = OrderNumber
            com.Parameters.Add(New SqlParameter("@TaxGSTPerc", SqlDbType.NVarChar, 36)).Value = TaxGSTPerc
            ''second line'''
            'EmployeeUserName, EmployeePassword, EmployeeName,AutoGenrate, ActiveYN
            com.Parameters.Add(New SqlParameter("@TaxGSTValue", SqlDbType.NVarChar, 15)).Value = TaxGSTValue
            com.Parameters.Add(New SqlParameter("@TaxPSTPerc", SqlDbType.NVarChar, 15)).Value = TaxPSTPerc
            com.Parameters.Add(New SqlParameter("@TaxPSTValue", SqlDbType.NVarChar, 50)).Value = TaxPSTValue

            com.Parameters.Add(New SqlParameter("@ShippingZone", SqlDbType.NVarChar, 50)).Value = drpZone.SelectedValue

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Public Function UpdateCaTaxDetail(ByVal OrderNumber As String, ByVal TaxGSTPerc As Decimal, ByVal TaxGSTValue As Decimal, ByVal TaxPSTPerc As Decimal, ByVal TaxPSTValue As Decimal) As Boolean

      

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Update  [CATaxByOrderNumber] SET   [TaxGSTPerc] = TaxGSTPerc " _
        & ",[TaxGSTValue]= @TaxGSTValue , [TaxPSTPerc] = @TaxPSTPerc , [TaxPSTValue] =@TaxPSTValue Where CompanyID = @CompanyID AND  DivisionID = @DivisionID AND  DepartmentID = @DepartmentID AND OrderNumber =@OrderNumber "

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = OrderNumber
            com.Parameters.Add(New SqlParameter("@TaxGSTPerc", SqlDbType.NVarChar, 36)).Value = TaxGSTPerc
            ''second line'''
            'EmployeeUserName, EmployeePassword, EmployeeName,AutoGenrate, ActiveYN
            com.Parameters.Add(New SqlParameter("@TaxGSTValue", SqlDbType.NVarChar, 15)).Value = TaxGSTValue
            com.Parameters.Add(New SqlParameter("@TaxPSTPerc", SqlDbType.NVarChar, 15)).Value = TaxPSTPerc
            com.Parameters.Add(New SqlParameter("@TaxPSTValue", SqlDbType.NVarChar, 50)).Value = TaxPSTValue

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



#Region "WireServiceTransmissionPriority"
    Public Sub PopulateWireServicesPriority()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopWireServices As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopWireServices.PopulateWireServicesPriority(CompanyID, DepartmentID, DivisionID)

        drpWireoutPriority.DataTextField = "WireServicePriorityID"
        drpWireoutPriority.DataValueField = "WireServicePriorityID"
        drpWireoutPriority.DataSource = rs

        drpWireoutPriority.DataBind()
        rs.Close()

        'New code for wire out
        Dim lstWireoutPriority As New ListItem
        lstWireoutPriority.Text = "-Select-"
        lstWireoutPriority.Value = ""
        drpWireoutPriority.Items.Insert(0, lstWireoutPriority)
        'New code for wire out


    End Sub

#End Region

#Region "WireServiceTransmissionMethod"
    Public Sub PopulateWireServiceTransmissionMethod()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)


        CompanyID = SessionKey("CompanyID")
        DivisionID = SessionKey("DivisionID")
        DepartmentID = SessionKey("DepartmentID")

        Dim PopWireServices As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopWireServices.PopulateWireServiceTransmissionMethod(CompanyID, DepartmentID, DivisionID)

        drpWireoutTransMethod.DataTextField = "WireServiceTransmisionID"
        drpWireoutTransMethod.DataValueField = "WireServiceTransmisionID"
        drpWireoutTransMethod.DataSource = rs

        drpWireoutTransMethod.DataBind()
        rs.Close()

        'New code for wire out
        Dim lstWireoutTransMethod As New ListItem
        lstWireoutTransMethod.Text = "-Select-"
        lstWireoutTransMethod.Value = ""
        drpWireoutTransMethod.Items.Insert(0, lstWireoutTransMethod)
        'New code for wire out

    End Sub

#End Region

#Region "PopulateOrderSourceCode"
    Public Sub PopulateOrderSourceCode()
        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID


        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")



        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderType.PopulateOrderSourceCodeID(CompanyID, DepartmentID, DivisionID)
        drpSource.DataTextField = "SourceCodeID"
        drpSource.DataValueField = "SourceCodeID"
        drpSource.DataSource = rs
        drpSource.DataBind()
        drpSource.Items.Insert(0, (New ListItem("-Select-", "0")))
        rs.Close()
    End Sub
#End Region


#Region "BindGrid"
    Sub BindGrid()


        If lblOrderNumberData.Text.Trim() = "(New)" Then
            OrderDetailGrid.DataBind()
            Exit Sub
        End If

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)

        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        Dim OrdNumber As String

        OrdNumber = lblOrderNumberData.Text.Trim()

        Dim FillItemDetailGrid As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = FillItemDetailGrid.PopulateItemDetailsGrid(CompanyID, DepartmentID, DivisionID, OrdNumber)

        '-- Store Dataset ds in Session
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Session("DataSetCount") = ds.Tables(0).Rows.Count
        OrderDetailGrid.DataSource = ds
        OrderDetailGrid.DataBind()

        If Request.QueryString("OrderNumber") <> "" Then
        Else
            CalculationPart()
        End If


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


            tr = tr & "<tr><td>" & ds.Tables(0).Rows(n)("OrderQty") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("ItemID") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("ItemName") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("Description") & "</td>"
            tr = tr & "<td>" & Format(ds.Tables(0).Rows(n)("ItemUnitPrice"), "0.00") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("DiscountPerc") & "</td>"
            tr = tr & "<td>" & ds.Tables(0).Rows(n)("DiscountFlatOrPercent") & "</td>"
            tr = tr & "<td>" & Format(ds.Tables(0).Rows(n)("SubTotal"), "0.00") & "</td>"
            tr = tr & "<td><a class='edit btn btn-info btn-block btn-xs' href=''>Edit</a></td><td><a class='delete btn btn-danger btn-block btn-xs' href=''>Delete</a></td>"
            tr = tr & "<td><input type=""hidden"" value=""" & ds.Tables(0).Rows(n)("OrderLineNumber") & """></td></tr>"

            txtfirst.Text = 0

        Next
        tbody.InnerHtml = tr

        CalculationPart()
        PopulatingTaxPercent()

    End Sub



#End Region

#Region "CalculationPart"
    Protected Sub CalculationPart()
        Dim FillItemDetailGrid As New CustomOrder()
        Dim dsTotal As SqlDataReader
        OrdNumber = lblOrderNumberData.Text.Trim()
        dsTotal = FillItemDetailGrid.PopulateSubTotal(CompanyID, DepartmentID, DivisionID, OrdNumber)
        Dim SubTotal As Decimal

        Dim Total As Decimal
        Dim Handling As Decimal
        Dim Freight As Decimal
        Dim Discount As Decimal
        Dim TaxAmount As Decimal
        Dim TaxPercent As Decimal
        Dim SubTotalToDisplay As Decimal

        While dsTotal.Read()
            SubTotalToDisplay = dsTotal("SubTotal")
            SubTotal = dsTotal("Total")
            Freight = dsTotal("Freight")
            Handling = dsTotal("Handling")
            Discount = dsTotal("DiscountAmount")
            TaxAmount = dsTotal("TaxAmount")
            TaxPercent = dsTotal("TaxPercent")
        End While

        txtSubtotal.Text = Format(SubTotalToDisplay, "0.00")
        If drpPaymentType.SelectedValue = "Wire In" Then
            txtTax.Text = Format(TaxAmount, "0.00")
            txtTaxPercent.Text = Format(TaxPercent, "0.00")
        Else
            TaxAmount = 0.0
            TaxPercent = 0.0

            txtTax.Text = Format(TaxAmount, "0.00")
            txtTaxPercent.Text = Format(TaxPercent, "0.00")
        End If

        Dim DiscountText As String
        If txtDiscounts.Text <> "" Then
            DiscountText = txtDiscounts.Text.Replace("%", "")
        Else
            DiscountText = 0.0
        End If

        If (Me.CompanyID = "FieldOfFlowers" Or Me.CompanyID = "FieldOfFlowersTraining") Then
            If drpShipMethod.SelectedValue = "Wire_Out" Then
                DiscountText = 0.0
            End If
        End If


        Discount = SubTotalToDisplay * ((Convert.ToDecimal(DiscountText)) / 100)
        'txtDiscountAmount.Text = Format(Discount, "0.00")

        Dim discountbycoupen As Double = 0
        Try
            discountbycoupen = lblDiscountCodeAmount.Text
        Catch ex As Exception

        End Try

        txtDiscountAmount.Text = Format((discountbycoupen + Discount), "0.00")

        If checkDiscounts.Checked Then

            If IsNumeric(txtdiscounttypevalue.Text) Then
                If drpdiscounttype.SelectedValue = "%" Then
                    Discount = SubTotalToDisplay * ((Convert.ToDecimal(txtdiscounttypevalue.Text)) / 100)
                End If

                If drpdiscounttype.SelectedValue = "Flat" Then
                    Discount = (Convert.ToDecimal(txtdiscounttypevalue.Text))
                End If
            End If


            txtDiscountAmount.Text = Format((Discount), "0.00")
        Else
            txtDiscountAmount.Text = Format((discountbycoupen + Discount), "0.00")

        End If

        'Edited by Jacob on 24-01-2008
        txtTotal.Text = Math.Round(((SubTotal + Freight + TaxAmount + Handling) - discountbycoupen + Discount), 2)
        dsTotal.Close()


    End Sub

#End Region

#Region "txtShippingZip_TextChanged"


    Public Function LoadLocationandZoneForZipDetails(ByVal ZipID As String) As Boolean

        'If (Me.CompanyID <> "FieldOfFlowersTraining")  Then
        '    Return True
        'End If

        If (Me.CompanyID = "FieldOfFlowersTraining") or (Me.CompanyID = "FieldOfFlowers") Then
            
            If cmblocationid.SelectedValue = "Wholesale" Then
                Return True
            End If

        If (Me.CompanyID = "FieldOfFlowers" Or Me.CompanyID = "FieldOfFlowersTraining") Then

            Dim sender As New Object
            Dim e As New System.EventArgs

            Dim dt_ZIP As New DataTable()
            dt_ZIP = PopulateLocationandZoneForZipDetails(txtShippingZip.Text)
            If dt_ZIP.Rows.Count <> 0 Then
                Dim LocationID As String = ""
                Dim ZoneID As String = ""
                Try
                    LocationID = dt_ZIP.Rows(0)("LocationID")
                Catch ex As Exception

                End Try

                Try
                    ZoneID = dt_ZIP.Rows(0)("ZoneID")
                Catch ex As Exception

                End Try

                    If LocationID <> "" Then
                        ' txtInternalNotes.Text = txtInternalNotes.Text & "--" & txtCustomerTypeID.Text
                        Try
                            If txtCustomerTypeID.Text = "WHO" Or txtCustomerTypeID.Text = "WHOC" Then
                                cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue("Wholesale"))
                                cmblocationid_SelectedIndexChanged(sender, e)
                            Else
                                cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(LocationID))
                                cmblocationid_SelectedIndexChanged(sender, e)
                            End If

                        Catch ex As Exception

                        End Try

                    End If

                If ZoneID <> "" Then
                    Try
                        drpZone.SelectedIndex = drpZone.Items.IndexOf(drpZone.Items.FindByValue(ZoneID))
                        drpZone_SelectedIndexChanged(sender, e)
                    Catch ex As Exception

                    End Try

                End If

            End If
        End If

End If
        Return True

    End Function

    Public Function PopulateLocationandZoneForZipDetails(ByVal ZipID As String) As DataTable

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim neworderentryform As Boolean = False
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT  LocationID, ZipCode, ZoneID FROM   Order_Location_Zip_Preference   where  CompanyID='" & Me.CompanyID & "' AND  [DivisionID]='" & Me.DivisionID & "' AND  [DepartmentID]='" & Me.DepartmentID & "' AND  [ZipCode]='" & ZipID & "'"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)



            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)

        End Try
        Return dt
    End Function



    Protected Sub txtShippingZip_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShippingZip.TextChanged

        ''Response.Write(txtShippingZip.Text)
        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "BrownstheFloristLtdV8W1G9" Or CompanyID = "TrainingBrownsTheFlorist" Or CompanyID = "Greene and Greene" Or CompanyID = "Quickflora-Canada" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-CanadaTest" Or CompanyID = "Quickflora-USATest" Then
            If drpZone.SelectedValue.ToString() = "0" Then
                If txtShippingZip.Text <> "" Then
                    Dim Zip As String = txtShippingZip.Text
                    ShippingDeliveryCharge(Zip)
                End If
                Session("CntlFromPostBackTrue") = "True"
                CheckItemGridOnPostBacks()
            End If
        Else
            If txtShippingZip.Text <> "" Then
                Dim Zip As String = txtShippingZip.Text
                ShippingDeliveryCharge(Zip)
            End If
            Session("CntlFromPostBackTrue") = "True"
            CheckItemGridOnPostBacks()
        End If


        LoadLocationandZoneForZipDetails(txtShippingZip.Text)

    End Sub
#End Region

#Region "ShippingDeliveryCharge"



    Public Function checkInventoryByfreeDelivery() As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT [OrderNumber] ,[freeDelivery] FROM [Enterprise].[dbo].[InventoryItems] Inner Join [OrderDetail] on  [InventoryItems].[CompanyID] = [OrderDetail].[CompanyID] AND   [InventoryItems].[DivisionID] = [OrderDetail].[DivisionID] AND  [InventoryItems].[DepartmentID] = [OrderDetail].[DepartmentID] AND  [InventoryItems].[ItemID] = [OrderDetail].[ItemID] "
        ssql = ssql & "  Where [InventoryItems].[CompanyID] = @CompanyID AND   [InventoryItems].[DivisionID] = @DivisionID AND  [InventoryItems].[DepartmentID] = @DepartmentID AND  [InventoryItems].[freeDelivery] = 1 AND  [OrderDetail].[OrderNumber] = @OrderNumber  "

        'ssql = "select * from InventoryByWarehouse  where CompanyID=@CompanyID and DivisionID=@DivisionID and DepartmentID=@DepartmentID and ItemID=@ItemID"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text.Trim

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False

        End Try
        Return False
    End Function



    Public Sub ShippingDeliveryChargeNew(ByVal ZIp As String)




        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")



        Dim chkdl As Boolean = False

        chkdl = checkInventoryByfreeDelivery()
        If chkdl Then
            If chkDeliveryOverride.Checked = False Then
                txtDelivery.Text = "0.00"
            End If

            If chkServiceOverride.Checked = False Then
                txtService.Text = "0.00"
            End If

            If chkRelayOverride.Checked = False Then
                txtRelay.Text = "0.00"
            End If


            WireOutPanelDisplay()
            CalculationPart()
            PopulatingTaxPercent()
            Exit Sub
        End If





        Dim objCustomer As New CustomOrder()
        Dim retailerCountry As String = ""

        Dim rsShipCountry As SqlDataReader
        rsShipCountry = objCustomer.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)
        If rsShipCountry.HasRows Then
            While rsShipCountry.Read
                retailerCountry = rsShipCountry("CompanyCountry").ToString()
            End While
        End If
        Dim result As DataTable
        If (ZIp.Trim().Length.ToString() > 5 And drpShipCountry.SelectedValue.ToUpper() = "US") Then
            ZIp = ZIp.Substring(0, 5)
        End If
        ''new coded added
        Dim DefaultLocalDeliveryCharge As Decimal = 0.0
        Dim DefaultServiceRelayCharge As Decimal = 0.0
        Dim DefaultInternationalDeliveryCharges As Decimal = 0.0
        Dim InternationalDeliveryServiceCharge As Decimal = 0.0

        Dim ZIPDeliverCharge As Decimal = 0.0


        Dim obj As New clsDeliveyByCountry
        obj.CompanyID = CompanyID
        obj.DepartmentID = DepartmentID
        obj.DivisionID = DivisionID
        obj.ZIPCODE = ZIp

        Dim dtdefault As New DataTable
        dtdefault = obj.FillDefaultDeliveryCharge

        ''New code added for Manuall value if needed
        Dim IsAddressDefault As Boolean = True
        Dim IsServiceChargesManual As Boolean = False
        Dim IsRelayChargesManual As Boolean = False
        Dim IsDeliveryChargesManual As Boolean = False
        If dtdefault.Rows.Count <> 0 Then
            Try
                IsAddressDefault = dtdefault.Rows(0)("IsAddressDefault")
            Catch ex As Exception
            End Try
            Try
                IsServiceChargesManual = dtdefault.Rows(0)("IsServiceChargesManual")
            Catch ex As Exception
            End Try
            Try
                IsRelayChargesManual = dtdefault.Rows(0)("IsRelayChargesManual")
            Catch ex As Exception
            End Try
            Try
                IsDeliveryChargesManual = dtdefault.Rows(0)("IsDeliveryChargesManual")
            Catch ex As Exception
            End Try
        End If
        ''New code added for Manuall value if needed


        

      

  'Edited by Imtiyaz on 15-04-2009

        If drpShipMethod.SelectedValue = "Pick Up" Or drpShipMethod.SelectedValue = "Taken" Or drpShipMethod.SelectedValue.ToLower = ("Curbside").ToLower Then
            If Not IsDeliveryChargesManual And chkDeliveryOverride.Checked = False Then
                txtDelivery.Text = "0.00"
            End If

            If Not IsServiceChargesManual And chkServiceOverride.Checked = False Then
                txtService.Text = "0.00"
            End If

            If Not IsRelayChargesManual And chkRelayOverride.Checked = False Then
                txtRelay.Text = "0.00"
            End If
            WireOutPanelDisplay()
            CalculationPart()
            PopulatingTaxPercent()
            Exit Sub
        End If




        If dtdefault.Rows.Count <> 0 Then
            Try
                DefaultLocalDeliveryCharge = dtdefault.Rows(0)("LocalDeliveryCharge")
            Catch ex As Exception

            End Try

            Try
                DefaultServiceRelayCharge = dtdefault.Rows(0)("ServiceCharge")
            Catch ex As Exception

            End Try

            Try
                DefaultInternationalDeliveryCharges = dtdefault.Rows(0)("DefaultInternationalDeliveryCharges")
            Catch ex As Exception

            End Try
            InternationalDeliveryServiceCharge = dtdefault.Rows(0)("InternationalDeliveryCharge")
            Try

            Catch ex As Exception

            End Try



        End If


        Dim dtzipcode As New DataTable
        dtzipcode = obj.FillDeliveryByZipCode

        If dtzipcode.Rows.Count <> 0 Then


	'lblCCMessage.Visible = True
       ' lblCCMessage.Text = " Reached IsDeliveryChargesManual =" & IsDeliveryChargesManual 


            ZIPDeliverCharge = dtzipcode.Rows(0)("EveryDayRate")
            Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Local_Truck")
            drpShipMethod.ClearSelection()
            'drpTransaction.ClearSelection()
            If Not Locallist Is Nothing Then
                drpShipMethod.Items.FindByValue("Local_Truck").Selected = True
                'drpTransaction.SelectedIndex = drpTransaction.Items.IndexOf(drpTransaction.Items.FindByValue("Order"))

            End If

            If Not IsDeliveryChargesManual And chkDeliveryOverride.Checked = False Then
                txtDelivery.Text = String.Format("{0:n2}", ZIPDeliverCharge)

            End If

            If Not IsServiceChargesManual And chkServiceOverride.Checked = False Then
                txtService.Text = "0.00"
            End If


            If Not IsRelayChargesManual And chkRelayOverride.Checked = False Then
                txtRelay.Text = "0.00"
            End If

 
         

        Else

            If drpTransaction.SelectedValue.ToLower = "wire in" Or drpTransaction.SelectedValue.ToLower = "wire_in" Then

                If Not IsDeliveryChargesManual And chkDeliveryOverride.Checked = False Then
                    txtDelivery.Text = String.Format("{0:n2}", DefaultLocalDeliveryCharge)
                End If

                If Not IsServiceChargesManual And chkServiceOverride.Checked = False Then
                    txtService.Text = "0.00"
                End If


                If Not IsRelayChargesManual And chkRelayOverride.Checked = False Then
                    txtRelay.Text = "0.00"
                End If

            Else
                Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Wire_Out")
                If Not Locallist Is Nothing Then
                    
                    If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then

                    Else
                        drpShipMethod.ClearSelection()
                        'drpTransaction.ClearSelection()
                        drpShipMethod.Items.FindByValue("Wire_Out").Selected = True
                        'drpTransaction.Items.FindByValue("Wire_Out").Selected = True
                    End If

                End If

                Dim dtCountryid As New DataTable
                obj.CountryID = drpShipCountry.SelectedValue
                dtCountryid = obj.FillDeliveryByCountryID

                If dtCountryid.Rows.Count <> 0 Then

                    If Not IsDeliveryChargesManual And chkDeliveryOverride.Checked = False Then
                        txtDelivery.Text = String.Format("{0:n2}", DefaultLocalDeliveryCharge)
                    End If

                    If Not IsServiceChargesManual And chkServiceOverride.Checked = False Then
                        txtService.Text = String.Format("{0:n2}", DefaultServiceRelayCharge)
                    End If

                    If Not IsRelayChargesManual And chkRelayOverride.Checked = False Then
                        txtRelay.Text = "0.00"
                    End If

                Else

                    If Not IsDeliveryChargesManual And chkDeliveryOverride.Checked = False Then
                        txtDelivery.Text = String.Format("{0:n2}", DefaultInternationalDeliveryCharges)
                    End If

                    If Not IsServiceChargesManual And chkServiceOverride.Checked = False Then
                        txtService.Text = String.Format("{0:n2}", InternationalDeliveryServiceCharge)
                    End If

                    If Not IsRelayChargesManual And chkRelayOverride.Checked = False Then
                        txtRelay.Text = "0.00"
                    End If

                End If
            End If



        End If




        Dim sender As New Object
        Dim e As New EventArgs

        'txtDelivery_TextChanged(sender, e)

        If CompanyID = "BranchingOutFloralL0E1E0" Then
            If txtDelivery.Text.trim = "" Then
                txtDelivery.Text = "10.00"
            End If

            Dim dl As Decimal = 0

            Try
                dl = txtDelivery.Text
            Catch ex As Exception

            End Try

            If dl = 0 Then
                txtDelivery.Text = "10.00"
            End If
        End If



        If CompanyID = "FieldOfFlowers" Or CompanyID = "FieldOfFlowersTraining" Then

            '' New code for delivery by customer
            Dim dtCST As New DataTable
            If txtCustomerTemp.Text.Trim <> "" Then
                dtCST = SelectCSTtypeDetail(txtCustomerTemp.Text)
            End If

            If dtCST.Rows.Count <> 0 Then

                Dim CustomerTypeID As String = ""

                Try
                    CustomerTypeID = dtCST.Rows(0)("CustomerTypeID")
                Catch ex As Exception

                End Try

                txtCustomerTypeID.Text = CustomerTypeID

                '' on Page load first time make it blank
                Session("CustomerTypeID") = CustomerTypeID

                If CustomerTypeID = "WHO" Or CustomerTypeID = "WHOC" Then
                    If chkDeliveryOverride.Checked = False Then
                        txtDelivery.Text = "10.00"
                    End If

                End If

                Dim ApplyDeliveryCharge As Boolean = False

                Try
                    ApplyDeliveryCharge = dtCST.Rows(0)("ApplyDeliveryCharge")
                Catch ex As Exception

                End Try

                If ApplyDeliveryCharge Then
                    Try
                        Dim dlchargeas As Decimal = 0
                        dlchargeas = dtCST.Rows(0)("DeliveryCharge")
                        dlchargeas = String.Format("{0:n2}", dlchargeas)
                        If chkDeliveryOverride.Checked = False Then
                            txtDelivery.Text = dlchargeas
                        End If

                    Catch ex As Exception

                    End Try
                End If

            End If
            '' New code for tax by zipcode
        End If

        WireOutPanelDisplay()
        CalculationPart()
        PopulatingTaxPercent()

        ''If CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "BrownstheFloristLtdV8W1G9" Or CompanyID = "TrainingBrownsTheFlorist" Or CompanyID = "Greene and Greene" Or CompanyID = "Quickflora-Canada" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-CanadaTest" Or CompanyID = "Quickflora-USATest" Then
        If Session("AllowZoneDeliveryChargeFilling") Then
            If Not drpZone.SelectedIndex <= 0 Then
                FillDeliveryByZone(drpZone.SelectedValue, cmblocationid.SelectedValue)
            End If
        End If

       

    End Sub

    Public Sub ShippingDeliveryCharge(ByVal ZIp As String)

        ShippingDeliveryChargeNew(ZIp)

        Exit Sub

        'Edited by Jacob on 24-01-2008

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim objCustomer As New CustomOrder()
        Dim retailerCountry As String = ""

        Dim rsShipCountry As SqlDataReader
        rsShipCountry = objCustomer.GettingRetailerCountryName(CompanyID, DepartmentID, DivisionID)
        If rsShipCountry.HasRows Then
            While rsShipCountry.Read

                retailerCountry = rsShipCountry("CompanyCountry").ToString()

            End While
        End If
        Dim DeliverCharge As Decimal = 0.0
        Dim WireOutCharge As Decimal = 0.0
        Dim SerCharge As Decimal = 0.0
        Dim RelCharge As Decimal = 0.0


        Dim result As DataTable
        If (ZIp.Trim().Length.ToString() > 5 And drpShipCountry.SelectedValue.ToUpper() = "US") Then

            ZIp = ZIp.Substring(0, 5)

        End If
        result = objCustomer.PopulateDeliveryChargeByZip(CompanyID, DivisionID, DepartmentID, ZIp)

        If result.Rows.Count > 0 Then
            DeliverCharge = result.Rows(0)("EveryDayrate").ToString()
            SerCharge = result.Rows(0)("ServiceCharge").ToString()
            RelCharge = result.Rows(0)("InternationalDeliveryCharges").ToString()
            WireOutCharge = result.Rows(0)("DefaultIncomingWireOrderCharge").ToString()

        End If

        If drpPaymentType.SelectedValue <> "Wire In" Then
            If retailerCountry = drpShipCountry.SelectedValue Then

                If DeliverCharge = 0.0 Then
                    txtService.Text = String.Format("{0:n2}", SerCharge)
                    txtDelivery.Text = "0.00"
                    txtRelay.Text = "0.00"
                    drpShipMethod.ClearSelection()

                    Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Wire_Out")
                    If Not Locallist Is Nothing Then

                        drpShipMethod.Items.FindByValue("Wire_Out").Selected = True
                    End If

                Else
                    txtDelivery.Text = String.Format("{0:n2}", DeliverCharge)
                    txtRelay.Text = "0.00"
                    txtService.Text = "0.00"
                    If retailerCountry = drpShipCountry.SelectedValue Then

                        drpShipMethod.ClearSelection()
                        Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Local_Truck")
                        If Not Locallist Is Nothing Then
                            drpShipMethod.Items.FindByValue("Local_Truck").Selected = True
                        End If

                    End If



                End If

            Else

                txtService.Text = String.Format("{0:n2}", RelCharge)
                txtDelivery.Text = "0.00"
                txtRelay.Text = "0.00"
                drpShipMethod.ClearSelection()

                Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Wire_Out")
                If Not Locallist Is Nothing Then
                    drpShipMethod.Items.FindByValue("Wire_Out").Selected = True
                End If



            End If

        Else
            If retailerCountry = drpShipCountry.SelectedValue Then

                txtDelivery.Text = String.Format("{0:n2}", WireOutCharge)
                txtService.Text = "0.00"
                txtRelay.Text = "0.00"
                drpShipMethod.ClearSelection()
                Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Local_Truck")
                If Not Locallist Is Nothing Then

                    drpShipMethod.Items.FindByValue("Local_Truck").Selected = True
                End If
            Else
                txtService.Text = String.Format("{0:n2}", RelCharge)
                txtDelivery.Text = "0.00"
                txtRelay.Text = "0.00"
                drpShipMethod.ClearSelection()

                Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Wire_Out")
                If Not Locallist Is Nothing Then

                    drpShipMethod.Items.FindByValue("Wire_Out").Selected = True
                End If


            End If
        End If

        WireOutPanelDisplay()
        CalculationPart()
        PopulatingTaxPercent()
    End Sub

#End Region

#Region "txtDelivery_TextChanged"
    Protected Sub txtDelivery_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDelivery.TextChanged

        CalculationPart()
        PopulatingTaxPercent()
        CheckItemGridOnPostBacks()
        'AddEditItemDetails()
        BindGrid()


    End Sub
#End Region

#Region "txtService_TextChanged"
    Protected Sub txtService_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtService.TextChanged

        CalculationPart()
        PopulatingTaxPercent()
        CheckItemGridOnPostBacks()
        BindGrid()

    End Sub
#End Region

#Region "txtRelay_TextChanged"

    Protected Sub txtRelay_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRelay.TextChanged

        CalculationPart()
        PopulatingTaxPercent()
        CheckItemGridOnPostBacks()
        BindGrid()

    End Sub
#End Region

#Region "WireOutPanelDisplay"
    Public Sub WireOutPanelDisplay()
        If drpShipMethod.Text = "Wire_Out" Or drpShipMethod.SelectedItem.Text = "Wire Out" Then
            pnlWireOutInformation.Visible = True
        Else

            pnlWireOutInformation.Visible = False
        End If
        Session("CntlFromPostBackTrue") = "True"


        CheckItemGridOnPostBacks()


    End Sub
#End Region

#Region "CheckItemGridOnPostBacks"
    Protected Sub CheckItemGridOnPostBacks()

        Orderbind = False



    End Sub
#End Region


#Region "drpPaymentType_SelectedIndexChanged"
    Protected Sub drpPaymentType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpPaymentType.SelectedIndexChanged


        Dim OrdNumber As String
        OrdNumber = lblOrderNumberData.Text


        ''Update Order Location Method''
        Dim objrderloaction As New clsOrder_Location
        objrderloaction.CompanyID = CompanyID
        objrderloaction.DivisionID = DivisionID
        objrderloaction.DepartmentID = DepartmentID
        objrderloaction.OrderNumber = OrdNumber
        objrderloaction.LocationID = cmblocationid.SelectedValue
        objrderloaction.UpdateOrderLocationID()

        Dim PaymentType As String = ""
        PaymentType = drpPaymentType.SelectedValue

        PaymentPanelChange(PaymentType)
        populateCustomerTypes()
        CheckItemGridOnPostBacks()

        If drpPaymentType.SelectedValue.ToLower = "EMV-Debit".ToLower Then
            'txtInternalNotes.Text = "Reach logged"

            Dim ordnm As String = ""

            Try
                ordnm = Request.QueryString("OrderNumber")
            Catch ex As Exception

            End Try

            'If ordnm <> "" Then
            'Else
            '    btnemv_Click(sender, e)
            'End If

            'pnlemv.Visible = True


            If CompanyID <> "BranchingOutFloralL0E1E0" Then
                If ordnm <> "" Then
                Else
                    btnemv_Click(sender, e)
                End If

                pnlemv.Visible = True
            End If



        Else
            pnlemv.Visible = False

        End If


        Dim Zip As String = txtShippingZip.Text
        If drpPaymentType.SelectedValue = "Wire In" Then
            drpWire.SelectedIndex = -1
            'drpShipMethod.Items.Clear()
            'drpShipMethod.Items.Insert(0, "Local Truck")
            'txtCustomerTemp.Visible = False
            'Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Local_Truck")
            'If Not Locallist Is Nothing Then
            '    drpShipMethod.Items.FindByValue("Local_Truck").Selected = True
            'End If
            'Else
            'If drpTransaction.SelectedValue <> "Wire_Out" Then
            '   PopulateDeliveryMethods()
            'End If
        End If


        If drpstoredcc.Items.Count = 1 Then
            If drpPaymentType.SelectedValue.ToLower = "Credit Card".ToLower Then

                Dim objCustomerCreditCards As New clsCustomerCreditCards
                objCustomerCreditCards.CompanyID = CompanyID
                objCustomerCreditCards.DivisionID = DivisionID
                objCustomerCreditCards.DepartmentID = DepartmentID
                If txtCustomerTemp.Text.Trim <> "" Then
                    objCustomerCreditCards.CustomerID = txtCustomerTemp.Text.Trim
                    Dim dtCustomerCreditCards As New DataTable
                    dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetails

                    drpstoredcc.Items.Clear()
                    drpstoredcc.Items.Add("Other")

                    If dtCustomerCreditCards.Rows.Count <> 0 Then
                        Dim n As Integer
                        For n = 0 To dtCustomerCreditCards.Rows.Count - 1
                            Dim lst As New ListItem
                            Dim crd As String
                            crd = dtCustomerCreditCards.Rows(n)("CreditCardNumber")

                            Try
                                If crd <> "" And txtCustomerTemp.Text.Trim <> "" Then
                                    crd = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(crd, txtCustomerTemp.Text.Trim)
                                End If
                            Catch ex As Exception
                            End Try

                            'Last number display X in Credit cards
                            Dim cardNo As String = ""
                            Dim cLen As Integer = 0
                            Dim subLen As Integer = 0
                            Dim SubcardNo As String = ""
                            cardNo = crd
                            cLen = cardNo.Length()
                            Dim slen As Integer = 0
                            If cLen > 0 Then
                                If cLen > 12 Then
                                    subLen = cLen - 12
                                    'SubcardNo = cardNo.Substring(0, subLen)
                                    SubcardNo = cardNo.Substring(12, subLen)
                                    slen = SubcardNo.Length()

                                    If slen > 4 Then
                                        SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                                    End If
                                    'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                                    cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                                Else
                                    cardNo = RepeatChar("X", cLen)
                                End If
                            End If


                            lst.Text = cardNo
                            lst.Value = dtCustomerCreditCards.Rows(n)("CardDetailLineNumber")
                            drpstoredcc.Items.Add(lst)



                        Next
                    End If

                    'drpstoredcc.DataSource = dtCustomerCreditCards
                    'drpstoredcc.DataTextField = "CreditCardNumber"
                    'drpstoredcc.DataValueField = "CardDetailLineNumber"
                    'drpstoredcc.DataBind()

                End If



            End If
        End If


        'ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("initInputHighlightScript();"), True)


    End Sub

#End Region

#Region "PaymentPanelChange"

    Sub CreditCardPanelChange()


        Dim obj As New clsPOSHarwares
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        Dim dt As New Data.DataTable

        dt = obj.FillDetails


        If dt.Rows.Count <> 0 Then
            If dt.Rows(0)("CardReader") = "XM90" Then
                pnlXM90.Visible = True
                pnlXM95.Visible = False
                ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("reswipe()"), True)
                txtrawcard.Focus()
            End If

            If dt.Rows(0)("CardReader") = "XM95" Then
                'rdposxXM95.Checked = True
                pnlXM90.Visible = False
                pnlXM95.Visible = True
                ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("OpenClaimEnable_Click()"), True)
            End If
        Else
            obj.CardReader = "XM95"

            pnlXM90.Visible = False
            pnlXM95.Visible = True
            obj.Insert()
            ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("reswipe()"), True)
        End If


    End Sub


    Sub PaymentPanelChange(ByVal PaymentType As String)
        PaymentType = PaymentType.ToLower()

        Select Case PaymentType
            Case "credit card"
                pnlCheck.Visible = False
                pnlCreditCard.Visible = True
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Credit Card Payment Information"
                CreditCardPanelChange()
            Case "gift card"
                pnlgiftCard.Visible = True
                pnlCheck.Visible = False
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Gift Card Payment Information"
            Case "amex"
                pnlCheck.Visible = False
                pnlCreditCard.Visible = True
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Credit Card Payment Information"
            Case "visa"
                pnlCheck.Visible = False
                pnlCreditCard.Visible = True
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Credit Card Payment Information"
            Case "master"
                pnlCheck.Visible = False
                pnlCreditCard.Visible = True
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Credit Card Payment Information"
            Case "cash"
                lblcashcheck.Text = "Amount Paid :"
                pnlCheck.Visible = True
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Payment Information"
            Case "check"
                lblcashcheck.Text = "Check #"
                pnlCheck.Visible = True
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Payment Information"
            Case "emv-debit"
                lblcashcheck.Text = "Payment Status"
                lblid.Text = "Approval No"
                pnlCheck.Visible = True
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Payment Information"
            Case "e check"
                pnlCheck.Visible = True
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Payment Information"
            Case "cod"
                pnlCheck.Visible = True
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = False
                lblTitle.Text = "Payment Information"

            Case "wire in"
                pnlCheck.Visible = False
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = True
                lblTitle.Text = "Wire In Payment Information"
            Case "wire"
                pnlCheck.Visible = False
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = False
                pnlWireService.Visible = True
                lblTitle.Text = "Wire In Payment Information"
            Case "0"
                pnlCheck.Visible = False
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = True
                pnlWireService.Visible = False
                lblTitle.Text = "Customer Profile"
            Case Else
                pnlCheck.Visible = False
                pnlCreditCard.Visible = False
                pnlCustomerProfile.Visible = True
                pnlWireService.Visible = False
                lblTitle.Text = "Customer Profile"
        End Select

    End Sub
#End Region



    Protected Sub btngiftcardbal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngiftcardbal.Click
        Dim dt As New DataTable
        Try
            Dim InLineNumber As String
            InLineNumber = Date.Now.Year & Date.Now.Month & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond

            Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim qry As String
            qry = "SELECT  *	FROM [GiftCards]	WHERE isnull([Active],0) = 1 and (isnull(Amount,0)-isnull(UsedAmount,0)) > 0   and  [CompanyID] = @CompanyID  and  [DivisionID] = @DivisionID and  [DepartmentID] = @DepartmentID and  [GiftCardNumber] = @GiftCardNumber  and AuthKey= @AuthKey "
            Dim com As SqlCommand

            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@GiftCardNumber", SqlDbType.NVarChar, 36)).Value = txtgiftcardnumber.Text
            com.Parameters.Add(New SqlParameter("@AuthKey", SqlDbType.NVarChar, 36)).Value = txtgiftcardpin.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)
            'txtWireNotes.Text = qry

            If dt.Rows.Count <> 0 Then
                Dim bal As Double = 0
                Dim amt As Double = 0
                Dim used As Double = 0
                Try
                    amt = dt.Rows(0)("Amount")
                Catch ex As Exception

                End Try
                Try
                    used = dt.Rows(0)("UsedAmount")
                Catch ex As Exception

                End Try

                Try
                    lblgiftcardInlinenumber.Text = dt.Rows(0)("Inlinenumber")
                Catch ex As Exception

                End Try

                lblgiftcardbalance.Text = amt - used

                Try
                    If (amt - used) >= txtTotal.Text Then
                        gifttr1.Visible = False
                        gifttr2.Visible = False
                        txtOtherAmount.Text = ""
                    Else
                        gifttr1.Visible = True
                        gifttr2.Visible = True
                        txtOtherAmount.Text = txtTotal.Text - (amt - used)
                    End If
                Catch ex As Exception

                End Try

               

                lblgiftcardexpdate.Text = dt.Rows(0)("ExpiryDate")
            Else
                lblgiftcardbalance.Text = "Please check Gift Card Details not valid or its Balance is zero."
                lblgiftcardbalance.ForeColor = Drawing.Color.Red
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            'txtWireNotes.Text = msg
        End Try
    End Sub

    'CheckGiftCardItemQty(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity)
    Public Function GetGiftCardItem(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemIDFromTextBox As String) As DataTable
        Dim dt As New DataTable
        Try

            Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim qry As String
            qry = "SELECT  *	FROM [GiftCards]	WHERE isnull([Active],0) = 0  and [ExpiryDate] >= GetDate() and  [CompanyID] = @CompanyID  and  [DivisionID] = @DivisionID and  [DepartmentID] = @DepartmentID and  [ItemID] = @ItemID "
            Dim com As SqlCommand

            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemIDFromTextBox

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            'txtWireNotes.Text = msg
        End Try
        Return dt
    End Function


    Public Function ActivateGiftCardForOrder(ByVal OrderNumber As String, ByVal itemid As String) As Boolean

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
        Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
        Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
        Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)

        Dim dt As New DataTable
        dt = GetGiftCardItem(CompanyID, DepartmentID, DivisionID, itemid)
        'Inlinenumber
        Dim Inlinenumber As Integer = 0

        If dt.Rows.Count <> 0 Then
            Inlinenumber = dt.Rows(0)("Inlinenumber")
        End If

        Dim giftcardnumber As String = ""
        Dim giftkey As String = ""
        Dim giftamount As String = ""
        Dim issuedate As String = ""
        Dim expdate As String = ""

        If dt.Rows.Count <> 0 Then
            Try
                giftcardnumber = dt.Rows(0)("GiftCardNumber")
            Catch ex As Exception

            End Try

            Try
                giftkey = dt.Rows(0)("AuthKey")
            Catch ex As Exception

            End Try

            Try
                giftamount = dt.Rows(0)("Amount")
            Catch ex As Exception

            End Try

            Try
                issuedate = Date.Now.Date
            Catch ex As Exception

            End Try

            Try
                expdate = Date.Now.Date.AddYears(10)

            Catch ex As Exception

            End Try
        End If

        Dim gft_obj As New clsGiftCard
        If txtCustomerEmail.Text.Trim <> "" Then

            gft_obj.SendGiftCardBookingEmail(CompanyID, DivisionID, DepartmentID, lblOrderNumberData.Text, giftcardnumber, giftkey, giftamount, issuedate, expdate, txtCustomerTemp.Text, txtCustomerFirstName.Text & " " & txtCustomerLastName.Text, txtCustomerEmail.Text)
        End If



        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "UPDATE [GiftCards] set [Active]=1,[IssueDate]=Getdate(),OrderNumber=@f4,[CustomerID]=@CustomerID Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And Inlinenumber=@Inlinenumber"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = OrderNumber
            com.Parameters.Add(New SqlParameter("@CustomerID", SqlDbType.NVarChar, 36)).Value = txtCustomerTemp.Text
            com.Parameters.Add(New SqlParameter("@Inlinenumber", SqlDbType.BigInt)).Value = Inlinenumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


    Public Function GetGiftCardApprovalnumber() As Boolean
        Dim dt As New DataTable
        Try

            Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim qry As String
            qry = "SELECT  *	FROM [Order_GiftCardApproval]	WHERE [CompanyID] = @CompanyID  and  [DivisionID] = @DivisionID and  [DepartmentID] = @DepartmentID and  [OrderNumber] = @OrderNumber "
            Dim com As SqlCommand

            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Try
                    txtgiftcardnumber.Text = dt.Rows(0)("GiftCardNumber")
                    txtgiftcardpin.Text = dt.Rows(0)("AuthKey")
                    txtgiftcardaprovalnumber.Text = dt.Rows(0)("ApprovalNumber")
                    Try
                        txtOtherAmount.Text = dt.Rows(0)("OtherAmount")

                    Catch ex As Exception
                        txtOtherAmount.Text = "0"
                    End Try
                    Try
                        drpOtherPaymentby.Text = dt.Rows(0)("OtherPaymentType")
                    Catch ex As Exception

                    End Try
                    Dim sender As New Object
                    Dim e As New Object
                    btngiftcardbal_Click(sender, e)
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            'txtWireNotes.Text = msg
        End Try
        Return True
    End Function



    Public Function InsertApprovalGiftCardForOrder() As Boolean

        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "INSERT INTO  Order_GiftCardApproval (CompanyID,DivisionID,DepartmentID,OrderNumber,GiftCardNumber,AuthKey,Amount,ApprovalNumber,OtherAmount,OtherPaymentType) values(@f1,@f2,@f3,@OrderNumber,@GiftCardNumber,@AuthKey,@Amount,@ApprovalNumber,@OtherAmount,@OtherPaymentType)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text
            com.Parameters.Add(New SqlParameter("@GiftCardNumber", SqlDbType.NVarChar, 36)).Value = txtgiftcardnumber.Text
            com.Parameters.Add(New SqlParameter("@AuthKey", SqlDbType.NVarChar, 36)).Value = txtgiftcardpin.Text


            com.Parameters.Add(New SqlParameter("@ApprovalNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text & "-" & Date.Now.Year.ToString & Date.Now.Month & Date.Now.Day & Date.Now.Hour
            If IsNumeric(txtOtherAmount.Text) Then
                com.Parameters.Add(New SqlParameter("@OtherAmount", SqlDbType.Money)).Value = txtOtherAmount.Text
                com.Parameters.Add(New SqlParameter("@Amount", SqlDbType.Money)).Value = (txtTotal.Text - txtOtherAmount.Text)
            Else
                com.Parameters.Add(New SqlParameter("@OtherAmount", SqlDbType.Money)).Value = 0
                com.Parameters.Add(New SqlParameter("@Amount", SqlDbType.Money)).Value = txtTotal.Text
            End If



            com.Parameters.Add(New SqlParameter("@OtherPaymentType", SqlDbType.NVarChar, 36)).Value = drpOtherPaymentby.Text

            txtgiftcardaprovalnumber.Text = lblOrderNumberData.Text & "-" & Date.Now.Year.ToString & Date.Now.Month & Date.Now.Day & Date.Now.Hour

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



    Public Function UpdateApprovalGiftCardForOrder() As Boolean

        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "UPDATE Order_GiftCardApproval SET Amount=@Amount, ApprovalNumber =@ApprovalNumber  where  CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@OrderNumber "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text
            com.Parameters.Add(New SqlParameter("@GiftCardNumber", SqlDbType.NVarChar, 36)).Value = txtgiftcardnumber.Text
            com.Parameters.Add(New SqlParameter("@AuthKey", SqlDbType.NVarChar, 36)).Value = txtgiftcardpin.Text
            com.Parameters.Add(New SqlParameter("@Amount", SqlDbType.Money)).Value = txtTotal.Text
            com.Parameters.Add(New SqlParameter("@ApprovalNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text & "-" & Date.Now.Year.ToString & Date.Now.Month & Date.Now.Day & Date.Now.Hour

            txtgiftcardaprovalnumber.Text = lblOrderNumberData.Text & "-" & Date.Now.Year.ToString & Date.Now.Month & Date.Now.Day & Date.Now.Hour

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



    Public Function InsertApprovalGiftCardForOrderLogs(ByVal amnt As Double) As Boolean

        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "INSERT INTO  [Order_GiftCardApproval_Logs] (CompanyID,DivisionID,DepartmentID,OrderNumber,GiftCardNumber,AuthKey,Amount,ApprovalNumber) values(@f1,@f2,@f3,@OrderNumber,@GiftCardNumber,@AuthKey,@Amount,@ApprovalNumber)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text
            com.Parameters.Add(New SqlParameter("@GiftCardNumber", SqlDbType.NVarChar, 36)).Value = txtgiftcardnumber.Text
            com.Parameters.Add(New SqlParameter("@AuthKey", SqlDbType.NVarChar, 36)).Value = txtgiftcardpin.Text
            com.Parameters.Add(New SqlParameter("@Amount", SqlDbType.Money)).Value = amnt
            com.Parameters.Add(New SqlParameter("@ApprovalNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text & "-" & Date.Now.Year.ToString & Date.Now.Month & Date.Now.Day & Date.Now.Hour

            txtgiftcardaprovalnumber.Text = lblOrderNumberData.Text & "-" & Date.Now.Year.ToString & Date.Now.Month & Date.Now.Day & Date.Now.Hour

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



    Public Function CheckGiftCardForOrderSecondBook() As Boolean
        Dim dt As New DataTable
        Dim ordbal As Double = 0

        Try

            Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim qry As String
            qry = "SELECT  *  FROM [Order_GiftCardApproval]	WHERE [CompanyID] = @CompanyID  and  [DivisionID] = @DivisionID and  [DepartmentID] = @DepartmentID and  [OrderNumber] = @OrderNumber "
            Dim com As SqlCommand

            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text

            Dim da As New SqlDataAdapter(com)

            da.Fill(dt)

            If dt.Rows.Count <> 0 Then
                Try
                    ordbal = dt.Rows(0)("Amount")
                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            'txtWireNotes.Text = msg
        End Try

        Dim curbal As Double = 0

        Try
            curbal = txtTotal.Text
        Catch ex As Exception

        End Try

        'txtInternalNotes.Text = "" & ordbal.ToString & "~~~~~ " & curbal.ToString & " (ordbal - curbal)" & (ordbal - curbal).ToString
        'Return False

        If (ordbal - curbal) = 0 Then
            Return True
        Else

            If (ordbal - curbal) > 0 Then
                UpdateBalanceNegativeGiftCardForOrder(ordbal - curbal)
                UpdateApprovalGiftCardForOrder()
                InsertApprovalGiftCardForOrderLogs(curbal - ordbal)
            Else
                Dim sender As New Object
                Dim e As New System.EventArgs

                btngiftcardbal_Click(sender, e)

                Dim bal As Double = 0
                Dim total As Double = 0
                Try
                    bal = lblgiftcardbalance.Text.Trim
                Catch ex As Exception

                End Try

                Dim OtherAmount As Double = 0
                Try
                    OtherAmount = txtOtherAmount.Text.Trim
                Catch ex As Exception

                End Try
                bal = bal + OtherAmount

                Try
                    total = txtTotal.Text.Trim
                Catch ex As Exception

                End Try

                If bal < (total - ordbal) Then
                    lblCCMessage.Text = "Please check that Gift Card don't have sufficient balance to process order."
                    lblCCMessage.Visible = True
                    lblCCMessage.ForeColor = Drawing.Color.Red
                    Return False
                Else
                    UpdateBalanceGiftCardForOrder(curbal - ordbal)
                    UpdateApprovalGiftCardForOrder()
                    InsertApprovalGiftCardForOrderLogs(curbal - ordbal)
                End If
            End If


        End If

        Return True
    End Function



    Public Function UpdateBalanceNegativeGiftCardForOrder(ByVal amnt As Double) As Boolean

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
        Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
        Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
        Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)


        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "UPDATE [GiftCards] set [UsedAmount]= ( Isnull(UsedAmount,0) -  @UsedAmount ) Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And Inlinenumber=@Inlinenumber"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@UsedAmount", SqlDbType.Money)).Value = amnt
            com.Parameters.Add(New SqlParameter("@Inlinenumber", SqlDbType.BigInt)).Value = lblgiftcardInlinenumber.Text

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Public Function UpdateBalanceGiftCardForOrder(ByVal amnt As Double) As Boolean

        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
        Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
        Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
        Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)


        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "UPDATE [GiftCards] set [UsedAmount]= (@UsedAmount + Isnull(UsedAmount,0)) Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And Inlinenumber=@Inlinenumber"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@UsedAmount", SqlDbType.Money)).Value = amnt
            com.Parameters.Add(New SqlParameter("@Inlinenumber", SqlDbType.BigInt)).Value = lblgiftcardInlinenumber.Text

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Public Function PopulateItemDetailsGrid(ByVal CompanyID As String, ByVal DeptID As String, ByVal DivID As String, ByVal OrderNumber As String) As Data.DataTable

        Dim ConnectionString As String = ""
        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        Dim ConString As New SqlConnection
        ConString.ConnectionString = ConnectionString

        Dim sqlStr As String = "SELECT OrderDetail.OrderQty, OrderDetail.ItemID , OrderDetail.OrderLineNumber FROM OrderDetail    WHERE OrderDetail.CompanyID='" & CompanyID & "' AND OrderDetail.DepartmentID='" & DeptID & "'AND OrderDetail.DivisionID='" & DivID & "'AND OrderNumber='" & OrderNumber & "' AND Ltrim(Rtrim(OrderNumber))<>'' order by OrderLineNumber asc "



        Dim Cmd As SqlCommand = New SqlCommand()
        Cmd.Connection = ConString
        Cmd.CommandText = sqlStr
        ConString.Open()
        Dim Adapter As New SqlDataAdapter(Cmd)
        Dim ds As New Data.DataTable
        Adapter.Fill(ds)
        ConString.Close()
        Return ds
    End Function

    Public Function CheckOrderItemsForGiftCards(ByVal OrderNumber As String) As Boolean


        Dim ds As New DataTable
        ds = PopulateItemDetailsGrid(CompanyID, DepartmentID, DivisionID, OrderNumber)
        'OrderDetail.OrderQty, OrderDetail.ItemID

        If ds.Rows.Count <> 0 Then
            Dim n As Integer = 0
            For n = 0 To ds.Rows.Count - 1
                Dim itemid As String = ""
                Dim qty As Integer = 0
                Try
                    itemid = ds.Rows(n)("ItemID")
                Catch ex As Exception

                End Try
                Try
                    qty = ds.Rows(n)("OrderQty")
                Catch ex As Exception

                End Try



                If CheckItemGiftCardType(CompanyID, DepartmentID, DivisionID, itemid) Then



                    If CheckGiftCardItemQty(CompanyID, DepartmentID, DivisionID, itemid, qty) Then

                        Dim m As Integer = 0
                        For m = 1 To qty
                            ActivateGiftCardForOrder(OrderNumber, itemid)
                            lblCCMessage.Text += " " + itemid + " Gift Cards Activated !"
                        Next
                    Else

                        lblCCMessage.Text += " " + ItemIDFromTextBox + " Gift Cards not availble !"
                    End If
                End If

            Next
        End If


        Return True
    End Function

    Public Function CheckGiftCardItemQty(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemIDFromTextBox As String, ByVal Quantity As Integer) As Boolean

        Try

            Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim qry As String
            qry = "SELECT  *	FROM [GiftCards]	WHERE isnull([Active],0) = 0  and [ExpiryDate] >= GetDate() and  [CompanyID] = @CompanyID  and  [DivisionID] = @DivisionID and  [DepartmentID] = @DepartmentID and  [ItemID] = @ItemID "
            Dim com As SqlCommand

            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemIDFromTextBox


            Dim da As New SqlDataAdapter(com)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count >= Quantity Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            'txtWireNotes.Text = msg
            Return False
        End Try

        Return False
    End Function


    Public Function CheckItemGiftCardType(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal ItemIDFromTextBox As String) As Boolean

        Try
            Dim InLineNumber As String
            InLineNumber = Date.Now.Year & Date.Now.Month & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond

            Dim connec As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
            Dim qry As String
            qry = "SELECT  GiftCardType	FROM [InventoryItems]	WHERE isnull([GiftCardType],'') = 'P-GiftCard'    and  [CompanyID] = @CompanyID  and  [DivisionID] = @DivisionID and  [DepartmentID] = @DepartmentID and  [ItemID] = @ItemID "
            Dim com As SqlCommand

            com = New SqlCommand(qry, connec)

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@ItemID", SqlDbType.NVarChar, 36)).Value = ItemIDFromTextBox


            Dim da As New SqlDataAdapter(com)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            'txtWireNotes.Text = msg
            txtInternalNotes.Text = msg
            Return False
        End Try

        Return False
    End Function



#Region "PopupControl_Change"

    Public Sub PopupControl_Change(ByVal sender As Object, ByVal e As EventArgs) Handles lkpCustomerID.DataBinding

        lblCustomerIDNull.Visible = False

        If Session("CustomerID") = "" Then

            Session("CustomerID") = lkpCustomerID.Text
            PopulateCustomerInfo(lkpCustomerID.Text)
            lnkBackToOption.Visible = True
            lkpCustomerID.Visible = False

            If lkpCustomerID.Text <> "" And txtCustomerTemp.Text <> "" Then

                txtCustomerTemp.Visible = True

                DropDownList1.Focus()
                drpCustomerID.Visible = False

            End If
            lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400,ScreenX=800,ScreenY=300');return false; ")

        Else
            If Session("CustomerID") <> lkpCustomerID.Value And lkpCustomerID.Value <> "" Then

                'Code added for dropdownvisible to set false
                txtCustomerTemp.Visible = True
                lnkBackToOption.Visible = True
                lkpCustomerID.Visible = False
                DropDownList1.Focus()
                drpCustomerID.Visible = False


                txtCustomerTemp.Text = lkpCustomerID.Value
                Session("CustomerID") = lkpCustomerID.Value
                PopulateCustomerInfo(txtCustomerTemp.Text)
                lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")


            Else
                If Session("CntrlFrmdrpCountry") <> "" Then
                    BindGrid()
                    'Added new code by jacob on 29/10/2007
                    PopulateCustomerInfo(lkpCustomerID.Text)

                Else
                    txtCustomerTemp.Text = Session("CustomerID")
                    PopulateCustomerInfo(txtCustomerTemp.Text)
                    lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")
                End If

            End If
        End If

        txtCustomerTemp.Text = Session("CustomerID")

        CheckItemGridOnPostBacks()
        BindGrid()
    End Sub
#End Region

    Protected Sub lnkBackToOption_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBackToOption.Click
        'PopupvendorControl.Visible = False
        'lkpCustomerID.Visible = True
        lnkCustomerSearch.Visible = True
        drpNewsletterID.Visible = True
        txtCustomerTemp.Text = ""
        txtCustomerTemp.Visible = False
        txtSpecifyCustomer.Visible = False
        txtSpecifyCustomer.Text = ""
        Try
            drpCustomerID.SelectedValue = "New Customer"
        Catch ex As Exception
            Dim list As New ListItem
            list.Value = "New Customer"
            list.Text = "New Customer"
            drpCustomerID.Items.Add(list)
        End Try

        ' Session("VID") = ""
        txtVendorName.Text = ""
        txtCustomerAddress1.Text = ""
        txtCustomerAddress2.Text = ""
        txtCustomerAddress3.Text = ""
        txtCustomerCity.Text = ""
        txtCustomerFax.Text = ""
        txtCustomerPhone.Text = ""
        txtCustomerEmail.Text = ""
        txtCreditLimit.Text = ""
        txtAccountStatus.Text = ""
        txtCustomerSince.Text = ""
        txtCreditComments.Text = ""
        txtCustomerZip.Text = ""
        drpCustomerID.Visible = True
        lnkBackToOption.Visible = False
        PopulateCustomerInfo("")

        lblAccountStatus.Text = ""
        lblCreditLimit.Text = ""
        lblCreditComments.Text = ""
        lblYTDOrders.Text = ""
        lblAverageSale.Text = ""
        lblSalesLifeTime.Text = ""
        lblCustomerSince.Text = ""
        lblMemberPoints.Text = ""
        lblDiscounts.Text = ""
        lblCustomerRank.Text = ""

    End Sub
 
    Protected Sub drpWire_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpWire.SelectedIndexChanged
        Try
            'drpCustomerID.SelectedValue = drpWire.SelectedValue
            'drpCustomerID_SelectedIndexChanged(sender, e)
            txtcustomersearch.Text = "[" & drpWire.SelectedValue & "]"
            Dim sn As New Object
            Dim en As System.Web.UI.ImageClickEventArgs
            btncustsearch_Click(sn, en)
            lnkBackToOption.Visible = False
        Catch ex As Exception
            txtcustomersearch.Text = ex.Message
        End Try
    End Sub


    Protected Sub drpTransaction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpTransaction.SelectedIndexChanged
        Dim chk As Boolean = True

        Dim PaymentType As String
        Dim PaymentTypebool As Boolean = False
        PaymentType = drpPaymentType.SelectedValue

        If drpTransaction.SelectedValue.ToLower = "wire in" Or drpTransaction.SelectedValue.ToLower = "wire_in" Then

            Dim Wire As ListItem = drpPaymentType.Items.FindByValue("Wire In")
            drpPaymentType.ClearSelection()
            If Not Wire Is Nothing Then
                PaymentTypebool = True
                drpPaymentType.Items.FindByValue("Wire In").Selected = True
            End If


            Dim lst As New ListItem
            lst.Text = "Local Truck"
            lst.Value = "Local_Truck"
            chk = False
            drpShipMethod.Items.Clear()
            drpShipMethod.Items.Insert(0, lst)
            txtCustomerTemp.Visible = False
            Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Local_Truck")
            If Not Locallist Is Nothing Then
                drpShipMethod.Items.FindByValue("Local_Truck").Selected = True
            End If
        End If

        If drpTransaction.SelectedValue.ToLower = "wire out" Or drpTransaction.SelectedValue.ToLower = "wire_out" Then



            Dim lst As New ListItem
            lst.Text = "Wire Out"
            lst.Value = "Wire_Out"
            chk = False
            drpShipMethod.Items.Clear()
            drpShipMethod.Items.Insert(0, lst)
            txtCustomerTemp.Visible = False
            Dim Locallist As ListItem = drpShipMethod.Items.FindByValue("Wire_Out")
            If Not Locallist Is Nothing Then
                drpShipMethod.Items.FindByValue("Wire_Out").Selected = True
            End If
        End If

        If (drpPaymentType.SelectedValue.ToLower = "wire in" Or drpPaymentType.SelectedValue.ToLower = "wire_in") Then
            If (drpTransaction.SelectedValue.ToLower <> "wire in" And drpTransaction.SelectedValue.ToLower <> "wire_in") Then
                drpPaymentType.SelectedIndex = -1
            End If
        End If

        If chk Then
            PopulateDeliveryMethods()
        End If


        drpPaymentType_SelectedIndexChanged(sender, e)


        CheckItemGridOnPostBacks()

    End Sub

#Region "drpCustomerID_SelectedIndexChanged"
    Protected Sub drpCustomerID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCustomerID.SelectedIndexChanged
        If drpCustomerID.SelectedValue = "Search" Then
            drpCustomerID.Visible = False

            txtCustomerTemp.Visible = True
            lkpCustomerID.Visible = True
        ElseIf drpCustomerID.SelectedValue = "Specify Customer" Then
            drpCustomerID.Visible = False
            txtCustomerTemp.Visible = False
            lkpCustomerID.Visible = False
            txtSpecifyCustomer.Visible = True
            lnkBackToOption.Visible = True
        ElseIf drpPaymentType.SelectedValue = "Wire In" And drpCustomerID.SelectedValue <> "Specify Customer" And drpCustomerID.SelectedValue <> "New Customer" Then
            txtSpecifyCustomer.Text = drpCustomerID.SelectedValue
            Try
                drpWire.SelectedValue = drpCustomerID.SelectedValue
            Catch ex As Exception
                drpWire.SelectedValue = "0"
            End Try
            'txtSpecifyCustomer_TextChanged(sender, e)

        End If

        CheckItemGridOnPostBacks()
    End Sub
#End Region

 

#Region "PopulateDeliveryMethods"
    Public Sub PopulateDeliveryMethods()
        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderType.PopulateDeliveryMethods(CompanyID, DepartmentID, DivisionID)

        drpShipMethod.DataTextField = "ShipMethodDescription"
        drpShipMethod.DataValueField = "ShipMethodID"
        drpShipMethod.DataSource = rs
        drpShipMethod.DataBind()
        drpShipMethod.Items.Insert(0, (New ListItem("-Select-", "0")))

        Try
            drpShipMethod.SelectedValue = drpShipMethod.Items.IndexOf(drpShipMethod.Items.FindByText("Taken"))

        Catch ex As Exception

        End Try

        rs.Close()
    End Sub
#End Region




    Protected Sub drpstoredcc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpstoredcc.SelectedIndexChanged
        If drpstoredcc.SelectedValue <> "Other" Then

            trswipecard.Visible = False
            trchkupdate.Visible = True

            pnlccdetails.Visible = False
            pnlCClable.Visible = True
            lblcard.Text = ""
            lblEXP.Text = ""
            lblcsv.Text = ""
            Dim objCustomerCreditCards As New clsCustomerCreditCards
            objCustomerCreditCards.CompanyID = CompanyID
            objCustomerCreditCards.DivisionID = DivisionID
            objCustomerCreditCards.DepartmentID = DepartmentID
            If txtCustomerTemp.Text.Trim <> "" Then
                objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
                Dim dtCustomerCreditCards As New DataTable
                dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetailsForlinenumber

                'drpstoredcc.Items.Clear()
                'drpstoredcc.Items.Add("Other")

                If dtCustomerCreditCards.Rows.Count <> 0 Then
                    Dim crd As String
                    crd = dtCustomerCreditCards.Rows(0)("CreditCardNumber")

                    Try
                        If crd.Trim <> "" And txtCustomerTemp.Text.Trim <> "" Then
                            crd = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(crd.Trim, txtCustomerTemp.Text.Trim)
                        End If
                    Catch ex As Exception

                    End Try


                    txtCard.Text = crd

                    'Last number display X in Credit cards
                    Dim cardNo As String = ""
                    Dim cLen As Integer = 0
                    Dim subLen As Integer = 0
                    Dim SubcardNo As String = ""
                    cardNo = crd
                    cLen = cardNo.Length()
                    Dim slen As Integer = 0
                    If cLen > 0 Then
                        If cLen > 12 Then
                            subLen = cLen - 12
                            'SubcardNo = cardNo.Substring(0, subLen)
                            SubcardNo = cardNo.Substring(12, subLen)
                            slen = SubcardNo.Length()

                            If slen > 4 Then
                                SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                            End If
                            'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                            cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                        Else
                            cardNo = RepeatChar("X", cLen)
                        End If
                    End If


                    lblcard.Text = cardNo

                    Try
                        lblEXP.Text = dtCustomerCreditCards.Rows(0)("CreditCardExpDate")
                        Dim ar() As String
                        ar = lblEXP.Text.Split("/")
                        If ar.Length = 2 Then
                            If ar(1).Length = 2 Then
                                lblEXP.Text = ar(0) & "/" & "20" & ar(1)
                            End If
                        End If
                        'lblEXP.Text = "XX/XXXX"
                    Catch ex As Exception

                    End Try

                    Try
                        lblcsv.Text = "" 'dtCustomerCreditCards.Rows(0)("CreditCardCSVNumber")
                        txtCSV.Text = "" 'lblcsv.Text

                        lblcsv.Text = RepeatChar("X", lblcsv.Text.Length)
                    Catch ex As Exception
                        lblcsv.Text = ""
                    End Try


                    Try
                        lblcctype.Text = dtCustomerCreditCards.Rows(0)("CreditCardTypeID")
                    Catch ex As Exception
                        lblcctype.Text = ""
                    End Try

                    ''New codeded for the Expiration date selection
                    Dim exdat As String = ""
                    Dim exdate As Date

                    Try
                        Dim ar() As String
                        ar = lblEXP.Text.Split("/")
                        If ar.Length = 2 Then
                            exdat = ar(0) & "/01/" & ar(1)
                        End If

                        If ar.Length = 3 Then
                            exdat = lblEXP.Text
                        End If

                        exdate = exdat
                        exdat = exdate.ToString("MM/yyyy")
                        lblEXP.ForeColor = Drawing.Color.Black
                        'BtnBookOrder.Enabled = True
                    Catch ex As Exception
                        lblEXP.Text = lblEXP.Text & "<br>Error: Allowed MM/YY"
                        lblEXP.ForeColor = Drawing.Color.Red
                        'BtnBookOrder.Enabled = False

                    End Try

                    'lblEXP.Text = exdate.ToString

                    drpExpirationDate.SelectedIndex = drpExpirationDate.Items.IndexOf(drpExpirationDate.Items.FindByValue(exdat))

                    Try
                        'drpCardType.SelectedIndex = 1
                        drpCardType.SelectedIndex = drpCardType.Items.IndexOf(drpCardType.Items.FindByValue(dtCustomerCreditCards.Rows(0)("CreditCardTypeID")))
                    Catch ex As Exception

                    End Try
                    'lblcsv.Text = ""
                    'lblEXP.Text = ""

                End If
            End If
            'pnlccdetails.Visible = True
        Else
            trswipecard.Visible = True
            trchkupdate.Visible = False

            txtCSV.Text = ""
            drpExpirationDate.SelectedIndex = -1
            txtCard.Text = ""
            drpstoredcc.SelectedIndex = -1

            pnlccdetails.Visible = True
            pnlCClable.Visible = False
        End If



        updatebillingforstoredCC()
        CheckItemGridOnPostBacks()

    End Sub



    Protected Sub updatebillingforstoredCC()
        Exit Sub

        'If chkupdatebilling.Checked = False Then
        '    PopulateCustomerInfo(txtCustomerTemp.Text)
        '    Exit Sub
        'End If

        Dim objCustomerCreditCards As New clsCustomerCreditCards
        objCustomerCreditCards.CompanyID = CompanyID
        objCustomerCreditCards.DivisionID = DivisionID
        objCustomerCreditCards.DepartmentID = DepartmentID
        objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
        If drpstoredcc.SelectedValue <> "Other" Then
            objCustomerCreditCards.OrderNumber = OrdNumber
            objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
            'objCustomerCreditCards.UpdateStoredCreditCardOrderNumber()
            objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
            Dim dtCustomerCreditCards As New DataTable
            dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetailsForlinenumber
            If dtCustomerCreditCards.Rows.Count <> 0 Then
                Dim FLName As String = ""
                Dim FLNameArr() As String
                FLName = dtCustomerCreditCards.Rows(0)("CustomerName").ToString
                If FLName.IndexOf("-") <> -1 Then
                    FLNameArr = FLName.Split("-")
                Else
                    FLNameArr = FLName.Split(" ")
                End If

                If FLNameArr.Length = 2 Then
                    txtCustomerFirstName.Text = FLNameArr(0)
                    txtCustomerLastName.Text = FLNameArr(1)
                End If
                If FLNameArr.Length = 3 Then
                    txtCustomerFirstName.Text = FLNameArr(0) & " " & FLNameArr(1)
                    txtCustomerLastName.Text = FLNameArr(2)
                End If
                If FLNameArr.Length = 1 Then
                    txtCustomerFirstName.Text = FLNameArr(0)
                    txtCustomerLastName.Text = ""
                End If
                txtCustomerAddress1.Text = dtCustomerCreditCards.Rows(0)("CustomerAddress1")
                txtCustomerAddress2.Text = dtCustomerCreditCards.Rows(0)("CustomerAddress2")
                txtCustomerAddress3.Text = dtCustomerCreditCards.Rows(0)("CustomerAddress3")
                txtCustomerCity.Text = dtCustomerCreditCards.Rows(0)("CustomerCity")
                drpCountry.SelectedIndex = drpCountry.Items.IndexOf(drpCountry.Items.FindByValue(dtCustomerCreditCards.Rows(0)("CustomerCountry").ToString()))
                If dtCustomerCreditCards.Rows(0)("CustomerCountry").ToString() = "US" Then
                    drpState.Visible = True
                    txtCustomerState.Visible = False
                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(dtCustomerCreditCards.Rows(0)("CustomerState").ToString()))
                ElseIf dtCustomerCreditCards.Rows(0)("CustomerCountry").ToString() = "CD" Then
                    drpState.Visible = True
                    txtCustomerState.Visible = False
                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(dtCustomerCreditCards.Rows(0)("CustomerState").ToString()))
                Else
                    drpState.Visible = False
                    txtCustomerState.Visible = True
                    txtCustomerState.Text = dtCustomerCreditCards.Rows(0)("CustomerState").ToString()
                End If
                txtCustomerZip.Text = dtCustomerCreditCards.Rows(0)("CustomerZip").ToString()
                PopulateCustomerEmail(txtCustomerTemp.Text.Trim)
                If chkBillingAddress.Checked = True Then
                    txtShippingName.Text = txtCustomerFirstName.Text
                    txtShippingLastName.Text = txtCustomerLastName.Text
                    txtShippingAddress1.Text = dtCustomerCreditCards.Rows(0)("CustomerAddress1").ToString()
                    txtShippingAddress2.Text = dtCustomerCreditCards.Rows(0)("CustomerAddress2").ToString()
                    txtShippingAddress3.Text = dtCustomerCreditCards.Rows(0)("CustomerAddress3").ToString()
                    txtShippingCity.Text = dtCustomerCreditCards.Rows(0)("CustomerCity").ToString()
                    txtShippingZip.Text = dtCustomerCreditCards.Rows(0)("CustomerZip").ToString()
                End If
            End If
        Else
            'txtCustomerTemp.Text 
            PopulateCustomerInfo(txtCustomerTemp.Text)
        End If

        'updatemulticard(lblOrderNumberData.Text)

        CheckItemGridOnPostBacks()

    End Sub

    Public Sub PopulateCustomerEmail(ByVal CustID As String)
        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderType.PopulateCustomerDetails(CompanyID, DepartmentID, DivisionID, CustID)
        While rs.Read()
            txtCustomerFax.Text = rs("CustomerFax").ToString()
            txtCustomerPhone.Text = rs("CustomerPhone").ToString()
            txtCustomerEmail.Text = rs("CustomerEmail").ToString()
        End While
        rs.Close()
    End Sub


#Region "PopulateCustomerInfo"

    Public Function FindTaxIDforcustomer(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal TaxGroupID As String) As DataTable

        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()


        Dim myCommand As New SqlCommand("[TaxIDforcustomer]", ConString)
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


        Dim parameterCustomerID As New SqlParameter("@TaxGroupID", Data.SqlDbType.NVarChar, 36)
        parameterCustomerID.Value = TaxGroupID
        myCommand.Parameters.Add(parameterCustomerID)

        Dim adapter As New SqlDataAdapter(myCommand)

        Dim dt As New DataTable

        ConString.Close()
        adapter.Fill(dt)


        Return dt

    End Function

 Public Function SelectCSTtypeDetail(ByVal CustID As String) As DataTable
        Dim dt As New DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "Select CustomerTypeID,DeliveryCharge,ApplyDeliveryCharge from   [CustomerInformation] Where CompanyID=@f1  AND   DivisionID =@f2  AND  DepartmentID =@f3  AND [CustomerID]=@f4"

        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID.Trim()
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID.Trim()
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID.Trim()
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = CustID

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


    Public Sub PopulateSpecialCustomerInfo(ByVal CustID As String)

        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderType.PopulateCustomerDetails(CompanyID, DepartmentID, DivisionID, CustID)
        While rs.Read()
            If rs("CreditLimit").ToString() <> "" Then
                txtCreditLimit.Text = Decimal.Parse(rs("CreditLimit").ToString()).ToString("0.00")
            End If

            txtAccountStatus.Text = rs("AccountStatus").ToString()
            If rs("CustomerSince").ToString() <> "" Then
                'Edited by Jacob on 24-01-2008

                txtCustomerSince.Text = DateTime.Parse(rs("CustomerSince").ToString()).ToString("MM/dd/yyyy")

            End If
            txtDiscounts.Text = rs("AllowanceDiscountPerc").ToString() + "%"
            txtCreditComments.Text = rs("CreditComments").ToString()

            '''''''''''''''  for filling all Details at Order Entry Form in Customer Profile Section ''''''''''''
            If rs("YTDOrders").ToString() <> "" Then
                txtYtdOrders.Text = Decimal.Parse(rs("YTDOrders").ToString()).ToString("0.00") ''  
            End If
            If rs("SalesLifeTime").ToString() <> "" Then
                txtSalesLifeTime.Text = Decimal.Parse(rs("SalesLifeTime").ToString()).ToString("0.00") ''  
            End If
            If rs("AverageSales").ToString() <> "" Then
                txtAverageSale.Text = Decimal.Parse(rs("AverageSales").ToString()).ToString("0.00") ''  
            End If
            If rs("MemberPoints").ToString() <> "" Then
                txtMemberPoints.Text = Decimal.Parse(rs("MemberPoints").ToString()).ToString("0.00") ''  
            End If
            txtCustomerRank.Text = rs("CustomerRank").ToString()    ''  
            Try
                txtclubcard.Text = rs("ClubCard").ToString()    '' Added By Vikas
            Catch ex As Exception

            End Try
            Try
                txtclubcardexpdate.Text = rs("CardExpiryDate")
            Catch ex As Exception

            End Try

            Dim expdate As Date
            Dim chlexp As Boolean = False
            Try
                expdate = txtclubcardexpdate.Text
                chlexp = True
            Catch ex As Exception

            End Try
            If chlexp Then
                Dim expcheck As Boolean = False
                If expdate.Year >= Date.Now.Year Then
                    If expdate.Month >= Date.Now.Month Then
                        If expdate.Day >= Date.Now.Day Then
                            expcheck = True
                        End If
                    End If
                End If
                If expcheck Then
                    txtclubcardstatus.Text = "Active"
                    txtclubcardstatus.ForeColor = Drawing.Color.Green
                Else
                    txtclubcardstatus.Text = "Expired"
                    txtclubcardstatus.ForeColor = Drawing.Color.Red
                End If
            End If


            ''''''''''''''  Up to Here 


            txtCustomerZip.Text = rs("CustomerZip").ToString()

            txtCompany.Text = rs("CustomerCompany").ToString()

            Session("DiscountPerc") = rs("AllowanceDiscountPerc").ToString()

            ''CustomerComments''
            txtComments.Text = rs("CustomerComments").ToString()




            txtCustomerFirstName.Text = ""
            txtVendorName.Text = ""
            '  cmbSalutation.Text = rs("CustomerSalutation").ToString()
            'DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows(0)("CustomerSalutation").ToString()))
            txtCustomerLastName.Text = ""
            txtAttention.Text = ""
            txtCustomerAddress1.Text = ""
            txtCustomerAddress2.Text = ""
            txtCustomerAddress3.Text = ""


        End While

    End Sub


    Public Sub PopulateCustomerInfo(ByVal CustID As String)


        If CompanyID = "FieldOfFlowers" Or CompanyID = "FieldOfFlowersTraining" Then

            '' New code for delivery by customer
            Dim dtCST As New DataTable
            If CustID.Trim <> "" Then
                dtCST = SelectCSTtypeDetail(CustID)
            End If

            If dtCST.Rows.Count <> 0 Then

                Dim CustomerTypeID As String = ""

                Try
                    CustomerTypeID = dtCST.Rows(0)("CustomerTypeID")
                Catch ex As Exception

                End Try
                txtCustomerTypeID.Text = CustomerTypeID
                '' on Page load first time make it blank
                Session("CustomerTypeID") = CustomerTypeID

                If CustomerTypeID = "WHO" Or CustomerTypeID = "WHOC" Then
                    If chkDeliveryOverride.Checked = False Then
                        txtDelivery.Text = "10.00"
                    End If

                End If

                Dim ApplyDeliveryCharge As Boolean = False

                Try
                    ApplyDeliveryCharge = dtCST.Rows(0)("ApplyDeliveryCharge")
                Catch ex As Exception

                End Try

                If ApplyDeliveryCharge Then
                    Try
                        Dim dlchargeas As Decimal = 0
                        dlchargeas = dtCST.Rows(0)("DeliveryCharge")
                        dlchargeas = String.Format("{0:n2}", dlchargeas)
                        If chkDeliveryOverride.Checked = False Then
                            txtDelivery.Text = dlchargeas
                        End If

                    Catch ex As Exception

                    End Try
                End If

            End If
            '' New code for tax by zipcode
        End If

       
        Dim objCustomerCreditCards As New clsCustomerCreditCards
        objCustomerCreditCards.CompanyID = CompanyID
        objCustomerCreditCards.DivisionID = DivisionID
        objCustomerCreditCards.DepartmentID = DepartmentID
        objCustomerCreditCards.CustomerID = CustID


        If drpstoredcc.SelectedValue <> "Other" Then
            objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
            Dim dtCustomerCreditCards As New DataTable
            dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetailsForlinenumberForCustomer
            If dtCustomerCreditCards.Rows.Count = 0 Then
                trswipecard.Visible = True
                trchkupdate.Visible = False
                txtCSV.Text = ""
                drpExpirationDate.SelectedIndex = -1
                txtCard.Text = ""
                drpstoredcc.Items.Clear()
                drpstoredcc.Items.Add("Other")
                pnlccdetails.Visible = True
                pnlCClable.Visible = False
            Else

                Exit Sub
            End If
        End If





        If 1 = 1 Then 'drpstoredcc.Items.Count = 1 Then
            If drpPaymentType.SelectedValue.ToLower = "Credit Card".ToLower Then


                If CustID <> "" Then
                    objCustomerCreditCards.CustomerID = CustID
                    Dim dtCustomerCreditCards As New DataTable
                    dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetails

                    drpstoredcc.Items.Clear()
                    drpstoredcc.Items.Add("Other")

                    If dtCustomerCreditCards.Rows.Count <> 0 Then
                        Dim n As Integer
                        For n = 0 To dtCustomerCreditCards.Rows.Count - 1
                            Dim lst As New ListItem
                            Dim crd As String
                            crd = dtCustomerCreditCards.Rows(n)("CreditCardNumber")
                            Try
                                If crd.Trim <> "" And CustID <> "" Then
                                    crd = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(crd.Trim, CustID)
                                End If
                            Catch ex As Exception

                            End Try

                            'Last number display X in Credit cards
                            Dim cardNo As String = ""
                            Dim cLen As Integer = 0
                            Dim subLen As Integer = 0
                            Dim SubcardNo As String = ""
                            cardNo = crd
                            cLen = cardNo.Length()
                            Dim slen As Integer = 0
                            If cLen > 0 Then
                                If cLen > 12 Then
                                    subLen = cLen - 12
                                    'SubcardNo = cardNo.Substring(0, subLen)
                                    SubcardNo = cardNo.Substring(12, subLen)
                                    slen = SubcardNo.Length()

                                    If slen > 4 Then
                                        SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                                    End If
                                    'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                                    cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                                Else
                                    cardNo = RepeatChar("X", cLen)
                                End If
                            End If


                            lst.Text = cardNo
                            lst.Value = dtCustomerCreditCards.Rows(n)("CardDetailLineNumber")
                            drpstoredcc.Items.Add(lst)



                        Next
                    End If


                End If
            End If

        End If

        If CustID = "Retail Customer" Or CustID = "5334972" Or CustID = "Returns" Or CustID = "C-69841" Or CustID = "C-67776" Or CustID = "C-69842" Or CustID = "C-69843" Or CustID = "C-67828" Then
            Dim obj As New clsCustomerInformation_RetailCustomer
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            obj.OrderNumber = lblOrderNumberData.Text
            Dim dt As New DataTable
            dt = obj.SelectCustomerInformationDetail

            If dt.Rows.Count <> 0 Then
                'CustomerFirstName.Text = dt.Rows(0)("CustomerFirstName")
                'CustomerLastName.Text = dt.Rows(0)("CustomerLastName")

                Session("CustomerID") = dt.Rows(0)("CustomerID").ToString()
                txtCustomerFirstName.Text = dt.Rows(0)("CustomerFirstName").ToString()
                txtVendorName.Text = dt.Rows(0)("CustomerFirstName").ToString()
                '  cmbSalutation.Text = rs("CustomerSalutation").ToString()
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(dt.Rows(0)("CustomerSalutation").ToString()))
                txtCustomerLastName.Text = dt.Rows(0)("CustomerLastName").ToString()
                txtAttention.Text = dt.Rows(0)("Attention").ToString()
                txtCustomerAddress1.Text = dt.Rows(0)("CustomerAddress1").ToString()
                txtCustomerAddress2.Text = dt.Rows(0)("CustomerAddress2").ToString()
                txtCustomerAddress3.Text = dt.Rows(0)("CustomerAddress3").ToString()
                txtCustomerCity.Text = dt.Rows(0)("CustomerCity").ToString()
                If dt.Rows(0)("Newsletter").ToString() <> "" Then
                    drpNewsletterID.SelectedIndex = drpNewsletterID.Items.IndexOf(drpNewsletterID.Items.FindByValue(dt.Rows(0)("Newsletter").ToString()))
                End If

                'drpState.Text = rs("CustomerState").ToString()
                'cmbCountry.Text = rs("CustomerCountry").ToString()

                drpCountry.SelectedIndex = drpCountry.Items.IndexOf(drpCountry.Items.FindByValue(dt.Rows(0)("CustomerCountry").ToString()))
                If dt.Rows(0)("CustomerCountry").ToString() = "US" Then
                    drpState.Visible = True
                    '  txtCustomerState.Visible = False
                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(dt.Rows(0)("CustomerState").ToString()))
                ElseIf dt.Rows(0)("CustomerCountry").ToString() = "CD" Then
                    drpState.Visible = True
                    '   txtCustomerState.Visible = False
                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(dt.Rows(0)("CustomerState").ToString()))
                Else


                    drpState.Visible = True
                    '  txtCustomerState.Visible = True
                    '  txtCustomerState.Text = rs("CustomerState").ToString()

                End If


                txtCustomerFax.Text = dt.Rows(0)("CustomerFax").ToString()
                txtCustomerPhone.Text = dt.Rows(0)("CustomerPhone").ToString()
                txtCustomerEmail.Text = dt.Rows(0)("CustomerEmail").ToString()
                txtCreditLimit.Text = "0.00"
                txtCustomerZip.Text = dt.Rows(0)("CustomerZip").ToString()
                txtCustomerCell.Text = dt.Rows(0)("CustomerCell").ToString()
                txtExt.Text = dt.Rows(0)("CustomerPhoneExt").ToString()
                txtCompany.Text = dt.Rows(0)("CustomerCompany").ToString()

                Session("DiscountPerc") = "0" 'rs("AllowanceDiscountPerc").ToString()

                If chkBillingAddress.Checked = True Then
                    txtShippingName.Text = dt.Rows(0)("CustomerFirstName").ToString()
                    'drpShipCustomerSalutation.Text = rs("CustomerSalutation").ToString()
                    drpShipCustomerSalutation.SelectedIndex = drpShipCustomerSalutation.Items.IndexOf(drpShipCustomerSalutation.Items.FindByValue(dt.Rows(0)("CustomerSalutation").ToString()))
                    txtShippingLastName.Text = dt.Rows(0)("CustomerLastName").ToString()
                    txtShippingAttention.Text = dt.Rows(0)("Attention").ToString()
                    txtShippingAddress1.Text = dt.Rows(0)("CustomerAddress1").ToString()
                    txtShippingAddress2.Text = dt.Rows(0)("CustomerAddress2").ToString()
                    txtShippingAddress3.Text = dt.Rows(0)("CustomerAddress3").ToString()
                    txtShippingCity.Text = dt.Rows(0)("CustomerCity").ToString()
                    'txtShippingState.Text = rs("CustomerState").ToString()
                    txtShippingZip.Text = dt.Rows(0)("Customerzip").ToString()
                End If
            Else
                PopulateSpecialCustomerInfo(CustID)
            End If

        Else



            Dim PopOrderType As New CustomOrder()
            Dim rs As SqlDataReader
            rs = PopOrderType.PopulateCustomerDetails(CompanyID, DepartmentID, DivisionID, CustID)
            While rs.Read()

                lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")

                'TaxGroupID
                Dim TaxGroupID As String = ""
                Try
                    TaxGroupID = rs("TaxGroupID").ToString()
                Catch ex As Exception
                End Try
                If TaxGroupID <> "" Then
                    Dim dtTaxGroupID As DataTable
                    dtTaxGroupID = FindTaxIDforcustomer(CompanyID, DepartmentID, DivisionID, TaxGroupID)
                    If dtTaxGroupID.Rows.Count <> 0 Then
                        Dim taxid As String = ""

                        Try
                            taxid = dtTaxGroupID.Rows(0)("TaxID")

                            If taxid <> "" Then
                                drpTaxes.SelectedValue = taxid.ToLower()
                                'drpTaxes.s
                                Dim sender As Object
                                Dim e As Object
                                drpTaxes_SelectedIndexChanged(sender, e)

                            End If

                        Catch ex As Exception

                        End Try



                    End If
                End If


                'TaxGroupID


                txtCustomerTemp.Text = rs("CustomerID").ToString()
                Session("CustomerID") = rs("CustomerID").ToString()
                txtCustomerFirstName.Text = rs("CustomerFirstName").ToString()
                txtVendorName.Text = rs("CustomerFirstName").ToString()

                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(rs("CustomerSalutation").ToString()))
                txtCustomerLastName.Text = rs("CustomerLastName").ToString()
                txtAttention.Text = rs("Attention").ToString()
                txtCustomerAddress1.Text = rs("CustomerAddress1").ToString()
                txtCustomerAddress2.Text = rs("CustomerAddress2").ToString()
                txtCustomerAddress3.Text = rs("CustomerAddress3").ToString()
                txtCustomerCity.Text = rs("CustomerCity").ToString()
                If rs("Newsletter").ToString() <> "" Then
                    drpNewsletterID.SelectedIndex = drpNewsletterID.Items.IndexOf(drpNewsletterID.Items.FindByValue(rs("Newsletter").ToString()))
                End If


                drpCountry.SelectedIndex = drpCountry.Items.IndexOf(drpCountry.Items.FindByValue(rs("CustomerCountry").ToString()))
                If rs("CustomerCountry").ToString() = "US" Then
                    drpState.Visible = True
                    txtCustomerState.Visible = False


                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(rs("CustomerState").ToString()))
                ElseIf rs("CustomerCountry").ToString() = "CD" Then
                    drpState.Visible = True
                    txtCustomerState.Visible = False

                    drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(rs("CustomerState").ToString()))
                Else


                    drpState.Visible = False
                    txtCustomerState.Visible = True
                    txtCustomerState.Text = rs("CustomerState").ToString()

                End If


                txtCustomerFax.Text = rs("CustomerFax").ToString()
                txtCustomerPhone.Text = rs("CustomerPhone").ToString()
                txtCustomerEmail.Text = rs("CustomerEmail").ToString()

                If rs("CreditLimit").ToString() <> "" Then
                    txtCreditLimit.Text = Decimal.Parse(rs("CreditLimit").ToString()).ToString("0.00")
                End If

                txtAccountStatus.Text = rs("AccountStatus").ToString()
                If rs("CustomerSince").ToString() <> "" Then
                    'Edited by Jacob on 24-01-2008

                    txtCustomerSince.Text = DateTime.Parse(rs("CustomerSince").ToString()).ToString("MM/dd/yyyy")

                End If
                txtDiscounts.Text = rs("AllowanceDiscountPerc").ToString() + "%"
                txtCreditComments.Text = rs("CreditComments").ToString()

                ''''''''''''''' Added By Vikas for filling all Details at Order Entry Form in Customer Profile Section ''''''''''''
                If rs("YTDOrders").ToString() <> "" Then
                    txtYtdOrders.Text = Decimal.Parse(rs("YTDOrders").ToString()).ToString("0.00") '' Added by Vikas
                End If
                If rs("SalesLifeTime").ToString() <> "" Then
                    txtSalesLifeTime.Text = Decimal.Parse(rs("SalesLifeTime").ToString()).ToString("0.00") '' Added By Vikas
                End If
                If rs("AverageSales").ToString() <> "" Then
                    txtAverageSale.Text = Decimal.Parse(rs("AverageSales").ToString()).ToString("0.00") '' Added By Vikas
                End If
                If rs("MemberPoints").ToString() <> "" Then
                    txtMemberPoints.Text = Decimal.Parse(rs("MemberPoints").ToString()).ToString("0.00") '' Added By Vikas
                End If
                txtCustomerRank.Text = rs("CustomerRank").ToString()    '' Added By Vikas

                Try
                    txtclubcard.Text = rs("ClubCard").ToString()    '' Added By Vikas
                Catch ex As Exception

                End Try
                Try
                    txtclubcardexpdate.Text = rs("CardExpiryDate")
                Catch ex As Exception

                End Try

                Dim expdate As Date
                Dim chlexp As Boolean = False
                Try
                    expdate = txtclubcardexpdate.Text
                    chlexp = True
                Catch ex As Exception

                End Try
                If chlexp Then
                    Dim expcheck As Boolean = False
                    If expdate.Year >= Date.Now.Year Then
                        If expdate.Month >= Date.Now.Month Then
                            If expdate.Day >= Date.Now.Day Then
                                expcheck = True
                            End If
                        End If
                    End If
                    If expcheck Then
                        txtclubcardstatus.Text = "Active"
                        txtclubcardstatus.ForeColor = Drawing.Color.Green
                    Else
                        txtclubcardstatus.Text = "Expired"
                        txtclubcardstatus.ForeColor = Drawing.Color.Red
                    End If
                End If


                ''''''''''''''  Up to Here 


                txtCustomerZip.Text = rs("CustomerZip").ToString()
                txtCustomerCell.Text = rs("CustomerCell").ToString()
                txtExt.Text = rs("CustomerPhoneExt").ToString()
                txtCompany.Text = rs("CustomerCompany").ToString()

                Session("DiscountPerc") = rs("AllowanceDiscountPerc").ToString()

                ''CustomerComments''
                txtComments.Text = rs("CustomerComments").ToString()
                ''CustomerComments''

                If chkBillingAddress.Checked = True Then
                    txtShippingName.Text = rs("CustomerFirstName").ToString()
                    drpShipCustomerSalutation.SelectedIndex = drpShipCustomerSalutation.Items.IndexOf(drpShipCustomerSalutation.Items.FindByValue(rs("CustomerSalutation").ToString()))
                    txtShippingLastName.Text = rs("CustomerLastName").ToString()
                    txtShippingAttention.Text = rs("Attention").ToString()
                    txtShippingAddress1.Text = rs("CustomerAddress1").ToString()
                    txtShippingAddress2.Text = rs("CustomerAddress2").ToString()
                    txtShippingAddress3.Text = rs("CustomerAddress3").ToString()
                    txtShippingCity.Text = rs("CustomerCity").ToString()
                    txtShippingZip.Text = rs("Customerzip").ToString()
                End If
            End While
            rs.Close()

        End If

        CheckItemGridOnPostBacks()


        lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")


        lblAccountStatus.Text = txtAccountStatus.Text
        lblCreditLimit.Text = txtCreditLimit.Text
        lblCreditComments.Text = txtCreditComments.Text
        lblYTDOrders.Text = txtYtdOrders.Text
        lblAverageSale.Text = txtAverageSale.Text
        lblSalesLifeTime.Text = txtSalesLifeTime.Text
        lblCustomerSince.Text = txtCustomerSince.Text
        lblMemberPoints.Text = txtMemberPoints.Text
        lblDiscounts.Text = txtDiscounts.Text
        lblCustomerRank.Text = txtCustomerRank.Text

        lbltxtclubcard.Text = txtclubcard.Text
        lbltxtclubcardexpdate.Text = txtclubcardexpdate.Text
        lbltxtclubcardstatus.Text = txtclubcardstatus.Text
        lbltxtclubcardstatus.ForeColor = txtclubcardstatus.ForeColor

    End Sub

#End Region


#Region "chkBillingAddress_CheckedChanged"
    Protected Sub chkBillingAddress_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBillingAddress.CheckedChanged


        'Dim PopOrderType As New CustomOrder()
        'Dim rs As SqlDataReader
        'Dim CustID As String = Session("CustomerID").ToString()

        'If CustID = "" Then
        '    CustID = txtCustomerTemp.Text
        'End If
        'Dim sp As String = txtCustomerTemp.Text
        'lkpCustomerID.Text = CustID
        'rs = PopOrderType.PopulateCustomerDetails(CompanyID, DepartmentID, DivisionID, CustID)
        'While rs.Read()
        '    If chkBillingAddress.Checked = True Then
        '        txtCustomerTemp.Text = rs("CustomerID").ToString()
        '        Session("CustomerID") = rs("CustomerID").ToString()
        '        txtShippingName.Text = rs("CustomerFirstName").ToString()
        '        drpShipCustomerSalutation.SelectedIndex = drpShipCustomerSalutation.Items.IndexOf(drpShipCustomerSalutation.Items.FindByValue(rs("CustomerSalutation").ToString()))
        '        txtShippingLastName.Text = rs("CustomerLastName").ToString()
        '        txtShippingAttention.Text = rs("Attention").ToString()
        '        txtShippingAddress1.Text = rs("CustomerAddress1").ToString()
        '        txtShippingAddress2.Text = rs("CustomerAddress2").ToString()
        '        txtShippingAddress3.Text = rs("CustomerAddress3").ToString()
        '        txtShippingCity.Text = rs("CustomerCity").ToString()

        '        Dim Ctry As String = rs("CustomerCountry").ToString()
        '        Dim sty As String = rs("CustomerState").ToString()

        '        drpShipCountry.SelectedIndex = drpShipCountry.Items.IndexOf(drpShipCountry.Items.FindByValue(rs("CustomerCountry").ToString()))
        '        drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(rs("CustomerState").ToString()))


        '        Dim drpCustomerCountry As String = rs("CustomerCountry").ToString()
        '        If drpCustomerCountry = "US" Then
        '            drpShippingState.Visible = True
        '            txtShippingState.Visible = False
        '        ElseIf drpCustomerCountry = "CD" Then
        '            drpShippingState.Visible = True
        '            txtShippingState.Visible = False
        '        Else
        '            drpShippingState.Visible = False

        '            txtShippingState.Visible = True
        '            txtShippingState.Text = rs("CustomerState").ToString()

        '        End If

        '        txtShippingZip.Text = rs("Customerzip").ToString()
        '        txtShipCustomerCell.Text = rs("CustomerCell").ToString()
        '        txtShipExt.Text = rs("CustomerPhoneExt").ToString()
        '        txtShipCompany.Text = rs("CustomerCompany").ToString()
        '        txtShipCustomerFax.Text = rs("CustomerFax").ToString()
        '        txtShipCustomerPhone.Text = rs("CustomerPhone").ToString()
        If chkBillingAddress.Checked = True Then
            txtShippingName.Text = txtCustomerFirstName.Text
            drpShipCustomerSalutation.SelectedValue = DropDownList1.SelectedValue
            'drpShipCustomerSalutation.SelectedValue = ""

            txtShippingLastName.Text = txtCustomerLastName.Text
            txtShippingAttention.Text = txtAttention.Text
            txtShippingAddress1.Text = txtCustomerAddress1.Text
            txtShippingAddress2.Text = txtCustomerAddress2.Text
            txtShippingAddress3.Text = txtCustomerAddress3.Text
            txtShippingCity.Text = txtCustomerCity.Text
            txtShippingState.Text = txtCustomerState.Text
            drpShippingState.SelectedValue = drpState.SelectedValue
            txtShippingZip.Text = txtCustomerZip.Text
            txtShipCustomerCell.Text = txtCustomerCell.Text
            txtShipExt.Text = txtExt.Text
            txtShipCompany.Text = txtCompany.Text
            txtShipCustomerPhone.Text = txtCustomerPhone.Text
            'txtDelivery.Text = ""
            'txtService.Text = ""
            'txtRelay.Text = ""
        Else
            txtShippingName.Text = ""
            'drpShipCustomerSalutation.Text = ""
            'drpShipCustomerSalutation.SelectedValue = ""

            txtShippingLastName.Text = ""
            txtShippingAttention.Text = ""
            txtShippingAddress1.Text = ""
            txtShippingAddress2.Text = ""
            txtShippingAddress3.Text = ""
            txtShippingCity.Text = ""
            '  txtShippingState.Text = ""
            txtShippingZip.Text = ""
            txtShipCustomerCell.Text = ""
            txtShipExt.Text = ""
            txtShipCompany.Text = ""
            txtShipCustomerPhone.Text = ""
            txtDelivery.Text = ""
            txtService.Text = ""
            txtRelay.Text = ""

        End If


        'End While
        'rs.Close()

        'Edited by Jacob on 24-01-2008
        Dim Zip As String = txtShippingZip.Text
        ShippingDeliveryCharge(Zip)
        Session("CntlFromPostBackTrue") = "True"
        CheckItemGridOnPostBacks()
    End Sub
#End Region


#Region "CreateNewCustomerID"
    Protected Function CreateNewCustomerID() As String
        Dim objCustomer As New CustomOrder()
        Dim rs As SqlDataReader
        Dim CustIDNew As String = ""
        Dim CustID As String = ""
        Dim CustIDCreated As String = ""
        Dim CustMaxCount As Integer = 0
        Dim Flg As Boolean = True
        rs = objCustomer.GetNextCustomerID(CompanyID, DepartmentID, DivisionID)
        While rs.Read()
            CustID = rs("CustomerID").ToString()
        End While

        Dim CustIDint As Integer = Convert.ToInt32(CustID)
        CustIDint = CustIDint + 1
        Dim CustIDString As String = Convert.ToString(CustIDint)
        CustIDNew = CustIDString.Insert(0, "C-")


        CustMaxCount = objCustomer.GetCustomerID(CompanyID, DepartmentID, DivisionID, CustIDNew)
        While CustMaxCount = 1
            CustIDint += 1
            CustIDString = Convert.ToString(CustIDint)
            CustIDNew = CustIDString.Insert(0, "C-")
            CustMaxCount = objCustomer.GetCustomerID(CompanyID, DepartmentID, DivisionID, CustIDNew)

        End While


        Return CustIDNew
    End Function
#End Region


#Region "UpdateCustomerDetails"
    Sub UpdateCustomerDetails()
        Dim objCustomer As New CustomOrder()
 
        If drpCustomerID.Visible Then
            If drpCustomerID.SelectedValue = "Specify Customer" Then
                objCustomer.CustomerID = txtSpecifyCustomer.Text
            ElseIf drpCustomerID.SelectedValue = "Retail Customer" Then
                objCustomer.CustomerID = "Retail Customer"
            Else
                objCustomer.CustomerID = txtCustomerTemp.Text
            End If
        Else
            objCustomer.CustomerID = txtCustomerTemp.Text
        End If


        If objCustomer.CustomerID <> "Retail Customer" And objCustomer.CustomerID <> "5334972" And objCustomer.CustomerID <> "Returns" And objCustomer.CustomerID <> "C-69841" And objCustomer.CustomerID <> "C-67776" And objCustomer.CustomerID <> "C-69842" And objCustomer.CustomerID <> "C-69843" And objCustomer.CustomerID <> "C-67828" Then
            objCustomer.CustomerFirstName = txtCustomerFirstName.Text
            objCustomer.CustomerLastName = txtCustomerLastName.Text
            objCustomer.Attention = txtAttention.Text
            objCustomer.CustomerAddress1 = txtCustomerAddress1.Text
            objCustomer.CustomerAddress2 = txtCustomerAddress2.Text
            objCustomer.CustomerAddress3 = txtCustomerAddress3.Text
            objCustomer.CustomerCity = txtCustomerCity.Text

            objCustomer.CustomerCountry = drpCountry.SelectedValue
            If drpCountry.SelectedValue = "US" Then
                objCustomer.CustomerState = drpState.SelectedValue
            ElseIf drpCountry.SelectedValue = "CD" Then
                objCustomer.CustomerState = drpState.SelectedValue
                'Else
                '    objCustomer.CustomerState = 'txtCustomerState.Text
            End If

            objCustomer.CustomerFax = txtCustomerFax.Text
            objCustomer.CustomerPhone = txtCustomerPhone.Text
            objCustomer.CustomerEmail = txtCustomerEmail.Text
            objCustomer.CreditLimit = txtCreditLimit.Text
            objCustomer.AccountStatus = txtAccountStatus.Text
            objCustomer.CustomerSince = txtCustomerSince.Text
            objCustomer.Discounts = txtDiscounts.Text
            objCustomer.CreditComments = txtCreditComments.Text
            objCustomer.CompanyID = CompanyID
            objCustomer.DivisionID = DivisionID
            objCustomer.DepartmentID = DepartmentID
            'edited by jacob on 10/12/2007
            objCustomer.CustomerSalutation = IIf(DropDownList1.SelectedValue = "0", "", DropDownList1.SelectedValue)

            objCustomer.Ytdorders = txtYtdOrders.Text
            objCustomer.MemberPoints = txtMemberPoints.Text
            objCustomer.CustomerRank = txtCustomerRank.Text
            objCustomer.SalesLifeTime = txtSalesLifeTime.Text
            objCustomer.CustomerComments = txtComments.Text
            objCustomer.CustomerZip = txtCustomerZip.Text
            objCustomer.CustomerCell = txtCustomerCell.Text
            objCustomer.CustomerPhoneExt = txtExt.Text
            objCustomer.CustomerCompany = txtCompany.Text
            objCustomer.Newsletter = drpNewsletterID.SelectedValue

            objCustomer.PO = txtPO.Text
            Dim EmpID As String = Session("EmployeeUserName")
            objCustomer.EmployeeID = EmployeeID
            objCustomer.AddEdit2 = Session("AddEdit")
            objCustomer.UpdateCustomerDetails()


        Else
            objCustomer.CustomerFirstName = ""
            objCustomer.CustomerLastName = ""
            objCustomer.Attention = ""
            objCustomer.CustomerAddress1 = ""
            objCustomer.CustomerAddress2 = ""
            objCustomer.CustomerAddress3 = ""
            objCustomer.CustomerCity = ""

            objCustomer.CustomerCountry = ""
            objCustomer.CustomerState = drpState.SelectedValue

            objCustomer.CustomerFax = ""
            objCustomer.CustomerPhone = ""
            objCustomer.CustomerEmail = ""
            objCustomer.CreditLimit = ""
            objCustomer.AccountStatus = "Open"
            objCustomer.CustomerSince = txtCustomerSince.Text
            objCustomer.Discounts = txtDiscounts.Text
            objCustomer.CreditComments = txtCreditComments.Text
            objCustomer.CompanyID = CompanyID
            objCustomer.DivisionID = DivisionID
            objCustomer.DepartmentID = DepartmentID
            'edited by jacob on 10/12/2007
            objCustomer.CustomerSalutation = ""
            objCustomer.Ytdorders = txtYtdOrders.Text
            objCustomer.MemberPoints = txtMemberPoints.Text
            objCustomer.CustomerRank = txtCustomerRank.Text
            objCustomer.SalesLifeTime = txtSalesLifeTime.Text
            objCustomer.CustomerComments = txtComments.Text
            objCustomer.CustomerZip = ""
            objCustomer.CustomerCell = ""
            objCustomer.CustomerPhoneExt = ""
            objCustomer.CustomerCompany = ""
            objCustomer.Newsletter = drpNewsletterID.SelectedValue

            objCustomer.PO = txtPO.Text
            Dim EmpID As String = Session("EmployeeUserName")
            objCustomer.EmployeeID = EmpID
            objCustomer.AddEdit2 = Session("AddEdit")
            ' objCustomer.UpdateCustomerDetails()


            If objCustomer.CustomerID <> "5334972" And objCustomer.CustomerID <> "Returns" And objCustomer.CustomerID <> "C-69841" And objCustomer.CustomerID <> "C-67776" And objCustomer.CustomerID <> "C-69842" And objCustomer.CustomerID <> "C-69843" And objCustomer.CustomerID <> "C-67828" Then
                objCustomer.UpdateCustomerDetails()
            End If

            Dim obj As New clsCustomerInformation_RetailCustomer
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            obj.OrderNumber = lblOrderNumberData.Text
            obj.CustomerID = objCustomer.CustomerID
            obj.CustomerFirstName = txtCustomerFirstName.Text
            obj.CustomerLastName = txtCustomerLastName.Text
            obj.Attention = txtAttention.Text
            obj.CustomerAddress1 = txtCustomerAddress1.Text
            obj.CustomerAddress2 = txtCustomerAddress2.Text
            obj.CustomerAddress3 = txtCustomerAddress3.Text
            obj.CustomerCity = txtCustomerCity.Text
            obj.CustomerState = drpState.SelectedValue
            obj.CustomerCountry = drpCountry.SelectedValue
            obj.CustomerFax = txtCustomerFax.Text
            obj.CustomerEmail = txtCustomerEmail.Text
            obj.CreditLimit = "0"
            obj.AccountStatus = "Open"
            obj.CustomerSince = Date.Now.Date
            obj.CreditComments = ""
            obj.CustomerSalutation = IIf(DropDownList1.SelectedValue = "0", "", DropDownList1.SelectedValue)
            obj.CustomerZip = txtCustomerZip.Text
            obj.CustomerCell = txtCustomerCell.Text
            obj.CustomerPhoneExt = txtExt.Text
            obj.CustomerCompany = txtCompany.Text
            obj.Newsletter = drpNewsletterID.SelectedValue
            obj.Login = ""
            obj.Password = ""
            obj.CustomerPhone = txtCustomerPhone.Text

            obj.DeleteCustomerInformationDetail()
            obj.InsertCustomerInformationDetail()

        End If




    End Sub
#End Region

#Region "AddCustomerInformationFromOrderForm"
    Sub AddCustomerInformation(ByVal CustomerIDNew)

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim objCustomer As New CustomOrder()

        objCustomer.CustomerID = CustomerIDNew
        If drpPaymentType.SelectedValue = "Wire In" Then
            objCustomer.CustomerFirstName = txtVendorName.Text
        Else
            objCustomer.CustomerFirstName = txtCustomerFirstName.Text
        End If
        objCustomer.CustomerLastName = txtCustomerLastName.Text
        objCustomer.Attention = txtAttention.Text
        objCustomer.CustomerAddress1 = txtCustomerAddress1.Text
        objCustomer.CustomerAddress2 = txtCustomerAddress2.Text
        objCustomer.CustomerAddress3 = txtCustomerAddress3.Text
        objCustomer.CustomerCity = txtCustomerCity.Text
        objCustomer.CustomerCountry = drpCountry.SelectedValue
        If drpCountry.SelectedValue = "US" Then
            objCustomer.CustomerState = drpState.SelectedValue
        ElseIf drpCountry.SelectedValue = "CD" Then
            objCustomer.CustomerState = drpState.SelectedValue
        Else
            objCustomer.CustomerState = txtCustomerState.Text
        End If
        objCustomer.CustomerFax = txtCustomerFax.Text
        objCustomer.CustomerPhone = txtCustomerPhone.Text
        objCustomer.CustomerEmail = txtCustomerEmail.Text
        objCustomer.CreditLimit = txtCreditLimit.Text
        objCustomer.AccountStatus = txtAccountStatus.Text
        objCustomer.CustomerSince = txtCustomerSince.Text
        objCustomer.Discounts = txtDiscounts.Text
        objCustomer.CreditComments = txtCreditComments.Text
        objCustomer.CompanyID = CompanyID
        objCustomer.DivisionID = DivisionID
        objCustomer.DepartmentID = DepartmentID
        'edited by jacob  on 10/12/2007
        objCustomer.CustomerSalutation = IIf(DropDownList1.SelectedValue = "0", "", DropDownList1.SelectedValue)
        objCustomer.Ytdorders = txtYtdOrders.Text
        objCustomer.MemberPoints = txtMemberPoints.Text
        objCustomer.CustomerRank = txtCustomerRank.Text
        objCustomer.SalesLifeTime = txtSalesLifeTime.Text

        objCustomer.CustomerZip = txtCustomerZip.Text
        objCustomer.CustomerCell = txtCustomerCell.Text
        objCustomer.CustomerPhoneExt = txtExt.Text
        objCustomer.CustomerCompany = txtCompany.Text
        objCustomer.PO = txtPO.Text
        Dim Zip As String = txtCustomerZip.Text

        objCustomer.AddCustomerInformationFromOrderForm()

    End Sub
#End Region


#Region "drpTaxes_SelectedIndexChanged"
    Protected Sub drpTaxes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpTaxes.SelectedIndexChanged

        PopulatingTaxPercent()
        CheckItemGridOnPostBacks()

    End Sub
#End Region


    Protected Sub OrderDetailGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles OrderDetailGrid.PageIndexChanging
        OrderDetailGrid.PageIndex = e.NewPageIndex
        OrderDetailGridBindAfterAdding()
    End Sub

    Protected Sub OrderDetailGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles OrderDetailGrid.RowCommand
        lblCCMessage.Text = ""
        If e.CommandName = "EmptySelect" Then
            BindEmptyGrid()

        ElseIf e.CommandName = "EmptyInsert" Then

            AddEmptyGrid()
        ElseIf e.CommandName = "footerBindDetails" Then

            BindFooterGrid()

        ElseIf e.CommandName = "footerPostDetails" Then
            AddFooterGrid()
        End If
        If txtDiscountCode.Text.Trim() <> "" Then
            If txtDiscountCode.Enabled = True Then
                lblcodeerror.Text = ""
                txtDiscountCode_TextChanged(sender, e)
                If lblcodeerror.Text.Trim <> "" Then
                    lblCCMessage.Text = lblcodeerror.Text
                    lblCCMessage.Visible = True
                    Exit Sub
                Else
                    lblCCMessage.Text = ""
                End If
            End If
        End If


        ShippingDeliveryChargeNew(txtShippingZip.Text)

    End Sub

    Sub AddFooterGrid()

        Dim ItemID As TextBox = TryCast(OrderDetailGrid.FooterRow.FindControl("txtItemIDTemp"), TextBox)
        Dim ItemName As Label = TryCast(OrderDetailGrid.FooterRow.FindControl("lblfooterItemName"), Label)
        Dim ItemDescription As TextBox = TryCast(OrderDetailGrid.FooterRow.FindControl("txtItemDescription"), TextBox)
        Dim ItemUOM As Label = TryCast(OrderDetailGrid.FooterRow.FindControl("lblfooterItemUOM"), Label)
        Dim ItemUnitPrice As TextBox = TryCast(OrderDetailGrid.FooterRow.FindControl("txtfooterItemPrice"), TextBox)
        Dim ItemTotal As Label = TryCast(OrderDetailGrid.FooterRow.FindControl("lblfooterSubTotal"), Label)
        Dim drpEmptyItemQty As DropDownList = CType(OrderDetailGrid.FooterRow.FindControl("grdDrpQty"), DropDownList)
        'JMT Code on 11th August 2008 Starts here
        Dim drpFlatOrPercent As DropDownList = CType(OrderDetailGrid.FooterRow.FindControl("drpFlatPerc"), DropDownList)
        'JMT Code on 11th August 2008 Ends here

        Dim ItemDiscount As TextBox = TryCast(OrderDetailGrid.FooterRow.FindControl("txtfooterDiscountPerc"), TextBox)


        Dim SessionKey As Hashtable = New Hashtable
        Dim DiscountTextLength As String
        Dim CustomerDiscount As Decimal
        If txtDiscounts.Text <> "" Then
            DiscountTextLength = txtDiscounts.Text.Length - 1
            CustomerDiscount = Convert.ToDecimal((txtDiscounts.Text).Remove(DiscountTextLength, 1))
        End If

        Dim PopOrderNo As New CustomOrder()

        Dim ItemDiscountPerc As Decimal

        If ItemDiscount.Text.Trim() <> "" Then
            If IsNumeric(ItemDiscount.Text) Then
                Try
                    ItemDiscountPerc = ItemDiscount.Text
                Catch ex As Exception
                    ItemDiscountPerc = 0
                End Try
            Else
                Exit Sub

            End If

        Else
            ItemDiscountPerc = 0
        End If
        If ItemID.Text <> "" Then


            Quantity = drpEmptyItemQty.SelectedValue


            ItemIDFromTextBox = ItemID.Text


            Dim FillGridWithTaxAdd As New CustomOrder()


            dsAdd = FillGridWithTaxAdd.Inventory_GetTotalWithTax(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity, CustomerDiscount)

            Dim dsTempAdd As New Data.DataSet
            Dim dsTemp As New Data.DataSet
            dsTempAdd = FillGridWithTaxAdd.Inventory_GetTotalWithTax(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity, CustomerDiscount)

            If dsTempAdd.Tables(0).Rows.Count <> 0 Then

                Dim ObjBackOrder As New BackOrder()

                Dim DeliveryStatus As Integer

                DeliveryStatus = ObjBackOrder.CheckOrderQty(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity)
                If DeliveryStatus = 0 Then
                    lblInventoryStatus.Text = ItemIDFromTextBox + " is out of Stock"
                    pnlPricerange.Visible = True
                Else
                    pnlPricerange.Visible = False
                    lblInventoryStatus.Text = ""

                End If
                Dim grdDescription As String = ""
                If ItemDescription.Text.Trim() <> "" Then

                    grdDescription = ItemDescription.Text.Trim()

                Else
                    grdDescription = dsAdd.Tables(0).Rows(0).Item("Description").ToString()
                End If

                Dim grdItemUOM = dsAdd.Tables(0).Rows(0).Item("ItemUOM").ToString()
                Dim grdPrice

                Dim OrderQty = drpEmptyItemQty.SelectedValue
                Dim grdTotal

                If ItemUnitPrice.Text.Trim() <> "0.00" Then

                    grdPrice = ItemUnitPrice.Text


                Else
                    grdPrice = dsAdd.Tables(0).Rows(0).Item("ItemUnitPrice").ToString()

                End If


                If IsNumeric(grdPrice) Then



                    If grdPrice <> 0 Then

                        OrderQty = dsAdd.Tables(0).Rows(0).Item("OrderQty").ToString()
                        grdTotal = OrderQty * grdPrice
                        'JMT Code on 11th Starts here
                        If drpFlatOrPercent.SelectedValue = "Flat" Then
                            Try
                                ItemDiscountPerc = ItemDiscount.Text
                            Catch ex As Exception
                                ItemDiscountPerc = 0
                            End Try
                        Else
                            Try
                                ItemDiscountPerc = ItemDiscount.Text
                            Catch ex As Exception
                                ItemDiscountPerc = 0
                            End Try
                            ItemDiscountPerc = (ItemDiscountPerc / 100) * grdTotal
                        End If
                        If ItemDiscountPerc > grdTotal Then
                            lblErrorText.Visible = True
                            lblErrorText.Text = "<font color='red'>Discount Entry Error!</font>"
                            Exit Sub
                        End If
                        'JMT code on 11th Ends here
                        'JMT rewritten the following line
                        'grdTotal = grdTotal - (((ItemDiscountPerc / 100)) * grdTotal)
                        grdTotal = grdTotal - ItemDiscountPerc
                    Else


                        Session("CtrlFrmItemNotFound") = "True"

                        Exit Sub
                    End If
                Else
                    Exit Sub

                End If


                Dim grdTaxGroupID = "DEFAULT"
                Dim grdTaxAmount = 0.0
                Dim ItemsDetailSubmit As New CustomOrder()
                ItemsDetailSubmit.CompanyID = CompanyID
                ItemsDetailSubmit.DivisionID = DivisionID
                ItemsDetailSubmit.DepartmentID = DepartmentID
                ItemsDetailSubmit.OrderNumber = lblOrderNumberData.Text
                ItemsDetailSubmit.Description = grdDescription
                ItemsDetailSubmit.ItemID = ItemIDFromTextBox
                ItemsDetailSubmit.OrderQty = Quantity
                ItemsDetailSubmit.ItemUOM = grdItemUOM

                ItemsDetailSubmit.ItemUnitPrice = grdPrice



                Try
                    ItemsDetailSubmit.ItemDiscountPerc = Convert.ToDecimal(ItemDiscount.Text) ' ItemDiscountPerc 'JMT edited on 11th August 2008
                Catch ex As Exception
                    ItemsDetailSubmit.ItemDiscountPerc = 0
                End Try



                ItemsDetailSubmit.ItemDiscountFlatOrPercent = drpFlatOrPercent.SelectedValue 'JMT Added this line on 11th August 2008
                ItemsDetailSubmit.TaxGroupID = grdTaxGroupID
                ItemsDetailSubmit.TaxPercent = grdTaxAmount
                ItemsDetailSubmit.Total = grdTotal
                ItemsDetailSubmit.SubTotal = grdTotal

                If drpCustomerID.SelectedValue = "New Customer" And drpCustomerID.Visible = "True" Then
                    If txtCustomerFirstName.Text.Trim() <> "" Or txtCustomerLastName.Text.Trim() <> "" Or txtCustomerAddress1.Text.Trim() <> "" Then
                        If Session("CustomerIDNew") = "" And txtCustomerTemp.Text.Trim = "" Then
                            Dim CustomerIDNew As String = CreateNewCustomerID()

                            AddCustomerInformation(CustomerIDNew)
                            drpCustomerID.Visible = False
                            txtCustomerTemp.Visible = True
                            txtCustomerTemp.Text = CustomerIDNew
                            Session("CustomerIDNew") = CustomerIDNew
                            Session("CustIDCreatedFrmGrid") = "True"
                            ' EmailSendingWithoutBcc(CompanyID & "- CustomerIDNew " & CustomerIDNew & " -POS line number 4825  order no - " & lblOrderNumberData.Text, "Existing customerid - " & txtCustomerTemp.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
                        End If
                    End If
                ElseIf drpCustomerID.SelectedValue = "Retail Customer" And drpCustomerID.Visible = "True" Then
                    Dim obj As New CustomerImport
                    obj.CompanyID = CompanyID
                    obj.DivisionID = DivisionID
                    obj.DepartmentID = DepartmentID
                    obj.CustomerPhone = ""
                    obj.Password = ""
                    obj.Login = "Retail Customer"
                    obj.CustomerFirstName = "Retail Customer"
                    obj.CustomerLastName = "Retail Customer"
                    obj.Attention = ""
                    obj.CustomerAddress1 = ""
                    obj.CustomerAddress3 = ""
                    obj.CustomerAddress2 = ""
                    obj.CustomerCity = ""
                    obj.CustomerState = ""
                    obj.CustomerCountry = ""
                    obj.CustomerFax = ""
                    obj.CustomerEmail = ""
                    obj.CreditLimit = "0"
                    obj.AccountStatus = ""
                    obj.CustomerSince = Date.Now
                    obj.CreditComments = ""
                    obj.CustomerSalutation = ""
                    obj.CustomerZip = ""
                    obj.CustomerCell = ""
                    obj.CustomerPhoneExt = ""
                    obj.CustomerCompany = ""
                    obj.Newsletter = ""
                    obj.CustomerID = "Retail Customer"

                    If obj.GetCustomerID() = 0 Then
                        obj.InsertCustomerInformationDetail()
                    End If
                    txtCustomerTemp.Text = "Retail Customer"
                    UpdateCustomerDetails()
                Else
                    UpdateCustomerDetails()
                End If

                Dim OrderLineNumber As Integer = ItemsDetailSubmit.AddItemDetailsCustomisedGrid()
                Session("Oln") = ""

                Session("OrderLineNumber") = OrderLineNumber

                AddEditItemDetails()

                CalculationPart()
                PopulatingTaxPercent()
                AddEditItemDetails()

                OrderDetailGridBindAfterAdding()
                lblErrorText.Visible = False
            Else
                lblErrorText.Visible = True
                lblErrorText.Text = "<font color='red'>ItemID Not Found.</font>"
                Session("CtrlFrmItemNotFound") = "True"


            End If


 


        End If

    End Sub
    Sub BindFooterGrid()

        Dim ItemID As TextBox = TryCast(OrderDetailGrid.FooterRow.FindControl("txtItemIDTemp"), TextBox)
        Dim ItemName As Label = TryCast(OrderDetailGrid.FooterRow.FindControl("lblfooterItemName"), Label)
        Dim ItemDescription As TextBox = TryCast(OrderDetailGrid.FooterRow.FindControl("txtItemDescription"), TextBox)
        Dim ItemUOM As Label = TryCast(OrderDetailGrid.FooterRow.FindControl("lblfooterItemUOM"), Label)
        Dim ItemUnitPrice As TextBox = TryCast(OrderDetailGrid.FooterRow.FindControl("txtfooterItemPrice"), TextBox)
        Dim ItemTotal As Label = TryCast(OrderDetailGrid.FooterRow.FindControl("lblfooterSubTotal"), Label)


        'Dim CustID As String = Session("CustomerID").ToString()

        'Dim DiscountTextLength As String
        'Dim CustomerDiscount As Decimal


        Dim CustID As String = ""
        If Not Session("CustomerID") Is Nothing Then
            CustID = Session("CustomerID").ToString()
        End If

        Dim DiscountTextLength As String = ""
        Dim CustomerDiscount As Decimal = 0.0

        If txtDiscounts.Text <> "" Then
            DiscountTextLength = txtDiscounts.Text.Length - 1
            CustomerDiscount = Convert.ToDecimal((txtDiscounts.Text).Remove(DiscountTextLength, 1))
        End If

        Dim drpEmptyItemQty As DropDownList = CType(OrderDetailGrid.FooterRow.FindControl("grdDrpQty"), DropDownList)
        Quantity = drpEmptyItemQty.SelectedValue

        Dim SessionKey As Hashtable = New Hashtable
        Session("Quantity") = Quantity
        ItemIDFromTextBox = ItemID.Text
        ItemIDTemp = 1

        Dim FillGridWithTax As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = FillGridWithTax.Inventory_GetTotalWithTax(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity, CustomerDiscount)


        If ds.Tables(0).Rows.Count <> 0 Then
            Dim ObjBackOrder As New BackOrder()

            Dim DeliveryStatus As Integer

            DeliveryStatus = ObjBackOrder.CheckOrderQty(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity)
            If DeliveryStatus = 0 Then
                lblInventoryStatus.Text = ItemIDFromTextBox + " is out of Stock"

            Else
                lblInventoryStatus.Text = ""

            End If
            ItemName.Text = ds.Tables(0).Rows(0).Item("ItemName").ToString()
            ItemDescription.Text = ds.Tables(0).Rows(0).Item("Description").ToString()
            ItemUOM.Text = ds.Tables(0).Rows(0).Item("ItemUOM")
            ItemUnitPrice.Text = ds.Tables(0).Rows(0).Item("ItemUnitPrice").ToString()
            lblErrorText.Visible = False
            PopulatePriceRange()
        Else
            lblErrorText.Visible = True
            lblErrorText.Text = "<font color='red'>ItemID Not Found..</font>"
            Session("CtrlFrmItemNotFound") = "True"

        End If


    End Sub

    Sub AddEmptyGrid()

        Dim ItemID As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyItem"), TextBox)
        Dim ItemName As Label = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("lblEmptyItemName"), Label)
        Dim ItemDescription As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyItemDescription"), TextBox)
        Dim ItemUOM As Label = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("lblEmptyUOM"), Label)
        Dim ItemUnitPrice As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyUnitPrice"), TextBox)
        Dim ItemTotal As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("lblEmptyTotal"), TextBox)
        Dim drpEmptyItemQty As DropDownList = CType(OrderDetailGrid.Controls(0).Controls(0).FindControl("drpEmptyItemQty"), DropDownList)
        Dim ItemDiscount As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyDiscountPerc"), TextBox)
        'JMT Code on 11th August 2008 Starts here
        Dim drpFlatOrPercent As DropDownList = CType(OrderDetailGrid.Controls(0).Controls(0).FindControl("drpEmptyFlatPerc"), DropDownList)

        'JMT Code on 11th August 2008 Ends here

        Dim SessionKey As Hashtable = New Hashtable
        Dim DiscountTextLength As String
        Dim CustomerDiscount As Decimal
        Dim ItemDiscountPerc As Decimal
        Dim ItemDiscountPrice As Decimal

        If ItemDiscount.Text.Trim() <> "" Then
            If IsNumeric(ItemDiscount.Text) Then
                ItemDiscountPerc = ItemDiscount.Text
            Else
                Exit Sub

            End If
        Else
            ItemDiscountPerc = 0

        End If
        If txtDiscounts.Text <> "" Then
            DiscountTextLength = txtDiscounts.Text.Length - 1
            CustomerDiscount = Convert.ToDecimal((txtDiscounts.Text).Remove(DiscountTextLength, 1))
        End If

        'Order Number Generating
        Dim PopOrderNo As New CustomOrder()
        Dim rs As SqlDataReader
       
        If IsNumeric(lblOrderNumberData.Text.Trim) = False Then
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextOrderNumber")
            While rs.Read()
                OrdNumber = rs("NextNumberValue")

            End While
            rs.Close()
            lblOrderNumberData.Text = OrdNumber
            Session("OrderNumber") = OrdNumber
            'EmailSendingWithoutBcc(CompanyID & "-POS new order no" & OrdNumber & " - line number 5017", "Existing order - " & lblOrderNumberData.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
        End If


        If ItemID.Text <> "" Then


            Quantity = drpEmptyItemQty.SelectedValue


            ItemIDFromTextBox = ItemID.Text


            Dim FillGridWithTaxAdd As New CustomOrder()



            dsAdd = FillGridWithTaxAdd.Inventory_GetTotalWithTax(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity, CustomerDiscount)

            Dim dsTempAdd As New Data.DataSet
            Dim dsTemp As New Data.DataSet
            dsTempAdd = FillGridWithTaxAdd.Inventory_GetTotalWithTax(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity, CustomerDiscount)

            If dsTempAdd.Tables(0).Rows.Count <> 0 Then

                Dim ObjBackOrder As New BackOrder()

                Dim DeliveryStatus As Integer

                DeliveryStatus = ObjBackOrder.CheckOrderQty(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity)
                If DeliveryStatus = 0 Then
                    lblInventoryStatus.Text = ItemIDFromTextBox + " is out of Stock"
                    pnlPricerange.Visible = True
                Else
                    pnlPricerange.Visible = False
                    lblInventoryStatus.Text = ""

                End If
                Dim grdDescription As String = ""
                If ItemDescription.Text <> "" Then

                    grdDescription = ItemDescription.Text

                Else
                    grdDescription = dsAdd.Tables(0).Rows(0).Item("Description").ToString()
                End If

                Dim grdItemUOM = dsAdd.Tables(0).Rows(0).Item("ItemUOM").ToString()
                Dim grdPrice

                Dim OrderQty = drpEmptyItemQty.SelectedValue
                Dim grdTotal
                If ItemUnitPrice.Text.Trim() <> "0.00" Then

                    grdPrice = ItemUnitPrice.Text


                Else
                    grdPrice = dsAdd.Tables(0).Rows(0).Item("ItemUnitPrice").ToString()

                End If



                If IsNumeric(grdPrice) Then


                    If grdPrice <> 0 Then

                        OrderQty = dsAdd.Tables(0).Rows(0).Item("OrderQty").ToString()
                        'JMT Code on 11th August 2008 Starts here
                        grdTotal = OrderQty * grdPrice
                        If drpFlatOrPercent.SelectedValue = "Flat" Then
                            ItemDiscountPrice = ItemDiscountPerc
                        Else
                            ItemDiscountPrice = (ItemDiscountPerc / 100) * grdTotal
                        End If
                        'JMT Code on 11th August 2008 Ends here
                        'JMT Commented following two line
                        'ItemDiscountPrice = ((ItemDiscountPerc / 100))
                        grdTotal = (OrderQty * grdPrice)
                        'JMT rewritten the following line
                        'grdTotal = grdTotal - (((ItemDiscountPerc / 100)) * grdTotal)
                        grdTotal = grdTotal - ItemDiscountPrice
                        'JMT Code on 11th August 2008 Starts Here
                        If grdTotal < 0 Then
                            lblErrorText.Visible = True
                            lblErrorText.Text = "<font color='red'>Discount Entry Error!</font>"
                            Exit Sub
                        End If
                        'JMT Code on 11th August 2008 Ends Here
                    Else

                        Session("CtrlFrmItemNotFound") = "True"

                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If




                Dim grdTaxGroupID = "DEFAULT"
                Dim grdTaxAmount = 0.0
                Dim ItemsDetailSubmit As New CustomOrder()
                ItemsDetailSubmit.CompanyID = CompanyID
                ItemsDetailSubmit.DivisionID = DivisionID
                ItemsDetailSubmit.DepartmentID = DepartmentID
                ItemsDetailSubmit.OrderNumber = lblOrderNumberData.Text
                ItemsDetailSubmit.Description = grdDescription
                ItemsDetailSubmit.ItemID = ItemIDFromTextBox
                ItemsDetailSubmit.OrderQty = Quantity
                ItemsDetailSubmit.ItemUOM = grdItemUOM
                ItemsDetailSubmit.ItemUnitPrice = grdPrice

                ItemsDetailSubmit.ItemDiscountPerc = ItemDiscountPerc
                'JMT Code on 11th August 2008 Starts here
                ItemsDetailSubmit.ItemDiscountFlatOrPercent = drpFlatOrPercent.SelectedValue
                'JMT Code on 11th August 2008 Ends here
                ItemsDetailSubmit.TaxGroupID = grdTaxGroupID
                ItemsDetailSubmit.TaxPercent = grdTaxAmount
                ItemsDetailSubmit.Total = grdTotal
                ItemsDetailSubmit.SubTotal = grdTotal

                If drpCustomerID.SelectedValue = "New Customer" And drpCustomerID.Visible = "True" Then
                    If txtCustomerFirstName.Text.Trim() <> "" Or txtCustomerLastName.Text.Trim() <> "" Or txtCustomerAddress1.Text.Trim() <> "" Then
                        If txtCustomerTemp.Text.Trim = "" And Session("CustomerIDNew") = "" Then
                            Dim CustomerIDNew As String = CreateNewCustomerID()

                            AddCustomerInformation(CustomerIDNew)
                            drpCustomerID.Visible = False
                            txtCustomerTemp.Visible = True
                            txtCustomerTemp.Text = CustomerIDNew
                            Session("CustomerIDNew") = CustomerIDNew
                            Session("CustIDCreatedFrmGrid") = "True"
                            'EmailSendingWithoutBcc(CompanyID & "- CustomerIDNew " & CustomerIDNew & " -POS line number 5150  order no - " & lblOrderNumberData.Text, "Existing customerid - " & txtCustomerTemp.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
                        End If

                    End If
                ElseIf drpCustomerID.SelectedValue = "Retail Customer" And drpCustomerID.Visible = "True" Then
                    Dim obj As New CustomerImport
                    obj.CompanyID = CompanyID
                    obj.DivisionID = DivisionID
                    obj.DepartmentID = DepartmentID
                    obj.CustomerPhone = ""
                    obj.Password = ""
                    obj.Login = "Retail Customer"
                    obj.CustomerFirstName = "Retail Customer"
                    obj.CustomerLastName = "Retail Customer"
                    obj.Attention = ""
                    obj.CustomerAddress1 = ""
                    obj.CustomerAddress3 = ""
                    obj.CustomerAddress2 = ""
                    obj.CustomerCity = ""
                    obj.CustomerState = ""
                    obj.CustomerCountry = ""
                    obj.CustomerFax = ""
                    obj.CustomerEmail = ""
                    obj.CreditLimit = "0"
                    obj.AccountStatus = ""
                    obj.CustomerSince = Date.Now
                    obj.CreditComments = ""
                    obj.CustomerSalutation = ""
                    obj.CustomerZip = ""
                    obj.CustomerCell = ""
                    obj.CustomerPhoneExt = ""
                    obj.CustomerCompany = ""
                    obj.Newsletter = ""
                    obj.CustomerID = "Retail Customer"

                    If obj.GetCustomerID() = 0 Then
                        obj.InsertCustomerInformationDetail()
                    End If
                    txtCustomerTemp.Text = "Retail Customer"
                    UpdateCustomerDetails()
                Else
                    UpdateCustomerDetails()
                End If

                Dim OrderLineNumber As Integer = ItemsDetailSubmit.AddItemDetailsCustomisedGrid()
                Session("Oln") = ""

                Session("OrderLineNumber") = OrderLineNumber

                lblErrorText.Visible = False
            Else
                lblErrorText.Visible = True
                lblErrorText.Text = "<font color='red'>ItemID Not Found...</font>"
                Session("CtrlFrmItemNotFound") = "True"


            End If

 


        End If

        AddEditItemDetails()

        CalculationPart()
        PopulatingTaxPercent()

        AddEditItemDetails()

        OrderDetailGridBindAfterAdding()

    End Sub

    Sub OrderDetailGridBindAfterAdding()
        Dim OrdNumber As String
        OrdNumber = lblOrderNumberData.Text
        Dim FillItemDetailGrid As New CustomOrder()
        Dim dsTemp As New Data.DataSet
        dsTemp = FillItemDetailGrid.PopulateItemDetailsGrid(CompanyID, DepartmentID, DivisionID, OrdNumber)

        OrderDetailGrid.DataSource = dsTemp
        OrderDetailGrid.DataBind()
        CalculationPart()
        PopulatingTaxPercent()



    End Sub

    Sub BindEmptyGrid()

        Dim ItemID As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyItem"), TextBox)
        Dim ItemName As Label = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("lblEmptyItemName"), Label)
        Dim ItemDescription As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyItemDescription"), TextBox)
        Dim ItemUOM As Label = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("lblEmptyUOM"), Label)
        Dim ItemUnitPrice As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("txtEmptyUnitPrice"), TextBox)
        Dim ItemTotal As TextBox = TryCast(OrderDetailGrid.Controls(0).Controls(0).FindControl("lblEmptyTotal"), TextBox)

        'Dim CustID As String = Session("CustomerID").ToString()
        'Dim DiscountTextLength As String
        'Dim CustomerDiscount As Decimal

        Dim CustID As String = ""
        If Not Session("CustomerID") Is Nothing Then
            CustID = Session("CustomerID").ToString()
        End If

        Dim DiscountTextLength As String = ""
        Dim CustomerDiscount As Decimal = 0.0



        If txtDiscounts.Text <> "" Then
            DiscountTextLength = txtDiscounts.Text.Length - 1
            CustomerDiscount = Convert.ToDecimal((txtDiscounts.Text).Remove(DiscountTextLength, 1))
        End If

        Dim drpEmptyItemQty As DropDownList = CType(OrderDetailGrid.Controls(0).Controls(0).FindControl("drpEmptyItemQty"), DropDownList)
        Try
            Quantity = drpEmptyItemQty.SelectedValue
        Catch ex As Exception
            Quantity = 1
        End Try

        Dim SessionKey As Hashtable = New Hashtable
        Session("Quantity") = Quantity
        ItemIDFromTextBox = ItemID.Text
        ItemIDTemp = 1

        Dim FillGridWithTax As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = FillGridWithTax.Inventory_GetTotalWithTax(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity, CustomerDiscount)


        If ds.Tables(0).Rows.Count <> 0 Then
            Dim ObjBackOrder As New BackOrder()


            Dim DeliveryStatus As Integer

            DeliveryStatus = ObjBackOrder.CheckOrderQty(CompanyID, DepartmentID, DivisionID, ItemIDFromTextBox, Quantity)
            If DeliveryStatus = 0 Then
                lblInventoryStatus.Text = ItemIDFromTextBox + " is out of Stock"

            Else
                lblInventoryStatus.Text = ""

            End If
            ItemName.Text = ds.Tables(0).Rows(0).Item("ItemName").ToString()
            ItemDescription.Text = ds.Tables(0).Rows(0).Item("Description").ToString()
            Try
                ItemUOM.Text = ds.Tables(0).Rows(0).Item("ItemUOM")
            Catch ex As Exception
                ItemUOM.Text = "0" 'ds.Tables(0).Rows(0).Item("ItemUOM")
            End Try

            ItemUnitPrice.Text = ds.Tables(0).Rows(0).Item("ItemUnitPrice").ToString()
            lblErrorText.Visible = False
            PopulatePriceRange()
        Else
            lblErrorText.Visible = True
            lblErrorText.Text = "<font color='red'>ItemID Not Found....</font>"
            Session("CtrlFrmItemNotFound") = "True"

        End If

    End Sub

    Protected Sub OrderDetailGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles OrderDetailGrid.RowDeleting
        Dim OrdNumber As String = lblOrderNumberData.Text

        Dim OrderLineNumber As Integer = 0
        Dim DeleteOrderDetails As New CustomOrder()
        If OrderDetailGrid.DataKeys(e.RowIndex).Value.ToString() <> "" Then
            OrderLineNumber = Convert.ToInt32(OrderDetailGrid.DataKeys(e.RowIndex).Value)
            DeleteOrderDetails.DeleteOrderDetails(CompanyID, DepartmentID, DivisionID, OrdNumber, OrderLineNumber)

        End If

        Dim dsDelete As New Data.DataSet
        dsDelete = DeleteOrderDetails.PopulateItemDetailsGrid(CompanyID, DepartmentID, DivisionID, OrdNumber)
        OrderDetailGrid.DataSource = dsDelete
        OrderDetailGrid.DataBind()

        CalculationPart()
        PopulatingTaxPercent()
        Session("Oln") = ""

        AddEditItemDetails()

        BindGrid()

        If txtDiscountCode.Text.Trim() <> "" Then
            If txtDiscountCode.Enabled = True Then
                lblcodeerror.Text = ""
                txtDiscountCode_TextChanged(sender, e)
                If lblcodeerror.Text.Trim <> "" Then
                    lblCCMessage.Text = lblcodeerror.Text
                    lblCCMessage.Visible = True
                    Exit Sub
                Else
                    lblCCMessage.Text = ""
                End If
            End If
        End If


    End Sub
    Protected Sub OrderDetailGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles OrderDetailGrid.PreRender

        Dim drpEmptyItemQty As DropDownList = CType(OrderDetailGrid.Controls(0).Controls(0).FindControl("drpemptyitemqty"), DropDownList)

        If Not (drpEmptyItemQty Is Nothing) Then
            PopulateOrderQuantity(drpEmptyItemQty)
        Else
            Dim drpEmptyItemQty1 As DropDownList = CType(OrderDetailGrid.FooterRow.FindControl("grddrpqty"), DropDownList)
            If Not (drpEmptyItemQty1 Is Nothing) Then
                PopulateOrderQuantity(drpEmptyItemQty1)
            End If

            Dim a As Integer = OrderDetailGrid.Rows.Count
            Dim drpEmptyItemQty2 As DropDownList = CType(OrderDetailGrid.Rows(OrderDetailGrid.Rows.Count - 1).FindControl("grdqty"), DropDownList)
            If Not (drpEmptyItemQty2 Is Nothing) Then
                PopulateOrderQuantity(drpEmptyItemQty2)
            End If

        End If


    End Sub

    Protected Sub OrderDetailGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles OrderDetailGrid.RowEditing
        OrderDetailGrid.EditIndex = e.NewEditIndex
        Dim ItemQty As Integer
        Dim lbl As Label
        lbl = OrderDetailGrid.Rows(e.NewEditIndex).FindControl("lblQty")
        ItemQty = CInt(lbl.Text)
        OrderDetailGridBindAfterAdding()
        Dim drpEmptyItemQty2 As DropDownList = CType(OrderDetailGrid.Rows(e.NewEditIndex).FindControl("grdqty"), DropDownList)

        If Not (drpEmptyItemQty2 Is Nothing) Then
            PopulateOrderQuantity(drpEmptyItemQty2)
        End If

        drpEmptyItemQty2.SelectedIndex = ItemQty - 1
    End Sub

    Protected Sub OrderDetailGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles OrderDetailGrid.RowUpdating
        Dim txtDescription As TextBox = CType(OrderDetailGrid.Rows(e.RowIndex).FindControl("tbxUpdateItemDescription"), TextBox)
        Dim drpQty As DropDownList = CType(OrderDetailGrid.Rows(e.RowIndex).FindControl("grdQty"), DropDownList)
        Dim txtItemID As TextBox = CType(OrderDetailGrid.Rows(e.RowIndex).FindControl("txtUpdateItemID"), TextBox)
        Dim ItemUnitPrice As TextBox = CType(OrderDetailGrid.Rows(e.RowIndex).FindControl("txtUpdateItemPrice"), TextBox)

        Dim ItemDiscount As TextBox = CType(OrderDetailGrid.Rows(e.RowIndex).FindControl("txtUpdateDiscountPerc"), TextBox)

        'JMT Code on 14th August 2008 Starts here
        Dim drpFlatOrPercent As DropDownList = CType(OrderDetailGrid.Rows(e.RowIndex).FindControl("FlatPerc"), DropDownList)
        'JMT Code on 14th August 2008 Ends here
        Dim txtIDItem As String = txtItemID.Text
        Dim grdDescription As String = ""
        Dim grdPrice
        grdDescription = txtDescription.Text

        Dim OrderLineNumber As Integer = 0
        OrderLineNumber = Convert.ToInt32(OrderDetailGrid.DataKeys(e.RowIndex).Value)

        grdPrice = ItemUnitPrice.Text

        If Not IsNumeric(grdPrice) Then
            lblErrorText.Text = "<font color='red'>Please put numeric value for price!</font>"
            lblErrorText.Visible = True
            Exit Sub
        Else
            lblErrorText.Visible = False
        End If

        Dim ItemDiscountPerc As Decimal
        Dim ItemDiscountPrice As Decimal


        If ItemDiscount.Text.Trim() <> "" Then
            If IsNumeric(ItemDiscount.Text) Then
                ItemDiscountPerc = ItemDiscount.Text
            Else
                Exit Sub

            End If
        Else
            ItemDiscountPerc = 0

        End If
        Dim OrderQty = drpQty.SelectedValue


        If Not IsNumeric(OrderQty) Then
            lblErrorText.Text = "<font color='red'>Please Select Order Quantity!</font>"
            lblErrorText.Visible = True
            Exit Sub
        Else
            lblErrorText.Visible = False
        End If


        Dim grdTotal
        Dim SubTotal
        Dim TempItemDis
        Dim ItemDiscountFlatOrPercent As String = ""
        If drpFlatOrPercent.SelectedValue = "Flat" Then
            ItemDiscountFlatOrPercent = "Flat"
            SubTotal = OrderQty * grdPrice
            If ItemDiscountPerc <= SubTotal Then
                lblErrorText.Visible = False
                SubTotal = (OrderQty * grdPrice) - ItemDiscountPerc
                grdTotal = OrderQty * grdPrice - ItemDiscountPerc
            Else
                lblErrorText.Visible = True
                lblErrorText.Text = "<font color='red'>Discount Entry Error!</font>"
                Exit Sub
            End If
        Else
            ItemDiscountFlatOrPercent = "%"
            SubTotal = (OrderQty * grdPrice)
            TempItemDis = (OrderQty * grdPrice) * (ItemDiscountPerc / 100)
            If TempItemDis <= SubTotal Then
                lblErrorText.Visible = False
                SubTotal = (OrderQty * grdPrice) - ((OrderQty * grdPrice) * (ItemDiscountPerc / 100))
                grdTotal = OrderQty * grdPrice - ((OrderQty * grdPrice) * (ItemDiscountPerc / 100))
            Else
                lblErrorText.Visible = True
                lblErrorText.Text = "<font color='red'>Discount Entry Error!</font>"
                Exit Sub
            End If
        End If

        'SubTotal = (OrderQty * grdPrice) - ((OrderQty * grdPrice) * (ItemDiscountPerc / 100))
        'grdTotal = OrderQty * grdPrice - ((OrderQty * grdPrice) * (ItemDiscountPerc / 100))
        OrdNumber = lblOrderNumberData.Text
        Dim objUser As New DAL.CustomOrder()

        objUser.UpdateOrderDetails(CompanyID, DepartmentID, DivisionID, OrdNumber, OrderLineNumber, txtIDItem, grdDescription, grdPrice, OrderQty, SubTotal, grdTotal, ItemDiscountPerc, ItemDiscountFlatOrPercent)
        Dim ObjBackOrder As New BackOrder()


        Dim DeliveryStatus As Integer

        DeliveryStatus = ObjBackOrder.CheckOrderQty(CompanyID, DepartmentID, DivisionID, txtItemID.Text, OrderQty)
        If DeliveryStatus = 0 Then
            lblInventoryStatus.Text = txtItemID.Text + " is out of Stock"
            pnlPricerange.Visible = True

        Else
            lblInventoryStatus.Text = ""
            pnlPricerange.Visible = False

        End If
        Session("Oln") = ""

        OrderDetailGrid.EditIndex = -1
        Dim FillItemDetailGrid As New CustomOrder()
        Dim ds As New Data.DataSet
        ds = FillItemDetailGrid.PopulateItemDetailsGrid(CompanyID, DepartmentID, DivisionID, OrdNumber)

        OrderDetailGrid.DataSource = ds
        OrderDetailGrid.DataBind()




        CalculationPart()
        PopulatingTaxPercent()
        BindGrid()


        If txtDiscountCode.Text.Trim() <> "" Then
            If txtDiscountCode.Enabled = True Then
                lblcodeerror.Text = ""
                txtDiscountCode_TextChanged(sender, e)
                If lblcodeerror.Text.Trim <> "" Then
                    lblCCMessage.Text = lblcodeerror.Text
                    lblCCMessage.Visible = True
                    Exit Sub
                Else
                    lblCCMessage.Text = ""
                End If
            End If
        End If


    End Sub

    Public Sub PopulatePriceRange()
        Dim rs As SqlDataReader
        Dim objUser As New DAL.CustomOrder()
        Dim DeluxePrice As String = ""
        Dim PremiumPrice As String = ""
        Dim Price As String = ""

        rs = ObjUser.PopulatePriceRange(CompanyID, DivisionID, DepartmentID, ItemIDFromTextBox)
        While rs.Read()
            pnlPricerange.Visible = True
            If rs("DeluxePrice").ToString() <> "" Then
                DeluxePrice = " - " + Format(Decimal.Parse(rs("DeluxePrice").ToString()), "C")
            End If
            If rs("PremiumPrice").ToString() <> "" Then

                PremiumPrice = " - " + Format(Decimal.Parse(rs("PremiumPrice").ToString()), "C")

            End If

            lblPricerange.Text = Format(Decimal.Parse(rs("Price").ToString()), "C") + DeluxePrice + PremiumPrice

            '  ItemPrice = Format(Decimal.Parse(rs("Price").ToString()), "C") + " - " + Format(Decimal.Parse(rs("Deluxestepamount").ToString()) + Decimal.Parse(rs("Price").ToString()), "C") + " - " + Format(Decimal.Parse(rs("Premiumstepamount").ToString()) + Decimal.Parse(rs("Price").ToString()), "C")
            ItemPrice = rs("Price").ToString() + " - " + rs("DeluxePrice").ToString() + " - " + rs("PremiumPrice").ToString()


        End While

        rs.Close()
    End Sub

 
    Sub PopulateOrderQuantity(ByVal drp As DropDownList)
        Dim Obj As New CustomOrder
        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Obj.CompanyID = CompanyID
        Obj.DepartmentID = DepartmentID
        Obj.DivisionID = DivisionID
        'Dim QtyStart As Integer
        Dim Qtyend As Integer

        Dim i As Integer
        Dim rs As DataTable

        rs = Obj.PopulateQtyRange()
        drp.AppendDataBoundItems = True
        If rs.Rows.Count > 0 Then
            Qtyend = rs.Rows(0)(0)
        Else
            Qtyend = 50
        End If

        If drp.Items.Count < 1 Then
            For i = 1 To Qtyend Step 1
                Dim l As New ListItem
                l.Text = i
                l.Value = i

                drp.Items.Add(l)
            Next
        End If
        'drpEmptyItemQty.SelectedIndex = 0

    End Sub

    
#Region "AddEditItemDetails"
    Sub AddEditItemDetails()
        Dim OrderNo As String = ""
        Dim PopOrderNo As New CustomOrder()
        Dim HeaderDetailSubmit As New CustomOrder()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)

        If IsNumeric(lblOrderNumberData.Text.Trim) = False Then
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextOrderNumber")
            While rs.Read()
                OrderNo = rs("NextNumberValue")

            End While
            rs.Close()
            lblOrderNumberData.Text = OrderNo
            Session("OrderNumber") = OrderNo
            'EmailSendingWithoutBcc(CompanyID & "- POS new order no" & OrderNo & " - line number 5602", "Existing order - " & lblOrderNumberData.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
        Else
            'lblOrderNumberData.Text = Session("OrderNumber")
            OrderNo = lblOrderNumberData.Text
        End If
         

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        HeaderDetailSubmit.CompanyID = CompanyID
        HeaderDetailSubmit.DivisionID = DivisionID
        HeaderDetailSubmit.DepartmentID = DepartmentID

        HeaderDetailSubmit.OrderNumber = OrderNo

        If IsNumeric(HeaderDetailSubmit.OrderNumber) Then
        Else
            Exit Sub
        End If

        ''new changes
        If drpTransaction.SelectedValue.ToLower = "Wire Out".ToLower Or drpTransaction.SelectedValue.ToLower = "Wire_Out".ToLower Or drpShipMethod.SelectedValue.ToLower = "Wire Out".ToLower Or drpShipMethod.SelectedValue.ToLower = "Wire_Out".ToLower Then
            HeaderDetailSubmit.TransactionTypeID = "Wire_Out"
        Else
            HeaderDetailSubmit.TransactionTypeID = drpTransaction.SelectedValue
        End If
        ''new changes


        HeaderDetailSubmit.OrderTypeID = drpOrderTypeIDData.SelectedValue

        'HeaderDetailSubmit.OrderDate = lblOrderDate.Text
        'HeaderDetailSubmit.OrderShipDate = Convert.ToDateTime(txtDeliveryDate.Text)

        If drpCustomerID.SelectedValue = "Specify Customer" Then
            HeaderDetailSubmit.CustomerID = txtSpecifyCustomer.Text
        ElseIf lkpCustomerID.Value <> "" Then
            HeaderDetailSubmit.CustomerID = lkpCustomerID.Value
        Else
            HeaderDetailSubmit.CustomerID = txtCustomerTemp.Text
        End If

        ' HeaderDetailSubmit.CustomerID = txtCustomerTemp.Text
        HeaderDetailSubmit.EmployeeID = drpEmployeeID.SelectedValue
        HeaderDetailSubmit.Assignedto = drpAssignedto.SelectedValue
        HeaderDetailSubmit.ShipMethodID = drpShipMethod.SelectedValue
        HeaderDetailSubmit.ShippingAddress1 = txtShippingAddress1.Text
        HeaderDetailSubmit.ShippingAddress2 = txtShippingAddress2.Text
        HeaderDetailSubmit.ShippingAddress3 = txtShippingAddress3.Text
        HeaderDetailSubmit.ShippingCity = txtShippingCity.Text

        HeaderDetailSubmit.CustomerComments = txtComments.Text

        HeaderDetailSubmit.ShippingCountry = drpShipCountry.SelectedValue
        If drpShipCountry.SelectedValue = "US" Then
            HeaderDetailSubmit.ShippingState = drpShippingState.SelectedValue
        ElseIf drpShipCountry.SelectedValue = "CD" Then
            HeaderDetailSubmit.ShippingState = drpShippingState.SelectedValue
        Else
            HeaderDetailSubmit.ShippingState = txtShippingState.Text
        End If

        'HeaderDetailSubmit.ShippingState = drpShippingState.SelectedValue


        HeaderDetailSubmit.ShippingZip = txtShippingZip.Text
        HeaderDetailSubmit.PaymentMethodID = drpPaymentType.SelectedValue
        HeaderDetailSubmit.CreditCardType = drpCardType.SelectedValue

        HeaderDetailSubmit.CreditCardNumber = txtCard.Text
        HeaderDetailSubmit.CreditCardExpDate = drpExpirationDate.SelectedValue

        'HeaderDetailSubmit.CreditCardCSVNumber = "" ' txtCSV.Text

        If checkstatusPaymentAccountsStatus() Then
            HeaderDetailSubmit.CreditCardCSVNumber = txtCSV.Text
        Else
            HeaderDetailSubmit.CreditCardCSVNumber = "" 'txtCSV.Text
        End If

        HeaderDetailSubmit.CreditCardApprovalNumber = txtApproval.Text
        HeaderDetailSubmit.CreditCardValidationCode = txtValidation.Text
        HeaderDetailSubmit.CreditCardBillToZip = txtBillZipCode.Text
        HeaderDetailSubmit.FraudRating = txtFraudRating.Text
        HeaderDetailSubmit.IpAddress = txtIpAddress.Text

        HeaderDetailSubmit.ShippingCell = txtShipCustomerCell.Text
        HeaderDetailSubmit.ShippingExt = txtShipExt.Text
        HeaderDetailSubmit.ShippingFax = txtShipCustomerFax.Text
        HeaderDetailSubmit.ShippingPhone = txtShipCustomerPhone.Text
        HeaderDetailSubmit.ShippingAttention = txtShippingAttention.Text

        HeaderDetailSubmit.OccasionCode = drpOccasionCode.Text
        HeaderDetailSubmit.Priority = drpPriorirty.SelectedValue

        HeaderDetailSubmit.DestinationType = drpDestinationType.SelectedValue
        HeaderDetailSubmit.OrderShipDate = txtDeliveryDate.Text
        HeaderDetailSubmit.ShippingFirstName = txtShippingName.Text
        HeaderDetailSubmit.ShippingLastName = txtShippingLastName.Text
        HeaderDetailSubmit.ShippingCompany = txtShipCompany.Text
        'edited by jacob  on 10/12/2007
        HeaderDetailSubmit.ShippingSalutation = IIf(drpShipCustomerSalutation.SelectedValue = "0", "", drpShipCustomerSalutation.SelectedValue)

        HeaderDetailSubmit.WarehouseID = drpLocation.SelectedValue
        HeaderDetailSubmit.ProjectID = drpProject.SelectedValue
        HeaderDetailSubmit.OrderSourceCode = drpSource.SelectedValue
        HeaderDetailSubmit.CheckID = txtID.Text
        HeaderDetailSubmit.CheckNumber = txtCheck.Text
        HeaderDetailSubmit.GiftCertificate = txtGiftCertificate.Text
        HeaderDetailSubmit.Coupon = txtCoupon.Text
        '  HeaderDetailSubmit.WireService = txtWireService.Text
        HeaderDetailSubmit.WireService = drpWire.SelectedValue
        HeaderDetailSubmit.WireCode = txtCode.Text
        HeaderDetailSubmit.WireRefernceID = txtReference.Text
        HeaderDetailSubmit.WireTransmitMethod = txtTransmitMethod.Text
        HeaderDetailSubmit.DriverRouteInfo = txtDriverRouteInfo.Text
        HeaderDetailSubmit.InternalNotes = txtInternalNotes.Text

        Try
            HeaderDetailSubmit.SubTotal = txtSubtotal.Text
        Catch ex As Exception
            HeaderDetailSubmit.SubTotal = 0
        End Try



        HeaderDetailSubmit.TaxGroupID = drpTaxes.SelectedValue
        If drpPaymentType.SelectedValue = "Wire In" Then

            HeaderDetailSubmit.TaxPercent = 0.0

        Else
            HeaderDetailSubmit.TaxPercent = txtTaxPercent.Text.Replace("%", "")

        End If
        HeaderDetailSubmit.TaxAmount = txtTax.Text

        HeaderDetailSubmit.Discounts = txtDiscountAmount.Text
        HeaderDetailSubmit.PO = txtPO.Text

        HeaderDetailSubmit.Total = txtTotal.Text


        If txtService.Text.Trim() <> "" Then
            If IsNumeric(txtService.Text.Trim()) Then
                HeaderDetailSubmit.Service = txtService.Text.Trim()
            Else
                HeaderDetailSubmit.Service = 0

            End If
        End If

        If txtDelivery.Text.Trim() <> "" Then
            If IsNumeric(txtDelivery.Text.Trim()) Then
                HeaderDetailSubmit.Delivery = txtDelivery.Text.Trim()
            Else

                HeaderDetailSubmit.Delivery = 0
            End If

        End If
        If txtRelay.Text.Trim() <> "" Then
            If IsNumeric(txtRelay.Text.Trim()) Then
                HeaderDetailSubmit.Relay = txtRelay.Text.Trim()

            Else
                HeaderDetailSubmit.Relay = 0

            End If


        End If
        '  txtIpAddress.Text
        ' txtFraudRating.Text
        ' HeaderDetailSubmit.CreditCardBillToZip = txtBillZipCode.Text

        If Session("AddEdit") <> " " Then
            HeaderDetailSubmit.AddEdit2 = Session("AddEdit")
        End If
        If lblDiscountCodeAmount.Text.Trim() <> "" Then
            If IsNumeric(lblDiscountCodeAmount.Text.Trim()) Then
                HeaderDetailSubmit.DiscountCouponAmount = lblDiscountCodeAmount.Text.Trim()
            Else
                HeaderDetailSubmit.DiscountCouponAmount = 0
            End If
        End If
        HeaderDetailSubmit.Coupon = txtDiscountCode.Text

        HeaderDetailSubmit.AddEdit = 1
        'Adding Customised Grid Values
        HeaderDetailSubmit.AddEditHeaderDetails()

        UpdateOrderOverride()

    End Sub
#End Region


    Protected Sub btnlinkCC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlinkCC.Click

        lbltxtCard.Visible = False
        txtCard.Visible = True
        txtCard.Text = ""
        btnlinkCC.Visible = False

    End Sub

#Region "BindCustomerDetails"

    Public Function OrderHeaderCreditCardProcessingDetails2ndList(ByVal Ordernumber As String) As DataTable

        Dim myCon As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("dbo.[OrderHeaderCreditCardProcessingDetails2ndList]", myCon)
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



        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar)
        parameterOrderNumber.Value = Ordernumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim dt As New DataTable
        adapter.Fill(dt)


        Return dt

    End Function



    Sub BindGridOrderHeaderCreditCardProcessingDetails2ndList(ByVal OrderNumber As String)
        Dim dtorder As New DataTable
        dtorder = OrderHeaderCreditCardProcessingDetails2ndList(OrderNumber)
        If dtorder.Rows.Count <> 0 Then
            gridPaymentGatewayTransactionLogs.DataSource = dtorder
            gridPaymentGatewayTransactionLogs.DataBind()
            gridPaymentGatewayTransactionLogs.Visible = True
            gridPaymentGatewayTransactionLogs.AllowPaging = False
        Else
            gridPaymentGatewayTransactionLogs.Visible = False
        End If


    End Sub


    Public Function UpdateOrderOverride() As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "UPDATE OrderHeader set DiscountOverride=@DiscountOverride,DiscountOverrideValue=@DiscountOverrideValue,DiscountOverrideType=@DiscountOverrideType,[ShippingZone]=@ShippingZone,DeliveryOverride=@DeliveryOverride,ServiceOverride=@ServiceOverride,RelayOverride=@RelayOverride  Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@DeliveryOverride", SqlDbType.Bit)).Value = chkDeliveryOverride.Checked
            com.Parameters.Add(New SqlParameter("@ServiceOverride", SqlDbType.Bit)).Value = chkServiceOverride.Checked
            com.Parameters.Add(New SqlParameter("@RelayOverride", SqlDbType.Bit)).Value = chkRelayOverride.Checked
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text
            com.Parameters.Add(New SqlParameter("@ShippingZone", SqlDbType.NVarChar, 50)).Value = drpZone.SelectedValue

            com.Parameters.Add(New SqlParameter("@DiscountOverride", SqlDbType.Bit)).Value = checkDiscounts.Checked
            com.Parameters.Add(New SqlParameter("@DiscountOverrideValue", SqlDbType.NVarChar, 36)).Value = txtdiscounttypevalue.Text
            com.Parameters.Add(New SqlParameter("@DiscountOverrideType", SqlDbType.NVarChar, 50)).Value = drpdiscounttype.SelectedValue


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function



    Public Function SelectOverride() As DataTable


        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select DeliveryOverride,ServiceOverride,RelayOverride,[DiscountOverride]  , [DiscountOverrideType] , [DiscountOverrideValue] from   OrderHeader where CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2  And OrderNumber=@f5"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)

        Try
            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = lblOrderNumberData.Text

            da.SelectCommand = com

            da.Fill(dt)
        Catch ex As Exception

        End Try




        Return dt
    End Function



    Public Sub BindCustomerDetails(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdN As String)

        '***started code added by Imtiyaz
        'variable for check the Transmit button visibility and enable control
        Dim Posted, Picked, Shipped, invoiced, transid As String


        Dim OrderRs As SqlDataReader
        Dim PopOrderNo As New CustomOrder()
        OrderRs = PopOrderNo.PopulateOrder(CompanyID, DepartmentID, DivisionID, OrdN)
        While OrderRs.Read()
            BindGridOrderHeaderCreditCardProcessingDetails2ndList(OrdN)
            '***started code added by Imtiyaz
            'variable for check the Transmit button visibility and enable control
            Posted = OrderRs("Posted")
            Picked = OrderRs("Picked")
            Shipped = OrderRs("Shipped")
            invoiced = OrderRs("invoiced")
            '***started code added by Imtiyaz

            lblOrderNumberData.Text = OrderRs("OrderNumber").ToString()

            Try
                lblOrderDate.Text = Convert.ToDateTime(OrderRs("OrderDate")).ToShortDateString
            Catch ex As Exception

            End Try


            Try
                If OrderRs("LocationID").ToString() <> "" Then
                    cmblocationid.SelectedIndex = cmblocationid.Items.IndexOf(cmblocationid.Items.FindByValue(OrderRs("LocationID").ToString()))
                End If
            Catch ex As Exception

            End Try

            Session("OrderLocationid") = cmblocationid.SelectedValue

            If OrderRs("EmployeeID").ToString() <> "" Then
                drpEmployeeID.SelectedIndex = drpEmployeeID.Items.IndexOf(drpEmployeeID.Items.FindByValue(OrderRs("EmployeeID")))
            End If

            If OrderRs("ShippingZone").ToString() <> "" Then
                Try
                    drpZone.SelectedIndex = drpZone.Items.IndexOf(drpZone.Items.FindByValue(OrderRs("ShippingZone")))
                    If drpZone.SelectedIndex = 0 Then
                        drpZone.Items.Add(OrderRs("ShippingZone"))
                        drpZone.SelectedValue = OrderRs("ShippingZone")
                    End If
                Catch ex As Exception

                End Try
            End If

            Try
                If OrderRs("Assignedto").ToString() <> "" Then
                    drpAssignedto.SelectedIndex = drpAssignedto.Items.IndexOf(drpAssignedto.Items.FindByValue(OrderRs("Assignedto")))
                End If
            Catch ex As Exception

            End Try

            If OrderRs("ProjectID").ToString() <> "" Then
                drpProject.SelectedIndex = drpProject.Items.IndexOf(drpProject.Items.FindByValue(OrderRs("ProjectID")))

            End If
            If OrderRs("WarehouseID").ToString() <> "" Then
                drpLocation.SelectedIndex = drpLocation.Items.IndexOf(drpLocation.Items.FindByValue(OrderRs("WarehouseID")))

            End If


            If OrderRs("PaymentMethodID").ToString() <> "" Then
                drpPaymentType.SelectedIndex = drpPaymentType.Items.IndexOf(drpPaymentType.Items.FindByValue(OrderRs("PaymentMethodID")))
                Dim sn As Object
                Dim en As System.EventArgs
                drpPaymentType_SelectedIndexChanged(sn, en)

                Session("StartPaymentMethodID") = OrderRs("PaymentMethodID").ToString()
            End If
            GetGiftCardApprovalnumber()
            If drpPaymentType.SelectedValue = "Wire In" Then
                If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "Demo Site-90210" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
                    'txtTaxPercentGST.Visible = True
                    'txtTaxPercentPST.Visible = True
                    'txtTaxPST.Visible = True
                    'txtTaxGST.Visible = True
                    'drpTaxesPST.Visible = True
                    'drpTaxesGSTHST.Visible = True
                    'drpTaxes.Visible = False
                    'txtTax.Visible = False
                    'txtTaxPercent.Visible = False

                    'drpTaxesPST.Items.Clear()
                    'drpTaxesGSTHST.Items.Clear()

                    'drpTaxesPST.Items.Add("0")
                    'drpTaxesGSTHST.Items.Add("0")

                    txtTaxPercentGST.Text = 0
                    txtTaxPercentPST.Text = 0


                    txtTax.Text = 0
                    txtTaxPercent.Text = 0

                    txtTaxPST.Text = 0
                    txtTaxGST.Text = 0

                    'Wire In
                End If
            End If



            drpOrderTypeIDData.SelectedIndex = drpOrderTypeIDData.Items.IndexOf(drpOrderTypeIDData.Items.FindByValue(OrderRs("OrderTypeID")))
            drpTransaction.SelectedIndex = drpTransaction.Items.IndexOf(drpTransaction.Items.FindByValue(OrderRs("TransactionTypeID")))
            txtCustomerTemp.Text = OrderRs("CustomerID").ToString()

            If txtCustomerTemp.Text.Trim() <> "" Then
                drpCustomerID.Visible = False
                lnkBackToOption.Visible = False
                lkpCustomerID.Visible = False
                txtCustomerTemp.Visible = True
            End If

            Session("CustomerID") = OrderRs("CustomerID").ToString()


            Try
                txtPO.Text = OrderRs("PurchaseOrderNumber").ToString()
            Catch ex As Exception

            End Try


            ' New code handle StoredCreditCardBit
            Dim StoredCreditCardBit As Boolean = False
            Try
                StoredCreditCardBit = OrderRs("StoredCreditCard").ToString()
            Catch ex As Exception

            End Try
            If StoredCreditCardBit Then
                Dim CustomerCardDetailsForlinenumber As String = ""
                Dim updatebillingCC As Boolean = False

                Try
                    CustomerCardDetailsForlinenumber = OrderRs("CustomerCardDetailsForlinenumber").ToString()
                Catch ex As Exception

                End Try
                Try
                    updatebillingCC = OrderRs("updatebillingCC").ToString()
                Catch ex As Exception

                End Try

                BindStoredCreditCard(OrdN, txtCustomerTemp.Text, CustomerCardDetailsForlinenumber, updatebillingCC)
            End If
            ' New code handle StoredCreditCardBit


            'Edited by Jacob
            Dim Decrypt As New Encryption


            If OrderRs("CreditCardNumber").ToString() <> "" Then
                Try
                    txtCard.Text = Decrypt.TripleDESDecode(OrderRs("CreditCardNumber").ToString(), OrderRs("CustomerID").ToString().ToUpper())
                Catch ex As Exception

                    Try
                        txtCard.Text = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(OrderRs("CreditCardNumber").ToString(), OrderRs("CustomerID").ToString().ToUpper())
                    Catch ex2 As Exception

                    End Try
                End Try

                Try
                    Dim crd As String
                    crd = txtCard.Text.Trim

                    'Last number display X in Credit cards
                    Dim cardNo As String = ""
                    Dim cLen As Integer = 0
                    Dim subLen As Integer = 0
                    Dim SubcardNo As String = ""
                    cardNo = crd
                    cLen = cardNo.Length()
                    Dim slen As Integer = 0
                    If cLen > 0 Then
                        If cLen > 12 Then
                            subLen = cLen - 12
                            'SubcardNo = cardNo.Substring(0, subLen)
                            SubcardNo = cardNo.Substring(12, subLen)
                            slen = SubcardNo.Length()

                            If slen > 4 Then
                                SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                            End If
                            'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                            cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                        Else
                            cardNo = RepeatChar("X", cLen)
                        End If
                    End If

                    lbltxtCard.Text = cardNo

                    lbltxtCard.Visible = True
                    txtCard.Visible = False
                    btnlinkCC.Visible = True


                    Dim bl_Posted As Boolean = False
                    Try
                        bl_Posted = Posted
                    Catch ex As Exception

                    End Try

                    If bl_Posted Then
                        btnlinkCC.Visible = False
                    End If

                Catch ex As Exception

                End Try

            Else
                txtCard.Text = OrderRs("CreditCardNumber").ToString()

            End If


            ''New codeded for the Expiration date selection
            Dim exdat As String = ""
            Dim exdate As Date
            Try
                exdat = OrderRs("CreditCardExpDate").ToString()
                exdate = exdat
                exdat = exdate.ToString("MM/yyyy")
            Catch ex As Exception

            End Try
            ''New codeded for the Expiration date selection


            drpExpirationDate.SelectedIndex = drpExpirationDate.Items.IndexOf(drpExpirationDate.Items.FindByValue(exdat))



            txtCSV.Text = "" ' OrderRs("CreditCardCSVNumber").ToString()
            txtApproval.Text = OrderRs("CreditCardApprovalNumber").ToString()
            txtValidation.Text = OrderRs("CreditCardValidationCode").ToString()
            txtBillZipCode.Text = OrderRs("CreditCardBillToZip").ToString()
            txtFraudRating.Text = OrderRs("FraudRating").ToString()
            txtIpAddress.Text = OrderRs("IpAddress").ToString()

            If txtApproval.Text.Trim <> "" Then
                txtCSV.Text = ""
            End If

            txtCardMessageDesc.Text = Server.HtmlDecode(OrderRs("CardMessage").ToString())

            txtID.Text = OrderRs("CheckID").ToString()
            txtCheck.Text = OrderRs("CheckNumber").ToString()
            txtGiftCertificate.Text = OrderRs("GiftCertificate").ToString()
            txtCoupon.Text = OrderRs("Coupon").ToString()
            drpWire.SelectedIndex = drpWire.Items.IndexOf(drpWire.Items.FindByValue(OrderRs("WireService").ToString()))
            txtCode.Text = OrderRs("WireCode").ToString()
            txtReference.Text = OrderRs("WireRefernceID").ToString()
            txtTransmitMethod.Text = OrderRs("WireTransmitMethod").ToString()

            txtDiscountCode.Text = OrderRs("Coupon").ToString()


            If IsNumeric(OrderRs("DiscountCouponAmount").ToString()) Then
                lblDiscountCodeAmount.Text = OrderRs("DiscountCouponAmount").ToString()
            Else
                lblDiscountCodeAmount.Text = 0
            End If

            'GetOrderApprovalNumber
            If drpPaymentType.SelectedValue = "Credit Card" Then
                If Convert.ToBoolean(OrderRs("Posted")) = True Then
                    If OrderRs("CreditCardApprovalNumber").ToString().Trim() = "" Then
                        txtApproval.Text = GetOrderApprovalNumber(CompanyID, DepartmentID, DivisionID, OrdN)
                    End If
                End If
            End If
            'GetOrderApprovalNumber



            ''New Section added here
            Dim obj As New clsOrderEntryFormWireIn
            obj.OrderNumber = OrdN
            obj.CompanyID = CompanyID
            obj.DepartmentID = DepartmentID
            obj.DivisionID = DivisionID
            Dim dt As New DataTable
            dt = obj.DetailsOrderHeader

            If dt.Rows.Count <> 0 Then
                Try
                    txtrepresentative.Text = CType(dt.Rows(0)("RepresentativeTalkedTo").ToString(), String)
                    txtReceivedamount.Text = CType(dt.Rows(0)("ReceivedAmount").ToString(), String)
                    txtFloristname.Text = CType(dt.Rows(0)("FloristName").ToString(), String)
                    txtFloristcity.Text = CType(dt.Rows(0)("FloristCity").ToString(), String)
                    txtFloristPhone.Text = CType(dt.Rows(0)("FloristPhone").ToString(), String)
                    txtFloriststate.Text = CType(dt.Rows(0)("FloristState").ToString(), String)
                Catch ex As Exception

                End Try

            End If
            ''New Section added here


            txtShippingName.Text = OrderRs("ShippingFirstName").ToString()

            drpShipCustomerSalutation.SelectedIndex = drpShipCustomerSalutation.Items.IndexOf(drpShipCustomerSalutation.Items.FindByValue(OrderRs("ShippingSalutation").ToString()))
            txtShippingLastName.Text = OrderRs("ShippingLastName").ToString()
            txtShippingAttention.Text = OrderRs("ShippingAttention").ToString()
            txtShippingAddress1.Text = OrderRs("ShippingAddress1").ToString()
            txtShippingAddress2.Text = OrderRs("ShippingAddress2").ToString()
            txtShippingAddress3.Text = OrderRs("ShippingAddress3").ToString()
            txtShippingCity.Text = OrderRs("ShippingCity").ToString()
            drpShipCountry.SelectedIndex = drpShipCountry.Items.IndexOf(drpShipCountry.Items.FindByValue(OrderRs("ShippingCountry").ToString()))
            PopulateState(CompanyID, DivisionID, DepartmentID, drpShipCountry.SelectedValue)
            drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(OrderRs("ShippingState").ToString()))
            Dim drpShippingCountry As String = OrderRs("ShippingCountry").ToString()
            If drpShippingCountry = "US" Then
                drpShippingState.Visible = True
                txtShippingState.Visible = False
            ElseIf drpShippingCountry = "CD" Then
                drpShippingState.Visible = True
                txtShippingState.Visible = False
            Else
                drpShippingState.Visible = False

                txtShippingState.Visible = True
                txtShippingState.Text = OrderRs("ShippingState").ToString()

            End If


            drpSource.SelectedIndex = drpSource.Items.IndexOf(drpSource.Items.FindByValue(OrderRs("SourceCodeID").ToString()))
            txtShippingZip.Text = OrderRs("ShippingZip").ToString()

          

            txtShipCustomerCell.Text = OrderRs("ShippingCell").ToString()
            txtShipExt.Text = OrderRs("ShippingExt").ToString()
            txtShipCompany.Text = OrderRs("ShippingCompany").ToString()
            txtShipCustomerFax.Text = OrderRs("ShippingFax").ToString()
            txtShipCustomerPhone.Text = OrderRs("ShippingPhone").ToString()
            drpDestinationType.Items.IndexOf(drpDestinationType.Items.FindByValue(OrderRs("DestinationType").ToString()))
            drpOccasionCode.SelectedIndex = drpOccasionCode.Items.IndexOf(drpOccasionCode.Items.FindByValue(OrderRs("OccasionCode").ToString()))
            ''drpPriorirty.SelectedIndex = drpPriorirty.Items.IndexOf(drpPriorirty.Items.FindByValue(OrderRs("Priority").ToString()))

            If OrderRs("Priority").ToString() <> "" Then
                Try
                    drpPriorirty.SelectedIndex = drpPriorirty.Items.IndexOf(drpPriorirty.Items.FindByValue(OrderRs("Priority").ToString()))

                    If drpPriorirty.SelectedIndex = 0 Then
                        drpPriorirty.Items.Add(OrderRs("Priority"))
                        drpPriorirty.SelectedValue = OrderRs("Priority")
                    End If
                Catch ex As Exception

                End Try
            End If

            drpShipMethod.SelectedIndex = drpShipMethod.Items.IndexOf(drpShipMethod.Items.FindByValue(OrderRs("ShipMethodID").ToString()))


            drpCardType.SelectedIndex = drpCardType.Items.IndexOf(drpCardType.Items.FindByValue(OrderRs("CreditCardTypeID").ToString()))

            If OrderRs("Service").ToString() <> "" Then
                txtService.Text = Format(OrderRs("Service"), "f")
            End If
            If OrderRs("Delivery").ToString() <> "" Then
                txtDelivery.Text = Format(OrderRs("Delivery"), "f")
            End If
            If OrderRs("Relay").ToString() <> "" Then
                txtRelay.Text = Format(OrderRs("Relay"), "f")
            End If
            If OrderRs("Total").ToString() <> "" Then
                txtTotal.Text = Format(OrderRs("Total"), "f")
            End If
            If OrderRs("SubTotal").ToString() <> "" Then
                txtSubtotal.Text = Format(OrderRs("SubTotal"), "f")
            End If
            'txtComments.Text = OrderRs("CustomerComments").ToString()


            Dim dt_Overrirde As New DataTable

            dt_Overrirde = SelectOverride()

            If dt_Overrirde.Rows.Count > 0 Then
                Try
                    chkDeliveryOverride.Checked = dt_Overrirde.Rows(0)("DeliveryOverride")
                Catch ex As Exception

                End Try


                Try
                    chkServiceOverride.Checked = dt_Overrirde.Rows(0)("ServiceOverride")
                Catch ex As Exception

                End Try


                Try
                    chkRelayOverride.Checked = dt_Overrirde.Rows(0)("RelayOverride")
                Catch ex As Exception

                End Try

                Try
                    checkDiscounts.Checked = dt_Overrirde.Rows(0)("DiscountOverride")
                Catch ex As Exception

                End Try

                Try
                    drpdiscounttype.SelectedValue = dt_Overrirde.Rows(0)("DiscountOverrideType")
                Catch ex As Exception

                End Try

                Try
                    txtdiscounttypevalue.Text = Format(dt_Overrirde.Rows(0)("DiscountOverrideValue"), "f")
                Catch ex As Exception

                End Try

                If checkDiscounts.Checked Then
                    drpdiscounttype.Visible = True
                    txtdiscounttypevalue.Visible = True
                    ' btnapplydiscount.Visible = True
                Else
                    drpdiscounttype.Visible = False
                    txtdiscounttypevalue.Visible = False
                    btnapplydiscount.Visible = False
                End If
                ' CalculationPart()

            Else
                chkDeliveryOverride.Checked = False
                chkServiceOverride.Checked = False
                chkRelayOverride.Checked = False

            End If

            If chkDeliveryOverride.Checked Then
                txtDelivery.Enabled = True
            Else
                txtDelivery.Enabled = False
            End If
            If chkRelayOverride.Checked Then
                txtRelay.Enabled = True
            Else
                txtRelay.Enabled = False
            End If
            If chkServiceOverride.Checked Then
                txtService.Enabled = True
            Else
                txtService.Enabled = False
            End If

            drpTaxes.SelectedIndex = drpTaxes.Items.IndexOf(drpTaxes.Items.FindByValue(OrderRs("TaxGroupID").ToString()))
            txtTaxPercent.Text = OrderRs("TaxPercent").ToString() + "%"
            If OrderRs("TaxAmount").ToString() <> "" Then
                txtTax.Text = Format(OrderRs("TaxAmount"), "f")
            End If

            txtInternalNotes.Text = OrderRs("InternalNotes").ToString()




            Dim dalObjTemplate As New CustomOrder()
            Dim rs As SqlDataReader
            rs = dalObjTemplate.GetHomePageSetupValues(CompanyID, DivisionID, DepartmentID)
            Dim AffiliatesName As String
            Dim AffiliatesCode As String
            While rs.Read()
                If rs("AffiliatesName").ToString() <> "" Then
                    AffiliatesName = rs("AffiliatesName").ToString()
                End If

                If rs("AffiliatesCode").ToString() <> "" Then
                    AffiliatesCode = rs("AffiliatesCode").ToString()
                End If
            End While

            Dim AffiliateID As Integer
            Try
                AffiliateID = OrderRs("AffiliateID")
            Catch ex As Exception
                AffiliateID = 0
            End Try


            If AffiliateID <> 0 Then
                Dim objUser As New clsAffiliate
                Dim rsAffiliateID As SqlDataReader
                Dim AffiliateNamebyid As String
                rsAffiliateID = objUser.PopulateAffiliateByID(AffiliateID)
                While rsAffiliateID.Read()
                    AffiliateNamebyid = rsAffiliateID("AffiliateName").ToString()
                End While

                lblAffliatesname.Text = AffiliatesName
                txtAffliate.Text = AffiliateNamebyid

                lblAffliatescode.Text = AffiliatesCode
                txtAffliatescode.Text = OrderRs("AffiliateCode").ToString()

                'txtInternalNotes.Text = txtInternalNotes.Text & " " & AffiliatesName & " " & AffiliateNamebyid
                'txtInternalNotes.Text = txtInternalNotes.Text & " " & AffiliatesCode & " " & OrderRs("AffiliateCode").ToString()
                traffliate.Visible = True
            Else
                traffliate.Visible = False
            End If

            PopulateChangeHistory(OrdN)


            txtDriverRouteInfo.Text = OrderRs("DriverRouteInfo").ToString()


            drpDestinationType.SelectedIndex = drpDestinationType.Items.IndexOf(drpDestinationType.Items.FindByValue(OrderRs("DestinationType").ToString()))

            If OrderRs("OrderShipDate").ToString() <> "" Then
                txtDeliveryDate.Text = Convert.ToDateTime(OrderRs("OrderShipDate")).ToShortDateString

            End If

            If OrderRs("PostedDate").ToString() <> "" Then
                txtPostedDate.Text = Convert.ToDateTime(OrderRs("PostedDate")).ToShortDateString
                txtPostedTime.Text = Convert.ToDateTime(OrderRs("PostedDate")).ToShortTimeString

            End If
            If OrderRs("PickedDate").ToString() <> "" Then
                txtPickedDate.Text = Convert.ToDateTime(OrderRs("PickedDate")).ToShortDateString
                txtPickedTime.Text = Convert.ToDateTime(OrderRs("PickedDate")).ToShortTimeString


            End If

            If OrderRs("PrintedDate").ToString() <> "" Then
                txtPrintedDate.Text = Convert.ToDateTime(OrderRs("PrintedDate")).ToShortDateString
                txtPrintedTime.Text = Convert.ToDateTime(OrderRs("PrintedDate")).ToShortTimeString

            End If
            If OrderRs("BilledDate").ToString() <> "" Then
                txtBilledDate.Text = Convert.ToDateTime(OrderRs("BilledDate")).ToShortDateString
                txtBilledTime.Text = Convert.ToDateTime(OrderRs("BilledDate")).ToShortTimeString

            End If
            If OrderRs("ShipDate").ToString() <> "" Then
                txtShipDate.Text = Convert.ToDateTime(OrderRs("ShipDate")).ToShortDateString
                txtShipTime.Text = Convert.ToDateTime(OrderRs("ShipDate")).ToShortTimeString

            End If
            If OrderRs("InvoiceDate").ToString() <> "" Then
                txtInvoiceDate.Text = Convert.ToDateTime(OrderRs("InvoiceDate")).ToShortDateString
                txtInvoiceTime.Text = Convert.ToDateTime(OrderRs("InvoiceDate")).ToShortTimeString

            End If

            If Convert.ToBoolean(OrderRs("Posted")) = True Then
                chkPosted.Checked = True
            End If
            If Convert.ToBoolean(OrderRs("Picked")) = True Then
                chkPicked.Checked = True
            End If
            If Convert.ToBoolean(OrderRs("Printed")) = True Then
                chkPrinted.Checked = True
            End If
            If Convert.ToBoolean(OrderRs("Billed")) = True Then
                chkBilled.Checked = True
            End If
            If Convert.ToBoolean(OrderRs("Shipped")) = True Then
                chkShipped.Checked = True
            End If
            If Convert.ToBoolean(OrderRs("Invoiced")) = True Then
                chkInvoiced.Checked = True
            End If

            If (txtPostedDate.Text = "1/1/1900") Or (txtPostedDate.Text = "1/1/1980") Then
                txtPostedDate.Text = ""
                txtPostedTime.Text = ""
            End If
            If (txtPickedDate.Text = "1/1/1900") Or (txtPickedDate.Text = "1/1/1980") Then
                txtPickedDate.Text = ""
                txtPickedTime.Text = ""
            End If
            If (txtPrintedDate.Text = "1/1/1900") Or (txtPrintedDate.Text = "1/1/1980") Then
                txtPrintedDate.Text = ""
                txtPrintedTime.Text = ""
            End If
            If (txtBilledDate.Text = "1/1/1900") Or (txtBilledDate.Text = "1/1/1980") Then
                txtBilledDate.Text = ""
                txtBilledTime.Text = ""
            End If
            If (txtShipDate.Text = "1/1/1900") Or (txtShipDate.Text = "1/1/1980") Then
                txtShipDate.Text = ""
                txtShipTime.Text = ""
            End If
            If (txtInvoiceDate.Text = "1/1/1900") Or (txtInvoiceDate.Text = "1/1/1980") Then
                txtInvoiceDate.Text = ""
                txtInvoiceTime.Text = ""
            End If


            Dim CustID As String = OrderRs("CustomerID").ToString() 'Session("CustomerID").ToString()
            lkpCustomerID.Text = CustID
            Dim PaymentType As String = OrderRs("PaymentMethodID").ToString()


            drpWireoutStatus.SelectedIndex = drpWireoutStatus.Items.IndexOf(drpWireoutStatus.Items.FindByValue(OrderRs("WireOutStatus").ToString()))
            drpWireoutService.SelectedIndex = drpWireoutService.Items.IndexOf(drpWireoutService.Items.FindByValue(OrderRs("WireOutService").ToString()))
            drpWireoutTransMethod.SelectedIndex = drpWireoutTransMethod.Items.IndexOf(drpWireoutTransMethod.Items.FindByValue(OrderRs("WireOutTransMethod").ToString()))
            drpWireoutPriority.SelectedIndex = drpWireoutPriority.Items.IndexOf(drpWireoutPriority.Items.FindByValue(OrderRs("WireOutPriority").ToString()))


            txtWireNotes.Text = OrderRs("WireNotes").ToString()
            txtWireoutOwner.Text = OrderRs("WireServiceOwner").ToString()
            txtWireoutTransID.Text = OrderRs("WireOutTransID").ToString()
            '***start*** code addd by imtiyaz
            transid = OrderRs("WireOutTransID").ToString()
            '***start*** code addd by imtiyaz
            txtWireoutCode.Text = OrderRs("WireOutCode").ToString()


            Dim VendorID As String = OrderRs("VendorID").ToString()
            If VendorID <> "" Then
                PopulateVendorDetails(VendorID)

            End If
            PaymentPanelChange(PaymentType)
            If drpPaymentType.SelectedValue <> "Wire In" Then
                lblCustomer.Text = "Bill to Customer ID"
                'PopupvendorControl.Visible = False
                lkpCustomerID.Visible = True
                lblNewsletter.Visible = True
                drpNewsletterID.Visible = True
                lblPO.Visible = True
                txtPO.Visible = True
                lblSource.Visible = True
                drpSource.Visible = True
                pnlCustomer.Visible = True
                pnlVendor.Visible = False
                lblSalutation.Text = "Salutation"

                lblCustomerComments.Text = "Customer Comments"
                chkBillingAddress.Visible = True
                lnkShipAddress.Visible = True
            Else
                lblCustomerComments.Text = "Florist Comments"
                drpCustomerID.Visible = False
                txtSpecifyCustomer.Visible = False
                lblCustomer.Text = "Bill to Florist ID"
                lkpCustomerID.Visible = False
                ''PopupvendorControl.Visible = True
                lblNewsletter.Visible = False
                drpNewsletterID.Visible = False
                lblPO.Visible = False
                txtPO.Visible = False
                lblSource.Visible = False
                drpSource.Visible = False
                pnlCustomer.Visible = False
                pnlVendor.Visible = True
                lblSalutation.Text = "Florist Name"

                chkBillingAddress.Visible = False
                lnkShipAddress.Visible = False

            End If




            ''new code for stored CC details
            Dim storedcc As Boolean = False
            Try
                'StoredCreditCard
                storedcc = OrderRs("StoredCreditCard")
            Catch ex As Exception
            End Try
            ''new code for stored CC details


            If storedcc Then
                FillCUstomerdetailsFromOrderBillingDetails(OrdN)
            Else
                PopulateCustomerInfo(CustID)
            End If

            If (Me.CompanyID = "FieldOfFlowers" Or Me.CompanyID = "FieldOfFlowersTraining") Then
                LoadLocationandZoneForZipDetails(txtShippingZip.Text)
            End If

            WireOutPanelDisplay()

        End While
        OrderRs.Close()



        '***start*** code addd by imtiyaz
        If Posted = "True" And 1 = 0 Then

            'it check for the case of OrderTransmission
            If drpShipMethod.SelectedValue = "Wire_Out" Then
                If drpWireoutService.SelectedValue = "FSI" Then
                    If drpWireoutTransMethod.SelectedValue = "WebService" Then
                        If transid = "" Then

                            Me.btnTransmit.Visible = True
                            Me.btnTransmit.Enabled = True
                        Else
                            Me.btnTransmit.Visible = True
                            Me.btnTransmit.Enabled = False
                        End If

                    End If
                ElseIf drpWireoutService.SelectedValue = "FTD" Then
                    If drpWireoutTransMethod.SelectedValue = "Email Out" Then
                        If transid = "" Then
                            Me.btnTransmit.Visible = True
                            Me.btnTransmit.Enabled = True
                        Else
                            Me.btnTransmit.Visible = True
                            Me.btnTransmit.Enabled = False
                        End If

                    End If
                ElseIf drpWireoutService.SelectedValue = "FSN" Then
                    If drpWireoutTransMethod.SelectedValue = "WebService" Then
                        If transid = "" Then
                            Me.btnTransmit.Visible = True
                            Me.btnTransmit.Enabled = True
                        Else
                            Me.btnTransmit.Visible = True
                            Me.btnTransmit.Enabled = False
                        End If

                    End If
                End If
            End If

        End If
        '***start*** code addd by imtiyaz

    End Sub
#End Region



    Public Function GetOrderApprovalNumber(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrderNumber As String) As String


        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("SELECT * from [OrderHeaderCreditCardProcessingDetails] Where Companyid=@CompanyID AND  DivisionID=@DivisionID  AND DepartmentID=@DepartmentID AND OrderNumber=@OrderNumber", ConString)
        myCommand.CommandType = Data.CommandType.Text

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar, 36)
        parameterOrderNumber.Value = OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataTable

        adapter.Fill(ds)
        ConString.Close()

        If ds.Rows.Count <> 0 Then

            Dim PaymentGateway As String = ""

            PaymentGateway = ds.Rows(0)("PaymentGateway")

            If PaymentGateway = "PayPal" Then

                Dim approval As String = ""
                Try
                    approval = ds.Rows(0)("PayPalPNREF")
                Catch ex As Exception

                End Try

                Return approval
            Else
                Dim approval As String = ""
                Try
                    approval = GetOrderPPIApprovalNumber(CompanyID, DepartmentID, DivisionID, OrderNumber)
                Catch ex As Exception

                End Try
                Return approval

            End If


        Else
            Return ""
        End If

        Return ""

        'Dim rs As SqlDataReader
        'rs = myCommand.ExecuteReader()
        'Return rs

    End Function



    Public Function GetOrderPPIApprovalNumber(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrderNumber As String) As String


        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("SELECT * from [PaymentGatewayTransactionLogs] Where Companyid=@CompanyID AND  DivisionID=@DivisionID  AND DepartmentID=@DepartmentID AND [ReferenceNumber]=@OrderNumber AND [RefrenaceType]='Order'  order by [ProcessDate] Desc ", ConString)
        myCommand.CommandType = Data.CommandType.Text

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar, 36)
        parameterOrderNumber.Value = OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataTable

        adapter.Fill(ds)
        ConString.Close()

        If ds.Rows.Count <> 0 Then
            Dim approval As String = ""
            Try
                approval = ds.Rows(0)("PPIApprovalNumber")
            Catch ex As Exception

            End Try

            Return approval
        Else
            Return ""
        End If


        Return ""



    End Function





    Public Sub FillCUstomerdetailsFromOrderBillingDetails(ByVal ordnum As String)



        Dim objCustomerCreditCards As New clsCustomerCreditCards
        objCustomerCreditCards.CompanyID = CompanyID
        objCustomerCreditCards.DivisionID = DivisionID
        objCustomerCreditCards.DepartmentID = DepartmentID
        objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
        objCustomerCreditCards.OrderNumber = ordnum
        ''New lines
        Dim dtCustomerCreditCardsFromOrder As New DataTable
        dtCustomerCreditCardsFromOrder = objCustomerCreditCards.CheckMultiCreditCardBillingDetails
        If dtCustomerCreditCardsFromOrder.Rows.Count <> 0 Then
            Dim FLName As String = ""
            Dim FLNameArr() As String
            FLName = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerName").ToString

            If FLName.IndexOf("-") <> -1 Then
                FLNameArr = FLName.Split("-")
            Else
                FLNameArr = FLName.Split(" ")
            End If

            If FLNameArr.Length = 2 Then
                txtCustomerFirstName.Text = FLNameArr(0)
                txtCustomerLastName.Text = FLNameArr(1)
            End If
            If FLNameArr.Length = 3 Then
                txtCustomerFirstName.Text = FLNameArr(0) & " " & FLNameArr(1)
                txtCustomerLastName.Text = FLNameArr(2)
            End If
            If FLNameArr.Length = 1 Then
                txtCustomerFirstName.Text = FLNameArr(0)
                txtCustomerLastName.Text = ""
            End If
            txtCustomerAddress1.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerAddress1")
            txtCustomerAddress2.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerAddress2")
            txtCustomerAddress3.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerAddress3")

            txtCustomerCity.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerCity")

            Try
                drpCountry.SelectedIndex = drpCountry.Items.IndexOf(drpCountry.Items.FindByValue(dtCustomerCreditCardsFromOrder.Rows(0)("CustomerCountry").ToString()))

            Catch ex As Exception

            End Try

            If dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerCountry").ToString() = "US" Then
                drpState.Visible = True
                txtCustomerState.Visible = False
                drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerState").ToString()))
            ElseIf dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerCountry").ToString() = "CD" Then
                drpState.Visible = True
                txtCustomerState.Visible = False
                drpState.SelectedIndex = drpState.Items.IndexOf(drpState.Items.FindByValue(dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerState").ToString()))
            Else
                drpState.Visible = False
                txtCustomerState.Visible = True
                txtCustomerState.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerState").ToString()
            End If
            txtCustomerZip.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerZip").ToString()

            Try
                txtCustomerFax.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerFax").ToString()
                txtCustomerPhone.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerPhone").ToString()
                txtCustomerCell.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerCell").ToString()
                txtCustomerEmail.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerEmail").ToString()
                txtExt.Text = dtCustomerCreditCardsFromOrder.Rows(0)("BillingCustomerPhoneExt").ToString()
            Catch ex As Exception

            End Try


            PopulateCustomerCompanyAtt(txtCustomerTemp.Text.Trim)
            If chkBillingAddress.Checked = True Then
                txtShippingName.Text = txtCustomerFirstName.Text
                txtShippingLastName.Text = txtCustomerLastName.Text
                txtShippingAddress1.Text = txtCustomerAddress1.Text
                txtShippingAddress2.Text = txtCustomerAddress2.Text
                txtShippingAddress3.Text = txtCustomerAddress3.Text
                txtShippingCity.Text = txtCustomerCity.Text
                txtShippingZip.Text = txtCustomerZip.Text
            End If

        End If

    End Sub

    Public Sub PopulateCustomerCompanyAtt(ByVal CustID As String)
        Dim filters As EnterpriseCommon.Core.FilterSet
        filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        CompanyID = filters!CompanyID
        DivisionID = filters!DivisionID
        DepartmentID = filters!DepartmentID
        Dim PopOrderType As New CustomOrder()
        Dim rs As SqlDataReader
        rs = PopOrderType.PopulateCustomerDetails(CompanyID, DepartmentID, DivisionID, CustID)
        While rs.Read()
            txtCompany.Text = rs("CustomerCompany").ToString()
            txtAttention.Text = rs("Attention").ToString()
        End While
        rs.Close()
    End Sub

    Public Sub BindStoredCreditCard(ByVal OrdN As String, ByVal custid As String, ByVal CustomerCardDetailsForlinenumber As String, ByVal updatebillingCC As Boolean)


        'new code for select stored card 

        Try
            If drpstoredcc.Items.Count = 1 Then
                If drpPaymentType.SelectedValue.ToLower = "Credit Card".ToLower Then

                    Dim objCustomerCreditCards As New clsCustomerCreditCards
                    objCustomerCreditCards.CompanyID = CompanyID
                    objCustomerCreditCards.DivisionID = DivisionID
                    objCustomerCreditCards.DepartmentID = DepartmentID
                    If txtCustomerTemp.Text.Trim <> "" Then
                        objCustomerCreditCards.CustomerID = txtCustomerTemp.Text.Trim
                        Dim dtCustomerCreditCards As New DataTable
                        dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetails

                        drpstoredcc.Items.Clear()
                        drpstoredcc.Items.Add("Other")

                        If dtCustomerCreditCards.Rows.Count <> 0 Then
                            Dim n As Integer
                            For n = 0 To dtCustomerCreditCards.Rows.Count - 1
                                Dim lst As New ListItem
                                Dim crd As String
                                crd = dtCustomerCreditCards.Rows(n)("CreditCardNumber")
                                Try
                                    If crd <> "" And custid.Trim <> "" Then
                                        crd = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(crd, custid)
                                    End If
                                Catch ex As Exception

                                End Try

                                'Last number display X in Credit cards
                                Dim cardNo As String = ""
                                Dim cLen As Integer = 0
                                Dim subLen As Integer = 0
                                Dim SubcardNo As String = ""
                                cardNo = crd
                                cLen = cardNo.Length()
                                Dim slen As Integer = 0
                                If cLen > 0 Then
                                    If cLen > 12 Then
                                        subLen = cLen - 12
                                        'SubcardNo = cardNo.Substring(0, subLen)
                                        SubcardNo = cardNo.Substring(12, subLen)
                                        slen = SubcardNo.Length()

                                        If slen > 4 Then
                                            SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                                        End If
                                        'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                                        cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                                    Else
                                        cardNo = RepeatChar("X", cLen)
                                    End If
                                End If


                                lst.Text = cardNo
                                lst.Value = dtCustomerCreditCards.Rows(n)("CardDetailLineNumber")
                                drpstoredcc.Items.Add(lst)



                            Next
                        End If

                        'drpstoredcc.DataSource = dtCustomerCreditCards
                        'drpstoredcc.DataTextField = "CreditCardNumber"
                        'drpstoredcc.DataValueField = "CardDetailLineNumber"
                        'drpstoredcc.DataBind()

                    End If



                End If
            End If
            drpstoredcc.SelectedIndex = drpstoredcc.Items.IndexOf(drpstoredcc.Items.FindByValue(CustomerCardDetailsForlinenumber))

            Try
                chkupdatebilling.Checked = updatebillingCC
            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try



        ''
        If drpstoredcc.SelectedValue <> "Other" Then

            ''
            'drpstoredcc.Enabled = False
            ''

            trswipecard.Visible = False
            trchkupdate.Visible = True

            pnlccdetails.Visible = False
            pnlCClable.Visible = True
            lblcard.Text = ""
            lblEXP.Text = ""
            lblcsv.Text = ""
            Dim objCustomerCreditCards As New clsCustomerCreditCards
            objCustomerCreditCards.CompanyID = CompanyID
            objCustomerCreditCards.DivisionID = DivisionID
            objCustomerCreditCards.DepartmentID = DepartmentID
            If txtCustomerTemp.Text.Trim <> "" Then
                objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
                Dim dtCustomerCreditCards As New DataTable
                dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetailsForlinenumber

                'drpstoredcc.Items.Clear()
                'drpstoredcc.Items.Add("Other")

                If dtCustomerCreditCards.Rows.Count <> 0 Then
                    Dim crd As String
                    crd = dtCustomerCreditCards.Rows(0)("CreditCardNumber")

                    Try
                        If crd <> "" And txtCustomerTemp.Text.Trim <> "" Then
                            crd = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(crd, custid)
                        End If
                    Catch ex As Exception

                    End Try


                    txtCard.Text = crd

                    'Last number display X in Credit cards
                    Dim cardNo As String = ""
                    Dim cLen As Integer = 0
                    Dim subLen As Integer = 0
                    Dim SubcardNo As String = ""
                    cardNo = crd
                    cLen = cardNo.Length()
                    Dim slen As Integer = 0
                    If cLen > 0 Then
                        If cLen > 12 Then
                            subLen = cLen - 12
                            'SubcardNo = cardNo.Substring(0, subLen)
                            SubcardNo = cardNo.Substring(12, subLen)
                            slen = SubcardNo.Length()

                            If slen > 4 Then
                                SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                            End If
                            'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                            cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                        Else
                            cardNo = RepeatChar("X", cLen)
                        End If
                    End If


                    lblcard.Text = cardNo

                    Try
                        lblEXP.Text = dtCustomerCreditCards.Rows(0)("CreditCardExpDate")
                        Dim ar() As String
                        ar = lblEXP.Text.Split("/")
                        If ar.Length = 2 Then
                            If ar(1).Length = 2 Then
                                lblEXP.Text = ar(0) & "/" & "20" & ar(1)
                            End If
                        End If
                        'lblEXP.Text = "XX/XXXX"
                    Catch ex As Exception

                    End Try

                    Try
                        lblcsv.Text = "" 'dtCustomerCreditCards.Rows(0)("CreditCardCSVNumber")
                        txtCSV.Text = "" 'lblcsv.Text

                        lblcsv.Text = RepeatChar("X", lblcsv.Text.Length)
                    Catch ex As Exception
                        lblcsv.Text = ""
                    End Try


                    Try
                        lblcctype.Text = dtCustomerCreditCards.Rows(0)("CreditCardTypeID")
                    Catch ex As Exception
                        lblcctype.Text = ""
                    End Try

                    ''New codeded for the Expiration date selection
                    Dim exdat As String = ""
                    Dim exdate As Date

                    Try
                        Dim ar() As String
                        ar = lblEXP.Text.Split("/")
                        If ar.Length = 2 Then
                            exdat = ar(0) & "/01/" & ar(1)
                        End If

                        If ar.Length = 3 Then
                            exdat = lblEXP.Text
                        End If

                        exdate = exdat
                        exdat = exdate.ToString("MM/yyyy")
                        lblEXP.ForeColor = Drawing.Color.Black
                        'BtnBookOrder.Enabled = True
                    Catch ex As Exception
                        lblEXP.Text = lblEXP.Text & "<br>Error: Allowed MM/YY"
                        lblEXP.ForeColor = Drawing.Color.Red
                        'BtnBookOrder.Enabled = False

                    End Try

                    'lblEXP.Text = exdate.ToString

                    drpExpirationDate.SelectedIndex = drpExpirationDate.Items.IndexOf(drpExpirationDate.Items.FindByValue(exdat))

                    Try
                        'drpCardType.SelectedIndex = 1
                        drpCardType.SelectedIndex = drpCardType.Items.IndexOf(drpCardType.Items.FindByValue(dtCustomerCreditCards.Rows(0)("CreditCardTypeID")))
                    Catch ex As Exception

                    End Try
                    'lblcsv.Text = ""
                    'lblEXP.Text = ""

                End If
            End If
            'pnlccdetails.Visible = True
        Else
            trswipecard.Visible = True
            trchkupdate.Visible = False

            txtCSV.Text = ""
            drpExpirationDate.SelectedIndex = -1
            txtCard.Text = ""
            drpstoredcc.SelectedIndex = -1

            pnlccdetails.Visible = True
            pnlCClable.Visible = False
        End If

        ''new card for select stored card

    End Sub



#Region "PopulateVendorDetails"
    Public Sub PopulateVendorDetails(ByVal VendorID As String)
        Dim objUser As New DAL.CustomOrder()
        Dim rs As SqlDataReader

        rs = objUser.PopulateVendorDetailsOrderForm(CompanyID, DivisionID, DepartmentID, VendorID)
        While rs.Read()
            txtWireoutFlorist.Text = rs("VendorName").ToString()

            txtAddress.Text = rs("VendorAddress1").ToString

            txtWireoutCity.Text = rs("VendorCity").ToString()

            txtWireoutEmail.Text = rs("VendorEmail").ToString()

            txtWireoutFax.Text = rs("VendorFax").ToString()

            txtWireoutPhone.Text = rs("VendorPhone").ToString()

            txtWireoutZip.Text = rs("VendorZip").ToString()

            txtWireoutWebsite.Text = rs("VendorWebPage").ToString()

            drpWireOutCountry.SelectedIndex = drpWireOutCountry.Items.IndexOf(drpWireOutCountry.Items.FindByValue(rs("VendorCountry").ToString()))
            drpWireOutState.SelectedIndex = drpWireOutState.Items.IndexOf(drpWireOutState.Items.FindByValue(rs("VendorState").ToString()))


        End While
    End Sub

#End Region

    Sub PopulateChangeHistory(ByVal OrderNum As String)

        Dim CompanyID As String = ""
        Dim DivisionID As String = ""
        Dim DepartmentID As String = ""

        Dim OrderNumber As String = OrderNum

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'Dim objUser As New DAL.CustomOrder()
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim ds As New DataTable()
        ds = PopulateDeliverytrace(CompanyID, DepartmentID, DivisionID, OrderNumber)
        Dim line As String = ""
        If ds.Rows.Count > 0 Then

            Dim n As Integer

            n = 0

            Try

                Dim type As String

                type = ds.Rows(n)("Type")

                If type = "Shipped" Then
                    line = "Shipped on ( " & ds.Rows(n)("SysDate").ToString & ")  with DriverID ( " & ds.Rows(n)("DriverID").ToString & ") having Trip Sheet ( " & ds.Rows(n)("TripID").ToString & ") on Vehicle- ( " & ds.Rows(n)("VehicleID").ToString & ")."
                End If

                If type = "Re-Shipped" Then
                    line = "Re-Shipped on ( " & ds.Rows(n)("SysDate").ToString & ")  with DriverID( " & ds.Rows(n)("DriverID").ToString & ") having Trip Sheet ( " & ds.Rows(n)("TripID").ToString & ") on Vehicle- ( " & ds.Rows(n)("VehicleID").ToString & ")."
                End If


                If type = "Delivered" Then
                    line = "Delivered on ( " & ds.Rows(n)("DriverDateTime").ToString & ")  by ( " & ds.Rows(n)("DriverID").ToString & ") with his comments  ( " & ds.Rows(n)("Confirmationcode").ToString & ") - ( " & ds.Rows(n)("Confirmationcodetext").ToString & ") - ( " & ds.Rows(n)("Drivercomments").ToString & ")."
                End If

                If type = "Returned" Then
                    line = "Returned on ( " & ds.Rows(n)("DriverDateTime").ToString & ")  by ( " & ds.Rows(n)("DriverID").ToString & ") with his comments  ( " & ds.Rows(n)("Confirmationcode").ToString & ") - ( " & ds.Rows(n)("Confirmationcodetext").ToString & ") - ( " & ds.Rows(n)("Drivercomments").ToString & ")."
                End If

            Catch ex As Exception

            End Try

            txtInternalNotes.Text = txtInternalNotes.Text & " " & line

        Else
            'lblErr.Text = "No Order shipping process detail Present"
        End If

    End Sub

    Public Function PopulateDeliverytrace(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrderNumber As String) As DataTable


        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        ConString.Open()

        Dim myCommand As New SqlCommand("SELECT * from [OrderHeaderDeliverTrace] Where Companyid=@CompanyID AND  DivisionID=@DivisionID  AND DepartmentID=@DepartmentID AND OrderNumber=@OrderNumber  order by SysDate Desc ", ConString)
        myCommand.CommandType = Data.CommandType.Text

        Dim parameterCompanyID As New SqlParameter("@CompanyID", Data.SqlDbType.NVarChar, 36)
        parameterCompanyID.Value = CompanyID
        myCommand.Parameters.Add(parameterCompanyID)

        Dim parameterDepartmentID As New SqlParameter("@DepartmentID", Data.SqlDbType.NVarChar, 36)
        parameterDepartmentID.Value = DepartmentID
        myCommand.Parameters.Add(parameterDepartmentID)

        Dim parameterDivisionID As New SqlParameter("@DivisionID", Data.SqlDbType.NVarChar, 36)
        parameterDivisionID.Value = DivisionID
        myCommand.Parameters.Add(parameterDivisionID)


        Dim parameterOrderNumber As New SqlParameter("@OrderNumber", Data.SqlDbType.NVarChar, 36)
        parameterOrderNumber.Value = OrderNumber
        myCommand.Parameters.Add(parameterOrderNumber)


        Dim adapter As New SqlDataAdapter(myCommand)
        Dim ds As New DataTable

        adapter.Fill(ds)
        ConString.Close()

        Return ds

        'Dim rs As SqlDataReader
        'rs = myCommand.ExecuteReader()
        'Return rs

    End Function




#Region "BtnBookOrder_Click"
    Protected Sub BtnBookOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBookOrder.Click
        'Edited by Jacob on 24-01-2008

        ShippingDeliveryCharge(txtShippingZip.Text)
        PopulatingTaxPercent()

        If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "Demo Site-90210" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
            If IsNumeric(lblOrderNumberData.Text.Trim()) Then
                InsertCaTaxDetail(lblOrderNumberData.Text, txtTaxPercentGST.Text.Replace("%", ""), txtTaxGST.Text, txtTaxPercentPST.Text.Replace("%", ""), txtTaxPST.Text)
            End If
        End If

		        If drpPaymentType.SelectedValue.ToLower = "Credit Card".ToLower Then
            If drpstoredcc.SelectedValue <> "Other" Then
                txtCSV.Text = lblcsv.Text
            End If
        End If
		
        Dim HeaderDetailSubmitCustomerID As String = ""

        If drpCustomerID.SelectedValue = "Retail Customer" And drpCustomerID.Visible = True Then
            HeaderDetailSubmitCustomerID = "Retail Customer"
        ElseIf drpCustomerID.SelectedValue = "New Customer" And drpCustomerID.Visible = True Then
            If txtCustomerTemp.Text.Trim = "Retail Customer" Then
                HeaderDetailSubmitCustomerID = ""
                txtCustomerTemp.Text = ""
            End If
        Else
            HeaderDetailSubmitCustomerID = txtCustomerTemp.Text
        End If

        If HeaderDetailSubmitCustomerID = "" Then

            If txtCustomerFirstName.Text.Trim() <> "" Or txtCustomerLastName.Text.Trim() <> "" Or txtCustomerAddress1.Text.Trim() <> "" Then
                If txtCustomerTemp.Text.Trim = "" And Session("CustomerIDNew") = "" Then
                    Dim CustomerIDNew As String = CreateNewCustomerID()

                    AddCustomerInformation(CustomerIDNew)
                    drpCustomerID.Visible = False
                    txtCustomerTemp.Visible = True
                    txtCustomerTemp.Text = CustomerIDNew
                    Session("CustomerIDNew") = CustomerIDNew
                    ' EmailSendingWithoutBcc(CompanyID & "- CustomerIDNew " & CustomerIDNew & " -POS line number 7106  order no - " & lblOrderNumberData.Text, "Existing customerid - " & txtCustomerTemp.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
                End If

            Else
                lblCustomerStatus.Text = "Customer Id not present please reselect it (Use Back to Option Link or Search link) ."
                lblCustomerStatus.Visible = True
                Exit Sub
            End If

        Else
            lblCustomerStatus.Text = ""
        End If

        If txtDiscountCode.Text.Trim() <> "" Then
            If txtDiscountCode.Enabled = True Then
                lblcodeerror.Text = ""
                txtDiscountCode_TextChanged(sender, e)
                If lblcodeerror.Text.Trim <> "" Then
                    lblCCMessage.Text = lblcodeerror.Text
                    lblCCMessage.Visible = True
                    Exit Sub
                Else
                    lblCCMessage.Text = ""
                End If
            End If
        End If

        UpdateOrderOverride()

        If Page.IsValid Then

             
            CompanyID = Session("CompanyID")
            DivisionID = Session("DivisionID")
            DepartmentID = Session("DepartmentID")
            EmployeeID = Session("EmployeeID")


            Dim OrdNumber As String
            OrdNumber = lblOrderNumberData.Text


            ''Update Order Location Method''
            Dim objrderloaction As New clsOrder_Location
            objrderloaction.CompanyID = CompanyID
            objrderloaction.DivisionID = DivisionID
            objrderloaction.DepartmentID = DepartmentID
            objrderloaction.OrderNumber = OrdNumber
            objrderloaction.LocationID = cmblocationid.SelectedValue
            objrderloaction.UpdateOrderLocationID()

            '''''''''this for check from where order entry form posted'''''''''''''''''''''''''''''
            objrderloaction.UpdateOrderPostedFrom(True)
            Session("thankspage") = "New"
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            ''''''''''''''''''''''''''''''''''''''
            If drpTransaction.SelectedValue = "Wire In" Or drpTransaction.SelectedValue = "Wire_In" Or drpPaymentType.SelectedValue = "Wire_In" Or drpPaymentType.SelectedValue = "Wire In" Then

                Dim objwirein As New clsOrderEntryFormWireIn
                objwirein.CompanyID = CompanyID
                objwirein.DivisionID = DivisionID
                objwirein.DepartmentID = DepartmentID
                objwirein.OrderNumber = OrdNumber

                objwirein.CustomerID = txtCustomerTemp.Text
                objwirein.TransactionTypeID = "Wire In"
                objwirein.OrderTypeID = drpOrderTypeIDData.SelectedValue
                objwirein.WireService = ""

                objwirein.FloristName = txtFloristname.Text
                objwirein.FloristPhone = txtFloristPhone.Text
                objwirein.City = txtFloristcity.Text
                objwirein.State = txtFloriststate.Text
                objwirein.Receivedamount = txtReceivedamount.Text
                objwirein.Representativetalkedto = txtrepresentative.Text

                 
                objwirein.InsertWireDetailsOrder()
                 
                objwirein.UpdateOrderWireIn()



            End If


            If drpTransaction.SelectedValue.ToLower = "Wire Out".ToLower Or drpTransaction.SelectedValue.ToLower = "Wire_Out".ToLower Or drpShipMethod.SelectedValue.ToLower = "Wire Out".ToLower Or drpShipMethod.SelectedValue.ToLower = "Wire_Out".ToLower Then
                 

                ''Updates new table of wire out section 
                Dim objwirein As New clsOrderEntryFormWireIn
                objwirein.CompanyID = CompanyID
                objwirein.DivisionID = DivisionID
                objwirein.DepartmentID = DepartmentID
                objwirein.OrderNumber = OrdNumber

                objwirein.CustomerID = txtCustomerTemp.Text
                objwirein.TransactionTypeID = "Wire_Out"
                objwirein.OrderTypeID = drpOrderTypeIDData.SelectedValue
                objwirein.WireService = drpWireoutService.SelectedValue

                objwirein.FloristName = txtWireoutFlorist.Text.Trim()
                objwirein.FloristPhone = txtWireoutPhone.Text.Trim()
                objwirein.City = txtWireoutZip.Text.Trim()
                objwirein.State = drpWireOutState.SelectedValue.Trim()
                objwirein.Receivedamount = 0
                objwirein.Representativetalkedto = ""

                objwirein.InsertWireDetailsOrder()

                objwirein.UpdateOrderWireIn()

                ''''''''''''''''''''''''''''''
            End If

            ''Local_Truck 
            If (drpShipMethod.SelectedValue = "Local_Truck" Or drpShipMethod.SelectedValue = "Local Truck") And (drpTransaction.SelectedValue <> "Wire In" And drpTransaction.SelectedValue <> "Wire_In" And drpPaymentType.SelectedValue <> "Wire_In" And drpPaymentType.SelectedValue <> "Wire In") Then

                ''Updates new table of wire out section 
                Dim objwirein As New clsOrderEntryFormWireIn
                objwirein.CompanyID = CompanyID
                objwirein.DivisionID = DivisionID
                objwirein.DepartmentID = DepartmentID
                objwirein.OrderNumber = OrdNumber

                objwirein.DeleteOrderWires()

                ''''''''''''''''''''''''''''''
            End If
            ''

            Dim FillItemDetailGrid As New CustomOrder()
            Dim ds As New Data.DataSet
            If drpPaymentType.SelectedValue = "Wire In" Then




                Dim rsamount As Double
                Dim subtotalamount As Double
                Dim deltotalamount As Double


                Try
                    rsamount = CType(txtReceivedamount.Text, Double)
                Catch ex As Exception
                    rsamount = 0
                End Try
                Try
                    subtotalamount = CType(txtSubtotal.Text, Double)
                Catch ex As Exception
                    subtotalamount = 0
                End Try
                Try
                    deltotalamount = CType(txtDelivery.Text, Double)
                Catch ex As Exception
                    deltotalamount = 0
                End Try

                Dim r As New Random

                Dim totalamount As Double = deltotalamount + subtotalamount

                rsamount = System.Math.Round(rsamount, 2)
                totalamount = System.Math.Round(totalamount, 2)

                If (rsamount - totalamount) <> 0 Then
                    Dim nt As Double = (rsamount - totalamount)

                    ClientScript.RegisterStartupScript(Me.GetType(), "LoginError" & r.Next(100001, 200001).ToString(), _
                        String.Format("alert('{0}');", "Received Amount must be equal to Sub total plus delivery amount. "), True)
                    Exit Sub
                End If

                If txtCustomerTemp.Text = "" Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "LoginError", _
                        String.Format("alert('{0}');", "Please Select an existing Florist ID in Bill to Florist Section"), True)
                    Exit Sub
                Else
                    'AddvendorInfoasCustomerInformation(txtCustomerTemp.Text)
                End If


            ElseIf drpCustomerID.SelectedValue = "New Customer" And drpCustomerID.Visible = "True" Then
                If txtCustomerTemp.Text.Trim = "" And Session("CustomerIDNew") = "" Then
                    Dim CustomerIDNew As String = CreateNewCustomerID()

                    AddCustomerInformation(CustomerIDNew)
                    drpCustomerID.Visible = False
                    txtCustomerTemp.Visible = True
                    lkpCustomerID.Visible = True
                    txtCustomerTemp.Text = CustomerIDNew
                    lkpCustomerID.Text = CustomerIDNew
                    Session("CustomerIDNew") = CustomerIDNew
                    'EmailSendingWithoutBcc(CompanyID & "- CustomerIDNew " & CustomerIDNew & " -POS line number 7287  order no - " & lblOrderNumberData.Text, "Existing customerid - " & txtCustomerTemp.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
                End If

            ElseIf drpCustomerID.SelectedValue = "Retail Customer" And drpCustomerID.Visible = "True" Then
                Dim obj As New CustomerImport
                obj.CompanyID = CompanyID
                obj.DivisionID = DivisionID
                obj.DepartmentID = DepartmentID
                obj.CustomerPhone = ""
                obj.Password = ""
                obj.Login = "Retail Customer"
                obj.CustomerFirstName = "Retail Customer"
                obj.CustomerLastName = "Retail Customer"
                obj.Attention = ""
                obj.CustomerAddress1 = ""
                obj.CustomerAddress3 = ""
                obj.CustomerAddress2 = ""
                obj.CustomerCity = ""
                obj.CustomerState = ""
                obj.CustomerCountry = ""
                obj.CustomerFax = ""
                obj.CustomerEmail = ""
                obj.CreditLimit = "0"
                obj.AccountStatus = ""
                obj.CustomerSince = Date.Now
                obj.CreditComments = ""
                obj.CustomerSalutation = ""
                obj.CustomerZip = ""
                obj.CustomerCell = ""
                obj.CustomerPhoneExt = ""
                obj.CustomerCompany = ""
                obj.Newsletter = ""
                obj.CustomerID = "Retail Customer"

                If obj.GetCustomerID() = 0 Then
                    obj.InsertCustomerInformationDetail()
                End If
                txtCustomerTemp.Text = "Retail Customer"
                UpdateCustomerDetails()
            Else
                UpdateCustomerDetails()
            End If

            Dim CreditCustomerID As String

            If drpPaymentType.SelectedValue = "House Account" Then
                If txtCreditLimit.Text.Trim() <> "" Or txtCreditLimit.Text.Trim() <> "0.0" Then


                    If drpCustomerID.SelectedValue = "Specify Customer" Then
                        CreditCustomerID = txtSpecifyCustomer.Text
                    ElseIf lkpCustomerID.Value <> "" Then
                        CreditCustomerID = lkpCustomerID.Value
                    Else
                        CreditCustomerID = txtCustomerTemp.Text
                    End If

                    Dim objCustomerCredit As New CustomerCredit()
                    Dim CustomerStatus As Integer

                    CustomerStatus = objCustomerCredit.CheckCustomerCredit(CompanyID, DivisionID, DepartmentID, OrdNumber, txtTotal.Text, CreditCustomerID)
                    If CustomerStatus = 0 Then
                        'lblCustomerStatus.Text = "This customer does not have a established credit line and the order will be put on hold. Please enter the amount on credit limit Text box"
                        'Exit Sub
                    Else
                        lblCustomerStatus.Text = ""
                    End If

                Else

                    'lblCustomerStatus.Text = "This customer does not have a established credit line and the order will be put on hold. Please enter the amount on credit limit Text box"
                    'Exit Sub
                End If
            End If
            'UpdateCustomerDetails()
            AddEditItemDetails()
            AddCardMessages(OrdNumber)


            If drpPaymentType.SelectedValue.ToLower = "Credit Card".ToLower Then
                updatemulticard(OrdNumber)
            End If

            If drpShipMethod.SelectedValue = "Wire_Out" Or drpShipMethod.SelectedValue = "Wire Out" Then


                'vendorID = txtVendorID.Value
                '***start Code Added Imtiyaz Ahamad
                If Request.QueryString("OrderNumber") = Nothing Then
                    'Code Added by jacob on 31-01-2008
                    '***start Code
                    If txtVendorID.Value <> "" Then
                        vendorID = txtVendorID.Value

                    Else
                        vendorID = txtWireoutFlorist.Text
                    End If

                    '***End Code
                Else

                    If txtWireoutFlorist.Text = "" Then
                        vendorID = txtVendorID.Value
                    Else
                        vendorID = txtWireoutFlorist.Text
                    End If
                End If
                '***start Code Added Imtiyaz Ahamad


                WireService = drpWireoutService.SelectedValue

                WireOutCode = txtWireoutCode.Text.Trim()
                WireOutOwner = txtWireoutOwner.Text.Trim()
                WireOutStatus = drpWireoutStatus.SelectedValue
                WireOutNotes = txtWireNotes.Text.Trim()
                WireOutTransID = txtWireoutTransID.Text.Trim()
                WireOutTransMethod = drpWireoutTransMethod.SelectedValue
                WireOutPriority = drpWireoutPriority.SelectedValue

                Dim objUser As New DAL.CustomOrder()
                objUser.UpdateWireOutInformation(CompanyID, DivisionID, DepartmentID, OrdNumber, vendorID, WireService, WireOutCode, WireOutOwner, WireOutStatus, WireOutNotes, WireOutTransID, WireOutTransMethod, WireOutPriority)

                VendorName = txtWireoutFlorist.Text.Trim()
                VendorAddress = txtAddress.Text.Trim()
                VendorCity = txtWireoutCity.Text.Trim()
                VendorCountry = drpWireOutCountry.SelectedValue.Trim()
                VendorState = drpWireOutState.SelectedValue.Trim()
                VendorZip = txtWireoutZip.Text.Trim()
                VendorPhone = txtWireoutPhone.Text.Trim()
                VendorEmail = txtWireoutEmail.Text.Trim()
                VendorWebPage = txtWireoutWebsite.Text.Trim()
                VendorFax = txtWireoutFax.Text.Trim()

                objUser.UpdateVendorDetails(CompanyID, DivisionID, DepartmentID, vendorID, VendorName, VendorAddress, VendorCity, VendorCountry, VendorZip, VendorState, VendorPhone, VendorEmail, VendorWebPage, VendorFax)

            End If
            ds = FillItemDetailGrid.PopulateItemDetailsGrid(CompanyID, DepartmentID, DivisionID, OrdNumber)

            Dim CreditCardCheck As Boolean = True

            If drpPaymentType.SelectedValue = "Credit Card" Then
                If txtCard.Text = "" Or drpExpirationDate.SelectedValue = "" Then
                    CreditCardCheck = False
                End If
            End If

            If drpShipMethod.SelectedValue = "0" Or ds.Tables(0).Rows.Count = 0 Or CreditCardCheck = False Then
                lblCCMessage.Visible = True

                If drpShipMethod.SelectedValue = "0" Then
                    lblCCMessage.Text = "Please select a Ship Method"
                    Exit Sub
                End If

                If ds.Tables(0).Rows.Count = 0 Then
                    lblCCMessage.Text = "Please select items"
                    Exit Sub
                End If

                If CreditCardCheck = False Then
                    lblCCMessage.Text = "Please Enter Card Number/ExpiryDate"
                    Exit Sub
                End If


            Else

                lblCustomerIDNull.Visible = False
                alertText.Focus()
                Dim PostOrder As New CustomOrder()
                Dim drApprovalNumber As SqlDataReader
                Dim ApprovalNumber As String = ""
                Dim PaymentMethodID As String = ""

                ' Check for PaymentMethodID
                ' If PaymentMethodID='Credit Card' and ApprovalNumber="" then 
                ' Pass the values to Paypal for Credit Card Processing

                drApprovalNumber = PostOrder.GetDetailsForCreditCardApproval(CompanyID,DivisionID, DepartmentID , OrdNumber)
                While drApprovalNumber.Read()
                    ApprovalNumber = drApprovalNumber("CreditCardApprovalNumber").ToString()
                    PaymentMethodID = drApprovalNumber("PaymentMethodID").ToString()

                End While


                'Added Code for Checking Credit Card Offline on 20/12/2007
                Dim PMGCHK As String = "Order"
                If drpOrderTypeIDData.SelectedValue = "POS" Then
                    PMGCHK = "POS"
                End If
                If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
                    PMGCHK = "CartOrder"
                End If

                Dim CreditCardOffline As Boolean = False
                ds = PostOrder.CheckCreditCardOffline(CompanyID, DepartmentID, DivisionID, PMGCHK)

                If ds.Tables(0).Rows.Count > 0 Then

                    CreditCardOffline = ds.Tables(0).Rows(0)("CreditCardOffline").ToString()

                End If

                If drpOrderTypeIDData.SelectedValue = "Estimate" Then

                    Response.Redirect("OrderHeaderDetails.aspx")

                End If

                If PaymentMethodID <> "Credit Card" Or drpPaymentType.SelectedValue <> "Credit Card" Then

                    Dim objcheck As New clsCreditCardChargeOnRebook
                    objcheck.CompanyID = CompanyID
                    objcheck.DivisionID = DivisionID
                    objcheck.DepartmentID = DepartmentID
                    objcheck.OrderNumber = OrdNumber

                    Dim dtcheck As New Data.DataTable
                    dtcheck = objcheck.DetailsOrderPaymentDetails

                    If dtcheck.Rows.Count <> 0 Then
                        SecondTimeBookOthersPaymentCheck(OrdNumber, dtcheck)
                        objcheck.PaymentMethod = PaymentMethodID
                        Try
                            objcheck.PaymentAmount = txtTotal.Text.Trim
                            objcheck.UpdateOrderPaymentDetails()
                        Catch ex As Exception

                        End Try
                        'Exit Sub
                    Else
                        Try
                            objcheck.PaymentAmount = txtTotal.Text.Trim
                        Catch ex As Exception
                            objcheck.PaymentAmount = 0
                        End Try
                        objcheck.PaymentMethod = PaymentMethodID
                        objcheck.InsertOrderPaymentDetails()

                    End If

                    If drpPaymentType.SelectedValue.ToLower = "EMV-Debit".ToLower Then

                        If txtCheck.Text.ToLower = "Approved".ToLower Then
                            PostingOrder(OrdNumber)
                        Else
                            lblCCMessage.Visible = True
                            lblCCMessage.ForeColor = Drawing.Color.Red
                            lblCCMessage.Text = "EMV Payment Not Approved :" & txtCheck.Text
                        End If


                    Else

                        ' PostingOrder(OrdNumber)
                        If PaymentMethodID = "Gift Card" Or drpPaymentType.SelectedValue = "Gift Card" Then
                            btngiftcardbal_Click(sender, e)

                            If txtgiftcardaprovalnumber.Text.Trim = "" Then
                                Dim bal As Double = 0
                                Dim total As Double = 0

                                Try
                                    bal = lblgiftcardbalance.Text.Trim
                                Catch ex As Exception

                                End Try

                                Dim OtherAmount As Double = 0
                                Try
                                    OtherAmount = txtOtherAmount.Text.Trim
                                Catch ex As Exception

                                End Try
                                bal = bal + OtherAmount

                                Try
                                    total = txtTotal.Text.Trim
                                Catch ex As Exception

                                End Try

                                If OtherAmount <> 0 Then
                                    If drpOtherPaymentby.SelectedValue = "" Then
                                        lblCCMessage.Text = "Please select Other Pyament method which used with Gift Card to process order."
                                        lblCCMessage.Visible = True
                                        lblCCMessage.ForeColor = Drawing.Color.Red
                                        Exit Sub
                                    End If
                                End If

                                If bal < total Then
                                    lblCCMessage.Text = "Please check that Gift Card don't have sufficient balance to process order."
                                    lblCCMessage.Visible = True
                                    lblCCMessage.ForeColor = Drawing.Color.Red
                                    Exit Sub
                                Else
                                    UpdateBalanceGiftCardForOrder(total - OtherAmount)
                                    InsertApprovalGiftCardForOrder()
                                    InsertApprovalGiftCardForOrderLogs(txtTotal.Text)
                                    PostingOrder(OrdNumber)

                                End If
                            Else
                                If CheckGiftCardForOrderSecondBook() Then
                                    PostingOrder(OrdNumber)
                                End If
                            End If

                        Else
                            PostingOrder(OrdNumber)
                        End If


                    End If


                ElseIf ApprovalNumber = "" And CreditCardOffline = False Then

                    Try
                        txtCard.Text = txtCard.Text.Trim
                        txtCard.Text = txtCard.Text.Replace(" ", "")
                        txtCard.Text = txtCard.Text.Replace("-", "")
                        txtCard.Text = txtCard.Text.Replace(".", "")
                        txtCard.Text = txtCard.Text.Replace("_", "")
                        txtCard.Text = txtCard.Text.Replace("(", "")
                        txtCard.Text = txtCard.Text.Replace(")", "")

                        If IsNumeric(txtCard.Text) Then
                        Else
                            lblCCMessage.Text = "Please check credit card number only numeric value allowed for it."
                            lblCCMessage.Visible = True
                            lblCCMessage.ForeColor = Drawing.Color.Red
                            Exit Sub
                        End If

                    Catch ex As Exception

                    End Try
                    ' PaymentDetails(OrdNumber)
                    Dim PMG As String = "POS"
                    If drpOrderTypeIDData.SelectedValue = "Order" Then
                        PMG = "Order"
                    End If
                    If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
                        PMG = "CartOrder"
                    End If
                    Dim obj1 As New clsPaymentGateWay
                    obj1.CompanyID = CompanyID
                    obj1.DivisionID = DivisionID
                    obj1.DepartmentID = DepartmentID
                    Dim dt As New Data.DataTable
                    dt = obj1.FillDetails(PMG)

                    If dt.Rows.Count <> 0 Then

                        'Try
                        If dt.Rows(0)("Payment_Gateway") = "PayPal" Then
                            PaymentDetails(OrdNumber)
                        End If
                        If dt.Rows(0)("Payment_Gateway") = "PPI" Then
                            PaymentDetailsPPI(OrdNumber)
                        End If


                        If dt.Rows(0)("Payment_Gateway") = "Mercury" Then
                            PaymentMercury(OrdNumber)
                        End If

                        If dt.Rows(0)("Payment_Gateway") = "MerchantWARE" Then
                            If txtApproval.Text.Trim() <> "" Then
                                PostingOrder(OrdNumber)
                            Else
                                PaymentMecrhantWareDetails(OrdNumber)
                                Exit Sub
                                lblCCMessage.Text = "Please use link to Process credit card or put Approval Code to process order."
                                lblCCMessage.Visible = True
                                lblCCMessage.ForeColor = Drawing.Color.Red
                                Exit Sub
                            End If
                        End If


                        If dt.Rows(0)("Payment_Gateway") = "AuthorizeNet" Then
                            PaymentAuthorizeNetDetails(OrdNumber)
                        End If

                        If dt.Rows(0)("Payment_Gateway") = "None" Then
                            PostingOrder(OrdNumber)
                        End If
                        'Catch ex As Exception
                        '    PaymentDetails(OrdNumber)
                        'End Try


                    Else
                        PaymentDetails(OrdNumber)
                    End If


                Else

                    If ApprovalNumber <> "" Then
                        Dim objcheck As New clsOrderAdjustments
                        objcheck.CompanyID = CompanyID
                        objcheck.DivisionID = DivisionID
                        objcheck.DepartmentID = DepartmentID
                        objcheck.OrderNumber = OrdNumber

                        Dim dtcheck As New Data.DataTable
                        dtcheck = objcheck.DetailsCreditCardPaymentDetails

                        If dtcheck.Rows.Count <> 0 Then
                            Dim output As String
                            output = SecondTimeBookPaymentCheck(OrdNumber, dtcheck)
                            If output <> "" Then
                                lblCCMessage.Text = output
                                Exit Sub
                            End If
                            ' Exit Sub
                        End If
                    End If
                    PostingOrder(OrdNumber)
                End If

            End If
        End If
        ' Session("PO") = "Booked"
        'Edited by Jacob on 24-01-2008

    End Sub
#End Region



 


    Sub SecondTimeBookOthersPaymentCheck(ByVal OrdNumber As String, ByVal dtcheck As Data.DataTable)

        Dim objcheck As New clsCreditCardChargeOnRebook
        objcheck.CompanyID = CompanyID
        objcheck.DivisionID = DivisionID
        objcheck.DepartmentID = DepartmentID
        objcheck.OrderNumber = OrdNumber


        Dim previouspayment As Decimal = 0
        Dim PaymentMethod As String = ""

        Try
            previouspayment = dtcheck.Rows(0)("PaymentAmount")
        Catch ex As Exception

        End Try
        Try
            PaymentMethod = dtcheck.Rows(0)("PaymentMethod")
        Catch ex As Exception

        End Try

        Dim currentpayment As Decimal = 0

        Try
            currentpayment = txtTotal.Text
        Catch ex As Exception

        End Try

        If PaymentMethod = "House Account" Then
            If currentpayment <> previouspayment Then
                If currentpayment > previouspayment Then
                    Dim obj As New clsOrderAdjustments
                    obj.OrderNumber = OrdNumber
                    obj.CompanyID = CompanyID
                    obj.DepartmentID = DepartmentID
                    obj.DivisionID = DivisionID
                    Dim chargevalue As Decimal
                    chargevalue = currentpayment - previouspayment
                    obj.AdjustmentValue = chargevalue
                    Dim output As String
                    output = obj.CreditAdjustments_CustomerAccount()

                Else
                    Dim obj As New clsOrderAdjustments
                    obj.OrderNumber = OrdNumber
                    obj.CompanyID = CompanyID
                    obj.DepartmentID = DepartmentID
                    obj.DivisionID = DivisionID
                    Dim chargevalue As Decimal
                    chargevalue = previouspayment - currentpayment
                    obj.AdjustmentValue = chargevalue
                    Dim output As String
                    output = obj.Refund_CreditAdjustments_CustomerAccount()
                End If
            End If
        End If


    End Sub



    Public Function SecondTimeBookPaymentCheck(ByVal OrdNumber As String, ByVal dtcheck As Data.DataTable) As String

        Dim objcheck As New clsCreditCardChargeOnRebook
        objcheck.CompanyID = CompanyID
        objcheck.DivisionID = DivisionID
        objcheck.DepartmentID = DepartmentID
        objcheck.OrderNumber = OrdNumber


        Dim previouspayment As Decimal = 0

        Try
            previouspayment = dtcheck.Rows(0)("PaymentAmount")
        Catch ex As Exception

        End Try

        Dim currentpayment As Decimal = 0

        Try
            currentpayment = txtTotal.Text
        Catch ex As Exception

        End Try


        Dim PreviousUsedCard As String = ""

        Try
            PreviousUsedCard = dtcheck.Rows(0)("CreditCardNumber")

            Try
                PreviousUsedCard = CryptographyRijndael.EncryptionRijndael.RijndaelDecode(PreviousUsedCard, OrdNumber)
                
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try

        If PreviousUsedCard <> "" Then
            If PreviousUsedCard.Trim <> txtCard.Text.Trim Then
                'clsCreditCardChargeOnRebookCardChanged
                Dim objcheckCardChanged As New clsCreditCardChargeOnRebookCardChanged
                objcheckCardChanged.CompanyID = CompanyID
                objcheckCardChanged.DivisionID = DivisionID
                objcheckCardChanged.DepartmentID = DepartmentID
                objcheckCardChanged.OrderNumber = OrdNumber

                objcheckCardChanged.AmttotalCharge = 0 ' currentpayment
                objcheckCardChanged.AmttotalRefund = previouspayment
                objcheckCardChanged.REMOTE_ADDR = Request.ServerVariables("REMOTE_ADDR")

                objcheckCardChanged.processfrom = "Credit Card Change Process From POS."

                Dim ApprovalNumber As String
                ApprovalNumber = objcheckCardChanged.RebookCreditCardCHarge()

                If ApprovalNumber.Trim = "Offline" Then
                    Return objcheckCardChanged.lblerrormessag.Text
                Else

                    UpdateCancelOrderNumber(OrdNumber)
                    DeleteOrderAdjustmentsApprovalNumber(OrdNumber)
                    Dim PMG As String = "POS"
                    If drpOrderTypeIDData.SelectedValue = "Order" Then
                        PMG = "Order"
                    End If
                    If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
                        PMG = "CartOrder"
                    End If
                    ' PaymentDetails(OrdNumber)
                    Dim obj1 As New clsPaymentGateWay
                    obj1.CompanyID = CompanyID
                    obj1.DivisionID = DivisionID
                    obj1.DepartmentID = DepartmentID
                    Dim dt As New Data.DataTable
                    dt = obj1.FillDetails(PMG)

                    If dt.Rows.Count <> 0 Then
                        'updatemulticard(OrdNumber)

                        'Try
                        If dt.Rows(0)("Payment_Gateway") = "PayPal" Then
                            PaymentDetails(OrdNumber)
                            Return lblCCMessage.Text
                        End If
                        If dt.Rows(0)("Payment_Gateway") = "PPI" Then
                            PaymentDetailsPPI(OrdNumber)
                            Return lblCCMessage.Text
                        End If

                        If dt.Rows(0)("Payment_Gateway") = "Mercury" Then
                            PaymentMercury(OrdNumber)
                        End If

                        If dt.Rows(0)("Payment_Gateway") = "MerchantWARE" Then

                            PaymentMecrhantWareDetails(OrdNumber)

                        End If

                        If dt.Rows(0)("Payment_Gateway") = "AuthorizeNet" Then
                            PaymentAuthorizeNetDetails(OrdNumber)
                        End If

                        If dt.Rows(0)("Payment_Gateway") = "None" Then
                            PostingOrder(OrdNumber)
                        End If
                        'Catch ex As Exception
                        '    PaymentDetails(OrdNumber)
                        'End Try


                    Else
                        PaymentDetails(OrdNumber)
                    End If


                End If



                'If ApprovalNumber.Trim = "" Then
                '    ApprovalNumber = "No Approval Number"
                'End If

                'txtApproval.Text = ApprovalNumber
                'txtIpAddress.Text = Request.ServerVariables("REMOTE_ADDR")
                'Dim objUser As New DAL.CustomOrder()
                'objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, txtIpAddress.Text)


                '''New Lines Added
                'Dim objpayment As New clsOrderAdjustments
                'objpayment.CompanyID = CompanyID
                'objpayment.DivisionID = DivisionID
                'objpayment.DepartmentID = DepartmentID
                'objpayment.OrderNumber = OrdNumber
                'objpayment.NewCreditCardNumber = txtCard.Text
                'objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                'objpayment.NewCreditCardCSVNumber = txtCSV.Text
                'objpayment.UpdateCreditCardPaymentNewCardDetails()
                '''New Lines Added

                Return ""
                Exit Function

            End If
        End If

        If currentpayment <> previouspayment Then


            If CompanyID = "Greene and Greene" Then
                Response.Redirect("SecondTimeBookPaymentCheck.aspx?OrderNumber=" & OrdNumber)
            End If

            objcheck.AmttotalCharge = 0 ' currentpayment
            objcheck.AmttotalRefund = previouspayment

            objcheck.processfrom = "Order Amount Change Process From POS."

            objcheck.REMOTE_ADDR = Request.ServerVariables("REMOTE_ADDR")
            Dim ApprovalNumber As String
            ApprovalNumber = objcheck.RebookCreditCardCHarge()

            If ApprovalNumber = "Offline" Then
                Return objcheck.lblerrormessag.Text
            Else

                UpdateCancelOrderNumber(OrdNumber)
                DeleteOrderAdjustmentsApprovalNumber(OrdNumber)
                Dim PMG As String = "POS"
                If drpOrderTypeIDData.SelectedValue = "Order" Then
                    PMG = "Order"
                End If
                If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
                    PMG = "CartOrder"
                End If
                ' PaymentDetails(OrdNumber)
                Dim obj1 As New clsPaymentGateWay
                obj1.CompanyID = CompanyID
                obj1.DivisionID = DivisionID
                obj1.DepartmentID = DepartmentID
                Dim dt As New Data.DataTable
                dt = obj1.FillDetails(PMG)

                If dt.Rows.Count <> 0 Then
                    'updatemulticard(OrdNumber)

                    'Try
                    If dt.Rows(0)("Payment_Gateway") = "PayPal" Then
                        PaymentDetails(OrdNumber)
                        Return lblCCMessage.Text
                    End If
                    If dt.Rows(0)("Payment_Gateway") = "PPI" Then
                        PaymentDetailsPPI(OrdNumber)
                        Return lblCCMessage.Text
                    End If

                    If dt.Rows(0)("Payment_Gateway") = "Mercury" Then
                        PaymentMercury(OrdNumber)
                    End If


                    If dt.Rows(0)("Payment_Gateway") = "MerchantWARE" Then

                        PaymentMecrhantWareDetails(OrdNumber)

                    End If


                    If dt.Rows(0)("Payment_Gateway") = "AuthorizeNet" Then
                        PaymentAuthorizeNetDetails(OrdNumber)
                    End If


                    If dt.Rows(0)("Payment_Gateway") = "None" Then
                        PostingOrder(OrdNumber)
                    End If
                    'Catch ex As Exception
                    '    PaymentDetails(OrdNumber)
                    'End Try


                Else
                    PaymentDetails(OrdNumber)
                End If




            End If

            'If ApprovalNumber.Trim = "" Then
            '    ApprovalNumber = "No Approval Number"
            'End If

            'txtApproval.Text = ApprovalNumber
            'txtIpAddress.Text = Request.ServerVariables("REMOTE_ADDR")
            'Dim objUser As New DAL.CustomOrder()
            'objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, txtIpAddress.Text)


        End If

        Return ""
    End Function



    Sub PaymentMercury(ByVal OrdNumber As String)

        objclsPaymentGatewayTransactionLogs.CustomerID = txtCustomerTemp.Text
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "Mercury"
        objclsPaymentGatewayTransactionLogs.RefrenaceType = "Order"
        objclsPaymentGatewayTransactionLogs.ProcessDate = Date.Now


        Dim objcheck As New clsOrderAdjustments
        objcheck.CompanyID = CompanyID
        objcheck.DivisionID = DivisionID
        objcheck.DepartmentID = DepartmentID
        objcheck.OrderNumber = OrdNumber

        Dim dtcheck As New Data.DataTable
        dtcheck = objcheck.DetailsCreditCardPaymentDetails

        If dtcheck.Rows.Count <> 0 Then
            Exit Sub
        Else
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "Mercury"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber = "" ' txtCSV.Text
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()
            Catch ex As Exception

            End Try
        End If


        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""

        Dim PMG As String = "Order"
        If drpOrderTypeIDData.SelectedValue = "POS" Then
            PMG = "POS"
        End If
        If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
            PMG = "CartOrder"
        End If




        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetailsMercury(PMG, cmblocationid.SelectedValue.Trim)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = txtTotal.Text
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"

        objclsPaymentGatewayTransactionLogs.ProcessDetails = "Entered Order From POS"
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable

        dtMecrhant = obj1.FillDetailsMercury(PMG, cmblocationid.SelectedValue.Trim)
        Dim APILoginID As String
        Dim TransactionKey As String


        If dt.Rows.Count <> 0 Then
            Try
                APILoginID = dtMecrhant.Rows(0)("MercuryMerchantID")
            Catch ex As Exception
            End Try

            Try
                TransactionKey = dtMecrhant.Rows(0)("MercuryWebPassword")
            Catch ex As Exception
            End Try

        End If



        ccExp = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If

        Dim Amt As String = (txtTotal.Text)
        CreditCardNumber = txtCard.Text
        CreditCardCSVNumber = txtCSV.Text
        'Dim CC As String = "4003000123456781"
        Dim expr As String = CreditCardExpMonth & CreditCardExpYear
        'Dim cvv As String = "123"
        Dim CSTaddress As String = txtCustomerAddress1.Text
        Dim zipcode As String = txtCustomerZip.Text
        Dim MercuryMerchantID As String = ""
        Dim MercuryWebPassword As String = ""
        Try
            MercuryMerchantID = APILoginID
        Catch ex As Exception
        End Try
        Try
            MercuryWebPassword = TransactionKey
        Catch ex As Exception
        End Try
        'Dim orderNo As String = Date.Now.Year & Date.Now.Month & Date.Now.Day & Date.Now.Millisecond

        Dim MercuryData As New clsMercury
        Dim TransactionDataXML As String
        If "CreditSale" = "CreditSale" Then
            MercuryData.MerchantID = MercuryMerchantID
            MercuryData.InvoiceNo = objclsPaymentGatewayTransactionLogs.InLineNumber
            MercuryData.RefNo = objclsPaymentGatewayTransactionLogs.InLineNumber
            MercuryData.Memo = "QuickFlora1.5"
            MercuryData.Frequency = "OneTime"
            MercuryData.RecordNo = "RecordNumberRequested"

            MercuryData.AcctNo = CreditCardNumber
            MercuryData.ExpDate = expr
            MercuryData.Purchase = Amt

            MercuryData.TerminalName = CompanyID
            MercuryData.OperatorID = "Admin"

            MercuryData.cvv = CreditCardCSVNumber
            'MercuryData.Address = address
            MercuryData.zipcode = zipcode

            TransactionDataXML = MercuryData.CreateMercuryXMLWithRecordNo("Credit", "Sale")
        End If

        'txtInternalNotes.Text = TransactionDataXML

        Dim TransactionResponseXML As String = ""

        ''ToDO: Switch over Web 1 and web 2 based upon available connection:

        Dim Mercury_ws1 As New ws.ws
        Dim Mercury As New ws1.ws


        If MercuryMerchantID = "395347306=TOKEN" Then

            TransactionResponseXML = Mercury_ws1.CreditTransaction(TransactionDataXML, MercuryWebPassword)
        Else
            TransactionResponseXML = Mercury.CreditTransaction(TransactionDataXML, MercuryWebPassword)
        End If

        MercuryData.ExtractResponse(TransactionResponseXML)
        Dim response_obj As New Object


        If MercuryData.CMDStatus = "Approved" Then


            lblCCMessage.Visible = True


            Dim AuthCode As String = MercuryData.Response_RefNo
            Dim ApprovalNumber As String = MercuryData.AuthCode

            lblCCMessage.Text = MercuryData.CMDStatus


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "Mercury"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = MercuryData.AuthCode
                objpayment.PPIReferenceID = MercuryData.Response_RefNo
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = Request.ServerVariables("REMOTE_ADDR")


            lblCCMessage.Text = MercuryData.CMDStatus




            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If

            objclsPaymentGatewayTransactionLogs.ResponseNumber = MercuryData.TextResponse
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = TransactionResponseXML 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = MercuryData.Response_RefNo
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = MercuryData.AuthCode
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


            txtApproval.Text = ApprovalNumber
            txtIpAddress.Text = Address

            ' Code Added For Updating Credit Card Validation Code returned From Paypal
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "Mercury"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = MercuryData.AuthCode
                objpayment.PPIReferenceID = MercuryData.Response_RefNo
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber = "" '  txtCSV.Text
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


                objpayment.MercuryData_InvoiceNo = MercuryData.InvoiceNo
                objpayment.MercuryData_RefNo = MercuryData.Response_RefNo
                objpayment.MercuryData_AuthCode = MercuryData.AuthCode
                objpayment.MercuryData_RecordNo = MercuryData.RecordNo
                objpayment.MercuryData_ACQRefData = MercuryData.ACQRefData
                objpayment.MercuryData_ProcessData = MercuryData.ProcessData
                objpayment.UpdateCreditCardPaymentMercury()

            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            PostingOrder(OrdNumber)



        Else


            lblCCMessage.ForeColor = Drawing.Color.Red

            lblCCMessage.Text = MercuryData.CMDStatus
            lblCCMessage.Visible = True

            objclsPaymentGatewayTransactionLogs.ResponseNumber = MercuryData.TextResponse
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "<!-- Request password = " & MercuryWebPassword & " CreditSale--><br>" & TransactionDataXML & "<!-- Response of CreditSale--><br>" & TransactionResponseXML 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = MercuryData.Response_RefNo
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = MercuryData.AuthCode
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "Mercurys"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try


        End If

    End Sub


    Sub PaymentAuthorizeNetDetails(ByVal OrdNumber As String)

        objclsPaymentGatewayTransactionLogs.CustomerID = txtCustomerTemp.Text
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "AuthorizeNet"
        objclsPaymentGatewayTransactionLogs.RefrenaceType = "Order"
        objclsPaymentGatewayTransactionLogs.ProcessDate = Date.Now


        Dim objcheck As New clsOrderAdjustments
        objcheck.CompanyID = CompanyID
        objcheck.DivisionID = DivisionID
        objcheck.DepartmentID = DepartmentID
        objcheck.OrderNumber = OrdNumber

        Dim dtcheck As New Data.DataTable
        dtcheck = objcheck.DetailsCreditCardPaymentDetails

        If dtcheck.Rows.Count <> 0 Then
            Exit Sub
        Else
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "AuthorizeNet"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber ="" ' txtCSV.Text
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()
            Catch ex As Exception

            End Try
        End If


        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""




        ccExp = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If

        Dim Amt As String = (txtTotal.Text)

        CreditCardNumber = txtCard.Text
        CreditCardCSVNumber = txtCSV.Text


        Dim PMG As String = "POS"
        If drpOrderTypeIDData.SelectedValue = "Order" Then
            PMG = "Order"
        End If
        If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
            PMG = "CartOrder"
        End If

        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = txtTotal.Text
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"

        objclsPaymentGatewayTransactionLogs.ProcessDetails = "Entered Order From POS"
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable
        'Dim PMG As String = "Order"
        
        dtMecrhant = obj1.FillDetails(PMG)
        Dim APILoginID As String
        Dim TransactionKey As String 


        If dt.Rows.Count <> 0 Then
            Try
                APILoginID = dtMecrhant.Rows(0)("APILoginID")
            Catch ex As Exception
            End Try

            Try
                TransactionKey = dtMecrhant.Rows(0)("TransactionKey")
            Catch ex As Exception
            End Try

            

        End If


        ' StatusInfo2 = objTXRetail31.IssueKeyedSale(txtName, txtSiteID, txtKey, OrdNumber, Amt, CreditCardNumber, CreditCardExpMonth & "" & CreditCardExpYear, txtCustomerFirstName.Text & " " & txtCustomerLastName.Text, txtCustomerAddress1.Text & " " & txtCustomerAddress2.Text, txtCustomerZip.Text, CreditCardCSVNumber, "True", "", objclsPaymentGatewayTransactionLogs.InLineNumber)


        Dim AuthorizeNetRequest As New AuthorizationRequest(CreditCardNumber, CreditCardExpMonth & "" & CreditCardExpYear, Amt, "Order Number " & OrdNumber)

        'These are optional calls to the API
        AuthorizeNetRequest.AddCardCode(CreditCardCSVNumber)

        'Customer info - this is used for Fraud Detection
        AuthorizeNetRequest.AddCustomer(txtCustomerTemp.Text, txtCustomerFirstName.Text, txtCustomerLastName.Text, txtCustomerAddress1.Text & " " & txtCustomerAddress2.Text, drpState.SelectedValue, txtCustomerZip.Text)

        'order number
        AuthorizeNetRequest.AddInvoice(OrdNumber)

        'Custom values that will be returned with the response
        AuthorizeNetRequest.AddMerchantValue("InLineNumber", objclsPaymentGatewayTransactionLogs.InLineNumber)

        'Shipping Address
        'request.AddShipping("id", "Shippingfirst", "Shippinglast", "Shippingaddress", "state", "zip")


        'step 2 - create the gateway, sending in your credentials and setting the Mode to Test (boolean flag)
        'which is true by default
        'this login and key are the shared dev account - you should get your own if you 
        'want to do more testing

        Dim gate As New Gateway(APILoginID, TransactionKey, True)

        gate.TestMode = False

        If CompanyID = "Greene and Greene" Then
            gate.TestMode = True
        End If

        Dim rwresponse As String()


        Dim response_obj As New AuthorizeNet.GatewayResponse(rwresponse)

        'response_obj = gate.Send(AuthorizeNetRequest)


 Try
            response_obj = gate.Send(AuthorizeNetRequest)
        Catch ex As Exception

			Dim obj_mail As New clsErrorMailHandling
                    obj_mail.OrderNumber = OrdNumber
                    obj_mail.ErrorMailHandling("Payment error Authorize.net :" & CompanyID, ex.Message, "BackOffice-Authorize.net: " & OrdNumber)


            Try
                If response_obj.Approved <> True Then

                    Dim objpayment_Del As New clsOrderAdjustments
                    objpayment_Del.CompanyID = CompanyID
                    objpayment_Del.DivisionID = DivisionID
                    objpayment_Del.DepartmentID = DepartmentID
                    objpayment_Del.OrderNumber = OrdNumber
                    objpayment_Del.PaymentGateway = "AuthorizeNet"
                    objpayment_Del.PayPalPNREF = ""
                    objpayment_Del.PPIOrderID = ""
                    objpayment_Del.PPIReferenceID = ""
                    objpayment_Del.PaymentAmount = txtTotal.Text.Trim
                    objpayment_Del.DeleteOrderAdjustmentsApprovalNumber()

                End If

            Catch ex1 As Exception
                Dim objpayment_Del As New clsOrderAdjustments
                objpayment_Del.CompanyID = CompanyID
                objpayment_Del.DivisionID = DivisionID
                objpayment_Del.DepartmentID = DepartmentID
                objpayment_Del.OrderNumber = OrdNumber
                objpayment_Del.PaymentGateway = "AuthorizeNet"
                objpayment_Del.PayPalPNREF = ""
                objpayment_Del.PPIOrderID = ""
                objpayment_Del.PPIReferenceID = ""
                objpayment_Del.PaymentAmount = txtTotal.Text.Trim
                objpayment_Del.DeleteOrderAdjustmentsApprovalNumber()

            End Try
            

        End Try



        If response_obj.Approved = True Then

            lblCCMessage.Visible = True


            Dim AuthCode As String = response_obj.AuthorizationCode
            Dim ApprovalNumber As String = response_obj.TransactionID


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "AuthorizeNet"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = response_obj.AuthorizationCode
                objpayment.PPIReferenceID = response_obj.TransactionID
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = Request.ServerVariables("REMOTE_ADDR")


            lblCCMessage.Text = response_obj.Message


            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If

            objclsPaymentGatewayTransactionLogs.ResponseNumber = response_obj.Code
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = response_obj.AuthorizationCode
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = response_obj.TransactionID
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

            txtApproval.Text = ApprovalNumber
            txtIpAddress.Text = Address

            ' Code Added For Updating Credit Card Validation Code returned From Paypal
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "AuthorizeNet"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = response_obj.AuthorizationCode
                objpayment.PPIReferenceID = response_obj.TransactionID
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber ="" ' txtCSV.Text
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            PostingOrder(OrdNumber)



        Else


            lblCCMessage.ForeColor = Drawing.Color.Red

            lblCCMessage.Text = response_obj.Message
            lblCCMessage.Visible = True


            objclsPaymentGatewayTransactionLogs.ResponseNumber = response_obj.Code
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& "  ResponseCodeText" & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = response_obj.ResponseCode
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()



            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "AuthorizeNet"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try


        End If

    End Sub




    Sub PaymentMecrhantWareDetails(ByVal OrdNumber As String)

        objclsPaymentGatewayTransactionLogs.CustomerID = txtCustomerTemp.Text
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "MerchantWARE"
        objclsPaymentGatewayTransactionLogs.RefrenaceType = "Order"
        objclsPaymentGatewayTransactionLogs.ProcessDate = Date.Now


        Dim objcheck As New clsOrderAdjustments
        objcheck.CompanyID = CompanyID
        objcheck.DivisionID = DivisionID
        objcheck.DepartmentID = DepartmentID
        objcheck.OrderNumber = OrdNumber

        Dim dtcheck As New Data.DataTable
        dtcheck = objcheck.DetailsCreditCardPaymentDetails

        If dtcheck.Rows.Count <> 0 Then
            Exit Sub
        Else
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber = "" ' txtCSV.Text
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()
            Catch ex As Exception

            End Try
        End If


        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""




        ccExp = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year().ToString().Substring(2, 2)

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If

        Dim Amt As String = (txtTotal.Text)

        CreditCardNumber = txtCard.Text
        CreditCardCSVNumber = txtCSV.Text


        Dim PMG As String = "POS"
        If drpOrderTypeIDData.SelectedValue = "Order" Then
            PMG = "Order"
        End If
        If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
            PMG = "CartOrder"
        End If

        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = txtTotal.Text
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"

        objclsPaymentGatewayTransactionLogs.ProcessDetails = "Entered Order From POS"
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        Dim dtMecrhant As New Data.DataTable
         
        dtMecrhant = obj1.FillDetails(PMG)
        Dim txtSiteID As String
        Dim txtKey As String
        Dim txtName As String
        Dim Merchant_Mode As String = ""


        If dt.Rows.Count <> 0 Then
            Try
                txtSiteID = dtMecrhant.Rows(0)("Merchant_SiteID")
            Catch ex As Exception
            End Try

            Try
                txtKey = dtMecrhant.Rows(0)("Merchant_Key")
            Catch ex As Exception
            End Try

            Try
                txtName = dtMecrhant.Rows(0)("Merchant_Name")
            Catch ex As Exception
            End Try

            Try
                Merchant_Mode = dtMecrhant.Rows(0)("Merchant_Mode")
            Catch ex As Exception
            End Try

        End If

        'Dim objTXRetail31 As New MerchantTest.TXRetail31

        'Dim objTXRetail31_live As New MerchantWARE31Services.TXRetail31

        'Dim StatusInfo2 As New MerchantTest.RetailTransactionStatusInfo2

        'Dim StatusInfo2_live As New MerchantWARE31Services.RetailTransactionStatusInfo2

        Dim objTXRetail31 As Object
        Dim StatusInfo2 As Object


        If Merchant_Mode = "TEST" Then
            objTXRetail31 = New MerchantTest.TXRetail31
            StatusInfo2 = New MerchantTest.RetailTransactionStatusInfo2
        Else
            objTXRetail31 = New MerchantWARE31Services.TXRetail31
            StatusInfo2 = New MerchantWARE31Services.RetailTransactionStatusInfo2
        End If


        StatusInfo2.ApprovalStatus = "Send for process"
        Try
            StatusInfo2 = objTXRetail31.IssueKeyedSale(txtName, txtSiteID, txtKey, OrdNumber, Amt, CreditCardNumber, CreditCardExpMonth & "" & CreditCardExpYear, txtCustomerFirstName.Text & " " & txtCustomerLastName.Text, txtCustomerAddress1.Text & " " & txtCustomerAddress2.Text, txtCustomerZip.Text, CreditCardCSVNumber, "True", "", objclsPaymentGatewayTransactionLogs.InLineNumber)
        Catch ex As Exception
            StatusInfo2.ApprovalStatus = ex.Message
        End Try



        If StatusInfo2.ApprovalStatus = "APPROVED" Then

            lblCCMessage.Visible = True


            Dim AuthCode As String = StatusInfo2.ReferenceID
            Dim ApprovalNumber As String = StatusInfo2.AuthCode


            ''Storing Paypment need to remove Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.UpdateOrderAdjustmentsApprovalNumber()
                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If
            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim Address As String = Request.ServerVariables("REMOTE_ADDR")

            lblCCMessage.Text = StatusInfo2.ApprovalStatus




            If Trim(ApprovalNumber) = "" Then
                ApprovalNumber = "No Approval"
            End If

            objclsPaymentGatewayTransactionLogs.ResponseNumber = StatusInfo2.ApprovalStatus
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.PPIOrderID = StatusInfo2.ReferenceID
            objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = StatusInfo2.AuthCode
            objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

            txtApproval.Text = ApprovalNumber
            txtIpAddress.Text = Address

            ' Code Added For Updating Credit Card Validation Code returned From Paypal
            Dim objUser As New DAL.CustomOrder()
            objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

            ''Storing Paypment Process details on new table for further Cancle it
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ApprovalNumber
                objpayment.PPIOrderID = StatusInfo2.ReferenceID
                objpayment.PPIReferenceID = StatusInfo2.AuthCode
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber = "" 'txtCSV.Text
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()

                If Auth_Capture Then
                    objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                Else
                    objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                End If


            Catch ex As Exception

            End Try
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            PostingOrder(OrdNumber)



        Else


            lblCCMessage.ForeColor = Drawing.Color.Red

            lblCCMessage.Text = StatusInfo2.ApprovalStatus
            lblCCMessage.Visible = True


            objclsPaymentGatewayTransactionLogs.ResponseNumber = lblCCMessage.Text
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& "  ResponseCodeText" & TrxnResponse.RespMsg
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()


            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "MerchantWARE"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try


        End If

    End Sub



    Public Function UpdateCancelOrderNumber(ByVal OrderNumber As String) As Boolean

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        txtApproval.Text = ""
        txtIpAddress.Text = ""
        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "UPDATE OrderHeader set  CreditCardApprovalNumber='',IpAddress=''  Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f5"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f5", SqlDbType.NVarChar, 36)).Value = OrderNumber

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()



            Return True
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try



    End Function


    Public Function DeleteOrderAdjustmentsApprovalNumber(ByVal OrderNumber As String) As Boolean

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "Delete from OrderHeaderCreditCardProcessingDetails Where CompanyID=@f1 and DivisionID=@f2 and DepartmentID=@f3 and OrderNumber=@f4 "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = OrderNumber

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



#Region "Credit Card Payment Section"
    Sub PaymentDetails(ByVal OrdNumber As String)

        objclsPaymentGatewayTransactionLogs.CustomerID = txtCustomerTemp.Text
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "PayPal"
        objclsPaymentGatewayTransactionLogs.RefrenaceType = "Order"
        objclsPaymentGatewayTransactionLogs.ProcessDate = Date.Now


        Dim objcheck As New clsOrderAdjustments
        objcheck.CompanyID = CompanyID
        objcheck.DivisionID = DivisionID
        objcheck.DepartmentID = DepartmentID
        objcheck.OrderNumber = OrdNumber

        Dim dtcheck As New Data.DataTable
        dtcheck = objcheck.DetailsCreditCardPaymentDetails

        If dtcheck.Rows.Count <> 0 Then
            Exit Sub
        Else
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "PayPal"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber = "" ' txtCSV.Text
                ''New Lines Added
                objpayment.InsertCreditCardPaymentDetails()
            Catch ex As Exception

            End Try
        End If

        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim PostOrder As New CustomOrder()
        Dim Partnername As String = ""
        Dim Vendorname As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Dim PaymentURL As String = ""

        ccExp = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If
        CreditCardDateString = CreditCardExpMonth + CreditCardExpYear.Substring(2)

        Dim strRequestID As String
        strRequestID = PayflowUtility.RequestId
        Dim Inv As New Invoice()

        Dim objUser As New DAL.CustomOrder()

        Dim rs As SqlDataReader

        Dim PMG As String = "POS"
        If drpOrderTypeIDData.SelectedValue = "Order" Then
            PMG = "Order"
        End If
        If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
            PMG = "CartOrder"
        End If

        rs = objUser.PopulatePaymentAccounts(CompanyID, DivisionID, DepartmentID, PMG)

        While rs.Read()

            Partnername = rs("Partnername").ToString()
            Vendorname = rs("Vendorname").ToString()
            If rs("Username").ToString() = "" Then
                UserName = rs("Vendorname").ToString()
            Else
                UserName = rs("Username").ToString()


            End If


            Password = rs("Password").ToString()
            If rs("TestMode").ToString() = "True" Then
                PaymentURL = rs("Testurl").ToString()
            Else
                PaymentURL = rs("Liveurl").ToString()
            End If

        End While
        rs.Close()



        Dim User As New UserInfo(UserName, Vendorname, Partnername, Password)
        'Dim Connection As New PayflowConnectionData(PaymentURL, 443, 40, "", 0, "", "", "C:\Certs\")
        Dim Connection As New PayflowConnectionData(PaymentURL, 443, "", 0, "", "")

        Dim Amt As New Currency(txtTotal.Text)
        Inv.Amt = Amt
        Inv.ItemAmt = Amt
        Amt.NoOfDecimalDigits = 2
        Amt.Round = True

        Inv.PoNum = "PO12345"
        Inv.InvNum = "INV12345"
        Inv.CustRef = "CustRef1"
        Inv.Comment1 = CompanyID
        Inv.Comment2 = OrdNumber

        CreditCardNumber = txtCard.Text
        CreditCardCSVNumber = txtCSV.Text

        Dim CC As New CreditCard(CreditCardNumber, CreditCardDateString)

        CC.Cvv2 = CreditCardCSVNumber

        'CC.Name = "Joe M Smith"
        Dim Card As New CardTender(CC)
        'Dim Trans As New SaleTransaction(User, Connection, Inv, Card, strRequestID)
        'Dim Resp As Response = Trans.SubmitTransaction()

        'Dim PMG As String = "POS"
        If drpOrderTypeIDData.SelectedValue = "Order" Then
            PMG = "Order"
        End If
        If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
            PMG = "CartOrder"
        End If

        'New Code For Two step chrage coding
        Dim Auth_Capture As Boolean = False
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        dt = obj1.FillDetails(PMG)

        If dt.Rows.Count <> 0 Then
            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try
        End If


        objclsPaymentGatewayTransactionLogs.ProcessAmount = txtTotal.Text
        If Auth_Capture Then
            objclsPaymentGatewayTransactionLogs.ProcessType = "Auth"
        Else
            objclsPaymentGatewayTransactionLogs.ProcessType = "SALE"
        End If

        objclsPaymentGatewayTransactionLogs.ProcessDetails = "Entered Order From POS"
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = CreditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = CreditCardExpMonth & "/" & CreditCardExpYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = CreditCardCSVNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()


        'New Code For Two step chrage coding
        Dim Resp As Response

        If Auth_Capture Then
            Dim Trans As New AuthorizationTransaction(User, Connection, Inv, Card, strRequestID)
            Resp = Trans.SubmitTransaction()
        Else
            Dim Trans As New SaleTransaction(User, Connection, Inv, Card, strRequestID)
            Resp = Trans.SubmitTransaction()
        End If

        If Not (Resp Is Nothing) Then

            lblCCMessage.Visible = True

            Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
            Dim Result As Integer = TrxnResponse.Result
            Dim RespMsg As String = TrxnResponse.RespMsg

            Dim AuthCode As String = TrxnResponse.AuthCode
            Dim ApprovalNumber As String = TrxnResponse.Pnref
            Dim Code As String = TrxnResponse.CVV2Match

            Dim Address As String = Request.ServerVariables("REMOTE_ADDR")

            lblCCMessage.Text = PayflowUtility.GetStatus(Resp)
            If Not (TrxnResponse Is Nothing) Then
                lblCCMessage.Text = lblCCMessage.Text & " (TrxnResponse Is Nothing)"
            End If

            Dim TransCtx As Context = Resp.TransactionContext
            Select Case Result
                Case -1
                    lblCCMessage.Text = " Failed to connect to Host."
                Case 0
                    lblCCMessage.Text = " Your order has been placed successfully ."
                Case 1
                    lblCCMessage.Text = " User Authentication Failed.  Please contact Customer Service. "
                Case 12
                    lblCCMessage.Text = " Your transaction was declined."
                Case 13
                    lblCCMessage.Text = " Your Transactions was declined."
                Case 4
                    lblCCMessage.Text = " Invalid Amount."
                Case 23
                    lblCCMessage.Text = " Invalid Account Number."
                Case 24
                    lblCCMessage.Text = " Invalid Credit Card Expiration Date."
                Case 124
                    lblCCMessage.Text = " Your Transactions has been declined. Contact Customer Service."
            End Select

            lblCCMessage.Text = lblCCMessage.Text & "<br>" & PayflowUtility.GetStatus(Resp).ToUpper() & "<br>" & TrxnResponse.RespMsg

            If Result = 0 Then
                txtApproval.Text = ApprovalNumber
                txtIpAddress.Text = Address

                ' Code Added For Updating Credit Card Validation Code returned From Paypal

                objclsPaymentGatewayTransactionLogs.ResponseNumber = Result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& " and   ResponseCodeText = " & TrxnResponse.RespMsg
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" 'TrxnResponse.RespText
                objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()


                objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

                ''Storing Paypment Process details on new table for further Cancle it
                Try
                    Dim objpayment As New clsOrderAdjustments
                    objpayment.CompanyID = CompanyID
                    objpayment.DivisionID = DivisionID
                    objpayment.DepartmentID = DepartmentID
                    objpayment.OrderNumber = OrdNumber
                    objpayment.PaymentGateway = "PayPal"
                    objpayment.PayPalPNREF = ApprovalNumber
                    objpayment.PPIOrderID = ""
                    objpayment.PPIReferenceID = ""
                    objpayment.PaymentAmount = txtTotal.Text.Trim
                    objpayment.DeleteOrderAdjustmentsApprovalNumber()
                    ''New Lines Added
                    objpayment.NewCreditCardNumber = txtCard.Text
                    objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                    objpayment.NewCreditCardCSVNumber = "" ' txtCSV.Text
                    ''New Lines Added
                    objpayment.InsertCreditCardPaymentDetails()

                    If Auth_Capture Then
                        objpayment.UpdateCreditCardPaymentChargeType("AUTH", "AUTH")
                    Else
                        objpayment.UpdateCreditCardPaymentChargeType("SALE", "SALE")
                    End If

                Catch ex As Exception

                End Try
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                PostingOrder(OrdNumber)
            Else

                objclsPaymentGatewayTransactionLogs.ResponseNumber = Result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& "  ResponseCodeText" & TrxnResponse.RespMsg
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = "" ' TrxnResponse.RespText
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

                Try
                    Dim objpayment As New clsOrderAdjustments
                    objpayment.CompanyID = CompanyID
                    objpayment.DivisionID = DivisionID
                    objpayment.DepartmentID = DepartmentID
                    objpayment.OrderNumber = OrdNumber
                    objpayment.PaymentGateway = "PayPal"
                    objpayment.PayPalPNREF = ApprovalNumber
                    objpayment.PPIOrderID = ""
                    objpayment.PPIReferenceID = ""
                    objpayment.PaymentAmount = txtTotal.Text.Trim
                    objpayment.DeleteOrderAdjustmentsApprovalNumber()
                Catch ex As Exception
                End Try

            End If
        Else
            lblCCMessage.ForeColor = Drawing.Color.Red
            Try
                lblCCMessage.Text = "General error:Payment engine error on Paypal Error: number:"
            Catch ex As Exception
                lblCCMessage.Text = "General error:Payment engine error on Paypal"
            End Try
            lblCCMessage.Text = lblCCMessage.Text & "<br>" & "Unable to process order this time."
            lblCCMessage.Visible = True

            objclsPaymentGatewayTransactionLogs.ResponseNumber = "No Response"
            objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()
        End If

    End Sub
#End Region

    Sub PaymentDetailsPPI(ByVal OrdNumber As String)


        objclsPaymentGatewayTransactionLogs.CustomerID = txtCustomerTemp.Text
        objclsPaymentGatewayTransactionLogs.PaymentGateway = "PPI"
        objclsPaymentGatewayTransactionLogs.RefrenaceType = "Order"
        objclsPaymentGatewayTransactionLogs.ProcessDate = Date.Now


        Dim objcheck As New clsOrderAdjustments
        objcheck.CompanyID = CompanyID
        objcheck.DivisionID = DivisionID
        objcheck.DepartmentID = DepartmentID
        objcheck.OrderNumber = OrdNumber

        Dim dtcheck As New Data.DataTable
        dtcheck = objcheck.DetailsCreditCardPaymentDetails

        If dtcheck.Rows.Count <> 0 Then
            'SecondTimeBookPaymentCheck(OrdNumber, dtcheck)
            Exit Sub
        Else
            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "PPI"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim

                ''New Lines Added
                objpayment.NewCreditCardNumber = txtCard.Text
                objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                objpayment.NewCreditCardCSVNumber = "" 'txtCSV.Text
                ''New Lines Added

                objpayment.InsertCreditCardPaymentDetails()
            Catch ex As Exception
            End Try
        End If

        Dim ccExp As DateTime
        Dim CreditCardCSVNumber As String = ""
        Dim CreditCardNumber As String = ""
        Dim CreditCardName As String = ""
        Dim ShippingZip As String = ""
        Dim CreditCardExpDate As String = ""
        Dim CreditCardExpMonth As String = ""
        Dim CreditCardDateString As String = ""
        Dim CreditCardExpYear As String = ""
        Dim PaymentMethodID As String = ""
        Dim TransactionID As String = ""
        Dim AuthorizationCode As String = ""
        Dim BillingZip As String = ""
        Dim Track1 As String = ""
        Dim Track2 As String = ""

        Dim Auth_Capture As Boolean = False


        ''New Feild ''
        Dim AVSCHECK As Boolean = False
        Dim AVSZIPCODECHECK As Boolean = False
        Dim CVSCHECK As String = False
        ''New Feild ''



        Dim PostOrder As New CustomOrder()
        Dim objUser As New DAL.CustomOrder()

        Dim eComerceTransactions, OrderEntryTransactions, POSTransactions As String

        Dim PaymentURL As String = ""

        Dim PPI_ACOUNT_TOKEN As String

        Dim PMG As String = "POS"
        If drpOrderTypeIDData.SelectedValue = "Order" Then
            PMG = "Order"
        End If
        If drpOrderTypeIDData.SelectedValue = "CartOrder" Then
            PMG = "CartOrder"
        End If

        'CompanyID, DepartmentID, DivisionID
        Dim obj1 As New clsPaymentGateWay
        obj1.CompanyID = CompanyID
        obj1.DivisionID = DivisionID
        obj1.DepartmentID = DepartmentID
        Dim dt As New Data.DataTable
        ' dt = obj1.FillDetails(PMG)

        If CompanyID = "Greene and Greene" Or CompanyID = "Ovando Floral and Event Design-10065" Then

            dt = obj1.FillDetailsPaymentGatwayByOrder(OrdNumber)

            If (dt.Rows.Count = 0) Then
                dt = obj1.FillDetailsPPI("POS", cmblocationid.SelectedValue.Trim)
            End If

        Else
            'dt = obj1.FillDetails
            dt = obj1.FillDetails(PMG)
        End If


        If dt.Rows.Count <> 0 Then


            Try
                Auth_Capture = dt.Rows(0)("Auth_Capture")
            Catch ex As Exception
                Auth_Capture = False
                obj1.UpdateAuth_Capture_Sale(False)
            End Try

            Dim activetoken As String
            activetoken = dt.Rows(0)("Active")

            If activetoken = "Live" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKEN")

            End If

            If activetoken = "Test" Then
                PPI_ACOUNT_TOKEN = dt.Rows(0)("PPI_TOKENTest")
            End If


            ''New Data Is geting from here''
            Try
                AVSCHECK = CType(dt.Rows(0)("AVS"), Boolean)
            Catch ex As Exception
                AVSCHECK = False
            End Try
            Try
                CVSCHECK = CType(dt.Rows(0)("CVV"), Boolean)
            Catch ex As Exception
                CVSCHECK = False
            End Try
            Try
                AVSZIPCODECHECK = CType(dt.Rows(0)("AVSZIPCODE"), Boolean)
            Catch ex As Exception
                AVSZIPCODECHECK = False
            End Try
            ''New Data Is geting from here''

            eComerceTransactions = dt.Rows(0)("eComerceTransactions")
            OrderEntryTransactions = dt.Rows(0)("OrderEntryTransactions")
            POSTransactions = dt.Rows(0)("POSTransactions")


        End If


        ccExp = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
        CreditCardExpMonth = ccExp.Month()
        CreditCardExpYear = ccExp.Year()

        If CreditCardExpMonth.Trim().Length < 2 Then
            CreditCardExpMonth = "0" + CreditCardExpMonth
        End If

        Dim Amt As String = txtTotal.Text

        CreditCardNumber = txtCard.Text
        CreditCardCSVNumber = txtCSV.Text

        Dim trackdata As String
        trackdata = intxtrawcard.Value

        If trackdata <> "" Then
            Dim TRACKarray As String()
            TRACKarray = trackdata.Split(";")
            If TRACKarray.Length = 2 Then
                Track1 = TRACKarray(0)
                Track2 = ";" & TRACKarray(1)
            Else
                Track1 = trackdata
                Track2 = ""
            End If
            
        End If
        '' PPI variable

        Dim Res As Boolean
        Dim result As Integer
        Dim Result2 As Integer
        Dim resResponseCode As String
        Dim resSecondaryResponseCode As String
        Dim resCreditCardVerificationResponse As String
        Dim resAVSCode As String



        Dim obj As New PPIPayMover

        obj.orderID = ""

        obj.creditCardNumber = CreditCardNumber
        obj.creditCardVerificationNumber = CreditCardCSVNumber
        obj.expireMonth = CreditCardExpMonth
        obj.expireYear = CreditCardExpYear

        obj.chargeTotal = Amt



        'obj.chargeType = "SALE"

        If Auth_Capture Then
            obj.chargeType = "AUTH"
        Else
            obj.chargeType = "SALE"
        End If



        obj.industry = "RETAIL"
        obj.ACCOUNT_TOKEN = PPI_ACOUNT_TOKEN


        'If CompanyID = "The Garden Gate-92886" Or CompanyID = "Flowers of the Field-89123" Or CompanyID = "Busseys,Inc.30125" Then
        If True Then
            If Track2 <> "" Or Track1 <> "" Then

                If Track1.IndexOf("^") > 0 Then
                    obj.transactionConditionCode = POSTransactions
                Else
                    Track1 = ""
                    Track2 = ""
                    obj.transactionConditionCode = OrderEntryTransactions
                End If

            Else
                Track1 = ""
                Track2 = ""
                obj.transactionConditionCode = OrderEntryTransactions
            End If

            If chkIgnoreTrack.Checked Then
                Track1 = ""
                Track2 = ""
                obj.transactionConditionCode = OrderEntryTransactions
            End If

        Else
            Track1 = ""
            Track2 = ""
            obj.transactionConditionCode = OrderEntryTransactions
        End If


        obj.Track1 = Track1
        obj.Track2 = Track2



        'If drpstoredcc.SelectedValue <> "Other" Then

        '    Dim objCustomerCreditCards As New clsCustomerCreditCards
        '    objCustomerCreditCards.CompanyID = CompanyID
        '    objCustomerCreditCards.DivisionID = DivisionID
        '    objCustomerCreditCards.DepartmentID = DepartmentID
        '    objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
        '    Dim dtCustomerCreditCards As New DataTable
        '    dtCustomerCreditCards = objCustomerCreditCards.CustomerCardDetailsForlinenumber
        '    If dtCustomerCreditCards.Rows.Count <> 0 Then
        '        'txtCustomerTemp.Text 
        '        Dim FLName As String = ""
        '        Dim FLNameArr() As String
        '        FLName = dtCustomerCreditCards.Rows(0)("CustomerName").ToString
        '        If FLName.IndexOf("-") <> -1 Then
        '            FLNameArr = FLName.Split("-")
        '        Else
        '            FLNameArr = FLName.Split(" ")
        '        End If
        '        If FLNameArr.Length = 2 Then
        '            obj.billFirstName = FLNameArr(0)
        '            obj.billLastName = FLNameArr(1)
        '        End If
        '        If FLNameArr.Length = 3 Then
        '            obj.billFirstName = FLNameArr(0) & " " & FLNameArr(1)
        '            obj.billLastName = FLNameArr(2)
        '        End If
        '        If FLNameArr.Length = 1 Then
        '            obj.billFirstName = FLNameArr(0)
        '            obj.billLastName = ""
        '        End If
        '        obj.billAddressOne = dtCustomerCreditCards.Rows(0)("CustomerAddress1")
        '        obj.billAddressTwo = dtCustomerCreditCards.Rows(0)("CustomerAddress2") & " " & dtCustomerCreditCards.Rows(0)("CustomerAddress3")
        '        obj.billZipOrPostalCode = dtCustomerCreditCards.Rows(0)("CustomerZip").ToString()
        '        obj.billCity = dtCustomerCreditCards.Rows(0)("CustomerCity")
        '        obj.billStateOrProvince = dtCustomerCreditCards.Rows(0)("CustomerState")
        '        obj.billCountry = dtCustomerCreditCards.Rows(0)("CustomerCountry")
        '    End If
        'Else
        '    obj.billFirstName = txtCustomerFirstName.Text
        '    obj.billLastName = txtCustomerLastName.Text
        '    obj.billAddressOne = txtCustomerAddress1.Text
        '    obj.billAddressTwo = txtCustomerAddress2.Text
        '    obj.billZipOrPostalCode = txtCustomerZip.Text
        '    obj.billCity = txtCustomerCity.Text
        '    obj.billStateOrProvince = txtCustomerState.Text
        '    obj.billCountry = drpCountry.SelectedValue

        'End If

        obj.billFirstName = txtCustomerFirstName.Text
        obj.billLastName = txtCustomerLastName.Text
        obj.billAddressOne = txtCustomerAddress1.Text
        obj.billAddressTwo = txtCustomerAddress2.Text
        obj.billZipOrPostalCode = txtCustomerZip.Text
        obj.billCity = txtCustomerCity.Text
        obj.billStateOrProvince = txtCustomerState.Text
        obj.billCountry = drpCountry.SelectedValue



        ''START'' set this to stop the extra checking for the Card processing
        AVSCHECK = False
        CVSCHECK = False
        AVSZIPCODECHECK = False
        ''STOP'' set this to stop the extra checking for the Card processing

        objclsPaymentGatewayTransactionLogs.ProcessAmount = obj.chargeTotal
        objclsPaymentGatewayTransactionLogs.ProcessType = obj.chargeType
        objclsPaymentGatewayTransactionLogs.ProcessDetails = "Order Entered From POS"
        objclsPaymentGatewayTransactionLogs.CreditCardNumber = obj.creditCardNumber
        objclsPaymentGatewayTransactionLogs.CreditCardExpDate = obj.expireMonth & "/" & obj.expireYear
        objclsPaymentGatewayTransactionLogs.CreditCardCSVNumber = obj.creditCardVerificationNumber

        objclsPaymentGatewayTransactionLogs.ReferenceNumber = OrdNumber
        objclsPaymentGatewayTransactionLogs.PPIOrderID = ""
        objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
        objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ""
        objclsPaymentGatewayTransactionLogs.ResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.ResponseNumber = ""
        objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = ""
        objclsPaymentGatewayTransactionLogs.InsertPaymentGatewayTransactionLogs()

        txtInternalNotes.Text = objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsTdata(trackdata, obj.transactionConditionCode)


        ''Res = obj.Process_Transaction
        If AVSCHECK = True Or CVSCHECK = True Or AVSZIPCODECHECK = True Then
            obj.chargeType = "AUTH"
            obj.chargeTotal = 1
            Res = obj.Process_Transaction

            If Res Then
                resResponseCode = obj.resResponseCode
                resSecondaryResponseCode = obj.resSecondaryResponseCode
                resCreditCardVerificationResponse = obj.resCreditCardVerificationResponse
                resAVSCode = obj.resAVSCode
                result = obj.resResponseCode
                Result2 = resSecondaryResponseCode

                If result = 1 Then

                    Dim chk As Boolean = True
                    'obj.chargeTotal = AmtPPI - 0.01
                    'Res = obj.Process_Transaction
                    If AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                            If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 4 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If AVSZIPCODECHECK Then
                        If IsNumeric(resAVSCode) Then
                            If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 5 And resAVSCode <> 6 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If CVSCHECK Then

                        If IsNumeric(resCreditCardVerificationResponse) Then
                            If resCreditCardVerificationResponse <> 1 Then
                                result = -1
                                chk = False
                            End If
                        End If

                    End If

                    If chk Then
                        obj.chargeType = "SALE"
                        obj.chargeTotal = Amt
                        Res = obj.Process_Transaction
                    End If

                End If
            Else
                Res = obj.Process_Transaction
                Try
                    result = obj.resResponseCode
                Catch ex As Exception
                    result = 0
                End Try
            End If

        Else
            Res = obj.Process_Transaction
            Try
                result = obj.resResponseCode
            Catch ex As Exception
                result = 0
            End Try
        End If

        Dim misc As String = ""


        If CompanyID = "Demo Site-90210" Then
            Res = True
            result = 1
            obj.resOrderID = "resOrderID-test"
            obj.resBankApprovalCode = "resBankApprovalCode-test"
            obj.resReferenceID = "resReferenceID-test"
        End If


        If Res Then

            Try

                Dim objpmt As New clsPaymentGateWay
                objpmt.CompanyID = CompanyID
                objpmt.DivisionID = DivisionID
                objpmt.DepartmentID = DepartmentID
                objpmt.StorePaymentGatwayByOrder("POS", cmblocationid.SelectedValue.Trim, OrdNumber, "Order")

            Catch ex As Exception

            End Try

            lblCCMessage.Visible = True
            Dim Address As String = Request.ServerVariables("REMOTE_ADDR")

            'For PPI

            Dim ApprovalNumber As String
            Dim resOrderID As String
            resOrderID = obj.resOrderID
            ApprovalNumber = obj.resBankApprovalCode
            Dim resReferenceID As String
            resReferenceID = obj.resReferenceID


            'result = obj.resResponseCode

            Dim chk As Boolean = False
            Select Case result

                Case 0
                    lblCCMessage.Text = " NONE."
                    chk = False
                Case 1
                    lblCCMessage.Text = " Your order has been placed successfully. "
                    chk = False
                Case 2
                    lblCCMessage.Text = " Missing required request field."
                    chk = False
                Case 3
                    lblCCMessage.Text = " Invalid request field."
                    chk = False
                Case 4
                    lblCCMessage.Text = " Illegal transaction request."
                    chk = False
                Case 5
                    lblCCMessage.Text = " Transaction server error."
                    chk = False
                Case 6
                    lblCCMessage.Text = " Transaction not possible."
                    chk = False
                Case 7
                    lblCCMessage.Text = " IInvalid version."
                    chk = False
                Case 100
                    lblCCMessage.Text = " Credit card declined."
                    chk = False
                Case 101
                    lblCCMessage.Text = " Acquirer gateway error."
                    chk = False
                Case 102
                    lblCCMessage.Text = " Payment engine error."
                    chk = False
            End Select

            lblCCMessage.Text = lblCCMessage.Text & " <br> " & obj.resResponseCodeText

            'If chk = False Then
            '    lblCCMessage.Text = "Response From payment GateWay :" & misc
            'End If

            If result = 1 Then

                If ApprovalNumber.Trim = "" Then
                    ApprovalNumber = "No Approval Number"
                End If

                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& " and   ResponseCodeText = " & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.PPIOrderID = obj.resOrderID
                objclsPaymentGatewayTransactionLogs.PPIApprovalNumber = ApprovalNumber
                objclsPaymentGatewayTransactionLogs.PayPalPNREF = ""
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseTrue()

                txtApproval.Text = ApprovalNumber
                txtIpAddress.Text = Address
                objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, ApprovalNumber, Address)

                ''Storing Paypment Process details on new table for further Cancle it
                Try
                    Dim objpayment As New clsOrderAdjustments
                    objpayment.CompanyID = CompanyID
                    objpayment.DivisionID = DivisionID
                    objpayment.DepartmentID = DepartmentID
                    objpayment.OrderNumber = OrdNumber
                    objpayment.PaymentGateway = "PPI"
                    objpayment.PayPalPNREF = ""
                    objpayment.PPIOrderID = resOrderID
                    objpayment.PPIReferenceID = resReferenceID
                    objpayment.PaymentAmount = txtTotal.Text.Trim
                    objpayment.DeleteOrderAdjustmentsApprovalNumber()
                    ''New Lines Added
                    objpayment.NewCreditCardNumber = txtCard.Text
                    objpayment.NewCreditCardExpDate = Convert.ToDateTime(drpExpirationDate.SelectedValue).Date
                    objpayment.NewCreditCardCSVNumber = ""  'txtCSV.Text
                    ''New Lines Added
                    objpayment.InsertCreditCardPaymentDetails()
                    objpayment.UpdateCreditCardPaymentChargeType(obj.chargeType, obj.chargeType)

                Catch ex As Exception
                End Try
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                PostingOrder(OrdNumber)
            Else

                objclsPaymentGatewayTransactionLogs.ResponseNumber = result
                objclsPaymentGatewayTransactionLogs.ResponseMessage = lblCCMessage.Text '& "  ResponseCodeText =" & obj.resResponseCodeText
                objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = obj.resSecondaryResponseCode
                objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()


                Try
                    Dim objpayment As New clsOrderAdjustments
                    objpayment.CompanyID = CompanyID
                    objpayment.DivisionID = DivisionID
                    objpayment.DepartmentID = DepartmentID
                    objpayment.OrderNumber = OrdNumber
                    objpayment.PaymentGateway = "PPI"
                    objpayment.PayPalPNREF = ""
                    objpayment.PPIOrderID = resOrderID
                    objpayment.PPIReferenceID = resReferenceID
                    objpayment.PaymentAmount = txtTotal.Text.Trim
                    objpayment.DeleteOrderAdjustmentsApprovalNumber()
                Catch ex As Exception
                End Try

                If result = -1 Then
                    Dim restxt0 As String = ""
                    Dim restxt1 As String = ""
                    Dim restxt11 As String = ""
                    Dim restxt111 As String = ""
                    Dim restxt2 As String = ""
                    If AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If
                        If resAVSCode <> 2 And resAVSCode <> 3 And resAVSCode <> 4 Then
                            Select Case resAVSCode
                                Case 1
                                    restxt0 = "Address do not match. "

                                Case 5
                                    restxt0 = "Address does not match. "
                                Case 6
                                    restxt0 = "Address does not match. "

                            End Select
                        End If
                    End If

                    If AVSZIPCODECHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If
                        If resAVSCode <> 2 And resAVSCode <> 3 Then
                            Select Case resAVSCode
                                Case 1
                                    restxt1 = " Postal Code do not match."
                                Case 4
                                    restxt1 = " Postal/Zip Code does not match."
                            End Select
                        End If
                    End If

                    If AVSZIPCODECHECK Or AVSCHECK Then
                        If IsNumeric(resAVSCode) Then
                        Else
                            resAVSCode = -1
                        End If

                        Select Case resAVSCode
                            Case 7
                                restxt11 = " AVS information is not available for this transaction."
                            Case 8
                                restxt11 = " No response from issuer."
                            Case 9
                                restxt11 = " AVS is not supported for this transaction."
                            Case 10
                                restxt11 = " AVS is not supported by the card issuer."
                            Case 11
                                restxt11 = " AVS is not supported for this card brand."
                            Case 12
                                restxt11 = " AVS information was not specified in the correct format."

                        End Select

                    End If

                    restxt111 = restxt0 & restxt1 & restxt11

                    If CVSCHECK Then
                        If IsNumeric(resCreditCardVerificationResponse) Then
                        Else
                            resCreditCardVerificationResponse = -1
                            restxt2 = "From CreditCard Verification No Response"
                        End If
                        If resCreditCardVerificationResponse <> 1 Then
                            Select Case resCreditCardVerificationResponse
                                Case 2
                                    restxt2 = " Credit Card Verification Not Match."
                                Case 3
                                    restxt2 = " Credit Card Verification Not processed."
                                Case 4
                                    restxt2 = " Credit Card Verification Should have been present."
                                Case 5
                                    restxt2 = " Credit Card Verification Not supported by issuer."
                                Case 6
                                    restxt2 = " Credit Card Verification No response from CVV system."
                            End Select
                        End If
                    End If
                    lblCCMessage.ForeColor = Drawing.Color.Red
                    lblCCMessage.Text = "Error Occured While Payment Processing : <b>" & restxt111 & "     " & restxt2 & " <b>"
                    'Response.Redirect("PaymentInfo.aspx?SessID=" & BwrID & "&Result1=" & "     " & restxt111 & "     " & restxt2, False)
                End If

            End If


        Else

            Try
                Dim objpayment As New clsOrderAdjustments
                objpayment.CompanyID = CompanyID
                objpayment.DivisionID = DivisionID
                objpayment.DepartmentID = DepartmentID
                objpayment.OrderNumber = OrdNumber
                objpayment.PaymentGateway = "PPI"
                objpayment.PayPalPNREF = ""
                objpayment.PPIOrderID = ""
                objpayment.PPIReferenceID = ""
                objpayment.PaymentAmount = txtTotal.Text.Trim
                objpayment.DeleteOrderAdjustmentsApprovalNumber()
            Catch ex As Exception
            End Try


            lblCCMessage.ForeColor = Drawing.Color.Red

            Try
                lblCCMessage.Text = "General error:Payment engine error on PPI Error: number:" & obj.resResponseCode
            Catch ex As Exception
                lblCCMessage.Text = "General error:Payment engine error on PPI"
            End Try
            lblCCMessage.Text = lblCCMessage.Text & "<br>" & "Unable to process order this time."
            lblCCMessage.Visible = True

            objclsPaymentGatewayTransactionLogs.ResponseNumber = "False"
            objclsPaymentGatewayTransactionLogs.ResponseMessage = "Unable to process order this time."
            objclsPaymentGatewayTransactionLogs.SecondryResponseMessage = lblCCMessage.Text
            objclsPaymentGatewayTransactionLogs.UpdatePaymentGatewayTransactionLogsOnResponseFalse()

        End If

    End Sub

    Protected Sub WmailBeforePostingOrder(ByVal OrdNumber As String)

        '  Exit Sub

        Dim dtp As New Data.DataTable
        Dim EmailContent As String = ""
        Dim objp As New clsOrderMailSendForTrace
        objp.CompanyID = CompanyID
        objp.DivisionID = DivisionID
        objp.DepartmentID = DepartmentID
        objp.OrderNumber = OrdNumber

        'Dim objUser As New DAL.CustomOrder()
        'objUser.UpdateCreditCardTransactions(CompanyID, DepartmentID, DivisionID, OrdNumber, txtApproval.Text, txtIpAddress.Text)


        dtp = objp.Details_OrderMailSendForTraceTemplates

        If dtp.Rows.Count <> 0 Then
            EmailContent = dtp.Rows(0)("MailSubjectTemplate")
        End If
        '<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>
        '</span>
        EmailContent = EmailContent.Replace("mgRetailerLogo", "Company = " & CompanyID & "<br> Division = " & DivisionID & "<br> Department = " & DepartmentID)

        EmailContent = EmailContent.Replace("lblOrderNumberData", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & lblOrderNumberData.Text & "</span>")
        EmailContent = EmailContent.Replace("lblOrderDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & lblOrderDate.Text & "</span>")
        EmailContent = EmailContent.Replace("drpOrderTypeIDData", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpOrderTypeIDData.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("drpTransaction", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpTransaction.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("drpEmployeeID", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpEmployeeID.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("drpLocation", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & cmblocationid.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("drpProject", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpProject.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("drpPaymentType", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpPaymentType.SelectedValue & "</span>")


        EmailContent = EmailContent.Replace("txtCustomerTemp", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerTemp.Text & "</span>")
        EmailContent = EmailContent.Replace("DropDownList1", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & DropDownList1.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerFirstName", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerFirstName.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerLastName", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerLastName.Text & "</span>")
        EmailContent = EmailContent.Replace("txtAttention", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtAttention.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCompany", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCompany.Text & "</span>")
        EmailContent = EmailContent.Replace("txtComments", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtComments.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerAddress1", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerAddress1.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerAddress2", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerAddress2.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerAddress3", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerAddress3.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerCity", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerCity.Text & "</span>")
        EmailContent = EmailContent.Replace("drpState", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpState.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerZip", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerZip.Text & "</span>")
        EmailContent = EmailContent.Replace("drpCountry", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpCountry.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerPhone", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerPhone.Text & "</span>")
        EmailContent = EmailContent.Replace("txtExt", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtExt.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerCell", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerCell.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerFax", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerFax.Text & "</span>")
        EmailContent = EmailContent.Replace("txtCustomerEmail", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCustomerEmail.Text & "</span>")
        EmailContent = EmailContent.Replace("drpNewsletterID", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpNewsletterID.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtPO", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtPO.Text & "</span>")
        EmailContent = EmailContent.Replace("drpSource", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpSource.SelectedValue & "</span>")


        EmailContent = EmailContent.Replace("txtDeliveryDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtDeliveryDate.Text & "</span>")
        EmailContent = EmailContent.Replace("drpShipMethod", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpShipMethod.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtCardMessageDesc", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCardMessageDesc.Text & "</span>")
        EmailContent = EmailContent.Replace("drpDestinationType", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpDestinationType.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("drpPriorirty", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpPriorirty.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("drpShipCustomerSalutation", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpShipCustomerSalutation.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtShippingName", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingName.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShippingLastName", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingLastName.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShippingAttention", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingAttention.Text & "</span>")
        EmailContent = EmailContent.Replace("txtDriverRouteInfo", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtDriverRouteInfo.Text & "</span>")
        EmailContent = EmailContent.Replace("drpZone", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpZone.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtShipCompany", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShipCompany.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShippingAddress1", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingAddress1.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShippingAddress2", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingAddress2.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShippingAddress3", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingAddress3.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShippingCity", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingCity.Text & "</span>")
        EmailContent = EmailContent.Replace("drpShippingState", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpShippingState.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtShippingZip", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShippingZip.Text & "</span>")
        EmailContent = EmailContent.Replace("drpShipCountry", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpShipCountry.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtShipCustomerPhone", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShipCustomerPhone.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShipExt", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShipExt.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShipCustomerCell", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShipCustomerCell.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShipCustomerFax ", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShipCustomerFax.Text & "</span>")
        EmailContent = EmailContent.Replace("drpOccasionCode", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpOccasionCode.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtSubtotal", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtSubtotal.Text & "</span>")
        EmailContent = EmailContent.Replace("txtDelivery", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtDelivery.Text & "</span>")
        EmailContent = EmailContent.Replace("txtService", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtService.Text & "</span>")
        EmailContent = EmailContent.Replace("txtRelay", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtRelay.Text & "</span>")
        EmailContent = EmailContent.Replace("txtDiscountAmount", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtDiscountAmount.Text & "</span>")
        EmailContent = EmailContent.Replace("drpTaxes", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpTaxes.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtTaxPercent", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtTaxPercent.Text & "</span>")
        EmailContent = EmailContent.Replace("txtTax", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtTax.Text & "</span>")
        EmailContent = EmailContent.Replace("txtTotal", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtTotal.Text & "</span>")
        EmailContent = EmailContent.Replace("txtInternalNotes", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtInternalNotes.Text & "</span>")


        EmailContent = EmailContent.Replace("txtPostedDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtPostedDate.Text & "</span>")
        EmailContent = EmailContent.Replace("txtPostedTime", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtPostedTime.Text & "</span>")

        EmailContent = EmailContent.Replace("txtPickedDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtPickedDate.Text & "</span>")
        EmailContent = EmailContent.Replace("txtPickedTime", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtPickedTime.Text & "</span>")

        EmailContent = EmailContent.Replace("txtPrintedDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtPrintedDate.Text & "</span>")
        EmailContent = EmailContent.Replace("txtPrintedTime", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtPrintedTime.Text & "</span>")

        EmailContent = EmailContent.Replace("txtBilledDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtBilledDate.Text & "</span>")
        EmailContent = EmailContent.Replace("txtBilledTime", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtBilledTime.Text & "</span>")

        EmailContent = EmailContent.Replace("txtShipDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShipDate.Text & "</span>")
        EmailContent = EmailContent.Replace("txtShipTime", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtShipTime.Text & "</span>")

        EmailContent = EmailContent.Replace("txtInvoiceDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtInvoiceDate.Text & "</span>")
        EmailContent = EmailContent.Replace("txtInvoiceTime", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtInvoiceTime.Text & "</span>")


        EmailContent = EmailContent.Replace("lblSystemWM", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & "" & "</span>")



        EmailContent = EmailContent.Replace("drpCardType", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpCardType.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtCard", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCard.Text & "</span>")

        EmailContent = EmailContent.Replace("drpExpirationDate", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & drpExpirationDate.SelectedValue & "</span>")
        EmailContent = EmailContent.Replace("txtCSV", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtCSV.Text & "</span>")

        EmailContent = EmailContent.Replace("txtApproval", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtApproval.Text & "</span>")
        EmailContent = EmailContent.Replace("txtValidation", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtValidation.Text & "</span>")

        EmailContent = EmailContent.Replace("txtIpAddress", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtIpAddress.Text & "</span>")
        EmailContent = EmailContent.Replace("txtFraudRating", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtFraudRating.Text & "</span>")
        EmailContent = EmailContent.Replace("txtBillZipCode", "<span style='display:inline-block;border-color:#939393;border-width:1px;border-style:solid;font-family:Tahoma;font-size:9pt;'>" & txtBillZipCode.Text & "</span>")


        ''Mail order items details
        Dim n As Integer
        Dim StrBody As New StringBuilder()
        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='775' id='table1'>")
        Dim MNUID As String = "3"
        StrBody.Append("<tr align='center'>")
        StrBody.Append("<td width='112'><font face='Verdana'><font size='1'><b>ItemID</b>")
        StrBody.Append("</td>")
        StrBody.Append("<td width='175'><font face='Verdana'><font size='1'><b>Item Name</b>")
        StrBody.Append("</td>")
        StrBody.Append("<td width='187'><font face='Verdana'><font size='1'><b>Item Description</b>")
        StrBody.Append("</td>")
        StrBody.Append("<td width='67'><b><font face='Verdana' size='1'>Unit Price</font></b></td>")
        StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> Order Quantity</b>")
        StrBody.Append("</td>")
        StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> Discount Type </b>")
        StrBody.Append("</td>")
        StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> Item Discount</b>")
        StrBody.Append("</td>")
        StrBody.Append("<td width='88'><font size='1' face='Verdana'><b> Total</b></td>")
        StrBody.Append("</tr>")

        For n = 0 To OrderDetailGrid.Rows.Count - 1
            Try
                Dim txtDescription As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblItemDescription"), Label)
                Dim drpQty As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblQty"), Label)
                Dim txtItemID As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblItemID"), Label)
                Dim ItemUnitPrice As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblPrice"), Label)
                Dim ItemDiscount As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblDiscountPerc"), Label)
                Dim drpFlatOrPercent As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblFlatPerc"), Label)
                Dim lblSubTotal As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblSubTotal"), Label)
                Dim lblItemName As Label = CType(OrderDetailGrid.Rows(n).FindControl("lblItemName"), Label)
                'lblItemName
                StrBody.Append("<tr align='center'>")
                StrBody.Append("<td width='112'><font face='Verdana'><font size='1'><b>" & txtItemID.Text & "</b>")
                StrBody.Append("</td>")
                StrBody.Append("<td width='175'><font face='Verdana'><font size='1'><b> " & lblItemName.Text & " </b>")
                StrBody.Append("</td>")
                StrBody.Append("<td width='187'><font face='Verdana'><font size='1'><b>" & txtDescription.Text & "</b>")
                StrBody.Append("</td>")
                StrBody.Append("<td width='67'><b><font face='Verdana' size='1'>" & ItemUnitPrice.Text & "</font></b></td>")
                StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> " & drpQty.Text & "</b>")
                StrBody.Append("</td>")
                StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> " & drpFlatOrPercent.Text & "</b>")
                StrBody.Append("</td>")
                StrBody.Append("<td width='50'><font size='1' face='Verdana'><b>" & ItemDiscount.Text & "</b>")
                StrBody.Append("</td>")
                StrBody.Append("<td width='88'><font size='1' face='Verdana'><b> " & lblSubTotal.Text & "</b></td>")
                StrBody.Append("</tr>")

            Catch ex As Exception

            End Try


        Next
        StrBody.Append("</table>")
        ''
        EmailContent = EmailContent.Replace("Order_Detail", StrBody.ToString)

        Dim PaymentType As String
        PaymentType = drpPaymentType.SelectedValue
        PaymentType = PaymentType.ToLower()

        Dim PaymentTypeDetails As String = ""

Dim crd As String
                    crd = txtCard.Text.Trim

                    'Last number display X in Credit cards
                    Dim cardNo As String = ""
                    Dim cLen As Integer = 0
                    Dim subLen As Integer = 0
                    Dim SubcardNo As String = ""
                    cardNo = crd
                    cLen = cardNo.Length()
                    Dim slen As Integer = 0
                    If cLen > 0 Then
                        If cLen > 12 Then
                            subLen = cLen - 12
                            'SubcardNo = cardNo.Substring(0, subLen)
                            SubcardNo = cardNo.Substring(12, subLen)
                            slen = SubcardNo.Length()

                            If slen > 4 Then
                                SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                            End If
                            'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                            cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                        Else
                            cardNo = RepeatChar("X", cLen)
                        End If
                    End If


        '<div style='padding:5px; font-size:12px;'>
        '</div>
        Select Case PaymentType
            Case "credit card"
                'pnlCheck.Visible = False
                'pnlCreditCard.Visible = True
                'pnlCustomerProfile.Visible = False
                'pnlWireService.Visible = False
                'lblTitle.Text = "Credit Card Payment Information"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Card Type = " & drpCardType.SelectedValue
                PaymentTypeDetails = PaymentTypeDetails & "<br> Card#  = " &  cardNo 'txtCard.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> EXP  = " & "XX/XXXX" ' drpExpirationDate.SelectedValue
                PaymentTypeDetails = PaymentTypeDetails & "<br> CSV  = " & "XXX" 'txtCSV.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Approval  = " & txtApproval.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Validation  = " & txtValidation.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & txtIpAddress.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Fraud Rating  = " & txtFraudRating.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Billing Zip Code  = " & ""
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"

            Case "cash"
                'pnlCheck.Visible = True
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = False
                'pnlWireService.Visible = False
                'lblTitle.Text = "Payment Information"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Check #  = " & txtCheck.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> ID   = " & txtID.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Coupon  = " & txtCoupon.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Gift Certificate = " & txtGiftCertificate.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try

                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"

            Case "check"
                'pnlCheck.Visible = True
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = False
                'pnlWireService.Visible = False
                'lblTitle.Text = "Payment Information"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Check #  = " & txtCheck.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> ID   = " & txtID.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Coupon  = " & txtCoupon.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Gift Certificate = " & txtGiftCertificate.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"

            Case "e check"
                'pnlCheck.Visible = True
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = False
                'pnlWireService.Visible = False
                'lblTitle.Text = "Payment Information"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Check #  = " & txtCheck.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> ID   = " & txtID.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Coupon  = " & txtCoupon.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Gift Certificate = " & txtGiftCertificate.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"

            Case "cod"
                'pnlCheck.Visible = True
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = False
                'pnlWireService.Visible = False
                'lblTitle.Text = "Payment Information"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = "<br> Check #  = " & txtCheck.Text
                PaymentTypeDetails = "<br> ID   = " & txtID.Text
                PaymentTypeDetails = "<br> Coupon  = " & txtCoupon.Text
                PaymentTypeDetails = "<br> Gift Certificate = " & txtGiftCertificate.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"

            Case "wire in"
                'pnlCheck.Visible = False
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = False
                'pnlWireService.Visible = True
                'lblTitle.Text = "Wire In Payment Information"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Wire Service   = " & drpWire.SelectedValue
                PaymentTypeDetails = PaymentTypeDetails & "<br> Code #   = " & txtCode.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Reference ID   = " & txtReference.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Transmit Method   = " & txtTransmitMethod.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Representative    = " & txtrepresentative.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist Name    = " & txtFloristname.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist Phone    = " & txtFloristPhone.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist City    = " & txtFloristcity.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist State    = " & txtFloriststate.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Received Amount    = " & txtReceivedamount.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"

            Case "wire"
                'pnlCheck.Visible = False
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = False
                'pnlWireService.Visible = True
                'lblTitle.Text = "Wire In Payment Information"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Wire Service   = " & drpWire.SelectedValue
                PaymentTypeDetails = PaymentTypeDetails & "<br> Code #   = " & txtCode.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Reference ID   = " & txtReference.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Transmit Method   = " & txtTransmitMethod.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Representative    = " & txtrepresentative.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist Name    = " & txtFloristname.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist Phone    = " & txtFloristPhone.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist City    = " & txtFloristcity.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Florist State    = " & txtFloriststate.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Received Amount    = " & txtReceivedamount.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"
            Case "0"
                'pnlCheck.Visible = False
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = True
                'pnlWireService.Visible = False
                'lblTitle.Text = "Customer Profile"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Account Status    = " & txtAccountStatus.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Credit Limit    = " & txtCreditLimit.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> YTD Orders    = " & txtYtdOrders.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Average Sale    = " & txtAverageSale.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Sales LifeTime    = " & txtSalesLifeTime.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Customer Since    = " & txtCustomerSince.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Member Points    = " & txtMemberPoints.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Credit Comments    = " & txtCreditComments.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Discounts   = " & txtDiscounts.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Customer Rank   = " & txtCustomerRank.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"
            Case Else
                'pnlCheck.Visible = False
                'pnlCreditCard.Visible = False
                'pnlCustomerProfile.Visible = True
                'pnlWireService.Visible = False
                'lblTitle.Text = "Customer Profile"
                PaymentTypeDetails = PaymentTypeDetails & "<div style='padding:5px; font-size:12px;'>"
                PaymentTypeDetails = PaymentTypeDetails & "<br> Account Status    = " & txtAccountStatus.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Credit Limit    = " & txtCreditLimit.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> YTD Orders    = " & txtYtdOrders.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Average Sale    = " & txtAverageSale.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Sales LifeTime    = " & txtSalesLifeTime.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Customer Since    = " & txtCustomerSince.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Member Points    = " & txtMemberPoints.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Credit Comments    = " & txtCreditComments.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Discounts   = " & txtDiscounts.Text
                PaymentTypeDetails = PaymentTypeDetails & "<br> Customer Rank   = " & txtCustomerRank.Text
Try
                    PaymentTypeDetails = PaymentTypeDetails & "<br> IP Address  = " & Request.ServerVariables("REMOTE_ADDR")
                Catch ex As Exception

                End Try
                PaymentTypeDetails = PaymentTypeDetails & "<br></div>"
        End Select


        EmailContent = EmailContent.Replace("PaymentTypeDetails", PaymentTypeDetails)


        Dim pnlWireOutInformation As String = ""

        If drpShipMethod.SelectedValue.ToString.ToLower = "wire out" Or drpShipMethod.SelectedValue.ToString.ToLower = "wire_out" Then

            pnlWireOutInformation = pnlWireOutInformation & "Wire Out Information"
            pnlWireOutInformation = pnlWireOutInformation & "<br>"
            pnlWireOutInformation = pnlWireOutInformation & "<br> Wire Service   = " & drpWireoutService.SelectedValue & "  Wire Service Code #   = " & txtWireoutCode.Text & "  Owner  = " & txtWireoutOwner.Text & " "
            pnlWireOutInformation = pnlWireOutInformation & "<br> Florist Name   = " & txtWireoutFlorist.Text & "  Trans.Method   = " & drpWireoutTransMethod.SelectedValue & "  Wire Notes  = " & txtWireNotes.Text & " "

        End If

        EmailContent = EmailContent.Replace("pnlWireOutInformation", pnlWireOutInformation)

        objp.Mailcontent = EmailContent

        objp.Card = "" 'txtCard.Text
        objp.CSV = "" 'txtCSV.Text
        objp.ExpirationDate = "" 'drpExpirationDate.SelectedValue
        ''  objp.Insert_OrderMailSendForTrace()


        Dim mailtype As String

        mailtype = ""

        If Not Request.QueryString("OrderNumber") Is Nothing Then
            mailtype = "Old Order Edited :"
        Else
            mailtype = "New Order Posted :"
        End If

        Dim subject As String = ""

        'subject = mailtype & " Order Back Up Mail For :" & OrdNumber & " for Company :" & CompanyID
        subject = "POS |  " & txtDeliveryDate.Text & " | " & CompanyID.Trim() & " | " & OrdNumber & " | " & mailtype

        EmailSendingWithoutBcc(subject, EmailContent, "support@quickflora.com", "qfordertracemail@gmail.com")


    End Sub

    Public Sub EmailSendingWithoutBcc(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String)
        ''
        'REMOVE EXIT SUB FROM FUNCTION BEFORE USING IT
        ''
        'Exit Sub
        ''
        'REMOVE EXIT SUB FROM FUNCTION BEFORE USING IT
        ''

        Dim mMailMessage As New MailMessage()

        ' Set the sender address of the mail message
        mMailMessage.From = New MailAddress(FromAddress)
        ' Set the recepient address of the mail message


        mMailMessage.To.Add(New MailAddress(ToAddress))
        mMailMessage.CC.Add(New MailAddress("orders@sunflowertechnologies.com"))

        ' Set the subject of the mail message
        mMailMessage.Subject = OrderPlacedSubject.ToString()

        ' Set the body of the mail message
        mMailMessage.Body = OrderPlacedContent.ToString()

        ' Set the format of the mail message body as HTML
        mMailMessage.IsBodyHtml = True

        ' Set the priority of the mail message to normal
        mMailMessage.Priority = MailPriority.Normal

        ' Instantiate a new instance of SmtpClient
        Dim smtp As New System.Net.Mail.SmtpClient()
        smtp.Host = ConfigurationManager.AppSettings("SystemSMTPServer")

        Try
            'smtp.Send(mMailMessage)
            newmailsending(mMailMessage)
        Catch ex As Exception

        End Try



    End Sub
    Public Sub newmailsending(ByVal Email As MailMessage)
        'Exit Sub

        Dim lblerrortestmail As New TextBox
        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim obj_InsertOutGoingMailDetails As New clsMailServer
        obj_InsertOutGoingMailDetails.CompanyID = CompanyID
        obj_InsertOutGoingMailDetails.DivisionID = DivisionID
        obj_InsertOutGoingMailDetails.DepartmentID = DepartmentID


        Try

            obj_InsertOutGoingMailDetails.From_Email = Email.From.ToString
            obj_InsertOutGoingMailDetails.To_Email = Email.To.ToString
            obj_InsertOutGoingMailDetails.CC_Email = Email.CC.ToString
            obj_InsertOutGoingMailDetails.Email_Subject = Email.Subject.ToString
            obj_InsertOutGoingMailDetails.Email_Body = Email.Body.ToString


            Dim Host As String = ""
            Dim Port As String = ""

            Dim NetworkCredential_username As String = ""
            Dim NetworkCredential_password As String = ""

            Dim Host2 As String = ""
            Dim Port2 As String = ""

            Dim NetworkCredential_username2 As String = ""
            Dim NetworkCredential_password2 As String = ""


            Dim obj As New clsMailServer
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            Dim dt As New Data.DataTable
            dt = obj.FillDetails

            If dt.Rows.Count <> 0 Then

                Host = dt.Rows(0)("MailServer1")
                Port = dt.Rows(0)("MailServerPort1")
                NetworkCredential_username = dt.Rows(0)("MailServerUserName1")
                NetworkCredential_password = dt.Rows(0)("MailServerPassword1")


                Host2 = dt.Rows(0)("MailServer2")
                Port2 = dt.Rows(0)("MailServerPort2")
                NetworkCredential_username2 = dt.Rows(0)("MailServerUserName2")
                NetworkCredential_password2 = dt.Rows(0)("MailServerPassword2")


                If Host.Trim <> "" Then
                    Dim mailClient As New System.Net.Mail.SmtpClient()

                    'This object stores the authentication values

                    If NetworkCredential_username.Trim <> "" And NetworkCredential_password.Trim <> "" Then
                        Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username.Trim, NetworkCredential_password.Trim)
                        'mailClient.UseDefaultCredentials = False
                        mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                        mailClient.Credentials = basicAuthenticationInfo
                    Else
                        mailClient.UseDefaultCredentials = True
                    End If

                    'Put your own, or your ISPs, mail server name onthis next line


                    mailClient.Host = Host.Trim
                    If Port.Trim <> "" Then
                        mailClient.Port = Port.Trim
                    End If

                    Try
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        lblerrortestmail.Text = "Mail Sent With Primary SMTP Details"

                    Catch ex As SmtpException
                        Dim Email_Subject1 As String = obj_InsertOutGoingMailDetails.Email_Subject

                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''

                        obj_InsertOutGoingMailDetails.Email_Subject = Email_Subject1

                        lblerrortestmail.Text = "Error From PRIMARY SMTP:" & ex.Message
                        If Host2.Trim <> "" Then
                            mailClient = New System.Net.Mail.SmtpClient()
                            'This object stores the authentication values

                            If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                                'mailClient.UseDefaultCredentials = False
                                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                                mailClient.Credentials = basicAuthenticationInfo
                            Else
                                mailClient.UseDefaultCredentials = True
                            End If

                            'Put your own, or your ISPs, mail server name onthis next line


                            mailClient.Host = Host2.Trim
                            If Port2.Trim <> "" Then
                                mailClient.Port = Port2.Trim
                            End If

                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                            mailClient.Send(Email)
                            '''''Email Details storing''''
                            obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                            obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                            obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                            '''''Email Details storing''''
                            lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"
                        End If

                    End Try



                Else
                    If Host2.Trim <> "" Then
                        Dim mailClient As New System.Net.Mail.SmtpClient()
                        'This object stores the authentication values

                        If NetworkCredential_username2.Trim <> "" And NetworkCredential_password2.Trim <> "" Then
                            Dim basicAuthenticationInfo As New System.Net.NetworkCredential(NetworkCredential_username2.Trim, NetworkCredential_password2.Trim)
                            mailClient.UseDefaultCredentials = False
                            mailClient.Credentials = basicAuthenticationInfo
                        Else
                            mailClient.UseDefaultCredentials = True
                        End If

                        'Put your own, or your ISPs, mail server name onthis next line


                        mailClient.Host = Host2.Trim
                        If Port2.Trim <> "" Then
                            mailClient.Port = Port2.Trim
                        End If

                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port

                        mailClient.Send(Email)
                        '''''Email Details storing''''
                        obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = lblerrortestmail.Text & "<br>" & "Mail Sent With Secondry SMTP Details"

                    Else

                        'Dim mailClient As New System.Net.Mail.SmtpClient()
                        ''This object stores the authentication values
                        ''mailClient.UseDefaultCredentials = True
                        ''Put your own, or your ISPs, mail server name onthis next line
                        ''mailClient.Host = "8.3.16.126"
                        ''mailClient.Port = "25"
                        'mailClient.Send(Email)
                        ''''''Email Details storing''''
                        'obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                        'obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                        'obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                        '''''Email Details storing''''
                        lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

                    End If
                End If



            Else

                Dim mailClient As New System.Net.Mail.SmtpClient()
                'This object stores the authentication values
                'mailClient.UseDefaultCredentials = True
                'Put your own, or your ISPs, mail server name onthis next line
                mailClient.Host = "8.3.16.126"
                mailClient.Port = "25"
                mailClient.Send(Email)
                '''''Email Details storing''''
                obj_InsertOutGoingMailDetails.SMTP_SERVER = mailClient.Host
                obj_InsertOutGoingMailDetails.SMTP_PORT = mailClient.Port
                ''  obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
                ''''Email Details storing''''
                lblerrortestmail.Text = "Mail Not Sent With SMTP Details"

            End If

        Catch ex As FormatException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send Format Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            ''  obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("Format Exception: " & ex.Message)

        Catch ex As SmtpException

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send SMTP Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            ''   obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''
            lblerrortestmail.Text = ("SMTP Exception:  " & ex.Message)

        Catch ex As Exception

            '''''Email Details storing''''
            obj_InsertOutGoingMailDetails.Email_Subject = "(Failed To Send General Exception Error: " & ex.Message & "  ) " & obj_InsertOutGoingMailDetails.Email_Subject
            ''   obj_InsertOutGoingMailDetails.InsertOutGoingMailDetails()
            '''''Email Details storing''''

            lblerrortestmail.Text = ("General Exception:  " & ex.Message)

        End Try
    End Sub

#Region "Call Posting Order"


    Public Function CheckAndUpdateCreditCardApprovalNumberAndIpAddress(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdNumber As String) As Boolean

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)

        Dim ApprovalNumber As String = txtApproval.Text
        Dim Address As String = txtIpAddress.Text

        Dim DBApprovalNumber As String = GetCreditCardApprovalNumberAndIpAddress(CompanyID, DepartmentID, DivisionID, OrdNumber)
        '
        If ApprovalNumber.Trim = DBApprovalNumber.Trim Then
            Exit Function
        End If

        Dim qry As String
        qry = "Update OrderHeader set  CreditCardApprovalNumber=@ApprovalNumber,IpAddress=@Address where CompanyID=@CompanyID and DivisionID= @DivisionID and DepartmentID=@DepartmentID  and OrderNumber=@OrderNumber "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
        com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar)).Value = OrdNumber
        com.Parameters.Add(New SqlParameter("@ApprovalNumber", SqlDbType.NVarChar)).Value = ApprovalNumber
        com.Parameters.Add(New SqlParameter("@Address", SqlDbType.NVarChar)).Value = Address

        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()

        Return True

    End Function


    Public Function GetCreditCardApprovalNumberAndIpAddress(ByVal CompanyID As String, ByVal DepartmentID As String, ByVal DivisionID As String, ByVal OrdNumber As String) As String

        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)


        Dim qry As String
        qry = "Select  CreditCardApprovalNumber From OrderHeader where CompanyID=@CompanyID and DivisionID= @DivisionID and DepartmentID=@DepartmentID  and OrderNumber=@OrderNumber "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)

        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 36)).Value = CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 36)).Value = DivisionID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 36)).Value = DepartmentID
        com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar)).Value = OrdNumber

        Dim Da As New SqlDataAdapter(com)
        Dim dt As New DataTable
        Da.Fill(dt)
        If dt.Rows.Count <> 0 Then
            Try
                Return dt.Rows(0)("CreditCardApprovalNumber")
            Catch ex As Exception
                Return ""
            End Try

        Else
            Return ""
        End If

        Return ""

    End Function
#Region "AddCardMessages"
    Sub AddCardMessages(ByVal OrdNumber As String)

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim OrderNumber As String = OrdNumber
        Dim CardMessage As String = txtCardMessageDesc.Text
        If IsNumeric(OrderNumber) Then
        Else
            Exit Sub
        End If
        Dim objOrder As New CustomOrder()
        objOrder.UpdateCardMessagesItemDetails(CompanyID, DivisionID, DepartmentID, OrdNumber, CardMessage)

    End Sub
#End Region

    Protected Sub PostingOrder(ByVal OrdNumber As String)
        If IsNumeric(OrdNumber) Then
        Else
            Exit Sub
        End If

        WmailBeforePostingOrder(OrdNumber)



        Dim PostOrder As New CustomOrder()



        AddCardMessages(OrdNumber)
        Dim RValue As String = ""

        ''RValue = PostOrder.PostingOrder(CompanyID, DepartmentID, DivisionID, OrdNumber)

        If drpPaymentType.SelectedValue <> "Will Call" Then
            RValue = PostOrder.PostingOrder(CompanyID, DepartmentID, DivisionID, OrdNumber)
            'Response.Redirect("POSOE.aspx")
            Dim objAudit_Will_Call As New clsAudit_Will_Call
            objAudit_Will_Call.CompanyID = CompanyID
            objAudit_Will_Call.DepartmentID = DepartmentID
            objAudit_Will_Call.DivisionID = DivisionID
            objAudit_Will_Call.OrderNumber = OrdNumber

            Dim dtAudit_Will_Call As New DataTable
            dtAudit_Will_Call = objAudit_Will_Call.FillDetailsAudit_Will_Call
            If dtAudit_Will_Call.Rows.Count <> 0 Then

                Dim pmid As String
                pmid = dtAudit_Will_Call.Rows(0)("PaymentMethodID")

                If pmid <> drpPaymentType.SelectedValue Then
                    objAudit_Will_Call.PaymentMethodID = drpPaymentType.SelectedValue
                    objAudit_Will_Call.Insert()
                    objAudit_Will_Call.UpdateOrderHeader()
                End If

            End If

        Else
            Dim objAudit_Will_Call As New clsAudit_Will_Call
            objAudit_Will_Call.CompanyID = CompanyID
            objAudit_Will_Call.DepartmentID = DepartmentID
            objAudit_Will_Call.DivisionID = DivisionID
            objAudit_Will_Call.OrderNumber = OrdNumber
            Dim dtAudit_Will_Call As New DataTable
            dtAudit_Will_Call = objAudit_Will_Call.FillDetailsAudit_Will_Call
            If dtAudit_Will_Call.Rows.Count <> 0 Then

                Dim pmid As String
                pmid = dtAudit_Will_Call.Rows(0)("PaymentMethodID")

                If pmid <> "Will Call" Then
                    objAudit_Will_Call.PaymentMethodID = drpPaymentType.SelectedValue
                    objAudit_Will_Call.Insert()
                End If

            Else
                objAudit_Will_Call.PaymentMethodID = drpPaymentType.SelectedValue
                objAudit_Will_Call.Insert()
            End If
        End If


        Dim Service As Decimal = 0.0
        Dim Delivery As Decimal = 0.0
        Dim Relay As Decimal = 0.0

        If txtService.Text.Trim() <> "" Then
            If IsNumeric(txtService.Text.Trim()) Then
                Service = txtService.Text.Trim()
            Else
                Service = 0

            End If

        End If

        If txtDelivery.Text.Trim() <> "" Then
            If IsNumeric(txtDelivery.Text.Trim()) Then
                Delivery = txtDelivery.Text.Trim()
            Else
                Delivery = 0

            End If

        End If
        If txtRelay.Text.Trim() <> "" Then
            If IsNumeric(txtRelay.Text.Trim()) Then
                Relay = txtRelay.Text.Trim()
            Else
                Relay = 0

            End If


        End If


        PostOrder.UpdateGrandTotal(CompanyID, DepartmentID, DivisionID, OrdNumber, drpTaxes.SelectedValue, txtTaxPercent.Text.Replace("%", ""), txtTax.Text, txtTotal.Text, Service, Delivery, Relay)


        ''new data added
        Dim drApprovalNumber As SqlDataReader
        Dim ds As New DataSet
        Dim ApprovalNumber As String = ""
        Dim PaymentMethodID As String = ""
        drApprovalNumber = PostOrder.GetDetailsForCreditCardApproval(CompanyID, DivisionID, DepartmentID, OrdNumber)
        While drApprovalNumber.Read()
            ApprovalNumber = drApprovalNumber("CreditCardApprovalNumber").ToString()
            PaymentMethodID = drApprovalNumber("PaymentMethodID").ToString()
        End While
        Dim CreditCardOffline As Boolean = False
        ds = PostOrder.CheckCreditCardOffline(CompanyID, DepartmentID, DivisionID)
        If ds.Tables(0).Rows.Count > 0 Then
            CreditCardOffline = ds.Tables(0).Rows(0)("CreditCardOffline").ToString()
        End If
        If PaymentMethodID = "Credit Card" Or drpPaymentType.SelectedValue = "Credit Card" Then
            Dim objrderAmountPaid As New clsOrderEntryForm
            objrderAmountPaid.CompanyID = CompanyID
            objrderAmountPaid.DivisionID = DivisionID
            objrderAmountPaid.DepartmentID = DepartmentID
            objrderAmountPaid.OrderNumber = OrdNumber
            objrderAmountPaid.BalanceDue = 0
            Try
                objrderAmountPaid.AmountPaid = CType(txtTotal.Text, Double)
            Catch ex As Exception
                objrderAmountPaid.AmountPaid = 0
            End Try
            objrderAmountPaid.UpdateOrderAmountPaid()
        End If
        ''new data added


        Session("PaymentType") = drpPaymentType.SelectedValue
        Session("OrdNumber") = OrdNumber

        Dim chk As String = ""
        If Not Session("StartPaymentMethodID") Is Nothing Then
            chk = Session("StartPaymentMethodID")
        End If

        ''New code TO check and Approval Number Data on Database and on Page
        If drpPaymentType.SelectedValue = "Credit Card" Then
            If txtApproval.Text.Trim <> "" Then
                CheckAndUpdateCreditCardApprovalNumberAndIpAddress(CompanyID, DepartmentID, DivisionID, OrdNumber)
            End If
        End If
        ''New code TO check and Approval Number Data on Database and on Page

        If RValue = "Order is already posted." Then
            SessionClearData()

            If drpPaymentType.SelectedValue = "Credit Card" Then
                Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & OrdNumber)

            ElseIf drpPaymentType.SelectedValue = "Cash" Then
                Session("CustomerType") = "CASH"

                Response.Redirect("CashEnterAmount.aspx?OrderNumber=" & OrdNumber)

            ElseIf drpPaymentType.SelectedValue = "House Account" Then

                Response.Redirect("HouseAccountenter.aspx?OrderNumber=" & OrdNumber)
            ElseIf Session("PaymentType") = "Check" Then
                Session("CustomerType") = "check"
                Session("CustomerID") = "CASH"

                Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & Session("OrdNumber"))
            Else
                'Response.Redirect("POSOE.aspx?f=1")
                If drpPaymentType.SelectedValue.ToLower = "EMV-Debit".ToLower Then
                    Response.Redirect("CreditCardEnter.aspx?EMV=1&OrderNumber=" & OrdNumber)
                Else
                    If drpPaymentType.SelectedValue = "Will Call" Then
                        Response.Redirect("CreditCardEnter.aspx?EMV=2&OrderNumber=" & OrdNumber)
                    End If

                    If drpPaymentType.SelectedValue = "Gift Card" Then
                        Response.Redirect("CreditCardEnter.aspx?EMV=4&OrderNumber=" & OrdNumber)
                    End If

                    If drpPaymentType.SelectedValue = "Wire In" Then
                        Response.Redirect("CreditCardEnter.aspx?EMV=3&OrderNumber=" & OrdNumber)
                    End If

                    Response.Redirect("POSOE.aspx")
                End If
            End If

        End If


        If RValue <> "" Then
            alertText.Value = RValue
            lblMessage.Visible = False
            lblMessage.Text = RValue


            If txtCreditLimit.Text.Trim = "" And RValue = "Order is moved to On Hold Orders.Reason:Customer has Exceeded Available Credit limit" Then
                Response.Redirect("POSOE.aspx")
            End If

            Dim r As New Random
            HidalertText.Focus()
            ClientScript.RegisterStartupScript(Me.GetType(), "LoginError" & r.Next(100003, 200004).ToString(), _
                          String.Format("alert('{0}');", lblMessage.Text), True)

            Response.Redirect("POSOE.aspx")
            Exit Sub

        End If







        'Added On 08/04/05 	This block inserts data into the POSShiftTransaction Table		
        '-------------------------------------

        '----------------------------

        'Code added on 13th Dec 2005 
        'Newly Declared Variables


        ShiftID = Session("ShiftID")
        TerminalID = Session("TerminalID")
        Dim myCommand As SqlCommand
        Dim reader As SqlDataReader
        Dim cn As New SqlConnection(ConnectionString)

        If drpPaymentType.SelectedValue <> "Will Call" Then
            myCommand = New SqlCommand( _
           " INSERT INTO POSShiftTransaction(CompanyID,DivisionID,DepartmentID,EmployeeID,OrderNumber," & _
           " ShiftID,TerminalID,CustomerID,TransDateTime,OrderAmount,TransType,PaymentMethodID)" & _
           " VALUES(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@OrderNumber," & _
           " @ShiftID,@TerminalID,@CustomerID,getdate(),@OrderAmount,@TransType,@PaymentMethodID)", cn)

            myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
            myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
            myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)
            myCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID)
            myCommand.Parameters.AddWithValue("@TransType", "Order")
            myCommand.Parameters.AddWithValue("@PaymentMethodID", drpPaymentType.SelectedValue)
            myCommand.Parameters.AddWithValue("@OrderNumber", lblOrderNumberData.Text)
            myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
            myCommand.Parameters.AddWithValue("@TerminalID", TerminalID)
            myCommand.Parameters.AddWithValue("@CustomerID", txtCustomerTemp.Text)
            myCommand.Parameters.AddWithValue("@OrderAmount", txtTotal.Text)
            cn.Open()
            myCommand.ExecuteNonQuery()
            cn.Close()
        End If

        'To Update the CurrentBalance in POSShiftMaster	

        Dim CurrentBalance As Double = 0.0
        myCommand = New SqlCommand( _
            "SELECT CurrentBalance FROM POSShiftMaster WHERE ShiftID=@ShiftID", cn)
        myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
        cn.Open()
        If IsDBNull(myCommand.ExecuteScalar) Then
            CurrentBalance = 0.0
        Else
            CurrentBalance = myCommand.ExecuteScalar
        End If
        cn.Close()

        If drpPaymentType.SelectedValue.ToLower = "cash" Or drpPaymentType.SelectedValue.ToLower = "check" Then

            CurrentBalance = CurrentBalance + txtTotal.Text

            myCommand = New SqlCommand( _
            "UPDATE POSShiftMaster SET CurrentBalance = @CurrentBalance WHERE ShiftID=@ShiftID ", cn)
            myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
            myCommand.Parameters.AddWithValue("@CurrentBalance", CurrentBalance)
            cn.Open()
            myCommand.ExecuteNonQuery()
            cn.Close()
        End If



        ''Code For trace all entry 
        If drpPaymentType.SelectedValue <> "Will Call" Then
            myCommand = New SqlCommand( _
            " INSERT INTO POSTransactionTrace(CompanyID,DivisionID,DepartmentID,EmployeeID,OrderNumber," & _
            " ShiftID,TerminalID,CustomerID,TransDateTime,OrderAmount,TransType,PaymentMethodID)" & _
            " VALUES(@CompanyID,@DivisionID,@DepartmentID,@EmployeeID,@OrderNumber," & _
            " @ShiftID,@TerminalID,@CustomerID,getdate(),@OrderAmount,@TransType,@PaymentMethodID)", cn)

            myCommand.Parameters.AddWithValue("@CompanyID", CompanyID)
            myCommand.Parameters.AddWithValue("@DivisionID", DivisionID)
            myCommand.Parameters.AddWithValue("@DepartmentID", DepartmentID)
            myCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID)
            myCommand.Parameters.AddWithValue("@TransType", "Order")
            myCommand.Parameters.AddWithValue("@PaymentMethodID", drpPaymentType.SelectedValue)
            myCommand.Parameters.AddWithValue("@OrderNumber", lblOrderNumberData.Text)
            myCommand.Parameters.AddWithValue("@ShiftID", ShiftID)
            myCommand.Parameters.AddWithValue("@TerminalID", TerminalID)
            myCommand.Parameters.AddWithValue("@CustomerID", txtCustomerTemp.Text)
            myCommand.Parameters.AddWithValue("@OrderAmount", txtTotal.Text)
            cn.Open()
            myCommand.ExecuteNonQuery()
            cn.Close()
        End If
        ''Code For trace all entry 


        If RValue = "" Then


            'code added by imtiyaz
            'Declaring the variable for storing the Printer name for peint and status that decide need to print or not
            Dim dtp As New Data.DataTable
            Dim cardbit, workticketbit As Boolean
            Dim cardbittest, workticketbittest As Boolean
            Dim CardMessagePrinterName, WorkTicketPrinterName As String
            WorkTicketPrinterName = ""
            CardMessagePrinterName = ""

            cardbit = False
            workticketbit = False

            Dim objp As New clsOrderfrmPrintingConfigurations
            objp.CompanyID = CompanyID
            objp.DivisionID = DivisionID
            objp.DepartmentID = DepartmentID
            dtp = objp.FillDetails()

            If dtp.Rows.Count <> 0 Then

                Try
                    txtActiveX.Text = dtp.Rows(0)("ActiveXName")
                Catch ex As Exception
                    txtActiveX.Text = ""
                End Try
                'SunflowerActiveX
                'AdvancedPDFPrinter

                Try
                    txtActiveXVersion.Text = dtp.Rows(0)("ActiveXADPVersion")
                Catch ex As Exception
                    txtActiveXVersion.Text = "3.0"
                End Try

            End If



            If dtp.Rows.Count <> 0 Then
                CardMessagePrinterName = dtp.Rows(0)("CardMessage1PrinterName")
                WorkTicketPrinterName = dtp.Rows(0)("WorkTicket1PrinterName")
                cardbittest = dtp.Rows(0)("CardMessage2bit")
                workticketbittest = dtp.Rows(0)("WorkTicket2bit")
            End If

            Dim dt As New Data.DataTable
            dt = objp.OrderDetailsDateWise(OrdNumber)
            Dim obj2 As New clsOrderfrmPrintingException
            obj2.CompanyID = CompanyID
            obj2.DivisionID = DivisionID
            obj2.DepartmentID = DepartmentID

            If cardbittest = False Then
                cardbit = False
            Else
                'dt.Rows(0)("ShipMethodID") 
                cardbit = False
                Dim dtopt As New DataTable
                obj2.OrderfrmType = "POS"
                obj2.PrintType = "Card"
                obj2.Method = "Delivery"
                Try
                    obj2.Value = dt.Rows(0)("ShipMethodID")
                Catch ex As Exception
                    obj2.Value = ""
                End Try
                dtopt = obj2.FillDetails_Order_Print_Type_value()

                If dtopt.Rows.Count = 0 Then
                    obj2.OrderfrmType = "POS"
                    obj2.PrintType = "Card"
                    obj2.Method = "Payment"
                    'dt.Rows(0)("PaymentMethodID") 
                    Try
                        obj2.Value = dt.Rows(0)("PaymentMethodID")
                    Catch ex As Exception
                        obj2.Value = ""
                    End Try
                    dtopt.Clear()
                    dtopt = obj2.FillDetails_Order_Print_Type_value()
                    If dtopt.Rows.Count = 0 Then
                        cardbit = True
                    End If
                End If


            End If

            If workticketbittest = False Then
                workticketbit = False
            Else
                'dt.Rows(0)("ShipMethodID") 
                workticketbit = False
                Dim dtopt As New DataTable
                obj2.OrderfrmType = "POS"
                obj2.PrintType = "WorkTicket"
                obj2.Method = "Delivery"
                Try
                    obj2.Value = dt.Rows(0)("ShipMethodID")
                Catch ex As Exception
                    obj2.Value = ""
                End Try
                dtopt.Clear()
                dtopt = obj2.FillDetails_Order_Print_Type_value()

                If dtopt.Rows.Count = 0 Then
                    obj2.OrderfrmType = "POS"
                    obj2.PrintType = "WorkTicket"
                    obj2.Method = "Payment"
                    'dt.Rows(0)("PaymentMethodID") 
                    Try
                        obj2.Value = dt.Rows(0)("PaymentMethodID")
                    Catch ex As Exception
                        obj2.Value = ""
                    End Try
                    dtopt.Clear()
                    dtopt = obj2.FillDetails_Order_Print_Type_value()
                    If dtopt.Rows.Count = 0 Then
                        workticketbit = True
                    End If
                End If

            End If



            'If drpPaymentType.SelectedValue <> "Will Call" And drpPaymentType.SelectedValue <> "Wire In" Then
            '    cardbit = False
            '    workticketbit = False
            'End If

            cardbit = False
            workticketbit = False

            EmailNotifications(OrdNumber)

            If dt.Rows.Count <> 0 Then
                'dt.Rows(0)("ShipMethodID") 

                If cardbit And workticketbit Then
                    Dim pdffilename As String
                    'Dim PdfPath As String = ConfigurationManager.AppSettings("DocPath")
                    pdffilename = CompanyID & Date.Now.Day & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".pdf"
                    Dim url As String
                    url = "OrderNumber=" & OrdNumber & "&pdffilename=" & pdffilename & "&print=ALL"

                    EnclosurecardPDF(pdffilename, OrdNumber)
                    WorkRoomTicketPDF(pdffilename, OrdNumber)
                    Page_LoadCard_Print(pdffilename)
                    Page_LoadWT_Print(pdffilename)
                    txtprint.Text = "ALL"

                    ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("printstart('{0}','{1}',{2});", url, CardMessagePrinterName, 2), True)
                Else

                    If cardbit Then
                        Dim pdffilename As String
                        'Dim PdfPath As String = ConfigurationManager.AppSettings("DocPath")
                        pdffilename = CompanyID & Date.Now.Day & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".pdf"

                        Dim url As String
                        url = "OrderNumber=" & OrdNumber & "&pdffilename=" & pdffilename & "&print=MC"
                        EnclosurecardPDF(pdffilename, OrdNumber)
                        'WorkRoomTicketPDF(pdffilename, OrdNumber)
                        Page_LoadCard_Print(pdffilename)
                        'Page_LoadWT_Print(pdffilename)
                        txtprint.Text = "MC"
                        ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("printstart('{0}','{1}',{2});", url, CardMessagePrinterName, 1), True)
                    End If

                    If workticketbit Then
                        Dim pdffilename As String
                        'Dim PdfPath As String = ConfigurationManager.AppSettings("DocPath")
                        pdffilename = CompanyID & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".pdf"

                        Dim url As String
                        url = "OrderNumber=" & OrdNumber & "&pdffilename=" & pdffilename & "&print=WT"

                        'EnclosurecardPDF(pdffilename, OrdNumber)
                        WorkRoomTicketPDF(pdffilename, OrdNumber)
                        'Page_LoadCard_Print(pdffilename)
                        Page_LoadWT_Print(pdffilename)
                        txtprint.Text = "WT"
                        ClientScript.RegisterStartupScript(Me.GetType(), "Startup", String.Format("printstart('{0}','{1}',{2});", url, WorkTicketPrinterName, 0), True)
                    End If

                End If

                If Not cardbit And Not workticketbit Then
                    SessionClearData()
                    If drpPaymentType.SelectedValue = "Credit Card" Then
                        Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & OrdNumber)

                    ElseIf drpPaymentType.SelectedValue = "Cash" Then
                        Session("CustomerType") = "CASH"

                        Response.Redirect("CashEnterAmount.aspx?OrderNumber=" & OrdNumber)

                    ElseIf drpPaymentType.SelectedValue = "House Account" Then

                        Response.Redirect("HouseAccountenter.aspx?OrderNumber=" & OrdNumber)
                    ElseIf Session("PaymentType") = "Check" Then
                        Session("CustomerType") = "check"
                        Session("CustomerID") = "CASH"

                        Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & Session("OrdNumber"))
                    Else
                        If drpPaymentType.SelectedValue.ToLower = "EMV-Debit".ToLower Then
                            Response.Redirect("CreditCardEnter.aspx?EMV=1&OrderNumber=" & OrdNumber)
                        Else
                            If drpPaymentType.SelectedValue = "Will Call" Then
                                Response.Redirect("CreditCardEnter.aspx?EMV=2&OrderNumber=" & OrdNumber)
                            End If

                            If drpPaymentType.SelectedValue = "Gift Card" Then
                                Response.Redirect("CreditCardEnter.aspx?EMV=4&OrderNumber=" & OrdNumber)
                            End If

                            If drpPaymentType.SelectedValue = "Wire In" Then
                                Response.Redirect("CreditCardEnter.aspx?EMV=3&OrderNumber=" & OrdNumber)
                            End If

                            Response.Redirect("POSOE.aspx")
                        End If
                    End If
                End If


            Else
                SessionClearData()
                If drpPaymentType.SelectedValue = "Credit Card" Then
                    Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & OrdNumber)

                ElseIf drpPaymentType.SelectedValue = "Cash" Then
                    Session("CustomerType") = "CASH"

                    Response.Redirect("CashEnterAmount.aspx?OrderNumber=" & OrdNumber)

                ElseIf drpPaymentType.SelectedValue = "House Account" Then

                    Response.Redirect("HouseAccountenter.aspx?OrderNumber=" & OrdNumber)
                ElseIf Session("PaymentType") = "Check" Then
                    Session("CustomerType") = "check"
                    Session("CustomerID") = "CASH"

                    Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & Session("OrdNumber"))
                Else
                    Response.Redirect("POSOE.aspx?f=3")

                End If
            End If

            ''''  
        Else
            SessionClearData()
            If drpPaymentType.SelectedValue = "Credit Card" Then
                Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & OrdNumber)

            ElseIf drpPaymentType.SelectedValue = "Cash" Then
                Session("CustomerType") = "CASH"

                Response.Redirect("CashEnterAmount.aspx?OrderNumber=" & OrdNumber)

            ElseIf drpPaymentType.SelectedValue = "House Account" Then

                Response.Redirect("HouseAccountenter.aspx?OrderNumber=" & OrdNumber)
            ElseIf Session("PaymentType") = "Check" Then
                Session("CustomerType") = "check"
                Session("CustomerID") = "CASH"

                Response.Redirect("CreditCardEnter.aspx?OrderNumber=" & Session("OrdNumber"))
            Else
                Response.Redirect("POSOE.aspx?f=4")
            End If

        End If


        SessionClearData()


    End Sub

    Function RepeatChar(ByVal c As Char, ByVal len As Integer) As String
        Dim retValue As String = ""
        Dim i As Integer
        If len > 0 Then
            For i = 1 To len
                If (i = 5) Then
                    retValue = retValue + "-"
                    retValue = retValue + c
                Else
                    retValue = retValue + c
                End If
            Next i
        End If
        Return retValue
    End Function

    Public Sub EnclosurecardPDF(ByVal pdffilename As String, ByVal OrderNumber As String)
        Dim rptDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'OrderNumber = Request.QueryString("OrderNumber")
        Dim ImgPath As String = ConfigurationManager.AppSettings("DocPath")
        If OrderNumber = "" Then
            OrderNumber = "*"
        End If
        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")
        'CompanyID = Session("CompanyID")
        'DivisionID = Session("DivisionID")
        'DepartmentID = Session("DepartmentID")
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim CommandText As String = "enterprise.RptDocOrderHeaderSingleSection"
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()
        myCommand.CommandType = CommandType.StoredProcedure
        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = OrderNumber
        Dim EnclosureCardDataSet As New DataSet
        Dim ImageName As String = ""



        Dim daAdapter As New SqlDataAdapter()
        daAdapter.SelectCommand = myCommand
        daAdapter.Fill(EnclosureCardDataSet, "EnclosurecardDetails")


        ''''new code '' Start here
        Dim details As String = ""
        details = details & ""
        Dim nchk As Boolean = True

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAttention").ToString.Trim <> "" Then
                nchk = False
                details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAttention").ToString.Trim
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCompany").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCompany").ToString.Trim
                Else
                    details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCompany").ToString.Trim
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress1").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress1").ToString.Trim
                Else
                    details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress1").ToString.Trim
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress2").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress2").ToString.Trim
                Else
                    details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress2").ToString.Trim
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress3").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress3").ToString.Trim
                Else
                    details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAddress3").ToString.Trim
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCity").ToString.Trim <> "" Then
                If nchk = False Then
                    details = details & vbCrLf & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCity").ToString.Trim & " "
                Else
                    details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCity").ToString.Trim & " "
                End If
                nchk = False
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingState").ToString.Trim <> "" Then
                details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingState").ToString.Trim & " "
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingZip").ToString.Trim <> "" Then
                details = details & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingZip").ToString.Trim
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingPhone").ToString.Trim <> "" Then
                details = details & vbCrLf & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingPhone").ToString.Trim & " "
            End If
        Catch ex As Exception




        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingExt").ToString.Trim <> "" Then
                details = details & "-" & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingExt").ToString.Trim & " "
            End If
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCell").ToString.Trim <> "" Then
                details = details & vbCrLf & EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingCell").ToString.Trim & " "
            End If
        Catch ex As Exception

        End Try



        Try
            EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("ShippingAttention") = details
        Catch ex As Exception

        End Try

        Try
            If EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("Priority").ToString.Trim = "0" Then
                EnclosureCardDataSet.Tables("EnclosurecardDetails").Rows(0)("Priority") = ""
            End If
        Catch ex As Exception

        End Try
        ''''new code '' end here


        Dim CommandText2 As String = "enterprise.spCompanyInformation"
        Dim myCommand2 As New SqlCommand(CommandText2, myConnection)



        myCommand2.CommandType = CommandType.StoredProcedure


        myCommand2.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand2.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand2.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID

        Dim daAdapter2 As New SqlDataAdapter()
        daAdapter2.SelectCommand = myCommand2
        daAdapter2.Fill(EnclosureCardDataSet, "CompanyDetails")
        ImageName = EnclosureCardDataSet.Tables("CompanyDetails").Rows(0)("CompanyLogoUrl").ToString()

        ImgPath = ImgPath & ImageName.ToString


        ''
        If System.IO.File.Exists(ImgPath) Then
            ImgPath = ImgPath
        Else
            Dim IMagePath As String = ConfigurationManager.AppSettings("DocPath")
            ImgPath = IMagePath & "No_Logo.jpg"
        End If
        ''


        Dim fs As System.IO.FileStream = New System.IO.FileStream(ImgPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)

        Dim Image() As Byte = New Byte(fs.Length - 1) {}


        ' fs.Read(Image, 0, Image.Length)

        fs.Read(Image, 0, CType(fs.Length, Integer))

        fs.Close()


        Dim Images As System.Data.DataTable = New DataTable()
        Images.TableName = "Images"
        'Images.Columns.Add(New DataColumn("imagedata", Image.GetType()))

        'Dim objRow() As Object = New Object(1) {}
        'objRow(0) = Image

        Dim objDataColumn As DataColumn = New DataColumn("imagedata", Image.GetType())
        Images.Columns.Add(objDataColumn)
        Dim row As DataRow
        row = Images.NewRow()
        row(0) = Image


        Images.Rows.Add(row)

        EnclosureCardDataSet.Tables.Add(Images)

        Dim itemslist As String = ""
        Dim dtitems As New DataTable
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID
        'OrderNumber
        dtitems = BindItems(CompanyID, DivisionID, DepartmentID, OrderNumber)

        If dtitems.Rows.Count <> 0 Then
            Dim n As Integer

            For n = 0 To dtitems.Rows.Count - 1
                itemslist = itemslist & n + 1 & "." & dtitems.Rows(n)("ItemName") & vbCrLf
            Next

        End If



        EnclosureCardDataSet.Tables("CompanyDetails").Rows(0)("CompanyLogoUrl") = itemslist

        Dim obj As New clsOrderfrmPrintingConfigurations
        'Dim obj3 As New ClsOrderfrmPrintingProfile
        obj.CompanyID = CompanyID
        obj.DivisionID = DivisionID
        obj.DepartmentID = DepartmentID

        Dim dtcardprint As SqlDataReader
        dtcardprint = obj.SelectCardPrinttype(CompanyID, DivisionID, DepartmentID)
        Dim st As String
        st = "WBGWT"
        If (dtcardprint.Read()) Then
            Dim cardtype As String
            Try
                cardtype = dtcardprint(0).ToString().Trim()
            Catch ex As Exception
                cardtype = "WBGWT"
            End Try

            st = cardtype
        Else
            st = "WBGWT"
        End If


        Dim chkrpt As Boolean = True

        If st = "WBGWT" Then
            rptDoc.Load(Server.MapPath("WbgWtNewEnclosureCardCrystalReport.rpt"))
            chkrpt = False
        End If

        If st = "WBGNOT" Then
            rptDoc.Load(Server.MapPath("WBgNotNewEnclosureCardCrystalReport.rpt"))
            chkrpt = False
        End If

        If st = "NOBGWT" Then
            rptDoc.Load(Server.MapPath("NoBgWtNewEnclosureCardCrystalReport.rpt"))
            chkrpt = False
        End If

        If st = "NOBGNOT" Then
            rptDoc.Load(Server.MapPath("NoBgNotNewEnclosureCardCrystalReport.rpt"))
            chkrpt = False
        End If

        If chkrpt = True Then
            rptDoc.Load(Server.MapPath("WbgWtNewEnclosureCardCrystalReport.rpt"))
        End If


        'rptDoc.Load(Server.MapPath("EnclosureCardCrystalReport.rpt"))
        CrystalReportViewer1.Visible = False

        CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf
        rptDoc.SetDataSource(EnclosureCardDataSet)
        CrystalReportViewer1.ReportSource = rptDoc

        Try
            If pdffilename <> "" Then
                Dim PdfPath As String = ConfigurationManager.AppSettings("DocPath")
                pdffilename = "Card_" & pdffilename ' CompanyID & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".pdf"
                pdffilename = pdffilename.Replace(" ", "_")
                rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, PdfPath & pdffilename)
                'Response.Redirect("Print_status.aspx?OrderNumber=" & OrderNumber & "&pdffilename=" & Request.QueryString("pdffilename") & "&print=" & Request.QueryString("print"))
            End If
        Catch ex As Exception

        Finally
            rptDoc.Dispose()
        End Try




    End Sub

    Public Function BindItems(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String) As DataTable
        Dim ConnectionString As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As New SqlConnection(ConnectionString)
        Dim objDA As New SqlDataAdapter("SELECT     dbo.InventoryItems.ItemID, dbo.InventoryItems.ItemName   FROM         dbo.InventoryItems INNER JOIN            dbo.OrderDetail ON dbo.InventoryItems.CompanyID = dbo.OrderDetail.CompanyID AND dbo.InventoryItems.DivisionID = dbo.OrderDetail.DivisionID AND                          dbo.InventoryItems.DepartmentID = dbo.OrderDetail.DepartmentID AND dbo.InventoryItems.ItemID = dbo.OrderDetail.ItemID where dbo.OrderDetail.CompanyID ='" & CompanyID & "' AND dbo.OrderDetail.DivisionID = '" & DivisionID & "' AND dbo.OrderDetail.DepartmentID= '" & DepartmentID & "' AND dbo.OrderDetail.OrderNumber= '" & OrderNumber & "'", objConn)
        Dim objDS As New DataTable()
        Try
            objDA.Fill(objDS)
        Finally

        End Try
        Return objDS
    End Function


    Public Sub WorkRoomTicketPDF(ByVal pdffilename As String, ByVal OrderNumber As String)

        Dim rptDoc As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim objUser As New DAL.CustomOrder()

        ' OrderNumber = Request.QueryString("OrderNumber")
        Dim ImgPath As String = ConfigurationManager.AppSettings("DocPath")


        If OrderNumber = "" Then
            OrderNumber = "*"
        End If


        ConnectionString = EnterpriseCommon.Configuration.Config.GetSetting("ConnectionString")

        Dim myConnection As New SqlConnection(ConnectionString)

        Dim CommandText As String = "enterprise.RptWorkTicketReport"
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = OrderNumber

        Dim WorkTicketDS As New DataSet

        Dim ImageName As String = ""



        Dim daAdapter As New SqlDataAdapter()
        daAdapter.SelectCommand = myCommand
        daAdapter.Fill(WorkTicketDS, "RptWorkRoomTicket")
        '    Dim AddOnItems As String = WorkTicketDS.Tables("OrderDetailsGrid").Rows(0)("AddOnsIDsQty").ToString()


        If WorkTicketDS.Tables("RptWorkRoomTicket").Rows.Count > 0 Then


            ''NEW CODE ADDED
            Dim slutation As String = ""
            Dim lname As String = ""
            Dim fname As String = ""
            Try
                slutation = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("ShippingSalutation").ToString()
            Catch ex As Exception
            End Try
            Try
                fname = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("ShippingFirstName").ToString()
            Catch ex As Exception
            End Try
            Try
                lname = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("ShippingLastName").ToString()
            Catch ex As Exception
            End Try
            WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("ShippingFirstName") = slutation & " " & fname & " " & lname

            Dim Clutation As String = ""
            Dim Clname As String = ""
            Dim Cfname As String = ""
            Try
                Clutation = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerSalutation").ToString()
            Catch ex As Exception
            End Try
            Try
                Cfname = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerFirstName").ToString()
            Catch ex As Exception
            End Try
            Try
                Clname = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerLastName").ToString()
            Catch ex As Exception
            End Try
            WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("DepartmentID") = Clutation & " " & Cfname & " " & Clname

            If WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerID").ToString() = "Retail Customer" Then

                Dim obj As New clsCustomerInformation_RetailCustomer
                obj.CompanyID = CompanyID
                obj.DivisionID = DivisionID
                obj.DepartmentID = DepartmentID
                obj.OrderNumber = OrderNumber
                Dim dt As New DataTable
                dt = obj.SelectCustomerInformationDetail

                If dt.Rows.Count <> 0 Then
                    'cmbSalutation.Text = dt.Rows(0)("CustomerSalutation").ToString()
                    'txtCustomerFirstName.Text = dt.Rows(0)("CustomerFirstName").ToString()
                    'txtCustomerLastName.Text = dt.Rows(0)("CustomerLastName").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("DepartmentID") = dt.Rows(0)("CustomerSalutation").ToString() & " " & dt.Rows(0)("CustomerFirstName").ToString() & " " & dt.Rows(0)("CustomerLastName").ToString()
                    'txtAttention.Text = dt.Rows(0)("Attention").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("Attention") = dt.Rows(0)("Attention").ToString()
                    'txtCustomerAddress1.Text = dt.Rows(0)("CustomerAddress1").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerAddress1") = dt.Rows(0)("CustomerAddress1").ToString()
                    'txtCustomerAddress2.Text = dt.Rows(0)("CustomerAddress2").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerAddress2") = dt.Rows(0)("CustomerAddress2").ToString()
                    'txtCustomerAddress3.Text = dt.Rows(0)("CustomerAddress3").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerAddress3") = dt.Rows(0)("CustomerAddress3").ToString()
                    'txtCustomerCity.Text = dt.Rows(0)("CustomerCity").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerCity") = dt.Rows(0)("CustomerCity").ToString()
                    'drpState.Text =  dt.Rows(0)("CustomerState").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("Attention") = dt.Rows(0)("Attention").ToString()
                    'cmbCountry.Text =  dt.Rows(0)("CustomerCountry").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerCountry") = dt.Rows(0)("CustomerCountry").ToString()
                    'txtCustomerFax.Text = dt.Rows(0)("CustomerFax").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerFax") = dt.Rows(0)("CustomerFax").ToString()
                    'txtCustomerPhone.Text = dt.Rows(0)("CustomerPhone").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerPhone") = dt.Rows(0)("CustomerPhone").ToString()
                    'txtCustomerEmail.Text = dt.Rows(0)("CustomerEmail").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerEmail") = dt.Rows(0)("CustomerEmail").ToString()
                    'txtCustomerZip.Text = dt.Rows(0)("CustomerZip").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerZip") = dt.Rows(0)("CustomerZip").ToString()
                    'txtCustomerCell.Text = dt.Rows(0)("CustomerCell").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerCell") = dt.Rows(0)("CustomerCell").ToString()
                    'txtExt.Text = dt.Rows(0)("CustomerPhoneExt").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerPhoneExt") = dt.Rows(0)("CustomerPhoneExt").ToString()
                    'txtCompany.Text = dt.Rows(0)("CustomerCompany").ToString()
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerCompany") = dt.Rows(0)("CustomerCompany").ToString()
                End If
            End If

            ''
            Dim objCustomerCreditCards As New clsCustomerCreditCards
            objCustomerCreditCards.CompanyID = CompanyID
            objCustomerCreditCards.DivisionID = DivisionID
            objCustomerCreditCards.DepartmentID = DepartmentID
            objCustomerCreditCards.OrderNumber = OrderNumber
            Dim dtCustomerCreditCards As New DataTable
            dtCustomerCreditCards = objCustomerCreditCards.CheckMultiCreditCardBillingDetails
            If dtCustomerCreditCards.Rows.Count <> 0 Then

                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("DepartmentID") = dtCustomerCreditCards.Rows(0)("BillingCustomerName").ToString()
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerAddress1") = dtCustomerCreditCards.Rows(0)("BillingCustomerAddress1").ToString()
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerAddress2") = dtCustomerCreditCards.Rows(0)("BillingCustomerAddress2").ToString()
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerAddress3") = dtCustomerCreditCards.Rows(0)("BillingCustomerAddress3").ToString()
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerCity") = dtCustomerCreditCards.Rows(0)("BillingCustomerCity").ToString()
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerZip") = dtCustomerCreditCards.Rows(0)("BillingCustomerZip").ToString()
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerCountry") = dtCustomerCreditCards.Rows(0)("BillingCustomerCountry").ToString()
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerState") = dtCustomerCreditCards.Rows(0)("BillingCustomerState").ToString()

            End If

            ''NEW CODE ADDED
            Try
                If WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("SourceCodeID").ToString.Trim = "0" Then
                    WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("SourceCodeID") = ""
                End If
            Catch ex As Exception

            End Try


            'Decrypting Credit Card Number
            Dim CrdNum As String = ""
            CrdNum = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardNumber").ToString()
            If CrdNum <> "" Then
                Dim DeCryptValue As New Encryption
                'CrdNum = DeCryptValue.DecryptData(WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerID").ToString(), CrdNum)
                CrdNum = DeCryptValue.TripleDESDecode(CrdNum, WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CustomerID").ToString())
            End If

            'Last number display X in Credit cards
            Dim cardNo As String = ""
            Dim cLen As Integer = 0
            Dim subLen As Integer = 0
            Dim SubcardNo As String = ""

            cardNo = CrdNum
            cLen = cardNo.Length()
            Dim slen As Integer = 0
            If cLen > 0 Then
                If cLen > 12 Then
                    subLen = cLen - 12
                    'SubcardNo = cardNo.Substring(0, subLen)
                    SubcardNo = cardNo.Substring(12, subLen)
                    slen = SubcardNo.Length()

                    If slen > 4 Then
                        SubcardNo = SubcardNo.Substring(0, slen - 4) & "-" & SubcardNo.Substring((slen - 4))
                    End If
                    'cardNo = SubcardNo & "-" & RepeatChar("X", 8)
                    cardNo = RepeatChar("X", 8) & "-" & RepeatChar("X", 4) & "-" & SubcardNo
                Else
                    cardNo = RepeatChar("X", cLen)
                End If
            End If
            WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardNumber") = cardNo

            Dim CreditCardExpDate As DateTime
            If WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardExpDate").ToString() <> "" Then

                CreditCardExpDate = Convert.ToDateTime(WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardExpDate").ToString())
                WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("CreditCardExpDate") = CreditCardExpDate.ToString("MM/dd/yyyy")
            End If


        End If
        ' get the connection ready

        Dim CommandText1 As String = "enterprise.RptDocOrderDetailSingle"
        Dim myCommand1 As New SqlCommand(CommandText1, myConnection)



        myCommand1.CommandType = CommandType.StoredProcedure


        myCommand1.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand1.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand1.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand1.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = OrderNumber
        Dim daAdapter1 As New SqlDataAdapter()
        daAdapter1.SelectCommand = myCommand1
        daAdapter1.Fill(WorkTicketDS, "OrderDetailsGrid")



        Dim CommandText2 As String = "enterprise.spCompanyInformation"
        Dim myCommand2 As New SqlCommand(CommandText2, myConnection)



        myCommand2.CommandType = CommandType.StoredProcedure


        myCommand2.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand2.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand2.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID

        Dim daAdapter2 As New SqlDataAdapter()
        daAdapter2.SelectCommand = myCommand2
        daAdapter2.Fill(WorkTicketDS, "CompanyDetails")

        ''new code added
        Dim CommandText3 As String = "select * from OrderWireServiceDetails where CompanyID=@CompanyID and DivisionID=@DivisionID and DepartmentID=@DepartmentID and OrderNumber=@OrderNumber"
        Dim myCommand3 As New SqlCommand(CommandText3, myConnection)
        myCommand3.CommandType = CommandType.Text
        myCommand3.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand3.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand3.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand3.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = OrderNumber
        Dim daAdapter3 As New SqlDataAdapter()
        daAdapter3.SelectCommand = myCommand3
        daAdapter3.Fill(WorkTicketDS, "OrderWireServiceDetails")
        ''new code added

        ImageName = WorkTicketDS.Tables("CompanyDetails").Rows(0)("CompanyLogoUrl").ToString()

        ImgPath = ImgPath & ImageName.ToString


        ''
        If System.IO.File.Exists(ImgPath) Then
            ImgPath = ImgPath
        Else
            Dim IMagePath As String = ConfigurationManager.AppSettings("DocPath")
            ImgPath = IMagePath & "No_Logo.jpg"
        End If
        ''

        Dim fs As System.IO.FileStream = New System.IO.FileStream(ImgPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)

        Dim Image() As Byte = New Byte(fs.Length - 1) {}




        fs.Read(Image, 0, CType(fs.Length, Integer))

        fs.Close()


        Dim Images As System.Data.DataTable = New DataTable()
        Images.TableName = "Images"


        Dim objDataColumn As DataColumn = New DataColumn("imagedata", Image.GetType())
        Images.Columns.Add(objDataColumn)
        Dim row As DataRow
        row = Images.NewRow()
        row(0) = Image


        Images.Rows.Add(row)

        WorkTicketDS.Tables.Add(Images)
        Dim PaymentMethod As String = ""

        If WorkTicketDS.Tables("RptWorkRoomTicket").Rows.Count > 0 Then
            PaymentMethod = WorkTicketDS.Tables("RptWorkRoomTicket").Rows(0)("PaymentMethodID").ToString()


        End If

        If PaymentMethod = "Wire In" Then

            rptDoc.Load(Server.MapPath("WireOutWorkTicketCrystalReport.rpt"))
        ElseIf PaymentMethod = "COD" Then
            rptDoc.Load(Server.MapPath("PaymentWorkTicketCrystalReport.rpt"))
        ElseIf PaymentMethod = "Check" Then
            rptDoc.Load(Server.MapPath("PaymentWorkTicketCrystalReport.rpt"))
        ElseIf PaymentMethod = "E Check" Then
            rptDoc.Load(Server.MapPath("PaymentWorkTicketCrystalReport.rpt"))
        ElseIf PaymentMethod = "Credit Card" Then
            'rptDoc.Load(Server.MapPath("WorkTicketCrystalReport.rpt"))
            rptDoc.Load(Server.MapPath("CreditPaymentWorkTicketCrystalReport.rpt"))
        ElseIf PaymentMethod = "House Account" Then
            rptDoc.Load(Server.MapPath("HouseAccountWorkTicketCrystalReport.rpt"))
        Else
            rptDoc.Load(Server.MapPath("HouseAccountWorkTicketCrystalReport.rpt"))

        End If

        rptDoc.SetDataSource(WorkTicketDS)
        CrystalReportViewer1.ReportSource = rptDoc
        CrystalReportViewer1.Visible = False

        Try
            If pdffilename <> "" Then
                Dim PdfPath As String = ConfigurationManager.AppSettings("DocPath")
                pdffilename = "Workticket_" & pdffilename
                pdffilename = pdffilename.Replace(" ", "_")
                rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, PdfPath & pdffilename)
            End If
        Catch ex As Exception

        Finally
            rptDoc.Dispose()
        End Try

    End Sub


    ''New function for the Printername and others
    'Card_Print
    Protected Sub Page_LoadCard_Print(ByVal pdffilename As String)
        If pdffilename <> "" Then

            Dim CompanyID As String
            Dim UserName As String
            Dim DivisionID As String
            Dim DepartmentID As String



            CompanyID = Session("CompanyID")
            DivisionID = Session("DivisionID")
            DepartmentID = Session("DepartmentID")


            Dim dt As New Data.DataTable
            Dim dt2 As New Data.DataTable
            Dim cardbit, workticketbit As Boolean
            Dim CardMessagePrinterName, WorkTicketPrinterName As String
            Dim CardMessagePrintTrayName, CardMessagePrintPaperSize As String '' Vikas 
            WorkTicketPrinterName = ""
            CardMessagePrinterName = ""
            CardMessagePrintTrayName = ""
            CardMessagePrintPaperSize = ""
            cardbit = False
            workticketbit = False

            Dim obj As New clsOrderfrmPrintingConfigurations
            Dim obj2 As New ClsOrderfrmPrintingProfileNew  ''' Vikas

            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            obj2.CompanyId = CompanyID  ''' Vikas
            obj2.DivisionId = DivisionID ''' Vikas
            obj2.DepartmentId = DepartmentID ''' Vikas

            dt = obj.FillDetails()
            If dt.Rows.Count <> 0 Then
                obj2.ProfileId = dt.Rows(0)("CardMessagePrinterProfilename") ''' Vikas
                cardbit = dt.Rows(0)("CardMessage1bit")
                workticketbit = dt.Rows(0)("WorkTicket1bit")

                dt2 = obj2.FillDetails() ''' Vikas
                If dt2.Rows.Count <> 0 Then
                    CardMessagePrintTrayName = dt2.Rows(0)("printertray") ''' Vikas
                    CardMessagePrintPaperSize = dt2.Rows(0)("printerpapersize") ''' Vikas
                    CardMessagePrinterName = dt2.Rows(0)("printername")
                    txtprintermc.Text = CardMessagePrinterName

                    txttraymc.Text = CardMessagePrintTrayName ''' Vikas

                    txtpapersizemc.Text = CardMessagePrintPaperSize ''' Vikas

                End If
            End If

            pdffilename = "Card_" & pdffilename ' CompanyID & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".pdf"
            pdffilename = pdffilename.Replace(" ", "_")
            txturlmc.Text = pdffilename

        End If
    End Sub


    'WT print
    Protected Sub Page_LoadWT_Print(ByVal pdffilename As String)

        If pdffilename <> "" Then
            Dim CompanyID As String
            Dim UserName As String
            Dim DivisionID As String
            Dim DepartmentID As String

            CompanyID = Session("CompanyID")
            DivisionID = Session("DivisionID")
            DepartmentID = Session("DepartmentID")

            Dim dt As New Data.DataTable
            Dim dt2 As New Data.DataTable
            Dim cardbit, workticketbit As Boolean
            Dim CardMessagePrinterName, WorkTicketPrinterName As String
            Dim WorkTicketPrintTrayName, WorkTicketPrintPaperSize As String '' Vikas 
            WorkTicketPrinterName = ""
            CardMessagePrinterName = ""
            WorkTicketPrintTrayName = ""
            WorkTicketPrintPaperSize = ""

            cardbit = False
            workticketbit = False

            Dim obj As New clsOrderfrmPrintingConfigurations
            Dim obj2 As New ClsOrderfrmPrintingProfileNew   ''' Vikas
            obj.CompanyID = CompanyID
            obj.DivisionID = DivisionID
            obj.DepartmentID = DepartmentID
            obj2.CompanyId = CompanyID  ''' Vikas
            obj2.DivisionId = DivisionID ''' Vikas
            obj2.DepartmentId = DepartmentID ''' Vikas
            dt = obj.FillDetails()
            If dt.Rows.Count <> 0 Then
                obj2.ProfileId = dt.Rows(0)("WorkTicketPrinterProfileName") ''' Vikas

                cardbit = dt.Rows(0)("CardMessage1bit")
                workticketbit = dt.Rows(0)("WorkTicket1bit")

                dt2 = obj2.FillDetails() ''' Vikas
                If dt2.Rows.Count <> 0 Then
                    WorkTicketPrintTrayName = dt2.Rows(0)("printertray") ''' Vikas
                    WorkTicketPrintPaperSize = dt2.Rows(0)("printerpapersize") ''' Vikas
                    WorkTicketPrinterName = dt2.Rows(0)("printername")
                    txtprinterwt.Text = WorkTicketPrinterName

                    txttraywt.Text = WorkTicketPrintTrayName ''' Vikas

                    txtpapersizewt.Text = WorkTicketPrintPaperSize ''' Vikas

                End If

            End If

            pdffilename = "Workticket_" & pdffilename ' CompanyID & Date.Now.Month & Date.Now.Year & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".pdf"
            pdffilename = pdffilename.Replace(" ", "_")
            txturlwt.Text = pdffilename

        End If

    End Sub

    ''New function for the Printername and others



    Public Sub SessionClearData()
        Session("Oln") = ""
        Session("Resultvalue") = "1"
        Session("PO") = ""
        Session("EditButton") = ""
        Session("TempOrderNumber") = ""
        Session("CustomerID") = ""
        Session("CntrlFrmdrpCountry") = ""
        Session("OrderNumberToItemSearch") = ""
        Session("OrderNumber") = ""
        Session("CustIDCreatedFrmGrid") = ""
        Session("CustIDNotFound") = ""
        Session("CustIDCreatedFrmItemSearch") = ""
        Session("SpecifyCustomer") = ""
        Session("CheckLinkCliked") = ""
        Session("DataPostedFrmItemSearch") = ""
        Session("CtrlFrmItemNotFound") = ""
        Session("CustomerIDFromItemSearch") = ""
        Session("NewCustomer") = "False"
        Session("CntlFromPostBackTrue") = ""
        Session("CntrlFromItemSearch") = "False"

        BtnBookOrder.Enabled = False
        BtnPostOrder.Enabled = True
        BtnPostOrder.Text = "UnBook Order"
        Session("CntlFromPostBackTrue") = ""
        Session("CtrlFrmItemNotFound") = "False"
        Session("DataFromAddOn") = "False"
        Session("PO") = ""
        Session("Grid") = ""
    End Sub

    Public Function UpdateOrderPrintedDate(ByVal OrderNumber As String) As Boolean
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        Dim CompanyID As String = CType(SessionKey("CompanyID"), String)
        Dim EmployeeID As String = CType(SessionKey("EmployeeID"), String)
        Dim DivisionID As String = CType(SessionKey("DivisionID"), String)
        Dim DepartmentID As String = CType(SessionKey("DepartmentID"), String)

        ConnectionString = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(ConnectionString)
        Dim qry As String
        qry = "UPDATE OrderHeader set Printed=1,PrintedDate=Getdate() Where CompanyID=@f1 And DivisionID=@f2 And DepartmentID=@f3 And OrderNumber=@f4"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar, 36)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@f4", SqlDbType.NVarChar, 36)).Value = OrderNumber


            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()

            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False
        End Try

    End Function


#End Region

#Region "EmailNotifications"


    Public Sub EmailNotifications(ByVal OrdNumber As String)


        Dim PostOrder As New DAL.CustomOrder()
        Dim EmailType As String = "Order Placed"
        Dim EmailContent As String = ""
        Dim EmailSubject As String = ""
        Dim EmailAllowed As Boolean = True


        Dim rs As SqlDataReader
        rs = PostOrder.PopulateEmailContent(CompanyID, DivisionID, DepartmentID)
        If rs.HasRows = True Then



            While rs.Read

                EmailType = rs("EmailType").ToString()
                EmailContent = rs("EmailContent").ToString()
                EmailSubject = rs("EmailSubject").ToString()
                'EmailContent = EmailContent & "<br><br><p class='MsoNormal' style='margin: 0in 0in 0pt' align='justify'><font face='Verdana' size='2'>Powered by Sunflower Technologies, Inc 323.735.7272</font></p>"

                Try
                    EmailAllowed = rs("EmailAllowed")
                Catch ex As Exception
                    EmailAllowed = False
                End Try

                If EmailAllowed = True Then

                    If EmailType = "Order Placed" Then

                        PopulateEmailForCompany(EmailType, EmailContent, EmailSubject, OrdNumber)



                    ElseIf EmailType.Trim() = "Website Order Notification" Then


                        PopulateEmailForCompany(EmailType, EmailContent, EmailSubject, OrdNumber)

                    ElseIf EmailType.Trim() = "Order Notification to Vendor" Then


                        PopulateEmailForVendorsForItems(EmailType, EmailContent, EmailSubject, OrdNumber)


                    End If

                End If

            End While


        End If



        rs.Close()





    End Sub


    '--New Function For--'

    Public Sub PopulateEmailForVendorsForItems(ByVal EmailType As String, ByVal EmailContent As String, ByVal EmailSubject As String, ByVal OrdNumber As String)

        Dim PostOrder As New DAL.CustomOrder()
        Dim ItemReader As SqlDataReader
        ItemReader = PostOrder.PopulateOrderItemDetails(CompanyID, DivisionID, DepartmentID, OrdNumber)

        '------------------------------------------------------------------------------------------------------------'

        Dim StrBody As New StringBuilder()
        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='663' id='table1'>")
        If ItemReader.HasRows = True Then

            While ItemReader.Read()

                'ItemReader("itemID").ToString()
                Dim itemid As String = ItemReader("itemID").ToString()
                Dim obj As New clsOrderNotificationtoVendor
                obj.CompanyID = CompanyID
                obj.DepartmentID = DepartmentID
                obj.DivisionID = DivisionID
                obj.ItemID = itemid
                Dim dt As New DataTable
                dt = obj.FillDetailsitems

                Dim strvendor As String = ""

                If dt.Rows.Count <> 0 Then
                    Try
                        strvendor = dt.Rows(0)("VendorID")
                    Catch ex As Exception
                        strvendor = ""
                    End Try
                End If

                If strvendor <> "" Then
                    obj.VendorID = strvendor
                    Dim dtvendor As DataTable
                    dtvendor = obj.FillDetailsVendor()
                    Dim vendoremail As String
                    vendoremail = ""

                    If dtvendor.Rows.Count <> 0 Then
                        Try
                            vendoremail = dtvendor.Rows(0)("VendorEmail")
                        Catch ex As Exception
                            vendoremail = ""
                        End Try

                        If vendoremail <> "" Then
                            PopulateEmailForVendors(EmailType, EmailContent, EmailSubject, OrdNumber, itemid, vendoremail)
                        End If

                    End If


                End If



            End While

        End If
        ItemReader.Close()


    End Sub


    Public Sub PopulateEmailForVendors(ByVal EmailType As String, ByVal EmailContent As String, ByVal EmailSubject As String, ByVal OrdNumber As String, ByVal ItemIDVendor As String, ByVal vendoremail As String)

        Dim PostOrder As New DAL.CustomOrder()

        Dim CompanyName As String = ""
        Dim CompanyPhone As String = ""
        Dim Salutation As String = ""
        Dim name As String = ""
        Dim CustomerEmail As String = ""
        Dim ShipDate As String = ""
        Dim Paymentmethod As String = ""
        Dim Total As String = ""

        Dim itemquantity As String = ""
        Dim itemID As String = ""


        Dim shipsalutation As String = ""

        Dim ShippingName As String = ""
        Dim ShippingAddress1 As String = ""
        Dim ShippingAddress2 As String = ""
        Dim ShippingAddress3 As String = ""
        Dim ShipCity As String = ""
        Dim ShipState As String = ""
        Dim ShipZip As String = ""
        Dim ShipCountry As String = ""
        Dim DeliveryMethod As String = ""
        Dim DestinationType As String = ""
        Dim OccasionCode As String = ""
        Dim SpecialInstructions As String = ""
        Dim RetailerDate As String = ""
        Dim RetailerTime As String = ""

        Dim CompanyFax As String = ""
        Dim CustomerAddress1 As String = ""
        Dim CustomerAddress2 As String = ""
        Dim CustomerAddress3 As String = ""
        Dim CustomerCity As String = ""
        Dim CustomerState As String = ""
        Dim CustomerZip As String = ""
        Dim CustomerCountry As String = ""
        Dim CustomerPhone As String = ""
        Dim CustomerCell As String = ""
        Dim CompanyEmail As String = ""
        Dim ShippingPhone As String = ""
        Dim ShippingCell As String = ""
        Dim IpAddress As String = ""

        Dim WebsiteAddress As String = ""


        Dim AddonStr As String = ""
        Dim addonsTotal As String = ""

        Dim CardMessage As String = ""

        Dim GMT As Integer
        Dim DeliveryCharge As Double = 0.0

        Dim ItemTxPer As String = ""

        Dim ItemTxTot As Double = 0.0


        Dim ShipAttention As String = ""
        Dim ShipCompany As String = ""

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim Retailerreader As SqlDataReader

        Retailerreader = PostOrder.RetailerGetCompanyTime(CompanyID, DivisionID, DepartmentID)




        If Retailerreader.HasRows() = True Then
            While Retailerreader.Read()
                GMT = Convert.ToInt16(Retailerreader("GMTOffset").ToString())
            End While
            RetailerTime = DateTime.UtcNow.AddHours(GMT).ToShortTimeString()
            RetailerDate = DateTime.UtcNow.AddHours(GMT).ToShortDateString()

        End If

        Retailerreader.Close()

        Dim objUser As New CustomOrder()
        Dim HomePageReader As SqlDataReader

        HomePageReader = objUser.GetHomePageSetupValues(CompanyID, DivisionID, DepartmentID)

        While HomePageReader.Read()
            WebsiteAddress = HomePageReader("FrontEndUrl").ToString()
        End While
        HomePageReader.Close()

        Dim CustomerReader As SqlDataReader
        CustomerReader = PostOrder.PopulateShippingCustomerDetails(CompanyID, DivisionID, DepartmentID, OrdNumber)
        While CustomerReader.Read()

            CustomerAddress1 = CustomerReader("CustomerAddress1").ToString()

            CustomerAddress2 = CustomerReader("CustomerAddress2").ToString()

            CustomerAddress3 = CustomerReader("CustomerAddress3").ToString()

            CustomerCity = CustomerReader("CustomerCity").ToString()

            CustomerState = CustomerReader("CustomerState").ToString()

            CustomerZip = CustomerReader("CustomerZip").ToString()
            CustomerCountry = CustomerReader("CustomerCountry").ToString()

            CustomerPhone = CustomerReader("CustomerPhone").ToString()

            CustomerCell = CustomerReader("CustomerCell").ToString()
            CardMessage = CustomerReader("CardMessage").ToString()
            IpAddress = CustomerReader("IpAddress").ToString()

            Salutation = CustomerReader("CustomerSalutation").ToString()
            CompanyFax = CustomerReader("CompanyFax").ToString()

            CompanyName = CustomerReader("CompanyName").ToString()
            CompanyPhone = CustomerReader("CompanyPhone").ToString()
            name = CustomerReader("CustomerName").ToString()
            CompanyEmail = CustomerReader("CompanyEmail").ToString()
            CustomerEmail = CustomerReader("CustomerEmail").ToString()
            ShipDate = Convert.ToDateTime(CustomerReader("OrderShipDate")).ToShortDateString()

            Paymentmethod = CustomerReader("PaymentMethodID").ToString()

            If CustomerReader("Total").ToString <> "" Then

                Total = Format(CustomerReader("Total"), "c")


            End If

            shipsalutation = CustomerReader("ShippingSalutation").ToString()
            ShippingName = CustomerReader("ShippingFirstName").ToString() & " " & CustomerReader("ShippingLastName").ToString()
            ShippingAddress1 = CustomerReader("ShippingAddress1").ToString()
            ShippingAddress2 = CustomerReader("ShippingAddress2").ToString()
            ShippingAddress3 = CustomerReader("ShippingAddress3").ToString()
            ShipCity = CustomerReader("ShippingCity").ToString()
            ShipState = CustomerReader("ShippingState").ToString()
            ShipZip = CustomerReader("ShippingZip").ToString()
            ShipCountry = CustomerReader("ShippingCountry").ToString()
            ShipAttention = CustomerReader("ShippingAttention").ToString()
            ShipCompany = CustomerReader("ShippingCompany").ToString()

            If CustomerReader("ShipMethodID").ToString() <> "0" Then
                DeliveryMethod = CustomerReader("ShipMethodID").ToString()
            End If

            If CustomerReader("DestinationType").ToString() <> "0" Then
                DestinationType = CustomerReader("DestinationType").ToString()
            End If

            If CustomerReader("OccasionCode").ToString() <> "0" Then
                OccasionCode = CustomerReader("OccasionCode").ToString()

            End If

            SpecialInstructions = CustomerReader("DriverRouteInfo").ToString()

            ShippingPhone = CustomerReader("ShippingPhone").ToString()
            ShippingCell = CustomerReader("ShippingCell").ToString()


        End While

        CustomerReader.Close()

        Dim ItemReader As SqlDataReader
        ItemReader = PostOrder.PopulateOrderItemDetails(CompanyID, DivisionID, DepartmentID, OrdNumber)

        '------------------------------------------------------------------------------------------------------------'


        Dim StrBody As New StringBuilder()
        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='663' id='table1'>")
        If ItemReader.HasRows = True Then
            Dim MNUID As String = "3"
            StrBody.Append("<tr    align='center'>")
            StrBody.Append("<td width='275'><font face='Verdana'><font size='1'><b>Item Name</b>")
            StrBody.Append("</td>")
            StrBody.Append("<td width='187'><font face='Verdana'><font size='1'><b>Item Description</b>")
            StrBody.Append("</td>")
            StrBody.Append("<td width='67'><b><font face='Verdana' size='1'></font></b></td>")
            StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> Order Quantity</b>")
            StrBody.Append("</td>")
            StrBody.Append("<td width='88'><font size='1' face='Verdana'><b> </b></td>")
            StrBody.Append("</tr>")




            While ItemReader.Read()

                If ItemIDVendor <> ItemReader("itemID").ToString() Then
                    Continue While
                End If

                StrBody.Append("<tr  align='center'>")

                StrBody.Append("<td width='175' valign='top'><font face='Verdana'>")
                StrBody.Append("<table border='0' width='100%' cellspacing='0' cellpadding='0' id='table4'>")
                StrBody.Append("<tr width='100%'>")

                ''Added New code for genrate the image path if it exist
                Dim DocumentDir As String = ""
                Dim img1 As String = ""
                DocumentDir = ConfigurationManager.AppSettings("ImgPath")
                Try
                    img1 = ItemReader("PictureURL").ToString().Trim()
                Catch ex As Exception
                    img1 = ""
                End Try

                If img1 <> "" Then
                    If System.IO.File.Exists(DocumentDir & img1) Then
                        StrBody.Append("<td valign='middle'><font face='Verdana'><a href='" & WebsiteAddress & "/ItemDetails.aspx?id=" & ItemReader("itemID").ToString() & "&MNU=" & MNUID & "'><img border='0'  width='100'height='100'   src='http://quickflorafrontend.quickflora.com/itemimages/" & ItemReader("PictureURL").ToString().Trim() & "'></a></td>")
                    Else
                        StrBody.Append("<td valign='middle'>&nbsp;</td>")
                    End If
                Else
                    StrBody.Append("<td valign='middle'>&nbsp;</td>")
                End If

                ''

                'StrBody.Append("<td valign='middle'><font face='Verdana'><a href='" & WebsiteAddress & "/ItemDetails.aspx?id=" & ItemReader("itemID").ToString() & "&MNU=" & MNUID & "'><img border='0'  width='100'height='100'   src='http://quickflorafrontend.quickflora.com/itemimages/" & ItemReader("PictureURL").ToString().Trim() & "'></a></td>")


                StrBody.Append("<td width='100%' valign='middle'><font face='Verdana'><a href='" & WebsiteAddress & "/ItemDetails.aspx?id=" & ItemReader("itemID").ToString() & "&MNU=" & MNUID & "'><font size='1' color='Black'>" & ItemReader("ItemName").ToString() & "</font></a><font size='1' color='Black'>")

                StrBody.Append("</td>")
                StrBody.Append("</tr>")
                StrBody.Append("</table>")

                '    'Addon item function calling here
                AddonStr = "" ' ADDonsValue(ItemReader("AddOnsIDsQty").ToString())

                If AddonStr.Trim() <> "" Then
                    addonsTotal = 0 ' AddonsTotalPrice(ItemReader("AddOnsIDsQty").ToString())
                    StrBody.Append(AddonStr)
                End If

                ' New Codes Starts

                StrBody.Append("</td>")
                StrBody.Append("<td width='187' valign='top'><font face='Verdana'><font size='1'>" + ItemReader("Description").ToString())
                StrBody.Append("</td>")

                StrBody.Append("<td width='67' valign='top'><font size='1' face='Verdana'>" + "</td>")
                StrBody.Append("<td width='50' valign='top'><font size='1' face='Verdana'>" + ItemReader("OrderQty").ToString() + "</td>")
                Dim BasePricePlusUpSellPrice As Double = ItemReader("ItemUnitPrice").ToString()
                Dim PrValue As Double = 0.0
                PrValue = BasePricePlusUpSellPrice * Integer.Parse(ItemReader("OrderQty").ToString())

                StrBody.Append("<td width='88' valign='top'><font size='1' face='Verdana'>" + "</td>")
                StrBody.Append("</tr>")
                'Dim TotalTaxValue As Double = 0.0
                'TotalTaxValue = (PrValue + addonsTotal) * ItemReader("TaxPercent") / 100

                DeliveryCharge = Convert.ToDouble(ItemReader("Delivery")) + Convert.ToDouble(ItemReader("Service")) + Convert.ToDouble(ItemReader("Relay"))

                ItemTxPer = ItemReader("TaxPercent").ToString()
                ItemTxTot = Convert.ToDouble(ItemReader("TaxTotal"))

                'StrBody.Append("<tr  align='center'>")
                'StrBody.Append("<td width='555' colspan='4'>")
                'StrBody.Append("<p align='right'><font face='Verdana' size='1'>Delivery/Service Charge:" + "$" + String.Format("{0:N2}", DeliveryCharge) + "<br>")
                'StrBody.Append("Tax (" + ItemReader("TaxPercent").ToString() + "%):" + "$" + String.Format("{0:N2}", ItemReader("TaxTotal")) + "<br>")
                'StrBody.Append("Total:</font></td>")
                'StrBody.Append("<td width='88' valign='bottom'><font size='1' face='Verdana'>" + "$" + String.Format("{0:N2}", ItemReader("Total")) + "</td>")
                'StrBody.Append("</tr>")

                'New Codes Ends


                ' GrItemTotalAmount += TotalSubvalue
            End While
            'Dim DeliSerCharge As Double = 0.0
            'DeliSerCharge = Convert.ToDouble(Total.ToString().Replace("$", "")) - GrItemTotalAmount
            ''  NewCode(Starts)
            StrBody.Append("</table>")
            'StrBody.Append("<table border='0' width='67%' cellspacing='1' cellpadding='0' id='table3'>")
            'StrBody.Append("<tr  align='center'>")
            'StrBody.Append("<td width='555' colspan='4'>")
            'StrBody.Append("<p align='right'><font face='Verdana' size='1'>Delivery/Service Charge:" + "$" + String.Format("{0:N2}", DeliveryCharge) + "<br>")
            'StrBody.Append("Tax (" + ItemTxPer.ToString() + "%):" + "$" + String.Format("{0:N2}", ItemTxTot) + "<br>")
            'StrBody.Append("</font></td>")
            'StrBody.Append("<td width='88' valign='bottom'><font size='1' face='Verdana'></td>")
            'StrBody.Append("</tr>")
            'StrBody.Append("<tr>")
            'StrBody.Append("<td>")
            'StrBody.Append("<p align='right'><b><font face='Verdana' size='1'>Net Total:" + Total + "</font></b></td>")
            'StrBody.Append("</tr>")
            'StrBody.Append("</table>")
            'NewCode Ends



        End If
        ItemReader.Close()
        Dim ItemDetails As String = StrBody.ToString()



        '--------------------------------------------------------------------------------------------------------------'
        EmailSubject = EmailSubject.Replace("~company name~", CompanyName)
        EmailSubject = EmailSubject.Replace("~order number~", OrdNumber)
        EmailSubject = EmailSubject.Replace("~ship date~", ShipDate)

        EmailContent = EmailContent.Replace("~RetailerDate~", RetailerDate)
        EmailContent = EmailContent.Replace("~RetailerTime~", RetailerTime)

        EmailContent = EmailContent.Replace("~ship date~", ShipDate)
        EmailContent = EmailContent.Replace("~Occasion code~", OccasionCode)
        EmailContent = EmailContent.Replace("~Special instructions~", SpecialInstructions)
        EmailContent = EmailContent.Replace("~shippingcity~", ShipCity)
        EmailContent = EmailContent.Replace("~shippingstate~", ShipState)
        EmailContent = EmailContent.Replace("~shippingzip~", ShipZip)
        EmailContent = EmailContent.Replace("~shippingcountry~", ShipCountry)
        EmailContent = EmailContent.Replace("~order number~", OrdNumber)
        EmailContent = EmailContent.Replace("~ship to address 1~", ShippingAddress1)
        EmailContent = EmailContent.Replace("~ship to address 2~", ShippingAddress2)
        EmailContent = EmailContent.Replace("~ship to address 3~", ShippingAddress3)


        EmailContent = EmailContent.Replace("~company name~", CompanyName)
        EmailContent = EmailContent.Replace("~company phone~", CompanyPhone)

        EmailContent = EmailContent.Replace("~salutation~", Salutation)
        EmailContent = EmailContent.Replace("~customername~", name)
        EmailContent = EmailContent.Replace("~payment method~", Paymentmethod)
        EmailContent = EmailContent.Replace("~Total~", Total)


        EmailContent = EmailContent.Replace("~Delivery method~", DeliveryMethod)
        EmailContent = EmailContent.Replace("~Destination type~", DestinationType)
        EmailContent = EmailContent.Replace("~payment method~", Paymentmethod)
        EmailContent = EmailContent.Replace("~item details~", ItemDetails)
        EmailContent = EmailContent.Replace("~ship to salutation~", shipsalutation)
        EmailContent = EmailContent.Replace("~shippingcustomername~", ShippingName)

        'New Changes Starts here
        EmailContent = EmailContent.Replace("~CompanyFax~", CompanyFax)
        EmailContent = EmailContent.Replace("~CustomerAddress1~", CustomerAddress1)
        EmailContent = EmailContent.Replace("~CustomerAddress2~", CustomerAddress2)
        EmailContent = EmailContent.Replace("~CustomerAddress3~", CustomerAddress3)
        EmailContent = EmailContent.Replace("~CustomerCity~", CustomerCity)
        EmailContent = EmailContent.Replace("~CustomerState~", CustomerState)
        EmailContent = EmailContent.Replace("~CustomerZip~", CustomerZip)
        EmailContent = EmailContent.Replace("~CustomerCountry~", CustomerCountry)
        EmailContent = EmailContent.Replace("~CustomerPhone~", CustomerPhone)
        EmailContent = EmailContent.Replace("~CustomerCell~", CustomerCell)
        EmailContent = EmailContent.Replace("~CustomerEmail~", CustomerEmail)
        EmailContent = EmailContent.Replace("~CardMessage~", CardMessage)

        EmailContent = EmailContent.Replace("~ShippingPhone~", ShippingPhone)
        EmailContent = EmailContent.Replace("~ShippingCell~", ShippingCell)

        IpAddress = Request.ServerVariables("REMOTE_ADDR")
        EmailContent = EmailContent.Replace("~IpAddress~", IpAddress)
        EmailContent = EmailContent.Replace("~WebsiteAddress~", "<a href='" & WebsiteAddress & "'>" & WebsiteAddress & "</a>")
        EmailContent = EmailContent.Replace("~Ship to Attention~", ShipAttention)
        EmailContent = EmailContent.Replace("~Ship to Company~", ShipCompany)

        Dim OrderPlacedSubject As String
        Dim OrderPlacedContent As String




        OrderPlacedSubject = EmailSubject
        OrderPlacedContent = EmailContent


        Dim ToAddress As String = vendoremail
        Dim FromAddress As String = CompanyEmail

        'Dim ToAddress As String = "jacob.mathew@iqss.co.in"
        'Dim FromAddress As String = "jacob.mathew@iqss.co.in"

        If vendoremail <> "" And CompanyEmail <> "" And OrderPlacedContent <> "" Then

            EmailSending(OrderPlacedSubject, OrderPlacedContent, FromAddress, ToAddress)

        End If


    End Sub

    '--------------------'
    Public Sub PopulateEmailForCompany(ByVal EmailType As String, ByVal EmailContent As String, ByVal EmailSubject As String, ByVal OrdNumber As String)

        Dim PostOrder As New DAL.CustomOrder()

        Dim CompanyName As String = ""
        Dim CompanyPhone As String = ""
        Dim Salutation As String = ""
        Dim name As String = ""
        Dim CustomerEmail As String = ""
        Dim ShipDate As String = ""
        Dim Paymentmethod As String = ""
        Dim Total As String = ""

        Dim itemquantity As String = ""
        Dim itemID As String = ""


        Dim shipsalutation As String = ""

        Dim ShippingName As String = ""
        Dim ShippingAddress1 As String = ""
        Dim ShippingAddress2 As String = ""
        Dim ShippingAddress3 As String = ""
        Dim ShipCity As String = ""
        Dim ShipState As String = ""
        Dim ShipZip As String = ""
        Dim ShipCountry As String = ""
        Dim DeliveryMethod As String = ""
        Dim DestinationType As String = ""
        Dim OccasionCode As String = ""
        Dim SpecialInstructions As String = ""
        Dim RetailerDate As String = ""
        Dim RetailerTime As String = ""

        Dim CompanyFax As String = ""
        Dim CustomerAddress1 As String = ""
        Dim CustomerAddress2 As String = ""
        Dim CustomerAddress3 As String = ""
        Dim CustomerCity As String = ""
        Dim CustomerState As String = ""
        Dim CustomerZip As String = ""
        Dim CustomerCountry As String = ""
        Dim CustomerPhone As String = ""
        Dim CustomerCell As String = ""
        Dim CompanyEmail As String = ""
        Dim ShippingPhone As String = ""
        Dim ShippingCell As String = ""
        Dim IpAddress As String = ""

	Dim Ship_priority As String = ""

        Dim WebsiteAddress As String = ""


        Dim AddonStr As String = ""
        Dim addonsTotal As String = ""

        Dim CardMessage As String = ""

        Dim GMT As Integer
        Dim DeliveryCharge As Double = 0.0

        Dim ItemTxPer As String = ""

        Dim ItemTxTot As Double = 0.0


        Dim ShipAttention As String = ""
        Dim ShipCompany As String = ""

        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim Retailerreader As SqlDataReader

        Retailerreader = PostOrder.RetailerGetCompanyTime(CompanyID, DivisionID, DepartmentID)




        If Retailerreader.HasRows() = True Then
            While Retailerreader.Read()
                GMT = Convert.ToInt16(Retailerreader("GMTOffset").ToString())
            End While
            RetailerTime = DateTime.UtcNow.AddHours(GMT).ToShortTimeString()
            RetailerDate = DateTime.UtcNow.AddHours(GMT).ToShortDateString()

        End If

        Retailerreader.Close()

        Dim objUser As New CustomOrder()
        Dim HomePageReader As SqlDataReader

        HomePageReader = objUser.GetHomePageSetupValues(CompanyID, DivisionID, DepartmentID)

        While HomePageReader.Read()
            WebsiteAddress = HomePageReader("FrontEndUrl").ToString()

        End While
        HomePageReader.Close()

        Dim CustomerReader As SqlDataReader
        CustomerReader = PostOrder.PopulateShippingCustomerDetails(CompanyID, DivisionID, DepartmentID, OrdNumber)
        While CustomerReader.Read()

            CustomerAddress1 = CustomerReader("CustomerAddress1").ToString()

            CustomerAddress2 = CustomerReader("CustomerAddress2").ToString()

            CustomerAddress3 = CustomerReader("CustomerAddress3").ToString()

            CustomerCity = CustomerReader("CustomerCity").ToString()

            CustomerState = CustomerReader("CustomerState").ToString()

            CustomerZip = CustomerReader("CustomerZip").ToString()
            CustomerCountry = CustomerReader("CustomerCountry").ToString()

            CustomerPhone = CustomerReader("CustomerPhone").ToString()

            CustomerCell = CustomerReader("CustomerCell").ToString()
            CardMessage = CustomerReader("CardMessage").ToString()
            IpAddress = CustomerReader("IpAddress").ToString()

            Salutation = CustomerReader("CustomerSalutation").ToString()
            CompanyFax = CustomerReader("CompanyFax").ToString()

            CompanyName = CustomerReader("CompanyName").ToString()
            CompanyPhone = CustomerReader("CompanyPhone").ToString()
            name = CustomerReader("CustomerName").ToString()
            CompanyEmail = CustomerReader("CompanyEmail").ToString()
            CustomerEmail = CustomerReader("CustomerEmail").ToString()
            ShipDate = Convert.ToDateTime(CustomerReader("OrderShipDate")).ToShortDateString()

            Paymentmethod = CustomerReader("PaymentMethodID").ToString()

            If CustomerReader("Total").ToString <> "" Then

                Total = Format(CustomerReader("Total"), "c")


            End If

            shipsalutation = CustomerReader("ShippingSalutation").ToString()
            ShippingName = CustomerReader("ShippingFirstName").ToString() & " " & CustomerReader("ShippingLastName").ToString()
            ShippingAddress1 = CustomerReader("ShippingAddress1").ToString()
            ShippingAddress2 = CustomerReader("ShippingAddress2").ToString()
            ShippingAddress3 = CustomerReader("ShippingAddress3").ToString()
            ShipCity = CustomerReader("ShippingCity").ToString()
            ShipState = CustomerReader("ShippingState").ToString()
            ShipZip = CustomerReader("ShippingZip").ToString()
            ShipCountry = CustomerReader("ShippingCountry").ToString()
            ShipAttention = CustomerReader("ShippingAttention").ToString()
            ShipCompany = CustomerReader("ShippingCompany").ToString()

            If CustomerReader("ShipMethodID").ToString() <> "0" Then
                DeliveryMethod = CustomerReader("ShipMethodID").ToString()
            End If

            If CustomerReader("DestinationType").ToString() <> "0" Then
                DestinationType = CustomerReader("DestinationType").ToString()
            End If

            If CustomerReader("OccasionCode").ToString() <> "0" Then
                OccasionCode = CustomerReader("OccasionCode").ToString()

            End If

            SpecialInstructions = CustomerReader("DriverRouteInfo").ToString()

            ShippingPhone = CustomerReader("ShippingPhone").ToString()
            ShippingCell = CustomerReader("ShippingCell").ToString()

Try
                Ship_priority = CustomerReader("Priority").ToString()
            Catch ex As Exception

            End Try


        End While

        CustomerReader.Close()

        Dim ItemReader As SqlDataReader
        ItemReader = PostOrder.PopulateOrderItemDetails(CompanyID, DivisionID, DepartmentID, OrdNumber)

        '------------------------------------------------------------------------------------------------------------'

        Dim CustomerDiscountPerc As Double
        Dim StrBody As New StringBuilder()
        StrBody.Append("<table border='1' cellspacing='0' cellpadding='0' width='663' id='table1'>")
        If ItemReader.HasRows = True Then
            Dim MNUID As String = "3"
            StrBody.Append("<tr    align='center'>")
            StrBody.Append("<td width='275'><font face='Verdana'><font size='1'><b>Item Name</b>")
            StrBody.Append("</td>")
            StrBody.Append("<td width='187'><font face='Verdana'><font size='1'><b>Item Description</b>")
            StrBody.Append("</td>")
            StrBody.Append("<td width='67'><b><font face='Verdana' size='1'>Unit Price</font></b></td>")
            StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> Order Quantity</b>")
            StrBody.Append("</td>")
            StrBody.Append("<td width='50'><font size='1' face='Verdana'><b> Item Discount</b>")
            StrBody.Append("</td>")
            StrBody.Append("<td width='88'><font size='1' face='Verdana'><b> Total</b></td>")
            StrBody.Append("</tr>")




            While ItemReader.Read()


                StrBody.Append("<tr  align='center'>")

                StrBody.Append("<td width='175' valign='top'><font face='Verdana'>")
                StrBody.Append("<table border='0' width='100%' cellspacing='0' cellpadding='0' id='table4'>")
                StrBody.Append("<tr width='100%'>")


                ''Added New code for genrate the image path if it exist
                Dim DocumentDir As String = ""
                Dim img1 As String = ""
                DocumentDir = ConfigurationManager.AppSettings("ImgPath")
                Try
                    img1 = ItemReader("PictureURL").ToString().Trim()
                Catch ex As Exception
                    img1 = ""
                End Try

                If img1 <> "" Then
                    If System.IO.File.Exists(DocumentDir & img1) Then
                        StrBody.Append("<td valign='middle'><font face='Verdana'><a href='" & WebsiteAddress & "/ItemDetails.aspx?id=" & ItemReader("itemID").ToString() & "&MNU=" & MNUID & "'><img border='0'  width='100'height='100'   src='http://quickflorafrontend.quickflora.com/itemimages/" & ItemReader("PictureURL").ToString().Trim() & "'></a></td>")
                    Else
                        StrBody.Append("<td valign='middle'>&nbsp;</td>")
                    End If
                Else
                    StrBody.Append("<td valign='middle'>&nbsp;</td>")
                End If

                ''


                'StrBody.Append("<td valign='middle'><font face='Verdana'><a href='" & WebsiteAddress & "/ItemDetails.aspx?id=" & ItemReader("itemID").ToString() & "&MNU=" & MNUID & "'><img border='0'  width='100'height='100'   src='http://quickflorafrontend.quickflora.com/itemimages/" & ItemReader("PictureURL").ToString().Trim() & "'></a></td>")



                StrBody.Append("<td width='100%' valign='middle'><font face='Verdana'><a href='" & WebsiteAddress & "/ItemDetails.aspx?id=" & ItemReader("itemID").ToString() & "&MNU=" & MNUID & "'><font size='1' color='Black'>" & ItemReader("ItemName").ToString() & "</font></a><font size='1' color='Black'>")

                StrBody.Append("</td>")
                StrBody.Append("</tr>")
                StrBody.Append("</table>")

                '    'Addon item function calling here
                AddonStr = "" ' ADDonsValue(ItemReader("AddOnsIDsQty").ToString())

                If AddonStr.Trim() <> "" Then
                    addonsTotal = 0 ' AddonsTotalPrice(ItemReader("AddOnsIDsQty").ToString())
                    StrBody.Append(AddonStr)
                End If

                ' New Codes Starts

                StrBody.Append("</td>")
                StrBody.Append("<td width='187' valign='top'><font face='Verdana'><font size='1'>" + ItemReader("Description").ToString())
                StrBody.Append("</td>")

                StrBody.Append("<td width='67' valign='top'><font size='1' face='Verdana'>" + "$" + String.Format("{0:n2}", ItemReader("ItemUnitPrice")) + "</td>")
                StrBody.Append("<td width='50' valign='top'><font size='1' face='Verdana'>" + ItemReader("OrderQty").ToString() + "</td>")
                Dim BasePricePlusUpSellPrice As Double = ItemReader("ItemUnitPrice").ToString()
                Dim ItemTaxPercent As Double = ItemReader("ItemtDiscountPerc").ToString()

                StrBody.Append("<td width='50' valign='top'><font size='1' face='Verdana'>" + String.Format("{0:n2}", ItemTaxPercent) + "<br/>" + ItemReader("DiscountFlatOrPercent").ToString() + "</td>")
                Dim PrValue As Double = 0.0
                'JMT Code on 11th August 2008 Starts here
                If ItemReader("DiscountFlatOrPercent").ToString() = "Flat" Then
                    PrValue = (BasePricePlusUpSellPrice * Integer.Parse(ItemReader("OrderQty").ToString()) - ItemTaxPercent)
                Else
                    PrValue = (BasePricePlusUpSellPrice * Integer.Parse(ItemReader("OrderQty").ToString()) * (100 - ItemTaxPercent) / 100)
                End If
                'JMT Code on 11th August 2008 Ends here
                'PrValue = (BasePricePlusUpSellPrice * Integer.Parse(ItemReader("OrderQty").ToString()) * (100 - ItemTaxPercent) / 100)

                StrBody.Append("<td width='88' align='right' valign='top'><font size='1' face='Verdana'>" + "$" + String.Format("{0:n2}", PrValue) + "</td>")
                StrBody.Append("</tr>")
                'Dim TotalTaxValue As Double = 0.0
                'TotalTaxValue = (PrValue + addonsTotal) * ItemReader("TaxPercent") / 100

                DeliveryCharge = Convert.ToDouble(ItemReader("Delivery")) + Convert.ToDouble(ItemReader("Service")) + Convert.ToDouble(ItemReader("Relay"))

                ItemTxPer = ItemReader("TaxPercent").ToString()
                ItemTxTot = Convert.ToDouble(ItemReader("TaxTotal"))
                CustomerDiscountPerc = ItemReader("DiscountAmount").ToString()
                'StrBody.Append("<tr  align='center'>")
                'StrBody.Append("<td width='555' colspan='4'>")
                'StrBody.Append("<p align='right'><font face='Verdana' size='1'>Delivery/Service Charge:" + "$" + String.Format("{0:N2}", DeliveryCharge) + "<br>")
                'StrBody.Append("Tax (" + ItemReader("TaxPercent").ToString() + "%):" + "$" + String.Format("{0:N2}", ItemReader("TaxTotal")) + "<br>")
                'StrBody.Append("Total:</font></td>")
                'StrBody.Append("<td width='88' valign='bottom'><font size='1' face='Verdana'>" + "$" + String.Format("{0:N2}", ItemReader("Total")) + "</td>")
                'StrBody.Append("</tr>")

                'New Codes Ends


                ' GrItemTotalAmount += TotalSubvalue
            End While
            'Dim DeliSerCharge As Double = 0.0
            'DeliSerCharge = Convert.ToDouble(Total.ToString().Replace("$", "")) - GrItemTotalAmount
            ''  NewCode(Starts)
            'StrBody.Append("</table>")
            'StrBody.Append("<table border='0' width='67%' cellspacing='1' cellpadding='0' id='table3'>")
            'StrBody.Append("<tr  align='center'>")
            'StrBody.Append("<td width='555' colspan='4'>")
            '            'StrBody.Append("<p align='right'><font face='Verdana' size='1'>Delivery/Service Charge:" + "$" + String.Format("{0:N2}", DeliveryCharge) + "<br>")
            '           StrBody.Append("<font face='Verdana' size='1'>Discount:" + "$" + String.Format("{0:N2}", CustomerDiscountPerc) + "<br>")
            '         StrBody.Append("Tax (" + ItemTxPer.ToString() + "%):" + "$" + String.Format("{0:N2}", ItemTxTot) + "<br>")
            ' '         StrBody.Append("</font></td>")
            '       StrBody.Append("<td width='88' valign='bottom'><font size='1' face='Verdana'></td>")
            '      StrBody.Append("</tr>")
            '     StrBody.Append("<tr>")
            '    StrBody.Append("<td>")
            '   StrBody.Append("<p align='right'><b><font face='Verdana' size='1'>Net Total:" + Total + "</font></b></td>")
            '  StrBody.Append("</tr>")
            ' StrBody.Append("</table>")

            StrBody.Append("<tr  align='center'>")
            StrBody.Append("<td width='455' colspan='3' align='right'></td>")
            StrBody.Append("<td width='100' colspan='2'  align='right'>")
            StrBody.Append("<font size='1' face='Verdana'><b>Total:</b></font></td>")
            StrBody.Append("<td width='88' align='right' valign='bottom'><font size='1' face='Verdana'><b>" + "$" & String.Format("{0:n2}", txtSubtotal.Text) + "</b></td>")
            StrBody.Append("</tr>")

            StrBody.Append("<tr  align='center'>")
            StrBody.Append("<td width='455' colspan='3' align='right'></td>")
            StrBody.Append("<td width='100' colspan='2'  align='right'>")
            StrBody.Append("<font size='1' face='Verdana'>Discount:</font></td>")
            StrBody.Append("<td width='88' align='right'  valign='bottom'><font size='1' face='Verdana'>" + "$" & String.Format("{0:n2}", CustomerDiscountPerc) + "</td>")
            StrBody.Append("</tr>")

            StrBody.Append("<tr  align='center'>")
            StrBody.Append("<td width='455' colspan='3' align='right'></td>")
            StrBody.Append("<td width='100' colspan='2'  align='right'>")
            StrBody.Append("<font size='1' face='Verdana'>Delivery/Service:</font></td>")
            StrBody.Append("<td width='88' align='right'  valign='bottom'><font size='1' face='Verdana'>" + "$" & String.Format("{0:n2}", DeliveryCharge) + "</td>")
            StrBody.Append("</tr>")

            'StrBody.Append("<tr  align='center'>")
            'StrBody.Append("<td width='455' colspan='3' align='right'></td>")
            'StrBody.Append("<td width='100' colspan='2'  align='right'>")
            'StrBody.Append("<font size='1' face='Verdana'>Tax (" + ItemTxPer.ToString() + "%):</font></td>")
            'StrBody.Append("<td width='88' align='right'  valign='bottom'><font size='1' face='Verdana'>" + "$" & String.Format("{0:n2}", ItemTxTot) + "</td>")
            'StrBody.Append("</tr>")


            If CompanyID = "BranchingOutFloralL0E1E0" Or CompanyID = "Greene and Greene" Or CompanyID = "Demo Site-90210" Or CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "PoppiesV8W1L8" Then
                'If IsNumeric(lblOrderNumberData.Text.Trim()) Then
                '    InsertCaTaxDetail(lblOrderNumberData.Text, txtTaxPercentGST.Text.Replace("%", ""), txtTaxGST.Text, txtTaxPercentPST.Text.Replace("%", ""), txtTaxPST.Text)
                'End If

                StrBody.Append("<tr  align='center'>")
                StrBody.Append("<td width='455' colspan='3' align='right'></td>")
                StrBody.Append("<td width='100' colspan='2'  align='right'>")
                StrBody.Append("<font size='1' face='Verdana'>GST/HST (" + txtTaxPercentGST.Text.Replace("%", "") + "%):</font></td>")
                StrBody.Append("<td width='88' align='right'  valign='bottom'><font size='1' face='Verdana'>" + "$" & String.Format("{0:n2}", txtTaxGST.Text) + "</td>")
                StrBody.Append("</tr>")

                StrBody.Append("<tr  align='center'>")
                StrBody.Append("<td width='455' colspan='3' align='right'></td>")
                StrBody.Append("<td width='100' colspan='2'  align='right'>")
                StrBody.Append("<font size='1' face='Verdana'>PST (" + txtTaxPercentPST.Text.Replace("%", "") + "%):</font></td>")
                StrBody.Append("<td width='88' align='right'  valign='bottom'><font size='1' face='Verdana'>" + "$" & String.Format("{0:n2}", txtTaxPST.Text) + "</td>")
                StrBody.Append("</tr>")
            Else

                StrBody.Append("<tr  align='center'>")
                StrBody.Append("<td width='455' colspan='3' align='right'></td>")
                StrBody.Append("<td width='100' colspan='2'  align='right'>")
                StrBody.Append("<font size='1' face='Verdana'>Tax (" + ItemTxPer.ToString() + "%):</font></td>")
                StrBody.Append("<td width='88' align='right'  valign='bottom'><font size='1' face='Verdana'>" + "$" & String.Format("{0:n2}", ItemTxTot) + "</td>")
                StrBody.Append("</tr>")


            End If

            StrBody.Append("<tr  align='center'>")
            StrBody.Append("<td width='455' colspan='3' align='right'></td>")
            StrBody.Append("<td width='100' colspan='2'  align='right'>")
            StrBody.Append("<font size='1' face='Verdana'><b>Total:</b></font></td>")
            StrBody.Append("<td width='88' align='right'  valign='bottom'><font size='1' face='Verdana'><b>" & String.Format("{0:n2}", Total) + "</b></td>")
            StrBody.Append("</tr>")


            StrBody.Append("</table>")

            'NewCode Ends



        End If
        ItemReader.Close()
        Dim ItemDetails As String = StrBody.ToString()



        '--------------------------------------------------------------------------------------------------------------'
        EmailSubject = EmailSubject.Replace("~company name~", CompanyName)
        EmailSubject = EmailSubject.Replace("~order number~", OrdNumber)
        EmailSubject = EmailSubject.Replace("~ship date~", ShipDate)

        EmailContent = EmailContent.Replace("~RetailerDate~", RetailerDate)
        EmailContent = EmailContent.Replace("~RetailerTime~", RetailerTime)

        EmailContent = EmailContent.Replace("~ship date~", ShipDate)
        EmailContent = EmailContent.Replace("~Occasion code~", OccasionCode)
        EmailContent = EmailContent.Replace("~Special instructions~", SpecialInstructions)
        EmailContent = EmailContent.Replace("~shippingcity~", ShipCity)
        EmailContent = EmailContent.Replace("~shippingstate~", ShipState)
        EmailContent = EmailContent.Replace("~shippingzip~", ShipZip)
        EmailContent = EmailContent.Replace("~shippingcountry~", ShipCountry)
        EmailContent = EmailContent.Replace("~order number~", OrdNumber)
        EmailContent = EmailContent.Replace("~ship to address 1~", ShippingAddress1)
        EmailContent = EmailContent.Replace("~ship to address 2~", ShippingAddress2)
        EmailContent = EmailContent.Replace("~ship to address 3~", ShippingAddress3)


        EmailContent = EmailContent.Replace("~company name~", CompanyName)
        EmailContent = EmailContent.Replace("~company phone~", CompanyPhone)

        EmailContent = EmailContent.Replace("~salutation~", Salutation)
        EmailContent = EmailContent.Replace("~customername~", name)
        EmailContent = EmailContent.Replace("~payment method~", Paymentmethod)
        EmailContent = EmailContent.Replace("~Total~", Total)


        EmailContent = EmailContent.Replace("~Delivery method~", DeliveryMethod)
        EmailContent = EmailContent.Replace("~Destination type~", DestinationType)
        EmailContent = EmailContent.Replace("~payment method~", Paymentmethod)
        EmailContent = EmailContent.Replace("~item details~", ItemDetails)
        EmailContent = EmailContent.Replace("~ship to salutation~", shipsalutation)
        EmailContent = EmailContent.Replace("~shippingcustomername~", ShippingName)

        'New Changes Starts here
        EmailContent = EmailContent.Replace("~CompanyFax~", CompanyFax)
        EmailContent = EmailContent.Replace("~CustomerAddress1~", CustomerAddress1)
        EmailContent = EmailContent.Replace("~CustomerAddress2~", CustomerAddress2)
        EmailContent = EmailContent.Replace("~CustomerAddress3~", CustomerAddress3)
        EmailContent = EmailContent.Replace("~CustomerCity~", CustomerCity)
        EmailContent = EmailContent.Replace("~CustomerState~", CustomerState)
        EmailContent = EmailContent.Replace("~CustomerZip~", CustomerZip)
        EmailContent = EmailContent.Replace("~CustomerCountry~", CustomerCountry)
        EmailContent = EmailContent.Replace("~CustomerPhone~", CustomerPhone)
        EmailContent = EmailContent.Replace("~CustomerCell~", CustomerCell)
        EmailContent = EmailContent.Replace("~CustomerEmail~", CustomerEmail)
        EmailContent = EmailContent.Replace("~CardMessage~", CardMessage)

        EmailContent = EmailContent.Replace("~ShippingPhone~", ShippingPhone)
        EmailContent = EmailContent.Replace("~ShippingCell~", ShippingCell)

        IpAddress = Request.ServerVariables("REMOTE_ADDR")
        EmailContent = EmailContent.Replace("~IpAddress~", IpAddress)
        EmailContent = EmailContent.Replace("~WebsiteAddress~", "<a href='" & WebsiteAddress & "'>" & WebsiteAddress & "</a>")
        EmailContent = EmailContent.Replace("~Ship to Attention~", ShipAttention)
        EmailContent = EmailContent.Replace("~Ship to Company~", ShipCompany)

EmailContent = EmailContent.Replace("~Ship to Priority~", Ship_priority)

        Dim OrderPlacedSubject As String
        Dim OrderPlacedContent As String


        If EmailType = "Order Placed" Then
            OrderPlacedSubject = EmailSubject
            OrderPlacedContent = EmailContent


            Dim ToAddress As String = CustomerEmail
            Dim FromAddress As String = CompanyEmail

            'Dim ToAddress As String = "jacob.mathew@iqss.co.in"
            'Dim FromAddress As String = "jacob.mathew@iqss.co.in"

            If CustomerEmail <> "" And CompanyEmail <> "" And OrderPlacedContent <> "" Then

                EmailSending(OrderPlacedSubject, OrderPlacedContent, FromAddress, ToAddress)

            End If
        ElseIf EmailType.Trim() = "Website Order Notification" Then
            OrderPlacedSubject = EmailSubject
            OrderPlacedContent = EmailContent

            Dim ToAddress As String = CompanyEmail
            Dim FromAddress As String = CompanyEmail

            'Dim ToAddress As String = "jacob.mathew@iqss.co.in"

            'Dim FromAddress As String = "jacob.mathew@iqss.co.in"

            If CompanyEmail <> "" And OrderPlacedContent <> "" Then


                EmailSending(OrderPlacedSubject, OrderPlacedContent, FromAddress, ToAddress)

            End If
        End If


    End Sub

    Public Sub EmailSendingWireIn(ByVal OrdNumber As String)
        Exit Sub

        Dim PostOrder As New DAL.CustomOrder()
        Dim Paymentmethod As String = ""
        Dim StrBody As New StringBuilder()
        Dim CompanyName As String = ""

        Dim shipsalutation As String = ""

        Dim ShippingName As String = ""
        Dim ShippingAddress1 As String = ""
        Dim ShippingAddress2 As String = ""
        Dim ShippingAddress3 As String = ""
        Dim ShipCity As String = ""
        Dim ShipState As String = ""
        Dim ShipZip As String = ""
        Dim ShipCountry As String = ""

        Dim CompanyEmail As String = ""

        Dim CustomerReader As SqlDataReader
        CustomerReader = PostOrder.PopulateShippingCustomerDetails(CompanyID, DivisionID, DepartmentID, OrdNumber)
        While CustomerReader.Read()

            Paymentmethod = CustomerReader("PaymentMethodID").ToString()
            CompanyName = CustomerReader("CompanyName").ToString()
            shipsalutation = CustomerReader("ShippingSalutation").ToString()
            ShippingName = CustomerReader("ShippingFirstName").ToString() & " " & CustomerReader("ShippingLastName").ToString()
            ShippingAddress1 = CustomerReader("ShippingAddress1").ToString()
            ShippingAddress2 = CustomerReader("ShippingAddress2").ToString()
            ShippingAddress3 = CustomerReader("ShippingAddress3").ToString()
            ShipCity = CustomerReader("ShippingCity").ToString()
            ShipState = CustomerReader("ShippingState").ToString()
            ShipZip = CustomerReader("ShippingZip").ToString()
            ShipCountry = CustomerReader("ShippingCountry").ToString()
            CompanyEmail = CustomerReader("CompanyEmail").ToString()
        End While


        If Paymentmethod = "Wire In" Then

            StrBody.Append(" <table border='0' width='100%'> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" Dear " + CompanyName + " , ")


            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")

            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")

            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")



            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> This email is to confirm that your order has been delivered by our staff.  Our internal order number is " + OrdNumber + " if you have any follow up on this order. ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> We take great pride in filling your orders and hope we can help you again in the future. ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")


            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")





            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> Sincerely,")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")


            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")

            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> Sunflower Technologies ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> 137 N. Larchmont Blvd Suite 529 ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> Los Angeles, CA 90004 USA ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> Phone : 323-735-7272 ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> Fax : 213-947-1276 ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'>info@sunflowerproduction.com")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'>")
            StrBody.Append("<a href='http://www.quickflora.com'>www.quickflora.com</a>")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")

            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")


            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")


            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")

            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")

            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> This message was generated using the QuickFlora POS Shop Management system. You can find  more Information ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'>about QuickFlora at ")

            StrBody.Append("<a href='http://www.quickflora.com'> www.quickflora.com   </a> ")


            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> This message was transmitted over the Solaris Order Network.")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" <tr> ")
            StrBody.Append(" <td width='4' style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" <td style='font-family: Verdana; font-size: 12px'> ")
            StrBody.Append(" </td> ")
            StrBody.Append(" </tr> ")
            StrBody.Append(" </table> ")
            Dim OrderPlacedContent As String = ""
            Dim OrderPlacedSubject As String = ""


            OrderPlacedContent = StrBody.ToString()
            OrderPlacedSubject = "Delivery confirmation for " + shipsalutation + " " + ShippingName + " in " + ShipCity + "," + ShipState + " from Quickflora"

            Dim FromAddress As String = "info@sunflowerproduction.com"
            Dim ToAddress As String = CompanyEmail

            EmailSending(OrderPlacedSubject, OrderPlacedContent, FromAddress, ToAddress)

        End If
    End Sub

    Public Sub EmailSending(ByVal OrderPlacedSubject As String, ByVal OrderPlacedContent As String, ByVal FromAddress As String, ByVal ToAddress As String)
        Dim mMailMessage As New MailMessage()
        Try

            ' Set the sender address of the mail message
            mMailMessage.From = New MailAddress(FromAddress)
            ' Set the recepient address of the mail message
            mMailMessage.To.Add(New MailAddress(ToAddress))

            'mMailMessage.Bcc.Add(New MailAddress("qfclientorders@sunflowertechnologies.com"))

            ' Set the subject of the mail message
            mMailMessage.Subject = OrderPlacedSubject.ToString()

            ' Set the body of the mail message
            mMailMessage.Body = OrderPlacedContent.ToString()



            ' Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = True


            ' Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal

            ' Instantiate a new instance of SmtpClient
            Dim smtp As New System.Net.Mail.SmtpClient()
            smtp.Host = ConfigurationManager.AppSettings("SystemSMTPServer")

            Try
                'smtp.Send(mMailMessage)
                newmailsending(mMailMessage)

            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try

    End Sub


#End Region


    Protected Sub ImgUpdateSearchitems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgUpdateSearchitems.Click

        'txtComments.Text = "In ord process"

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")


        Dim PopOrderNo As New CustomOrder()
        Dim rs As SqlDataReader
        If IsNumeric(lblOrderNumberData.Text.Trim) = False Then
            rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextOrderNumber")
            While rs.Read()
                OrdNumber = rs("NextNumberValue")
            End While
            rs.Close()
            lblOrderNumberData.Text = OrdNumber
            Session("OrderNumber") = OrdNumber
            txtOrderNumber.Text = lblOrderNumberData.Text
            'EmailSendingWithoutBcc(CompanyID & "- POS new order no" & OrdNumber & " - line number item search 14224", "Existing order - " & lblOrderNumberData.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
        End If


    End Sub

    Sub OrderDetailGridBindAdding()

        Dim OrdNumber As String
        OrdNumber = lblOrderNumberData.Text
        Dim FillItemDetailGrid As New CustomOrder()
        Dim dsTemp As New Data.DataSet
        dsTemp = FillItemDetailGrid.PopulateItemDetailsGrid(CompanyID, DepartmentID, DivisionID, OrdNumber)
        OrderDetailGrid.DataSource = dsTemp
        OrderDetailGrid.DataBind()
        '  OrderDetailGrid.Rows(0).RowState = DataControlRowState.Edit
        Dim i As Integer = OrderDetailGrid.Rows.Count
        If i > 0 Then
            OrderDetailGrid.EditIndex = OrderDetailGrid.Rows(i - 1).RowIndex

        End If
        OrderDetailGridBindAfterAdding()
        CalculationPart()
        PopulatingTaxPercent()

        ''
        ItemSearchPostOrderHeader(OrdNumber)
        ''

        Dim ObjBackOrder As New BackOrder()

        Dim DeliveryStatus As Integer

        Dim InventoryItemID As String = ""
        Dim InventoryQuantity As Decimal
        If Not Session("BackOrderItemID") Is Nothing Then

            InventoryItemID = Session("BackOrderItemID").ToString()

            InventoryQuantity = 1

            DeliveryStatus = ObjBackOrder.CheckOrderQty(CompanyID, DepartmentID, DivisionID, InventoryItemID, InventoryQuantity)

            If DeliveryStatus = 0 Then
                lblInventoryStatus.Text = InventoryItemID + " is out of Stock"
                pnlPricerange.Visible = True

            Else
                lblInventoryStatus.Text = ""
                pnlPricerange.Visible = False


            End If
            Session.Remove("BackOrderItemID")
        End If
    End Sub
#Region "ItemSearchPostOrderHeader"
    Protected Sub ItemSearchPostOrderHeader(ByVal OrderNo As String)
        Dim PopOrderNo As New CustomOrder()
        Dim HeaderDetailSubmit As New CustomOrder()
        Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
        'Dim filters As EnterpriseCommon.Core.FilterSet
        'filters = CType(Session("SessionFilters"), EnterpriseCommon.Core.FilterSet)
        'CompanyID = filters!CompanyID
        'DivisionID = filters!DivisionID
        'DepartmentID = filters!DepartmentID

        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        HeaderDetailSubmit.CompanyID = CompanyID
        HeaderDetailSubmit.DivisionID = DivisionID
        HeaderDetailSubmit.DepartmentID = DepartmentID

        HeaderDetailSubmit.OrderNumber = OrderNo



        ''new changes
        If drpTransaction.SelectedValue = "Wire Out" Or drpTransaction.SelectedValue = "Wire_Out" Or drpShipMethod.SelectedValue = "Wire Out" Or drpShipMethod.SelectedValue = "Wire_Out" Then
            HeaderDetailSubmit.TransactionTypeID = "Wire_Out"
        Else
            HeaderDetailSubmit.TransactionTypeID = drpTransaction.SelectedValue
        End If
        ''new changes


        HeaderDetailSubmit.OrderTypeID = drpOrderTypeIDData.SelectedValue

        'HeaderDetailSubmit.OrderDate = lblOrderDate.Text
        'HeaderDetailSubmit.OrderShipDate = Convert.ToDateTime(txtDeliveryDate.Text)
        If drpCustomerID.SelectedValue = "Specify Customer" Then
            HeaderDetailSubmit.CustomerID = txtSpecifyCustomer.Text
        ElseIf lkpCustomerID.Value <> "" Then
            'New Code Added 0n 08/Aug/2007

            If lkpCustomerID.Text <> "" Then
                HeaderDetailSubmit.CustomerID = lkpCustomerID.Text
            Else
                HeaderDetailSubmit.CustomerID = lkpCustomerID.Value
            End If

        Else
            HeaderDetailSubmit.CustomerID = txtCustomerTemp.Text
        End If
        'HeaderDetailSubmit.CustomerID = txtCustomerTemp.Text
        HeaderDetailSubmit.EmployeeID = drpEmployeeID.SelectedValue
        HeaderDetailSubmit.Assignedto = drpAssignedto.SelectedValue
        HeaderDetailSubmit.ShipMethodID = drpShipMethod.SelectedValue
        HeaderDetailSubmit.ShippingAddress1 = txtShippingAddress1.Text
        HeaderDetailSubmit.ShippingAddress2 = txtShippingAddress2.Text
        HeaderDetailSubmit.ShippingAddress3 = txtShippingAddress3.Text
        HeaderDetailSubmit.ShippingCity = txtShippingCity.Text

        HeaderDetailSubmit.ShippingCountry = drpShipCountry.SelectedValue
        If drpShipCountry.SelectedValue = "US" Then
            HeaderDetailSubmit.ShippingState = drpShippingState.SelectedValue
        ElseIf drpShipCountry.SelectedValue = "CD" Then
            HeaderDetailSubmit.ShippingState = drpShippingState.SelectedValue
        Else
            HeaderDetailSubmit.ShippingState = txtShippingState.Text
        End If

        'HeaderDetailSubmit.ShippingState = drpShippingState.SelectedValue

        HeaderDetailSubmit.ShippingZip = txtShippingZip.Text
        HeaderDetailSubmit.PaymentMethodID = drpPaymentType.SelectedValue
        HeaderDetailSubmit.CreditCardType = drpCardType.SelectedValue
        HeaderDetailSubmit.CreditCardNumber = txtCard.Text
        'HeaderDetailSubmit.CreditCardExpDate = txtEXP.Text
        HeaderDetailSubmit.CreditCardExpDate = drpExpirationDate.SelectedValue

        'HeaderDetailSubmit.CreditCardCSVNumber = "" ' txtCSV.Text

        If checkstatusPaymentAccountsStatus() Then
            HeaderDetailSubmit.CreditCardCSVNumber = txtCSV.Text
        Else
            HeaderDetailSubmit.CreditCardCSVNumber = "" 'txtCSV.Text
        End If

        HeaderDetailSubmit.CreditCardApprovalNumber = txtApproval.Text
        HeaderDetailSubmit.CreditCardValidationCode = txtValidation.Text
        HeaderDetailSubmit.CreditCardBillToZip = txtBillZipCode.Text
        HeaderDetailSubmit.FraudRating = txtFraudRating.Text
        HeaderDetailSubmit.IpAddress = txtIpAddress.Text

        HeaderDetailSubmit.ShippingCell = txtShipCustomerCell.Text
        HeaderDetailSubmit.ShippingExt = txtShipExt.Text
        HeaderDetailSubmit.ShippingFax = txtShipCustomerFax.Text
        HeaderDetailSubmit.ShippingPhone = txtShipCustomerPhone.Text
        HeaderDetailSubmit.ShippingAttention = txtShippingAttention.Text

        HeaderDetailSubmit.OccasionCode = drpOccasionCode.Text
        HeaderDetailSubmit.Priority = drpPriorirty.SelectedValue

        HeaderDetailSubmit.DestinationType = drpDestinationType.SelectedValue
        HeaderDetailSubmit.OrderShipDate = txtDeliveryDate.Text
        HeaderDetailSubmit.ShippingFirstName = txtShippingName.Text
        HeaderDetailSubmit.ShippingLastName = txtShippingLastName.Text
        HeaderDetailSubmit.ShippingCompany = txtShipCompany.Text

        'edited by jacob  on 10/12/2007
        HeaderDetailSubmit.ShippingSalutation = IIf(drpShipCustomerSalutation.SelectedValue = "0", "", drpShipCustomerSalutation.SelectedValue)

        HeaderDetailSubmit.WarehouseID = drpLocation.SelectedValue
        HeaderDetailSubmit.ProjectID = drpProject.SelectedValue


        HeaderDetailSubmit.CheckID = txtID.Text
        HeaderDetailSubmit.CheckNumber = txtCheck.Text
        HeaderDetailSubmit.GiftCertificate = txtGiftCertificate.Text
        HeaderDetailSubmit.Coupon = txtCoupon.Text




        ' HeaderDetailSubmit.WireService = txtWireService.Text
        HeaderDetailSubmit.WireService = drpWire.SelectedValue
        HeaderDetailSubmit.WireCode = txtCode.Text
        HeaderDetailSubmit.WireRefernceID = txtReference.Text
        HeaderDetailSubmit.WireTransmitMethod = txtTransmitMethod.Text



        HeaderDetailSubmit.DriverRouteInfo = txtDriverRouteInfo.Text
        HeaderDetailSubmit.InternalNotes = txtInternalNotes.Text

        HeaderDetailSubmit.SubTotal = txtSubtotal.Text
        HeaderDetailSubmit.TaxGroupID = drpTaxes.SelectedValue
        If drpPaymentType.SelectedValue = "Wire In" Then

            HeaderDetailSubmit.TaxPercent = 0.0

        Else
            HeaderDetailSubmit.TaxPercent = txtTaxPercent.Text.Replace("%", "")

        End If
        HeaderDetailSubmit.TaxAmount = txtTax.Text
        HeaderDetailSubmit.Discounts = txtDiscountAmount.Text
        HeaderDetailSubmit.PO = txtPO.Text
        HeaderDetailSubmit.Total = txtTotal.Text


        '  txtIpAddress.Text
        ' txtFraudRating.Text
        ' HeaderDetailSubmit.CreditCardBillToZip = txtBillZipCode.Text


        If lblDiscountCodeAmount.Text.Trim() <> "" Then
            If IsNumeric(lblDiscountCodeAmount.Text.Trim()) Then
                HeaderDetailSubmit.DiscountCouponAmount = lblDiscountCodeAmount.Text.Trim()
            Else
                HeaderDetailSubmit.DiscountCouponAmount = 0
            End If
        End If
        HeaderDetailSubmit.Coupon = txtDiscountCode.Text


        HeaderDetailSubmit.AddEdit = 1

        'Adding Customised Grid Values
        HeaderDetailSubmit.AddEditHeaderDetails()
        Session("AddEdit") = ""
    End Sub

    Public Function checkstatusPaymentAccountsStatus() As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "SELECT CompanyID  from [PaymentAccountsStatus] 		Where  isnull([active],0) = 1"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            da.SelectCommand = com
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return False
        End Try
        Return False
    End Function

#End Region

    Protected Sub btncustsearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btncustsearch.Click
        Dim TempCustomerID As String = txtcustomersearch.Text
        lblsearchcustomermsg.ForeColor = Drawing.Color.Black

        If txtcustomersearch.Text.Trim = "" Then
            Exit Sub
        Else
            lblsearchcustomermsg.Text = "Please first select from search list"
            lblsearchcustomermsg.ForeColor = Drawing.Color.Red
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
        txtSpecifyCustomer.Text = CustomerID


        CompanyID = Session("CompanyID")
        DivisionID = Session("DivisionID")
        DepartmentID = Session("DepartmentID")
        EmployeeID = Session("EmployeeID")

        Dim CheckForCustomerID As New CustomOrder()
        Dim ReturnValue As String = CheckForCustomerID.CheckCustomerIDExists(CompanyID, DepartmentID, DivisionID, CustomerID)
        If ReturnValue > 0 Then
            PopulateCustomerInfo(CustomerID)
            txtSpecifyCustomer.Text = CustomerID
            txtCustomerTemp.Text = CustomerID
            txtcustomersearch.Text = ""
            drpCustomerID.Visible = False
            txtCustomerTemp.Visible = True
            lnkCustomerSearch.Visible = False
            lnkBackToOption.Visible = True
            lblsearchcustomermsg.Text = ""
            lnkShipAddress.Attributes.Add("onclick", "Javascript:window.open('ShipAddress.aspx?CustomerID=" + txtCustomerTemp.Text + " ','MyWindow','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400');return false; ")

            If IsNumeric(lblOrderNumberData.Text.Trim) = False Then
                Dim PopOrderNo As New CustomOrder()
                Dim rs As SqlDataReader
                rs = PopOrderNo.GetNextOrdNumber(CompanyID, DepartmentID, DivisionID, "NextOrderNumber")
                While rs.Read()
                    OrdNumber = rs("NextNumberValue")

                End While
                rs.Close()
                lblOrderNumberData.Text = OrdNumber
                Session("OrderNumber") = OrdNumber
                ' EmailSendingWithoutBcc(CompanyID & "- POS new order no" & OrdNumber & " - line number customer search 14224", "Existing order - " & lblOrderNumberData.Text & " - " & Date.Now, "support@quickflora.com", "imtiyazsir@gmail.com")
            End If

        Else
            lblsearchcustomermsg.Text = "Your search - <b>" & txtcustomersearch.Text.Trim & "</b> - did not match any customers."
            lblsearchcustomermsg.ForeColor = Drawing.Color.Red
            Exit Sub
            txtVendorName.Text = ""
            txtCustomerFirstName.Text = ""
            txtCustomerTemp.Text = ""
            Session("CustomerID") = ""
            txtSpecifyCustomer.Text = ""
            txtCustomerLastName.Text = ""
            txtAttention.Text = ""
            txtCustomerAddress1.Text = ""
            txtCustomerAddress2.Text = ""
            txtCustomerAddress3.Text = ""
            txtCustomerCity.Text = ""
            txtCustomerFax.Text = ""
            txtCustomerPhone.Text = ""
            txtCustomerEmail.Text = ""
            txtCreditLimit.Text = ""
            txtAccountStatus.Text = ""
            txtCustomerSince.Text = ""
            txtDiscounts.Text = ""
            txtCreditComments.Text = ""
            txtCustomerZip.Text = ""
            txtCustomerCell.Text = ""
            txtExt.Text = ""
            txtCompany.Text = ""


            lblAccountStatus.Text = ""
            lblCreditLimit.Text = ""
            lblCreditComments.Text = ""
            lblYTDOrders.Text = ""
            lblAverageSale.Text = ""
            lblSalesLifeTime.Text = ""
            lblCustomerSince.Text = ""
            lblMemberPoints.Text = ""
            lblDiscounts.Text = ""
            lblCustomerRank.Text = ""


        End If

        BindGrid()

        AddEditItemDetails()

        Dim onloadScript As String = ""
        onloadScript = onloadScript & "<script type='text/javascript'>" & vbCrLf
        onloadScript = onloadScript & "CcustomersearchcloseProcess();" & vbCrLf
        onloadScript = onloadScript & "<" & "/" & "script>"
        ' Register script with page 
        Me.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())

    End Sub



    Public Sub updatemulticard(ByVal OrdNumber As String)
        If drpPaymentType.SelectedValue.ToLower = "Credit Card".ToLower Then

            Dim objCustomerCreditCards As New clsCustomerCreditCards
            objCustomerCreditCards.CompanyID = CompanyID
            objCustomerCreditCards.DivisionID = DivisionID
            objCustomerCreditCards.DepartmentID = DepartmentID
            objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue

            ''These are new lines
            objCustomerCreditCards.CustomerID = txtCustomerTemp.Text
            objCustomerCreditCards.BillingCustomerName = txtCustomerFirstName.Text & "-" & txtCustomerLastName.Text
            objCustomerCreditCards.BillingCustomerAddress1 = txtCustomerAddress1.Text
            objCustomerCreditCards.BillingCustomerAddress2 = txtCustomerAddress2.Text
            objCustomerCreditCards.BillingCustomerAddress3 = txtCustomerAddress3.Text
            objCustomerCreditCards.BillingCustomerCity = txtCustomerCity.Text

            objCustomerCreditCards.BillingCustomerState = txtCustomerState.Text
            If txtCustomerState.Text.Trim = "" Then
                objCustomerCreditCards.BillingCustomerState = drpState.SelectedValue
            End If
            objCustomerCreditCards.BillingCustomerZip = txtCustomerZip.Text
            objCustomerCreditCards.BillingCustomerCountry = drpCountry.SelectedValue
            objCustomerCreditCards.CreditCardTypeID = drpCardType.SelectedValue
            objCustomerCreditCards.CreditCardName = objCustomerCreditCards.BillingCustomerName
            objCustomerCreditCards.CreditCardNumber = txtCard.Text
            objCustomerCreditCards.CreditCardExpDate = drpExpirationDate.SelectedValue
            objCustomerCreditCards.CreditCardCSVNumber = "" ' txtCSV.Text
            objCustomerCreditCards.Priority = 0
            ''These are new files

            If drpstoredcc.SelectedValue = "Other" Then
                If chkstoreCC.Checked Then
                    objCustomerCreditCards.InsertCustomerCardDetail()
                End If
            End If

            ''These are new lines
            If drpstoredcc.SelectedValue <> "Other" Then
                objCustomerCreditCards.OrderNumber = OrdNumber

                objCustomerCreditCards.BillingCustomerCell = txtCustomerCell.Text
                objCustomerCreditCards.BillingCustomerEmail = txtCustomerEmail.Text
                objCustomerCreditCards.BillingCustomerFax = txtCustomerFax.Text
                objCustomerCreditCards.BillingCustomerPhone = txtCustomerPhone.Text
                objCustomerCreditCards.BillingCustomerPhoneExt = txtExt.Text

                objCustomerCreditCards.linenumber = drpstoredcc.SelectedValue
                objCustomerCreditCards.updatebillingCC = chkupdatebilling.Checked
                objCustomerCreditCards.UpdateStoredCreditCardOrderNumber()
                objCustomerCreditCards.UpdateBillingOrderNumber()
            End If
            ''These are new lines

        End If

    End Sub


    Public Function CustomerCardDetails(ByVal CreditCardNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from CustomerCardDetail Where IsNull(CreditCardNumber,'') =@f3 AND CompanyID=@f0 and DivisionID=@f1 and DepartmentID=@f2"
        Dim da As New SqlDataAdapter
        Dim com As SqlCommand
        com = New SqlCommand(ssql, connec)
        Try

            com.Parameters.Add(New SqlParameter("@f0", SqlDbType.NVarChar, 36)).Value = Me.CompanyID
            com.Parameters.Add(New SqlParameter("@f1", SqlDbType.NVarChar, 36)).Value = Me.DivisionID
            com.Parameters.Add(New SqlParameter("@f2", SqlDbType.NVarChar, 36)).Value = Me.DepartmentID
            com.Parameters.Add(New SqlParameter("@f3", SqlDbType.NVarChar)).Value = CreditCardNumber

            da.SelectCommand = com
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            'HttpContext.Current.Response.Write(msg)
            Return dt
        End Try
    End Function






    Public Function InsertEMV(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal Active As Boolean, ByVal TerminalName As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String
        qry = "insert into TransentryPayMentRequest_PRD( CompanyID, DivisionID, DepartmentID, OrderNumber, Active, TerminalName  " _
        & "  ) values(@CompanyID, @DivisionID, @DepartmentID, @OrderNumber, @Active, @TerminalName)"
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)


        com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 50)).Value = CompanyID
        com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 50)).Value = DivisionID
        com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 50)).Value = DepartmentID
        com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 50)).Value = OrderNumber
        com.Parameters.Add(New SqlParameter("@Active", SqlDbType.Bit)).Value = Active
        com.Parameters.Add(New SqlParameter("@TerminalName", SqlDbType.NVarChar, 50)).Value = TerminalName



        com.Connection.Open()
        com.ExecuteNonQuery()
        com.Connection.Close()

        Return True
        Try
        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function


    Public Function UpdateEMV(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String, ByVal Active As Boolean, ByVal TerminalName As String) As Boolean
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim connec As New SqlConnection(constr)
        Dim qry As String

        qry = "Update TransentryPayMentRequest_PRD SET  Active = @Active, TerminalName = @TerminalName   Where CompanyID=@CompanyID AND DivisionID=@DivisionID AND DepartmentID=@DepartmentID AND OrderNumber=@OrderNumber "
        Dim com As SqlCommand
        com = New SqlCommand(qry, connec)
        Try

            com.Parameters.Add(New SqlParameter("@CompanyID", SqlDbType.NVarChar, 50)).Value = CompanyID
            com.Parameters.Add(New SqlParameter("@DivisionID", SqlDbType.NVarChar, 50)).Value = DivisionID
            com.Parameters.Add(New SqlParameter("@DepartmentID", SqlDbType.NVarChar, 50)).Value = DepartmentID
            com.Parameters.Add(New SqlParameter("@OrderNumber", SqlDbType.NVarChar, 50)).Value = OrderNumber
            com.Parameters.Add(New SqlParameter("@Active", SqlDbType.Bit)).Value = Active
            com.Parameters.Add(New SqlParameter("@TerminalName", SqlDbType.NVarChar, 50)).Value = TerminalName

            com.Connection.Open()
            com.ExecuteNonQuery()
            com.Connection.Close()
            Return True

        Catch ex As Exception
            Dim msg As String
            msg = ex.Message
            HttpContext.Current.Response.Write(msg)
            Return False

        End Try
    End Function


    Public Function CheckEMVDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from TransentryPayMentRequest_PRD Where CompanyID='" & CompanyID & "' AND DivisionID='" & DivisionID & "' AND DepartmentID='" & DepartmentID & "' AND OrderNumber='" & OrderNumber & "' "
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Protected Sub btnemv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnemv.Click

        AddEditItemDetails()

        HD_Ordernumber.Value = lblOrderNumberData.Text
        Dim r As New Random
        HidalertText.Focus()
        ClientScript.RegisterStartupScript(Me.GetType(), "Paymentrequest" & r.Next(100003, 200004).ToString(), "calldoPollTimePayment();", True)


        Dim TerminalID As String = ""
        Try
            TerminalID = Session("TerminalID")
        Catch ex As Exception

        End Try

        'txtCard.Text = "XXXXXXXXXXXXXXXX"
        'txtCSV.Text = "XXX"

        lblCCMessage.Text = ""

        Dim dt As New DataTable
        dt = CheckEMVDetails(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblOrderNumberData.Text.Trim)
        If (dt.Rows.Count = 0) Then
            InsertEMV(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblOrderNumberData.Text.Trim, True, TerminalID)
        Else
            UpdateEMV(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblOrderNumberData.Text.Trim, True, TerminalID)
        End If

        btnemv.Enabled = False
        btnemvres.Enabled = True
        'txtCheck.ReadOnly = True
        'txtID.ReadOnly = True

    End Sub

    Public Function CheckEMVOrderDetails(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal OrderNumber As String) As DataTable
        Dim constr As String = ConfigurationManager.AppSettings("ConnectionString")

        Dim ssql As String = ""
        Dim dt As New DataTable()
        ssql = "select * from orderheader Where CompanyID='" & CompanyID & "' AND DivisionID='" & DivisionID & "' AND DepartmentID='" & DepartmentID & "' AND OrderNumber='" & OrderNumber & "' "
        Dim da As New SqlDataAdapter(ssql, constr)
        da.Fill(dt)
        Return dt
    End Function

    Protected Sub btnemvres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnemvres.Click
        btnemvres.Enabled = False
        ' txtInternalNotes.Text = "Response"
        Dim dt As New DataTable
        dt = CheckEMVOrderDetails(Me.CompanyID, Me.DivisionID, Me.DepartmentID, lblOrderNumberData.Text.Trim)

        If (dt.Rows.Count > 0) Then
            ' txtInternalNotes.Text = "Response In"
            Try
                txtID.Text = dt.Rows(0)("CheckID").ToString()
            Catch ex As Exception
                txtApproval.Text = ex.Message
            End Try

            Try
                txtCheck.Text = dt.Rows(0)("CheckNumber").ToString()
            Catch ex As Exception

            End Try
            'EMVPaymentError


            lblCCMessage.Text = txtCheck.Text & "<br>" & dt.Rows(0)("EMVPaymentError").ToString()
            lblCCMessage.Visible = True


            btnemv.Enabled = True

        End If


        If txtCheck.Text = "Approved" Then
            lblCCMessage.Font.Size = "18"
            lblCCMessage.ForeColor = Drawing.Color.Green
            btnemv.Enabled = False
        Else
            lblCCMessage.Font.Size = "18"
            lblCCMessage.ForeColor = Drawing.Color.Red
        End If


    End Sub

    Private Sub GetZoneID(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal LocationID As String)

        If Not objZone.CheckAddressZoneVerification(CompanyID, DivisionID, DepartmentID) Then
            btnVerifyAddress.Visible = False
            btnZoneMap.Visible = False
        End If

        Session("AllowZoneDeliveryChargeFilling") = objZone.CheckZoneChargeSetting(CompanyID, DivisionID, DepartmentID)

        If Not objZone.CheckAddressZoneSetting(CompanyID, DivisionID, DepartmentID) Then
            Exit Sub
        End If


        Dim ds As New DataSet
        ds = objZone.FillZoneList(CompanyID, DivisionID, DepartmentID, LocationID)


        If ds.Tables(0).Rows.Count > 0 Then
            drpZone.Items.Clear()
            drpZone.SelectedIndex = -1

            drpZone.DataSource = ds
            drpZone.DataTextField = "ZoneName"
            drpZone.DataValueField = "ZoneID"
            drpZone.DataBind()
            drpZone.Items.Remove("")
            drpZone.Items.Insert(0, (New ListItem("Select", "0")))
        End If
        

    End Sub

    Protected Sub drpZone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpZone.SelectedIndexChanged

        ''If CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "BrownstheFloristLtdV8W1G9" Or CompanyID = "TrainingBrownsTheFlorist" Or CompanyID = "Greene and Greene" Or CompanyID = "Quickflora-Canada" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-CanadaTest" Or CompanyID = "Quickflora-USATest" Then
        If Session("AllowZoneDeliveryChargeFilling") Then
            FillDeliveryByZone(drpZone.SelectedValue, cmblocationid.SelectedValue)
        End If

        'If objZone.CheckAddressZoneVerification(CompanyID, DivisionID, DepartmentID) Then
        '    If Session("SupressEvent") Then
        '        Session("SupressEvent") = False
        '        TypedSearchOntextChange = True
        '        Exit Sub
        '    End If

        '    Dim ds As New DataSet
        '    Dim obj As New clsAddressZone

        '    ds = obj.GetAddressListByZoneID(CompanyID, DivisionID, DepartmentID, drpZone.SelectedValue)

        '    If ds.Tables(0).Rows.Count > 0 Then
        '        If ds.Tables(0).Rows.Count = 1 Then

        '            txtShippingAddress1.Text = ds.Tables(0).Rows(0)("StreetNumber").ToString + " " + ds.Tables(0).Rows(0)("StreetName").ToString
        '            txtShippingAddress2.Text = ds.Tables(0).Rows(0)("StreetTypeCode").ToString + " " + ds.Tables(0).Rows(0)("StreetDirCode").ToString
        '            txtShippingAddress3.Text = ds.Tables(0).Rows(0)("StreetSuffix").ToString
        '            txtShippingCity.Text = ds.Tables(0).Rows(0)("City").ToString

        '            drpShippingState.Items.Clear()

        '            PopulateState(CompanyID, DivisionID, DepartmentID, ds.Tables(0).Rows(0)("CountryID").ToString)

        '            drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(ds.Tables(0).Rows(0)("Province").ToString.ToUpper))

        '            txtShippingZip.Text = ds.Tables(0).Rows(0)("PostalCode").ToString

        '            drpShipCountry.SelectedIndex = drpShipCountry.Items.IndexOf(drpShipCountry.Items.FindByValue(ds.Tables(0).Rows(0)("CountryID").ToString.ToUpper))
        '            Session("SearchType") = ""
        '        Else

        '            'DOING NOTHING TO OPEN MULTIUPLE RECRODS 
        '            Dim objR As New Random()
        '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindow" + Convert.ToString(objR.Next(1000)), _
        '                "window.open('AddressListSearch.aspx?ST=drpselection&ZoneID=" + drpZone.SelectedValue + "','','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=1100,height=650,Left=100,top=50,screenX=50,screenY=50,alwaysRaised=yes');", True)

        '            txtShippingZip.Text = ""

        '        End If
        '    End If
        'End If
    End Sub

    Public Sub GetAddressListByTypedSearch(ByVal TypedSearch As Boolean)

        If TypedSearchOntextChange And 1 = 2 Then
            If objZone.CheckAddressZoneVerification(CompanyID, DivisionID, DepartmentID) Then
                Dim ds As New DataSet
                Dim obj As New clsAddressZone

                ds = obj.GetAddressListByTypedSearch(CompanyID, DivisionID, DepartmentID, txtShippingAddress1.Text.Trim, txtShippingAddress2.Text.Trim, txtShippingAddress3.Text.Trim, txtShippingCity.Text.Trim, "", "", IIf(drpZone.SelectedValue <> "0", drpZone.SelectedValue, "")) 'IIf(drpZone.SelectedValue <> "0", txtShippingZip.Text.Trim, ""), IIf(drpZone.SelectedValue <> "0", drpZone.SelectedValue, ""))

                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows.Count = 1 Then

                        txtShippingAddress1.Text = ds.Tables(0).Rows(0)("StreetNumber").ToString + " " + ds.Tables(0).Rows(0)("StreetName").ToString
                        txtShippingAddress2.Text = ds.Tables(0).Rows(0)("StreetTypeCode").ToString + " " + ds.Tables(0).Rows(0)("StreetDirCode").ToString
                        txtShippingAddress3.Text = ds.Tables(0).Rows(0)("StreetSuffix").ToString
                        txtShippingCity.Text = ds.Tables(0).Rows(0)("City").ToString

                        drpShippingState.Items.Clear()

                        PopulateState(CompanyID, DivisionID, DepartmentID, ds.Tables(0).Rows(0)("CountryID").ToString)

                        drpShipCountry.SelectedIndex = drpShipCountry.Items.IndexOf(drpShipCountry.Items.FindByValue(ds.Tables(0).Rows(0)("CountryID").ToString.ToUpper))
                        drpZone.SelectedIndex = drpZone.Items.IndexOf(drpZone.Items.FindByValue(ds.Tables(0).Rows(0)("ZoneID").ToString.ToUpper))
                        Session("ZoneChange") = True
                        drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(ds.Tables(0).Rows(0)("Province").ToString.ToUpper))

                        txtShippingZip.Text = ds.Tables(0).Rows(0)("PostalCode").ToString

                    Else

                        Dim objR As New Random()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindowTypesSearch" + Convert.ToString(objR.Next(1000)), _
                            "window.open('AddressListSearch.aspx?ST=typedsearch&Add1=" + txtShippingAddress1.Text.Trim + _
                            "&Add2=" + txtShippingAddress2.Text.Trim + _
                            "&Add3=" + txtShippingAddress3.Text.Trim + _
                            "&City=" + txtShippingCity.Text.Trim + _
                            "&Province=" + drpShippingState.SelectedValue + _
                            "&PostalCode=" + txtShippingZip.Text + _
                            "&Country=" + drpShipCountry.SelectedValue + _
                            "&ZoneID=" + IIf(drpZone.SelectedValue <> "0", drpZone.SelectedValue, "") + "','','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=1100,height=650,Left=100,top=50,screenX=50,alwaysRaised=yes');", True)

                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub txtShippingAddress1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShippingAddress1.TextChanged

        'TypedSearchOntextChange = True
        Session("SupressEvent") = True

    End Sub


    Private Function VerifyAddressWithAllValues() As Integer

        'Dim ds As New DataSet

        'Dim address As String = txtShippingAddress1.Text.Trim + " " + txtShippingAddress2.Text.Trim + " " + txtShippingAddress3.Text.Trim

        'ds = objZone.VerifyAddressWithAllValues(address.Trim.Replace(" ", ","), txtShippingCity.Text.Trim, drpShippingState.SelectedValue, txtShippingZip.Text, drpZone.SelectedValue)

        'If ds.Tables(0).Rows.Count > 0 Then
        '    Return ds.Tables(0).Rows.Count
        'Else
        '    Return 0
        'End If

    End Function

    Private Function VerifyAddressWithAllValues2() As Integer

        'Dim ds As New DataSet

        'Dim address As String = txtShippingAddress1.Text.Trim + " " + txtShippingAddress2.Text.Trim + " " + txtShippingAddress3.Text.Trim

        'ds = objZone.VerifyAddressWithAllValues2(address.Trim.Replace(" ", ","), txtShippingCity.Text.Trim, drpShippingState.SelectedValue, txtShippingZip.Text, drpZone.SelectedValue)

        'If ds.Tables(0).Rows.Count > 0 Then
        '    Return ds.Tables(0).Rows.Count
        'Else
        '    Return 0
        'End If

    End Function

    Protected Sub btnVerifyAddress_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVerifyAddress.Click

        Dim wc As New System.Net.WebClient()
        Dim nvc As New System.Collections.Specialized.NameValueCollection()
        Dim byteBuffer As Byte()
        Dim stringBuffer As String = ""

        Dim RequestURL As String = ""

        Try
		
            RequestURL = objZone.GetAddressVerificationURL(CompanyID, DivisionID, DepartmentID)
        
            If RequestURL = "" Then
                RequestURL = "http://quickfloraav.com/maps/JsonAjax_UC.php"
            End If

            RequestURL = RequestURL + "?address=" + txtShippingAddress1.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            RequestURL = RequestURL + "," + txtShippingAddress2.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            RequestURL = RequestURL + "," + txtShippingAddress3.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            RequestURL = RequestURL + "," + txtShippingCity.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            RequestURL = RequestURL + "," + txtShippingZip.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            RequestURL = RequestURL + "," + drpShipCountry.SelectedItem.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")

            If drpShipCountry.SelectedValue = "US" Then
                RequestURL = RequestURL + "&Cnt=Usa"
            ElseIf drpShipCountry.SelectedValue = "CD" Then
                RequestURL = RequestURL + "&Cnt=Canada"
            End If

            'ElseIf CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-USATest" Then

            'If RequestURL = "" Then
            '    RequestURL = "http://quickfloraav.com/maps/JsonAjax.php"
            'End If

            'RequestURL = RequestURL + "?address=" + txtShippingAddress1.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            'RequestURL = RequestURL + "," + txtShippingAddress2.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            'RequestURL = RequestURL + "," + txtShippingAddress3.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            'RequestURL = RequestURL + "," + txtShippingCity.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            'RequestURL = RequestURL + "," + txtShippingZip.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")
            'RequestURL = RequestURL + "," + drpShipCountry.SelectedItem.Text.Replace("#", "").Replace("@", "").Replace("%", "").Replace("&", "").Replace("~", "").Replace("!", "").Replace("^", "")

            'If drpShipCountry.SelectedValue = "US" Then
            '    RequestURL = RequestURL + "&Cnt=USA"
            'End If

            'End If
            wc.Headers("Accept") = "application/x-ms-application, image/jpeg, application/xaml+xml, image/gif, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*"
            wc.Headers("User-Agent") = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MDDC)"


            byteBuffer = wc.UploadValues(RequestURL, "POST", nvc)
            stringBuffer = System.Text.Encoding.Default.GetString(byteBuffer)

        Catch ex As Exception
            Exit Sub
        End Try

        Dim json As New JavaScriptSerializer
        Dim obj As New AddressVerifyClass

        Try
            obj = json.Deserialize(Of AddressVerifyClass)(stringBuffer)
        Catch ex As Exception
            Exit Sub
        End Try

        Dim objR As New Random()

        If obj.Status.ToLower = "ok" Then

            Dim Country As String = drpShipCountry.Items.FindByText(StrConv(obj.Country, VbStrConv.ProperCase)).Value

            Dim State As String = ""

            Dim dsState As New DataSet
            Dim objAZ As New clsAddressZone
            dsState = objAZ.GetStateCodeByName(CompanyID, DivisionID, DepartmentID, Country, obj.State)

            If dsState.Tables(0).Rows.Count > 0 Then
                State = dsState.Tables(0).Rows(0)("StateID")
            End If

            Session("DoNotCallCountry") = True

            Dim OldZone As String = drpZone.SelectedValue


            Dim AddressRowID As String
            AddressRowID = objAZ.InsertVerifiedAddress(CompanyID, DivisionID, DepartmentID, obj.Zone, obj.Number, obj.Street, obj.City, State, obj.State, obj.Zip, Country, obj.Country, obj.Lat, obj.Lon)

	
	Try	
	    objAZ.UpdateLatLongOnOrder(CompanyID, DivisionID, DepartmentID, lblOrderNumberData.Text.Trim, obj.Lat, obj.Lon)
	Catch ex As Exception

        End Try


            btnZoneMap.Visible = True
            btnAcceptAddres.Visible = True
            btnRejectAddress.Visible = True

            btnAcceptAddres.CommandArgument = AddressRowID
            btnRejectAddress.CommandArgument = obj.Address

            txtDriverRouteInfo.Text = txtDriverRouteInfo.Text.Replace(obj.Address, "")
            txtDriverRouteInfo.Text = txtDriverRouteInfo.Text.Replace("Address verified by " + drpEmployeeID.SelectedItem.Text, "")
            txtDriverRouteInfo.Text = txtDriverRouteInfo.Text + vbNewLine + obj.Address
            txtDriverRouteInfo.BackColor = Drawing.Color.Green
            txtDriverRouteInfo.ForeColor = Drawing.Color.White

        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "UnVerified" + Convert.ToString(objR.Next(1000)), _
            "alert('Address Verification does not verify this address.');", True)
        End If


    End Sub

    Protected Sub txtShippingCity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShippingCity.TextChanged

        If PostalCodeDropDown Then
            'TypedCitySerch = True
        End If

    End Sub

    Public Sub GetPostalCodeDropDownOnCitySearch()

        'If TypedCitySerch Then
        '    GetPostalCodeDropwDown("", "", "", txtShippingCity.Text, "", IIf(drpZone.SelectedValue <> "0", drpZone.SelectedValue, ""))
        'End If

    End Sub

    Private Sub PopulateState(ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal CountryID As String)

        drpShippingState.Items.Clear()

        Dim dsState As New DataSet
        dsState = objZone.GetState(CompanyID, DivisionID, DepartmentID, CountryID)

        If dsState.Tables(0).Rows.Count > 0 Then
            drpShippingState.Items.Clear()
            drpShippingState.DataSource = dsState
            drpShippingState.DataTextField = "StateID"
            drpShippingState.DataValueField = "StateID"
            drpShippingState.DataBind()
            '    drpShippingState.Items.Remove("")
        End If

    End Sub

    Protected Sub drpShipCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpShipCountry.SelectedIndexChanged

         Dim drpCountry As String = drpShipCountry.SelectedValue

        If drpCountry = "US" Or drpCountry = "CD" Then
            drpShippingState.Visible = True
            txtShippingState.Visible = False

            'Dim State As String = drpShippingState.SelectedValue
            Dim State As String = hiddenState.Value
            PopulateState(CompanyID, DivisionID, DepartmentID, drpShipCountry.SelectedValue)
            drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(State.ToUpper))

        Else
            drpShippingState.Visible = False
            txtShippingState.Visible = True
            txtCustomerState.Text = ""
        End If

    End Sub

    Protected Sub ImgPostalCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImgPostalCode.Click

        If objZone.CheckAddressZoneVerification(CompanyID, DivisionID, DepartmentID) And Session("AllowZoneDeliveryChargeFilling") Then
            FillDeliveryByZone(drpZone.SelectedValue, cmblocationid.SelectedValue)
        End If

    End Sub

    Protected Sub cmblocationid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblocationid.SelectedIndexChanged
        Session("OrderLocationid") = cmblocationid.SelectedValue

	If Not objZone.CheckAddressZoneVerification(CompanyID, DivisionID, DepartmentID) Then
            Exit Sub
        End If

        Try

            Dim Zone As String = drpZone.SelectedValue

            GetZoneID(CompanyID, DivisionID, DepartmentID, cmblocationid.SelectedValue)
            ' drpZone.SelectedValue = drpZone.Items.FindByValue(Zone).Value
            ' drpZone.SelectedIndex = drpZone.Items.IndexOf(drpZone.Items.FindByValue(Zone))
        Catch ex As Exception

        End Try
        ''If CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "BrownstheFloristLtdV8W1G9" Or CompanyID = "TrainingBrownsTheFlorist" Or CompanyID = "Greene and Greene" Or CompanyID = "Quickflora-Canada" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-CanadaTest" Or CompanyID = "Quickflora-USATest" Then
        If Session("AllowZoneDeliveryChargeFilling") Then
            FillDeliveryByZone(drpZone.SelectedValue, cmblocationid.SelectedValue)
        End If

    End Sub

    Private Function FillDeliveryByZone(ByVal ZoneID As String, ByVal LocationID As String) As Boolean

        If drpZone.SelectedValue = "0" Then
            ShippingDeliveryCharge(txtShippingZip.Text)
            Return False
            Exit Function
        End If

        If drpTransaction.SelectedValue.ToLower = "wire in" Or drpTransaction.SelectedValue.ToLower = "wire_in" Then
            txtService.Text = "0.00"
            If chkDeliveryOverride.Checked = False Then
                txtDelivery.Text = "0.00"
            End If

            Return False
            Exit Function
        End If

        If drpShipMethod.SelectedValue = "Pick Up" Or drpShipMethod.SelectedValue = "Taken" Or drpShipMethod.SelectedValue.ToLower = ("Curbside").ToLower Then
            If chkServiceOverride.Checked = False Then
                txtService.Text = "0.00"
            End If

            If chkDeliveryOverride.Checked = False Then
                txtDelivery.Text = "0.00"
            End If

            Return False
            Exit Function
        End If

        Dim ds As New DataSet
        ds = objZone.GetDetails(CompanyID, DivisionID, DepartmentID, "", ZoneID, LocationID)

        Dim DefaultLocalDeliveryCharge As Decimal = 0
        Dim DefaultServiceRelayCharge As Decimal = 0

        If ds.Tables(0).Rows.Count > 0 Then
            If chkDeliveryOverride.Checked = False Then
                txtDelivery.Text = String.Format("{0:n2}", ds.Tables(0).Rows(0)("DeliveryCharge"))
            End If
            If chkServiceOverride.Checked = False Then
                txtService.Text = String.Format("{0:n2}", ds.Tables(0).Rows(0)("WireCharge"))
            End If

        Else
            ds = objZone.GetDetails(CompanyID, DivisionID, DepartmentID, "", ZoneID, "")

            If ds.Tables(0).Rows.Count > 0 Then
                If chkDeliveryOverride.Checked = False Then
                    txtDelivery.Text = String.Format("{0:n2}", ds.Tables(0).Rows(0)("DeliveryCharge"))
                End If
                If chkServiceOverride.Checked = False Then
                    txtService.Text = String.Format("{0:n2}", ds.Tables(0).Rows(0)("WireCharge"))
                End If

            Else
                If chkDeliveryOverride.Checked = False Then
                    txtDelivery.Text = String.Format("{0:n2}", 0)
                End If
                If chkServiceOverride.Checked = False Then
                    txtService.Text = String.Format("{0:n2}", 0)
                End If

            End If

        End If

        If CompanyID.ToLower() = "fieldofflowers" And drpShipMethod.SelectedValue.ToLower <> "wire_out" Then
            txtService.Text = String.Format("{0:n2}", 0)
        End If


        CalculationPart()
        PopulatingTaxPercent()



        Return True

    End Function

    Protected Sub drpShippingState_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpShippingState.SelectedIndexChanged
        txtShippingState.Text = drpShippingState.SelectedValue
        PopulatingTaxPercent()
    End Sub

    Protected Sub btnZoneMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZoneMap.Click

        Dim objR As New Random()

        Dim Address As String = ""
        Address = txtShippingAddress1.Text.Trim + " " + txtShippingAddress2.Text.Trim + " " + txtShippingAddress3.Text.Trim + " " + txtShippingCity.Text.Trim + " " + drpShippingState.SelectedItem.Text.Trim + " " + drpCountry.SelectedItem.Text.Trim + " " + txtShippingZip.Text.Trim

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenWindowTypesSearch" + Convert.ToString(objR.Next(1000)), _
                "window.open('http://quickfloraav.com/maps/indexAuto.php?Address=" + Address.Replace(" ", "+") + "','','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=1100,height=650,Left=100,top=50,screenX=50,alwaysRaised=yes');", True)

    End Sub

    Protected Sub drpShipMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpShipMethod.SelectedIndexChanged
        ''If CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "BrownstheFloristLtdV8W1G9" Or CompanyID = "TrainingBrownsTheFlorist" Or CompanyID = "Greene and Greene" Or CompanyID = "Quickflora-Canada" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-CanadaTest" Or CompanyID = "Quickflora-USATest" Then
        If Session("AllowZoneDeliveryChargeFilling") Then
            FillDeliveryByZone(drpZone.SelectedValue, cmblocationid.SelectedValue)
        End If
    End Sub

    Protected Sub btnAcceptAddres_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAcceptAddres.Click


        Dim ds As New DataSet
        Dim objA As New clsAddressZone

        Try


            ds = objA.GetVerifiedAddres(CompanyID, DivisionID, DepartmentID, btnAcceptAddres.CommandArgument.ToString)

            If ds.Tables(0).Rows.Count > 0 Then

                If Session("AllowZoneDeliveryChargeFilling") And Not IsDBNull(ds.Tables(0).Rows(0)("ZoneID")) Then
                  drpZone.SelectedIndex = drpZone.Items.IndexOf(drpZone.Items.FindByValue(ds.Tables(0).Rows(0)("ZoneID")))
                End If

                drpShipCountry.SelectedValue = ds.Tables(0).Rows(0)("CountryCode")
                PopulateState(CompanyID, DivisionID, DepartmentID, ds.Tables(0).Rows(0)("CountryCode"))

                drpShippingState.SelectedIndex = drpShippingState.Items.IndexOf(drpShippingState.Items.FindByValue(ds.Tables(0).Rows(0)("Province")))
                drpShippingState.Visible = True
                txtShippingState.Visible = False
                drpShippingState_SelectedIndexChanged(sender, e)

                txtShippingAddress1.Text = ds.Tables(0).Rows(0)("StreetNumber") + " " + ds.Tables(0).Rows(0)("StreetName")
                txtShippingAddress2.Text = ""
                txtShippingAddress3.Text = ""

                Dim City As String = ds.Tables(0).Rows(0)("City")
                Try

                    Dim objEEE As New AddressRemoveStrings
                    For Each Str As String In objEEE.AddressRemoveStrings()
                        If (City.Contains(Str)) Then
                            City = City.Replace(Str, "")
                            Exit For
                        End If
                    Next
                Catch ex As Exception

                End Try

                txtShippingCity.Text = City

                'txtShippingCity.Text = ds.Tables(0).Rows(0)("City")
                txtShippingZip.Text = ds.Tables(0).Rows(0)("PostalCode")

                txtDriverRouteInfo.Text = txtDriverRouteInfo.Text.Replace(btnRejectAddress.CommandArgument.ToString, "")
                txtDriverRouteInfo.Text = txtDriverRouteInfo.Text.Replace("Address verified by " + drpEmployeeID.SelectedItem.Text + vbNewLine, "")
                txtDriverRouteInfo.Text = txtDriverRouteInfo.Text + vbNewLine + "Address verified by " + drpEmployeeID.SelectedItem.Text
                txtDriverRouteInfo.BackColor = Drawing.Color.White
                txtDriverRouteInfo.ForeColor = Drawing.Color.Green

		objA.UpdateAddressVerificationStatus(CompanyID, DivisionID, DepartmentID, lblOrderNumberData.Text.Trim, "Address verifies at POS by " + drpEmployeeID.SelectedItem.Text + " at " + DateTime.Now.ToString)


                btnAcceptAddres.Visible = False
                btnRejectAddress.Visible = False

                btnAcceptAddres.CommandArgument = ""
                btnRejectAddress.CommandArgument = ""

                ''If CompanyID = "BrownsTheFloristV8Z5N6" Or CompanyID = "BrownstheFloristLtdV8W1G9" Or CompanyID = "TrainingBrownsTheFlorist" Or CompanyID = "Greene and Greene" Or CompanyID = "Quickflora-Canada" Or CompanyID = "PoppiesV8W1L8" Or CompanyID = "ApacheJunctionFlowers85120" Or CompanyID = "Quickflora-CanadaTest" Or CompanyID = "Quickflora-USATest" Then
                If Session("AllowZoneDeliveryChargeFilling") Then
                    FillDeliveryByZone(drpZone.SelectedValue, cmblocationid.SelectedValue)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnRejectAddress_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRejectAddress.Click

        txtDriverRouteInfo.Text = txtDriverRouteInfo.Text.Replace(btnRejectAddress.CommandArgument.ToString, "")
        txtDriverRouteInfo.BackColor = Drawing.Color.White
        txtDriverRouteInfo.ForeColor = Drawing.Color.Black
        btnAcceptAddres.Visible = False
        btnRejectAddress.Visible = False
        btnZoneMap.Visible = False
        btnAcceptAddres.CommandArgument = ""
        btnRejectAddress.CommandArgument = ""

    End Sub



    Protected Sub btnDiscountApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiscountApply.Click
        Dim DiscountCode As String
        DiscountCode = txtDiscountCode.Text.Trim()

        DiscountApply(DiscountCode)
    End Sub

    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Dim DiscountCode As String = ""
        txtDiscountCode.Text = DiscountCode

        'DiscountApply(DiscountCode)
        lblDiscountCodeAmount.Text = "0"
        lblcodeerror.Text = ""
        lbldiscountcodemessage.Text = ""

        CalculationPart()
        PopulatingTaxPercent()
        CheckItemGridOnPostBacks()

    End Sub

    Private Sub DiscountApply(ByVal DiscountCode As String)
        Dim myreg As New Regex("\d+[\.,]?\d+")
        Dim Flg As String = ""
        Dim Amt As String = ""
        Dim Ddate As String = ""
        Dim DiscountAmt As Double
        Dim itemssubtotal As Double

        If IsNumeric(txtSubtotal.Text.Trim()) Then
            itemssubtotal = txtSubtotal.Text.Trim()
        Else
            itemssubtotal = 0
        End If

        If itemssubtotal = 0 Then
            Exit Sub
        End If

        Ddate = Now.Date.Date.Month.ToString() & "/" & Now.Date.Day.ToString() & "/" & Now.Date.Year.ToString()

        Dim ordernumber As String = ""
        ordernumber = lblOrderNumberData.Text.Trim

        Flg = ValidateDiscountCoupons(ordernumber, CompanyID, DivisionID, DepartmentID, txtShippingZip.Text, txtCustomerTemp.Text, DiscountCode, Ddate, itemssubtotal)
        Amt = Flg

        Amt = Flg.Substring(2)
        Flg = Flg.Substring(0, Flg.IndexOf(":"))
        lbldiscountcodemessage.Text = ""
        Select Case Flg
            Case "0"
                lblcodeerror.Visible = True
                lblcodeerror.ForeColor = Drawing.Color.Green
                lblcodeerror.Text = ""
                lbldiscountcodemessage.Visible = True
                lbldiscountcodemessage.Text = "Coupon code is applied successfully. Discount  amount = " & String.Format("{0:n2}", Amt)
            Case "1"
                lblcodeerror.Visible = True
                lblcodeerror.ForeColor = Drawing.Color.Red
                lblcodeerror.Text = "Coupon code entered can only be used for local delivery orders. Please enter a new code."

            Case "2"
                lblcodeerror.Visible = True
                lblcodeerror.ForeColor = Drawing.Color.Red
                lblcodeerror.Text = "Coupon code has already been used. Please enter a new code."

            Case "3"
                lblcodeerror.Visible = True
                lblcodeerror.ForeColor = Drawing.Color.Red
                lblcodeerror.Text = "Coupon code has been used the maximum number of allowable times. Please enter a new code."

            Case "4"
                lblcodeerror.Visible = True
                lblcodeerror.ForeColor = Drawing.Color.Red
                lblcodeerror.Text = "Coupon code is not valid. Please enter a new code."

            Case "5"
                lblcodeerror.Visible = True
                lblcodeerror.ForeColor = Drawing.Color.Red
                lblcodeerror.Text = "Coupon code cannot be applied to this delivery date as this date is blacked out. Please enter a new code."

            Case "6"
                lblcodeerror.Visible = True
                lblcodeerror.ForeColor = Drawing.Color.Red
                lblcodeerror.Text = "Coupon code can only be applied to order with a minimum order amount. Please enter a new code."

            Case Else
        End Select

        DiscountAmt = Convert.ToDouble(Amt)
        If txtDiscountCode.Text.Trim() <> "" Then
            If DiscountAmt = 0.0 Then
                'Exit Sub
                'lblDiscountMessg.Visible = True
            End If
            DiscountAmt = String.Format("{0:n2}", DiscountAmt)

            If IsNumeric(txtDiscountAmount.Text.Trim()) Then
            Else
                lblDiscountCodeAmount.Text = "0"
            End If
            lblDiscountCodeAmount.Text = DiscountAmt

        Else
            'DiscountPrice = ""
            'lblDiscountPrice.Text = ""
            'lblDiscountMessg.Visible = False
            lblDiscountCodeAmount.Text = "0"
            'Exit Sub
        End If

        Dim totalamount As Double = 0
        Dim totalDisamount As Double = 0

        Try
            totalamount = txtTotal.Text
        Catch ex As Exception

        End Try


        Try
            totalDisamount = txtDiscountAmount.Text
        Catch ex As Exception

        End Try

        totalamount = totalamount + totalDisamount

        If (totalamount - DiscountAmt) < 0 Then
            lblcodeerror.Visible = True
            lblcodeerror.ForeColor = Drawing.Color.Red
            lblcodeerror.Text = "Coupon code can not applied because its total amount ( $ " & DiscountAmt & " ) should less than order total amount.Please enter a new code."

            lbldiscountcodemessage.Text = ""
            lblDiscountCodeAmount.Text = "0"
            'Exit Sub
        End If


        CalculationPart()
        PopulatingTaxPercent()
        CheckItemGridOnPostBacks()

    End Sub

    Public Function ValidateDiscountCoupons(ByVal ordernumber As String, ByVal CompanyID As String, ByVal DivisionID As String, ByVal DepartmentID As String, ByVal zipCode As String, ByVal CustomerID As String, ByVal DiscountCouponCode As String, ByVal DeliDate As String, ByVal itemssubtotal As Double) As String
        Dim ConString As New SqlConnection(ConfigurationManager.AppSettings("ConnectionString"))
        Dim myCommand As New SqlCommand("ValidateDiscountCouponOE", ConString)
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

        Dim parameterordernumber As New SqlParameter("@ordernumber", Data.SqlDbType.NVarChar, 36)
        parameterordernumber.Value = ordernumber
        myCommand.Parameters.Add(parameterordernumber)

        Dim parameterzipCode As New SqlParameter("@zipCode", Data.SqlDbType.NVarChar, 36)
        parameterzipCode.Value = zipCode
        myCommand.Parameters.Add(parameterzipCode)

        Dim parameterCustomerID As New SqlParameter("@CustomerID", Data.SqlDbType.NVarChar, 36)
        parameterCustomerID.Value = CustomerID
        myCommand.Parameters.Add(parameterCustomerID)

        Dim parameterTotalITemPrice As New SqlParameter("@TotalITemPrice", Data.SqlDbType.Money)
        parameterTotalITemPrice.Value = itemssubtotal
        myCommand.Parameters.Add(parameterTotalITemPrice)

        Dim parameterDiscountCouponCode As New SqlParameter("@DiscountCouponCode", Data.SqlDbType.NVarChar, 36)
        parameterDiscountCouponCode.Value = DiscountCouponCode
        myCommand.Parameters.Add(parameterDiscountCouponCode)


        Dim parameterDeliDate As New SqlParameter("@DeliDate", Data.SqlDbType.DateTime)
        parameterDeliDate.Value = DeliDate
        myCommand.Parameters.Add(parameterDeliDate)


        Dim paramcartFlagValue As New SqlParameter("@FlagValue", Data.SqlDbType.NVarChar, 100)
        paramcartFlagValue.Direction = ParameterDirection.Output
        myCommand.Parameters.Add(paramcartFlagValue)
        Try



            ConString.Open()

            myCommand.ExecuteNonQuery()
        Catch ex As Exception
        Finally
            ConString.Close()
        End Try



        Dim OutPutValue As String


        OutPutValue = paramcartFlagValue.Value


        Return OutPutValue
    End Function

    Protected Sub txtDiscountCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiscountCode.TextChanged
        Dim DiscountCode As String
        DiscountCode = txtDiscountCode.Text.Trim()

        DiscountApply(DiscountCode)
    End Sub


    Protected Sub checkDiscounts_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles checkDiscounts.CheckedChanged


        If checkDiscounts.Checked Then
            drpdiscounttype.Visible = True
            txtdiscounttypevalue.Visible = True
            'btnapplydiscount.Visible = True
        Else
            drpdiscounttype.Visible = False
            txtdiscounttypevalue.Visible = False
            btnapplydiscount.Visible = False
        End If
        CalculationPart()
        PopulatingTaxPercent()

    End Sub


    Protected Sub btnapplydiscount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnapplydiscount.Click, drpdiscounttype.SelectedIndexChanged, txtdiscounttypevalue.TextChanged
        CalculationPart()
        PopulatingTaxPercent()
    End Sub

End Class
 
