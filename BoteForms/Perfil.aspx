<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="BoteForms.Perfil" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <h2><asp:Label ID="MensajeBienvenida" runat="server" Text="" /></h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Total de bote:</h2>
            <asp:Label runat="server" ID="lblErrorBote" ForeColor="Red"></asp:Label>
            <div class="input-group">
                <asp:TextBox ID="txtBote" runat="server" CssClass="form-control" type="number" min="1" max="1000000" placeholder="Total del bote recaudado" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Trabajadores:</h2>
        </div>
    </div>
    <div class="container">
        <table class="table">
            <thead>
                <tr>
                    <th></th>
                    <th><asp:CheckBox ID="chkHorasPredefinidas" runat="server" Text=" &nbsp Marca esto si quieres recuperar las horas de la última sesión" AutoPostBack="true" OnCheckedChanged="CheckBoxSeleccionado" /></th>
                    <th></th>
                </tr>
            </thead>
        </table>
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
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="BtnLimpiar_Click" />
            <asp:Button ID="BtnAddWorker" runat="server" Text="Añadir Trabajador" OnClientClick="openModal(); return false;" />

            <!-- Modal for Adding Worker -->
            <div id="addWorkerModal" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeModal()">&times;</span>
                    <h2>Añadir Trabajador</h2>
                    <asp:TextBox ID="txtNewWorkerName" runat="server" Placeholder="Nombre del trabajador"></asp:TextBox>
                    <asp:TextBox ID="txtNewWorkerHours" runat="server" Placeholder="Horas semanales" TextMode="Number"></asp:TextBox>
                    <asp:Button ID="btnSaveWorker" runat="server" Text="Guardar" OnClick="btnSaveWorker_Click" />
                    <asp:Button ID="btnCancelWorker" runat="server" Text="Cancelar" OnClientClick="closeModal(); return false;" />
                </div>
            </div>

            <script>
                var modal;
                var span;

                window.onload = function () {
                    modal = document.getElementById("addWorkerModal");
                    span = document.getElementsByClassName("close")[0];

                    span.onclick = function () {
                        modal.style.display = "none";
                    }

                    window.onclick = function (event) {
                        if (event.target == modal) {
                            modal.style.display = "none";
                        }
                    }
                };

                function openModal() {
                    modal.style.display = "block";
                }

                function closeModal() {
                    modal.style.display = "none";
                    return false;
                }
            </script>

            <br />
            <asp:Label runat="server" ID="lblMensaje" Visible="false" ForeColor="Red"></asp:Label>
        </div>
        <br />
        <asp:Button ID="btnHistorial" runat="server" Text="Ver historial de propinas" CssClass="btn btn-primary" OnClick="BtnHistorial_Click" />
        <asp:Button ID="brnSalir" runat="server" Text="Cerrar sesión" CssClass="btn btn-primary" OnClick="BtnSalir_Click" />
    </div>
</asp:Content>
