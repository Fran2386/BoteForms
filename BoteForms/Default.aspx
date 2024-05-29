<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BoteForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <h2>Total de bote:</h2>
            <div class="input-group">
                <asp:TextBox ID="txtBote" runat="server" CssClass="form-control" type="number" />
                <div class="input-group-append">
                    <asp:Button ID="btnCalcular" runat="server" Text="Calcular" CssClass="btn btn-primary" OnClick="BtnCalcularClick" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="BtnLimpiar_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h4>Número de trabajadores:
            <asp:DropDownList ID="ddlNumeroTrabajadores" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNumeroTrabajadores_SelectedIndexChanged">
                <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
            </asp:DropDownList>
                </h4>
        </div>
    </div>

    <div class="container">
        <table class="table">
            <thead>
                <tr>
                    <th>Trabajador</th>
                    <th>Horas semanales para el cómputo</th>
                    <th>Bote correspondiente</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder ID="phTrabajadores" runat="server"></asp:PlaceHolder>
            </tbody>
        </table>
    </div>
</asp:Content>

