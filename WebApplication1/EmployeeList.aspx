<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="EmployeeList.aspx.vb" Inherits="EmployeeList" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">Employee List</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <ajax:AjaxPanel ID="AjaxPanel3" runat="server">
        <asp:Panel ID="pnlgrid" runat="server" Visible="true">
            <!-- BEGIN PORTLET-->
            <%--<div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">Search Options</div>
                    <div class="tools"><a href="javascript:;" class="collapse"></a></div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-xs-4 col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="drpFieldName" CssClass="form-control input-sm select2me" runat="server">
                                    <asp:ListItem Value="CustomerLastName">Customer Last Name</asp:ListItem>
                                    <asp:ListItem Value="CustomerFirstName">Customer First Name</asp:ListItem>
                                    <asp:ListItem Value="CustomerCompany">Customer Company</asp:ListItem>
                                    <asp:ListItem Value="OrderNumber">Order Number</asp:ListItem>
                                    <asp:ListItem Value="OrderTypeID">Order Type</asp:ListItem>
                                    <asp:ListItem Value="CustomerID">Customer ID</asp:ListItem>
                                    <asp:ListItem Value="OrderShipDate">Delivery Date</asp:ListItem>
                                    <asp:ListItem Value="Total">Total</asp:ListItem>
                                    <asp:ListItem Value="ShippingLastName">Ship To Last Name</asp:ListItem>
                                    <asp:ListItem Value="ShippingFirstName">Ship To First Name</asp:ListItem>
                                    <asp:ListItem Value="ShippingCity">Ship To City</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-3 col-md-1">
                            <div class="form-group">
                                <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                    <asp:ListItem Value="Like">Like</asp:ListItem>
                                    <asp:ListItem Value=">">&gt;</asp:ListItem>
                                    <asp:ListItem Value="<">&lt;</asp:ListItem>
                                    <asp:ListItem Value="=">=</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-xs-5 col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtSearchExpression" CssClass="form-control input-sm" runat="server"></asp:TextBox>
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
            </div>--%>
            <!-- END PORTLET-->

            <div class="table-toolbar">
                <div class="btn-group">
                    <a href="EmployeeDetail.aspx" class="btn green">Add New <i class="fa fa-plus"></i>
                    </a>
                </div>
                <%--<div class="btn-group pull-right">
                    <button class="btn dropdown-toggle" data-toggle="dropdown">
                        Tools <i class="fa fa-angle-down"></i>
                    </button>
                    <ul class="dropdown-menu pull-right">
                        <li><a href="#">Print</a> </li>
                        <li><a href="#">Save as PDF</a> </li>
                        <li><a href="#">Export to Excel</a> </li>
                    </ul>
                </div>--%>
            </div>

            <div class="table-responsive">
                <asp:HiddenField ID="gvSortDirection" runat="server" />

                <asp:GridView ID="EmployeeGrid" AllowSorting="True" runat="server" DataKeyNames="EmployeeID"
                    CssClass="table table-bordered table-striped table-condensed flip-content" 
                    HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#C6DBAD" AutoGenerateColumns="false" AllowPaging="True" PageSize="200">

                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <a href="<%# String.Format("EmployeeDetail.aspx?EmployeeID={0}", Eval("EmployeeID"))%>" 
                                    class="btn default btn-xs purple"><i class="fa fa-edit"></i> Edit</a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <%--<asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <a href="<%# String.Format("EmployeeDetail.aspx?EmployeeID={0}", Eval("EmployeeID"))%>" 
                                    class="btn default btn-xs purple"><i class="fa fa-trash"></i> Delete</a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>                                
                                <asp:LinkButton ID="ImageButton11" CausesValidation="false" 
                                   CssClass="btn default btn-xs red" 
                                    runat="server" CommandName="Delete"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Employee Name" DataField="EmployeeName" SortExpression="EmployeeName" />

                        <asp:BoundField HeaderText="Employee ID" DataField="EmployeeID" SortExpression="EmployeeID" />
                        <asp:BoundField HeaderText="Employee Type" DataField="EmployeeTypeID" SortExpression="EmployeeTypeID" />
                        <asp:BoundField HeaderText="User Name" DataField="EmployeeUserName" SortExpression="EmployeeUserName" />
                         

                        
                        <%--<asp:BoundField HeaderText="ActiveYN" DataField="ActiveYN" SortExpression="ActiveYN" />--%>
                        <asp:BoundField HeaderText="Hire Date" DataField="HireDate" SortExpression="HireDate" />
                        <asp:BoundField HeaderText="Location " DataField="LocationID" SortExpression="LocationID" />

                    </Columns>
                </asp:GridView>

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
        jQuery(document).ready(function () {
            Search.init();
        });
    </script>

</asp:Content>

