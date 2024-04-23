<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" Debug="true" AutoEventWireup="false" CodeFile="InventoryFormAdd.aspx.vb" Inherits="InventoryFormAdd" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    <link rel="stylesheet" type="text/css" href="assets/components.css" />

<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
<link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
<script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>


    
    <script type="text/javascript">


        var req;

        function Initialize() {
            try {
                req = new ActiveXObject("Msxml2.XMLHTTP");
            }
            catch (e) {
                try {
                    req = new ActiveXObject("Microsoft.XMLHTTP");
                }
                catch (oc) {
                    req = null;
                }
            }

            if (!req && typeof XMLHttpRequest != "undefined") {
                req = new XMLHttpRequest();
            }

        }

        
    function SendQuery2(key) {



        // alert(document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value);
        var start = 0;
        start = new Date();
        start = start.getTime();

        Initialize();
        var url = "AjaxAvaibilityItemsSearch.aspx?k=" + key + "&start=" + start + "&locationid="  + document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value;
      // alert(url);
        if (req != null) {
            req.onreadystatechange = Process2;
            req.open("GET", url, true);
            req.send(null);

        }

    }

    function Process2() {
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(ajax-loader-text.gif)';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'right';


        if (req.readyState == 4) {
            // only if "OK"
            if (req.status == 200) {
                document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url()';
                if (req.responseText == "") {
                    //alert("in blank");  
                    //HideDiv("autocomplete");
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
                    document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
                    //document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some other keyword.</div>";
                    var container = document.getElementById("autocomplete2");
                    container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                    container.appendChild(newdiv)
                }
                else {

                    if (document.getElementById("<%=txtitemsearch.ClientID%>").value != "") {
                        ShowDiv("autocomplete2");
                        //document.getElementById("autocomplete2").innerHTML = req.responseText;

                        var newdiv = document.createElement("div");
                        newdiv.innerHTML = req.responseText;
                        var container = document.getElementById("autocomplete2");
                        container.innerHTML = "<div style='text-align:center' ><a class='btn btn-danger btn-xs'  href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                        container.appendChild(newdiv)
                    }
                }
            }
            else {
                document.getElementById("autocomplete2").innerHTML = "There was a problem retrieving data:<br>" + req.statusText;
            }
        }
    }
        function FillSearchtextBox2(val) {
          //  document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = val;
            document.getElementById("ctl00_ContentPlaceHolder_btnSearch").focus();
            document.getElementById("ctl00_ContentPlaceHolder_btnSearch").click();
            itemrsearchcloseProcess();

    }


    function itemrsearchcloseProcess() {

        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundRepeat = 'no-repeat';
        document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundPositionX = 'left';
      //  document.getElementById("<%=txtitemsearch.ClientID%>").value = "";
        document.getElementById("autocomplete2").innerHTML = "";
        HideDiv("autocomplete2");


    }


    function itemsearchBlurProcess() {

        if (document.getElementById("<%=txtitemsearch.ClientID%>").value == "") {
            HideDiv("autocomplete2");
            document.getElementById("<%=txtitemsearch.ClientID%>").style.backgroundImage = 'url(searchbackground.jpg)';
        }

    }
         
        function Qtybalance(This, qty, name, id ,qty2) {
            qty = qty - qty2;
            
            document.getElementById(id).value = qty - (This.value);
             This.style.backgroundColor = "";
            if (document.getElementById(id).value < 0) {
                alert("Please check Accept Qty must be less than equal to balance Qty");
                This.style.backgroundColor = "Red";
                This.focus();
                This.click();
            }
        }
        function Saveitem(This, ItemID, name, oldvalue) {
            Initialize();

            This.style.backgroundColor = "";

            var url = "AjaxSaveGrowerInventory.aspx?txtend=" + document.getElementById("ctl00_ContentPlaceHolder_txtend").value + "&txtDateTo=" + document.getElementById("ctl00_ContentPlaceHolder_txtDateTo").value + "&txtstart=" + document.getElementById("ctl00_ContentPlaceHolder_txtstart").value + "&LocationID=" + document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value + "&ItemID=" + ItemID + "&value=" + This.value + "&name=" + name;
            //alert(url);
            //alert(This.value);
            //alert(oldvalue);

            

            if (This.value == oldvalue) {
                return;
            }

             // alert(url);

            document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
            document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

            $.get(url, function(data, status) {
              //  alert("Data: " + data + "\nStatus: " + status);
                document.getElementById("autosave").style.backgroundImage = 'url()';
            });

 

        }
         
        var value_on_click = "";
        
        function myFocusFunction(This) {
            This.style.backgroundColor = "yellow";
            if (This.value == "0") {
                This.value = "";
            }
            value_on_click = This.value;
        }

        function myonblurFunction(This) {
            This.style.backgroundColor = "";
        }
 
          
        function ShowDiv(divid) {
            document.getElementById(divid).style.height = "200px";
            if (document.layers) document.layers[divid].visibility = "show";
            else document.getElementById(divid).style.visibility = "visible";
        }


        function HideDiv(divid) {
            document.getElementById(divid).style.height = "0px";
            if (document.layers) document.layers[divid].visibility = "hide";
            else document.getElementById(divid).style.visibility = "hidden";
        }


    </script>


    <!-- Free javascripts @ https://www.hscripts.com -->
