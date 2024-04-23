<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="InventoryStatus.aspx.vb" Inherits="InventoryStatus" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>
 

    
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

<div style="display:none" class="row">
                        <div class="col-md-8">
                          <div class="row form-group">
                                <label class="control-label col-md-3">Delivery Date</label>
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
                                                    <span class="col-md-1">
                                                      </span>
                                         <div class="col-md-5">     
                                                   
                                                  
                                                   
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
                             <td width="40%" >&nbsp;</td>
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
                                        <td> <div class='input-group date' id='datetimepicker3'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="Start Date"     runat="server" ID="txtstart"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div> </td>
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
                        <asp:Label ID="lblQrtBackOrder" Text='<%#Eval("TotalQtyToReceive")%>' runat="server"></asp:Label>
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
       
        doOnLoadNew1();
        doOnLoadNew2();

        var myCalendarto;
        function doOnLoadNew() {
            //alert('test');
           
           // myCalendarto = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtDateTo"]);
            //alert(myCalendar);
           // myCalendarto.setDateFormat("%m/%d/%Y");
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




</asp:Content>


