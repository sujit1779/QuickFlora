<%@ Page Title="" Language="VB" MasterPageFile="~/MainMasterInv.master" AutoEventWireup="false" CodeFile="InventoryStatus2.aspx.vb" Inherits="InventoryStatus2" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Inventory Status &nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink5" NavigateUrl="~/InventoryStatus2.aspx"  class="btn green" runat="server">Inventory Status</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Report3.aspx"  class="btn green" runat="server">Products Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Report2.aspx"  class="btn green" runat="server">PO Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Report1.aspx"  class="btn green" runat="server">Product Committed Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Home.aspx" class="btn green" runat="server">Back to Home</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <ajax:AjaxPanel ID="AjaxPanel101" runat="server">



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

<div class="row">
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
                                                   
                                                   <asp:RadioButton ID="rdall" GroupName="date" Text="&nbsp;All" Checked="true" AutoPostBack="true" runat="server" />
                                                    &nbsp;&nbsp;<asp:RadioButton GroupName="date" AutoPostBack="true" ID="rdselected" Text="&nbsp;Only Selected" runat="server" />
                                                   
                                                   
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

                   <div class="row">
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
                                        <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label>
                                </div>
                   </div>


                
                <div class="note note-success margin-bottom-0">

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
                            <asp:DropDownList ID="cmblocationid" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Location--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                         
                      

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
    </ajax:AjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">



<script type="text/javascript">
$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip();   
});
</script>

</asp:Content>


