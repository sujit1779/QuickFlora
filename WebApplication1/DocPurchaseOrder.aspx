<%@ Page Language="VB" Debug="True" %>
<%@ import Namespace="System.Data" %>
<%@ import Namespace="System.Data.SqlClient" %>
<%@ import Namespace="System.Web" %>
<%@ import Namespace="System.Web.UI" %>
<%@ import Namespace="System.Web.UI.WebControls" %>
<%@ import Namespace="System.Web.UI.HTMLControls" %>
<%@ import Namespace="System.Diagnostics" %>
<%@ Register TagPrefix="sp1" TagName="CompanyInformation" Src="spCompanyInformation.ascx" %>

<script runat="server">

    ' Report - Purchase Order.aspx
    '
    ' This report takes an input a single Purchase order number and displays it to the screen
    ' you can then prints that purchase order to the printer if you like
    '
    ' Very simple report - use this as a template for your custom purchase order forms
    '
    ' Written by EDE 08/26/2003
    '
    ' Copyright 2003 STFB Inc.

    'declare variables
    Dim ConnectionString As String = ""
    Dim CompanyID As String = ""
    Dim DivisionID As String = ""
    Dim DepartmentID As String = ""
    Dim EmployeeID As String = ""
    Dim PurchaseNumber as String = ""
    Dim CurrencySymbol as String =""
    Dim Allowed As Boolean = False
    Dim reader As SqlDataReader


   Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        

       ' get the order number passed to us as a string
        PurchaseNumber = Request.QueryString("PurchaseNumber")
        If PurchaseNumber = "" Then
           PurchaseNumber="*"
        End If


        ' Ok so everything is fine! Let's run our report now!
        ConnectionString = EnterpriseCommon.Configuration.ConfigSettings.ConnectionString
       
            If Session("CompanyID") Is Nothing Then
                Response.Redirect("loginform.aspx")
            End If


 
            'Dim SessionKey As Hashtable = CType(Session("SessionKey"), Hashtable)
            '' get the connection ready
            CompanyID = Session("CompanyID")
            DivisionID = Session("DivisionID")
            DepartmentID = Session("DepartmentID")
            EmployeeID = Session("EmployeeID")

        BindHeaderGrid()

    End sub


    Sub BindHeaderGrid()

    ' We are going to execute two stroed procedures here, one to populate the header and summary reader
    ' and another to populate the detail reader

    ' Header and summary reader Stored procedure

    ' Load the name of the stored procedure where our data comes from here into commandtext
      Dim CommandText As String = "enterprise.RptDocPurchaseOrderHeaderSingle"

       ' get the connection ready
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@PurchaseNumber", SqlDbType.NVarChar).Value = PurchaseNumber

      ' open the connection
        myConnection.Open()

       'bind the datasource
        HeaderGrid.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        HeaderGrid.DataBind()

     End Sub

    Sub BindDetailGrid(DetailGrid As DataGrid)

    ' We are going to execute two stroed procedures here, one to populate the header and summary reader
    ' and another to populate the detail reader

    ' Detail Stored procedure

    ' Load the name of the stored procedure where our data comes from here into commandtext
      Dim CommandText As String = "enterprise.RptDocPurchaseOrderDetailSingle"

       ' get the connection ready
        Dim myConnection As New SqlConnection(ConnectionString)
        Dim myCommand As New SqlCommand(CommandText, myConnection)
        Dim workParam As New SqlParameter()

        myCommand.CommandType = CommandType.StoredProcedure

        ' Set the input parameter, companyid, divisionid, departmentid
        ' these parameters are set in the sub page_load
        myCommand.Parameters.Add("@CompanyID", SqlDbType.NVarChar).Value = CompanyID
        myCommand.Parameters.Add("@DivisionID", SqlDbType.NVarChar).Value = DivisionID
        myCommand.Parameters.Add("@DepartmentID", SqlDbType.NVarChar).Value = DepartmentID
        myCommand.Parameters.Add("@PurchaseNumber", SqlDbType.NVarChar).Value = PurchaseNumber

        ' open the connection
        myConnection.Open()

        'bind the datasource
        DetailGrid.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        DetailGrid.DataBind()

     End Sub

    Sub HeaderGrid_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType=ListItemType.Item Or e.Item.ItemType=ListItemType.AlternatingItem Then
            Dim DetailGrid As DataGrid = CType(e.Item.FindControl("DetailGrid"),DataGrid)
            BindDetailGrid(DetailGrid)
        End If
    End Sub

    Function FormatDate(ByVal dt As Object) As String
        If dt Is DBNull.Value OrElse dt Is Nothing Then Return ""
        Return CType(dt, DateTime).ToShortDateString()
    End Function


