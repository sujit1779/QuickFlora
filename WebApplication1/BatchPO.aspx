<%@ Page Title="" Language="VB" MasterPageFile="~/MainMasterBatchPO.master" AutoEventWireup="false" ValidateRequest="false"
    CodeFile="BatchPO.aspx.vb" Inherits="BatchPO" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    <link rel="stylesheet" type="text/css" href="assets/components.css" />

<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>
  

        <style>
        li.c-active > span, .c-content-pagination.c-theme > li.c-active > a {
            border-color: #cf4647;
            background: #cf4647;
            color: #fff;
        }
    </style>

        <style>
        .autocompletenew {
            overflow-x: hidden;
            overflow-y: auto;
            height: 200px;
            position: absolute;
            z-index: 2;
            width: 200px;
            background-color: #fff;
            box-shadow: 5px 5px rgba(102, 102, 102, 0.1);
            border: 1px solid #40BD24;
            visibility: hidden;
        }




    </style>


<style type="text/css">
 
 th {
    background-color: #678b38;
    color:#fff;
}
 
 th a {
    background-color: #678b38;
    color:#fff;
}
 </style> 

 
 

<!--Script by hscripts.com-->
<script type="text/javascript">
    checked = false;
    function checkedAll(frm1) {
        var aa = document.getElementById('aspnetForm');
        if (checked == false) {
            checked = true
        }
        else {
            checked = false
        }

        for (var i = 0; i < aa.elements.length; i++) {
            // alert(aa.elements[i].id);
            if (aa.elements[i].id != 'ctl00_ContentPlaceHolder_chkWithother' && aa.elements[i].id != 'ctl00_ContentPlaceHolder_chkNotavailable' && aa.elements[i].id != 'ctl00_ContentPlaceHolder_chkonlyStanding' && aa.elements[i].id != 'ctl00_ContentPlaceHolder_chkincludeStanding' && aa.elements[i].id != 'ctl00_ContentPlaceHolder_chkincludeBought') {
                aa.elements[i].checked = checked;
            }


        }
    }
 
