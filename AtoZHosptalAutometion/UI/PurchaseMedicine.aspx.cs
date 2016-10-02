using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class PurchaseMedicine : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        public static int UserId { set; get; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //login
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            if (oUser.Roles != "Admin" && oUser.Roles != "Pharmacy")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }

            UserId = oUser.Id;
            if (!IsPostBack)
            {
                BindColumnToGridview();
            }

        }

        private void BindColumnToGridview()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SL");
            dt.Columns.Add("Medicine");
            dt.Columns.Add("Rate");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Total");
            dt.Columns.Add("Management");
            dt.Rows.Add();
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            gvDetails.Rows[0].Visible = false;
        }

        [WebMethod]
        public static MedicinePurchaseTemp[] BindGridview()
        {
            using (var db = new Entities())
            {
                List<MedicinePurchaseTemp> list = db.MedicinePurchaseTemps.Where(m => m.CreatedBy == UserId).ToList();
                return list.ToArray();
            }
        }


        [WebMethod]
        public static string crudoperations(string status, string medicine, decimal price, int quantity, int id)
        {

            //DateTime expDateTime = expenseDate == null ? DateTime.Today : expenseDate == "" ? DateTime.Today : Convert.ToDateTime(expenseDate);
            string msg = "false";
            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //It will be collected from session
                con.Open();
                SqlCommand cmd = new SqlCommand("SPPurchaseMedicineTamp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (status == "INSERT")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@MedicineName", medicine);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Total", (price * quantity));
                    cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                }
                else if (status == "DELETE")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                }


                cmd.ExecuteNonQuery();
                msg = "true";
            }

            return msg;
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchMedicine(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name from Medicine where " +
                    "Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"].ToString());
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }


        [ScriptMethod()]
        [WebMethod]
        public static decimal GetProductRate(string purticular)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Price from Medicine where Name like  @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", purticular);
                    cmd.Connection = conn;
                    decimal r = 0.0M;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            r = Convert.ToDecimal(sdr["Price"]);
                        }
                    }
                    conn.Close();
                    return r;
                }
            }
        }

        protected void savePurchaseButton_Click(object sender, EventArgs e)
        {
            MedicineBLL oMedicineBll = new MedicineBLL();
            Functions oFunctions = new Functions();

            string purchasingDate = purchasingDateTextBox.Text;
            string totals = sumTotalLabel.Value;
            int total = Convert.ToInt32(Convert.ToDecimal(totals));
            string word = oFunctions.NumberToWord((int)total);
            if (purchasingDate == "")
            {
                Response.Write("<script>alert('Select purchasing date correctly!');</script>");
            }
            else
            {
                int inoviceId = oMedicineBll.SavePurchaseMedicine(purchasingDate, total, word, UserId);
                if (inoviceId > 60000)
                {
                    DataSet ds = oMedicineBll.PurchaseMedicineDataSet(inoviceId);
                    Session["rpt"] = ds;
                    printReport.Visible = true;
                    printReport.PostBackUrl = "/UI/ReportForm/PurchaseReportViewer.aspx";
                    Response.Write("<script>alert('Medicine has been stored successfully!');</script>");
                    ClearField();
                }
            }

        }

        private void ClearField()
        {
            medicineTextBox.Text = String.Empty;
            priceTextBox.Text = "";
            quantityTextBox.Text = "";
            purchasingDateTextBox.Text = "";
        }

        [WebMethod]
        public static string ConvertToString(int data)
        {
            Functions oFunctions = new Functions();
            string word = oFunctions.NumberToWord(data);
            return word;
        }


    }
}