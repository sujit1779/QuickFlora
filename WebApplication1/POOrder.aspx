<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false"
    CodeFile="POOrder.aspx.vb" Inherits="PO" %>

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
    Create Purchase Order
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">


<style type="text/css">
		.fixed_headers {
			width: 100%;
			table-layout: fixed;
			border-collapse: collapse;
		}
		.fixed_headers th {
			text-decoration: underline;
		}
		.fixed_headers th,
		.fixed_headers td {
			padding: 5px;
			text-align: left;
		}
		.fixed_headers td:nth-child(1),
		.fixed_headers th:nth-child(1) {
			min-width: 150px;
		}
		.fixed_headers td:nth-child(2),
		.fixed_headers th:nth-child(2) {
			min-width: 200px;
		}
		.fixed_headers td:nth-child(3),
		.fixed_headers th:nth-child(3) {
			width: 100px;
		}
		 
		 
		.fixed_headers thead {
			background-color: #678B38;
			color: #fdfdfd;
			font-size:12px;
		}
		.fixed_headers thead tr {
			display: block;
			position: relative;
		}
		.fixed_headers tbody {
			display: block;
			overflow: auto;
			width: 100%;
			height: 400px;
		}
		.fixed_headers tbody tr:nth-child(even) {
			background-color: #dddddd;
		}
		.old_ie_wrapper {
			height: 400px;
			width: 100%;
			overflow-x: hidden;
			overflow-y: auto;
		}
		.old_ie_wrapper tbody {
			height: auto;
		}
	</style>



