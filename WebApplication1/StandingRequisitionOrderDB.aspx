<%@ Page Title="" Language="VB" MasterPageFile="~/MainMaster.master" ValidateRequest="false" AutoEventWireup="false" CodeFile="StandingRequisitionOrderDB.aspx.vb" Inherits="StandingRequisitionOrderDB" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Core" TagPrefix="core" %>
<%@ Register Assembly="EnterpriseASPClient" Namespace="EnterpriseASPClient.Controls" TagPrefix="ctls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />

    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
    <link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.css"></link>
    <link rel="stylesheet" type="text/css" href="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/skins/dhtmlxcalendar_dhx_skyblue.css"></link>
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


    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    Create new Standing Order
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

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


        function SetStatusColor(status, RowNumber) {
            if (status == "Pending-Stg email") {
                document.getElementById(RowNumber).style.backgroundColor = "#ffffff";
            }

            if (status == "Do Not Touch") {
                document.getElementById(RowNumber).style.backgroundColor = "#ccccff";
            }

            if (status == "Wed") {
                document.getElementById(RowNumber).style.backgroundColor = "#ffcccc";
            }

            if (status == "Not Avail") {
                document.getElementById(RowNumber).style.backgroundColor = "#cccccc";
            }

            if (status == "Pending") {
                document.getElementById(RowNumber).style.backgroundColor = "#99ff99";
            }

            if (status == "Bought") {
                document.getElementById(RowNumber).style.backgroundColor = "#99ffff";
            }

            if (status == "In Process") {
                document.getElementById(RowNumber).style.backgroundColor = "#ccffff";
            }

            if (status == "No Action") {
                document.getElementById(RowNumber).style.backgroundColor = "#ffff99";
            }
            if (status == "Pending Auction") {
                document.getElementById(RowNumber).style.backgroundColor = "#99ff99";
            }

            if (status == "Pending-Stg") {
                document.getElementById(RowNumber).style.backgroundColor = "#ccffcc";
            }
            if (status == "Wed In Process") {
                document.getElementById(RowNumber).style.backgroundColor = "#ffcccc";
            }
            if (status == "Bought Wed") {
                document.getElementById(RowNumber).style.backgroundColor = "#ffcccc";
            }
            if (status == "Pending Email") {
                document.getElementById(RowNumber).style.backgroundColor = "#ffffff";
            }

        }



        function Saveitem(This, RowNumber, name, oldvalue) {
            Initialize();

            This.style.backgroundColor = "";

            var url = "AjaxStandingPOSave.aspx?RowNumber=" + RowNumber + "&value=" + This.value + "&name=" + name;
            //alert(url);
            //alert(This.value);
            //alert(oldvalue);

            if (name == "Status") {

                SetStatusColor(This.value, RowNumber);
            }

            if (This.value == oldvalue) {
                return;
            }

            // alert(url);

            document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
            document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

            $.get(url, function(data, status) {
                //alert("Data: " + data + "\nStatus: " + status);
                document.getElementById("autosave").style.backgroundImage = 'url()';
            });

//            if (req != null) {
//                req.onreadystatechange = ProcessSave;
//                req.open("GET", url, true);
//                req.send(null);


//            }

        }

        
            
        function myFocusFunctiontotalnewqreq(idqreq,id0, RowNumber, id1, id2, id3,id4) {

            // alert(RowNumber);
            var QReqty = 0;
            var Qty = 0.00;
            var Pack = 0.00;
            var Cost = 0.00;

            //alert("-" + document.getElementById(id1).value + "-");
            QReqty = document.getElementById(idqreq).value;
            document.getElementById(id1).value = QReqty;

            document.getElementById(id1).value = $.trim(document.getElementById(id1).value);
            document.getElementById(id2).value = $.trim(document.getElementById(id2).value);
            document.getElementById(id3).value = $.trim(document.getElementById(id3).value);

            //alert("-" + document.getElementById(id1).value + "-");

            Qty = document.getElementById(id1).value;
            Pack = document.getElementById(id2).value;
            Cost = document.getElementById(id3).value;
            // alert(document.getElementById(id0).value);
             

            if ($.isNumeric(Qty))
            { }
            else {
               // alert('Please enter a numeric value for Qty for this row.');
                //document.getElementById(id1).focus();
                //document.getElementById(id1).click();
                //document.getElementById(id1).style.backgroundColor = "#ff0000";
                //Qty = 0;
                //return 0;
            }
            if ($.isNumeric(Pack))
            { }
            else {
                //alert('Please enter a numeric value for Pack for this row.');
//                document.getElementById(id2).focus();
//                document.getElementById(id2).click();
//                document.getElementById(id2).style.backgroundColor = "#ff0000";
//                Pack = 0;
//                return 0;
            }
            if ($.isNumeric(Cost))
            { }
            else {
                //alert('Please enter a numeric value for Cost for this row.');
//                document.getElementById(id3).focus();
//                document.getElementById(id3).click();
//                document.getElementById(id3).style.backgroundColor = "#ff0000";
//                Cost = 0;
//                return 0;
            }

            document.getElementById(id0).value = (Qty * Pack * Cost).toFixed(2);

            //  alert(document.getElementById(id0).value);

            //alert(id0.value);
            //idqreq
            Saveitemnew(idqreq, RowNumber, "Q_REQ", 0);
            Saveitemnew(id0, RowNumber, "Ext_COSt", 0);
            Saveitemnew(id1, RowNumber, "Q_ORD", 0);
            Saveitemnew(id2, RowNumber, "PACK", 0);
            Saveitemnew(id3, RowNumber, "COST", 0);
            Saveitemnew(id4, RowNumber, "Vendor_Code", document.getElementById(id4).value); 

            var url = "AjaxROTotal.aspx?PurchaseNumber=" + document.getElementById('ctl00_ContentPlaceHolder_txtoderno').value;
           // alert(url);

            $.get(url, function (data, status) {
                document.getElementById('ctl00_ContentPlaceHolder_txttotal').value = data;
                document.getElementById('ctl00_ContentPlaceHolder_lblsave2').innerHTML  = 'Total Amount: <b>$' + data + '<b/>';
             }); 

            //This.style.backgroundColor = "yellow";
        }

 //myFocusFunctiontotalnewqreq(key,RowNumber, id2, id3)
        function ItemNamesearch(id1,RowNumber, id2, id3, id4) {

            var key = "";
             key = document.getElementById(id1).value;
            Saveitemnew(id1, RowNumber, "Product", 0);
            if (key == "") {
                return 0;
            }
            var url = "AjaxItemsSearchName.aspx?k=" + key;
           // alert(url);

            $.get(url, function(data, status) {
                //alert("Data: " + data + "\nStatus: " + status);
                //document.getElementById("autosave").style.backgroundImage = 'url()';
                var str = data;
                var str1;
                str1 = str.split("~!");
                //str1[0];
                if (str1[0] != "Not Found") {
                    document.getElementById(id1 + "name").innerHTML = str1[0]; 
                    document.getElementById(id2).value = str1[1];
                    document.getElementById(id3).value = str1[2];
                    document.getElementById(id4).value = str1[3];
                    //alert(id2);
                    Saveitemnew(id2, RowNumber, "PACK", str1[1]);
                    Saveitemnew(id3, RowNumber, "COST", str1[2]);
                    Saveitemnew(id4, RowNumber, "Vendor_Code", str1[3]); 
                    }
            });             

        }


        function Saveitemnew(id0, RowNumber, name, oldvalue) {
            Initialize();

            document.getElementById(id0).style.backgroundColor = "";

            var url = "AjaxStandingPOSave.aspx?RowNumber=" + RowNumber + "&value=" + document.getElementById(id0).value + "&name=" + name;
            // alert(url);

            document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
            document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';

            $.get(url, function(data, status) {
                //alert("Data: " + data + "\nStatus: " + status);
                document.getElementById("autosave").style.backgroundImage = 'url()';
            });

//            if (req != null) {
//                req.onreadystatechange = ProcessSave;
//                req.open("GET", url, true);
//                req.send(null);


//            }

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

        function myFocusFunctiontotal(id0, RowNumber, id1, id2, id3) {

            // alert(RowNumber);

            var Qty = 0.00;
            var Pack = 0.00;
            var Cost = 0.00;

            //alert("-" + document.getElementById(id1).value + "-");

            document.getElementById(id1).value = $.trim(document.getElementById(id1).value);
            document.getElementById(id2).value = $.trim(document.getElementById(id2).value);
            document.getElementById(id3).value = $.trim(document.getElementById(id3).value);

            //alert("-" + document.getElementById(id1).value + "-");

            Qty = document.getElementById(id1).value;
            Pack = document.getElementById(id2).value;
            Cost = document.getElementById(id3).value;
            // alert(document.getElementById(id0).value);


            if ($.isNumeric(Qty))
            { }
            else {
               // alert('Please enter a numeric value for Qty for this row.');
                //document.getElementById(id1).focus();
                //document.getElementById(id1).click();
                //document.getElementById(id1).style.backgroundColor = "#ff0000";
                //Qty = 0;
                //return 0;
            }
            if ($.isNumeric(Pack))
            { }
            else {
                //alert('Please enter a numeric value for Pack for this row.');
//                document.getElementById(id2).focus();
//                document.getElementById(id2).click();
//                document.getElementById(id2).style.backgroundColor = "#ff0000";
//                Pack = 0;
//                return 0;
            }
            if ($.isNumeric(Cost))
            { }
            else {
                //alert('Please enter a numeric value for Cost for this row.');
//                document.getElementById(id3).focus();
//                document.getElementById(id3).click();
//                document.getElementById(id3).style.backgroundColor = "#ff0000";
//                Cost = 0;
//                return 0;
            }

            document.getElementById(id0).value = (Qty * Pack * Cost).toFixed(2);

            //  alert(document.getElementById(id0).value);

            //alert(id0.value);

            Saveitemnew(id0, RowNumber, "Ext_COSt", 0);
            Saveitemnew(id1, RowNumber, "Q_ORD", 0);
            Saveitemnew(id2, RowNumber, "PACK", 0);
            Saveitemnew(id3, RowNumber, "COST", 0);



            //This.style.backgroundColor = "yellow";
        }


        function ProcessSave() {

            // alert(document.getElementById("autosave"));


            document.getElementById("autosave").style.backgroundImage = 'url(progress_bar.gif)';
            document.getElementById("autosave").style.backgroundRepeat = 'no-repeat';


            if (req.readyState == 4) {
                // only if "OK"
                // alert(req.readyState);

                if (req.status == 200) {
                    document.getElementById("autosave").style.backgroundImage = 'url()';
                    //  alert(req.status);

                    if (req.responseText == "") {


                    }
                    else {
                        //alert(req.responseText);
                        //  document.getElementById("autosave").innerHTML = "Data Saved For RowNumber:" + req.responseText;
                    }
                }
                else {
                    document.getElementById("autosave").innerHTML = "There was a problem saving data:" + req.responseText;

                }
            }
        }



        function SendQuery(key, This, RowNumber) {
            //alert(This.id);
            //alert(RowNumber);

            var str = key;
            var n = str.length;
            if (n < 2) {
                return 0;
            }
            Initialize();
            var url = "AjaxVendorSearchNew.aspx?k=" + key + "&id=" + This.id + "&RowNumber=" + RowNumber;
            //alert(url);

            stop = 1;

            if (req != null) {
                req.onreadystatechange = function() {
                    ProcessSearch(RowNumber);
                };
                req.open("GET", url, true);
                req.send(null);

            }

        }

        function SendQuery2(key, This, RowNumber, txtPACK_ClientID, txtCOST_ClientID) {
            //alert(This.id);
            //alert(RowNumber);

            RowNumber = "2" + RowNumber;

            var str = key;
            var n = str.length;
            if (n < 2) {
                return 0;
            }
            Initialize();
            var url = "AjaxItemsSearchNew.aspx?k=" + key + "&id=" + This.id + "&RowNumber=" + RowNumber + "&txtPACK_ClientID=" + txtPACK_ClientID + "&txtCOST_ClientID=" + txtCOST_ClientID;
            // alert(url);

            stop = 1;

            if (req != null) {
                req.onreadystatechange = function() {
                    ProcessSearch(RowNumber);
                };
                req.open("GET", url, true);
                req.send(null);

            }

        }


        function ProcessSearch(RowNumber) {
            //alert("In ProcessSearch");
            //alert(RowNumber);

            if (req.readyState == 4) {
                if (req.status == 200) {

                    //alert("Result : " +  req.responseText);
                    var id;
                    id = "autocomplete" + RowNumber;
                    ShowDiv(id);

                    var newdiv = document.createElement("div");
                    newdiv.innerHTML = req.responseText;
                    //alert(newdiv.innerHTML);
                    var container = document.getElementById(id);
                    var newid = "";
                    newid = "'" + id + "'";
                    container.innerHTML = '<div style="text-align:center" ><a class="btn btn-danger btn-xs" href="Javascript:CcustomersearchcloseProcess(' + newid + ');" >Close</a></div>';
                    container.appendChild(newdiv)

                }
                else {
                    alert("There was a problem saving data:<br>" + req.statusText);
                }
            }

        }

        function CcustomersearchcloseProcess(id) {
            //alert(CcustomersearchcloseProcess);
            stop = 0;
            document.getElementById(id).innerHTML = "";
            //document.getElementById("txtOrderSearch.ClientID").value = "";
            HideDiv(id);


        }

        function FillSearchtextBox(val, ID, DIVID) {
            // alert(val);
            document.getElementById(ID).value = val;
            document.getElementById("autocomplete" + DIVID).innerHTML = "";
            document.getElementById(ID).focus();
            document.getElementById(ID).click();
            HideDiv("autocomplete" + DIVID);

            //ImgUpdateSearchitems
        }

        function FillSearchtextBoxnew(val, ID, DIVID, val1, val2, id1, id2, nm) {
            // alert(val);
            document.getElementById(ID).value = val;
            document.getElementById(ID + "name").innerHTML = nm;
            

            document.getElementById(id1).value = val1;
            document.getElementById(id2).value = val2;
            document.getElementById("autocomplete" + DIVID).innerHTML = "";
            document.getElementById(ID).focus();
            document.getElementById(ID).click();
            HideDiv("autocomplete" + DIVID);

            //ImgUpdateSearchitems
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


    <ajax:AjaxPanel ID="AjaxPanel101" runat="server">

        <!-- BEGIN PORTLET 1st Block-->
        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    &nbsp;Requisition Details
                </div>
                <div class="tools">
                    <a href="javascript:;" class="collapse"></a>
                </div>
            </div>

            <div class="portlet-body">
                <div class="row">
                    <div class="col-xs-12" style="text-align:left;">
                          
                    </div> 
                </div> 
                

                <div class="note note-success margin-bottom-0">

                    <div class="row">
                        <div class="col-xs-3">
                            Requisition No.
                            <asp:TextBox ID="txtoderno" CssClass="form-control input-sm" Text="(New)" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-3">
                            Location 
                            <asp:DropDownList ID="cmblocationid" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Location--" Value=""></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-xs-3">

                            Inventory Origin 
                            <asp:DropDownList ID="drpInventoryOrigin" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Origin--" Value=""></asp:ListItem>
                            </asp:DropDownList>


                        </div>
                        <div class="col-xs-3">
                            
                            Ship Method:
                              <asp:DropDownList ID="drpshipemthod" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Ship Method--" Value=""></asp:ListItem>   
                            </asp:DropDownList> 

                            
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                           Ship Date:
                             
                            <div class="form-group">
                                <div class='input-group date' id='datetimepicker3'>
                                    <asp:TextBox ID="txtshipdate" CssClass="form-control input-sm"  AutoPostBack="true" autocomplete="off" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-3">
                            Arrive Date:
                             
                            <div class="form-group">
                                <div class='input-group date' id='datetimepicker5'>
                                    <asp:TextBox ID="txtarrivedate" CssClass="form-control input-sm"   autocomplete="off" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            
                            
                           
                        </div>
                        <div class="col-xs-3">
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
                        <div class="col-xs-3">
                         
                            Remarks:
                            <asp:TextBox ID="txtRemarks" CssClass="form-control input-sm" autocomplete="off" runat="server"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            Last Change At:
                            
                            <div class="form-group">
                                <div class='input-group date' id='datetimepicker2'>
                                    <asp:TextBox ID="txtlastchanged" Enabled="false" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-3">
                            Last Change By:
                            <asp:TextBox ID="txtlastchangedby" Enabled="false" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-3">
                         Order Placed:
                             
                            <div class="form-group">
                                <div class='input-group date' id='datetimepicker4'>
                                    <asp:TextBox ID="txtorderplaced" CssClass="form-control input-sm" Enabled="false" autocomplete="off" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>

                            
                        </div>
                        <div class="col-xs-3">
                               Order by:
                            <asp:TextBox ID="txtorderby" CssClass="form-control input-sm" autocomplete="off" Enabled="false"  runat="server"></asp:TextBox>
   
                            
                        </div>

                    </div>
                    <div class="row">
                        <div runat="server" id="dv1" visible="false"  class="col-xs-2">
                            Recv On:
                             
                            <div class="form-group">
                                <div class='input-group date' id='datetimepicker6'>
                                    <asp:TextBox ID="txtrecvon" CssClass="form-control input-sm" autocomplete="off" runat="server"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>  
                         
                            
                        </div>
                        <div runat="server" id="dv2" visible="false"  class="col-xs-2">
                            Recv by:
                            <asp:TextBox ID="txtrecvby" CssClass="form-control input-sm" autocomplete="off" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-3">
                            Total Amount:
                            <asp:TextBox ID="txttotal" CssClass="form-control input-sm" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-xs-3">
                            
                            Status
                             <asp:DropDownList ID="drpStatus" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Text="--Select Status--" Value=""></asp:ListItem>
                                <asp:ListItem Text="Buying Completed" Value="Buying Completed"></asp:ListItem>                             						
                                <asp:ListItem Text="Buying In Process" Value="Buying In Process"></asp:ListItem>
                                <asp:ListItem Text="Entry Completed" Value="Entry Completed"></asp:ListItem>
                                <asp:ListItem Text="Entry In Process" Selected="True" Value="Entry In Process"></asp:ListItem>
                                <asp:ListItem Text="Received" Value="Received"></asp:ListItem>
                                <asp:ListItem Text="Stg Completed" Value="Stg Completed"></asp:ListItem>
                                <asp:ListItem Text="Stg In Process" Value="Stg In Process"></asp:ListItem>
                            </asp:DropDownList>


                        </div>
                        
                        <div class="col-xs-3">
                            Create on Every:
                            <asp:DropDownList ID="drpCreateDay" CssClass="form-control input-sm select2me" runat="server" AppendDataBoundItems="true" AutoPostBack="False" >
                                             
                                            <asp:ListItem Text="1 [Monday]" Value="1" />
                                            <asp:ListItem Text="2 [Tuesday]" Value="2" />
                                            <asp:ListItem Text="3 [Wednesday]" Value="3" />
                                            <asp:ListItem Text="4 [Thursday]" Value="4" />
                                            <asp:ListItem Text="5 [Friday]" Value="5" />
                                            <asp:ListItem Text="6 [Saturday]" Value="6" />
                                            <asp:ListItem Text="7 [Sunday]" Value="7" />
                             </asp:DropDownList>
                        </div> 
                   
                        <div class="col-xs-3">
                             
                        </div> 
                        
                         </div>


                </div>


            </div>
        </div>
        <!-- END PORTLET-->

        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption">
                    &nbsp;Products Details
                </div>
                <div class="tools">
                    <a href="javascript:;" class="collapse"></a>
                </div>
            </div>
 
            <div class="portlet-body">
                <div class="row">
                    <div style="text-align: left; padding-bottom:10px;" class="col-xs-12">
                          <asp:Button CssClass="btn btn-success btn-xs" ID="btnaddnew" runat="server" Text="Add New" />&nbsp;&nbsp;<asp:Button CssClass="btn btn-warning btn-xs" ID="btnDelete" runat="server" Text="Delete" />&nbsp;&nbsp;<asp:Button CssClass="btn btn-success btn-xs" ID="btnLoadProductList" runat="server" Text="Load Product List" />  
                        <asp:Button ID="Button1" CssClass="btn btn-success btn-xs" runat="server" Text="Button" />
                        &nbsp;&nbsp;<asp:Button CssClass="btn btn-success btn-xs" ID="btnFlowers" runat="server" Text="Load Flowers" />  
                        <asp:Button ID="Button2" CssClass="btn btn-success btn-xs" runat="server" Text="Button" />
                        &nbsp;&nbsp;<asp:Button CssClass="btn btn-success btn-xs" ID="btnGreens" runat="server" Text="Load Greens" />  
                        <asp:Button ID="Button3" CssClass="btn btn-success btn-xs" runat="server" Text="Button" />
                        &nbsp;&nbsp;<asp:Button CssClass="btn btn-success btn-xs" ID="btnHardgoods" runat="server" Text="Load Hardgoods" />  
                        <asp:Button ID="Button4" CssClass="btn btn-success btn-xs" runat="server" Text="Button" />
                        &nbsp;&nbsp;<asp:Button CssClass="btn btn-success btn-xs" ID="btnPlants" runat="server" Text="Load Plants" /> 
                        <asp:Button ID="Button5" CssClass="btn btn-success btn-xs" runat="server" Text="Button" />
                        &nbsp;&nbsp;<a onClick="window.open('ExportExcelSTROItems.aspx?OrderNo=' + document.getElementById('ctl00_ContentPlaceHolder_txtoderno').value,'gotFusion','toolbar=1,location=1,directories=0,status=0,menubar=1,scrollbars=1,resizable=1,copyhistory=0,width=800,height=600,left=0,top=0')" href="#" ><img  alt="excel" src="https://secure.quickflora.com/images/excel.png" width="75" height="75" border="0" /></a>
                         &nbsp;&nbsp;<asp:Button ID="btnsavechangesUP" CssClass="btn btn-success btn-xs" runat="server" Text=" Save Changes" />&nbsp;&nbsp; <asp:Button ID="btnsaveUP"  OnClientClick="return validateForm();" CssClass="btn btn-success btn-xs" runat="server" Text=" Submit " />&nbsp;&nbsp;<asp:Button CssClass="btn btn-warning btn-xs" ID="btncloseUP" runat="server" Text="Close" />
                    </div>
                </div>
               
                <div class="row">
                    <div style="text-align: center; padding-top:10px;" class="col-xs-12">
                      
                    </div>
                </div>
                 <div class="row">
                    <div style="text-align:center; padding-bottom:10px;" class="col-xs-12">
                        <asp:Label ID="lblsavechanges" runat="server" Visible="false" Text=""></asp:Label>
                    </div> 
                </div> 
                <div  style='z-index:90;height:100%' class="table-responsive">

                
                <asp:GridView ID="OrderHeaderGrid" ShowHeaderWhenEmpty="true" AllowSorting="True" runat="server" DataKeyNames="InLineNumber" CssClass="table table-bordered table-striped table-condensed flip-content" AutoGenerateColumns="false" AllowPaging="True" PageSize="25">
                    <Columns>
                        <asp:TemplateField   HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkseleect" runat="server" />
                            </ItemTemplate>
                         </asp:TemplateField>

                        <asp:TemplateField Visible="false"  HeaderText="InLineNumber">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtInLineNumber" Text='<%#Eval("InLineNumber")%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Product">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" autocomplete="off" Width="180px" ID="txtProduct" Text='<%#Eval("Product")%>' runat="server"></asp:TextBox>
                                <div id="autocomplete2<%#Eval("InLineNumber")%>"  align="left" class="box autocompletenew" style="visibility: hidden;"  > </div>
                                <asp:Label ID="txtProductname" Text=''  runat="server"></asp:Label>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="QOH">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtQOH" Text='<%#Eval("QOH")%>' Width="40px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Dump">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtDUMP" Text='<%#Eval("DUMP")%>' Width="40px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Q Req">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtQ_REQ" Text='<%#Eval("Q_REQ")%>' Width="40px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pre Sold">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtPRESOLD" Text='<%#Eval("PRESOLD")%>' Width="40px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Color Variety">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" Width="150px" ID="txtCOLOR_VARIETY" Text='<%#Eval("COLOR_VARIETY")%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                         </asp:TemplateField>

                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" Width="200px" ID="txtREMARKS" Text='<%#Eval("REMARKS")%>' runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q Ord">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtQ_ORD" Text='<%#Eval("Q_ORD")%>' Width="50px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pack">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtPACK" Text='<%#Eval("PACK")%>' Width="40px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtCOST" Text='<%#Eval("COST")%>' Width="70px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ext.Cost">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtExt_COSt" Text='<%#Eval("Ext_COSt")%>'  Width="100px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor Code">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtVendor_Code" autocomplete="off" Width="180px" Text='<%#Eval("Vendor_Code")%>' runat="server"></asp:TextBox>
                                <div id="autocomplete<%#Eval("InLineNumber")%>"  align="left" class="box autocompletenew" style="visibility: hidden;"  > </div>
                            </ItemTemplate>
                              
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Buyer">
                            <ItemTemplate>
                                <asp:Label  ID="txtBuyer" ReadOnly="True"  Text='<%#Eval("Buyer")%>' runat="server"></asp:Label>
                                <asp:DropDownList ID="drpBuyer" CssClass="form-control input-sm" Width="100px" Visible="false"  runat="server">
 
                                     
                                </asp:DropDownList>   
                            </ItemTemplate>

                              
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label   ID="txtStatus" ReadOnly="True" Text='<%#Eval("Status")%>' runat="server"></asp:Label>
                                <asp:DropDownList ID="drpPOStatus" CssClass="form-control input-sm" Visible="false" Width="100px" runat="server">
                                    <asp:ListItem Text="--select--" Value=""></asp:ListItem>
                 		            <asp:ListItem Text="Bought" Value="Bought"></asp:ListItem>
                 		            <asp:ListItem Text="Bought Wed" Value="Bought Wed"></asp:ListItem>
                                    <asp:ListItem Text="Do Not Touch" Value="Do Not Touch"></asp:ListItem>  
                                    <asp:ListItem Text="In Process" Value="In Process"></asp:ListItem>                                    
                                    <asp:ListItem Text="No Action" Selected="True" Value="No Action"></asp:ListItem>                                    
                 		            <asp:ListItem Text="Not Avail" Value="Not Avail"></asp:ListItem>
                                    <asp:ListItem Text="Pass" Value="Pass"></asp:ListItem>
                                    <asp:ListItem Text="Pass email" Value="Pass email"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                    <asp:ListItem Text="Pending Auction" Value="Pending Auction"></asp:ListItem>
                                    <asp:ListItem Text="Pending email" Value="Pending eml"></asp:ListItem>                                  		
                                    <asp:ListItem Text="Pending-Stg" Value="Pending-Stg"></asp:ListItem>
                                    <asp:ListItem Text="Pending-Stg email" Value="Pending-Stg email"></asp:ListItem>
                                    <asp:ListItem Text="Wed" Value="Wed"></asp:ListItem>
                                    <asp:ListItem Text="Wed In Process" Value="Wed In Process"></asp:ListItem>
                                    <asp:ListItem Text="With-Other" Value="With-Other"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                             
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q Recv">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtQ_Recv" Text='<%#Eval("Q_Recv")%>' Width="40px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issue">
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control input-sm" ID="txtISSUE" Text='<%#Eval("ISSUE")%>' Width="40px" runat="server"></asp:TextBox>
                            </ItemTemplate>
                             
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                </div> 
                <div class="row" ID="dvsave"  runat="server" >
                    <div style="text-align:center; padding-top:10px;" class="col-xs-12">
                            <div class="alert alert-success">
                                <asp:Label ID="lblsave"  runat="server" Text=""></asp:Label>
                            </div> 
                    </div> 
                </div>
                 <div class="row"  >
                    <div style="text-align:center; padding-top:0px;" class="col-xs-12">
                            <div class="alert alert-success">
                                <asp:Label ID="lblsave2"  runat="server" Text=""></asp:Label>
                            </div> 
                    </div> 
                </div>
                <div class="row" ID="dvsavealert"  visible="false"  runat="server" >
                    <div style="text-align:center; padding-top:10px;" class="col-xs-12">
                            <div class="alert alert-danger">
                                <asp:Label ID="lblsavealert"  runat="server" Text=""></asp:Label>
                            </div> 
                    </div> 
                </div>

                <div class="row">
                    <div style="text-align: center; padding-top:10px;" class="col-xs-12">
                       <asp:Button ID="btnsavechanges" CssClass="btn btn-success btn-xs" runat="server" Text=" Save Changes" />&nbsp;&nbsp; <asp:Button ID="btnsave"  OnClientClick="return validateForm();" CssClass="btn btn-success btn-xs" runat="server" Text=" Submit " />&nbsp;&nbsp;<asp:Button CssClass="btn btn-warning btn-xs" ID="btnclose" runat="server" Text="Close" />
                    </div>
                </div>
                 
                <div   id="autosave">
                   <br />
                     <br />
                 </div>

                <asp:LinkButton ID="btnarrivedate" Width="0" ToolTip="Update arrivedate" ImageUrl="~/images/2-sh-stock-in.gif"  runat="server" /> 
            </div>
        </div>



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
        jQuery(document).ready(function() {

            Search.init();
        });
        
    </script>
    

    
    <script type="text/javascript" src="https://secure.quickflora.com/Admin/EnterpriseASPAR/CustomOrder/codebase/dhtmlxcalendar.js"></script>
    <script type="text/javascript" >

        function FillArrivaldate() {
            // alert('hi');
            document.getElementById("<%=txtshipdate.ClientID%>").focus();
            document.getElementById("<%=txtshipdate.ClientID%>").click();
            //ImgUpdateSearchitems
        }

        function FillArrivaldate2() {
            alert('hi');
            document.getElementById("<%=btnarrivedate.ClientID%>").focus();
            document.getElementById("<%=btnarrivedate.ClientID%>").click();
            //ImgUpdateSearchitems
        }

        //alert('test');
        doOnLoad();
        var myCalendar;
        var myCalendar3;
        var myCalendar4;
        var myCalendar5;
        function doOnLoad() {
            //alert('test');
            myCalendar = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtlastchanged"]);
            //alert(myCalendar);
            myCalendar.setDateFormat("%m/%d/%Y");
            myCalendar.setSensitiveRange("3/20/2017", null);
            myCalendar3 = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtorderplaced"]);
            myCalendar3.setDateFormat("%m/%d/%Y");
            myCalendar3.setSensitiveRange("3/20/2017", null);
            //myCalendar4 = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtarrivedate"]);
            //myCalendar4.setDateFormat("%m/%d/%Y");
            myCalendar4.setSensitiveRange("3/20/2017", null);
            myCalendar5 = new dhtmlXCalendarObject(["ctl00_ContentPlaceHolder_txtrecvon"]);
            myCalendar5.setDateFormat("%m/%d/%Y");
            myCalendar5.setSensitiveRange("3/20/2017", null);
        }
</script>
 

    <script type="text/javascript" >
        function validateForm() {
            var x0 = document.getElementById('<%=drpInventoryOrigin.ClientID%>').value;
            if (x0 == "") {
                alert("Please select Inventory Origin.");
                document.getElementById('<%=drpInventoryOrigin.ClientID%>').focus();
                document.getElementById('<%=drpInventoryOrigin.ClientID%>').click();
                document.getElementById('<%=drpInventoryOrigin.ClientID%>').style.backgroundColor = "yellow";
                return false;
            }
            else { document.getElementById('<%=drpInventoryOrigin.ClientID%>').style.backgroundColor = ""; }

            var x1 = document.getElementById('<%=drpshipemthod.ClientID%>').value;
            if (x1 == "") {
                alert("Please select Ship Method.");
                document.getElementById('<%=drpshipemthod.ClientID%>').focus();
                document.getElementById('<%=drpshipemthod.ClientID%>').click();
                document.getElementById('<%=drpshipemthod.ClientID%>').style.backgroundColor = "yellow";
                return false;
            }
            else { document.getElementById('<%=drpshipemthod.ClientID%>').style.backgroundColor = ""; }

            var x = document.getElementById('<%=txtshipdate.ClientID%>').value;
            if (x == "") {
                alert("Please provide valid Ship Date");
                document.getElementById('<%=txtshipdate.ClientID%>').focus();
                document.getElementById('<%=txtshipdate.ClientID%>').click();
                document.getElementById('<%=txtshipdate.ClientID%>').style.backgroundColor = "yellow";
                return false;
            }
            else { document.getElementById('<%=txtshipdate.ClientID%>').style.backgroundColor = ""; }

            var x2 = document.getElementById('<%=txtarrivedate.ClientID%>').value;
            if (x2 == "") {
                alert("Please provide valid Arrive Date");
                document.getElementById('<%=txtarrivedate.ClientID%>').focus();
                document.getElementById('<%=txtarrivedate.ClientID%>').click();
                document.getElementById('<%=txtarrivedate.ClientID%>').style.backgroundColor = "yellow";
                return false;
            }
            else { document.getElementById('<%=txtarrivedate.ClientID%>').style.backgroundColor = ""; }

            //drpType

            var x4 = document.getElementById('<%=drpType.ClientID%>').value;
            if (x4 == "") {
                alert("Please select request Type.");
                document.getElementById('<%=drpType.ClientID%>').focus();
                document.getElementById('<%=drpType.ClientID%>').click();
                document.getElementById('<%=drpType.ClientID%>').style.backgroundColor = "yellow";
                return false;
            }
            else { document.getElementById('<%=drpType.ClientID%>').style.backgroundColor = ""; }


            return true;
        }
    </script>

 <script type="text/javascript">
     function AvoidWrite(evt) {
         // alert('hi');
         var x0 = document.getElementById('<%=drpInventoryOrigin.ClientID%>').value;
         if (x0 == "") {
             alert("Please select Inventory Origin.");
             document.getElementById('<%=drpInventoryOrigin.ClientID%>').focus();
             document.getElementById('<%=drpInventoryOrigin.ClientID%>').click();
             document.getElementById('<%=drpInventoryOrigin.ClientID%>').style.backgroundColor = "yellow";
             //return false;
         }
         else { document.getElementById('<%=drpInventoryOrigin.ClientID%>').style.backgroundColor = ""; }

         var x1 = document.getElementById('<%=drpshipemthod.ClientID%>').value;
         if (x1 == "") {
             alert("Please select Ship Method.");
             document.getElementById('<%=drpshipemthod.ClientID%>').focus();
             document.getElementById('<%=drpshipemthod.ClientID%>').click();
             document.getElementById('<%=drpshipemthod.ClientID%>').style.backgroundColor = "yellow";
             //return false;
         }
         else { document.getElementById('<%=drpshipemthod.ClientID%>').style.backgroundColor = ""; }
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode < 48 || charCode > 57)
             return false;
         else {
             return false;
         }
     }   
    </script>

 

</asp:Content>

