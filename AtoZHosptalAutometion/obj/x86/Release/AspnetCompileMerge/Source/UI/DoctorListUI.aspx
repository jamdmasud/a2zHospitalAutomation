<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorListUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.DoctorListUI" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctors here</title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../scripts/jquery-3.1.0.js"></script>
    <script src="../scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="../scripts/bootstrap.js"></script>
    
    
    <script>
        var count = 1;
        $(function () {
            BindGridview();
        });
        function BindGridview() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "DoctorListUI.aspx/BindGridview",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var result = data.d; 
                    //alert(result[0].Id+ ' '+ result[0].Amount + ' ' + result[0].Description + ' ' + result[0].ExpenseType);
                    // alert(result.length);
                    for (var i = 0; i < result.length; i++) {
                        $("#gvDetails").append('<tr><td>' + count + '</td><td>' + result[i].Code + '</td><td>' + result[i].Name + '</td><td>' + result[i].Specialist + '</td><td>' + result[i].PermanetHospital + '</td><td>' + result[i].Mobile + '</td><td>' + result[i].Email + '</td><td> <img src=delete.png onclick=deleterecords(' + result[i].Id + ')> </td></tr>');
                        count += 1;
                    }                   
                },
                error: function (data) {
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                }
            });
        }

        function deleterecords(id) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "DoctorListUI.aspx/DeleteItem",
                data: "{'id':'" + id + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'true')
                        window.location.reload();

                },
                error: function (data) {
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                }
            });
        }


    </script>

</head>
<body>


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
                                            <li><a href="../UI/DipositListUI.aspx">Deposite Book</a></li>
                                            <li class="active"><a href="../UI/DueListUI.aspx">Due Collection</a></li>
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
                                <asp:GridView runat="server" ID="gvDetails" CssClass="table table-hover table-responsive">
                                    <HeaderStyle BackColor="sandybrown" ForeColor="white" BorderColor="white"></HeaderStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

