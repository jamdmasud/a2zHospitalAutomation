using System;
using System.Collections.Generic;
using System.Web.UI;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class AgentPayment : Page
    {
        private int userId { get; set; }
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Login security
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            userId = oUser.Id;
            if (oUser.Roles != "Admin" && oUser.Roles != "Manager")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                AgentBLL oAgentBll = new AgentBLL();
                int agentId = oAgentBll.GetAgentIdByCode(agentIDTextBox.Text);
                int amount = Convert.ToInt32(dueTextBox1.Text);
                if (oAgentBll.AgentDuePayment(agentId, amount, userId))
                {
                    List<HonorariumPayment> oPayment = oAgentBll.PayAgent(agentId);
                    agentDueGridView.DataSource = oPayment;
                    agentDueGridView.DataBind();
                    dueTextBox1.Text = String.Empty;
                    
                }

            }
            catch (Exception exception)
            {
                Response.Write("<scriptr>alert('" + exception.InnerException.Message + "');</script>");
            }
        }

        protected void showResultButton_Click(object sender, EventArgs e)
        {
            try
            {
                AgentBLL oAgentBll = new AgentBLL();
                int AgentId = oAgentBll.GetAgentIdByCode(agentIDTextBox.Text);
                List<HonorariumPayment> oPayment = oAgentBll.PayAgent(AgentId);
                agentDueGridView.DataSource = oPayment;
                agentDueGridView.DataBind();

            }
            catch (Exception exception)
            {
                Response.Write("<scriptr>alert('" + exception.InnerException.Message + "');</script>");
            }
        }
    }

    public class HonorariumPayment
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Paid { get; set; }
        public decimal Honorarium { get; set; }
    }
}