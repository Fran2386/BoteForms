<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BoteForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 <div class="row">
        <div class="col-md-4">
            <h2>Total de bote:</h2>
            <div class="input-group">
                <asp:TextBox ID="txtBote" runat="server" CssClass="form-control" type="number"/>
                <div class="input-group-append">
                    <asp:Button ID="btnCalcular" runat="server" Text="Calcular" CssClass="btn btn-primary" OnClick="BtnCalcularClick" />
                </div>
                <div>
            <asp:Button ID="btnConnect" runat="server" Text="Connect to Database" OnClick="btnConnect_Click" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
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
            <asp:RadioButton ID="hrsEditables" runat="server" Text="Horas editables" GroupName="opciones" AutoPostBack="true" OnCheckedChanged="RadioButttonSeleccionado" />
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
                <tr>
                    <td>
                        <asp:CheckBox ID="chkAni" runat="server" Text="Ani"  CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAni" runat="server" type="number" Enabled="false" CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblAni" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkCris" runat="server" Text="Cristina" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCris" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblCris" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkDiana" runat="server" Text="Diana" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtDiana" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblDiana" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkFran" runat="server" Text="Fran" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFran" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblFran" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkMarina" runat="server" Text="Marina" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtMarina" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblMarina" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkVictor" runat="server" Text="Victor" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVictor" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblVictor" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkYoli" runat="server" Text="Yolanda" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtYoli" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblYoli" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExtra1" runat="server" Text="Extra1" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtExtra1" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblExtra1" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkExtra2" runat="server" Text="Extra2" CssClass="checkbox" OnCheckedChanged="chkHabilitarTextBox" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtExtra2" runat="server" type="number" Enabled="false"  CssClass="form-control" />
                    </td>
                    <td>
                        <asp:Label ID="lblExtra2" runat="server" CssClass="form-control" Text="" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

</asp:Content>
