<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="AjaxItemsAdd.aspx.vb" Inherits="AjaxItemsAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="Server">
 <!-- BEGIN GLOBAL MANDATORY STYLES -->
<link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
<link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
<link href="assets/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css"/>
<!-- END GLOBAL MANDATORY STYLES -->
<!-- BEGIN PAGE LEVEL STYLES -->
<link rel="stylesheet" type="text/css" href="assets/plugins/select2/select2_metro.css"/>
<link rel="stylesheet" href="assets/plugins/data-tables/DT_bootstrap.css"/>
<!-- END PAGE LEVEL STYLES -->
 

<script type="text/javascript" >

    function FillSearchtextBox122() {
        // alert(val);
       
        document.getElementById("sample_editable_1_new").focus();
        document.getElementById("sample_editable_1_new").click();
        //ImgUpdateSearchitems
    }

</script>


<style type="text/css"> 
		
		/* Grid */
.searchgrid
{
	font-family: Tahoma;
	font-size:9pt;
}
.searchgrid-header
{
	background-color:#E1DCAC;
	background-repeat:repeat-x;
	background-position: bottom;
	height: 22px;	
	border-bottom-style:solid;
	border-bottom-color:#456037;
	border-bottom-width:3px;
    color: #ffffff;
  
	
}
.searchgrid-header a
{
	text-decoration: none;
	font-weight: bold;	
	display: block;
	height: 20px;
	color: #456037;
	text-align:center;
}

.searchgrid a
{
	text-decoration: none;
	font-weight: bold;	
	display: block;
	color: #456037;
	
}

.searchgrid-header a:hover
{
	color: #456037;
}
.searchgrid-row
{
background-color: #CAC590;
}

.searchgrid-alternative-row
{
	background-color: #E6E3CB;
}

 
	 
		.box { 
		
			       visibility : hidden;
	                margin : 0px!important;
	                background-color : inherit;
	                color : windowtext;
	                border : buttonshadow;
	                border-width : 1px;
	                border-style : solid;
	                cursor : 'default';
	                overflow : auto;
	                height : 200px;
                    text-align : left; 
                    list-style-type : none;
                    font-family: Verdana;
                    font-size:8pt;
		 }
		 
		.heading {
			font-family: Arial;
			font-weight: bold;
			font-size: 12px;
			 
		}
		
	</style>


    <style type='text/css' >

fieldset {
  width: 350px;
}

.textInput,textarea {
  
  font-family: arial;
  background-color: #FFFFFF;
  
  
}

