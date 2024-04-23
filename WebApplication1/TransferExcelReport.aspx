﻿<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="~/MainMaster.master" CodeFile="TransferExcelReport.aspx.vb" Inherits="TransferExcelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    
<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>

<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>

     


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Transfers Export
</asp:Content>
 
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
		<div class="portlet box green">
			<div class="portlet-title">
				<div class="caption">Search Options</div>
				<div class="tools"><a href="javascript:;" class="collapse"></a></div>
			</div>
			<div class="portlet-body"  >
			        <div class="row">   
			            <div class="col-md-3">
			                   <asp:Repeater ID="DataGrid1" runat="server">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" border="0" ImageUrl='<%# PopulateImage(DataBinder.Eval(Container, "DataItem.CompanyLogoUrl")) %> '>
                                    </asp:Image>
                                    <br />
                                    <%# DataBinder.Eval(Container, "DataItem.CompanyName") %>
                                     
                                      
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
			            </div> 
			            <div class="col-md-9">
			          <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="height: 40px" colspan="3" align="left">
                                <asp:Label ID="lbReportdate" runat="server" Text='Transfer Date : ' CssClass="font11_blue_black_padding" />
                                <asp:TextBox ID="txtDeliveryDate" runat="server" Width="101px"></asp:TextBox>&nbsp;
                                <img id="btnCal1" src="https://secure.quickflora.com/Admin/images/calender.gif" style="cursor: pointer;"  />
                                &nbsp;
                                <asp:Label ID="Label1" runat="server" Visible="false"  Text=' Arrive Date : ' CssClass="font11_blue_black_padding" />
                                <asp:TextBox ID="txtDeliveryDateTO"  Visible="false"  runat="server" Width="101px"></asp:TextBox>&nbsp;
                                                            

                                 &nbsp;&nbsp; 
                                   <asp:RadioButton ID="optbyShipDate" Checked="true" AutoPostBack="true"  Visible="false"  Text="&nbsp;By Ship Date" GroupName="status"   runat="server" />
                                   &nbsp;&nbsp;  
                                   <asp:RadioButton ID="optbyArriveDate" Text="&nbsp;By Arrive Date" Visible="false" AutoPostBack="true"   GroupName="status"   runat="server" />
                                 

                                &nbsp; <asp:Button runat="server" ID="SearchButton"  CssClass="btn btn-success" Text="Search" /> 
                            </td> 
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                &nbsp;
                            </td> 
                        </tr> 
                        <tr>
                            <td align="left" colspan="3">
                                
                                   Transfer From&nbsp;<asp:DropDownList ID="drpTansferFromLocaton" AutoPostBack="true"   runat="server"></asp:DropDownList>
                                &nbsp;&nbsp;
                                Transfer To&nbsp; <asp:DropDownList ID="drpTransferToLocaton" AutoPostBack="true"   runat="server"></asp:DropDownList>
                 
                            </td>
                        </tr>
                    </table>
			            </div> 
			        </div> 
			</div>
		</div> 
    
    <div id="Div1">
    
    
    <div>
         
        <table cellpadding=0 cellspacing=0 style="width:100%;">
            <tr>
                <td style="width:25%; text-align:left;" >Executed By <% =EmployeeID & " on " & Date.Now.Date %> </td>
                <td style="width:25%; text-align:center;" ><h3><u> Transfers Export </u> </h3> </td>
                <td style="width:25%; text-align:center; padding-right:30px;" >&nbsp; <a href="#" onClick="window.open('TransferExcelImport.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="50" height="50" border="0" /></a>  </td>
                <td style="width:25%; text-align:center; padding-right:30px;" >&nbsp; 
                    <asp:ImageButton Width="60" Height="50" ID="btnsendemail" ImageUrl="https://secure.quickflora.com/images/iconemail.png" runat="server" />
                </td>
            </tr>
        </table>
         
           <asp:GridView ID="AutoGrid"    AllowSorting="false"  runat="server" 
               CssClass="table table-bordered table-striped table-condensed flip-content"   
               AutoGenerateColumns="true" AllowPaging="false"   >

                   <Columns>
                  

                </Columns> 

        </asp:GridView> 
            
                 
                   
    </div>
    </div>
   <div   align="center" >
           <br />
            <input  type="button" value="Print To Normal Printer" class="btn btn-success" onclick="JavaScript:printPartOfPage('Div1');">   
            <br />
           
           </div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">


<script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript"  src="assets/admin/pages/scripts/search.js"></script>

<script>
    jQuery(document).ready(function() {

        Search.init();
    });
</script>


<script type="text/javascript">
<!--
    function printPartOfPage(elementId) {



        var printContent = document.getElementById(elementId);
        var windowUrl = 'about:blank';
        var uniqueName = new Date();
        var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');

        printWindow.document.write(printContent.innerHTML);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    }
// -->
</script>


    <script   type="text/javascript">
        doOnLoadNew();
        var myCalendarFrom;
        var myCalendarto;
        function doOnLoadNew() {
            //alert('test');
            myCalendarFrom = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtDeliveryDate"]);
            //alert(myCalendarFrom);
            myCalendarFrom.setDateFormat("%m/%d/%Y");
            myCalendarto = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtDeliveryDateTO"]);
            //alert(myCalendarto);
            myCalendarto.setDateFormat("%m/%d/%Y");
        }
         
    </script>



</asp:Content>
 


