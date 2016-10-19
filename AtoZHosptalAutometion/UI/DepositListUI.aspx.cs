using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class DepositListUI : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //login
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];

            //Identify user type
            if (oUser.Roles != "Admin" && oUser.Roles != "Manager")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }
        }
        protected void submitButton_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DateTime tosDate = Convert.ToDateTime(toDate.Value);
            DateTime fromsDate = Convert.ToDateTime(fromDate.Value);
            using (SqlConnection con = new SqlConnection(cs))
            {
                //It will be collected from session
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Voucher where DateOfDeal  between @fromDate and @toDate", con);

                cmd.Parameters.AddWithValue("@fromDate", fromsDate);
                cmd.Parameters.AddWithValue("@toDate", tosDate);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dt);
                dt.TableName = "Command";
                ds.Tables.Add(dt.Copy());

                medicineGridView.DataSource = ds;
                medicineGridView.DataBind();
                Session["rpt"] = ds;
                printButton.Visible = true;
                printButton.PostBackUrl = "~/UI/ReportForm/DepositViewer.aspx";
                cmd.Dispose();
                con.Close();
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                //It will be collected from session
                con.Open();
                SqlCommand cmd = new SqlCommand("select SUM(Diposit) as Total from Voucher where DateOfDeal  between @fromDate and @toDate", con);

                cmd.Parameters.AddWithValue("@fromDate", fromsDate);
                cmd.Parameters.AddWithValue("@toDate", tosDate);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        totalsLabel.Text = reader["Total"].ToString();
                    }

                }

                cmd.Dispose();
                con.Close();
            }
        }
    }
}