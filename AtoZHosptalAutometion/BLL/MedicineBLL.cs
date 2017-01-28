using System;
using System.Collections.Generic;
using System.Data;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;
using AtoZHosptalAutometion.UI;

namespace AtoZHosptalAutometion.BLL
{
    public class MedicineBLL
    {
        public bool Save(Medicine medicine, MedicineDetails pMedicineDetails)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
             medicine.UpdatedDate = DateTime.Now;
            string code = oMedicineDal.GetLastCode();
            medicine.Code = GeneratedCode(code);
            if (oMedicineDal.isMedicineExist(medicine.Name))
            {
                throw new Exception("This medicine already exist in database!");
            }
            if (!oMedicineDal.IsGroupExist(pMedicineDetails.GroupName))
            {
                Group group = new Group();
                group.Name = pMedicineDetails.GroupName;
                group.UpdatedBy = pMedicineDetails.UpdatedBy;
                group.UpdatedDate = DateTime.Now;
                medicine.GroupId = oMedicineDal.SaveGroup(group);
            }
            if (!oMedicineDal.IsCompanyExist(pMedicineDetails.CompanyName))
            {
                Company company = new Company();
                company.Name = pMedicineDetails.CompanyName;
                company.UpdatedBy = pMedicineDetails.UpdatedBy;
                company.UpdatedDate = DateTime.Now;

                medicine.CompanyId = oMedicineDal.SaveCompany(company);
            }
            return oMedicineDal.Save(medicine);
        }

        private string GeneratedCode(string code)
        {
            int partOne = Convert.ToInt32(code.Substring(6)) + 1;
            string strPart = code.Substring(0, 6);
            return strPart + partOne;
        }

