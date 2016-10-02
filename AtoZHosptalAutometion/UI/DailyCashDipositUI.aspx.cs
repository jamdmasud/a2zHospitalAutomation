using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class DailyCashDipositUI : System.Web.UI.Page
    {
        private  int userId { set; get; }
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            //login
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            userId = oUser.Id;

            //Identify user type
            if (oUser.Roles != "Admin" && oUser.Roles != "Manager")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }

            // dateTextBox.Value = DateTime.Today.ToShortDateString();
        }

        protected void getButton_Click(object sender, EventArgs e)
        {
            AccountBll oAccountBll = new AccountBll();
            try
            {
                string user = oAccountBll.GetUserNameByUserId(Convert.ToInt32(userIdTextBox.Text));
                string type = serviceDropDownList.Text;
                DateTime date = Convert.ToDateTime(dateTextBox.Value);

                string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                if (type == "Pharmacy")
                {

                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        //It will be collected from session
                        con.Open();
                        SqlCommand cmd = new SqlCommand("spTodaySale", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@today", date);
                        cmd.Parameters.AddWithValue("@user", user);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        medicineGridView.DataSource = dt;
                        medicineGridView.DataBind();
                        cmd.Dispose();
                        con.Close();
                    }
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        //It will be collected from session
                        con.Open();
                        SqlCommand cmd = new SqlCommand("spTodaySaleService", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@today", date);
                        cmd.Parameters.AddWithValue("@user", user);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        medicineGridView.DataSource = dt;
                        medicineGridView.DataBind();
                        cmd.Dispose();
                        con.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                Response.Write("<script>alert('"+exception.Message+"');</script>");
            }
        }

        protected void postButton_Click(object sender, EventArgs e)
        {
            try
            {
                AccountBll oAccountBll = new AccountBll();
                Functions oFunctions = new Functions();
                DataSet ds = new DataSet();
                Voucher oVoucher = new Voucher();
                oVoucher.Diposit = Convert.ToDecimal(amountTextBox.Text);
                oVoucher.EmployeeName = oAccountBll.GetUserNameByUserId(Convert.ToInt32(userIdTextBox.Text));
                oVoucher.UpdatedBy = userId;
                oVoucher.DateOfDeal = Convert.ToDateTime(dealingDateTextBox.Text);
                oVoucher.UpdatedDate = DateTime.Today;
                oVoucher.Particulars = serviceDropDownList.Text;
                int Id = oFunctions.SaveDiposit(oVoucher);
                if(Id > 0)
                {
                    //show result and print confirmation receipt 
                    ds = oFunctions.GetDipositData(Id);
                    Session["rpt"] = ds;
                    Response.Write("<script>alert('Diposit submited successfully!');</script>");
                    voucheButton.Visible = true;
                    voucheButton.PostBackUrl = "~/UI/ReportForm/DailyDipositReportViewer.aspx";
                }
                else
                {
                    //show cause of failurity
                    Response.Write("<script>alert('Diposit submition faild!');</script>");
                }
            }
            catch (Exception exception)
            {
                Response.Write("<script>alert('"+exception.Message+"');</script>");
            }
        }
    }
}