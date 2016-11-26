using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using AtoZHosptalAutometion.Models;
using AtoZHosptalAutometion.UI;

namespace AtoZHosptalAutometion.DAL
{
    public class MedicineDAL
    {
        
        public bool Save(Medicine medicine)
        {
            try
            {
                using (var db = new Entities())
                {
                    db.Medicines.Add(medicine);
                    int affected = db.SaveChanges();
                    return affected >= 0;
                }
            }
            catch (Exception)
            {
                throw new Exception("Opsss! we can't Save your medicine!");
            }
        }

        public int? GetGroupId(string group)
        {
            try
            {
                int Id = 0;
                using (var db = new Entities())
                {
                   Id = db.Groups.Where(g => g.Name == group).Select(g => g.Id).FirstOrDefault();
                }
                return Id;
            }
            catch (Exception)
            {
                throw  new Exception("Sorry, We can't get the group ID");
            }
        } 

        public int? GetCompanyId(string name)
        {
            try
            {
                int Id = 0;
                using (var db = new Entities())
                {
                    Id = db.Companies.Where(c => c.Name == name).Select(c => c.Id).FirstOrDefault();
                }
                return Id;
            }
            catch (Exception)
            {
                throw new Exception("Sorry, We can't get the Company ID");
            }
        }

        

        public string GetLastCode()
        {
            try
            {
                string code = "";
                using (var db = new Entities())
                {
                    code = db.Medicines.ToList().Select(m => m.Code).LastOrDefault();
                }
                return code;
            }
            catch (Exception)
            {
                throw new Exception("Sorry, We can't get the Code");
            }
        }
        

        public int SaveGroup(Group group)
        {
            try
            {
                int Id = 0;
                using (var db = new Entities())
                {
                    db.Groups.Add(group);
                    int affectd = db.SaveChanges();
                    Id = db.Groups.Where(g => g.Name == group.Name).Select(g => g.Id).LastOrDefault();
                }
                return Id;
            }
            catch (Exception)
            {
                throw new Exception("Sorry, We can't get the Code");
            }
            
           
        }

        public int SaveCompany(Company company)
        {
            try
            {
                using (var db = new Entities())
                {
                    db.Companies.Add(company);
                    int affectd = db.SaveChanges();
                    return db.Companies.Where(g => g.Name == company.Name).Select(g => g.Id).LastOrDefault();
                }
            }
            catch (Exception)
            {
                throw new Exception("Opss! we can't save your company");
            }
        }

        public bool IsGroupExist(string groupName)
        {
            try
            {
                using (var db = new Entities())
                {
                    string exist = null;
                    exist = db.Groups.Where(g => g.Name == groupName).Select(g => g.Name).FirstOrDefault();
                    return exist != null;
                }
            }
            catch (Exception)
            {
                throw  new Exception("Something going to wrong");
            }
        }

