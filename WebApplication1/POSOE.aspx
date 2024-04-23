<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" ValidateRequest="false"  CodeFile="POSOE.aspx.vb" Inherits="POSOE" %>


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

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

    POS

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">


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

    function stopRKey(evt) {
        var evt = (evt) ? evt : ((event) ? event : null);
        var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        //alert(node.type);
        if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
    }

    document.onkeypress = stopRKey; 

</script>

    <script type="text/javascript" language="javascript">
        function TypedSearch() {
           <% GetAddressListByTypedSearch(true) %> 
        }

        function TypedCitySearch() {
           <% GetPostalCodeDropDownOnCitySearch() %> 
        }

        function findPos(obj) {
            var curtop = 0;
            if (obj.offsetParent) {
                do {
                    curtop += obj.offsetTop;
                } while (obj = obj.offsetParent);
            return [curtop];
            }
        }
    </script>

 
<script type="text/javascript">

    function stopRKey(evt) {
        var evt = (evt) ? evt : ((event) ? event : null);
        var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        //alert(node.type);
        if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
    }

    document.onkeypress = stopRKey; 

</script>

    <script type="text/javascript" language="javascript">
        function TypedSearch() {
           <% GetAddressListByTypedSearch(true) %> 
        }

        function TypedCitySearch() {
           <% GetPostalCodeDropDownOnCitySearch() %> 
        }

        function findPos(obj) {
            var curtop = 0;
            if (obj.offsetParent) {
                do {
                    curtop += obj.offsetTop;
                } while (obj = obj.offsetParent);
            return [curtop];
            }
        }
    </script>
    
   

        <script type="text/javascript">
            function open_fun() {
                document.getElementById('link').innerHTML = "<a class='pagelink' href='javascript:clo_fun()'>Hide Shipping Section</a>";
                var pnl = document.getElementById('dvship');
                pnl.style.display = "block";


                var pnl1 = document.getElementById('dvship1');
                pnl1.style.display = "block";
            }
            function clo_fun() {
                document.getElementById('link').innerHTML = "<a class='pagelink' href='javascript:open_fun()'>Show Shipping Section</a>";
                var pnl = document.getElementById('dvship');
                pnl.style.display = "none";

                var pnl1 = document.getElementById('dvship1');
                pnl1.style.display = "none";

            }

            function open_fun1() {
                document.getElementById('link1').innerHTML = "<a class='pagelink' href='javascript:clo_fun1()'>Hide Billing Section</a>";
                var pnl = document.getElementById('dvbill');
                pnl.style.display = "block";


                var pnl1 = document.getElementById('dvbill1');
                pnl1.style.display = "block";

                var pnl2 = document.getElementById('dvbill2');
                pnl2.style.display = "block";

            }
            function clo_fun1() {
                document.getElementById('link1').innerHTML = "<a class='pagelink' href='javascript:open_fun1()'>Show Billing Section</a>";
                var pnl = document.getElementById('dvbill');
                pnl.style.display = "none";


                var pnl1 = document.getElementById('dvbill1');
                pnl1.style.display = "none";

                var pnl2 = document.getElementById('dvbill2');
                pnl2.style.display = "none";
            }





            function clo_All() {

                var pnl = document.getElementById('link1');
                pnl.style.display = "none";


                var pnl1 = document.getElementById('link');
                pnl1.style.display = "none";
            }
            
    </script>

 	
 

    <script language="javascript">


        var zChar = new Array(' ', '(', ')', '-', '.');
        var maxphonelength = 13;
        var phonevalue1;
        var phonevalue2;
        var cursorposition;

        function ParseForNumber1(object) {
            phonevalue1 = ParseChar(object.value, zChar);
        }
        function ParseForNumber2(object) {
            phonevalue2 = ParseChar(object.value, zChar);
        }

        function backspacerUP(object, e) {
            if (e) {
                e = e
            } else {
                e = window.event
            }
            if (e.which) {
                var keycode = e.which
            } else {
                var keycode = e.keyCode
            }

            ParseForNumber1(object)

            if (keycode >= 48) {
                ValidatePhone(object)
            }
        }

        function backspacerDOWN(object, e) {
            if (e) {
                e = e
            } else {
                e = window.event
            }
            if (e.which) {
                var keycode = e.which
            } else {
                var keycode = e.keyCode
            }
            ParseForNumber2(object)
        }

        function GetCursorPosition() {

            var t1 = phonevalue1;
            var t2 = phonevalue2;
            var bool = false
            for (i = 0; i < t1.length; i++) {
                if (t1.substring(i, 1) != t2.substring(i, 1)) {
                    if (!bool) {
                        cursorposition = i
                        bool = true
                    }
                }
            }
        }

        function ValidatePhone(object) {

            var p = phonevalue1

            p = p.replace(/[^\d]*/gi, "")

            if (p.length < 3) {
                object.value = p
            } else if (p.length == 3) {
                pp = p;
                d4 = p.indexOf('(')
                d5 = p.indexOf(')')
                if (d4 == -1) {
                    pp = "(" + pp;
                }
                if (d5 == -1) {
                    pp = pp + ")";
                }
                object.value = pp;
            } else if (p.length > 3 && p.length < 7) {
                p = "(" + p;
                l30 = p.length;
                p30 = p.substring(0, 4);
                p30 = p30 + ")"

                p31 = p.substring(4, l30);
                pp = p30 + p31;

                object.value = pp;

            } else if (p.length >= 7) {
                p = "(" + p;
                l30 = p.length;
                p30 = p.substring(0, 4);
                p30 = p30 + ")"

                p31 = p.substring(4, l30);
                pp = p30 + p31;

                l40 = pp.length;
                p40 = pp.substring(0, 8);
                p40 = p40 + "-"

                p41 = pp.substring(8, l40);
                ppp = p40 + p41;

                object.value = ppp.substring(0, maxphonelength);
            }

            GetCursorPosition()

            if (cursorposition >= 0) {
                if (cursorposition == 0) {
                    cursorposition = 2
                } else if (cursorposition <= 2) {
                    cursorposition = cursorposition + 1
                } else if (cursorposition <= 5) {
                    cursorposition = cursorposition + 2
                } else if (cursorposition == 6) {
                    cursorposition = cursorposition + 2
                } else if (cursorposition == 7) {
                    cursorposition = cursorposition + 4
                    e1 = object.value.indexOf(')')
                    e2 = object.value.indexOf('-')
                    if (e1 > -1 && e2 > -1) {
                        if (e2 - e1 == 4) {
                            cursorposition = cursorposition - 1
                        }
                    }
                } else if (cursorposition < 11) {
                    cursorposition = cursorposition + 3
                } else if (cursorposition == 11) {
                    cursorposition = cursorposition + 1
                } else if (cursorposition >= 12) {
                    cursorposition = cursorposition
                }

                var txtRange = object.createTextRange();
                txtRange.moveStart("character", cursorposition);
                txtRange.moveEnd("character", cursorposition - object.value.length);
                txtRange.select();
            }

        }

        function ParseChar(sStr, sChar) {
            if (sChar.length == null) {
                zChar = new Array(sChar);
            }
            else zChar = sChar;

            for (i = 0; i < zChar.length; i++) {
                sNewStr = "";

                var iStart = 0;
                var iEnd = sStr.indexOf(sChar[i]);

                while (iEnd != -1) {
                    sNewStr += sStr.substring(iStart, iEnd);
                    iStart = iEnd + 1;
                    iEnd = sStr.indexOf(sChar[i], iStart);
                }
                sNewStr += sStr.substring(sStr.lastIndexOf(sChar[i]) + 1, sStr.length);

                sStr = sNewStr;
            }

            return sNewStr;
        }
</script>

         
            
<asp:ScriptManager ID="ScriptManager1" runat="server">
  <Services >
        <asp:ServiceReference Path="AutoCompleteAjax.asmx" />
        
   </Services>
</asp:ScriptManager>  

<script type="text/javascript">

    function stopRKey(evt) {
        var evt = (evt) ? evt : ((event) ? event : null);
        var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
        if ((evt.keyCode == 13)) { return false; }
    }

    // document.onkeypress = stopRKey; 

