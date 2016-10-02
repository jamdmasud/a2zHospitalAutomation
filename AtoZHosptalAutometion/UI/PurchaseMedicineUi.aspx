<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseMedicineUi.aspx.cs" Inherits="AtoZHosptalAutometion.UI.PurchaseMedicineUi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceHolder" runat="server">
    Purchase Medicine
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="server">
    <%--<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>--%>
    <script>
        $(function () {
            //$("#datepicker").datepicker();
            $('#<%= purchasingDateTextBox.ClientID %>').datepicker();
            BindGridview();
        });

        function BindGridview() {
            
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PurchaseMedicineUi.aspx/BindGridview",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    var tot = 0;
                    //alert(result[0].Id+ ' '+ result[0].Amount + ' ' + result[0].Description + ' ' + result[0].ExpenseType);
                    alert(result.length);
                    //for (var i = 0; i < result.length; i++) {
                    //    $("#gvDetails").append('<tr><td>' + result[i].Description + '</td><td>' + result[i].Amount + '</td><td>' + result[i].ExpenseType + '</td><td> <img src=delete.png onclick=deleterecords(' + result[i].Id + ')> </td></tr>');
                    //    tot += result[i].Amount;
                    //}
                    document.getElementById("sumTotal").innerHTML = "Total: " + tot;
                },
                error: function (data) {
                   
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                    console.log(errorMessage);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyPlaceHolder" runat="server">

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
                                    Purchasing Date: <asp:TextBox ID="purchasingDateTextBox"  CssClass="input-sm" runat="server"></asp:TextBox><br/>
                        </div>
                        <br/>
                        <br/>
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
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success btn-block" Text="Add" OnClick="btnSubmit_Click" />
                                </div>
                                <div class="col-md-3 col-xs-3">
                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-default btn-block" Text="Reset" OnClick="btnReset_Click1" />
                                </div>
                            </div>
                        </div>

                         <asp:GridView CssClass="table table-hover table-responsive" runat="server" ID="gvDetails">
                                </asp:GridView>
                        <div class="col-md-4 col-xs-4 col-md-offset-10 col-xs-10">
                            <asp:Label ID="sumTotalLabel" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:Button ID="savePurchaseButton" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="save_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
