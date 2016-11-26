﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowExpensesByDate_new.aspx.cs" Inherits="AtoZHosptalAutometion.UI.ShowExpensesByDate_new" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Show Expense
    </title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../scripts/jquery-3.1.0.js"></script>
    <script src="../scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="../scripts/bootstrap.js"></script>
    <script>
        $(document).ready(function () {
            var h = $('body').height();
            $('.side-bar').css('height', h);
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="titl-bar">
                <p>A2Z Hospital Automation</p>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-md-3 col-sm-3 side-bar">
                        <a href="../default.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-home"></span>Home
                            </div>
                        </a>
                        <a href="#">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-th-large"></span>Dashboard
                            </div>
                        </a>
                        <a href="../UI/IndoorSeriviceUI.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-file"></span>Indoor Service
                            </div>
                        </a>
                        <a href="../UI/SalesMedicine.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-shopping-cart"></span>Sales Medicine
                            </div>
                        </a>
                        <a href="../UI/PurchaseMedicine.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-briefcase"></span>Purchase Medicine
                            </div>
                        </a>
                        <a href="../UI/OutdoorServiceUI.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-usd"></span>Outdoor Service
                            </div>
                        </a>
                        <a href="../UI/ShowExpensesByDate_new.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-plus"></span>Show Expense
                            </div>
                        </a>
                        <a href="../UI/AddExpense.aspx">
                            <div class="btn-full btn-full-last col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-earphone"></span>Add Expense
                            </div>
                        </a>
                        <a href="../UI/PatienEntry.aspx">
                            <div class="btn-full btn-full-last col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-earphone"></span>Patient Entry
                            </div>
                        </a>
                    </div>

                    <div class="col-md-9 col-sm-9 col-xs-9" style="padding: 0">

                        <!--test Navbar start-->
                        <nav class="navbar navbar-inverse" style="padding-right: 10px">
                            <div class="container-fluid">
                                <ul class="nav navbar-nav">
                                    <li><a href="../default.aspx">Home</a></li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Sold <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../UI/ServiceSaleListUI.aspx">Sold Service</a></li>
                                            <li><a href="../UI/MedicineSaleListUI.aspx">Sold Medicine</a></li>
                                        </ul>
                                    </li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">View <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li class="active"><a href="../UI/ShowExpensesByDate_new.aspx">View Expense</a></li>
                                            <li><a href="../UI/DipositListUI.aspx">Deposite Book</a></li>
                                            <li><a href="../UI/DueListUI.aspx">Due Collection</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="../UI/AddExpense.aspx">Save Expense</a></li>
                                    <li><a href="../UI/AgentPayment.aspx">Agent Payment</a></li>
                                    <li><a href="../UI/DoctorRegistration.aspx">Register Doctor</a></li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li><a href="../UI/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
                                </ul>
                            </div>
                        </nav>

                        <!--test Navbar end-->


                        <div class="col-md-8 col-xs-8 col-md-offset-2 col-xs-offset-2">
                            <div class="panel panel-success">
                                <div class="panel panel-heading">
                                    <p>View Expense</p>
                                </div>
                                <div class="panel panel-body">
                                    <div class="form-inline">
                                        <div class="col-md-10 col-xs-10 col-xs-offset-1 padding-bottom-10">
                                            <div class="form-group padding-bottom-10">
                                                <input type="text" runat="server" class="form-control" id="txtFromDate" placeholder="From Date" />
                                            </div>
                                            <div class="form-group padding-bottom-10">
                                                <input type="text" runat="server" class="form-control" id="txtTodate" placeholder="To Date" />
                                            </div>
                                        </div>
                                        <div class="col-md-10 col-xs-10 col-xs-offset-1 padding-bottom-10">
                                            <asp:Button ID="showExpenseButton" CssClass="btn btn-success" runat="server" Text="Show Expense" OnClick="showExpenseButton_Click" />
                                            <asp:Button ID="printExpenseButton" CssClass="btn btn-default" Visible="False" runat="server" Text="Print Expense" />
                                        </div>
                                    </div>

                                    <br />
                                    <div class="GridviewDiv">
                                        <asp:GridView CssClass="table table-hover table-responsive" runat="server" ID="GridView" AutoGenerateColumns="False">
                                            <HeaderStyle ForeColor="White" Font-Bold="True" BackColor="#A55129"></HeaderStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate><%# ((GridViewRow)Container).RowIndex + 1%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate><%# Eval("Description") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expensed by">
                                                    <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expense Date">
                                                    <ItemTemplate><%# Eval("ExpenseDate") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate><%# Eval("Amount") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-md-6 col-sm-6 col-xs-6 col-md-offset-6 col-sm-offset-6 col-xs-offset-6" style="background: #d3d3d3; font-weight: 800; border: 1px solid #d2b48c; border-radius: 5px; text-align: right">
                                            <strong>Total:</strong>
                                            <asp:Label ID="totalsLabel" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
                        <script src="../scripts/jquery-1.10.2.js"></script>
                        <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
                        <script>
                            $(function () {
                                //$("#datepicker").datepicker();
                                $('#txtFromDate').datepicker();
                                $('#txtTodate').datepicker();

                            });
                        </script>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

