<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddServicesUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.AddServicesUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyPlaceHolder" runat="server">
    
    
  <!--Navbar start-->
                        <nav class="navbar navbar-inverse" style="padding-right: 10px">
                            <div class="container-fluid">
                                <ul class="nav navbar-nav">
                                    <li><a href="../default.aspx">Home</a></li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Store <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../UI/IndoorSeriviceUI.aspx">Indoor Service</a></li>
                                            <li><a href="../UI/OutdoorServiceUI.aspx">Outdoor Service</a></li>
                                        </ul>
                                    </li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Store <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../UI/AddExpense.aspx">Save Expense</a></li>
                                            <li class="active"><a href="../UI/AddServicesUI.aspx">Save Service </a></li>
                                            <li><a href="../UI/AddTestUI.aspx">Save Pathology Test</a></li>
                                        </ul>
                                    </li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Registration<span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../UI/PatientAdmissionUI.aspx">Patient Admission</a></li>
                                            <li><a href="../UI/PatienEntry.aspx">Register Patient</a></li>
                                            <li><a href="../UI/DoctorRegistration.aspx">Register Doctor</a></li>
                                            <li><a href="../UI/RegisterAgent.aspx">Register Agent</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="../UI/ShowExpensesByDate_new.aspx">Show Expense</a></li>
                                    <li><a href="../UI/ReportDeliveryCheckUI.aspx">Report Delivery</a></li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li><a href="#"><span class="glyphicon glyphicon-user"></span>Sign Up</a></li>
                                    <li><a href="#"><span class="glyphicon glyphicon-log-in"></span>Login</a></li>
                                </ul>
                            </div>
                        </nav>
                        <!--Navbar end-->    

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
                        <label class="col-md-4 col-xs-4 control-label">Service Name: </label>
                        <div class="col-md-8 col-xs-8 form-group">
                            <asp:TextBox ID="serviceNameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <label class="col-md-4 col-xs-4 control-label">Price: </label>
                        <div class="col-md-8 col-xs-8 form-group">
                            <asp:TextBox ID="rateTextBox" CssClass="form-control" runat="server"></asp:TextBox>
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
