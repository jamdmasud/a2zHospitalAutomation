using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.BLL
{
    public class ExpenseBLL
    {
        public int SaveExapense(int userId)
        {

            ExpenseDAL oExpenseDal = new ExpenseDAL();
            CoreDAL oCoreDal = new CoreDAL();
            Invoice oInvoice = new Invoice();
            List<Expens> oExpenses = new List<Expens>();


            oInvoice.InvoiceDate = DateTime.Today;
            oInvoice.UserId = userId; // it will be collected from session
            oInvoice.UpdatedBy = oInvoice.UserId;
            oInvoice.InvoiceType = "Expense";
            oInvoice.UpdatedDate = oInvoice.InvoiceDate;
            oInvoice.Status = "pending";
            int invoiceId = oCoreDal.SaveInvoice(oInvoice);
            List<ExpenseTamp> oExpenseTamps = oExpenseDal.GetExpenseFromTemp(oInvoice.UserId);
            foreach (ExpenseTamp expenseTamp in oExpenseTamps)
            {
                Expens expens = new Expens();
                expens.Description = expenseTamp.Description;
                expens.Amount = expenseTamp.Amount;
                expens.ExpenseType = expenseTamp.ExpenseType;
                expens.InvoiceId = invoiceId;
                expens.UpdatedBy = oInvoice.UpdatedBy;
                expens.UpdatedDate = oInvoice.UpdatedDate;
                expens.ExpenseDate = expenseTamp.ExpenseDate;
                oExpenses.Add(expens);
            }
            int affected = oExpenseDal.SaveExapense(oExpenses);
            if(affected>0)RemoveExpensesTamp(userId);
            return affected;
        }

        public void RemoveExpensesTamp(int userId)
        {
            ExpenseDAL oExpenseDal = new ExpenseDAL();
            oExpenseDal.RemoveExpensesTamp(userId);
        }

        public DataSet ShowExpense(DateTime fromDate, DateTime tomDate, string type)
        {
            ExpenseDAL oExpenseDal = new ExpenseDAL();
            return oExpenseDal.ShowExpense(fromDate, tomDate, type);
        }
        public DataSet ShowDeposit(DateTime fromDate, DateTime tomDate)
        {
            ExpenseDAL oExpenseDal = new ExpenseDAL();
            return oExpenseDal.ShowDeposit(fromDate, tomDate);
        }
    }
}