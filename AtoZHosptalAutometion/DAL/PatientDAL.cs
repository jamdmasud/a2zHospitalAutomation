using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.DAL
{
    public class PatientDAL
    {
         
        public bool IsDoctorExist(string code)
        {
           int doctors = 0;
            try
            {
                using (var db = new Entities())
                {
                      doctors = db.Doctors.Where(d => d.Code == code).Select(d => d.Id).FirstOrDefault();
                }
              
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return doctors != 0;
        }

        public bool IsAgentExist(string code)
        {
            try
            {
                int agent = 0;
                using (var db = new Entities())
                {
                    agent = db.Agents.Where(d => d.Code == code).Select(d => d.Id).FirstOrDefault();
                }
                
                return agent != 0;
            }
            catch (Exception EX_NAME)
            {
                throw new Exception(EX_NAME.InnerException.Message);
            }
        }

        public int GetDoctorIdFromCode(string code)
        {
            try
            {
                int Id = 0;
                using (var db = new Entities())
                {
                    Id = db.Doctors.Where(d => d.Code == code).Select(d => d.Id).FirstOrDefault();
                }
                return Id;
            }
            catch (Exception)
            {
                throw new Exception("Sorry, we can't get that user Id!");
            }
        }
        


        public int GetAgentIdFromCode(string code)
        {
            try
            {
                int Id = 0;
                using (var db = new Entities())
                {
                    Id = db.Agents.Where(a => a.Code == code).Select(a => a.Id).FirstOrDefault();
                }
                return Id;
            }
            catch (Exception)
            {
                throw new Exception("Sorry, we can't get that user Id!");
            }
        }

        

        public bool Register(Patient oPatient)
        {
            int affected = -1;
            try
            {
                using (var db = new Entities())
                {
                    db.Patients.Add(oPatient);
                    affected = db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return affected > 0;
        }

        public string GetLastCode()
        {
            try
            {
                string code = "";
                using (var db = new Entities())
                {
                    code = db.Patients.ToList().OrderByDescending(p => p.Id).Select(p => p.Code).FirstOrDefault();
                }
                return code;
            }
            catch (Exception)
            {
                throw new Exception("Sorry, we can't get that user Code!");
            }
        }
        

        public int GetCustomerIdByCode(string code)
        {
            try
            {
                int id = 0;
                using (var db = new Entities())
                {
                    id = db.Patients.Where(p => p.Code == code).Select(p => p.Id).FirstOrDefault();
                }
                return id;
            }
            catch (Exception)
            {
                throw new Exception("Sorry, we can't get that user ID!");
            }
        }


        public void SaveDoctorNameToPatientSub(PatientSub oPatient)
        {
            try
            {
               
                using (var db = new Entities())
                {
                    db.PatientSubs.Add(oPatient);
                    db.SaveChanges();
                }
              
            }
            catch (Exception)
            {
                throw new Exception("Sorry, we can't save your doctor!");
            }
        }

        public int Admit(PatientSub oPatientSub)
        {
            try
            {
                int affected = 0;
                using (var db = new Entities())
                {
                    db.PatientSubs.Add(oPatientSub);
                    affected = db.SaveChanges();
                }
                return affected;
            }
            catch (Exception ex)
            {
                throw new Exception("Sorry, we can't save your Patient sub!"+ex);
            }
        }

        public void UpdateAdmissionDate(Patient oPatient)
        {
            try
            {
                using (var db = new Entities())
                {
                    Patient patient = db.Patients.Find(oPatient.Id);
                    patient.AddmissionDate = oPatient.AddmissionDate;
                    patient.RefencedBy = oPatient.RefencedBy;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Sorry, we can't save your Patient sub!");
            }
        }
    }
}