<style type="text/css" >
	 
 
 
  
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

     function onblurtxtQty() {

         if (document.getElementById("ctl00_ContentPlaceHolder_txtQty").value == '') {
             
             return false;
         }

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtQty").value) == false) {
             alert("Please Enter Valid quantity!");
             document.getElementById("ctl00_ContentPlaceHolder_txtQty").focus();
             return false;
         }
       
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

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value) == false) {
             alert("Please Enter Valid price!");
             document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").focus();
             return false;
         }

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtQty").value) == false) {
             alert("Please Enter Valid quantity!");
             document.getElementById("ctl00_ContentPlaceHolder_txtQty").focus();
             return false;
         }

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtPack").value) == false) {
             alert("Please Enter Valid Pack Size!");
             document.getElementById("ctl00_ContentPlaceHolder_txtPack").focus();
             return false;
         }

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtunits").value) == false) {
             alert("Please Enter Valid Units!");
             document.getElementById("ctl00_ContentPlaceHolder_txtunits").focus();
             return false;
         }
         
         units = document.getElementById("ctl00_ContentPlaceHolder_txtunits").value;
         qty = document.getElementById("ctl00_ContentPlaceHolder_txtQty").value;
         unitprice = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value;

         total = units * unitprice;

         total = total.toFixed(2);

         document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal").value = total;


     }


     function Processuomtotalchange() {

         var total = 0.00;
         var units = 0;
         var qty = 0;
         var unitprice = 0.00;

         
         units = document.getElementById("ctl00_ContentPlaceHolder_txtunits").value;
         //alert('1');
        // alert(document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal"));
         total = document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal").value;
          
         //alert('2');
         unitprice = total / units;
         unitprice = unitprice.toFixed(2);
         //alert('3');
         document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = unitprice;


     }  
         


     function Processuomwithpacktotal() {

         var total = 0.00;
         var units = 0;
         var qty = 0;
         var unitprice = 0.00;

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value) == false) {
             alert("Please Enter Valid price!");
             document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").focus();
             return false;
         }

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtQty").value) == false) {
             alert("Please Enter Valid quantity!");
             document.getElementById("ctl00_ContentPlaceHolder_txtQty").focus();
             return false;
         }

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtPack").value) == false) {
             alert("Please Enter Valid Pack Size!");
             document.getElementById("ctl00_ContentPlaceHolder_txtPack").focus();
             return false;
         }

         if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_txtunits").value) == false) {
             alert("Please Enter Valid Units!");
             document.getElementById("ctl00_ContentPlaceHolder_txtunits").focus();
             return false;
         }

         var pack = 0;

         pack = document.getElementById("ctl00_ContentPlaceHolder_txtPack").value;
         qty = document.getElementById("ctl00_ContentPlaceHolder_txtQty").value;
         unitprice = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value;

         units = qty * pack;
         total = units * unitprice;

         total = total.toFixed(2);

         document.getElementById("ctl00_ContentPlaceHolder_txtunits").value = units ;
         document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal").value = total;


     }  
         
      


     function ProcessUOM() {

         if (req.readyState == 4) {
             if (req.status == 200) {

                 // alert(req.responseText);

                  var str = req.responseText;
                 // alert(str);
                  if (str != '') {
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
                      document.getElementById("ctl00_ContentPlaceHolder_txtPack").value = 0
                      document.getElementById("ctl00_ContentPlaceHolder_txtunits").value = 0;
                      document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal").value = 0;
                  }
                 

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
         var url = "AjaxItemsUpdate.aspx?" + key;
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
                 Subtotal();

             }
             else {
                 alert("There was a problem updating data:<br>" + req.statusText);
             }
         }

     }




     function Deleteitem(key, saveid) {
         Initialize();
         var url = "AjaxItemsDelete.aspx?" + key;

          //alert(url);
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
             $("#ItemID").val(str1[0]);
             //console.log(str1);
             //console.log(str1[0]);
             document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = str1[1];
             document.getElementById("ItemUnitPrice").value = str1[1];
             document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = str1[2];
             document.getElementById("name").value = str1[2];

             document.getElementById("ctl00_ContentPlaceHolder_txtcolor").value = str1[3];
             document.getElementById("ctl00_ContentPlaceHolder_txtuom").value = str1[4];
             document.getElementById("ItemUOM").value = str1[4];



           
             document.getElementById("ctl00_ContentPlaceHolder_txtDesc").value = '';
             document.getElementById("Description").value = '';
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

                 //HideDiv("dvitems");
                 //ShowDiv("newedititems");
                 //ShowDiv("new");
                 //HideDiv("update");
                 $('.not-valid').text('');
            $("#type").val('');
            $("#PurchaseLineNumber").val('');
            //$("#ItemID").val('');
            //$('#Description').val('');
            $('#OrderQty').val('');
            //$('#ItemUOM option').removeAttr("selected");
            $('#Pack').val('');
            $('#Size').val('');
                 $('#Unit').val('');
                 $('#itemBoxFee').val('');
                 $('#itemBoxPack').val('');
                 $('#itemBoxUom').val('');
                 $('#itemBoxType').val('');

            //$('#ItemUnitPrice').val('');
                 $('#Total').val('');

                 var method = $("#ctl00_ContentPlaceHolder_drpShipMethod").val();
                 //$("#txtDelType").text(method);
                 $("#txtDelType").text($("option[value='" + method + "']").text());


            $('#ItemBoxBrand option').removeAttr("selected");
            $('#ItemSleeve option').removeAttr("selected");
            $('#ItemCutStage option').removeAttr("selected");
            $('#CalculatedPrice').val('');
            $('#POItemModel').modal('show');
                 
                 document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = "";
                 //global_item_add = 1;
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
                                    PO Number
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

                    					<asp:ListItem Value="POFS" Text="POFS-PO Fresh standing"></asp:ListItem>
                     
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
                                 <tr id="trpay" runat="server"  >
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
                                
                                <tr>
                                <td width="110" height="15" valign="middle">
                                    Tracking number
                                </td>
                                <td width="101" height="15">
                                 <ajax:ajaxpanel id="AjaxPanel511" runat="server">
                                    
                                     <asp:TextBox ID="txtTrackingno" CssClass="form-control input-sm"   runat="server"></asp:TextBox>

                                 </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Order number
                                </td>
                                <td width="101" height="15">
                                 <ajax:ajaxpanel id="AjaxPanel1111" runat="server">
                                    
                                     <asp:TextBox ID="txtOrderno" CssClass="form-control input-sm"   runat="server"></asp:TextBox>

                                 </ajax:ajaxpanel>
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
                                            <asp:Label ID='lblCustomer'   runat='server'  Text="Vendor ID"></asp:Label>
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
        <div class="portlet-body form">
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
                                      <td>Address 1</td>
                                      <td colspan="3"  > <asp:TextBox CssClass="form-control input-sm"  ID="txtVendorAddress1" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Address 2</td>
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
    <div class="row">
					<div class="col-md-12">
                    <!-- BEGIN PORTLET-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                    	
							<div class="caption" style="width: 95%;">
                        <div class="row">
                            <div class="col-md-6">
							<div class="text-left">
							    Shipping Detail
							</div> 
							</div> 
							
                         </div>
							
							
							 </div> 
							
							
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
							
						</div>
						<div class="portlet-body">
							<div class="row">
                                <div class="col-12 col-md-4">
                                    <label>Shipping Method</label>
                                    <asp:DropDownList CssClass="form-control input-sm" ID="drpShipMethod"    runat="server" TabIndex="2">
                                                                <%--<asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>--%>
                                                                <%--<asp:ListItem Text="FedEx" Value="FedEx"></asp:ListItem>--%>
<%--                                                                <asp:ListItem Text="Ship" Value="Ship"></asp:ListItem>
                                                                <asp:ListItem Text="Pick Up" Value="Pick Up"></asp:ListItem>--%>
                                                                <asp:ListItem Text="FOB BOGOTA" Value="FOB_BOGOTA"></asp:ListItem>
                                                                <asp:ListItem Text="FOBFarm" Value="FOB Farm"></asp:ListItem>
                                                                <asp:ListItem Text="FOBMiami" Value="FOB Miami"></asp:ListItem>
                                                             </asp:DropDownList>
                                </div>
                                <div class="col-12 col-md-4">
                                    <label>Ship Date</label>
                                    <asp:TextBox ID="txtshipdate"   runat="server"  CssClass="form-control input-sm date-picker input-500" ></asp:TextBox>
                                </div>
                                <div class="col-12 col-md-4">
                                    <label>Departure Date</label>
                                    <asp:TextBox ID="txtArrivalDate"   runat="server"  CssClass="form-control input-sm date-picker input-500" ></asp:TextBox>
                                </div>
                                </div>
                        </div>
                    </div>
                </div>
        </div>
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
                                             <div align="left" class="box autocompleteTB" style="visibility: hidden;" id="autocomplete2"   ></div>   
                                           <div id="newedititems"    style="visibility: hidden;" class="box autocomplete">
                                            <div class="rows">
								             <div style="padding-top:2px;" class="col-md-12"    >

                                             <div class="row">
                                                <div class="col-md-2">
							                        <div class="text-center">
							                                ID <asp:TextBox ID="txtitemid"  CssClass="form-control input-sm"   runat="server"></asp:TextBox>
							                        </div> 
							                    </div> 

							                    <div class="col-md-4">
                                                    <div class="text-center">
                                                         Name <asp:TextBox ID="txtitemsearchDesc" CssClass="form-control input-sm" runat="server"></asp:TextBox>    
                                                    </div>
                                                </div>
                                                 <div class="col-md-6">
                                                    <div class="text-center">
                                                         Desc <asp:TextBox ID="txtDesc"  CssClass="form-control input-sm" runat="server"></asp:TextBox>    
                                                    </div>
                                                </div>
                                                 
                                                 
                                                 </div> 
                                                 
                                                  
                                                <div class="row"> 
                                                
                                                <div class="col-md-2">
                                                    <div class="text-center">
                                                        Qty <asp:TextBox ID="txtQty" CssClass="form-control input-sm" Text="1"  runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="text-center">
                                                           UOM  
                                                             <asp:DropDownList ID="txtuom" CssClass="form-control input-sm"   runat="server"   >
                                                             <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                             <asp:ListItem Text="BU" Value="BU"></asp:ListItem>
                                                             <asp:ListItem Text="BX" Value="BX"></asp:ListItem>
                                                             <asp:ListItem Text="CS" Value="CS"></asp:ListItem>
                                                             <asp:ListItem Text="EA" Value="EA"></asp:ListItem>
                                                             <asp:ListItem Text="ST" Value="ST"></asp:ListItem>
                                                             <asp:ListItem Text="HB" Value="HB"></asp:ListItem>
                                                         </asp:DropDownList>
                                                    </div>
                                                </div>
                                                  <div class="col-md-2">
                                                    <div class="text-center">
                                                         Pack <asp:TextBox ID="txtPack" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                    
                                                </div>

                                                  <div class="col-md-2">
                                                    <div class="text-center">
                                                         Units   <asp:TextBox ID="txtunits" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                    
                                                </div>

                                                 <div class="col-md-2">
                                                    <div class="text-center">
                                                     Price <asp:TextBox ID="txtitemsearchprice"   CssClass="form-control input-sm"   runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                                 <div class="col-md-2">
                                                    <div class="text-center">
                                                           Total <asp:TextBox ID="txtitemTotal" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                    
                                                </div>
                                               
                                                   
                                             </div>


<div class="row"> 
                                                    <div class="col-md-1">
                                                    <div class="text-center">
                                                         Color <asp:TextBox ID="txtcolor" CssClass="form-control input-sm"  runat="server"></asp:TextBox>    
                                                    </div>
                                                </div>
                                                
                                                <div class="col-md-8">
                                                    <div class="text-center">
                                                           Comments <asp:TextBox ID="txtComments" CssClass="form-control input-sm"  runat="server"></asp:TextBox> 
                                                    </div>
                                                </div>
                                                
                                                 
                                                  <div class="col-md-1">
                                                    <div id="new" style="padding-top:25px;" class="text-center">
                                                           <button id="sample_editable_1_new" class="btn green">
                                                            Add New <i class="fa fa-plus"></i>
                                                            </button>
                                                       </div> 
                                                </div> 
                                                 <div class="col-md-1">
                                                         
                                                      
                                                        <div id="update" style="padding-top:25px;" class="text-center">
                                                           <button id="sample_editable_1_update" class="btn green">
                                                            Update <i class="fa fa-plus"></i>
                                                            </button>
                                                       </div> 
                                                       
                                                  </div>
                                                   <div class="col-md-1">
                                                    <div style="padding-top:25px;" class="text-center">
                                                     
                                                    
                                                     <button id="sample_editable_1_cancel" class="btn red">
                                                            Cancel <i class="fa fa-minus"></i>
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
  
                                    
                                        
                                       
                                    <div id="dvitemsNew" style="padding-left:1rem;padding-right:1rem;">
                                      
                                       
  
                                      <div class="table-responsive">                    
                      <table class="table table-striped hover " id="table-Item" style="width: 100%;">
                        <thead>
                          <tr>  
                            <th>QTY</th> 
                            <th>Item Id</th> 
                             
                            <th>Description</th> 
                            <th>Box Type</th> 
                            <th>Pack</th> 
                            <th>Total Unit</th> 
                            <th>Unit Price</th> 
                            <th>Calculated Price</th> 
                            
                            <th>Total</th> 
                            <th>Margin (%)</th> 
                            <th>Action</th>                          
                          </tr>
                        </thead>
                        <tbody id="tbodyItem">
                         
                        </tbody>
                      </table>
                    </div>
                                    </div>
  
                                    <div id="dvitems"  class="row" style="display:none;">
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
                                        Color 
                                    </th>
                                    <th>
                                        Qty 
                                    </th>
                                    <th>
                                        UOM 
                                    </th>
                                    <th>
                                       Pack 
                                    </th>
                                     <th>
                                       Total Units 
                                    </th>
                                     <th>
                                       Price 
                                    </th>
                                    <th>
                                        Extended 
                                    </th>
                                    <th>
                                        Edit
                                    </th>
                                    <th>
                                        Delete
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
      <div class="modal fade" id="POItemModel" tabindex="-1" role="dialog" aria-labelledby="formModal" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document" style="width:80vw!important;">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title" style="display:inline-block" id="formModal">Item Detail</h2>
                    <label class="form-control border-0 text-center" style="display:inline-block;width:50%;border:none;" id="txtDelType"></label>
                    <button type="button" class="btn btn-danger" style="float:right" onclick="closeModal('POItemModel')">X
                    </button>
                </div>
                <div class="modal-body overflow-auto">
                    <div class="row m-0">
                        <%--Modal left side column 8--%>
                        <div class="col-12 col-md-12">
                            
                             <div class="row">                                
                                
                                 <div class="form-group col-12 col-md-2">                                     
                                    <label for="ItemID">Item ID</label>
                                     <input type="hidden" id="type"  />
                                     <input type="hidden" id="PurchaseLineNumber"  />
                                     
                                     
                                    <input id="ItemID" type="text" class="form-control" name="ItemID">
                                        
                               
                                 </div>
                                 <div class="form-group col-12 col-md-3">                                     
                                    <label for="name">Name</label>
                                    
                                    <input id="name" type="text" class="form-control" name="name">
                                        
                               
                                 </div>
                                <div class="form-group col-12 col-md-4">                                     
                                    <label for="Description">Description</label>                                     
                                    <input id="Description" type="text" class="form-control" name="Description">
                                       
                               
                                 </div>
                                <div class="form-group col-12 col-md-2">                                     
                                    <label for="Size">Size</label>                                     
                                    <input id="Size" type="text" class="form-control" name="Size">
                                        
                                
                                 </div>
                                 <div class="form-group col-12 col-md-1">                                     
                                    <label for="price">Price</label>                                     
                                    <input id="price" type="text" class="form-control" name="price" disabled="disabled">
                                        
                                
                                 </div>
                                
                            </div>
                            <div class="row">                                
                                <div class="form-group col-12 col-md-1">                                     
                                    <label for="OrderQty">Qty</label>                                     
                                    <input id="OrderQty" type="text" class="form-control calItemTotal" name="OrderQty">
                                        
                               
                                 </div>
                                 <div class="form-group col-6 col-md-2">                                     
                                    <label for="itemBoxType">Box Type</label>
                                    <select class="form-control calItemTotal" id="itemBoxType" >
                                                <option value="" selected="selected" >--Please Select --</option>
                                                <option value="FB">FB</option>
                                                <option value="HB">HB</option>                              
                                                <option value="QB">QB</option>                              
                                                                              
                                        </select>                 
                                 </div>
                                <div class="form-group col-6 col-md-2" style="display:none;">                                     
                                    <label for="itemBoxUom">UOM</label>
                                    <select class="form-control " id="itemBoxUom" >
                                                <option value="" selected="selected" >--Please Select --</option>
                                                <option value="Bunch">Bunch</option>
                                                <option value="Stem">Stem</option>                              
                                                <option value="Each">Each</option>                              
                                                                              
                                        </select>                        
                               
                                 </div>
                                
                                <div class="form-group col-6 col-md-2">                                     
                                    <label for="itemBoxPack">Stems/Bunch</label>                                     
                                    <input id="itemBoxPack" type="text" class="form-control calItemTotal" name="Pack">
                                       
                                 </div>
                                <div class="form-group col-6 col-md-2" style="display:none;">                                     
                                    <label for="ItemUOM">Item UOM</label>
                                    <select class="form-control " id="ItemUOM" >
                                                <option value="" selected="selected" >--Please Select --</option>
                                                <option value="Bunch">Bunch</option>
                                                <option value="Stem">Stem</option>                              
                                                <option value="Each">Each</option>                              
                                                                              
                                        </select>                        
                               
                                 </div>
                               
                                <div class="form-group col-6 col-md-2">                                     
                                    <label for="Pack">Stems/Box</label>                                     
                                    <input id="Pack" type="text" class="form-control calItemTotal" name="Pack">
                                        
                               
                                 </div>
                                
                                <div class="form-group col-6 col-md-1">                                     
                                    <label for="Unit">Total Unit</label>                                     
                                    <input id="Unit" type="text" class="form-control calItemTotal" name="Unit">
                                        
                               
                                 </div>
                                <div class="form-group col-6 col-md-2">                                     
                                    <label for="ItemUnitPrice">Cost</label>
                                    
                                     
                                     
                                    <input id="ItemUnitPrice" type="text" class="form-control calItemTotal" name="ItemUnitPrice">
                                        
                               
                                 </div>
                                
                                <div class="form-group col-6 col-md-2">                                     
                                    <label for="Total">Total</label>                                     
                                    <input id="Total" type="text" class="form-control" name="Total" disabled>
                                        
                               
                                 </div>
                                
                            </div>
                            <div class="row">                                
                                
                                 
                                <div class="form-group col-12 col-md-2">                                     
                                    <label for="ItemBoxBrand">Box Brand</label>                                     
                                    
                                     <select class="form-control " id="ItemBoxBrand" >                              
                                                                              
                                        </select>      
                               
                                 </div>
                                <div class="form-group col-12 col-md-2">                                     
                                    <label for="ItemSleeve">Sleeve</label>                                     
                                    
                                     <select class="form-control " id="ItemSleeve" >                             
                                                                              
                                        </select>    
                               
                                 </div>
                                
                                <div class="form-group col-12 col-md-2">                                     
                                    <label for="ItemCutStage">Cut Stage</label>
                                    
                                     <select class="form-control " id="ItemCutStage" >                              
                                                                              
                                        </select>   
                                     
                                    
                                        
                               
                                 </div>
                                 <div class="form-group col-12 col-md-2">                                     
                                    <label for="itemBoxFee">Freight</label>                                     
                                    <input id="itemBoxFee" type="text" class="form-control" name="itemBoxFee" disabled>
                                        
                               
                                 </div>

                                <div class="form-group col-12 col-md-2">                                     
                                    <label for="CalculatedPrice">Calculated Price</label>                                     
                                    <input id="CalculatedPrice" type="text" class="form-control" name="CalculatedPrice">
                                        
                               
                                 </div>

                                <div class="form-group col-12 col-md-2">                                     
                                    <label for="margin">Margin (%)</label>                                     
                                    <input id="margin" type="text" class="form-control" name="margin">
                                        
                               
                                 </div>
                                
                            </div>
                            
                            
                    </div>
                    <div class="text-right mt-3 mr-2" style="margin-top:1rem;">
                        <button type="button" class="btn btn-primary" onclick="insertData()">Submit</button>
                        <button type="button" class="btn btn-light" onclick="closeModal('POItemModel')" >Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </div>
      
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
							    Shipping Detail
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
                                                            <asp:Label ID="lblpo" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr> 

                                                    <tr>
                                      <td>Address</td>
                                      <td    > <asp:TextBox CssClass="form-control input-sm"  ID="txtLCAddress" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Address 2</td>
                                      <td    > <asp:TextBox CssClass="form-control input-sm"  ID="txtLCAddress2" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                     <tr>
                                      <td>Address 3</td>
                                      <td    > <asp:TextBox CssClass="form-control input-sm"  ID="txtLCAddress3" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                  
                                   
                                                </table> 
                                                 <table class="table table-striped table-hover table-bordered">
                                <tbody>
                                
                                    <tr>
                                      <td>City</td>
                                      <td>  <asp:TextBox ID="txtLCCity" CssClass="form-control input-sm"  runat="server" Text=''  ></asp:TextBox> </td>
                                        <td>State</td>
                                      <td  >  <asp:TextBox CssClass="form-control input-sm"  ID="txtLCstate" runat="server" Text=''  ></asp:TextBox> </td>
                                      
                                       
                                    </tr>
                                    <tr>
                                    <td  >Zip/Postal</td>
                                      <td   > <asp:TextBox ID="txtLCZip" runat="server" CssClass="form-control input-sm"  Text=''  ></asp:TextBox> </td>

                                     <td></td>
                                     <td></td>  
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
                                        <div class="form-body" >
                                         <Ajax:AjaxPanel ID="AjaxPanel20" runat="server">
                                          <table class="table table-striped table-hover table-bordered" style="display:none">
                                                 <tr>
                                                        <td>Ship Method</td>
                                                        <td>
                                                             
                                                             
                                                        </td>
                                                    </tr>
                                               <tr>
                                                        <td>Ship Date</td>
                                                        <td>  
                                                        
                                                        
                                                        <table>
                                                            <tr>
                                                                <td> <i class="fa fa-calendar"></i> </td>
                                                                <td>&nbsp;</td>
                                                                <td></td>
                                                            </tr>
                                                        </table>   
                                                        
                                                        
                                                                
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Departure Date</td>
                                                        <td>  
                                                        
                                                        
                                                        <table>
                                                            <tr>
                                                                <td> <i class="fa fa-calendar"></i> </td>
                                                                <td>&nbsp;</td>
                                                                <td></td>
                                                            </tr>
                                                        </table>   
                                                        
                                                        
                                                                
                                                        </td>
                                                    </tr>
                                                  
                                                 
                                                </table>
                                             <table class="table table-striped table-hover table-bordered" >
                                                 <tr>
                                                    <td colspan="2"  >Internal Notes</td>
                                                  </tr>
                                                  <tr>
                                                    <td colspan="2" >
                                                        <asp:TextBox ID="txtInternalNotes"   TextMode="MultiLine"   Rows="4"  CssClass="form-control input-xs"    runat="server"></asp:TextBox>
                                                         
                                                    </td>
                                                  </tr>
                                             </table>
                                                 <br /><br />
                                             <table class="table table-striped table-hover table-bordered">
                                                 <tr>
                                                          
                                                            <td style="width: 30%">
                                                                 <Ajax:AjaxPanel ID="AjaxPanel46" runat="server">
                                                                     Cargo agency<br />
                                                                    <asp:DropDownList ID="drpShipMethod1" runat="server" CssClass="form-control input-sm"
                                                                        AutoPostBack="True" data-tab-priority="14">
                                                                    </asp:DropDownList>  
                                                                </Ajax:AjaxPanel>
                                                            </td>
                                                            <td style="width: 30%">
                                                                 <Ajax:AjaxPanel ID="AjaxPanel45" runat="server">
                                                                     Trucking <br />
                                                                    <asp:DropDownList ID="drpShipMethod2" runat="server" CssClass="form-control input-sm"
                                                                        AutoPostBack="True" data-tab-priority="14">
                                                                    </asp:DropDownList>  
                                                                </Ajax:AjaxPanel>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <Ajax:AjaxPanel ID="AjaxPanel44" runat="server">
                                                                    Other <br />
                                                                    <asp:DropDownList ID="drpShipMethod3" runat="server" CssClass="form-control input-sm"
                                                                        AutoPostBack="True" data-tab-priority="14">
                                                                    </asp:DropDownList> 
                                                                </Ajax:AjaxPanel>
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
                                                  <td><asp:TextBox ID="txtTaxPercent" CssClass="form-control input-xs" runat="server"  AutoPostBack="True"  Text="0.00"   Enabled="True"   Width="50"></asp:TextBox></td>
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
                                                        <asp:Button ID="btnsave"   CausesValidation="false"    CssClass="btn btn-success btn-xs" runat="server"  Text="Save" Width="150" ></asp:Button>
                                                    </td>
                                                    <td colspan="2" class="text-center">
                                                        <asp:Button ID="btnpost"   CausesValidation="false"  CssClass="btn btn-success btn-xs" runat="server" Text="Book Purchase"  Width="150"  ></asp:Button>
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
                                         Order Logs
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


<script type="text/javascript" src="assets/scripts/table-editable.js?ver=4"></script>

<script type="text/javascript" >
    jQuery(document).ready(function() {

        TableEditable.init();
        Search.init();
    });
</script>
    <script>  
        var CompanyID = "<%=Session("CompanyID")%>";
        var DivisionID = "<%=Session("DivisionID")%>";
        var DepartmentID = "<%=Session("DepartmentID")%>";
        var PurchaseLineNumber ="";
          

        

        $(document).ready(function () {
            loaddata();
            var Z = 0.00;

            $("#CalculatedPrice").blur(function () {
                calculate();
            });

            $(".calItemTotal").blur(function () {
                calculate();
            });
            function calculate() {
                var qty = $("#OrderQty").val();
                var pack = $("#Pack").val();
                var boxpack = $("#itemBoxPack").val();
                var size = $("#itemBoxType").val();
                var itemUnitPrice = $("#ItemUnitPrice").val();
                var unit = 0;
                var total = 0;

                qty = ((qty != "") ? parseInt(qty) : 0); 
                boxpack = ((boxpack != "") ? parseInt(boxpack) : 0); 
                pack = ((pack != "") ? parseInt(pack) : 0);  
                itemUnitPrice = ((itemUnitPrice != "") ? parseFloat(itemUnitPrice) : 0); 

                unit = qty * pack;

                $("#Unit").val(unit);

                total = unit * itemUnitPrice;

                $("#Total").val(parseFloat(total).toFixed(2));

                var calPrice = $("#CalculatedPrice").val();
                calPrice = parseFloat(calPrice).toFixed(2)
                //if ($("#Pack").val() != "")
                //{
                //    if ($("#Size").val() != "")
                //    {
                //        unit = pack * size;
                //        $("#Unit").val(unit);
                //    }
                //    if ($("#ItemUnitPrice").val() != "")
                //    {
                //        total = unit * itemUnitPrice;
                //        $("#Total").val(total);
                //    }

                //}
                //if (total > 0 && size != "") {
                //    loadDelivryFee();
                //}
                if (total > 0 && calPrice > 0) {
                    var tp = (((parseFloat(calPrice) - parseFloat(total)) / parseFloat(total)) ) * 100;
        var marginVal = parseFloat(tp).toFixed(2);
        $("#margin").val(marginVal+"%");

                }
                
                $("#saveItemDetails").prop("disabled", false);

            }


        });
        function loadStart() {
            //$('#loader').waitMe({
            //    effect: 'win8_linear',
            //    text: '',
            //    bg: 'rgba(255,255,255,0.7)',
            //    color: '#036A37',
            //    waitTime: -1,
            //    textPos: 'vertical',
            //    onClose: function () { }
            //});
        }
        function loadStop() {
            //$('#loader').waitMe('hide');
        }

        function loaddata() {
            //alert('load');
            // loadStart();
            var PurchaseNumber = $("#<%=lblPurchaseOrderNumberData.ClientID%>").text(); // $("#OrderNumber").val();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PurchaseDetail?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&PurchaseNumber=${PurchaseNumber}`,
                dataType: 'json',
                error: function () {

                    applyDatatable('');
                    //  toastr.error('Server Not Found!');
                },
                success: function (response) {

                    if (response.length && response != "Failed") {
                        buildTableItem(response);
                        //loadStop();
                    }
                    else {
                        //   toastr.error('No Result Found!');
                        buildTableItem('');

                    }

                }

            });
        }


        function loadDelivryFee() {
           var boxtype = $("#itemBoxType").val();
            var DeliveryMethod = $("#ctl00_ContentPlaceHolder_drpShipMethod").val();
            if (DeliveryMethod != "FOBFarm") {
                $.ajax({
                    method: 'GET',
                    url: `https://secureapps.quickflora.com/V2/api/POMOE/fee?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&DeliveryMethod=${DeliveryMethod}&Size=${boxtype}`,
                    dataType: 'json',
                    error: function () {

                        alert('Something Went Wrong!');
                        //  toastr.error('Server Not Found!');
                    },
                    success: function (response) {
                        if (response.length && response != "Failed") {
                            $("#itemBoxFee").val(response);
                            var total = $("#Total").val();
                            if (total) {
                                total = parseFloat(total);
                                total = total - parseFloat(response);
                                $("#CalculatedPrice").val(parseFloat(total).toFixed(2));
                            } else {
                                $("#CalculatedPrice").val("0.00");
                            }

                        }
                        else {
                            $("#CalculatedPrice").val("0.00");
                        }

                    }

                });
            } else {
                var total = $("#Total").val();
                if (total) {
                    total = parseFloat(total);
                }
                            $("#itemBoxFee").val("0.00");
                            $("#CalculatedPrice").val(parseFloat(total).toFixed(2));

            }
        }
        
        function buildTableItem(JsonData) {
            var list = [];
            var txtOrderSub = 0;
            var mar = 0;
            if (JsonData) {
                JsonData.forEach(record => {
                    txtOrderSub = parseFloat(txtOrderSub) + parseFloat(record.Total);
                    if (parseFloat(record.CalculatedPrice) > 0 && parseFloat(record.Total) > 0) {
                        var tp = (((parseFloat(record.CalculatedPrice) - parseFloat(record.Total)) / parseFloat(record.Total))) * 100;
                        var marginVal = parseFloat(tp).toFixed(2);
                        mar = marginVal + "%";
                    } else {
                        mar = 0;
                    }
                    list.push([

                        record.OrderQty,
                        record.ItemID,
                        //record.ItemID,
                        record.Description,
                        //record.ItemBoxBrand+" "+
                        //record.ItemSleeve+" "+
                        //record.ItemCutStage,

                        record.itemBoxType,
                        record.Pack,
                        record.Unit,
                        parseFloat(record.ItemUnitPrice).toFixed(2),
                        //parseFloat(record.BoxFee).toFixed(2),
                        parseFloat(record.CalculatedPrice).toFixed(2),
                        parseFloat(record.Total).toFixed(2),
                        mar,
                        `<button type="button" class="btn btn-primary btn-sm" onclick="FillItem('${record.PurchaseLineNumber}','POItemModel')" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit Item"><i class="fa fa-pencil"></i></button>
                    <button type="button" class="btn btn-primary btn-sm" onclick="DelModal('${record.PurchaseLineNumber}','ConfirmModal')" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete Item"><i class="fa fa-trash-o"></i></button>`

                    ]);
                });
            }
            applyDatatable(list);
            //console.log(parseFloat(txtOrderSub).toFixed(2));
            $("#ctl00_ContentPlaceHolder_txtSubtotal").val(parseFloat(txtOrderSub).toFixed(2));
            
            Processtotal();
        }
       
        function applyDatatable(tableData) {
            loadStop();
            if ($.fn.DataTable.isDataTable("#table-Item")) {
                $("#table-Item").DataTable().destroy();
                $("#tbodyItem").html('');
            }

            $('#table-Item').DataTable({
                dom: 'frtBlip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'pdfHtml5',
                        title: 'Web App List',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    },
                ],
                title: 'Web App List',
                data: tableData

            });

        }
        
        function insertData()
        {
            var type = $("#type").val();
            if (type == "") {
                var formData =
                {
                    'CompanyID': CompanyID,
                    'DivisionID': DivisionID,
                    'DepartmentID': DepartmentID, 
                    'PurchaseNumber': $("#<%=lblPurchaseOrderNumberData.ClientID%>").text(),
                    'ItemID': $("#ItemID").val(),
                    'Description': $("#Description").val(),
                    'OrderQty': $("#OrderQty").val(),
                    'ItemUOM': $("#ItemUOM").val(),
                    'Pack': $("#Pack").val(),
                    'Size': $("#Size").val(),
                    'Unit': $("#Unit").val(),
                    'ItemUnitPrice': $("#ItemUnitPrice").val(),
                    'Total': $("#Total").val(),
                    'ItemBoxBrand': $("#ItemBoxBrand").val(),
                    'ItemSleeve': $("#ItemSleeve").val(),
                    'ItemCutStage': $("#ItemCutStage").val(),
                    'CalculatedPrice': $("#CalculatedPrice").val(),
                    'itemBoxType': $("#itemBoxType").val(),
                    'BoxUOM': $("#itemBoxUom").val(),
                    'BoxPack': $("#itemBoxPack").val(),
                    'itemBoxFee': $("#itemBoxFee").val()

                }
                $.ajax({
                    method: 'Post',
                    url: 'https://secureapps.quickflora.com/V2/api/PurchaseDetail',
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successful") {
                            closeModal('POItemModel');

                            loaddata();
                            // toastr.success("Added Successfully");

                        } else {
                            alert("Something went wrong");
                            // toastr.error("Something Went Wrong!");

                        }
                        loadStop();
                    }
                });
            }

            //======= UpDate ===
            if (type == "update") {
                var formData =
                {
                    'CompanyID': CompanyID,
                    'DivisionID': DivisionID,
                    'DepartmentID': DepartmentID,
                    'PurchaseLineNumber': $("#PurchaseLineNumber").val(),
                    'Description': $("#Description").val(),
                    'OrderQty': $("#OrderQty").val(),
                    'ItemUOM': $("#ItemUOM").val(),
                    'Pack': $("#Pack").val(),
                    'Size': $("#Size").val(),
                    'Unit': $("#Unit").val(),
                    'ItemUnitPrice': $("#ItemUnitPrice").val(),
                    'Total': $("#Total").val(),
                    'ItemBoxBrand': $("#ItemBoxBrand").val(),
                    'ItemSleeve': $("#ItemSleeve").val(),
                    'ItemCutStage': $("#ItemCutStage").val(),
                    'CalculatedPrice': $("#CalculatedPrice").val(),
                    'BoxUOM': $("#itemBoxUom").val(),
                    'BoxPack': $("#itemBoxPack").val(),
                    'itemBoxType': $("#itemBoxType").val(),
                    'itemBoxFee': $("#itemBoxFee").val()

                }

                $.ajax({
                    method: 'Put',
                    url: `https://secureapps.quickflora.com/V2/api/PurchaseDetail`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successful") {
                            closeModal('POItemModel');
                            loaddata();

                            //  toastr.success("Updated Successfully");

                        } else {
                            alert("Something went wrong");
                            //   toastr.error("Something Went Wrong!");

                        }
                        //loadStop();
                    }
                });

            }
        }





        
        function FillItem(PurchaseLineNumber, id)
        {

            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PurchaseDetail?OrderLineNumber=${PurchaseLineNumber}`,
                dataType: 'json',
                success: function (response) {
                    //alert(response);
                    if (response == "Failed") {
                        // toastr.error('Something Went Wrong!!');
                    }
                    else {
                       
                        $('#type').val("update");
                        

                        $("#PurchaseLineNumber").val(response[0].PurchaseLineNumber);
                        $("#ItemID").val(response[0].ItemID);
                        $("#Description").val(response[0].Description);
                        $("#OrderQty").val(response[0].OrderQty);
                        $("#ItemUOM").val(response[0].ItemUOM);
                        $("#Pack").val(response[0].Pack);
                        $("#Size").val(response[0].Size);
                        $("#itemBoxType").val(response[0].itemBoxType);
                        $("#name").val(response[0].ItemName);
                        $("#itemBoxFee").val(response[0].itemBoxFee);
                        $("#Unit").val(response[0].Unit);
                        $("#ItemUnitPrice").val(parseFloat(response[0].ItemUnitPrice).toFixed(2));
                        $("#Total").val(parseFloat(response[0].Total).toFixed(2));
                        $("#ItemBoxBrand").val(response[0].ItemBoxBrand);
                        $("#ItemSleeve").val(response[0].ItemSleeve);
                        $("#itemBoxUom").val(response[0].BoxUOM);
                        $("#itemBoxPack").val(response[0].BoxPack);
                        $("#ItemCutStage").val(response[0].ItemCutStage);
                        $("#price").val(parseFloat(response[0].SalePrice).toFixed(2));
                        $("#CalculatedPrice").val(parseFloat(response[0].CalculatedPrice).toFixed(2));
                        if (parseFloat(response[0].CalculatedPrice) > 0 && parseFloat(response[0].Total) > 0) {
                            var tp = (((parseFloat(response[0].CalculatedPrice) - parseFloat(response[0].Total)) / parseFloat(response[0].Total))) * 100;
                            var marginVal = parseFloat(tp).toFixed(2);
                            $("#margin").val(marginVal + "%");
                        } else {
                            $("#margin").val(0);
                        }
                        $('#' + id).modal('show');

                    }
                }

            });


         }

        //function (PurchaseLineNumber, id)
        //{
           
        //    var OrderLineid = $('#OrderLineid').val(PurchaseLineNumber);
        //    $('#confirmDelete').text('Are you sure to delete' + " " + "\"" + OrderLineid + "\"");

        //    $('#' + id).modal('show');
        //}
        function DelModal(PurchaseLineNumber) {
            //var OrderLineid = $('#OrderLineid').val();
            $.ajax({
                method: 'Delete',
                url: `https://secureapps.quickflora.com/V2/api/PurchaseDetail?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&OrderLineid=${PurchaseLineNumber}`,
                dataType: 'json',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                success: function (response) {
                    if (response == "Successful") {
                        //$('#ConfirmModal').modal('hide');
                        //toastr.success('Deleted Successfully!!');
                        loaddata();
                        
                    } else {
                        //toastr.error('Something Went Wrong!!')
                    }
                }

            });
        }
       


        


        
        
        function addModal(id) {
            $('.not-valid').text('');
            $("#type").val('');
            $("#PurchaseLineNumber").val('');
            $("#ItemID").val('');
            $('#Description').val('');
            $('#OrderQty').val('');
            $('#ItemUOM option').removeAttr("selected");
            $('#Pack').val('');
            $('#Size').val('');
            $('#Unit').val('');
            $('#ItemUnitPrice').val('');
            $('#Total').val('');

            $('#ItemBoxBrand option').removeAttr("selected");
            $('#ItemSleeve option').removeAttr("selected");
            $('#ItemCutStage option').removeAttr("selected");
            $('#CalculatedPrice').val('');
            $('#' + id).modal('show');
        }
        function closeModal(id) {
            $("#type").val('');
            $("#PurchaseLineNumber").val('');
            $("#ItemID").val('');
            $('#Description').val('');
            $('#OrderQty').val('');
            $('#ItemUOM option').removeAttr("selected");
            $('#Pack').val('');
            $('#Size').val('');
            $('#Unit').val('');
            $('#ItemUnitPrice').val('');
            $('#Total').val('');

            $('#ItemBoxBrand option').removeAttr("selected");
            $('#ItemSleeve option').removeAttr("selected");
            $('#ItemCutStage option').removeAttr("selected");
            $('#CalculatedPrice').val('');
            $('.not-valid').text('');
            $('#' + id).modal('hide');
        }

        $.getJSON("https://secureapps.quickflora.com/V2/api/BoxBrands?CompanyID=<%= Session("CompanyID") %>&DivisionID=<%= Session("DivisionID") %>&DepartmentID=<%= Session("DepartmentID") %>", function (data) {
                binddatadrpBoxBrand(data);
                
            });

              $.getJSON("https://secureapps.quickflora.com/V2/api/Sleeves?CompanyID=<%= Session("CompanyID") %>&DivisionID=<%= Session("DivisionID") %>&DepartmentID=<%= Session("DepartmentID") %>", function (data) {
                binddatadrpSleeve(data);
                
            });

              $.getJSON("https://secureapps.quickflora.com/V2/api/CutStages?CompanyID=<%= Session("CompanyID") %>&DivisionID=<%= Session("DivisionID") %>&DepartmentID=<%= Session("DepartmentID") %>", function (data) {
                binddatadrpCutStage(data);
                
            });

        function binddatadrpBoxBrand(data) {
        $('#ItemBoxBrand').append($('<option></option>').val('').text('---- Select Box Brand ----')); 
        $.each(data, function (key, value) {
            $('#ItemBoxBrand').append($('<option></option>').val(value.BoxBrandID).text(value.BoxBrandName)); 
        });
        
        }

                
      function binddatadrpSleeve(data) {
        $('#ItemSleeve').append($('<option></option>').val('').text('---- Select Sleeve ----')); 
        $.each(data, function (key, value) {
            $('#ItemSleeve').append($('<option></option>').val(value.SleeveID).text(value.SleeveName)); 
        });
        
        }

        function binddatadrpCutStage(data) {
        $('#ItemCutStage').append($('<option></option>').val('').text('---- Select Cut Stage ----')); 
        $.each(data, function (key, value) {
            $('#ItemCutStage').append($('<option></option>').val(value.Cut_StageID).text(value.Cut_StageName)); 
        });
        
        }

    </script>


</asp:Content>

