<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="DuplicatePO.aspx.vb" Inherits="DuplicatePO" %>


<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Import Namespace="CrystalDecisions.CrystalReports" %>
<%@ Import Namespace="CrystalDecisions.Shared" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Duplicate Purchase Order
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">



<style type="text/css" >
	/* Grid */
 
 
  
.searchgrid-row
{
background-color: #FFFFFF;
}

.searchgrid-alternative-row
{
	background-color: #F5F5F5;
}


.boxdiv { 
		
			       visibility : hidden;
	                margin : 0px!important;
	                background-color : inherit;
	                color : windowtext;
	                border : buttonshadow;
	                border-width : 1px;
	                border-style : solid;
	                cursor : 'default';
	                overflow : auto;
	                height : 200px;
                    text-align : left; 
                    list-style-type : none;
                    font-family: Verdana;
                    font-size:8pt;
		 }
		 
		 
		 
 .paging_bootstrap {
    display: none !important
}

        
         div#sample_editable_1_filter
          {
          	display:none;
          }

          div#sample_editable_1_length
           {
          	display:none;
          }
          
          
          div#sample_editable_1_info
           {
          	display:none;
          }
              

</style>


<script type="text/javascript">


    var req;

    function Initialize() {
        try {
            req = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                req = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch (oc) {
                req = null;
            }
        }

        if (!req && typeof XMLHttpRequest != "undefined") {
            req = new XMLHttpRequest();
        }

    }
    var global_item_add = 0;

    function Saveitem(key, saveid) {
        Initialize();
        var url = "AjaxItemsSave.aspx?" + key;

        global_item_add = 0;

        //         alert(saveid);
        //          alert(document.getElementById(saveid));

        document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

        if (req != null) {
            req.onreadystatechange = ProcessSave;
            req.open("GET", url, true);
            req.send(null);


        }

    }

    function ProcessSave() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                // alert("Items saved Inline Number =" + req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";
                Subtotal();

            }
            else {
                alert("There was a problem saving data:<br>" + req.statusText);
            }
        }

    }


    function onblurtxtQty() {


        var itemid = '';
        itemid = document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value;
        var Qty = 0;

        Qty = document.getElementById("ctl00_ContentPlaceHolder_txtQty").value;

        var UOM = '';
        UOM = document.getElementById("ctl00_ContentPlaceHolder_txtuom").value;


        var url = 'AjaxItemPackSizeandUnits.aspx?ItemID=' + itemid + '&OrderQty=' + Qty + '&ItemUOM=' + UOM;

        // alert(url);

        Initialize();

        var start = 0;
        start = new Date();
        start = start.getTime();

        //  alert(url);
        if (req != null) {
            req.onreadystatechange = ProcessUOM;
            req.open("GET", url, true);
            req.send(null);


        }
    }

    function Processuomtotal() {

        var total = 0.00;
        var units = 0;
        var qty = 0;
        var unitprice = 0.00;
        units = document.getElementById("ctl00_ContentPlaceHolder_txtunits").value;
        qty = document.getElementById("ctl00_ContentPlaceHolder_txtQty").value;
        unitprice = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value;

        total = units * unitprice;

        total = total.toFixed(2);

        document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal").value = total;


    }

    function ProcessRecevieQtytotal() {

        var RUnits = 0.00;
        var RQty = 0;
        var RPack = 0;

        RQty = document.getElementById("ctl00_ContentPlaceHolder_RQty").value;
        RPack = document.getElementById("ctl00_ContentPlaceHolder_RPack").value;

        if (RQty != '' && RPack != '') {
            if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_RQty").value) == false) {
                alert("Please Enter Valid received quantity!");
                document.getElementById("ctl00_ContentPlaceHolder_RQty").focus();
                return false;
            }

            if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_RPack").value) == false) {
                alert("Please Enter Valid received pack size!");
                document.getElementById("ctl00_ContentPlaceHolder_RPack").focus();
                return false;
            }

            RUnits = RQty * RPack;

            document.getElementById("ctl00_ContentPlaceHolder_RUnits").value = RUnits;



        }


    }


    function checksubmit() {

        if (document.getElementById("ctl00_ContentPlaceHolder_drpReceiveby").value == '') {
            alert("Please select Received by!");
            document.getElementById("ctl00_ContentPlaceHolder_drpReceiveby").focus();
            return false;
        }

        if (document.getElementById("ctl00_ContentPlaceHolder_txtVendorInvoiceNumber").value == '') {
            alert("Please enter Vendor Invoice Number!");
            document.getElementById("ctl00_ContentPlaceHolder_txtVendorInvoiceNumber").focus();
            return false;
        }

        return true;

    }

    function IsNumeric(strString)
    //  check for valid numeric strings	
    {
        var strValidChars = "0123456789.";
        var strChar;
        var blnResult = true;

        if (strString.length == 0) return false;

        //  test strString consists of valid characters listed above
        for (i = 0; i < strString.length && blnResult == true; i++) {
            strChar = strString.charAt(i);
            if (strValidChars.indexOf(strChar) == -1) {
                blnResult = false;
            }
        }
        return blnResult;
    }



    function ProcessUOM() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                //alert(req.responseText);

                var str = req.responseText;
                // alert(str);
                var str1;
                str1 = str.split("~!");

                // alert(str1[1]);
                // alert(str1[2]);

                // document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value = str1[0];
                document.getElementById("ctl00_ContentPlaceHolder_txtPack").value = str1[1];
                document.getElementById("ctl00_ContentPlaceHolder_txtunits").value = str1[2];
                Processuomtotal();

            }
            else {
                alert("There was a problem updating ProcessUOM.<br>" + req.statusText);
            }
        }

    }


    function Subtotal() {
        Initialize();

        var start = 0;
        start = new Date();
        start = start.getTime();

        var url = "AjaxItemsOrderSubtotal.aspx?PurchaseNumber=" + document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value + "&start=" + start;

        //alert(url);
        if (req != null) {
            req.onreadystatechange = ProcessSubtotal;
            req.open("GET", url, true);
            req.send(null);


        }

    }



    function ProcessSubtotal() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                //alert(req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_txtSubtotal").value = req.responseText;
                Processtotal();


            }
            else {
                alert("There was a problem updating subtotal.<br>" + req.statusText);
            }
        }

    }


    function Processtotal() {

        var Delivery = 0.00;
        var Relay = 0.00;
        var Service = 0.00;
        var DiscountAmount = 0.00;



        Delivery = document.getElementById("ctl00_ContentPlaceHolder_txtDelivery").value;

        //            alert(DiscountAmount);
        //            alert(Delivery);
        //            alert(Relay);
        //            alert(Service);

        var tax = "";

        tax = document.getElementById("ctl00_ContentPlaceHolder_txtTaxPercent").value;

        //  alert(tax);

        tax = tax.replace("%", "");

        //  alert(tax);

        var taxperc = 0.00;

        taxperc = tax;

        var taxamnt = 0.00;
        var total = 0.00;
        var subtotal = 0.00;

        subtotal = document.getElementById("ctl00_ContentPlaceHolder_txtSubtotal").value;
        // alert(subtotal);

        subtotal = parseFloat(subtotal) + parseFloat(Delivery) + parseFloat(Relay) + parseFloat(Service);

        // alert(subtotal);



        taxamnt = (parseFloat(subtotal) * parseFloat(taxperc)) / 100;

        //alert(taxamnt);

        document.getElementById("ctl00_ContentPlaceHolder_txtTax").value = taxamnt.toFixed(2);
        taxamnt = taxamnt.toFixed(2);
        total = parseFloat(subtotal) + parseFloat(taxamnt);

        document.getElementById("ctl00_ContentPlaceHolder_txtTotal").value = total.toFixed(2);

    }

    function Updateitem(key, saveid) {
        Initialize();
        var url = "AjaxItemsReceivePOUpdate.aspx?LocationID=" + document.getElementById("ctl00_ContentPlaceHolder_drpShiplocation").value + "&" + key;
        // alert(url);
        //          alert(document.getElementById(saveid));

        document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

        if (req != null) {
            req.onreadystatechange = ProcessUpdate;
            req.open("GET", url, true);
            req.send(null);


        }

    }

    function ProcessUpdate() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                //alert("Items updated Inline Number =" + req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";
                // Subtotal();

            }
            else {
                alert("There was a problem updating data:<br>" + req.statusText);
            }
        }

    }




    function Deleteitem(key, saveid) {
        Initialize();
        var url = "AjaxItemsDelete.aspx?" + key;

        alert(url);
        //          alert(document.getElementById(saveid));

        document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

        if (req != null) {
            req.onreadystatechange = ProcessDelete;
            req.open("GET", url, true);
            req.send(null);


        }

    }

    function ProcessDelete() {

        if (req.readyState == 4) {
            if (req.status == 200) {

                //alert("Items Deleted Inline Number =" + req.responseText);
                document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";
                Subtotal();

            }
            else {
                alert("There was a problem Delete data:<br>" + req.statusText);
            }
        }

    }


    function SendQuery(key) {
        Initialize();
        var url = "AjaxVendorSearch.aspx?k=" + key;
        //alert(url);
        if (req != null) {
            req.onreadystatechange = Process;
            req.open("GET", url, true);
            req.send(null);

        }

    }

    function Process() {

        //alert(document.getElementById("<%=txtcustomersearch.ClientID%>"));

        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'right';



        if (req.readyState == 4) {
            // only if "OK"

            if (req.status == 200) {
                document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url()';
                // alert(req.status);
                if (trim(req.responseText) == "") {
                    //alert("in blank");  
                    //HideDiv("autocomplete");
                    document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
                    document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
                    document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'left';
                    document.getElementById("<%=txtcustomersearch.ClientID%>").value = "";
                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some other keyword.</div>";
                    var container = document.getElementById("autocomplete");
                    container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:CcustomersearchcloseProcess();' >Close Search result</a></div>";
                    container.appendChild(newdiv)
                }
                else {
                    if (document.getElementById("<%=txtcustomersearch.ClientID%>").value != "") {

                        ShowDiv("autocomplete");

                        var newdiv = document.createElement("div");
                        newdiv.innerHTML = req.responseText;
                        var container = document.getElementById("autocomplete");
                        container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs' href='javascript:CcustomersearchcloseProcess();' >Close Search result</a></div>";
                        container.appendChild(newdiv)
                    }
                }
            }
            else {
                document.getElementById("autocomplete").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;

            }
        }
    }

    function ShowDiv(divid) {


        document.getElementById(divid).style.height = "200px";
        if (document.layers) document.layers[divid].visibility = "show";
        else document.getElementById(divid).style.visibility = "visible";
    }


    function HideDiv(divid) {
        document.getElementById(divid).style.height = "0px";
        if (document.layers) document.layers[divid].visibility = "hide";
        else document.getElementById(divid).style.visibility = "hidden";
    }

    // Removes leading whitespaces
    function LTrim(value) {

        var re = /\s*((\S+\s*)*)/;
        return value.replace(re, "$1");

    }

    // Removes ending whitespaces
    function RTrim(value) {

        var re = /((\s*\S+)*)\s*/;
        return value.replace(re, "$1");

    }

    // Removes leading and ending whitespaces
    function trim(value) {

        return LTrim(RTrim(value));

    }

    function CcustomersearchcloseProcess() {




        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'left';
        document.getElementById("<%=txtcustomersearch.ClientID%>").value = "";
        document.getElementById("autocomplete").innerHTML = "";
        HideDiv("autocomplete");


    }

    function CcustomersearchBlurProcess() {

        if (document.getElementById("<%=txtcustomersearch.ClientID%>").value == "") {
            HideDiv("autocomplete");
            document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        }

    }

    function FillSearchtextBox(val) {
        // alert(val);
        document.getElementById("<%=txtcustomersearch.ClientID%>").value = val;
        document.getElementById("autocomplete").innerHTML = "";
        HideDiv("autocomplete");
        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url()';
        document.getElementById("<%=btncustsearch.ClientID%>").focus();
        document.getElementById("<%=btncustsearch.ClientID%>").click();
    }

    function SendQuery2(key) {



        // alert(document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value);
        var start = 0;
        start = new Date();
        start = start.getTime();

        Initialize();
        var url = "AjaxItemsSearch.aspx?k=" + key + "&start=" + start + "&lc=";  /// + document.getElementById("ctl00_ContentPlaceHolder_txtCustomerTypeID").value;
        //alert(url);
        if (req != null) {
            req.onreadystatechange = Process2;
            req.open("GET", url, true);
            req.send(null);

        }

    }

    function Process2() {
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'right';


        if (req.readyState == 4) {
            // only if "OK"
            if (req.status == 200) {
                document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url()';
                if (req.responseText == "") {
                    //alert("in blank");  
                    //HideDiv("autocomplete");
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
                    document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some other keyword.</div>";
                    var container = document.getElementById("autocomplete2");
                    container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                    container.appendChild(newdiv)
                }
                else {


                    if (document.getElementById("<%=txtitemsearch.ClientID%>").value != "") {
                        ShowDiv("autocomplete2");
                        //document.getElementById("autocomplete2").innerHTML = req.responseText;

                        var newdiv = document.createElement("div");
                        newdiv.innerHTML = req.responseText;
                        var container = document.getElementById("autocomplete2");
                        container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                        container.appendChild(newdiv)
                    }
                }
            }
            else {
                document.getElementById("autocomplete2").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;
            }
        }
    }


    function FillSearchtextBox22() {
        // alert(val);
        if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value != "") {
            FillSearchtextBox2(document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value);

        }
    }

    function FillSearchtextBox2_old(val) {
        // alert(val);
        document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = val;
        document.getElementById("autocomplete2").innerHTML = "";
        HideDiv("autocomplete2");
        document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';
        document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").focus();
        document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").click();
        //ImgUpdateSearchitems
    }



    function FillSearchtextBox2(val) {
        // alert(val);




        if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value == "") {

        }
        else {
            var str = val;
            var str1;
            str1 = str.split("~!");

            document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value = str1[0];
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = str1[1];
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = str1[2];

            document.getElementById("ctl00_ContentPlaceHolder_txtcolor").value = str1[3];
            document.getElementById("ctl00_ContentPlaceHolder_txtuom").value = str1[4];




            document.getElementById("ctl00_ContentPlaceHolder_txtDesc").value = '';
            document.getElementById("ctl00_ContentPlaceHolder_txtComments").value = '';

            document.getElementById("ctl00_ContentPlaceHolder_txtQty").value = '';

            document.getElementById("ctl00_ContentPlaceHolder_txtunits").value = '';
            document.getElementById("ctl00_ContentPlaceHolder_txtPack").value = '';

            document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal").value = '';


            document.getElementById("autocomplete2").innerHTML = "";
            HideDiv("autocomplete2");
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';
            //alert(val);
            if (val.length > 3) {
                //document.getElementById("sample_editable_1_new").focus();
                //document.getElementById("sample_editable_1_new").click();

                HideDiv("dvitems");
                ShowDiv("newedititems");
                ShowDiv("new");
                HideDiv("update");


                document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = "";
                global_item_add = 1;
            }
            // document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").focus();

        }

        var ordernumber = "";
        ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;


        // alert(ordernumber);

        if (ordernumber == "(New)") {
            // alert(document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems"));
            document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").focus();
            document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").click();
            //document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").focus();
            // return;
        }


        itemrsearchcloseProcess();

    }


    function itemrsearchcloseProcess() {

        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
        document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
        document.getElementById("autocomplete2").innerHTML = "";
        HideDiv("autocomplete2");


    }


    function itemsearchBlurProcess() {

        if (document.getElementById("<%=txtitemsearch.ClientID%>").value == "") {
            HideDiv("autocomplete2");
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        }

    }

     
