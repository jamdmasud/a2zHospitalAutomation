using System;
using System.Web;


namespace AtoZHosptalAutometion
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            
        }
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Response.Redirect("~/Login.aspx");

        }
    }
}