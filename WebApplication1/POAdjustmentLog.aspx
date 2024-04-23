<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="POAdjustmentLog.aspx.vb" Inherits="POAdjustmentLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />


    <style>
        div.dataTables_info
        {
            padding-top:40px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    PO Adjustment Logs



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    
           

                
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Filter Option
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

                                <div class="form-group col-sm-2 col-md-2">
                                    <div class="form-body">
                                        <label for="fdate">From Date :</label>
                                        <input type="date" class="form-control" id="fromdate" required />
                                    </div>

                                </div>
                                <div class="form-group col-sm-2 col-md-2">
                                    <div class="form-body">
                                        <label for="todate">To Date :</label>
                                        <input type="date" class="form-control" id="todate" required />
                                    </div>
                                </div>
                                <div class="form-group col-sm-2 col-md-2 margin-top-20">
                                    <div class="form-body">
                                        <div class="form-check form-check-inline">
                                                  <input class="form-check-input" type="radio" id="allDate" name="dateType" value="0">
                                                  <label class="form-check-label" for="allDate" style="vertical-align:sub;margin-left:5px;">All Dates</label>
                                                    
                                          </div>
                                    </div>
                                </div>
                                <div class="form-group col-sm-2 col-md-2 margin-top-20">
                                    <div class="form-body">
                                        <div class="form-check form-check-inline ">                                                  
                                                  <input class="form-check-input float-start" type="radio" id="processDate" name="dateType" value="1" />
                                                  <label class="form-check-label" for="processDate" style="vertical-align:sub;margin-left:5px;">Process Date</label>
                                          </div>
                                    </div>
                                </div>
                                <div class="form-group col-sm-2 col-md-2">
                                    <div class="form-body">
                                        <label for="PONumber">PO Number:</label>
                                        <input type="text" class="form-control" id="PONumber"  />
                                    </div>
                                </div>
                                <div class="form-group col-sm-2 col-md-2">
                                    <div class="form-body">
                                        <label for="ItemID">Item ID:</label>
                                        <input type="text" class="form-control" id="ItemID" />
                                    </div>
                                </div>
                               
                                

                            </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                
                                    <div class="form-body">
                                       <div class="text-right">
                                         <button type="button" class="btn btn-primary border-0" onclick="SearchData()">Search</button>
                                         <button type="button" class="btn btn-light" onclick="clearData()">Clear</button>
                                       
                                        
                                    </div>
                                    </div>
                                
                            </div>
                            </div>
                            
                        </div>
                        
                    </div>
                    
                </div>
                
                <!-- END PAGE CONTENT-->
            </div>
            
            
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                PO Adjustment Logs
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="table-responsive">                    
                                <table class="table table-striped hover " id="table-Item" style="width: 100%;">
                                     <thead>
                                          <tr>  
                                            <th>PO Number </th> 
                                            <th>Item ID </th> 
                                            <th>Item Name</th>                                            
                                            <th>Received Qty</th> 
                                            <th>Adjusted Qty</th> 
                                            <th>Reason</th>                            
                                            <th>Adjusted By</th>      
                                            <th>Adjusted Date</th>                          
                                          </tr>
                                    </thead>
                                    <tbody id="tbodyItem">
                         
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
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


        $("#allDate").prop("checked", true);

         var CompanyID = "<%=CompanyID%>";
            var DivisionID = "<%=DivisionID%>";
            var DepartmentID = "<%=DepartmentID%>";
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

            SearchData();
           
        }
        function formatDate(date) {
            date = new Date(date);
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();

            return m + '/' + d + '/' + y;
        }

       

        
        
        function SearchData()
        {
            var fromdate = $('#fromdate').val();
            var todate = $('#todate').val();
            var dateType = parseInt($("input[name='dateType']:checked").val());
            var PONumber = $("#PONumber").val();
            var ItemID = $("#ItemID").val();
           


           
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/POAdjustmentLog?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&fromdate=${fromdate}&todate=${todate}&dateType=${dateType}&PONumber=${PONumber}&ItemID=${ItemID}`,
                dataType: 'json',
                error: function () {

                    applyDatatable('');
                },
                success: function (response) {
                    if (response.length)
                    {
                        
                        buildTable(response);
                       
                    }
                    else {
                        

                        applyDatatable(response);

                    }

                }

            });
        }
        
        
       
        function buildTable(JsonData) {
            var list = [];
            JsonData.forEach(record =>
            {
                list.push([


                    record.PONumber,
                    record.ItemID,
                    record.ItemName,
                    
                    record.ReceivedQty,
                    record.AdjustQty,
                    record.Reason,
                    record.EmployeeID,
                    formatDate(record.DateTime),
                   

                ]);
            });

            applyDatatable(list);

        }
        function applyDatatable(tableData) {
           
            if ($.fn.DataTable.isDataTable("#table-Item")) {
                $("#table-Item").DataTable().destroy();
            }

            $('#table-Item').DataTable({
                dom: 'frtBlip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'pdfHtml5',
                        title: 'Adjustments',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    },
                ],
                title: 'Adjustments',
                data: tableData,
                order:[]

            });

        }

        function clearData()
        {
            $("#allDate").prop("checked", true);
            $("#processDate").prop("checked", false);
            $("#PONumber").val('');
            $("#ItemID").val('');

            today();
        }
        

    </script>

</asp:Content>