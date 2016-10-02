<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AtoZHosptalAutometion._default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="scripts/jquery-3.1.0.js"></script>
    <script src="scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="scripts/bootstrap.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="titl-bar">
                <p>A2Z Hospital Automation</p>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"><span class="glyphicon glyphicon-log-out" style="float: right"></span></asp:LinkButton>

            </div>
            <div class="container">
                <div class="row">
                    <div class="col-md-3 col-sm-3 side-bar">
                        <a href="/default.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-home"></span>Home
                            </div>
                        </a>
                        <a href="#">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-th-large"></span>Dashboard
                            </div>
                        </a>
                        <a href="/UI/IndoorSeriviceUI.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-file"></span>Indoor Service
                            </div>
                        </a>
                        <a href="/UI/SalesMedicine.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-shopping-cart"></span>Sales Medicine
                            </div>
                        </a>
                        <a href="/UI/PurchaseMedicine.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-briefcase"></span>Purchase Medicine
                            </div>
                        </a>
                        <a href="/UI/OutdoorServiceUI.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-usd"></span>Outdoor Service
                            </div>
                        </a>
                        <a href="/UI/ShowExpensesByDate_new.aspx">
                            <div class="btn-full col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-calendar"></span>Show Expense
                            </div>
                        </a>
                        <a href="/UI/AddExpense.aspx">
                            <div class="btn-full btn-full-last col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-usd"></span>Add Expense
                            </div>
                        </a>
                        <a href="/UI/PatienEntry.aspx" style="border-top: 1px solid #fafad2">
                            <div class="btn-full btn-full-last col-md-3 col-sm-3 col-xs-3">
                                <span class="glyphicon glyphicon-user"></span>Patient Entry
                            </div>
                        </a>
                    </div>
                    <div class="col-md-9 col-sm-9 col-xs-9" style="padding: 0">

                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <a href="#">
                                    <div class="col-md-5 col-sm-5 col-xs-5 btn btn-default" style="margin: 10px; text-align: center; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-th-large ico"></span>Daily Diposit
                                    </div>
                                </a>
                                <a href="#">
                                    <div class="col-md-5 col-sm-5 col-xs-5 btn btn-default" style="margin: 10px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-random ico"></span>Summery
                                    </div>
                                </a>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <a href="UI/SalesMedicine.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; text-align: center; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-shopping-cart ico"></span>Sales Medicine
                                    </div>
                                </a>
                                <a href="/UI/PurchaseMedicine.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-briefcase ico"></span>Purchase Medicine
                                    </div>
                                </a>
                                <a href="/UI/SaveMedicine.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-floppy-disk ico"></span>Save Medicine
                                    </div>
                                </a>
                                <a href="/UI/MedicineSearch.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-search ico"></span>Medicine Search
                                    </div>
                                </a>
                                <a href="/UI/MedicineStorage.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-hdd ico"></span>Medicine Storage
                                    </div>
                                </a>
                                <a href="/UI/DuePaymentUI.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-yen ico"></span>Due payment
                                    </div>
                                </a>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <a href="/UI/ReportDeliveryCheckUI.aspx">
                                    <div class="col-md-6 col-sm-6 col-xs-6 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-refresh ico"></span>Report Delivery
                                    </div>
                                </a>
                                <a href="/UI/AddExpense.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; text-align: center; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-yen ico"></span>Save Expense
                                    </div>
                                </a>
                                <a href="/UI/AddServicesUI.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-saved ico"></span>Save Service
                                    </div>
                                </a>
                                <a href="/UI/AddTestUI.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-glass ico"></span>Save Pathology test
                                    </div>
                                </a>
                                <a href="/UI/DoctorRegistration.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-star ico"></span>Doctor Registration
                                    </div>
                                </a>
                                <a href="/UI/RegisterAgent.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-user ico"></span>Agent Registration
                                    </div>
                                </a>
                                <a href="/UI/PatienEntry.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-save-file ico"></span>Patient  Registration
                                    </div>
                                </a>
                                <a href="/UI/PatientAdmissionUI.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-text-background ico"></span>Patient Admission
                                    </div>
                                </a>
                                <a href="/UI/IndoorSeriviceUI.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-file ico"></span>Indoor Service
                                    </div>
                                </a>
                                <a href="/UI/OutdoorServiceUI.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-usd ico"></span>Outdoor Service
                                    </div>
                                </a>
                                <a href="/UI/ShowExpensesByDate_new.aspx">
                                    <div class="col-md-3 col-sm-3 col-xs-3 btn btn-default" style="margin: 12px; height: 100px; padding-top: 35px">
                                        <span class="glyphicon glyphicon-open ico"></span>Show Expense
                                    </div>
                                </a>

                            </div>
                        </div>

                        <script>
                            var h = $('body').height();
                            console.log(h);
                            $('.side-bar').css("height", h);
                        </script>

                    </div>
                </div>



            </div>
        </div>
    </form>
</body>
</html>

