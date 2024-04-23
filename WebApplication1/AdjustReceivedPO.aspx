<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="AdjustReceivedPO.aspx.vb" Inherits="AdjustReceivedPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    <style>div.dataTables_info {
  padding-top: 40px;
}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <h3 class="page-title">Adjustments</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    
           

                
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Purchase Details
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

                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >PO Number:</label>
                                        <label for="PurchaseNumber" id="PurchaseNumber" style="font-weight: bold;"></label>
                                    </div>

                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >PO Date:</label>
                                        <label id="PurchaseDate" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >PO Location:</label>
                                        <label id="LocationID" style="font-weight: bold;"> </label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Entered by:</label>
                                        <label id="EnteredBy" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Payment Method:</label>
                                        <label id="PaymentMethodID" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Vendor:</label>
                                        <label id="VendorID" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Ship Method:</label>
                                        <label id="ShipMethodID" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Arival Date:</label>
                                        <label id="ArrivalDate" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Total:</label>
                                        <label id="Total" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Receive by:</label>
                                        <label id="ReceivedBy" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Received Date:</label>
                                        <label id="ReceivedDate" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Ship to Location:</label>
                                        <label id="ShipLocationID" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                

                            </div>
                        </div>
                            
                        </div>
                        
                    </div>
                    
                </div>
                
                <!-- END PAGE CONTENT-->
            </div>
            <%--<div class="row">
                <div class="col-md-12">
                    <div class="portlet box">
                       
                        <div class="portlet-body form">
                            <div class="form-horizontal" role="form">
                                <div class="form-body">
                                    <div class="form-actions right">
                                        <asp:Label ID="lblStatus" runat="server" class="col-md-5 control-label" ForeColor="Red" Visible="false"></asp:Label>
                                       
                                        <a ID="btnSubmit" class="btn green" onclick="insertData();">SUBMIT</a>
                                        <a id="btnClear" runat="server" class="btn grey" onclick="clearform();">CLEAR</a>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Item Details
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="table-responsive">                    
                                <table class="table table-striped hover " id="table-Item" style="width: 100%;">
                                     <thead>
                                          <tr>  
                                            <th>Item ID </th> 
                                            <th>Item Name</th> 
                                            <th>Order Qty</th> 
                                            <th>Received Qty</th> 
                                            <th>Adjusted QTY</th>                          
                                            <th>Adjustment Qty</th> 
                                            <th style="width:20%">Reason</th>                            
                                            <th>Adjust</th>      
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
        
            var CompanyID = "<%=CompanyID%>";
            var DivisionID = "<%=DivisionID%>";
            var DepartmentID = "<%=DepartmentID%>";
            var PurchaseNumber = "<%=PO%>";


        function formatDate(date) {
            date = new Date(date);
            var time = date.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });
            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();
            return m + '/' + d + '/' + y;
        }

        function today() {
            var date = new Date();
            var today = formatted_date(date);

            $('#ServiceDate').val(today);
            $('#PurchaseDate').val(today);

        }
        function formatted_date(date) {
            date = new Date(date);

            var m = ("0" + (date.getMonth() + 1)).slice(-2);
            var d = ("0" + date.getDate()).slice(-2);
            var y = date.getFullYear();
            return y + '-' + m + '-' + d;
        }

        
        function checkQty(el, qty) {
            var current = $(el).val();

            if (parseInt(current) > parseInt(qty)) {
                $(el).val('');
                alert("Maximum quantity allowed is " + qty);
            }
        }
        
        
        HeaderDetail();
        ItemDetail();


        function HeaderDetail()
        {
            var type = "Detail";
            //var PurchaseNumber = $("#PurchaseNumber").val();
            
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/POItemAdjustment?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&PurchaseNumber=${PurchaseNumber}&type=${type}`,
                dataType: 'json',
                error: function () {

                    applyDatatable('');

                },
                success: function (response)
                {
                    if (response.length) {
                        $("#PurchaseNumber").text(response[0].PurchaseNumber);
                        $("#PurchaseDate").text((response[0].PurchaseDate != "1900-01-01T00:00:00" ? formatDate(response[0].PurchaseDate) : ''));
                        $("#LocationID").text(response[0].LocationID);
                        $("#EnteredBy").text(response[0].EnteredBy);
                        $("#PaymentMethodID").text(response[0].PaymentMethodID);
                        $("#VendorID").text(response[0].VendorID);
                        $("#ShipMethodID").text(response[0].ShipMethodID);
                        $("#ArrivalDate").text((response[0].PurchaseDate != "1900-01-01T00:00:00" ? formatDate(response[0].ArrivalDate) : ''));
                        $("#Total").text(response[0].Total);
                        $("#ReceivedBy").text(response[0].ReceivedBy);
                        $("#ReceivedDate").text((response[0].ReceivedDate != "1900-01-01T00:00:00" ? formatDate(response[0].ReceivedDate) : ''));
                        $("#ShipLocationID").text(response[0].ShipLocationID);
                        
                    }
                    else {

                        alert('No Data Found');

                    }

                }

            });
        }

        function ItemDetail()
        {
            var type = "";
            
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/POItemAdjustment?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&PurchaseNumber=${PurchaseNumber}&type=${type}`,
                dataType: 'json',
                error: function () {

                    applyDatatable('');
                    //toastr.error('Server Not Found!');
                },
                success: function (response)
                {
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
            JsonData.forEach(record => {
                var balance = 0;
                balance = parseInt(record.ReceivedQty) - parseInt(record.AdjustedQty);
                list.push([
                    record.ItemID,
                    record.ItemNmae,
                    record.OrderQty,
                    record.ReceivedQty + `<input type="hidden" id="${record.PurchaseLineNumber}-receive" value="${record.ReceivedQty}" />`,
                    record.AdjustedQty,
                    `<input type="text" id="${record.PurchaseLineNumber}-qty" onblur="checkQty(this,${balance})" class="form-control input-md"  />`,
                    `<input type="text" id="${record.PurchaseLineNumber}-Reason" class="form-control"  />`,
                    `<button type="button" class="btn btn-primary btn-sm" onclick="AdjustQty(this,'${record.PurchaseLineNumber}')">Adjust</button>`
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

        function AdjustQty(el, lineno) {
            $(el).attr("disabled", true);
            var qty = $("#" + lineno + "-qty").val();
            qty = (qty ? qty : 0);
            if (qty > 0) {
                var formData =
                {
                    'CompanyID': CompanyID,
                    'DivisionID': DivisionID,
                    'DepartmentID': DepartmentID,
                    'PurchaseNumber': PurchaseNumber,
                    'ItemID': lineno,
                    'EnteredBy': "<%=Session("EmployeeID")%>",
                    'ReceivedQty': $("#" + lineno + "-receive").val(),
                    'AdjustedQty': $("#" + lineno + "-qty").val(),
                    'Reason': $("#" + lineno + "-Reason").val(),
                    'PurchaseLineNumber': lineno
                };

                $.ajax({
                    method: 'Post',
                    url: 'https://secureapps.quickflora.com/V2/api/POItemAdjustment',
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successful") {
                            alert("Adjusted Successfully");
                            HeaderDetail();
                            ItemDetail();
                        } else {
                            alert("Something Went Wrong!");
                        }

                    }
                });
            } else {
                $(el).attr("disabled", false);

                alert("Please Enter Qty for Adjustment");
            }
        }


    </script>

</asp:Content>