</script>

    
    <!----------------------Ends here-------------------------->
   <script type="text/javascript" language="javascript">
       function drpstoredcc_Change() {
           return true;
           alert("As a security precaution, we are temporarily disabling the 'Stored' credit card function in the POS and Order Entry module while we perform a security audit over the next several weeks.  This will also affect the 'DUP' feature that relates to any orders with credit cards.\n\nWe apologize for this inconvenience and hope to have this operational for all our users as soon as possible.  In the meantime, you will need to ask customers for the full credit card number when taking an order during this time period.  ");
           return false;
       }
            
        </script>
        

         <input id="Hidden1"  type="hidden" runat="server" />

     <script language="javascript" type="text/javascript" >

         function ShowPopup(panel, gridviewRow) {
             var row = document.getElementById(gridviewRow);
             row.style.backgroundColor = "#F7F1BE";
             row.style.color = "#456037";
             var pnl = document.getElementById(panel);
             pnl.style.display = "block";


         }

         //Hides DIV popup commands for gridview
         function HidePopup(panel, gridviewRow, alternateRow) {
             var row = document.getElementById(gridviewRow);
             if (alternateRow == "1") {
                 row.style.backgroundColor = "#E6E3CB";
                 row.style.color = "#000000";
             }
             else {
                 row.style.backgroundColor = "#CAC590";
                 row.style.color = "#000000";
             }

             var pnl = document.getElementById(panel);
             pnl.style.display = "none";

         }
    
    </script>          
        
                        <!--Script code addded by imtiyaz for display message in div while transmit button is used -->
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


     function Subtotal() {
         Initialize();

         var start = 0;
         start = new Date();
         start = start.getTime();

         var url = "AjaxItemsOrderSubtotal.aspx?OrderNumber=" + document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value + "&start=" + start;

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

         var _checkDiscounts = document.getElementById("ctl00_ContentPlaceHolder_checkDiscounts").checked;

         //alert('hi');
         if (_checkDiscounts) {
            // alert('hi in');
            // alert(document.getElementById("ctl00_ContentPlaceHolder_drpdiscounttype"));
             
             if (document.getElementById("ctl00_ContentPlaceHolder_drpdiscounttype").value == "Flat") {
                 document.getElementById("ctl00_ContentPlaceHolder_txtDiscountAmount").value = document.getElementById("ctl00_ContentPlaceHolder_txtdiscounttypevalue").value;
             }

             if (document.getElementById("ctl00_ContentPlaceHolder_drpdiscounttype").value == "%") {
                 var disc = 0.00;
                 var discperc = 0.00;
                 var discsubtotal = 0.00;

                 discsubtotal = document.getElementById("ctl00_ContentPlaceHolder_txtSubtotal").value;
                 discperc = document.getElementById("ctl00_ContentPlaceHolder_txtdiscounttypevalue").value;

                 disc = discsubtotal * discperc;
                 disc = disc / 100;

                 document.getElementById("ctl00_ContentPlaceHolder_txtDiscountAmount").value = disc.toFixed(2);
             }
         }
         else {
             DiscountAmount = document.getElementById("ctl00_ContentPlaceHolder_txtDiscountAmount").value;
         }

         DiscountAmount = document.getElementById("ctl00_ContentPlaceHolder_txtDiscountAmount").value;
         Delivery = document.getElementById("ctl00_ContentPlaceHolder_txtDelivery").value;
         Relay = document.getElementById("ctl00_ContentPlaceHolder_txtRelay").value;
         Service = document.getElementById("ctl00_ContentPlaceHolder_txtService").value;

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

         subtotal = parseFloat(subtotal) - parseFloat(DiscountAmount);

         taxamnt = (parseFloat(subtotal) * parseFloat(taxperc)) / 100;

         //alert(taxamnt);

         document.getElementById("ctl00_ContentPlaceHolder_txtTax").value = taxamnt.toFixed(2);
         taxamnt = taxamnt.toFixed(2);
         total = parseFloat(subtotal) + parseFloat(taxamnt);

         document.getElementById("ctl00_ContentPlaceHolder_txtTotal").value = total.toFixed(2);

     }

     function Saveitem(key, saveid) {
         Initialize();
         var url = "AjaxItemsSave.aspx?" + key;

         global_item_add = 0;

         //          alert(saveid);
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


     function Updateitem(key, saveid) {
         Initialize();
         var url = "AjaxItemsUpdate.aspx?" + key;
         //alert(url);
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

     //new code added



     function SendQuery(key) {
         Initialize();
         var url = "AjaxCustomerSearch.aspx?k=" + key;

         if (req != null) {
             req.onreadystatechange = Process;
             req.open("GET", url, true);
             req.send(null);

         }

     }


     function SendQuery2(key) {

         

         // alert(document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value);
         var start = 0;
         start = new Date();
         start = start.getTime();
         
         Initialize();
         var url = "AjaxItemsSearch.aspx?k=" + key + "&start=" + start + "&lc=" + document.getElementById("ctl00_ContentPlaceHolder_txtCustomerTypeID").value;
         //alert(url);
         if (req != null) {
             req.onreadystatechange = Process2;
             req.open("GET", url, true);
             req.send(null);

         }

     }


     /////////////////////////////////////////////////////////////////////////////////////////////////// Starting Ajax search on postal code here
     function SendQuery3(key) {

         //alert("getting this");
         Initialize();
         var url = "AjaxPostalCodeSearch.aspx?k=" + key + ',' + document.getElementById("ctl00_ContentPlaceHolder_drpShipCountry").value;

         if (req != null) {
             req.onreadystatechange = Process3;
             req.open("GET", url, true);
             req.send(null);

         }

     }

     function postalcodesearchcloseProcess() {

         //alert("close box");
         //         document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
         //         document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundRepeat = 'no-repeat';
         //         document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundPositionX = 'left';
         document.getElementById("<%=txtShippingZip.ClientID%>").value = "";
         document.getElementById("autocomplete3").innerHTML = "";
         HideDiv("autocomplete3");


     }


     function postalcodesearchBlurProcess() {

         if (document.getElementById("<%=txtShippingZip.ClientID%>").value == "") {
             HideDiv("autocomplete3");
             //document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
         }

     }

     function Process3() {
         document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
         document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundRepeat = 'no-repeat';
         document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundPositionX = 'right';
         document.getElementById("<%=txtShippingZip.ClientID%>").style.forecolor = 'white';

         if (req.readyState == 4) {
             // only if "OK"
             if (req.status == 200) {
                 document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundImage = 'url()';
                 if (req.responseText == "") {

                     //                     document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
                     //                     document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundRepeat = 'no-repeat';
                     //                     document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundPositionX = 'left';
                     document.getElementById("<%=txtShippingZip.ClientID%>").value = "";
                     var newdiv = document.createElement("div");
                     newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some code.</div>";
                     var container = document.getElementById("autocomplete3");
                     container.innerHTML = "<div style='text-align:center' ><a href='javascript:postalcodesearchcloseProcess();' >Close Search result</a></div>";
                     container.appendChild(newdiv)
                 }
                 else {


                     if (document.getElementById("<%=txtShippingZip.ClientID%>").value != "") {
                         ShowDiv("autocomplete3");
                         //document.getElementById("autocomplete2").innerHTML = req.responseText;

                         var newdiv = document.createElement("div");
                         newdiv.innerHTML = req.responseText;
                         var container = document.getElementById("autocomplete3");
                         container.innerHTML = "<div style='text-align:center' ><a href='javascript:postalcodesearchcloseProcess();' >Close Search result</a></div>";
                         container.appendChild(newdiv)
                     }
                 }
             }
             else {
                 ShowDiv("autocomplete3");
                 //document.getElementById("autocomplete2").innerHTML = req.responseText;

                 var newdiv = document.createElement("div");
                 newdiv.innerHTML = req.responseText;
                 var container = document.getElementById("autocomplete3");
                 container.innerHTML = "<div style='text-align:center' ><a href='javascript:postalcodesearchcloseProcess();' >Close Search result</a></div>";
                 container.appendChild(newdiv)
             }
         }
     }

     function FillSearchtextBox3(val, city, state) {
         // alert(val);
         document.getElementById("<%=txtShippingZip.ClientID%>").value = val;
         document.getElementById("<%=txtShippingCity.ClientID%>").value = city;
         document.getElementById("<%=drpShippingState.ClientID%>").value = state;

         //         alert(city);
         //         alert(state);

         document.getElementById("autocomplete3").innerHTML = "";
         HideDiv("autocomplete3");
         document.getElementById("<%=txtShippingZip.ClientID%>").style.backgroundImage = 'url()';
         document.getElementById("<%=ImgPostalCode.ClientID%>").focus();
         document.getElementById("<%=ImgPostalCode.ClientID%>").click();

         //alert(document.getElementById("<%=ImgUpdateSearchitems.ClientID%>"));
         //ImgUpdateSearchitems
     }

     ///////////////////////////////////////////////////////////////////////////////////////end up here for ajax search for postal code

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

     function Process() {

         document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
         document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
         document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'right';



         if (req.readyState == 4) {
             // only if "OK"
             if (req.status == 200) {
                 document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url()';
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


     function BodyLoad() {
         HideDiv("autocomplete");
         HideDiv("autocomplete2");
         //document.form1.keyword.focus();
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
        // document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").focus();
        // document.getElementById("ctl00_ContentPlaceHolder_ImgUpdateSearchitems").click();
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

             document.getElementById("autocomplete2").innerHTML = "";
             HideDiv("autocomplete2");
             document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';
             //alert(val);
             if (val.length > 3) {
                 document.getElementById("sample_editable_1_new").focus();
                 document.getElementById("sample_editable_1_new").click();
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

     

     function jumptodiv() {
         document.getElementById("<%=divordertransmiterror.ClientID%>").innerHTML = "<b>Please wait your request is procesing........</b>"

     }

     function drpShipMethod_Change() {
        

         if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "Will Call") {
             if (document.getElementById("<%=drpShipMethod.ClientID%>").value == "Taken") {
                 alert("Please Select Different Ship Method With Will Call");
                 document.getElementById("<%=drpShipMethod.ClientID%>").focus();
                 return false;
             }
         }

     }

                           </script>

 <script>

 
     var mode = 0
     function blinkscroll() {
         if (document.getElementById("<%=lblPricerange.ClientID%>") != null) {
             if (mode == 0) {
                 document.getElementById("<%=lblPricerange.ClientID%>").style.color = "white";
             }
             else {
                 document.getElementById("<%=lblPricerange.ClientID%>").style.color = "#456037";
             }
             mode = (mode == 0) ? 1 : 0
         }
     }
     var invmode = 0

     

     function inventoryblinkroll() {
         if (document.getElementById("<%=lblInventoryStatus.ClientID%>") != null) {
             if (invmode == 0) {
                 document.getElementById("<%=lblInventoryStatus.ClientID%>").style.color = "red";
             }
             else {
                 document.getElementById("<%=lblInventoryStatus.ClientID%>").style.color = "#456037";
             }
             invmode = (invmode == 0) ? 1 : 0
         }
     }
     setInterval("inventoryblinkroll()", 400)
     setInterval("blinkscroll()", 400) 
</script>


   

   
     
     <script type="text/javascript" language="javascript">

         function Itemsearch() {
             var CustomerIDTemp = " ";

             window.open("ItemDetailsSearch.aspx?CustomerID=" + CustomerIDTemp + "", 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=650,height=400,Left=350,top=275,screenX=0,screenY=275,alwaysRaised=yes');


         }


         function VendorSearch() {

             if (document.getElementById("<%=drpShippingState.ClientID%>") != null) {

                 var State = document.getElementById("<%=drpShippingState.ClientID%>").value;

                 var City = document.getElementById("<%=txtShippingCity.ClientID%>").value;
                 window.open("VendorDetails.aspx?State=" + State + "&City=" + City + "", 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=775,height=400,Left=200,top=275,screenX=0,screenY=275,alwaysRaised=yes');

             }

             else {

                 var State = document.getElementById("<%=drpShippingState.ClientID%>").value
                 var City = document.getElementById("<%=txtShippingCity.ClientID%>").value
                 window.open("VendorDetails.aspx?State=" + State + "&City=" + City + "", 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=775,height=400,Left=200,top=275,screenX=0,screenY=275,alwaysRaised=yes');


             }

         }
         function changeIt() {
         

             var val;
             val = document.getElementById("<%=drpCountry.ClientID%>").value;
             objDiv = document.getElementById("dvCustomerState");
             if (val == "united states") {
                 //document.getElementById("dvDrpState").style.display="block";
                 //document.getElementById("dvDrpState").style.zIndex="100";
                 document.getElementById("dvCustomerState").style.zIndex = "0";
                 document.getElementById("dvCustomerState").style.display = "none";
                 document.getElementById("dvCustomerState").style.position = "absolute";
                 document.getElementById("dvDrpState").style.position = "relative";

                 // objDiv.innerHTML= "<select id='Country'> <option value='blore'> bangalore </option>	<option value='kochi'> kochi </option> </select>";
                 // document.getElementById("ctl00_ContentPlaceHolder_txtCustomerState").style.visibility="hidden";
             }
             else {
                 document.getElementById("dvCustomerState").style.display = "block";
                 document.getElementById("dvCustomerState").style.zIndex = "100";
                 document.getElementById("dvDrpState").style.zIndex = "0";
                 document.getElementById("dvDrpState").style.display = "none";
                 document.getElementById("dvDrpState").style.position = "absolute";
                 document.getElementById("dvCustomerState").style.position = "relative";
             }
         }


         function ChkAddress() {

             if (document.getElementById("ChkBillAddress").checked) {



                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingName").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerFirstName").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingLastName").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerLastName").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAttention").value = document.getElementById("ctl00_ContentPlaceHolder_txtAttention").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCompany").value = document.getElementById("ctl00_ContentPlaceHolder_txtCompany").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAddress1").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerAddress1").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAddress2").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerAddress2").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAddress3").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerAddress3").value;
                 //document.getElementById("ctl00_ContentPlaceHolder_drpShippingState").value=document.getElementById("ctl00_ContentPlaceHolder_drpState").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingZip").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerZip").value;
                 document.getElementById("ctl00_ContentPlaceHolder_drpShipCountry").value = document.getElementById("ctl00_ContentPlaceHolder_drpCountry").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCustomerPhone").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerPhone").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipExt").value = document.getElementById("ctl00_ContentPlaceHolder_txtExt").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCustomerFax").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerFax").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingCity").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerCity").value;
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCustomerCell").value = document.getElementById("ctl00_ContentPlaceHolder_txtCustomerCell").value;

             }
             else {
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingName").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingLastName").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAttention").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCompany").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAddress1").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAddress2").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingAddress3").value = "";
                 //document.getElementById("ctl00_ContentPlaceHolder_drpShippingState").value="";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingZip").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_drpShipCountry").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCustomerPhone").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipExt").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCustomerFax").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShippingCity").value = "";
                 document.getElementById("ctl00_ContentPlaceHolder_txtShipCustomerCell").value = "";
             }

         }

         function MyConfirm() {
             needToConfirm = false;
             return confirm('Do you wish to delete all this order data?');
         }


         function CheckValues() {
             needToConfirm = false;
             //alert(cmblocationid);
             


             if (window.document.getElementById("<%=txtCheck.ClientID%>") != null) {
                 if (window.document.getElementById("<%=txtCheck.ClientID%>").value == "Approved") {
                     alert('Please note :\r\r\"Your Order is being processed\r\r\t Please Wait......');
                     //document.getElementById("<%=BtnBookOrder.ClientID%>").disabled = true; 
                     return true;
                 }
             }

             //New Validations Section start
             //var regex = new RegExp("^[-+]?(\[0-9]*\.\[0-9]{1,2})$");
             var regex = new RegExp("^-?([0-9]+(\.[0-9]{1,2})?|\.[0-9]{1,2})$");
             var checkReg = true;
             var errMsg = [];

             errMsg[errMsg.length] = "Form Error Alert!\n";

             if (!regex.test(document.getElementById("<%=txtDelivery.ClientID%>").value) && document.getElementById("<%=txtDelivery.ClientID%>").value.trim() != "") {
                 document.getElementById("<%=txtDelivery.ClientID%>").style.backgroundColor = '#FF3D35';
                 errMsg[errMsg.length] = "\nDelivery Charge: Invalid Amount format";
                 checkReg = false;
             }
             else if (document.getElementById("<%=txtDelivery.ClientID%>").value < 0) {
                 document.getElementById("<%=txtDelivery.ClientID%>").style.backgroundColor = '#FF3D35';
                 errMsg[errMsg.length] = "\nDelivery Charge: Must be positive value";
                 checkReg = false;
             }
             else {
                 document.getElementById("<%=txtDelivery.ClientID%>").style.backgroundColor = '';
             }




             if (!regex.test(document.getElementById("<%=txtService.ClientID%>").value) && document.getElementById("<%=txtService.ClientID%>").value.trim() != "") {
                 document.getElementById("<%=txtService.ClientID%>").style.backgroundColor = '#FF3D35';
                 errMsg[errMsg.length] = "\nService Charge: Invalid Amount format";
                 checkReg = false;
             }
             else {
                 document.getElementById("<%=txtService.ClientID%>").style.backgroundColor = '';
             }

             if (!regex.test(document.getElementById("<%=txtRelay.ClientID%>").value) && document.getElementById("<%=txtRelay.ClientID%>").value.trim() != "") {
                 document.getElementById("<%=txtRelay.ClientID%>").style.backgroundColor = '#FF3D35';
                 errMsg[errMsg.length] = "\nRelay Charge: Invalid Amount format";
                 checkReg = false;
             }
             else {
                 document.getElementById("<%=txtRelay.ClientID%>").style.backgroundColor = '';
             }

             if ("<%=Opt_Employee%>" == "1") {
                 if (document.getElementById("<%=drpEmployeeID.ClientID%>").value == "") {
                     document.getElementById("<%=drpEmployeeID.ClientID%>").style.backgroundColor = '#FF3D35';
                     errMsg[errMsg.length] = "\nEmployee ID: Must Select";
                     checkReg = false;
                 }
                 else
                     document.getElementById("<%=drpEmployeeID.ClientID%>").style.backgroundColor = '';
             }

             if (document.getElementById("<%=txtCustomerEmail.ClientID%>").value != "") {
                 var EmailValue = document.getElementById("<%=txtCustomerEmail.ClientID%>").value;
                 if (!EmailValue.match(/^([\w\-\.]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$/)) {
                     errMsg[errMsg.length] = "\nCustomer Email: Invalid Email format";
                     document.getElementById("<%=txtCustomerEmail.ClientID%>").style.backgroundColor = '#FF3D35';
                     checkReg = false;
                 }
                 else
                     document.getElementById("<%=txtCustomerEmail.ClientID%>").style.backgroundColor = '';
             }

             //Added by GG 
             if ("<%=Opt_Priority%>" == "1") {
                 if (document.getElementById("<%=drpPriorirty.ClientID%>").value == "0") {
                     errMsg[errMsg.length] = "\nPriority: Must Select";
                     document.getElementById("<%=drpPriorirty.ClientID%>").style.backgroundColor = '#FF3D35';
                     checkReg = false;
                 }
                 else
                     document.getElementById("<%=drpPriorirty.ClientID%>").style.backgroundColor = '';
             }

             if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "0") {
                 errMsg[errMsg.length] = "\nPayment Type: Must Select";
                 document.getElementById("<%=drpPaymentType.ClientID%>").style.backgroundColor = '#FF3D35';
                 checkReg = false;
             }
             else {
                 document.getElementById("<%=drpPaymentType.ClientID%>").style.backgroundColor = '';
             }

             if (!checkReg) { alert(errMsg.join('')); return false; }

             //New Validations Section End




             if (window.document.getElementById("<%=drpPaymentType.ClientID%>").value == "EMV-Debit") {
                 if (window.document.getElementById("<%=txtCheck.ClientID%>").value == "") {
                     alert('Please first process emv payment');
                     return false;
                 }
             }


             //if ("<%=Opt_Employee%>"=="1")
             //{
             //if (document.getElementById("<%=drpEmployeeID.ClientID%>").value=="")
             //{
             //alert("Please Select Employee ID");
             //document.getElementById("<%=drpEmployeeID.ClientID%>").focus();

             // return false;
             //}
             //}

             ///if (document.getElementById("<%=txtCustomerEmail.ClientID%>").value != ""){
             //var EmailValue=document.getElementById("<%=txtCustomerEmail.ClientID%>").value;
             //if (!EmailValue.match(/^([\w\-\.]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$/))
             //{
             //alert("Invalid Email, Please enter correct Email");
             // document.getElementById("<%=txtCustomerEmail.ClientID%>").select();
             //document.getElementById("<%=txtCustomerEmail.ClientID%>").focus();
             // return false;
             //}
             //}


             //Added by GG 
             //if ("<%=Opt_Priority%>" == "1") {
             //if (document.getElementById("<%=drpPriorirty.ClientID%>").value == "0") {
             ////alert("Please Select Priority");
             //document.getElementById("<%=drpPriorirty.ClientID%>").focus();

             /// return false;
             //}
             //}




             // if (document.getElementById("<%=drpPaymentType.ClientID%>").value=="0")
             //{
             //alert("Please Enter Payment Type  ");
             //document.getElementById("<%=drpPaymentType.ClientID%>").focus();

             //return false;
             //}
             if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "Credit Card") {


                 if (document.getElementById("<%=drpstoredcc.ClientID%>").value == "Other") {

                     if (document.getElementById("<%=txtCard.ClientID%>").value == "") {
                         alert("Please Enter Card Number");
                         document.getElementById("<%=txtCard.ClientID%>").focus();
                         return false;
                     }


                     if (IsNumeric(document.getElementById("<%=txtCard.ClientID%>").value) == false) {
                         alert("Please Enter Card Number only numeric value!");
                         document.getElementById("<%=txtCard.ClientID%>").focus();
                         return false;
                     }



                     if (document.getElementById("<%=txtCSV.ClientID%>").value == "") {
                         if ("<%=publicCVSCHECK %>" == "true") {
                             var n;
                             n = confirm("You have not provided the CVV. Press 'OK' to ignore or press 'Cancel' to enter CVV.");
                             if (n) {
                                 return true;
                             }
                             else {
                                 document.getElementById("<%=txtCSV.ClientID%>").focus();
                                 return false;
                             }
                         }
                     }

                 }

             }
             if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "Amex") {
                 if (document.getElementById("<%=txtCard.ClientID%>").value == "") {
                     alert("Please Enter Card Number");
                     document.getElementById("<%=txtCard.ClientID%>").focus();
                     return false;
                 }



                 if (document.getElementById("<%=txtCSV.ClientID%>").value == "") {
                     alert("Please Enter CSV  ");
                     document.getElementById("<%=txtCSV.ClientID%>").focus();
                     return false;
                 }

             }
             if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "Visa") {
                 if (document.getElementById("<%=txtCard.ClientID%>").value == "") {
                     alert("Please Enter Card Number");
                     document.getElementById("<%=txtCard.ClientID%>").focus();
                     return false;
                 }



                 if (document.getElementById("<%=txtCSV.ClientID%>").value == "") {
                     alert("Please Enter CSV  ");
                     document.getElementById("<%=txtCSV.ClientID%>").focus();
                     return false;
                 }

             }

             if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "Wire In") {
                 if (document.getElementById("<%=drpWire.ClientID%>").value == "0") {
                     alert("Please Enter Wire Service  ");
                     document.getElementById("<%=drpWire.ClientID%>").focus();
                     return false;
                 }

                 //new checks put for the new feild added in the form as recvied
                 if (document.getElementById("<%=txtReceivedamount.ClientID%>").value == "") {
                     alert("Please Enter Wire Service Received Amount ");
                     document.getElementById("<%=txtReceivedamount.ClientID%>").focus();
                     return false;
                 }



             }
             //if (document.getElementById("<%=drpPaymentType.ClientID%>").value=="0")
             //{
             //alert("Please Enter Payment Method");
             //document.getElementById("<%=drpPaymentType.ClientID%>").focus();
             //return false;
             //}
             // if (document.getElementById("ctl00_ContentPlaceHolder_OrderDetailGrid_ctl02_grdDrpQty").value=="0")
             //   {
             //        alert("Please Enter Quantity");
             //        return false;
             //   }

             //if (document.getElementById("ctl00_ContentPlaceHolder_OrderDetailGrid_ctl02_txtItemIDTemp").value=="")
             //   {
             //        alert("Please Enter Item ");
             //        document.getElementById("ctl00_ContentPlaceHolder_OrderDetailGrid_ctl02_txtItemIDTemp").focus();
             //        return false;
             //   }
             //   

             if (document.getElementById("<%=drpPaymentType.ClientID%>").value != "Wire In") {

                 if ("<%=Opt_Source%>" == "1") {
                     if (document.getElementById("<%=drpSource.ClientID%>").value == "0") {
                         alert("Please Select Source Code  ");
                         document.getElementById("<%=drpSource.ClientID%>").focus();

                         return false;
                     }
                 }

             }

             //if (document.getElementById("<%=txtShippingName.ClientID%>").value=="") 
             //{
             //alert("Please Enter Recipient Name");
             // document.getElementById("<%=txtShippingName.ClientID%>").focus();

             // return false;
             //}

             if (document.getElementById("ctl00_ContentPlaceHolder_drpShipMethod") != null) {
                 if (document.getElementById("<%=drpShipMethod.ClientID%>").value == "") {
                     alert("Please Enter Delivery Method");
                     document.getElementById("<%=drpShipMethod.ClientID%>").focus();
                     return false;
                 }
             }

             //// if (document.getElementById("ctl00_ContentPlaceHolder_drpWireoutService")!= null)
             //// {
             ////         if (document.getElementById("<%=drpWireoutService.ClientID%>").value=="0")
             ////        {
             ////        alert("Please Select Wireout Service");
             ////         document.getElementById("<%=drpWireoutService.ClientID%>").focus();
             ////         return false;
             ////        }
             ////}

             if ("<%=Opt_Occasion%>" == "1") {
                 if (document.getElementById("<%=drpOccasionCode.ClientID%>").value == "") {
                     alert("Please Select Occasion Code  ");
                     document.getElementById("<%=drpOccasionCode.ClientID%>").focus();

                     return false;
                 }
             }


             //var txtDelivery;
             //txtDelivery=document.getElementById("<%=txtDelivery.ClientID%>").value;
             //alert(Total);
             //alert(CreditLimit);
             //alert(dif);
             //if (txtDelivery < 0 )
             //{
             //alert("Please provide non negative values for Delivery");
             //document.getElementById("<%=txtDelivery.ClientID%>").focus(); 
             //return false;            
             //}


             if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "House Account") {

                 if (document.getElementById("<%=txtCreditLimit.ClientID%>").value == "" || document.getElementById("<%=txtCreditLimit.ClientID%>").value == "0.00") {
                     var n;
                     n = confirm("Customer does not have a house account or they do not have enough credit for this order. \n  Press 'Ok' to put the order on hold or press 'Cancel' to specify different payment type");
                     if (n) {
                         return true;
                     }
                     else {
                         document.getElementById("<%=drpPaymentType.ClientID%>").focus();
                         return false;
                     }
                 }
                 else {

                     var CreditLimit;
                     CreditLimit = document.getElementById("<%=txtCreditLimit.ClientID%>").value;

                     var Total;
                     Total = document.getElementById("<%=txtTotal.ClientID%>").value;
                     //alert(Total);
                     //alert(CreditLimit);
                     var dif;
                     dif = CreditLimit - Total;
                     //alert(dif);
                     if (dif < 0) {
                         var n;
                         n = confirm("Customer do not have enough credit for this order. \n  Press 'Ok' to put the order on hold or press 'Cancel' to specify different payment type");
                         if (n) {
                             return true;
                         }
                         else {
                             document.getElementById("<%=drpPaymentType.ClientID%>").focus();
                             return false;
                         }
                     }

                 }

             }


             if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "Will Call") {
                 if (document.getElementById("<%=drpShipMethod.ClientID%>").value == "Taken") {
                     alert("Please Select Different Ship Method With Will Call");
                     document.getElementById("<%=drpShipMethod.ClientID%>").focus();
                     return false;
                 }
             }

             alert('Please note :\r\r\"Your Order is being processed\r\r\t Please Wait......');

         }


         function IsNumeric(strString)
         //  check for valid numeric strings	
         {
             var strValidChars = "0123456789";
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




         /*function ExpDate()
         {
         var d = new Date();
         var myDate=new Date();
         var CurrDate=document.aspnetForm.ctl00_ContentPlaceHolder_txtEXP.value;
         var helpArray1=CurrDate.split('/');
         var War=0;
  
         myDate.setMonth(helpArray1[0]-1);
         myDate.setDate(helpArray1[1]);
         myDate.setFullYear(helpArray1[2]); 
    
         if (myDate<d)
         {
         War=1;
        
         }
         if ((helpArray1[0])>12 || (helpArray1[0])<=0)
         {
         War=1;
        
         }
         if (helpArray1[1]>31 || helpArray1[1]<=0)
         {
         War=1;
        
         }
         if(helpArray1[2]<d.getFullYear() )
         {
         War=1;
        
         }
         if (daysInFebruary(helpArray1[2])<helpArray1[1]  && ((helpArray1[0]==02) || (helpArray1[0]==02)))
         {
         War=1;
         }
         if (myDate=="NaN")
         {
         War=1;
     
         }
         if (War==1)
         {
         alert("Invalid Date, Please check the Expiry date (MM/DD/YYYY) !");
         document.aspnetForm.ctl00_ContentPlaceHolder_txtEXP.focus();
         }
         War=0;

         }*/

         function popdate1() {
             var d = new Date();
             var myDate = new Date();
             var CurrDate = document.aspnetForm.ctl00_ContentPlaceHolder_txtDeliveryDate.value;
             var helpArray1 = CurrDate.split('/');
             var WarnMessage = 0;

             var mn;
             var da;
             var yr;

             mn = helpArray1[0] - 1;
             da = helpArray1[1];
             yr = helpArray1[2];
             //    alert("myDate=" + myDate.toDateString())                    
             //    myDate.setMonth(helpArray1[0]-1);
             //    alert("myDate=" + myDate.toDateString())
             //    myDate.setDate (helpArray1[1]);
             //    alert("myDate=" + myDate.toDateString())
             myDate.setFullYear(yr, mn, da);
             //alert("myDate=" + myDate.toDateString())

             if (helpArray1[0] == 2) {
                 if (daysInFebruary(helpArray1[2]) < helpArray1[1]) {
                     WarnMessage = 2;
                 }
             }

             var After30Days = new Date(d.valueOf() + 30000 * 60 * 60 * 24);
             if (myDate >= After30Days) {
                 if (WarnMessage != 2) {
                     WarnMessage = 6;
                     var TempD1 = new Date();
                     var delFlag = confirm("Please Varify the Delivery date! Delivery date beyond 30 days");
                     if (delFlag)
                         if (document.getElementById("ctl00_ContentPlaceHolder_drpShipMethod") != null) {
                             document.getElementById("<%=drpShipMethod.ClientID %>").focus();
                         }
                         else {

                             document.getElementById("<%=txtDeliveryDate.ClientID %>").value = TempD1.getMonth() + 1 + "/" + TempD1.getDate() + "/" + TempD1.getFullYear();

                         }


                 }

             }

             if (myDate < d) {
                 WarnMessage = 1;

             }
             if ((helpArray1[0]) > 12 || (helpArray1[0]) <= 0) {
                 WarnMessage = 1;

             }
             if (helpArray1[1] > 31 || helpArray1[1] <= 0) {
                 WarnMessage = 1;

             }
             if (helpArray1[2] < d.getFullYear()) {
                 WarnMessage = 1;

             }
             if (daysInFebruary(helpArray1[2]) < helpArray1[1] && ((helpArray1[0] == 02) || (helpArray1[0] == 02))) {
                 WarnMessage = 1;

             }
             if (myDate == "NaN") {
                 WarnMessage = 1;

             }

             if (WarnMessage == 1 || WarnMessage == 2) {
                 var TempD = new Date();

                 alert("Invalid Date, Please check the delivery date ! Format Should be (MM/DD/YYYY)");

                 document.getElementById("<%=txtDeliveryDate.ClientID %>").value = TempD.getMonth() + 1 + "/" + TempD.getDate() + "/" + TempD.getFullYear();
                 document.aspnetForm.ctl00_ContentPlaceHolder_txtDeliveryDate.focus();
             }
             WarnMessage = 0;
         }

         function daysInFebruary(year) {
             // February has 29 days in any year evenly divisible by four,
             // EXCEPT for centurial years which are not also divisible by 400.
             return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
         }
         function callme() {

             var completeText = "<%=MacroValues%>";

             var completeCode = "<%=MacroCodes%>";
             var helpArray = completeText.split('~');
             var helpCode = completeCode.split('~');
             var Mes = "";
             var FinalMess = "";

             //if (event.keyCode==13)
             //{
             //    document.getElementById("<%=txtCardMessageDesc.ClientID %>").value+= "\r" + " ";
             //        event.returnValue=false; 
             //       event.cancel = true; 
             //      
             // 
             //}

             if (event.keyCode == 113) {

                 var enteredText = document.getElementById("<%=txtCardMessageDesc.ClientID %>").value;

                 var test = enteredText;
                 for (var i = 0; i < helpCode.length; i++) {
                     if (helpCode[i].toLowerCase() == enteredText.toLowerCase()) {
                         document.getElementById("<%=txtCardMessageDesc.ClientID %>").value = helpArray[i];
                     }
                 }
                 var ArrString = enteredText.split(' ');
                 if (ArrString[ArrString.length - 1] != "") {
                     var macrotest = 0;
                     for (var i = 0; i < helpCode.length; i++) {
                         if (helpCode[i].toLowerCase() == ArrString[ArrString.length - 1].toLowerCase()) {
                             macrotest = 1;
                             Mes = helpArray[i] + " ";
                         }
                     }
                     var FinalTest = 0;
                     FinalTest = ArrString.length;
                     if (macrotest == 1) {
                         FinalTest = ArrString.length - 1;
                     }
                     for (var j = 0; j < FinalTest; j++) {
                         FinalMess += ArrString[j] + " ";
                     }
                     FinalMess += Mes;
                     test += Mes;
                     document.getElementById("<%=txtCardMessageDesc.ClientID %>").value = FinalMess;
                 }
             }
         }
         function CallMeCountry() {
             var CountryCode = document.getElementById("<%=drpCountry.ClientID %>").value;
             var USPhNo = document.getElementById("<%=txtCustomerPhone.ClientID %>").value;

             if (CountryCode == "united states") {
                 if (!USPhNo.match(/^[ ]*[(]{0,1}[ ]*[0-9]{3,3}[ ]*[)]{0,1}[-]{0,1}[ ]*[0-9]{3,3}[ ]*[-]{0,1}[ ]*[0-9]{4,4}[ ]*$/)) {
                     alert("Invalid US Phone Number, Please enter Phone Number");
                     document.getElementById('<%=txtCustomerPhone.ClientID %>').select();
                     document.getElementById('<%=txtCustomerPhone.ClientID %>').focus();
                     event.returnValue = false;
                 }
             }

         }
         function CallMeCountryCell() {
             var CountryCode = document.getElementById("<%=drpCountry.ClientID %>").value;
             var USPhNo = document.getElementById("<%=txtCustomerCell.ClientID %>").value;

             if (CountryCode == "united states") {
                 if (!USPhNo.match(/^[ ]*[(]{0,1}[ ]*[0-9]{3,3}[ ]*[)]{0,1}[-]{0,1}[ ]*[0-9]{3,3}[ ]*[-]{0,1}[ ]*[0-9]{4,4}[ ]*$/)) {
                     alert("Invalid US Cell Number, Please enter Cell Number");
                     document.getElementById('<%=txtCustomerCell.ClientID %>').select();
                     document.getElementById('<%=txtCustomerCell.ClientID %>').focus();
                     event.returnValue = false;
                 }
             }

         }
         function CallMeCountryFax() {
             var CountryCode = document.getElementById("<%=drpCountry.ClientID %>").value;
             var USPhNo = document.getElementById("<%=txtCustomerFax.ClientID %>").value;

             if (CountryCode == "united states") {
                 if (!USPhNo.match(/^[ ]*[(]{0,1}[ ]*[0-9]{3,3}[ ]*[)]{0,1}[-]{0,1}[ ]*[0-9]{3,3}[ ]*[-]{0,1}[ ]*[0-9]{4,4}[ ]*$/)) {
                     alert("Invalid US Fax Number, Please enter Fax Number");
                     document.getElementById('<%=txtCustomerFax.ClientID %>').select();
                     document.getElementById('<%=txtCustomerFax.ClientID %>').focus();
                     event.returnValue = false;
                     //  event.cancel = true;
                 }
             }

         }
         function CallMeZipCountry() {
             var ZipCountryCode = document.getElementById("<%=drpCountry.ClientID %>").value;
             var USZip = document.getElementById("<%=txtCustomerZip.ClientID %>").value;
             if (ZipCountryCode == "united states") {
                 if (!USZip.match(/^[ ]*[0-9]{5,5}[ ]*$/)) {
                     alert("Invalid US Zip Code, Please enter Zip code");
                 }

             }
         }

         //function CallMeShipCountry()
         //{
         //    var ShipCountryCode=document.getElementById("<%=drpShipCountry.ClientID %>").value;
         //    var ShipPhoneNo=document.getElementById("<%=txtShipCustomerPhone.ClientID %>").value;
         //    if (ShipCountryCode=="united states")
         //    {
         //        if (!ShipPhoneNo.match(/^[ ]*[(]{0,1}[ ]*[0-9]{3,3}[ ]*[)]{0,1}[-]{0,1}[ ]*[0-9]{3,3}[ ]*[-]{0,1}[ ]*[0-9]{4,4}[ ]*$/))
         //        {
         //             alert("Invalid US Phone Number, Please enter Phone Number");
         //        }
         //    
         //    }
         //    
         //}

         function CallMeShipZipCountry() {
             var ShipZipCountryCode = document.getElementById("<%=drpShipCountry.ClientID %>").value;
             var ShipZip = document.getElementById("<%=txtShippingZip.ClientID %>").value;
             if (ShipZipCountryCode == "united states") {
                 if (!ShipZip.match(/^[ ]*[0-9]{5,5}[ ]*$/)) {
                     alert("Invalid US Zip Code, Please enter Zip Code");
                 }

             }

         }
         function CallMeShipCountryCell() {
             var CountryCode = document.getElementById("<%=drpShipCountry.ClientID %>").value;
             var USPhNo = document.getElementById("<%=txtShipCustomerCell.ClientID %>").value;

             if (CountryCode == "united states") {
                 if (!USPhNo.match(/^[ ]*[(]{0,1}[ ]*[0-9]{3,3}[ ]*[)]{0,1}[-]{0,1}[ ]*[0-9]{3,3}[ ]*[-]{0,1}[ ]*[0-9]{4,4}[ ]*$/)) {
                     alert("Invalid US Cell Number, Please enter Cell Number");
                     document.getElementById('<%=txtShipCustomerCell.ClientID %>').select();
                     document.getElementById('<%=txtShipCustomerCell.ClientID %>').focus();
                     event.returnValue = false;
                 }
             }

         }
         function CallMeShipCountryFax() {
             var CountryCode = document.getElementById("<%=drpShipCountry.ClientID %>").value;
             var USPhNo = document.getElementById("<%=txtShipCustomerFax.ClientID %>").value;

             if (CountryCode == "united states") {
                 if (!USPhNo.match(/^[ ]*[(]{0,1}[ ]*[0-9]{3,3}[ ]*[)]{0,1}[-]{0,1}[ ]*[0-9]{3,3}[ ]*[-]{0,1}[ ]*[0-9]{4,4}[ ]*$/)) {
                     alert("Invalid US Fax Number, Please enter Fax Number");
                     document.getElementById('<%=txtShipCustomerFax.ClientID %>').select();
                     document.getElementById('<%=txtShipCustomerFax.ClientID %>').focus();
                     event.returnValue = false;
                 }
             }

         }
         function CallMeShipCountryPhone() {
             var CountryCode = document.getElementById("<%=drpShipCountry.ClientID %>").value;
             var USPhNo = document.getElementById("<%=txtShipCustomerPhone.ClientID %>").value;

             if (CountryCode == "united states") {
                 if (!USPhNo.match(/^[ ]*[(]{0,1}[ ]*[0-9]{3,3}[ ]*[)]{0,1}[-]{0,1}[ ]*[0-9]{3,3}[ ]*[-]{0,1}[ ]*[0-9]{4,4}[ ]*$/)) {
                     alert("Invalid US Phone Number, Please enter Phone Number");
                     document.getElementById('<%=txtShipCustomerPhone.ClientID %>').select();
                     document.getElementById('<%=txtShipCustomerPhone.ClientID %>').focus();
                     event.returnValue = false;
                 }
             }

         }
         function ChangeState() {
             var CountryCode = document.getElementById("<%=drpCountry.ClientID %>").value;


             if (CountryCode == "united states") {
                 document.getElementById("<%=drpShippingState.ClientID %>").style.visibility = "hidden";
                 
             }
         }

         function CallFkey() {

                
             var completeText = "<%=Action%>";
             var ek = event.keyCode;
             var mk;
             var GrdRowCount = "<%=GridCount %>"

             var completeCode = "<%=Fkey %>";
             var helpArray = completeText.split('~');
             var helpCode = completeCode.split('~');

             if (event.keyCode == 122) {
                 event.keyCode = 0;
                 event.returnValue = false;
             }

             if (event.keyCode == 114) {
                 event.keyCode = 0;
                 event.returnValue = false;
             }
             if (event.keyCode == 115) {
                 event.keyCode = 0;
                 event.returnValue = false;
             }
             if (event.keyCode == 116) {
                 event.keyCode = 0;
                 event.returnValue = false;
             }
             if (event.keyCode == 117) {
                 event.keyCode = 0;
                 event.returnValue = false;
             }
             if (event.keyCode == 121) {
                 event.keyCode = 0;
                 event.returnValue = false;
             }



             for (var i = 0; i < helpCode.length; i++) {


                 if (helpCode[i].toLowerCase() == ek) {



                     var result = helpArray[i];
                     if (result == "Card Message") {

                         document.getElementById("<%=txtCardMessageDesc.ClientID %>").focus();
                         

                     }

                     if (result == "DeliveryDate") {

                         document.getElementById("<%=txtDeliveryDate.ClientID %>").focus();
                         

                     }
                     if (result == "ShippingSalutation") {

                         document.getElementById("<%=drpShipCustomerSalutation.ClientID %>").focus();
                         

                     }


                     if (result == "pointofinterest") {

                         Javascript: window.open('DeliveryLocations.aspx', 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400'); return false;

                     }

                     if (result == "WireInInformationBox") {




                         if (document.getElementById("<%=drpPaymentType.ClientID%>").value == "Wire") {

                             document.getElementById("<%=drpWire.ClientID%>").focus();


                         }


                     }

                     if (result == "WireOutInformation") {

                         if (document.getElementById("<%=drpShipMethod.ClientID%>").value == "Wire_Out") {

                             document.getElementById("<%=drpWireoutService.ClientID%>").focus();


                         }
                     }
                     if (document.getElementById("<%=drpShipMethod.ClientID %>") != null) {
                         if (result == "DeliveryMethod") {


                             document.getElementById("<%=drpShipMethod.ClientID %>").focus();

                         }
                     }
                     if (result == "PaymentType") {

                         document.getElementById("<%=drpPaymentType.ClientID %>").focus();
                         
                     }

                     if (result == "Address") {

                         document.getElementById("<%=txtShippingAddress1.ClientID %>").focus();
                         
                     }


                     if (result == "Suggestions") {

                         Javascript: window.open('MessageSuggestions.aspx', 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400')


                     }

                     if (result == "CustomerSalutation") {


                         document.getElementById("<%=DropDownList1.ClientID %>").focus();

                     }
                     if (result == "ItemID") {

                         if (document.getElementById("ctl00_ContentPlaceHolder_OrderDetailGrid_ctl01_txtEmptyItem") == null) {

                         }
                         else {

                             document.getElementById("ctl00_ContentPlaceHolder_OrderDetailGrid_ctl01_txtEmptyItem").focus();


                         }

                     }
                     
                     if (result == "ItemSearch") {
                         var CustomerIDTemp
                         if (document.getElementById("<%=txtCustomerTemp.ClientID %>") != null) {
                             var CustomerIDTemp;
                             CustomerIDTemp = document.getElementById("<%=txtCustomerTemp.ClientID %>").value;
                             window.open("ItemDetailsSearch.aspx?CustomerID=" + CustomerIDTemp + "", 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=600,height=400,Left=350,top=275,screenX=0,screenY=275,alwaysRaised=yes');
                         }
                         else {

                             var CustomerIDTemp = "";
                             window.open("ItemDetailsSearch.aspx?CustomerID=" + CustomerIDTemp + "", 'MyWindow', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,width=650,height=400,Left=350,top=275,screenX=0,screenY=275,alwaysRaised=yes');

                         }


                     }

                 }


             }



         }

</script>

   

<script  type="text/javascript" language="javascript">
    function GoogleDirect() {
     
        var ToAddress1 = document.getElementById('<%=txtShippingAddress1.ClientID %>').value;
        var ToAddress2 = document.getElementById('<%=txtShippingAddress2.ClientID %>').value;
        var ToZip = document.getElementById('<%=txtShippingZip.ClientID %>').value;
        var ToCity = document.getElementById('<%=txtShippingCity.ClientID %>').value;
        var ToState = document.getElementById('<%=drpShippingState.ClientID %>').value;

        var FromAddress = '<% =RetailerAddress %>'
        var FromState = '<% =RetailerState %>'
        var FromCity = '<% =RetailerCity %>'
        var FromZip = '<% =RetailerZip %>'

        //window.open ("http://maps.google.com/maps?daddr="+ToAddress+"&saddr="+FromAddress);
        //window.open ("http://maps.google.com/maps?daddr="+ToAddress1+","+ToAddress2+","+ToZip+"&saddr="+FromAddress1+","+FromAddress2+","+FromZip);

        window.open("http://maps.google.com/maps?daddr=" + ToAddress1 + "," + ToAddress2 + "," + ToCity + "," + ToState + "," + ToZip + "&saddr=" + FromAddress + "," + FromCity + "," + FromState + "," + FromZip);

    }


    function updateCountdriverinfo(textbox) {

        //alert(textbox.value.length);
        var count = 136 - textbox.value.length;
        //alert(count)
        if (count > 15) {
            document.getElementById("txtDriverMessageLen").style.backgroundColor = "white";
        }
        var MaxLimit = 136;

        //note innerHTML - NOT value

        if (textbox.value.length > 136) {
            document.getElementById("txtDriverMessageLen").value = 0;
            alert("Only 136 Characters Are Allowed !");
            textbox.value = textbox.value.substring(0, 136);

            //alert(" Maximum " + count + " chars");
        }
        else {
            document.getElementById("txtDriverMessageLen").value = count.toString();
            if (count <= 15) {
                document.getElementById("txtDriverMessageLen").style.backgroundColor = "red";
            }

        }
    }



    function updateCount(textbox) {

        //alert(textbox.value.length);
        var count = 330 - textbox.value.length;
        //alert(count)
        if (count > 15) {
            document.getElementById("txtMessageLen").style.backgroundColor = "white";
        }
        var MaxLimit = 330;

        //note innerHTML - NOT value

        if (textbox.value.length > 330) {
            document.getElementById("txtMessageLen").value = 0;
            alert("Only 330 Characters Are Allowed !");
            textbox.value = textbox.value.substring(0, 330);

            //alert(" Maximum " + count + " chars");
        }
        else {
            document.getElementById("txtMessageLen").value = count.toString();
            if (count <= 15) {
                document.getElementById("txtMessageLen").style.backgroundColor = "red";
            }

        }
    }

    function SetFocusAll() {

        document.getElementById("<%=drpOrderTypeIDData.ClientID %>").focus();
    }


    function SetFocusTo() {

        document.getElementById("<%=DropDownList1.ClientID %>").focus();
    }

    function ChangeFocustoSalutation() {

        document.getElementById("<%=DropDownList1.ClientID %>").focus();

    }
    function ChangeFocustoCustomerID() {

        document.getElementById("<%=drpCustomerID.ClientID %>").focus();
    }
     </script>  
                    

     <script  type="text/javascript" language="JavaScript" src="Scripts/datepicker.js"></script>

     <span id="advFeature" runat="server" visible="false">
<link rel="stylesheet" type="text/css" href="https://ws1.postescanada-canadapost.ca/css/addresscomplete-1.00.min.css?key=bt75-ar31-gb17-am45" /><script type="text/javascript" src="https://ws1.postescanada-canadapost.ca/js/addresscomplete-1.00.min.js?key=bt75-ar31-gb17-am45&app=10602"></script><div id="bt75ar31gb17am4510602"></div>
<link rel="stylesheet" type="text/css" href="https://ws1.postescanada-canadapost.ca/css/addresscomplete-1.00.min.css?key=xc73-pj65-fj34-xt77" /><script type="text/javascript" src="https://ws1.postescanada-canadapost.ca/js/addresscomplete-1.00.min.js?key=xc73-pj65-fj34-xt77&app=16869"></script><div id="xc73pj65fj34xt7716869"></div>
     </span>
     


      <input id="txtVendorID"  type="hidden" runat="server" />
     
     		 <!-- BEGIN PORTLET 1st Block-->
					<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">&nbsp;Order Details</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
                        <div class="portlet-body" >
							<div class="row">
								<div class="col-md-4">
									<div class="text-center" style="padding-top:100px;">
                                     <asp:Image ID="ImgRetailerLogo" CssClass="img-rounded" ImageUrl="" runat="server" />
                                     </div>
								</div>
								<div class="col-md-4">
									<!-- BEGIN FORM-->
                                    
                                        <div class="form-body">
                                         <table    id="table3" class="table table-striped table-hover table-bordered">
                                                      
                                                        
                                                        <tr id="trtop" runat="server" visible="false" >
                                                            
                                                            <td width="110" height="12">
                                                                Auto Saved At</td>
                                                            <td width="101" style="background-color:#F6F6F6;" height="12">
                                                                 <Ajax:AjaxPanel ID="AjaxPanel1" runat="server">
                                                                    <asp:Label Width="95" BorderWidth="1px" BorderColor="#939393" ID='lblrefersh'    CssClass="txtStyle" Height="18" runat="server" Text=''></asp:Label>
                                                                </Ajax:AjaxPanel> 
                                                                  
                                                            </td>
                                                             
                                                        </tr>    
                                                                                                                                                               
                                                        <tr>
                                                            
                                                            <td width="110" height="12">
                                                                Order Number</td>
                                                            <td width="101" height="12">
                                                                  <Ajax:AjaxPanel ID="AjaxPanel2" runat="server">
                                                                    <asp:Label Width="95"  ID='lblOrderNumberData'   Height="18" runat="server" Text=''></asp:Label>
                                                                  </Ajax:AjaxPanel> 
                                                            </td>
                                                            
                                                        </tr>
                                                     
                                                        <tr>
                                                            
                                                            <td width="110" height="12" valign="middle">
                                                                Order Date</td>
                                                            <td width="101" height="12">
                                                            
                                                                <asp:Label ID="lblOrderDate"   Height="18" Width="95" runat="server" Text=" "></asp:Label>
                                                                  
                                                            </td>
                                                            
                                                        </tr>
                                                     
                                                        <tr>
                                                             
                                                            <td width="110" height="15" valign="middle">
                                                                Order Type</td>
                                                            <td width="101" height="15">
                                                           
                                                                <asp:DropDownList CssClass="form-control input-xs" ID="drpOrderTypeIDData"   runat="server"   TabIndex="1">
                                                                </asp:DropDownList>
                                                           
                                                            </td>
                                                             
                                                        </tr>
                                                         
                                                        <tr>
                                                            
                                                            <td width="110" height="15" valign="middle">
                                                                Transaction Type</td>
                                                            <td width="101" height="15">
                                                              <Ajax:AjaxPanel ID="AjaxPanel270" runat="server">
                                                                <asp:DropDownList CssClass="form-control input-xs" ID="drpTransaction" AutoPostBack="true"    runat="server" TabIndex="2">
                                                                </asp:DropDownList>
                                                                </Ajax:AjaxPanel> 
                                                                
                                                              
                                                            </td>
                                                            
                                                        </tr>
                                                  
                                                       
                                                
                                                       
                                                        <tr>
                                                            
                                                            <td width="110" height="15" valign="middle">
                                                                WareHouse ID</td>
                                                            <td width="101" height="15">
                                                             
                                                                <asp:DropDownList ID="drpLocation" CssClass="form-control input-xs"  runat="server"   TabIndex="4">
                                                                </asp:DropDownList>
                                                            
                                                            </td>
                                                            
                                                        </tr>
                                                        <!--location id-->
                                                        <tr>
                                                             
                                                            <td width="110" height="15" valign="middle">
                                                                Order Location</td>
                                                            <td width="101" height="15">
                                                            <Ajax:AjaxPanel ID="AjaxPanel2710" runat="server">
                                                                <asp:DropDownList ID="cmblocationid" CssClass="form-control input-xs" runat="server"    TabIndex="4">
                                                                </asp:DropDownList>
                                                              </Ajax:AjaxPanel>    
                                                            </td>
                                                            
                                                        </tr>
                                                        <!--location id-->
                                                       
                                                        <tr id="trproject" runat="server" visible="false" >
                                                            
                                                            <td width="110" height="15" valign="middle">
                                                                Project/Event</td>
                                                            <td width="101" height="15">
                                                            
                                                                <asp:DropDownList ID="drpProject" CssClass="form-control input-xs" runat="server"   TabIndex="5">
                                                                </asp:DropDownList>
                                                           
                                                            </td>
                                                             
                                                        </tr>
                                                        
                                                        
                                                         <tr>
                                                             
                                                            <td width="110" height="15" valign="middle">
                                                                Employee ID</td>
                                                            <td width="101" height="15">
                                                            
                                                                <asp:DropDownList CssClass="form-control input-xs" ID="drpEmployeeID"     runat="server"   TabIndex="3">
                                                                </asp:DropDownList>
                                                               
                                                            </td>
                                                            
                                                        </tr>
                                                        
                                                          <tr>
                                                             
                                                            <td width="110" height="15" valign="middle">
                                                                Assigned to</td>
                                                            <td width="101" height="15">
                                                            
                                                                <asp:DropDownList CssClass="form-control input-xs" ID="drpAssignedto"     runat="server"   TabIndex="3">
                                                                </asp:DropDownList>
                                                               
                                                            </td>
                                                            
                                                        </tr>
                                                        
                                                        
                                                    </table>


                                        </div>
                                  
                                    <!-- END FORM-->
								</div>
								<div class="col-md-4">
									<div class="alert alert-info">System Wide Messages</div>
                                    <asp:Label ID="lblSystemWM" Height="160" runat="server" Font-Size="9pt"></asp:Label>
								</div>
							</div>
						</div>

                        </div>
				<!-- END PORTLET-->

				 
                    <!-- BEGIN PORTLET 2nd Block-->
					<div class="portlet box green">
                    	<div class="portlet-title">
                        <div class="caption" style="width:95%;"> 
							 <div class="row">
                                <div class="col-md-4">
									<div class="text-left" >
                                         <Ajax:AjaxPanel ID="AjaxPanel3" runat="server">
                                            <asp:Label ID='lblCustomer'   runat='server'  Text="Bill to Customer ID"></asp:Label>
                                        </Ajax:AjaxPanel> 
                                    </div> 
                                  </div> 
                                  <div class="col-md-3">
									<div class="text-left" >
                                       <Ajax:AjaxPanel ID="AjaxPanel4" runat="server">
                                        <asp:DropDownList ID="drpCustomerID" Visible="True" CssClass="form-control input-sm input-200"  runat="server"  AutoPostBack="True">
                                        <asp:ListItem Selected="True">Retail Customer</asp:ListItem>
                                            <asp:ListItem >New Customer</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSpecifyCustomer" runat="server" AutoPostBack="True" Visible="False"></asp:TextBox>
			                            <asp:TextBox ID="txtCustomerTemp" Visible="False" runat="server"  ReadOnly="True" CssClass="form-control input-sm input-200"  ></asp:TextBox>
			                            <span style="display:none;" >
                                            <ctls:PopupControl EnableViewState="true"  id="lkpCustomerID" runat="server"     TargetURL="CustomerLookup.aspx"    ButtonText="Search"   AutoPostBack="True"   OnDataBinding="PopupControl_Change"   ButtonCssClass="PopupSearchButton" Visible="false"/>   
                                        </span>
                                    </Ajax:AjaxPanel> 
                                    </div> 
                                  </div>

                                  <div class="col-md-5">
									<div class="text-right" >
                                         <Ajax:AjaxPanel ID="AjaxPanel5" runat="server">
                                             &nbsp;  <asp:LinkButton ID="lnkCustomerSearch"   ForeColor="White"    runat="server">Advanced Customer Search</asp:LinkButton>
                                                                
                                             <asp:LinkButton ID="lnkBackToOption"   Visible="false"     runat="server">Back to Option</asp:LinkButton>
                                        </Ajax:AjaxPanel> 

                                    </div> 
                                  </div>

                            </div> 
                        </div>
							<div class="tools">
                                <a href="javascript:;" class="<%=Bill_to_Customer_ID %>"></a>
                            </div>
						</div>


						<div class="portlet-body form"  style="display: none;">
                           

                              <div class="form-group-search-block">
                                <div class="input-group">
                                    <span class="input-group-addon input-circle-left">
                                    <i class="fa fa-search"></i>
                                    </span>
                                    <Ajax:AjaxPanel ID="AjaxPanel6" runat="server">
                                      <asp:TextBox ID="txtcustomersearch" runat="server" CssClass="form-control input-circle-right"    ></asp:TextBox>
                                    </Ajax:AjaxPanel> 
                                    <br />
                                    <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete"  ></div> 
                                    
                                </div>
                            </div>


                            <div class="row">
								<div class="col-md-12" style="padding-top:10px;">
                                     <Ajax:AjaxPanel ID="AjaxPanel7" runat="server">
                                         &nbsp;<asp:ImageButton ID="btncustsearch"  ToolTip="Update Customer" ImageUrl="~/images/2-sh-stock-in.gif" Width="0" runat="server" /> 
                                         <asp:Label ID="lblsearchcustomermsg" runat="server" Text=""></asp:Label>
                                    </Ajax:AjaxPanel> 
								</div>
								 
							</div>


                            <div class="row">
								<div class="col-md-8">
                                  <Ajax:AjaxPanel ID="AjaxPanel8" runat="server">
                                 
                                 
                                 	<div class="table-responsive">
                      		<table class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                      <td>
                                      
                                        <asp:Label ID="lblSalutation" runat="server" Text="Salutation"></asp:Label>

                                       <asp:DropDownList ID="DropDownList1" CssClass="form-control input-sm"  runat="server"  >
                                         </asp:DropDownList>
                                        
                                        
                                        </td>
                                      <td colspan="3">
                                      <div class="row">
                                      <asp:Panel ID="pnlCustomer" runat="server">
                                          <div class="col-sm-6">
                                                    <label>First</label>
                                        
                                                    <asp:TextBox ID="txtCustomerFirstName" runat="server" Text='' CssClass="form-control input-sm"  ></asp:TextBox>
                                              </div>
                                              <div class="col-sm-6">
                                                    <label>Last</label>
                                                    <asp:TextBox ID="txtCustomerLastName" runat="server" Text='' CssClass="form-control input-sm"  ></asp:TextBox>
                                              </div>
                                        </asp:Panel>
                                      	<asp:Panel ID="pnlVendor" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:TextBox ID="txtVendorName" Width="410" CssClass="form-control input-sm"  runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                        </asp:Panel>


                                      </div>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>Attention</td>
                                      <td>
                                         <asp:TextBox CssClass="form-control input-sm "  ID="txtAttention" runat="server" Text=''  ></asp:TextBox>
                                      </td>
                                      <td colspan="2" rowspan="5">
                                      	<label class="margin-bottom-10 margin-top-10"><asp:Label ID="lblCustomerComments" runat="server" Text="Customer Comments"></asp:Label></label>

                                        <asp:TextBox ID="txtComments"    CssClass="form-control input-xs" Rows="11" Columns="20"  TextMode="MultiLine" runat="server"  ></asp:TextBox>
                                         
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                      <td>Company</td>
                                      <td>
                                        <asp:TextBox  CssClass="form-control input-sm"  ID="txtCompany" runat="server"  ></asp:TextBox>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>Add 1</td>
                                      <td> <asp:TextBox CssClass="form-control input-sm"  ID="txtCustomerAddress1" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Add 2</td>
                                      <td> <asp:TextBox CssClass="form-control input-sm"  ID="txtCustomerAddress2" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Add 3</td>
                                      <td> <asp:TextBox CssClass="form-control input-sm"  ID="txtCustomerAddress3" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    
                                    <tr>
                                      <td>City</td>
                                      <td>  <asp:TextBox ID="txtCustomerCity" CssClass="form-control input-sm"  runat="server" Text=''  ></asp:TextBox> </td>
                                      <td colspan="2">
                                      	  
                                      </td>
                                    </tr>
                                    <tr>
                                      <td colspan="4">
                                      	<div class="row">
                                      		<div class="col-md-4">
                                            <label>Country</label>
                                            <Ajax:AjaxPanel ID="AjaxPanel28" runat="server">
                                                 <asp:DropDownList ID="drpCountry" CssClass="form-control input-sm"   runat="server"   AutoPostBack="True">
                                                         </asp:DropDownList>
                                            </Ajax:AjaxPanel> 
                                                        
                                            </div>
                                            <div class="col-md-4">
                                            <label>State/Province</label>

                                                <asp:DropDownList ID="drpState"  runat="server" CssClass="form-control input-sm" >
                                                </asp:DropDownList>

                                                <asp:TextBox ID="txtCustomerState" Visible="false" runat="server" CssClass="form-control input-sm"  Text=""></asp:TextBox>

                                            </div>
                                            <div class="col-md-4">
                                            <label>Zip/Postal</label>
                                                 <asp:TextBox ID="txtCustomerZip" runat="server" CssClass="form-control input-sm"  Text=''  ></asp:TextBox>
                                            </div>
                                      	</div>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td style="width:20%">Phone</td>
                                      <td style="width:30%"> <asp:TextBox CssClass="form-control input-sm" ID="txtCustomerPhone" runat="server" Text=''  ></asp:TextBox> </td>
                                      <td style="width:20%">Ext</td>
                                      <td style="width:30%"> <asp:TextBox  CssClass="form-control input-sm input-50" ID="txtExt" runat="server"  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Cell</td>
                                      <td> <asp:TextBox CssClass="form-control input-sm" ID="txtCustomerCell" runat="server" ></asp:TextBox> </td>
                                      <td>Fax</td>
                                      <td> <asp:TextBox CssClass="form-control input-sm" ID="txtCustomerFax" runat="server" Text=''  ></asp:TextBox> </td>
                                    </tr>
                                    <tr>
                                      <td>Email</td>
                                      <td>  <asp:TextBox CssClass="form-control input-sm"  ID="txtCustomerEmail" runat="server" Text=''  ></asp:TextBox> </td>
                                      <td><asp:Label ID="lblNewsletter" runat="server" Text="Newsletter"></asp:Label></td>
                                      <td>
                                      
                                        <asp:DropDownList ID="drpNewsletterID" runat="server" CssClass="form-control input-sm"  >
                                            <asp:ListItem Text="Select One" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Yes-HTML" Value="Yes-HTML"></asp:ListItem>
                                            <asp:ListItem Text="Yes-Plain Text" Value="Yes-Plain Text"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                            
                                            </td>
                                    </tr>
                                    <tr>
                                      <td><asp:Label ID="lblPO" runat="server" Text="PO#"></asp:Label>
                                            <asp:TextBox CssClass="form-control input"  ID="txtPO" runat="server"  ></asp:TextBox>
                                      </td>
                                      <td colspan="3">
                                          <div class="row">
                                            <div class="col-sm-6">

                                              <asp:Label ID="lblSource" runat="server" Text="Source"></asp:Label>
                                                <asp:DropDownList ID="drpSource" runat="server" CssClass="form-control input-sm"   >
                                                </asp:DropDownList>

                                              </div>
                                              <div class="col-sm-6">
                                               
                                                 &nbsp;
                                              </div>
                                          </div>
                                       </td>
                                    </tr>
                                      <tr id="traffliate" runat="server" visible="false">
                                        <td  >
                                        </td>
                                        <td  >
                                            <asp:Label ID="lblAffliatesname" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td  >
                                            <table border="0" width="420" id="table53" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="100px">
                                                        <asp:Label ID="txtAffliate" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="100px">
                                                        <asp:Label ID="lblAffliatescode" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td width="100px">
                                                        <asp:Label ID="txtAffliatescode" Style="text-align: left" runat="server"></asp:Label>
                                                    </td>
                                                    <td width="120">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td  >
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="4" >
                                        <asp:Label ID="lblCustomerIDNull" runat="server" ForeColor="Red" Visible="False"></asp:Label> 
                                       
                                        </td>
                                    </tr>

                                </tbody>
                        	</table>
                        </div>

                                 
                                 </Ajax:AjaxPanel> 
                                </div> 
                                <div class="col-md-4">
                                        <Ajax:AjaxPanel ID="AjaxPanel9" runat="server">
                                                                    <asp:Panel ID="pnlCustomerProfile1" Visible="true" runat="server">
                                                                        
                                                                        <table class="table table-striped table-hover table-bordered">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td style="width:50%"><a href="#">Account Status</a></td>
                                                                                  <td style="width:50%;">
                                                                                   
                                                                                  <asp:Label ID="lblAccountStatus"   CssClass="form-control input-sm" runat="server" Text=''   ></asp:Label>
                                                                                  </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Credit Limit</td>
                                                                                    <td>
                                                                                         
                                                                                        <asp:Label ID="lblCreditLimit"   CssClass="form-control input-sm" runat="server"   Text=''   ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>YTD Orders</td>
                                                                                    <td>  
                                                                                            <asp:Label ID="lblYTDOrders"   CssClass="form-control input-sm" runat="server"   ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Average Sale</td>
                                                                                    <td>  
                                                                                            <asp:Label ID="lblAverageSale"   CssClass="form-control input-sm" runat="server"   ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Sales LifeTime</td>
                                                                                    <td> 
                                                                                         <asp:Label ID="lblSalesLifeTime"  CssClass="form-control input-sm" runat="server"  ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Customer Since</td>
                                                                                    <td>  
                                                                                        <asp:Label ID="lblCustomerSince"   CssClass="form-control input-sm" runat="server" Text=''  ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Member Points</td>
                                                                                    <td> 
                                                                                        <asp:Label ID="lblMemberPoints"  CssClass="form-control input-sm" runat="server" ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Credit Comments</td>
                                                                                    <td>  
                                                                                        <asp:Label ID="lblCreditComments"  CssClass="form-control input-sm" runat="server" Text=''  ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Discounts</td>
                                                                                    <td>  
                                                                                        <asp:Label ID="lblDiscounts"   CssClass="form-control input-sm" runat="server"     Enabled="False"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Customer Rank</td>
                                                                                    <td>  
                                                                                          <asp:Label ID="lblCustomerRank"  CssClass="form-control input-sm" runat="server"   ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Customer Type</td>
                                                                                    <td>  
                                                                                          <asp:TextBox ID="txtCustomerTypeID" CssClass="form-control input-sm" Enabled="false" runat="server"  ></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Club Card # </td>
                                                                                    <td>  
                                                                                        <asp:Label ID="lbltxtclubcard"  CssClass="form-control input-sm" runat="server" Text=''  ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Card Status </td>
                                                                                    <td>  
                                                                                        <asp:Label ID="lbltxtclubcardstatus"   CssClass="form-control input-sm" runat="server"     Enabled="False"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Card Exp Date </td>
                                                                                    <td>  
                                                                                          <asp:Label ID="lbltxtclubcardexpdate"  CssClass="form-control input-sm" runat="server"   ></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                          </table>


                                                                    </asp:Panel>



                                       </Ajax:AjaxPanel> 
									                                

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
                                         Order Detail 
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
                                                 <Ajax:AjaxPanel ID="AjaxPanel11" runat="server">
                                                 <asp:LinkButton ID="ItemSearch" Font-Underline="true" runat="server">
                                                        <font color="white">Advanced Item Search</font>
                                                 </asp:LinkButton>
                                                 </Ajax:AjaxPanel> 
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
  
                                    <div class="row">
								        <div class="col-md-12"  c >
                          
                           <table class="table table-striped table-hover table-bordered dataTable tableRecord"   id="sample_editable_1">
							 <thead>
                                <tr>
                                    <th class="datatable-nosort" >
                                        Qty
                                
                                    </th>
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
                                        Price 
                                    </th>
                                    <th>
                                        Discount 
                                    </th>
                                    <th>
                                    Disc. Type 
                                    </th>
                                    <th>
                                        Total 
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
                                            <button id="sample_editable_1_new" class="btn green">
                                                Add New <i class="fa fa-plus"></i>
                                            </button>
                                            <Ajax:AjaxPanel ID="AjaxPanel104" runat="server">
                                             ID :<asp:TextBox ID="txtitemid" Width="200px"   Font-Size="12"   Height="28" runat="server"></asp:TextBox>
                                                Name : <asp:TextBox ID="txtitemsearchDesc" Width="200px"   Font-Size="12"   Height="28" runat="server"></asp:TextBox> 
                                            Price : <asp:TextBox ID="txtitemsearchprice" Width="50px"   Font-Size="12"   Height="28" runat="server"></asp:TextBox> 

                                            <asp:TextBox ID="txtfirst"   Text="1" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="reqsaveid"   Text="" runat="server"></asp:TextBox>
                                                                       
                                            <asp:TextBox ID="txtOrderNumber" Width="200px"   Font-Size="12"  Text=""  Height="28" runat="server"></asp:TextBox>
                                           </Ajax:AjaxPanel>
                                        </div>
                                        
                
                        
                                        <Ajax:AjaxPanel ID="AjaxPanel14" runat="server">
                                           <asp:Label ID="lblErrorText" TabIndex="63" runat="server" Visible="false"></asp:Label>
                                        </Ajax:AjaxPanel> 

                                        </div>


                         


                                  <div class="row">
								        <div class="col-md-12"  style="display:none;"  >

                                             <Ajax:AjaxPanel ID="AjaxPanel15" runat="server">
                                             <asp:GridView ID="OrderDetailGrid" Width="775px" Visible="false"  runat="server" DataKeyNames="OrderLineNumber"
                                            ShowFooter="true" AutoGenerateColumns="False" TabIndex="56">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                            <HeaderStyle CssClass="grid-header" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="50" HeaderText="Edit" ItemStyle-HorizontalAlign="Center"
                                                    FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgEdit" TabIndex="57" ToolTip="Edit Item" ImageUrl="~/Images/edit.gif"
                                                            runat="server" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="ImgUpdate" TabIndex="57" ToolTip="Update Item" ImageUrl="~/images/Add-Button.gif"
                                                            runat="server" CommandName="Update" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton CausesValidation="true" ID="imgPostItemDetails" TabIndex="59" CommandName="footerPostDetails"
                                                            ToolTip="Add Item" ImageUrl="~/images/Add-Button.gif" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle Width="50px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="50" HeaderText="Delete" ItemStyle-HorizontalAlign="Center"
                                                    FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgEditDelet" TabIndex="58" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                            CommandName="Delete" ToolTip="Delete Item" ImageUrl="~/images/Delete.gif" runat="server" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="ImgDelete" TabIndex="58" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                            CommandName="Delete" ToolTip="Delete Item" ImageUrl="~/images/Delete.gif" runat="server" />
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="50px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <FooterTemplate>
                                                        <asp:ImageButton CausesValidation="true" ImageAlign="Middle" ID="imgBindItemDetails"
                                                            TabIndex="60" CommandName="footerBindDetails" ToolTip="Preview Item" ImageUrl="~/images/Preview-Button.gif"
                                                            runat="server" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" Text='<%# Eval("OrderQty") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="grdQty" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="grdDrpQty" TabIndex="61" runat="server">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item ID" ItemStyle-Width="60">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemID" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtUpdateItemID" Width="60" runat="server" Text='<%# Bind("ItemID") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtItemIDTemp" Width="60" runat="server" Text=''></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemName" Text='<%# Eval("ItemName") %>' Width="100" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblUpdateItemName" Text='<%# Bind("ItemName") %>' Width="100" runat="server"></asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfooterItemName" Text='' Width="100" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="200">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemDescription" Text='<%# Eval("Description") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemPrice" ForeColor="red" runat="server" Text=" "></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="tbxUpdateItemDescription" Width="200" MaxLength="240" TextMode="MultiLine"
                                                            runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtItemDescription" Width="200" MaxLength="240" TextMode="MultiLine"
                                                            TabIndex="62" runat="Server" Text=' ' />
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemUOM" Text='<%# Eval("ItemUOM") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblUpdateItemUOM" Text='<%# Bind("ItemUOM") %>' runat="server"></asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfooterItemUOM" Text=' ' runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price" ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" Text=' <%#String.Format("{0:N2}", Eval("ItemUnitPrice"))%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtUpdateItemPrice" Width="40" runat="server" Text=' <%#String.Format("{0:N2}", Eval("ItemUnitPrice"))%>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="validator"
                                                            Display="Dynamic" ControlToValidate="txtUpdateItemPrice" ErrorMessage="**" ValidationExpression="[+]?([0-9]*\.[0-9]+|[0-9]+)"
                                                            runat="server"></asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtfooterItemPrice" runat="server" Width="40" Text='0.00'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="validator"
                                                            Display="Dynamic" ControlToValidate="txtfooterItemPrice" ErrorMessage="**" ValidationExpression="[+]?([0-9]*\.[0-9]+|[0-9]+)"
                                                            runat="server"></asp:RegularExpressionValidator>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount" ItemStyle-Width="40">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscountPerc" Text=' <%#String.Format("{0:N2}", Eval("DiscountPerc"))%> '
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtUpdateDiscountPerc" Width="37" runat="server" Text=' <%#String.Format("{0:N2}", Eval("DiscountPerc"))%>'> </asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorDiscountPerc" CssClass="validator"
                                                            Display="Dynamic" ControlToValidate="txtUpdateDiscountPerc" ErrorMessage="**"
                                                            ValidationExpression="[+]?([0-9]*\.[0-9]+|[0-9]+)" runat="server"></asp:RegularExpressionValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtfooterDiscountPerc" runat="server" Width="37" Text='0.00'> </asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorDiscountPerc" CssClass="validator"
                                                            Display="Dynamic" ControlToValidate="txtfooterDiscountPerc" ErrorMessage="**"
                                                            ValidationExpression="[+]?([0-9]*\.[0-9]+|[0-9]+)" runat="server"></asp:RegularExpressionValidator>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount Type" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlatPerc" Text='<%# Eval("DiscountFlatOrPercent") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="FlatPerc" Width="50" DataTextField="DiscountFlatOrPercent"
                                                            DataValueField="DiscountFlatOrPercent" SelectedValue='<%# Bind("DiscountFlatOrPercent") %>'
                                                            runat="server">
                                                            <asp:ListItem>Flat</asp:ListItem>
                                                            <asp:ListItem>%</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="drpFlatPerc" TabIndex="61" Width="50" runat="server">
                                                            <asp:ListItem>Flat</asp:ListItem>
                                                            <asp:ListItem>%</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total" ItemStyle-Width="70">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubTotal" Text=' <%#String.Format("{0:N2}", Eval("SubTotal"))%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblUpateDescription" Text=' <%#String.Format("{0:N2}", Eval("SubTotal"))%>'
                                                            runat="server"></asp:Label>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfooterSubTotal" Text=' ' runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <table border="0" cellpadding="1" cellspacing="1" width="100%" id="table1">
                                                    <tr style="text-align: center; font-family: Verdana; color: #000000; background-color: #CAC674">
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Add
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Preview
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Qty
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Item ID
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Item Name
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Description
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            UOM
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Price
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Discount</td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Discount Type
                                                        </td>
                                                        <td style="text-align: center; font-weight: bold;">
                                                            Total
                                                        </td>
                                                    </tr>
                                                    <tr style="text-align: center;">
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton3" ValidationGroup="EmptyInsert" ToolTip="Add Item"
                                                                CommandName="EmptyInsert" ImageUrl="~/images/Add-Button.gif" runat="server" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton4" ToolTip="Preview Item" CommandName="EmptySelect"
                                                                ImageUrl="~/images/Preview-Button.gif" runat="server" /></td>
                                                        <td>
                                                            <asp:DropDownList ID="drpEmptyItemQty" AppendDataBoundItems="True" runat="server">
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmptyItem" Width="60" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEmptyItemName" runat="server" Width="100" Text=" "></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmptyItemDescription" Width="200" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:Label ID="lblEmptyUOM" runat="server" Text=""></asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmptyUnitPrice" Width="40" Text="0.00" runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="validator"
                                                                Display="Dynamic" ControlToValidate="txtEmptyUnitPrice" ErrorMessage="**" ValidationExpression="[+]?([0-9]*\.[0-9]+|[0-9]+)"
                                                                runat="server"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmptyDiscountPerc" ValidationGroup="EmptyInsert" Width="40" Text="0.00"
                                                                runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorDiscountPerc" ValidationGroup="EmptyInsert"
                                                                CssClass="validator" Display="Dynamic" ControlToValidate="txtEmptyDiscountPerc"
                                                                ErrorMessage="**" ValidationExpression="[+]?([0-9]*\.[0-9]+|[0-9]+)" runat="server"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpEmptyFlatPerc" TabIndex="61" Width="50" runat="server">
                                                                <asp:ListItem>Flat</asp:ListItem>
                                                                <asp:ListItem>%</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEmptyTotal" runat="server" Text="0.00"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                            </Ajax:AjaxPanel> 
                                         </div>
								 
							        </div>
                    </div>
                    <!-- END PORTLET-->
                     
                     

                 <!-- BEGIN PORTLET-->
					<div class="portlet box green">
                          <div class="portlet-title">
                            <div class="caption" style="width:60%;" >

                           <Ajax:AjaxPanel ID="AjaxPanel16" runat="server">
                              <div class="row">
                                    <div class="col-md-3 text-left">Ship To </div>
                                    <div class="col-md-3 text-left"><asp:CheckBox ID="chkBillingAddress" Visible="true" Text="same as billing" ForeColor="white"   runat="server" AutoPostBack="True"  /></div>
                                    <div class="col-md-3 text-left"><asp:LinkButton ID="lnkShipAddress" ForeColor="white"     runat="server"> View 'ship to' address </asp:LinkButton> </div>
                                    <div class="col-md-3 text-left">
                                        <asp:LinkButton ID="lnkDeliveryLocations"    ForeColor="white"    runat="server">Points of interest</asp:LinkButton>
                                     </div> 
                                  </div>
                           </Ajax:AjaxPanel> 
                           
                            </div>
                            <div class="tools"> <a href="javascript:;" class="<%=Ship_To %>"></a> </div>
                          </div>
                  <div class="portlet-body" style="display: none;">
                   
                	<div class="row">
                      <div class="col-md-8">
                      	<div class="table-responsive">
                             <Ajax:AjaxPanel ID="AjaxPanel17" runat="server">
                      		<table class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td style="width:20%">Delivery Date</td>
                                        <td style="width:30%">
                                        
                                        <div class="input-icon">
                                          
                                          <asp:TextBox ID="txtDeliveryDate"   runat="server"  CssClass="form-control input-sm date-picker" ></asp:TextBox>

                                        </div>
                                        </td>
                                        <td style="width:20%">Delivery Method</td>
                                        <td style="width:30%">
                                        
                                            
                                      <Ajax:AjaxPanel ID="AjaxPanel29" runat="server">
                                            <asp:DropDownList ID="drpShipMethod" runat="server"  CssClass="form-control input-sm"  AutoPostBack="True">
                                            </asp:DropDownList>
                                     </Ajax:AjaxPanel> 
                                                 
                                        </td>
                                    </tr>
                                    <tr>
                                      <td>Destination Type</td>
                                      <td>
                                      
                                      <asp:DropDownList ID="drpDestinationType" runat="server" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                    
                                    
                                    </td>
                                      <td>Priority</td>
                                      <td>
                                      
                                      <asp:DropDownList ID="drpPriorirty" runat="server" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                      <td><label>Salutation</label>
                                        
                                        <asp:DropDownList ID="drpShipCustomerSalutation" runat="server" CssClass="form-control input-sm">
                                                </asp:DropDownList>

                                        </td>
                                      <td colspan="3">
                                      <div class="row">
                                      	<div class="col-sm-6">
                                          <label>First</label>
                                      
                                          <asp:TextBox ID="txtShippingName" runat="server" Text=''  CssClass="form-control input-sm"  ></asp:TextBox>
                                      </div>
                                      <div class="col-sm-6"><label>Last</label>
                                            <asp:TextBox ID="txtShippingLastName" runat="server" CssClass="form-control input-sm"  ></asp:TextBox>
                                       
                                      </div>
                                      </div>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>Attention</td>
                                      <td>
                                          <asp:TextBox   ID="txtShippingAttention" runat="server"  CssClass="form-control input-sm"  ></asp:TextBox>   
                                         
                                      
                                      </td>
                                      <td>Driver Routing Info</td>
                                      <td>Zone
                                        <Ajax:AjaxPanel ID="AjaxPanel30" runat="server">

               
                                        
                                        <asp:DropDownList   ID="drpZone" runat="server"  CssClass="form-control input-sm input-inline input-150"  AutoPostBack="true">
                                        </asp:DropDownList>
                                        </Ajax:AjaxPanel> 
                                        </td>
                                    </tr>
                                    <tr>
                                      <td>Company</td>
                                      <td>
                                      
                                                                        
                                      <asp:TextBox   ID="txtShipCompany" runat="server" CssClass="form-control input-sm"  ></asp:TextBox>

                                      </td>
                                      <td colspan="2" rowspan="4">

                                        <asp:TextBox  ID="txtDriverRouteInfo"  Rows="11" Columns="20"  CssClass="form-control input-xs"   TextMode="MultiLine" runat="server"></asp:TextBox>

                                      	
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>Add 1</td>
                                      <td>
                                       
                                       <Ajax:AjaxPanel ID="AjaxPanel301" runat="server">

               
                                        <asp:TextBox ID="txtShippingAddress1"   runat="server" Text='' AutoPostBack="true"  CssClass="form-control input-sm" ></asp:TextBox>
                                        
                                        </Ajax:AjaxPanel> 
                                        
                                       </td>
                                    </tr>
                                    <tr>
                                      <td>Add 2</td>
                                      <td>
                                        <asp:TextBox ID="txtShippingAddress2" runat="server" Text='' CssClass="form-control input-sm"></asp:TextBox>
                                         
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                      <td>Add 3</td>
                                      <td>
                                            <asp:TextBox ID="txtShippingAddress3" runat="server" Text='' CssClass="form-control input-sm"  ></asp:TextBox>
                                        
                                      </td>
                                    </tr>
                                    
                                    <tr>
                                      <td>City</td>
                                      <td> <asp:TextBox ID="txtShippingCity"   runat="server" Text=''  CssClass="form-control input-sm"  ></asp:TextBox> </td>
                                      <td colspan="2">&nbsp;
                                       <table   width="100%"   id="table24"  >
                                            <tr style="text-align:center">
                                                <td>
                                                    <div id="Div2" runat="server" visible="false"> Char &nbsp; <input id="txtDriverMessageLen" value="136" tabindex="65" style="width: 24px" type="text" /> </div>

                                                    <asp:Button ID="btnVerifyAddress"  CssClass="btn btn-block btn-xs btn-success" runat="server" Text="Verify Address"></asp:Button>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="btnZoneMap" Visible="false" runat="server" CssClass="btn btn-block btn-xs btn-info" Text="Zone Map" ></asp:Button>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <a href="#" onclick="GoogleDirect()" class="btn btn-block btn-xs btn-info"   id="HyperLink1" tabindex="79"><u>Google Map</u></a>                                                                                                        
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink Target="_blank" NavigateUrl="http://zip4.usps.com/zip4/welcome.jsp" CssClass="btn btn-block btn-xs btn-primary" ID="HlkFindZip" runat="server"  >Find Zip/Postal</asp:HyperLink>
                                                </td>
                                                 <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                      <asp:Button ID="btnAcceptAddres" runat="server" Text="ACCEPT" CssClass="actionbutton" Visible="false" />
                                      <asp:Button ID="btnRejectAddress" runat="server" Text="REJECT" CssClass="actionbutton" Visible="false"/>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td colspan="4">
                                      	<div class="row">
                                      		<div class="col-md-4">
                                            <label>Country</label>
                                             

                                            <asp:HiddenField ID="hiddenState" runat="server" />
                                            
                                            <Ajax:AjaxPanel ID="AjaxPanel32" runat="server">

                                            
                                            <asp:DropDownList ID="drpShipCountry"   runat="server"  CssClass="form-control input-sm" AutoPostBack="True">
                                            </asp:DropDownList>

                                            </Ajax:AjaxPanel> 
                                            </div>
                                            <div class="col-md-4">
                                            <label>State/Province</label>
                                                  <Ajax:AjaxPanel ID="AjaxPanel33" runat="server">

              
                                            <asp:DropDownList ID="drpShippingState"  AutoPostBack="true" CssClass="form-control input-sm" runat="server"  >
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtShippingState" Visible="false"  runat="server"   CssClass="form-control input-sm"   Text=""></asp:TextBox>
                                                 </Ajax:AjaxPanel> 
                                            </div>
                                            <div class="col-md-4">
                                            <label>Zip/Postal</label>
                                            
                                            <Ajax:AjaxPanel ID="AjaxPanel34" runat="server">

               
                                                <asp:TextBox ID="txtShippingZip" runat="server"   Text='' CssClass="form-control input-sm" AutoPostBack="True" ></asp:TextBox>
                                                <div align="left" class="boxdiv" id="autocomplete3" style="overflow-x:hidden; overflow-y:auto;BACKGROUND-COLOR:#E1DCAC;height:0px;position:absolute;z-index:2; width:300px" ></div>
                                                <asp:LinkButton ID="ImgPostalCode" runat="server" Text=""></asp:LinkButton>
                                                
                                                </Ajax:AjaxPanel>     
                                            </div>
                                      	</div>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>Phone</td>
                                      <td>
                                       
                                      <asp:TextBox  CssClass="form-control input-sm" ID="txtShipCustomerPhone" runat="server"  ></asp:TextBox>
                                      </td>
                                      <td>Ext</td>
                                      <td>
                                      <asp:TextBox CssClass="form-control input-sm input-50"  ID="txtShipExt" runat="server"  ></asp:TextBox>
                                       </td>
                                    </tr>
                                    <tr>
                                      <td>Cell</td>
                                      <td>
                                      <asp:TextBox CssClass="form-control input-sm" ID="txtShipCustomerCell" runat="server"  ></asp:TextBox>
                                      </td>
                                      <td>Fax</td>
                                      <td><asp:TextBox CssClass="form-control input-sm" ID="txtShipCustomerFax" runat="server"  ></asp:TextBox></td>
                                    </tr>
                                </tbody>
                        	</table>
                            </Ajax:AjaxPanel> 
                        </div>
                      </div>
                      <div class="col-md-4">
                             <Ajax:AjaxPanel ID="AjaxPanel18" runat="server">
                      		<table class="table table-striped table-hover table-bordered">
                      	<tbody>
                        	<tr>
                            	<td>
                                <div class="alert alert-info margin-bottom-0">
                                  <div class="row">
                                    <div class="col-md-6">Card Message</div>
                                    <div class="col-md-6"><div class="pull-right">Char <input id="txtMessageLen" value="330" class="form-control input-xs input-75" type="text" style="display:inline-block" /></div>
                                    </div>
                                  </div>
                                </div>
                                </td>
                            </tr>
                            <tr>
                            	<td>
                                <asp:TextBox ID="txtCardMessageDesc" runat="server"  Rows="29"  Columns="20" CssClass="form-control input-xs"  TextMode="MultiLine"  ></asp:TextBox>
                                 
                                </td>
                            </tr>
                            <tr>
                            <td>
                                
                                <div class="row">
                                  <div class="col-md-6">Occasion Code</div>
                                  <div class="col-md-6">
                                     <asp:DropDownList ID="drpOccasionCode" runat="server" Width="105" TabIndex="89">    </asp:DropDownList>
                                  </div>
                                </div>
                            </td>
                            </tr>
                            <tr>
                            	<td>
                                <div class="row">
                                          <div class="col-xs-4">
                          <asp:LinkButton ID="lnkSuggestions" runat="server"  CssClass="btn btn-info btn-block btn-xs"  >Suggestions</asp:LinkButton>
                            
                          </div>
                                          <div class="col-xs-4">
                          <asp:LinkButton ID="lnkMacro"  CssClass="btn btn-info btn-block btn-xs" runat="server"    >Macro</asp:LinkButton>
                          
                          </div>
                                              <div class="col-xs-4">
                           <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="btn btn-primary btn-block btn-xs" >Spell Check</asp:LinkButton>
                           
                          
                          </div>
                                 </div>
                        
                        </td>
                            </tr>
                        </tbody>
                      </table>
                           </Ajax:AjaxPanel> 
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
							<div class="caption">Order Total</div>
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
                                                    <td> <asp:CheckBox ID="chkPosted" runat="server" Enabled="False"></asp:CheckBox>  </td>
                                                    <td>Posted</td>
                                                    <td> <asp:TextBox ID="txtPostedDate" runat="server" ReadOnly="false" Text=''  CssClass="form-control input-xs"   TabIndex="94"></asp:TextBox>   </td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtPostedTime" runat="server" ReadOnly="false"  CssClass="form-control input-xs"   TabIndex="95"></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                  <tr>
                                                    <td> <asp:CheckBox ID="chkPicked" runat="server" Enabled="False" />   
                                                                
                                                    
                                                    </td>
                                                    <td>Picked</td>
                                                    <td><asp:TextBox ID="txtPickedDate" runat="server" ReadOnly="false" CssClass="form-control input-xs" Text=''  ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtPickedTime" runat="server" ReadOnly="false" CssClass="form-control input-xs"  ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                  <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkPrinted" runat="server" Enabled="False" />  </td>
                                                    <td>Printed</td>
                                                    <td><asp:TextBox ID="txtPrintedDate" runat="server" ReadOnly="false" CssClass="form-control input-xs" Text=''  ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtPrintedTime" runat="server" ReadOnly="false" CssClass="form-control input-xs" ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                  <tr>
                                                    <td> <asp:CheckBox ID="chkBilled" runat="server" Enabled="False" /> </td>
                                                    <td>Billed</td>
                                                    <td><asp:TextBox ID="txtBilledDate" runat="server" CssClass="form-control input-xs" ReadOnly="false" Text='' ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtBilledTime" runat="server" CssClass="form-control input-xs" ReadOnly="false"  ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                  <tr>
                                                    <td> <asp:CheckBox ID="chkShipped" runat="server" Enabled="False" /> </td>
                                                    <td>Shipped</td>
                                                    <td><asp:TextBox ID="txtShipDate" runat="server" CssClass="form-control input-xs" ReadOnly="false"  ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtShipTime" runat="server" CssClass="form-control input-xs" ReadOnly="false"  ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
                                                  <tr>
                                                    <td> <asp:CheckBox ID="chkInvoiced" runat="server" Enabled="False" /> </td>
                                                    <td>Invoiced</td>
                                                    <td><asp:TextBox ID="txtInvoiceDate" CssClass="form-control input-xs" runat="server" ReadOnly="false"   ></asp:TextBox></td>
                                                    <td>Date</td>
                                                    <td><asp:TextBox ID="txtInvoiceTime" runat="server" CssClass="form-control input-xs" ReadOnly="false"  ></asp:TextBox></td>
                                                    <td>Time</td>
                                                  </tr>
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
                                                    <td colspan="2">Internal Notes</td>
                                                  </tr>
                                                  <tr>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtInternalNotes"   TextMode="MultiLine"   Rows="10"  CssClass="form-control input-xs"    runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtInternal" Visible="false"  ReadOnly="true" CssClass="NotesStyle" TextMode="MultiLine"   Height="95" Width="200" runat="server"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                  <tr>
                                                    <td class="text-center">
                                                        <asp:LinkButton ID="lnkChangeDelivery"   CausesValidation="false"    CssClass="btn btn-success btn-xs" runat="server"  >Shipping Trace</asp:LinkButton>
                                                    </td>
                                                    <td class="text-center">
                                                        <asp:LinkButton ID="lnkChangeHistory"   CausesValidation="false"  CssClass="btn btn-success btn-xs" runat="server"  >Change History</asp:LinkButton>
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
                                            <td colspan="3" class="text-right">Sub Total</td>
                                            <td style="width:25%"><asp:TextBox ID="txtSubtotal" runat="server" Enabled="True" ReadOnly="True" Text='' CssClass="form-control input-xs"   ></asp:TextBox> </td>
                                            </tr>
                                            <tr>
                                            <td colspan="3" class="text-right">
                                             <Ajax:AjaxPanel ID="AjaxPanel305" runat="server">
                                                 <asp:CheckBox ID="chkDeliveryOverride" runat="server" Text="Override" AutoPostBack="True"  />  &nbsp;&nbsp;
                                             </Ajax:AjaxPanel> 
                                            Delivery</td>
                                            <td>
                                            
                                             <Ajax:AjaxPanel ID="AjaxPanel35" runat="server">

               
                                            <asp:TextBox ID="txtDelivery" CssClass="form-control input-xs"  runat="server"     AutoPostBack="True"></asp:TextBox>
                                            
                                            </Ajax:AjaxPanel> 
                                            </td>
                                            
                                            
                                            </tr>
                                            <tr>
                                            <td colspan="3" class="text-right">
                                            <Ajax:AjaxPanel ID="AjaxPanel3600" runat="server">
                                                <asp:CheckBox ID="chkServiceOverride" runat="server" Text="Override" AutoPostBack="True" /> &nbsp;&nbsp;&nbsp;
                                            </Ajax:AjaxPanel> 
                                            Service</td>
                                            <td>
                                            <Ajax:AjaxPanel ID="AjaxPanel36" runat="server">

               
                                                <asp:TextBox ID="txtService" CssClass="form-control input-xs" runat="server" CausesValidation="true"   AutoPostBack="True"></asp:TextBox>
                                              </Ajax:AjaxPanel>   
                                             </td>
                                            </tr>
                                            <tr>
                                            <td colspan="3" class="text-right">
                                            <Ajax:AjaxPanel ID="AjaxPanel3007" runat="server">
                                              <asp:CheckBox ID="chkRelayOverride" runat="server" AutoPostBack="True"  Text="Override" /> &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                            </Ajax:AjaxPanel> 
                                            Relay</td>
                                            <td> 
                                            <Ajax:AjaxPanel ID="AjaxPanel37" runat="server">

              
                                                <asp:TextBox ID="txtRelay" CssClass="form-control input-xs" runat="server" CausesValidation="true"   AutoPostBack="True"></asp:TextBox> 
                                             </Ajax:AjaxPanel> 
                                             </td>
                                            </tr>
                                            <tr>
                                            <td colspan="3" class="text-right">
                                            <Ajax:AjaxPanel ID="AjaxPanel30077" runat="server">
                                                <asp:CheckBox ID="checkDiscounts" runat="server" Text="Override" CssClass="txtStyle" AutoPostBack="True"  /> &nbsp;Discounts
                                                <table  width="100%">
                                                    <tr >
                                                        <td width="50%" class="text-right"> 
                                                            <asp:DropDownList ID="drpdiscounttype" visible="false" runat="server">
                                                            <asp:ListItem Text="Flat" Value="Flat"></asp:ListItem>
                                                            <asp:ListItem Text="%" Value="%"></asp:ListItem>
                                                        </asp:DropDownList> 
                                                        </td>
                                                        <td width="50%" class="text-right">
                                                            <asp:TextBox ID="txtdiscounttypevalue" visible="false" runat="server" CssClass="form-control input-xs" Width="70" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr >
                                                        <td width="100%" colspan="2" align="center">
                                                            <asp:Button ID="btnapplydiscount" Visible="false" CssClass="btn btn-success btn-lg"  runat="server" Text="Apply Discount" /> 
                                                        </td>
                                                    </tr>
                                                </table>
                                                         
                                                    
                                                          
                                                    
                                                        
                                            </Ajax:AjaxPanel> 
                                            
                                            </td>
                                            <td><asp:TextBox ID="txtDiscountAmount" CssClass="form-control input-xs" runat="server" Text=''   Enabled="True" ReadOnly="True"  ></asp:TextBox></td>
                                            </tr>

                                            <tr runat="server" id="trTax" >
                                                <td  class="text-right">Tax</td>
                                                <td>
                                                
                                                <Ajax:AjaxPanel ID="AjaxPanel38" runat="server">

               
                                                    <asp:DropDownList AutoPostBack="true" CssClass="form-control input-xs" ID="drpTaxes" runat="server">
                                                     </asp:DropDownList>
                                                 </Ajax:AjaxPanel> 
                                                 
                                                 
                                                </td>
                                                <td><asp:TextBox ID="txtTaxPercent" CssClass="form-control input-xs" runat="server" ReadOnly="True"   SkinID="textEditorSkin38" Enabled="True" TabIndex="112" Width="30"></asp:TextBox></td>
                                                <td><asp:TextBox ID="txtTax" CssClass="form-control input-xs"  runat="server" ReadOnly="True"     Enabled="True"  ></asp:TextBox></td>
                                            </tr>
 
                                                               

                                           <tr runat="server" id="trGSTTax" >
                                            <td rowspan="2" style="width:25%" >Tax</td>
                                            <td style="width:25%">
                                            
                                            <Ajax:AjaxPanel ID="AjaxPanel39" runat="server">

               
                                                <asp:DropDownList AutoPostBack="true"  CssClass="form-control input-xs" ID="drpTaxesGSTHST" runat="server">
                                                    <asp:ListItem Value="GST/HST" Text="GST/HST"></asp:ListItem> 
                                                </asp:DropDownList>
                                                
                                            </Ajax:AjaxPanel> 
                                            </td>
                                            <td style="width:25%"><asp:TextBox ID="txtTaxPercentGST" CssClass="form-control input-xs" runat="server" ReadOnly="True" Text="5%"  SkinID="textEditorSkin38" Enabled="True" TabIndex="112" Width="30"></asp:TextBox></td>
                                            <td style="width:25%"><asp:TextBox ID="txtTaxGST"   runat="server" ReadOnly="True" CssClass="form-control input-xs"    Enabled="True"  ></asp:TextBox></td>
                                            </tr>
                                            <tr runat="server" id="trPSTTax" >
                                            <td>
                                             <Ajax:AjaxPanel ID="AjaxPanel40" runat="server">

               
                                                <asp:DropDownList AutoPostBack="true" CssClass="form-control input-xs" ID="drpTaxesPST" runat="server">
                                                    <asp:ListItem Value="PST" Text="PST"></asp:ListItem> 
                                                </asp:DropDownList>
                                                
                                              </Ajax:AjaxPanel> 
                                            </td>
                                            <td><asp:TextBox ID="txtTaxPercentPST" CssClass="form-control input-xs" runat="server" ReadOnly="True" Text="7%"     Enabled="True" TabIndex="112" Width="30"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtTaxPST" CssClass="form-control input-xs"  runat="server" ReadOnly="True"    Enabled="True"  ></asp:TextBox></td>
                                            </tr>
                                            <tr class="total-foot">
                                            <td colspan="3" class="text-right"><strong class="text-white">Total</strong></td>
                                            <td><asp:TextBox ID="txtTotal" CssClass="form-control input-xs" runat="server" ReadOnly="True"      Text='' Enabled="True"  ></asp:TextBox></td>
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

             <Ajax:AjaxPanel ID="AjaxPanel22" runat="server">
                <asp:Panel ID="pnlWireOutInformation" runat="server" Visible="false">
                <div     class="row">
					<div class="col-md-12">
                    <!-- BEGIN PORTLET-->
					<div class="portlet box green">
                    	<div class="portlet-title">
							<div class="caption">Wire Out Information</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body">
                        
                                        
                                            <table  class="table table-striped table-hover table-bordered">
                                                <tr>
                                                    <td valign="top">
                                                        <table border="0" width="757" id="table43" cellspacing="0" cellpadding="0">
                                                            <tr bgcolor="#456037">
                                                                <td height="26" width="19">
                                                                </td>
                                                                <td height="26" width="805" colspan="7" align="left">
                                                                    <b><font color="white">Wire Out Information</font></b></td>
                                                            </tr>
                                                            <tr>
                                                                <td height="14" width="19">
                                                                </td>
                                                                <td height="14" width="92">
                                                                </td>
                                                                <td height="14" width="152">
                                                                </td>
                                                                <td height="14" width="127">
                                                                </td>
                                                                <td height="14" width="130">
                                                                </td>
                                                                <td height="14" width="51">
                                                                </td>
                                                                <td height="14" width="171">
                                                                </td>
                                                                <td height="14" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="14" width="19">
                                                                </td>
                                                                <td height="14" width="92">
                                                                    Wire Service</td>
                                                                <td height="14" width="152">
                                                                    <asp:DropDownList ID="drpWireoutService" Width="123" runat="server">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td height="14" width="127">
                                                                    Wire Service Code #</td>
                                                                <td height="14" width="130">
                                                                    <asp:TextBox ID="txtWireoutCode" runat="server" Width="117px"></asp:TextBox>
                                                                </td>
                                                                <td height="14" width="51">
                                                                    Owner</td>
                                                                <td height="14" width="171">
                                                                    <asp:TextBox ID="txtWireoutOwner" runat="server" Width="180px"></asp:TextBox>
                                                                </td>
                                                                <td height="14" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="4" width="19">
                                                                </td>
                                                                <td height="4" width="92">
                                                                </td>
                                                                <td height="4" width="152">
                                                                </td>
                                                                <td height="4" width="127">
                                                                </td>
                                                                <td height="4" width="130">
                                                                </td>
                                                                <td height="4" width="51">
                                                                </td>
                                                                <td height="4" width="171">
                                                                </td>
                                                                <td height="4" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="14" width="805" colspan="8">
                                                                    <table border="0" width="775" id="table44" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="19">
                                                                            </td>
                                                                            <td width="84">
                                                                                Florist Name</td>
                                                                            <td width="60" align="left">
                                                                                <asp:Button CssClass="actionbutton" ID="btnFloristSelect" OnClientClick="VendorSearch()"
                                                                                    runat="server" Width="50" Text="Select" /></td>
                                                                            <td width="230" align="right">
                                                                                <asp:TextBox ID="txtWireoutFlorist" Width="210" runat="server">
                                                                                </asp:TextBox></td>
                                                                            <td width="69">
                                                                                &nbsp;&nbsp;Address</td>
                                                                            <td width="291">
                                                                                <asp:TextBox ID="txtAddress" Width="295" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="19">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="4" width="19">
                                                                </td>
                                                                <td height="4" width="92">
                                                                </td>
                                                                <td height="4" width="152">
                                                                </td>
                                                                <td height="4" width="127">
                                                                </td>
                                                                <td height="4" width="130">
                                                                </td>
                                                                <td height="4" width="51">
                                                                </td>
                                                                <td height="4" width="171">
                                                                </td>
                                                                <td height="4" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="14" width="19">
                                                                </td>
                                                                <td height="14" width="92">
                                                                    City</td>
                                                                <td height="14" width="152">
                                                                    <asp:TextBox ID="txtWireoutCity" Width="117" runat="server">
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td height="14" width="547" colspan="4">
                                                                    <table border="0" width="493" id="table45" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="66">
                                                                                State/Prov</td>
                                                                            <td width="147">
                                                                                <asp:DropDownList ID="drpWireOutState" Width="135" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="66">
                                                                                Zip/Postal</td>
                                                                            <td width="55">
                                                                                <asp:TextBox Width="46" ID="txtWireoutZip" runat="server">
                                                                                </asp:TextBox></td>
                                                                            <td width="53">
                                                                                Country</td>
                                                                            <td width="106">
                                                                                <asp:DropDownList ID="drpWireOutCountry" Width="106" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td height="14" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="4" width="19">
                                                                </td>
                                                                <td height="4" width="92">
                                                                </td>
                                                                <td height="4" width="152">
                                                                </td>
                                                                <td height="4" width="127">
                                                                </td>
                                                                <td height="4" width="130">
                                                                </td>
                                                                <td height="4" width="51">
                                                                </td>
                                                                <td height="4" width="171">
                                                                </td>
                                                                <td height="4" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="14" width="19">
                                                                </td>
                                                                <td height="14" width="92">
                                                                    Phone</td>
                                                                <td height="14" width="152">
                                                                    <asp:TextBox ID="txtWireoutPhone" Width="117" runat="server">
                                                                    </asp:TextBox>
                                                                </td>
                                                                <td height="14" width="547" colspan="4">
                                                                    <table border="0" width="493" id="table46" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="68">
                                                                                Email</td>
                                                                            <td width="189">
                                                                                <asp:TextBox ID="txtWireoutEmail" Width="175" runat="server">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                            <td width="51">
                                                                                WebSite</td>
                                                                            <td width="185">
                                                                                <asp:TextBox ID="txtWireoutWebsite" runat="server" Width="180px"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td height="14" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="4" width="19">
                                                                </td>
                                                                <td height="4" width="92">
                                                                </td>
                                                                <td height="4" width="152">
                                                                </td>
                                                                <td height="4" width="127">
                                                                </td>
                                                                <td height="4" width="130">
                                                                </td>
                                                                <td height="4" width="51">
                                                                </td>
                                                                <td height="4" width="171">
                                                                </td>
                                                                <td height="4" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="14" width="19">
                                                                </td>
                                                                <td height="14" width="92">
                                                                    Fax</td>
                                                                <td height="14" width="152">
                                                                    <asp:TextBox ID="txtWireoutFax" runat="server" Width="117px"></asp:TextBox>
                                                                </td>
                                                                <td height="14" width="547" colspan="4">
                                                                    <table border="0" width="493" id="table47" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="68">
                                                                                Trans. ID</td>
                                                                            <td width="189">
                                                                                <asp:TextBox ID="txtWireoutTransID" runat="server" Width="175px"></asp:TextBox></td>
                                                                            <td width="51">
                                                                                Status</td>
                                                                            <td width="185">
                                                                                <asp:DropDownList ID="drpWireoutStatus" Width="185" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td height="14" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="4" width="19">
                                                                </td>
                                                                <td height="4" width="92">
                                                                </td>
                                                                <td height="4" width="152">
                                                                </td>
                                                                <td height="4" width="547" colspan="4">
                                                                    <table border="0" width="493" id="table48" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="68">
                                                                            </td>
                                                                            <td width="189">
                                                                            </td>
                                                                            <td width="51">
                                                                            </td>
                                                                            <td width="185">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td height="4" width="0">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="805" colspan="8">
                                                                    <table border="0" width="775" id="table49" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td width="19" height="14">
                                                                                &nbsp;</td>
                                                                            <td width="92" height="14">
                                                                                Trans.Method</td>
                                                                            <td width="152" height="14">
                                                                                <asp:DropDownList ID="drpWireoutTransMethod" Width="123" runat="server">
                                                                                </asp:DropDownList></td>
                                                                            <td width="68" height="14">
                                                                                Wire Notes</td>
                                                                            <td rowspan="3" height="14" valign="top">
                                                                                <asp:TextBox ID="txtWireNotes" CssClass="txtWireNotesStyle" runat="server" Height="43"
                                                                                    TextMode="MultiLine" Width="419"></asp:TextBox>
                                                                            </td>
                                                                            <td width="19" height="14">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="19" height="4">
                                                                            </td>
                                                                            <td width="92" height="4">
                                                                            </td>
                                                                            <td width="152" height="4">
                                                                            </td>
                                                                            <td width="68" height="4">
                                                                            </td>
                                                                            <td width="19">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="19" height="14">
                                                                                &nbsp;</td>
                                                                            <td width="92" height="14">
                                                                                Priority</td>
                                                                            <td width="152" height="14">
                                                                                <asp:DropDownList ID="drpWireoutPriority" Width="123" runat="server">
                                                                                </asp:DropDownList></td>
                                                                            <td width="68" height="14">
                                                                                &nbsp;</td>
                                                                            <td height="14">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="7" width="736" colspan="8">
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#456037">
                                                    <td height="1" width="736" colspan="8">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="42" width="805" colspan="8" valign="top">
                                                        <table border="0" width="775" id="table50" cellspacing="0" cellpadding="0" height="11">
                                                            <tr>
                                                                <td width="19" height="9">
                                                                </td>
                                                                <td width="222" height="9">
                                                                </td>
                                                                <td width="226" height="9">
                                                                </td>
                                                                <td width="220" height="9">
                                                                </td>
                                                                <td width="69" height="9">
                                                                </td>
                                                                <td width="19" height="9">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6">
                                                                    <div style="color: Red;" id="divordertransmiterror" runat="server">
                                                                    </div>
                                                                    <br />
                                                                    <asp:Button CssClass="actionbutton" ID="btnTransmit" Visible="false" runat="server"
                                                                        Text="Transmit" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="19" height="24">
                                                                </td>
                                                                <td width="222" height="24" align="left">
                                                                    <asp:Button CssClass="actionbutton" ID="btnUpdate" Visible="false" runat="server"
                                                                        Text="Update" /></td>
                                                                <td width="226" height="24" align="left">
                                                                    <asp:Button CssClass="actionbutton" Visible="false" ID="btnForward" runat="server"
                                                                        Text="Forward" /></td>
                                                                <td width="220" height="24">
                                                                    <asp:Button CssClass="actionbutton" ID="btnWireCancel" Visible="false" runat="server"
                                                                        Text="Cancel" /></td>
                                                                <td width="69" height="24">
                                                                </td>
                                                                <td width="19" height="24">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td width="19" height="11">
                                                                </td>
                                                                <td width="222" height="11">
                                                                </td>
                                                                <td width="226" height="11">
                                                                </td>
                                                                <td width="220" height="11">
                                                                </td>
                                                                <td width="69" height="11">
                                                                </td>
                                                                <td width="19" height="11">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        
                                        <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="red"></asp:Label>
                                      
                        </div>
					</div>
                    <!-- END PORTLET-->
					</div>
				</div>
                </asp:Panel>
            </Ajax:AjaxPanel> 

 <div class="row">
					<div class="col-md-12">
                    <!-- BEGIN PORTLET-->
					<div class="portlet box green">
                    	<div class="portlet-title">
							<div class="caption">Payment Information</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body">
							<div class="row">
                                <div class="col-md-8">
									<!-- BEGIN FORM-->
                                        <div class="form-body">
                                       <Ajax:AjaxPanel ID="AjaxPanel23" runat="server">
                                          <table class="table table-bordered">
                                                  <tr>
                                                    <td style="width:40%;" align="center">
                                                    <div class="alert alert-info"> 
                                                     <asp:Label ID="Label2" runat="server" Text="Coupon Code"></asp:Label></div> 
                                                      
                                                   
                                                    <asp:TextBox ID="txtDiscountCode" CssClass="form-control input-sm" Width="200"  runat="server"></asp:TextBox>
                                                    <asp:TextBox ID="lblDiscountCodeAmount" runat="server" Visible="false" ReadOnly="false"  Text='0' Enabled="False"></asp:TextBox>
                                                <br /><br />
                                                 <asp:Button CssClass="btn btn-success btn-lg" ID="btnDiscountApply" ToolTip="Click to apply discount code"  Text='Apply'  runat="server" />
                                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                                 <asp:Button CssClass="btn btn-success btn-lg" ID="btncancel" ToolTip="Click to Remove discount code"  Text='Remove'  runat="server" />
                                                 
                                                 <br /><br />

                                                <asp:Label ID="lblcodeerror" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                                <asp:Label ID="lbldiscountcodemessage" runat="server" ForeColor="Green" Visible="False"></asp:Label>
                                                    
                                                    </td>
                                                    <td>
                                                   <div class="alert alert-info"> Payment Type </div>  
                                                      
                                                     <Ajax:AjaxPanel ID="AjaxPanel41" runat="server">

                                                            <asp:DropDownList ID="drpPaymentType" CssClass="form-control input-sm" AutoPostBack="true"    runat="server" OnSelectedIndexChanged="drpPaymentType_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                       </Ajax:AjaxPanel>     
                                                           
                                                         
                                                           
                                                           
                                                            <br />
                                                        
                                                            <asp:Panel ID="pnlemv"  Visible="false"  runat="server">
                                                            <center >
                                                             <asp:Button ID="btnemv" runat="server"   Text="Click For EMV Payment Start" />
                                                                                        &nbsp;<br />
                                                             <asp:Button ID="btnemvres" Enabled = "False" runat="server"   Text="Click For EMV Payment Status" />
                                                             </center>
                                                            </asp:Panel>

                                                            <br />
                                            
                                             
                                                            <asp:GridView ID="gridPaymentGatewayTransactionLogs"  AllowSorting="True" runat="server" DataKeyNames="InLineNumber"    AutoGenerateColumns="false" AllowPaging="False" Visible="false"   >
                                                             <HeaderStyle   />
                                                                <Columns>
                                                                             
                                                                            
                                                                        <asp:BoundField DataField="ProcessDate" HeaderText="Process Date" SortExpression="Date" />
                                                                                               
                                                                        <asp:BoundField DataField="ProcessTypeAmount" HeaderText="Amount Type"   />                     
                                                                        <asp:BoundField DataField="ProcessType" HeaderText="Process Type"   /> 
                                                                        <asp:BoundField DataField="RunningAmount" DataFormatString="{0:N2}" HeaderText="Running Amount"  />
                                                                                                                                                          
                                                                </Columns>
                                                            </asp:GridView> 
                                                    
                                                    
                                                    </td>
                                                  </tr>
                                                </table>  
                                        </Ajax:AjaxPanel> 
                                        </div>
                                    <!-- END FORM-->
								</div>
								<div class="col-md-4">
									<!-- BEGIN FORM-->
                                         <Ajax:AjaxPanel ID="AjaxPanel24" runat="server">
                                        <div class="form-body">
                                            <div class="alert alert-info"><asp:Label ID="lblTitle"    runat="server" Text="Customer Profile"></asp:Label></div>
                                              

                                             <asp:Panel ID="pnlCustomerProfile" runat="server">
                                                                    <table   id="table58" class="table table-bordered">
                                                                        <tr>
                                                                            <td width="15" height="13">
                                                                            </td>
                                                                            <td width="110" height="13">
                                                                            </td>
                                                                            <td width="122" height="13">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                <asp:HyperLink Target="_blank" NavigateUrl='' ID="HyperLink2" runat="server">Account Status</asp:HyperLink>
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtAccountStatus" CssClass="form-control input-xs" runat="server" Text=''
                                                                                    TabIndex="29"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="6">
                                                                            </td>
                                                                            <td width="110" height="6">
                                                                            </td>
                                                                            <td width="122" height="6">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Credit Limit</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtCreditLimit" Enabled="false" MaxLength="14" SkinID="textEditorSkin101"
                                                                                    runat="server" Text='' TabIndex="30"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" CssClass="validator"
                                                                                    Width="150" Display="Dynamic" ControlToValidate="txtCreditLimit" ErrorMessage="Numeric only"
                                                                                    ValidationExpression="[-+]?([0-9]*\.[0-9]+|[0-9]+)" runat="server"></asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="5">
                                                                            </td>
                                                                            <td width="110" height="5">
                                                                            </td>
                                                                            <td width="122" height="5">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15" valign="middle">
                                                                                YTD Orders</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtYtdOrders" CssClass="form-control input-xs" runat="server" TabIndex="31"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="4">
                                                                            </td>
                                                                            <td width="110" height="4">
                                                                            </td>
                                                                            <td width="122" height="4">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Average Sale</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtAverageSale" CssClass="form-control input-xs" runat="server" TabIndex="32"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="3">
                                                                            </td>
                                                                            <td width="110" height="3">
                                                                            </td>
                                                                            <td width="122" height="3">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Sales LifeTime</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtSalesLifeTime" CssClass="form-control input-xs" runat="server" TabIndex="33"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="2">
                                                                            </td>
                                                                            <td width="110" height="2">
                                                                            </td>
                                                                            <td width="122" height="2">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Customer Since</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtCustomerSince" CssClass="form-control input-xs" runat="server" Text=''
                                                                                    TabIndex="34"></asp:TextBox>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtCustomerSince"
                                                                                    Display="Dynamic" ErrorMessage="mm/dd/yyyy" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="6">
                                                                            </td>
                                                                            <td width="110" height="6">
                                                                            </td>
                                                                            <td width="122" height="6">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Member Points</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtMemberPoints" CssClass="form-control input-xs" runat="server" TabIndex="35"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="7">
                                                                            </td>
                                                                            <td width="110" height="7">
                                                                            </td>
                                                                            <td width="122" height="7">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="22">
                                                                            </td>
                                                                            <td width="110" height="22" valign="middle">
                                                                                Credit Comments</td>
                                                                            <td width="122" height="22">
                                                                                <asp:TextBox ID="txtCreditComments" CssClass="form-control input-xs" runat="server" Text=''
                                                                                    TabIndex="36"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="5">
                                                                            </td>
                                                                            <td width="110" height="5 ">
                                                                            </td>
                                                                            <td width="122" height="5">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Discounts</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtDiscounts" CssClass="form-control input-xs" runat="server" TabIndex="37"
                                                                                    Enabled="False"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="5">
                                                                            </td>
                                                                            <td width="110" height="5">
                                                                            </td>
                                                                            <td width="122" height="5">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Customer Rank</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtCustomerRank" CssClass="form-control input-xs" runat="server" TabIndex="38"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Club Card #
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:TextBox ID="txtclubcard" CssClass="form-control input-xs" runat="server" TabIndex="38"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Card Status
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:TextBox ID="txtclubcardstatus" CssClass="form-control input-xs" runat="server" TabIndex="38"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Card Exp Date
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:TextBox ID="txtclubcardexpdate" CssClass="form-control input-xs" runat="server" TabIndex="38"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlWireService" Visible="false" runat="server">
                                                                    <table    id="table60" class="table table-bordered">
                                                                        <tr>
                                                                            <td width="15" height="12">
                                                                            </td>
                                                                            <td width="110" height="12">
                                                                            </td>
                                                                            <td width="122" height="12">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Wire Service</td>
                                                                            <td width="122" height="15">
                                                                            
                                                                             <Ajax:AjaxPanel ID="AjaxPanel401" runat="server">
 
                                                                                <asp:DropDownList ID="drpWire" AutoPostBack="true" Width="105" runat="server">
                                                                                </asp:DropDownList>
                                                                                 
                                                                                 </Ajax:AjaxPanel> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Code #</td>
                                                                            <td width="122" height="15">
                                                                             <Ajax:AjaxPanel ID="AjaxPanel411" runat="server">

             
                                                                                <asp:TextBox ID="txtCode" SkinID="textEditorSkin101" ToolTip="Vendor Info will be populated on the code entered"
                                                                                    AutoPostBack="True" runat="server" TabIndex="39"></asp:TextBox>
                                                                                 </Ajax:AjaxPanel> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Reference ID</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtReference" SkinID="textEditorSkin101" runat="server" TabIndex="40"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Transmit Method</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtTransmitMethod"  SkinID="textEditorSkin101" runat="server"
                                                                                    TabIndex="41"></asp:TextBox>
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Representative talked to
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtrepresentative" SkinID="textEditorSkin101" runat="server" TabIndex="41"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Florist Name
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtFloristname" SkinID="textEditorSkin101" runat="server" TabIndex="41"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Florist Phone
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtFloristPhone" SkinID="textEditorSkin101" runat="server" TabIndex="41"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Florist City
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtFloristcity" SkinID="textEditorSkin101" runat="server" TabIndex="41"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Florist State
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtFloriststate" SkinID="textEditorSkin101" runat="server" TabIndex="41"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                                Received Amount
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtReceivedamount" SkinID="textEditorSkin101" runat="server" TabIndex="41"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="15" height="15">
                                                                            </td>
                                                                            <td width="110" height="15">
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlCheck" Visible="false" runat="server">
                                                                    <table   id="table61" class="table table-bordered">
                                                                         
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                                <asp:Label ID="lblcashcheck" runat="server" Text="Check #"></asp:Label></td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtCheck" CssClass="form-control input-xs" runat="server" TabIndex="51"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                                <asp:Label ID="lblid" runat="server" Text="ID"></asp:Label>
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtID" CssClass="form-control input-xs" runat="server" TabIndex="52"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="display:none;" >
                                                                             
                                                                            <td width="110" height="15">
                                                                                Coupon</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtCoupon" CssClass="form-control input-xs" runat="server" TabIndex="53"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="display:none;">
                                                                            
                                                                            <td width="110" height="15">
                                                                                Gift Certificate</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtGiftCertificate" CssClass="form-control input-xs" runat="server" TabIndex="54"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlCreditCard" Visible="false" runat="server">
                                                                <div class="table-responsive">
                                                                    <table   id="table62" class="table table-bordered">
                                                                        
                                                                  <tr>
                                                                                
                                                                                <td width="70" height="15">
                                                                                    Stored CC</td>
                                                                                <td width="172" height="15">
                                                                                 <Ajax:AjaxPanel ID="AjaxPanel42" runat="server">

               
                                                                                     <asp:DropDownList AutoPostBack="true"   ID="drpstoredcc" CssClass="form-control"  runat="server">
                                                                                     <asp:ListItem Text="Other" Value="Other"></asp:ListItem> 
                                                                                    </asp:DropDownList>
                                                                                    </Ajax:AjaxPanel> 
                                                                                </td>
                                                                 </tr>    
                                                                            <tr id="trchkupdate" visible="false"  runat="server" >
                                                                                 
                                                                                <td width="70" height="15">
                                                                                    </td>
                                                                                <td width="172" height="15"> 
                                                                                <Ajax:AjaxPanel ID="AjaxPanel43" runat="server">
                                                                                    <asp:CheckBox  visible="false" CssClass="form-control" ID="chkupdatebilling" Text="Update Customer Billing" Checked="False" AutoPostBack="true"  runat="server" />
                                                                                   </Ajax:AjaxPanel>  
                                                                                </td>
                                                                            </tr>                                                                                                                                      
                                                                        
                                                                <tr id="trswipecard" runat="server">
                                                                    <td colspan="2">
                                                                       <div onclick="javascript:drpstoredcc_Change();">
                                                                        <asp:CheckBox ID="chkstoreCC"    Checked="false"   CssClass="form-control"   Text="Store Credit Card" runat="server" />
                                                                        </div> 
                                                                        <asp:Panel ID="pnlXM90" runat="server">
                                                                            <table class="table table-bordered">
                                                                                <tr>
                                                                                    
                                                                                    <td  >
                                                                                        <a href="javascript:reswipe( );" >Clear Card Data</a> 
                                                                                    </td>
                                                                                    <td  >
                                                                                        <a href="javascript:call();" >Translate</a> 
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                     
                                                                                    <td  >
                                                                                        CARD DATA:
                                                                                    </td>
                                                                                    <td  align="left" >
                                                                                        <asp:TextBox ID="txtrawcard" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        <input type="hidden" id="intxtrawcard" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                   
                                                                                    <td  >
                                                                                        Ignore Swipe:
                                                                                    </td>
                                                                                    <td  >
                                                                                        <asp:CheckBox ID="chkIgnoreTrack" runat="server" />
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlXM95" runat="server">
                                                                            <table>
                                                                                <tr>
                                                                                    <td width="15">
                                                                                    </td>
                                                                                    <td width="110">
                                                                                        <br />
                                                                                        <div style="padding: 15px 15px 15px 15px;">
                                                                                            <input type="button" name="DataEventEnabled" value="Reswipe">

                                                                                            <script for="DataEventEnabled" event="onClick" language="VBScript">
                                                                                               OpenClaimEnable_Click()
                                                                                            </script>

                                                                                        </div>
                                                                                    </td>
                                                                                    <td width="122">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="15">
                                                                                    </td>
                                                                                    <td width="110">
                                                                                    </td>
                                                                                    <td width="122">
                                                                                        <div style="display: none;">
                                                                                            <p>
                                                                                                Track 1 Data
                                                                                                <asp:TextBox Width="400" ID="Track1Data" runat="server"></asp:TextBox>
                                                                                            </p>
                                                                                            <p>
                                                                                                Track 2 Data
                                                                                                <asp:TextBox Width="400" ID="Track2Data" runat="server"></asp:TextBox>
                                                                                            </p>
                                                                                            <p>
                                                                                                Track 3 Data
                                                                                                <asp:TextBox Width="400" ID="Track3Data" runat="server"></asp:TextBox>
                                                                                            </p>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                            
                                                                            <td colspan="2">
                                                                                <asp:Panel ID="pnlCClable" Visible="false"   runat="server">
                                                                                
                                                                           <table >
                                                                           

                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Card Type</td>
                                                                                <td width="122" align=left height="15">
                                                                                                 <asp:Label ID="lblcctype" runat="server" Text=""></asp:Label>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="232" height="15" colspan="2" valign="top">
                                                                                    <table style="width: 232px;">
                                                                                        <tr>
                                                                                            <td style="width: 32px">
                                                                                                Card#
                                                                                            </td>
                                                                                            <td style="width: 200px">
                                                                                                 <asp:Label ID="lblcard" runat="server" Text="XXXX-XXXX-XXXX-4242"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="232" height="15" colspan="2" valign="top">
                                                                                    <table class="table table-bordered">
                                                                                        <tr>
                                                                                            <td style="width: 48px">
                                                                                                EXP
                                                                                            </td>
                                                                                            <td style="width: 64px">
                                                                                                 <asp:Label ID="lblEXP" runat="server" Text="XX/XXXX"></asp:Label>
                                                                                                 
                                                                                            </td>
                                                                                            <td style="width: 40px">
                                                                                                CSV
                                                                                            </td>
                                                                                            <td style="width: 80px">
                                                                                                <asp:TextBox ID="lblcsv" MaxLength="4" Width="56"  runat="server" Text=""></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>


                                                                           
                                                                            
                                                                           </table>                                                                            
                                                                            </asp:Panel> 
                                                                                                                                                       
                                                                                <asp:Panel ID="pnlccdetails" runat="server">
                                                                                
                                                                           <table class="table table-bordered" >
                                                                           

                                                                            <tr>
                                                                                
                                                                                <td width="110" height="15">
                                                                                    Card Type</td>
                                                                                <td  align=left height="15">
                                                                                    <asp:DropDownList ID="drpCardType" CssClass="form-control" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                <td width="232" height="15" colspan="2" valign="top">
                                                                                    <table class="table table-bordered">
                                                                                        <tr>
                                                                                            <td  >
                                                                                                Card#
                                                                                            </td>
                                                                                            <td align=left  >
                                                                                                <asp:TextBox ID="txtCard" MaxLength="16" AutoCompleteType=BusinessCountryRegion CssClass="form-control" runat="server"  ></asp:TextBox>
																								<asp:Label ID="lbltxtCard" Visible="false"  runat="server" Text="XXXX-XXXX-XXXX-4242"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
																						<tr>
                                                                                            <td>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnlinkCC" Visible="false" OnClientClick="javascript:if(confirm('Please note, on this operation you will have to enter full credit card number as it will update current credit card value. Do you want to continue?')){return true;}else{return false;};" runat="server">Edit Card</asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                <td width="232" height="15" colspan="2" valign="top">
                                                                                    <table class="table table-bordered">
                                                                                        <tr>
                                                                                            <td  >
                                                                                                EXP
                                                                                            </td>
                                                                                            <td  >
                                                                                                <asp:DropDownList ID="drpExpirationDate" CssClass="form-control input-xs" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td  >
                                                                                                CSV
                                                                                            </td>
                                                                                            <td style="width: 80px">
                                                                                                <asp:TextBox ID="txtCSV" MaxLength="4" Width="56" CssClass="form-control" runat="server"  ></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>


                                                                           
                                                                            
                                                                           </table>                                                                            
                                                                            </asp:Panel>

                                                                            </td>
                                                                            </tr>
                                                                        
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                                Approval</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtApproval" CssClass="form-control" runat="server" TabIndex="46"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                                Validation</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtValidation" CssClass="form-control" runat="server" TabIndex="47"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                                IP Address</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtIpAddress" CssClass="form-control" runat="server" TabIndex="48"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                                Fraud Rating</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtFraudRating" CssClass="form-control" runat="server" TabIndex="49"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                                Billing Zip Code</td>
                                                                            <td width="122" height="15">
                                                                                <asp:TextBox ID="txtBillZipCode" CssClass="form-control" runat="server" TabIndex="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                             
                                                                            <td width="110" height="15">
                                                                                <asp:Label ID="lblPayPalType" runat="server" Text="PayPal Type"></asp:Label></td>
                                                                            <td width="122" height="15">
                                                                                <asp:Label ID="lblPaypalmethod" Font-Bold="true" runat="server" Text="Label"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            
                                                                            <td width="110" height="15">
                                                                            </td>
                                                                            <td width="122" height="15">
                                                                                <asp:Label ID="lblCCMessage1" runat="server" Visible="false" ForeColor="red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                               </div> 
                                                                </asp:Panel>
                                                                
                                                                   <asp:Panel ID="pnlgiftCard" Visible="false" runat="server">
                                                                        <table  id="table66" class="table table-bordered">
                                                                             
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Gift Card #
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:TextBox ID="txtgiftcardnumber" CssClass="form-control" runat="server"  ></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    PIN 
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:TextBox ID="txtgiftcardpin" CssClass="form-control"   runat="server"  ></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Approval Number 
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:TextBox ID="txtgiftcardaprovalnumber" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr  >
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td valign="middle" colspan="2" width="232" height="15">
                                                                                <br />    
                                                                                    <asp:Button ID="btngiftcardbal" CssClass="actionbutton" runat="server" Text="Gift Card Balance" />
                                                                                <br /><br /><br />
                                                                                 <b>  Balance Amount : $<asp:Label   ID="lblgiftcardbalance" SkinID="textEditorSkin101" runat="server" TabIndex="53"></asp:Label> </b>
                                                                                <br /><br />    
                                                                                </td>
                                                                            </tr>
                                                                            <tr  >
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                     
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                     <asp:Label   ID="lblgiftcardexpdate" Visible="false"  SkinID="textEditorSkin101" runat="server" TabIndex="53"></asp:Label>
                                                                                     <br />
                                                                                     <asp:Label   ID="lblgiftcardInlinenumber"  Visible="false"  runat="server" ></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                             <tr runat="server" visible="false" id="gifttr1">
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Other Payment By
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:DropDownList ID="drpOtherPaymentby" runat="server">
                                                                                    <asp:ListItem Text="--Select--" Value="" ></asp:ListItem>
                                                                                    <asp:ListItem Text="Cash" Value="Cash" ></asp:ListItem>
                                                                                    <asp:ListItem Text="Check" Value="Check" ></asp:ListItem>
                                                                                    <asp:ListItem Text="Credit Card" Value="Credit Card" ></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>

                                                                             <tr runat="server" visible="false" id="gifttr2">
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                    Other Amount
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                    <asp:TextBox ID="txtOtherAmount" SkinID="textEditorSkin101" runat="server"  ></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="15" height="15">
                                                                                </td>
                                                                                <td width="110" height="15">
                                                                                </td>
                                                                                <td width="122" height="15">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel> 

                                        </div>
                                        </Ajax:AjaxPanel> 
                                    <!-- END FORM-->
								</div>
							</div>

						</div>
					</div>
                    <!-- END PORTLET-->
					</div>
				</div>
               
               <div class="row">
					<div class="col-md-12">
                    <!-- BEGIN PORTLET-->
                  	<div class="action-link">
                         <Ajax:AjaxPanel ID="AjaxPanel25" runat="server">
                        <asp:Label ID="lblCCMessage" runat="server" Visible="false" ForeColor="red"></asp:Label>
                         <div id="autocompletePayment" ></div>
                          <asp:Label ID="lblCustomerStatus" runat="server" ForeColor="red"></asp:Label>
                        </Ajax:AjaxPanel> 
                     </div>
                    <!-- END PORTLET-->
					</div>
				</div>

                <div class="row">
					<div class="col-md-12">
                    <div class="text-center" style="width:100%;" >
                    <!-- BEGIN PORTLET-->
                    <Ajax:AjaxPanel ID="AjaxPanel26" runat="server">
                  	<div class="action-link"> 

                    <asp:Button CssClass="btn btn-success btn-lg" ID="BtnBookOrder" OnClientClick="Javascript:if (global_item_add == 1) {alert('Please check items searched but not added please complete process..'); return false;}return CheckValues();"     runat="server" Text="Book Order"   />

                     <asp:Button CssClass="btn btn-success btn-lg" ID="BtnPostOrder" Visible="false" runat="server"   Text="Post Order" /> 

                    <div style="display: none;">
                        <asp:Button ID="BtnEdit" CssClass="actionbutton" runat="server" Text="Duplicate"  />
                      </div>
                    
                    </div>
                    
                    </Ajax:AjaxPanel> 
                    <!-- END PORTLET-->
                    </div> 
					</div>
				</div>

                          
                        
                
    
      
     <Ajax:AjaxPanel ID="alertAjax" runat="server">
         
        <input type="text" id="alertText" runat="server" visible="False" />&nbsp;
        <input type="text" id="HidalertText" runat="server" onfocus="AlertDisplay()" visible="False" />
    
 <div  style="display:none;">
    
                 <asp:TextBox ID="txtprint"  runat="server"></asp:TextBox>
                 <asp:TextBox ID="txtActiveX"  runat="server"></asp:TextBox>

                <asp:TextBox ID="txtActiveXVersion"  runat="server"></asp:TextBox>  
                
                <div>
                
               
                 <asp:TextBox ID="txturlwt"  runat="server"></asp:TextBox>
                 <asp:TextBox ID="txtprinterwt"  runat="server" Width="120px"></asp:TextBox>
                 <asp:TextBox ID="txttraywt" runat="server" Width="87px"></asp:TextBox>
                 <asp:TextBox ID="txtpapersizewt" runat="server" Width="85px"></asp:TextBox></div>
                
                
                 <div>
                
                    
                     <asp:TextBox ID="txturlmc"  runat="server"></asp:TextBox>
                     <asp:TextBox ID="txtprintermc"  runat="server" Width="146px"></asp:TextBox>
                     <asp:TextBox ID="txttraymc" runat="server"></asp:TextBox>
                     <asp:TextBox ID="txtpapersizemc" runat="server"></asp:TextBox></div>
    
 <CR:CrystalReportViewer   ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </div>
<input type="hidden" name="HD_Ordernumber" id="HD_Ordernumber"   runat="server" value="" />



</Ajax:AjaxPanel>

    <script type="text/javascript" language="javascript">
        function AlertDisplay() {
            /*
            if (aspnetForm.ctl00$_mainContent$alertText.value!="")
            {
            alert(document.aspnetForm.ctl00$_mainContent$alertText.value);
            }
            */
            alert(document.aspnetForm.ctl00$_mainContent$alertText.value);

        }

        function AlertDisplay1() {
            document.aspnetForm.ctl00$_mainContent$HidalertText.focus();
        }


        function poptasticcomman(url, size) {
            var newwindow;
            newwindow = window.open(url, 'name3', size);

        }

        function printstart(url, printername, type) {
            //size='height=350,width=350';

            var txtActiveX = document.getElementById("<%=txtActiveX.ClientID%>").value;
            var txtActiveXVersion = document.getElementById("<%=txtActiveXVersion.ClientID%>").value;

            //alert("txtActiveX" +  txtActiveX); 

            if (txtActiveX == "AdvancedPDFPrinter") {

                //poptasticcomman("WTReport.aspx?"+ url,size);
                //var flag;
                //var Prn = new ActiveXObject("SunflowerActiveX.clsSunflowerActiveX");

                var AdvPDFPrinter = new ActiveXObject("AdvancedPDFPrinter.Printer");

                if (txtActiveXVersion == "3.1") {
                    AdvPDFPrinter.Key = "D9FE60D9252460EFBDF19F658EBE8EC857DD3600FBAEC8E8CE3363442564713A42A425049FC95213ECFF8B473168567602DC060"
                }
                else {
                    AdvPDFPrinter.Key = "D9DC538CDACAF9CDE8C217ED17278E26CEEEAF66FB9DD98E463327EE709BCAE79F2CCBC8CAFAF89B8A22DE128A4AED770379060"
                }


                var printtype = document.getElementById("<%=txtprint.ClientID%>").value;
                var objprinterwt = document.getElementById("<%=txtprinterwt.ClientID%>").value;
                var objtraywt = document.getElementById("<%=txttraywt.ClientID%>").value;
                var objpapersizewt = document.getElementById("<%=txtpapersizewt.ClientID%>").value;
                var objprintermc = document.getElementById("<%=txtprintermc.ClientID%>").value;
                var objtraymc = document.getElementById("<%=txttraymc.ClientID%>").value;
                var objpapersizemc = document.getElementById("<%=txtpapersizemc.ClientID%>").value;

                if (printtype == "ALL") {

                    var url;


                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlmc.ClientID%>").value;  // ''' ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlmc").value;
                    //alert(document.getElementById("txturl").value);
                    if (objprintermc != "") {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url, objprintermc);
                        //flag=Prn.PrintPDFFileFnc(url , "Card Message",objprintermc,objtraymc,objpapersizemc); 
                    }
                    else {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);
                        //flag=Prn.PrintPDFFileFnc(url);  
                    }

                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlwt.ClientID%>").value; //ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlwt").value;

                    //alert(document.getElementById("txturl").value);
                    if (objprinterwt != "") {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url, objprinterwt);
                        //flag=Prn.PrintPDFFileFnc(url , "Work Ticket",objprinterwt,objtraywt,objpapersizewt); 
                    }
                    else {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);
                        ///flag=Prn.PrintPDFFileFnc(url);
                    }

                }

                if (printtype == "MC") {
                    var url;
                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlmc.ClientID%>").value;  // ''' ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlmc").value;
                    //alert(document.getElementById("txturl").value);
                    if (objprintermc != "") {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url, objprintermc);
                        //flag=Prn.PrintPDFFileFnc(url , "Card Message",objprintermc,objtraymc,objpapersizemc); 
                    }
                    else {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);
                        //flag=Prn.PrintPDFFileFnc(url);  
                    }
                }

                if (printtype == "WT") {
                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlwt.ClientID%>").value; //ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlwt").value;

                    //alert(document.getElementById("txturl").value);
                    if (objprinterwt != "") {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url, objprinterwt);
                        //flag=Prn.PrintPDFFileFnc(url , "Work Ticket",objprinterwt,objtraywt,objpapersizewt); 
                    }
                    else {
                        var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);
                        //flag=Prn.PrintPDFFileFnc(url);
                    }
                }

            }
            else {

                //poptasticcomman("WTReport.aspx?"+ url,size);
                var flag;
                var Prn = new ActiveXObject("SunflowerActiveX.clsSunflowerActiveX");

                //var AdvPDFPrinter = new ActiveXObject("AdvancedPDFPrinter.Printer"); 
                //AdvPDFPrinter.Key="D9DC538CDACAF9CDE8C217ED17278E26CEEEAF66FB9DD98E463327EE709BCAE79F2CCBC8CAFAF89B8A22DE128A4AED770379060"


                var printtype = document.getElementById("<%=txtprint.ClientID%>").value;
                var objprinterwt = document.getElementById("<%=txtprinterwt.ClientID%>").value;
                var objtraywt = document.getElementById("<%=txttraywt.ClientID%>").value;
                var objpapersizewt = document.getElementById("<%=txtpapersizewt.ClientID%>").value;
                var objprintermc = document.getElementById("<%=txtprintermc.ClientID%>").value;
                var objtraymc = document.getElementById("<%=txttraymc.ClientID%>").value;
                var objpapersizemc = document.getElementById("<%=txtpapersizemc.ClientID%>").value;

                if (printtype == "ALL") {

                    var url;


                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlmc.ClientID%>").value;  // ''' ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlmc").value;
                    //alert(document.getElementById("txturl").value);
                    if (objprintermc != "") {
                        //   var retVal = AdvPDFPrinter.PrintRemotePDFFile(url,objprintermc);
                        flag = Prn.PrintPDFFileFnc(url, "Card Message", objprintermc, objtraymc, objpapersizemc);
                    }
                    else {
                        //      var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);                 
                        flag = Prn.PrintPDFFileFnc(url);
                    }

                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlwt.ClientID%>").value; //ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlwt").value;

                    //alert(document.getElementById("txturl").value);
                    if (objprinterwt != "") {
                        //var retVal = AdvPDFPrinter.PrintRemotePDFFile(url,objprinterwt); 			
                        flag = Prn.PrintPDFFileFnc(url, "Work Ticket", objprinterwt, objtraywt, objpapersizewt);
                    }
                    else {
                        //     var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);                                      
                        flag = Prn.PrintPDFFileFnc(url);
                    }

                }

                if (printtype == "MC") {
                    var url;
                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlmc.ClientID%>").value;  // ''' ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlmc").value;
                    //alert(document.getElementById("txturl").value);
                    if (objprintermc != "") {
                        //  var retVal = AdvPDFPrinter.PrintRemotePDFFile(url,objprintermc);
                        flag = Prn.PrintPDFFileFnc(url, "Card Message", objprintermc, objtraymc, objpapersizemc);
                    }
                    else {
                        //     var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);                   
                        flag = Prn.PrintPDFFileFnc(url);
                    }
                }

                if (printtype == "WT") {
                    url = "https://secure.quickflora.com/EnterprisePOS/images/" + document.getElementById("<%=txturlwt.ClientID%>").value; //ok
                    //url="http://localhost:81/EnterprisePOS/images/" + document.getElementById("txturlwt").value;

                    //alert(document.getElementById("txturl").value);
                    if (objprinterwt != "") {
                        //var retVal = AdvPDFPrinter.PrintRemotePDFFile(url,objprinterwt); 			
                        flag = Prn.PrintPDFFileFnc(url, "Work Ticket", objprinterwt, objtraywt, objpapersizewt);
                    }
                    else {
                        //   var retVal = AdvPDFPrinter.PrintRemotePDFFile(url);                                        
                        flag = Prn.PrintPDFFileFnc(url);
                    }
                }



            }

            window.location = "OrderEntryForm.aspx?redirect=y";

        }


        function drpPaymentTypechange() {

            //alert("Hello");
            var drp = document.getElementById("<%=drpPaymentType.ClientID%>");
            //alert("Hello 1");
            if (drp == "[object]") {
                var p1 = drp.selectedIndex;
                var strpayment = drp.options[p1].value;

                if (strpayment == "Credit Card") {
                    //alert("Hello 2");

                    //                   var field=window.document.getElementById("<%=txtCustomerFirstName.ClientID%>");
                    //      
                    //                  
                    //                     window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value="";
                    //    	             window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value="";
                    window.document.getElementById("<%=txtCard.ClientID%>").focus();
                    //}
                }
            }
        }

        function reswipe() {
            //window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value="";
            // window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value="";
            // window.document.getElementById("<%=txtCard.ClientID%>").value="";
            //alert("hi test");
            if (window.document.getElementById("<%=txtrawcard.ClientID%>") != null) {
                window.document.getElementById("<%=txtrawcard.ClientID%>").value = "";
                window.document.getElementById("<%=txtrawcard.ClientID%>").focus();
                calldoPollTime();
            }

        }


        function calldoPollTime() {
            var pollHand = setTimeout(calldoPollTime, 6000);
            if (window.document.getElementById("<%=txtrawcard.ClientID%>") != null) {
                if (window.document.getElementById("<%=txtrawcard.ClientID%>").value != "") {
                    //alert("hi");
                    call();
                }
            }

        }


        function call() {

            //alert("hi in call");
            var objtxtnum = window.document.getElementById("<%=txtCard.ClientID%>");
            var objtxtrawcard = window.document.getElementById("<%=txtrawcard.ClientID%>");
            var objintxtrawcard = window.document.getElementById("<%=intxtrawcard.ClientID%>");
            //var objtxtname=document.getElementById("txtname");
            //var objtxtdate=document.getElementById("txtdate");
            var objtxtfname = window.document.getElementById("<%=txtCustomerFirstName.ClientID%>");
            var objtxtlname = window.document.getElementById("<%=txtCustomerLastName.ClientID%>");

            if (objtxtrawcard.value != "") {
                //alert(objtxtcard.value);
                var card = objtxtrawcard.value;
                objintxtrawcard.value = objtxtrawcard.value;
                var mytool_array = card.split("^");
                //alert(mytool_array[0]+"\n"+mytool_array[1]+"\n"+mytool_array[2]);
                if (mytool_array.length != 3) {
                    reswipe();
                    return;
                }
                var cardnumber = mytool_array[0];
                var cardholdername = mytool_array[1];
                var carddate = mytool_array[2];

                var start;
                var stop;
                start = 2;
                stop = cardnumber.length;
                cardnumber = cardnumber.substring(start, stop);
                //alert(cardnumber);
                objtxtnum.value = cardnumber;

                //stop=cardholdername.length;
                cardholdername = Trim(cardholdername);
               // alert(cardholdername);

                var r = confirm("Do you want to update Customer Name.");
                if (r == true) {
                    var chk;
                    chk = cardholdername.indexOf('/');
                    //alert(chk);
                    if (chk == -1) {

                    }
                    else {
                        var ln = cardholdername.length;
                        //alert(ln);
                        if (ln == (chk + 1)) {
                            cardholdername = cardholdername.substring(0, chk);
                            var myspace_array = cardholdername.split(" ");
                            if (myspace_array.length != 0) {

                                objtxtlname.value = myspace_array[myspace_array.length - 1];

                                var i;
                                objtxtfname.value = "";
                                for (i = 0; i < myspace_array.length - 1; i++) {

                                    if (i == 0) {
                                        objtxtfname.value = objtxtfname.value + myspace_array[i];
                                    }
                                    else {
                                        objtxtfname.value = objtxtfname.value + " " + myspace_array[i];
                                    }

                                }

                            }

                        }
                        else {
                            var myslash_array = cardholdername.split("/");
                            if (myslash_array.length == 2) {
                                objtxtfname.value = myslash_array[1];
                                objtxtlname.value = myslash_array[0];
                            }


                        }
                    }
                } else {
                    //x = "You pressed Cancel!";
                }
                
               



                var mm, yy;
                stop = 4
                carddate = carddate.substring(0, stop);
                yy = carddate.substring(0, 2);
                mm = carddate.substring(2, 4);
                var dt = yy + "/" + mm;

                var dte = "";
                dte = mm + "/20" + yy;

                window.document.getElementById("<%=drpExpirationDate.ClientID%>").value = dte;

                ShowCardDataJs(cardnumber, dt);
                reswipe();
            }

        }

        function Trim(s) {
            // Remove leading spaces and carriage returns
            while ((s.substring(0, 1) == ' ') || (s.substring(0, 1) == '\n') || (s.substring(0, 1) == '\r'))
            { s = s.substring(1, s.length); }

            // Remove trailing spaces and carriage returns
            while ((s.substring(s.length - 1, s.length) == ' ') || (s.substring(s.length - 1, s.length) == '\n') || (s.substring(s.length - 1, s.length) == '\r'))
            { s = s.substring(0, s.length - 1); }

            return s;
        }


        function ShowCardDataJs(num, dte) {

            var cc = "";
            cc = num;
            var cclen = 0;
            cclen = cc.length;

            if (cc.substring(0, 1) == "4") {
                window.document.getElementById("<%=drpCardType.ClientID%>").value = "Visa";
            }

            if (cc.substring(0, 1) == "5") {
                window.document.getElementById("<%=drpCardType.ClientID%>").value = "Master";
            }
            if (cc.substring(0, 1) == "3") {
                window.document.getElementById("<%=drpCardType.ClientID%>").value = "Amex";
            }



        }
    
    
    
    </script>
