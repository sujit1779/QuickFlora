<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="InventoryStatusNew2.aspx.vb" Inherits="InventoryStatusNew2" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>
 
    <style>
         <%=itemsearchcss%>
.autocomplete_item {
  overflow-x: hidden;
  overflow-y: hidden;
  height: 400px;
  position: absolute;
  z-index: 3;
  width: 80%;
  background-color: #fff;
  box-shadow: 5px 5px rgba(102, 102, 102, 0.1);
  border: 2px solid #40BD24;
  top: 33px;
  visibility: hidden;
}
    /*body{
             font-size:16px !important;
             color:#000000;
        }
        label{
            font-size:16px !important;
            color:#000000;
        }
        .input-sm
        {
            font-size:16px !important;
            color:#000000;
        }
        .input-xs
        {
            font-size:16px !important;
            color:#000000;
        }
        .select.input-xs
        {
            font-size:16px !important;
            color:#000000;
        }*/

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

		.fixed_headers td:nth-child(2),
		.fixed_headers th:nth-child(2) {
			min-width: 120px;
		}
		.fixed_headers td:nth-child(3),
		.fixed_headers th:nth-child(3) {
			min-width: 250px;
		}
		.fixed_headers td:nth-child(4),
		.fixed_headers th:nth-child(4) {
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
			/*background-color: #FFFFF0;*/
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

        .autocomplete_item
        {
            overflow-x: auto !important;
			overflow-y: auto !important;
        }
        div.dataTables_length label{
            float:right;
        }
        div.dataTables_paginate{
            float:left;
            display:block!important;
        }
        .preview_desc{
              display: none;
              position: absolute;
              background: #eaeaea;
              Max-height: 130px;
              width: 250px;
              font-size: 12px;
              padding: 3px 7px 3px;
              text-align: justify;
              overflow: hidden;
              /*white-space: nowrap;*/
              overflow: hidden;
              text-overflow: ellipsis;

        }
        #autocomplete2{
            height:400px!important;
        }
        .autocomplete{
            z-index:3!important;
        }
    .form-control , label   {
        font-size: 13px!important;
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

        
    function SendQuery2(key) {



        // alert(document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value);
        var start = 0;
        start = new Date();
        start = start.getTime();

        Initialize();
        var url = "AjaxAvaibilityItemsSearchAllNew.aspx?k=" + key + "&start=" + start + "&locationid="  + document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value;
      // alert(url);
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
                    //document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
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
        function FillSearchtextBox2(val) {
            document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = val;
            document.getElementById("ctl00_ContentPlaceHolder_btn").focus();
            document.getElementById("ctl00_ContentPlaceHolder_btn").click();
            itemrsearchcloseProcess();

    }


    function itemrsearchcloseProcess() {

        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
      //  document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
        document.getElementById("autocomplete2").innerHTML = "";
        HideDiv("autocomplete2");


    }


    function itemsearchBlurProcess() {

        if (document.getElementById("<%=txtitemsearch.ClientID%>").value == "") {
            HideDiv("autocomplete2");
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        }

    }
         
        function Qtybalance(This, qty, name, id ,qty2) {
            qty = qty - qty2;
            
            document.getElementById(id).value = qty - (This.value);
             This.style.backgroundColor = "";
            if (document.getElementById(id).value < 0) {
                alert("Please check Accept Qty must be less than equal to balance Qty");
                This.style.backgroundColor = "Red";
                This.focus();
                This.click();
            }
        }
        function Saveitem(This, ItemID, name, oldvalue) {
            Initialize();

            This.style.backgroundColor = "";

            var url = "AjaxSaveGrowerInventory.aspx?txtend=" + document.getElementById("ctl00_ContentPlaceHolder_txtend").value + "&txtDateTo=" + document.getElementById("ctl00_ContentPlaceHolder_txtDateTo").value + "&txtstart=" + document.getElementById("ctl00_ContentPlaceHolder_txtstart").value + "&LocationID=" + document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value + "&ItemID=" + ItemID + "&value=" + This.value + "&name=" + name;
            //alert(url);
            //alert(This.value);
            //alert(oldvalue);

            

            if (This.value == oldvalue) {
                return;
            }

             // alert(url);

            document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
            document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

            $.get(url, function(data, status) {
              //  alert("Data: " + data + "\nStatus: " + status);
                document.getElementById("autosave").style.backgroundImage = 'url()';
            });

 

        }
         
        var value_on_click = "";
        
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





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Inventory Status
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    Search Options
                </div>
                <div class="tools">
                    <a href="javascript:;" class="collapse"></a>
                </div>
            </div>
            <div class="portlet-body">

<div   class="row">
                        <div class="col-md-8">
                          <div class="row form-group">
                                <label class="control-label col-md-3">Delivery Date</label>
                                    <div class="col-md-9">
                                         <div class="row">
                                             <div class="col-md-5">
                                                    <div class='input-group date' id='datetimepicker1'>
                                                        <asp:TextBox  CssClass="form-control input-sm"  placeholder="From"      runat="server" ID="txtstart"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                                        </div> 
                                                    <span class="col-md-1">
                                                      </span>
                                         <div class="col-md-5">     
                                                   
                                                   <div class='input-group date' id='datetimepicker3'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="To Date"     runat="server" ID="txtend"> </asp:TextBox>
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
                            <div class="row form-group">
                                 
                                <div class="col-md-12">
                                    
                                    
                                           
                                </div>
                                 
                                    
                                           
                                 
                            </div>
                        </div>
                    </div>

                   <div style="display:none" class="row">
                                    <div class="col-xs-4 col-md-2">
                                    		<div class="form-group">
                                                 
                                                <asp:DropDownList ID="drpFieldName"  CssClass="form-control input-sm select2me" runat="server">
                                                         
                                                        <asp:ListItem  Value="ItemID">Item ID</asp:ListItem>                                                                                                
                                                        <asp:ListItem Value="VendorID">Vendor ID</asp:ListItem>  
                                                                                                               
                                                 </asp:DropDownList>	

                                            </div>
                                     </div>
                                     <div class="col-xs-3 col-md-1">
                                     		<div class="form-group">
                                             
                                            <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Selected="True" Value="=">=</asp:ListItem>
                                                <asp:ListItem Value="Like">Like</asp:ListItem>
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

                                        </div>
                                       
                                </div>
                   </div>


                
                <div style="display:none" class="note note-success margin-bottom-0">

                    <div class="row">
                        <div class="col-xs-6">
                            Type:
                             <asp:DropDownList ID="drpType" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                 <asp:ListItem Text="--Select Type--" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Flowers" Value="Flowers"></asp:ListItem>
                                 <asp:ListItem Text="Greens" Value="Greens"></asp:ListItem>
                                 <asp:ListItem Text="Plants" Value="Plants"></asp:ListItem>
                                 <asp:ListItem Text="Holland" Value="Holland"></asp:ListItem>
                                 
                             </asp:DropDownList>
                        </div>
                        <div class="col-xs-6">
                            Location 
                            <asp:DropDownList ID="cmblocationid1" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Location--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                         
                       

                    </div>
                     
                </div>

                
               <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td width="25%" ><b>Location/Farm</b>  </td> 
                            <td width="35%"  align=left>    <asp:DropDownList ID="cmblocationid"   CssClass="form-control input-sm" runat="server"  AppendDataBoundItems="true" AutoPostBack="true" >
                                                    <asp:ListItem Text="--All Location--" Value=""></asp:ListItem>
                                                </asp:DropDownList> </td>
                             <td width="40%" style="text-align:center" >&nbsp;
                                 <asp:CheckBox ID="excludeQOH" runat="server" /> Exclude Quantity On Hand
                             </td>
                        </tr>
                         <tr>
                            <td><b>Select Availability</b> </td> <td colspan="2" align=left>
                                <asp:RadioButton GroupName="rdb"  ID="rdallorder" Text="Show All"   Checked="true" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton GroupName="rdb"  ID="rdbackorder" Text="Show only back order"   runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton GroupName="rdb"  ID="rdoversold" Text="Show only over sold"   runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton GroupName="rdb"  ID="rdonhand" Text="Show only on hand"   runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton GroupName="rdb"  ID="rdforsale" Text="Show only available for sale"   runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                            
                             </td> 
                        </tr>
                    <tr>
                            <td><b>Availability</b> </td> <td align=left>
                             <table>
                                   <tr>
                            <td><b>  &nbsp;</b> </td> <td align=left>
                                <table>
                                    <tr>
                                        <td> </td>
                                        <td>  &nbsp; </td>
                                        <td>  <b>   </b>&nbsp;</td>
                                        <td>    <asp:RadioButton ID="rdall" GroupName="date" Text="&nbsp;All" Checked="true" AutoPostBack="true" runat="server" />

                                                    &nbsp;&nbsp;<asp:RadioButton GroupName="date" AutoPostBack="true" ID="rdselected" Text="&nbsp;Only Selected" runat="server" />
                                                   


                                        </td>
                                    </tr>

                                </table>
                             
                            
                             </td><td>&nbsp;</td>
                        </tr>
                   

                             </table>
                            
                             </td><td>&nbsp;</td>
                        </tr>


                        <tr>
                            <td><b> </b> </td> <td align="center">
                              
                                <asp:Button ID="btn" runat="server" class="btn green"  Text="Search" />
                             </td><td>&nbsp; <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label></td>
                        </tr>
                 
               
               
               </table>
            
                                      <div class="form-group-search-block">
                                        <div class="input-group">
                                            <span class="input-group-addon input-circle-left">
                                            <i class="fa fa-search"></i>
                                            </span>
                                               
                                                <asp:TextBox ID="txtitemsearch"  CssClass="form-control input-circle-right"    runat="server"></asp:TextBox>
                                               
                                             <br />
                                             <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete2"   ></div>   
                                           
                                            
                                        </div>
                                    </div>
              
            

            </div>
        </div>
        <!-- END PORTLET-->
           <div class="portlet box green">
              <div class="portlet-title">
                  <div class="caption">Item Search</div>
                  <div class="tools"><a href="javascript:;" class="collapse"></a></div>
              </div>
              <div class="portlet-body">
                  <%-- Item Search --%>

                  <div class="note note-success margin-bottom-0">
                      <div class="input-group">
                          <table id="tbpc" runat="server">

                              <tr>
                                  <%--<td>Product Family </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Family</label>
                                      <asp:DropDownList  AutoPostBack="true" ID="drpProductFamily" runat="server" CssClass="form-control"></asp:DropDownList>
                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td> </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Category</label>
                                      <asp:DropDownList  AutoPostBack="true"  ID="ctl00_ContentPlaceHolder_drpProductCategory" runat="server" CssClass="form-control"></asp:DropDownList>

                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td>Product Size </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Group</label>
                                      <asp:DropDownList  AutoPostBack="true" ID="drpGrp" runat="server" CssClass="form-control"></asp:DropDownList>

                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td>Product Color </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Color</label>
                                      <input type="text" class="form-control" id="itemColor" runat="server" />

                                  </td>
                                  <td>&nbsp; </td>
                                  <td>&nbsp; </td>
                                  <%--<td>Product Size </td>--%>
                                  <td>&nbsp; </td>
                                  <td>
                                      <label>Product Size</label>
                                      <input type="text" class="form-control" id="itemSize" runat="server" />

                                  </td>
                              </tr>
                          </table>
                          <br />
                      </div>

                      <div class="input-group">
                          <span class="input-group-addon input-circle-left"><i class="fa fa-search"></i></span>
                          <ajax:AjaxPanel ID="AjaxPanel12" runat="server">
                              <asp:TextBox ID="txtcustomersearch" CssClass="form-control input-circle-right" runat="server" data-tab-priority="28" placeholder="Search Product" autocomplete="off"></asp:TextBox>
                          </ajax:AjaxPanel>
                          <br />
                          <div align="left" class="box autocomplete_item" style="visibility: hidden; overflow-y: hidden !important; overflow-x: auto !important; overflow-x: auto  !important; height: 400px!important;" id="autocomplete3">
                          </div>
                      </div>
                  </div>

                  <div style="text-align: right">
                      <asp:Button ID="Search2" runat="server" Text="Search" CssClass="btn btn-primary" />
                  </div>
              </div>
          </div>

        <asp:HiddenField ID="hdSortDirection" runat="server" />

        <div  style='z-index:90;height:100%' class="table-responsive">
        
               
        <asp:GridView ID="InventoryStatusGrid" runat="server"  AutoGenerateColumns="false" AllowSorting="true"
            DataKeyNames="ItemID" PageSize="25" AllowPaging="true" CssClass="table table-bordered table-striped table-condensed flip-content">
            <Columns>

                 
                <asp:TemplateField    >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Item ID</span>
                         <%--<i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Unique Item ID" class="fa fa-info-circle" aria-hidden="true"></i>--%>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemID" Text='<%#Eval("ItemID")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Item Name</span>
                         <%--<i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Item Name" class="fa fa-info-circle" aria-hidden="true"></i>--%>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" Text='<%#Eval("ItemName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                
                <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Location ID</span>
                         <%--<i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Item Location ID" class="fa fa-info-circle" aria-hidden="true"></i>--%>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLocationID" Text='<%#FillLocationLocationName(Eval("LocationID"))%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                
                   
                 <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Qty Ordered</span>
                         <i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Item count of all the customer booked orders in system, having ship date greater from today." class="fa fa-info-circle" aria-hidden="true"></i>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQtyOnOrder" Text='<%#Eval("TotalQtyOnOrder")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                 
                 <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Qty On Hand</span>
                         <i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Anything item in physical Inventory at a Location which is not booked and has end/aged date within (specified days) from today." class="fa fa-info-circle" aria-hidden="true"></i>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblQtyOnHand" Text='<%#Eval("QtyOnHand")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                  
                 <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Qty Assigned on Orders</span>
                         <i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Qty Available to fill the Orders." class="fa fa-info-circle" aria-hidden="true"></i>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQtyCommitted" Text='<%#Eval("TotalQtyCommitted")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                
                 <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >QTY To Receive</span>
                         <i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Inventory in Transit (Yet to physically receive)." class="fa fa-info-circle" aria-hidden="true"></i>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQtyToReceive" Text='<%#Eval("TotalQtyToReceive")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                                  
                    
                 <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Over Sold</span>
                         <i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Any Inventory count of a item (for next (Specified aged/end days) from today) which is booked over the available physical on hand Qty or (in transit inventory)." class="fa fa-info-circle" aria-hidden="true"></i>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTotalOverSold" Text='<%#Eval("TotalOverSold")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                                  
                    
                 
                
                <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Qty on BackOrder</span>
                         <i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="Any Inventory count of a item for future ship days started from 'Specified aged/end days' of the item. Which is booked over the available physical on hand Qty or (in transit inventory)." class="fa fa-info-circle" aria-hidden="true"></i>
                    </HeaderTemplate>
                    <ItemTemplate>                        
                            <asp:Label ID="lblQrtBackOrder" Text='<%#Eval("TotalQtyBackOrder")%>' runat="server"></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>    
                
                <asp:TemplateField  >
                    <HeaderTemplate  >
                         <span style="color:#678b38; vertical-align:middle;" >Total AvailFor Sale</span>
                         <i style="color:#678b38; font-size:20px;" data-toggle="tooltip" title="QTY in Hand + QTY to Receive - All booked orders as of today." class="fa fa-info-circle" aria-hidden="true"></i>
                    </HeaderTemplate>
                    <ItemTemplate>                        
                            <asp:Label ID="lblTotalAvailableForSale" Text='<%#Eval("TotalAvailableForSale")%>' runat="server"></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>             

                
                 
                
                
                              
 


 <%--              <asp:TemplateField HeaderText="LocationID">
                    <ItemTemplate>
                        <asp:Label ID="TXTLocationID" Text='<%#Eval("LocationID")%>' runat="server"></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category ID">
                    <ItemTemplate>
                        <asp:Label ID="TXTItemCategoryID" Text='<%#Eval("ItemCategoryID")%>' runat="server"></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>

               
                <asp:TemplateField HeaderText="Item ID">
                    <ItemTemplate>
                        <%#Eval("ItemID")%>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <%#Eval("ItemName")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Color">
                    <ItemTemplate>
                        <%#Eval("ItemColor")%>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Variety">
                    <ItemTemplate>
                        <%#Eval("Variety")%>
                    </ItemTemplate>

                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Item Pack Size">
                    <ItemTemplate>
                        <%#Eval("ItemPackSize")%>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item UOM">
                    <ItemTemplate>
                        <%#Eval("ItemUOM")%>
                    </ItemTemplate>

                </asp:TemplateField>


                <asp:TemplateField HeaderText="Qty On Hand">
                    <ItemTemplate>
                         <%#Eval("QtyOnHand")%> 
                    </ItemTemplate>

                </asp:TemplateField>
               

               

                <asp:TemplateField HeaderText="Qty On Backorder">
                    <ItemTemplate>
                        <%#Eval("QtyOnBackorder")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Qty On Order">
                    <ItemTemplate>
                        <%#Eval("QtyOnOrder")%>
                    </ItemTemplate>
                </asp:TemplateField>

                

                <asp:TemplateField HeaderText="QTY To Receive">
                    <ItemTemplate>
                         <%#Eval("QTYToReceive")%> 
                    </ItemTemplate>

                </asp:TemplateField>

                

                <asp:TemplateField HeaderText="Vendor ID">
                    <ItemTemplate>
                        <%#Eval("VendorID")%>
                    </ItemTemplate>

                </asp:TemplateField>


                <asp:TemplateField HeaderText="Qty Committed">
                    <ItemTemplate>
                        <%#Eval("QtyCommitted")%>
                    </ItemTemplate>

                </asp:TemplateField>

               
                 <asp:TemplateField HeaderText="Re Order Qty">
                    <ItemTemplate>
                        <%#Eval("ReOrderQty")%>
                    </ItemTemplate>
                </asp:TemplateField>

                 
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        $<%#String.Format("{0:N2}", Eval("Price"))%>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Average Cost">
                    <ItemTemplate>
                        $<%#String.Format("{0:N2}", Eval("AverageCost"))%>
                    </ItemTemplate>

                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Purchase Ship Date">
                    <ItemTemplate>
                         
                         <%#Eval("PurchaseShipDate")%> 
                    </ItemTemplate>
                </asp:TemplateField>--%>


            </Columns>
        </asp:GridView>


        </div> 
     
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">



<script type="text/javascript">
$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip();   
});
</script>

    
    <script type="text/javascript" >
        //alert('test');
      // doOnLoadNew();
        doOnLoadNew1();
        doOnLoadNew2();

        var myCalendarto;
        function doOnLoadNew() {
            //alert('test');
           
            myCalendarto = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtDateFrom"]);
            //alert(myCalendar);
            myCalendarto.setDateFormat("%m/%d/%Y");
        }

         var myCalendarto1;
        function doOnLoadNew1() {
            //alert('test');
           
            myCalendarto1 = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtstart"]);
            //alert(myCalendar);
            myCalendarto1.setDateFormat("%m/%d/%Y");
        }



         var myCalendarto2;
        function doOnLoadNew2() {
            //alert('test');
           
            myCalendarto2 = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtend"]);
            //alert(myCalendar);
            myCalendarto2.setDateFormat("%m/%d/%Y");
        }




       </script>

    
