<%@ Page Title="" Language="VB" MasterPageFile="~/MainMasterInv.master" Debug="true" AutoEventWireup="false" CodeFile="InventoryForm.aspx.vb" Inherits="InventoryForm" %>
<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
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
        var url = "AjaxAvaibilityItemsSearchAll.aspx?k=" + key + "&start=" + start + "&locationid="  + document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value + "&txtend=" + document.getElementById("ctl00_ContentPlaceHolder_txtend").value + "&txtstart=" + document.getElementById("ctl00_ContentPlaceHolder_txtstart").value;
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
           // document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = val;
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

        function Saveitem(This, ItemID, name, oldvalue) {
            Initialize();
            //alert(This.value);
            This.style.backgroundColor = "";

            var url = "AjaxSaveGrowerInventory.aspx?txtend=" + document.getElementById("ctl00_ContentPlaceHolder_txtend").value + "&txtDateTo=" + document.getElementById("ctl00_ContentPlaceHolder_txtend").value + "&txtstart=" + document.getElementById("ctl00_ContentPlaceHolder_txtstart").value + "&LocationID=" + document.getElementById("ctl00_ContentPlaceHolder_cmblocationid").value + "&ItemID=" + ItemID + "&value=" + This.value + "&name=" + name;
            // alert(url);
            //alert(This.value);
            //alert(oldvalue);
            if (This.value == oldvalue) {
               // return;
            }
             
            document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
            document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

            $.get(url, function(data, status) {
                // alert("Data: " + data + "\nStatus: " + status);
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
Add Grower Availability &nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink5" NavigateUrl="~/InventoryStatus2.aspx"  class="btn green" runat="server">Inventory Status</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Report3.aspx"  class="btn green" runat="server">Products Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp;  <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Report2.aspx"  class="btn green" runat="server">PO Shipment Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Report1.aspx"  class="btn green" runat="server">Product Committed Report</asp:HyperLink>&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Home.aspx" class="btn green" runat="server">Back to Home</asp:HyperLink>
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
                    <div id="dv" runat=server  visible="true"   style="display:none;" class="table-toolbar text-center">
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
                                                    <asp:ListItem Text="--Select Location--" Value=""></asp:ListItem>
                                                </asp:DropDownList> </td>
                             <td width="40%" >&nbsp;</td>
                        </tr>
                         <tr id="trav" runat="server" visible="false" >
                            <td><b>Availability End Date</b> </td> <td align=left>
                             <div class='input-group date' id='datetimepicker2'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="Date"     runat="server" ID="txtDateTo"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                            
                             </td><td>&nbsp;</td>
                        </tr>
                    <tr  >
                            <td><b>Availability</b> </td> <td align=left>
                             <table>
                                   <tr>
                            <td><b>Start Date&nbsp;&nbsp;</b> </td> <td align=left>
                             <div class='input-group date' id='datetimepicker3'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="Arrival Date"     runat="server" ID="txtstart"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                            
                             </td><td>&nbsp;</td>
                        </tr>
                    <tr>
                            <td><b>End Date&nbsp;&nbsp;&nbsp;</b> </td> <td align=left>
                             <div class='input-group date' id='datetimepicker4'>
                                                        <asp:TextBox    CssClass="form-control input-sm" placeholder="Arrival Date"     runat="server" ID="txtend"> </asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>
                                                    </div>
                            
                             </td><td>&nbsp;</td>
                        </tr>
                    

                             </table>
                            
                             </td><td>
                                 
                                 &nbsp;<a href="#" onClick="window.open('ExportExcelItems3.aspx','gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="75" height="75" border="0" /></a>


                                  </td>
                        </tr>


                        <tr>
                            <td><b> </b> </td> <td align=left>
                              
                                <asp:Button ID="btn" runat="server" class="btn green"  Text="Refresh" />

                                <div style="display:none"> 
                                    
                                    
                                </div>

                             </td><td>&nbsp;
                                 Show Committed only:&nbsp; <asp:CheckBox ID="chkrequested" AutoPostBack="true" runat="server" />
                            </td>
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
              


              <div class="note note-info">
                        <p>
                            <asp:Label id="lblInfo" runat="server"></asp:Label>
                        </p>
                    </div>
                    <hr />
            <div style='z-index: 90; height: 100%' class="table-responsive">
                <asp:HiddenField ID="gvSortDirection" runat="server" />
                <asp:GridView ID="gvItemsList" runat="server" CssClass="table table-striped table-bordered table-hover"
                    AllowSorting="true" AllowPaging="true" PageSize="50" AutoGenerateColumns="false" DataKeyNames="ItemID">
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                         <asp:BoundField HeaderText="Group Code" HeaderStyle-HorizontalAlign="Left" DataField="GroupCode"   />
                        <asp:BoundField HeaderText="Item ID" Visible="false" HeaderStyle-HorizontalAlign="Center" DataField="ItemID"  />

                         <asp:TemplateField  Visible="false" HeaderText="Item ID">
                            <ItemTemplate>
                                <asp:label ID="txtItemID" Text='<%#Eval("ItemID")%>'    runat="server"></asp:label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        
                        <asp:BoundField HeaderText="Item Name" Visible="false" HeaderStyle-HorizontalAlign="Left" DataField="ItemName"   />
                        <asp:BoundField HeaderText="Variety" HeaderStyle-HorizontalAlign="Left" DataField="Variety"   />
                        <asp:BoundField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" DataField="ItemDescription"   />
                        <asp:BoundField HeaderText="Grower" HeaderStyle-HorizontalAlign="Left" DataField="Location"   />

                        <asp:BoundField HeaderText="Category" HeaderStyle-HorizontalAlign="Left" DataField="ItemCategoryID"   />
                        <asp:BoundField HeaderText="Variety Code" HeaderStyle-HorizontalAlign="Left" DataField="VarietyID"   />
                        <asp:BoundField HeaderText="Color" HeaderStyle-HorizontalAlign="Left" DataField="ItemColor"   />
                        <asp:BoundField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" DataField="FlowerType"   />
                        <asp:BoundField HeaderText="Grade" HeaderStyle-HorizontalAlign="Left" DataField="Grade"   />
                         
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtstem" Text='<%#Eval("Qty")%>'   CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Price(USD)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtprice"  Text='<%#frmt(Eval("Price"))%>'  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Start Date"  >
                            <ItemTemplate>
					            <asp:label ID="lblstartavailabledate" Text='<%#Eval("startavailabledate")%>'    runat="server"></asp:label>
				            </ItemTemplate>
				     
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="End Date"  >
                            <ItemTemplate>
					            <asp:label ID="lblendavailabledate" Text='<%#Eval("endavailabledate")%>'    runat="server"></asp:label>
				            </ItemTemplate>
				     
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <br /> 

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


