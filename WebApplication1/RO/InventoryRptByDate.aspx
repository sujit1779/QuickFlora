<%@ Page Language="vb" AutoEventWireup="false" CodeFile="InventoryRptByDate.aspx.vb" Inherits="Report_InventoryByLocation" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Report</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.2/css/dataTables.bootstrap5.min.css" />
    <link rel="stylesheet" href="assets/css/fontawesome.min.css" />
    <link rel="stylesheet" href="assets/css/customerList.css" />
    <link rel="stylesheet" href="assets/plugin/waitMe/waitMe.min.css" />
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
                <%--<a class="navbar-brand" href="InventoryDayWiseLog.aspx">Inventory Day Wise Log</a>--%>
                
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        
                    </ul>
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
                        <div class="col-12 col-md-2">
                            <label for="fdate">From Date :</label>
                            <input type="date" class="form-control" id="fdate" required />
                        </div>

                        <div class="col-12 col-md-2">
                            <label for="todate">To Date :</label>
                            <input type="date" class="form-control" id="todate" required />
                        </div>
                        <div class="col-12 col-md-3">
                            <label for="location">Location :</label>
                            <select id="location" class="form-control drpLocation">
                                <%--<option selected="selected" value="">-All-</option>--%>
                            </select>
                        </div>
                        <div class="col-12 col-md-2">
                            <label for="status">Item Status :</label>
                            <select id="status" class="form-control">
                                <option value="">-All-</option>
                                <option selected="selected" value="True">Active</option>
                                <option value="False">Inactive</option>
                            </select>
                        </div>
                        <div class="col-12 col-md-3 pt-4 text-center">
                            <input type="checkbox" id="available" checked="checked" />
                            <label for="available">Show Available Only</label>

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
                    <h4 class="col-12">Inventory Report</h4>
                </div>

                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Item ID</th>
                                <th>Item Name</th>
                                <th>UOM</th>
                                <th>Location</th>
                                <%--<th>Physical QTY</th>--%>
                                <th>Sold QTY</th>
                                <th>QTY On Hand</th>
                                <th>BackOrder Qty</th>
                                <th>Qty To Receive</th>
                                <th>Oversold</th>
                                <th>Available To Sale</th>
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
                    <input type="text" id="txtItemID" class="form-control" disabled />
                    <label for="updateLocation" class="mt-4">Location :</label>
                    <select id="updateLocation" class="form-control drpLocation" disabled>
                    </select>
                    <label for="qtyHand" class="mt-4">Qty On Hand :</label>
                    <input type="text" id="qtyHand" class="form-control"/>
                            <input type="hidden" id="row-id" />
                        
                    <div class="text-end mt-4"><button type="button" id="addbh" class="btn btn-primary btn-sm border-0"  >Submit</button></div>
                </div>
            </div>
        </div>
    </div>

    </form>

    <script>

        var CompanyID = "<%= CompanyID %>";
        var DivisionID = "<%= DivisionID %>";
        var DepartmentID = "<%= DepartmentID %>";

        today();

        function today() {
            // $('input[type="date"]').attr('disabled',true);
            var date = new Date();
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();
            var today = y + '-' + m + '-' + d;

            
            var friday = getNextFriday(new Date());
            
            

            var m1 = ("0" + (friday.getMonth() + 1)).slice(-2);
            var d1 = ("0" + friday.getDate()).slice(-2);
            var y1 = friday.getFullYear();
            var today1 = y1 + '-' + m1 + '-' + d1;

            $('#fdate').val(today);
            $('#todate').val(today1);
        }

        function getNextFriday(date = new Date()) {
          const dateCopy = new Date(date.getTime());

          const nextFriday = new Date(
            dateCopy.setDate(
              dateCopy.getDate() + ((7 - dateCopy.getDay() + 5) % 7 || 7),
            ),
          );

          return nextFriday;
        }

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
                    //loaddata();
                    applyDatatable('');
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


                var item = $("#txtItemID").val();
                var location = $("#updateLocation").val();
               
                 var qty= $("#qtyHand").val();


                $.ajax({
                    type: "PUT",
                    url: `https://secureapps.quickflora.com/V2/api/InventorybyDateRangeCal?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${item}&location=${location}&qty=${qty}&emp=<%=Session("EmployeeID")%>`,
                    encode: true
                }).done(function (data) {
                    var id = $("#row-id").val();
                    $("#row-id").val('');
                    //console.log(data);
                    //bind back the gird of emails on page
                    $('#txtItemID').val('');
                    $('#qtyHand').val('0');
                    $("#updateLocation").val('');
                    $("#editInventory").modal('hide');
                    loadRowdata(item, location,id);


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
                $('.drpLocation').val('DEFAULT');
            }
        });

        //loaddata();


        function loaddata() {

            loadStart();
            var search = $('input[type="search"]').val();
            var location = $('#location').val();
            var itemID = '';
            var fdate = $('#fdate').val();
            var todate = $('#todate').val();
            var color = $('#color').val();
            var size = $('#size').val();
            var familyID = ($('#family').val() ? $('#family').val() : '');
            var status = ($('#status').val() ? $('#status').val() : '');
            var category = ($('#ItemCategoryID').val() ? $('#ItemCategoryID').val() : '');
            var grp = ($('#GroupCode').val() ? $('#GroupCode').val() : '');
            var available = ($('#available').prop("checked") ? 1 : 0);
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/InventorybyDateRangeCal?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&fromDate=${fdate}&toDate=${todate}&location=${location}&family=${familyID}&category=${category}&color=${color}&size=${size}&grp=${grp}&available=${available}&active=${status}`,
                dataType: 'json',
                success: function (response) {
                    if (response.length) {
                        buildTable(response, search);
                    } else {
                        applyDatatable('');
                    }

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
            var Locat = $("#location").val();
            records.forEach(data => {
                id++;
                list.push(
                    [
                        data.ItemID,
                        data.ItemName,
                        data.ItemUOM,
                        Locat,
                        //`<span id="${id}_PQ">${(data.PhysicalQty)}</span>`,
                        `<span id="${id}_SQ">${(data.QtySold)}</span>`,
                        `<span id="${id}_QOH">${(data.TotalOnHandQty)}</span>`,
                        `<span id="${id}_QC">${(data.TotalBackOrderQty)}</span>`,
                        `<span id="${id}_QOO">${(data.TotalPurchaseQty)}</span>`,
                        (data.Diff < 0 ? `<span class="text-danger" style="font-weight:bold">${Math.abs(data.Diff)}</span>` : 0),
                        (data.Diff > 0 ? `<span class="text-success" style="font-weight:bold">${Math.abs(data.Diff)}</span>` : 0),
                        `<a href="Javascript:;" class="btn btn-primary border-0 btn-sm p-2" title="Edit" onclick="Javascript:edit('${data.ItemID}','${Locat}','${id}');"><i class="fas fa-pen"></i></a>`
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
                 "pageLength": 100,
                 "lengthMenu": ['100','200','300','500','1000'],
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
            today();
        }

        function changeSize(el) {
            let size = $(el).val();
            $('body').css('font-size', size);
        }


        function edit(ItemID, LocationID,id) {
            //$.getJSON("../AjaxInventorybylocation.aspx?ItemID=" + ItemID + "&LocationID=" + LocationID, function (data) {
            $.getJSON(`https://secureapps.quickflora.com/V2/api/Inventorybylocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${ItemID}&location=${LocationID}&family=&category=&color=&size=&grp=`, function (data) {
                $.each(data, function (key, value) {
                    $('#txtItemID').val(value.ItemID);
                    $('#qtyHand').val(value.QtyOnHand);
                    $('#qtyCommitted').val(value.QtyCommitted);
                    $('#qtyOrder').val(value.QtyOnOrder);
                    $('#qtyBack').val(value.QtyOnBackorder);
                    $('#reOrder').val(value.ReOrderQty);
                    $("#updateLocation").val(value.LocationID);
                    $("#editCost").val(value.Cost);
                    $("#row-id").val(id);
                    $("#editInventory").modal('show');
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
                    url: 'https://secureapps.quickflora.com/V2/api/EmployeeUsedPages',
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