<script type="text/vbscript"  language="VBSCRIPT">
   
   sub ShowCardData(num,dte)

    
'    	name = msr.Firstname
'    	trim(name)
'    	if len(msr.MiddleInitial) > 0 then name = name & " " & msr.MiddleInitial
'    	trim(name)
'    	name = name + " " + msr.Surname
'    	trim(name)
'    	
'    	window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value=msr.Firstname
'    	window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value=msr.Surname
'    	window.document.getElementById("<%=txtCard.ClientID%>").value=msr.AccountNumber
    	dim Number
        Number=num
    	Dim NumberLength  
        NumberLength = Len(Number)
        'alert(NumberLength)
        '4 Visa
        '5 Master
        '3 A
        if  Left(Number, 1)="4" then
            'alert("Visa")
             window.document.getElementById("<%=drpCardType.ClientID%>").value="Visa"
        end if
        if  Left(Number, 1)="5" then
            'alert("Master")
            window.document.getElementById("<%=drpCardType.ClientID%>").value="Master"
        end if
        if  Left(Number, 1)="3" then
            'alert("Amex")
            window.document.getElementById("<%=drpCardType.ClientID%>").value="Amex"
        end if
        
'    	2010 and 2008
'        o4 and 1
    	dim str 
        str=dte
        dim m,y
        y=Left(str,2)
        m=Right(str,2)
        dim dt
        dt=m + "/20" + y
        
        y="20" + Y
        dim st
        st=date
        dim nst
        nst=split(st,"/",-1)
