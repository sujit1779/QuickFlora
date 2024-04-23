<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReceivingByScan.aspx.vb" MasterPageFile="~/MainMaster.master"  Inherits="ReceivingByScan" %>
 


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    
<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>

<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>

     


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Receiving 
</asp:Content>
 
 <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
		<div class="portlet box green">
			<div class="portlet-title">
				<div class="caption">Scan Box</div>
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
                                

                                 <table>
                                     <tr> 
                                         <td style="vertical-align:bottom;" ><img  alt="excel" src="https://secure.quickflora.com/images/scanbar.png" width="75" height="75" border="0" />  </td>
                                         <td style="vertical-align:middle;" >&nbsp; <asp:TextBox ID="txtscan" runat="server" CssClass="form-control" ></asp:TextBox>&nbsp; </td>
                                         <td style="vertical-align:middle;" >&nbsp; <asp:Button runat="server" ID="SearchButton"   CssClass="btn btn-success" Text="Scan" />  </td>
                                         <td style="vertical-align:middle;" >&nbsp; <asp:Button runat="server" ID="btnrecevied"  Visible="false"   CssClass="btn btn-success" Text="Mark Received" />  </td>
                                     </tr>

                                 </table>
                                 
                               
                                
                             
                                 
                                
                            </td> 
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                &nbsp;
                            </td> 
                        </tr> 
                        <tr>
                            <td align="left" colspan="3">
                                
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
                <td style="width:30%; text-align:left;" >Receiving By <% =EmployeeID & " on " & Date.Now.Date %> </td>
                <td style="width:30%; text-align:center;" ><h3><u> Purchase Order Details </u> </h3> </td>
                <td style="width:30%; text-align:right; padding-right:30px;" >&nbsp;    </td>
            </tr>
        </table>
          
           
                    <table cellspacing="1" cellpadding="1" border="0"    id="Table2" style="color: Black;    background-color: White; width: 100%;">
                            <tr  >
                                <td style="padding:2px; background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center" > PO #</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Vendor Code</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Item ID</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Item Name</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Description</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Qty</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > UOM</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Pack </td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Total Units </td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  >  Received Qty</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Received Pack </td> 
                                <td style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  >  Receive Units</td>  
                            </tr>
                         <asp:Repeater runat="server"   ID="rptorderlist">
                            <ItemTemplate>
                                <tr style="background-color: White; font-size: 8pt;">
                                    <td align="center" >
                                        <%# (Eval("PurchaseNumber"))%>
                                         
                                    </td>
                                    <td align="center" >
                                        <%# (Eval("VendorID"))%>
                                    </td>
                                    <td align="left" >
                                        <%# (Eval("ItemID"))%>
                                    </td>
                                        <td align="left" >
                                        <%# (Eval("ItemName"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Description"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("VendorQTY"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%#  (Eval("ItemUOM"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%#  (Eval("VendorPacksize"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("OrderQty"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("ReceivedVenorQTY"))%>
                                    </td>
                                        <td align="center" >
                                        &nbsp;<%# (Eval("ReceivedPackSize"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("ReceivedQty"))%>
                                    </td>
                                     
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #F4F4F4; font-size: 8pt;">
                                     <td align="center" >
                                        <%# (Eval("PurchaseNumber"))%>
                                         
                                    </td>
                                    <td align="center" >
                                        <%# (Eval("VendorID"))%>
                                    </td>
                                    <td align="left" >
                                        <%# (Eval("ItemID"))%>
                                    </td>
                                        <td align="left" >
                                        <%# (Eval("ItemName"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Description"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("VendorQTY"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%#  (Eval("ItemUOM"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%#  (Eval("VendorPacksize"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("OrderQty"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("ReceivedVenorQTY"))%>
                                    </td>
                                        <td align="center" >
                                        &nbsp;<%# (Eval("ReceivedPackSize"))%>
                                    </td>
                                    <td align="center" >
                                        &nbsp;<%# (Eval("ReceivedQty"))%>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                         </asp:Repeater>

                        <tr align="right" style="background-color: #C6C6C6; font-size: 12pt;" >
                            <td align="left"  >  </td> 
                            <td align="left"  >  </td>
                            <td align="left"  >  </td> 
                            <td align="left"  >  </td>
                            <td align="left"  >   </td>
                            <td align="left"  >   </td> 
                            <td align="left"  >  </td>
                            <td align="left"  >  </td>
                            <td > <asp:Label ID="lbltax"    runat="server" Text=''></asp:Label></td>
                            <td align="left"  >   </td> 
                            <td align="left"  >  </td> 
                            <td align="left"  >  </td>
                            <td > <asp:Label ID="lblTotal"    runat="server" Text=''></asp:Label></td>
                            <td align="left"  >   </td>
                            <td align="left"  >   </td>
                            </tr>


                        </table>
                 

                 
                   
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


         function reswipe() {
             
            if (window.document.getElementById("<%=txtscan.ClientID%>") != null) {
                window.document.getElementById("<%=txtscan.ClientID%>").value = "";
                window.document.getElementById("<%=txtscan.ClientID%>").focus();
               
            }

        }

    </script>



</asp:Content>
 