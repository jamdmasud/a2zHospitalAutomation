<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddExpenseUIold.aspx.cs" Inherits="AtoZHosptalAutometion.UI.AddExpenseUI" %>

    <script src="../scripts/jquery-3.1.0.js"></script>
   
    <script type="text/javascript">
        var prodid = 0, opstatus = '';
        $(function () {
            BindGridview();
        });
        function BindGridview() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AddExpenseUI.aspx/BindGridview",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var result = data.d;
                    
                    for (var i = 0; i < result.length; i++) {
                        console.log(result[i].productname+" "+result[i].price);
                        $("#gvDetails").append('<tr><td>' + result[i].productid + '</td><td>' + result[i].productname + '</td><td>' + result[i].price + '</td><td> <img src=delete.png onclick=deleterecords(' + result[i].productid + ')> </td></tr>');
                    }
                },
                error: function (data) {
                    var r = data.responseText;
                    var errorMessage = r.Message;
                    alert(errorMessage);
                }
            });
        }
        function deleterecords(productid) {
            insertupdatedata(productid, '', '', 'DELETE');
        }
        function insertupdatedata(productid, productname, price, status) {
            if (prodid != 0 && opstatus == 'UPDATE')
                productid = prodid;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AddExpenseUI.aspx/crudoperations",
                data: "{'productid':'" + productid + "','productname':'" + productname + "','price':'" + price + "','status':'" + status + "'}",
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


    
   
    <table>
        <tr>
            <td>
                Product Name:
            </td>
            <td>
                <input type="text" id="txtProduct" />
            </td>
        </tr>
        <tr>
            <td>
                Price:
            </td>
            <td>
                <input type="text" id="txtPrice" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="button" id="btnInsert" value="Insert" onclick="insertupdatedata('0',$('#txtProduct').val(),$('#txtPrice').val(),'INSERT')" />
            </td>
        </tr>
    </table>
    <br />
    <div class="GridviewDiv">
        <asp:GridView  runat="server" ID="gvDetails">
            <HeaderStyle CssClass="headerstyle" />
        </asp:GridView>
    </div>

</asp:Content>
