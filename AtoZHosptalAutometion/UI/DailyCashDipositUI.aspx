<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyCashDipositUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.DailyCashDipositUI" %>
<%@ Import Namespace="AtoZHosptalAutometion.Models" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diposit Cash
    </title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../scripts/jquery-3.1.0.js"></script>
    <script src="../scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="../scripts/bootstrap.js"></script>
    <style>
        #getButton{margin-top:20px}
    </style>
</head>
<body>
     <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="titl-bar">
                <p>Hospital Automation</p>
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
                                <span class="glyphicon glyphicon-calendar"></span>Show Expense
                            </div>
                        </a>
                        <a href="../UI/AddExpense.aspx">
                            <div class="btn-full btn-full-last col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-usd"></span>Add Expense
                            </div>
                        </a>
                        <a href="../UI/PatienEntry.aspx" style="border-top: 1px solid #fafad2">
                            <div class="btn-full btn-full-last col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-user"></span>Patient Entry
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
                                            <li><a href="../UI/ShowExpensesByDate_new.aspx">View Expense</a></li>
                                            <li class="active"><a href="../UI/DipositListUI.aspx">Deposite Book</a></li>
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
                        <div class="col-md-10 col-sm-10 col-xs-10 col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="form-inline" style="margin-bottom: 30px">
                                    <div class="form-group">
                                        <asp:DropDownList ID="serviceDropDownList" CssClass="form-control" runat="server">
                                            <asp:ListItem Selected="True" Value="0">Select Type</asp:ListItem>
                                            <asp:ListItem>Reception</asp:ListItem>
                                            <asp:ListItem>Pharmacy</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        User Id:
                                        <asp:TextBox ID="userIdTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        Date of Deal:
                                        <input id="dateTextBox" class="form-control" runat="server"/>
                                    </div>
                                    <asp:Button ID="getButton" CssClass="btn btn-info margin-top-15" runat="server" Text="Get Total" OnClick="getButton_Click" />
                                </div>
                            </div>
                            
                            <div class="col-md-4 col-sm-4 col-xs-4 col-md-offset-4 col-sm-offset-4 col-xs-offset-4" style="font-weight: 800;  text-align: center; margin:15px">
                               <asp:GridView ID="medicineGridView" CssClass="table table-responsive table-hover" runat="server" AutoGenerateColumns="False">
                                <HeaderStyle ForeColor="White" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#A55129"></HeaderStyle>
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate><%# Eval("Total") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Cash">
                                        <ItemTemplate><%# Eval("Paid") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due">
                                        <ItemTemplate><%# Eval("Due") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate><%# Eval("Discount") %></ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                                <asp:Label ID="Label781" runat="server" Text="Collected Due: "></asp:Label>
                                <asp:Label ID="lblcolDue" runat="server" BorderColor="gray"></asp:Label>
                            </div>
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="form-inline" style="margin-bottom: 30px">
                                    
                                    <div class="form-group">
                                        <asp:TextBox ID="dealingDateTextBox" Visible="False" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="amountTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="postButton" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="postButton_Click" OnClientClick = "Confirm()" />
                                    <asp:Button ID="voucheButton" CssClass="btn btn-default" runat="server" Text="Print Voucher" Visible="False" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
    <script src="../scripts/jquery-1.10.2.js"></script>
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
    <script>
        $(function () {
            //$("#datepicker").datepicker();
            $('#dateTextBox').datepicker({
                dateFormat: 'yy/mm/dd',
                onSelect: function (datetext) {

                    var d = new Date(); // for now

                    var h = d.getHours();
                    h = (h < 10) ? ("0" + h) : h;

                    var m = d.getMinutes();
                    m = (m < 10) ? ("0" + m) : m;

                    var s = d.getSeconds();
                    s = (s < 10) ? ("0" + s) : s;

                    datetext = datetext + " " + h + ":" + m + ":" + s;

                    $('#dateTextBox').val(datetext);
                }
            });
            $('#dealingDateTextBox').datepicker();

            var h = $('body').height();
            $('.side-bar').css("height", h);
        });
    </script>
</body>
</html>


