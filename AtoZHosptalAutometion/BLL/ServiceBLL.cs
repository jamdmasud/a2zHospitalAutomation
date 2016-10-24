using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;
using AtoZHosptalAutometion.UI;

namespace AtoZHosptalAutometion.BLL
{
    public class ServiceBLL
    {
        public int SaveOutDoorService(ServiceDetails serviceDetails)
        {
            CoreDAL oCoreDal  = new CoreDAL();
            ServiceDAL oServiceDal = new ServiceDAL();
            Invoice oInvoice = new Invoice();
            Functions oFunctions = new Functions();
            InvoiceSub oInvoiceSub = new InvoiceSub();
            PatientDAL oPatientDal = new PatientDAL();
            List<Income> oIncomes = new List<Income>();
            AgentDAL oAgentDal = new AgentDAL();

            int saveAffect = 0, removeAffect = 0;
            string code = "";
            string agentCode = "";
            if (serviceDetails.PatientCode.Length > 12)
            {
                code = serviceDetails.PatientCode.Substring((serviceDetails.PatientCode.Length - 9), 8);
            }
            if (serviceDetails.AgentName.Length > 8)
            {
                agentCode = serviceDetails.AgentName.Substring(0, 7);
            }
            //Get Customer Id from customer Code
            int customerId = oPatientDal.GetCustomerIdByCode(code);
            //Save invoice 
            oInvoice.UserId = serviceDetails.UpdatedBy; //It will always be collected from session
            oInvoice.InvoiceType = "Outdoor Services";
            oInvoice.CustomerId = customerId;
            oInvoice.DoctorName = serviceDetails.Doctor;
            oInvoice.UpdatedBy = oInvoice.UserId;
            oInvoice.UpdatedDate = DateTime.Today;
            oInvoice.InvoiceDate = oInvoice.UpdatedDate;
            int invoiceId = oCoreDal.SaveInvoice(oInvoice);
            //Save Honourariam
            Honorarium oHonorarium = new Honorarium();
            oHonorarium.AgentId = oAgentDal.GetAgentIdFromCode(agentCode);
            oHonorarium.Honorarium1 = serviceDetails.Honouriam;
            oHonorarium.InvoiceId = invoiceId;
            oHonorarium.UpdatedBy = oInvoice.UserId;
            oHonorarium.UpdatedDate = DateTime.Today;
            oAgentDal.SaveOnorariam(oHonorarium);
            //Save vat in invoiceSub
            oInvoiceSub.Discount = serviceDetails.Discount;
            oInvoiceSub.Due = serviceDetails.Due;
            oInvoiceSub.GrandTotal = serviceDetails.GrandTotal;
            oInvoiceSub.Total = serviceDetails.Amount;
            oInvoiceSub.InvoiceId = invoiceId;
            oInvoiceSub.vat = serviceDetails.Vat;
            oInvoiceSub.Paid = serviceDetails.Paid;
            oInvoiceSub.TotalinWord = oFunctions.NumberToWord((int)oInvoiceSub.GrandTotal);
            oInvoiceSub.UpdatedDate = DateTime.Today;
            oInvoiceSub.UpdatedBy = oInvoice.UserId;
            oServiceDal.SaveOutDoorInvoiceSub(oInvoiceSub);
            //Save income
            List<IncomeTamp> oIncomeTamps = oServiceDal.GetIncomeFromTamp(oInvoice.UserId);
            foreach (IncomeTamp income in oIncomeTamps)
            {
                Income oIncome = new Income();
                oIncome.InvoiceId = invoiceId;
                oIncome.Particulars = income.Name;
                oIncome.ServiceType = oInvoice.InvoiceType;
                oIncome.Quantity = income.Quantity;
                oIncome.Rate = income.Rate;
                oIncome.Total = (income.Rate * income.Quantity);
                oIncome.UpdatedBy = oInvoice.UserId;
                oIncome.UpdatedDate = oInvoice.UpdatedDate;
                oIncomes.Add(oIncome);
            }
            saveAffect = oServiceDal.SaveOutDoorService(oIncomes);
            //remove incomeTamp
            removeAffect = oServiceDal.RemoveIncomTamp(oInvoice.UserId);
            bool result  = (saveAffect > 0 && removeAffect != 0);

            return invoiceId;
        }

