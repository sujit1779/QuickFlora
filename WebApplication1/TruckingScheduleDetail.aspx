<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false"
    CodeFile="TruckingScheduleDetail.aspx.vb" Inherits="TruckingScheduleDetail" %>

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
   Trucking Schedule Detail
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
                                <asp:Label ID='lblTruckingScheduleDetail' runat='server' Text=" Trucking Schedule Detail"></asp:Label>
                                <asp:Label ID='lbldebug' runat='server' Text=""></asp:Label>
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
                                    Schedule ID
                                    <asp:TextBox ID="txtScheduleID" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                </div> 
                                <div class="col-xs-3">
                                    Origin
                                    <asp:DropDownList ID="drpInventoryOrigin" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Text="--Select Origin--" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                </div> 
                                <div class="col-xs-3">
                                     Ship Method
                                    <asp:DropDownList ID="drpShipMethod" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Text="--Select Ship Method--" Value=""></asp:ListItem>   
                                            </asp:DropDownList> 
                                </div> 
                                <div class="col-xs-3">
                                    Ship To Location
                                    <asp:DropDownList ID="drpLocation" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                                 <asp:ListItem Text="--Select Store Location--" Value=""></asp:ListItem>
                                             </asp:DropDownList>
                                </div> 
                          </div> 
                          <hr />
                        <div class="row">
                                <div class="col-xs-3">
                                    Trucking Day
                                      <asp:DropDownList ID="drpTruckingDay" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="False" >
                                                <asp:ListItem Text="1 [Monday]" Value="1" />
                                                <asp:ListItem Text="2 [Tuesday]" Value="2" />
                                                <asp:ListItem Text="3 [Wednesday]" Value="3" />
                                                <asp:ListItem Text="4 [Thursday]" Value="4" />
                                                <asp:ListItem Text="5 [Friday]" Value="5" />
                                                <asp:ListItem Text="6 [Saturday]" Value="6" />
                                                <asp:ListItem Text="7 [Sunday]" Value="7" />
                                            </asp:DropDownList>
                                </div> 
                                <div class="col-xs-3">
                                    Arrival Day
                                       <asp:DropDownList ID="drpArrivalDay" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="False" >

                                                <asp:ListItem Text="1 [Monday]" Value="1" />
                                                <asp:ListItem Text="2 [Tuesday]" Value="2" />
                                                <asp:ListItem Text="3 [Wednesday]" Value="3" />
                                                <asp:ListItem Text="4 [Thursday]" Value="4" />
                                                <asp:ListItem Text="5 [Friday]" Value="5" />
                                                <asp:ListItem Text="6 [Saturday]" Value="6" />
                                                <asp:ListItem Text="7 [Sunday]" Value="7" />
                                            </asp:DropDownList>                                            
                                </div> 
                                <div class="col-xs-3">
                                    Cut Off Day
                                      <asp:DropDownList ID="drpDayCutOff" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="False" >
                                                <asp:ListItem Text="1 [Monday]" Value="1" />
                                                <asp:ListItem Text="2 [Tuesday]" Value="2" />
                                                <asp:ListItem Text="3 [Wednesday]" Value="3" />
                                                <asp:ListItem Text="4 [Thursday]" Value="4" />
                                                <asp:ListItem Text="5 [Friday]" Value="5" />
                                                <asp:ListItem Text="6 [Saturday]" Value="6" />
                                                <asp:ListItem Text="7 [Sunday]" Value="7" />
                                            </asp:DropDownList> 
                                </div> 
                                <div class="col-xs-3">
                                    CutOff Time
                                    <div class="row">
                                        <div class="col-xs-3">
                                            <asp:DropDownList ID="drpHours" runat="server">
                                                <asp:ListItem Text="01" Value="01" />
                                                <asp:ListItem Text="02" Value="02" />
                                                <asp:ListItem Text="03" Value="03" />
                                                <asp:ListItem Text="04" Value="04" />
                                                <asp:ListItem Text="05" Value="05" />
                                                <asp:ListItem Text="06" Value="06" />
                                                <asp:ListItem Text="07" Value="07" />
                                                <asp:ListItem Text="08" Value="08" />
                                                <asp:ListItem Text="09" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="col-xs-3">
                                            <asp:DropDownList ID="drpMinutes" runat="server">
                                                <asp:ListItem Text="00" Value="00" />
                                                <asp:ListItem Text="05" Value="05" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="15" Value="15" />
                                                <asp:ListItem Text="20" Value="20" />
                                                <asp:ListItem Text="25" Value="25" />
                                                <asp:ListItem Text="30" Value="30" />
                                                <asp:ListItem Text="35" Value="35" />
                                                <asp:ListItem Text="40" Value="40" />
                                                <asp:ListItem Text="45" Value="45" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="55" Value="55" />
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="col-xs-3">
                                            <asp:DropDownList ID="drpAMPM" runat="server">
                                                <asp:ListItem Text="AM" Value="AM" />
                                                <asp:ListItem Text="PM" Value="PM" />
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="col-xs-3">
                                            <asp:DropDownList ID="drpTimeZone" runat="server">
                                                <asp:ListItem Text="CST" Value="CST" />
                                                <asp:ListItem Text="EST" Value="EST" />
                                                <asp:ListItem Text="MST" Value="MST" />
                                                <asp:ListItem Text="PST" Value="PST" />
                                            </asp:DropDownList>
                                        </div> 
                                  </div> 
                                </div> 
                          </div> 
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