        public bool IsCompanyExist(string companyName)
        {
            try
            {
                using (var db = new Entities())
                {
                    string exist = null;
                    exist = db.Companies.Where(g => g.Name == companyName).Select(g => g.Name).FirstOrDefault();
                    return exist != null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Something going to wrong");
            }
            
        }

        public int GetMedicineIdByName(string medicineName)
        {
            int id = 0;
            try
            {
                using (var dbContext  = new Entities())
                {
                   id = dbContext.Medicines.Where(m => m.Name == medicineName).Select(m => m.Id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                
                throw new Exception(e.InnerException.Message);
            }
            return id;
        }
            

        public void SaveToTemp(PurchaseTemp temp)
        {
            try
            {
                using (var db = new Entities())
                {
                    MedicinePurchaseTemp mpt = new MedicinePurchaseTemp();
                    mpt.MedicineName = temp.MedicineName;
                    mpt.Quantity = temp.Quantity;
                    mpt.Price = temp.Price;
                    mpt.Total = temp.Total;
                    db.MedicinePurchaseTemps.Add(mpt);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Something going to wrong");
            }
        }

        public List<MedicinePurchaseTemp> GetMedicineFromPurchaseTemp(int userId)
        {
            List<MedicinePurchaseTemp> temps = new List<MedicinePurchaseTemp>();
            try
            {
                using (var dbContext = new Entities())
                {
                    temps = dbContext.MedicinePurchaseTemps.Where(m => m.CreatedBy == userId).ToList();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.InnerException.Message);
            }
            return temps;
        }

        public int SavePurchaseMedicine(List<Purchase> purchases)
        {
            int affected = 0;
            try
            {
                using ( var dbs = new Entities())
                {
                    foreach (Purchase purchase in purchases)
                    {
                        #region IF CUSTOMER NEED TO CHANGE
                        //if (IsMedicineExist(purchase.MedicineId))
                        //{
                        //    //update medicine quantity
                        //    Purchase purchasesToUpdate = db.Purchases.Find(purchase.MedicineId);
                        //    purchasesToUpdate.Quantity = purchase.Quantity;
                        //    purchasesToUpdate.UpdatedBy = purchase.UpdatedBy;
                        //    purchasesToUpdate.UpdatedDate = p
                        //}
                        //else
                        //{

                        //}

                        #endregion

                        //Save medicine
                        dbs.Purchases.Add(purchase);

                    }
                    affected = dbs.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return affected;
        }

        private bool IsMedicineExist(int medicineId)
        {
            int mId = 0;
            try
            {
                using (var db = new Entities())
                {
                    mId = db.Purchases.Where(p => p.MedicineId == medicineId).Select(p => p.MedicineId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return mId != 0;
        }

        public void RemoveTemp(int? userId)
        {
            try
            {
                using (var dbContext = new Entities())
                {
                    List<MedicinePurchaseTemp> temps = dbContext.MedicinePurchaseTemps.Where(p => p.CreatedBy == userId).ToList();
                    //dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [MedicinePurchaseTemp]");
                    foreach (MedicinePurchaseTemp temp in temps)
                    {
                        dbContext.MedicinePurchaseTemps.Attach(temp);
                        dbContext.MedicinePurchaseTemps.Remove(temp);
                    }
                    dbContext.SaveChanges(); 
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            
        }

        public bool SaveToTempSale(SalesTemp temp)
        {
            try
            {
                using (var dbContext = new Entities())
                {
                    dbContext.SalesTemps.Add(temp);
                   bool affected = dbContext.SaveChanges() > 0;
                    return affected;
                }
                
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int SaveSalesMedicine(List<Sale> oSales)
        {
            int affected = 0;
            try
            {
                using (var context = new Entities())
                {
                    foreach (Sale sale in oSales)
                    {
                        context.Sales.Add(sale);
                    }
                   affected = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: In SaveSaleMedicine :::"+ex.InnerException.Message);
            }
            return affected;
        }

        public List<SalesTemp> GetMedicineFromSalesTemp(int userId)
        {
            List<SalesTemp> temps = new List<SalesTemp>();
            try
            {
                using (var context = new Entities())
                {
                  temps = context.SalesTemps.Where(s =>s.CreatedBy == userId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }

            return temps;
        }

        public void RemoveSalesTemp(int? userId)
        {
            try
            {
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "delete from SalesTemp where CreatedBy = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    int affected = command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetMedicineNameById(int? medicineId)
        {
            try
            {
                using (var dbContext = new Entities())
                {
                   string name = dbContext.Medicines.Where(s => s.Id == medicineId).Select(s => s.Name).FirstOrDefault();
                    return name;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public List<SalesTemp> GetSalesMedicineFromTemp()
        {
            List<SalesTemp> sales;
            try
            {
                using (var dbContext = new Entities())
                {
                    sales = dbContext.SalesTemps.ToList();
                    return sales;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteMedicine(int id)
        {
            try
            {
                using (var dbContext = new Entities())
                {
                    SalesTemp temp = dbContext.SalesTemps.Find(id);
                    dbContext.SalesTemps.Attach(temp);
                    dbContext.SalesTemps.Remove(temp);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SavePurchaseMedicineToInvoiceSub(InvoiceSub oInvoiceSub)
        {
            try
            {
                using (var db = new Entities())
                {
                    db.InvoiceSubs.Add(oInvoiceSub);
                   int aff = db.SaveChanges();
                     Console.WriteLine(aff);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public DataSet SaleMedicineDataSet(int invoiceId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "spSalseMedicine";
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
            catch (Exception)
            {
                throw new Exception("Database error!! in DAL");
            }
            return ds;
        }

        public DataSet PurchaseMedicineDataSet(int inoviceId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                string cs = WebConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                string query = "spPurchaseMedicine";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InvoiceId", inoviceId);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    dt.TableName = "Command";
                    ds.Tables.Add(dt.Copy());
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Database error!! in DAL");
            }
            return ds;
        }

        public bool isMedicineExist(string name)
        {
            string medicineName = "";
            try
            {
                using (var db = new Entities())
                {
                    medicineName = db.Medicines.Where(p => p.Name == name).Select(p => p.Name).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return medicineName == name;
        }
    }
}