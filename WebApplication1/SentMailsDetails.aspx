
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SentMailsDetails.aspx.vb" Inherits="EnterpriseASPSystem_CustomCompanySetup_SentMailsDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sent Mails Details</title>
    <style type="text/css">
       
        .tabs
        {
            position:relative;
            top:1px;
            left:10px;
        }
        .tab
        {
            border:solid 1px #C6DBAD;
            background-color:#C6DBAD;
            padding:2px 10px;
            color:#ffffff;
            font-weight:bold;
        }
        .selectedTab
        {
            background-color:white;
            border-bottom:solid 1px white;
             background-color:#F6F4BE;
            color:#C6DBAD;
            font-weight:bold;
        }
        .tabContents
        {
            border:solid 1px black;
            padding:10px;
            background-color:white;
        }
        
         table.admintable td.key,  
             table.admintable td.paramlist_key 
             { 
             background-color: #C6DBAD; 
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
             .adminform { border: 1px solid #ccc; margin: 0 10px 10px 10px; } 
              
    </style> 
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <fieldset class="adminform">
				            <legend><b>Sent Mail Details</b></legend>
                        <table      class="admintable">
	                          
                             <tr>
		                        <td  nowrap="nowrap"  class ="key" >
                                From: 
		                        </td>
		                        <td >

                            <div >
                                 
                                    &nbsp; 
                                 <asp:Label ID="txtfrom" runat="server"  ></asp:Label>
                            </div>		                        
                            </td>
                            </tr>
                                                          
					         <tr>
		                        <td  nowrap="nowrap"  class ="key" >
                                 To: 

		                        </td>
		                        <td >

                            <div >
                            
                                    &nbsp; 
                                <asp:Label ID="txtto" runat="server"  ></asp:Label>
                            </div>		                        
                            </td>
                            </tr>

                            
                           <tr>
		                        <td  nowrap="nowrap"  class ="key" >
                                
                                CC:
		                        </td>
		                        <td >

                            <div >
                            
                                    &nbsp; 
                                 <asp:Label ID="txtcc"   runat="server"></asp:Label>
                            </div>		                        
                            </td>
                            </tr>
                            
                           <tr>
		                        <td  nowrap="nowrap"  class ="key" >
                                
                                Subject:
		                        </td>
		                        <td >

                            <div >
                            
                                    &nbsp; 
                                 <asp:Label ID="txtSubject" runat="server"  ></asp:Label>
                            </div>		                        
                            </td>
                            </tr>                            
                             
                           <tr>
		                        <td  nowrap="nowrap"  class ="key" >
                                
                               Message
		                        </td>
		                        <td >

                                                     
                                    &nbsp; 
                                    
                                <div ID="txtMessage" runat="server"  ></div>
                            		                        
                            </td>
                            </tr>
                             
                            			        
					      
					 </table>
                 
                   </fieldset>	
    </div>
    </form>
</body>
</html>
