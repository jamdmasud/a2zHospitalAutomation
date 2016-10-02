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
    public partial class AddServicesUI : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
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
            Service oService = new Service();
            ServiceBLL oServiceBll = new ServiceBLL();

            try
            {
                oService.Name = serviceNameTextBox.Text;
                oService.Rate = Convert.ToDecimal(rateTextBox.Text);
                oService.UpdatedBy = 300001; //from session
                oService.UpdatedDate = DateTime.Now;
                if (oServiceBll.AddService(oService) > 0)
                {
                    //
                    Response.Write("<script>alert('Saved successfully!');</script>");
                    serviceNameTextBox.Text = "";
                    rateTextBox.Text = "";
                }
            }
            catch (Exception exception)
            {
                faildPanel.Visible = true;
                faildLabel.Text = exception.InnerException.Message;
            }
        }
    }
}