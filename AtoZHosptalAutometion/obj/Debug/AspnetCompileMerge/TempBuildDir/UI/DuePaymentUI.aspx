<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DuePaymentUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.DuePaymentUI" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    </title>
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


                        
        <nav class="navbar navbar-inverse" style="padding-right: 10px">
        <div class="container-fluid">
            <ul class="nav navbar-nav">
                <li><a href="../default.aspx">Home</a></li>
                <li><a href="../UI/SalesMedicine.aspx">Sale Medicine</a></li>
                <li><a href="../UI/PurchaseMedicine.aspx">Purchase Medicine</a></li>
                <li class="active"><a href="../UI/SaveMedicine.aspx">save Medicine</a></li>
                <li><a href="../UI/MedicineSearch.aspx">Search Medicine</a></li>
                <li><a href="../UI/MedicineStorage.aspx">Medicine storage</a></li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="../UI/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
            </ul>
        </div>
    </nav>
        

    <div class="col-md-9 col-sm-9 col-xs-9" style="padding: 0">

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
                            <asp:Button ID="showResultButton" CssClass="btn btn-success" runat="server" Text="Show Due" OnClick="showResultButton_Click"  />
                        </div>
                    </div>

                    <br />
                    <div class="GridviewDiv">
                        <asp:GridView CssClass="table table-hover table-responsive" runat="server" ID="GridView1" AutoGenerateColumns="False">
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
                        <div class="col-md-12 form-inline">

                            <div class="form-group">
                                Due:
                                <asp:TextBox ID="dueTextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            
                                <asp:Button ID="submitButton" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="submitButton_Click" />
                            
                                <asp:Button ID="printButton" CssClass="btn btn-info" runat="server" Text="Print Receipt" Visible="False" PostBackUrl="~/UI/ReportForm/IndoorServiceViewer.aspx"/>
                          
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