'        alert("year from card " + y )
'        alert("year today " + nst(2) )
'        
'        alert("Month from card " + m )
'        alert("Month today " + nst(0) )
        
        if Cint(y)<Cint(nst(2)) then
            alert("Credit Card Year Used is Expired Please Use another One")
        window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value=""
    	window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value=""
    	window.document.getElementById("<%=txtCard.ClientID%>").value=""
    	window.document.getElementById("<%=drpCardType.ClientID%>").SelectedIndex=0
    	window.document.getElementById("<%=drpExpirationDate.ClientID%>").SelectedIndex=0
    	
        else
            if Cint(y)=Cint(nst(2)) then
                if Cint(m)<Cint(nst(0)) then
                alert("Credit Card Month Used is Expired Please Use another One")
                  window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value=""
    	            window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value=""
    	            window.document.getElementById("<%=txtCard.ClientID%>").value=""
    	            window.document.getElementById("<%=drpCardType.ClientID%>").SelectedIndex=0
    	            window.document.getElementById("<%=drpExpirationDate.ClientID%>").SelectedIndex=0
                else
                 window.document.getElementById("<%=drpExpirationDate.ClientID%>").value=dt    
                 
                end if
            else
                window.document.getElementById("<%=drpExpirationDate.ClientID%>").value=dt    
            end if
            
        end if
        
       
		
