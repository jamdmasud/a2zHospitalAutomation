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
    public partial class PatienEntry1 : System.Web.UI.Page
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Patient oPatient = new Patient();
            PatientBLL oPatientBll = new PatientBLL();
            try
            {
                oPatient.UpdatedBy = oUser.Id;//This code should be assigned from Session
                oPatient.Name = NameTextBox.Text;
                oPatient.fatherOhusbandName = fatherTextBox.Text;
                oPatient.MotherName = motherTextBox.Text;
                oPatient.PermenantAddress = permanentAddressTextBox.Text;
                oPatient.Sex = sexDropdownList.Value;
                oPatient.Dob = datepicker.Value == "" ? DateTime.Today : Convert.ToDateTime(datepicker.Value);
                oPatient.AddmissionDate = admissionDateTextBox.Value == "" ? DateTime.Today : Convert.ToDateTime(admissionDateTextBox.Value);
                oPatient.Phone = phoneTextBox.Text;
                oPatient.Email = emailTextBox.Text;
                oPatient.presentAddress = addressTextBox.Text;
                string doctorName = doctorNameTextBox.Text;//
                oPatient.RefencedBy = doctorName == "" ? 0 : oPatientBll.GetDoctorIdFromCode(doctorName.Substring(doctorName.Length - 11));  //if doctor does not exist reference value will be null
                string agentName = agentTextBox2.Text;
                oPatient.AgentsId = agentName == "" ? null : oPatientBll.GetAgentIdFromCode(agentName.Substring(agentName.Length - 7)); //Same condition applicable for this  like ReferencedBy
                if (oPatient.Name != "")
                {
                    if (oPatientBll.Register(oPatient))
                    {
                        successPanel.Visible = true;
                        faildPanel.Visible = false;
                        ClearField();
                    }
                }
                else
                {
                    successPanel.Visible = false;
                    faildPanel.Visible = true;
                    faildLabel.Text = "Name field should " +
                                      "not be empty!";
                }

            }
            catch (Exception exception)
            {
                successPanel.Visible = false;
                faildPanel.Visible = true;
                faildLabel.Text = exception.Message;
            }

        }

        private void ClearField()
        {
            NameTextBox.Text = String.Empty;
            sexDropdownList.Value = "0";
            phoneTextBox.Text = String.Empty;
            admissionDateTextBox.Value = String.Empty;
            emailTextBox.Text = String.Empty;
            addressTextBox.Text = String.Empty;
            doctorNameTextBox.Text = String.Empty;
            agentTextBox2.Text = String.Empty;
            datepicker.Value = String.Empty;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearField();
        }
    }
}