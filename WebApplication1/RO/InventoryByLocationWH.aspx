<%@ Page Language="vb" AutoEventWireup="false" CodeFile="InventoryByLocationWH.aspx.vb" Inherits="RO_InventoryByLocation" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory By Location</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <link href="assets/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.2/css/dataTables.bootstrap5.min.css"/>
    <link rel="stylesheet" href="assets/css/fontawesome.min.css"  />
    <link rel="stylesheet" href="assets/css/customerList.css"  />
    <link rel="stylesheet" href="assets/plugin/waitMe/waitMe.min.css"  />
    <link href="assets/plugin/toast/build/toastr.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/plugin/typehead/dist/jquery.typeahead.min.css"/>
    <script src="./assets/js/jquery.min.js"></script>
    <script src="./assets/js/bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.2/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.2/js/dataTables.bootstrap5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.0.0/js/dataTables.buttons.min.js"></script> 
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.0.0/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="assets/js/fontawesome.min.js"></script>
    <script type="text/javascript" src="assets/plugin/waitMe/waitMe.min.js"></script>
    <script src="assets/plugin/toast/toastr.js"></script>
    <script type="text/javascript" src="assets/plugin/typehead/dist/jquery.typeahead.min.js"></script>

    <style>
        .buttons-html5 {
            background: #678B38;
            color: #ffffff;
            border-radius: 5px;
            border: none;
            padding: 4px 8px;
        }

        .page-item.active .page-link {
            background-color: #678b38 !important;
            color: #ffffff !important;
            border-color: #678b38 !important;
        }

        .page-item .page-link {
            color: #678b38 !important;
        }
        .green-border{
            border: 2px solid #678B38;
        }
        .portlet-title{
            background-color:#678B38;
            color:white;
            border-radius:0!important;
        }
        .portlet-body{
            padding:1.25rem;
        }
        .modal-header{
            background-color:#678B38;
            color:white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">


        <nav class="navbar navbar-expand-lg navbar-dark bg-dark" style="background-color: #678B38!important;">
            <div class="container-fluid text-white">
                <a class="navbar-brand" href="../Home.aspx">Home</a>
                
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="#">Inventory By Location</a>
                        </li>
                        <!-- <li class="nav-item">
                <a class="nav-link" href="#">Link</a>
              </li>
              <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                  Dropdown
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                  <li><a class="dropdown-item" href="#">Action</a></li>
                  <li><a class="dropdown-item" href="#">Another action</a></li>
                  <li><hr class="dropdown-divider"></li>
                  <li><a class="dropdown-item" href="#">Something else here</a></li>
                </ul>
              </li>
              <li class="nav-item">
                <a class="nav-link disabled" href="#" tabindex="-1" aria-disabled="true">Disabled</a>
              </li> -->
                    </ul>
                    <!-- <form class="d-flex">
              <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
              <button class="btn btn-outline-success" type="submit">Search</button>
            </form> -->
                </div>
            </div>
        </nav>

        <div class="container-fluid">
            <div class="card mt-3 text-wrap">
                <div class="card-header row m-0">
                    <h4 class="col-10 p-0">Filter Options</h4>
                </div>

                <div class="card-body overflow-auto">
                    <div class="row m-0">
                        <div class="col-12 col-md-12">
                            <label for="location">Location :</label>
                            <select id="location" class="form-control drpLocation">
                                <option selected="selected" value="">-All-</option>
                            </select>
                        </div>
                    </div>
                    <div class="row m-0 mt-2">
                        <div class="col-12 col-md-2">
                            <label for="location">Item Family :</label>
                            <select id="family" class="form-control" onChange="getCategory('','','')">
                            </select>
                        </div>
                        <div class="col-12 col-md-3">
                            <%--<label for="itemID">ItemID :</label>
                            <input type="text" id="itemID" class="form-control" />--%>
                            <label for="ItemCategoryID">Item Category</label>
                            <select class="form-control " id="ItemCategoryID" onchange="getGroup('','')">
                            </select>
                        </div>
                        <div class="col-12 col-md-3">
                            <label for="GroupCode">Item Group</label>
                            <select class="form-control " id="GroupCode">
                            </select>

                        </div>
                        <div class="col-12 col-md-2">
                            <label for="color">Item Color</label>
                            <input type="text" class="form-control" id="color" />

                        </div>
                        <div class="col-12 col-md-2">
                            <label for="color">Item Size</label>
                            <input type="text" class="form-control" id="size" />

                        </div>
                    </div>
                    <div class="text-end pt-3">
                        <button type="button" class="btn btn-primary border-0" onclick="loaddata()">Search</button>
                        <button type="button" class="btn btn-light" onclick="clearData()">Clear</button>
                    </div>
                </div>
            </div>

            <div class="card mt-3 text-wrap" id="loader">
                <div class="card-header row m-0">
                    <h4 class="col-12">Inventory By Location</h4>
                </div>

                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Item ID</th>
                                <th>Item Name</th>
                                <th>Location</th>
                                <th>QTY On Hand</th>
                                <th>QTY Committed</th>
                                <th>QTY On Order</th>
                                <th>QTY Backordered</th>
                                <th>Re-Order QTY</th>
                                <th>Average Cost</th>
                                <th>Total Stock Value</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

    <%-- Edit Inventoy Modal --%>
    <div class="modal fade" id="editInventory" tabindex="-1" role="dialog" aria-labelledby="formModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Inventory</h5>
                    <button type="button" class="btn-close" onclick="closeModal('editInventory')"></button>
                </div>
                <div class="modal-body">
                    <label for="txtItemID">ItemID :</label>
                    <input type="text" id="txtItemID" class="form-control"/>
                    <label for="updateLocation" class="mt-4">Location :</label>
                    <select id="updateLocation" class="form-control drpLocation">
                    </select>
                    <div class="row mt-4">
                        <div class="col-12 col-md-4">
                             <label for="qtyHand">Qty On Hand :</label>
                            <input type="text" id="qtyHand" class="form-control"/>
                        </div>
                        <div class="col-12 col-md-4">
                             <label for="qtyCommitted">Qty Committed :</label>
                            <input type="text" id="qtyCommitted" class="form-control"/>
                        </div>
                        <div class="col-12 col-md-4">
                             <label for="qtyOrder">Qty On Order :</label>
                            <input type="text" id="qtyOrder" class="form-control"/>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-12 col-md-4">
                             <label for="qtyBack">Qty Backordered :</label>
                            <input type="text" id="qtyBack" class="form-control"/>
                        </div>
                        <div class="col-12 col-md-4">
                             <label for="reOrder">Re-Order Qty :</label>
                            <input type="text" id="reOrder" class="form-control"/>
                        </div>
                        <div class="col-12 col-md-4">
                             <label for="editCost">Average Cost :</label>
                            <input type="text" id="editCost" class="form-control" value="0.00"/>
                            <input type="hidden" id="row-id" />
                         </div>
                    </div>
                    <div class="text-end mt-4"><button type="button" id="addbh" class="btn btn-primary btn-sm border-0"  >Submit</button></div>
                </div>
            </div>
        </div>
    </div>

     <%-- Add Inventoy Modal --%>
    <div class="modal fade" id="addInventory" tabindex="-1" role="dialog" aria-labelledby="formModal" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add New Inventory</h5>
                    <button type="button" class="btn-close" onclick="closeModal('addInventory')"></button>
                </div>
                <div class="modal-body">
                    <label for="txtItemID2">ItemID :</label>
                    <input type="text" id="txtItemID2" class="form-control" disabled="disabled" />
                    <label for="addLocation" class="mt-4">Location :</label>
                    <select id="addLocation" class="form-control drpLocation" disabled="disabled">
                    </select>
                    <label for="qtyHandadd" class="mt-4">Qty On Hand Presently:</label>
                    <input type="text" id="qtyHandadd" class="form-control" disabled="disabled"/>

                    <div class="row mt-4">
                        <div class="col-6">
                             <label for="addNewQty">Add New Qty:</label>
                            <input type="text" id="addNewQty" class="form-control"/>
                        </div>
                        <div class="col-6">
                             <label for="txtvendor">Average Cost :</label>
                            <input type="text" id="avgCost" class="form-control" value="0.00"/>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-12">
                             <label for="txtvendor">Vendor Name :</label>
                            <input type="text" id="txtvendor" class="form-control"/>
                        </div>
                    </div>
                    <div class="text-end mt-4"><button type="button" id="addbh2" class="btn btn-primary btn-sm border-0">Submit</button></div>
                </div>
            </div>
        </div>
    </div>

    </form>

    <script>

        var CompanyID = "<%= CompanyID %>";
        var DivisionID = "<%= DivisionID %>";
        var DepartmentID = "<%= DepartmentID %>";

        //Order Location API Call
        //$.ajax({
        //    method: 'GET',
        //    url: `https://secureapps.quickflora.com/V2/api/OrderLocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}`,
        //    dataType: 'json',
        //    success: function (response) {
        //        var options_html = '';
        //        if (response) {
        //            response.forEach(location => {
        //                if (location.LocationName) {
        //                    options_html += `<option value="${location.LocationID}">${location.LocationName}</option>`;
        //                }
        //            });
        //            $('#cmblocationid').append(options_html);
        //        }
        //    }
        //});
        getFamily('Fresh');

        function getFamily(id)
        {
            $.ajax({
                method: 'GET',
                url: `../AjaxFamily.aspx`,
                dataType: "json",
                encode: true,
                success: function (data)
                {

                    var ItemFamilyID = $("[id*=family]");
                    ItemFamilyID.empty().append('<option selected="selected" value="">Select Family</option>');
                    $.each(data, function (key, value)
                    {
                        var sel = '';
                        if (id == value.ItemFamilyID) {
                            sel = "selected='selected'";
                        }
                        $("#family").append($(`<option ${sel}></option>`).val(value.ItemFamilyID).html(value.FamilyName));
                        
                    });
                    loaddata();
                    getCategory(id, '', '');
                },
                error: function (data) {
                    alert("Error");
                }
                

            });
        }
        function getCategory(ItemFamilyID,cat,grp)
        {
            if (!ItemFamilyID) {
                ItemFamilyID = $('#family').val();
            }
            $.ajax({
                method: 'GET',
                url: `../AjaxCategories.aspx?id=${ItemFamilyID}`,
                dataType: "json",
                encode: true,
                success: function (data) {

                    var ItemCategoryID = $("[id*=ItemCategoryID]");
                    ItemCategoryID.empty().append('<option value="">Select Category</option>');
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
            var ItemFamilyID = $('#family').val();
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
                    GroupCode.empty().append('<option value="">Select Group</option>');
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

        jQuery(document).ready(function () {

            // initiate layout and plugins
            //SEARCH();

            //$.getJSON("AjaxInventorybylocation.aspx", function (data) {
            //    binddata(data);
            //});


            // Add button click Event start from here
            $("#addbh").click(function () {

                //Validate email to proceed
                if ($('#txtItemID').val() == "") {
                    alert('Please enter ItemID for add');
                    $('#txtItemID').css('border-color', 'red');
                    document.getElementById("txtItemID").focus();
                    return;
                }
                else {
                    $('#txtItemID').css('border-color', '');
                }

                //Validate email to proceed
                if ($('#updateLoation').val() == "") {
                    alert('Please select Location for add');
                    $('#updateLoation').css('border-color', 'red');
                    document.getElementById("updateLoation").focus();
                    return;
                }
                else {
                    $('#updateLoation').css('border-color', '');
                }


                //new section
                if ($('#qtyHand').val() == "") {
                    alert('Please enter Amount for Qty OnHand ');
                    $('#qtyHand').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#qtyHand').val())) {
                        $('#qtyHand').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Qty OnHand ');
                        $('#qtyHand').css('border-color', 'red');
                        return;

                    }

                }
                //new section

                //new section
                if ($('#qtyCommitted').val() == "") {
                    alert('Please enter Amount for Qty Committed ');
                    $('#qtyCommitted').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#qtyCommitted').val())) {
                        $('#qtyCommitted').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Qty Committed ');
                        $('#qtyCommitted').css('border-color', 'red');
                        return;

                    }

                }
                //new section
                //new section
                if ($('#qtyOrder').val() == "") {
                    alert('Please enter Amount for Qty On Order ');
                    $('#qtyOrder').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#qtyOrder').val())) {
                        $('#qtyOrder').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Qty On Order ');
                        $('#qtyOrder').css('border-color', 'red');
                        return;

                    }

                }
                //new section

                //new section
                if ($('#qtyBack').val() == "") {
                    alert('Please enter Amount for Qty Back ordered ');
                    $('#qtyBack').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#qtyBack').val())) {
                        $('#qtyBack').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Qty Back ordered ');
                        $('#qtyBack').css('border-color', 'red');
                        return;

                    }

                }
                //new section

                if ($('#editCost').val() == "") {
                    alert('Please enter Amount for Average Cost you want to add ');
                    $('#editCost').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#editCost').val())) {
                        $('#editCost').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Average Cost you want to add ');
                        $('#editCost').css('border-color', 'red');
                        return;

                    }

                }

                //new section
                if ($('#reOrder').val() == "") {
                    alert('Please enter Amount for Qty Reordered ');
                    $('#reOrder').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#reOrder').val())) {
                        $('#reOrder').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Qty Reordered ');
                        $('#reOrder').css('border-color', 'red');
                        return;

                    }

                }
                //new section

                var item = $("#txtItemID").val();
                var location = $("#updateLocation").val();
                // return;
                //Prepae formdata to submit
                var formData = {
                    txtItemID: $("#txtItemID").val(),
                    txtQtyOnHand: $("#qtyHand").val(),
                    txtQtyCommitted: $("#qtyCommitted").val(),
                    txtQtyOnOrder: $("#qtyOrder").val(),
                    txtQtyBackordered: $("#qtyBack").val(),
                    txtQtyReordered: $("#reOrder").val(),
                    location: $("#updateLocation").val(),
                    avg: $("#editCost").val()
                };


                $.ajax({
                    type: "POST",
                    url: "../AjaxInventorybylocation.aspx",
                    data: formData,
                    dataType: "json",
                    encode: true
                }).done(function (data) {
                    var id = $("#row-id").val();
                    $("#row-id").val('');
                    //console.log(data);
                    //bind back the gird of emails on page
                    $('#txtItemID').val('');
                    $('#qtyHand').val('0');
                    $('#qtyCommitted').val('0');
                    $('#qtyOrder').val('0');
                    $('#qtyBack').val('0');
                    $('#reOrder').val('0');
                    $("#updateLocation").val('');
                    $("#editCost").val('0')
                    $("#editInventory").modal('hide');
                    loadRowdata(item, location,id);


                });

            });


            // Add button click Event start from here
            $("#addbh2").click(function () {


                //new section
                if ($('#addNewQty').val() == "") {
                    alert('Please enter Amount for Qty OnHand you want to add ');
                    $('#addNewQty').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#addNewQty').val())) {
                        $('#addNewQty').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Qty OnHand you want to add ');
                        $('#addNewQty').css('border-color', 'red');
                        return;

                    }

                }
                if ($('#avgCost').val() == "") {
                    alert('Please enter Amount for Average Cost you want to add ');
                    $('#avgCost').css('border-color', 'red');
                    return;
                }
                else {
                    if ($.isNumeric($('#avgCost').val())) {
                        $('#avgCost').css('border-color', '');
                    }
                    else {
                        alert('Please enter numeric Amount for Average Cost you want to add ');
                        $('#avgCost').css('border-color', 'red');
                        return;

                    }

                }
                //new section



                // return;
                //Prepae formdata to submit
                //var formData = {
                //    txtItemID2: $("#txtItemID2").val(),
                //    txtQtyOnHand2: $("#addNewQty").val(),
                //    txtlocation: $("#addLocation").val(),
                //    txtvendor: $("#txtvendor").val()

                //};


                //$.ajax({
                //    type: "POST",
                //    url: "../AjaxInventorybylocation.aspx",
                //    data: formData,
                //    dataType: "json",
                //    encode: true
                //}).done(function (data) {
                //    //console.log(data);
                //    //bind back the gird of emails on page
                //    $('#txtItemID2').val('');
                //    $('#addNewQty').val('0');
                //    $('#addLocation').val('0');
                //    $("#addInventory").modal('hide');
                //    loaddata();

                //});
                var itemID= $("#txtItemID2").val();
                var qty= $("#addNewQty").val();
                var location=$("#addLocation").val();
                var cost= $("#avgCost").val();

                $.ajax({
                    type: "Put",
                    url: `https://secureapps.quickflora.com/V2/api/Inventorybylocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${itemID}&location=${location}&qty=${qty}&cost=${cost}`,
                }).done(function (data) {
                    //console.log(data);
                    //bind back the gird of emails on page
                    if (data == "Successful") {
                        var id = $("#row-id").val();
                    $("#row-id").val('');
                        $('#txtItemID2').val('');
                        $('#addNewQty').val('0');
                        $('#addLocation').val('0');
                        $('#avgCost').val('0');
                        $("#addInventory").modal('hide');
                        loadRowdata(itemID, location,id);
                    } else {
                        alert("Something Went Wrong!");
                    }
                });

            });


        });
        //end of event add

        //Order Location API Call
        $.ajax({
            method: 'GET',
            url: `https://secureapps.quickflora.com/V2/api/OrderLocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}`,
            dataType: 'json',
            success: function (response) {
                var options_html = '';
                if (response) {
                    response.forEach(location => {
                        if (location.LocationName) {
                            options_html += `<option value="${location.LocationID}">${location.LocationName}</option>`;
                        }
                    });
                    $('.drpLocation').append(options_html);
                }
            }
        });

        //loaddata();


        function loaddata() {

            loadStart();
            var search = $('input[type="search"]').val();
            var location = $('#location').val();
            var itemID = '';
            var color = $('#color').val();
            var size = $('#size').val();
            var familyID = ($('#family').val() ? $('#family').val() : '');
            var category = ($('#ItemCategoryID').val() ? $('#ItemCategoryID').val() : '');
            var grp = ($('#GroupCode').val() ? $('#GroupCode').val() : '');
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/Inventorybylocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${itemID}&location=${location}&family=${familyID}&category=${category}&color=${color}&size=${size}&grp=${grp}`,
                dataType: 'json',
                success: function (response) {
                    buildTable(response, search);
                    loadStop();

                }
            });

        }

        function loadRowdata(itemID,location,id) {

            loadStart();
            var color = '';
            var size = '';
            var familyID = '';
            var category = '';
            var grp = '';
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/Inventorybylocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${itemID}&location=${location}&family=${familyID}&category=${category}&color=${color}&size=${size}&grp=${grp}`,
                dataType: 'json',
                success: function (response) {
                    if (response.length) {
                        var data = response[0];

                        $("#" + id + "_QOH").text((data.QtyOnHand ? data.QtyOnHand : 0));
                        $("#" + id + "_QC").text((data.QtyCommitted ? data.QtyCommitted : 0));
                        $("#" + id + "QOO").text((data.QtyOnOrder ? data.QtyOnOrder : 0));
                        $("#" + id + "_QOB").text((data.QtyOnBackorder ? data.QtyOnBackorder : 0));
                        $("#" + id + "_ROQ").text((data.ReOrderQty ? data.ReOrderQty : 0));
                        $("#" + id + "_Cost").text((data.Cost ? data.Cost : 0));
                        $("#" + id + "_TC").text((data.TotalValue ? data.TotalValue : 0));
                    }
                        loadStop();
                }
            });

        }

        function buildTable(records, search) {
            var list = [];
            var id = 0;
            records.forEach(data => {
                id++;
                list.push(
                    [
                        data.ItemID,
                        data.ItemName,
                        data.LocationID,
                        `<span id="${id}_QOH">${(data.QtyOnHand ? data.QtyOnHand : 0)}</span>`,
                        `<span id="${id}_QC">${(data.QtyCommitted ? data.QtyCommitted : 0)}</span>`,
                        `<span id="${id}_QOO">${(data.QtyOnOrder ? data.QtyOnOrder : 0)}</span>`,
                        `<span id="${id}_QOB">${(data.QtyOnBackorder ? data.QtyOnBackorder : 0)}</span>`,
                        `<span id="${id}_ROQ">${(data.ReOrderQty ? data.ReOrderQty : 0)}</span>`,
                        `<span id="${id}_Cost">${(data.Cost ? data.Cost : 0)}</span>`,
                        `<span id="${id}_TC">${(data.TotalValue ? data.TotalValue : 0)}</span>`,
                        `<a href="Javascript:;" class="btn btn-primary border-0 btn-sm p-2" title="Add" onclick="Javascript:add('${data.ItemID}','${data.LocationID}','${id}');"><i class="fas fa-plus"></i></a>
                        <a href="Javascript:;" class="btn btn-primary border-0 btn-sm p-2" title="Edit" onclick="Javascript:edit('${data.ItemID}','${data.LocationID}','${id}');"><i class="fas fa-pen"></i></a>`
                    ]
                );
            });
            applyDatatable(list, search);

        }

        function applyDatatable(tableData, search) {
            loadStop();
            if ($.fn.DataTable.isDataTable('#details')) {
                $('#details').DataTable().destroy();
                $('tbody').html('');
            }
             var table = $('#details').DataTable({
                dom: 'frBtlip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'pdfHtml5',
                        title: 'Inventory By Location',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    }
                ],
                title: 'Inventory By Location',
                "order": [[ 1, "asc" ]],
                data: tableData
            });
            if (search) {
                table.search(search).draw();
            }
        }
        function loadStart() {
            $('#loader').waitMe({
                effect: 'win8_linear',
                text: '',
                bg: 'rgba(255,255,255,0.7)',
                color: '#678b38',
                waitTime: -1,
                textPos: 'vertical',
                onClose: function () { }
            });
        }

        function loadStop() {
            $('#loader').waitMe('hide');
        }


        function formatDate(date) {
            date = new Date(date);
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();
            return m + '/' + d + '/' + y;
        }

        function clearData() {
            $('#itemID').val('');
            $('#location').val('');
            $('#ItemCategoryID').val('');
            $('#GroupCode').val('');
            $('#color').val('');
            $('#size').val('');
            loaddata();
        }

        function changeSize(el) {
            let size = $(el).val();
            $('body').css('font-size', size);
        }


        function edit(ItemID, LocationID,id) {
            $.getJSON("../AjaxInventorybylocation.aspx?ItemID=" + ItemID + "&LocationID=" + LocationID, function (data) {
                $.each(data, function (key, value) {
                    $('#txtItemID').val(value.ItemID);
                    $('#qtyHand').val(value.QtyOnHand);
                    $('#qtyCommitted').val(value.QtyCommitted);
                    $('#qtyOrder').val(value.QtyOnOrder);
                    $('#qtyBack').val(value.QtyOnBackorder);
                    $('#reOrder').val(value.QtyOnBackorder);
                    $("#updateLocation").val(value.LocationID);
                    $("#editCost").val(value.AverageCost);
                    $("#row-id").val(id);
                    $("#editInventory").modal('show');
                });
            });
        }


        function add(ItemID, LocationID,id) {
            $.getJSON("../AjaxInventorybylocation.aspx?Add=yes&ItemID=" + ItemID + "&LocationID=" + LocationID, function (data) {
                $.each(data, function (key, value) {
                    $('#txtItemID2').val(value.ItemID);
                    $("#row-id").val(id);
                    $('#qtyHandadd').val(value.QtyOnHand);
                    $("#addLocation").val(value.LocationID);
                    $("#addInventory").modal('show');
                });
            });
        }

        function closeModal(id) {
            $('#' + id).modal('hide');
        }
    </script>

    <%-- Create Log Script --%>
    <script>
        AddSessionLog();
        function AddSessionLog() {
            if ("<%= Session("CompanyID") %>" && "<%= Session("EmployeeID") %>") {

                var Path = window.location.pathname;
                var PathString = window.location.href;
                var QString = PathString.split("?");
                var URLNames = Path.split("/");
                var page = URLNames[URLNames.length - 1];
                page = page.split(".");

                if (typeof page[0] == 'string') { } else { page[0] = ''; }
                if (typeof QString[QString.length - 1] == 'string') { } else { QString[QString.length - 1] = ''; }
                var formData = {
                    "CompanyID": "<%= Session("CompanyID") %>",
                    "DivisionID": "<%= Session("DivisionID") %>",
                    "DepartmentID": "<%= Session("DepartmentID") %>",
                    "EmployeeID": "<%= Session("EmployeeID") %>",
                    "PageName": page[0],
                    "PageDetails": QString[QString.length - 1]
                };
                $.ajax({
                    method: 'POST',
                    url: 'https://secureapps.quickflora.com/V2/api/EmployeeActionLogPOM',
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response = "Successful") {
                        } else {
                            //toastr.error('Something Went Worng!');
                        }
                    }
                });
            }
        }
    </script>
    
    
</body>
</html>