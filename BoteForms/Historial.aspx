<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historial.aspx.cs" Inherits="BoteForms.Historial" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="col-md-4">
        <h2>Historial de propinas</h2>
         <div class="container">
        <table class="table">
            <thead>
                <tr>
                    <th>Nombre Trabajador</th>                
                    <th>Último bote percibido</th>
                    <th>Bote acumulado</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder ID="phTrabajadores" runat="server"></asp:PlaceHolder>
            </tbody>
        </table>

        <asp:panel runat="server" ID="Botones">
        <div class="input-group-append">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="BtnVolver_Click" />
                    <asp:Button ID="btnSalir" runat="server" Text="Cerrar sesión" CssClass="btn btn-primary" OnClick="BtnSalir_Click" />
                </div>        
            </asp:panel>
    </div>



             </div>            
</asp:Content>

    