end sub


  '----------Code for the Card reader IBM--------Start-------------*/
 
 UseCommonControls = 1

 Sub OpenClaimEnable_Click()
 
Dim rc

	if MSRCommon.deviceenabled = true then
		rc = MSRCommon.clearinput()
		'showRC "ClearInput",rc
	end if

 rc = MSRCommon.Close()

    window.document.getElementById("<%=Track1Data.ClientID%>").value = ""
    window.document.getElementById("<%=Track2Data.ClientID%>").value = ""
    window.document.getElementById("<%=Track3Data.ClientID%>").value = ""
    'window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value=""
    'window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value=""
    'window.document.getElementById("<%=txtCard.ClientID%>").value=""
    'window.document.getElementById("<%=drpCardType.ClientID%>").SelectedIndex=0
    'window.document.getElementById("<%=drpExpirationDate.ClientID%>").SelectedIndex=0    	
 
MSRCommon.DataEventEnabled = 1

EnableMSR(MSRCommon)

End Sub


sub EnableMSR(msr)
      
    Dim str
    str=""
    orc = msrcommon.Open("MAGSWIPE_USBKB")
 	'showrc "Open()", orc
 	
 	str= str & "Open()" & orc & vbCrLf 
 		 	
 	if orc = 0 then
  		crc = msrcommon.ClaimDevice(1000)
 		'showrc "Claim()", orc
 		str= str & "Claim()" & orc & vbCrLf 
 	end if

 	if msrcommon.claimed = true then
		msr.DeviceEnabled = 1
 	end if
 	'addResult("DeviceEnabled = " & msrcommon.DeviceEnabled)
    
    str= str & "DeviceEnabled = " & msrcommon.DeviceEnabled & vbCrLf 

	if msrcommon.deviceenabled = true then
		msr.DataEventEnabled = 1
	end if
 	'addResult("DataEventEnabled = " & msrcommon.DataEventEnabled)
    str= str & "DataEventEnabled = " & msrcommon.DataEventEnabled & vbCrLf 
    
    'alert(str)
    
   	if msrcommon.deviceenabled = true then
		dim tracks
       	tracks = 0
       	tracks = tracks + 1
		tracks = tracks + 2
		tracks = tracks + 4
		tracks = tracks + 8
		
	 	msrcommon.TracksToRead = tracks
  		'alert("Tracks to read = " & msrcommon.TracksToRead )
   	end if
    
