<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="EmployeeDetail.aspx.vb" Inherits="EmployeeDetail" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">Employee Detail</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <ajax:AjaxPanel ID="AjaxPanel1" runat="server">
        <asp:Panel ID="pnlgrid" runat="server" Visible="true">

            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Employee Details
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="col-md-4">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">ID</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control input-md" placeholder="Employee ID"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Name</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeName" runat="server" class="form-control input-md" placeholder="Employee Name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div  ID="dvEmployeeUserName" runat="server"  class="form-group">
                                            <label class="col-md-5 control-label">User Name</label>
                                            <div class="col-md-6">
                                            <asp:TextBox ID="txtEmployeeUserName"  runat="server" class="form-control input-md" placeholder="Login"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Password</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeePassword" TextMode="Password" runat="server" class="form-control input-md" placeholder="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Employee Type</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="drpEmployeeType" runat="server" class="form-control input-md"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Employee Department</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="drpEmployeeDepartment" runat="server" class="form-control input-md"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Location</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="drpLocation" runat="server" class="form-control input-md"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Hire Date</label>
                                            <div class="col-md-6">
                                                <%--<asp:TextBox ID="s" runat="server" class="form-control input-md"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtEmployeeHireDate" runat="server" class="form-control input-sm date-picker" 
                                                    data-date-format="mm-dd-yyyy" data-date-viewmode="years" data-date="03/25/2017"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">SSN</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeSSN" runat="server" class="form-control input-md"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Email</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeEmail" runat="server" class="form-control input-md" placeholder="Email"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">
                                        <%--<div class="form-group">
                                            <label class="col-md-3 control-label">Email</label>
                                            <div class="col-md-6">
                                                <div class="input-group">
                                                    <input type="text" class="form-control" placeholder="Email Address" value="jenn@aol.com">
                                                    <span class="input-group-addon">
                                                        <i class="fa fa-envelope"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Address 1</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeAddress1" runat="server" class="form-control input-md" placeholder="Address Line 1"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Address 2</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeAddress2" runat="server" class="form-control input-md" placeholder="Address Line 2"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">City</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeCity" runat="server" class="form-control input-md" placeholder="City"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">State</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="drpEmployeeState" runat="server" class="form-control input-md"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Zip</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeZip" runat="server" class="form-control input-md" placeholder="Zip"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Country</label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="drpEmployeeCountry" runat="server" class="form-control input-md"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Phone</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeePhone" runat="server" class="form-control input-md" placeholder="Phone"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Fax</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeFax" runat="server" class="form-control input-md" placeholder="Fax"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">BirthDay</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtEmployeeBirthday" runat="server" class="form-control input-sm date-picker" 
                                                    data-date-format="mm-dd-yyyy" data-date-viewmode="years" data-date="03/25/2017"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Is Admin</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkIsAdmin" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Is Master</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkIsMasterEmployee" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Active</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkActive" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Commissionable</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkCommissionable" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Commission Percent</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtCommissionPercent" runat="server" class="form-control input-md"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Commission Cal Method</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtCommissionCalMethod" runat="server" class="form-control input-md" placeholder=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" ID="dvpicture" runat="server" visible="false">
                                            <label class="col-md-6 control-label">Picture URL</label>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="fuImage" runat="server" CssClass="form fileinput" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Notes</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtNotes" runat="server" class="form-control input-md" TextMode="MultiLine" placeholder=""></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Employee's Module Access
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="col-md-12">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:GridView ID="AccessModuleGrid" DataKeyNames="ModuleID" runat="server"
                                                CssClass="table table-striped table-bordered table-hover"
                                                AllowSorting="false" AllowPaging="true" PageSize="50" AutoGenerateColumns="false">

                                                <Columns>

                                                    <%--<asp:BoundField HeaderText="Employee ID" DataField="EmployeeID" SortExpression="EmployeeID" />--%>
                                                    <asp:BoundField HeaderText="Module ID" DataField="ModuleID" SortExpression="ModuleID" />
                                                    <asp:BoundField HeaderText="Module Name" DataField="ModuleName" SortExpression="ModuleName" />

                                                    <asp:TemplateField HeaderText="Has Module Access">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAccess" Checked='<%# Bind("CheckAccess")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

            <!-- Action Buttons -->
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box">
                        <%--<div class="portlet-title">
                            <div class="caption">
                                
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>--%>
                        <div class="portlet-body form">
                            <div class="form-horizontal" role="form">
                                <div class="form-body">
                                    <div class="form-actions right">
                                        <asp:Label ID="lblStatus" runat="server" class="col-md-5 control-label" ForeColor="Red" Visible="false"></asp:Label>
                                        <asp:Button ID="btnSubmit" runat="server" class="btn green" Text="SUBMIT"/>
                                        <%--<asp:Button ID="btnClear" runat="server" class="btn grey" Text="CLEAR" OnClientClick="clearform();" CausesValidation="false" />--%>
                                        <a id="btnClear" runat="server" class="btn grey" onclick="clearform();">CLEAR</a>
                                        <asp:Button ID="btnUpdate" runat="server" class="btn green" Text="UPDATE" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn grey" Text="CANCEL" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>



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
        jQuery(document).ready(function() {
            Search.init();
        });
        function clearform() {
            $(':input', aspnetForm).each(function() {
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
        //function validate() {
        //    var id = document.getElementById("ctl00_ContentPlaceHolder_txtEmployeeID").value;
        //    //var regEx = "/[^a-zA-Z0-9]/";          

        //    if (/[!@#$%\^\&\*`~,.<>;':"\/\[\]\|{}()=_+-]/.test(id)) {
        //        alert("Special Chars not allowed");
        //        return false;
        //    }
        //    return true;
        //}
    </script>


    <script>


            
    </script>

</asp:Content>

