<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false"
    CodeFile="VendorDetails.aspx.vb" Inherits="VendorDetails" %>

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
    Vendor Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <!-- BEGIN PORTLET 2nd Block-->
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption" style="width: 95%;">
                <div class="row">
                    <div class="col-md-2">
                        <div class="text-left">
                            <Ajax:AjaxPanel ID="AjaxPanel3" runat="server">
                                <asp:Label ID='lblCustomer' runat='server' Text="Vendor Detail"></asp:Label>
                            </Ajax:AjaxPanel>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="text-left">
                            <Ajax:AjaxPanel ID="AjaxPanel4" runat="server">
                                <asp:TextBox ID="txtVendorTemp" runat="server"  CssClass="form-control input-sm input-200"></asp:TextBox>
                            </Ajax:AjaxPanel>
                        </div>
                    </div>
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
          

            <div class="row">
                <div class="col-md-6">
                    <Ajax:AjaxPanel ID="AjaxPanel8" runat="server">
                        <div class="table-responsive">
                            <table class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td>
                                            Vendor Name
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtVendorName" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Attention
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox CssClass="form-control input-sm " ID="txtAttention" runat="server" Text=''></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Add 1
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox CssClass="form-control input-sm" ID="txtVendorAddress1" runat="server"
                                                Text=''></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Add 2
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox CssClass="form-control input-sm" ID="txtVendorAddress2" runat="server"
                                                Text=''></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> Vendor Type  </td>
                                        <td>
                                            <asp:DropDownList ID="drpVendorTypeID" CssClass="form-control input-sm" AppendDataBoundItems="true"  runat="server">
                                                <asp:ListItem Text="-Select-" Value=""></asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>
                                            Export to Excel
                                        </td>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkExportoExcel" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </Ajax:AjaxPanel>
                </div>
                <div class="col-md-6">
                    <Ajax:AjaxPanel ID="AjaxPanel9" runat="server">
                        <table class="table table-striped table-hover table-bordered">
                            <tbody>
                                <tr>
                                    <td>
                                        City
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVendorCity" CssClass="form-control input-sm" runat="server" Text=''></asp:TextBox>
                                    </td>
                                    <td>
                                        State/Province
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVendorState" runat="server" CssClass="form-control input-sm"
                                            Text=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Country</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVendorCountry" CssClass="form-control input-sm" runat="server"
                                            Text=''></asp:TextBox>
                                    </td>
                                    <td>
                                        Zip/Postal
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVendorZip" runat="server" CssClass="form-control input-sm" Text=''></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Phone
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control input-sm" ID="txtVendorPhone" runat="server"
                                            Text=''></asp:TextBox>
                                    </td>
                                    <td>
                                        Fax
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="form-control input-sm" ID="txtVendorFax" runat="server" Text=''></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox CssClass="form-control input-sm" ID="txtVendorEmail" runat="server"
                                            Text=''></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email 2nd
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox CssClass="form-control input-sm" ID="txtVendorEmail2" runat="server"
                                            Text=''></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Email 3rd
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox CssClass="form-control input-sm" ID="txtVendorEmail3" runat="server"
                                            Text=''></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </Ajax:AjaxPanel>
                </div>
            </div>
        
        
          
            <div class="row">
                <div class="col-md-12" style="padding-top: 10px;">
                    <Ajax:AjaxPanel ID="AjaxPanel7" runat="server">
                          <table class="table table-striped table-hover table-bordered">
                                <tbody>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnsave"      CausesValidation="false"    CssClass="btn btn-success btn-xs" runat="server"  Text="Save" Width="150" ></asp:Button> 
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                        </td>
                                        <td align="left" >
                                            

                                            <asp:Button ID="btnback"      CausesValidation="false"    CssClass="btn btn-success btn-xs" runat="server"  Text="Back" Width="150" ></asp:Button>

                                        </td>
                                    </tr>
                                </tbody> 
                        </table> 

                    </Ajax:AjaxPanel>
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
