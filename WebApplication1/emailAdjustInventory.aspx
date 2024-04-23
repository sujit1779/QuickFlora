<%@ Page Title="" ValidateRequest="false" Language="VB" MasterPageFile="~/MainMaster.master" AutoEventWireup="false" CodeFile="emailAdjustInventory.aspx.vb" Inherits="emailAdjustInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

    Preview and send Adjust Inventory mail

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">


  <table border="0" class="table table-bordered table-striped table-condensed flip-content"  id="table2">
         
 
        <tr>
             
            <td  >
            
                Adjustment # 
            </td>
            
            <td width="560">
                &nbsp;<asp:Label ID="lblordernumber" Width="300" CssClass="form-control" runat="server" Text=""></asp:Label>
                
                </td>
        </tr>
        <tr id="tr1" runat="server" visible="false" >
            
            <td  >
                Email Type</td>
             
            <td width="560">
                <asp:DropDownList ID="drpEmailTypes" Width="300" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="Order Notification to Vendor" Value="PO Notification to Vendor"></asp:ListItem>
                
                </asp:DropDownList></td>
        </tr>
       <tr>
            
            <td  width="10">
                &nbsp;</td>
            
            <td width="560">
                           
                                <asp:Label ID="lblconfirmation" runat="server" Text=""></asp:Label>
                
            </td>
        </tr>          
       <tr>
            
            <td width="70">
                &nbsp;</td>
            
            <td width="560">
            <table>
                 <tr>
                    <td>
                        <asp:Button ID="BtnSubmit" CssClass="btn blue btn-block" Width="150" runat="server" Text="Send" />
                    </td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                    <asp:Button ID="Button1" CssClass="btn blue btn-block"  Width="150" runat="server" Text="Back To List" />
                    </td>
                </tr>
            </table>
            <br />
                
                
            </td>
        </tr>  
        
        </table> 
        
<table border="0" class="table table-bordered table-striped table-condensed flip-content"   visible="false"   runat="server"   id="table3">                
       <tr>
            
            <td width="10">
                &nbsp;</td>
             
            <td width="560">

                <asp:Button ID="btnmore" CssClass="btn blue btn-block"  Width="150" runat="server" Text="Send more" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnback" CssClass="btn blue btn-block"  Width="150" runat="server" Text="Back To Orders" />
            </td>
        </tr>   
        </table> 
        
<table border="0" class="table table-bordered table-striped table-condensed flip-content"  runat="server"   id="table1">               
        <tr>
            
            <td width="100"  >
                From Email</td>
             
            <td width="560" style="height: 26px">
                <asp:TextBox Enabled="true" ID="txtfrom" Width="300px" CssClass="form-control" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            
            <td width="100" style="height: 26px;" >
                To Email</td>
            
            <td width="560" style="height: 26px">
                <asp:TextBox ID="txtto" Width="300px" CssClass="form-control" runat="server"></asp:TextBox>
            </td>
        </tr>                
        <tr>
            
            <td width="100" style="height: 26px;"  >
                CC Email</td>
            
            <td width="560" style="height: 26px">
                <asp:TextBox ID="txtcc" Width="300px" CssClass="form-control" runat="server"></asp:TextBox>
            </td>
        </tr>    
        
          <tr>
           
            <td width="100" style="height: 26px;">
                CC 2 Email</td>
             
            <td width="560" style="height: 26px">
                <asp:TextBox ID="txtcc2" Width="300px" Text="" runat="server"></asp:TextBox>
            </td>
        </tr> 
        
        
        
            
                <tr  id="tr2" runat="server" visible="false"   >
             
            <td width="100" style="height: 26px;"  >
                Order modified?</td>
            
            <td width="560" style="height: 26px">
                <asp:CheckBox ID="checkOrdermodified" runat="server" />
            </td>
        </tr>  
        <tr>
            
            <td width="100" style="height: 26px;"  >
                Email Subject</td>
             
            <td width="560" style="height: 26px">
                <asp:TextBox ID="txtemailsubject" Width="500px"  CssClass="form-control" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            
            <td width="100" style="height: 402px;"  >
                Email Content</td>
            
            <td width="560" style="padding:5px; border-style:solid; border-width:thin; border-color:Gray; "  >
                <div id="divEmailContent"  runat="server"  ></div>
            </td>
        </tr>
       
       
    </table>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

