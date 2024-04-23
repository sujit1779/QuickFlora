<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" CodeFile="PreBookItems2.aspx.vb" Inherits="RO_PreBookItems2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PreBooks Items</title>
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
        table > tbody > tr:first-child{
            background-color:#BCD1A3;
        }

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
        th{
            overflow:hidden;
            white-space:nowrap;
            text-overflow:ellipsis;
        }
        table{
            table-layout:fixed;
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
                <a class="nav-link active" aria-current="page" href="#">PreBooks Item</a>
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

                <div class="card-body">
                    
                    <div class="row m-0">
                        <div class="col-12 col-md-2">
                            <label for="SshipDate">Start Shipping Date :</label>
                            <input type="date" class="form-control" id="SshipDate"/>
                        </div>
                        <div class="col-12 col-md-2">
                            <label for="EshipDate">End Shipping Date :</label>
                            <input type="date" class="form-control" id="EshipDate"/>
                        </div>
                        <div class="col-12 col-md-2">
                            <label for="ItemID">Item Code :</label>
                            <div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpItems" placeholder="Search Item Code" autocomplete="off" id="ItemID" />
                                     </div>
                                 </div>
                             </div>
                        </div>
                        <div class="col-12 col-md-2">
                            <label for="vendor">Grower :</label>
                            <div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="vendor" />
                                     </div>
                                 </div>
                             </div>
                        </div>
                        <div class="col-12 col-md-2">
                            <label for="allocateType">Type :</label>
                            <select id="allocateType" class="form-control">
                                <option value="">--All--</option>
                                <option value="Not_Allocated">Not Allocated</option>
                                <option value="Partial">Partially Allocated</option>
                                <option value="Allocated">Allocated</option>
                            </select>
                        </div>
                        <div class="col-12 col-md-2 pt-4">
                            <input type="checkbox" id="type" />
                            <label for="type">Show All</label>
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
                    <h4 class="col-12 col-md-7">PreBooks Item</h4>
                    <div class="col-12 col-md-5 text-end">
                        <button type="button" class="btn btn-primary border-0 me-3" onclick="openModal('PreBookModal')">Add New</button>
                        <input type="checkbox" id="exportable" onclick="loaddata()" />
                        <label for="exportable">Exportable</label>
                    </div>
                </div>

                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                             <tr>
                                <th style="width:6%;" data-bs-toggle="tooltip" data-bs-placement="top" title="PO Number">PO Number</th>
                                <th style="width:6%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Item Code">Item Code</th>
                                <th style="width:9%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Product">Product</th>
                                <th style="width:10%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Shipping Start Date">Start Date</th>
                                <th style="width:10%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Shipping End Date">End Date</th>
                                <th style="width:6%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Color / Variety">Color / Variety</th>
                                <th style="width:5%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Quantity">QTY</th>
                                <th style="width:3%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Balance Quantity">Bal. QTY</th>
                                <th style="width:5%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Pack">Pack</th>
                                <th style="width:5%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Cost">Cost</th>
                                <th style="width:8%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Grower">Grower</th>
                                <th style="width:8%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Priority">Priority</th>
                                <th style="width:9%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Vendor Remarks">Remarks</th>
                                <th style="width:10%;" data-bs-toggle="tooltip" data-bs-placement="top" title="Dedicate Location">Location</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

    <%-- Edit Inventoy Modal --%>
    <div class="modal fade" id="PreBookModal" tabindex="-1" role="dialog" aria-labelledby="formModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Pre Book Product</h5>
                    <button type="button" class="btn-close" onclick="closeModal('PreBookModal')"></button>
                    <input type="hidden" id="prebookID" />
                </div>
                <div class="modal-body">
                    <div class="row m-0">
                        <div class="col-12 col-md-6">
                            <label for="PO">PO Number : </label>
                            <input type="text" id="PO" class="form-control" />
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="itemCode">Item Code : </label>
                            <div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpItems" placeholder="Search Item Code" autocomplete="off" id="itemCode" />
                                     </div>
                                 </div>
                             </div>
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-6">
                            <label for="product">Product : </label>
                            <input type="text" id="product" class="form-control" />
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="color">Color / Variety : </label>
                            <input type="text" id="color" class="form-control" />
                        </div>
                        
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-6">
                            <label for="shipD">Start Shipping Date : </label>
                            <input type="date" id="SshipD" class="form-control" />
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="shipD"> End Shipping Date : </label>
                            <input type="date" id="EshipD" class="form-control" />
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-6">
                            <label for="qty">Quantity : </label>
                            <input type="text" id="qty" class="form-control" />
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="pack">Pack : </label>
                            <input type="text" id="pack" class="form-control" />
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-6">
                            <label for="cost">Cost : </label>
                            <input type="text" id="cost" class="form-control" />
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="priority">Priority : </label>
                            <select id="priority" class="form-control drpLocation">
                            </select>
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-6">
                            <label for="vendor">Grower :</label>
                            <div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="grower" />
                                     </div>
                                 </div>
                             </div>
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="location">Location : </label>
                            <select id="location" class="form-control drpLocation">
                            </select>
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12">
                            <label for="notes">Notes :</label>
                            <textarea class="form-control" rows="5" id="notes"></textarea>
                        </div>
                    </div>
                    <div class="text-end mt-4">
                        <button type="button" class="btn btn-primary btn-sm border-0" onclick="SubmitData()">Submit</button>
                    </div>
                </div>
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
        ><%= DateTime.Now.Year %>&copy;</span
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


        today();

        function loaddata() {

            loadStart();
            var SshipDate = $('#SshipDate').val();
            var EshipDate = $('#EshipDate').val();
            var itemID = $('#ItemID').val();
            var vendor = $('#vendor').val();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&SshipDate=${SshipDate}&EshipDate=${EshipDate}&ItemID=${itemID}&vendor=${vendor}`,
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
            var date = new Date();
            var today = formatted_date(date);
            list.push([
                `<input type="text" class="form-control" id="InLinePONumber" />`,
                `<div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpItems" placeholder="Search Item Code" autocomplete="off" id="InLineItemCode" />
                                     </div>
                                 </div>
                             </div>`,
                `<input type="text" class="form-control" id="InLineProduct" />`,
                `<input type="date" class="form-control" id="InLineSShipDate" value="${today}"/>`,
                `<input type="date" class="form-control" id="InLineEShipDate" value="${today}"/>`,
                `<input type="text" class="form-control" id="InLineColor" />`,
                `<input type="text" class="form-control" id="InLineQTY" />`,
                `<input type="text" class="form-control" id="InLinePack" />`,
                `<input type="text" class="form-control" id="InLineCost" />`,
                `<div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="InLineGrower" />
                                     </div>
                                 </div>
                             </div>`,
                `<input type="text" class="form-control" id="InLineNotes" />`,
                `<button type="button" class="btn btn-primary btn-sm border-0" onclick="SubmitInLineData()"><i class="fas fa-plus"></i> Add</button>`
            ]);
            records.forEach(data => {
                list.push(
                    [
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="PONumber" value="${data.PONumber}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="ItemCode" value="${data.ItemCode}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Product" value="${data.Product}" />`,
                        `<input type="date" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="StartShipDate" value="${formatted_date(data.StartShipDate)}" />`,
                        `<input type="date" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="EndShipDate" value="${formatted_date(data.EndShipDate)}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Color" value="${data.Color}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="QTY" value="${data.QTY}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Pack" value="${data.Pack}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Cost" value="${data.Cost}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Grower" value="${data.Grower}" />`,
                        `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Notes" value="${data.Notes}" />`,
                        //`<button type="button" class="btn btn-primary btn-sm border-0" onclick="editModal('${data.PreBookID}','PreBookModal')" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit Tax Details"><i class="fas fa-pen"></i></button>
                        `<a target="_blank" href="AllocatingProducts.aspx?PreBookID=${data.PreBookID}" class="btn btn-primary btn-sm border-0">Allocate</a>`
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
                        title: 'PreBook Products',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    }
                ],
                title: 'PreBook Products',
                data: tableData
           });
            getVendor();
            getItems();
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
            var today = formatted_date(date);

            $('#SshipDate').val(today);
            $('#EshipDate').val(today);
            $('#SshipD').val(today);
            $('#EshipD').val(today);
            loaddata();
        }


        function closeModal(id) {
            $('#' + id).modal('hide');
        }

          function allocate(ID, modaID) {

              $(location).attr('href', `https://secure.quickflora.com/POM2021/RO/AllocatingProducts.aspx?PreBookID=${ID}`);
          }

        function editModal(ID,modaID) {
            if (ID) {
                $.ajax({
                    method: 'GET',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&preBookID=${ID}`,
                    dataType: 'json',
                    success: function (response) {
                        if (response == "Failed") {
                            toastr.error('Something Went Wrong!!');
                        } else {
                            if (response.length) {
                                $('#prebookID').val(response[0].PreBookID);
                                $('#PO').val(response[0].PONumber);
                                $('#itemCode').val(response[0].ItemCode);
                                $('#product').val(response[0].Product);
                                $('#SshipD').val(response[0].StartShipDate.replace("T00:00:00", ""));
                                $('#EshipD').val(response[0].EndShipDate.replace("T00:00:00", ""));
                                $('#color').val(response[0].Color);
                                $('#qty').val(response[0].QTY);
                                $('#pack').val(response[0].Pack);
                                $('#cost').val(response[0].Cost);
                                $('#grower').val(response[0].Grower);
                                $('#notes').val(response[0].Notes);
                                $('#' + modaID).modal('show');
                            }
                        }
                    }
                });
            }
        }

          function openModal(id) {
            var date = new Date();
            var today = formatted_date(date);
            $('#prebookID').val('');
            $('#PO').val('');
            $('#itemCode').val('');
            $('#product').val('');
            $('#SshipD').val(today);
            $('#EshipD').val(today);
            $('#color').val('');
            $('#qty').val('');
            $('#pack').val('');
            $('#cost').val('');
            $('#grower').val('');
            $('#notes').val('');
            $('#' + id).modal('show');
        }

        function SubmitData() {
            var id = $('#prebookID').val();
            if (id) {
                var formData = {
                    "CompanyID": CompanyID,
                    "DivisionID": DivisionID,
                    "DepartmentID": DepartmentID,
                    "PreBookID" : $('#prebookID').val(),
                    "PONumber" : $('#PO').val(),
                    "ItemCode" : $('#itemCode').val(),
                    "Product" : $('#product').val(),
                    "StartShipDate": $('#SshipD').val(),
                    "EndShipDate" : $('#EshipD').val(),
                    "Color" : $('#color').val(),
                    "QTY" : $('#qty').val(),
                    "Pack" : $('#pack').val(),
                    "Cost" : $('#cost').val(),
                    "Grower" : $('#grower').val(),
                    "Notes" : $('#notes').val()
                }
                $.ajax({

                    method: 'Put',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response != "Unsuccessfull") {
                            closeModal("PreBookModal");
                            loaddata();
                           //toastr.success('Updated Successfully!!');
                        } else {
                           // toastr.error('Something Went Wrong!!')
                        }
                    }

                });
            } else {
                var formData = {
                    "CompanyID": CompanyID,
                    "DivisionID": DivisionID,
                    "DepartmentID": DepartmentID,
                    "PONumber": $('#PO').val(),
                    "ItemCode": $('#itemCode').val(),
                    "Product": $('#product').val(),
                    "StartShipDate": $('#SshipD').val(),
                    "EndShipDate": $('#EshipD').val(),
                    "Color": $('#color').val(),
                    "QTY": $('#qty').val(),
                    "Pack": $('#pack').val(),
                    "Cost": $('#cost').val(),
                    "Grower": $('#grower').val(),
                    "Notes": $('#notes').val()
                };
                $.ajax({
                    method: 'Post',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response != "Unsuccessfull") {
                            closeModal("PreBookModal");
                            loaddata();
                            //toastr.success('Added Successfully!!');
                        } else {
                           // toastr.error('Something Went Wrong!!')
                        }
                    }

                });
            }
        }


          function SubmitInLineData() {
                var formData = {
                    "CompanyID": CompanyID,
                    "DivisionID": DivisionID,
                    "DepartmentID": DepartmentID,
                    "PONumber": $('#InLinePONumber').val(),
                    "ItemCode": $('#InLineItemCode').val(),
                    "Product": $('#InLineProduct').val(),
                    "StartShipDate": $('#InLineSShipDate').val(),
                    "EndShipDate": $('#InLineEShipDate').val(),
                    "Color": $('#InLineColor').val(),
                    "QTY": $('#InLineQTY').val(),
                    "Pack": $('#InLinePack').val(),
                    "Cost": $('#InLineCost').val(),
                    "Grower": $('#InLineGrower').val(),
                    "Notes": $('#InLineNotes').val()
                };
                $.ajax({
                    method: 'Post',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response != "Unsuccessfull") {
                            closeModal("PreBookModal");
                            loaddata();
                            //toastr.success('Added Successfully!!');
                        } else {
                            //toastr.error('Something Went Wrong!!')
                        }
                    }

                });
        }

        function saveOnBlur(id,el) {
            var key = $(el).attr("id");
            var valueData = $(el).val();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&preBookID=${id}&key=${key}&value=${valueData}&type=0&type1=1&type2=2&type3=3&type4=4`,
                dataType: 'json',
                success: function (response) {
                    if (response.length && response != "Failed") {
                        loaddata();
                    } else {
                        alert("Something Went Wrong!")
                    }
                }
            });
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
                          $.typeahead({
                              order: "asc",
                              minLength: 1,
                              input: "#InLineGrower",
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
                          $.typeahead({
                              order: "asc",
                              minLength: 1,
                              input: "#InLineItemCode",
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
                                      $.ajax({
                                          method: 'GET',
                                          url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${i.display}&type=1`,
                                          dataType: 'json',
                                          success: function (response) {
                                              if (response.length && response != "Failed") {
                                                  $('#InLineProduct').val(response[0].ItemName);
                                                $('#InLineCost').val(response[0].Price);
                                              } else {
                                                  alert("No Item found on " + i.display + " item code");
                                              }
                                          }
                                      });
                                  }
                              }
                          });
                          $.typeahead({
                              order: "asc",
                              minLength: 1,
                              input: "#itemCode",
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

                                      $.ajax({
                                          method: 'GET',
                                          url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${i.display}&type=1`,
                                          dataType: 'json',
                                          success: function (response) {
                                              if (response.length && response != "Failed") {
                                                  $('#product').val(response[0].ItemName);
                                                  $('#cost').val(response[0].Price);
                                              } else {
                                                  alert("No Item found on " + i.display + " item code");
                                              }
                                          }
                                      });
                                  }
                              }
                          });
                      }
                  }
              });
          }


          </script>
