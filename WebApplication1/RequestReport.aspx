<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RequestReport.aspx.vb" MasterPageFile="~/MainMaster.master"  Inherits="RequestReport" %>
 


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    
<link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css"/>
<link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"/>

<link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css"/>

  <link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
    <link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
    <script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>

     


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Request Order Report
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
                                <asp:Label ID="lbReportdate" runat="server" Text='Report Date From : ' CssClass="font11_blue_black_padding" />
                                <asp:TextBox ID="txtDeliveryDate" runat="server" Width="101px"></asp:TextBox>&nbsp;
                                <img id="btnCal1" src="https://secure.quickflora.com/Admin/images/calender.gif" style="cursor: pointer;"  />
                                &nbsp;
                                <asp:Label ID="Label1" runat="server" Text=' To : ' CssClass="font11_blue_black_padding" />
                                <asp:TextBox ID="txtDeliveryDateTO" runat="server" Width="101px"></asp:TextBox>&nbsp;
                                <img id="Img1" src="https://secure.quickflora.com/Admin/images/calender.gif" style="cursor: pointer;"  />
                             
                                &nbsp; <asp:RadioButton ID="optShipDate" Checked="true"  AutoPostBack="true"   Text="&nbsp;By Ship Date" GroupName="Date"   runat="server" /> &nbsp; <asp:RadioButton ID="OptArriveDate" Text="&nbsp;By Arrive Date"  AutoPostBack="true"  GroupName="Date"   runat="server" />
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
                                
                                Location
                                <asp:DropDownList ID="cmblocationid" AppendDataBoundItems="true"  AutoPostBack="true"  runat="server">
                                    <asp:ListItem Text="--All Location--" Value=""> </asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                            Inventory Origin 
                            <asp:DropDownList ID="drpInventoryOrigin"   runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Origin--" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;
                                Ship Method:
                                 <asp:DropDownList ID="drpshipemthod"   runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Ship Method--" Value=""></asp:ListItem>   
                            </asp:DropDownList> 
                               
                               <br />
                               <div style=" text-align:center; width:75%;">
                                 &nbsp;&nbsp; <asp:RadioButton ID="optallstatus" Checked="true" AutoPostBack="true"    Text="&nbsp;All Status" GroupName="status"   runat="server" /> &nbsp; <asp:RadioButton ID="optBought" Text="&nbsp;Bought"  AutoPostBack="true"   GroupName="status"   runat="server" />
                                 &nbsp;<asp:CheckBox ID="chkWithOther" AutoPostBack="true" Text ="&nbsp;Include With-Other" runat="server" />
                               </div>
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
                <td style="width:30%; text-align:left;" >Executed By <% =EmployeeID & " on " & Date.Now.Date %> </td>
                <td style="width:30%; text-align:center;" ><h3><u> Request Order Report </u> </h3> </td>
                <td style="width:30%; text-align:right; padding-right:30px;" >&nbsp; <a href="#" onClick="window.open('ExportRequestReport.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="50" height="50" border="0" /></a>  </td>
            </tr>
        </table>
         
           <asp:GridView ID="xyza" visible="false"  AllowSorting="True" runat="server"   CssClass="table table-bordered table-striped table-condensed flip-content"     AutoGenerateColumns="false" AllowPaging="True" PageSize="25">

           <Columns>
                 <asp:TemplateField HeaderText="Req.Order #"  >
                    <ItemTemplate>
                       <asp:Label ID="OrderNo" Text='<%#Eval("OrderNo")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="PO #"  >
                    <ItemTemplate>
                       <asp:Label ID="PONO" Text='<%#Eval("PONO")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Ship Date"  >
                    <ItemTemplate>
                       <asp:Label ID="ShipDate" Text='<%#Eval("ShipDate")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Arrive Date"  >
                    <ItemTemplate>
                       <asp:Label ID="ArriveDate" Text='<%#Eval("ArriveDate")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Vendor Name"  >
                    <ItemTemplate>
                       <asp:Label ID="VendorName" Text='<%#Eval("VendorName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Product"  >
                    <ItemTemplate>
                       <asp:Label ID="Product" Text='<%#Eval("Product")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Item Name"  >
                    <ItemTemplate>
                       <asp:Label ID="ItemName" Text='<%#Eval("ItemName")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Q REQ"  >
                    <ItemTemplate>
                       <asp:Label ID="Q_REQ" Text='<%#Eval("Q_REQ")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COLOR/VARIETY"  >
                    <ItemTemplate>
                       <asp:Label ID="COLOR_VARIETY" Text='<%#Eval("COLOR_VARIETY")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Q. ORD"  >
                    <ItemTemplate>
                       <asp:Label ID="Q_ORD" Text='<%#Eval("Q_ORD")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="PACK"  >
                    <ItemTemplate>
                       <asp:Label ID="PACK" Text='<%#Eval("PACK")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="COST"  >
                    <ItemTemplate>
                       <asp:Label ID="COST" Text='<%#Eval("COST")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="Ext. COSt"  >
                    <ItemTemplate>
                       <asp:Label ID="Ext_COSt" Text='<%#Eval("Ext_COSt")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Buyer"  >
                    <ItemTemplate>
                       <asp:Label ID="Buyer" Text='<%#Eval("Buyer")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Status"  >
                    <ItemTemplate>
                       <asp:Label ID="Status" Text='<%#Eval("Status")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Location"  >
                    <ItemTemplate>
                       <asp:Label ID="Location" Text='<%#Eval("Location")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns> 

