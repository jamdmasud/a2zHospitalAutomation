<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseMedicine.aspx.cs" Inherits="AtoZHosptalAutometion.UI.PurchaseMedicine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Medicine</title>

    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../scripts/jquery-3.1.0.js"></script>
    <script src="../scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <script src="../scripts/bootstrap.js"></script>
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
                url: "PurchaseMedicine.aspx/BindGridview",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    var tot = 0;
                    //alert(result[0].Id+ ' '+ result[0].Amount + ' ' + result[0].Description + ' ' + result[0].ExpenseType);
                    // alert(result.length);
                    for (var i = 0; i < result.length; i++) {
                        $("#gvDetails").append('<tr><td>' + count + '</td><td>' + result[i].MedicineName + '</td><td>' + result[i].Price + '</td><td>' + result[i].Quantity + '</td><td>' + result[i].Total + '</td><td> <img src=delete.png onclick=deleterecords(' + result[i].Id + ')> </td></tr>');
                        tot += result[i].Total;
                        count += 1;
                    }
                    //document.getElementById("sumTotalLabel").innerHTML = tot;
                    $('#sumTotalLabel').val(tot);
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
                url: "PurchaseMedicine.aspx/ConvertToString",
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

            $("#<%# medicineTextBox.ClientID%>").focusout(function () {
                rate = $('#medicineTextBox').val();
                //alert(rate);
                GetRate(rate);
            });

            function GetRate(rate) {
                //alert(rate);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PurchaseMedicine.aspx/GetProductRate",
                    data: "{'purticular':'" + rate + "'}",
                    dataType: "json",
                    success: function (data) {
                        var result = data.d;
                        $('#priceTextBox').val(result);
                    },
                    error: function (data) {
                        // alert('problem here');
                        var r = data.responseText;
                        var errorMessage = r.Message;
                        alert(errorMessage);
                    }
                });
            }

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

            // $("#quantityTextBox").focusout(function () {
            //     var qty = $('#quantityTextBox').val();
            //     var rt = $('#priceTextBox').val();
            //    //alert(v);
            //    var total = qty * rt;
            //    //alert(total);
            //    $("#totalTextbox").val(total);
            //});

            //var side = $('.side-bar').height();
            //var con = $('.content').height();
            //if (side > con) {
            //    $('.content').css('height', side);
            //} else {
            //    $('.side-bar').css('height', con);
            //}

            var b = $('body').height();
            $('.side-bar').css('height', b);
        });


            function deleterecords(id) {
                insertupdatedata(id, '', 0, 0, 'DELETE');
            }
            function insertupdatedata(id, medicine, price, quantity, status) {
                if (prodid != 0 && opstatus == 'UPDATE')
                    id = prodid;

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PurchaseMedicine.aspx/crudoperations",
                    data: "{'id':'" + id + "','medicine':'" + medicine + "','price':'" + price + "','quantity':'" + quantity + "','status':'" + status + "'}",
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
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">POS <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li class="active"><a href="../UI/PurchaseMedicine.aspx">Purchase Medicine</a></li>
                                            <li><a href="../UI/SalesMedicine.aspx">Sale Medicine</a></li>
                                        </ul>
                                    </li>
                                    <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Management <span class="caret"></span></a>
                                        <ul class="dropdown-menu">
                                            <li><a href="../UI/SaveMedicine.aspx">Save Medicine</a></li>
                                            <li><a href="../UI/MedicineSearch.aspx">Search Medicine</a></li>
                                            <li><a href="../UI/MedicineStorage.aspx">Medicine Storage</a></li>
                                        </ul>
                                    </li>
                                    <li><a href="../UI/DuePaymentUI.aspx">Due Payment</a></li>
                                </ul>
                                <ul class="nav navbar-nav navbar-right">
                                    <li><a href="../UI/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
                                </ul>
                            </div>
                        </nav>

                        <!--test Navbar end-->
                        <%--content goes here--%>


                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="form-horizontal">
                            <div class="col-md-9 col-sm-9 col-xs-9 col-sm-offset-2 col-md-offset-2 col-xs-offset-2 padding-top-30">
                                <div class="row">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <p class="panel-title">Purchase Medicine</p>
                                        </div>
                                        <div class="panel-body">
                                            <asp:Panel ID="successPanel" CssClass="alert alert-success" Visible="False" runat="server">
                                                <h5>Saved successfully!</h5>
                                            </asp:Panel>
                                            <asp:Panel ID="faildPanel" CssClass="alert alert-danger" Visible="False" runat="server">
                                                <h5>
                                                    <asp:Label ID="faildLabel" runat="server"></asp:Label></h5>
                                            </asp:Panel>
                                            <div class="col-md-12 col-sm-12 col-xs-12 ">
                                                Purchasing Date:
                                                <asp:TextBox ID="purchasingDateTextBox" CssClass="input-sm" runat="server"></asp:TextBox><br />
                                            </div>
                                            <br />
                                            <br />
                                            <div class="col-xs-10 col-sm-10 col-md-10 col-xs-offset-1">
                                                <table width="100%">
                                                    <tr>
                                                        <th>Medicine</th>
                                                        <th>Price</th>
                                                        <th>Quantity</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="form-group">
                                                                <div class="col-md-10 col-xs-10">
                                                                    <asp:TextBox ID="medicineTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ServiceMethod="SearchMedicine"
                                                                        MinimumPrefixLength="1"
                                                                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                        TargetControlID="medicineTextBox"
                                                                        ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
                                                                    </cc1:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="form-group">
                                                                <div class="col-md-10 col-xs-10">
                                                                    <asp:TextBox ID="priceTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="form-group">
                                                                <div class="col-md-10 col-xs-10">
                                                                    <asp:TextBox ID="quantityTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="form-group">
                                                    <div class="col-md-8 col-xs-8">
                                                        <input type="submit" id="btnSubmit" class="btn btn-success btn-block" value="Add" onclick="insertupdatedata('0', $('#medicineTextBox').val(), $('#priceTextBox').val(), $('#quantityTextBox').val(), 'INSERT')" />
                                                    </div>
                                                    <div class="col-md-3 col-xs-3">
                                                        <asp:Button ID="saveMedicineBtn" PostBackUrl="~/UI/SaveMedicine.aspx" CssClass="btn btn-primary btn-block" runat="server" Text="Save" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:GridView CssClass="table table-hover table-responsive" runat="server" ID="gvDetails">
                                            </asp:GridView>

                                            <div class="col-md-6 col-sm-6 col-xs-6" style="background: #e0ffff">
                                                <input id="wordToText" type="text" readonly="readonly" style="border: none; background: none; width: 100%" />
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-6" style="background: #f5f5dc">
                                                <table class="table table-responsive table-condensed">
                                                    <tr>
                                                        <th>Total: </th>
                                                        <td>
                                                            <input runat="server" type="text" id="sumTotalLabel" readonly="readonly" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Discount: </td>
                                                        <td><input type="text" runat="server" id="txtDiscount"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Grand Total: </td>
                                                        <td><input type="text" runat="server" id="txtGrandTotal" readonly="readonly"/></td>
                                                    </tr>
                                                     
                                                </table>
                                            </div>
                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                <asp:Button ID="savePurchaseButton" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="savePurchaseButton_Click" />
                                                <asp:Button runat="server" ID="printReport" CssClass="btn btn-info" Visible="False" Text="Print Voucher" />
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

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
    <script>
        $(function () {
            //$("#datepicker").datepicker();
            $('#purchasingDateTextBox').datepicker({
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

                    $('#purchasingDateTextBox').val(datetext);
                }
            });
            $('#txtExpenseType').autocomplete({
                source: ["Indoor", "Outdoor", "Pharmacy", "Others"]
            });

            $('#txtDiscount').focusout(function() {
                var total = $('#sumTotalLabel').val();
                var discount = $('#txtDiscount').val();
                var gTotal = total - discount;
                $('#txtGrandTotal').val(gTotal);
            });
        });
    </script>
</body>
</html>

