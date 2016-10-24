using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AtoZHosptalAutometion.Models;
using AtoZHosptalAutometion.UI;

namespace AtoZHosptalAutometion.DAL
{
    public class ServiceDAL
    {
        public List<IncomeTamp> GetIncomeFromTamp(int? userId)
        {
            try
            {
                using (var db = new Entities())
                {
                    return db.IncomeTamps.Where(i => i.CreatedBy == userId).ToList();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int SaveOutDoorService(List<Income> oIncomes)
        {
            try
            {
                using (var db = new Entities())
                {
                    int affected = 0;
                    foreach (Income income in oIncomes)
                    {
                        db.Incomes.Add(income);
                    }
                    affected = db.SaveChanges();
                    return affected;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int RemoveIncomTamp(int? userId)
        {
            try
            {
                int affected = 0;
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "delete from IncomeTamp where CreatedBy = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    affected = command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                }
                return affected;
            }
            catch (Exception mgs)
            {
                throw new Exception(mgs.InnerException.Message);
            }
        }

        public int RemoveIndoorIncomTamp(int? userId)
        {
            try
            {
                int affected = 0;
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "delete from IndoorIncomeTamp where CreatedBy = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    affected = command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                }
                return affected;
            }
            catch (Exception mgs)
            {
                throw new Exception(mgs.InnerException.Message);
            }
        }

        public List<IndoorIncomeTamp> GetIncomeFromIndoorTamp(int? userId)
        {
            try
            {
                using (var db = new Entities())
                {
                    return db.IndoorIncomeTamps.Where(i => i.CreatedBy == userId).ToList();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int SaveInDoorService(List<Income> oIncomes)
        {
            try
            {
                using (var db = new Entities())
                {
                    int affected = 0;
                    foreach (Income income in oIncomes)
                    {
                        db.Incomes.Add(income);
                    }
                    affected = db.SaveChanges();
                    return affected;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public void SaveOutDoorInvoiceSub(InvoiceSub oInvoiceSub)
        {
            try
            {
                using (var db = new Entities())
                {
                    db.InvoiceSubs.Add(oInvoiceSub);

                    int affected = db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public DataSet GetOutdoorServiceData(int invoiceId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                using (var db = new Entities())
                {
                    string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                    SqlConnection connection = new SqlConnection(cs);
                    string query = "spOutdoorService";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(dt);
                        dt.TableName = "Command";
                        ds.Tables.Add(dt.Copy());
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
            return ds;
        }
        public DataSet GetIndoorServiceData(int invoiceId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "spIndoorService";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    dt.TableName = "Command";
                    ds.Tables.Add(dt.Copy());
                    command.Dispose();
                    connection.Close();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
            return ds;
        }

        public int ChangeDeliveryStatus(int invoiceId, int status)
        {
            try
            {
                int affected = 0;
                using (var db = new Entities())
                {
                    Invoice oInvoice = db.Invoices.Find(invoiceId);
                    oInvoice.isProductDelivered = Convert.ToBoolean(status);
                    affected = db.SaveChanges();
                }
                return affected;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int UpdatePayment(int id,  decimal paid)
        {
            try
            {
                int affected = 0;
                using (var db = new Entities())
                {
                    InvoiceSub oInvoiceSub = db.InvoiceSubs.FirstOrDefault(ins => ins.InvoiceId == id);
                    if (oInvoiceSub != null)
                    {
                        oInvoiceSub.Paid = oInvoiceSub.Paid + paid;
                         oInvoiceSub.Discount = oInvoiceSub.Total - oInvoiceSub.Paid;

                        oInvoiceSub.Due = 0;
                    }
                    affected = db.SaveChanges();
                }
                return affected;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int AddService(Service oService)
        {
            try
            {
                int affected = 0;
                using (var db = new Entities())
                {
                    string lastCode = db.Services.OrderByDescending(s => s.Id).Select(s => s.Code).FirstOrDefault();
                    int num = Convert.ToInt32(lastCode.Substring(4)) + 1;
                    string str = lastCode.Substring(0, 4);
                    string code = str + num;
                    oService.Code = code;
                    db.Services.Add(oService);
                    affected = db.SaveChanges();
                }
                return affected;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int AddTests(Test oTest)
        {
            try
            {
                int affected = 0;
                using (var db = new Entities())
                {
                    string lastCode = db.Tests.OrderByDescending(s => s.Id).Select(s => s.Code).FirstOrDefault();
                    int num = Convert.ToInt32(lastCode.Substring(3)) + 1;
                    string str = lastCode.Substring(0, 3);
                    string code = str + num;
                    oTest.Code = code;
                    db.Tests.Add(oTest);
                    affected = db.SaveChanges();
                }
                return affected;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public void InsertDue(int id, decimal paid, User oUser)
        {
            
        }
    }
}