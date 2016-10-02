using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class PurchaseMedicineUi : System.Web.UI.Page
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
        }

        private void BindColumnToGridview()
        {
            DataTable dt = new DataTable();
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
        [WebMethod]
        public static MedicinePurchaseTemp[] BindGridview()
        {
            HospitalDbContext db = new HospitalDbContext();
            DataTable dt = new DataTable();
            List<AddExp.ProductDetails> details = new List<AddExp.ProductDetails>();
            List<MedicinePurchaseTemp> purchaseTemps = db.MedicinePurchaseTemps.ToList();
            string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SPExpenseTamp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", 0);
                cmd.Parameters.AddWithValue("@Description", "");
                cmd.Parameters.AddWithValue("@Amount", 0);
                cmd.Parameters.AddWithValue("@ExpenseType", "");
                cmd.Parameters.AddWithValue("@ExpenseDate", "");
                cmd.Parameters.AddWithValue("@CreatedBy", UserId);
                cmd.Parameters.AddWithValue("@status", "SELECT");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                foreach (DataRow dtrow in dt.Rows)
                {
                    AddExp.ProductDetails product = new AddExp.ProductDetails();
                    product.Id = Convert.ToInt32(dtrow["Id"]);
                    product.Description = dtrow["Description"].ToString();
                    product.Amount = Convert.ToInt32(dtrow["Amount"]);
                    product.ExpenseType = dtrow["ExpenseType"].ToString();
                    details.Add(product);
                }
            }
            return purchaseTemps.ToArray();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            PurchaseTemp temp = new PurchaseTemp();
            decimal sumTotal = ViewState["Total"] == null ? 0 : (decimal)ViewState["Total"];
            MedicineBLL oMedicineBll = new MedicineBLL();
            List<PurchaseTemp> purchaseTemps = new List<PurchaseTemp>();
            List<PurchaseTemp> purchaseTable = new List<PurchaseTemp>();
            temp.MedicineName = medicineTextBox.Text;
            temp.MedicineId = oMedicineBll.GetMedicineIdByName(temp.MedicineName);
            temp.Price = priceTextBox.Text == "" ? 0 : Convert.ToDecimal(priceTextBox.Text);
            temp.Quantity = quantityTextBox.Text == "" ? 0 : Convert.ToInt32(quantityTextBox.Text);
            temp.Total = temp.Price*temp.Quantity;

            oMedicineBll.SaveToTemp(temp);      // if  we need to delete row from table
                                               //we will generate table from db delete from db

            sumTotal += temp.Total;
            sumTotalLabel.Text = sumTotal.ToString();
            ViewState["Total"] = temp.Total;



            #region OLD TEMP TABLE

          
            //if (temp.MedicineId == 0 || temp.Price == 0 || temp.Quantity == 0)
            //{
            //    if(temp.MedicineId == 0)
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

        }

        private void ClearField()
        {
            medicineTextBox.Text = String.Empty;
            priceTextBox.Text = String.Empty;
            quantityTextBox.Text = String.Empty;
        }

        //private void Delete_item(object sender, EventArgs e)
        //{
        //    Button selectedLink = (Button)sender;
        //    string link = selectedLink.ID.Replace("del", "");
        //    int id = Convert.ToInt32(link);
        //    Table1.Controls.RemoveAt(id);
        //}
        protected void btnReset_Click(object sender, EventArgs e)
        {

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

        protected void btnReset_Click1(object sender, EventArgs e)
        {
            ClearField();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            MedicineBLL oMedicineBll = new MedicineBLL();

            string purchasingDate = purchasingDateTextBox.Text;
            bool affeted = oMedicineBll.SavePurchaseMedicine(purchasingDate);
            if (affeted)
            {
                Response.Write("<script>alert('Medicine has been stored successfully!');</script>");
            }
        }
    }
 
}