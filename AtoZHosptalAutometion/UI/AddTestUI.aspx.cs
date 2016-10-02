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
    public partial class AddTestUI : System.Web.UI.Page
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
            Test oTest = new Test();
            ServiceBLL oServiceBll = new ServiceBLL();

            try
            {
                oTest.Name = testNameTextBox.Text;
                oTest.Rate = Convert.ToDecimal(rateTextBox.Text);
                oTest.UpdatedBy = 300001; //from session
                oTest.UpdatedDate = DateTime.Now;
                if (oServiceBll.AddTests(oTest) > 0)
                {
                    //
                    successPanel.Visible = true;
                    testNameTextBox.Text = "";
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