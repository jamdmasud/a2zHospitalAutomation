<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportDeliveryCheckUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.ReportDeliveryCheckUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Status</title>
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
                                            <li><a href="../UI/AddServicesUI.aspx">Save Service </a></li>
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
                                    <li class="active"><a href="../UI/ReportDeliveryCheckUI.aspx">Report Delivery</a></li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li><a href="#"><span class="glyphicon glyphicon-user"></span>Sign Up</a></li>
                                    <li><a href="#"><span class="glyphicon glyphicon-log-in"></span>Login</a></li>
                                </ul>
                            </div>
                        </nav>
                        <!--Navbar end-->


                        <div class="col-md-8 col-xs-8 col-md-offset-2 col-xs-offset-2">
                            <div class="panel panel-success">
                                <div class="panel panel-heading">
                                    <p>Show Report Status</p>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <div class="panel panel-body">
                                    <div class="form-inline">
                                        <div class="col-md-10 col-xs-10 col-xs-offset-1 padding-bottom-10">
                                            <div class="form-group padding-bottom-10">
                                                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="Invoice No:"></asp:Label>
                                                <asp:TextBox ID="invoiceIDTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10 col-xs-10 col-xs-offset-1 padding-bottom-10">
                                            <asp:Button ID="showResultButton" CssClass="btn btn-success" runat="server" Text="Show Expense" OnClick="showResultButton_Click" />
                                        </div>
                                    </div>

                                    <br />
                                    <div class="GridviewDiv">
                                        <asp:GridView CssClass="table table-hover table-responsive" runat="server" ID="GridView" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate><%# ((GridViewRow)Container).RowIndex + 1%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Patient Name">
                                                    <ItemTemplate><%# Eval("PatienName") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate><%# Eval("GrandTotal") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Due">
                                                    <ItemTemplate><%# Eval("Due") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paid">
                                                    <ItemTemplate><%# Eval("Paid") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Delivered">
                                                    <ItemTemplate><%# Eval("IsProductDelivered") %></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="col-md-12">
                                            
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="deliveryDropDownList" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="deliveryDropDownList_SelectedIndexChanged">
                                                    <asp:ListItem Value="">Select result</asp:ListItem>
                                                    <asp:ListItem Value="1">True</asp:ListItem>
                                                    <asp:ListItem Value="0">False</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            
                                        </div>
                                        

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