</asp:GridView> 
           
                    <table cellspacing="1" cellpadding="1" border="0"    id="Table2" style="color: Black;    background-color: White; width: 100%;">
                            <tr  >
                                <td style="padding:2px; background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center" > Req.<br /> Order #</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > PO #</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Ship Date</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Arrive Date</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Vendor Name</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Product Code</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Product Name</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Qty Req. </td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > COLOR/VARIETY </td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Qty. Ord.</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Pack </td> 
                                <td style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Cost</td> 
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Ext Cost</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Buyer</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Status</td>
                                <td  style="padding:2px;background-color: #C6C6C6; font-size: 10pt; border-right-width:1px; border-right-style:solid; border-color:White;" align="center"  > Location</td>
                            </tr>
                         <asp:Repeater runat="server"   ID="rptorderlist">
                            <ItemTemplate>
                                <tr style="background-color: White; font-size: 8pt;">
                                    <td align="left" >
                                        <%# (Eval("OrderNo"))%>
                                         
                                    </td>
                                    <td align="left" >
                                        <%# (Eval("PONO"))%>
                                    </td>
                                    <td align="left" >
                                        <%# (Eval("ShipDate"))%>
                                    </td>
                                        <td align="left" >
                                        <%# (Eval("ArriveDate"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("VendorName"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Product"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%#  (Eval("ItemName"))%>
                                    </td>
                                    <td align="right" >
                                        &nbsp;<%#  (Eval("Q_REQ"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("COLOR_VARIETY"))%>
                                    </td>
                                    <td align="right" >
                                        &nbsp;<%# (Eval("Q_ORD"))%>
                                    </td>
                                        <td align="right" >
                                        &nbsp;<%# (Eval("PACK"))%>
                                    </td>
                                    <td align="right" >
                                        &nbsp;$<%#Format((Eval("COST")), "0.00")%>
                                    </td>
                                    <td align="right" >
                                        &nbsp;$<%# (Eval("Ext_COSt"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Buyer"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Status"))%>
                                    </td>
                                    <td>
                                        &nbsp;<%# (Eval("Location"))%> 
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #F4F4F4; font-size: 8pt;">
                                     <td align="left" >
                                        <%# (Eval("OrderNo"))%>
                                          
                                    </td>
                                    <td align="left" >
                                        <%# (Eval("PONO"))%>
                                    </td>
                                    <td align="left" >
                                        <%# (Eval("ShipDate"))%>
                                    </td>
                                        <td align="left" >
                                        <%# (Eval("ArriveDate"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("VendorName"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Product"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%#  (Eval("ItemName"))%>
                                    </td>
                                    <td align="right" >
                                        &nbsp;<%#  (Eval("Q_REQ"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("COLOR_VARIETY"))%>
                                    </td>
                                    <td align="right" >
                                        &nbsp;<%# (Eval("Q_ORD"))%>
                                    </td>
                                        <td align="right" >
                                        &nbsp;<%# (Eval("PACK"))%>
                                    </td>
                                    <td align="right" >
                                     &nbsp;$<%#Format((Eval("COST")), "0.00")%>
                                       
                                    </td>
                                    <td align="right" >
                                    &nbsp;$<%# String.Format("{0:N2}", Convert.ToString(Eval("Ext_COSt")) ) %>
                                        
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Buyer"))%>
                                    </td>
                                    <td align="left" >
                                        &nbsp;<%# (Eval("Status"))%>
                                    </td>
                                    <td>
                                        &nbsp;<%# (Eval("Location"))%> 
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
         
    </script>



</asp:Content>
 