--%>

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
        var locations = [];
        var locationsHTML = '';
        //Order Location API Call
        function getLocations() {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/OrderLocation?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}`,
                dataType: 'json',
                success: function (response) {
                    var options_html = '<option value="">Please Select</option>';
                    if (response) {
                        locations = response;
                        response.forEach(location => {
                            if (location.LocationName) {
                                options_html += `<option value="${location.LocationID}">${location.LocationName}</option>`;
                            }
                        });
                        $('.drpLocation').append(options_html);
                        locationsHTML = options_html;
                    }
                }
            });
        }

        getSavedData();
        //Get Saved Parameters
        function getSavedData() {
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/SavedParams?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&Emp=<%= Session("EmployeeID") %>&Page=PreBookItems2`,
                dataType: 'json',
                success: function (response) {
                    if (response.length) {
                        response.forEach(data => {
                            if ($("#" + data.FieldName).attr("type") == "checkbox") {
                                if (data.FieldValue == "true") {
                                    $("#" + data.FieldName).prop("checked",true);
                                } else
                                    $("#" + data.FieldName).prop("checked",false);
                            }
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
                    "PageName": "PreBookItems2",
                    "FieldName": $(el).attr("id"),
                    "FieldValue": $(el).val()
            };

            if ($(el).attr("type") == "checkbox") {
                var value = ($(el).prop("checked") ? "true" : "false");
                formData["FieldValue"] = value;
            }
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

        getLocations();

        function loaddata() {

            loadStart();
            var SshipDate = $('#SshipDate').val();
            var EshipDate = $('#EshipDate').val();
            var itemID = $('#ItemID').val();
            var vendor = $('#vendor').val();
            var AType = $('#allocateType').val();
            var type = ($('#type').prop("checked") ? 1 : 0);
            
            SaveParams($('#SshipDate'));
            SaveParams($('#EshipDate'));
            SaveParams($('#ItemID'));
            SaveParams($('#vendor'));
            SaveParams($('#allocateType'));
            SaveParams($('#type'));

            $.ajax({
                method: 'GET',
                //url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&SshipDate=${SshipDate}&EshipDate=${EshipDate}&ItemID=${itemID}&vendor=${vendor}&type=`,
                url: `https://secureapps.quickflora.com/V2/api/PreBooktemsList?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&SshipDate=${SshipDate}&EshipDate=${EshipDate}&ItemID=${itemID}&vendor=${vendor}&type=${type}&allocateType=${AType}`,
                dataType: 'json',
                error: function () {
                    applyDatatable('');
                },
                success: function (response) {
                //if (response.length && response != "Failed") {
                          buildTable(response);
                      //} else {
                      //    applyDatatable('');
                      //}
                }
            });

        }

        function buildTable(records) {
            var list = [];
            var listnew = [];
            var exp = ($("#exportable").prop("checked") ? 1 : 0);
            console.log(exp);
            if (exp == 0) {
                var date = new Date();
                var today = formatted_date(date);
                list.push([
                    `<input type="text" class="form-control" id="InLinePONumber" />`,
                    `<div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpItems" placeholder="Search Item Code" autocomplete="off" id="InLineItemCode" />
                                     </div>
                                 </div>
                             </div>`,
                    `<input type="text" class="form-control" id="InLineProduct" />`,
                    `<input type="date" class="form-control" id="InLineSShipDate" value="${today}"/>`,
                    `<input type="date" class="form-control" id="InLineEShipDate" value="${today}"/>`,
                    `<input type="text" class="form-control" id="InLineColor" />`,
                    `<input type="text" class="form-control" id="InLineQTY" />`, '',
                    `<input type="text" class="form-control" id="InLinePack" />`,
                    `<input type="text" class="form-control" id="InLineCost" />`,
                    `<div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="InLineGrower" />
                                     </div>
                                 </div>
                             </div>`,
                    `<select id="InLineP" class="form-control">
                    ${locationsHTML}
                 </select>`,
                    `<input type="text" class="form-control" id="InLineNotes" />`,
                    `<select id="InLineL" class="form-control w-75 d-inline">
                    ${locationsHTML}
                 </select><button type="button" class="btn btn-primary btn-sm border-0" onclick="SubmitInLineData()"><i class="fas fa-plus"></i></button>`
                ]);
                records.forEach(data => {
                    var html = '<option value="">Please Select</option>';
                    var LocationHtml = '<option value="">Please Select</option>';
                    locations.forEach(location => {
                        html += `<option value="${location.LocationID}" ${(data.Priority == location.LocationID ? "selected" : "")}>${location.LocationName}</option>`;
                        LocationHtml += `<option value="${location.LocationID}" ${(data.LocationID == location.LocationID ? "selected" : "")}>${location.LocationName}</option>`;
                    });
                    list.push(
                        [
                            `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="PONumber" value="${data.PONumber}" />`,
                            //`<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="ItemCode" value="${data.ItemCode}" />`,
                            data.ItemCode,
                            `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Product" value="${data.Product}" />`,
                            `<input type="date" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="StartShipDate" value="${formatted_date(data.StartShipDate)}" />`,
                            `<input type="date" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="EndShipDate" value="${formatted_date(data.EndShipDate)}" />`,
                            `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Color" value="${data.Color}" />`,
                            `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="QTY" value="${data.QTY}" />`,
                            data.BalanceQTY,
                            `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Pack" value="${data.Pack}" />`,
                            `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Cost" value="${data.Cost}" />`,
                            `<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Grower" value="${data.Grower}" />`,
                            `<select id="Priority" class="form-control drpLocation" onblur="saveOnBlur(${data.PreBookID},this)" value="${data.Priority}">
                            ${html}
                        </select>`,
                            data.VendorRemark,
                            //`<input type="text" class="form-control" onblur="saveOnBlur(${data.PreBookID},this)" id="Notes" value="${data.Notes}" />`,
                            //`<button type="button" class="btn btn-primary btn-sm border-0" onclick="editModal('${data.PreBookID}','PreBookModal')" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit Tax Details"><i class="fas fa-pen"></i></button>
                            //`<a target="_blank" href="AllocatingProducts.aspx?PreBookID=${data.PreBookID}&priority=${data.Priority}" class="btn btn-primary btn-sm border-0">Allocate</a>`
                            `<select id="LocationID" class="form-control drpLocation" onblur="saveOnBlur(${data.PreBookID},this)" value="${data.LocationID}">
                            ${LocationHtml}
                        </select >`
                        ]);
                });
                applyDatatable(list);
            }
            else {
                records.forEach(data => {
                    listnew.push(
                        [
                            data.PONumber,
                            data.ItemCode,
                            data.Product,
                            formatDate(data.StartShipDate),
                            formatDate(data.EndShipDate),
                            data.Color,
                            data.QTY,
                            data.BalanceQTY,
                            data.Pack,
                            data.Cost,
                            data.Grower,
                            data.Priority,
                            data.VendorRemark,
                            data.LocationID
                        ]
                    )
                });
                applyDatatable(listnew);

            }
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
                paging:false
            });

            getVendor();
            //getLocations();
            getItems();
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
            $('#allocateType').val('');
            $('#type').prop("checked", false);
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
            $('#shipD').val(today);
            loaddata();
        }


        function closeModal(id) {
            $('#' + id).modal('hide');
        }

        function editModal(ID,modaID) {
            if (ID) {
                $.ajax({
                    method: 'GET',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems/Edit?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&preBookID=${ID}`,
                    dataType: 'json',
                    success: function (response) {
                        if (response == "Failed") {
                            toastr.error('Something Went Wrong!!');
                        } else {
                            if (response.length) {
                                $('#prebookID').val(response[0].PreBookID);
                                $('#PO').val(response[0].PONumber);
                                $('#itemCode').val(response[0].ItemCode);
                                $('#product').val(response[0].Product);
                                $('#shipD').val(response[0].ShipDate);
                                $('#color').val(response[0].Color);
                                $('#qty').val(response[0].QTY);
                                $('#pack').val(response[0].Pack);
                                $('#priority').val(response[0].Priority);
                                $('#cost').val(response[0].Cost);                                
                                $('#location').val(response[0].LocationID);
                                $('#grower').val(response[0].Grower);
                                $('#notes').val(response[0].Notes);
                                $('#' + modaID).modal('show');
                            }
                        }
                    }
                });
            }
        }

        function openModal(id) {
            var date = new Date();
            var today = formatted_date(date);

            $('#prebookID').val('');
            $('#PO').val('');
            $('#itemCode').val('');
            $('#product').val('');
            $('#SshipD').val(today);
            $('#EshipD').val(today);
            $('#color').val('');
            $('#qty').val('');
            $('#pack').val('');
            $('#cost').val('');
            $('#priority').val('');
            $('#location').val('');
            $('#grower').val('');
            $('#notes').val('');
            $('#' + id).modal('show');
        }

        function SubmitData() {
            var id = $('#prebookID').val();
            if (id) {
                var formData = {
                    "CompanyID": CompanyID,
                    "DivisionID": DivisionID,
                    "DepartmentID": DepartmentID,
                    "PreBookID" : $('#prebookID').val(),
                    "PONumber" : $('#PO').val(),
                    "ItemCode" : $('#itemCode').val(),
                    "Product" : $('#product').val(),
                    "StartShipDate": $('#SshipD').val(),
                    "EndShipDate" : $('#EshipD').val(),
                    "Color": $('#color').val(),
                    "Priority": $('#priority').val(),
                    "LocationID" : $('#location').val(),
                    "QTY" : $('#qty').val(),
                    "Pack" : $('#pack').val(),
                    "Cost" : $('#cost').val(),
                    "Grower" : $('#grower').val(),
                    "Notes" : $('#notes').val()
                }
                $.ajax({

                    method: 'Put',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successfull") {
                            closeModal("PreBookModal");
                            loaddata();
                           //toastr.success('Updated Successfully!!');
                        } else {
                            //toastr.error('Something Went Wrong!!')
                        }
                    }

                });
            } else {
                var formData = {
                    "CompanyID": CompanyID,
                    "DivisionID": DivisionID,
                    "DepartmentID": DepartmentID,
                    "PONumber": $('#PO').val(),
                    "ItemCode": $('#itemCode').val(),
                    "Product": $('#product').val(),
                    "StartShipDate": $('#SshipD').val(),
                    "EndShipDate": $('#EshipD').val(),
                    "Color": $('#color').val(),
                    "QTY": $('#qty').val(),
                    "Pack": $('#pack').val(),
                    "Priority": $('#priority').val(),
                    "LocationID": $('#location').val(),
                    "Cost": $('#cost').val(),
                    "Grower": $('#grower').val(),
                    "Notes": $('#notes').val()
                };
                $.ajax({
                    method: 'Post',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successfull") {
                            closeModal("PreBookModal");
                            loaddata();
                            //toastr.success('Added Successfully!!');
                        } else {
                            //toastr.error('Something Went Wrong!!')
                        }
                    }

                });
            }
        }

        function SubmitInLineData() {
                var formData = {
                    "CompanyID": CompanyID,
                    "DivisionID": DivisionID,
                    "DepartmentID": DepartmentID,
                    "PONumber": $('#InLinePONumber').val(),
                    "ItemCode": $('#InLineItemCode').val(),
                    "Product": $('#InLineProduct').val(),
                    "StartShipDate": $('#InLineSShipDate').val(),
                    "EndShipDate": $('#InLineEShipDate').val(),
                    "Color": $('#InLineColor').val(),
                    "QTY": $('#InLineQTY').val(),
                    "Pack": $('#InLinePack').val(),
                    "Priority": $('#InLineP').val(),
                    "LocationID": $('#InLineL').val(),
                    "Cost": $('#InLineCost').val(),
                    "Grower": $('#InLineGrower').val(),
                    "Notes": $('#InLineNotes').val()
                };
                $.ajax({
                    method: 'Post',
                    url: `https://secureapps.quickflora.com/V2/api/PreBooktems`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successfull") {
                            closeModal("PreBookModal");
                            loaddata();
                            //toastr.success('Added Successfully!!');
                        } else {
                            //toastr.error('Something Went Wrong!!')
                        }
                    }

                });
        }

        function saveOnBlur(id,el) {
            var key = $(el).attr("id");
            var valueData = $(el).val();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&preBookID=${id}&key=${key}&value=${valueData}&type=0&type1=1&type2=2&type3=3&type4=4`,
                dataType: 'json',
                success: function (response) {
                    if (response == "Successfull") {
                        loaddata();
                    } else {
                        alert("Something Went Wrong!")
                    }
                }
            });
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
                          $.typeahead({
                              order: "asc",
                              minLength: 1,
                              input: "#InLineGrower",
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
                          $.typeahead({
                              order: "asc",
                              minLength: 1,
                              input: "#InLineItemCode",
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
                                      $.ajax({
                                          method: 'GET',
                                          url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${i.display}&type=1`,
                                          dataType: 'json',
                                          success: function (response) {
                                              if (response.length && response != "Failed") {
                                                  $('#InLineProduct').val(response[0].ItemName);
                                                $('#InLineCost').val(response[0].Price);
                                              } else {
                                                  alert("No Item found on " + i.display + " item code");
                                              }
                                          }
                                      });
                                  }
                              }
                          });
                          $.typeahead({
                              order: "asc",
                              minLength: 1,
                              input: "#itemCode",
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

                                      $.ajax({
                                          method: 'GET',
                                          url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&itemID=${i.display}&type=1`,
                                          dataType: 'json',
                                          success: function (response) {
                                              if (response.length && response != "Failed") {
                                                  $('#product').val(response[0].ItemName);
                                                  $('#cost').val(response[0].Price);
                                              } else {
                                                  alert("No Item found on " + i.display + " item code");
                                              }
                                          }
                                      });
                                  }
                              }
                          });
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