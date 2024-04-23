<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreBookList.aspx.vb" Inherits="RO_PreBookList" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Allocation Products</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.2/css/dataTables.bootstrap5.min.css" />
    <link rel="stylesheet" href="assets/css/fontawesome.min.css" />
    <link rel="stylesheet" href="assets/css/customerList.css" />
    <link rel="stylesheet" href="assets/plugin/waitMe/waitMe.min.css" />
    <link rel="stylesheet" href="assets/plugin/typehead/dist/jquery.typeahead.min.css" />
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
        .scrollable-dropdown-menu .tt-dropdown-menu {
          max-height: 150px;
          overflow-y: auto;
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
                            <a class="nav-link active" aria-current="page" href="#">Allocation Quantity</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <%--<div class="container-fluid">
            <div class="card mt-3 text-wrap">
                <div class="card-header row m-0">
                    <h4 class="col-10 p-0">Filter Options</h4>
                </div>

                <div class="card-body overflow-auto">
                    
                    <div class="row m-0">
                        <div class="col-12 col-md-3">
                            <label for="SshipDate">Start Shipping Date :</label>
                            <input type="date" class="form-control" id="SshipDate"/>
                        </div>
                        <div class="col-12 col-md-3">
                            <label for="EshipDate">End Shipping Date :</label>
                            <input type="date" class="form-control" id="EshipDate"/>
                        </div>
                        <div class="col-12 col-md-3">
                            <label for="ItemID">Item Code :</label>
                            <div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpItems" placeholder="Search Item Code" autocomplete="off" id="ItemID" />
                                     </div>
                                 </div>
                             </div>
                        </div>
                        <div class="col-12 col-md-3">
                            <label for="vendor">Grower :</label>
                            <div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="vendor" />
                                     </div>
                                 </div>
                             </div>
                        </div>
                    </div>
                    <div class="row m-0 mt-3">
                        <div class="col-12 mt-4 text-end">
                            <button type="button" class="btn btn-primary border-0" onclick="loaddata()">Search</button>
                            <button type="button" class="btn btn-light" onclick="clearData()">Clear</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-3 text-wrap" id="loader">
                <div class="card-header row m-0">
                    <h4 class="col-12 col-md-8">Pre Book Products</h4>
                </div>

                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Item Code</th>
                                 <th>RO Number</th>
                                <th>Location</th>
                                <th>Ship date</th>
                                <th>QOH</th>
                                <th>UOM</th>
                                <th>Type</th>
                                <th>Pre-Sold</th>
                                <th>Remarks</th>
                                <th>Requested QTY</th>
                                <th>Allocated QTY</th>
                                <th>Allocate QTY</th>
                                <th>Total Quantity</th>
                                <th>Balance Quantity</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>--%>


        <div class="container-fluid">
            <%--<div class="card mt-3 text-wrap">
                <div class="card-header row m-0">
                    <h4 class="col-10 p-0">Filter Options</h4>
                </div>

                <div class="card-body">
                    
                    <div class="row m-0">
                        <div class="col-12 col-md-4">
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <label for="SshipDate">Start Shipping Date :</label>
                                    <input type="date" class="form-control" id="SshipDate" />
                                </div>
                                <div class="col-12 col-md-6">
                                    <label for="EshipDate">End Shipping Date :</label>
                                    <input type="date" class="form-control" id="EshipDate" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-md-8">
                            <div class="row">
                                <div class="col-12 col-md-4">
                                    <label for="ItemID">Item Code :</label>
                                    <div class="typeahead__container">
                                        <div class="typeahead__field">
                                            <div class="typeahead__query">
                                                <input class="typehead drpItems" placeholder="Search Item Code" autocomplete="off" id="ItemID" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-4">
                                    <label for="vendor">Grower :</label>
                                    <div class="typeahead__container">
                                        <div class="typeahead__field">
                                            <div class="typeahead__query">
                                                <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="vendor" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-4">
                                    <label for="vendor">Location :</label>
                                    <select id="location" class="form-control drpLocation">
                                        <option value="">-- All --</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0 mt-3">
                        <div class="col-12 mt-4 text-end">
                            <button type="button" class="btn btn-primary border-0" onclick="loaddata()">Search</button>
                            <button type="button" class="btn btn-light" onclick="clearData()">Clear</button>
                        </div>
                    </div>
                </div>
            </div>--%>
             <div class="card mt-3 text-wrap">
                <div class="card-header row m-0">
                    <h4 class="col-10 p-0">Filter Options</h4>
                </div>

                <div class="card-body">

                    <div class="row m-0">
                        <div class="col-12 col-md-4">
                            <label for="SshipDate">Start Shipping Date :</label>
                            <input type="date" class="form-control" id="SshipDate" />
                        </div>
                        <div class="col-12 col-md-4">

                            <label for="EshipDate">End Shipping Date :</label>
                            <input type="date" class="form-control" id="EshipDate" />
                        </div>
                        <div class="col-12 col-md-4">
                            <label for="ItemID">Item Code :</label>
                            <div class="typeahead__container">
                                <div class="typeahead__field">
                                    <div class="typeahead__query">
                                        <input class="typehead drpItems" placeholder="Search Item Code" autocomplete="off" id="ItemID" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row  m-0 mt-3">

                        <div class="col-12 col-md-4">
                            <label for="vendor">Grower :</label>
                            <div class="typeahead__container">
                                <div class="typeahead__field">
                                    <div class="typeahead__query">
                                        <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="vendor" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <label for="vendor">Location :</label>
                            <select id="location" class="form-control drpLocation">
                                <option value="">-- All --</option>
                            </select>
                        </div>
                        <div class="col-12 col-md-4">
                            <label for="preGrower">PreBook Grower :</label>
                            <select id="preGrower" class="form-control drpGrower">
                                <option value="">-- All --</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row m-0 mt-3">
                    <div class="col-12 mb-4 text-end">
                        <button type="button" class="btn btn-primary border-0" onclick="loaddata()">Search</button>
                        <button type="button" class="btn btn-light" onclick="clearData()">Clear</button>
                    </div>
                </div>
            </div>

            <div class="card mt-3 text-wrap" id="loader">
                <div class="card-header row m-0">
                    <h4 class="col-12 col-md-8">Allocation Quantity</h4>
                    <div class="col-12 col-md-4 text-end pt-1">
                        <button type="button" class="btn btn-primary border-0" onclick="allocateAll('')">Allocate QTY</button>
                    
                    </div>
                </div>
                
                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Item Code</th>
                                <%--
                                <th>Action</th>--%>
                                 <th>RO Number</th>
                                <th>Location</th>
                                <th>Ship date</th>
                                <th>QOH</th>
                                <th>UOM</th>
                                <th>Type</th>
                                <th>Pack</th>
                                <th>Color / Variety</th>
                                <th>Pre-Sold</th>
                                <th>Remarks</th>
                                <th>Requested QTY</th>
                                <th>Allocated QTY</th>
                                <th>Allocate QTY</th>
                                <th>Total Quantity</th>
                                <th>Balance Quantity</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

     <%-- View Process Modal --%>
    <div class="modal fade" id="ViewProcess" tabindex="-1" role="dialog" aria-labelledby="formModal" aria-hidden="true">
        <div class="modal-dialog" role="document" style="min-height:45vh;max-height:88vh;">
            <div class="modal-content" style="min-height:45vh;max-height:88vh;">
                <div class="modal-header">
                    <h5 class="modal-title">Successful Allocation</h5>
                </div>
                <div class="modal-body" style="min-height:45vh;max-height:88vh;">
                    <div class="m-4 overflow-auto p-2" style="min-height:40vh;max-height:69vh;background:#edffec;" id="viewDiv">

                    </div>
                </div>
            </div>
        </div>
    </div>


        <!-- footer -->
        <!--begin::Container-->
        <div
            class="d-flex footer mt-3 flex-column flex-md-row align-items-center justify-content-between">
            <!--begin::Copyright-->
            <div class="text-white order-2 order-md-1 mx-2">
                <span class="text-white font-weight-bold mx-2"><%= DateTime.Now.Year %>&copy;</span>
                <a
                    href="http://quickflora.com"
                    target="_blank"
                    class="text-white">QuickFlora</a>
            </div>
            <!--end::Copyright-->

            <!--begin::Nav-->
            <div class="nav nav-dark">
                <a
                    href="http://quickflora.com"
                    target="_blank"
                    class="nav-link text-white pl-0 pr-5">About</a>
                <a
                    href="http://quickflora.com"
                    target="_blank"
                    class="nav-link text-white pl-0 pr-5">Team</a>
                <a
                    href="http://quickflora.com"
                    target="_blank"
                    class="nav-link text-white pl-0 pr-0">Contact</a>
            </div>
            <!--end::Nav-->
        </div>
        <!--end::Container-->

    </form>

    <script>

        $(function () {
            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            });

        });

        var CompanyID = "<%= CompanyID %>";
        var DivisionID = "<%= DivisionID %>";
        var DepartmentID = "<%= DepartmentID %>";
        var error = 0;
      getLocations();
        //Order Location API Call
        function getLocations() {
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
        }
        getSavedData();
        //Get Saved Parameters
        function getSavedData() {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/SavedParams?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&Emp=<%= Session("EmployeeID") %>&Page=PreBookList`,
                dataType: 'json',
                success: function (response) {
                    if (response.length) {
                        response.forEach(data => {
                            $("#" + data.FieldName).val(data.FieldValue);
                        });
                        loaddata();
                    } else {
                        today();
                    }
                }
            });
        }

        //Get Saved Parameters
        function SaveParams(el) {
            var formData = {
                    "CompanyID": CompanyID,
                    "DivisionID": DivisionID,
                    "DepartmentID": DepartmentID,
                    "EmployeeID": '<%= Session("EmployeeID") %>',
                    "PageName": "PreBookList",
                    "FieldName": $(el).attr("id"),
                    "FieldValue": $(el).val()
                };
                $.ajax({
                    method: 'Post',
                    url: `https://secureapps.quickflora.com/V2/api/SavedParams`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successful") {
                            //toastr.success('Added Successfully!!');
                        } else {
                            //toastr.error('Something Went Wrong!!')
                        }
                    }

                });
        }

        function loaddata() {
            error = 0;
            loadStart();
            var SshipDate = $('#SshipDate').val();
            var EshipDate = $('#EshipDate').val();
            var itemID = $('#ItemID').val();
            var vendor = $('#vendor').val();
            var grower = $('#preGrower').val();

            SaveParams($('#SshipDate'));
            SaveParams($('#EshipDate'));
            SaveParams($('#ItemID'));
            SaveParams($('#vendor'));
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&SshipDate=${SshipDate}&EshipDate=${EshipDate}&ItemID=${itemID}&vendor=${vendor}&type=Group&grower=${grower}`,
                dataType: 'json',
                success: function (response) {
                          buildTable(response);
                }
            });

        }

        async function buildTable(records) {
            var list = [];
            var promises = [];
            records.forEach(data => {
                var SshipDate = $('#SshipDate').val();
                var EshipDate = $('#EshipDate').val();
                var vendor = $('#vendor').val();
                var grower = $('#preGrower').val();
                var location = $('#location').val();
                var request = $.ajax({
                    method: 'GET',
                    url: `https://secureapps.quickflora.com/V2/api/AllocatingProducts/New?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&ItemID=${data.ItemCode}&vendor=${vendor}&SshipDate=${SshipDate}&EshipDate=${EshipDate}&priority=&location=${location}&type1=0&emp=<%=Session("EmployeeID")%>&grower=${grower}`,
                    dataType: 'json',
                    success: function (response) {
                        if (response.length && response != "Failed") {
                            var balance = data.BalanceQTY;
                            response.forEach(PO => {
                                var assignQTY = 0;
                                if (parseInt(balance) != 0) {
                                    if (parseInt(balance) > parseInt(PO.Qty_requested)) {
                                        assignQTY = parseInt(PO.Qty_requested);
                                        balance = parseInt(balance) - parseInt(PO.Qty_requested);
                                    } else {
                                        assignQTY = parseInt(balance);
                                        balance = 0;
                                    }
                                }
                                list.push(
                                    [
                                        data.ItemCode,
                                        PO.OrderNo,
                                        PO.Location,
                                        formatDate(PO.ShipDate),
                                        PO.QOH,
                                        PO.UOM,
                                        PO.Type,
                                        PO.PACK,
                                        PO.COLOR_VARIETY,
                                        PO.PRESOLD,
                                        PO.REMARKS,
                                        PO.Qty_requested,
                                        PO.Allocated_QTY,
                                        `<input type="text" id="${PO.InLineNumber}" class="form-control d-inline w-75 qtyInput ${data.ItemCode.trim()}" data-item="${data.ItemCode.trim()}" onblur="checkQTY('${data.ItemCode}',this)" value="${(PO.Allocated_QTY ? PO.Allocated_QTY : assignQTY)}"/>
                                        <input type="hidden" id="Bal_${PO.InLineNumber}" value="${data.BalanceQTY}" />`,
                                        data.QTY,
                                        data.BalanceQTY
                                    ]
                                );
                            });
                        }
                    }
                });
                 promises.push(request);
            });
            //setTimeout(function () { applyDatatable(list); }, 5000);
            $.when.apply(null, promises).done(function () {
                applyDatatable(list);
            });
        }

        function applyDatatable(tableData) {
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
                        title: 'PreBook Products',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    }
                ],
                title: 'PreBook Products',
                data: tableData,
                order:[],
                paging:false
            });
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
            $('#ItemID').val('');
            $('#vendor').val('');
            $('#preGrower').val('');
            $('#location').val('');
            today();
        }

        function changeSize(el) {
            let size = $(el).val();
            $('body').css('font-size', size);
        }

        function formatted_date(date) {
            date = new Date(date);

            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();
            return y + '-' + m + '-' + d;
        }

        function today() {
            var date = new Date();
            var endD = new Date(date);
            var today = formatted_date(date);
            endD.setMonth(endD.getMonth() + 1);
            
            $('#SshipDate').val(today);
            $('#EshipDate').val(formatted_date(endD));
            loaddata();
        }


        function closeModal(id) {
            $('#' + id).modal('hide');
        }

        async function allocateAll() {
            loadStart();
            if (error == 0 || error < 0) {
                var rows = [];
                await $(".qtyInput").map(function () {
                    var qty = $(this).val();
                    if (qty > 0) {
                        var id = $(this).attr("id");
                        var item = $(this).attr("data-item");
                        rows.push({ 'id': id, 'item': item });
                    }
                }).get();
                if (rows.length) {
                    await openModal();
                    allocateQty(rows);
                } else {
                    alert("No data found");
                    loadStop();
                }
            } else {
                alert("Input qty cannot be greater that balance qty.");
                loadStop();

            }
        }

        function checkQTY(item, el) {
            var id = $(el).attr("id");
            var BalQty = $("#Bal_" + id).val();
            var totalSelected = 0;
            $("."+item).map(function () {
                var qty = $(this).val();
                totalSelected += parseInt(qty);
            }).get();
            if (totalSelected > parseInt(BalQty)) {
                error++;
                alert("Item: "+ item +" input qty is greater than the balance qty " + BalQty);
            } else {
                    error = 0;
            }
            console.log(error);
        }

        function allocateQty(data) {
            var SshipDate = $('#SshipDate').val();
            var EshipDate = $('#EshipDate').val();
            var grower = $('#preGrower').val();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems/GetPreBookID?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${data[0].item}&fDate=${SshipDate}&tDate=${EshipDate}&lineNo=${data[0].id}&emp=<%=Session("EmployeeID")%>&grower=${grower}`,
                  dataType: 'json',
                error: function () {
                    //alert('Something Went Wrong!!');
                    //$("#doneBtn").remove();
                    //$("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                    console.log("Error in Prebook");
                    
                    data.shift();

                    if (data.length) {
                        allocateQty(data);
                    } else {
                        $("#doneBtn").remove();
                        $("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                    }
                },
                success: function (response) {
                    if (response.length) {
                        var formdata = {
                            "CompanyID": '<%=Session("CompanyID")%>',
                            "DivisionID": '<%=Session("DivisionID")%>',
                            "DepartmentID": '<%=Session("DepartmentID")%>',
                            "EmployeeID": '<%=Session("EmployeeID")%>',
                            "PreBookID": response[0].PreBookID,
                            "InLineNumber": data[0].id,
                            "Allocated_QTY": $("#" + data[0].id).val(),
                            "Item": data[0].item
                        };

                        $.ajax({
                            method: 'Post',
                            url: `https://secureapps.quickflora.com/V2/api/AllocatingProducts`,
                            dataType: 'json',
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json'
                            },
                            error: function () {
                                //$("#doneBtn").remove();
                                //$("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                                //alert("Something Went Wrong");
                                console.log("Error in Allocation");
                                
                                data.shift();

                                if (data.length) {
                                    allocateQty(data);
                                } else {
                                    $("#doneBtn").remove();
                                    $("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                                }
                            },
                            data: JSON.stringify(formdata),
                            success: function (response) {
                                if (response == "Successfull") {
                                    $("#viewDiv").append(`<span style="color:#6C8A3C;display:block;font-weight:700;">${data[0].id}  &ensp; ${data[0].item}</span>`);

                                    data.shift();

                                    if (data.length) {
                                        allocateQty(data);
                                    } else {
                                        $("#doneBtn").remove();
                                        $("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                                    }
                                } else {
                                    //$("#doneBtn").remove();
                                    //$("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                                    //alert("Something Went Wrong");
                                    console.log("Allocation is not successful");
                                    
                                    data.shift();

                                    if (data.length) {
                                        allocateQty(data);
                                    } else {
                                        $("#doneBtn").remove();
                                        $("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                                    }
                                }
                            }
                        });
                    } else {
                        //$("#doneBtn").remove();
                        //$("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                        //alert("Something Went Wrong");
                        console.log("Prebook is empty");
                        
                        data.shift();

                        if (data.length) {
                            allocateQty(data);
                        } else {
                            $("#doneBtn").remove();
                            $("#viewDiv").parent().append(`<button type="button" class="btn btn-primary border-0" style="float:right;" id="doneBtn" onclick="done(this)">Done</button>`);
                        }
                    }
                }
            });

        }

        function done(el) {
            $("#ViewProcess").modal("hide");
            $(el).remove();
            $("#preGrower").val('');
            loadStop();
            loaddata();

        }

        async function openModal() {
            $("#ViewProcess").modal({
                backdrop: 'static',
                keyboard: false
            });
            $("#viewDiv").html('');
            $("#ViewProcess").modal("show");
        }
        getVendor();
        getItems();
        //Vender ID API Call
        function getVendor() {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=&type=1&type2=2&type3=3&type4=4`,
                dataType: 'json',
                success: function (response) {
                    var vendors = [];
                    if (response) {
                        FillPreBookGrower(response);
                        response.forEach(data => {
                            if (data.VendorID) {
                                vendors.push(data.VendorID);
                            }
                        });
                        $.typeahead({
                            order: "asc",
                            minLength: 1,
                            input: ".drpVendor",
                            order: "asc",
                            emptyTemplate: function (query) {
                                return 'No Grower found' + ((query) ? ' matching "' + query + '"' : '');
                            },
                            source: {
                                groupName: {
                                    data: vendors
                                }
                            },
                            callback: {
                                onClick: function (node, a, i, event) {

                                }
                            }
                        });
                    }
                }
            });
        }
        //Item ID API Call
        function getItems() {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=&type=1&type2=2&type3=3`,
                dataType: 'json',
                success: function (response) {
                    var items = [];
                    if (response) {
                        response.forEach(data => {
                            if (data.ItemID) {
                                items.push(data.ItemID);
                            }
                        });
                        $.typeahead({
                            order: "asc",
                            minLength: 1,
                            input: "#ItemID",
                            order: "asc",
                            emptyTemplate: function (query) {
                                return 'No Item found' + ((query) ? ' matching "' + query + '"' : '');
                            },
                            source: {
                                groupName: {
                                    data: items
                                }
                            },
                            callback: {
                                onClick: function (node, a, i, event) {

                                }
                            }
                        });
                    }
                }
            });
        }
        function FillPreBookGrower(data) {
            var options_html = '';
            data.forEach(vendor => {
                if (vendor.VendorID) {
                    options_html += `<option value="${vendor.VendorID}">${vendor.VendorName}</option>`;
                }
            });
            $('.drpGrower').append(options_html);
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
