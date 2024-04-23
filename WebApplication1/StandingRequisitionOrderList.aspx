<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="StandingRequisitionOrderList.aspx.vb" Inherits="StandingRequisitionOrderList" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    <link rel="stylesheet" type="text/css" href="assets/components.css" />

<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>
  

        <style>
        li.c-active > span, .c-content-pagination.c-theme > li.c-active > a {
            border-color: #cf4647;
            background: #cf4647;
            color: #fff;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Standing Orders List
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">


    <ajax:AjaxPanel ID="AjaxPanel101" runat="server">
        <asp:Panel ID="pnlgrid" runat="server" Visible="true">
        <!-- BEGIN PORTLET 1st Block-->
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    &nbsp;Search
                </div>
                <div class="tools">
                    <a href="javascript:;" class="collapse"></a>
                </div>
            </div>

            <div class="portlet-body">
                 <div class="row">
                        <div class="col-md-8">
                          <div class="row form-group">
                                <label class="control-label col-md-3">Requisition Range</label>
                                    <div class="col-md-9">
                                         <div class="row">
                                             <div class="col-md-5">
                                                    <div class='input-group date' id='datetimepicker1'>
                                                        <asp:TextBox  CssClass="form-control input-sm"  placeholder="From"      runat="server" ID="txtDateFrom"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                                        </div> 
                                                    <span class="col-md-2">
                                                    to </span>
                                         <div class="col-md-5">     
                                                   <div class='input-group date' id='datetimepicker2'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="To"     runat="server" ID="txtDateTo"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                                           </div> 
                                      </div>                          
                                    </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="row form-group">
                                 
                                <div class="col-md-12">
                                    
                                    <asp:RadioButton ID="rdall" GroupName="date" Text="&nbsp;All" Checked="true" AutoPostBack="true" runat="server" />
                                    &nbsp;&nbsp;<asp:RadioButton GroupName="date" AutoPostBack="true" ID="rdselected" Text="&nbsp;Only Selected" runat="server" />
                                    &nbsp;&nbsp;<asp:CheckBox ID="chkincludeStanding" Visible="false" AutoPostBack="true" Text="&nbsp;Include Standing Requests" Checked="true" runat="server" />
                                        <asp:Button ID="btnSearch" Visible="false"  CssClass="btn btn-success btn-xs" runat="server" Text="SEARCH" />
                                </div>
                                            
                                 
                            </div>
                        </div>
                    </div>


                          <div class="row">
                                    <div class="col-xs-4 col-md-2">
                                    		<div class="form-group">
                                                 
                                                <asp:DropDownList ID="drpFieldName"  CssClass="form-control input-sm select2me" runat="server">
                                                        <asp:ListItem  Value="OrderNo">Request Number</asp:ListItem>                                                                                                
                                                 </asp:DropDownList>	

                                            </div>
                                     </div>
                                     <div class="col-xs-3 col-md-1">
                                     		<div class="form-group">
                                             
                                            <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Value="Like">Like</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="=">=</asp:ListItem>
                                            </asp:DropDownList>	

                                            </div>
                                     </div>
                                     <div class="col-xs-5 col-md-3">
                                     		<div class="form-group">
                                             
                                                 <asp:TextBox ID="txtSearchExpression"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                          	</div>
                                    </div>
                                    <div class="col-xs-12 col-md-6">
                                    	<div class="form-group">
                                         
                                            <asp:Button ID="btnSearchExpression" CssClass="btn btn-success btn-xs" runat="server" Text="SEARCH" />
                &nbsp;&nbsp;<asp:CheckBox ID="chkIncludeCanceled" Visible="false" AutoPostBack="true" Text="&nbsp;Include Canceled" runat="server" />
                &nbsp;&nbsp;<asp:CheckBox ID="chkIncludeReceived" Visible="false" AutoPostBack="true" Text="&nbsp;Include Received" runat="server" /> 
                &nbsp;&nbsp;<asp:CheckBox   AutoPostBack="true"  Visible="false" ID="rdOnlyCanceled" Text="&nbsp;Only Canceled" runat="server" />
                &nbsp;&nbsp;<asp:CheckBox  AutoPostBack="true" Visible="false" ID="rdOnlyReceived" Text="&nbsp;Only Received" runat="server" /> 
                                        </div>
                                        <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label>
                                </div>
                                </div>



                <div class="note note-success margin-bottom-0">

                    <div class="row">
                        <div class="col-xs-4">
                            Type:
                             <asp:DropDownList ID="drpType" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                 <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                                 <asp:ListItem Text="Distribute" Value="Distribute"></asp:ListItem>
                                 <asp:ListItem Text="Hardgoods" Value="Hardgoods"></asp:ListItem>
                                 <asp:ListItem Text="Holiday" Value="Holiday"></asp:ListItem>
                                 <asp:ListItem Text="Flowers" Value="Flowers"></asp:ListItem>
                                 <asp:ListItem Text="Special-Cal" Value="Special-Cal"></asp:ListItem>
                                 <asp:ListItem Text="Special-Hol" Value="Special-Hol"></asp:ListItem>
                                 <asp:ListItem Text="Standing Auto" Value="Standing Auto"></asp:ListItem>                                						 
                                 <asp:ListItem Text="Standing Order" Value="Stg Order"></asp:ListItem>
                                 <asp:ListItem Text="Wedding" Value="Wedding"></asp:ListItem>
                                 <asp:ListItem Text="Wedding-Cal" Value="Wedding-Cal"></asp:ListItem>
                             </asp:DropDownList>
                        </div>
                        <div class="col-xs-4">
                            Location 
                            <asp:DropDownList ID="cmblocationid" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Location--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                         
                        <div class="col-xs-4">
                            Status
                            <asp:DropDownList ID="drpStatus" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Buying Completed" Value="Buying Completed"></asp:ListItem>
                                <asp:ListItem Text="Buying In Process" Value="Buying In Process"></asp:ListItem>                                
                                <asp:ListItem Text="Entry Completed" Value="Entry Completed"></asp:ListItem>
                                <asp:ListItem Text="Entry In Process" Value="Entry In Process"></asp:ListItem>
                                <asp:ListItem Text="Received" Value="Received"></asp:ListItem>
                                <asp:ListItem Text="Stg Completed" Value="Stg Completed"></asp:ListItem>
                                <asp:ListItem Text="Stg In Process" Value="Stg In Process"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                     
                </div>


            </div>
        </div>
        <!-- END PORTLET-->
 

                <div  style='z-index:90;height:100%' class="table-responsive">

                <div class="row">
                    <div style="text-align:left; padding-bottom:10px;" class="col-xs-12">
                      <a href="#" onClick="window.open('ExportExcelROL.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="75" height="75" border="0" /></a> &nbsp;  <asp:Label ID="lblmsg" runat="server"  Text=""></asp:Label>
                    </div> 
                </div> 

                <asp:GridView ID="OrderHeaderGrid" ShowHeaderWhenEmpty="true" AllowSorting="True" runat="server" DataKeyNames="OrderNo" 
                    CssClass="table table-bordered table-striped table-condensed flip-content" AutoGenerateColumns="false" AllowPaging="false"   >
                    <Columns>
                      <asp:TemplateField HeaderText="View" Visible="false" ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a target="_blank" class="btn btn-warning btn-xs tooltips" href='https://reports.quickflora.com/reports/scripts/ROReport.aspx?CompanyID=<%=CompanyID %>&DivisionID=<%=DivisionID %>&DepartmentID=<%=DepartmentID %>&PurchaseNumber=<%# Eval("OrderNo")%>'>
                                <i class="fa fa-search"></i>
                                
                                </a>
                        </ItemTemplate>
                    </asp:TemplateField> 

                        <asp:TemplateField HeaderText="Edit" SortExpression="OrderNo" >
                            <ItemTemplate>
                                 <asp:Panel ID="Panel1" runat="server"> <a href='StandingRequisitionOrder.aspx?OrderNo=<%#Eval("OrderNo")%>' ><%#Eval("OrderNo")%></a></asp:Panel> 
                                 <asp:Label ID="lblOrderNumber" Visible="false"  Text='<%#Eval("OrderNo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>

                         <asp:TemplateField Visible="false" HeaderText="Cancel">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgCancel" CommandArgument='<%# Eval("OrderNo") %>'  
                                        OnClientClick="return confirm('Warning! You are about to cancel RO. \n If you wish to continue, press OK.');"
                                        CommandName="CancelOrder" ToolTip="Cancel" ImageUrl="~/Images/un-post.gif"
                                        runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="25px" />
                            </asp:TemplateField>

                        <asp:TemplateField Visible="false"  HeaderText="PRN">

                    <ItemTemplate>
                        <asp:ImageButton ID="imgWorkTicketPrint" 
                            CommandName="ROPrint" CommandArgument='<%# Eval("OrderNo") %>' ToolTip="Print RO"
                            ImageUrl="~/images/print.gif" runat="server" />
                    </ItemTemplate>
                    
                </asp:TemplateField>
                         

                        <asp:TemplateField HeaderText="Status" SortExpression="Status" >
                            <ItemTemplate>
                                <asp:Label  ID="txtStatus" Text='<%#Eval("Status")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Type" SortExpression="Type" >
                            <ItemTemplate>
                                <asp:Label  ID="txtType" Text='<%#Eval("Type")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total" SortExpression="TotalAmount" > 
                            <ItemTemplate>
                                $<%#String.Format("{0:N2}", Eval("TotalAmount"))%>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ship From" SortExpression="InventoryOrigin" >
                            <ItemTemplate>
                                <asp:Label  ID="txtInventoryOrigin" Text='<%#Eval("InventoryOrigin")%>' runat="server"></asp:Label>
                            </ItemTemplate>                             
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Ship To" SortExpression="Location" >
                            <ItemTemplate>
                                <asp:Label  ID="txtLocation" Text='<%#Eval("Location")%>' runat="server"></asp:Label>
                            </ItemTemplate>                             
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Shiping Method" SortExpression="ShipMethodID" >
                            <ItemTemplate>
                                <asp:Label  ID="txtShipMethodID" Text='<%#Eval("ShipMethodID")%>' runat="server"></asp:Label>
                            </ItemTemplate>                             
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Ship Date" SortExpression="ShipDate" >
                            <ItemTemplate>
                                <%#Eval("ShipDate")%>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Ship Day"  >
                            <ItemTemplate>
                                 <%#CreateDayName(Eval("ShipDay"))%> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        

                        <asp:TemplateField HeaderText="Arrive Date" SortExpression="ArriveDate"   >
                            <ItemTemplate>
                                <%#Eval("ArriveDate")%>
                            </ItemTemplate>
                         </asp:TemplateField>
                         
                         <asp:TemplateField HeaderText="Arrival Day"  >
                            <ItemTemplate>
                                 <%#CreateDayName(Eval("ArrivalDay"))%> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Create on Every"  >
                            <ItemTemplate>
                                 <%#CreateDayName(Eval("CreateDay"))%> 
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Placed Date" SortExpression="OrderPlacedDate" >
                            <ItemTemplate>
                                <%#Eval("OrderPlacedDate")%>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order By"  SortExpression="OrderBy" >
                            <ItemTemplate>
                                <asp:Label  ID="txtOrderBy" Text='<%#Eval("OrderBy")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Remarks"   SortExpression="Remarks"  >
                            <ItemTemplate>
                                <asp:Label  ID="txtRemarks" Text='<%#Eval("Remarks")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                         
                        
                        

                       



                <asp:TemplateField HeaderText="Status"   >
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" Text='' runat="server"></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>

                        <asp:TemplateField Visible="false"  HeaderText="Change History">
                            <ItemTemplate>
                                 <a href='RequisitionOrderHistory.aspx?OrderNumber=<%#Eval("OrderNo")%>' >Change History</a>
                            </ItemTemplate>
                         </asp:TemplateField>
                         
                         
                    </Columns>
                </asp:GridView>
 
 <div class="container"> 
				<div class="row">
                                	
                    <div class="col-md-4">
                        <div class="c-filter">
                                 Show: 
                                    <asp:DropDownList ID="drppagelimit" AutoPostBack="True" runat="server" Width="100px" class="form-control c-square c-theme c-input"  >
                                    <asp:ListItem  Text="25" Value="25" ></asp:ListItem>
                                    <asp:ListItem   Text="50" Value="50" ></asp:ListItem>
                                    <asp:ListItem  Selected="True"  Text="100" Value="100" ></asp:ListItem>
                                    <asp:ListItem  Text="150" Value="150" ></asp:ListItem>
                                    <asp:ListItem  Text="200" Value="200" ></asp:ListItem>
                                    <asp:ListItem  Text="250" Value="250" ></asp:ListItem>
                                    <asp:ListItem  Text="300" Value="300" ></asp:ListItem>
                                                                        <asp:ListItem  Text="350" Value="350" ></asp:ListItem>
                                    <asp:ListItem  Text="400" Value="400" ></asp:ListItem>
                                    <asp:ListItem  Text="450" Value="450" ></asp:ListItem>
                                    <asp:ListItem  Text="500" Value="500" ></asp:ListItem>
                                </asp:DropDownList>
                              
                                            
                            </div>
                    </div>
                                
                    <div class="col-md-8">
                   
                          <ul class="c-content-pagination c-square c-theme pull-right">
                            <li class="c-prev">
                                <a id="linkleft"  onclick="<%=pagingleft%>" href="Javascript:;">
                                    <i class="fa fa-angle-left"></i>
                                </a>
                            </li>

                            <%=paging %>

                            <li class="c-next">
                                <a id="linkright" onclick="<%=pagingright%>" href="Javascript:;">
                                    <i class="fa fa-angle-right"></i>
                                </a>
                            </li>
                        </ul>

 
                    </div> 
                                
                                
                </div> 
  
</div> 

                    </div> 
                <div class="row">
                    <div style="text-align:left; padding-bottom:10px;" class="col-md-12">
                      <a href="#" onClick="window.open('ExportExcelROL.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="75" height="75" border="0" /></a> &nbsp;  <asp:Label ID="Label1" runat="server"  Text=""></asp:Label>
                    </div> 
                </div>

                
                 
        
          <div style="display:none;" >
                <asp:TextBox ID="txtpageno" runat="server"></asp:TextBox>
                <asp:LinkButton ID="btnpageno" Width="0" ToolTip="page no" ImageUrl="~/images/2-sh-stock-in.gif"  runat="server" /> 
            </div>

        <asp:Label ID="lblpagecount" Visible="false" runat="server" Text=""></asp:Label>

</asp:Panel> 

    </ajax:AjaxPanel>
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>
    <script>
        function paging(val) {
            //alert(val);
            document.getElementById("<%=txtpageno.ClientID%>").value = val;
            document.getElementById("<%=btnpageno.ClientID%>").focus();
            document.getElementById("<%=btnpageno.ClientID%>").click();
            //ImgUpdateSearchitems
        }



        jQuery(document).ready(function() {

            Search.init();
        });
    </script>

          <script type="text/javascript" >
              //alert('test');
              doOnLoadNew();
              var myCalendarFrom;
              var myCalendarto;
              function doOnLoadNew() {
                  //alert('test');
                  myCalendarFrom = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtDateFrom"]);
                  //alert(myCalendar);
                  myCalendarFrom.setDateFormat("%m/%d/%Y");
                  myCalendarto = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtDateTo"]);
                  //alert(myCalendar);
                  myCalendarto.setDateFormat("%m/%d/%Y");
              }
       </script>

</asp:Content>

