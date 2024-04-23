<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="DeliveryScheduleDetail.aspx.vb" Inherits="DeliveryScheduleDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    <style>
        .autocompletenew {
            overflow-x: hidden;
            overflow-y: auto;
            height: 200px;
            position: absolute;
            z-index: 2;
            width: 250px;
            background-color: #fff;
            box-shadow: 5px 5px rgba(102, 102, 102, 0.1);
            border: 1px solid #40BD24;
            visibility: hidden;
        }
        .dataTables_info{
            text-align:end;
        }
        @media  (min-width: 768px) 
        {
          .modal-dialog
          {
            width:768px !important;
            height:200px;
          }
        }

        @media (min-width: 992px)
        {
            .modal-dialog
            {
            width: 800px;
            }
        }

        @media (min-width: 576px) {
            .modal-dialog {
                width: 500px;
                height:200px;
                margin: 1.75rem auto;
            }
        }

        @media (min-width: 992px){
            .modal-dialog {
              width: 800px;
              height:200px;
            }
        }

        @media (min-width: 576px) {
            .modal-dialog {
                width: 500px;
                height:200px;
                margin: 1.75rem auto;
            }
        }

    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    Delivery Schedule Details
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <div class="card mt-3 text-wrap" id="loader">
        
            <div class="card-header text-white bg-dark" style="margin-bottom:20px;">
                <div class="row">
                    <div class="col-md-8">
                    </div>
                  
                    <div class="col-md-4 text-right"> 
                        <button type="button" class="btn btn-primary" onclick="addModal('ScheduleModal')" data-bs-toggle="tooltip" data-bs-placement="top">Add New</button>
                    </div>
                </div>              
            </div>   
        <div class="card-body">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption" style="width: 95%;">
                        <div class="row">
                            <div class="col-md-12">
                                    Schedule Details
                            </div>
                        </div>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body form"  >
                    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green ">

                <div class="portlet-body form">
                    <div class="card-body">
                        <div class="row">
                            <div class="form-group">

                                <div class="form-group col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Schedule ID: </label>
                                        <label for="ScheduleID" id="ScheduleID" style="font-weight: bold;"></label>
                                        <input type="hidden" id="mainID" />
                                    </div>

                                </div>
                                <div class="form-group col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Origin: </label>
                                        <label id="TxtOrigin" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Shipping Method: </label>
                                        <label id="ShipMethodDescription" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Ship To Location: </label>
                                        <label id="LocationName" style="font-weight: bold;"></label>
                                    </div>
                                </div>

                                <div class="form-group col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Trucking Day: </label>
                                        <label id="TruckingDay" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                <div class="form-group col-sm-6 col-md-4">
                                    <div class="form-body">
                                        <label >Arrival Day: </label>
                                        <label id="ArrivalDay" style="font-weight: bold;"></label>
                                    </div>
                                </div>
                                

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END PAGE CONTENT-->
    </div>

                    <div class="row " style="margin:0;margin-bottom:2rem;">
                        
                    </div>
                      <div class="table-responsive">                    
                              <table class="table table-striped hover " id="table-Country" style="width: 100%;">
                                <thead>
                                  <tr>  
                                    <th>Step</th> 
                                    <th>Origin</th> 
                                    <th>Destination</th>  
                                    <th>Carrier</th> 
                                    <th>Carrier Type</th> 
                                    <th>Start Day</th> 
                                    <th>End Day</th> 
                                    <th>Total Days</th> 
                                    <th>Cut Off Day</th> 
                                    <th>Cut Off Time</th> 
                                    <th>Charges</th> 
                                    <th>Action</th>                          
                                  </tr>
                                </thead>
                                <tbody id="tbodyCountry">
                         
                                </tbody>
                              </table>
                            </div>
                 </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="ScheduleModal" tabindex="-1" role="dialog" aria-labelledby="formModal" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content" style="padding: 0px 20px;">
                <div class="modal-header bg-dark row m-0">
                    <h2 class="modal-title" style="display:inline-block;" id="formModal">Delivery Schedule</h2>
                    <button type="button" class="btn-danger" style="float:right;" onclick="closeModal('ScheduleModal')">X
                    </button>
                </div>
                <div class="modal-body overflow-auto">
                    <div class="row m-0">

                        <div class="form-group col-12 col-md-12">
                            <label for="truckID">Select Trucking ID</label>
                            <input type="text"  id="truckID" class="form-control" disabled/>
                            
                        </div>
                    </div>
                    <div class="row m-0">

                        <div class="form-group col-12 col-md-6">
                            <label for="origin">Origin</label>
                            <input type="hidden" id="type" />

                            <select id="Origin" class="form-control">
                                <option value="">-- Please Select --</option>
                                <option value="BOG">BOG</option>
                                <option value="AMS">AMS</option>
                                <option value="KGF">KGF</option>
                                <option value="NQZ">NQZ</option>
                                <option value="ALA">ALA</option>
                            </select>

                        </div>
                        <div class="form-group col-12 col-md-6">
                            <label for="destination">Destination</label>
                            <select id="destination" class="form-control">
                                <option value="">-- Please Select --</option>
                                <option value="BOG">BOG</option>
                                <option value="AMS">AMS</option>
                                <option value="KGF">KGF</option>
                                <option value="NQZ">NQZ</option>
                                <option value="ALA">ALA</option>
                            </select>

                        </div>

                    </div>
                    <div class="row m-0">

                        <div class="form-group col-12 col-md-6">
                            <label for="carrier">Carrier</label>
                            <select id="carrier" class="form-control">
                                <option value="">-- Please Select --</option>
                                <option value="CEVA L">CEVA L</option>
                                <option value="FLOWERCARGO">FLOWERCARGO</option>
                            </select>

                        </div>
                        <div class="form-group col-12 col-md-6">
                            <label for="carrierType">Carrier Type</label>
                            <select id="carrierType" class="form-control">
                                <option value="">-- Please Select --</option>
                                <option value="Truck">Truck</option>
                                <option value="Flight">Flight</option>
                            </select>

                        </div>

                    </div>

                    <div class="row m-0">

                        <div class="form-group col-12 col-md-6">
                            <label for="startDay">Start Day</label>
                            <select id="startDay" class="form-control">
                                <option Value="1">1 [Monday]</option>
                                <option Value="2">2 [Tuesday]</option>
                                <option Value="3">3 [Wednesday]</option>
                                <option Value="4">4 [Thursday]</option>
                                <option Value="5">5 [Friday]</option>
                                <option Value="6">6 [Saturday]</option>
                                <option Value="7">7 [Sunday]</option>
                            </select>

                        </div>
                        <div class="form-group col-12 col-md-6">
                            <label for="endDay">End Day</label>
                            <select id="endDay" class="form-control">
                                <option Value="1">1 [Monday]</option>
                                <option Value="2">2 [Tuesday]</option>
                                <option Value="3">3 [Wednesday]</option>
                                <option Value="4">4 [Thursday]</option>
                                <option Value="5">5 [Friday]</option>
                                <option Value="6">6 [Saturday]</option>
                                <option Value="7">7 [Sunday]</option>
                            </select>

                        </div>

                    </div>
                    <input type="hidden" id="txtScheduleID" />
                    <div class="row m-0">

                        <div class="form-group col-12 col-md-6">
                            <label for="totalDay">Total Days</label>
                            <select id="totalDay" class="form-control">
                                <option Value="1">1</option>
                                    <option Value="2">2</option>
                                    <option Value="3">3</option>
                                    <option Value="4">4</option>
                                    <option Value="5">5</option>
                                    <option Value="6">6</option>
                                    <option Value="7">7</option>
                                    <option Value="8">8</option>
                                    <option Value="9">9</option>
                                    <option Value="10">10</option>
                                    <option Value="11">11</option>
                                    <option Value="12">12</option>
                                    <option Value="13">13</option>
                                    <option Value="14">14</option>
                                    <option Value="15">15</option>
                                    <option Value="16">16</option>
                                    <option Value="17">17</option>
                                    <option Value="18">18</option>
                                    <option Value="19">19</option>
                                    <option Value="20">20</option>
                                    <option Value="21">21</option>
                                    <option Value="22">22</option>
                                    <option Value="23">23</option>
                                    <option Value="24">24</option>
                                    <option Value="25">25</option>
                                    <option Value="26">26</option>
                                    <option Value="27">27</option>
                                    <option Value="28">28</option>
                                    <option Value="29">29</option>
                                    <option Value="30">30</option>
                                    <option Value="31">31</option>
                                    <option Value="32">32</option>
                                    <option Value="33">33</option>
                                    <option Value="34">34</option>
                                    <option Value="35">35</option>
                                    <option Value="36">36</option>
                                    <option Value="37">37</option>
                                    <option Value="38">38</option>
                                    <option Value="39">39</option>
                                    <option Value="40">40</option>
                                    <option Value="41">41</option>
                                    <option Value="42">42</option>
                                    <option Value="43">43</option>
                                    <option Value="44">44</option>
                                    <option Value="45">45</option>
                                    <option Value="46">46</option>
                                    <option Value="47">47</option>
                                    <option Value="48">48</option>
                                    <option Value="49">49</option>
                                    <option Value="50">50</option>
                            </select>

                        </div>

                        <div class="form-group col-12 col-md-6">
                            <label for="cutDay">Cut Off Day</label>
                            <select id="cutDay" class="form-control">
                                <option Value="1">1 [Monday]</option>
                                <option Value="2">2 [Tuesday]</option>
                                <option Value="3">3 [Wednesday]</option>
                                <option Value="4">4 [Thursday]</option>
                                <option Value="5">5 [Friday]</option>
                                <option Value="6">6 [Saturday]</option>
                                <option Value="7">7 [Sunday]</option>
                            </select>

                        </div>

                    </div>

                    <div class="row m-0">

                        <div class="form-group col-12 col-md-12">
                            <label for="startDay">Cut off time</label>
                            
                                    <div class="row">
                                        <div class="col-3 col-md-3">
                                            <select id="drpHours" class="form-control">
                                                <option Value="01" selected>01</option>
                                                <option Value="02">02</option>
                                                <option Value="03">03</option>
                                                <option Value="04">04</option>
                                                <option Value="05">05</option>
                                                <option Value="06">06</option>
                                                <option Value="07">07</option>
                                                <option Value="08">08</option>
                                                <option Value="09">09</option>
                                                <option Value="10">10</option>
                                                <option Value="11">11</option>
                                                <option Value="12">12</option>
                                            </select>
                                        </div> 
                                        <div class="col-3 col-md-3">
                                            <select id="drpMinutes" class="form-control">
                                                <option Value="00" selected>00</option>
                                                <option Value="05">05</option>
                                                <option Value="10">10</option>
                                                <option Value="15">15</option>
                                                <option Value="20">20</option>
                                                <option Value="25">25</option>
                                                <option Value="30">30</option>
                                                <option Value="35">35</option>
                                                <option Value="40">40</option>
                                                <option Value="45">45</option>
                                                <option Value="50">50</option>
                                                <option Value="55">55</option>
                                            </select>
                                        </div> 
                                        <div class="col-3 col-md-3">
                                            <select id="drpAMPM" class="form-control">
                                                <option Value="AM">AM</option>
                                                <option Value="PM">PM</option>
                                            </select>
                                        </div> 
                                        <div class="col-3 col-md-3">
                                            <select id="drpTimeZone" class="form-control">
                                                <option Value="CST">CST</option>
                                                <option Value="EST">EST</option>
                                                <option Value="MST">MST</option>
                                                <option Value="PST">PST</option>
                                            </select>
                                        </div> 
                                  </div> 

                        </div>

                    </div>

                    <div class="row m-0">

                        <div class="form-group col-12 col-md-6">
                            <label for="charges">Charges</label>
                            <input type="text" id="charges" class="form-control" />

                        </div>
                        <div class="form-group col-12 col-md-6">
                            <label for="step">Step</label>
                            <select id="step" class="form-control">
                                <option Value="1">1</option>
                                    <option Value="2">2</option>
                                    <option Value="3">3</option>
                                    <option Value="4">4</option>
                                    <option Value="5">5</option>
                                    <option Value="6">6</option>
                                    <option Value="7">7</option>
                                    <option Value="8">8</option>
                                    <option Value="9">9</option>
                                    <option Value="10">10</option>
                                    <option Value="11">11</option>
                                    <option Value="12">12</option>
                                    <option Value="13">13</option>
                                    <option Value="14">14</option>
                                    <option Value="15">15</option>
                                    <option Value="16">16</option>
                                    <option Value="17">17</option>
                                    <option Value="18">18</option>
                                    <option Value="19">19</option>
                                    <option Value="20">20</option>
                            </select>

                        </div>

                    </div>

                    <div class="text-right " style="margin-top:1.5rem;">
                        <button type="button" class="btn btn-primary" onclick="insertData()">Submit</button>
                        <button type="button" class="btn btn-light" onclick="closeModal('ScheduleModal')" >Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="ConfirmModal" tabindex="-1" role="dialog" aria-labelledby="ConModal" style="display: none;" aria-hidden="true">
                <div class="modal-dialog " role="document">
                    <div class="modal-content">
                        <div class="modal-header bg-dark">
                    <h2 class="modal-title" style="display:inline-block;">Confirm Delete</h2>
                    <button type="button" class="btn-danger" style="float:right;" onclick="closeModal('ConfirmModal')">X
                    </button>
                </div>
                        <div class="modal-body overflow-auto">
                            <div class="">
                                <input type="hidden" id="schlID" />
                                <div class="col-12 col-md-12">
                                    <div class="row">
                                        <div class="form-group col-12">
                                            <label id="confirmDelete"> Are you sure, you want to delete? </label>
                                            <%--<input id="OccasionDesc" type="text" class="form-control " >--%>
                                        </div>
                                    </div>
                                </div>
                                <div class=" text-right mt-3">
                                    <button type="button" class="btn btn-primary" onclick="DeleteData()">Yes</button>
                                    <button type="button" class="btn btn-light" onclick="closeModal('ConfirmModal')">No</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>
    <script>  
        var CompanyID = "<%=Session("CompanyID")%>";
        var DivisionID = "<%=Session("DivisionID")%>";
        var DepartmentID = "<%=Session("DepartmentID")%>";
        var ScheId = "<%=ScheduleID%>";
         
        $('#Dashboard').addClass('active');

       loadHeadData(ScheId);



        function loadHeadData(truckID) {
            if (truckID) {
                $.ajax({
                    method: 'GET',
                    url: `https://secureapps.quickflora.com/V2/api/GetTruckingSchedules?CompanyID=<%=Session("CompanyID")%>&DivisionID=<%=Session("DivisionID")%>&DepartmentID=<%=Session("DepartmentID")%>&ScheduleID=${truckID}`,
                    dataType: 'json',
                    error: function () {

                    },
                    success: function (response) {
                        if (response.length) {
                            $("#ScheduleID").text(response[0].ScheduleID);
                            $("#mainID").val(response[0].ScheduleID);
                            $("#TxtOrigin").text(response[0].OriginName);
                            $("#ShipMethodDescription").text(response[0].ShipMethodDescription);
                            $("#LocationName").text(response[0].LocationName);
                            $("#TruckingDay").text(response[0].TruckingDay);
                            $("#ArrivalDay").text(response[0].ArrivalDay);

                        }
                        else {

                            alert('No data Found');

                        }

                    }

                });
            } else {
                alert("Please Provide Schedule ID");
            }
        }

        $(document).ready(function ()
        {
           loaddata();

        });
        function loadStart() {
            //$('#loader').waitMe({
            //    effect: 'win8_linear',
            //    text: '',
            //    bg: 'rgba(255,255,255,0.7)',
            //    color: '#036A37',
            //    waitTime: -1,
            //    textPos: 'vertical',
            //    onClose: function () { }
            //});
        }
        function loadStop() {
            //$('#loader').waitMe('hide');
        }

        function loaddata() {
            var truck = ScheId;
            loadStart();
            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/TruckScheduleDetail?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&TruckSchedule=${truck}`,
                dataType: 'json',
                error: function () {
                   
                    applyDatatable('');
                },
                success: function (response)
                {
                    if (response.length && response != "Failed")
                    {
                        buildTable(response);
                    }
                    else
                    {
                        applyDatatable('');
                        
                    }
                    
                }

            });
        }
        //<button type="button" class="btn btn-primary btn-sm" onclick="addModal('ScheduleModal')" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit"><i class="fas fa-plus"></i></button>
        function buildTable(JsonData) {
            var list = [];
            JsonData.forEach(record => {
                list.push([
                    
                   
                    record.ScheduleStep,
                    record.RouteOrigin,
                    record.RouteDestination,
                    record.Carrier,
                    record.CarrierType,
                    record.StartDayText,
                    record.EndDayText,
                    record.ScheduleTodalDays,
                    record.DayCutoffText,
                    record.CutOffTime,
                    parseFloat(record.CarrierFee).toFixed(2),
                    `<button type="button" class="btn btn-primary btn-sm" onclick="editData('${record.ScheduleID}','ScheduleModal')"><i class="fa fa-pencil" aria-hidden="true"></i></button>
                    <button type="button" class="btn btn-danger btn-sm" onclick="delData('${record.ScheduleID}')"><i class="fa fa-trash-o" aria-hidden="true"></i></button>`

                ]);
            });

            applyDatatable(list);

        }
        //DelModal
        function applyDatatable(tableData) {
            loadStop();
            if ($.fn.DataTable.isDataTable("#table-Country")) {
                $("#table-Country").DataTable().destroy();
            }

            $('#table-Country').DataTable({
                dom: 'frtBlip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    {
                        extend: 'pdfHtml5',
                        title: 'Schedule',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        text: 'PDF',
                        titleAttr: 'PDF'
                    },
                ],
                title: 'Schedule',
                data: tableData

            });

        }
        //BoxSzie, DeliveryMethod, DeliveryFee
        function insertData()
        {
            var type = $("#type").val();
            if (type == "") {
                if (validateFields()) {
                    
                    var formData = {
                        'CompanyID': CompanyID,
                        'DivisionID': DivisionID,
                        'DepartmentID': DepartmentID,
                        'TruckingScheduleID': $("#truckID").val(),
                        'RouteOrigin': $('#Origin').val(),            
                        'RouteDestination': $('#destination').val(),
                        'Carrier': $('#carrier').val(), 
                        'CarrierType': $('#carrierType').val(), 
                        'ScheduleStartDay': $('#startDay').val(), 
                        'ScheduleEndDay': $('#endDay').val(), 
                        'ScheduleTodalDays': $('#totalDay').val(), 
                        'DayCutoff': $('#cutDay').val(), 
                        'CutOffTime': $('#drpHours').val()+':'+$('#drpMinutes').val()+' '+$('#drpAMPM').val() + ' ' +$('#drpTimeZone').val(), 
                        'Hours': $('#drpHours').val(), 
                        'Minutes': $('#drpMinutes').val(), 
                        'AMPM': $('#drpAMPM').val(), 
                        'TimeZone': $('#drpTimeZone').val(), 
                        'CarrierFee': $('#charges').val(), 
                        'ScheduleStep': $('#step').val(), 
                    }
                    $.ajax({
                        method: 'Post',
                        url: 'https://secureapps.quickflora.com/V2/api/TruckScheduleDetail',
                        dataType: 'json',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        data: JSON.stringify(formData),
                        success: function (response) {
                            if (response == "Successful") {
                                closeModal('ScheduleModal');
                                loaddata();
                                //toastr.success("Added Successfully");

                            } else {
                                alert("Something Went Wrong!");

                            }
                            loadStop();
                        }
                    });
                }
            }

            //======= UpDate ===
            if (type == "update") {
                var formData = {
                    'CompanyID': CompanyID,
                        'DivisionID': DivisionID,
                        'DepartmentID': DepartmentID,
                        'ScheduleID': $("#txtScheduleID").val(),
                        'TruckingScheduleID': $("#truckID").val(),
                        'RouteOrigin': $('#Origin').val(),            
                        'RouteDestination': $('#destination').val(),
                        'Carrier': $('#carrier').val(), 
                        'CarrierType': $('#carrierType').val(), 
                        'ScheduleStartDay': $('#startDay').val(), 
                        'ScheduleEndDay': $('#endDay').val(), 
                        'ScheduleTodalDays': $('#totalDay').val(), 
                        'DayCutoff': $('#cutDay').val(), 
                        'CutOffTime': $('#drpHours').val()+':'+$('#drpMinutes').val()+' '+$('#drpAMPM').val() + ' ' +$('#drpTimeZone').val(), 
                        'Hours': $('#drpHours').val(), 
                        'Minutes': $('#drpMinutes').val(), 
                        'AMPM': $('#drpAMPM').val(), 
                        'TimeZone': $('#drpTimeZone').val(), 
                        'CarrierFee': $('#charges').val(), 
                        'ScheduleStep': $('#step').val(), 
                }

                $.ajax({
                    method: 'Put',
                    url: `https://secureapps.quickflora.com/V2/api/TruckScheduleDetail`,
                    dataType: 'json',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    data: JSON.stringify(formData),
                    success: function (response) {
                        if (response == "Successful") {
                            closeModal('ScheduleModal');
                            loaddata();
                            
                            //toastr.success("Updated Successfully");

                        } else {
                            alert("Something Went Wrong!");

                        }
                        //loadStop();
                    }
                });

            }
        }
        
        //BoxSzie, DeliveryMethod, DeliveryFee
        function editData(ID, id)
        {

            $.ajax({
                method: 'GET',
                url: `https://secureapps.quickflora.com/V2/api/TruckScheduleDetail?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&ID=${ID}`,
                    dataType: 'json',
                    success: function (response) {
                        //alert(response);
                        if (response == "Failed") {
                           // toastr.error('Something Went Wrong!!');
                        }
                        else {
                            //$("#Accept option[value=" + 0 + "]").attr("selected", "selected");
                            $('#type').val("update");
                            $("#txtScheduleID").val(response[0].ScheduleID)
                            $("#truckID").val(response[0].TruckingScheduleID);
                            $('#Origin').val(response[0].RouteOrigin);            
                            $('#destination').val(response[0].RouteDestination);
                            $('#carrier').val(response[0].Carrier); 
                            $('#carrierType').val(response[0].CarrierType); 
                            $('#startDay').val(response[0].ScheduleStartDay); 
                            $('#endDay').val(response[0].ScheduleEndDay); 
                            $('#totalDay').val(response[0].ScheduleTodalDays); 
                            $('#cutDay').val(response[0].DayCutoff); 
                            $('#drpHours').val(response[0].Hours); 
                            $('#drpMinutes').val(response[0].Minutes); 
                            $('#drpAMPM').val(response[0].AMPM); 
                            $('#drpTimeZone').val(response[0].TimeZone); 
                            $('#charges').val(response[0].CarrierFee); 
                            $('#step').val(response[0].ScheduleStep); 
                            
                            
                            $('#' + id).modal('show');

                        }
                    }

                });


         }

        function delData(id) {
            $('#schlID').val(id);
            $("#ConfirmModal").modal("show");
        }

        function DeleteData() {
            var OrderLineid = $('#schlID').val();
            $.ajax({
                method: 'Delete',
                url: `https://secureapps.quickflora.com/V2/api/TruckScheduleDetail?CompanyID=${CompanyID}&DivisionID=${DivisionID}&DepartmentID=${DepartmentID}&id=${OrderLineid}`,
                dataType: 'json',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                success: function (response) {
                    if (response == "Successful") {
                        $('#ConfirmModal').modal('hide');
                      //  toastr.success('Deleted Successfully!!');
                        loaddata();
                        
                    } else {
                      //  toastr.error('Something Went Wrong!!')
                    }
                }

            });
        }
       

        function validateFields() {
            var count = 0;
            $('.valid-field').each(function (i, el) {
                if (this.value == '') {
                    $(this).next('span').text("This Field Can't be Empty.").css("color", "red");
                    count++;
                } else {
                    $(this).next('span').text('');
                }
            });
            if (count == 0) {
                return true;
            } else {
                return false;
            }
        }
        // BoxSzie,DeliveryMethod,DeliveryFee
        function addModal(id) {
            var dt = $("#mainID").val();
            console.log(dt);
            $("#truckID").val(dt);
            $('.not-valid').text('');
            $("#type").val('');
            $('#Origin').val('');            
            $('#destination').val('');
            $('#carrier').val(''); 
            $('#carrierType').val(''); 
            $('#startDay').val('1'); 
            $('#endDay').val('1'); 
            $('#totalDay').val('1'); 
            $('#cutDay').val('1'); 
            $('#drpHours').val('01'); 
            $('#drpMinutes').val('00'); 
            $('#drpAMPM').val('AM'); 
            $('#drpTimeZone').val('CST'); 
            $('#charges').val(''); 
            $('#step').val('1'); 
            $('#' + id).modal('show');
        }
        function closeModal(id) {
            var dt = $("#mainID").val();
            $("#truckID").val(dt);
            $('.not-valid').text('');
            $("#type").val('');
            $('#Origin').val('');            
            $('#destination').val('');
            $('#carrier').val(''); 
            $('#carrierType').val(''); 
            $('#startDay').val('1'); 
            $('#endDay').val('1'); 
            $('#totalDay').val('1'); 
            $('#cutDay').val('1'); 
            $('#drpHours').val('01'); 
            $('#drpMinutes').val('00'); 
            $('#drpAMPM').val('AM'); 
            $('#drpTimeZone').val('CST'); 
            $('#charges').val(''); 
            $('#step').val('1'); 
            $('#' + id).modal('hide');
        }

       

    </script>


</asp:Content>

