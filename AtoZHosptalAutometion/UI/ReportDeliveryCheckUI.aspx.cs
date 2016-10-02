using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Mapping;
using System.Data.SqlClient;
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
    public partial class ReportDeliveryCheckUI : System.Web.UI.Page
    {
        private User oUser = null;
        private bool login = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Login security
            if (Session["login"] != null) login = (bool)Session["login"];
            if (login == false) Response.Redirect("~/Login.aspx");
            oUser = (User)Session["user"];

            //Identify user type
            if (oUser.Roles != "Admin" && oUser.Roles != "Reception")
            {
                Response.Redirect("~/UI/AccessDeniedUI.aspx");
            }
        }
        [ScriptMethod()]
        [WebMethod]
        public static List<ReportChecker> SearchInvoice(int prefixText)
        {
            List<ReportChecker> details = new List<ReportChecker>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select * from vwReportChecker where invoiceid = @SearchText";
                        cmd.Parameters.AddWithValue("@SearchText", prefixText);
                        cmd.Connection = conn;
                        conn.Open();
                       
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    ReportChecker detail = new ReportChecker();
                                    detail.DoctorName = sdr["DoctorName"].ToString();
                                    detail.PatienName = sdr["PatientName"].ToString();
                                    detail.Phone = sdr["Phone"].ToString();
                                    detail.GrandTotal = Convert.ToDecimal(sdr["GrandTotal"]);
                                    detail.Paid = Convert.ToDecimal(sdr["Paid"]);
                                    detail.Due = Convert.ToDecimal(sdr["Due"]);
                                    bool isdel = Convert.ToBoolean(sdr["isProductDelivered"]);
                                    detail.IsProductDelivered = isdel == true ? "Yes" : "No";
                                    detail.InvoiceId = Convert.ToInt32(sdr["invoiceId"]);
                                    detail.InvoiceDate = Convert.ToDateTime(sdr["InvoiceDate"]).ToShortDateString();
                                    details.Add(detail);
                                }

                            }
                        }
                        conn.Close();
                        
                    }
                }
                
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
            return details;
        }

        protected void showResultButton_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(invoiceIDTextBox.Text);
                IEnumerable<ReportChecker> oChecker = SearchInvoice(id);
                GridView.DataSource = oChecker;
                GridView.DataBind();
            }
            catch (Exception exception)
            {
                Response.Write("<script>alert('"+exception.Message+"');</script>");
            }
        }

        protected void deliveryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ServiceBLL oServiceBll = new ServiceBLL();
                int id = Convert.ToInt32(invoiceIDTextBox.Text);
                IEnumerable<ReportChecker> oChecker = SearchInvoice(id);
                int iv = oChecker.Select(p => p.InvoiceId).FirstOrDefault();
                int status = Convert.ToInt32(deliveryDropDownList.SelectedValue);
                int affected = oServiceBll.ChangeDeliveryStatus(iv, status);

                if (affected > 0)
                {
                    GridView.DataSource = oChecker;
                    GridView.DataBind();
                }
            }
            catch (Exception exception)
            {
                Response.Write("<script>alert('" + exception.Message + "');</script>");
            }
    }
    }

    public class ReportChecker
    {
        public int InvoiceId { get; set; }
        public string PatienName { get; set; }
        public string DoctorName { get; set; }
        public string Phone { get; set; }
        public string InvoiceDate { get; set; }
        public string IsProductDelivered { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Due { get; set; }
        public decimal Discount { get; set; }
        public decimal Paid { get; set; }

    }

}