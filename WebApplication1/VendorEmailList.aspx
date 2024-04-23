<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="VendorEmailList.aspx.vb" Inherits="VendorEmailList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
        
<!-- BEGIN PAGE BAR -->
                        <div class="page-bar">
                             
                                      <%=CustomerName %> Emails List 
                                
                             <%--<div class="page-toolbar">
                                <div id="dashboard-report-range" class="pull-right tooltips btn btn-sm" data-container="body" data-placement="bottom" data-original-title="Create New Customer">
                                    <i class="fa fa-plus"></i>&nbsp;
                                   <a href="CustomerInformationDetails.aspx" class="btn sbold green" >New Customer</a>
                                </div>
                            </div>--%>
                        </div>
                        <!-- END PAGE BAR -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

    <!-- BEGIN PAGE HEADER-->
			<div class="row">
				<div class="col-md-12">
					<!-- BEGIN PAGE TITLE & BREADCRUMB-->
					 
					<ul class="page-breadcrumb breadcrumb">
						 
						<li>
							<i class="fa fa-home"></i>
							<a href="Home.aspx">Home</a>
							<i class="fa fa-angle-right"></i>
						</li>
						<li>
							<a href="VendorList.aspx">Vendors</a>
							<i class="fa fa-angle-right"></i>
						</li>
						<li>
							<a href="Javascript:;"> Vendors Email List</a>
						</li>
					</ul>
					<!-- END PAGE TITLE & BREADCRUMB-->
				</div>
			</div>
			<!-- END PAGE HEADER-->

    <div class="row">
				<div class="col-md-6">
					<!-- BEGIN ALERTS PORTLET-->
					<div class="portlet purple box">
						<div class="portlet-title">
							<div class="caption">
								<i class="fa fa-cogs"></i>Add New Email 
							</div>
							<div class="tools">
								<a href="javascript:;" class="collapse"></a>
								<a href="#portlet-config" data-toggle="modal" class="config"></a>
								<a href="javascript:;" class="reload"></a>
								<a href="javascript:;" class="remove"></a>
							</div>
						</div>
							<div class="portlet-body">
							  
                              <div class="alert alert-success">
                                
										<span class="help-block">
											Vendor ID
										</span>
										 <input type="text" value='<%=CustomerID %>' class="form-control" readonly=readonly id="txtCustomerID" placeholder="VendorID"/>
										 
								 
							</div>

                           
							<div class="alert alert-info">
                                
										<span class="help-block">
											Email
										</span>
										 <input type="text" class="form-control" id="txtemail" placeholder="Email"/>
										 
								 
							</div>
                            
							<%--<div style="text-align:left;" class="alert alert-info">
                                <input id="chkorder" class="form-control"  style="width:34px;"  type="checkbox" />
										 
										<span class="help-block">
											Send Order email copy
										</span>
										  
							</div>--%>
                            
							<%--<div style="text-align:left;" class="alert alert-info">
                              
                                <input id="chkdeliveryemail" class="form-control"   style="width:34px;"  type="checkbox" />
										
										<span class="help-block">
											Send Order Delivery confirmation email copy
										</span>
								 
							</div>
                            --%>
							<div class="alert alert-info">
                                
                                <a href="Javascript:;" id="addbh" class="btn green">Add</a>
							</div>
                          
							 
						</div>
					</div>
					<!-- END ALERTS PORTLET-->
				</div>
				<div class="col-md-6">
					<!-- BEGIN ALERTS PORTLET-->
					<div class="portlet yellow box">
						<div class="portlet-title">
							<div class="caption">
								<i class="fa fa-cogs"></i>Email List For <b><%=CustomerName %></b> 
							</div>
							<div class="tools">
								<a href="javascript:;" class="collapse"></a>
								<a href="#portlet-config" data-toggle="modal" class="config"></a>
								<a href="javascript:;" class="reload"></a>
								<a href="javascript:;" class="remove"></a>
							</div>
						</div>
						<div class="portlet-body">
							 
                             <div class="table-responsive">
								<table class="table table-striped table-bordered table-advance table-hover">
								<thead>
								<tr>
									<th>
										<i class="fa fa-briefcase"></i>  Email
									</th>
									<%--<th class="hidden-xs">
										<i class="fa fa-user"></i>  Order
									</th>
                                    <th class="hidden-xs">
										<i class="fa fa-user"></i>  Order Delivery
									</th>--%>
									 
									<th>
                                    Delete
									</th>
								</tr>
								</thead>
								<tbody id="tbodyrs">
								
								 
								</tbody>
								</table>
							</div>

						</div>
					</div>
					<!-- END ALERTS PORTLET-->
				</div>
			</div>



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <!-- END PAGE LEVEL SCRIPTS -->
<script>

     function validateEmail($email) {
          var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
          return emailReg.test( $email );
    }

    function loaddata() {
        var vendor = $('#txtCustomerID').val();
        $.getJSON(`https://secureapps.quickflora.com/V2/api/VendorEmails?CompanyID=<%=Session("CompanyID")%>&DivisionID=<%=Session("DivisionID")%>&DepartmentID=<%=Session("DepartmentID")%>&vendorID=${vendor}`, function (data) {
            binddata(data);
        });
    }

    jQuery(document).ready(function () {
        // initiate layout and plugins
        
        loaddata();


        // Add button click Event start from here
        $("#addbh").click(function () {

            //Validate email to proceed
            if ($('#txtemail').val() == "") {
                alert('Please enter email.');
                $('#txtemail').css('border-color', 'red');
                return;
            }
            else {
                $('#txtemail').css('border-color', '');
            }

            if (!validateEmail($('#txtemail').val())) {
              alert('Please eneter a vaild email.');
                $('#txtemail').css('border-color', 'red');
                return;
        }

            //Prepae formdata to submit
            var formData = {
                "CompanyID" : "<%=Session("CompanyID")%>",
                "DivisionID" : "<%=Session("DivisionID")%>",
                "DepartmentID" : "<%=Session("DepartmentID")%>",
              "VendorID" : $("#txtCustomerID").val(),
              "Email": $("#txtemail").val()
            };


            $.ajax({
                  type: "POST",
                  url: "https://secureapps.quickflora.com/V2/api/VendorEmails",
                  data: formData,
                  dataType: "json",
                  encode: true
                }).done(function (data) {
                    //bind back the gird of emails on page
                    loaddata();
                    $('#txtemail').val('');
                });
                 
          });

 

    });
    //end of event add


    function DeleteItem(id) {
        if (confirm("Are you sure, you want to delete this email?")) {

        } else {
            return;
        }
        $('#tbodyrs').html('');

        $.getJSON(`https://secureapps.quickflora.com/V2/api/VendorEmails?CompanyID=<%=Session("CompanyID")%>&DivisionID=<%=Session("DivisionID")%>&DepartmentID=<%=Session("DepartmentID")%>&emailID=${id}&type=del`, function (data) {
            loaddata();
        });
    }

    function binddata(data) {
        var html = '';
        //[Email],[ActiveForOrder],[ActiveForOrderDelivery],[EmailListID] 
        $.each(data, function (key, value) {
            html += '<tr> <td class="highlight">  <div class="success"> </div><a href="#">';
            html += value.Email;
            html += '</a> </td> ';
            html += '<td>  <a href="Javascript:;" onclick="Javascript:DeleteItem(' + value.EmailListID + ');" class="btn btn-danger btn-xs"><i class="fa fa-trash-o"></i> Delete</a></td></tr>';
        });
        $('#tbodyrs').html(html);
    }


</script>
<!-- END JAVASCRIPTS -->
</asp:Content>

