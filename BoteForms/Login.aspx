<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BoteForms.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Acceso</h2>
            <asp:Label ID="LoginErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Nombre de usuario"></asp:Label>
            <asp:TextBox ID="UsernameLogin" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Contraseña"></asp:Label>
            <asp:TextBox ID="PasswordLogin" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="LoginButton" runat="server" Text="Acceder" OnClick="LoginButton_Click" />
        </div>
    </form>
</body>
</html>