        public int? GetGroupId(string group)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            return oMedicineDal.GetGroupId(group);
        }

        public int? GetCompanyId(string name)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            return oMedicineDal.GetCompanyId(name);
        }

        public int GetMedicineIdByName(string medicineName)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            return oMedicineDal.GetMedicineIdByName(medicineName);
        }

        public void SaveToTemp(PurchaseTemp temp)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            oMedicineDal.SaveToTemp(temp);
        }

        public int SavePurchaseMedicine(string purchasingDate, int total, decimal gTotal, decimal discount, string word, int userId)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            CoreDAL oCoreDal = new CoreDAL();
            List<MedicinePurchaseTemp> purchasesTemp = oMedicineDal.GetMedicineFromPurchaseTemp(userId);
            List<Purchase> purchases = new List<Purchase>();
            Invoice oInvoice = new Invoice();
            InvoiceSub oInvoiceSub = new InvoiceSub();

            // Save into    Invoice description 
            oInvoice.InvoiceDate = DateTime.Today;
            oInvoice.UserId = userId; // it will be collected from session
            oInvoice.UpdatedBy = oInvoice.UserId;
            oInvoice.InvoiceType = "Purchase Medicine";
            oInvoice.UpdatedDate = oInvoice.InvoiceDate;
            oInvoice.Status = "pending";
            int invoiceId = oCoreDal.SaveInvoice(oInvoice);

            //Save into invoiceSub for description
            oInvoiceSub.InvoiceId = invoiceId;
            oInvoiceSub.Total = total;
            oInvoiceSub.GrandTotal = gTotal;

            oInvoiceSub.TotalinWord = word;
            oInvoiceSub.UpdatedDate =  DateTime.Today;
            oInvoiceSub.UpdatedBy = oInvoice.UpdatedBy;
            oInvoiceSub.Discount = discount;
            oInvoiceSub.Paid = 0;
            oInvoiceSub.Due = 0;
            oInvoiceSub.vat = 0;
            oMedicineDal.SavePurchaseMedicineToInvoiceSub(oInvoiceSub);
            decimal totalPurchase = 0;
            foreach (var item in purchasesTemp)
            {
                Purchase oPurchase = new Purchase();
                oPurchase.MedicineId = Convert.ToInt32(oMedicineDal.GetMedicineIdByName(item.MedicineName));
                oPurchase.Price = item.Price;
                oPurchase.Quantity = Convert.ToInt32(item.Quantity);
                oPurchase.Total = item.Total;
                totalPurchase += item.Total ?? 0;
                oPurchase.CustomerId = userId; // it has been collected from session
                oPurchase.UpdatedDate = DateTime.Today;
                oPurchase.UpdatedBy = oPurchase.CustomerId;
                oPurchase.PurchasingDate = Convert.ToDateTime(purchasingDate);
                oPurchase.InvoiceId = invoiceId;
                oPurchase.EmployeeId = oPurchase.UpdatedBy;
                purchases.Add(oPurchase);
            }

            //SaveExpenseForMedicine(purchasingDate, userId, totalPurchase, invoiceId);

            int affected = oMedicineDal.SavePurchaseMedicine(purchases);

            oMedicineDal.RemoveTemp(oInvoice.UserId);
            oInvoice = null;
            oInvoiceSub = null;
            purchases = null;
            purchasesTemp = null;
            return affected > 0 ? invoiceId : 0 ;
        }

        private static void SaveExpenseForMedicine(string purchasingDate, int userId, decimal totalPurchase, int invoiceId)
        {
            Expens oExpens = new Expens();
            List<Expens> listExpenses = new List<Expens>();
            ExpenseDAL oExpenseBll = new ExpenseDAL();

            oExpens.Amount = totalPurchase;
            oExpens.Description = "Purchase Medicine";
            oExpens.ExpenseType = "Pharmacy";
            oExpens.InvoiceId = invoiceId;
            oExpens.UpdatedBy = userId;
            oExpens.UpdatedDate = DateTime.Today;
            oExpens.ExpenseDate = Convert.ToDateTime(purchasingDate);
            listExpenses.Add(oExpens);
            oExpenseBll.SaveExapense(listExpenses);
        }

        public bool SaveToTempSale(SalesTemp temp)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            return oMedicineDal.SaveToTempSale(temp);

        }

        public int SaveSallingMedicine(SaleSave oSaleSave)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            CoreDAL oCoreDal = new CoreDAL();
            List<SalesTemp> salesTemp = oMedicineDal.GetMedicineFromSalesTemp(oSaleSave.UserId);
            List<Sale> oSales = new List<Sale>();
            Invoice oInvoice = new Invoice();
            InvoiceSub oInvoiceSub = new InvoiceSub();
            PatientBLL oPatientBll = new PatientBLL();
            string customentId = "";
            if (oSaleSave.Customer.Length > 12)
            {
                customentId = oSaleSave.Customer.Substring((oSaleSave.Customer.Length - 8), 8);
            }
            oInvoice.InvoiceDate = DateTime.Today;
            oInvoice.UserId = oSaleSave.UserId; // it will be collected from session
            oInvoice.UpdatedBy = oInvoice.UserId;
            oInvoice.CustomerId = Convert.ToInt32(oPatientBll.GetCustomerIdByCode(customentId));
            oInvoice.InvoiceType = "Sales Medicine";
            oInvoice.UpdatedDate = oInvoice.InvoiceDate;
            oInvoice.Status = "pending";
            int invoiceId = oCoreDal.SaveInvoice(oInvoice);


            //Saving invoicesub
            oInvoiceSub.InvoiceId = invoiceId;
            oInvoiceSub.Total = oSaleSave.Total;
            oInvoiceSub.GrandTotal = oSaleSave.GrandTotal;
            oInvoiceSub.TotalinWord = oSaleSave.AmountInword;
            oInvoiceSub.UpdatedDate = DateTime.Now;
            oInvoiceSub.UpdatedBy = oInvoice.UpdatedBy;
            oInvoiceSub.Discount = oSaleSave.Discount;
            oInvoiceSub.Paid = oSaleSave.Advanced;
            oInvoiceSub.Due = oSaleSave.Due;
            oInvoiceSub.vat = oSaleSave.Vat;
            oMedicineDal.SavePurchaseMedicineToInvoiceSub(oInvoiceSub);


            foreach (var item in salesTemp)
            {
                Sale oSale = new Sale();
                oSale.MedicineId = Convert.ToInt32(oMedicineDal.GetMedicineIdByName(item.MedicineName));
                oSale.UnitPrice = item.Price;
                oSale.Quantity = Convert.ToInt32(item.Quantity);
                oSale.Total = item.Total;
                oSale.CustomerId = oMedicineDal.GetMedicineIdByName(item.MedicineName);
                oSale.PatientType = oSaleSave.CustomerType;
                oSale.UpdatedDate = DateTime.Today;
                oSale.UpdatedBy = oSaleSave.UserId; // it will be collected from session
                oSale.SalingDate = Convert.ToDateTime(oSaleSave.SaleDate);
                oSale.InvoiceId = invoiceId;
                oSale.EmployeeId = oSale.UpdatedBy;
                oSales.Add(oSale);
            }

            int affected = oMedicineDal.SaveSalesMedicine(oSales);

            oMedicineDal.RemoveSalesTemp(oSaleSave.UserId);
            oSaleSave = null;
            salesTemp = null;
            oInvoice = null;
            oInvoiceSub = null;
            return affected > 0 ? invoiceId : 0;
        }

        public string GetMedicineNameById(int? medicineId)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            return oMedicineDal.GetMedicineNameById(medicineId);
        }

        public List<SalesTemp> GetSalesMedicineFromTemp()
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            return oMedicineDal.GetSalesMedicineFromTemp();
        }

        public void DeleteMedicine(int id)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            oMedicineDal.DeleteMedicine(id);
        }

        public DataSet SaleMedicineDataSet(int invoiceId)
        {
            MedicineDAL oMedicineDal  = new MedicineDAL();
            return oMedicineDal.SaleMedicineDataSet(invoiceId);
        }


        public DataSet PurchaseMedicineDataSet(int inoviceId)
        {
            MedicineDAL oMedicineDal = new MedicineDAL();
            return oMedicineDal.PurchaseMedicineDataSet(inoviceId);
        }
    }
}