<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="InventoryReceiveDetails.aspx.vb" Inherits="InventoryReceiveDetails" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Merchandise Receive
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <%--<asp:ServiceReference Path="~/EnterpriseASPAR/CustomOrder/AutoCompleteAjax.asmx" />--%>
        </Services>
    </asp:ScriptManager>

           
        <!-- BEGIN PORTLET 1st Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                &nbsp;Merchandise Transfer Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="text-center" style="padding-top:50px;">
                        <asp:Image ID="ImgRetailerLogo" CssClass="img-rounded" ImageUrl="" runat="server" />
                    </div>
                </div>
                <div class="col-md-4">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table3" class="table table-striped table-hover table-bordered">
                            
                            <tr>
                                <td width="110" height="12">
                                    Transfer Number  
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel2" runat="server">
                                                <asp:TextBox ID="txtTransferNumber" runat="server" Width="150px"></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
                            <tr id="tr111" runat="server" visible="false"  >
                                <td width="110" height="12" valign="middle">
                                     
                                </td>
                                <td width="101" height="12">
                                   <asp:Button ID="btnGetTransferDetail" CssClass="btn blue btn-block"  Width="150" runat="server" Text="Get Details"  
                                        OnClientClick="javascript:return CheckTransferNumber();"/>
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Employee
                                </td>
                                <td width="101" height="15">
                                  <asp:DropDownList ID="drpTansferByEmployee" CssClass="form-control"   runat="server" Enabled="false"></asp:DropDownList>
                                </td>
                            </tr>

                              <tr>
                                <td width="110" height="15" valign="middle">
                                    
                                </td>
                                <td width="101" height="15">
                                    
                                </td>
                            </tr>


                             <tr>
                                <td width="110" height="15" valign="middle">
                                    Transfer Date
                                </td>
                                <td width="101" height="15">
                                    <div class="input-group input-sm date-picker input-daterange" data-date="07/03/2015" data-date-format="mm/dd/yyyy">
                                    
                                     <div class="input-icon">
                                       <table>
                                            <tr>
                                                <td> <i class="fa fa-calendar"></i></td>
                                                <td>
                                                   <asp:TextBox Width="200" ID="txtTransferDate" CssClass="form-control"   runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                       </div>
                                </div> 

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
            <div class="caption">
                &nbsp;Transfer Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                
                <div class="col-md-6">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table1" class="table table-striped table-hover table-bordered">
                            
                            
                            <tr>
                                <td width="110" height="12" valign="middle">
                                       <span style="display: inline-block;">Transfer From</span> 
                                
                                </td>
                                <td width="101" height="12">
                                     <span>
                                   <asp:DropDownList ID="drpTansferFromLocaton" CssClass="form-control"   runat="server" Enabled="false"></asp:DropDownList>
                                   </span>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                   <span style="display: inline-block;">Approved By </span>
                                </td>
                                <td width="101" height="15">
                                    
                                          <span>  
                                          
                                             <asp:DropDownList ID="drpApprovedByEmployee" CssClass="form-control"   runat="server" Enabled="false"></asp:DropDownList>
                                          
                                          </span>
                                    
                                </td>
                            </tr>
                             <tr>
                                <td width="110" height="15" valign="middle">
                                   <span style="display: inline-block;">Received By </span>
                                </td>
                                <td width="101" height="15">
                                    
                                          <span>  
                                          
                                              <asp:DropDownList ID="drpReceivedByEmployee" CssClass="form-control"   runat="server"></asp:DropDownList>
                                          
                                          </span>
                                    
                                </td>
                            </tr>
                         
                           

                                    
                        </table>
                    </div>
                    <!-- END FORM-->
                </div>
                <div class="col-md-6">
                    
                      <table id="table2" class="table table-striped table-hover table-bordered">
                            
                            
                            <tr>
                                <td width="110" height="12" valign="middle">
                                     <span>Transfer To</span> 
                                
                                </td>
                                <td width="101" height="12">
                                     <span>
                                        <asp:DropDownList ID="drpTransferToLocaton" CssClass="form-control"   runat="server" Enabled="false"></asp:DropDownList>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                     
                                <span>Approved At</span> 
                              
                               
                                </td>
                                <td width="101" height="15">
                                    
                                         <span>  
                                         
                                            <asp:TextBox ID="txtApprovedTime" CssClass="form-control"   runat="server" Enabled="false"></asp:TextBox>
                                         
                                         </span>
                                     
                                </td>
                            </tr>
                           
                            <tr>
                                <td width="110" height="15" valign="middle">
                                   <span style="display: inline-block;">Received Date </span>
                                </td>
                                <td width="101" height="15">
                                    
                                         
                                          
                                                  
                                                <div class="input-group input-sm date-picker input-daterange" data-date="07/03/2015" data-date-format="mm/dd/yyyy">
                                                      <div class="input-icon">
                                                            <table>
                                                                <tr>
                                                                    <td> <i class="fa fa-calendar"></i></td>
                                                                    <td><asp:TextBox ID="txtReceivedDate" CssClass="form-control"   runat="server"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                         </div>
                                                    </div> 
                                
                                          
                                          
                                    
                                </td>
                            </tr>
                                    
                        </table>


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
                                          
								    </div>
                                    <div class="col-md-5">
									        <div class="text-right" >
                                                 <asp:LinkButton ID="ItemSearch" Font-Underline="true" Visible="false" runat="server" Text="Advanced Item Search" OnClientClick="javascript:return checkPopupItemSearch();"></asp:LinkButton>
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
                                                    <asp:TextBox ID="txtitemsearch" CssClass="form-control input-circle-right" runat="server" onKeyUp="SendQuery2(this.value);"></asp:TextBox>
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
  
                                    
                                     

                              <asp:HiddenField ID="hdSortDirection" runat="server" />
                

                <asp:GridView ID="InventoryTransferItemGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-condensed flip-content"  
                    DataKeyNames="RowID" AllowSorting="true">
                    <Columns>
                        
                      

                        <asp:TemplateField HeaderText="Row ID"  Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRowID" Text='<%# Bind("RowID")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                           
                             
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item ID"  >
                            <ItemTemplate>
                                <asp:Label ID="lblItemID" Text='<%# Bind("ItemID") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item Name" >
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" Text='<%# Bind("ItemName") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Transfer Qty"  >
                            <ItemTemplate>
                                <asp:Label ID="lblQty" Text='<%# Eval("TransferQty")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Received Qty" >
                            <ItemTemplate>
                                
                                <asp:TextBox ID="txtUpdateReceivedQty" Text='<%# Eval("ReceivedQty") %>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit Price" >
                            <ItemTemplate>
                                <asp:Label ID="lblUnitPrice" Text='<%#String.Format("${0:N2}", Eval("UnitPrice")) %>'
                                    Width="100" runat="server"></asp:Label>
                            </ItemTemplate>
                           
                           
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Addional Notes"  >
                            <ItemTemplate>
                                <asp:Label ID="lblAdditionalNotes" Text='<%# Eval("AddtionalItemNotes")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>


                                 
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
									        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
								    </div>
								    <div class="col-md-4">
                                              
                                      <table  class="table table-bordered table-striped table-condensed flip-content">
                                        <tr>
                                            <td> 
                                                    <asp:Button ID="btnSave" runat="server" Text="Save Receive" CssClass="btn blue btn-block"  OnClientClick="javascript:return CheckValues();" />
                                            </td>
                                            <td> &nbsp;&nbsp; </td>
                                            <td> 
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn blue btn-block"  />
                                              </td>
                    
                                        </tr>
                                    </table>
								    </div>
                                    <div class="col-md-4"> 
									      
                                    <table  class="table table-bordered table-striped table-condensed flip-content">
                    
                                           <tr>
                                                    <td width="110" height="15" valign="middle">
                                                       Total # of Items
                                                    </td>
                                                    <td width="101" height="15">
                                                       <asp:Label ID="lblTotalNumberOfItems" CssClass="form-control"  runat="server"></asp:Label>
                                                    </td>
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
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript"  src="assets/admin/pages/scripts/search.js"></script>

<script>
    jQuery(document).ready(function() {

        Search.init();
    });
</script>

</asp:Content>

