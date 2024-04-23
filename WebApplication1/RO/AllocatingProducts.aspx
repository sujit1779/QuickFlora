<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AllocatingProducts.aspx.vb" Inherits="RO_AllocatingProducts" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Allocate Pre Book Product</title>
     <meta charset="utf-8"/>
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
        .buttons-html5{
            background: #678B38;
            color: #ffffff;
            border-radius: 5px;
            border: none;
            padding: 4px 8px;
        }
        .page-item.active .page-link {
            background-color: #678b38!important;
            color: #ffffff!important;
            border-color: #678b38!important;
        }
        .page-item .page-link {
            color:#678b38!important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
   </form> 
     <nav class="navbar navbar-expand-lg navbar-dark bg-dark" style="background-color: #678B38!important;">
        <div class="container-fluid text-white">
          <a class="navbar-brand" href="../Home.aspx">Home</a>
          <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
              <li class="nav-item">
                <a class="nav-link active" aria-current="page" href="PreBookItems.aspx">Pre Book Product</a>
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
                    <h4 class="col-10 p-0">Product Details</h4>
                </div>

                <div class="card-body overflow-auto" style="font-weight:bold;">
                    <div class="row m-0">
                        <div class="col-12 col-md-3">
                            <span>PO Number : </span>
                            <span id="PO"></span>
                        </div>
                        <div class="col-12 col-md-3">
                            <span>Item Code : </span>
                            <span id="TxtItemCode" style="background-color:yellow;" class="px-1"></span>
                            <input type="hidden" id="itemCode" />
                        </div>
                        <div class="col-12 col-md-3">
                            <span>Product : </span>
                            <span id="product"></span>
                        </div>
                        <div class="col-12 col-md-3">
                            <span>Color : </span>
                            <span id="color"></span>
                        </div>
                        
                    </div>
                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-3">
                            <span>Start Ship Date : </span>
                            <span id="txtSShipD"></span>
                            <input type="hidden" id="SshipD" />
                        </div>
                        <div class="col-12 col-md-3">
                            <span>End Ship Date : </span>
                            <span id="txtEShipD"></span>
                            <input type="hidden" id="EshipD" />
                        </div>
                        <div class="col-12 col-md-3">
                            <span>QTY : </span>
                            <span id="qty" style="background-color:orange;" class="px-1"></span>
                            <span>Balance QTY : </span>
                            <span id="BalanceQTY" style="background-color:green;color:white;" class="px-1"></span>
                            <input type="hidden" id="BalQty" />
                        </div>
                        <div class="col-12 col-md-3">
                            <span>Pack : </span>
                            <span id="pack"></span>
                        </div>
                        
                    </div>
                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-3">
                            <span>Cost : </span>
                            <span id="cost"></span>
                        </div>
                        <div class="col-12 col-md-3">
                            <span>Grower : </span>
                            <span id="grower"></span>
                        </div>
                        <div class="col-12 col-md-6">
                            <span>Notes : </span>
                            <span id="notes"></span>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-3 text-wrap" id="loader">
                <div class="card-header row m-0">
                    <h4 class="col-12 col-md-8">Allocate Pre Book Product</h4>
                    <div class="col-12 col-md-4 text-end pt-1">
                        <input type="checkbox" style="vertical-align:middle;" onclick="displayItemData('<%= Session("PreBookID").ToString() %>')" id="GetAll"/> <label for="GetAll"> Get All</label>
                        &emsp;<button type="button" class="btn btn-primary border-0" onclick="allocateAll('')">Submit Selected</button>
                    
                    </div>
                </div>

                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr>
                                <th><input type="checkbox" id="SelectAll"/> Select All</th>
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
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <div class="text-end">
                        <button type="button" class="btn btn-primary border-0" onclick="allocateAll('')">Submit Selected</button>
                    </div>
                </div>
            </div>
        </div>
    <!--begin::Container-->
    <div
    class="d-flex footer mt-3 flex-column flex-md-row align-items-center justify-content-between"
    >
    <!--begin::Copyright-->
    <div class="text-white order-2 order-md-1 mx-2">
      <span class="text-white font-weight-bold mx-2"
        >2022&copy;</span
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
   
    <%--<script>

        $(function () {
            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            });

        });

        var CompanyID = "<%= CompanyID %>";
        var DivisionID = "<%= DivisionID %>";
        var DepartmentID = "<%= DepartmentID %>";
        var PreBookID = "<%= Session("PreBookID").ToString() %>"

        displayItemData(PreBookID);

        function loaddata(itemID, SshipDate, EshipDate) {

            loadStart();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/AllocatingProducts?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&ItemID=${itemID}&SshipDate=${SshipDate}&EshipDate=${EshipDate}`,
                dataType: 'json',
                success: function (response) {
                if (response.length && response != "Failed") {
                          buildTable(response);
                      } else {
                          applyDatatable('');
                      }
                }
            });

        }

        function buildTable(records) {
            var list = [];
            records.forEach(data => {
                list.push(
                    [
                        data.OrderNo,
                        data.Location,
                        formatDate(data.ShipDate),
                        data.QOH,
                        data.UOM,
                        data.Type,
                        data.PRESOLD,
                        data.REMARKS,
                        data.Qty_requested,
                        data.Allocated_QTY,
                        `<input type="text" id="allocatedQty_${data.InLineNumber}" class="form-control d-inline w-75" value="${data.Allocated_QTY}"/>
                        <button type="button" class="btn btn-primary border-0 btn-sm py-1 px-2" style="vertical-align: baseline;" onclick="allocateQty('${data.InLineNumber}')"><i class="fas fa-paper-plane"></i></button>`
                    ]
                );
            });
            applyDatatable(list);

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
                        title: 'Allocate PreBook Product',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    }
                ],
                title: 'Allocate PreBook Product',
                data: tableData
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

        function changeSize(el) {
            let size = $(el).val();
            $('body').css('font-size', size);
        }

        function displayItemData(ID) {
            if (ID) {
                $.ajax({
                    method: 'GET',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems/Edit?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&preBookID=${ID}`,
                    dataType: 'json',
                    success: function (response) {
                        if (response == "Failed") {
                            alert('Something Went Wrong!!');
                        } else {
                            if (response.length) {
                                $('#PO').text(response[0].PONumber);
                                $('#itemCode').val(response[0].ItemCode);
                                $('#TxtItemCode').text(response[0].ItemCode);
                                $('#product').text(response[0].Product);
                                $('#SshipD').val(formatDate(response[0].StartShipDate));
                                $('#txtSShipD').text(formatDate(response[0].StartShipDate));
                                $('#EshipD').val(formatDate(response[0].EndShipDate));
                                $('#txtEShipD').text(formatDate(response[0].EndShipDate));
                                $('#color').text(response[0].Color);
                                $('#qty').text(response[0].QTY);
                                $('#pack').text(response[0].Pack);
                                $('#cost').text(response[0].Cost);
                                $('#grower').text(response[0].Grower);
                                $('#notes').text(response[0].Notes);
                                loaddata(response[0].ItemCode, response[0].StartShipDate, response[0].EndShipDate);
                            }
                        }
                    }
                });
            }
        }

        function allocateQty(inLineNo) {
            var formdata = {
                "PreBookID": PreBookID,
                "InLineNumber": inLineNo,
                "Allocated_QTY": $("#allocatedQty_" + inLineNo).val()
            };

            $.ajax({
                method: 'Post',
                url: `https://secureapps.quickflora.com/V2/api/AllocatingProducts`,
                dataType: 'json',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(formdata),
                success: function (response) {
                    if (response != "Unsuccessfull") {
                        var item = $("#itemCode").val();
                        var SshipD = $("#SshipD").val();
                        var EshipD = $("#EshipD").val();
                        loaddata(item, SshipD, EshipD);
                        //toastr.success('Added Successfully!!');
                    } else {
                        //toastr.error('Something Went Wrong!!')
                    }
                }
            });
        }

    </script>--%>
    
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
        var PreBookID = "<%= Session("PreBookID").ToString() %>";
        var priority = "<%= Request.Params("priority").ToString() %>";

        displayItemData(PreBookID);

        function loaddata(itemID, SshipDate, EshipDate) {

            loadStart();
            var type = ($("#GetAll").prop("checked") ? 1 : 0);
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/AllocatingProducts?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&ItemID=${itemID}&SshipDate=${SshipDate}&EshipDate=${EshipDate}&type=${type}&priority=${priority}`,
                dataType: 'json',
                success: function (response) {
                if (response.length && response != "Failed") {
                          buildTable(response);
                      } else {
                          applyDatatable('');
                      }
                }
            });

        }

        function buildTable(records) {
            var list = [];
            records.forEach(data => {
                list.push(
                    [
                        `<input type="checkbox" class="selectAllocate" id="${data.InLineNumber}" onclick="getValue(this)" value="${data.Qty_requested}" />`,
                        data.OrderNo,
                        data.Location,
                        formatDate(data.ShipDate),
                        data.QOH,
                        data.UOM,
                        data.Type,
                        data.PRESOLD,
                        data.REMARKS,
                        data.Qty_requested,
                        data.Allocated_QTY,
                        `<input type="text" id="allocatedQty_${data.InLineNumber}" class="form-control d-inline w-75 qtyInput" value="${data.Allocated_QTY}"/>`
                        //<button type="button" class="btn btn-primary border-0 btn-sm py-1 px-2" style="vertical-align: baseline;" onclick="allocateQty('${data.InLineNumber}')"><i class="fas fa-paper-plane"></i></button>`
                    ]
                );
            });
            applyDatatable(list);

        }

        function applyDatatable(tableData) {
            loadStop();
            if ($.fn.DataTable.isDataTable('#details')) {
                $('#details').DataTable().destroy();
                $('tbody').html('');
                $("#SelectAll").prop("checked", false);
            }
            var table = $('#details').DataTable({
                dom: 'frBtlip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'pdfHtml5',
                        title: 'Allocate PreBook Product',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    }
                ],
                title: 'Allocate PreBook Product',
                data: tableData,
                paging: false,
                order:[]
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

        function changeSize(el) {
            let size = $(el).val();
            $('body').css('font-size', size);
        }

        function displayItemData(ID) {
            if (ID) {
                $.ajax({
                    method: 'GET',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems/Edit?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&preBookID=${ID}`,
                    dataType: 'json',
                    success: function (response) {
                        if (response == "Failed") {
                            alert('Something Went Wrong!!');
                        } else {
                            if (response.length) {
                                $('#PO').text(response[0].PONumber);
                                $('#itemCode').val(response[0].ItemCode);
                                $('#TxtItemCode').text(response[0].ItemCode);
                                $('#BalQty').val(response[0].BalanceQTY);
                                $('#BalanceQTY').text((response[0].BalanceQTY ? response[0].BalanceQTY : '0'));
                                $('#product').text(response[0].Product);
                                $('#SshipD').val(formatDate(response[0].StartShipDate));
                                $('#txtSShipD').text(formatDate(response[0].StartShipDate));
                                $('#EshipD').val(formatDate(response[0].EndShipDate));
                                $('#txtEShipD').text(formatDate(response[0].EndShipDate));
                                $('#color').text(response[0].Color);
                                $('#qty').text(response[0].QTY);
                                $('#pack').text(response[0].Pack);
                                $('#cost').text(response[0].Cost);
                                $('#grower').text(response[0].Grower);
                                $('#notes').text(response[0].Notes);
                                loaddata(response[0].ItemCode, response[0].StartShipDate, response[0].EndShipDate);
                            }
                        }
                    }
                });
            }
        }

        $("#SelectAll").on("click change", function () {
            if ($(this).prop("checked")) {
                $(".selectAllocate").prop("checked", true).map(function () { getValue($(this)) }).get();
            } else {
                $(".selectAllocate").prop("checked", false);
            }
        });

        function getValue(el) {
            var id = $(el).attr("id");
            $("#allocatedQty_" + id).val($(el).val());
        }

        async function allocateAll() {
            loadStart();
            let ret = await VerifyQTY();
            if (ret == 1) {
                await $("input[class='selectAllocate']:checked").map(function () {
                    var id = $(this).attr("id");
                    allocateQty(id);
                }).get();
            } else {
                var bal = $("#BalQty").val();
                alert("Selected QTY cannot be greater that balane QTY "+bal);
            }
            loadStop();
        }

        async function VerifyQTY() {
            var bal = $("#BalQty").val();
            var totalSelected = 0;
            await $("input[class='selectAllocate']:checked").map(function () {
                var id = $(this).attr("id");
                totalSelected += parseInt($("#allocatedQty_" + id).val());
            }).get();
            console.log(totalSelected);
            if (totalSelected > parseInt(bal)) {
                return 2;
            } else {
                return 1;
            }
        }

        async function allocateQty(inLineNo) {
            var formdata = {
                "PreBookID": PreBookID,
                "InLineNumber": inLineNo,
                "Allocated_QTY": $("#allocatedQty_" + inLineNo).val(),
                "Item": $('#itemCode').val()
            };

            $.ajax({
                method: 'Post',
                url: `https://secureapps.quickflora.com/V2/api/AllocatingProducts`,
                dataType: 'json',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(formdata),
                success: function (response) {
                    if (response == "Successfull") {
                        var item = $("#itemCode").val();
                        var SshipD = $("#SshipD").val();
                        var EshipD = $("#EshipD").val();
                        loaddata(item, SshipD, EshipD);
                        displayItemData(PreBookID);
                        //toastr.success('Added Successfully!!');
                    } else {
                        //toastr.error('Something Went Wrong!!')
                    }
                }
            });
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