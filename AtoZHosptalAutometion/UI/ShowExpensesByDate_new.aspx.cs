using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class ShowExpensesByDate_new : System.Web.UI.Page
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
            DataTable dt2 = new DataTable();
            DataSet dSet = new DataSet();

            try
            {
                DataSet ds = oExpenseBll.ShowExpense(fromDate, tomDate);
                Session["rpt"] = ds;
                GridView.DataSource = ds;
                GridView.DataBind();
                printExpenseButton.Visible = true;
                printExpenseButton.PostBackUrl = "~/UI/ReportForm/ShowExpenseViewer.aspx";


                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                string query = "select sum(Amount) Amount from Expenses  where  ExpenseDate between @startDate and @endDate";

                using (SqlConnection con = new SqlConnection(cs))
                {
                    //It will be collected from session
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@startDate", fromDate);
                    cmd.Parameters.AddWithValue("@endDate", tomDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            totalsLabel.Text = reader["Amount"].ToString();
                        }

                    }

                    cmd.Dispose();
                    con.Close();
                }
            }
            catch (Exception exception)
            {
                Response.Write("<script>alert('"+exception+"');</script>");
            }
        }

        
    }
}