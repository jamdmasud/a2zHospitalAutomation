using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class DoctorRegistration : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        private int userId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //login
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            userId = oUser.Id;
            //Identify user type
            if (oUser.Roles == "Pharmacy")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Doctor oDoctor = new Doctor();

            DoctorBLL oDoctorBll = new DoctorBLL();
            try
            {

                //from session
                oDoctor.UpdatedBy = userId;

                oDoctor.Name = nameTextBox.Text;
                oDoctor.Age = Convert.ToInt32(ageTextBox1.Text);
                oDoctor.Email = emailTextBox.Text;
                oDoctor.PermanetHospital = permanentHospitalTextBox.Text;
                oDoctor.Mobile = phoneTextBox.Text;
                oDoctor.JoiningDate = Convert.ToDateTime(joiningTextBox.Text);
                oDoctor.Address = addressTextBox.Text;
                oDoctor.Specialist = specialistTextBox.Text;


                string code = oDoctorBll.Register(oDoctor);

                ClearField();

                Response.Write("<script>");
                Response.Write("alert('Your  Code : " + code + "');");
                Response.Write("</script>");
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
            nameTextBox.Text = String.Empty;
            ageTextBox1.Text = String.Empty;
            emailTextBox.Text = String.Empty;
            permanentHospitalTextBox.Text = String.Empty;
            phoneTextBox.Text = String.Empty;
            joiningTextBox.Text = String.Empty;
            addressTextBox.Text = String.Empty;
            specialistTextBox.Text = String.Empty;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearField();
        }
    }
}