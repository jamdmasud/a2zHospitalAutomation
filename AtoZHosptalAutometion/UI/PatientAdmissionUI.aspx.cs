using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class PatientAdmissionUI : Page
    {
        private int UserId { set; get; }
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
                    cmd.CommandText = "select Name, Code, Specialist from Doctors where " +
                    "Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"] + " -> " + sdr["Specialist"] + " : " + sdr["Code"]);
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
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
                    cmd.CommandText = "select Name, Code from Agent where " +
                    "Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"] + "-" + sdr["Code"]);
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
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
                    cmd.CommandText = "select Name, Code from Patient where " +
                    "Code like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> codeList = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            codeList.Add(sdr["Code"].ToString());
                        }
                    }
                    conn.Close();
                    return codeList;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                PatientSub oPatientSub = new PatientSub();
                PatientBLL oPatientBll = new PatientBLL();
                oPatientSub.UpdatedBy = UserId;
                oPatientSub.PatientId = oPatientBll.GetCustomerIdByCode(codeTextBox.Text);
                oPatientSub.GuadianName = guardianTextBox.Text;
                oPatientSub.GuardianMobile = guardianMobileTextBox.Text;
                oPatientSub.AddmissionDate = admissionDateTextBox.Value == "" ? DateTime.Now : Convert.ToDateTime(admissionDateTextBox.Value);
                oPatientSub.AgentId =
                    oPatientBll.GetAgentIdFromCode(agentTextBox2.Text.Substring((agentTextBox2.Text.Length - 7), 7));
                oPatientSub.DoctorId =
                    oPatientBll.GetDoctorIdFromCode(doctorNameTextBox.Text.Substring((doctorNameTextBox.Text.Length - 11),
                        11));
                int invoiceId = oPatientBll.Admit(oPatientSub);
                if (invoiceId > 1000)
                {
                    Response.Write("<script>alert('your invoice ID is" + invoiceId + "');</script>");
                    ClearField();
                }
                else
                {
                    Response.Write("<script>alert('Admission with error!');</script>");
                }
            }
            catch (Exception EX_NAME)
            {
                faildPanel.Visible = true;
                faildLabel.Text = EX_NAME.InnerException.Message;
            }
        }

        private void ClearField()
        {
            codeTextBox.Text = "";
            guardianTextBox.Text = "";
            guardianMobileTextBox.Text = "";
            admissionDateTextBox.Value = "";
            agentTextBox2.Text = "";
            doctorNameTextBox.Text = "";
        }
    }
}