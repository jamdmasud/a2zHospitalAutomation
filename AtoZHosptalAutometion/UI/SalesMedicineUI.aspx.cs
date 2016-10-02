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
        protected void Page_Load(object sender, EventArgs e)
        {
            Table1.Caption = "Dynamic Table";
            Table1.CssClass = "table table-hover";
            sallingDateTextBox.Text = DateTime.Today.ToString();
            FillTable();
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

        protected void saveSalesButton_Click(object sender, EventArgs e)
        {
            MedicineBLL oMedicineBll = new MedicineBLL();
            PatientBLL oPatientBll = new PatientBLL();
            string sallingDate = sallingDateTextBox.Text;
            string NameCode = customerIdTextbox.Text;
            string Code = NameCode.Substring(NameCode.Length - 8);
            int customerId = oPatientBll.GetCustomerIdByCode(Code);
            string patientType = patientTypeDropDownList.Text;
            bool affected = oMedicineBll.SaveSallingMedicine(sallingDate, customerId, patientType);
            if (affected)
            {
                //show print button
                printReportButton.Visible = true;

            }
            else
            {
                //submission failed
            }
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

            oMedicineBll.SaveToTempSale(temp);      // if  we need to delete row from table


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
            if(!IsPostBack)
            FillTable();
        }

        private void FillTable()
        {
            MedicineBLL oMedicineBll = new MedicineBLL();
            List<SalesTemp> sales;
            sales = oMedicineBll.GetSalesMedicineFromTemp(); //we will generate table from db delete from db
            decimal? sum = 0;

            foreach (SalesTemp item in sales)
            {
                TableCell medicineCell = new TableCell();
                TableCell priCell = new TableCell();
                TableCell quantityCell = new TableCell();
                TableCell totalCell = new TableCell();
                TableCell operationCell = new TableCell();
                TableRow row = new TableRow();
                medicineCell.Text = oMedicineBll.GetMedicineNameById(item.MedicineId);
                priCell.Text = item.Price.ToString();
                quantityCell.Text = item.Quantity.ToString();
                totalCell.Text = item.Total.ToString();

                sum += item.Total;
                Button btnButton = new Button
                {
                    ID = "del_" + item.Id,
                    Text = "Delete",
                    CssClass = "btn btn-danger" 
                };

                btnButton.Click += new EventHandler(Delete_Item);

                operationCell.Controls.Add(btnButton);

                row.Controls.Add(medicineCell);
                row.Controls.Add(priCell);
                row.Controls.Add(quantityCell);
                row.Controls.Add(totalCell);
                row.Controls.Add(operationCell);
                Table1.Controls.Add(row);
            }
            sumTotalLabel.Text = sum.ToString();

        }

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