<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SaveMedicine.aspx.cs" Inherits="AtoZHosptalAutometion.UI.PuchaseMedicine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceHolder" runat="server">
    Purchase Medicine
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyPlaceHolder" runat="server">
    <nav class="navbar navbar-inverse" style="padding-right: 10px">
        <div class="container-fluid">
            <ul class="nav navbar-nav">
                <li><a href="../default.aspx">Home</a></li>
                <li><a href="../UI/SalesMedicine.aspx">Sale Medicine</a></li>
                <li><a href="../UI/PurchaseMedicine.aspx">Purchase Medicine</a></li>
                <li class="active"><a href="../UI/SaveMedicine.aspx">save Medicine</a></li>
                <li><a href="#">Search Medicine</a></li>
                <li><a href="#">Medicine storage</a></li>
            </ul>
        </div>
    </nav>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-md-9 col-sm-9 col-xs-9 col-sm-offset-2 col-md-offset-2 col-xs-offset-2 padding-top-30">
        <div class="row">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <p class="panel-title">Save Medicine</p>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="successPanel" CssClass="alert alert-success" Visible="False" runat="server">
                        <h3>Medicine saved successfully!</h3>
                    </asp:Panel>
                    <asp:Panel ID="faildPanel" CssClass="alert alert-danger" Visible="False" runat="server">
                        <h3>
                            <asp:Label ID="faildLabel" runat="server"></asp:Label></h3>
                    </asp:Panel>
                    <div class="col-xs-10 col-sm-10 col-md-10 col-xs-offset-1">
                        <label class="col-md-4 col-xs-4 control-label">Medicine Name: </label>
                        <div class="col-md-8 col-xs-8 form-group">
                            <asp:TextBox ID="medicineNameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-md-4 col-xs-4 control-label">Group Name: </label>
                        <div class="col-md-8 col-xs-8 form-group">
                            <asp:TextBox ID="GroupNameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchGroups"
                                MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="GroupNameTextBox"
                                ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                            </cc1:AutoCompleteExtender>
                        </div>
                        <label class="col-md-4 col-xs-4 control-label">Company Name: </label>
                        <div class="col-md-8 col-xs-8 form-group">
                            <asp:TextBox ID="companyTextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchCompanies"
                                MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                TargetControlID="companyTextBox2"
                                ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                            </cc1:AutoCompleteExtender>
                        </div>
                        <label class="col-md-4 col-xs-4 control-label">Price: </label>
                        <div class="col-md-8 col-xs-8 form-group">
                            <asp:TextBox ID="priceTextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-md-4 col-xs-4 control-label">Quantity: </label>
                        <div class="col-md-8 col-xs-8 form-group">
                            <asp:TextBox ID="quantityTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-4 col-md-8">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
