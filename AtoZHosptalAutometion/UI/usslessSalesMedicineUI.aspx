<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="usslessSalesMedicineUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.SalesMedicineUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceHolder" runat="server">
    Sales Medicine
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="server">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
    <script>
        $(function () {
            //$("#purchasingDateTextBox").datepicker();
            $('#<%= sallingDateTextBox.ClientID %>').datepicker();

        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyPlaceHolder" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="form-horizontal">
        <div class="col-md-9 col-sm-9 col-xs-9 col-sm-offset-2 col-md-offset-2 col-xs-offset-2 padding-top-30">
            <div class="row">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <p class="panel-title">Sales Medicine</p>
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

                            <table width="100%">
                                <tr>
                                    <th>Sales Date</th>
                                    <th>Customer Id</th>
                                    <th>Customer Type</th>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="form-group">
                                            <div class="col-md-10 col-xs-10">
                                                <asp:TextBox ID="sallingDateTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <div class="col-md-10 col-xs-10">
                                                <asp:TextBox ID="customerIdTextbox" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ServiceMethod="SearchCustomerByCode"
                                                    MinimumPrefixLength="1"
                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                    TargetControlID="customerIdTextbox"
                                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <div class="col-md-10 col-xs-10">
                                                <asp:DropDownList ID="patientTypeDropDownList" CssClass="form-control" runat="server">
                                                    <asp:ListItem Selected="True" Value="0">Selecte patient type</asp:ListItem>
                                                    <asp:ListItem Value="Indoor">Indoor</asp:ListItem>
                                                    <asp:ListItem Value="Outdoor">Out-door</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>

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
                                                <asp:TextBox type="text" ID="priceTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="form-group">
                                            <div class="col-md-10 col-xs-10">
                                                <asp:TextBox type="text" ID="quantityTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-8">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-block" Text="Add" OnClick="btnSubmit_Click1" />
                                </div>
                                <div class="col-md-3 col-xs-3">
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default btn-block" Text="Reset" />
                                </div>
                            </div>
                        </div>

                       
                        <asp:GridView runat="server" ID="gvDetails" CssClass="table table-hover table-responsive"></asp:GridView>

                        <div class="col-md-4 col-xs-4 col-md-offset-10 col-xs-10">
                            <asp:Label ID="sumTotalLabel" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Button ID="saveSalesButton" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="saveSalesButton_Click" />
                            <asp:Button ID="printReportButton" Visible="False" runat="server" Text="Print" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--
    <script>
        $(document).ready(function () {
            $("#btnSubmit").click(function () {

                var medicineName = $('#<%= medicineTextBox.ClientID %>').val();
                var price = $("#priceTextBox").val();
                var quantity = $("#quantityTextBox").val();
                alert(medicineName);
                $.ajax({
                    type: "POST",
                    url: "SalesMedicineUI.aspx/InsertSale",
                    data: '{medicine: "' + medicineName + '", price: "' + price + '" , qty: "' + quantity + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response.d);
                        alert(response.d);
                        $('#<%= medicineTextBox.ClientID %>').val("");
                        $("#priceTextBox").val("");
                        $("#quantityTextBox").val("");
                    }
                });
                return false;
            });

            function fill(parameters) {
                
            }
        });
    </script>--%>
</asp:Content>
