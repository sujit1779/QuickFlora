<%@ Page Title="Item Details" Language="VB" MasterPageFile="~/MainMaster.master" ValidateRequest="false" AutoEventWireup="false" CodeFile="ItemDetails.aspx.vb" Inherits="ItemDetails" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    <style>
        .autocompletenew {
            overflow-x: hidden;
            overflow-y: auto;
            height: 200px;
            position: absolute;
            z-index: 2;
            width: 250px;
            background-color: #fff;
            box-shadow: 5px 5px rgba(102, 102, 102, 0.1);
            border: 1px solid #40BD24;
            visibility: hidden;
        }
    </style>

    <script type="text/javascript">


        var req;

        function Initialize() {
            try {
                req = new ActiveXObject("Msxml2.XMLHTTP");
            }
            catch (e) {
                try {
                    req = new ActiveXObject("Microsoft.XMLHTTP");
                }
                catch (oc) {
                    req = null;
                }
            }

            if (!req && typeof XMLHttpRequest != "undefined") {
                req = new XMLHttpRequest();
            }

        }

        function SendQuery(key, This, RowNumber) {
            //alert(This.id);
            //alert(RowNumber);

            var str = key;
            var n = str.length;
            if (n < 2) {
                return 0;
            }
            Initialize();
            var url = "AjaxVendorSearchNew.aspx?k=" + key + "&id=" + This.id + "&RowNumber=" + RowNumber;
            //alert(url);

            stop = 1;

            if (req != null) {
                req.onreadystatechange = function () {
                    ProcessSearch(RowNumber);
                };
                req.open("GET", url, true);
                req.send(null);

            }

        }


        function ProcessSearch(RowNumber) {
            //alert("In ProcessSearch");
            //alert(RowNumber);

            if (req.readyState == 4) {
                if (req.status == 200) {

                    //alert("Result : " +  req.responseText);
                    var id;
                    id = "autocomplete" + RowNumber;
                    ShowDiv(id);

                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = req.responseText;
                    //alert(newdiv.innerHTML);
                    var container = document.getElementById(id);
                    var newid = "";
                    newid = "'" + id + "'";
                    container.innerHTML = '<div style="text-align:center" ><a class="btn btn-danger btn-xs" href="Javascript:CcustomersearchcloseProcess(' + newid + ');" >Close</a></div>';
                    container.appendChild(newdiv)

                }
                else {
                    alert("There was a problem saving data:<br>" + req.statusText);
                }
            }

        }

        function CcustomersearchcloseProcess(id) {
            //alert(CcustomersearchcloseProcess);
            stop = 0;
            document.getElementById(id).innerHTML = "";
            //document.getElementById("txtOrderSearch.ClientID").value = "";
            HideDiv(id);


        }

        function FillSearchtextBox(val, ID, DIVID) {
            // alert(val);
            document.getElementById(ID).value = val;
            document.getElementById("autocomplete" + DIVID).innerHTML = "";
            document.getElementById(ID).focus();
            document.getElementById(ID).click();
            HideDiv("autocomplete" + DIVID);

            //ImgUpdateSearchitems
        }

        function processmarkup() {
            //alert('processmarkup');
            var price = 0.0;
            var WholesalePrice = 0.0;
            var markup = 0.0;
            WholesalePrice = document.getElementById('<%=txtItemWholeSalePrice.ClientID%>').value;
                          markup = document.getElementById('<%=txtMarkup.ClientID%>').value;
                          //alert(WholesalePrice);
                          //alert(price);
                          price = parseFloat(WholesalePrice) + parseFloat((WholesalePrice * markup) / 100);
                          document.getElementById('<%=txtItemPrice.ClientID%>').value = price.toFixed(2);


            //ImgUpdateSearchitems
        }


        function ShowDiv(divid) {
            document.getElementById(divid).style.height = "200px";
            if (document.layers) document.layers[divid].visibility = "show";
            else document.getElementById(divid).style.visibility = "visible";
        }


        function HideDiv(divid) {
            document.getElementById(divid).style.height = "0px";
            if (document.layers) document.layers[divid].visibility = "hide";
            else document.getElementById(divid).style.visibility = "hidden";
        }



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">Item Setup</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <ajax:AjaxPanel ID="AjaxPanel3" runat="server">
    </ajax:AjaxPanel>

    <asp:Panel ID="pnlgrid" runat="server" Visible="true">

        <ajax:AjaxPanel ID="AjaxPanel1" runat="server">


            <!-- Item Main Tab -->
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Details
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


                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">ID</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemID" runat="server" CssClass="form-control input-md" placeholder="Item ID"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Type</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpItemType" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Stock" Value="Stock"></asp:ListItem>
                                                        <asp:ListItem Text="Non Stock" Value="Non Stock"></asp:ListItem>
                                                        <asp:ListItem Text="Class" Value="Flower Class"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Name</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemName" runat="server" class="form-control input-md" placeholder="Item Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Short Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemShortDescription" runat="server" class="form-control input-md" placeholder="Short Desciption" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Long Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemLongDescription" runat="server" class="form-control input-md" placeholder="Long Desciption" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Care Instruction</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemCareInstruction" runat="server" class="form-control input-md" placeholder="Item Care Instruction"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item UPC Code</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemUPCCode" runat="server" class="form-control input-md" placeholder="Item UPC Code"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Color</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemColor" runat="server" class="form-control input-md" placeholder="Item Color"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item UOM</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemUOM" runat="server" class="form-control input-md" placeholder="Item UOM"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Currency ID</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCurrencyID" runat="server" class="form-control input-md" placeholder="Currency"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Currency Exchange Rate</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCurrencyExchangeRate" runat="server" Text="1" class="form-control input-md" placeholder="CurrencyExchangeRate"></asp:TextBox>
                                                </div>
                                            </div>
                                           
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Pricing Code</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemPricingCode" runat="server" class="form-control input-md" placeholder="Pricing Code"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Pricing Method</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtPricingMethod" runat="server" class="form-control input-md" placeholder="Pricing Method"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Taxable</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkItemTaxable" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Tax Group ID</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpTaxbleGroupID" runat="server" class="form-control input-md"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">GST Tax</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkGSTTax" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">PST Tax</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkPSTTax" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>

                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Is Two Items</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkIsTwoItems" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Is Three Items</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkIsThreeItems" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Best Selling</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkBestSelling" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Is Assembly</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkIsAssembly" runat="server" class="form-md-checkboxes" placeholder="Gift Card Type"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Assembly</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpItemAssembly" runat="server" class="form-control input-md"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Flower Class for Series</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpFlowerClassForSeries" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-5 control-label">Flower Class Unit Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFlowerClassUnitPtrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Class Unit Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Sort Order</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSortOrder" runat="server" Text="0" class="form-control input-md" placeholde="Sort Order"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="form-group">
                                                <label class="col-md-5 control-label">Active For Store</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkActiveForStore" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Active For POM</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkActiveForPOM" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Group</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtGroup" runat="server" Text="" class="form-control input-md" placeholder="Group"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Class</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtClass" runat="server" Text="" class="form-control input-md" placeholder="Class"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Buyer pack size</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtUnitsPerBox" runat="server" Text="1" class="form-control input-md" placeholder="Units Per Box"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display: none" class="form-group">
                                                <label class="col-md-5 control-label">Buyer Pack Size</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtbuyerpacksize" runat="server" Text="0" class="form-control input-md" placeholde="Sort Order"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display: none" class="form-group">
                                                <label class="col-md-5 control-label">Buyer UOM</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtbuyeruom" runat="server" Text="0" class="form-control input-md" placeholde="Sort Order"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <%--<div class="form-group">
                                            <label class="col-md-5 control-label">Qty on Hand</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtItemQtyOnHand" runat="server" class="form-control input-md" placeholder="Qty on Hand"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Active for Events</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkActiveforEvents" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Active for Recipe</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkActiveforRecipe" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Mark If Gift Card</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkMarkIfGiftCard" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Active For Back Office</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkActiveForBackOffice" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-6 control-label">Wire Service Product</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkWireServiceProduct" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-6 control-label">Image Copyright Holder</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpImageCopyrightHolder" runat="server" class="form-control input-md"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Sales Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSalesDescription" runat="server" class="form-control input-md" placeholder="Sales Description"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Purchase Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtPurchaseDescription" runat="server" class="form-control input-md" placeholder="Purchase Description"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-6 control-label">Gift Card Type</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtGiftCardType" runat="server" class="form-control input-md" placeholder="Gift Card Type"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-md-6 control-label">SKU</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSKU" runat="server" class="form-control input-md" placeholder="SKU"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-6 control-label">Lead Time</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtLeadTime" runat="server" class="form-control input-md" placeholder="Lead Time"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Item Size</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemSize" runat="server" class="form-control input-md" placeholder="Item Size"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-6 control-label">Item Style</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemStyle" runat="server" class="form-control input-md" placeholder="Item Style"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="display:none" class="form-group">
                                                <label class="col-md-6 control-label">Item RF ID</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemRFID" runat="server" class="form-control input-md" placeholder="Item RF ID"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Vendor</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList Visible="false" ID="drpVendor" runat="server" class="form-control input-md"></asp:DropDownList>
                                                    <asp:TextBox ID="txtVendor_Code" runat="server" class="form-control input-md"></asp:TextBox>
                                                    <div id="autocompletev" align="left" class="box autocompletenew" style="visibility: hidden;"></div>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Product Vendor</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtProductVendor" runat="server" class="form-control input-md"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Entered By</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpEnteredBy" runat="server" class="form-control input-md"></asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <br />
                                            <br />
                                            Additional Information
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtAdditionalInformation" runat="server" class="form-control input-md" placeholder="Additional Information" TextMode="MultiLine"></asp:TextBox>
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


            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Category Details
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div id="famcat" runat="server" class="form-group">
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Item Family ID 1</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList AutoPostBack="true" AppendDataBoundItems="true" ID="drpItemFamilyID1" class="form-control input-md" runat="server">
                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Item Category ID 1</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpItemCategoryID1" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Item Family ID 2</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList AutoPostBack="true" AppendDataBoundItems="true" ID="drpItemFamilyID2" class="form-control input-md" runat="server">
                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Item Category ID 2</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpItemCategoryID2" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Active Item Family ID 2</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkActiveItemFamilyID2" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Item Family ID 3</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList AutoPostBack="true" AppendDataBoundItems="true" ID="drpItemFamilyID3" class="form-control input-md" runat="server">
                                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Item Category ID 3</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpItemCategoryID3" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Active Item Family ID 3</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkActiveItemFamilyID3" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="famcat2" runat="server" class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab4Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                                 <asp:Button ID="btnTab4SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

        </ajax:AjaxPanel>

        <ajax:AjaxPanel ID="AjaxPanel2" runat="server">
            <!--Item Price-->
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Price Details
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
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Item Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Deluxe Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemDeluxePrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Deluxe Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Premium Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtPremiumPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Premium Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Holiday Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemHolidayPrice" Text="0.00" runat="server" class="form-control input-md" placeholder="Holiday Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Wholesale Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemWholeSalePrice" Text="0.00" runat="server" class="form-control input-md" placeholder="Wholesale Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">MT Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemMTPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="MT Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Markup (%)</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtMarkup" runat="server" Text="0.00" class="form-control input-md" placeholder="MT Price"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">CostWOFreight Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemCostWOFreightPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="CostWOFreight Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Local Everyday Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtLocalEverydayPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Local Everyday Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Wireout Everyday Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtWireoutEverydayPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Wireout Everyday Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Wireout Holiday Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtWireoutHolidayPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Wireout Holiday Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Dropship Everyday Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDropshipEverydayPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Dropship Everyday Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Dropship Holiday Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDropshipHolidayPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Dropship Holiday Price"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Average Cost</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtAverageCost" runat="server" Text="0.00" class="form-control input-md" placeholder="Average Cost"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Average Value</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtAverageValue" runat="server" Text="0.00" class="form-control input-md" placeholder="Average Value"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Commissionable</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkCommissionable" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Commission Type</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCommissionType" runat="server" class="form-control input-md" placeholder="Commission Type"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Commission Percent</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCommissionPercent" runat="server" Text="0" class="form-control input-md" placeholder="Commission Percent"></asp:TextBox>
                                                </div>
                                            </div>




                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">

                                    <asp:Button ID="btnsavetab2" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                                 <asp:Button ID="btnsaveandclosetab2" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>


        </ajax:AjaxPanel>

        <!-- Item Images and Shopping Cart Settings -->
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box green ">
                    <div class="portlet-title">
                        <div class="caption">
                            Images and Shopping Cart Settings
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
                            <div class="col-md-6">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Small Image (100x100)</label>
                                            <div class="col-md-6">
                                                <asp:Image ID="ImgSmallImage" runat="server" class="img-thumbnail" alt="Item Small Image 100 x 100" Width="100px" Height="100px" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Upload Image</label>
                                            <div class="col-md-6">
                                                <%--<asp:CheckBox ID="CheckBox1" runat="server" class="form-control input-md"></asp:CheckBox>--%>
                                                <asp:FileUpload ID="fuSmallImage" runat="server" CssClass="form fileinput" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Medium Image (200x200)</label>
                                            <div class="col-md-6">
                                                <asp:Image ID="ImgMediumImage" runat="server" class="img-thumbnail" alt="Item Small Image 100 x 100" Width="100px" Height="100px" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Upload Medium Image</label>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="fuMediumImage" runat="server" CssClass="form fileinput" />
                                            </div>
                                        </div>

                                         <br />
                                        <br />
                                        <br />
                                        <br />

                                        <div   class="form-group">
                                            <label class="col-md-6 control-label">Click to Upload Thumbnail Image</label>
                                            <div class="col-md-6">
                                                <a  onclick="Javascript:AddStocknew();"   href="Javascript:;"  class="btn default btn-xs green">
                                                     <img id="thm"  width="100" height="100" src="<%=ThumbnailImage %>" /> 
                                                 </a> 
                                            </div>
                                        </div>
                                         

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">

                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Large Image (400x400)</label>
                                            <div class="col-md-6">
                                                <asp:Image ID="ImgLargeImage" runat="server" class="img-thumbnail" alt="Item Small Image 100 x 100" Width="100px" Height="100px" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Upload Large Image</label>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="fuLargeImage" runat="server" CssClass="form fileinput" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Mark If Sale</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkMarkIfSale" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Mark If New</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkMarkIfNew" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Featured</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkFeatured" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Active for Shopping Cart</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkActiveForShoppingCart" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Enable Item Price</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkEnableItemPrice" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Enable Add to Cart</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkEnableAddToCart" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>




                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <div style="text-align: center; padding: 10px;" class="col-md-12">
                                <asp:Button ID="btnTab3Save" runat="server" class="btn green" Text="Save" />
                                &nbsp;&nbsp;
                                    <asp:Button ID="btnTab3SaveClose" runat="server" class="btn green" Text="Save and Close" />
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>

        <!-- Mobile App Configurations -->
        <div id="mobapp" class="row">
            <div class="col-md-12">
                <div class="portlet box green ">
                    <div class="portlet-title">
                        <div class="caption">
                            Mobile App Configurations
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
                            <div class="col-md-6">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Icon Small Image</label>
                                            <div class="col-md-6">
                                                <asp:Image ID="ImgIconSmallImage" runat="server" class="img-thumbnail" alt="Item Small Image 100 x 100" Width="100px" Height="100px" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Upload Icon Small Image</label>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="fuIconSmallImage" runat="server" CssClass="form fileinput" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Icon Medium Image</label>
                                            <div class="col-md-6">
                                                <asp:Image ID="ImgIconMediumImage" runat="server" class="img-thumbnail" alt="Item Small Image 100 x 100" Width="100px" Height="100px" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Icon Medium Image</label>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="fuIconMediumImage" runat="server" CssClass="form fileinput" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-horizontal" role="form">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Icon Large Image</label>
                                            <div class="col-md-6">
                                                <asp:Image ID="ImgIconLargeImage" runat="server" class="img-thumbnail" alt="Item Small Image 100 x 100" Width="100px" Height="100px" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Icon Large Image</label>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="fuIconLargeImage" runat="server" CssClass="form fileinput" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Mobile App Special</label>
                                            <div class="col-md-6">
                                                <asp:CheckBox ID="chkMobileAppSpecial" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Video URL</label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtVideoURL" runat="server" class="form-control input-md"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div style="text-align: center; padding: 10px;" class="col-md-12">
                                <asp:Button ID="btnMobileAppsave" runat="server" class="btn green" Text="Save" />
                                &nbsp;&nbsp;
                                                 <asp:Button ID="btnMobileAppsaveclose" runat="server" class="btn green" Text="Save and Close" />
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>


        <ajax:AjaxPanel ID="AjaxPanel4" runat="server">
            <!-- Item Family - Category -->
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Family and Category
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">

                            <div class="row">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-10">
                                    <!-- BEGIN FORM-->
                                    <div class="form-body">
                                        <table id="table3" class="table table-striped table-hover table-bordered">
                                            <tr id="famcat3" runat="server" visible="false">
                                                <td colspan="2">

                                                    <div class="form-group-search-block">
                                                        <div class="input-group">
                                                            <span style="color: White;" class="input-group-addon input-circle-left">Item &nbsp;<i class="fa fa-search"></i>
                                                            </span>
                                                            <ajax:AjaxPanel ID="AjaxPanel12" runat="server">
                                                                <asp:TextBox ID="txtitemsearch" CssClass="form-control input-circle-right" runat="server"></asp:TextBox>
                                                            </ajax:AjaxPanel>
                                                            <br />
                                                            <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete2"></div>
                                                        </div>
                                                    </div>

                                                </td>

                                            </tr>

                                            <tr>

                                                <td style="text-align: center;">
                                                    <asp:DropDownList AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control input-250" OnSelectedIndexChanged="DropDown_SelectedIndexChanged"
                                                        ID="Family1" runat="server">
                                                        <asp:ListItem Text="--Please Select Family--" Value="---None---"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>


                                                <td style="text-align: center;">
                                                    <asp:DropDownList ID="Category1" AppendDataBoundItems="true" CssClass="form-control input-250" runat="server">
                                                        <asp:ListItem Text="--Please Select Category--" Value="---None---"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn green" Text="Associate" />
                                                    <br />

                                                    <asp:Label ID="lblErr2" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                    <!-- END FORM-->
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:GridView ID="grdProducts" runat="server" DataKeyNames="AssociationID"
                                    AutoGenerateColumns="false" AllowPaging="false" CssClass="table table-striped table-hover table-bordered">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                    CommandName="Delete" ToolTip="Delete" ImageUrl="https://secure.quickflora.com/EnterpriseASP/EnterpriseASPAP/images/Delete.gif"
                                                    runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="25px" HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemID" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Family">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemFamilyID" runat="server" Text='<%# Eval("ItemFamilyID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCategoryID" runat="server" Text='<%# Eval("ItemCategoryID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </div>


                        </div>
                    </div>
                </div>
            </div>


        </ajax:AjaxPanel>
        <!-- Item Accouting Tab -->
        <ajax:AjaxPanel ID="AjaxPanel5" runat="server">
            <div id="ItemAccountingDetails" class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Accounting Details
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
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">GL Item Sales Account</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpGLItemSalesAccount" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">GL Item COGS Account</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpGLItemCOGSAccount" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">GL Item Inventory Account</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpGLItemInventoryAccount" runat="server" class="form-control input-md">
                                                        <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab5Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                                 <asp:Button ID="btnTab5SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>
        </ajax:AjaxPanel>

        <ajax:AjaxPanel ID="AjaxPanel6" runat="server">
            <!-- Item SEO -->
            <div id="ItemSEODetails" class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item SEO Details
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

                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Page Title</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtPageTitle" runat="server" class="form-control input-md" placeholder="Page Title"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Meta Keywords</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtMetaKeywords" runat="server" class="form-control input-md" placeholder="Meta Keywords"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Meta Description</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtMetaDescription" runat="server" class="form-control input-md" placeholder="Meta Description"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab6Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnTab6SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

        </ajax:AjaxPanel>

        <ajax:AjaxPanel ID="AjaxPanel7" runat="server">
            <!-- Item Charges -->
            <div id="ItemCharges" class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Charges
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
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Free Delivery</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkItemFreeDelivery" runat="server" class="form-md-checkboxes"></asp:CheckBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Delivery by Item</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDeliveryByItem" runat="server" Text="0.00" class="form-control input-md" placeholder="Delivery by Item"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Discount Coupon Code</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDiscountCouponCode" runat="server" class="form-control input-md" placeholder="Discount Coupon Code"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab7Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnTab7SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

        </ajax:AjaxPanel>

        <ajax:AjaxPanel ID="AjaxPanel8" runat="server">
            <!-- Item on Sale -->
            <div id="ItemonSale" class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item on Sale
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
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">MSRP/Strike Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemMSRPStrikePrice" Text="0.00" runat="server" class="form-control input-md" placeholder="MSRP/Strike Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Sales Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSalesPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Sales Price"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Sale Start Date Price</label>
                                                <div class="col-md-6">
                                                    <%--<div class="btn-group">--%>
                                                    <%--<div class="input-group date-picker" data-date="10/11/2012" data-date-format="mm/dd/yyyy">--%>
                                                    <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                                    <asp:TextBox ID="txtSaleStartDate" runat="server" class="form-control input-sm date-picker" data-date-format="mm-dd-yyyy" data-date-viewmode="years" data-date="03/25/2017"></asp:TextBox>
                                                    <%--</div>--%>
                                                    <%--</div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Sale End Date Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtSaleEndDate" runat="server" class="form-control input-md"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab8Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnTab8SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

        </ajax:AjaxPanel>

        <ajax:AjaxPanel ID="AjaxPanel9" runat="server">
            <!-- Item Inventory -->
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Inventory
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
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">LIFO Cost</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtLIFOCost" Text="0.00" runat="server" class="form-control input-md" placeholder="LIFO Cost"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">LIFO Value</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtLIFOValue" Text="0.00" runat="server" class="form-control input-md" placeholder="LIFO Value"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">FIFO Cost</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFIFOCost" Text="0.00" runat="server" class="form-control input-md" placeholder="FIFO Cost"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">FIFO Value</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFIFOValue" Text="0.00" runat="server" class="form-control input-md" placeholder="FIFO Value"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">ReOrder Level</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtReOrderLevel" runat="server" class="form-control input-md" placeholder="ReOrder Level"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">ReOrder Qty</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtReOrderQty" runat="server" Text="1" class="form-control input-md" placeholder="ReOrder Qty"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Default Warehouse</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpDefaultWarehouse" runat="server" AutoPostBack="true" class="form-control input-md" placeholder="Default Warehouse"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-6 control-label">Default Warehouse Bin</label>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="drpDefaultWareHouseBin" runat="server" class="form-control input-md" placeholder="Default Warehouse Bin"></asp:DropDownList>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab9Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnTab9SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

        </ajax:AjaxPanel>

        <ajax:AjaxPanel ID="AjaxPanel10" runat="server">
            <!-- Wholesale Setup -->
            <div id="WholesaleSetup" class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Wholesale Setup
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
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Common Name</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemCommonName" runat="server" class="form-control input-md" placeholder="Common Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Botanical Name</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemBotanicalName" runat="server" class="form-control input-md" placeholder="Botanical Name"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Color Group</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtColorGroup" runat="server" class="form-control input-md" placeholder="Color Group"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Flower Type</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFlowerType" runat="server" class="form-control input-md" placeholder="Flower Type"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Variety </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtVariety" runat="server" class="form-control input-md" placeholder="Variety"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Grade</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtGrade" runat="server" class="form-control input-md" placeholder="Grade"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Box Size</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtBoxSize" runat="server" class="form-control input-md" placeholder="Box Size"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Actual Weight</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtActualWeight" runat="server" class="form-control input-md" placeholder="Actual Weight"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Dimensional Weight</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDimensionalWeight" runat="server" class="form-control input-md" placeholder="Dimensional Weight"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Origin</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtOrigin" runat="server" class="form-control input-md" placeholder="Origin"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Start Date Available</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtStartDateAvailable" runat="server" class="form-control input-md" placeholder="Start Date"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">End Date Available</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtEndDateAvailable" runat="server" class="form-control input-md" placeholder="End Date"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Ship Method Allowed</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkShipMethodAllwed" runat="server"></asp:CheckBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Payment Method Allowed</label>
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkPaymentMethodAllowed" runat="server"></asp:CheckBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Age In days</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtAgeIndays" runat="server" class="form-control input-md" placeholder="Age In days"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Ship Preparation</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtShipPreparation" runat="server" class="form-control input-md" placeholder="Ship Preparation"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Box Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtBoxPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Box Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Unit Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtUnitPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Unit Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Units Per Bunch</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtUnitsPerBunch" Text="1" runat="server" class="form-control input-md" placeholder="Units Per Bunch"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Standing Order Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtStandingOrderPrice" Text="0.00" runat="server" class="form-control input-md" placeholder="Standing Order Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Pre Book Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtPreBookPrice" runat="server" Text="0.00" class="form-control input-md" placeholder="Pre Book Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Cutoff Time</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCutoffTime" runat="server" class="form-control input-md" placeholder="Cutoff Time"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Cut Point</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCutPoint" runat="server" class="form-control input-md" placeholder="Cut Point"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Storage Temperature</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtStorageTemperature" runat="server" class="form-control input-md" placeholder="Temperature"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Misllenous Information</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtMiscllenousinformation" runat="server" class="form-control input-md" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Variety Information</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtVarietyInformation" runat="server" class="form-control input-md" placeholder="" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Grower</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtGrower" runat="server" class="form-control input-md" placeholder="Grower"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Flag</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtFlag" runat="server" class="form-control input-md" placeholder="Flag"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Available Number of Boxes</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtAvailableNumberOfBoxes" runat="server" class="form-control input-md" placeholder="Available Boxes"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Country of Origin</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtCountryOfOrigin" runat="server" class="form-control input-md" placeholder="Origin Country"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Location</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtLocation" runat="server" class="form-control input-md" placeholder="Location"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Box Width</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtBoxWidth" runat="server" class="form-control input-md" placeholder="Box Width"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Box Length</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtBoxLength" runat="server" class="form-control input-md" placeholder="Box Length"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Box Height</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtBoxHeight" runat="server" class="form-control input-md" placeholder="Box Height"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">UOM</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtUOM" runat="server" class="form-control input-md" placeholder="UOM"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Original Unit Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtOriginalUnitPrice" Text="0.00" runat="server" class="form-control input-md" placeholder="Original Unit Price"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Imported From</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtImportedFrom" runat="server" class="form-control input-md" placeholder="Imported From"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Box Size UOM</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtBoxSizeUOM" runat="server" class="form-control input-md" placeholder="Box Size UOM"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Imported At</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtImportedAt" runat="server" class="form-control input-md" placeholder="Imported At"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Pack Size</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtItemPackSize" runat="server" Text="1" class="form-control input-md" placeholder="Item Pack Size"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Variety ID</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtVarietyID" runat="server" class="form-control input-md" placeholder="Variety ID"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Notify Price</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox Text="0.00" ID="txtNotifyPrice" runat="server" class="form-control input-md" placeholder="Notify Price"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div style="text-align: center; padding: 10px;" class="col-md-12">
                                    <asp:Button ID="btnTab10Save" runat="server" class="btn green" Text="Save" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnTab10SaveClose" runat="server" class="btn green" Text="Save and Close" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
            </div>

        </ajax:AjaxPanel>

        <ajax:AjaxPanel ID="AjaxPanel11" runat="server">
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
                                        <asp:Button ID="btnSubmit" runat="server" class="btn green" Text="SUBMIT" />
                                        <%--<asp:Button ID="btnClear" runat="server" class="btn grey" Text="CLEAR" OnClientClick="clearform();" CausesValidation="false" />--%>
                                        <a id="btnClear" runat="server" class="btn grey" onclick="clearform();">CLEAR</a>
                                        <asp:Button ID="btnUpdate" runat="server" class="btn green" Text="Save All" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn grey" Text="CANCEL" />
                                        <br />
                                        <asp:Label ID="lbldebug" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ajax:AjaxPanel>


    </asp:Panel>

     <asp:Panel ID="pnldisable" runat="server">
        <style type="text/css" >

            #mobapp
            {
                display:none;
            }
            #ItemAccountingDetails
            {
                display:none;
            }
            #ItemSEODetails
            {
                  display:none;
            }
            #ItemCharges
            {
                 display:none;
            }
            #ItemonSale
            {
                 display:none;
            }
            #WholesaleSetup
            {
                display:none;
            }

        </style>

    </asp:Panel>

     
    <!-- /.modal -->
							<div id="responsive" class="modal fade" tabindex="-1" aria-hidden="true">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
											<h4 class="modal-title"><b>Update Item Image </b></h4>
										</div>
                                        <%--ItemID,ItemName,Price,BHID,PictureURL,BinId--%>
										<div class="modal-body">
											<div class="scroller" style="height:550px" data-always-visible="1" data-rail-visible1="1">
												<div class="row">
													<div class="col-md-6">
														<h4><b>Item Image</b></h4>
                                                        <hr />
                                                        <p>
                                                            <img class="img-thumbnail" id="imgmitem" style="max-width: 90%;" src="https://secure.quickflora.com/itemimages/no_image.gif" />
                                                        </p>
														<p>
                                                            <strong>Item ID</strong>
															<input type="text" readonly="true" id="txtmItemID" class="col-md-12 form-control">
														</p>
														<p>
                                                            <strong>Item Name</strong>
															<input type="text" readonly="true" id="txtmItemName" class="col-md-12 form-control">
														</p>
														 
														<p>
                                                            <strong>Price</strong>
															<input type="text" readonly="true" id="txtmPrice" class="col-md-12 form-control">
														</p>
                                                        
														 
													</div>
													<div class="col-md-6">
														<h4><b>Upload New Image <span id="imgtype"></span>  </b></h4>
														   <hr />
														<div>
                                                              
                                                       <input type="file" class="dropzone-select btn btn-light-primary font-weight-bold btn-sm dz-clickable" id="homePhoto">
                                                    <br />
                                                    <img   id="imgspin" />
                                                    <br />
													<a  href="Javascript:;"  id="but_upload" class="btn btn-primary text-uppercase font-weight-bolder px-15 py-3">Upload</a>
                                                    <br />
													   
														 
													</div>
												</div>
											</div>
										</div>
										<div class="modal-footer">
											<button type="button" data-dismiss="modal" class="btn default">Close</button>
											 <div id="dvtype" style="display:none;"></div>
										</div>
									</div>
								</div>
							</div>
							<div class="modal fade" id="ajax" tabindex="-1" role="basic" aria-hidden="true">
								<img src="assets/img/ajax-modal-loading.gif" alt="" class="loading">
							</div>
							<!-- /.modal -->


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>




    <script type="text/javascript">
        jQuery(document).ready(function () {
            Search.init();
        });
        
    </script>

     <!-- END PAGE LEVEL SCRIPTS -->
