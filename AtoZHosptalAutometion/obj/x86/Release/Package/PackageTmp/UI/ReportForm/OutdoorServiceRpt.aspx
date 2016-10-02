<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutdoorServiceRpt.aspx.cs" Inherits="AtoZHosptalAutometion.UI.ReportForm.OutdoorServiceRpt" %>

<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../scripts/bootstrap.min.js"></script>
    <script src="../../scripts/jquery-3.1.0.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="col-md-10 col-md-offset-1" style="border: 1px solid gray">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </div>
    </form>
</body>
</html>
