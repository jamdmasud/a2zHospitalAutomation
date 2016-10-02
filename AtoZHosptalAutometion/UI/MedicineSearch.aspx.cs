using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class MedicineSearch : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //login
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];
            if (oUser.Roles != "Admin" && oUser.Roles != "Pharmacy" && oUser.Roles != "Reception" && oUser.Roles != "Manager")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }

        }

        
        public List<MedicineStore> SearchMedicine(string prefixText)
        {
            List<MedicineStore> Stores = new List<MedicineStore>();
            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //It will be collected from session
                con.Open();
                SqlCommand cmd = new SqlCommand("spMedicineStorage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                List<MedicineStore> medicineStores = new List<MedicineStore>();
                
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MedicineStore store = new MedicineStore();
                        store.Name = reader["Name"].ToString();
                        store.Code = reader["code"].ToString();
                        store.GroupName = reader["GroupName"].ToString();
                        store.Company = reader["Company"].ToString();
                        store.Balance = Convert.ToInt32(reader["Balance"].ToString());
                        medicineStores.Add(store);
                    }
                    reader.Close();
                }
                con.Close();
                Stores = (medicineStores.Where(m => m.Company.Contains(prefixText) || m.GroupName.Contains(prefixText) || m.Name.Contains(prefixText))).ToList();
                Stores = Stores.Where(m => m.Balance > 0).ToList();
            }
            return Stores;
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            medicineGridView.DataSource = SearchMedicine(searcgTextBox.Text);
            medicineGridView.DataBind();
        }
    }
}