<script type="text/javascript">
checked=false;
function checkedAll (frm1)
 {
 var aa= document.getElementById('aspnetForm'); 
 if (checked == false)
    {
    checked = true
    }
    else
    {
    checked = false
    }

    for (var i =0; i < aa.elements.length; i++)
    { 
        if (aa.elements[i].id != "ctl00_ContentPlaceHolder_chkexculea" ) {
            if(aa.elements[i].disabled != true)
                aa.elements[i].checked = checked;

        }
       

    }
}
 
</script>
<!-- Script by hscripts.com -->


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Grower Availability 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
 
        <asp:Panel ID="pnlgrid" runat="server" Visible="true">
             
            <div id="dv2" runat=server visible=true  class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                         Search Item
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="dv" runat=server  visible="true" style="display:none;" class="table-toolbar text-center">
                        <div class="btn-group">
                            <div class="col-md-4">
                                

                                <asp:DropDownList ID="drpSearchFor" runat="server" CssClass="form-control input-medium select2me select2-offscreen" data-placeholder="Select..." tabindex="-1">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="ItemID" Text="Item ID"></asp:ListItem>
                                    <asp:ListItem Value="GroupCode" Text="Group Code"></asp:ListItem>
                                    <asp:ListItem Value="Variety" Text="Variety"></asp:ListItem>
                                    <asp:ListItem Value="ItemDescription" Text="Description"></asp:ListItem>
                                    <asp:ListItem Value="Location" Text="Grower"></asp:ListItem>
                                    <asp:ListItem Value="ItemColor" Text="Color"></asp:ListItem>
                                    <asp:ListItem Value="FlowerType" Text="Type"></asp:ListItem>
                                    <asp:ListItem Value="Grade" Text="Grade"></asp:ListItem>
                                    <%--<asp:ListItem Value="OrderNumber" Text="Order Number"></asp:ListItem>--%>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="btn-group">
                            <div class="col-md-1">
                                
                                <asp:DropDownList ID="drpSearchCondition" runat="server" CssClass="form-control input-medium select2me select2-offscreen" data-placeholder="Select..." tabindex="-1">
                                  <asp:ListItem Value="=" Text="">=</asp:ListItem>
                                      <%--<asp:ListItem Value=">" Text=">"></asp:ListItem>
                                    <asp:ListItem Value="<" Text="<"></asp:ListItem>--%>
                                    <asp:ListItem Value="Like" Text="Like"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="btn-group">
                            <%--<input type="text" class="form-control input-large">--%>
                            <asp:TextBox ID="txtSearchValue" runat="server" CssClass="form-control input-large"></asp:TextBox>
                        </div>
                        <div class="btn-group">
                            <%--<button id="Button1" class="btn green">SEARCH</button>--%>
                            <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btn green"/>
                        </div>
                        <div class="btn-group">
                            <%--<button id="Button2" class="btn grey">CLEAR</button>--%>
                            <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn grey"/>
                        </div>
                    </div>

                 
                  

                </div>
            </div>
           
               <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td width="25%" ><b>Location/Farm</b>  </td> 
                            <td width="35%"  align=left>    <asp:DropDownList ID="cmblocationid"   CssClass="form-control input-sm" runat="server"  AppendDataBoundItems="true" AutoPostBack="true" >
                                                    <asp:ListItem Text="--All Location--" Value=""></asp:ListItem>
                                                </asp:DropDownList> </td>
                             <td width="40%" >&nbsp;</td>
                        </tr>
                         <tr>
                            <td><b>Exclude Date</b> </td> <td align=left>
                                <table id="trav" runat="server" visible="false">

                                    <tr>
                                        <td>  <div  class='input-group date' id='datetimepicker2'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="Arrival Date"     runat="server" ID="txtDateTo"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>  </td>
                                        
                                        <td>  &nbsp;&nbsp;&nbsp;&nbsp; </td>
                                        <td>  </td>
                                    </tr>

                                </table>

                             <asp:CheckBox ID="chkexculea" Checked="true" Text="Exclude Date" Font-Bold="true" runat="server" />
                            
                             </td><td></td>
                        </tr>
                    <tr  >
                            <td><b>Availability</b> </td> <td align=left>
                             <table>
                                   <tr>
                            <td><b>Start &nbsp;</b> </td> <td align=left>
                                <table>
                                    <tr>
                                        <td> <div class='input-group date' id='datetimepicker3'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="Start Date"     runat="server" ID="txtstart"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div> </td>
                                        <td>  &nbsp; </td>
                                        <td>  <b> End </b>&nbsp;</td>
                                        <td>  <div class='input-group date' id='datetimepicker4'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="End Date"     runat="server" ID="txtend"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>  </td>
                                    </tr>

                                </table>
                             
                            
                             </td><td>&nbsp;</td>
                        </tr>
                   

                             </table>
                            
                             </td><td>&nbsp;</td>
                        </tr>


                        <tr>
                            <td><b> </b> </td> <td align="center">
                              
                                <asp:Button ID="btn" runat="server" class="btn green"  Text="Search" />
                             </td><td>&nbsp;</td>
                        </tr>
                 
               
               
               </table>
            
                                      <div class="form-group-search-block">
                                        <div class="input-group">
                                            <span class="input-group-addon input-circle-left">
                                            <i class="fa fa-search"></i>
                                            </span>
                                               
                                                <asp:TextBox ID="txtitemsearch"  CssClass="form-control input-circle-right"    runat="server"></asp:TextBox>
                                               
                                             <br />
                                             <div align="left" class="box autocomplete" style="visibility: hidden;" id="autocomplete2"   ></div>   
                                           
                                            
                                        </div>
                                    </div>
              
            
            
              <div style='z-index: 90; height: 100%' class="table-responsive">
                <hr />
               <table width="100%" >
                   <tr >
                       <td width="10%"  style="text-align:left;" >&nbsp;&nbsp;&nbsp;<input type='checkbox' name='checkall' onclick='checkedAll(aspnetForm);'> &nbsp;&nbsp;<b>Select All</b>   </td>
                       <td width="40%"  style="text-align:left;">
                           <table>
                               <tr>
                                   <td>Ship Method &nbsp;&nbsp; </td>
                                   <td>  <asp:DropDownList ID="drpshipemthod" CssClass="form-control input-sm" Width="200px" runat="server">
                                   
                                        </asp:DropDownList>

                                   </td>
                                   <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  </td>
                                   <td>Ship Date &nbsp;&nbsp; </td>
                                   <td> <asp:TextBox ID="lblShipDate" Text=''   CssClass="form-control input-sm" Width="120px" runat="server"></asp:TextBox> </td>
                               </tr>
                           </table>
                       </td> 
                       <td width="20%"  style="text-align:left;">
                           &nbsp;&nbsp;<asp:Button ID="btnAccept"  Visible="true"  class="btn green" runat="server" Text="Accept" />
                        </td> 
                       <td width="20%"  style="text-align:left;">
                          <b>
                                   <asp:Label id="lblInfo" runat="server"></asp:Label>
                          </b>
                       </td>
                       <td width="20%"  style="text-align:right ;">
                           <a href="#" onClick="window.open('ExportExcelItems2.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="37.5" height="37.5" border="0" /></a>
                       </td>
                   </tr>
               </table>
                  <hr />
                <asp:HiddenField ID="gvSortDirection" runat="server" />
                <asp:GridView ID="gvItemsList" runat="server" CssClass="table table-striped table-bordered table-hover"
                    AllowSorting="true" AllowPaging="False" AutoGenerateColumns="false" DataKeyNames="ItemID">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                         
                        <asp:BoundField HeaderText="Item ID" Visible="false" HeaderStyle-HorizontalAlign="Left" DataField="ItemID"  />

                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkOrderNumber" runat="server" />
                                <asp:Button ID="Button1" Visible="false" CommandName="Edit" CommandArgument='<%# Eval("ItemID") %>' class="btn green" runat="server" Text="Accept" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        
                        <asp:TemplateField Visible="false"  HeaderText="Ship Method">
                            <itemtemplate>
                                <asp:DropDownList ID="drpshipemthods" CssClass="form-control input-sm" Width="100px" runat="server">
                                   
                                </asp:DropDownList>
				            </itemtemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Ship Date" Visible="false"  >
                            <ItemTemplate>
                                <asp:TextBox ID="lblShipDates" Text=''   CssClass="form-control input-sm" Width="80px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                     

                          <asp:TemplateField HeaderText="Group Code"   >
                            <ItemTemplate>
                                <asp:Label  ID="lblGroupCode" Text='<%#Eval("GroupCode")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                         
                         <asp:TemplateField Visible="false" HeaderText="Item ID">
                            <ItemTemplate>
                                <asp:label ID="txtItemID" Text='<%#Eval("ItemID")%>'    runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                        <asp:BoundField HeaderText="Item Name" Visible="false" HeaderStyle-HorizontalAlign="Left" DataField="ItemName"   />
                        <asp:BoundField HeaderText="Variety" HeaderStyle-HorizontalAlign="Left" DataField="Variety"   />
                        
                          <asp:TemplateField HeaderText="Description"   >
                            <ItemTemplate>
                                <asp:Label  ID="lblItemDescription" Text='<%#Eval("ItemDescription")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Grower" HeaderStyle-HorizontalAlign="Left" DataField="Location"   />
                        <asp:TemplateField  Visible="false" HeaderText="Grower Code">
                            <ItemTemplate>
                                <asp:label ID="LBLLocation" Text='<%#Eval("LocationID")%>'  Width="100px"  CssClass="form-control input-sm" runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" DataField="ItemCategoryID"   />
                        <asp:BoundField HeaderText="Variety Code" HeaderStyle-HorizontalAlign="Left" DataField="VarietyID"   />
                         

                          <asp:TemplateField HeaderText="Color"   >
                            <ItemTemplate>
                                <asp:Label  ID="lblItemColor" Text='<%#Eval("ItemColor")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" DataField="FlowerType"   />
                        <asp:BoundField HeaderText="Grade" HeaderStyle-HorizontalAlign="Left" DataField="Grade"   />

                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:label ID="txtstem" Text='<%#Eval("Qty")%>'  Width="50px"  CssClass="form-control input-sm" runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Qty Ordered">
                            <ItemTemplate>
                                <asp:label ID="txtQtyAccepted" BackColor="Wheat" Text='<%#Eval("QtyAccepted")%>'  Width="100px"  CssClass="form-control input-sm" runat="server"></asp:label>
                                <asp:Label ID="lblinlineNumber" Visible="false" runat="server" Text='<%#Eval("inlineNumber")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Accepting">
                            <ItemTemplate>
                                <asp:TextBox ID="txtrecevied"  Text='' Width="50px"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Balance">
                            <ItemTemplate>
                                <asp:TextBox ID="txtBalance"  Enabled="false" Text='' Width="50px" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Price(USD)">
                            <ItemTemplate>
                                <asp:label ID="txtprice"  Text='<%#priceformat(Eval("Price"))%>'  CssClass="form-control input-sm" runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         
                         <asp:TemplateField Visible="false" HeaderText="Delivery Date"  >
                            <ItemTemplate>
					            <asp:label ID="lblArrivalDate"  Text='<%#Convert.ToDateTime(Eval("ArrivalDate")).ToShortDateString()%>' CssClass="form-control input-sm" runat="server"></asp:label>
					   
				            </ItemTemplate>
				     
                        </asp:TemplateField>
                        
                         <asp:TemplateField  HeaderText="Start"  >
                            <ItemTemplate>
					            <%#Convert.ToDateTime(Eval("startavailabledate")).ToShortDateString()%>
					   
				            </ItemTemplate>
				     
                        </asp:TemplateField>
                         <asp:TemplateField   HeaderText="End"  >
                            <ItemTemplate>
					            <%#Convert.ToDateTime(Eval("endavailabledate")).ToShortDateString()%>
					   
				            </ItemTemplate>
				     
                        </asp:TemplateField>
                         
                    </Columns>
                </asp:GridView>
                <br /> 
                <asp:Label ID="lblmsg" runat="server" Visible="false" Text=""></asp:Label>
                <div style="text-align:center">
                      
                </div>
                

            </div>

              <div   id="autosave">
                   <br />
                     <br />
                 </div>
            <hr />
        </asp:Panel>
  
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="assets/global/plugins/keyboard/keyboard.js"></script>
    <script type="text/javascript" src="assets/admin/pages/scripts/search.js"></script>
    <%--<script type="text/javascript" src="assests/dist/bootstrap.min.js"></script>
    <script type="text/javascript" src="assests/dist/bootstrap-table.js"></script>--%>
    <%--<script src="assets/dist/bootstrap-table.min.js" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" src="//code.jquery.com/jquery-1.12.4.js"></script>--%>
    <%--<script type="text/javascript" src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>--%>

    <script>
        jQuery(document).ready(function () {
            Search.init();
            //$('#vendorlist').DataTable({
            //    "processing": true,
            //    "serverSide": true,
            //    "ajax": "EventVendors.aspx/GauravGoyal"
            //});

            //$('#vendorlist').DataTable({
            //    processing: true,
            //    serverSide: true,
            //    ajax: {
            //        type: "POST",
            //        contentType: "application/json; charset=utf-8",
            //        url: "EventVendors.aspx/GauravGoyal",
            //        data: function (d) {
            //            //return JSON.stringify({ parameters: d });
            //            return JSON.stringify(d);
            //            //return { parameters: d };
            //        }
            //    }
            //});

        });

        function deleteRecord(d) {
            alert(d);
        }


        function allnumeric(inputtxt) {
            var numbers = /^[0-9]+$/;
            if (inputtxt.value.match(numbers)) {
                return true;
            }
            else {
                if (inputtxt.value != "") {
                    alert('Please input numeric only');
                    inputtxt.value = "";
                    return false;
                }
             
            }
        }

        function allnumeric2(inputtxt) {
            if (validateCurrency(inputtxt.value)) {
                return true;
            }
            else {
                if (inputtxt.value != "") {
                    alert('Please input Currency only allowed 0.00');
                    inputtxt.value = "";
                    return false;
                }
                
            }
        }


        function validateCurrency(amount) {
            var regex = /^[0-9]\d*(?:\.\d{0,2})?$/;
            return regex.test(amount);
        }

    </script>


    <script type="text/javascript" >
        //alert('test');
        doOnLoadNew();
        doOnLoadNew1();
        doOnLoadNew2();

        var myCalendarto;
        function doOnLoadNew() {
            //alert('test');
           
            myCalendarto = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtDateTo"]);
            //alert(myCalendar);
            myCalendarto.setDateFormat("%m/%d/%Y");
        }

         var myCalendarto1;
        function doOnLoadNew1() {
            //alert('test');
           
            myCalendarto1 = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtstart"]);
            //alert(myCalendar);
            myCalendarto1.setDateFormat("%m/%d/%Y");
        }



         var myCalendarto2;
        function doOnLoadNew2() {
            //alert('test');
           
            myCalendarto2 = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtend"]);
            //alert(myCalendar);
            myCalendarto2.setDateFormat("%m/%d/%Y");
        }




       </script>


</asp:Content>