</script>
<!-- Script by hscripts.com -->



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">
         Batch PO &nbsp;&nbsp;&nbsp;&nbsp;<img id="loading"  style="max-width:30px;border:0px;" src="loading.gif" alt="Loading..." />
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
     
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

         function SetStatusColor(status, RowNumber) {
             if (status == "Pending-Stg email") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ffffff";
             }

             if (status == "Do Not Touch") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ccccff";
             }

             if (status == "Wed") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ffcccc";
             }

             if (status == "Not Avail") {
                 document.getElementById(RowNumber).style.backgroundColor = "#cccccc";
                 var bb = document.getElementById('ctl00_ContentPlaceHolder_chkNotavailable');
                 if (bb.checked == false)
                     document.getElementById(RowNumber).style.display = "none";
             }
             if (status == "With-Other") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ffffff";
                 var aa = document.getElementById('ctl00_ContentPlaceHolder_chkWithother');
                 if (aa.checked == false)
                     document.getElementById(RowNumber).style.display = "none";
             }

             if (status == "Pending") {
                 document.getElementById(RowNumber).style.backgroundColor = "#99ff99";
             }

             if (status == "Bought") {
                 document.getElementById(RowNumber).style.backgroundColor = "#99ffff";
             }

             if (status == "In Process") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ccffff";
             }

             if (status == "No Action") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ffff99";
             }
             if (status == "Pending Auction") {
                 document.getElementById(RowNumber).style.backgroundColor = "#99ff99";
             }

             if (status == "Pending-Stg") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ccffcc";
             }
             if (status == "Wed In Process") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ffcccc";
             }
             if (status == "Bought Wed") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ffcccc";
             }
             if (status == "Pending Email") {
                 document.getElementById(RowNumber).style.backgroundColor = "#ffffff";
             }

         }

         function Saveitem(This, RowNumber, name, oldvalue, Statusid) {
             Initialize();

             This.style.backgroundColor = "";
             var start = 0;
             start = new Date();
             start = start.getTime();


             var url = "AjaxPOSave.aspx?RowNumber=" + RowNumber + "&value=" + This.value + "&name=" + name + '&start=' + start;

             if (name == "Status") {

                 SetStatusColor(This.value, RowNumber);
             }




             //alert(This.value);
             //alert(oldvalue);

             if (This.value == oldvalue) {
                 return;
             }

             document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
             document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

             $.get(url, function(data, status) {
                 //alert("Data: " + data + "\nStatus: " + status);
                 document.getElementById("autosave").style.backgroundImage = 'url()';
             });

             // alert(url);

//             if (req != null) {
//                 req.onreadystatechange = ProcessSave;
//                 req.open("GET", url, true);
//                 req.send(null);
//             }

             if (name != "Status") {
                 var DefaultBuyStatus;
                 DefaultBuyStatus = document.getElementById('<%=drpDefaultBuyStatus.ClientID%>').value;
                 var _BuyStatus;
                 _BuyStatus = document.getElementById(Statusid).value;
                 //alert(DefaultBuyStatus);
                 //alert(_BuyStatus);
                 if (DefaultBuyStatus != "" && _BuyStatus != "Bought" && _BuyStatus != "Pending Email") {
                     var dd = document.getElementById(Statusid)
                     var aryOptions = dd.getElementsByTagName('option');
                     var cpt = 0;
                     var indexValue = false;
                     for (cpt = 0; cpt < aryOptions.length; cpt++) {
                         if (aryOptions[cpt].textContent == DefaultBuyStatus) {
                             indexValue = cpt;
                         }
                     }
                     dd.selectedIndex = indexValue;
                     Saveitemnew(Statusid, RowNumber, "Status", 0);
                 }


             }


         }

         function myFocusFunction(This) {
             This.style.backgroundColor = "yellow";
             if (This.value == "0") {
                 This.value = "";
             }
             value_on_click = This.value;
         }

         function myonblurFunction(This) {
             This.style.backgroundColor = "";
         }

         function myFocusFunctiontotal(This, id1, id2, id3) {

             var Qty = 0.00;
             var Pack = 0.00;
             var Cost = 0.00;

             Qty = document.getElementById(id1).value;
             Pack = document.getElementById(id2).value;
             Cost = document.getElementById(id3).value;

             This.value = (Qty * Pack * Cost).toFixed(2);
             This.style.backgroundColor = "yellow";
         }

         function Saveitemnew(id0, RowNumber, name, oldvalue) {
             //Initialize();

             document.getElementById(id0).style.backgroundColor = "";

             var start = 0;
             start = new Date();
             start = start.getTime();


             var url = "AjaxPOSave.aspx?RowNumber=" + RowNumber + "&value=" + document.getElementById(id0).value + "&name=" + name + '&start=' + start;
             //alert(url);

             if (name == "Status") {
                 SetStatusColor(document.getElementById(id0).value, RowNumber);
             }

             document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
             document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

             $.get(url, function(data, status) {
                 //alert("Data: " + data + "\nStatus: " + status);
                 document.getElementById("autosave").style.backgroundImage = 'url()';
             });

//             if (req != null) {
//                 req.onreadystatechange = ProcessSave;
//                 req.open("GET", url, true);
//                 req.send(null);


//             }

         }

         var value_on_click = "";

         function myFocusFunctionNew(This) {
             This.style.backgroundColor = "orange";
             if (This.value == "0") {
                 This.value = "";
             }
             value_on_click = This.value;
         }

         function myFocusFunctiontotalnew(This, Old_value, id0, RowNumber, id1, id2, id3, Statusid) {

             //alert(RowNumber);

             var Qty = 0.00;
             var Pack = 0.00;
             var Cost = 0.00;

            // alert("-" + document.getElementById(id1).value + "-");

             document.getElementById(id1).value = $.trim(document.getElementById(id1).value);
             document.getElementById(id2).value = $.trim(document.getElementById(id2).value);
             document.getElementById(id3).value = $.trim(document.getElementById(id3).value);

            // alert("-" + document.getElementById(id1).value + "-");

             Qty = document.getElementById(id1).value;
             //alert(Qty);
             Pack = document.getElementById(id2).value;
             //alert(Pack);
             Cost = document.getElementById(id3).value;
             //alert(Cost);
             // alert(document.getElementById(id0).value);

             if ($.isNumeric(Qty))
             { }
             else {
                // alert('Please enter a numeric value for Qty for this row.');
//                 document.getElementById(id1).focus();
//                 document.getElementById(id1).click();
//                 document.getElementById(id1).style.backgroundColor = "#ff0000";
//                 Qty = 0;
//                 return 0;
             }
             if ($.isNumeric(Pack))
             { }
             else {
                 //alert('Please enter a numeric value for Pack for this row.');
//                 document.getElementById(id2).focus();
//                 document.getElementById(id2).click();
//                 document.getElementById(id2).style.backgroundColor = "#ff0000";
//                 Pack = 0;
//                 return 0;
             }
             if ($.isNumeric(Cost))
             { }
             else {
                 //alert('Please enter a numeric value for Cost for this row.');
//                 document.getElementById(id3).focus();
//                 document.getElementById(id3).click();
//                 document.getElementById(id3).style.backgroundColor = "#ff0000";
//                 Cost = 0;
//                 return 0;
             }

             document.getElementById(id0).value = (Qty * Pack * Cost).toFixed(2);
             // Saveitemnew(id0, RowNumber, "Ext_COSt", 0);
             
             
//             Saveitemnew(id1, RowNumber, "Q_ORD", 0);
//             Saveitemnew(id2, RowNumber, "PACK", 0);
//             Saveitemnew(id3, RowNumber, "COST", 0);

             if (This.id == id1) {
                 if (Old_value != document.getElementById(id1).value && value_on_click != document.getElementById(id1).value) {
                     Saveitemnew(id1, RowNumber, "Q_ORD", 0);
                     
                 }
             }

             if (This.id == id2) {
                 if (Old_value != document.getElementById(id2).value && value_on_click != document.getElementById(id2).value) {
                     Saveitemnew(id2, RowNumber, "PACK", 0);
                 }

             }


             if (This.id == id3) {
                 if (Old_value != document.getElementById(id3).value && value_on_click != document.getElementById(id3).value) {
                     Saveitemnew(id3, RowNumber, "COST", 0);
                     
                 }

             }
             
             //alert(document.getElementById(id0).value);
             //alert(id0.value);
 

             var DefaultBuyStatus;
             DefaultBuyStatus = document.getElementById('<%=drpDefaultBuyStatus.ClientID%>').value;

            var _BuyStatus;
             _BuyStatus = document.getElementById(Statusid).value;
             //alert(DefaultBuyStatus);
             //alert(_BuyStatus);

               if (DefaultBuyStatus != "" && _BuyStatus != "Bought" && _BuyStatus != "Pending Email") {
                 var dd = document.getElementById(Statusid)
                 var aryOptions = dd.getElementsByTagName('option');
                 var cpt = 0;
                 var indexValue = false;
                 for (cpt = 0; cpt < aryOptions.length; cpt++) {
                     if (aryOptions[cpt].textContent == DefaultBuyStatus) {
                         indexValue = cpt;
                     }
                 }
                 dd.selectedIndex = indexValue;
                 Saveitemnew(Statusid, RowNumber, "Status", 0);
             }


             This.style.backgroundColor = "";
         }



         function ProcessSave() {

             // alert(document.getElementById("autosave"));


             document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
             document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';


             if (req.readyState == 4) {
                 // only if "OK"
                 // alert(req.readyState);

                 if (req.status == 200) {
                     document.getElementById("autosave").style.backgroundImage = 'url()';
                     //  alert(req.status);

                     if (req.responseText == "") {


                     }
                     else {
                         //alert(req.responseText);
                         //  document.getElementById("autosave").innerHTML = "Data Saved For RowNumber:" + req.responseText;
                     }
                 }
                 else {
                     document.getElementById("autosave").innerHTML = "There was a problem saving data:" + req.responseText;

                 }
             }
         }



         function SendQuery(key) {
             //alert('hi');

             var str = key;
             var n = str.length;
             if (n < 2) {
                 return 0;
             }
             Initialize();

             var start = 0;
             start = new Date();
             start = start.getTime();

             var url = "AjaxPOBSearch.aspx?k=" + key + '&start=' + start;
             // alert(url);

             stop = 1;

             if (req != null) {
                 req.onreadystatechange = ProcessSearch;
                 req.open("GET", url, true);
                 req.send(null);

             }

         }


         function ProcessSearch() {

             if (req.readyState == 4) {
                 if (req.status == 200) {

                     //alert("Items saved Inline Number =" + req.responseText);
                     ShowDiv("autocomplete");

                     var newdiv = document.createElement("div");
                     newdiv.innerHTML = req.responseText;
                     var container = document.getElementById("autocomplete");
                     container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs' href='javascript:CcustomersearchcloseProcess();' >Close Search result</a></div>";
                     container.appendChild(newdiv)

                 }
                 else {
                     alert("There was a problem saving data:<br>" + req.statusText);
                 }
             }

         }

         function CcustomersearchcloseProcess() {
             stop = 0;
             document.getElementById("autocomplete").innerHTML = "";
             document.getElementById("<%=txtOrderSearch.ClientID%>").value = "";
             HideDiv("autocomplete");


         }

         function FillSearchtextBox2(val) {
             // alert(val);
             document.getElementById("<%=txtOrderSearch.ClientID%>").value = val;
             document.getElementById("autocomplete").innerHTML = "";
             HideDiv("autocomplete");

             document.getElementById("<%=btnOrderSearch.ClientID%>").focus();
             document.getElementById("<%=btnOrderSearch.ClientID%>").click();
             //ImgUpdateSearchitems
         }


         function SendQuerynew(key, This, RowNumber) {
             //alert(This.id);
             //alert(RowNumber);

             var str = key;
             var n = str.length;
             if (n < 2) {
                 return 0;
             }
             Initialize();

             var start = 0;
             start = new Date();
             start = start.getTime();


             var url = "AjaxVendorSearchNew.aspx?k=" + key + "&id=" + This.id + "&RowNumber=" + RowNumber + '&start=' + start;
             //alert(url);

             stop = 1;

             if (req != null) {
                 req.onreadystatechange = function() {
                     ProcessSearchnew(RowNumber);
                 };
                 req.open("GET", url, true);
                 req.send(null);

             }

         }

         function ProcessSearchnew(RowNumber) {
             //alert("In ProcessSearch");
             //alert(RowNumber);

             if (req.readyState == 4) {
                 if (req.status == 200) {

                     //alert("Result : " +  req.responseText);
                     var id;
                     id = "autocomplete" + RowNumber;
                     ShowDiv(id);

                     var newdiv = document.createElement("div");
                     newdiv.innerHTML = req.responseText;
                     //alert(newdiv.innerHTML);
                     var container = document.getElementById(id);
                     var newid = "";
                     newid = "'" + id + "'";
                     container.innerHTML = '<div style="text-align:center" ><a class="btn btn-danger btn-xs" href="Javascript:CcustomersearchcloseProcessnew(' + newid + ');" >Close</a></div>';
                     container.appendChild(newdiv)

                 }
                 else {
                     alert("There was a problem saving data:<br>" + req.statusText);
                 }
             }

         }

         function CcustomersearchcloseProcessnew(id) {
             //alert(CcustomersearchcloseProcess);
             stop = 0;
             document.getElementById(id).innerHTML = "";
             //document.getElementById("txtOrderSearch.ClientID").value = "";
             HideDiv(id);


         }

         function FillSearchtextBox(val, ID, DIVID) {
             // alert(val);
             document.getElementById(ID).value = val;
             document.getElementById("autocomplete" + DIVID).innerHTML = "";
             document.getElementById(ID).focus();
             document.getElementById(ID).click();
             HideDiv("autocomplete" + DIVID);
             //alert(DIVID);
             if (DIVID == '91') {
                 document.getElementById("<%=btnsearchbyitem.ClientID%>").focus();
                 document.getElementById("<%=btnsearchbyitem.ClientID%>").click();

             }
             //ImgUpdateSearchitems
         }

         function FillSearchtextBoxnew(val, ID, DIVID, val1, val2, id1, id2, nm) {
             // alert(val);
             document.getElementById(ID).value = val;
             document.getElementById("autocomplete" + DIVID).innerHTML = "";
             document.getElementById(ID).focus();
             document.getElementById(ID).click();
             HideDiv("autocomplete" + DIVID);
             //alert(DIVID);
             if (DIVID == '91') {
                 document.getElementById("<%=btnsearchbyitem.ClientID%>").focus();
                 document.getElementById("<%=btnsearchbyitem.ClientID%>").click();

             }
             //ImgUpdateSearchitems
         }


         function SendQuery2(key, This, RowNumber) {
             //alert(This.id);
             //alert(RowNumber);

             RowNumber = "9" + RowNumber;

             var str = key;
             var n = str.length;
             if (n < 2) {
                 return 0;
             }
             Initialize();

             var start = 0;
             start = new Date();
             start = start.getTime();

             var url = "AjaxItemsSearchNew.aspx?k=" + key + "&id=" + This.id + "&RowNumber=" + RowNumber + '&start=' + start;
             //alert(url);

             stop = 1;

             if (req != null) {
                 req.onreadystatechange = function() {
                     ProcessSearchnew(RowNumber);
                 };
                 req.open("GET", url, true);
                 req.send(null);

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


    </script>
    
     
    <ajax:AjaxPanel ID="AjaxPanel3" runat="server">
        <asp:Panel ID="pnlgrid" runat="server" Visible="true">
            <!-- BEGIN PORTLET-->
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Search Options</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body"   >
                    <div class="row">
                        <div class="col-md-8">
                          <div class="row form-group">
                                <label class="control-label col-md-3">Requisition Range</label>
                                    <div class="col-md-9">
                                         <div class="row">
                                             <div class="col-md-5">
                                                    <div class='input-group date' id='datetimepicker1'>
                                                        <asp:TextBox  CssClass="form-control input-sm"  placeholder="From"      runat="server" ID="txtDateFrom"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                                        </div> 
                                       <span style="align-content:center;" class="col-md-2"> To </span>
                                         <div class="col-md-5">     
                                                   <div class='input-group date' id='datetimepicker2'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="To"     runat="server" ID="txtDateTo"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                                           </div> 
                                      </div>                          
                                    </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div id="dvsrch" runat="server"  visible="false"  class="row form-group">
                                <div class="col-md-8">
                                    <asp:RadioButton ID="rdall" GroupName="date" Text="All" AutoPostBack="true"   runat="server" />&nbsp;&nbsp;<asp:RadioButton GroupName="date" ID="rdselected" Text="Only Selected" Checked="true" runat="server" AutoPostBack="true" />
                                </div>
                                <div class="col-md-4">
                                     <asp:Button ID="btnSearch" CssClass="btn btn-success btn-xs" runat="server" Text="SEARCH" />
                                </div>
                            </div>
                       
                              
                        </div>
                    </div>
                    

                         <div class="row">
                                    <div class="col-xs-4 col-md-2">
                                    		<div class="form-group">
                                                <asp:DropDownList ID="drpFieldName"  CssClass="form-control input-sm select2me" runat="server">
                                                        <asp:ListItem Value="PONO">PO Number</asp:ListItem>                                                
                                                        <asp:ListItem  Value="OrderNo">Request Number</asp:ListItem>                                        
                                                 </asp:DropDownList>	
                                            </div>
                                     </div>
                                     <div class="col-xs-3 col-md-1">
                                     		<div class="form-group">
                                             
                                            <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Value="Like">Like</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="=">=</asp:ListItem>
                                            </asp:DropDownList>	

                                            </div>
                                     </div>
                                     <div class="col-xs-5 col-md-3">
                                     		<div class="form-group">
                                                 <asp:TextBox ID="txtSearchExpression"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                          	</div>
                                    </div>
                                    <div class="col-xs-12 col-md-6">
                                    	<div class="form-group">
                                         
                                            <asp:Button ID="btnSearchExpression" CssClass="btn btn-success btn-xs" runat="server" Text="SEARCH" />
                                            
                                            &nbsp;&nbsp;<asp:Button ID="btnreset" CssClass="btn btn-success btn-xs" runat="server" Text="RESET SEARCH" />
                                        </div>
                                        <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label>
                                </div>
                                </div>

                    <div  class="portlet-body form">
                        <div class="form-group-search-block">
                            <div class="input-group">
                                <span class="input-group-addon input-circle-left"><i class="fa fa-search"></i></span>
                                <ajax:ajaxpanel id="AjaxPanel6" runat="server">
                                                 
                                                  <asp:TextBox ID="txtOrderSearch"  runat="server" CssClass="form-control input-circle-right"   ></asp:TextBox>
                                 </ajax:ajaxpanel>
                                <br />
                                  <div id="autocomplete"  align="left" class="box autocomplete" style="visibility: hidden;z-index:100;"  > </div>
                            </div>
                        </div>                
                          
                        <div class="row">
                            <div class="col-md-12" style="padding-top: 0px;">
                                <ajax:ajaxpanel id="AjaxPanel7" runat="server">
                                     &nbsp;<asp:LinkButton ID="btnOrderSearch" Width="0" ToolTip="Update Customer" ImageUrl="~/images/2-sh-stock-in.gif"  runat="server" /> 
                                    <asp:Label ID="lblsearchordermsg" runat="server" Text=""></asp:Label>
                                </ajax:ajaxpanel>
                            </div>
                        </div> 
                    </div>

                        <div  class="portlet-body form">
                        <div class="form-group-search-block">
                            <div class="input-group">
                                <span class="input-group-addon input-circle-left"><i class="fa fa-search"></i></span>
                                <ajax:ajaxpanel id="AjaxPanel1" runat="server">
                                                 
                                                  <asp:TextBox ID="txtitemsearchajax"  runat="server" CssClass="form-control input-circle-right"   ></asp:TextBox>
                                 </ajax:ajaxpanel>
                                <br />
                                  <div id="autocomplete91"  align="left" class="box autocomplete" style="visibility: hidden;"  > </div>
                            </div>
                        </div>                
                          
                        <div class="row">
                            <div class="col-md-12" style="padding-top: 0px;">
                                <ajax:ajaxpanel id="AjaxPanel2" runat="server">
                                     &nbsp;<asp:LinkButton ID="btnsearchbyitem" Width="0" ToolTip="search item" ImageUrl="~/images/2-sh-stock-in.gif"  runat="server" /> 
                                    <asp:Label ID="lblsearchbyitem" runat="server" Text=""></asp:Label>
                                </ajax:ajaxpanel>
                            </div>
                        </div> 
                    </div>

<div class="row">
                        <div class="col-md-12">
                            &nbsp;&nbsp;<asp:CheckBox ID="chkonlyStanding" AutoPostBack="true" Text="&nbsp; Only Standing" runat="server" />
                            &nbsp;&nbsp;<asp:CheckBox ID="chkincludeStanding" AutoPostBack="true" Text="&nbsp; Include 'Standing'" runat="server" />
                            &nbsp;<asp:CheckBox ID="chkincludeBought" AutoPostBack="true" Text="&nbsp; Include 'Bought'" runat="server" />
                            &nbsp;<asp:CheckBox ID="chkWithother" AutoPostBack="true" Text="&nbsp; Include 'With other'" runat="server" />
                            &nbsp;<asp:CheckBox ID="chkNotavailable" AutoPostBack="true" Text="&nbsp; Include 'Not available'" runat="server" /> 
                        </div> 
                    </div> 
                    

                      <hr style="margin-top:5px;"/>
                            
                            
                              <div class="note note-success margin-bottom-0">
                              
                               
                            
								 
								<div class="row">
                                	<div class="col-xs-3">
                                             Order Types 
                                            <asp:DropDownList  AutoPostBack="true"  AppendDataBoundItems="true"   CssClass="form-control input-sm select2me"  ID="drpProductTypes" runat="server">
                                                <asp:ListItem Text="--Please Select Product Type--" Value="" ></asp:ListItem>
                                 <asp:ListItem Text="Distribute" Value="Distribute"></asp:ListItem>
                                 <asp:ListItem Text="Hardgoods" Value="Hardgoods"></asp:ListItem>
                                 <asp:ListItem Text="Holiday" Value="Holiday"></asp:ListItem>
                                 <asp:ListItem Text="Flowers" Value="Flowers"></asp:ListItem>
                                 <asp:ListItem Text="Special-Cal" Value="Special-Cal"></asp:ListItem>
                                 <asp:ListItem Text="Special-Hol" Value="Special-Hol"></asp:ListItem>
                                 <asp:ListItem Text="Standing Auto" Value="Standing Auto"></asp:ListItem>                                						 
                                 <asp:ListItem Text="Standing Order" Value="Stg Order"></asp:ListItem>
                                 <asp:ListItem Text="Wedding" Value="Wedding"></asp:ListItem>
                                 <asp:ListItem Text="Wedding-Cal" Value="Wedding-Cal"></asp:ListItem>
                                                 
                                            </asp:DropDownList> 
                                    </div>
                                    <div class="col-xs-3">
                                            Buy Status
                                             <asp:DropDownList ID="drpBuyStatus" AutoPostBack="true"   AppendDataBoundItems="true" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Text="--Please Select Buy Status--" Value="" ></asp:ListItem>
                                    
                 		            <asp:ListItem Text="Bought" Value="Bought"></asp:ListItem>
                                    <asp:ListItem Text="Not Bought" Value="Not Bought"></asp:ListItem>
                                                 
                 		            <asp:ListItem Text="Bought Wed" Value="Bought Wed"></asp:ListItem>
                                    <asp:ListItem Text="Do Not Touch" Value="Do Not Touch"></asp:ListItem>  
                                    <asp:ListItem Text="In Process" Value="In Process"></asp:ListItem>                                    
                                    <asp:ListItem Text="No Action" Value="No Action"></asp:ListItem>                                    
                 		            <asp:ListItem Text="Not Avail" Value="Not Avail"></asp:ListItem>
                                    <asp:ListItem Text="Pass" Value="Pass"></asp:ListItem>
                                    <asp:ListItem Text="Pass Email" Value="Pass eml"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Pending Auction" Value="Pending Auction"></asp:ListItem>
                                    <asp:ListItem Text="Pending Email" Value="Pending Email"></asp:ListItem>                                  		
                                    <asp:ListItem Text="Pending Vendor Response" Value="PendingResponse"></asp:ListItem>
                                    <asp:ListItem Text="Pending-Stg" Value="Pending-Stg"></asp:ListItem>
                                    <asp:ListItem Text="Pending-Stg Email" Value="Pending-Stg eml"></asp:ListItem>
                                    <asp:ListItem Text="Received" Value="Received"></asp:ListItem>
                                    <asp:ListItem Text="Request" Value="Request"></asp:ListItem>                                                                        
                                    <asp:ListItem Text="Shipped" Value="Shipped"></asp:ListItem>                                      
                                    <asp:ListItem Text="Wed" Value="Wed"></asp:ListItem>
                                    <asp:ListItem Text="Wed In Process" Value="Wed In Process"></asp:ListItem>
                                    <asp:ListItem Text="With-Other" Value="With-Other"></asp:ListItem>
                                              </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-3">
                                            Locations 
                                             <asp:DropDownList ID="cmblocationid"   CssClass="form-control input-sm select2me" runat="server"  AppendDataBoundItems="true" AutoPostBack="true" >
                                                    <asp:ListItem Text="--All Locations--" Value=""></asp:ListItem>
                                              </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-3">
                                        Buyer 
                                        <asp:DropDownList ID="drpBuyerlist"   CssClass="form-control input-sm select2me" runat="server"  AppendDataBoundItems="true" AutoPostBack="true" >
                                                    <asp:ListItem Text="--All Buyer--" Value=""></asp:ListItem>
                                              </asp:DropDownList>
                                    </div> 
                               </div>
					   </div>

                </div>
            </div>

            <div class="note note-success margin-bottom-0">
                <div  style='z-index:90;height:100%' class="table-responsive">
                    <table style="padding:5px;" >
                        <tr>
                             
                            <td>
                                <asp:Button ID="btnSENDPO" CausesValidation="false" CssClass="btn btn-success btn-xs"
                                    runat="server" Text="SEND PO"  ></asp:Button>
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td>
                                <asp:Button ID="btnPUSHITEM" CausesValidation="false" CssClass="btn btn-success btn-xs"
                                    runat="server" Text="PUSH ITEM"  ></asp:Button>
                            </td>
                            <td>&nbsp;&nbsp;</td>
 
 
                            <td>
                                <asp:Button ID="btnSETTOBOUGHT" CausesValidation="false" CssClass="btn btn-success btn-xs"
                                    runat="server" Text="SET TO BOUGHT"  ></asp:Button>
                            </td>
                            <td>&nbsp;&nbsp;</td>
                            <td>
                                <asp:Button ID="btnManualBought" CausesValidation="false" CssClass="btn btn-success btn-xs"
                                    runat="server" Text="MANUAL BOUGHT" OnClientClick="return CheckTmethod();"  ></asp:Button>
                            </td>
                            <td>&nbsp;&nbsp;</td>
 
                            <td>
                                <asp:Button ID="btnREFRESHPURCH" CausesValidation="false" CssClass="btn btn-success btn-xs"
                                    runat="server" Text="REFRESH PURCH"  ></asp:Button>
                            </td>
                            <td>
                                &nbsp;&nbsp;<asp:CheckBox ID="chkvalidate" Checked="false" runat="server" />&nbsp;<b>Validate Cost For Non Zero</b>
                            </td>
  
                        </tr>
                    </table>
                </div> 
                       
            </div>

            <!-- END PORTLET-->
            <div  style='z-index:90;height:100%' class="table-responsive">
            </div> 
                   <div   id="autosave">
                   <br />
                     <br />
                 </div>
                 
                <div class="row">
                 <div class="col-md-2 text-left">
                   <input type='checkbox' name='checkall' onclick='checkedAll(aspnetForm);'> &nbsp;&nbsp;<b>Select All</b>
               </div>
                     <div  class="col-md-4 text-left">
                         <div class="row">
                             <div  class="col-md-6 text-right ">
                                <b>Transmission Method </b>
                            </div> 
                             <div  class="col-md-6 text-right ">
                                   <asp:DropDownList ID="drpTransmission"  AppendDataBoundItems="true" CssClass="form-control input-sm select2me" runat="server">
                                        <asp:ListItem Text="--Please Select Transmission--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                                        <asp:ListItem Text="Skype" Value="Skype"></asp:ListItem>
                                        <asp:ListItem Text="Phone" Value="Phone"></asp:ListItem>
                                        <asp:ListItem Text="Komet" Value="Komet"></asp:ListItem>
                                    </asp:DropDownList>
                    
                            </div> 
                        </div> 
                         
                         
                             
                    </div>
                    <div class="col-md-4 text-left">
                        <div class="row">
                             <div  class="col-md-6 text-right ">
                                <b>Default Buy Status</b>
                            </div> 
                             <div  class="col-md-6 text-right ">
                                     <asp:DropDownList ID="drpDefaultBuyStatus" AutoPostBack="true"  AppendDataBoundItems="true" CssClass="form-control input-sm select2me" runat="server">
                                        <asp:ListItem Text="--Please Select Buy Status--" Value=""></asp:ListItem>
                                         
                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                       
                                    </asp:DropDownList>
                    
                            </div> 
                        </div> 
                         
                    </div> 
                 <div class="col-md-2 text-left">
                    <a onClick="window.open('ExportExcelBPO.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" href="#" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="50" height="50" border="0" /></a>
                </div> 
                </div> 
                 <asp:Label ID="LBLMSG" runat="server" Text=""></asp:Label>
<br />
            <asp:GridView ID="OrderHeaderGrid" AllowSorting="true"  runat="server" DataKeyNames="InLineNumber"
                CssClass="table table-bordered table-striped table-condensed flip-content sticky-header" AutoGenerateColumns="false"
                AllowPaging="false"    >
                <Columns>
                    <asp:TemplateField HeaderText="Add New" Visible="false" >
                        <ItemTemplate>
                            <asp:ImageButton ID="imgAdd" ToolTip="Add NEW" ImageUrl="~/Images/Add-Button.gif"
                                runat="server" CommandName="Add" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Sl.No">
                        <ItemTemplate>
                             <%#Eval("RowRank")%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkRow" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField  Visible="false"    HeaderText="RowNumber">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRowNumber" Text='<%#Eval("InLineNumber")%>' CssClass="form-control input-sm"   runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     

                    <asp:TemplateField HeaderText="Location" SortExpression="Location"  >
                        <ItemTemplate>
                            <asp:Label  ID="lblLocationID" Text='<%#Eval("Location")%>'   runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prod. Desc." SortExpression="Product"  >
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" Visible="false"  Text='<%#Eval("Product")%>'  runat="server"></asp:Label>
                            <asp:Label ID="lblProductName" Text='<%#Eval("ItemName") %>'  runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type" SortExpression="Type"  >
                        <ItemTemplate>
                            <%#Eval("Type")%> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order #" SortExpression="OrderNo" >
                        <ItemTemplate>
                             <%#Eval("OrderNo")%> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO#"   >
                        <ItemTemplate>
                            <%#Eval("PONO")%> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QOH" SortExpression="QOH" >
                        <ItemTemplate>
                             <%#Eval("QOH")%> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pre Sold"  >
                        <ItemTemplate>
                             <%#Eval("PRESOLD")%> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Req"  SortExpression="Q_REQ" >
                        <ItemTemplate>
                             <%#Eval("Q_REQ")%> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Color Variety"  >
                        <ItemTemplate>
                             <asp:TextBox ID="txtColorVerity" Text='<%#Eval("COLOR_VARIETY")%>' CssClass="form-control input-sm" Width="150px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Store Rem."  >
                        <ItemTemplate>
                            <asp:TextBox ID="lblPoNotes" Text='<%#Eval("REMARKS")%>' CssClass="form-control input-sm" Width="200px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Ship Date" SortExpression="ShipDate"  >
                        <ItemTemplate>
                            <asp:TextBox ID="lblShipDate" Text='<%#Eval("ShipDate")%>'   CssClass="form-control input-sm" Width="100px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" SortExpression="Q_ORD" >
                        <ItemTemplate>
                            <asp:TextBox ID="lblQtyOrdered" Text='<%#Eval("Q_ORD")%>' CssClass="form-control input-sm" Width="50px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pack" >
                        <ItemTemplate>
                            <asp:TextBox ID="lblPack" Text='<%#Eval("PACK")%>' CssClass="form-control input-sm" Width="50px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost"  >
                            <itemtemplate>
                                <asp:TextBox ID="lblCost" Text='<%# String.Format("{0:N2}", Eval("COST"))%>' CssClass="form-control input-sm" Width="100px" runat="server"></asp:TextBox>
				            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total"  >
                            <itemtemplate>
                                    <asp:TextBox ID="lblTotal" Text='<%# String.Format("{0:N2}", Eval("Ext_COSt"))%>' CssClass="form-control input-sm" Width="100px" runat="server"></asp:TextBox>
				            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor" SortExpression="Vendor_Code">
                            <ItemTemplate>
                              <asp:TextBox ID="lblVendor" Text='<%# Eval("Vendor_Code")%>' placeholder="Vendor" Width="150" CssClass="form-control input-sm"  runat="server"></asp:TextBox>
                             <div id="autocomplete<%#Eval("InLineNumber")%>"  align="left" class="box autocompletenew" style="visibility: hidden;"  > </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ven Rem."    >
                            <itemtemplate>
                                <asp:TextBox ID="lblVendor_Remarks" Text='<%# Eval("Vendor_Remarks")%>' CssClass="form-control input-sm" Width="100px" runat="server"></asp:TextBox>
				            </itemtemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="PO Status">
                            <itemtemplate>
                                <asp:Label ID="lblBuyStatus" Text='<%#Eval("Status")%>' Visible="false"   runat="server"></asp:Label>
                                <asp:DropDownList ID="drpPOStatus" CssClass="form-control input-sm" Width="100px" runat="server">
                                    <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Bought" Value="Bought"></asp:ListItem>
                 		            <asp:ListItem Text="Bought Wed" Value="Bought Wed"></asp:ListItem>
                                    <asp:ListItem Text="Do Not Touch" Value="Do Not Touch"></asp:ListItem>  
                                    <asp:ListItem Text="In Process" Value="In Process"></asp:ListItem>                                    
                                    <asp:ListItem Text="No Action" Value="No Action"></asp:ListItem>                                    
                 		            <asp:ListItem Text="Not Avail" Value="Not Avail"></asp:ListItem>
                                    <asp:ListItem Text="Pass" Value="Pass"></asp:ListItem>
                                    <asp:ListItem Text="Pass Email" Value="Pass Email"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Pending Auction" Value="Pending Auction"></asp:ListItem>
                                    <asp:ListItem Text="Pending Email" Value="Pending Email"></asp:ListItem>                                  		
                                    <asp:ListItem Text="Pending-Stg" Value="Pending-Stg"></asp:ListItem>
                                    <asp:ListItem Text="Pending-Stg Email" Value="Pending-Stg Email"></asp:ListItem>
                                    <asp:ListItem Text="Wed" Value="Wed"></asp:ListItem>
                                    <asp:ListItem Text="Wed In Process" Value="Wed In Process"></asp:ListItem>
                                    <asp:ListItem Text="With-Other" Value="With-Other"></asp:ListItem>
                                </asp:DropDownList>
				    </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Buyer">
                            <itemtemplate>
                              <asp:Label ID="lblBuyer" Text='<%#Eval("Buyer")%>' Visible="false"   runat="server"></asp:Label>
                               <asp:DropDownList ID="drpBuyer" CssClass="form-control input-sm" Width="100px" runat="server">
                                     <asp:ListItem Text="-Select-" Value="0" />
                                        <asp:ListItem Text="Admin" Value="Admin" />
                                        <asp:ListItem Text="Arlington" Value="Arlington" />
                                        <asp:ListItem Text="Betty" Value="Betty" />
                                        <asp:ListItem Text="Bree" Value="Bree" />
                                        <asp:ListItem Text="Brian" Value="Brian" />
                                        <asp:ListItem Text="Cesar" Value="Cesar" />
                                        <asp:ListItem Text="Claire" Value="Claire" />
                                        <asp:ListItem Text="ClarksSummit2" Value="ClarksSummit2" />
                                        <asp:ListItem Text="ClarksSummit4" Value="ClarksSummit4" />
                                        <asp:ListItem Text="Debbie" Value="Debbie" />
                                        <asp:ListItem Text="Debi" Value="Debi" />
                                        <asp:ListItem Text="Diane" Value="Diane" />
                                        <asp:ListItem Text="Erin" Value="Erin" />
                                        <asp:ListItem Text="Frank" Value="Frank" />
                                        <asp:ListItem Text="Frankc" Value="Frankc" />
                                        <asp:ListItem Text="Frankp" Value="Frankp" />
                                        <asp:ListItem Text="Gayle" Value="Gayle" />
                                        <asp:ListItem Text="Heather" Value="Heather" />
                                        <asp:ListItem Text="Hevinh" Value="Hevinh" />
                                        <asp:ListItem Text="IAN" Value="IAN" />
                                        <asp:ListItem Text="Jane" Value="Jane" />
                                        <asp:ListItem Text="Jennies" Value="Jennies" />
                                        <asp:ListItem Text="Jennifer" Value="Jennifer" />
                                        <asp:ListItem Text="Judy" Value="Judy" />
                                        <asp:ListItem Text="Kathi" Value="Kathi" />
                                        <asp:ListItem Text="Kelsey" Value="Kelsey" />
                                        <asp:ListItem Text="Kennysmith" Value="Kennysmith" />
                                        <asp:ListItem Text="Kevin" Value="Kevin" />
                                        <asp:ListItem Text="Kristina" Value="Kristina" />
                                        <asp:ListItem Text="liz" Value="liz" />
                                        <asp:ListItem Text="Mariana" Value="Mariana" />
                                        <asp:ListItem Text="Mark" Value="Mark" />
                                        <asp:ListItem Text="Mike" Value="Mike" />
                                        <asp:ListItem Text="Pat" Value="Pat" />
                                        <asp:ListItem Text="Penny" Value="Penny" />
                                        <asp:ListItem Text="Rebecca" Value="Rebecca" />
                                        <asp:ListItem Text="Rhiannon" Value="Rhiannon" />
                                        <asp:ListItem Text="Steve" Value="Steve" />
                                        <asp:ListItem Text="Tammy" Value="Tammy" />
                                        <asp:ListItem Text="Taylor" Value="Taylor" />
                                        <asp:ListItem Text="Walker" Value="Walker" />
                                     
                                </asp:DropDownList>   
				    </itemtemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
                <div class="container"> 
				<div class="row">
                                	
                    <div class="col-xs-4">
                        <div class="c-filter">
                                 Show: 
                                    <asp:DropDownList ID="drppagelimit" AutoPostBack="True" runat="server" Width="100px" class="form-control c-square c-theme c-input"  >
                                    <asp:ListItem   Text="25" Value="25" ></asp:ListItem>
                                    <asp:ListItem   Text="50" Value="50" ></asp:ListItem>
                                    <asp:ListItem  Selected="True" Text="100" Value="100" ></asp:ListItem>
                                    <asp:ListItem  Text="150" Value="150" ></asp:ListItem>
                                    <asp:ListItem  Text="200" Value="200" ></asp:ListItem>
                                    <asp:ListItem  Text="250" Value="250" ></asp:ListItem>
                                    <asp:ListItem  Text="300" Value="300" ></asp:ListItem>
                                    <asp:ListItem  Text="350" Value="350" ></asp:ListItem>
                                    <asp:ListItem  Text="400" Value="400" ></asp:ListItem>
                                    <asp:ListItem  Text="450" Value="450" ></asp:ListItem>
                                    <asp:ListItem  Text="500" Value="500" ></asp:ListItem>
                                </asp:DropDownList>
                              
                                            
                            </div>
                    </div>
                                
                    <div class="col-xs-8">

                        <ul class="c-content-pagination c-square c-theme pull-right">
                            <li class="c-prev">
                                <a id="linkleft"  onclick="<%=pagingleft%>" href="Javascript:;">
                                    <i class="fa fa-angle-left"></i>
                                </a>
                            </li>

                            <%=paging %>

                            <li class="c-next">
                                <a id="linkright" onclick="<%=pagingright%>" href="Javascript:;">
                                    <i class="fa fa-angle-right"></i>
                                </a>
                            </li>
                        </ul>

                    </div> 
                                
                                
                </div> 
                 </div> 
                
            
            <div style="display:none;" >
                <asp:TextBox ID="txtpageno" runat="server"></asp:TextBox>
                <asp:LinkButton ID="btnpageno" Width="0" ToolTip="page no" ImageUrl="~/images/2-sh-stock-in.gif"  runat="server" /> 
            </div>

            <div id="divEmailContent"  runat="server"  ></div>
            <asp:Label ID="lblerrortestmail" runat="server" Text=""></asp:Label>
             

<asp:Label ID="lblsql" Visible="false"  runat="server" Text=""></asp:Label>

        </asp:Panel>
    </ajax:AjaxPanel>
    
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>
    <script>
        function paging(val) {
            //alert(val);
            document.getElementById("<%=txtpageno.ClientID%>").value = val;
            document.getElementById("<%=btnpageno.ClientID%>").focus();
            document.getElementById("<%=btnpageno.ClientID%>").click();
            //ImgUpdateSearchitems
        }



        jQuery(document).ready(function() {

            Search.init();
        });
    </script>

       

    <script type="text/javascript">
        function CheckTmethod() {
            if (document.getElementById("ctl00_ContentPlaceHolder_drpTransmission").value == "") {
                alert("Please Select Transmission Method To Proceed.");
                document.getElementById("ctl00_ContentPlaceHolder_drpTransmission").focus();
                return false;
            }
            else {
                return true;
            }

        }
    </script>
                         
<script type="text/javascript" src="jquery.min.js"></script>
<script type="text/javascript" src="bootstrap.min.js"></script>
                         
 <script type="text/javascript" src="jquery.floatThead.min.js"></script>
    <script language="javascript" type="text/javascript" >

        $(document).ready(function() {
            $('.sticky-header').floatThead();
        });


        function CalcKeyCode(aChar) {
            var character = aChar.substring(0, 1);
            var code = aChar.charCodeAt(0);
            return code;
        }

        function checkNumber(val) {
            var strPass = val.value;
            var strLength = strPass.length;
            var lchar = val.value.charAt((strLength) - 1);
            var cCode = CalcKeyCode(lchar);

            /* Check if the keyed in character is a number
            do you want alphabetic UPPERCASE only ?
            or lower case only just check their respective
            codes and replace the 48 and 57 */
            //alert(cCode);
            if (cCode < 48 || cCode > 57) {
                if (cCode != 46) {
                    alert('Please enter numeric only.');
                    var myNumber = val.value.substring(0, (strLength) - 1);
                    val.value = '0';
                }    
            }
            return false;
        }

        function checkNumber(val) {
            var strPass = val.value;
            var strLength = strPass.length;
            var lchar = val.value.charAt((strLength) - 1);
            var cCode = CalcKeyCode(lchar);

            /* Check if the keyed in character is a number
            do you want alphabetic UPPERCASE only ?
            or lower case only just check their respective
            codes and replace the 48 and 57 */
            //alert(cCode);
            if (cCode < 48 || cCode > 57) {
                if (cCode != 46) {
                    alert('Please enter numeric only.');
                    var myNumber = val.value.substring(0, (strLength) - 1);
                    val.value = '0';
                }
            }
            return false;
        }
        
        
      </script>

<script type="text/javascript">
    function AllowAlphabet(e) {
        //alert(e.keyCode);
        if (e.keyCode == 9) {
            //TAB is pressed keyCode = 9 for TAB key
            return true ;
        }
        else {
            return false ;
        }
    }
</script>

</asp:Content>
