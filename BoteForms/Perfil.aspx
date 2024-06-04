<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="BoteForms.Perfil" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-4">
        <h2>Mi perfil</h2>
        <asp:Label ID="MensajeBienvenida" runat="server" Text="" />
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Total de bote:</h2>
             <asp:Label runat ="server" ID="lblErrorBote" ForeColor="Red"></asp:Label>
            <div class="input-group">
                <asp:TextBox ID="txtBote" runat="server" CssClass="form-control" type="number" min="1" max="1000000" placeholder="Total del bote recaudado"/>               
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Trabajadores:</h2>

            <asp:DropDownList ID="ddlOpciones" runat="server">
                <asp:ListItem Text="Opción 1" Value="1"></asp:ListItem>
                <asp:ListItem Text="Opción 2" Value="2"></asp:ListItem>
                <asp:ListItem Text="Opción 3" Value="3"></asp:ListItem>
            </asp:DropDownList>

            <asp:RadioButton ID="hrsPredefinidas" runat="server" Text="Horas predefinidas" GroupName="opciones" AutoPostBack="true" OnCheckedChanged="RadioButttonSeleccionado" />           
        </div>
    </div>  
        <div class="container">
        <table class="table">
            <thead>
                <tr>
                    <th>Trabajador</th>
                    <th>Horas semanales</th>
                    <th>Importe correspondiente</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder ID="phTrabajadores" runat="server"></asp:PlaceHolder>
            </tbody>
        </table> 
             <div class="input-group-append">
                    <asp:Button ID="btnCalcular" runat="server" Text="Calcular" CssClass="btn btn-primary" OnClick="BtnCalcularClick" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="BtnLimpiar_Click" /><br />
                 <asp:Label runat="server" ID="lblMensaje" Visible="false" ForeColor="Red"></asp:Label>
                </div>
            <br />
            <asp:Button ID="btnHistorial" runat="server" Text="Ver historial de propinas" CssClass="btn btn-primary" OnClick="BtnHistorial_Click" />
            <asp:Button ID="brnSalir" runat="server" Text="Cerrar sesión" CssClass="btn btn-primary" OnClick="BtnSalir_Click" />
    </div>            
</asp:Content>
