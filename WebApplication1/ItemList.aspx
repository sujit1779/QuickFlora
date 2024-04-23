<%@ Page Title="Items" Language="VB" MasterPageFile="~/MainMaster.master"
    AutoEventWireup="false" CodeFile="ItemList.aspx.vb" Inherits="ItemList" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    
    <style>
        .preview { display: none; }
        .link:hover .preview { display: inline-block; position: absolute; }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">Items
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <ajax:AjaxPanel ID="AjaxPanel3" runat="server">
        <asp:Panel ID="pnlgrid" runat="server" Visible="true">

            <div class="table-toolbar">
                <div class="btn-group">
                    <a href="ItemDetails.aspx" class="btn green">Add New <i class="fa fa-plus"></i>
                    </a>
                </div>
                <div class="btn-group pull-right">
                    
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

                                <asp:DropDownList ID="drpSearchFor" runat="server" CssClass="form-control input-medium "   tabindex="-1">
                                    <asp:ListItem Value="" Text="--Please select--"></asp:ListItem>
                                    <asp:ListItem Value="ItemID" Text="Item ID"></asp:ListItem>
                                    <asp:ListItem Value="ItemName" Text="Item Name"></asp:ListItem>
                                    <asp:ListItem Value="ItemType" Text="Item Type"></asp:ListItem>
                                    <asp:ListItem Value="Location" Text="Location"></asp:ListItem>
                                    <asp:ListItem Value="VendorID" Text="Vendor"></asp:ListItem>
                                    <%--<asp:ListItem Value="OrderNumber" Text="Order Number"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="btn-group">
                            <div class="col-md-1">
                                
                                <asp:DropDownList ID="drpSearchCondition" runat="server" CssClass="form-control input-medium" data-placeholder="Select..." tabindex="-1">
                                    <%--
                                    <asp:ListItem Value=">" Text=">"></asp:ListItem>
                                    <asp:ListItem Value="<" Text="<"></asp:ListItem>--%>
                                    <asp:ListItem Value="Like" Text="Like"></asp:ListItem>
                                    <asp:ListItem Value="=" Text="="></asp:ListItem>
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

                </div>
            </div>

              <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                More Filters Option
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div id="famcat" runat="server" visible="true" class="form-group">
                                <table style="width:100%;padding:5px;column-gap:10px; " >
                                    <tr style="width:100%;background-color:#f9f9f9;">
                                        <td colspan="5" style="width:20%;text-align:left;">&nbsp; </td>
                                    </tr> 
                                    <tr style="width:100%;background-color:#FFFFFF;">
                                        <td  colspan="2" style="width:20%;text-align:center;">Primary Family&nbsp;  
                                                <asp:DropDownList AutoPostBack="true" AppendDataBoundItems="true" ID="drpItemFamilyID1" Width="150"   runat="server">
                                                    <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                </asp:DropDownList> 
                                        </td>
                                        <td colspan="2" style="width:20%;text-align:center;">Primary Category&nbsp;  
                                                <asp:DropDownList ID="drpItemCategoryID1" AutoPostBack="true" Width="150" runat="server"  >
                                                            <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                 </asp:DropDownList> 
                                        </td>
                                        <td colspan="2" style="width:20%;text-align:center;">
                                            <asp:Label ID="grpLabel" runat="server" Visible="false" Text="Label">Primary Group&nbsp;</asp:Label>
                                            <asp:DropDownList ID="drpItemGroup"  AutoPostBack="true" Visible="false" Width="150" runat="server">
                                                </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="width:100%;background-color:#f9f9f9;">
                                        <td colspan="5" style="width:20%;text-align:left;">&nbsp; </td>
                                    </tr> 
                                     <tr style="width:100%;background-color:#FFFFFF;">
                                         <td style="width:20%;text-align:left;"  > </td>
                                         <td style="width:20%;text-align:left;">  <asp:CheckBox AutoPostBack="true" ID="chkactive" Text="&nbsp;POS Items" runat="server" /> </td>
                                         <td style="width:20%;text-align:left;"  > <asp:CheckBox AutoPostBack="true" ID="chkwebsite" Text="&nbsp;Website Items" runat="server" /> </td>
                                         <td style="width:20%;text-align:left;"> <asp:CheckBox AutoPostBack="true" ID="chktaxable" Text="&nbsp;Non Taxable Items" runat="server" /> </td>
                                         <td style="width:20%;text-align:left;"  > </td>
                                    </tr>
                                     <tr style="width:100%;background-color:#f9f9f9;">
                                         <td style="width:20%;text-align:left;"  > </td>
                                         <td style="width:20%;text-align:left;"> <asp:CheckBox AutoPostBack="true" ID="chkRecipe" Text="&nbsp;Recipe Items" runat="server" /> </td>
                                         <td style="width:20%;text-align:left;"> <asp:CheckBox AutoPostBack="true" ID="chkEvents" Text="&nbsp;Events Items" runat="server" /> </td>
                                         <td style="width:20%;text-align:left;">  <asp:CheckBox AutoPostBack="true" ID="chkArchive" Text="&nbsp;Archived Items" runat="server" />  </td>
                                         <td style="width:20%;text-align:left;"  > </td>
                                    </tr>
                                     <tr style="width:100%;background-color:#FFFFFF;">
                                         <td style="width:20%;text-align:left;"  > </td>
                                         <td style="width:20%;text-align:left;"> <asp:CheckBox AutoPostBack="true" ID="chkActiveForPOM" Text="&nbsp;POM Item" runat="server" /> </td>
                                         <td style="width:20%;text-align:left;" > <asp:CheckBox AutoPostBack="true" ID="chkActiveForStore" Text="&nbsp;Store Item" runat="server" /> </td>
                                         <td style="width:20%;text-align:left;">  <asp:Button ID="Button1" runat="server" Text="SEARCH" CssClass="btn green"/> </td>
                                         <td style="width:20%;text-align:left;"  > </td>
                                    </tr>
                                </table>
                                 
                            </div>
                           
                            <div class="row">
               
            </div>
                         
                            
                        </div>
                    </div>
                </div>
            </div>
            
            
                    <div class="note note-info">
                        <p>
                            <asp:Label id="lblInfo" runat="server"></asp:Label>
                        </p>
                    </div>

            <div style='z-index: 90; height: 100%' class="table-responsive">
                <asp:HiddenField ID="gvSortDirection" runat="server" />
                <asp:GridView ID="gvItemsList" runat="server" CssClass="table table-striped table-bordered table-hover"
                    AllowSorting="true" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" DataKeyNames="ItemID">
                    <Columns>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <a href="<%# String.Format("ItemDetails.aspx?ItemID={0}", Eval("ItemID"))%>" class="btn default btn-xs purple"><i class="fa fa-edit"></i>Edit</a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton11" CausesValidation="false"
                                    CssClass="btn default btn-xs red"
                                    runat="server" CommandName="Delete"><i class="fa fa-trash-o"></i> Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         

                        <asp:BoundField HeaderText="Item ID" DataField="ItemID" SortExpression="ItemID" />
                        <asp:BoundField HeaderText="Item Name" Visible="false" DataField="ItemName" SortExpression="ItemName" />
                         <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                 <a class="link" href="javascript:void();">
                                    <%# Eval("ItemName")%> 
                                    <img class="preview" src="<%# returl(Eval("ThumbnailImage"))%>">
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Item Description" DataField="ItemDescription" SortExpression="ItemDescription" />
                        <asp:BoundField HeaderText="Item Type" DataField="ItemTypeID" SortExpression="ItemTypeID" />
                        <asp:BoundField HeaderText="Price"  DataFormatString="{0:N}" DataField="Price" SortExpression="Price" />
                        <asp:BoundField HeaderText="Whole Price"  DataFormatString="{0:N}" DataField="WP" SortExpression="WP" />
                        <asp:BoundField HeaderText="Sort Order" DataField="SortOrder" SortExpression="SortOrder" />
                        <asp:BoundField HeaderText="Item UOM" DataField="ItemUOM" SortExpression="ItemUOM" />
                        <asp:BoundField HeaderText="Active For Shopping Cart" DataField="EnabledfrontEndItem" SortExpression="EnabledfrontEndItem" />
                        <asp:BoundField HeaderText="Active For POS" DataField="IsActive" SortExpression="IsActive" />
                        <asp:BoundField HeaderText="Primary Family" DataField="ItemFamilyID" SortExpression="ItemFamilyID" />
                        <asp:BoundField HeaderText="Primary Category" DataField="ItemCategoryID" SortExpression="ItemCategoryID" />
                         <asp:TemplateField HeaderText="Archive">
                            <ItemTemplate>
                                <a href="<%# String.Format("ItemsmarkArchived.aspx?ItemID={0}", Eval("ItemID"))%>" style="display:<%# archived(Eval("ar"))%>;" class="btn default btn-xs yellow"><i class="fa fa-anchor"></i>Archive</a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
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
