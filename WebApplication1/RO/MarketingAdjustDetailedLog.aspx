<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MarketingAdjustDetailedLog.aspx.vb" Inherits="Report_MarketingAdjustLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quickflora | Adjustment Detailed Logs</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.2/css/dataTables.bootstrap5.min.css" />
    <link rel="stylesheet" href="assets/css/fontawesome.min.css" />
    <link rel="stylesheet" href="assets/css/customerList.css" />
    <link rel="stylesheet" href="assets/plugin/toast/build/toastr.min.css">
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
    <script type="text/javascript" src="assets/plugin/toast/build/toastr.min.js"></script>

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
                <a class="navbar-brand" href="../Home.aspx"><i class="fas fa-arrow-left hover-visible"></i>&nbsp;Home</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                            <%--<li><a class="nav-link" aria-current="page" href="Adjustment.aspx">Marketing Adjustment</a></li>--%>
                        
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
                        <div class="col-12 col-md-3">

                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <label for="fdate">From Date :</label>
                                    <input type="date" class="form-control" id="fromdate" required />
                                </div>

                                <div class="col-12 col-md-6">
                                    <label for="todate">To Date :</label>
                                    <input type="date" class="form-control" id="todate" required />
                                </div>
                            </div>
                        </div>

                        <div class="col-12 col-md-3 mt-4">
                            <div class="row g-0 ">
                                <div class="form-group col-4 ">
                                    <div class="form-check form-check-inline">
                                        <input class="form-check-input" type="radio" id="allDate" name="dateType" value="0">
                                        <label class="form-check-label" for="allDate" style="vertical-align: sub; margin-left: 5px;">All Dates</label>

                                    </div>

                                </div>
                                <div class="form-group col-5">
                                    <div class="form-check form-check-inline ">
                                        <input class="form-check-input float-start" type="radio" id="processDate" name="dateType" value="1">
                                        <label class="form-check-label" for="processDate" style="vertical-align: sub; margin-left: 5px;">Process Date</label>
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="col-12 col-md-2">
                            <label for="LocationID">Location ID:</label>

                            <select class="form-control" id="LocationID">
                            </select>
                        </div>
                        <%--<div class="col-12 col-md-2">
                            <label for="TransactionType">Transaction Type:</label>
                            <input type="text" class="form-control" id="TransactionType" />
                        </div>--%>
                        <div class="col-12 col-md-2">
                            <label for="TransactionNumber">Transaction No:</label>
                            <input type="text" class="form-control" id="TransactionNumber" />
                        </div>
                        <div class="col-12 col-md-2">
                            <label for="itemID">ItemID:</label>
                            <input type="text" class="form-control" id="itemID" />
                        </div>

                    </div>
        <div class="mt-3 text-end">
                        <button type="button" class="btn btn-primary border-0" onclick="SearchData()">Search</button>
                        <button type="button" class="btn btn-light" onclick="clearData()">Clear</button>
                    </div>
                </div>
            </div>

            <div class="card mt-3 text-wrap" id="loader">
                <div class="card-header row m-0">
                    <h4 class="col-10 p-0" id="headMessage">Adjustment Detailed Logs</h4>
                </div>

                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>View</th>
                                <th>Transaction Number</th>
                                <th>Transaction Date</th>
                                <th>Item ID</th>
                                <th>Item Name</th>
                                <th>Qty</th>
                                <th>Cost</th>
                                <th>Item Total</th>
                                <th>Transaction Type</th>
                                <th>LocationID</th>
                                <th>Employee ID</th>
                                <th>Internal Notes</th>
                                <%-- <th>Status</th>--%>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

   


    <!-- footer -->
    <!--begin::Container-->
    <div
    class="d-flex footer mt-3 flex-column flex-md-row align-items-center justify-content-between"
    >
    <!--begin::Copyright-->
    <div class="text-white order-2 order-md-1 mx-2">
      <span class="text-white font-weight-bold mx-2"
        ><%=DateTime.Now.Year %></span
      >
      <a
        href="http://quickflora.com"
        target="_blank"
        class="text-white"
        >QuickFlora</a
      >
    </div>
    <!--end::Copyright-->

    <!--begin::Nav-->
    <div class="nav nav-dark">
      <a
        href="http://quickflora.com"
        target="_blank"
        class="nav-link text-white pl-0 pr-5"
        >About</a
      >
      <a
        href="http://quickflora.com"
        target="_blank"
        class="nav-link text-white pl-0 pr-5"
        >Team</a
      >
      <a
        href="http://quickflora.com"
        target="_blank"
        class="nav-link text-white pl-0 pr-0"
        >Contact</a
      >
    </div>
    <!--end::Nav-->
    </div>
    <!--end::Container-->
    </form>

    <script>

        var CompanyID = '<%= Session("CompanyID") %>';
        var DivisionID = '<%= Session("DivisionID") %>';
        var DepartmentID = '<%= Session("DepartmentID") %>';

        $("#allDate").prop("checked", true);
        today();
        function today() {
            // $('input[type="date"]').attr('disabled',true);
            var date = new Date();
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();
            var today = y + '-' + m + '-' + d;

            $('#fromdate').val(today);
            $('#todate').val(today);
            $("#copyYear").html(y);

            SearchData();
        }
        function formatDate(date) {
            date = new Date(date);
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();

            return m + '/' + d + '/' + y;
        }
        /* $(document).ready(function () { SearchData(); });*/

        getLocation();
        function getLocation() {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/OrderLocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}`,
                data: "{}",
                dataType: "json",
                encode: true,
                success: function (data) {

                    var LocationID = $("#LocationID");
                    LocationID.empty().append('<option selected="selected" value="">Select Location</option>');
                    $.each(data, function (key, value) {
                        $("#LocationID").append($("<option></option>").val(value.LocationID).html(value.LocationName));

                    });

                },
                error: function (data) {
                    alert("Error");
                }


            });
        }



        function SearchData() {

            var fromdate = $('#fromdate').val();
            var todate = $('#todate').val();
            var dateType = parseInt($("input[name='dateType']:checked").val());
            var LocationID = ($('#LocationID').val() ? $('#LocationID').val() : '');
            var ItemID = ($('#itemID').val() ? $('#itemID').val() : '');
            var TransactionType = '';//'Marketing';//($('#TransactionType').val() ? $('#TransactionType').val() : '');
            var TransactionNumber = ($('#TransactionNumber').val() ? $('#TransactionNumber').val() : 0);

            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/InventoryAdjustmentDetailedLog?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&fromdate=${fromdate}&todate=${todate}&dateType=${dateType}&LocationID=${LocationID}&TransactionType=${TransactionType}&TransactionNumber=${TransactionNumber}&itemID=${ItemID}`,
                dataType: 'json',
                error: function () {

                    applyDatatable('');
                    toastr.error('Server Not Found!');
                },
                success: function (response) {
                    if (response.length) {

                        buildTable(response);
                        loadStop();
                    }
                    else {
                        //toastr.error('No Result Found!');
                        applyDatatable(response);

                    }

                }

            });
        }

        function buildTable(records) {
            var list = [];
            records.forEach(data => {
                list.push(
                    [
                        `<a target="_blank" href="https://reports.quickflora.com/reports/scripts/AdjustInventoryReport.aspx?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&InventoryAdjustmentsNumber=${data.TransactionNumber}" class="btn btn-warning btn-sm"><i class="fa fa-search" aria-hidden="true"></i></a>`
                        ,data.TransactionNumber,
                        formatDate(data.TransactionDate),
                        data.ItemID,
                        data.ItemName,
                        data.Qty,
                        parseFloat(data.AverageCost).toFixed(2),
                        parseFloat(data.ItemTotal).toFixed(2),
                        data.TransactionType,
                        data.LocationID,
                        data.EmployeeID,
                        data.InternalNotes
                        //data.Status,


                    ]
                );
            });
            applyDatatable(list);

        }
        function applyDatatable(tableData) {
            loadStop();
            if ($.fn.DataTable.isDataTable('#details')) {
                $('#details').DataTable().destroy();
            }
            $('#details').DataTable({
                dom: 'frBtlip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'pdfHtml5',
                        title: 'Inventory Day Wise Log',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    }
                ],
                title: 'Inventory Day Wise Log',
                data: tableData
            });
        }

        function clearData() {
            $("#allDate").prop("checked", true);
            $("#processDate").prop("checked", false);
            $('#TransactionNumber').val('');
            //$('#TransactionType').val('');
            $('#LocationID').val('');
            today();
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


        function changeSize(el) {
            let size = $(el).val();
            $('body').css('font-size', size);
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