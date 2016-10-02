<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyDipositReportViewer.aspx.cs" Inherits="AtoZHosptalAutometion.UI.ReportForm.DailyDipositReportViewer" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diposit Report View</title>
    <link href="/Content/Site.css" rel="stylesheet" />
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="col-md-10 col-md-offset-1" style="border: 2px solid gray">
        <!--test Navbar start-->
                        <nav class="navbar navbar-inverse" style="padding-right: 10px">
                            <div class="container-fluid">
                                <ul class="nav navbar-nav">
                                    <li><a href="../../default.aspx">Home</a></li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Sold <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../../UI/ServiceSaleListUI.aspx">Sold Service</a></li>
                                            <li><a href="../../UI/MedicineSaleListUI.aspx">Sold Medicine</a></li>
                                        </ul>
                                    </li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">View <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../../UI/ShowExpensesByDate_new.aspx">View Expense</a></li>
                                            <li class="active"><a href="../../UI/DipositListUI.aspx">Deposite Book</a></li>
                                            <li><a href="../../UI/DueListUI.aspx">Due Collection</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="../../UI/AddExpense.aspx">Save Expense</a></li>
                                    <li><a href="../../UI/AgentPayment.aspx">Agent Payment</a></li>
                                    <li><a href="../../UI/DoctorRegistration.aspx">Register Doctor</a></li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li><a href="../../UI/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
                                </ul>
                            </div>
                        </nav>

                        <!--test Navbar end-->
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </div>
    </div>
    </form>
</body>
</html>
