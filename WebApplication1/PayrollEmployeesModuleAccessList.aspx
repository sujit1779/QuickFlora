<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false"
    CodeFile="PayrollEmployeesModuleAccessList.aspx.vb" Inherits="PayrollEmployeesModuleAccessList" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Import Namespace="CrystalDecisions.CrystalReports" %>
<%@ Import Namespace="CrystalDecisions.Shared" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Employees Module Access
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <!-- BEGIN PORTLET 1st Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                &nbsp;Module Details</div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="text-center" style="padding-top: 10px;">
                        <asp:Image ID="ImgRetailerLogo" CssClass="img-rounded" ImageUrl="" runat="server" />
                    </div>
                </div>
                <div class="col-md-4">
                    <!-- BEGIN FORM-->
                    <div class="form-body">
                        <table id="table3" class="table table-striped table-hover table-bordered">
                           
                            <tr>
                                <td width="110" height="15" valign="middle">
                                    Employee
                                </td>
                                <td width="101" height="15">
                                    <asp:DropDownList CssClass="form-control input-xs" ID="drpEmployeeID" runat="server" AppendDataBoundItems="true"
                                        TabIndex="3">
                                        <asp:ListItem Value="" Text="--Select--" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td width="110" height="15" colspan="2" valign="middle">
                                     
                               
                                    <Ajax:AjaxPanel ID="AjaxPanel270" runat="server">
                                        <asp:DropDownList CssClass="form-control input-xs" Visible="false" ID="drpQuickfloraModules" runat="server" AppendDataBoundItems="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="" Text="--Select--" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </Ajax:AjaxPanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                              <asp:GridView ID="grdModule" AllowSorting="True" runat="server" 
                                    AutoGenerateColumns="false" AllowPaging="False" CssClass="table table-bordered table-striped table-condensed flip-content">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkselect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                     

                                         <asp:TemplateField HeaderText="Module ID" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccessModule" Text='<%#Eval("ModuleID")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Module Name" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccessModulename" Text='<%#QuickfloraModules(Eval("ModuleID"))%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                             
                                        
                                    </Columns>
                                </asp:GridView>
                                </td>
                               
                            </tr>
                            <tr>
                                <td width="110" height="15" valign="middle">
                                </td>
                                <td width="101" height="15">
                                    <asp:Button ID="btnadd" runat="server" Text="Allow Access" CssClass="btn btn-success btn-xs" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- END FORM-->
                </div>
                <div class="col-md-4">
                    <div class="alert alert-info">
                        System Wide Messages</div>
                    <asp:Label ID="lblSystemWM" Height="100" runat="server" Font-Size="9pt"></asp:Label>
                </div>
            </div>
        
         <div id="dvitems" class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gridAccessModule" AllowSorting="True" runat="server" 
                        AutoGenerateColumns="false" AllowPaging="False" CssClass="table table-bordered table-striped table-condensed flip-content">
                        <Columns>
                            <asp:TemplateField HeaderText="Employee ID" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeID" Text='<%#Eval("EmployeeID")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeName" Text='<%#Employeename(Eval("EmployeeID"))%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Module ID" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAccessModule" Text='<%#Eval("AccessModule")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Module Name" >
                                <ItemTemplate>
                                    <asp:Label ID="lblAccessModulename" Text='<%#QuickfloraModules(Eval("AccessModule"))%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnunpickorder" runat="server" Text="Revoke Access" CssClass="btn btn-success btn-xs"
                                        Width="150" CommandName="Update" CommandArgument='<%#Eval("EmployeeID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <Ajax:AjaxPanel ID="AjaxPanel14" runat="server">
                <asp:Label ID="lblErrorText" TabIndex="63" runat="server" Visible="false"></asp:Label>
            </Ajax:AjaxPanel>
        
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
    <script type="text/javascript" src="assets/scripts/table-editable3.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function() {

            TableEditable.init();
            Search.init();
        });
    </script>
</asp:Content>
