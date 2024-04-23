<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustomerReport.aspx.vb" Inherits="Report_CustomerReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QuickFlora | Customer Report</title>
     <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <link href="assets/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.11.2/css/dataTables.bootstrap5.min.css"/>
    <link rel="stylesheet" href="assets/css/fontawesome.min.css"  />
    <link rel="stylesheet" href="assets/css/customerList.css"  />
    <link rel="stylesheet" href="assets/plugin/waitMe/waitMe.min.css"  />
     <link href="assets/plugin/toast/build/toastr.css" rel="stylesheet" />
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
                <a class="nav-link active" aria-current="page" href="#">Customers Report</a>
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
      <div class="card mt-3">
        <h5 class="card-header">Filter Options</h5>
        <div class="card-body">
         
            <div class="row">
                <div class="col-md-10">
                     <div class="row">
            <div class="col-6 text-center">
              <span class="btn btn-light rounded btn-sm">
                <input id="emial" type="radio" name="email" value="0" checked>
                <label for="emial">All</label>
              </span>&nbsp;&nbsp;
              <span class="btn btn-light rounded btn-sm">
                <input id="emailtrue" type="radio" name="email" value="1">
                <label for="emailtrue">Email Mandatory</label>
              </span>
            </div>
            <div class="col-6 text-center">
              <span class="btn btn-light rounded btn-sm">
                <input id="alltrade" type="radio" name="trade" value="0" checked>
                <label for="alltrade">All</label>
              </span>&nbsp;&nbsp;
              <span class="btn btn-light rounded btn-sm">
                <input id="tradetrue" type="radio" name="trade" value="1">
                <label for="tradetrue">Trade Discount Applied</label>
              </span>&nbsp;&nbsp;
              <span class="btn btn-light rounded btn-sm">
                <input id="nottrade" type="radio" name="trade" value="2">
                <label for="nottrade">Trade Discount Not Applied</label>
              </span>
            </div>
          </div>

                </div> 
                
                 <div class="col-md-2">
                              <button type="button" id="start" class="buttons-html5"><i class="fa fa-cogs"></i> Refresh Report  </button>
                         </div> 
           
       </div>
      </div>

      <div class="card mt-3 text-wrap" id="loader">
        <div class="card-body overflow-auto">

          <table id="customers" class="table table-striped hover" style="width: 100%;">
            <thead>
              <tr>
                <th>Customer ID</th>
                <th>Customer Name</th>
                <th>Phone</th>
                <th>Email</th>
                <th>Address 1</th>
                <th>Address 2</th>
                <th>City</th>
                <th>State</th>
                <th>Zip</th>
                <th>Country</th>
                <th>Fax</th>
                <th>Type</th>
                <th>Company</th>
                <th>Credit Limit</th>
                <th>Since</th>
                <th>Trade Discount</th>
                <th>Price Type</th>
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
        >2021&copy;</span
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

         var queryString = "CompanyID=<%=CompanyID%>&DivisionID=<%=DivisionID%>&DepartmentID=<%=DepartmentID%>";
        

      $('input[name = "email"]').on('change', function(){
       // loaddata();
      });
      $('input[name = "trade"]').on('change', function(){
      //  loaddata();
        });

        $("#start").click(function () {
            loaddata();
        });
      
      
      function loaddata()
      {
        var email = $('input[name = "email"]:checked').val();
        var trade = $('input[name = "trade"]:checked').val();
        loadStart();
        $.ajax({
          method:'GET',
          url:`https://secureapps.quickflora.com/V1/api/CustomerReport?CompanyID=<%=CompanyID%>&DivisionID=<%=DivisionID%>&DepartmentID=<%=DepartmentID%>&email=${email}&applyTrade=${trade}`,
          dataType : 'json',
          success:function(response){
            buildTable(response);
          }
        });
              
      }

	    function buildTable(customers){
	    	var table = document.getElementById('customers');
            var list = [];
            customers.forEach(customer => {
                list.push(
                    [
                        customer.CustomerID,
                        customer.CustomerFirstName +" " + customer.CustomerLastName,
                        customer.CustomerPhone,
                        customer.CustomerEmail,
                        customer.CustomerAddress1,
                        customer.CustomerAddress2,
                        customer.CustomerCity,
                        customer.CustomerState,
                        customer.CustomerZip,
                        customer.CustomerCountry,
                        customer.CustomerFax,
                        customer.CustomerTypeID,
                        customer.CustomerCompany,
                        customer.CreditLimit,
                        formatDate(customer.CustomerSince),
                        customer.TradeDiscount,
                        customer.PriceType
                    ]
                );
            });
        applyDatatable(list);
        
      }

      function applyDatatable(tableData) {
        loadStop();
        if($.fn.DataTable.isDataTable('#customers')){
          $('#customers').DataTable().destroy();
        }
          var customers = $('#customers').DataTable( {
             dom: 'frBtilp',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ],
              title : 'Customer Report',
              select: 'multi',
              data : tableData
          } );
      }

      function loadStart(){
        $('#loader').waitMe({
          effect : 'win8_linear',
          text : '',
          bg : 'rgba(255,255,255,0.7)',
          color : '#678b38',
          waitTime : -1,
          textPos : 'vertical',
          onClose : function() {}
          });
      }
        
      function loadStop(){
        $('#loader').waitMe('hide');
      }

      function formatDate(date){
        if(date){
            date  = new Date(date);
            var time = date.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
            var m = ("0"+ (date.getMonth()+1)).slice(-2); 
            var d = ("0"+ date.getDate()).slice(-2); 
            var y = date.getFullYear();
            return  m +'/'+d+'/'+y+" "+time; 
        }else{
            return "";
        }
      }

      function changeSize(el){
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