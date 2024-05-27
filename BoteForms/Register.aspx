<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BoteForms.Register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Registro</h2>
            <asp:Label ID="RegisterMessage" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Nombre de usuario"></asp:Label>
            <asp:TextBox ID="UsernameRegister" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Contraseña"></asp:Label>
            <asp:TextBox ID="PasswordRegister" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="EmailRegister" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="RegisterButton" runat="server" Text="Registrar" OnClick="RegisterButton_Click" />
        </div>
    </form>
</body>
</html>