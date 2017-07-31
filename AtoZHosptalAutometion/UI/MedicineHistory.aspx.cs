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
    public partial class MedicineHistory : System.Web.UI.Page
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
            int medicineId = Convert.ToInt32(Request.QueryString["id"]);
            medicineGridView.DataSource = SearchMedicine(medicineId);
            medicineGridView.DataBind();
        }

        
        public List<VmMedicineHistory> SearchMedicine(int id)
        {
            List<VmMedicineHistory> Stores = new List<VmMedicineHistory>();
            List<VmMedicineHistory> Stores2 = new List<VmMedicineHistory>();

            Entities db = new Entities();

            var sale = db.Sales.ToList().Where(a => a.MedicineId == id).Select(s => new VmMedicineHistory
            {
                MedicineId = s.MedicineId,
                MedicineName = s.Medicine?.Name,
                TransactionDate = s.UpdatedDate??DateTime.Now,
                Category = "Sold",
                EmployeeName = s.EmployeeId.ToString(),
                Sold = s.Quantity,
                Balance = 0
            }).ToList();
            var purchase = db.Purchases.ToList().Where(a => a.MedicineId == id).Select(s => new VmMedicineHistory
            {
                MedicineId = s.MedicineId,
                MedicineName = "",
                TransactionDate = s.UpdatedDate ?? DateTime.Now,
                Category = "Purchase",
                EmployeeName = s.EmployeeId.ToString(),
                Purchased = s.Quantity,
            }).ToList();
            Stores = sale;
            foreach (var item in purchase)
            {
                VmMedicineHistory m = new VmMedicineHistory()
                {
                    MedicineId = item.MedicineId,
                    MedicineName = GetMedicineName(item.MedicineId),
                    Category = item.Category,
                    Balance = 0,
                    EmployeeName = item.EmployeeName,
                    Purchased = item.Purchased,
                    TransactionDate = item.TransactionDate
                };
                Stores.Add(m);
            }

            Stores2 = Stores.OrderBy(a => a.TransactionDate).ToList();
            Stores.Clear();
            decimal balance = Convert.ToDecimal(db.Medicines.FirstOrDefault(a => a.Id == id)?.Quantity);

            Stores.Add(new VmMedicineHistory() {Balance = balance,Category = "", Sold = 0, Purchased = 0,EmployeeName = "Opening Balance",MedicineId = 0,MedicineName = ""});
            foreach (var item in Stores2)
            {
                VmMedicineHistory m = new VmMedicineHistory();
                balance = item.Category == "Sold" ? balance - item.Sold : balance + item.Purchased;
                m.MedicineId = item.MedicineId;
                m.MedicineName = GetMedicineName(item.MedicineId);
                m.Category = item.Category;
                m.EmployeeName = GetEmployeeName(int.Parse(item.EmployeeName));
                m.Purchased = item.Purchased;
                m.Sold = item.Sold;
                m.TransactionDate = item.TransactionDate.Date;
                m.Balance = balance;
                Stores.Add(m);
            }
            balance = 0;
            medicineNameLabel.Text = Stores.FirstOrDefault(a=>!string.IsNullOrEmpty(a.MedicineName))?.MedicineName;
            return Stores;
        }

        private string GetEmployeeName(int id)
        {
            Entities db = new Entities();
            return db.Users.FirstOrDefault(a => a.Id == id)?.Name;
        }
        private string GetMedicineName(int id)
        {
            Entities db = new Entities();
            return db.Medicines.FirstOrDefault(a=>a.Id == id)?.Name;
        }


    }
}