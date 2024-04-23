<%@ Page Language="VB" AutoEventWireup="false" CodeFile="POReceiveInventoryUpdate.aspx.vb" Inherits="POReceiveInventoryUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        Enter PO Receive Date <asp:TextBox ID="txtReceiveDate" runat="server"></asp:TextBox>

    <br /><br />
    Enter PO #: <asp:TextBox ID="txtPO" runat="server"></asp:TextBox>
        <br /><br />
        <asp:Button ID="btnPO" runat="server" Text="Receive Inventory" />
    </div>
    </form>
</body>
</html>
