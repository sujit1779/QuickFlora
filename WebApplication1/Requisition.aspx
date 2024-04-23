<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="Requisition.aspx.vb" Inherits="Requisition" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Requisition
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
 

     <Ajax:AjaxPanel ID="AjaxPanel101" runat="server">

        <!-- BEGIN PORTLET 1st Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                &nbsp;Requisition Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
               <div class="col-md-2">
               </div> 
                <div class="col-md-4">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table3" class="table table-striped table-hover table-bordered">
                            
                             <tr>
                                <td width="110" height="15" valign="middle">
                                    Store-Location
                                </td>
                                <td width="101" height="15">
                                   <asp:DropDownList ID="cmblocationid" CssClass="form-control"  runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="110" height="15" valign="middle">
                                   Requested By
                                </td>
                                 <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPaneRequestedBy" runat="server">
                                                <asp:TextBox ID="txtRequestedBy" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>

                            <tr>
                                <td width="110" height="12">
                                    Product 
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel2" runat="server">
                                                <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
                          
                            <tr>
                                <td width="110" height="12">
                                    Type 
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel1" runat="server">
                                                <asp:TextBox ID="txtType" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="110" height="12">
                                    PO Number 
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel3" runat="server">
                                                <asp:TextBox ID="txtPONumber" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
                            
                             <tr>
                                <td width="110" height="12">
                                    Quanity On Hand
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel4" runat="server">
                                                <asp:TextBox ID="txtQOH" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>

                             
                             <tr>
                                <td width="110" height="12">
                                    Pre Sold
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel5" runat="server">
                                                <asp:TextBox ID="txtPreSold" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
 

                             <tr>
                                <td width="110" height="12">
                                    Requested
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel8" runat="server">
                                                <asp:TextBox ID="txtRequested" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>

                             <tr>
                                <td width="110" height="12">
                                   Color Verity
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel7" runat="server">
                                                <asp:TextBox ID="txtColorVerity" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
                             
                           <tr>
                                <td width="110" height="12">
                                  Po Notes
                                </td>
                                <td width="101" height="12">
                                    <ajax:ajaxpanel id="AjaxPanel9" runat="server">
                                                <asp:TextBox ID="txtPoNotes" runat="server" CssClass="form-control"  ></asp:TextBox>
                                   </ajax:ajaxpanel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td width="110" height="12" valign="middle">
                                     
                                </td>
                                <td width="101" height="12">
                                    <asp:Button ID="btnRequisition"   CssClass="btn btn-success btn-xs" runat="server" Text="Submit" />
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
                 <div class="col-md-2">
                    <div class="text-center" style="padding-top:50px;">
                        <asp:Image ID="ImgRetailerLogo" CssClass="img-rounded" ImageUrl="" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- END PORTLET-->

     
</Ajax:AjaxPanel> 

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

