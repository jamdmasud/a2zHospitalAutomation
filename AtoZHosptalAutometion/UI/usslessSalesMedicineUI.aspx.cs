using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class SalesMedicineUI : System.Web.UI.Page
    {
        public static int UserId { set; get; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userId"] = 30001;
            UserId = (int)Session["userId"];
            if (!IsPostBack)
            {
                BindColumnToGridview();
            }
            ClearField();

        }

        private void BindColumnToGridview()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SL");
            dt.Columns.Add("Medicine");
            dt.Columns.Add("Rate");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Total");
            dt.Columns.Add("Management");
            dt.Rows.Add();
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
            gvDetails.Rows[0].Visible = false;
        }
        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchMedicine(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name from Medicine where " +
                    "Name like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"].ToString());
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static List<string> SearchCustomerByCode(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Code, Name from Patient where " +
                    "Code like @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> names = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            names.Add(sdr["Name"] +" >> "+ sdr["Code"]);
                        }
                    }
                    conn.Close();
                    return names;
                }
            }
        }

        [WebMethod]
        public static int InsertSale(string medicine, double price, int qty)
        {
            HospitalDbContext db = new HospitalDbContext();

            int medicineId = db.Medicines.Where(m => m.Name == medicine).Select(m => m.Id).FirstOrDefault();
            string constr = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO SalesTemp (MedicineId, Quantity, Price) VALUES(@MedicineId, @Quantity, @Price) SELECT SCOPE_IDENTITY()"))
                {
                    cmd.Parameters.AddWithValue("@MedicineId", medicineId);
                    cmd.Parameters.AddWithValue("@Quantity", qty);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Connection = con;
                    con.Open();
                    int customerId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return customerId;
                }
            }
        }

        [WebMethod]
        public static SalesTemp[] BindGridview()
        {
            using (var db = new HospitalDbContext())
            {
                List<SalesTemp> list = db.SalesTemps.ToList();
                return list.ToArray();
            }
        }

        [WebMethod]
        public static bool crudoperations(string status, string medicine, decimal price, int quantity, int id)
        {
            var db = new HospitalDbContext();
            //DateTime expDateTime = expenseDate == null ? DateTime.Today : expenseDate == "" ? DateTime.Today : Convert.ToDateTime(expenseDate);
            bool msg = false;

            if (status == "INSERT")
            {
                MedicinePurchaseTemp temp = new MedicinePurchaseTemp();
                temp.MedicineName = medicine;
                temp.Quantity = quantity;
                temp.Price = price;
                temp.Total = temp.Quantity * temp.Price;
                temp.CreatedBy = UserId;
                db.MedicinePurchaseTemps.Add(temp);
                msg = db.SaveChanges() > 0;

            }
            else if (status == "DELETE")
            {
                //delete item
                MedicinePurchaseTemp temp = db.MedicinePurchaseTemps.Find(id);
                db.MedicinePurchaseTemps.Attach(temp);
                db.MedicinePurchaseTemps.Remove(temp);
                msg = db.SaveChanges() > 0;
            }
            //cmd.ExecuteNonQuery();


            return msg;
        }

        [ScriptMethod()]
        [WebMethod]
        public static decimal GetProductRate(string purticular)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Price from Medicine where Name like  @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", purticular);
                    cmd.Connection = conn;
                    decimal r = 0.0M;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            r = Convert.ToDecimal(sdr["Price"]);
                        }
                    }
                    conn.Close();
                    return r;
                }
            }
        }

        [WebMethod]
        public static string ConvertToString(int data)
        {
            Functions oFunctions = new Functions();
            string word = oFunctions.NumberToWord(data);
            return word;
        }

        protected void saveSalesButton_Click(object sender, EventArgs e)
        {
           
        }

        private void ClearField()
        {
            medicineTextBox.Text = String.Empty;
            priceTextBox.Text = String.Empty;
            quantityTextBox.Text = String.Empty;
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            PurchaseTemp temp = new PurchaseTemp();
            //decimal sumTotal = ViewState["Total"] == null ? 0 : (decimal)ViewState["Total"];
            MedicineBLL oMedicineBll = new MedicineBLL();
            List<SalesTemp> sales = new List<SalesTemp>();
            List<PurchaseTemp> purchaseTable = new List<PurchaseTemp>();
            temp.MedicineName = medicineTextBox.Text;
            temp.MedicineId = oMedicineBll.GetMedicineIdByName(temp.MedicineName);
            temp.Price = priceTextBox.Text == "" ? 0 : Convert.ToDecimal(priceTextBox.Text);
            temp.Quantity = quantityTextBox.Text == "" ? 0 : Convert.ToInt32(quantityTextBox.Text);
            temp.Total = temp.Price * temp.Quantity;

            //oMedicineBll.SaveToTempSale(temp);      // if  we need to delete row from table


            //sumTotal += temp.Total;
            //sumTotalLabel.Text = "Total: " + sumTotal;
            //ViewState["Total"] = sumTotal;


            //FillTable();

            #region OLD TEMP TABLE


            //if (temp.MedicineId == 0 || temp.Price == 0 || temp.Quantity == 0)
            //{
            //    if (temp.MedicineId == 0)
            //        Response.Write("<script>alert('Please save medicine before purchase entry!');</script>");
            //    purchaseTemps = (List<PurchaseTemp>)ViewState["tableValue"];
            //    if (purchaseTemps != null)
            //    {
            //        foreach (PurchaseTemp item in purchaseTemps)
            //        {
            //            purchaseTable.Add(item);
            //        }
            //    }
            //    purchaseTable.Reverse();
            //    foreach (PurchaseTemp item in purchaseTable)
            //    {
            //        TableCell medicineCell = new TableCell();
            //        TableCell priCell = new TableCell();
            //        TableCell quantityCell = new TableCell();
            //        TableCell totalCell = new TableCell();
            //        TableRow row = new TableRow();
            //        medicineCell.Text = item.MedicineName;
            //        priCell.Text = item.Price.ToString();
            //        quantityCell.Text = item.Quantity.ToString();
            //        totalCell.Text = item.Total.ToString();



            //        row.Controls.Add(medicineCell);
            //        row.Controls.Add(priCell);
            //        row.Controls.Add(quantityCell);
            //        row.Controls.Add(totalCell);
            //        Table1.Controls.Add(row);
            //    }
            //    ViewState["tableValue"] = purchaseTable;

            //}
            //else
            //{
            //    purchaseTable.Add(temp);
            //    purchaseTemps = (List<PurchaseTemp>)ViewState["tableValue"];
            //    if (purchaseTemps != null)
            //    {
            //        foreach (PurchaseTemp item in purchaseTemps)
            //        {
            //            purchaseTable.Add(item);
            //        }
            //    }
            //    purchaseTable.Reverse();

            //    foreach (PurchaseTemp item in purchaseTable)
            //    {
            //        TableCell medicineCell = new TableCell();
            //        TableCell priCell = new TableCell();
            //        TableCell quantityCell = new TableCell();
            //        TableCell totalCell = new TableCell();
            //        TableRow row = new TableRow();
            //        medicineCell.Text = item.MedicineName;
            //        priCell.Text = item.Price.ToString();
            //        quantityCell.Text = item.Quantity.ToString();
            //        totalCell.Text = item.Total.ToString();



            //        row.Controls.Add(medicineCell);
            //        row.Controls.Add(priCell);
            //        row.Controls.Add(quantityCell);
            //        row.Controls.Add(totalCell);
            //        Table1.Controls.Add(row);
            //        ClearField();
            //    }
            //    ViewState["tableValue"] = purchaseTable;
            //}

            #endregion

            ClearField();
            
            //FillTable();
        }

        //private void FillTable()
        //{
        //    MedicineBLL oMedicineBll = new MedicineBLL();
        //    List<SalesTemp> sales;
        //    sales = oMedicineBll.GetSalesMedicineFromTemp(); //we will generate table from db delete from db
        //    decimal? sum = 0;

        //    foreach (SalesTemp item in sales)
        //    {
        //        TableCell medicineCell = new TableCell();
        //        TableCell priCell = new TableCell();
        //        TableCell quantityCell = new TableCell();
        //        TableCell totalCell = new TableCell();
        //        TableCell operationCell = new TableCell();
        //        TableRow row = new TableRow();
        //        medicineCell.Text = oMedicineBll.GetMedicineNameById(item.MedicineId);
        //        priCell.Text = item.Price.ToString();
        //        quantityCell.Text = item.Quantity.ToString();
        //        totalCell.Text = item.Total.ToString();

        //        sum += item.Total;
        //        Button btnButton = new Button
        //        {
        //            ID = "del_" + item.Id,
        //            Text = "Delete",
        //            CssClass = "btn btn-danger" 
        //        };

        //        btnButton.Click += new EventHandler(Delete_Item);

        //        operationCell.Controls.Add(btnButton);

        //        row.Controls.Add(medicineCell);
        //        row.Controls.Add(priCell);
        //        row.Controls.Add(quantityCell);
        //        row.Controls.Add(totalCell);
        //        row.Controls.Add(operationCell);
        //        Table1.Controls.Add(row);
        //    }
        //    sumTotalLabel.Text = sum.ToString();

        //}

        private void Delete_Item(object sender, EventArgs e)
        {
            Button btnButton = (Button)sender;
            string btnId = btnButton.ID.Substring(4);
            int id = Convert.ToInt32(btnId);
            MedicineBLL oMedicineBll = new MedicineBLL();
            oMedicineBll.DeleteMedicine(id);
        }
    }
}