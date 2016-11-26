using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class SaveMedicine : System.Web.UI.Page
    {
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
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Medicine medicine = new Medicine();
            MedicineBLL oMedicineBll = new MedicineBLL();
            MedicineDetails oMedicineDetails = new MedicineDetails();
            try
            {

                if (medicineNameTextBox.Text != "" && GroupNameTextBox.Text != "" && companyTextBox2.Text != "" && quantityTextBox.Text != "" && priceTextBox3.Text != "")
                {
                    medicine.UpdatedBy = oUser.Id; // user id must from session
                    oMedicineDetails.UpdatedBy = medicine.UpdatedBy;

                    medicine.Name = medicineNameTextBox.Text;
                    oMedicineDetails.GroupName = GroupNameTextBox.Text;
                    medicine.GroupId = oMedicineBll.GetGroupId(oMedicineDetails.GroupName);
                    oMedicineDetails.CompanyName = companyTextBox2.Text;
                    medicine.CompanyId = oMedicineBll.GetCompanyId(oMedicineDetails.CompanyName);
                    medicine.Quantity = Convert.ToInt32(quantityTextBox.Text);
                    medicine.Price = Convert.ToDecimal(priceTextBox3.Text);

                    if (oMedicineBll.Save(medicine, oMedicineDetails))
                    {
                        faildPanel.Visible = false;
                        successPanel.Visible = true;
                        ClearField();
                    }
                    else
                    {
                        successPanel.Visible = false;
                        faildPanel.Visible = true;
                        faildLabel.Text = "Medicine save faild";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Fill up all field correctly!');</script>");
                }
            }
            catch (Exception EX_NAME)
            {
                successPanel.Visible = false;
                faildPanel.Visible = true;
                faildLabel.Text = EX_NAME.Message;
            }


        }

        private void ClearField()
        {
            medicineNameTextBox.Text = String.Empty;
            GroupNameTextBox.Text = String.Empty;
            companyTextBox2.Text = String.Empty;
            priceTextBox3.Text = String.Empty;
            quantityTextBox.Text = String.Empty;
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
        public static List<string> SearchGroups(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name from Groups where " +
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
        public static List<string> SearchCompanies(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name from Company where " +
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
    }
}