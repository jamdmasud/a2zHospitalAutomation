using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class AgentListUI : System.Web.UI.Page
    {

        public static int UserId { set; get; }
        public static string UserRule{ set; get; }
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Login security
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            UserRule = oUser.Roles;
            //if (oUser.Roles != "Admin" && oUser.Roles != "Manager")
            //{
            //    Response.Redirect("~/UI/AccessDeniedUI.aspx");
            //}

            UserId = oUser.Id;
            if (!IsPostBack)
            {
                BindColumnToGridview();
            }
        }

        private void BindColumnToGridview()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SL");
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Address");
            dt.Columns.Add("Operation");
            dt.Rows.Add();
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            gvDetails.Rows[0].Visible = false;
        }
        [WebMethod]
        public static Agent[] BindGridview()
        {
            using (var db = new Entities())
            {
                List<Agent> agents = db.Agents.ToList();
                return agents.ToArray();
            }
        }

        [WebMethod]
        public static string DeleteItem(int id)
        {
            if (UserRule != "Admin" && UserRule != "Manager")
            {
                throw new Exception("You are not eligible to get this service");
            }

            string msg = "false";
            try
            {
                //DateTime expDateTime = expenseDate == null ? DateTime.Today : expenseDate == "" ? DateTime.Today : Convert.ToDateTime(expenseDate);

                string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    //It will be collected from session
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Agent WHERE Id = @Id", con);

                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                    msg = "true";

                    cmd.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception exception)
            {
                msg = exception.Message;
            }
            return msg;
        }
    }
}