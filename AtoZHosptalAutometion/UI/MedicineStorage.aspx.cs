using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class MedicineStorage : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //login
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            if (oUser.Roles != "Admin" && oUser.Roles != "Pharmacy" && oUser.Roles != "Manager")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }

            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //It will be collected from session
                con.Open();
                SqlCommand cmd = new SqlCommand("spMedicineStorage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dt);
                dt.TableName = "Command";
                ds.Tables.Add(dt.Copy());

                medicineGridView.DataSource = ds;
                medicineGridView.DataBind();
                 
                cmd.Dispose();
                con.Close();
            }
        }
    }
}