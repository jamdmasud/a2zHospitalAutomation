using System;
using System.Web.UI;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void singInButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernames.Value;
                string password = passwords.Value;
                AccountBll oAccountBll = new AccountBll();
                User oUser = oAccountBll.GetUserInfo(username, password);
                
                Session["user"] = oUser;
                if(oUser != null)
                Session["Login"] = true;
                Response.Redirect("default.aspx");
                
            }
            catch (Exception exception)
            {
                failLabel.Visible = true;
                failLabel.Text = exception.Message+"<br/>";
                failLabel.CssClass = "textRed";
            }
        }
    }
}