end sub

Sub MSRCommon_DataEvent(Status)
    'alert("Data Event Received")
	ShowCardDataXM95()
End Sub

sub ShowCardDataXM95()
       
        'alert("Track1Data=" & MSRCommon.Track1Data)
        'alert("Track2Data=" & MSRCommon.Track2Data)
        'alert("Track3Data=" & MSRCommon.Track3Data)

        window.document.getElementById("<%=Track1Data.ClientID%>").value = MSRCommon.Track1Data
        window.document.getElementById("<%=Track2Data.ClientID%>").value = MSRCommon.Track2Data
        window.document.getElementById("<%=Track3Data.ClientID%>").value = MSRCommon.Track3Data


        'alert("Firstname=" & MSRCommon.Firstname)
        'alert("MiddleInitial=" & MSRCommon.MiddleInitial)
        'alert("MiddleInitial=" & MSRCommon.MiddleInitial)
        'alert("Surname=" & MSRCommon.Surname)
        'alert("AccountNumber=" & MSRCommon.AccountNumber)
        
        
    	name = MSRCommon.Firstname
    	trim(name)
    	if len(MSRCommon.MiddleInitial) > 0 then name = name & " " & MSRCommon.MiddleInitial
    	trim(name)
    	name = name + " " + MSRCommon.Surname
    	trim(name)
    	
    	window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value=MSRCommon.Firstname
    	window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value=MSRCommon.Surname
    	window.document.getElementById("<%=txtCard.ClientID%>").value=MSRCommon.AccountNumber
    	 dim Number
        Number=MSRCommon.AccountNumber
    	Dim NumberLength  
        NumberLength = Len(Number)
        'alert(NumberLength)
        '4 Visa
        '5 Master
        '3 A
        if  Left(Number, 1)="4" then
            'alert("Visa")
             window.document.getElementById("<%=drpCardType.ClientID%>").value="Visa"
        end if
        if  Left(Number, 1)="5" then
            'alert("Master")
            window.document.getElementById("<%=drpCardType.ClientID%>").value="Master"
        end if
        if  Left(Number, 1)="3" then
            'alert("Amex")
            window.document.getElementById("<%=drpCardType.ClientID%>").value="Amex"
        end if
        
