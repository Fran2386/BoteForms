<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="BoteForms.Registro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <h2 class="text-center">Registro</h2>
                <asp:Label ID="RegisterMessage" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Nombre de usuario"></asp:Label>
                    <asp:TextBox ID="UsernameRegister" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Contraseña"></asp:Label>
                    <asp:TextBox ID="PasswordRegister" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="EmailRegister" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Button ID="RegisterButton" runat="server" Text="Registrar" OnClick="RegisterButton_Click" CssClass="btn btn-primary btn-block" />
            </div>
        </div>
    </div>
</asp:Content>
