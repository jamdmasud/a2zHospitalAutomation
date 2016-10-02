using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.DAL
{
    public class CoreDAL
    {
        
        public List<InvoiceType> GetInvoiceType()
        {
            using (var db = new Entities())
            {
               return db.InvoiceTypes.ToList(); 
            }
           
        }

        public int SaveInvoice(Invoice oInvoice)
        {
            oInvoice.CustomerId = oInvoice.CustomerId ?? 0;
            oInvoice.DoctorName = oInvoice.DoctorName ?? "";
            int id = 0;
            try
            {
                int affected = 0;
                using (var dbContext  = new Entities())
                {
                    id = dbContext.Invoices.OrderByDescending(i => i.Id).Select(i => i.Id).FirstOrDefault();
                     oInvoice.Id = id + 1;
                    string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                    SqlConnection connection = new SqlConnection(cs);
                    SqlCommand command = new SqlCommand();
                    string query = "insert into Invoice (Id, InvoiceDate, InvoiceType, UserId, CustomerId, DoctorName, UpdatedBy, UpdatedDate) values(@Id, @InvoiceDate, @InvoiceType, @UserId, @CustomerId, @DoctorName, @UpdatedBy, @UpdatedDate)";
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@Id", oInvoice.Id);
                    command.Parameters.AddWithValue("@InvoiceDate", oInvoice.InvoiceDate);
                    command.Parameters.AddWithValue("@InvoiceType", oInvoice.InvoiceType);
                    command.Parameters.AddWithValue("@UserId", oInvoice.UserId);
                    command.Parameters.AddWithValue("@CustomerId", oInvoice.CustomerId);
                    command.Parameters.AddWithValue("@DoctorName", oInvoice.DoctorName);
                    command.Parameters.AddWithValue("@UpdatedBy", oInvoice.UpdatedBy);
                    command.Parameters.AddWithValue("@UpdatedDate", oInvoice.UpdatedDate);

                    connection.Open();
                    affected = command.ExecuteNonQuery();
                    connection.Close();
                }

                if (affected > 0)
                {
                    using (var  db = new Entities())
                    {
                       id = db.Invoices.OrderByDescending(i => i.Id).Select(i => i.Id).FirstOrDefault(); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DAL:" + ex.Message);
            }
            return id;
        }

        public int SaveDiposit(Voucher oVoucher)
        {
            try
            {
                int aff = 0, id = 0;
                using (var db = new Entities())
                {
                    db.Vouchers.Add(oVoucher);
                    aff = db.SaveChanges();
                    if (aff > 0)
                    {
                        List<Voucher> voucher = db.Vouchers.ToList();
                        id = voucher.OrderByDescending(v => v.Id).Select(v => v.Id).FirstOrDefault();
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DAL:"+ex.Message);
            }
        }

        public DataSet GetDipositData(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string cs = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    //It will be collected from session
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Voucher where Id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dt.TableName = "Command";
                    ds.Tables.Add(dt.Copy());
                    cmd.Dispose();
                    con.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
    }
}