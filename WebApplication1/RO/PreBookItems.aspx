<%@ Page Language="VB" AutoEventWireup="true" CodeFile="PreBookItems.aspx.vb" ValidateRequest="false" Inherits="RO_PreBookItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pre Book Items</title>
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
                <a class="nav-link active" aria-current="page" href="#">Pre Book Item</a>
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
                        <div class="col-12 col-md-4">
                            <label for="shipDate">Shipping Date :</label>
                            <input type="date" class="form-control" id="shipDate"/>
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
                    <div class="col-12 col-md-4 text-end">
                        <button type="button" class="btn btn-primary border-0" onclick="openModal('PreBookModal')">Add New</button>
                    </div>
                </div>

                <div class="card-body overflow-auto">
                    <table id="details" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>PO Number</th>
                                <th>Item Code</th>
                                <th>Product</th>
                                <th>Shipping Date</th>
                                <th>Color / Variety</th>
                                <th>QTY</th>
                                <th>Pack</th>
                                <th>Cost</th>
                                <th>Grower</th>
                                <th>Notes</th>
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
                            <label for="shipD">Shipping Date : </label>
                            <input type="date" id="shipD" class="form-control" />
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-6">
                            <label for="color">Color / Variety : </label>
                            <input type="text" id="color" class="form-control" />
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="qty">Quantity : </label>
                            <input type="text" id="qty" class="form-control" />
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12 col-md-6">
                            <label for="pack">Pack : </label>
                            <input type="text" id="pack" class="form-control" />
                        </div>
                        <div class="col-12 col-md-6">
                            <label for="cost">Cost : </label>
                            <input type="text" id="cost" class="form-control" />
                        </div>
                    </div>

                    <div class="row m-0 mt-4">
                        <div class="col-12">
                            <label for="vendor">Grower :</label>
                            <div class="typeahead__container">
                                 <div class="typeahead__field">
                                     <div class="typeahead__query">
                                         <input class="typehead drpVendor" placeholder="Search Grower" autocomplete="off" id="grower" />
                                     </div>
                                 </div>
                             </div>
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


        //Vender ID API Call
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
                }
            }
        });

        //Item ID API Call
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

        today();

        function loaddata() {

            loadStart();
            var shipDate = $('#shipDate').val();
            var itemID = $('#ItemID').val();
            var vendor = $('#vendor').val();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/PreBooktems?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&shipDate=${shipDate}&ItemID=${itemID}&vendor=${vendor}`,
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
                        data.PONumber,
                        data.ItemCode,
                        data.Product,
                        (data.ShipDate ? formatDate(data.ShipDate) : ""),
                        data.Color,
                        data.QTY,
                        data.Pack,
                        data.Cost,
                        data.Grower,
                        data.Notes,
                        `<button type="button" class="btn btn-primary btn-sm" onclick="editModal('${data.PreBookID}','PreBookModal')" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit Tax Details"><i class="fas fa-pen"></i></button> &nbsp;&nbsp;<button type="button" class="btn btn-primary btn-sm" onclick="allocate('${data.PreBookID}','PreBookModal')" data-bs-toggle="tooltip" data-bs-placement="top" title="Allocate"><i class="fas fa-check-circle"></i></button>`
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

            $('#shipDate').val(today);
            $('#shipD').val(today);
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
                                $('#shipD').val(response[0].ShipDate.replace("T00:00:00", ""));
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
            $('#prebookID').val('');
            $('#PO').val('');
            $('#itemCode').val('');
            $('#product').val('');
            $('#shipD').val('');
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
                    "ShipDate" : $('#shipD').val(),
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
                            toastr.success('Updated Successfully!!');
                        } else {
                            toastr.error('Something Went Wrong!!')
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
                    "ShipDate": $('#shipD').val(),
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
                            toastr.success('Added Successfully!!');
                        } else {
                            toastr.error('Something Went Wrong!!')
                        }
                    }

                });
            }
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