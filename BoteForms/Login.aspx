<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BoteForms.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <h2 class="text-center">Acceso</h2>
                <asp:Label ID="LoginErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Nombre de usuario"></asp:Label>
                    <asp:TextBox ID="UsernameLogin" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Contraseña"></asp:Label>
                    <asp:TextBox ID="PasswordLogin" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="Acceder" OnClick="btnLoginClick" CssClass="btn btn-primary btn-block" />
                <br />
                <h3>¿Aún no estás registrado?</h3>
                <asp:Button ID="btnRegistro" runat="server" Text="Registrate aquí" OnClick="btnRegistroClick" CssClass="btn btn-primary btn-block" />
            </div>
        </div>
    </div>
</asp:Content>
