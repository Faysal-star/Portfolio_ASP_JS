<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="portfolio_admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Panel</title>
    <link href="styles/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginG">
            <div class="heading">
                <h1>Admin Panel</h1>
            </div>
            <div class="lform">
                <asp:TextBox ID="txtCodeName" runat="server" placeholder="Code Name" CssClass="linp"></asp:TextBox>
                <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" CssClass="linp"></asp:TextBox>
 
                <asp:Label ID="lblError" runat="server" Text="" CssClass="lerror"></asp:Label>
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="lbtn"/>
            </div>
        </div>
    </form>
</body>
</html>
