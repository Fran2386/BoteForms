<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Preferences.aspx.cs" Inherits="BoteForms.Preferences" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="col-md-4">
            <h2>Mi perfil</h2>
             <asp:Label ID="MensajeBienvenida" runat="server" Text="" />
        </div>
      <div class="row">
        <div class="col-md-4">
            <h2>Total de bote:</h2>
            <div class="input-group">
                <asp:TextBox ID="txtBote" runat="server" CssClass="form-control" type="number" />
                <div class="input-group-append">
                    <asp:Button ID="btnCalcular" runat="server" Text="Calcular" CssClass="btn btn-primary" OnClick="BtnCalcularClick" />
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Número de trabajadores:</h2>
            <asp:DropDownList ID="ddlNumeroTrabajadores" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNumeroTrabajadores_SelectedIndexChanged">
                <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                <asp:ListItem Text="3" Value="3"></asp:ListItem>

            </asp:DropDownList>
        </div>
    </div>

    <div class="container">
        <asp:PlaceHolder ID="phTrabajadores" runat="server"></asp:PlaceHolder>
    </div>

     </asp:Content>



