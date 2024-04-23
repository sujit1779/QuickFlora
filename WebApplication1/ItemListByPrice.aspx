<%@ Page Title="Items" Language="VB" MasterPageFile="~/MainMaster.master"
    AutoEventWireup="false" CodeFile="ItemListByPrice.aspx.vb" Inherits="ItemListByPrice" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">Items Price List
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <ajax:AjaxPanel ID="AjaxPanel3" runat="server">
        <asp:Panel ID="pnlgrid" runat="server" Visible="true">

            <div class="table-toolbar">
                <div class="btn-group">
                    <a href="ItemDetails.aspx" style="display:none;" class="btn green">Add New <i class="fa fa-plus"></i>
                    </a>
                </div>
                <div class="btn-group pull-right">
                    <button class="btn dropdown-toggle" data-toggle="dropdown">
                        Tools <i class="fa fa-angle-down"></i>
                    </button>
                    <ul class="dropdown-menu pull-right">
                        <li> <a href="#" onClick="window.open('ExportExcelItems.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="75" height="75" border="0" /></a> </li>
                    </ul>
                </div>
            </div>

            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Search Options
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="table-toolbar text-center">
                        <div class="btn-group">
                            <div class="col-md-4">
                                <%--<div class="select2-container form-control input-medium select2me select2-dropdown-open select2-container-active select2-drop-above" id="s2id_autogen1">
                                    <a href="javascript:void(0)" onclick="return false;" class="select2-choice" tabindex="-1">
                                        <span class="select2-chosen">Invoice Number</span>
                                        <abbr class="select2-search-choice-close"></abbr>
                                        <span class="select2-arrow"><b></b></span>
                                    </a>
                                        <input class="select2-focusser select2-offscreen" type="text" id="s2id_autogen2" disabled="">

                                </div>--%>
                                <%--<select class="form-control input-medium select2me select2-offscreen" data-placeholder="Select..." 
                                    tabindex="-1">
                                    <!--<option value=""></option>-->
                                    <option value="InvoiceNumber">Invoice Number</option>
                                    <option value="Type">Type</option>
                                    <option value="InvoiceDate">Invoice Date</option>
                                    <option value="CustomerID">Customer ID</option>
                                    <option value="OrderNumber">Order Number</option>
                                    <option value="GLAccount">GL Account</option>
                                </select>--%>

                                <asp:DropDownList ID="drpSearchFor" runat="server"   CssClass="form-control input-medium"   tabindex="-1">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="ItemID" Selected="True" Text="Item ID"></asp:ListItem>
                                    <asp:ListItem Value="ItemName" Text="Item Name"></asp:ListItem>
                                    <%--<asp:ListItem Value="OrderNumber" Text="Order Number"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="btn-group">
                            <div class="col-md-1">
                                <%--<div class="select2-container form-control input-small select2me" id="s2id_autogen3"><a href="javascript:void(0)" onclick="return false;" class="select2-choice" tabindex="-1"><span class="select2-chosen">=</span><abbr class="select2-search-choice-close"></abbr>
                                    <span class="select2-arrow"><b></b></span></a>
                                    <input class="select2-focusser select2-offscreen" type="text" id="s2id_autogen4"><div class="select2-drop select2-display-none select2-with-searchbox">
                                        <div class="select2-search">
                                            <input type="text" autocomplete="off" autocorrect="off" autocapitalize="off" spellcheck="false" class="select2-input">
                                        </div>
                                        <ul class="select2-results"></ul>
                                    </div>
                                </div>--%>
                                <%--<select class="form-control input-small select2me select2-offscreen" tabindex="-1">
                                    <option value="=">=</option>
                                    <option value=">">&gt;</option>
                                    <option value="<">&lt;</option>
                                    <option value="Like">LIKE</option>
                                </select>--%>
                                <asp:DropDownList ID="drpSearchCondition" runat="server" CssClass="form-control input-medium"   tabindex="-1">
                                    <%--
                                    <asp:ListItem Value=">" Text=">"></asp:ListItem>
                                    <asp:ListItem Value="<" Text="<"></asp:ListItem>--%>
                                    <asp:ListItem Value="Like" Text="Like"></asp:ListItem> 
                                    <asp:ListItem Value="=" Text="">=</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="btn-group">
                            <%--<input type="text" class="form-control input-large">--%>
                            <asp:TextBox ID="txtSearchValue" runat="server" CssClass="form-control input-large"></asp:TextBox>
                        </div>
                        <div class="btn-group">
                            <%--<button id="Button1" class="btn green">SEARCH</button>--%>
                            <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btn green"/>
                        </div>
                        <div class="btn-group">
                            <%--<button id="Button2" class="btn grey">CLEAR</button>--%>
                            <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn grey"/>
                        </div>
                    </div>

                    <div class="note note-info">
                        <p>
                            <asp:Label id="lblInfo" runat="server"></asp:Label>
                        </p>
                    </div>

                </div>
            </div>

            

            <div style='z-index: 90; height: 100%' class="table-responsive">
                <asp:HiddenField ID="gvSortDirection" runat="server" />
                <asp:GridView ID="gvItemsList" runat="server" CssClass="table table-striped table-bordered table-hover"
                    AllowSorting="true" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" DataKeyNames="ItemID">
                    <Columns>
                        <asp:TemplateField Visible="false"  HeaderText="Edit">
                            <ItemTemplate>
                                <a href="<%# String.Format("ItemDetails.aspx?ItemID={0}", Eval("ItemID"))%>" class="btn default btn-xs purple"><i class="fa fa-edit"></i>Edit</a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false"  HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton11" CausesValidation="false"
                                    CssClass="btn default btn-xs red"
                                    runat="server" CommandName="Delete"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Item ID" DataField="ItemID" SortExpression="ItemID" />
                        <asp:BoundField HeaderText="Item Name" DataField="ItemName" SortExpression="ItemName" />
                        <asp:BoundField HeaderText="Item Description" DataField="ItemDescription" SortExpression="ItemDescription" />
                        <asp:BoundField HeaderText=" UPC Code"   DataField="ItemUPCCode" SortExpression="ItemUPCCode" />
                        <asp:BoundField HeaderText="Price"  DataFormatString="{0:N}" DataField="Price" SortExpression="Price" />
                        <asp:BoundField HeaderText="Sort Order" Visible="false"  DataField="SortOrder" SortExpression="SortOrder" />
                        <asp:BoundField HeaderText="Item UOM" Visible="false"  DataField="ItemUOM" SortExpression="ItemUOM" />
                        <asp:BoundField HeaderText="Active For Shopping Cart" Visible="false"  DataField="ActiveForShoppingCart" SortExpression="ActiveForShoppingCart" />
                        <asp:BoundField HeaderText="Active For POS" Visible="false"  DataField="ActiveForPOS" SortExpression="ActiveForPOS" />
                        <asp:BoundField HeaderText="Featured" Visible="false"  DataField="Featured" SortExpression="Featured" />
                        <asp:BoundField HeaderText="Grower" Visible="false"  DataField="Grower" SortExpression="Grower" />
                        <asp:BoundField HeaderText="Who Price"  DataFormatString="{0:N}" DataField="wholesalePrice" SortExpression="wholesalePrice" />
                        <asp:BoundField HeaderText="UnitsPerBox" Visible="false" DataField="UnitsPerBox" SortExpression="UnitsPerBox" />
                        <asp:BoundField HeaderText="Unit Price" DataFormatString="{0:N}" DataField="UnitPrice" SortExpression="UnitPrice" /> 
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
    <%--<script type="text/javascript" src="assests/dist/bootstrap.min.js"></script>
    <script type="text/javascript" src="assests/dist/bootstrap-table.js"></script>--%>
    <%--<script src="assets/dist/bootstrap-table.min.js" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" src="//code.jquery.com/jquery-1.12.4.js"></script>--%>
    <%--<script type="text/javascript" src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>--%>

    <script>
        jQuery(document).ready(function () {
            Search.init();
            //$('#vendorlist').DataTable({
            //    "processing": true,
            //    "serverSide": true,
            //    "ajax": "EventVendors.aspx/GauravGoyal"
            //});

            //$('#vendorlist').DataTable({
            //    processing: true,
            //    serverSide: true,
            //    ajax: {
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        url: "EventVendors.aspx/GauravGoyal",
            //        data: function (d) {
            //            //return JSON.stringify({ parameters: d });
            //            return JSON.stringify(d);
            //            //return { parameters: d };
            //        }
            //    }
            //});

        });

        function deleteRecord(d) {
            alert(d);
        }
    </script>
</asp:Content>
