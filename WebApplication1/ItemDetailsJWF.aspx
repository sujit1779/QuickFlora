<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="ItemDetailsJWF.aspx.vb" Inherits="ItemDetailsJWF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">Item Setup</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
            
    <input type="hidden" id="txtItem" />
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Detail
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
								<div class="form-group">

                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="ItemID">Item ID</label> 
                                        <input id="ItemID" type="text" value="<%=NewItemID %>"  Class="form-control input-md" name="ItemId" onblur="Check()" >
                                        <span id="lableid" style="color:red;display:none;">&nbsp Item ID already exist. Please use different ID.</span>
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="ItemID">Item Name</label> 
                                        <input id="ItemName" type="text"  Class="form-control input-md" name="ItemName"  >
                                       
                                      
                                    </div>
                                          
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="ItemType">Item Type</label> 
                                        
                                           <select id="ItemTypeID" Class="form-control ">
                                               <option value="">--Please Select--</option>
	                                                <option value="Stock">Stock</option>
	                                                <option value="Non Stock">Non Stock</option>
	                                                <option value="Flower Class">Class</option>
                                           </select>
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                    </div>
                              </div>
                            <div class="row">
								<div class="form-group">

                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="ItemFamilyID">Item Family</label> 
                                         <select class="form-control " id="ItemFamilyID" onChange="getCategory('','','')">
                                        </select>                                       
                                      
                                    </div>
                                        
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="ItemCategoryID">Item Category</label> 
                                         <select class="form-control " id="ItemCategoryID" onChange="getGroup('','')">
                                        </select>                                       
                                      
                                    </div>
                                         
                                  </div>
                                    <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="GroupCode">Item Group</label> 
                                         <select class="form-control " id="GroupCode" >
                                        </select>                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                    </div>
                              </div>

                            <div class="row">
								<div class="form-group">

                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="Color">Item Color</label> 
                                        <input id="ItemColor" type="text"  Class="form-control input-md" name="Color"  ><%--style="display:none;"--%>
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="Size">Item Size</label> 
                                        <input id="ItemSize" type="text"  Class="form-control input-md" name="Size"  >
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="UnitPerBox">Unit Per Box</label> 
                                        <input id="UnitsPerBox" type="text"  Class="form-control input-md" name="UnitPerBox"  >
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                    </div>
                              </div>
                            
                            
                            <div class="row">
								<div class="form-group">

                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="Price">Item Price</label> 
                                        <input id="Price" type="text"  Class="form-control input-md" name="Price"  >
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="UOM">Item UOM</label> 
                                           <input id="ItemUOM" type="text"  Class="form-control input-md" name="Price"  >
                                          
                                       
                                     
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="SKU">SKU</label> 
                                        <input id="SKU" type="text"  Class="form-control input-md" name="SKU"  >
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                    </div>
                              </div>
                            <div class="row">
								<div class="form-group">

                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="PackSize">Pack Size</label> 
                                        <input id="ItemPackSize" type="text"  Class="form-control input-md" name="PackSize"  >
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="BoxSize">Box Size</label> 
                                         <input id="BoxSize" type="text"  Class="form-control input-md" name="BoxSize"  >  
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="Box">Box UOM</label> 
                                        <input id="BoxSizeUOM" type="text"  Class="form-control input-md" name="Box"  >
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                    </div>
                              </div>
                            <div class="row">
								<div class="form-group">

                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="Vender">Vendor</label> 
                                        <select class="form-control " id="VendorID" >
                                        </select>                                   

                                       </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="BoxPrice">Box Price</label> 
                                         <input id="BoxPrice" type="text"  Class="form-control input-md" name="BoxPrice"  >  
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="UPCItem">UPC Item</label> 
                                        <input id="ItemUPCCode" type="text"  Class="form-control input-md" name="UPCItem"  >
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                    </div>
                              </div>

                            <div class="row">
								<div class="form-group">

                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="GLItemSaleAccount">GL Item Sale Account</label> 
                                        
                                           <select class="form-control GLITEM" id="GLItemSaleAccount" >
                                           </select> 
                                       
                                      
                                    </div>
                                         
                                  </div>
                                    <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="GLItemCOGSAccount">GL Item COGS Account</label> 
                                        
                                           <select class="form-control GLITEM" id="GLItemCOGSAccount" >
                                           </select> 
                                       
                                      
                                    </div>
                                         
                                  </div>
                                    <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <label for="GLItemInventoryAccount">GL Item Inventory Account</label> 
                                        
                                           <select class="form-control GLITEM" id="GLItemInventoryAccount" >
                                           </select>
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                   
                                    </div>
                              </div>
                            <%--<div class="row">
								<div class="form-group">

                                    
                                    
                                    <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <input id="Taxable" type="checkbox"  name="Taxable">
                                        <label for="Taxable">Taxable</label> 
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   
                                   
                                    </div>
                              </div>--%>
                            <div class="row">
								<div class="form-group">

                                   <div class="form-group col-md-8">
                                       <div class="form-body">
                                        <label for="Description">Description</label> 
                                        <textarea  id="ItemDescription" type="text"  Class="form-control input-md multiple"  name="Description" rows="4"></textarea>
                                       
                                      
                                    </div>
                                         
                                  </div>
                                   <div class="form-group col-md-4">
                                       <div class="form-body">
                                        <input id="IsActive" type="checkbox"  name="ActiveforPOS">
                                        <label for="IsActive">Active for POS</label> 
                                       
                                      
                                    </div>
                                       <div class="form-body">
                                        <input id="ActiveforPOM" type="checkbox"  name="ActiveforPOM">
                                        <label for="ActiveForPOM">Active for POM</label> 
                                       
                                      
                                    </div>
                                       <div class="form-body">
                                        <input id="Taxable" type="checkbox"  name="Taxable">
                                        <label for="Taxable">Taxable</label> 
                                       
                                      
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
                        
                        <div class="portlet-body form">
                            <div class="form-horizontal" role="form">
                                <div class="form-body">
                                    <div class="form-actions right">
                                        
                                        <input type="button" ID="btnSubmit" class="btn green" value="SUBMIT" OnClick="Save(this)"/>
                                        <%--<asp:Button ID="btnClear" runat="server" class="btn grey" Text="CLEAR" OnClientClick="clearform();" CausesValidation="false" />--%>
                                        <a id="btnClear" runat="server" class="btn grey" onclick="clearform();">CLEAR</a>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>

    <script>
        var CompanyID = '<%=Session("CompanyID") %>';
        var DivisionID = '<%=Session("DivisionID") %>';
        var DepartmentID = '<%=Session("DepartmentID") %>';
        var ItemID = '<%=ItemID %>';

       

        function Check()
        {

            var ItemID = $("#ItemID").val();
            $.ajax({
                method: "GET",
                url: `https://secureapps.quickflora.com/V2/api/ItemDetails?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&ItemID=${ItemID}`,

                dataType: 'json',
                success: function (response)
                {

                    if (response.length)
                    {
                        
                        $("#lableid").css("display", "block");
                        $("#lableid").css("display", "show");
                        $("#ItemID").val('');



                    }
                    else
                    {
                        
                        $("#lableid").hide();
                    }

                }
            });

        }

        getVendor();
        getFamily('');
        getGLITEM();

        function getVendor()
        {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=&type=1&type2=2&type3=3&type4=4`,
                data: "{}",
                dataType: "json",
                encode: true,
                success: function (data)
                {

                    var VendorID = $("[id*=VendorID]");
                    VendorID.empty().append('<option selected="selected" value="0">Select Vendor</option>');
                    $.each(data, function (key, value)
                    {
                        $("#VendorID").append($("<option></option>").val(value.VendorID).html(value.VendorName));

                    });
                },
                error: function (data)
                {
                    alert("Error");
                }


            });

            
        }

        function getFamily(id)
        {
            $.ajax({
                method: 'GET',
                url: `AjaxFamily.aspx`,
                data: "{}",
                dataType: "json",
                encode: true,
                success: function (data)
                {

                    var ItemFamilyID = $("[id*=ItemFamilyID]");
                    ItemFamilyID.empty().append('<option selected="selected" value="0">Select Family</option>');
                    $.each(data, function (key, value)
                    {
                        var sel = '';
                        if (id == value.ItemFamilyID) {
                            sel = "selected='selected'";
                        }
                        $("#ItemFamilyID").append($(`<option ${sel}></option>`).val(value.ItemFamilyID).html(value.FamilyName));
                        
                    });
                },
                error: function (data) {
                    alert("Error");
                }


            });
        }
        function getCategory(ItemFamilyID,cat,grp)
        {
            if (!ItemFamilyID) {
                ItemFamilyID = $('#ItemFamilyID').val();
            }
            $.ajax({
                method: 'GET',
                url: `AjaxCategories.aspx?id=${ItemFamilyID}`,
                data: "{}",
                dataType: "json",
                encode: true,
                success: function (data) {

                    var ItemCategoryID = $("[id*=ItemCategoryID]");
                    ItemCategoryID.empty().append('<option value="0">Select Category</option>');
                    $.each(data, function (key, value) {
                        var sel = '';
                        if (cat == value.ItemCategoryID) {
                            sel = "selected='selected'";
                        }
                        $("#ItemCategoryID").append($(`<option ${sel}></option>`).val(value.ItemCategoryID).html(value.CategoryName));

                    });
                },
                error: function (data) {
                    alert("Error");
                }


            });
            getGroup(cat,grp);
        }
        function getGroup(cat,grp)
        {
            var ItemFamilyID = $('#ItemFamilyID').val();
            var ItemCategoryID = $('#ItemCategoryID').val();
            if (!cat) {
                ItemCategoryID = $('#ItemCategoryID').val();
            } else {
                ItemCategoryID = cat;
            }
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/ItemGroup?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&family=${ItemFamilyID}&category=${ItemCategoryID}`,
                dataType: "json",
                encode: true,
                success: function (data) {

                    var GroupCode = $("[id*=GroupCode]");
                    GroupCode.empty().append('<option value="0">Select Group</option>');
                    $.each(data, function (key, value)
                    {
                        var sel = '';
                        if (grp == value.GroupID) {
                            sel = "selected='selected'";
                        }
                        $("#GroupCode").append($(`<option ${sel}></option>`).val(value.GroupID).html(value.GroupName));

                    });
                },
                error: function (data) {
                    alert("Error");
                }


            });
        }

        function getGLITEM() {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/QFtoQBMapping?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}`,
                
                dataType: "json",
                encode: true,
                success: function (data) {

                    var GLITEM = $(".GLITEM");
                    GLITEM.empty().append('<option selected="selected" value="0">Please Select</option>');
                    $.each(data, function (key, value)
                    {
                        $(".GLITEM").append($("<option></option>").val(value.QFGLCode).html(value.QFGLMix));

                    });
                    if (ItemID) {
                        $("#txtItem").val(ItemID);
                        loadData();
                    }
                },
                error: function (data) {
                    alert("Error");
                }


            });


        }

        //loadData();

        $(function () {
            
        });

        function loadData()
        {
                    console.log("Loadworked");
            $("#ItemID").attr("disabled", true);
            var ItemID= $("#txtItem").val();
            $.ajax({
                method: "GET",
                url: `https://secureapps.quickflora.com/V2/api/ItemDetails?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&ItemID=${ItemID}`,
                
                dataType: 'json',
                success: function (response)
                {
                    if (response)
                    {
                        
                        $("#ItemID").val(response[0].ItemID);
                        $("#ItemName").val(response[0].ItemName);
                        $("#ItemTypeID").val(response[0].ItemTypeID);
                        getFamily(response[0].ItemFamilyID);
                        $("#ItemFamilyID").val(response[0].ItemFamilyID);
                        getCategory(response[0].ItemFamilyID,response[0].ItemCategoryID,response[0].GroupCode);
                        $("#ItemCategoryID").val(response[0].ItemCategoryID);
                        $("#GroupCode").val(response[0].GroupCode);
                        $("#ItemColor").val(response[0].ItemColor);
                        $("#ItemSize").val(response[0].ItemSize);
                        $("#UnitsPerBox").val(response[0].UnitsPerBox);
                        if (response[0].IsActive)
                        {
                            $("#IsActive").prop("checked",true);
                        }
                        
                        if (response[0].ActiveForPOM) {
                            $("#ActiveforPOM").prop("checked", true);
                        }

                        if (response[0].Taxable) {
                            $("#Taxable").prop("checked", true);
                        }
                        
                        $("#ItemDescription").val(response[0].ItemDescription);
                        $("#Price").val(response[0].Price);
                        $("#ItemUOM").val(response[0].ItemUOM);
                        $("#SKU").val(response[0].SKU);
                        $("#ItemPackSize").val(response[0].ItemPackSize);
                        $("#BoxSize").val(response[0].BoxSize);
                        $("#BoxSizeUOM").val(response[0].BoxSizeUOM);
                        /*$("#VendorID option[value=" + response[0].VendorID + "]").attr("selected", "selected");*/
                        $("#VendorID").val(response[0].VendorID);
                        $("#BoxPrice").val(response[0].BoxPrice);
                        $("#ItemUPCCode").val(response[0].ItemUPCCode);
                        $("#GLItemSaleAccount").val(response[0].GLItemSalesAccount);
                        $("#GLItemCOGSAccount").val(response[0].GLItemCOGSAccount);
                        $("#GLItemInventoryAccount").val(response[0].GLItemInventoryAccount);

                       
                        

                    }
                    else {

                    }

                }
            });
        }


        function Save(el)
        {
            $(el).attr("disabled", true);
            var entryType="";
            var item = $("#txtItem").val();
            if (item) {
                entryType = "UpdateItem";
            } else {
                entryType = "InsertItem";
            }

            
                var formData =
                {
                    CompanyID: CompanyID,
                    DivisionID: DivisionID,
                    DepartmentID: DepartmentID,
                    ItemID: $("#ItemID").val(),
                    SKU: $("#SKU").val(),
                    ItemFamilyID: $("#ItemFamilyID").val(),
                    ItemCategoryID: $("#ItemCategoryID ").val(),
                    GroupCode: $("#GroupCode").val(),
                    ItemName: $("#ItemName").val(),
                    ItemDescription: $("#ItemDescription").val(),
                    ItemSize: $("#ItemSize").val(),
                    ItemColor: $("#ItemColor").val(),
                    ItemPackSize: $("#ItemPackSize").val(),
                    UnitsPerBox: $("#UnitsPerBox").val(),
                    BoxSizeUOM: $("#BoxSizeUOM").val(),
                    BoxSize: $("#BoxSize").val(),
                    ItemUOM: $("#ItemUOM").val(),
                    ItemUPCCode: $("#ItemUPCCode").val(),
                    VendorID: $("#VendorID").val(),
                    GLItemSalesAccount: $("#GLItemSaleAccount").val(),
                    GLItemCOGSAccount: $("#GLItemCOGSAccount").val(),
                    GLItemSalesAccountWH: $("#GLItemSaleAccount").val(),
                    GLItemCOGSAccountWH: $("#GLItemCOGSAccount").val(),
                    GLItemInventoryAccount: $("#GLItemInventoryAccount").val(),
                    Price: $("#Price").val(),
                    BoxPrice: $("#BoxPrice").val(),
                    ActiveForPOM: ($("#ActiveforPOM").prop("checked") ? "true" : "false"),
                    IsActive: ($("#IsActive").prop("checked") ? "true" : "false"),
                    Taxable: ($("#Taxable").prop("checked") ? "true" : "false"),
                    ItemTypeID: $("#ItemTypeID").val(),
                    entryType: entryType
                }

                $.ajax({
                    method: "POST",
                    url: `https://secureapps.quickflora.com/V2/api/ItemDetails`,
                    data: formData,
                    dataType: 'json',
                    error: function (){
                        $(el).attr("disabled", false);
                            alert("Something went wrong! Please contact support.");
                    },
                    success: function (reponse) {

                        if (reponse == "Successful") {
                            window.location = "ItemList.aspx";

                        }
                        else {
                        $(el).attr("disabled", false);
                            alert("Something went wrong! Please try again later.");
                        }

                    }
                });

        }

        function clearform()
        {
            $("#ItemID").val('');
            $("#ItemName").val('');
            $("#ItemTypeID").val('');
            $("#ItemFamilyID").val('');
            $("#ItemCategoryID").val('');
            $("#GroupCode").val('');
            $("#ItemColor").val('');
            $("#ItemSize").val('');
            $("#UnitsPerBox").val('');

            $("#IsActive").prop("checked", false);
           

           
            $("#ActiveForPOM").prop("checked", false);
          

           
            $("#Taxable").prop("checked", false);
           



            $("#ItemDescription").val('');
            $("#Price").val('');
            $("#ItemUOM").val('');
            $("#SKU").val('');
            $("#ItemPackSize").val('');
            $("#BoxSize").val('');
            $("#BoxSizeUOM").val('');
            
            $("#VendorID").val('0');
            $("#BoxPrice").val('');
            $("#ItemUPCCode").val('');
            $("#GLItemSaleAccount").val('0');
            $("#GLItemCOGSAccount").val('0');
            $("#GLItemInventoryAccount").val('0');
            $("#lableid").hide();
        }



    </script>



</asp:Content>
