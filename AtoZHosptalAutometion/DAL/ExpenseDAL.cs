using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.DAL
{
    public class ExpenseDAL
    {
        public List<ExpenseTamp> GetExpenseFromTemp(int? userId)
        {
            try
            {
                using (var db = new Entities())
                {
                    return db.ExpenseTamps.Where(e => e.CreatedBy == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveExpensesTamp(int userId)
        {
            try
            {
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "delete from ExpenseTamp where CreatedBy = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    int affected = command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public int SaveExapense(List<Expens> oExpenses)
        {
            try
            {
                int affected = 0;
                using (var db = new Entities())
                {
                    foreach (Expens expens in oExpenses)
                    {
                        db.Expenses.Add(expens);
                    }
                    affected = db.SaveChanges();
                }
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public DataSet ShowExpense(DateTime fromDate, DateTime tomDate)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);

                string query = " select e.Description [Description], u.Name, CONVERT(date, CONVERT(varchar, e.ExpenseDate) , 20) ExpenseDate, e.Amount from Expenses e left join Users u on e.UpdatedBy = u.Id where  ExpenseDate between @startDate and @endDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startDate", fromDate);
                    command.Parameters.AddWithValue("@endDate", tomDate);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    dt.TableName = "Command";
                    ds.Tables.Add(dt.Copy());
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }

        public DataSet ShowDeposit(DateTime fromDate, DateTime tomDate)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "select @StartDate as fromDate, @EndDate as toDate, Particulars ,DateOfDeal,Diposit,EmployeeName,UpdatedDate,UpdatedBy  from Voucher where UpdatedDate between @StartDate and @EndDate";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", fromDate);
                    command.Parameters.AddWithValue("@EndDate", tomDate);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    dt.TableName = "Command";
                    ds.Tables.Add(dt.Copy());
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return ds;
        }
    }
}