using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class AddExpense : System.Web.UI.Page
    {
        public static int UserId { set; get; }
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
            //identify
            UserId = oUser.Id;
            if (!IsPostBack)
            {
                BindColumnToGridview();
            }
        }

        private void BindColumnToGridview()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Description");
            dt.Columns.Add("Amount");
            dt.Columns.Add("ExpenseType");
            dt.Columns.Add("Edit");
            dt.Rows.Add();
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            gvDetails.Rows[0].Visible = false;
        }
        [WebMethod]
        public static ProductDetails[] BindGridview()
        {
            DataTable dt = new DataTable();
            List<ProductDetails> details = new List<ProductDetails>();

            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SPExpenseTamp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", 0);
                cmd.Parameters.AddWithValue("@Description", "");
                cmd.Parameters.AddWithValue("@Amount", 0);
                cmd.Parameters.AddWithValue("@ExpenseType", "");
                cmd.Parameters.AddWithValue("@ExpenseDate", "");
                cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                cmd.Parameters.AddWithValue("@status", "SELECT");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                foreach (DataRow dtrow in dt.Rows)
                {
                    ProductDetails product = new ProductDetails();
                    product.Id = Convert.ToInt32(dtrow["Id"]);
                    product.Description = dtrow["Description"].ToString();
                    product.Amount = Convert.ToInt32(dtrow["Amount"]);
                    product.ExpenseType = dtrow["ExpenseType"].ToString();
                    details.Add(product);
                }
            }
            return details.ToArray();
        }
        public class ProductDetails
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public string ExpenseType { get; set; }
        }
        [WebMethod]
        public static string crudoperations(string status, string description, string amount, string expenseType, string expenseDate, int id)
        {
            string msg = "false";
            try
            {
                DateTime expDateTime = expenseDate == null ? DateTime.Today : expenseDate == "" ? DateTime.Today : Convert.ToDateTime(expenseDate);
            
                string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    //It will be collected from session
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SPExpenseTamp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (status == "INSERT")
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@ExpenseType", expenseType);
                        cmd.Parameters.AddWithValue("@ExpenseDate", expDateTime);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                    }
                    else if (status == "DELETE")
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@ExpenseType", expenseType);
                        cmd.Parameters.AddWithValue("@ExpenseDate", expDateTime);
                        cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                    }
                    cmd.ExecuteNonQuery();
                    msg = "true";
                }
            }
            catch (Exception EX_NAME)
            {
                Console.WriteLine(EX_NAME.Message);
            }
            return msg;
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            //user id crack should be from session
            //date of submission / use datepicker to insert date
            try
            {
                ExpenseBLL oExpenseBll = new ExpenseBLL();
                    int affected = oExpenseBll.SaveExapense(UserId);
                if (affected > 0)
                {
                    Response.Write("<script>alert('Submission completed succssfully');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Submission faild!');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExpenseBLL oExpenseBll = new ExpenseBLL();
            //if(!IsPostBack)
            //oExpenseBll.RemoveExpensesTamp();
        }
    }
}