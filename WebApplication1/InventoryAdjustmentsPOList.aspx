<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="InventoryAdjustmentsPOList.aspx.vb" Inherits="InventoryAdjustmentsPOList" %>

<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>
 <%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
	<h3 class="page-title">
			Adjustments Log
			</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

  
 
<script type="text/javascript" language="javascript">
    function checkDate() {


        var criteria = document.getElementById("ctl00__mainContent_drpFieldName").value;
        if (criteria == "OrderShipDate") {
            var dtvalue = document.getElementById("ctl00__mainContent_txtSearchExpression").value;
            //  alert(dtvalue);
            if (!isDate(dtvalue)) {
                alert("Invalid Date");
            }
        }
    }
</script>

 

  
  <ajax:AjaxPanel id="AjaxPanel3" runat="server">
      
      <asp:Panel ID="pnlgrid" runat="server"  Visible="true"  >    
      
      
      <!-- BEGIN PORTLET-->
					<div class="portlet box green">
						<div class="portlet-title">
							<div class="caption">Search Options</div>
							<div class="tools"><a href="javascript:;" class="collapse"></a></div>
						</div>
						<div class="portlet-body" >
                                <div class="row">
                                    <div class="col-md-6">
                                    	<div class="row form-group">
                                        	<label class="control-label col-md-3">Date Range</label>
                                                        <div class="col-md-9">
                                                            <div class="input-group input-sm date-picker input-daterange" data-date="07/03/2015" data-date-format="mm/dd/yyyy">
                                                                <div class="input-icon">
                                                               <table>
                                                                    <tr>
                                                                        <td> <i class="fa fa-calendar"></i></td>
                                                                        <td><asp:TextBox  CssClass="form-control"      runat="server" ID="txtDateFrom"> </asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                                    
                                                                     
                                                                 
                                                                
                                                                </div>
                                                                <span class="input-group-addon">
                                                                to </span>
                                                                <div class="input-icon">
                                                                <table>
                                                                    <tr>
                                                                        <td>&nbsp;&nbsp;</td>
                                                                        <td> <i class="fa fa-calendar"></i></td>
                                                                        <td><asp:TextBox    CssClass="form-control"      runat="server" ID="txtDateTo"> </asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                               
                                                                 
                                                                    

                                                                </div>
                                                            </div>
                                                            <!-- /input-group -->
                                                            
                                                        </div>
                                       	</div>
                                           <!-- <div class="row form-group">
                                                <label class="col-md-3 control-label">Date Range :</label>
                                                <div class="col-md-9">
                                                    <div class="row form-group">
                                                        

                                                        <div class="col-xs-6 col-md-6">
                                                            <label class="control-label"><small>FROM</small></label>
                                                            <div class="input-icon">
                                                                <i class="fa fa-calendar"></i>
                                                                <input class="form-control input-sm date-picker" size="16" type="text" value="12-02-2012" data-date="12-02-2012" data-date-format="dd-mm-yyyy" data-date-viewmode="years"/>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6 col-md-6">
                                                            <label class="control-label"><small>TO</small></label>
                                                            <div class="input-icon">
                                                                <i class="fa fa-calendar"></i>
                                                                <input class="form-control input-sm date-picker" size="16" type="text" value="12-02-2012" data-date="12-02-2012" data-date-format="dd-mm-yyyy" data-date-viewmode="years"/>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>-->
                                    </div>
                                    <div class="col-md-6">
                                        <div class="row form-group">
                                            <label class="col-md-3 control-label">Search By</label>
                                            <div class="col-md-9">
                                            <div class="radio-list">
                                                     <table>
                                                        <tr>
                                                            <td><asp:RadioButton ID="rdAllDates" Checked="true" GroupName="Dates" Text="All Dates"    runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdOrderDates"  GroupName="Dates" Text="Adjustment Dates"   runat="server" /></td>
                                                            <td>&nbsp;</td>
                                                            <td><asp:RadioButton ID="rdDeliveryDates"  Visible=false GroupName="Dates" Text="Arrival Date"   runat="server" /></td>
                                                        </tr>
                                                     
                                                     </table>
                                                     
                                                    
                                                    

                                                </div>
                                                
                                            </div>
                                    	</div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4 col-md-2">
                                    		<div class="form-group">

                                                

                                                <asp:DropDownList ID="drpFieldName"  CssClass="form-control input-sm select2me" runat="server">
                                                        <asp:ListItem Value="EmployeeID">Adjusted by</asp:ListItem>
                                                                                             
                                                        <asp:ListItem Value="TransactionNumber">Adjustment #</asp:ListItem>                    
                                                                      
                                                        
                                                 </asp:DropDownList>	

                                            </div>
                                     </div>
                                     <div class="col-xs-3 col-md-1">
                                     		<div class="form-group">
                                             
                                            <asp:DropDownList ID="drpCondition" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Value="Like">Like</asp:ListItem>
                                                <asp:ListItem Value=">">&gt;</asp:ListItem>
                                                <asp:ListItem Value="<">&lt;</asp:ListItem>
                                                <asp:ListItem Value="=">=</asp:ListItem>
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
                                         
                                            <asp:Button ID="btnSearch" CssClass="btn btn-success btn-xs" runat="server" Text="SEARCH" />

                                        </div>
                                        <asp:Label ID="lblErr" runat="server" Text=" "></asp:Label>
                                </div>
                                </div>
                            
                            <hr style="margin-top:5px;"/>
                            
                              <div class="note note-success margin-bottom-0">
                              
                              
                            
								 
								<div class="row">
                                	<div class="col-xs-4">
                                    	 
                                    	 
                                    	   Location 
                                            

                                             <asp:DropDownList ID="cmblocationid"   CssClass="form-control input-sm select2me" runat="server"  AppendDataBoundItems="true" AutoPostBack="true" >
                                                    <asp:ListItem Text="--All Locations--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            
                                             

                                          

                                    	 
                                      	 
                                    </div>
                                    <div class="col-xs-4">
                                    		 
                                             <asp:DropDownList  AutoPostBack="true"  Visible="false" AppendDataBoundItems="true"   CssClass="form-control input-sm select2me"  ID="Payment" runat="server">
                                                <asp:ListItem Text="--Please Select Payment--" Value="" ></asp:ListItem>
                                            </asp:DropDownList> 
                                             

                                             <asp:DropDownList ID="Delivery"  Visible="false" AutoPostBack="true"   AppendDataBoundItems="true" CssClass="form-control input-sm select2me" runat="server">
                                                <asp:ListItem Text="--Please Select Delivery--" Value="" ></asp:ListItem>
                                                 <asp:ListItem Text="Delivery" Value="Delivery"></asp:ListItem>
                                                                <asp:ListItem Text="Ship" Value="Ship"></asp:ListItem>
                                              </asp:DropDownList>

                                    	 
                                    </div>
                                    
                                     
                             <div class="col-xs-4">
                                    		 
                                          

                                    	 
                                        	 
                                    </div>
                            
                                    
                                </div>
							
							
							
							   </div> 
                            
                           
                            
                            
                           
                            
                           
                            <div style="display:none;" class="note note-success margin-bottom-0">
								 
								<div class="row">
                                	<div class="col-xs-3">
                                    	<div class="row">
                                            <label class="col-md-4 control-label text-right">All Orders</label>
                                            <div class="col-md-8 text-left">
                                           
                                            <asp:RadioButton ID="chkall" GroupName="ordertype"   Checked="true" AutoPostBack="true" runat="server" />

                                    	</div>
                                      	</div>
                                    </div>
                                    <div class="col-xs-3">
                                    		<div class="row">
                                            <label class="col-md-4 control-label text-right">Hold Orders</label>
                                            <div class="col-md-8 text-left">
                                              <asp:RadioButton ID="chkhold" GroupName="ordertype"   CssClass="radio-inline"  runat="server"    AutoPostBack="true"  />
                                                                    
                                            </div>
                                        	</div>
                                    </div>
                                    <div class="col-xs-3">
                                    		<div class="row">
                                            <label class="col-md-4 control-label text-right">Return Orders</label>
                                            <div class="col-md-8 text-left">
                                                 <asp:RadioButton ID="checkreturn" GroupName="ordertype"  CssClass="radio-inline" runat="server"   AutoPostBack="true"  />
                                            </div>
                                        	</div>
                                    </div>
                                     <div class="col-xs-3">
                                    		<div class="row">
                                            <label class="col-md-4 control-label text-right">Website/Mobile</label>
                                            <div class="col-md-8 text-left">
                                                <asp:RadioButton ID="chkcartorders"  GroupName="ordertype"  CssClass="radio-inline"    runat="server"     AutoPostBack="true"  />                                   
                                            </div>
                                        	</div>
                                    </div>
                                </div>
							</div>
                            
                            
						</div>
					</div>
				<!-- END PORTLET-->
        
     
    
 
 <asp:GridView ID="OrderHeaderGrid" AllowSorting="True" runat="server" DataKeyNames="TransactionNumber"  CssClass="table table-bordered table-striped table-condensed flip-content"     AutoGenerateColumns="false" AllowPaging="True" PageSize="25">
  
            <Columns>
            
              <asp:TemplateField HeaderText="View"  ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a target="_blank" class="btn btn-warning btn-xs tooltips" href='https://reports.quickflora.com/reports/scripts/AdjustInventoryReport.aspx?CompanyID=<%=CompanyID %>&DivisionID=<%=DivisionID %>&DepartmentID=<%=DepartmentID %>&InventoryAdjustmentsNumber=<%# Eval("TransactionNumber")%>'>
                                <i class="fa fa-search"></i>
                                
                                </a>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Edit" >
                <ItemTemplate>
                   <asp:ImageButton ID="imgEdit"    ToolTip="Edit" ImageUrl="~/Images/edit.gif" runat="server" CommandName="Edit" />
                </ItemTemplate>
                    
                </asp:TemplateField>
                
                  
                  <asp:TemplateField HeaderText="PRN">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgWorkTicketPrint" 
                            CommandName="AIPrint" CommandArgument='<%# Eval("TransactionNumber") %>' ToolTip="Print Adjustments"
                            ImageUrl="~/images/print.gif" runat="server" />
                    </ItemTemplate>
                    
                </asp:TemplateField>     
                      
                      
                   <asp:TemplateField HeaderText="Email"  ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a  class="btn btn-warning btn-xs tooltips" href='emailAdjustInventory.aspx?InventoryAdjustmentsNumber=<%# Eval("TransactionNumber")%>'>
                                <img border=0 src="https://secure.quickflora.com/EnterpriseASP/images/menu_icons/imp.png" />
                                
                                </a>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                
                
                 <asp:TemplateField HeaderText="Adjustment #" SortExpression="TransactionNumber">
                    <ItemTemplate>
                       <asp:Label ID="lblstrik" Text='*' ForeColor="Orange" Visible="false" runat="server"></asp:Label> <asp:Label ID="lblOrderNumber" Text='<%#Eval("TransactionNumber")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                     
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Adjusted by"  SortExpression="EmployeeID">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerID" Text='<%#Eval("EmployeeID")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                     
                </asp:TemplateField>
                 
                
                <asp:TemplateField HeaderText="Adjustment Date" SortExpression="CreatedDate">
                    <ItemTemplate>
					    <%#Convert.ToDateTime(Eval("CreatedDate")).ToShortDateString()%>
					   
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                  
                
                  
                
                   <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <ItemTemplate>
                       <asp:Label ID="lblOrderStatus" Text='<%#GetStatus(Eval("Status"))%>' runat="server"></asp:Label>
                       <%-- <%#Eval("OrderStatus")%>--%>
				    </ItemTemplate>
				     
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Location" >
                    <ItemTemplate>
                       
                        <%# FillLocationName(Eval("LocationID"))%> 
                    </ItemTemplate>
                    
                </asp:TemplateField>                
                
                
                
            </Columns>
        </asp:GridView>
  
 
           </asp:Panel>

   <asp:Panel ID="pnlconfirm" runat="server" Visible="false"   >
           <div style="text-align:center;"> <b>Order Number : <asp:Label ID="lblordernumber" runat="server" Text="Label"></asp:Label> </b> 
           <input type="hidden" name="HD_Ordernumber" id="HD_Ordernumber"   runat="server" value="" />
           </div>
            <table border="0" width="775" id="table8" align=center cellspacing="0" cellpadding="0">
                                              <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp;Reason for cancel
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" align="center" valign="top">
                                                        <asp:Label ID="lblmessage" runat="server" ForeColor=red Text=""></asp:Label>
                                                    <br />
                                                        <asp:TextBox ID="txtcancelDesc" TextMode="MultiLine" Width="400" Height="100"
                                                            runat="server"></asp:TextBox>
                                                    <br />
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp;Employee Name
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" align="center" valign="top">
                                                    <asp:TextBox ID="txtemployee"  Width="400" 
                                                            runat="server"></asp:TextBox>
                                                    <br />
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>   
                                              <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                              
                                                    </td>
                                                </tr> 
                                              <tr  >
                                                    <td colspan="5" width="775" height="4">
                                                     <asp:Panel ID="pnlemv"  Visible=false  runat="server">
                                                     <center>
                                                         <asp:Label ID="lblCCMessage" runat="server" Text=""></asp:Label>
                                                         <br />
                                                         Aprroval Status : <asp:TextBox ID="txtCheck" runat="server"></asp:TextBox>
                                                         Aprroval Number : <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                                                     </center>
                                                     <br />
                                                     <br />
                                                     <div id="autocompletePayment" ></div>
                                                     <br />
                                                        <center >
                                                         <asp:Button ID="btnemv" runat="server"  Text="EMV Payment Request" />
                                                                                    &nbsp;<br />
                                                         <asp:Button ID="btnemvres" runat="server"  Text="EMV Payment Respose" />
                                                         </center>
                                                        </asp:Panel>  
                                                    </td>
                                                </tr> 
                                                                                                                                            
                                                <tr>
                                                    <td width="176" align="center">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="450" valign="top">
                                                        <table border="0" width="450" id="table9" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="20" height="8">
                                                                </td>
                                                                <td width="210" height="8">
                                                                </td>
                                                                <td width="201" height="8">
                                                                </td>
                                                                <td width="19" height="8">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="20" height="12">
                                                                </td>
                                                                <td width="411" align="center" colspan="3" height="12">
                                                                    <asp:Button CssClass="actionbutton" ID="btnProcess" runat="server" Text="Confirm for order Cancel" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
 <asp:Button ID="btnwindoprint" CssClass="actionbutton" Visible="false"  runat="server" Text="Print To Receipt Printer" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;                                                                    
                                                                    <asp:Button CssClass="actionbutton" ID="btncancel" runat="server" Text="Back" />
                                                                </td>
                                                                <td width="19" height="12">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="20" height="8">
                                                                </td>
                                                                <td width="210" height="8">
                                                                </td>
                                                                <td width="201" height="8">
                                                                </td>
                                                                <td width="19" height="8">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="1" bgcolor="#808080">
                                                    </td>
                                                    <td width="147" valign="top">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#456037">
                                                    <td colspan="5" width="775" height="4">
                                                    </td>
                                                </tr>
                                            </table>
        
        
        </asp:Panel>
 
  </ajax:AjaxPanel> 

   




  <script type="text/javascript">


      var reqPayment;

      function InitializePayment() {
          try {
              reqPayment = new ActiveXObject("Msxml2.XMLHTTP");
          }
          catch (e) {
              try {
                  reqPayment = new ActiveXObject("Microsoft.XMLHTTP");
              }
              catch (oc) {
                  reqPayment = null;
              }
          }

          if (!reqPayment && typeof XMLHttpRequest != "undefined") {
              reqPayment = new XMLHttpRequest();
          }

      }

      function SendQueryPayment(key) {
          InitializePayment();
          var start = 0;
          start = new Date();
          start = start.getTime();
          var url = "AjaxOrderPaymentStatus.aspx?k=" + key + '&start=' + start;
          //alert(url);
          if (reqPayment != null) {
              reqPayment.onreadystatechange = ProcessPayment;
              reqPayment.open("GET", url, true);
              reqPayment.send(null);

          }

      }



      function ProcessPayment() {

          //alert('In Payment process Main');
          if (reqPayment.readyState == 4) {

              //alert('In Payment process Main 4');
              // only if "OK"
              if (reqPayment.status == 200) {

                  //alert('In Payment process Main 200');

                  //alert(reqPayment.responseText);
                  //alert(document.getElementById("ctl00_ContentPlaceHolder_txtCheck"));  
                  if (trim(reqPayment.responseText) == "") {
                      // alert("in blank");  
                      //HideDiv("autocomplete");
                      document.getElementById("<%=txtCheck.ClientID%>").value = "";

                      var newdiv = document.createElement("div");
                      newdiv.innerHTML = "<br><div style='text-align:center;font-size:18px; color:Red;' >your Payment request processing.......</div>";
                      var container = document.getElementById("autocompletePayment");

                      container.appendChild(newdiv)
                      //calldoPollTimePayment();
                      var pollHand = setTimeout(calldoPollTimePayment, 10000);
                  }
                  else {

                      //alert("in not blank");  

                      document.getElementById("<%=txtCheck.ClientID%>").value = reqPayment.responseText;
                      var container = document.getElementById("autocompletePayment");



                      if (window.document.getElementById("<%=txtCheck.ClientID%>").value == "Approved") {
                          //alert("Approved");
                          container.innerHTML = "<div style='text-align:center;font-size:18px; color:Green;' >Payment Process Result " + reqPayment.responseText + " </div>";
                          window.document.getElementById("<%=btnemvres.ClientID%>").focus();
                          window.document.getElementById("<%=btnemvres.ClientID%>").click();
                          //BtnBookOrder

                      }
                      else {
                          container.innerHTML = "<div style='text-align:center;font-size:18px; color:Red;' >Payment Process Result " + reqPayment.responseText + " </div>";

                      }

                  }
              }
              else {
                  document.getElementById("autocompletePayment").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;

              }
          }
      }



      function calldoPollTimePayment() {
          //alert('In Payment process');

          if (window.document.getElementById("<%=txtCheck.ClientID%>") != null) {
              if (window.document.getElementById("<%=txtCheck.ClientID%>").value == "") {
                  //alert(window.document.getElementById("<%=HD_Ordernumber.ClientID%>").value);
                  SendQueryPayment(window.document.getElementById("<%=HD_Ordernumber.ClientID%>").value);
              }


          }

      }
       

</script>



 

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
<script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
<script type="text/javascript"  src="assets/admin/pages/scripts/search.js"></script>

<script>
    jQuery(document).ready(function () {
        
        Search.init();
    });
</script>
</asp:Content>

