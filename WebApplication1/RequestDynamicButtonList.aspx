<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="RequestDynamicButtonList.aspx.vb" Inherits="RequestDynamicButtonList" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <div class="row">
        <div class="col-md-6">
            Requisition Product hot links 
        </div>
        <div class="col-md-6">
            
            <div class="btn-group  pull-right">
                     
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
                        &nbsp;List
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
        </div>

        <div class="portlet-body">
             
        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>

        <asp:GridView ID="categorylist" runat="server" AutoGenerateColumns="False"
            DataKeyNames="inlinenumber" AllowSorting="true" CssClass="table table-bordered table-striped table-condensed flip-content">
            <Columns>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1"  CausesValidation="false"
                            ToolTip="Edit" ImageUrl="~/Images/edit.gif" runat="server" CommandName="Edit" />
                    </ItemTemplate>
                    <ItemStyle Width="3px" HorizontalAlign="Center" />
                </asp:TemplateField>
                 
                <asp:BoundField HeaderText="Button Name" ItemStyle-HorizontalAlign="Left" DataField="ButtonName" />
                <asp:BoundField HeaderText="Family" ItemStyle-HorizontalAlign="Left" DataField="ItemFamilyID" />
                <asp:BoundField HeaderText="Category" ItemStyle-HorizontalAlign="Left" DataField="ItemCategoryID" />
                 
                
            </Columns>
        </asp:GridView>
    

        </div> 

</div> 
        
    
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