</script>     

    <!-- BEGIN PORTLET 1st Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                &nbsp;PO Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="text-center" style="padding-top: 100px;">
                        <asp:Image ID="ImgRetailerLogo" CssClass="img-rounded" ImageUrl="" runat="server" />
                    </div>
                </div>
                <div class="col-md-4">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table3" class="table table-striped table-hover table-bordered">
                            <tr id="trtop" runat="server" visible="false">
                                <td width="110" height="12">
                                    Auto Saved At
                                </td>
                                <td width="101" style="background-color: #F6F6F6;" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel1" runat="server">
                                                                    <asp:Label Width="95" BorderWidth="1px" BorderColor="#939393" ID='lblrefersh'    CssClass="txtStyle" Height="18" runat="server" Text=''></asp:Label>
                                                                </ajax:ajaxpanel>
                                </td>
                            </tr>
                             <tr>
                                <td width="110" height="12">
                                    New PO#
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel22" runat="server">
                                       <asp:Label Width="95"  ID='Label1'   Height="18" runat="server" Text='(New)'></asp:Label>
                                     </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="12">
                                    Existing PO#
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel2" runat="server">
                                                                    <asp:Label Width="95"  ID='lblPurchaseOrderNumberData'   Height="18" runat="server" Text='(New)'></asp:Label>
                                                                  </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="12" valign="middle">
                                    PO Date
                                </td>
                                <td width="101" height="12">
                                    <asp:Label ID="lblPurchaseOrderDate" Height="18" Width="95" runat="server" Text=" "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Trans. Type
                                </td>
                                <td width="101" height="15">
                                    <ajax:ajaxpanel id="AjaxPanel270" runat="server">
                                       <asp:DropDownList CssClass="form-control input-xs" ID="drpPurchaseTransaction"   runat="server" TabIndex="2">
                                        <asp:ListItem Value="POH" Text="POH- Hard goods"></asp:ListItem>
                                        <asp:ListItem Value="POF" Text="POF- Fresh"></asp:ListItem>
                                       </asp:DropDownList>
                                     </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <!--location id-->
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    PO Location
                                </td>
                                <td width="101" height="15">
                                    <ajax:ajaxpanel id="AjaxPanel2710" runat="server">
                                                                <asp:DropDownList ID="cmblocationid" CssClass="form-control input-xs" runat="server"    TabIndex="4">
                                                                </asp:DropDownList>
                                                              </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <!--location id-->
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Entered by
                                </td>
                                <td width="101" height="15">
                                    <asp:DropDownList CssClass="form-control input-xs" ID="drpEmployeeID" runat="server"
                                        TabIndex="3">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                                 <tr>
                                <td width="110" height="15" valign="middle">
                                    Payment Method
                                </td>
                                <td width="101" height="15">
                                 <ajax:ajaxpanel id="AjaxPanel27110" runat="server">
                                   <asp:DropDownList ID="drpPaymentType" CssClass="form-control input-sm"     runat="server"  >
                                                            </asp:DropDownList>
                                 </ajax:ajaxpanel>
                                </td>
                            </tr>
                                 
                              <tr id="trtn" runat="server" visible="false" >
                                <td width="110" height="15" valign="middle">
                                    Tracking number
                                </td>
                                <td width="101" height="15">
                                 <ajax:ajaxpanel id="AjaxPanel511" runat="server">
                                    
                                     <asp:TextBox ID="txtTrackingno" CssClass="form-control input-sm"   runat="server"></asp:TextBox>

                                 </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr id="tron" runat="server" visible="false"  >
                                <td width="110" height="15" valign="middle">
                                    Order number
                                </td>
                                <td width="101" height="15">
                                 <ajax:ajaxpanel id="AjaxPanel1111" runat="server">
                                    
                                     <asp:TextBox ID="txtOrderno" CssClass="form-control input-sm"   runat="server"></asp:TextBox>

                                 </ajax:ajaxpanel>
                                </td>
                            </tr>      
                                 
                             <tr id="trrb" runat="server" visible="false"  >
                                <td width="110" height="15" valign="middle">
                                    Receive by
                                </td>
                                <td width="101" height="15">
                                    <asp:DropDownList CssClass="form-control input-xs" ID="drpReceiveby" AppendDataBoundItems="true" runat="server"
                                        TabIndex="3">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>  
                             <tr  id="trVI" runat="server" visible="false"  >
                                <td width="110" height="15" valign="middle">
                                    Vendor Invoice #
                                </td>
                                <td width="101" height="15">
                                     <asp:TextBox ID="txtVendorInvoiceNumber"   runat="server"   CssClass="form-control input-sm"  ></asp:TextBox>
			                           
                                </td>
                            </tr>     
                                    
                        </table>
                    </div>
                    <!-- END FORM-->
                </div>
                <div class="col-md-4">
                    <div class="alert alert-info">
                        System Wide Messages</div>
                    <asp:Label ID="lblSystemWM" Height="160" runat="server" Font-Size="9pt"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <!-- END PORTLET-->
    <!-- BEGIN PORTLET 2nd Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption" style="width: 95%;">
                <div class="row">
                    <div class="col-md-2">
                        <div class="text-left">
                            <ajax:ajaxpanel id="AjaxPanel3" runat="server">
                                            <asp:Label ID='lblCustomer'   runat='server'  Text="Vendor Detail"></asp:Label>
                                        </ajax:ajaxpanel>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="text-left">
                            <ajax:ajaxpanel id="AjaxPanel4" runat="server">
                                         
			                            <asp:TextBox ID="txtVendorTemp"   runat="server"  ReadOnly="True" CssClass="form-control input-sm input-200"  ></asp:TextBox>
			                            
                                    </ajax:ajaxpanel>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="text-right">
                            
                        </div>
                    </div>
                </div>
            </div>
            <div class="tools">
                <a href="javascript:;" class="<%=Bill_to_Customer_ID %>"></a>
            </div>
        </div>
        <div class="portlet-body form"  style="display: none;" >
            <div class="form-group-search-block">
                <div class="input-group">
                    <span class="input-group-addon input-circle-left"><i class="fa fa-search"></i></span>
                    <ajax:ajaxpanel id="AjaxPanel6" runat="server">
                                      <asp:TextBox ID="txtcustomersearch" runat="server" CssClass="form-control input-circle-right"    ></asp:TextBox>
                                    </ajax:ajaxpanel>
                    <br />
                    <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="padding-top: 10px;">
                    <ajax:ajaxpanel id="AjaxPanel7" runat="server">
                                         &nbsp;<asp:ImageButton ID="btncustsearch"  ToolTip="Update Customer" ImageUrl="~/images/2-sh-stock-in.gif" Width="0" runat="server" /> 
                                         <asp:Label ID="lblsearchcustomermsg" runat="server" Text=""></asp:Label>
                                    </ajax:ajaxpanel>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <ajax:ajaxpanel id="AjaxPanel8" runat="server">
                                 
                                 
                   <div class="table-responsive">
                      		<table class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                      <td>
                                      
                                         Vendor Name 

                                      
                                        
                                        </td>
                                      <td colspan="3"  >
                                       
                                         <asp:TextBox ID="txtVendorName"  CssClass="form-control input-sm"  runat="server"></asp:TextBox>
                                                
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>Attention</td>
                                      <td colspan="3"  >
                                         <asp:TextBox CssClass="form-control input-sm "  ID="txtAttention" runat="server" Text=''  ></asp:TextBox>
                                      </td>
                                       
                                    </tr>
                                   
                                    <tr>
                                      <td>Add 1</td>
                                      <td colspan="3"  > <asp:TextBox CssClass="form-control input-sm"  ID="txtVendorAddress1" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Add 2</td>
                                      <td colspan="3"  > <asp:TextBox CssClass="form-control input-sm"  ID="txtVendorAddress2" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    
                                    
                                  
                                        </td>
                                    </tr>

                                </tbody>
                        	</table>
                        </div>

                   </ajax:ajaxpanel>
                </div>
                <div class="col-md-6">
                    <ajax:ajaxpanel id="AjaxPanel9" runat="server">
                                <table class="table table-striped table-hover table-bordered">
                                <tbody>
                                
                                    
                                    <tr>
                                        <td colspan="4" >
                                        <asp:Label ID="lblCustomerIDNull" runat="server" ForeColor="Red" Visible="False"></asp:Label> 
                                         <tr>
                                      <td>City</td>
                                      <td>  <asp:TextBox ID="txtVendorCity" CssClass="form-control input-sm"  runat="server" Text=''  ></asp:TextBox> </td>
                                       <td  >State/Province</td>
                                      <td  > 
                                        

                                                <asp:TextBox ID="txtVendorState" runat="server" CssClass="form-control input-sm"  Text=""></asp:TextBox>
                                      
                                       </td>
                                    </tr>
                                    <tr>
                                      <td  >
                                       
                                            <label>Country</label>
                                            
                                      </td>
                                      <td>
                                             
                                           <asp:TextBox ID="txtVendorCountry" CssClass="form-control input-sm"  runat="server" Text=''  ></asp:TextBox>             
                                            
                                      </td>
                                      <td  >Zip/Postal</td>
                                      <td  > <asp:TextBox ID="txtVendorZip" runat="server" CssClass="form-control input-sm"  Text=''  ></asp:TextBox> </td>
                                       
                                    </tr>
                                    
                                   
                                    <tr>
                                      <td  >Phone</td>
                                      <td  > <asp:TextBox CssClass="form-control input-sm" ID="txtVendorPhone" runat="server" Text=''  ></asp:TextBox> </td>
                                    
                                      <td>Fax</td>
                                      <td> <asp:TextBox CssClass="form-control input-sm" ID="txtVendorFax" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Email</td>
                                      <td colspan="3" >  <asp:TextBox CssClass="form-control input-sm"  ID="txtVendorEmail" runat="server" Text=''  ></asp:TextBox> </td>
                                       
                                    </tr>
                                </tbody> 
                                </table>                                          
                          
                 </ajax:ajaxpanel>
                </div>
            </div>
        </div>
    </div>
    <!-- END PORTLET-->
    
      <!-- BEGIN PORTLET 3rd Block-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                            <div class="caption" style="width:95%;"> 
                                 <div class="row">
								    <div class="col-md-2">
									    <div class="text-left" >
                                         Items Detail 
                                        </div> 
								    </div>
								    <div class="col-md-5">
                                         <Ajax:AjaxPanel ID="AjaxPanel10" runat="server">
									     <div ID="pnlPricerange" runat="server" class="text-center" >
                                     
                                             <asp:Label ID="lblPricerange" BackColor="#40BD24" ForeColor="white" Font-Bold="true"   runat="server"   Text=""></asp:Label>
                                            <asp:Label ID="lblInventoryStatus" BackColor="#40BD24" ForeColor="red" Font-Bold="true"  runat="server"   Text=""></asp:Label>
                                     
                                         </div> 
                                         </Ajax:AjaxPanel> 
								    </div>
                                    <div class="col-md-5">
									        <div class="text-right" >
                                                 
                                            </div>     
								    </div>
							</div>
                            </div>
							<div class="tools">
                                <a href="javascript:;" class="collapse"></a>
                            </div>
						</div>

                        	<div class="portlet-body form"  >

                         
                   
                                      <div class="form-group-search-block">
                                        <div class="input-group">
                                        
                                            <span class="input-group-addon input-circle-left">
                                            <i class="fa fa-search"></i>
                                            </span>
                                               <Ajax:AjaxPanel ID="AjaxPanel12" runat="server">
                                                <asp:TextBox ID="txtitemsearch"  CssClass="form-control input-circle-right"    runat="server"></asp:TextBox>
                                             </Ajax:AjaxPanel> 
                                            <br />
                                              
                                             <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete2"   ></div>   
                                           <div id="newedititems"    style="visibility: hidden;" class="box autocomplete">
                                            <div class="rows">
								             <div style="padding-top:2px;" class="col-md-12"    >

                                             <div class="row">
                                                <div class="col-md-2">
							                        <div class="text-center">
							                                ID <asp:TextBox ID="txtitemid" ReadOnly="true"  CssClass="form-control input-sm"   runat="server"></asp:TextBox>
							                        </div> 
							                    </div> 

							                    <div class="col-md-2">
                                                    <div class="text-center">
                                                         Name <asp:TextBox ID="txtitemsearchDesc" ReadOnly="true" CssClass="form-control input-sm" runat="server"></asp:TextBox>    
                                                    </div>
                                                </div>
                                                 <div class="col-md-4">
                                                    <div class="text-center">
                                                         Desc <asp:TextBox ID="txtDesc" ReadOnly="true"  CssClass="form-control input-sm" runat="server"></asp:TextBox>    
                                                    </div>
                                                </div>
                                                  
                                                
                                                <div class="col-md-4">
                                                    <div class="text-center">
                                                           Comments <asp:TextBox ID="txtComments" ReadOnly="true" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                                
                                                 
                                                 
                                                 </div> 
                                                 
                                                <div class="row"> 
                                                
                                                <div class="col-md-1">
                                                    <div class="text-center">
                                                        Qty<br /> <asp:TextBox ID="txtQty" ReadOnly="true" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="text-center">
                                                           UOM<br /> <asp:TextBox ID="txtuom" ReadOnly="true" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                                  <div class="col-md-2">
                                                    <div class="text-center">
                                                         Pack<br /> <asp:TextBox ID="txtPack" ReadOnly="true" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                    
                                                </div>

                                                  <div class="col-md-2">
                                                    <div class="text-center">
                                                        Total Units<br />   <asp:TextBox ID="txtunits" ReadOnly="true" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                    
                                                </div>

                                                
                                                 <div class="col-md-2">
                                                    <div class="text-center">
                                                     Received Qty  <asp:TextBox ID="RQty" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                    
                                                </div>
                                                 <div class="col-md-2">
                                                    <div class="text-center">
                                                     Received Pack  <asp:TextBox ID="RPack"   CssClass="form-control input-sm"   runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                                
                                                 <div class="col-md-2">
                                                    <div class="text-center">
                                                     Received Units  <asp:TextBox ID="RUnits" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                    
                                                </div>
                                               
                                                    
                                             </div>
                                             
                                               <div class="row"> 
                                               
                                                 <div class="col-md-6">
                                                   
                                                    <div style="padding-top:20px;" class="text-center">
                                                     
                                                    
                                                     <button id="sample_editable_1_cancel" class="btn red">
                                                            Cancel <i class="fa fa-minus"></i>
                                                     </button> 
                                                     </div> 
                                                        
                                                        <div id="new" style="padding-top:20px;" class="text-center">
                                                           <button id="sample_editable_1_new" class="btn green">
                                                            Add New <i class="fa fa-plus"></i>
                                                            </button>
                                                       </div>    
                                                       
                                                </div> 
                                                 
                                                   <div class="col-md-6">
                                                   
                                                     
                                                     
                                                     <div id="update" style="padding-top:20px;" class="text-center">
                                                           <button id="sample_editable_1_update" class="btn green">
                                                            Received <i class="fa fa-plus"></i>
                                                            </button>
                                                       </div> 
                                                     
                                                  </div>
                                               
                                               </div> 


							                     </div> 
							                </div>
                                        </div>
                                            
                                        </div>
                                    </div>


                                      <div class="row">
								        <div class="col-md-12"  >
                                             <Ajax:AjaxPanel ID="AjaxPanel13" runat="server">
                                                <asp:ImageButton ID="ImgUpdateSearchitems" OnClientClick="javascript:FillSearchtextBox22();"  ToolTip="Update Item" ImageUrl="~/images/2-sh-stock-in.gif" Width="0"    runat="server"  />  
                                                <asp:Label ID="lblitemsearchmsg" runat="server" Text=""></asp:Label>
                                            </Ajax:AjaxPanel> 
                                        </div>
								 
							        </div>
  
                                    
                                        
                                       
                                      
                                       
  
                                      
  
                                    <div id="dvitems"  class="row">
								        <div class="col-md-12"    >
                           
                                        
                                        
                           <table class="table table-striped table-hover table-bordered dataTable tableRecord"   id="sample_editable_1">
							 <thead>
                                <tr>
                                    
                                    <th>
                                        Item ID 
                               
                                    </th>
                                    <th>
                                        Item Name 
                                    </th>
                                    <th>
                                        Description 
                                    </th>
                                     <th>
                                        Comments 
                                    </th>
                                   
                                    <th>
                                        Qty 
                                    </th>
                                    <th>
                                        Type 
                                    </th>
                                    <th>
                                       Pack 
                                    </th>
                                     <th>
                                       Units 
                                    </th>
                                     <th>
                                       Received Qty
                                    </th>
                                    <th>
                                        Received Pack  
                                    </th>
                                     
                                    <th>
                                        Receive Units
                                    </th>
                                     
                                    <th>
                                
                                    </th>
                                    <th>
                                
                                    </th>
                                </tr>
                            </thead>
							<tbody runat="server" id="tbody" >
							    
                            </tbody> 
                            </table> 

                                        </div>
								 
							        </div>
                                    
                                      
                                        <div style="display:none" >
                                            
                                            <Ajax:AjaxPanel ID="AjaxPanel104" runat="server">
                                            
                                            <asp:TextBox ID="txtfirst"   Text="1" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="reqsaveid"   Text="" runat="server"></asp:TextBox>
                                                                       
                                            <asp:TextBox ID="txtOrderNumber" Width="200px"   Font-Size="12"  Text=""  Height="28" runat="server"></asp:TextBox>
                                           </Ajax:AjaxPanel>
                                        </div>
                                        
                
                        
                                        <Ajax:AjaxPanel ID="AjaxPanel14" runat="server">
                                           <asp:Label ID="lblErrorText" TabIndex="63" runat="server" Visible="false"></asp:Label>
                                        </Ajax:AjaxPanel> 

                                        </div>


                         


                                 
                    </div>
      <!-- END PORTLET-->
      
      
      <!-- Begin 4th Block -->
      
      
                  <div class="row">
					<div class="col-md-12">
                    <!-- BEGIN PORTLET-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                    	
							<div class="caption" style="width: 95%;">
                        <div class="row">
                            <div class="col-md-6">
							<div class="text-left">
							    Shipping Info
							</div> 
							</div> 
							<div class="col-md-6">
                                <div class="text-right">
                                    Order Total
                                </div>
                            </div>
                         </div>
							
							
							 </div> 
							
							
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
							
						</div>
						<div class="portlet-body">
							<div class="row">
								<div class="col-md-4">
									<!-- BEGIN FORM-->
                                        <div class="form-body">
                                             <Ajax:AjaxPanel ID="AjaxPanel19" runat="server">
                                              
                                                <table class="table table-striped table-hover table-bordered">
                                                     <tr>
                                                        <td>Ship to Location</td>
                                                        <td>
                                                             
                                                             <asp:DropDownList CssClass="form-control input-xs" ID="drpShiplocation" AutoPostBack="true"    runat="server" TabIndex="2">
                                                                
                                                             </asp:DropDownList>
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                      <td>Address</td>
                                      <td    > <asp:TextBox CssClass="form-control input-sm"  ID="txtLCAddress" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                   
                                  
                                   
                                                </table> 
                                                 <table class="table table-striped table-hover table-bordered">
                                <tbody>
                                
                                    <tr>
                                      <td>City</td>
                                      <td>  <asp:TextBox ID="txtLCCity" CssClass="form-control input-sm"  runat="server" Text=''  ></asp:TextBox> </td>
                                       
                                      <td  >Zip/Postal</td>
                                      <td  > <asp:TextBox ID="txtLCZip" runat="server" CssClass="form-control input-sm"  Text=''  ></asp:TextBox> </td>
                                       
                                    </tr>
                                    
                                   
                                    <tr>
                                      <td  >Phone</td>
                                      <td  > <asp:TextBox CssClass="form-control input-sm" ID="txtLCPhone" runat="server" Text=''  ></asp:TextBox> </td>
                                    
                                      <td>Fax</td>
                                      <td> <asp:TextBox CssClass="form-control input-sm" ID="txtLCFax" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Email</td>
                                      <td colspan="3" >  <asp:TextBox CssClass="form-control input-sm"  ID="txtLCEmail" runat="server" Text=''  ></asp:TextBox> </td>
                                       
                                    </tr>
                                </tbody> 
                                </table>  
                                            </Ajax:AjaxPanel> 
                                        
                                        </div>
                                    <!-- END FORM-->
								</div>
                                <div class="col-md-4">
									<!-- BEGIN FORM-->
                                        <div class="form-body">
                                         <Ajax:AjaxPanel ID="AjaxPanel20" runat="server">
                                          <table class="table table-striped table-hover table-bordered">
                                                 <tr>
                                                        <td>Ship Method</td>
                                                        <td>
                                                             
                                                             <asp:DropDownList CssClass="form-control input-xs" ID="drpShipMethod"    runat="server" TabIndex="2">
                                                                <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                                                <asp:ListItem Text="Ship" Value="Ship"></asp:ListItem>
                                                             </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Arrival Date</td>
                                                        <td>  
                                                        
                                                        
                                                        <table>
                                                            <tr>
                                                                <td> <i class="fa fa-calendar"></i> </td>
                                                                <td>&nbsp;</td>
                                                                <td><asp:TextBox ID="txtArrivalDate"   runat="server"  CssClass="form-control input-sm date-picker input-200" ></asp:TextBox></td>
                                                            </tr>
                                                        </table>   
                                                        
                                                        
                                                                
                                                        </td>
                                                    </tr>
                                                  <tr>
                                                    <td colspan="2"  >Internal Notes</td>
                                                  </tr>
                                                  <tr>
                                                    <td colspan="2" >
                                                        <asp:TextBox ID="txtInternalNotes"   TextMode="MultiLine"   Rows="4"  CssClass="form-control input-xs"    runat="server"></asp:TextBox>
                                                         
                                                    </td>
                                                  </tr>
                                                 
                                                </table>  
                                        </Ajax:AjaxPanel> 
                                        </div>
                                    <!-- END FORM-->
								</div>
								<div class="col-md-4">
									<!-- BEGIN FORM-->
                                        <div class="form-body">
                                         <Ajax:AjaxPanel ID="AjaxPanel21" runat="server">
                                          <table class="table table-striped table-hover table-bordered">
                                             <tr>
                                            <td colspan="2"  class="text-right">Sub Total</td>
                                            <td colspan="2"  style="width:50%"><asp:TextBox ID="txtSubtotal" runat="server" Enabled="True" ReadOnly="True" Text="0.00"  CssClass="form-control input-xs"   ></asp:TextBox> </td>
                                            </tr>
                                            <tr>
                                            <td colspan="2" class="text-right">
                                              
                                            Frieght</td>
                                            <td colspan="2"  >
                                            
                                             <Ajax:AjaxPanel ID="AjaxPanel35" runat="server">

               
                                            <asp:TextBox ID="txtDelivery" CssClass="form-control input-xs"  runat="server"  Text="0.00"    AutoPostBack="True"></asp:TextBox>
                                            
                                            </Ajax:AjaxPanel> 
                                            </td>
                                            
                                            
                                            </tr>
  

                                            <tr runat="server" id="trTax" >
                                                <td  class="text-right">Tax</td>
                                                <td>
                                                <table>
                                                <tr>
                                                    <td>
                                                     <Ajax:AjaxPanel ID="AjaxPanel38" runat="server">

               
                                                    <asp:DropDownList AutoPostBack="true" CssClass="form-control input-xs" ID="drpTaxes" runat="server">
                                                        <asp:ListItem Text="DEFAULT" Value="DEFAULT"></asp:ListItem>
                                                     </asp:DropDownList>
                                                 </Ajax:AjaxPanel> 
                                                 
                                                  </td>
                                                  <td><asp:TextBox ID="txtTaxPercent" CssClass="form-control input-xs" runat="server" ReadOnly="True"  Text="6.00"   Enabled="True"   Width="50"></asp:TextBox></td>
                                                </tr>
                                                </table>
                                               
                                                 
                                                 
                                                </td>
                                                
                                                
                                                <td colspan="2" ><asp:TextBox ID="txtTax" CssClass="form-control input-xs"  runat="server" ReadOnly="True"     Enabled="True" Text="0.00"  ></asp:TextBox></td>
                                            </tr>
 
                                                   
                                            <tr class="total-foot">
                                            <td colspan="2"  class="text-right"><strong class="text-white">Total</strong></td>
                                            <td colspan="2"  ><asp:TextBox ID="txtTotal" CssClass="form-control input-xs" runat="server" ReadOnly="True"      Text="0.00"  Enabled="True"  ></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                &nbsp;<br /> 
                                                </td>
                                            </tr>
                                             <tr>
                                                    <td colspan="2" class="text-center">
                                                        <asp:Button ID="btnsave"      CausesValidation="false"    CssClass="btn btn-success btn-xs" runat="server"  Text="Duplicate" Width="150" ></asp:Button>
                                                    </td>
                                                    <td colspan="2" class="text-center">
                                                          <asp:Button ID="btnpost"     CausesValidation="false"  CssClass="btn btn-success btn-xs" runat="server" Text="Back"  Width="150"  ></asp:Button>
                                      
                                                    </td>
                                                  </tr>
                                            </table>  
                                        </Ajax:AjaxPanel> 
                                        </div>
                                    <!-- END FORM-->
								</div>
							</div>

						</div>
					</div>
                    <!-- END PORTLET-->
					</div>
				</div>
