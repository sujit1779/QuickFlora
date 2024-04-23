<%@ Page Title="" Language="VB" MasterPageFile="~/MainMasterInv.master" AutoEventWireup="false" CodeFile="Report3.aspx.vb" Inherits="Report3" %>
<%@ Register TagPrefix="sp1" TagName="ReportHeaderControl" Src="spReportHeader.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    Products Shipment Report &nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink5" NavigateUrl="~/InventoryStatus2.aspx"  class="btn green" runat="server">Inventory Status</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Report3.aspx"  class="btn green" runat="server">Products Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Report2.aspx"  class="btn green" runat="server">PO Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Report1.aspx"  class="btn green" runat="server">Product Committed Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Home.aspx" class="btn green" runat="server">Back to Home</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    
<div id="Div1" >
				<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">Search Options</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body"  >
					    <div class="row">
					                <div class="col-md-3">
					                      <asp:Repeater ID="DataGrid1" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             
                                <asp:Image ID="Image1" runat="server" border="0" ImageUrl='<%# PopulateImage(DataBinder.Eval(Container, "DataItem.CompanyLogoUrl")) %> '>
                                </asp:Image>
                                <br />
                                <%# DataBinder.Eval(Container, "DataItem.CompanyName") %> 
                                <br />
                                Executed By <%# EmployeeID & " on " & Today %> 
                                    

                                                    
                                    
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                        </AlternatingItemTemplate>
                                         
                                    </asp:Repeater>
					                </div> 
                                    <div class="col-md-9">
                                        <div class="row">
                                        <div class="col-md-6">    
                                    	        <div class="row form-group">
                                                	 
                                                                
                                                                <div data-date-format="mm/dd/yyyy" data-date="10/11/2012" class="input-group input-sm date-picker input-daterange">
                                                                        <div class="input-icon">
                                                                            <i class="fa fa-calendar"></i><asp:TextBox  CssClass="form-control"  placeholder="From"    runat="server" ID="txtDeliveryDate"> </asp:TextBox>
                                                                        </div>
                                                                        <span class="input-group-addon">
                                                                        to </span>
                                                                        <div class="input-icon">
                                                                            <i class="fa fa-calendar"></i><asp:TextBox    CssClass="form-control" placeholder="To"     runat="server" ID="txtDeliveryDateTO"> </asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                
                                                                  
                                       	        </div>
                                       	       
                                       	        
                                        </div> 
                                        <div class="col-md-6">
                                                <div class="row form-group">
                                                    
                                                  
                                                         <table>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;</td>
                                                            <td>
                                                                 <asp:Button runat="server"   ID="SearchButton" CssClass="btn btn-success" Text="Search" />
                                                            </td>
                                                            <td>&nbsp;&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdAllDates" Checked="true" GroupName="Dates" Text="All Dates"    runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdOrderDates"  GroupName="Dates" Text=" PO Date"   runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdDeliveryDates"  GroupName="Dates" Text="Arrival Date"   runat="server" /></td>
                                                        </tr>
                                                     
                                                     </table>
                                                      
                                                 </div>    
                                            
                                        </div> 
                                        
                                        </div>   
                                        
                                    </div>
                                    
                                </div>
                           
					    <div id="dv123" runat="server" visible="false" class="row">
					        <div class="col-md-3">
					          
                                                    <asp:RadioButton ID="optOrderDate" Text="By Order Date" GroupName="Date" runat="server" />
                                                    <asp:RadioButton ID="OptDeliveryDate" Text="By Delivery Date" GroupName="Date" runat="server" />&nbsp;

					        </div> 
					        <div class="col-md-9">
    					            <div class="row">
					                <div class="col-md-6">
					                <div class="row form-group">
                                       	                Location:&nbsp;
					                                 <asp:DropDownList ID="cmblocationid" AppendDataBoundItems=true  Width="150"  CssClass="form-control input-sm select2me"  runat="server"  >
                                                     <asp:ListItem Text="All Location" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                </div> 
					                    </div> 
					                    <div class="col-md-6">
                					    <div class="row form-group">
                                            Order Status:&nbsp;
                                                <asp:DropDownList CssClass="form-control input-sm select2me"  Width="150"  ID="drpordrtype"  runat="server">
                                                    <asp:ListItem Text="Booked" Value="Booked"></asp:ListItem>
                                                    <asp:ListItem Text="Invoiced" Value="Invoiced"></asp:ListItem>
                                                </asp:DropDownList>
                                          </div> 
					                    </div> 
					                </div> 
    					    
					        </div> 
					    </div> 
						
						</div> 
				</div> 
     
    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
    <hr>
        <table width="100%" align="center"  border="0" >
            <tr>
                <td width="2%" valign="top">

                </td>
            <td width="96%" valign="top">
     
 <div  style='z-index:90;height:100%' class="table-responsive">   
 
 <asp:GridView ID="OrderHeaderGrid" AllowSorting="True" runat="server" DataKeyNames="PurchaseNumber"  CssClass="table table-bordered table-striped table-condensed flip-content"     AutoGenerateColumns="false" AllowPaging="True" PageSize="25">
  
            <Columns>
               
                  
                
                 <asp:TemplateField HeaderText="PO #"  >
                    <ItemTemplate>
                       <asp:Label ID="lblstrik" Text='*' ForeColor="Orange" Visible="false" runat="server"></asp:Label> <asp:Label ID="lblOrderNumber" Text='<%#Eval("PurchaseNumber")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                     
                </asp:TemplateField>
                
                   <asp:TemplateField HeaderText="PO Date"  >
                    <ItemTemplate>
					    <%#Convert.ToDateTime(Eval("PurchaseDate")).ToShortDateString()%>
					   
				    </ItemTemplate>
				     
                </asp:TemplateField>
                   
                    
                    <asp:TemplateField HeaderText="View"  ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a target="_blank" class="btn btn-warning btn-xs tooltips" href='https://reports.quickflora.com/reports/scripts/POReport.aspx?prev=True&CompanyID=<%=CompanyID %>&DivisionID=<%=DivisionID %>&DepartmentID=<%=DepartmentID %>&PurchaseNumber=<%# Eval("PurchaseNumber")%>'>
                                <i class="fa fa-search"></i>
                                
                                </a>
                        </ItemTemplate>
                        
                    </asp:TemplateField> 
                     
                <asp:TemplateField HeaderText="Vendor ID"   >
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerID" Text='<%#Eval("VendorID")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                     
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vendor Name"   >
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerFirstName" Text='<%#Eval("VendorName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Arrival . Date"  >
                    <ItemTemplate>
					    <%#Convert.ToDateTime(Eval("OrderShipDate")).ToShortDateString()%>
					   
				    </ItemTemplate>
				     
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="ItemID"   >
                    <ItemTemplate>
					    <%#Eval("ItemID")%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
               
                
                  
                <asp:TemplateField HeaderText="Item Qty"   >
                    <ItemTemplate>
					    <%#Eval("OrderQty")%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Price" >
                    <ItemTemplate>
                       <%#String.Format("{0:N2}", Eval("ItemUnitPrice"))%>
                         
                    </ItemTemplate>
                    
                </asp:TemplateField>  
                
                 <asp:TemplateField HeaderText="Item Total"  >
                    <ItemTemplate>
					    <%#String.Format("{0:N2}", Eval("ItemTotal"))%>
				    </ItemTemplate>
				     
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Color"   >
                    <ItemTemplate>
					    <%#Eval("Color")%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Delivery Method"   >
                    <ItemTemplate>
                        <asp:Label ID="lblShipMethodID" Text='<%#Eval("ShipMethodID")%>' runat="server"></asp:Label>

				    </ItemTemplate>
				    
                </asp:TemplateField>
                
                 
                
                 

                
            </Columns>
        </asp:GridView>
  
 </div> 
          
   

            </td>
             <td width="2%" valign="top">

             </td>
            </tr>
        </table>

 
        </div>   

      <div align="center" >
           <br />
            <input  type="button" value="Print To Printer" class="btn btn-success" onclick="JavaScript:printPartOfPage('Div1');">   
            <br />
            <br />
          
          <div id="Div2"   class="row">
                <div style="text-align:left;" class="col-md-12">
                
                     
                </div> 
            </div> 
    </div>
      
 
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
    
<script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript"  src="assets/admin/pages/scripts/search.js"></script>

<script>
    jQuery(document).ready(function() {

        Search.init();
    });
</script>



<script type="text/javascript">
<!--
    function printPartOfPage(elementId) {


      
        var printContent = document.getElementById(elementId);
        var windowUrl = 'about:blank';
        var uniqueName = new Date();
        var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');

        printWindow.document.write(printContent.innerHTML);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    }
// -->
</script>

</asp:Content>

