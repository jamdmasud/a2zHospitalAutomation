using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class DipositListUI : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Login security
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            if (oUser.Roles != "Admin" && oUser.Roles != "Manager")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }
        }

        protected void showExpenseButton_Click(object sender, EventArgs e)
        {
            
            DateTime fromDate = Convert.ToDateTime(txtFromDate.Value);
            DateTime tomDate = Convert.ToDateTime(txtTodate.Value);
            ExpenseBLL oExpenseBll = new ExpenseBLL();

            try
            {
                DataSet ds = oExpenseBll.ShowDeposit(fromDate, tomDate);
                decimal total = ds.Tables[0].Rows.Cast<DataRow>().Sum(item => Convert.ToDecimal(item[4]));
                lblTotal.Text = total.ToString();
                lblTotal.ForeColor = Color.DarkRed;
                lblTotal.Visible = true;
                Session["rpt"] = ds;
                GridView.DataSource = ds;
                GridView.DataBind();
                printExpenseButton.Visible = true;
                printExpenseButton.PostBackUrl = "~/UI/ReportForm/DepositViewer.aspx";
            }
            catch (Exception exception)
            {
                Response.Write("<script>alert('" + exception + "');</script>");
            }
        }
    }
}