<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgentRegistrationSuccess.aspx.cs" Inherits="AtoZHosptalAutometion.UI.AgentRegistrationSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceHolder" runat="server">
    Registration Successfull
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyPlaceHolder" runat="server">
    <div class="col-md-8 col-xs-8 col-sm-offset-2 jumbotron padding-top" style="padding-top: 30px">
        <div class="alert alert-success">
            <asp:Label ID="statusLabel" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
