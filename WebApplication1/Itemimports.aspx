<%@ Page Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="Itemimports.aspx.vb" Inherits="Itemiupdate" %>

<%@ Register TagPrefix="ajax" Namespace="MagicAjax.UI.Controls" Assembly="MagicAjax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/keyboard/keyboard.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
Items Update
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">

 <script language="JavaScript">

   function WebForm_OnSubmit()
   {
       obj11 = window.document.getElementById("ctl00__mainContent_drpItemID");
        
       var p1=obj11.selectedIndex;
       var val1=obj11.options[p1].value; 
       
             
        if( val1=="")
        {
           alert("Please select ItemId to Proceed");
           return false;
        }
        
        
        
        obj12 = window.document.getElementById("ctl00__mainContent_drpItemName");
        
       
           var p2=obj12.selectedIndex;
           var val2=obj12.options[p2].value; 
           
                 
            if( val2=="")
            {
               alert("Please select Item Name to Proceed");
               return false;
            }
             
        
             
           return true;
             
        
        
    }
    
    
    var newwindow;
    function poptastic(url)
    {
	    newwindow=window.open(url,'name','height=400,width=500');
	    if (window.focus) {newwindow.focus()}
    }
    
    
</script>

<style type='text/css' >
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
             background-color: #C6DBAD; 
             text-align: right; 
             width: 200px; 
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
              
            </style><br />    
        <br />
              <div class="row">
                   <div class="col-md-12">
                    <div class="portlet box green ">
                        <div class="portlet-title">
                            <div class="caption">
                                Upload Details
                            </div>
                            <div class="tools">
                                <a href="#" class="collapse"></a>
                                <!--<a href="#portlet-config" data-toggle="modal" class="config"></a>-->
                                <!--<a href="" class="reload"></a>-->
                                <!--<a href="#" class="remove"></a>-->
                            </div>
                        </div>
                        <div class="portlet-body form">
         <asp:Panel ID="Panel4" Visible=false  runat="server" >
            
            <div class="tabContents">
            <fieldset class="adminform">
                <legend><b>File uploaded last time</b></legend>
                
                       <br />
                &nbsp;&nbsp; <b>File upladed on </b><asp:Label ID="lbltime" runat="server" Text=""></asp:Label>
                &nbsp;&nbsp;
                 <asp:Button ID="btnselect" runat="server" Text="Select Uploaded File" />
               
                 &nbsp;&nbsp;&nbsp; <asp:Button ForeColor=Red Font-Bold=true   ID="btndelete" runat="server" Text="Delete Uploaded File" />
         <br />
        <br />
                   &nbsp;&nbsp;&nbsp; <asp:Label ID="lblfiledelete" ForeColor=Red Font-Bold=true  runat="server" Text=""></asp:Label> 
                
        <br />
        <br />
            </fieldset> 
            </div>
            
            <asp:Label ID="hiddenfilename"  runat="server" Visible=false></asp:Label>        
            
            </asp:Panel>
        
            <asp:Panel ID="Panel0" runat="server" >
            
            <div class="tabContents">
            <fieldset class="adminform">
                <legend><b>Upload Items File</b></legend>
               <ajax:AjaxPanel id="AjaxPanel3" runat="server">
                
                   <table class="admintable">
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                        Select File type
                        </td>
                        <td>
                            <asp:DropDownList Enabled="false" AutoPostBack="true"  ID="drpfiletype" runat="server">
                            <asp:ListItem Value="Excel" Text="Excel 2003"></asp:ListItem>
                            <asp:ListItem Value="Delimited" Selected="True" Text="Delimited"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="style1">* </span>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                            Select Delimeted File Type
                        </td>
                        <td>
                             <asp:DropDownList  Enabled="false"     ID="drpdelemitedfiletype" runat="server">
                                <asp:ListItem Value="CSV" Text="Comma delimited CSV"></asp:ListItem>
                                <asp:ListItem Value="TAB"  Text="Tab Delimited"></asp:ListItem>
                                <asp:ListItem Value="Semicolon"  Text="Semicolon delimited"></asp:ListItem>
                                <asp:ListItem Value="Pipe"  Text="Pipe delimited"></asp:ListItem>
                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                    </table>       
               </ajax:AjaxPanel>  
                
                &nbsp;&nbsp;
                <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                <br />
                <div style="padding-left:50px;">
                    <table>
                        <tr>
                            <td>
                                 <asp:FileUpload ID="FileUpload1" CssClass="btn green" runat="server" Width="400" />
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                 <asp:Button ID="Upload" CssClass="btn green" runat="server" Text="Upload" />
                            </td>
                        </tr>

                    </table>
                     
                
                

                </div>

              
        
        <br />
        <br />
            </fieldset> 
            </div>
            
               <div ID="Panel111" Visible="false" runat="server"  class="tabContents">
                <fieldset class="adminform">
                <legend><b>Upload Images for Item</b></legend>
                <div  style="padding:10px;">
                
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="itemsimageupload.aspx" runat="server">Upload Images For Existing Items </asp:HyperLink>
                    
               </div>
              </fieldset> 
            </div>          
            
            </asp:Panel>
            
             <asp:Panel ID="Panel1" Visible="false" runat="server" >
             <div style="padding-left:10px"  class="tabContents">
            <fieldset class="adminform">
                <legend><b>Map columns</b></legend>
                <br />
               <div style="padding-left:10px"  > 
                <table class="admintable">
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                        Item ID
                        </td>
                        <td>
                            <asp:DropDownList ID="drpItemID" runat="server">
                            </asp:DropDownList>
                            <span class="style1">* </span>
                        </td>
                    </tr>
                        
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                        Item Name
                        </td>
                        <td>
                      <asp:DropDownList ID="drpItemName" runat="server">
                            </asp:DropDownList>
                            <span class="style1">* </span>
                        </td>
                    </tr>
                   
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                        Price
                        </td>
                        <td>
                        <asp:DropDownList ID="drpPrice" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                        Wholesale Price
                        </td>
                        <td>
                        <asp:DropDownList ID="drpWholesalePrice" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td nowrap="nowrap" class ="key" >
                            MTPrice 
                        </td>
                        <td>
                        <asp:DropDownList ID="drpMTPrice" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                        AVG Cost
                        </td>
                        <td>
                       <asp:DropDownList ID="drpAVGCost" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" class ="key" >
                        Pack 
                        </td>
                        <td>
                        <asp:DropDownList ID="drpPack" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
             <br />
             <table cellspacing="20" width="100%" >
                <tr >
                 <td  align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnreload" CssClass="btn green"   runat="server" Text="Re Upload File" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnPreview" CssClass="btn green"   runat="server" Text="Preview" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnImport" CssClass="btn green"   runat="server" Text="Update Item" />
                    </td>
                    
                </tr>
                <tr>
                <td align="left" colspan="2">
                <span class="style1">* </span>Select all required fields to Proceed
                </td>
                </tr>
             </table>
             <br />
            </div> 
            </fieldset> 
            </div>
            
                 
            
            </asp:Panel>
             <asp:Panel ID="Panel2"  Visible=false   runat="server" >
            
            
