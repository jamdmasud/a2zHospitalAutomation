using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using AtoZHosptalAutometion.BLL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.UI
{
    public partial class DuePaymentUI : Page
    {
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //login
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];

            if (oUser.Roles != "Admin" && oUser.Roles != "Pharmacy" && oUser.Roles != "Reception")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }
        }



        [ScriptMethod()]
        [WebMethod]
        public static List<ReportChecker> SearchInvoice(int prefixText)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select i.id as invoiceId, i.InvoiceDate InvoiceDate, ins.GrandTotal, ins.Discount," +
                                      " ins.Due, ins.Paid, p.Name, p.Phone from Invoice i   inner join InvoiceSub ins" +
                                      " on i.Id = ins.InvoiceId left join Patient p on i.CustomerId = p.Id" +
                                      " where invoiceid = @SearchText";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<ReportChecker> details = new List<ReportChecker>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                ReportChecker detail = new ReportChecker();
                                detail.PatienName = sdr["Name"].ToString();
                                detail.Phone = sdr["Phone"].ToString();
                                detail.GrandTotal = Convert.ToDecimal(sdr["GrandTotal"]);
                                detail.Paid = Convert.ToDecimal(sdr["Paid"]);
                                detail.Discount = Convert.ToDecimal(sdr["Discount"]);
                                detail.Due = Convert.ToDecimal(sdr["Due"]);
                                detail.InvoiceId = Convert.ToInt32(sdr["invoiceId"]);
                                detail.InvoiceDate = Convert.ToDateTime(sdr["InvoiceDate"]).ToShortDateString();
                                details.Add(detail);
                            }

                        }
                    }
                    conn.Close();
                    return details;
                }
            }
        }

        protected void showResultButton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(invoiceIDTextBox.Text);
            IEnumerable<ReportChecker> oChecker = SearchInvoice(id);
            GridView1.DataSource = oChecker;
            GridView1.DataBind();
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            decimal paid = Convert.ToDecimal(dueTextBox1.Text);
            int id = Convert.ToInt32(invoiceIDTextBox.Text);
            IEnumerable<ReportChecker> oChecker = SearchInvoice(id);
            decimal total = oChecker.Select(p => p.GrandTotal).FirstOrDefault();
            ServiceBLL oServiceBll = new ServiceBLL();
            int invoiceId = oServiceBll.UpdatePayment(id, paid, oUser);
            if (invoiceId > 0)
            {

                DataSet ds = oServiceBll.GetIndoorServiceData(id);
                Session["rpt"] = ds;
                printButton.Visible = true;
                printButton.PostBackUrl = "~/UI/ReportForm/IndoorServiceViewer.aspx";
                // show success massage
                Response.Write("<script>alert('Bill submited successfully!');</script>");
            }
            else
            {
                //show error message
                Response.Write("<script>alert('Bill submition failed!');</script>");
            }
        }
    }
}