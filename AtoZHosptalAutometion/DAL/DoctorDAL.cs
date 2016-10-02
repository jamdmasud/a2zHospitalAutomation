using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.DAL
{
    public class DoctorDAL
    {

        public string GetLastCode()
        {
            try
            {
                using (var db = new Entities())
                {
                  return  db.Doctors.ToList().OrderByDescending(d => d.Id).Select(p => p.Code).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }


        public bool Register(Doctor oDoctor)
        {
            int affected = 0;
            try
            {
                using (var db = new Entities())
                {
                    db.Doctors.Add(oDoctor);
                    affected = db.SaveChanges();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return affected != 0;
        }
    }
}