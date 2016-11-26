<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndoorServiceViewer.aspx.cs" Inherits="AtoZHosptalAutometion.UI.ReportForm.IndoorServiceViewer" %>

<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<!DOCTYPE html>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Indoor Service</title>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../scripts/bootstrap.min.js"></script>
    <script src="../../scripts/jquery-3.1.0.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-10 col-md-offset-1" style="border: 1px solid gray">
            <!--test Navbar start-->
            <nav class="navbar navbar-inverse" style="padding-right: 10px">
                <div class="container-fluid">
                    <ul class="nav navbar-nav">
                        <li><a href="/default.aspx">Home</a></li>
                        <li><a href="#"  onclick="Print()" >Print</a></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="/UI/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
                    </ul>
                </div>
            </nav>

            <!--test Navbar end-->
            <div id="dvReport">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
            </div>
            <br />
            <input type="button" id="btnPrint" class="btn btn-info" value="Print" onclick="Print()" />
        </div>
    </form>
    <script type="text/javascript">
        function Print() {
            var dvReport = document.getElementById("dvReport");
            var frame1 = dvReport.getElementsByTagName("iframe")[0];
            if (navigator.appName.indexOf("Internet Explorer") != -1) {
                frame1.name = frame1.id;
                window.frames[frame1.id].focus();
                window.frames[frame1.id].print();
            }
            else {
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.print();
            }
        }
    </script>
</body>
</html>
