<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientAdmissionUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.PatientAdmissionUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
    <link href="/Content/Site.css" rel="stylesheet" />
    <script src="/scripts/jquery-3.1.0.js"></script>
    <script src="/scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="/scripts/bootstrap.js"></script>

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



                        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
                        <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
                        <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
                        <script>
                            $(function() {
                                //$("#datepicker").datepicker();
                                $('#<%= admissionDateTextBox.ClientID %>').datepicker({
                                    dateFormat: 'yy-mm-dd'
                            });
            //$("#admissionDateTextBox").datepicker();
        });
                        </script>


                        <!--Navbar start-->
                        <nav class="navbar navbar-inverse" style="padding-right: 10px">
                            <div class="container-fluid">
                                <ul class="nav navbar-nav">
                                    <li><a href="../default.aspx">Home</a></li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Services<span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../UI/IndoorSeriviceUI.aspx">Indoor Service</a></li>
                                            <li><a href="../UI/OutdoorServiceUI.aspx">Outdoor Service</a></li>
                                        </ul>
                                    </li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Store <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../UI/AddServicesUI.aspx">Save Service </a></li>
                                            <li><a href="../UI/AddTestUI.aspx">Save Pathology Test</a></li>
                                        </ul>
                                    </li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Registration<span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li class="active"><a href="../UI/PatientAdmissionUI.aspx">Patient Admission</a></li>
                                            <li><a href="../UI/PatienEntry.aspx">Register Patient</a></li>
                                            <li><a href="../UI/RegisterAgent.aspx">Register Agent</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="../UI/ReportDeliveryCheckUI.aspx">Report Delivery</a></li>
                                </ul>
                               <ul class="nav navbar-nav navbar-right">
                                    <li><a href="../UI/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
                                </ul>
                            </div>
                        </nav>
                        <!--Navbar end-->
                        <!--Navbar end-->

                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="form-horizontal">
                            <div class="col-md-9 col-sm-9 col-xs-9 col-sm-offset-2 col-md-offset-2 col-xs-offset-2 padding-top-30">
                                <div class="row">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <p class="panel-title">Patient Admission</p>
                                        </div>
                                        <div class="panel-body">
                                            <asp:Panel ID="successPanel" CssClass="alert alert-success" Visible="False" runat="server">
                                                <h5>Patient registration completed successfully!</h5>
                                            </asp:Panel>
                                            <asp:Panel ID="faildPanel" CssClass="alert alert-danger" Visible="False" runat="server">
                                                <h5>
                                                    <asp:Label ID="faildLabel" runat="server"></asp:Label></h5>
                                            </asp:Panel>
                                            <div class="col-xs-10 col-sm-10 col-md-10 col-xs-offset-1">
                                                <div class="form-group">
                                                    <label class="col-md-4 col-xs-4 control-label">Patient Code: </label>
                                                    <div class="col-md-8 col-xs-8">
                                                        <asp:TextBox ID="codeTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ServiceMethod="SearchPatient"
                                                            MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="codeTextBox"
                                                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 col-xs-4 control-label">Guardian Name: </label>
                                                    <div class="col-md-8 col-xs-8">
                                                        <asp:TextBox ID="guardianTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 col-xs-4 control-label">Guardia Mobile: </label>
                                                    <div class="col-md-8 col-xs-8">
                                                        <asp:TextBox ID="guardianMobileTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 col-xs-4 control-label">Admission Date: </label>
                                                    <div class="col-md-8 col-xs-8">
                                                        <input type="text" runat="server" id="admissionDateTextBox" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 col-xs-4 control-label">Referenced By: </label>
                                                    <div class="col-md-8 col-xs-8">
                                                        <asp:TextBox ID="doctorNameTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ServiceMethod="SearchDoctor"
                                                            MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="doctorNameTextBox"
                                                            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-4 col-xs-4 control-label">Agent Name: </label>
                                                    <div class="col-md-8 col-xs-8">
                                                        <asp:TextBox ID="agentTextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ServiceMethod="SearchAgent"
                                                            MinimumPrefixLength="1"
                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                            TargetControlID="agentTextBox2"
                                                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                        </cc1:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-offset-4 col-md-8">
                                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Reset" />
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
            </div>
        </div>
    </form>
</body>
</html>
