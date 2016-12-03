using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;
using Microsoft.ApplicationInsights.Web;

namespace AtoZHosptalAutometion.UI
{
    public partial class SalesMedicine : Page
    {

        public static int UserId { set; get; }
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Login security
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
            ClearField();

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
        public static List<string> SearchCustomerByCode(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Code, Name from Patient where " +
                    "Code like @SearchText or Name like @SearchText";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText + "%");
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"] + " >> " + sdr["Code"]);
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }

        [WebMethod]
        public static int InsertSale(string medicine, double price, int qty)
        {
            Entities db = new Entities();

            int medicineId = db.Medicines.Where(m => m.Name == medicine).Select(m => m.Id).FirstOrDefault();
            string constr = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO SalesTemp (MedicineId, Quantity, Price) VALUES(@MedicineId, @Quantity, @Price) SELECT SCOPE_IDENTITY()"))
                {
                    cmd.Parameters.AddWithValue("@MedicineId", medicineId);
                    cmd.Parameters.AddWithValue("@Quantity", qty);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Connection = con;
                    con.Open();
                    int customerId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return customerId;
                }
            }
        }

        [WebMethod]
        public static SalesTemp[] BindGridview()
        {
            using (var db = new Entities())
            {
                List<SalesTemp> list = db.SalesTemps.Where(m=>m.CreatedBy == UserId).ToList();
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
                SqlCommand cmd = new SqlCommand("SPSaleMedicineTamp", con);
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

        [WebMethod]
        public static string ConvertToString(int data)
        {
            Functions oFunctions = new Functions();
            string word = oFunctions.NumberToWord(data);
            return word;
        }



        private void ClearField()
        {
            medicineTextBox.Text = String.Empty;
            priceTextBox.Text = String.Empty;
            quantityTextBox.Text = String.Empty;
        }


        protected void saveSalesButton_Click1(object sender, EventArgs e)
        {
           
           
            try
            {
                MedicineBLL oMedicineBll = new MedicineBLL();
                Functions oFunctions = new Functions();
                if (sallingDateTextBox.Text != "")
                {
                    SaleSave oSaleSave = new SaleSave();
                    oSaleSave.UserId = UserId;
                    oSaleSave.SaleDate = Convert.ToDateTime(sallingDateTextBox.Text);
                    oSaleSave.Customer = customerIdTextbox.Text;
                    oSaleSave.CustomerType = patientTypeDropDownList.Text;

                    oSaleSave.Total = Convert.ToDecimal(sumTotalLabel.Value);
                    oSaleSave.GrandTotal = txtGrandTotal.Value == "0"
                        ? oSaleSave.Total
                        : Convert.ToDecimal(txtGrandTotal.Value);
                    oSaleSave.AmountInword = oFunctions.NumberToWord(Convert.ToInt32(oSaleSave.GrandTotal));
                    oSaleSave.Discount = Convert.ToDecimal(txtDiscount.Value);
                    oSaleSave.Advanced = Convert.ToDecimal(txtAdvance.Value);
                    oSaleSave.Due = Convert.ToDecimal(txtDue.Value);

                    int affect = oMedicineBll.SaveSallingMedicine(oSaleSave);
                    if (affect > 0)
                    {
                        //show print button
                        printReportButton.Visible = true;
                        printReportButton.Text = "Print Report";
                        DataSet ds = oMedicineBll.SaleMedicineDataSet(affect);
                        Session["rpt"] = ds;
                        printReportButton.PostBackUrl = "~/UI/SaleMedicineReportView.aspx";

                        ClearFields();
                    }
                    else
                    {
                        //submission failed
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please fill up avobe boxes correctly!');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.InnerException.Message);
            }
        }

        private void ClearFields()
        {
            customerIdTextbox.Text = "";
            sumTotalLabel.Value = "0";
            txtGrandTotal.Value = "0";
            txtVat.Value = "0";
            txtAdvance.Value = "0";
            txtDiscount.Value = "0";
            txtDue.Value = "0";
        }
    }

    public class SaleSave
    {
        public int UserId { get; set; } 
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public string CustomerType { get; set; }
        public string AmountInword { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Advanced { get; set; }
        public decimal Due { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }

    }
}