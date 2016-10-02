using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AtoZHosptalAutometion.UI
{
    public partial class AgentRegistrationSuccess : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Session["Code"].ToString();
            statusLabel.Text = string.Format("New Agent Code:  {0}", code);
        }
    }
}