<script>
     // Format the price above to USD, INR, EUR using their locales.
let dollarUS = Intl.NumberFormat("en-US", {
    style: "currency",
    currency: "USD",
    });

    jQuery(document).ready(function () {
 
          $("#but_upload").click(function () {
               // alert('hi');
                
               document.getElementById('imgspin').src = 'https://secure.quickflora.com/images/wait.gif';
              var fd = new FormData();
              var files = $('#homePhoto')[0].files[0];
              fd.append('file', files);
                var filename = "";
                filename = files.name;  
                //alert(filename);
              $.ajax({
                  url: 'upload.aspx?itemid=' + $('#txtmItemID').val() + '&type=' + $('#dvtype').html(),
                  type: 'post',
                  data: fd,
                  contentType: false,
                  processData: false,
                  success: function (response) {
                      if (response != 0) {
                          document.getElementById('imgmitem').src = 'https://secure.quickflora.com/itemimages/' + response; 
                          document.getElementById('thm').src = 'https://secure.quickflora.com/itemimages/' + response; 
                          document.getElementById('imgspin').src = '';
                      }
                      else {
                          alert('file not uploaded');
                      }
                  },
              });
          });




    });      
        
    function AddStock(data,type) {
       // alert('AddStock');
        $.each(data, function (key, value) {
            $('#txtmItemID').val(value.ItemID);
            $('#txtmItemName').val(value.ItemName);
            $('#txtmPrice').val(value.Price);
            $('#dvtype').html(type);

            if (type == "sm")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.PictureURL);

             if(type=="md")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.MediumPictureURL);

             if(type=="lg")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.LargePictureURL);

             if(type=="thm")
                $("#imgmitem").attr("src", "https://secure.quickflora.com/itemimages/" + value.ThumbnailImage);
        });
       
        //$("#responsive").modal('show');
    }

    function AddStocknew() {
       // alert('AddStocknew');
        var Itemid = ''
        var type = 'thm';
        Itemid = document.getElementById('ctl00_ContentPlaceHolder_txtItemID').value;
         if(type=="thm")
            $("#imgtype").html("Thumbnail");

        $("#responsive").modal('show');
       // return;
        $.getJSON("Ajaxitemsimgdetails.aspx?Itemid=" + Itemid, function (data) {
                 AddStock(data,type);
        });
         
    }
   
     
</script>
<!-- END JAVASCRIPTS -->
</asp:Content>


