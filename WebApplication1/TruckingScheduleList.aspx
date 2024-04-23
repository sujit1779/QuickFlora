<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="TruckingScheduleList.aspx.vb" Inherits="TruckingScheduleList" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core"
    TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls"
    TagPrefix="ctls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="row">
        <div class="col-md-6">
            Trucking Schedule
        </div>
        <div class="col-md-6">
            
            <div class="btn-group  pull-right">
                    <a href="TruckingScheduleDetail.aspx" class="btn green">Create New Trucking Schedule <i class="fa fa-plus"></i>
                    </a>
                </div>
        </div> 
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <script type="text/javascript">

        function deleteConfirmForSchedule() {
            if (confirm("Are you sure to delete this Trucking Schedule?")) {
                return true;
            } else {
                return false;
            }
        }

    </script>
    
    <!-- END PORTLET-->
    <ajax:AjaxPanel ID="AjaxPanel077" runat="server">
        <asp:HiddenField ID="hdSortDirection" runat="server" />
        
        <div class="portlet box green">
        <div class="portlet-title">
                    <div class="caption">
                        &nbsp;Search
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
        </div>

        <div class="portlet-body">

            
                    <div class="note note-success margin-bottom-0">

                        <div class="row">
                            <div class="col-xs-4">
                                Origin:
                             <asp:DropDownList ID="drpOrigin" CssClass="form-control input-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                 <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                             </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                Location 
                            <asp:DropDownList ID="cmblocationid" CssClass="form-control input-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Location--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                            </div>

                            <div class="col-xs-4">
                                Ship Method
                            <asp:DropDownList ID="drpShipMethod" CssClass="form-control input-sm" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                               
                            </asp:DropDownList>
                            </div>

                        </div>

                    </div>


        </div> 

</div> 
        
        <asp:GridView ID="TruckingScheduleGrid" runat="server" AutoGenerateColumns="False"
            DataKeyNames="ScheduleID" AllowSorting="true" CssClass="table table-bordered table-striped table-condensed flip-content">
            <Columns>
                <asp:TemplateField HeaderText="Details">
                    <ItemTemplate>
                        <a target="_blank" class="btn btn-primary btn-sm" href="https://secure.quickflora.com/POM/DeliveryScheduleDetail.aspx?ScheduleID=<%#Eval("ScheduleID")%>">
                            <i class="fa fa-eye" aria-hidden="true"></i>

                        </a>
                    </ItemTemplate>
                    <ItemStyle Width="3px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1"  CausesValidation="false"
                            ToolTip="Edit" ImageUrl="~/Images/edit.gif" runat="server" CommandName="Edit" />
                    </ItemTemplate>
                    <ItemStyle Width="3px" HorizontalAlign="Center" />
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="Schedule ID" ItemStyle-HorizontalAlign="Right" DataField="ScheduleID" />              
                <asp:TemplateField HeaderText="Origin">
                    <ItemTemplate>
                        <asp:Label ID="lblOrigin" runat="server" Text='<%#Eval("OriginName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Ship Method ID"  ItemStyle-HorizontalAlign="Left" DataField="ShipMethodID" />               
                 <asp:TemplateField HeaderText="Ship Method Description">
                    <ItemTemplate>
                        <asp:Label ID="lblShipMethodDescription" runat="server" Text='<%#Eval("ShipMethodDescription")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Ship To Location">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationID" runat="server" Text='<%#Eval("LocationID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Trucking Day" ItemStyle-HorizontalAlign="Left" DataField="TruckingDayText" />
                <asp:BoundField HeaderText="Arrival Day" ItemStyle-HorizontalAlign="Left" DataField="ArrivalDayText" />
                <asp:BoundField HeaderText="Cut Off Day" ItemStyle-HorizontalAlign="Left" DataField="DayCutoffText" />
                <asp:BoundField HeaderText="Cut Off Time" ItemStyle-HorizontalAlign="Left" DataField="CutOffTime" />
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2"   CausesValidation="false"
                            OnClientClick="return confirm('Are you sure you want to delete this record?');"
                            ToolTip="Delete" ImageUrl="~/Images/Delete.gif" runat="server" CommandName="Delete" />
                    </ItemTemplate>
                    <ItemStyle Width="3px" HorizontalAlign="Center" />
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </ajax:AjaxPanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>
    <script>
        jQuery(document).ready(function () {

            Search.init();
        });
    </script>
</asp:Content>
