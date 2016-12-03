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
    public partial class PatientListUI : System.Web.UI.Page
    {
        public static int UserId { set; get; }
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Login security
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];

           
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
            dt.Columns.Add("Specialist");
            dt.Columns.Add("From");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Email");
            dt.Columns.Add("Operation");
            dt.Rows.Add();
            //gvDetails.DataSource = dt;
            //gvDetails.DataBind();
            //gvDetails.Rows[0].Visible = false;
        }
        [WebMethod]
        public static List<Doctor> BindGridview()
        {
            using (var db = new Entities())
            {
                List<Doctor> doctors = db.Doctors.ToList();
                return doctors;
            }
        }

        [WebMethod]
        public static string DeleteItem(int id)
        {

            string msg = "false";
            try
            {
                //DateTime expDateTime = expenseDate == null ? DateTime.Today : expenseDate == "" ? DateTime.Today : Convert.ToDateTime(expenseDate);

                string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    //It will be collected from session
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Doctors WHERE Id = @Id", con);

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