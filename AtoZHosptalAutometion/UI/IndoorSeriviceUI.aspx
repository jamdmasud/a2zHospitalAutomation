<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndoorSeriviceUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.IndoorSeriviceUI" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Indoor Bill</title>

    <link href="../Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../scripts/jquery-3.1.0.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">
        var prodid = 0, opstatus = '';
        var count = 1, rate = 0;

        $(function () {
            BindGridview();
        });
        function BindGridview() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "IndoorSeriviceUI.aspx/BindGridview",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    var tot = 0;
                    //alert(result[0].Id+ ' '+ result[0].Amount + ' ' + result[0].Description + ' ' + result[0].ExpenseType);
                    //alert(result.length);
                    for (var i = 0; i < result.length; i++) {
                        $("#gvService").append('<tr><td>' + count + '</td><td>' + result[i].Code + '</td><td>' + result[i].TestName + '</td><td>' + result[i].Quantity + '</td><td>' + result[i].Rate + '</td><td>' + result[i].Amount + '</td><td> <img src=delete.png onclick=deleterecords(' + result[i].Id + ')> </td></tr>');
                        tot += result[i].Amount;
                        count += 1;
                    }
                    //document.getElementById("sumTotalLabel").innerHTML = tot;
                    $('#sumTotalLabel').val(tot);
                    var totals = parseFloat($('#sumTotalLabel').val());
                    var vate = parseFloat($('#vat').val());
                    var vat = (totals / 100) * vate;
                    $('#txtVat').val(vat);


                    var total = parseFloat($('#sumTotalLabel').val());
                    var discount = parseFloat($('#txtDiscount').val());
                    var vats = parseFloat($('#txtVat').val());
                    total = parseFloat(total + vats);
                    var grTotal = total - discount;
                    $('#txtGrandTotal').val(grTotal);

                    var gTotal = parseInt($('#txtGrandTotal').val());
                    var advance = parseInt($('#txtAdvance').val());
                    var due = gTotal - advance;
                    $('#txtDue').val(due);
                    // Number conver to integer
                    ConvertNumberToWor(tot);
                },
                error: function (data) {
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                }
            });
        }
        function ConvertNumberToWor(tot) {
            //alert("working");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "IndoorSeriviceUI.aspx/ConvertToString",
                data: "{'data':'" + tot + "'}",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    if (result != 'Zero')
                        $('#wordToText').val("In word: " + result);
                    console.log(result);
                },
                error: function (data) {
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                }
            });
        }

        $(document).ready(function () {

            $("#<%= particularTextbox.ClientID%>").focusout(function () {
                rate = $('#particularTextbox').val();
                //alert(rate);
                GetRate(rate);
            });
            $("#qtyTextbox").focusout(function () {
                var qty = $('#qtyTextbox').val();
                var rt = $('#rateTextbox').val();
                //alert(v);
                var total = qty * rt;
                //alert(total);
                $("#totalTextbox").val(total);
            });


            $('#vat').focusout(function () {
                var total = parseFloat($('#sumTotalLabel').val());
                var vats = parseFloat($('#vat').val());
                var vat = (total / 100) * vats;
                $('#txtVat').val(vat);
            });
            $('#txtDiscount').focusout(function () {
                var total = parseFloat($('#sumTotalLabel').val());
                var discount = parseFloat($('#txtDiscount').val());
                var vats = parseFloat($('#txtVat').val());
                total = parseFloat(total + vats);

                var gTotal = total - discount;
                $('#txtGrandTotal').val(gTotal);
            });
            $('#txtAdvance').focusout(function () {
                var gTotal = parseInt($('#txtGrandTotal').val());
                var advance = parseInt($('#txtAdvance').val());
                var due = gTotal - advance;
                $('#txtDue').val(due);
            });


            var b = $('body').height();
            $('.side-bar').css('height', b);
        });

            function GetRate(rate) {
                //alert(rate);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "IndoorSeriviceUI.aspx/GetProductRate",
                    data: "{'purticular':'" + rate + "'}",
                    dataType: "json",
                    success: function (data) {
                        var result = data.d;
                        $('#rateTextbox').val(result);
                    },
                    error: function (data) {
                        // alert('problem here');
                        var r = data.responseText;
                        var errorMessage = r.Message;
                        alert(errorMessage);
                    }
                });
            }

            function deleterecords(id) {
                insertupdatedata(id, '', 0, 0, 0, 'DELETE');
            }
            function insertupdatedata(id, particular, quantity, rate, total, status) {
                if (prodid != 0 && opstatus == 'UPDATE')
                    id = prodid;

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "IndoorSeriviceUI.aspx/crudoperations",
                    data: "{'id':'" + id + "','particular':'" + particular + "','quantity':'" + quantity + "','rate':'" + rate + "','total':'" + total + "','status':'" + status + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'true')
                            window.location.reload();
                            //BindGridview();
                    },
                    error: function (data) {
                        //alert("faield");
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
                        <a href="../UI/IndoorSeriviceUI.aspx"><div class="btn-full col-md-3 col-sm-3 col-xs-3">
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


                    <div class="col-md-9 col-sm-9 col-xs-9 content" style="padding: 0">
                        
                           <!--Navbar start-->
                        <nav class="navbar navbar-inverse" style="padding-right: 10px">
                            <div class="container-fluid">
                                <ul class="nav navbar-nav">
                                    <li><a href="../default.aspx">Home</a></li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Services<span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li class="active"><a href="../UI/IndoorSeriviceUI.aspx">Indoor Service</a></li>
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
                                            <li><a href="../UI/PatientAdmissionUI.aspx">Patient Admission</a></li>
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
                        

                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <%--   content goes here--%>
                        <div class="col-md-9 col-sm-9 col-xs-9 col-sm-offset-2 col-md-offset-2 col-xs-offset-2 padding-top-30">
                            <div class="row">
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <p class="panel-title">Indoor Bill</p>
                                    </div>
                                    <div class="panel-body">
                                        <asp:Panel ID="successPanel" CssClass="alert alert-success" Visible="False" runat="server">
                                            <h3>Indoor saved successfully!</h3>
                                        </asp:Panel>
                                        <asp:Panel ID="faildPanel" CssClass="alert alert-danger" Visible="False" runat="server">
                                            <h3>
                                                <asp:Label ID="faildLabel" runat="server"></asp:Label></h3>
                                        </asp:Panel>
                                        <fieldset>
                                            <legend>Customer Information</legend>

                                            <div class="form-inline">
                                                <div class="form-group padding-bottom-10">
                                                    <label class="control-label" for="<%= patientTextBox.ClientID %>">Patient</label>
                                                    <asp:TextBox ID="patientTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ServiceMethod="SearchPatient"
                                                        MinimumPrefixLength="1"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                        TargetControlID="patientTextBox"
                                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                                <div class="form-group padding-bottom-10">
                                                    <label class="control-label" for="<%= doctorTextBox.ClientID %>">Doctor</label>
                                                    <asp:TextBox ID="doctorTextBox" placeholder="Doctor Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ServiceMethod="SearchDoctor"
                                                        MinimumPrefixLength="1"
                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                        TargetControlID="doctorTextBox"
                                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                    </cc1:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </fieldset>
                                        <div class="col-md-6 col-xs-6 col-xs-offset-3">
                                            <div class="row">
                                                <fieldset>
                                                    <legend>Indoor Order</legend>

                                                    <div class="form-horizontal">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <div class="form-group margin-right-20">
                                                                        <asp:TextBox ID="particularTextbox" class="form-control" placeholder="Particulars" runat="server" />
                                                                        <cc1:AutoCompleteExtender ServiceMethod="SearchProduct"
                                                                            MinimumPrefixLength="1"
                                                                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                            TargetControlID="particularTextbox"
                                                                            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
                                                                        </cc1:AutoCompleteExtender>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="form-group">
                                                                        <input id="qtyTextbox" type="text" class="form-control" placeholder="Quantity" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div class="form-group margin-right-20">
                                                                        <input id="rateTextbox" type="text" class="form-control" placeholder="Rate" />
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="form-group">
                                                                        <input id="totalTextbox" type="text" class="form-control" placeholder="Total" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <input type="button" id="submitServiceButton" class="btn btn-info btn-block margin-bottom-10"
                                                        value="Add" onclick="insertupdatedata('0', $('#particularTextbox').val(), $('#qtyTextbox').val(), $('#rateTextbox').val(), $('#totalTextbox').val(), 'INSERT')" />
                                                </fieldset>
                                            </div>
                                        </div>
                                        <div class="col-md-10 col-xs-10 col-md-offset-1">
                                            <div class="col-md-12 col-xs-12" style="background: #696969; text-align: center; font-size: 16px; font-weight: 700">Order Table</div>
                                            <asp:GridView ID="gvService" CssClass="table table-responsive table-hover" runat="server"></asp:GridView>

                                            <div class="col-md-6 col-sm-6 col-xs-6" style="background: #e0ffff">
                                                <input id="wordToText" runat="server" type="text" readonly="readonly" style="border: none; background: none; width: 100%" />
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-6" style="background: #f5f5dc">
                                                <table class="table table-responsive table-condensed">
                                                    <tr>
                                                        <th>Total: </th>
                                                        <td>
                                                            <input type="text" runat="server" id="sumTotalLabel" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Vat:<input type="number" value="0" step="any" id="vat" style="width: 30px" /></td>
                                                        <td>
                                                            <input type="text" runat="server" id="txtVat" value="0" readonly="readonly" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Discount: </td>
                                                        <td>
                                                            <input type="text" runat="server" value="0" id="txtDiscount" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Grand Total: </td>
                                                        <td>
                                                            <input type="text" runat="server" id="txtGrandTotal" value="0" readonly="readonly" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Advance: </td>
                                                        <td>
                                                            <input type="text" runat="server" value="0" id="txtAdvance" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Due: </td>
                                                        <td>
                                                            <input type="text" runat="server" id="txtDue" value="0" readonly="readonly" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>AgentId</td>
                                                        <td>
                                                            <asp:TextBox ID="txtAget" runat="server"></asp:TextBox>
                                                            <cc1:AutoCompleteExtender ServiceMethod="SearchAgent"
                                                                MinimumPrefixLength="1"
                                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                TargetControlID="txtAget"
                                                                ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false">
                                                            </cc1:AutoCompleteExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Amount:</td>
                                                        <td>
                                                            <input type="text" runat="server" value="0" id="txtHonorarium" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <asp:Button runat="server" ID="submitButton" Text="Submit" CssClass="btn btn-success btn-block" OnClick="submitButton_Click" />
                                        <asp:Button ID="printButton" CssClass="btn btn-default btn-block" Visible="False" runat="server" Text="Print Report" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- content close here --%>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