</script>
<html>
<head>
    <title>Purchase Order</title>
</head>
<body style="FONT-FAMILY: arial">
    <form method="post" runat="server">
        <asp:Repeater id="HeaderGrid" runat="server" OnItemDataBound="HeaderGrid_ItemDataBound">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate>
                <asp:Panel id="headerpanel" runat="server" Font-Names="Arial" GridLines="None" Borderstyle="Solid" BorderColor="#000000" BorderWidth="1px">
                    <table style="font-size: 0.9em; font-name: Arial" border="0" cellpadding="0" cellspacing="0" width="99%">
                        <tbody>
                            <tr>
                                <td width="70%" rowspan="3">
				<sp1:CompanyInformation id="CompanyInformation" runat="server"></sp1:CompanyInformation>
				</td>
                                <td colspan="2" width="30%">
                                    <center><b><font size="6">Purchase Order</font></b>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    <center><b>Date</b>
                                    </center>
                                </td>
                                <td width="15%">
                                    <center><b>Purchase #</b>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    <center><%# FormatDate(DataBinder.Eval(Container, "DataItem.PurchaseDate")) %>
                                    </center>
                                </td>
                                <td width="15%">
                                    <center><%# DataBinder.Eval(Container, "DataItem.PurchaseNumber") %>
                                    </center>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:panel>
                <asp:Panel id="shiptobilltopanel" runat="server" Font-Names="Arial" GridLines="None" Borderstyle="Solid" BorderColor="#000000" BorderWidth="1px">
                    <table style="FONT-SIZE: 0.9em; FONT-NAME: Arial;" border="0" cellpadding="0" cellspacing="0" width="99%">
                        <tbody>
                            <tr>
                                <td width="50%">
                                    <b>Ship To</b></td>
                                <td width="50%">
                                    <b>Vendor</b></td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.ShippingName") %></td>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.VendorName") %></td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.ShippingAddress1") %></td>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.VendorAddress1") %></td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.ShippingAddress2") %></td>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.VendorAddress2") %></td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.ShippingAddress3") %></td>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.VendorAddress3") %></td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.ShippingCity") & " " & DataBinder.Eval(Container, "DataItem.ShippingState") & ",  " & DataBinder.Eval(Container, "DataItem.ShippingZip") & "  " & DataBinder.Eval(Container, "DataItem.ShippingCountry") %></td>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.VendorCity") & " " & DataBinder.Eval(Container, "DataItem.VendorState") & ",  " & DataBinder.Eval(Container, "DataItem.VendorZip") & "  " & DataBinder.Eval(Container, "DataItem.VendorCountry") %></td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    &nbsp;</td>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.VendorEmail") %></td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    &nbsp;</td>
                                <td width="50%">
                                    <%# DataBinder.Eval(Container, "DataItem.VendorPhone") %></td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                </asp:panel>
                <table style="FONT-WEIGHT: bold; FONT-SIZE: 0.9em" border="1" bordercolor="#000000" cellspacing="0" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <td width="20%">
                                <center>Ordered By
                                </center>
                            </td>
                             
                            <td width="20%">
                                <center>Ship Via
                                </center>
                            </td>
                            <td width="20%">
                                <center>Arrival Date
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <center><%# DataBinder.Eval(Container, "DataItem.OrderedBy") %>
                                </center>
                            </td>
                             
                                        <td width="20%">
                                            <center><%# DataBinder.Eval(Container, "DataItem.ShipMethodID") %>
                                            </center>
                                        </td>
                                        <td width="20%">
                                            <center><%# FormatDate(DataBinder.Eval(Container, "DataItem.PurchaseDateRequested")) %>
                                            </center>
                                        </td>
                                        </tr>
                                        </tbody>
                                        </table>
                                        <asp:Table id="detailinfo" runat="server" width="100%" height="60%" Font-Names="Arial" GridLines="None" Borderstyle="Solid" BorderColor="#000000" BorderWidth="1px" cellpadding="0" cellspacing="0">
                                            <asp:tablerow>
                                                <asp:tablecell VerticalAlign="top">
                                                    <asp:DataGrid id="DetailGrid" runat="server" EnableViewState="False" Width="100%" AutoGenerateColumns="False" font-name="Arial" font-size="10pt" Font-Names="Arial" BorderStyle="None" Gridlines="None">
                                                        <Columns>
                                                            <asp:BoundColumn DataField="ItemID" HeaderText="Item ID">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="Left" forecolor="White" width="100px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="left"   height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Description" HeaderText="Description">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="Left" forecolor="White" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="left" height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="DetailMemo1" HeaderText="Comments">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="Left" forecolor="White" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="left" height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Color" HeaderText="Color">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="right" forecolor="White" width="80px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="right"   height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="VendorQTY" HeaderText="Qty">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="right" forecolor="White" width="80px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="right"   height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="ItemUOM" HeaderText="UOM">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="right" forecolor="White" width="80px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="right"   height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="VendorPacksize" HeaderText="Pack">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="right" forecolor="White" width="80px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="right"   height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="OrderQty" HeaderText="Units">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="right" forecolor="White" width="80px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="right"   height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="ItemUnitPrice" DataFormatString="{0:c}" HeaderText="Unit Price">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="right" forecolor="White" width="80px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="right"   height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Total" DataFormatString="{0:c}" HeaderText="Amount">
                                                                <HeaderStyle font-size="10pt" font-names="Arial" font-bold="True" horizontalalign="right" forecolor="White" width="80px" height="5px" backcolor="Black"></HeaderStyle>
                                                                <ItemStyle font-size="10pt" font-names="Arial" horizontalalign="right" width="80px" height="5px"></ItemStyle>
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </asp:tablecell>
                                            </asp:tablerow>
                                        </asp:Table>
                                        <div align="right">
                                            <asp:Table id="summaryinfo" runat="server" width="100%" HorizontalAlign="Right" Font-Names="Arial" GridLines="None" Borderstyle="Solid" BorderColor="#000000" BorderWidth="1px">
                                                <asp:tablerow>
                                                    <asp:tablecell>
                                                        <table runat="server" align="right" style="FONT-WEIGHT: bold; FONT-SIZE: 0.9em; FONT-NAME: Arial;" border="0" cellpadding="0" cellspacing="0" borderstyle="Solid" bordercolor="#000000" borderwidth="1px">
                                                            <tr align="right">
                                                                <td align="right" width="150">
                                                                    SubTotal:&nbsp;</td>
                                                                <td>
                                                                    <%# CurrencySymbol & DataBinder.Eval(Container, "DataItem.SubTotal" ,"{0:c}") %></td>
                                                            </tr>
                                                            <tr align="right">
                                                                <td align="right" width="150">
                                                                    Frieght:&nbsp;</td>
                                                                <td>
                                                                    <%# CurrencySymbol & DataBinder.Eval(Container, "DataItem.Freight" ,"{0:c}") %></td>
                                                            </tr>
                                                            
                                                            <tr align="right">
                                                                <td align="right" width="150">
                                                                    Tax:&nbsp;</td>
                                                                <td>
                                                                    <%# CurrencySymbol & DataBinder.Eval(Container, "DataItem.TaxAmount" ,"{0:c}") %></td>
                                                            </tr>
                                                            <tr align="right">
                                                                <td align="right" width="150">
                                                                    Total:&nbsp;</td>
                                                                <td>
                                                                    <%# CurrencySymbol & DataBinder.Eval(Container, "DataItem.Total" ,"{0:c}") %></td>
                                                            </tr>
                                                            <tr align="right">
                                                                <td align="right" width="150">
                                                                    Payments:&nbsp;</td>
                                                                <td>
                                                                    <%# CurrencySymbol & DataBinder.Eval(Container, "DataItem.AmountPaid" ,"{0:c}") %></td>
                                                            </tr>
                                                            <tr align="right">
                                                                <td align="right" width="150">
                                                                    Balance Due:&nbsp;</td>
                                                                <td>
                                                                    <%# CurrencySymbol & DataBinder.Eval(Container, "DataItem.BalanceDue" ,"{0:c}") %></td>
                                                            </tr>
                                                        </table>
                                                    </asp:tablecell>
                                                </asp:tablerow>
                                            </asp:Table>
                                        </div>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate></AlternatingItemTemplate>
                                        <FooterTemplate></FooterTemplate>
                                        </asp:Repeater>
        </form>
        </body>
        </html>
