<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExpense.aspx.cs" Inherits="AtoZHosptalAutometion.UI.AddExpense" %>

<!DOCTYPE html>

<html lang="us-en">
<head runat="server">
    <title>Add Expense</title>

    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../scripts/jquery-3.1.0.js"></script>
    <script src="../scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="../scripts/bootstrap.js"></script>
    <script type="text/javascript">
        var prodid = 0, opstatus = '';
        $(function () {
            BindGridview();

        });
        function BindGridview() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AddExpense.aspx/BindGridview",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    var tot = 0;
                    //alert(result[0].Id+ ' '+ result[0].Amount + ' ' + result[0].Description + ' ' + result[0].ExpenseType);
                    //alert(result.length);
                    for (var i = 0; i < result.length; i++) {
                        $("#gvDetails").append('<tr><td>' + result[i].Description + '</td><td>' + result[i].Amount + '</td><td> <img src=delete.png onclick=deleterecords(' + result[i].Id + ')> </td></tr>');
                        tot += result[i].Amount;
                    }
                    document.getElementById("sumTotal").innerHTML = "Total: " + tot;
                },
                error: function (data) {
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                }
            });
        }
        function deleterecords(id) {
            insertupdatedata(id, '', 0, '', '', 'DELETE');
        }
        function insertupdatedata(id, description, amount, expenseType, expenseDate, status) {
            if (prodid != 0 && opstatus == 'UPDATE')
                id = prodid;

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AddExpense.aspx/crudoperations",
                data: "{'id':'" + id + "','description':'" + description + "','amount':'" + amount + "','expenseType':'" + expenseType + "','expenseDate':'" + expenseDate + "','status':'" + status + "'}",
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
        function updatedata(productid, productname, price) {
            prodid = productid;
            $('#txtProduct').val(productname);
            $('#txtPrice').val(price);
            opstatus = 'UPDATE';
        }


        $(document).ready(function () {
            var height = $('body').height();
            $('.side-bar').css('height', height);
        });


    </script>


</head>
<body style="min-height: 620px">
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
                                            <li><a href="../UI/ShowExpensesByDate_new.aspx">View Expense</a></li>
                                            <li><a href="../UI/DipositListUI.aspx">Deposite Book</a></li>
                                            <li><a href="../UI/DueListUI.aspx">Due Collection</a></li>
                                        </ul>
                                    </li>
                                    <li class="active"><a href="../UI/AddExpense.aspx">Save Expense</a></li>
                                    <li><a href="../UI/AgentPayment.aspx">Agent Payment</a></li>
                                    <li><a href="../UI/DoctorRegistration.aspx">Register Doctor</a></li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li><a href="../UI/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
                                </ul>
                            </div>
                        </nav>

                        <!--test Navbar end-->
                        <%--<asp:ContentPlaceHolder ID="bodyPlaceHolder" runat="server">    --%>

                        <div class="col-md-8 col-xs-8 col-md-offset-2 col-xs-offset-2">
                            <div class="panel panel-success">
                                <div class="panel panel-heading">
                                    <p>Add Expense</p>
                                </div>
                                <div class="panel panel-body">
                                    <div class="form-inline">
                                        <div class="col-md-10 padding-bottom-10">
                                            <div class="form-group padding-bottom-10">
                                                <input type="text" class="form-control" id="txtDescription" placeholder="Description" />
                                            </div>
                                            <div class="form-group padding-bottom-10">
                                                <input type="text" class="form-control" id="txtAmount" placeholder="Amount" />
                                            </div>
                                            <div class="form-group padding-bottom-10">
                                                <input type="text" class="form-control" id="txtExpenseDate" placeholder="ExpenseDate" />
                                            </div>
                                            <div class="form-group padding-bottom-10">
                                                <input type="button" id="btnInsert" class="btn btn-info btn-block" value="Add Expense" onclick="insertupdatedata('0', $('#txtDescription').val(), $('#txtAmount').val(), $('#txtExpenseType').val(), $('#txtExpenseDate').val(), 'INSERT')" />
                                            </div>
                                        </div>
                                    </div>

                                    <br />
                                    <div class="GridviewDiv">
                                        <asp:GridView CssClass="table table-hover table-responsive" runat="server" ID="gvDetails">
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-4 col-xs-4 col-md-offset-6 col-xs-offset-6">
                                        <label id="sumTotal" style="font-size: 15px"></label>
                                    </div>
                                    <div class="col-md-10 col-xs-10 col-xs-offset-1">
                                        <asp:Button ID="submitButton" runat="server" CssClass="btn btn-success btn-block padding-bottom-10" Text="Submit" OnClick="submitButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- </asp:ContentPlaceHolder>--%>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
    <script>
        $(function () {
            //$("#datepicker").datepicker();
            $('#txtExpenseDate').datepicker().datepicker("setDate", new Date());
            $('#txtDescription').autocomplete({
                source: ["Dr. Food", "Salary", "Duty Doctor", "Honorarium", "Surgical Equipment", "X-ray Report",
                "Marketing", "shop", "Miking", "Surgion+Enestray", "Oxygen", "Altransno", "Electric Bill", "Printing Bill",
                "Mobile Bill", "Medicine", "Office", "Diesel", "Mosque/Donation", "Kistri", "Pathology", "Bank deposit",
                    "Pharmacy", "Others"]
            });
        });
    </script>
</body>
</html>

