using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace AtoZHosptalAutometion.UI.ReportForm
{
    public partial class OutdoorServiceRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet id = (DataSet)Session["rpt"];
            ReportDocument rptDocument = new ReportDocument();
            rptDocument.Load(Server.MapPath("~/Report/rptSaleOutdoorService.rpt"));
            rptDocument.SetDataSource(id);
            CrystalReportViewer1.ReportSource = rptDocument;
        }
    }
}