.inputHighlighted {
  
  background-color: #335EA8;
  color: #FFFFFF;
  overflow:hidden;
   
}



     .tabs
        {
            position:relative;
            top:1px;
            left:10px;
        }
        .tab
        {
            border:solid 1px #5D7249;
            background-color:#5D7249;
            padding:2px 10px;
            color:#ffffff;
            font-weight:bold;
        }
        .selectedTab
        {
            background-color:white;
            border-bottom:solid 1px white;
             background-color:#F6F4BE;
            color:#5D7249;
            font-weight:bold;
        }
        .tabContents
        {
             
            padding:10px;
            background-color:white;
             
        }
        
            table.admintable td.key,  
             table.admintable td.paramlist_key 
             { 
             background-color: #FFFFCC; 
             text-align: right; 
             width: 100px; 
             color: #000000; 
             font-weight: bold; 
             font-size:11px; 
             border-bottom: 1px solid #e9e9e9; 
             border-right: 1px solid #e9e9e9; 
             } 
             table.admintable td  { padding: 3px; } 
             table.admintable input  {font-size:11px; } 
             table.admintable td.key.vtop { vertical-align: top; } 
             .adminform { border: 1px solid #ccc; margin: 10px 10px 10px 10px; } 
              
            .style1
                {
                    color: #FF3300;
                }
              
   .style2
    {
        color: #339933;
        font-weight: bold;
        font-family: "Times New Roman", Times, serif;
        font-size: large;
    }
        
        
            .autocomplete_completionListElement 
                {  
	                visibility : hidden;
	                margin : 0px!important;
	                background-color : inherit;
	                color : windowtext;
	                border : buttonshadow;
	                border-width : 1px;
	                border-style : solid;
	                cursor : 'default';
	                overflow : auto;
	                height : 200px;
                    text-align : left; 
                    list-style-type : none;
                    font-family: Verdana;
                    font-size:8pt;
                }

                /* AutoComplete highlighted item */

                .autocomplete_highlightedListItem
                {
	                background-color: #FFFFCC;
	                color: black;
	                padding: 1px;
                }

                /* AutoComplete item */

                .autocomplete_listItem 
                {
	                background-color : window;
	                color : windowtext;
	                padding : 1px;
                }
        
.AutoExtender
{
    
    font-family: Verdana, Helvetica, sans-serif;
    font-size: 10px;
    font-weight: normal;
    border: solid 1px #006699;
   	overflow : auto;  
   	height : 200px;
    background-color: White;
    margin-left:0px;    
}
.AutoExtenderList
{
    border-bottom: dotted 1px #006699;
    cursor: pointer;
    color: Maroon;
}
.AutoExtenderHighlight
{
    color: White;
    background-color: #006699;
    cursor: pointer;
}

.backgroundimage
{
 
background-repeat:no-repeat;
}

          
          
          div#sample_editable_1_filter
          {
          	display:none;
          }

          div#sample_editable_1_length
           {
          	display:none;
          }
          
          
          div#sample_editable_1_info
           {
          	display:none;
          }
              
              
          .itemText
          {
          	
          	font-family:"Courier New",Courier;
          	border:0.5px;
          	border-style:solid;
          	background-color:#EAF2FB;
          }    
              
              
            </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 


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

      

      function Saveitem(key,saveid) {
          Initialize();
          var url = "AjaxItemsSave.aspx?" + key;

//          alert(saveid);
//          alert(document.getElementById(saveid));

          document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

          if (req != null) {
              req.onreadystatechange = ProcessSave;
              req.open("GET", url, true);
              req.send(null);


          }

      }

      function ProcessSave( ) {
         
          if (req.readyState == 4) {
              if (req.status == 200) {

                 // alert("Items saved Inline Number =" + req.responseText);
                  document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";

              }
              else {
                  alert("There was a problem saving data:<br>" + req.statusText);
              }
          }

      }


      function Updateitem(key, saveid) {
          Initialize();
          var url = "AjaxItemsUpdate.aspx?" + key;

          //          alert(document.getElementById(saveid));

          document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

          if (req != null) {
              req.onreadystatechange = ProcessUpdate;
              req.open("GET", url, true);
              req.send(null);


          }

      }

      function ProcessUpdate() {

          if (req.readyState == 4) {
              if (req.status == 200) {

                 // alert("Items saved Inline Number =" + req.responseText);
                  document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";

              }
              else {
                  alert("There was a problem updating data:<br>" + req.statusText);
              }
          }

      }


      function Deleteitem(key, saveid) {
          Initialize();
          var url = "AjaxItemsDelete.aspx?" + key;

          //alert(url);
          //          alert(document.getElementById(saveid));

          document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = saveid;

          if (req != null) {
              req.onreadystatechange = ProcessDelete;
              req.open("GET", url, true);
              req.send(null);


          }

      }

      function ProcessDelete() {

          if (req.readyState == 4) {
              if (req.status == 200) {

                  //alert("Items Deleted Inline Number =" + req.responseText);
                  document.getElementById("ctl00_ContentPlaceHolder_reqsaveid").value = "";

              }
              else {
                  alert("There was a problem Delete data:<br>" + req.statusText);
              }
          }

      }


      function SendQuery2(key) {
          Initialize();
          var url = "AjaxItemsSearch.aspx?k=" + key;
          
          if (req != null) {
              req.onreadystatechange = Process2;
              req.open("GET", url, true);
              req.send(null);
             

          }

      }

      // Removes leading whitespaces
      function LTrim(value) {

          var re = /\s*((\S+\s*)*)/;
          return value.replace(re, "$1");

      }

      // Removes ending whitespaces
      function RTrim(value) {

          var re = /((\s*\S+)*)\s*/;
          return value.replace(re, "$1");

      }

      // Removes leading and ending whitespaces
      function trim(value) {

          return LTrim(RTrim(value));

      }




      function itemrsearchcloseProcess() {

          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url(searchbackground.jpg)';
          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundRepeat = 'no-repeat';
          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundPositionX = 'left';
          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = "";
          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = "";
          document.getElementById("autocomplete2").innerHTML = "";
          HideDiv("autocomplete2");


      }


      function itemsearchBlurProcess() {

          if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value == "") {
              HideDiv("autocomplete2");
              document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url(searchbackground.jpg)';
          }

      }


    

      function Process2() {
          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url(ajax-loader-text.gif)';
          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundRepeat = 'no-repeat';
          document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundPositionX = 'right';


          if (req.readyState == 4) {
              // only if "OK"
              if (req.status == 200) {
                  document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';
                  if (req.responseText == "") {
                      //alert("in blank");  
                      //HideDiv("autocomplete");
                      document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url(searchbackground.jpg)';
                      document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundRepeat = 'no-repeat';
                      document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundPositionX = 'left';
                      
                      var newdiv = document.createElement("div");
                      newdiv.innerHTML = "<br><br><div style='text-align:center' >No result found, Please try with some other keyword.</div>";
                      var container = document.getElementById("autocomplete2");
                      container.innerHTML = "<div style='text-align:center' ><a href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
                      container.appendChild(newdiv)
                  }
                  else {


                      if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value != "") {
                          ShowDiv("autocomplete2");
                          //document.getElementById("autocomplete2").innerHTML = req.responseText;

                          var newdiv = document.createElement("div");
                          newdiv.innerHTML = req.responseText;
                          var container = document.getElementById("autocomplete2");
                          container.innerHTML = "<div style='text-align:center' ><a href='javascript:itemrsearchcloseProcess();' >Close Search result</a></div>";
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
          // alert(val);

          if (document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value == "") {

          }
          else {
              var str = val;
              var str1;
              str1 = str.split("~!");

              document.getElementById("ctl00_ContentPlaceHolder_txtitemid").value = str1[0];
              document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchprice").value = str1[1];
              document.getElementById("ctl00_ContentPlaceHolder_txtitemsearchDesc").value = str1[2];

              document.getElementById("autocomplete2").innerHTML = "";
              HideDiv("autocomplete2");
              document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").style.backgroundImage = 'url()';


              document.getElementById("sample_editable_1_new").focus();
              document.getElementById("sample_editable_1_new").click();

              document.getElementById("ctl00_ContentPlaceHolder_txtitemsearch").value = "";

          }

          
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




    <table border="1" cellpadding="5" cellspacing="5">
        <tr>
            <td   align="center" colspan="3">
            <br />
            <br />
                &nbsp;<b> Order Number :<asp:TextBox ID="txtOrderNumber" Width="200px"   Font-Size="12"  Text="4014"  Height="28" runat="server"></asp:TextBox> </b>

                <img src="Images/Add-Button.gif" border=0 />
                <img src="Images/edit.gif" border=0 />
                <img src="Images/Delete.gif" border=0 />
                <img src="Images/Cancel.gif" border=0 />
                <img src="Images/Save.gif" border=0 />        
            </td>
         </tr>
         
        <tr>
            <td colspan="3">
            
                <table border="1" cellpadding="0" cellspacing="0" width="100%" id="table54">
                                           <tr>
                                                        
                                                        <td style="text-align:left; font-weight: bold;">
                                                            <table border="1"  cellspacing="0" cellpadding="0" >

                                                             <tr>
                                                              <td>&nbsp;&nbsp;&nbsp;
                                                            </td> 
                                                                <td>
                                                                    <div style="display:none"; >
                                                                         ID :<asp:TextBox ID="txtitemid" Width="200px"   Font-Size="12"   Height="28" runat="server"></asp:TextBox>
                                                                         Name : <asp:TextBox ID="txtitemsearchDesc" Width="200px"   Font-Size="12"   Height="28" runat="server"></asp:TextBox> 
                                                                        Price : <asp:TextBox ID="txtitemsearchprice" Width="50px"   Font-Size="12"   Height="28" runat="server"></asp:TextBox> 

                                                                        <asp:TextBox ID="txtfirst"   Text="1" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="reqsaveid"   Text="" runat="server"></asp:TextBox>
                                                                       
                                                                    </div>
                                                                 
                                                                </td>
                                                              <td>&nbsp;&nbsp;&nbsp;
                                                            </td> 
                                                            </tr>

                                                            <tr>
                                                            <td>&nbsp; Search &nbsp;
                                                            </td>                                                            
                                                            <td>                                                            
                                                            
                                                          
                                                             <asp:TextBox ID="txtitemsearch" Width="600px" SkinID="textEditorSkin667" Font-Size="14" CssClass="backgroundimage"  Height="28" runat="server"></asp:TextBox> 
                                                         
                                                           
                                                                 
                                                            <br />
                                                           <div align="left" class="box" id="autocomplete2" style="overflow-x:hidden; overflow-y:auto;BACKGROUND-COLOR:#E1DCAC;height:0px;position:absolute;z-index:2; width:600px" ></div>                                                            
                                                            </td>
                                                            <td>
                                                                                                                  
                                                            </td>
                                                            </tr>
                                                           
                                                            </table>                                                               
                                                            
                                                        </td>
                                                                                                             
                                                    </tr>
                                    </table> 

            
            </td>
        
        </tr>
        <tr>
            <td   colspan="3">
            <div style="display:none"; >
                <button id="sample_editable_1_new" class="btn green">
                    Add New <i class="fa fa-plus"></i>
                </button>
            </div>
                <br />
                <br />
                <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                    <thead>
                        <tr>
                            <th>
                                Qty
                                
                            </th>
                            <th>
                                Item ID 
                               
                            </th>
                            <th>
                                Item Name 
                            </th>
                            <th>
                                Description 
                            </th>
                            <th>
                                Price 
                            </th>
                            <th>
                                Discount 
                            </th>
                            <th>
                            Disc. Type 
                            </th>
                            <th>
                                Total 
                            </th>
                            <th>
                                Edit
                            </th>
                            <th>
                                Delete
                            </th>
                            <th>
                                
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <input type="text" style="width:50px;" class="form-control" value="1">
                            </td>
                            <td>
                                <input type="text" style="width:80px;" class="form-control" value="C-01">
                            </td>
                            <td>
                                <input type="text" style="width:150px;" readonly class="form-control" value="Red Roses">
                            </td>
                            <td class="center">
                                <input type="text" style="width:250px;" class="form-control" value="12 dozen roses">
                            </td>
                            <td>
                                <input type="text" style="width:70px;" class="form-control" value="0">
                            </td>
                            <td>
                                <input type="text" style="width:50px;" class="form-control" value="0">
                            </td>
                            <td>
                                <input type="text" style="width:50px;" class="form-control" value="Flat">
                            </td>
                            <td>
                                <input type="text" style="width:70px;" class="form-control" readonly value="0">
                            </td>
                            <td>
                                <a class="edit" href="">Save</a>
                            </td>
                            <td>
                                <a class="cancel" href="">Cancel</a>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        
                    </tbody>
                </table>
            </td>
        </tr>
    </table>

  

  <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
<!-- BEGIN CORE PLUGINS -->
<!--[if lt IE 9]>
<script src="assets/plugins/respond.min.js"></script>
<script src="assets/plugins/excanvas.min.js"></script> 
<![endif]-->
<script src="assets/plugins/jquery-1.10.2.min.js" type="text/javascript"></script>
<script src="assets/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
<script src="assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<script src="assets/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
<script src="assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
<script src="assets/plugins/jquery.blockui.min.js" type="text/javascript"></script>
<script src="assets/plugins/jquery.cokie.min.js" type="text/javascript"></script>
<script src="assets/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL PLUGINS -->
<script type="text/javascript" src="assets/plugins/select2/select2.min.js"></script>
<script type="text/javascript" src="assets/plugins/data-tables/jquery.dataTables.js"></script>
<script type="text/javascript" src="assets/plugins/data-tables/DT_bootstrap.js"></script>
<!-- END PAGE LEVEL PLUGINS -->
<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="assets/scripts/app.js"></script>
<script src="assets/scripts/table-editable.js"></script>
<script>
    jQuery(document).ready(function () {
        App.init();
        TableEditable.init();
    });
</script>

</asp:Content>