        public int SaveIndoorServices(ServiceDetails serviceDetails)
        {
            CoreDAL oCoreDal = new CoreDAL();
            ServiceDAL oServiceDal = new ServiceDAL();
            Invoice oInvoice = new Invoice();
            InvoiceSub oInvoiceSub = new InvoiceSub();
            Functions oFunctions =new Functions();
            PatientDAL oPatientDal = new PatientDAL();
            PatientSub oPatient = new PatientSub();
            AgentDAL oAgentDal = new AgentDAL();
            List<Income> oIncomes = new List<Income>();
            int saveAffect = 0, removeAffect = 0;
            string code = "";
            string agentCode = "";
            if (serviceDetails.PatientCode.Length > 12)
            {
                code = serviceDetails.PatientCode.Substring((serviceDetails.PatientCode.Length - 9), 8);
            }
            if (serviceDetails.AgentName.Length > 12)
            {
                agentCode = serviceDetails.AgentName.Substring(0, 7);
            }
            //Get Customer Id from customer Code
            int customerId = oPatientDal.GetCustomerIdByCode(code);
            //Save invoice 
            oInvoice.UserId = serviceDetails.UpdatedBy; //It will always be collected from session
            oInvoice.InvoiceType = "Indoor Services";
            oInvoice.CustomerId = customerId;
            oInvoice.DoctorName = serviceDetails.Doctor;
            oInvoice.UpdatedBy = oInvoice.UserId;
            oInvoice.UpdatedDate = DateTime.Today;
            oInvoice.InvoiceDate = oInvoice.UpdatedDate;
            int invoiceId = oCoreDal.SaveInvoice(oInvoice);
            //Save discount
            //Save Honourariam
            Honorarium oHonorarium = new Honorarium();
            oHonorarium.AgentId = oAgentDal.GetAgentIdFromCode(agentCode);
            oHonorarium.Honorarium1 = serviceDetails.Honouriam;
            oHonorarium.InvoiceId = invoiceId;
            oHonorarium.UpdatedBy = oInvoice.UserId;
            oHonorarium.UpdatedDate = DateTime.Today;
            oAgentDal.SaveOnorariam(oHonorarium);

           

            //Save vat,due,discount in invoiceSub
            oInvoiceSub.Discount = serviceDetails.Discount;
            oInvoiceSub.Due = serviceDetails.Due;
            oInvoiceSub.GrandTotal = serviceDetails.GrandTotal;
            oInvoiceSub.Total = serviceDetails.Amount;
            oInvoiceSub.InvoiceId = invoiceId;
            oInvoiceSub.vat = serviceDetails.Vat;
            oInvoiceSub.Paid = serviceDetails.Paid;
            oInvoiceSub.TotalinWord = oFunctions.NumberToWord((int)oInvoiceSub.GrandTotal);
            oInvoiceSub.UpdatedDate = DateTime.Today;
            oInvoiceSub.UpdatedBy = oInvoice.UserId;
            oServiceDal.SaveOutDoorInvoiceSub(oInvoiceSub);
            //Save income
            List<IndoorIncomeTamp> oIncomeTamps = oServiceDal.GetIncomeFromIndoorTamp(oInvoice.UserId);
            foreach (IndoorIncomeTamp income in oIncomeTamps)
            {
                Income oIncome = new Income();
                oIncome.InvoiceId = invoiceId;
                oIncome.Particulars = income.Name;
                oIncome.ServiceType = oInvoice.InvoiceType;
                oIncome.Quantity = income.Quantity;
                oIncome.Rate = income.Rate;
                oIncome.Total = (income.Rate * income.Quantity);
                oIncome.UpdatedBy = oInvoice.UserId;
                oIncome.UpdatedDate = oInvoice.UpdatedDate;
                oIncomes.Add(oIncome);
            }
            saveAffect = oServiceDal.SaveInDoorService(oIncomes);
            //remove incomeTamp
            removeAffect = oServiceDal.RemoveIndoorIncomTamp(oInvoice.UserId);
            bool result = (saveAffect > 0 && removeAffect != 0);

            return invoiceId;
        }

        public DataSet GetOutdoorServiceData(int invoiceId)
        {
            ServiceDAL oServiceDal = new ServiceDAL();
            return oServiceDal.GetOutdoorServiceData(invoiceId);
        }
        public DataSet GetIndoorServiceData(int invoiceId)
        {
            ServiceDAL oServiceDal = new ServiceDAL();
            return oServiceDal.GetIndoorServiceData(invoiceId);
        }

        public int ChangeDeliveryStatus(int invoiceId, int status)
        {
            ServiceDAL oServiceDal = new ServiceDAL();
            return oServiceDal.ChangeDeliveryStatus(invoiceId, status);
        }

        public int UpdatePayment(int id, decimal paid, User oUser)
        {
            ServiceDAL oServiceDal = new ServiceDAL();
            oServiceDal.InsertDue(id, paid, oUser);
            return oServiceDal.UpdatePayment(id, paid);
        }

        public int AddService(Service oService)
        {
            ServiceDAL oServiceDal = new ServiceDAL();
            return oServiceDal.AddService(oService);
        }

        public int AddTests(Test oTest)
        {
            ServiceDAL oServiceDal = new ServiceDAL();
            return oServiceDal.AddTests(oTest);
        }
    }
}