'    	2010 and 2008
'        o4 and 1
    	dim str 
        str=MSRCommon.ExpirationDate
        dim m,y
        y=Left(str,2)
        m=Right(str,2)
        dim dt
        dt=m + "/20" + y
        
        y="20" + Y
        dim st
        st=date
        dim nst
        nst=split(st,"/",-1)
        'alert("year from card " + y )
        'alert("year today " + nst(2) )
        'alert("Month from card " + m )
        'alert("Month today " + nst(0) )
        
        if Cint(y)<Cint(nst(2)) then
            alert("Credit Card Year Used is Expired Please Use another One")
        window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value=""
    	window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value=""
    	window.document.getElementById("<%=txtCard.ClientID%>").value=""
    	window.document.getElementById("<%=drpCardType.ClientID%>").SelectedIndex=0
    	window.document.getElementById("<%=drpExpirationDate.ClientID%>").SelectedIndex=0
    	
        else
            if Cint(y)=Cint(nst(2)) then
                if Cint(m)<Cint(nst(0)) then
                alert("Credit Card Month Used is Expired Please Use another One")
                  window.document.getElementById("<%=txtCustomerFirstName.ClientID%>").value=""
    	            window.document.getElementById("<%=txtCustomerLastName.ClientID%>").value=""
    	            window.document.getElementById("<%=txtCard.ClientID%>").value=""
    	            window.document.getElementById("<%=drpCardType.ClientID%>").SelectedIndex=0
    	            window.document.getElementById("<%=drpExpirationDate.ClientID%>").SelectedIndex=0
                else
                 window.document.getElementById("<%=drpExpirationDate.ClientID%>").value=dt    
                 
                end if
            else
                window.document.getElementById("<%=drpExpirationDate.ClientID%>").value=dt    
            end if
            
        end if
        
        'alert(dt)
    	'DataEventEnabled_Click()
    	
    	
    	
    	'window.document.getElementById("<%=drpPaymentType.ClientID%>").value=""
    	'addResult("Name: " & name & chr(13) + chr(10) & _
		'			"Acct: " & msr.AccountNumber & chr(13) + chr(10) & _
		'			"Expr: " & msr.ExpirationDate)

		
		
		Close_Click()
