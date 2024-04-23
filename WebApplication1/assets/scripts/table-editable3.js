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

                nEditing = nRow;

                var aData = oTable.fnGetData(nRow);

                var jqInputs = $('input', nRow);

                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = '<input type="text"    value="' + aData[0] + '">';
                jqTds[1].innerHTML = '<input type="text"   value="' + aData[1] + '" readonly>';
                jqTds[2].innerHTML = '<input type="text"   value="' + aData[2] + '"  >';
                jqTds[3].innerHTML = '<input type="text"   value="' + aData[3] + '">';
                jqTds[4].innerHTML = '<input type="text"   value="' + aData[4] + '">';

                jqTds[5].innerHTML = '<a class="edit btn btn-success btn-block btn-xs" href="">Save</a>';
                jqTds[6].innerHTML = '<a class="cancel btn btn-danger btn-block btn-xs"  href="">Cancel</a>';

                jqTds[7].innerHTML = aData[7];

                //alert(aData[10]);



            }

            function editnewRow(oTable, nRow) {
                var aData = oTable.fnGetData(nRow);

                // alert("hi in edit ");
                // var jqInputs = $('input', nRow);

                var jqTds = $('>td', nRow);
                jqTds[0].innerHTML = '<input type="text"    value="' + aData[0] + '" readonly >';
                jqTds[1].innerHTML = '<input type="text"   value="' + aData[1] + '"  readonly >';
                jqTds[2].innerHTML = '<input type="text"   value="' + aData[2] + '"  >';
                jqTds[3].innerHTML = '<input type="text"   value="' + aData[3] + '" >';
                jqTds[4].innerHTML = '<input type="text"   value="' + aData[4] + '" readonly >';


                jqTds[5].innerHTML = '<a class="edit btn btn-success btn-block btn-xs" href="">Save</a>';
                jqTds[6].innerHTML = '<a class="cancel btn btn-danger btn-block btn-xs"  data-mode="new"  href="">Cancel</a>';

                jqTds[7].innerHTML = aData[7];

                //alert(aData[10]);

                // saveRow(oTable, nRow);


            }

            function saveRow(oTable, nRow) {
                var total = 0.00;

                var jqInputs = $('input', nRow);


                total = (jqInputs[2].value * jqInputs[3].value);


                var aData = oTable.fnGetData(nRow);



                total = total.toFixed(2);

                //alert(total);

                oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
                oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
                oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
                oTable.fnUpdate(total, nRow, 4, false);


                oTable.fnUpdate('<a class="edit btn btn-info btn-block btn-xs" href="">Edit</a>', nRow, 5, false);
                oTable.fnUpdate('<a class="delete btn btn-danger btn-block btn-xs" href="">Delete</a>', nRow, 6, false);

                var ordernumber = "";
                ordernumber = document.getElementById("ctl00_ContentPlaceHolder_txtOrderNumber").value;



                var qr = "";

                qr = "InventoryAdjustmentsNumber=" + ordernumber + "&ItemID=" + jqInputs[0].value + "&name=" + jqInputs[1].value + "&qty=" + jqInputs[2].value + "&cost=" + jqInputs[3].value + "&total=" + total;

               // alert(qr);



                var inlineno = aData[7];

                //  alert(inlineno);
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
                   // alert(qr);
                     Saveitem(qr, inlineno);

                }
                else {

                    qr = qr + "&inlineno=" + inlineno;
                    //alert(qr);
                     Updateitem(qr, inlineno);

                }



                oTable.fnUpdate('<input type="hidden" value="' + inlineno + '">', nRow, 7, false);

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
                oTable.fnUpdate('<a class="edit btn btn-info btn-block btn-xs" href="">Edit</a>', nRow, 5, false);
                oTable.fnDraw();
            }

            var oTable = $('#sample_editable_1').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 50,

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
                [7, "desc"]
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




            $('#sample_editable_1_new').click(function(e) {
                e.preventDefault();


                var txtitemid;
                var txtitemsearchprice;
                var txtitemsearchDesc;

                txtitemid = document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value;
                txtitemsearchDesc = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value;
                txtitemsearchprice = document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value;



                var aiNew = oTable.fnAddData([txtitemid, txtitemsearchDesc, 0, txtitemsearchprice, 0,
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

                qr = "InventoryAdjustmentsNumber=" + ordernumber + "&ItemID=" + aData[0];

                //alert(qr);



                var inlineno = aData[7];

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




