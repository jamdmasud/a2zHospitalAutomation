<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DuePaymentUI.aspx.cs" Inherits="AtoZHosptalAutometion.UI.DuePaymentUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceHolder" runat="server">
    Due Payment
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyPlaceHolder" runat="server">
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
                            <asp:Button ID="showResultButton" CssClass="btn btn-success" runat="server" Text="Show Expense" OnClick="showResultButton_Click"  />
                        </div>
                    </div>

                    <br />
                    <div class="GridviewDiv">
                        <asp:GridView CssClass="table table-hover table-responsive" runat="server" ID="GridView" AutoGenerateColumns="False">
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
  
</asp:Content>
