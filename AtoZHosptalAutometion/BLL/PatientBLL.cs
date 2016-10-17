using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.BLL
{
    public class PatientBLL
    {
        public int GetDoctorIdFromCode(string doctorCode)
        {
            PatientDAL oPatientDal = new PatientDAL();
            int flag = 0;
            if (oPatientDal.IsDoctorExist(doctorCode))
            {
                //return  doctor ID
                flag = oPatientDal.GetDoctorIdFromCode(doctorCode);
            }
            return flag;
        }

        public int? GetAgentIdFromCode(string agentCode)
        {
            PatientDAL oPatientDal = new PatientDAL();
            int flag = 0;
            if (oPatientDal.IsAgentExist(agentCode))
            {
                //return  doctor ID
                flag = oPatientDal.GetAgentIdFromCode(agentCode);
            }
            return flag;
        }

        public bool Register(Patient oPatient)
        {
            PatientDAL  oPatientDal = new PatientDAL();
            string code = oPatientDal.GetLastCode();
            oPatient.Code = GeneratedCode(code);
            oPatient.UpdatedDate = DateTime.Now;
            return oPatientDal.Register(oPatient);
        }

        private string GeneratedCode(string code)
        {
            int partOne = Convert.ToInt32(code.Substring(2)) + 1;
            string strPart = code.Substring(0, 2);
            return strPart + partOne;
        }

        public int GetCustomerIdByCode(string code)
        {
            PatientDAL oPatientDal = new PatientDAL();
            return oPatientDal.GetCustomerIdByCode(code);
        }

        public int Admit(PatientSub oPatientSub)
        {
            PatientDAL oPatientDal = new PatientDAL();
            CoreDAL oCoreDal = new CoreDAL();
            Invoice oInvoice = new Invoice();
            Patient oPatient= new Patient();


            oInvoice.InvoiceDate = DateTime.Today;
            oInvoice.UserId = oPatientSub.UpdatedBy; // it will be collected from session
            oInvoice.UpdatedBy = oInvoice.UserId;
            oInvoice.InvoiceType = "Admission";
            oInvoice.UpdatedDate = oInvoice.InvoiceDate;
            oInvoice.Status = "pending";
            int invoiceId = oCoreDal.SaveInvoice(oInvoice);

            oPatient.AddmissionDate = oPatientSub.AddmissionDate;
            oPatient.Id = (int) oPatientSub.PatientId;
            oPatient.RefencedBy = Convert.ToInt32(oPatientSub.DoctorId);
            oPatientDal.UpdateAdmissionDate(oPatient);

            // add invoiceId
            oPatientSub.InvoiceId = invoiceId;
            oPatientSub.UpdatedBy = oInvoice.UpdatedBy;
            oPatientSub.UpdatedDate = DateTime.Now;
            oPatientDal.Admit(oPatientSub);
            return invoiceId;
        }
    }
}