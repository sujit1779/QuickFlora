<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="RequestDynamicButton.aspx.vb" Inherits="RequestDynamicButton" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
   Requisition Product hot links 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <!-- BEGIN PORTLET 2nd Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption" style="width: 95%;">
                <div class="row">
                    <div class="col-md-5">
                        <div class="text-left">
                            <Ajax:AjaxPanel ID="AjaxPanel3" runat="server">
                                <asp:Label ID='lblerror' Visible="false"  runat='server' Text=""></asp:Label>
                            </Ajax:AjaxPanel>
                        </div>
                    </div>
                     <!-- <div class="col-md-5">
                        <div class="text-left">
                            <Ajax:AjaxPanel ID="AjaxPanel4" runat="server">
                                <asp:TextBox ID="lblTruckingScheduleDetailTemp" runat="server"  CssClass="form-control input-sm input-200"></asp:TextBox>
                            </Ajax:AjaxPanel>
                        </div>
                    </div>-->
                    <div class="col-md-5">
                        <div class="text-right">
                        </div>
                    </div>
                </div>
            </div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body form"  >
          

            <div style="padding:20px;" class="row">
                <div class="col-md-1">
                 </div> 
                <div class="col-md-10">
                    <Ajax:AjaxPanel ID="AjaxPanel8" runat="server">
                        <hr />
                         <div class="row">
                                <div class="col-xs-3">
                                    Button Text
                                    <asp:TextBox ID="txtbutton1" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div> 
                                <div class="col-xs-3">
                                    Family 
                                    <asp:DropDownList ID="drpfamily1" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Text="--Select Origin--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                </div> 
                                <div class="col-xs-3">
                                     Category
                                    <asp:DropDownList ID="drpcategory1" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Text="--Select Ship Method--" Value=""></asp:ListItem>   
                                            </asp:DropDownList> 
                                </div> 
                                <div class="col-xs-3">
                                    
                                </div> 
                          </div> 
                          <hr />
                        
                        <hr />
                        <div class="row">
                                <div class="col-xs-3">
                                    <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                                </div> 
                                <div class="col-xs-3">
                                    <asp:Button ID="btnsave"      CausesValidation="false"    CssClass="btn btn-success btn-xs" runat="server"  Text="Save" Width="150" ></asp:Button> 
                                </div> 
                                <div class="col-xs-3">
                                    <asp:Button ID="btnback"      CausesValidation="false"    CssClass="btn btn-success btn-xs" runat="server"  Text="Cancel" Width="150" ></asp:Button>
                                </div> 
                                <div class="col-xs-3">
                                </div> 
                          </div> 
                    <hr />
                        
                    </Ajax:AjaxPanel>
                </div>               
                <div class="col-md-1">
                 </div> 
            </div>
        
            
        
        </div>
    </div>
    <!-- END PORTLET-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>
    <script type="text/javascript" src="assets/plugins/data-tables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="assets/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function() {


            Search.init();
        });
    </script>
</asp:Content>