end sub




Sub Close_Click()
    Dim rc
        rc = MSRCommon.Close()
    'alert("Close()" & rc)
End Sub

Sub showRC(msg, rc)
	addResult(msg & " = " & rc)
End Sub


Sub MSRCommon_ErrorEvent(ResultCode, ResultCodeExtended, ErrorLocus, pErrorResponse)
    addResult ("Error Event Received" & chr(13) & chr(10) & _
    			  "    Result Code:     " & ResultCode & chr(13) & chr(10) & _
				  "    Result Code Ext: " & resultcodetextended & chr(13) & chr(10))

End Sub

 

Sub addResult(msg)
'	t = frmMain.Results.value
'	t = t + msg  & chr(13)
	'frmMain.Results.value = t
End Sub

 

Sub Properties_Click()
    Dim rc
    Dim msr
    
    If (UseCommonControls) Then
        Set msr = MSRCommon
    Else
        Set msr = MSRIBM
    End If

	addResult("*********** Properties and Capabilities ***********")
	addResult("ControlObjectDescription: " & msr.ControlObjectDescription)
	addResult("ControlObjectVersion: " & msr.ControlObjectVersion)
	addResult("ServiceObjectDescription: " & msr.ServiceObjectDescription)
	addResult("ServiceObjectVersion: " & msr.ServiceObjectVersion)
	addResult("DeviceDescription: " & msr.DeviceDescription)
	addResult("DeviceName: " & msr.DeviceName)
	addResult("AutoDisable: " & msr.AutoDisable)
	addResult("BinaryConversion: " & msr.BinaryConversion)
	addResult("CapPowerReporting: " & msr.CapPowerReporting)
	addResult("Claimed: " & msr.Claimed)
	addResult("DataCount: " & msr.DataCount)
	addResult("DataEventEnabled: " & msr.DataEventEnabled)
	addResult("DeviceEnabled: " & msr.DeviceEnabled)
	addResult("FreezeEvents: " & msr.FreezeEvents)
	addResult("OpenResult: " & msr.OpenResult)
	addResult("PowerNotify: " & msr.PowerNotify)
	addResult("PowerState: " & msr.PowerState)
	addResult("ResultCode: " & msr.ResultCode)
	addResult("ResultCodeExtended: " & msr.ResultCodeExtended)
	addResult("State: " & msr.State)
	addResult("CapISO: " & msr.CapISO)
	addResult("CapJISOne: " & msr.CapJISOne)
	addResult("CapJISTwo: " & msr.CapJISTwo)
	addResult("CapTransmitSentinels: " & msr.CapTransmitSentinels)
	addResult("TracksToRead: " & msr.TracksToRead)
	addResult("DecodeData: " & msr.DecodeData)
	addResult("ParseDecodeData: " & msr.ParseDecodeData)
	addResult("ErrorReportingType: " & msr.ErrorReportingType)
	addResult("Track1Data: " & msr.Track1Data)
	addResult("Track2Data: " & msr.Track2Data)
	addResult("Track3Data: " & msr.Track3Data)
	addResult("Track4Data: " & msr.Track4Data)
	addResult("AccountNumber: " & msr.AccountNumber)
	addResult("ExpirationDate: " & msr.ExpirationDate)
	addResult("Title: " & msr.Title)
	addResult("FirstName: " & msr.FirstName)
	addResult("MiddleInitial: " & msr.MiddleInitial)
	addResult("Surname: " & msr.Surname)
	addResult("Suffix: " & msr.Suffix)
	addResult("ServiceCode: " & msr.ServiceCode)
	addResult("Track1DiscretionaryData: " & msr.Track1DiscretionaryData)
	addResult("Track2DiscretionaryData: " & msr.Track2DiscretionaryData)
	addResult("TransmitSentinels: " & msr.TransmitSentinels)
	addResult("***************************************************")
End Sub
'----------Code for the Card reader IBM--------Start-------------*/

    </script>
  <%--Added by imtiyaz for card reader --%>




  <script type="text/javascript">


      var reqPayment;

      function InitializePayment() {
          try {
              reqPayment = new ActiveXObject("Msxml2.XMLHTTP");
          }
          catch (e) {
              try {
                  reqPayment = new ActiveXObject("Microsoft.XMLHTTP");
              }
              catch (oc) {
                  reqPayment = null;
              }
          }

          if (!reqPayment && typeof XMLHttpRequest != "undefined") {
              reqPayment = new XMLHttpRequest();
          }

      }

      function SendQueryPayment(key) {
          InitializePayment();
          var start = 0;
          start = new Date();
          start = start.getTime();
          var url = "AjaxOrderPaymentStatus.aspx?k=" + key + '&start=' + start;
          //alert(url);
          if (reqPayment != null) {
              reqPayment.onreadystatechange = ProcessPayment;
              reqPayment.open("GET", url, true);
              reqPayment.send(null);

          }

      }



      function ProcessPayment() {

          //alert('In Payment process Main');
          if (reqPayment.readyState == 4) {

              //alert('In Payment process Main 4');
              // only if "OK"
              if (reqPayment.status == 200) {

                  //alert('In Payment process Main 200');

                  //alert(reqPayment.responseText);
                  //alert(document.getElementById("ctl00_ContentPlaceHolder_txtCheck"));  
                  if (trim(reqPayment.responseText) == "") {
                      // alert("in blank");  
                      //HideDiv("autocomplete");
                      document.getElementById("ctl00_ContentPlaceHolder_txtCheck").value = "";

                      var newdiv = document.createElement("span");
                      newdiv.innerHTML = "<span style='text-align:center;font-size:18px; color:Red;' >..  </span>";
                      var container = document.getElementById("autocompletePayment");
                      container.appendChild(newdiv)
                      //calldoPollTimePayment();
                      var pollHand = setTimeout(calldoPollTimePayment, 10000);
                  }
                  else {

                      //alert("in not blank");  

                      document.getElementById("ctl00_ContentPlaceHolder_txtCheck").value = reqPayment.responseText;
                      var container = document.getElementById("autocompletePayment");



                      if (window.document.getElementById("<%=txtCheck.ClientID%>").value == "Approved") {
                          //alert("Approved");
                          container.innerHTML = "<div style='text-align:center;font-size:18px; color:Green;' >Payment Process Result " + reqPayment.responseText + " </div>";
                          window.document.getElementById("<%=BtnBookOrder.ClientID%>").focus();
                          window.document.getElementById("<%=BtnBookOrder.ClientID%>").click();
                          //BtnBookOrder

                      }
                      else {
                          container.innerHTML = "<div style='text-align:center;font-size:18px; color:Red;' >Payment Process Result " + reqPayment.responseText + " </div>";
                          var pollHand = setTimeout(calldoPollTimePayment, 10000);
                      }

                  }
              }
              else {
                  document.getElementById("autocompletePayment").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;

              }
          }
      }



      function calldoPollTimePayment() {
          //alert('In Payment process');
          var container = document.getElementById("autocompletePayment");
          container.innerHTML = "<br><div style='text-align:center;font-size:18px; color:Red;' >your Payment request processing.......</div>"; ;

          if (window.document.getElementById("<%=txtCheck.ClientID%>") != null) {
              if (window.document.getElementById("<%=txtCheck.ClientID%>").value != "Approved") {
                  //alert(window.document.getElementById("<%=HD_Ordernumber.ClientID%>").value);
                  SendQueryPayment(window.document.getElementById("<%=HD_Ordernumber.ClientID%>").value);
              }


          }

      }
       

</script>


 


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


<script type="text/javascript" src="assets/scripts/table-editable.js"></script>

<script type="text/javascript" >
    jQuery(document).ready(function () {
        
        TableEditable.init();
        Search.init();
    });
</script>

</asp:Content>