<script>
    var CompanyID = "<%= CompanyID %>";
        var DivisionID = "<%= DivisionID %>";
    var DepartmentID = "<%= DepartmentID %>";
 


         
    function SendQuery2callfromcategory() {
        var key = "";
        key = document.getElementById("ctl00_ContentPlaceHolder_txtcustomersearch").value;
        SendQuery3(key);
    }

    

    function SendQuery3(key) {

        

        // alert(document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value);
        var start = 0;
        start = new Date();
        start = start.getTime();

        Initialize();
        //var url = "AjaxItemsSearch.aspx?k=" + key + "&start=" + start + "&lc=" + document.getElementById("ctl00_ContentPlaceHolder_txtCustomerTypeID").value;
        var url = 'AjaxItemsSearchPOS';
            url = url + ".aspx?k=" + key + "&start=" + start
                + "&lc=" 
                + "&locationid=" + (document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value ? document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value : 'DEFAULT')
                //+ "&od=" + document.getElementById("ctl00_ContentPlaceHolder_lblOrderDate").value
                + "&sd=" + document.getElementById("ctl00_ContentPlaceHolder_txtstart").value
                + "&ProductCategory=" + document.getElementById("<%=ctl00_ContentPlaceHolder_drpProductCategory.ClientID%>").value
                + "&drpProductFamily=" + document.getElementById("<%=drpProductFamily.ClientID%>").value
                + "&ProductColor=" + document.getElementById("<%=itemColor.ClientID%>").value
                + "&ProductGroup=" + document.getElementById("<%=drpGrp.ClientID%>").value
                + "&ProductSize=" + document.getElementById("<%=itemSize.ClientID%>").value;



            // alert(url);
            if (req != null) {
                req.onreadystatechange = Process3;
                req.open("GET", url, true);
                req.send(null);

            }

        }


    function Process3() {
            document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
            document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
            document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'right';


            if (req.readyState == 4) {
                // only if "OK"
                if (req.status == 200) {
                    document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url()';
                    //console.log(req.responseText);
                    if (req.responseText == "") {
                        //alert("in blank");  
                        //HideDiv("autocomplete");
                       // document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundImage = 'url(searchanimation.gif)';
                        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
                        document.getElementById("<%=txtcustomersearch.ClientID%>").style.backgroundPositionX = 'left';
                       // document.getElementById("<%=txtcustomersearch.ClientID%>").value = "";
                        var newdiv = document.createElement("div");
                        newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some other keyword.</div>";
                        var container = document.getElementById("autocomplete3");
                        container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess3();' >Close Search result</a></div>";
                        container.appendChild(newdiv)
                    }
                    else {


                        if (document.getElementById("<%=txtcustomersearch.ClientID%>").value != "") {
                            ShowDiv("autocomplete3");
                            //document.getElementById("autocomplete2").innerHTML = req.responseText;

                            var newdiv = document.createElement("div");
                            newdiv.innerHTML = req.responseText;
                            var container = document.getElementById("autocomplete3");
                            container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess3();' >Close Search result</a></div>";
                            container.appendChild(newdiv)

                             var table = document.getElementById('display-table');
                            var cells = table.getElementsByTagName('td');

                            for (var i = 0; i < cells.length; i++) {
                                // Take each cell
                                var cell = cells[i];
                                // do something on onclick event for cell
                                cell.onclick = function () {
                                    // Get the row id where the cell exists
                                    var rowId = this.parentNode.rowIndex;

                                    var rowsNotSelected = table.getElementsByTagName('tr');
                                    for (var row = 0; row < rowsNotSelected.length; row++) {
                                        rowsNotSelected[row].style.backgroundColor = "";
                                       // rowsNotSelected[row].classList.remove('selected');
                                    }
                                    var rowSelected = table.getElementsByTagName('tr')[rowId];
                                    rowSelected.style.backgroundColor = "yellow";
                                   // rowSelected.className += " selected";

                                    msg = rowSelected.cells[0].innerHTML;
                                    //alert(msg);
                                    FillSearchtextBox3(msg);
                                }
                            }

                        }
                    }
                }
                else {
                    document.getElementById("autocomplete3").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;
                }
            }
    }

    
        function FillSearchtextBox3(val) {
            // alert(val);
            if (document.getElementById("ctl00_ContentPlaceHolder_txtcustomersearch").value == "") {

            }
            else {
                var str = val;
                var str1;
                str1 = str.split("~!");

                document.getElementById("ctl00_ContentPlaceHolder_txtcustomersearch").value = str1[0];
                //    document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value = str1[0];
                //    document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = str1[1];
                //    document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = str1[2];

                itemrsearchcloseProces3();

            }
    }


    function itemrsearchcloseProces3() {

           // document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url(searchanimation.gif)';
            document.getElementById("ctl00_ContentPlaceHolder_txtcustomersearch").style.backgroundRepeat = 'no-repeat';
            document.getElementById("ctl00_ContentPlaceHolder_txtcustomersearch").style.backgroundPositionX = 'left';
            //document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = "";
            document.getElementById("autocomplete3").innerHTML = "";
            HideDiv("autocomplete3");


        }

    
</script>


</asp:Content>


