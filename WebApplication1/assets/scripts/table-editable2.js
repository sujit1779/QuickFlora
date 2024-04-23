var TableEditable = function() {

    return {

        //main function to initiate the module
        init: function() {
            function restoreRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);
                var jqTds = $('>td', nRow);

                for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                    oTable.fnUpdate(aData[i], nRow, i, false);
                }




                oTable.fnDraw();
            }

            function editRow(oTable, nRow) {



                if (document.getElementById("ctl00_ContentPlaceHolder_drpReceiveby").value == '') {

                    alert("Please select value for Receive by");
                    return;

                }



                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);

                var txtitemid;
                var txtitemsearchDesc;
                var txtDesc;
                var txtComments;
                var color;
                var qty = 0;
                var RQty = 0;
                var typeUOM;
                var Pack = 0;
                var RPack = 0;
                var units = 0;
                var txtitemsearchprice;
                var total = 0.00;

                var RUnits = 0.00;
                
                txtitemid = aData[0];
                txtitemsearchDesc = aData[1];
                txtDesc = aData[2];
                txtComments = aData[3];
                // color = aData[4];
                qty = aData[4];
                typeUOM = aData[5];
                Pack = aData[6];
                units = aData[7];
                RQty = aData[8];
                RPack = aData[9];
                
                RUnits = aData[10];
                //txtitemsearchprice = aData[9];
                //total = aData[10];

                ShowDiv("newedititems");
                HideDiv("dvitems");

                HideDiv("new");
                ShowDiv("update");

                // alert(total);

                document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value = txtitemid;
                document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = txtitemsearchDesc;
                document.getElementById("ctl00_ContentPlaceHolder_txtDesc").value = txtDesc;
                document.getElementById("ctl00_ContentPlaceHolder_txtComments").value = txtComments;
                //document.getElementById("ctl00_ContentPlaceHolder_txtcolor").value = color;
                document.getElementById("ctl00_ContentPlaceHolder_txtQty").value = qty;
                document.getElementById("ctl00_ContentPlaceHolder_txtuom").value = typeUOM;
                document.getElementById("ctl00_ContentPlaceHolder_txtunits").value = units;
                document.getElementById("ctl00_ContentPlaceHolder_txtPack").value = Pack;
                if (RPack == "") {
                    document.getElementById("ctl00_ContentPlaceHolder_RPack").value = Pack;
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder_RPack").value = RPack;
                }
                
                document.getElementById("ctl00_ContentPlaceHolder_RQty").value = RQty;
                document.getElementById("ctl00_ContentPlaceHolder_prevReceived").value = RQty;
                document.getElementById("ctl00_ContentPlaceHolder_RUnits").value = RUnits;
                document.getElementById("ctl00_ContentPlaceHolder_prevRUnits").value = RUnits;

            }

            function editnewRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);

                // var jqInputs = $('input', nRow);

                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = '<input type="text"    value="' + aData[0] + '">';
                jqTds[1].innerHTML = '<input type="text"   value="' + aData[1] + '" readonly >';
                jqTds[2].innerHTML = '<input type="text"   value="' + aData[2] + '"  >';
                jqTds[3].innerHTML = '<input type="text"   value="' + aData[3] + '">';
                jqTds[4].innerHTML = '<input type="text"   value="' + aData[4] + '">';
                jqTds[5].innerHTML = '<input type="text"   value="' + aData[5] + '">';

                jqTds[6].innerHTML = '<input type="text"   value="' + aData[6] + '">';
                jqTds[7].innerHTML = '<input type="text"   value="' + aData[7] + '"    >';
                jqTds[8].innerHTML = '<input type="text"   value="' + aData[8] + '"    >';
                jqTds[9].innerHTML = '<input type="text"   value="' + aData[9] + '"    >';
                jqTds[10].innerHTML = '<input type="text"   value="' + aData[10] + '"  readonly >';

                jqTds[11].innerHTML = '<a class="edit btn btn-success btn-block btn-xs" href="">Save</a>';
                jqTds[12].innerHTML = '<a class="cancel btn btn-danger btn-block btn-xs"  data-mode="new"  href="">Cancel</a>';

                jqTds[13].innerHTML = aData[13];

                //alert(aData[10]);

                saveRow(oTable, nRow);


            }

            function saveRow(oTable, nRow) {
                var total = 0.00;
                var totalunits = 1;

                var jqInputs = $('input', nRow);


                totalunits = (jqInputs[4].value * jqInputs[6].value);
                total = totalunits * jqInputs[8].value;

                var aData = oTable.fnGetData(nRow);



                total = total.toFixed(2);

                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
                oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
                oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
                oTable.fnUpdate(jqInputs[6].value, nRow, 6, false);
                oTable.fnUpdate(jqInputs[7].value, nRow, 7, false);
                oTable.fnUpdate(jqInputs[8].value, nRow, 8, false);
                // oTable.fnUpdate(totalunits, nRow, 7, false);

                oTable.fnUpdate(jqInputs[9].value, nRow, 9, false);
                oTable.fnUpdate(jqInputs[10].value, nRow, 10, false);
                // oTable.fnUpdate(total, nRow, 9, false);

                oTable.fnUpdate('<a class="edit btn btn-info btn-block btn-xs" href="">Edit</a>', nRow, 11, false);
                oTable.fnUpdate('<a class="delete btn btn-danger btn-block btn-xs" href="">Delete</a>', nRow, 12, false);

                var ordernumber = "";
                ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;



                var qr = "";

                qr = "PurchaseNumber=" + ordernumber + "&ItemID=" + jqInputs[0].value + "&Description=" + jqInputs[2].value + "&Comments=" + jqInputs[3].value + "&Color=" + jqInputs[4].value + "&OrderQty=" + jqInputs[5].value + "&ItemUOM=" + jqInputs[6].value + "&Pack=" + jqInputs[7].value + "&Units=" + jqInputs[8].value + "&ItemUnitPrice=" + jqInputs[9].value + "&Total=" + jqInputs[10].value;

                // alert(qr);



                var inlineno = aData[13];

                // alert(inlineno);
                inlineno = inlineno.replace('input', "");
                inlineno = inlineno.replace('INPUT', "");
                inlineno = inlineno.replace('type', "");
                inlineno = inlineno.replace('hidden', "");
                inlineno = inlineno.replace('=', "");
                //  alert(inlineno);
                inlineno = inlineno.replace('value', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('>', "");
                inlineno = inlineno.replace('<', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace('=', "");
                inlineno = inlineno.replace('=', "");
                // alert(inlineno);

                if (inlineno == "") {
                    inlineno = generateUUID();
                    qr = qr + "&inlineno=" + inlineno;
                    Saveitem(qr, inlineno);

                }
                else {

                    qr = qr + "&inlineno=" + inlineno;
                    //alert(qr);
                    Updateitem(qr, inlineno);

                }



                oTable.fnUpdate('<input type="hidden" value="' + inlineno + '">', nRow, 13, false);

                //Main working row below
                //oTable.fnUpdate(inlineno, nRow, 10, false);


                oTable.fnDraw();
                document.getElementById("ctl00_ContentPlaceHolder_txtfirst").value = 0;
                nEditing = null;
            }


            function generateUUID() {
                var d = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
                    var r = (d + Math.random() * 16) % 16 | 0;
                    d = Math.floor(d / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            };


            function cancelEditRow(oTable, nRow) {
                var jqInputs = $('input', nRow);
                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
                oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
                oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
                oTable.fnUpdate(jqInputs[6].value, nRow, 6, false);
                oTable.fnUpdate(jqInputs[7].value, nRow, 7, false);
                oTable.fnUpdate(jqInputs[8].value, nRow, 8, false);
                oTable.fnUpdate(jqInputs[9].value, nRow, 9, false);
                oTable.fnUpdate(jqInputs[10].value, nRow, 10, false);
                oTable.fnUpdate('<a class="edit btn btn-info btn-block btn-xs" href="">Edit</a>', nRow, 11, false);
                oTable.fnDraw();
            }

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 200,

                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ],
                "order": [
                [12, "asc"]
                ]
            });

            jQuery('#sample_editable_1_wrapper .dataTables_filter input').addClass("form-control input-small"); // modify table search input
            jQuery('#sample_editable_1_wrapper .dataTables_length select').addClass("form-control input-small"); // modify table per page dropdown
            jQuery('#sample_editable_1_wrapper .dataTables_length select').select2({
                showSearchInput: false //hide search box with special css class
            }); // initialize select2 dropdown

            var nEditing = null;
            $('#sample_editable_1_cancel').click(function(e) {
                e.preventDefault();
                ShowDiv("dvitems");
                HideDiv("newedititems");

            });

            $('#sample_editable_1_update').click(function(e) {
                e.preventDefault();

                var txtitemid;
                var txtitemsearchprice = 0;
                var txtitemsearchDesc;
                var txtComments;
                var color = '';
                var typeUOM;
                var txtDesc;

                var Pack = 0;
                var RPack = 0;
                var units = 0;
                var qty = 0;
                var RQty = 0;
                var prevRQty = 0;
                var total = 0.00;

                var RUnits = 0.00;
                var prevUnits = 0.00;


                if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_RQty").value) == false) {
                    alert("Please Enter Valid received quantity!");
                    document.getElementById("ctl00_ContentPlaceHolder_RQty").focus();
                    return false;
                }

                if (IsNumeric(document.getElementById("ctl00_ContentPlaceHolder_RPack").value) == false) {
                    alert("Please Enter Valid received pack size!");
                    document.getElementById("ctl00_ContentPlaceHolder_RPack").focus();
                    return false;
                }
                qty = document.getElementById("ctl00_ContentPlaceHolder_txtQty").value;

                RQty = document.getElementById("ctl00_ContentPlaceHolder_RQty").value;
                RUnits = document.getElementById("ctl00_ContentPlaceHolder_RUnits").value;
                prevUnits = document.getElementById("ctl00_ContentPlaceHolder_prevRUnits").value;
                prevRQty = document.getElementById("ctl00_ContentPlaceHolder_prevReceived").value;
                units = document.getElementById("ctl00_ContentPlaceHolder_txtunits").value;
                if (!prevRQty) {
                    prevRQty = 0;
                }
                if (!prevUnits) {
                    prevUnits = 0;
                }
                console.log(prevRQty);
                var totalRe = parseInt(RQty) + parseInt(prevRQty);
                var TotalprevUnits = parseInt(RUnits) + parseInt(prevUnits);
                console.log(qty);
                console.log(totalRe);

                //if (parseInt(totalRe) > parseInt(qty)) {
                //    alert("Received quantity cannot be greater than Order quantity!");
                //    document.getElementById("ctl00_ContentPlaceHolder_RQty").focus();
                //    return false;
                //}

                if (parseInt(TotalprevUnits) > parseInt(units)) {
                    alert("Received uints cannot be greater than Order uints!");
                    document.getElementById("ctl00_ContentPlaceHolder_RQty").focus();
                    return false;
                }

                ShowDiv("dvitems");
                HideDiv("newedititems");




                txtitemid = document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value;

                txtitemsearchDesc = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value;

                txtDesc = document.getElementById("ctl00_ContentPlaceHolder_txtDesc").value;
                txtComments = document.getElementById("ctl00_ContentPlaceHolder_txtComments").value;

                //color = document.getElementById("ctl00_ContentPlaceHolder_txtcolor").value;
                typeUOM = document.getElementById("ctl00_ContentPlaceHolder_txtuom").value;


                Pack = document.getElementById("ctl00_ContentPlaceHolder_txtPack").value;
                
                RPack = document.getElementById("ctl00_ContentPlaceHolder_RPack").value;

                // alert(total);

                var nRow = nEditing;

                // alert(nRow);

                var aData = oTable.fnGetData(nRow);


                // alert(oTable);



                oTable.fnUpdate(txtitemid, nRow, 0, false);
                oTable.fnUpdate(txtitemsearchDesc, nRow, 1, false);
                oTable.fnUpdate(txtDesc, nRow, 2, false);
                oTable.fnUpdate(txtComments, nRow, 3, false);
                oTable.fnUpdate(qty, nRow, 4, false);
                oTable.fnUpdate(typeUOM, nRow, 5, false);
                oTable.fnUpdate(Pack, nRow, 6, false);
                oTable.fnUpdate(units, nRow, 7, false);
                oTable.fnUpdate(totalRe, nRow, 8, false);

                oTable.fnUpdate(RPack, nRow, 9, false);
                oTable.fnUpdate(TotalprevUnits, nRow, 10, false);

                oTable.fnUpdate('<a class="edit btn btn-info btn-block btn-xs" href="">Edit</a>', nRow, 11, false);

                oTable.fnUpdate(aData[12], nRow, 12, false);


                // alert(aData[13]);
                var ordernumber = "";
                ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;
                var drpReceiveby = "";
                drpReceiveby = document.getElementById("ctl00_ContentPlaceHolder_drpReceiveby").value;
                var txtVendorInvoiceNumber = "";
                txtVendorInvoiceNumber = document.getElementById("ctl00_ContentPlaceHolder_txtVendorInvoiceNumber").value;


                var qr = "";

                qr = "RUnits=" + RUnits + "&drpReceiveby=" + drpReceiveby + "&txtVendorInvoiceNumber=" + txtVendorInvoiceNumber + "&RPack=" + RPack + "&RQty=" + RQty + "&PurchaseNumber=" + ordernumber + "&ItemID=" + txtitemid + "&Description=" + txtDesc + "&Comments=" + txtComments + "&Color=" + color + "&OrderQty=" + qty + "&ItemUOM=" + typeUOM + "&Pack=" + Pack + "&Units=" + units + "&ItemUnitPrice=" + txtitemsearchprice + "&Total=" + total;

                //  alert(qr);



                var inlineno = aData[12];

                // alert(inlineno);
                inlineno = inlineno.replace('input', "");
                inlineno = inlineno.replace('INPUT', "");
                inlineno = inlineno.replace('type', "");
                inlineno = inlineno.replace('hidden', "");
                inlineno = inlineno.replace('=', "");
                //  alert(inlineno);
                inlineno = inlineno.replace('value', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('>', "");
                inlineno = inlineno.replace('<', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace('=', "");
                inlineno = inlineno.replace('=', "");
                // alert(inlineno);

                if (inlineno == "") {
                    inlineno = generateUUID();
                    qr = qr + "&inlineno=" + inlineno;
                    Saveitem(qr, inlineno);

                }
                else {

                    qr = qr + "&inlineno=" + inlineno;
                    // alert(qr);
                    Updateitem(qr, inlineno);

                }



                oTable.fnUpdate('<input type="hidden" value="' + inlineno + '">', nRow, 12, false);

                //Main working row below
                //oTable.fnUpdate(inlineno, nRow, 10, false);


                oTable.fnDraw();
                document.getElementById("ctl00_ContentPlaceHolder_txtfirst").value = 0;
                nEditing = null;

            });




            $('#sample_editable_1_new').click(function(e) {
                e.preventDefault();

                var txtitemid;
                var txtitemsearchprice;
                var txtitemsearchDesc;
                var txtComments;
                var color;
                var typeUOM;
                var txtDesc;

                var Pack = 0;
                var units = 0;
                var qty = 0;
                var total = 0.00;



                ShowDiv("dvitems");
                HideDiv("newedititems");




                txtitemid = document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value;
                txtitemsearchprice = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value;
                txtitemsearchDesc = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value;

                txtDesc = document.getElementById("ctl00_ContentPlaceHolder_txtDesc").value;
                txtComments = document.getElementById("ctl00_ContentPlaceHolder_txtComments").value;

                color = document.getElementById("ctl00_ContentPlaceHolder_txtcolor").value;
                typeUOM = document.getElementById("ctl00_ContentPlaceHolder_txtuom").value;


                Pack = document.getElementById("ctl00_ContentPlaceHolder_txtPack").value;
                units = document.getElementById("ctl00_ContentPlaceHolder_txtunits").value;
                qty = document.getElementById("ctl00_ContentPlaceHolder_txtQty").value;

                total = document.getElementById("ctl00_ContentPlaceHolder_txtitemTotal").value;


                var aiNew = oTable.fnAddData([txtitemid, txtitemsearchDesc, txtDesc, txtComments, color, qty, typeUOM, Pack, units, txtitemsearchprice, total,
                        '<a class="edit btn btn-info btn-block btn-xs" href="">Edit</a>', '<a class="cancel btn btn-danger btn-block btn-xs" data-mode="new" href="">Cancel</a>', ''
                ]);
                var nRow = oTable.fnGetNodes(aiNew[0]);
                editnewRow(oTable, nRow);
                nEditing = nRow;

            });

            $('#sample_editable_1 a.delete').live('click', function(e) {
                e.preventDefault();

                if (confirm("Are you sure to delete this row ?") == false) {
                    return;
                }

                var nRow = $(this).parents('tr')[0];

                var aData = oTable.fnGetData(nRow);




                var ordernumber = "";
                ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;

                var qr = "";

                qr = "PurchaseNumber=" + ordernumber + "&ItemID=" + aData[0];

                //alert(qr);



                var inlineno = aData[13];

                // alert(inlineno);
                inlineno = inlineno.replace('input', "");
                inlineno = inlineno.replace('INPUT', "");
                inlineno = inlineno.replace('type', "");
                inlineno = inlineno.replace('hidden', "");
                inlineno = inlineno.replace('=', "");
                //  alert(inlineno);
                inlineno = inlineno.replace('value', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('"', "");
                inlineno = inlineno.replace('>', "");
                inlineno = inlineno.replace('<', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace(' ', "");
                inlineno = inlineno.replace('=', "");
                inlineno = inlineno.replace('=', "");
                //   alert(inlineno);

                if (inlineno != "") {
                    qr = qr + "&inlineno=" + inlineno;
                    //alert(qr);
                    Deleteitem(qr, inlineno);

                }


                oTable.fnDeleteRow(nRow);
                //alert("Deleted! Do not forget to do some ajax to sync with backend :)");
            });

            $('#sample_editable_1 a.cancel').live('click', function(e) {
                e.preventDefault();
                global_item_add = 0;
                // alert(this.innerHTML);
                // alert($(this).attr("data-mode"));
                if ($(this).attr("data-mode") == "new") {
                    var nRow = $(this).parents('tr')[0];
                    oTable.fnDeleteRow(nRow);
                    nEditing = null;
                    //  alert("fnDeleteRow");
                } else {
                    restoreRow(oTable, nEditing);
                    nEditing = null;
                    //  alert("restoreRow");
                }
            });

            $('#sample_editable_1 a.edit').live('click', function(e) {
                e.preventDefault();


                // alert(this.innerHTML);
                if (true) {

                    var nRow = $(this).parents('tr')[0];

                    //  alert(nRow);

                    var T_txtfirst = "";
                    T_txtfirst = document.getElementById("ctl00_ContentPlaceHolder_txtfirst").value;

                    // alert(T_txtfirst);

                    if (T_txtfirst == "1") {


                        var ordernumber = "";
                        ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;


                        //  alert(ordernumber);

                        if (ordernumber == "(New)") {
                            alert("Please wait order number generation is in process....try again after 2 seconds");

                        }
                        else {
                            document.getElementById("ctl00_ContentPlaceHolder_txtfirst").value = 0;
                            nEditing = nRow;

                            saveRow(oTable, nEditing);
                            nEditing = null;
                        }





                    }
                    else {

                        /* Get the row as a parent of the link that was clicked on */


                        if (nEditing !== null && nEditing != nRow) {
                            /* Currently editing - but not this row - restore the old before continuing to edit mode */
                            // alert("1");
                            restoreRow(oTable, nEditing);
                            editRow(oTable, nRow);
                            nEditing = nRow;
                        } else if (nEditing == nRow && this.innerHTML == "Save") {
                            /* Editing this row and want to save it */
                            //  alert("2");
                            saveRow(oTable, nEditing);
                            nEditing = null;
                            //alert("Updated! Do not forget to do some ajax to sync with backend :)");

                        } else {
                            /* No edit in progress - let's start one */
                            //  alert("3");
                            editRow(oTable, nRow);
                            nEditing = nRow;
                        }


                    }


                }
                else {

                    alert("Please wait last item save in process...");
                }



            });
        }

    };

} ();




