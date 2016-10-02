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
    public partial class RegisterAgent : System.Web.UI.Page
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Agent oAgent = new Agent();
            AgentBLL oAgentBll = new AgentBLL();
            try
            {
                oAgent.UpdatedBy = 30001; //this value will be collected from session
                oAgent.Name = nameTextBox.Text;
                oAgent.Phone = phoneTextBox.Text;
                oAgent.Address = addressTextBox.Text;
                oAgent.RegDate = regDateTextBox.Text == "" ? DateTime.Now : Convert.ToDateTime(regDateTextBox.Text);
                string code = oAgentBll.Register(oAgent);
                Session["Code"] = code;
                Response.Redirect("~/UI/AgentRegistrationSuccess.aspx");
            }
            catch (Exception exception)
            {
                successPanel.Visible = false;
                faildPanel.Visible = true;
                faildLabel.Text = exception.Message;
            }
        }
    }
}