<!-- END PORTLET-->
    
          <!-- BEGIN PORTLET 5th Block-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                            <div class="caption" style="width:95%;"> 
                                 <div class="row">
								    <div class="col-md-2">
									    <div class="text-left" >
                                         Logs
                                        </div> 
								    </div>
								    <div class="col-md-5">
                                          
								    </div>
                                    <div class="col-md-5">
									        <div class="text-right" >
                                                 
                                            </div>     
								    </div>
							</div>
                            </div>
							<div class="tools">
                                <a href="javascript:;" class="collapse"></a>
                            </div>
						</div>

                        	<div class="portlet-body form"  >
                        	  <div class="row">
								    <div class="col-md-4">
									      <table class="table table-striped table-hover table-bordered">
                                                   <tr>
                                                    <td> <asp:CheckBox ID="chkPosted" runat="server" Enabled="False"></asp:CheckBox>  </td>
                                                    <td>Posted</td>
                                                    <td> <asp:TextBox ID="txtPostedDate" runat="server" ReadOnly="false" Text=''  CssClass="form-control input-xs"   TabIndex="94"></asp:TextBox>   </td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtPostedTime" runat="server" ReadOnly="false"  CssClass="form-control input-xs"   TabIndex="95"></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                   <tr>
                                                    <td> <asp:CheckBox ID="chkPrint" runat="server" Enabled="False"></asp:CheckBox>  </td>
                                                    <td>Print</td>
                                                    <td> <asp:TextBox ID="txtPrintDate" runat="server" ReadOnly="false" Text=''  CssClass="form-control input-xs"   TabIndex="94"></asp:TextBox>   </td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtPrintTime" runat="server" ReadOnly="false"  CssClass="form-control input-xs"   TabIndex="95"></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                          </table>         
								    </div>
								    <div class="col-md-4">
                                            <table class="table table-striped table-hover table-bordered">
                                                   
                                                  <tr>
                                                    <td> <asp:CheckBox ID="chkApproved" runat="server" Enabled="False" />   
                                                                
                                                    
                                                    </td>
                                                    <td>Approved</td>
                                                    <td><asp:TextBox ID="txtApprovedDate" runat="server" ReadOnly="false" CssClass="form-control input-xs" Text=''  ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtApprovedTime" runat="server" ReadOnly="false" CssClass="form-control input-xs"  ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                   <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkSent" runat="server" Enabled="False" />  </td>
                                                    <td>Sent</td>
                                                    <td><asp:TextBox ID="txtSentDate" runat="server" ReadOnly="false" CssClass="form-control input-xs" Text=''  ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtSentTime" runat="server" ReadOnly="false" CssClass="form-control input-xs" ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                          </table>    
                                      
								    </div>
                                    <div class="col-md-4"> 
									      
                                    
                                    <table class="table table-striped table-hover table-bordered">
                                              
                                                  <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkReceived" runat="server" Enabled="False" />  </td>
                                                    <td>Received</td>
                                                    <td><asp:TextBox ID="txtReceivedDate" runat="server" ReadOnly="false" CssClass="form-control input-xs" Text=''  ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtReceivedTime" runat="server" ReadOnly="false" CssClass="form-control input-xs" ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                  <tr>
                                                    <td> <asp:CheckBox ID="chkShipped" runat="server" Enabled="False" /> </td>
                                                    <td>Shipped</td>
                                                    <td><asp:TextBox ID="txtShippedDate" runat="server" CssClass="form-control input-xs" ReadOnly="false" Text='' ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtShippedTime" runat="server" CssClass="form-control input-xs" ReadOnly="false"  ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                  
                                                 
                                                </table>
                                    </div> 
							</div>
                        	    
                        	</div> 
                      </div> 
    
            <!-- END PORTLET-->
      
    
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>

<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>


<script type="text/javascript" src="assets/plugins/data-tables/jquery.dataTables.js"></script>
<script type="text/javascript" src="assets/plugins/data-tables/DT_bootstrap.js"></script>


<script type="text/javascript" src="assets/scripts/table-editable2.js"></script>

<script type="text/javascript" >
    jQuery(document).ready(function() {

        TableEditable.init();
        Search.init();
    });
</script>

</asp:Content>