<div  style="padding:5px 5px 5px 5px;">
<div  style="display:none;">
    <asp:TextBox ID="txtgrid" runat="server"></asp:TextBox>
</div>
<fieldset class="adminform">
                <legend><b>Preview Of records after relate</b></legend>
                <br />
    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
 
  
     <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped table-condensed flip-content"  runat="server" AutoGenerateColumns="true"   PageSize="1000" DataKeyNames ="ItemID" AllowPaging ="true"   Width="90%"   >
                
                   
                </asp:GridView>
                <br />
                <br />
                 <b>Details of errors on grid if any row is red</b>
                 <div style="padding:10px 10px 10px 10px;" id="dvvalidation"  runat="server">
                 
                 </div> 
  <br />
  <br />
           <table cellspacing="20" width="40%" >
           <tr >
                    <td align="right">
                    
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkupdate" Visible="false"  runat="server" /> 
                        <br />
                    </td>
           </tr>    
                         
                <tr >
                    <td align="right">
                        <asp:Button ID="btnback" CssClass="btn green" runat="server" Text="Back To Relate" />
                    </td>
                    <td align="left">
                       &nbsp;&nbsp; <asp:Button ID="btnimport2" runat="server" CssClass="btn green" Text="Update Now" />
                    </td>
                    
                </tr>
             </table> 
            </fieldset> 

</div> 

</asp:Panel>

   <asp:Panel ID="Panel3"  Visible=false   runat="server" >
   <div  style="padding:5px 5px 5px 5px;">
       <fieldset class="adminform">
                <legend><b>Summary of Updates</b></legend>
                <br />    
           
     <asp:GridView ID="GridView2" CssClass="table table-bordered table-striped table-condensed flip-content"  runat="server" AutoGenerateColumns="true"   PageSize="1000" DataKeyNames ="ItemID" AllowPaging ="true"   Width="90%"   >
                
                   
                </asp:GridView>
            <div style="padding:10px 10px 10px 10px;" id="dvupdate" runat="server">
            </div>
             <div style="padding:10px 10px 10px 10px;" id="dvimports" runat="server">
           </div>
            
            <div style="padding:10px 10px 10px 10px;" id="dverrror" runat="server"></div>
           
            
     </fieldset> 
      </div>   
   </asp:Panel>
                     </div>
                    </div>
                </div>
                <!-- END PAGE CONTENT-->
    </div> 
</asp:Content>
