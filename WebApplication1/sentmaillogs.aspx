<%@ Page Language="VB" MasterPageFile="~/MainMaster.master"  AutoEventWireup="false" CodeFile="sentmaillogs.aspx.vb" Inherits="EnterpriseASPSystem_CustomCompanySetup_sentmaillogs" %>


<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
     Sent Mails Logs 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

 

    <ajax:AjaxPanel ID="AjaxPanel3" runat="server">
          
      <!-- BEGIN PORTLET-->
					<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">Search Options</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body" >
                             <div class="row">
                                    <div class="col-md-6">
                                    	<div class="row form-group">
                                        	<label class="control-label col-md-2">Date Range</label>
                                                        <div class="col-md-10">
                                                        
                                                        
                                                        <div data-date-format="mm/dd/yyyy" data-date="10/11/2012" class="input-group input-sm date-picker input-daterange">
                                                                <div class="input-icon">
                                                                    <i class="fa fa-calendar"></i><asp:TextBox  CssClass="form-control"  placeholder="From"    runat="server" ID="txtDateFrom"> </asp:TextBox>
                                                                </div>
                                                                <span class="input-group-addon">
                                                                to </span>
                                                                <div class="input-icon">
                                                                    <i class="fa fa-calendar"></i><asp:TextBox    CssClass="form-control" placeholder="To"     runat="server" ID="txtDateTo"> </asp:TextBox>
                                                                </div>
                                                            </div>
                                                        
                                                         
                                                            
                                                        </div>
                                       	</div>
                                          
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row form-group">
                                            <label class="col-md-3 control-label">Search By</label>
                                            <div class="col-md-9">
                                            <div class="radio-list">
                                                     <table>
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButton ID="rdAllDates" Checked="true" GroupName="Dates" Text="All Dates"    runat="server" />
                                                            </td>
                                                            <td>
                                                                 <asp:RadioButton ID="rdOrderDates"  GroupName="Dates" Text="Process date"   runat="server" />
                                                            </td>
                                                            
                                                        </tr>
                                                     
                                                     </table>
                                                     
                                                   
                                                    

                                                </div>
                                                
                                            </div>
                                    	</div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4 col-md-2">
                                    		<div class="form-group">

                                                  <asp:DropDownList ID="drpFieldName" CssClass="form-control input-sm select2me" runat="server">
                                                    <asp:ListItem Value="Email_Subject">Email Subject</asp:ListItem>
                                                        <asp:ListItem Value="SMTP_SERVER">SMTP SERVER</asp:ListItem>
                                                        <asp:ListItem Value="From_Email">From Email</asp:ListItem>
                                                        <asp:ListItem Value="To_Email">To Email</asp:ListItem>
                                                        <asp:ListItem Value="Email_Date">Email Date</asp:ListItem>
                                    
                                                    </asp:DropDownList>

                                            </div>
                                     </div>
                                     <div class="col-xs-3 col-md-1">
                                     		<div class="form-group">
                                             
                                          
                                        <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                                        <asp:ListItem Value="=">=</asp:ListItem>
                                                        <asp:ListItem Value="<">&lt;</asp:ListItem>
                                                        <asp:ListItem Value=">">&gt;</asp:ListItem>
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
                                         
                                            <asp:Button ID="btnSearch" CssClass="btn btn-success btn-xs" runat="server" Text="SEARCH" />

                                        </div>
                                        <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label>
                                </div>
                                </div>


                        </div> 
                </div> 

         
 

        <asp:GridView ID="gridPaymentGatewayTransactionLogs" AllowSorting="True" runat="server"  
             CssClass="table table-bordered table-striped table-condensed flip-content" 
     HeaderStyle-Font-Size="14px"
     HeaderStyle-BackColor="#C6DBAD"  
            AutoGenerateColumns="false" AllowPaging="True" PageSize="25">
             
            <Columns>
                   
                  <asp:TemplateField HeaderText="View" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a target="_blank" href='SentMailsDetails.aspx?EmailID=<%# Eval("EmailID")%>'>
                               <img alt="Preview Transactions" src="../../images/Preview-Button.gif" />
                                
                                </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:BoundField DataField="SMTP_SERVER" ItemStyle-HorizontalAlign="Left"  HeaderText="SMTP SERVER" SortExpression="SMTP_SERVER" />                    
                      <asp:BoundField DataField="Email_Date" ItemStyle-HorizontalAlign="Left" HeaderText="Email Date" SortExpression="Email_Date" />       
                    <asp:BoundField DataField="From_Email" ItemStyle-HorizontalAlign="Left" HeaderText="From Email" SortExpression="From_Email" />
                    <asp:BoundField DataField="To_Email" ItemStyle-HorizontalAlign="Left" HeaderText="To Email" SortExpression="To_Email" />
                    <asp:BoundField DataField="CC_Email" ItemStyle-HorizontalAlign="Left" HeaderText="CC Email" SortExpression="CC_Email" /> 
                    <asp:BoundField DataField="Email_Subject" ItemStyle-HorizontalAlign="Left" HeaderText="Email Subject" SortExpression="Email_Subject" />
                    <asp:BoundField DataField="sent" ItemStyle-HorizontalAlign="Left" HeaderText="sent" SortExpression="sent" />
                    
                    <asp:TemplateField HeaderText="Trace" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a target="_blank" href='SentMailTrace.aspx?EmailID=<%# Eval("EmailID")%>'>
                               <img alt="Preview Trace" src="../../images/Preview-Button.gif" />
                                
                                </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                                                                                                
            </Columns>
        </asp:GridView> 
        
 
        
        
    </ajax:AjaxPanel>
    <table>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>


   

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>

    <script>
        jQuery(document).ready(function() {
            Search.init();
        });
    </script>

</asp:Content>

