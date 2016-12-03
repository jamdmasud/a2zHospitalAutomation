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
    public partial class IndoorSeriviceUI : System.Web.UI.Page
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
            if (oUser.Roles != "Admin" && oUser.Roles != "Reception")
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
            dt.Columns.Add("S.N");
            dt.Columns.Add("Code");
            dt.Columns.Add("Test Name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Rate");
            dt.Columns.Add("Item");
            dt.Columns.Add("Management");
            dt.Rows.Add();
            gvService.DataSource = dt;
            gvService.DataBind();
            gvService.Rows[0].Visible = false;
        }

        [WebMethod]
        public static ServiceDetails[] BindGridview()
        {
            DataTable dt = new DataTable();
            List<ServiceDetails> details = new List<ServiceDetails>();

            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spIndoorIncomeTamp", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                cmd.Parameters.AddWithValue("@Status", "SELECT");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                foreach (DataRow dtrow in dt.Rows)
                {
                    ServiceDetails oService = new ServiceDetails();
                    oService.Id = Convert.ToInt32(dtrow["Id"]);
                    oService.Code = dtrow["Code"].ToString();
                    oService.TestName = dtrow["Name"].ToString();
                    oService.Amount = Convert.ToInt32(dtrow["Amount"]);
                    oService.Quantity = Convert.ToInt32(dtrow["Quantity"]);
                    oService.Rate = Convert.ToDecimal(dtrow["Rate"]);
                    details.Add(oService);
                }
            }
            return details.ToArray();
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchPatient(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Code, Name from Patient where " +
                    "Code like @SearchText or Name like @SearchText";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText+"%");
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"] + " (" + sdr["Code"] + ")");
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }


        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchProduct(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "spGetServices";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@keyword", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["name"] + "(" + sdr["Code"] + ")");
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchDoctor(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name,Specialist,PermanetHospital from Doctors where " +
                    "Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"] + ". " + sdr["Specialist"] + "," + sdr["PermanetHospital"]);
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }


        [WebMethod]
        public static string crudoperations(string status, string particular, int quantity, decimal rate, decimal total, int id)
        {
            string code = "";
            string name = "";
            if (particular.Length > 12)
            {
                code = particular.Substring((particular.Length - 10), 9);
                name = particular.Substring(0, (particular.Length - 11));
            }

            string msg = "false";
            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //It will be collected from session
                con.Open();
                SqlCommand cmd = new SqlCommand("spIndoorIncomeTamp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (status == "INSERT")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@Particular", name);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Amount", total);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Rate", rate);
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
            string code = "";
            string tableSelect = "", table = "";
            if (purticular.Length > 12)
            {
                code = purticular.Substring((purticular.Length - 10), 9);
                tableSelect = code.Substring(0, 2);
            }

            if (tableSelect == "PT") table = "Test";
            else table = "Service";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Rate from " + table + " where Code like  @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", code);
                    cmd.Connection = conn;
                    decimal r = 0.0M;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            r = Convert.ToDecimal(sdr["Rate"]);
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

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchAgent(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name,Code from Agent where Code like @SearchText + '%' OR Phone like  @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Code"] + ":" + sdr["Name"]);
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceBLL oServiceBll = new ServiceBLL();
                ServiceDetails oDetails = new ServiceDetails();

                oDetails.PatientCode = patientTextBox.Text;
                oDetails.Doctor = doctorTextBox.Text;

                if (oDetails.PatientCode != "" && oDetails.Doctor != "")
                {

                    oDetails.AgentName = txtAget.Text;
                    oDetails.Honouriam = txtHonorarium.Value == "" ? 0 : Convert.ToDecimal(txtHonorarium.Value);
                    oDetails.Due = txtDue.Value == "" ? 0 : Convert.ToDecimal(txtDue.Value);
                    oDetails.Paid = txtAdvance.Value == "" ? 0 : Convert.ToDecimal(txtAdvance.Value);
                    oDetails.GrandTotal = txtGrandTotal.Value == "" ? 0 : Convert.ToDecimal(txtGrandTotal.Value);
                    oDetails.Vat = Convert.ToDecimal(txtVat.Value);
                    oDetails.Discount = txtDiscount.Value == "" ? 0 : Convert.ToDecimal(txtDiscount.Value);
                    oDetails.Amount = sumTotalLabel.Value == "" ? 0 : Convert.ToDecimal(sumTotalLabel.Value);
                    oDetails.UpdatedBy = UserId;
                    int invoiceId = oServiceBll.SaveIndoorServices(oDetails);

                    if (invoiceId > 0)
                    {

                        DataSet ds = oServiceBll.GetIndoorServiceData(invoiceId);
                        Session["rpt"] = ds;
                        printButton.Visible = true;
                        printButton.PostBackUrl = "~/UI/ReportForm/IndoorServiceViewer.aspx";
                        // show success massage
                        Response.Write("<script>alert('Bill submited successfully!');</script>");
                    }
                    else
                    {
                        //show error message
                        Response.Write("<script>alert('Bill submition failed!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please fill up all field correctly!');</script>");
                }
            }
            catch (Exception ex)
            {
                faildPanel.Visible = true;
                successPanel.Visible = false;
                faildLabel.Text = ex.InnerException.Message;
            }


        }
    }
}