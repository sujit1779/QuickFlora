<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReceivedPOAdjustment.aspx.vb" Inherits="RO_ReceivedPOAdjustment" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quickflora Order Entry</title>
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
                <a class="navbar-brand" href="../Home.aspx"><i class="fas fa-arrow-left hover-visible"></i>&nbsp;Back to POS Dashboard</a>
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
                            <input type="date" class="form-control" id="fromdate" required />
                        </div>

                        <div class="col-12 col-md-2">
                            <label for="todate">To Date :</label>
                            <input type="date" class="form-control" id="todate" required />
                         </div>                       
                        
                        <div class="col-12 col-md-3 mt-4" >
                                 <div class="row g-0 ">
                                       <div class="form-group col-4 ">
                                         <div class="form-check form-check-inline">
                                                  <input class="form-check-input" type="radio" id="allDate" name="processDate" value="0">
                                                  <label class="form-check-label" for="allDate" style="vertical-align:sub;margin-left:5px;">All Dates</label>
                                                    
                                          </div>
                                       
                                       </div>
                                       <div class="form-group col-5">
                                          <div class="form-check form-check-inline ">                                                  
                                                  <input class="form-check-input float-start" type="radio" id="processDate" name="processDate" value="1" >
                                                  <label class="form-check-label" for="processDate" style="vertical-align:sub;margin-left:5px;">Process Date</label>
                                          </div>
                                       
                                      </div>
                                 </div>
                        </div>

                        
                         <div class="col-12 col-md-2">
                            <label for="PONumber">PO Number:</label>
                           
                            <input type="text" class="form-control" id="PONumber" />
                        </div>
                        
                        <div class="col-12 col-md-2">
                            <label for="ItemID">Item ID:</label>
                            <input type="text" class="form-control" id="ItemID" />
                        </div>

                    </div>

                    <div class="text-end pt-3">
                        <button type="button" class="btn btn-primary border-0" onclick="SearchData()">Search</button>
                        <button type="button" class="btn btn-light" onclick="clearData()">Clear</button>
                    </div>
                </div>
            </div>

            <div class="card mt-3 text-wrap" id="loader">
                <div class="card-header row m-0">
                    <h4 class="col-12">Received PO Adjustment Log</h4>
                </div>

                <div class="card-body overflow-auto">
                    <table id="table-Item" class="table table-striped hover" style="width: 100%;">
                        <thead>
                            <tr >
                            
                                <th>PO Number</th>
                                <th>Item Id</th>
                               
                                <th>Date</th>
                                <th>Employee </th> 
                                <th>Received Qty</th>
                                <th>Adjust Qty</th>
                                <th>Reason</th>
                                 
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
        ><span id="copyYear"></span>&copy;</span
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

        

        //var CompanyID = $('#CompanyID').val();
        //var DivisionID = $('#DivisionID').val();
        //var DepartmentID = $('#DepartmentID').val();

         var CompanyID = "<%=CompanyID%>";
            var DivisionID = "<%=DivisionID%>";
            var DepartmentID = "<%=DepartmentID%>";

        $("#allDate").prop("checked", true);
        today();

        function today() {
            // $('input[type="date"]').attr('disabled',true);
            var date = new Date();
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();

           
            var today = y + '-' + m + '-' + d ;


           

            $('#fromdate').val(today);
            $('#todate').val(today);
           

            SearchData();
        }
        function formatDate(date) {
            date = new Date(date);
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();
            return m + '/' + d + '/' + y + " " + date.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', second: 'numeric', hour12: true });
        }

       

        


        function SearchData() {

            loadStart();
            var fromdate = $('#fromdate').val();
            var toDate = $('#todate').val();
            var processDate = parseInt($("input[name='processDate']:checked").val());
            var po = ($('#PONumber').val() ? $('#PONumber').val() : '');
            var itemID = ($('#ItemID').val() ? $('#ItemID').val() : '');


            
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/ReceivedPOAdjustmentLog?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&fromdate=${fromdate}&toDate=${toDate}&po=${po}&itemID=${itemID}&processDate=${processDate}`,
                dataType: 'json',
                error: function () {

                    applyDatatable('');

                },
                success: function (response) {
                    if (response.length) {

                        buildTable(response);

                    }
                    else {

                        applyDatatable('');

                    }

                }

            });

        }

        function buildTable(JsonData) {
            var list = [];
          
            JsonData.forEach(record => {
                
                list.push([

                    record.PONumber,
                    record.ItemID,
                   
                   formatDate(record.DateTime),
                    record.EmployeeID,
                    record.ReceivedQty,
                    record.AdjustQty,
                    record.Reason
                    

                ]);
            });

            applyDatatable(list);

        }
        function applyDatatable(tableData) {
            loadStop();
            if ($.fn.DataTable.isDataTable("#table-Item")) {
                $("#table-Item").DataTable().destroy();
            }

            $('#table-Item').DataTable({

                dom: 'frtBlip',
                "iDisplayLength": 200,
                "lengthMenu": [200, 300, 400, 500, 1000],
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'pdfHtml5',
                        title: 'Web App List',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    },
                ],
                title: 'Web App List',
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



       



        function clearData() {
            $("#allDate").prop("checked", true);
            $("#processDate").prop("checked", false);
           
            $('#PONumber').val('');
            $('#ItemID').val('');
            
            today();
        }
    </script>

</body>
    </html>
