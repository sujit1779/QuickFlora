<%@ Page Language="VB" MasterPageFile="~/MainMaster.master"  AutoEventWireup="false" CodeFile="InventoryFamiliesDetails.aspx.vb" Inherits="InventoryFamiliesDetails" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

    <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Items Family Details
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-group">


                                <div class="col-md-12">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Family ID</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemFamilyID" runat="server" CssClass="form-control input-md" placeholder="ItemFamilyID"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Family Name</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFamilyName" runat="server" CssClass="form-control input-md" placeholder="FamilyName"></asp:TextBox> 
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFamilyDescription" runat="server" class="form-control input-md" placeholder="Family Description"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Long Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFamilyLongDescription" runat="server"  class="form-control input-md" placeholder="FamilyLongDescription" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="dvoff" runat="server" Visible="False"  class="form-group">
                                                <label class="col-md-5 control-label"> Picture URL</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFamilyPictureURL" runat="server" class="form-control input-md" placeholder="FamilyPictureURL" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Active</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkEnableItemFamilyID" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Family Sort Order</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSortOrder" runat="server" class="form-control input-md" placeholder="Sort Order"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Meta Keywords</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtMetaKeywords" runat="server" class="form-control input-md" placeholder="MetaKeywords"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Meta description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtMetadescription" runat="server" class="form-control input-md" placeholder="Metadescription"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <label class="col-md-5 control-label">SEO Title</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSEOTitle" runat="server" class="form-control input-md" placeholder="SEOTitle"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
  
                                 

                            </div>

                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab1Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnTab1SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>
    <%--<script type="text/javascript" src="assests/dist/bootstrap.min.js"></script>
    <script type="text/javascript" src="assests/dist/bootstrap-table.js"></script>--%>
    <%--<script src="assets/dist/bootstrap-table.min.js" type="text/javascript"></script>--%>

    <script type="text/javascript" src="//code.jquery.com/jquery-1.12.4.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            Search.init();
        });
        function clearform() {
            $(':input', aspnetForm).each(function () {
                var type = this.type;
                var tag = this.tagName.toLowerCase(); // normalize case
                if (type == 'text' || type == 'password' || tag == 'textarea' || type == 'file')
                    this.value = "";
                else if (type == 'checkbox' || type == 'radio')
                    this.checked = false;
                else if (tag == 'select')
                    this.selectedIndex = 0;
            });
        }
    </script>

    <!-- To Do
    1) Family - Category Post back refreshing whole page - try to do with Ajax
    
     -->
</asp:Content>


