<%@ Page Title="" Language="VB" MasterPageFile="~/MainMasterInv.master" AutoEventWireup="false" CodeFile="Report1.aspx.vb" Inherits="Report1" %>
<%@ Register TagPrefix="sp1" TagName="ReportHeaderControl" Src="spReportHeader.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    Product Committed Report &nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink5" NavigateUrl="~/InventoryStatus2.aspx"  class="btn green" runat="server">Inventory Status</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Report3.aspx"  class="btn green" runat="server">Products Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Report2.aspx"  class="btn green" runat="server">PO Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Report1.aspx"  class="btn green" runat="server">Product Committed Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Home.aspx" class="btn green" runat="server">Back to Home</asp:HyperLink>
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
                                                    
                                                    <asp:Button runat="server"   ID="SearchButton" CssClass="btn btn-success" Text="Search" />
                                                     <asp:CheckBox ID="chkexculea" Checked="true" Text="Exclude Date" Font-Bold="true" runat="server" />
                            
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

                <asp:HiddenField ID="gvSortDirection" runat="server" />
                <asp:GridView ID="gvItemsList" runat="server" CssClass="table table-striped table-bordered table-hover"
                    AllowSorting="true" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" DataKeyNames="ItemID">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                         
                        <asp:BoundField HeaderText="Item ID" Visible="false" HeaderStyle-HorizontalAlign="Left" DataField="ItemID"  />
                          
                          <asp:TemplateField HeaderText="Group Code"   >
                            <ItemTemplate>
                                <asp:Label  ID="lblGroupCode" Text='<%#Eval("GroupCode")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                         
                         <asp:TemplateField Visible="false" HeaderText="Item ID">
                            <ItemTemplate>
                                <asp:label ID="txtItemID" Text='<%#Eval("ItemID")%>'    runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                        <asp:BoundField HeaderText="Item Name" Visible="false" HeaderStyle-HorizontalAlign="Left" DataField="ItemName"   />
                        <asp:BoundField HeaderText="Variety" HeaderStyle-HorizontalAlign="Left" DataField="Variety"   />
                        
                          <asp:TemplateField HeaderText="Description"   >
                            <ItemTemplate>
                                <asp:Label  ID="lblItemDescription" Text='<%#Eval("ItemDescription")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Grower" HeaderStyle-HorizontalAlign="Left" DataField="Location"   />
                        <asp:TemplateField  Visible="false" HeaderText="Grower Code">
                            <ItemTemplate>
                                <asp:label ID="LBLLocation" Text='<%#Eval("LocationID")%>'  Width="100px"  CssClass="form-control input-sm" runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" DataField="ItemCategoryID"   />
                        <asp:BoundField HeaderText="Variety Code" HeaderStyle-HorizontalAlign="Left" DataField="VarietyID"   />
                         

                          <asp:TemplateField HeaderText="Color"   >
                            <ItemTemplate>
                                <asp:Label  ID="lblItemColor" Text='<%#Eval("ItemColor")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" DataField="FlowerType"   />
                        <asp:BoundField HeaderText="Grade" HeaderStyle-HorizontalAlign="Left" DataField="Grade"   />

                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:label ID="txtstem" Text='<%#Eval("Qty")%>'  Width="50px"  CssClass="form-control input-sm" runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Qty Ordered">
                            <ItemTemplate>
                                <asp:label ID="txtQtyAccepted" BackColor="Wheat" Text='<%#Eval("QtyAccepted")%>'  Width="100px"  CssClass="form-control input-sm" runat="server"></asp:label>
                                <asp:Label ID="lblinlineNumber" Visible="false" runat="server" Text='<%#Eval("inlineNumber")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Price(USD)">
                            <ItemTemplate>
                                <asp:label ID="txtprice"  Text='<%#Eval("Price")%>'  CssClass="form-control input-sm" runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         
                         <asp:TemplateField Visible="false" HeaderText="Delivery Date"  >
                            <ItemTemplate>
					            <asp:label ID="lblArrivalDate"  Text='<%#Convert.ToDateTime(Eval("ArrivalDate")).ToShortDateString()%>' CssClass="form-control input-sm" runat="server"></asp:label>
					   
				            </ItemTemplate>
				     
                        </asp:TemplateField>
                        
                         <asp:TemplateField  HeaderText="Start"  >
                            <ItemTemplate>
					            <%#Convert.ToDateTime(Eval("startavailabledate")).ToShortDateString()%>
					   
				            </ItemTemplate>
				     
                        </asp:TemplateField>
                         <asp:TemplateField   HeaderText="End"  >
                            <ItemTemplate>
					            <%#Convert.ToDateTime(Eval("endavailabledate")).ToShortDateString()%>
					   
				            </ItemTemplate>
				     
                        </asp:TemplateField>
                         
                    </Columns>
                </asp:GridView>
                <br /> 



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

