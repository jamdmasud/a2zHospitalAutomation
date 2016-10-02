using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using CrystalDecisions.CrystalReports.Engine;

namespace AtoZHosptalAutometion.UI
{
    public partial class SaleMedicineReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                DataSet id = (DataSet)Session["rpt"];
                ReportDocument rptDocument = new ReportDocument();
                rptDocument.Load(Server.MapPath("~/Report/rptSalesMedicine.rpt"));
                rptDocument.SetDataSource(id);
                CrystalReportViewer1.ReportSource = rptDocument;
          
            
        }
    }
}