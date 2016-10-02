using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.BLL
{
    public class DoctorBLL
    {
        public string Register(Doctor oDoctor)
        {
            Functions functions = new Functions();
            DoctorDAL oDoctorDal = new DoctorDAL();

            oDoctor.UpdatedDate = DateTime.Now;
            string lastCode = oDoctorDal.GetLastCode();
            oDoctor.Code = functions.GeneratedCode(lastCode, 7);
            string agentCode = null;
            if (oDoctorDal.Register(oDoctor))
            {
                agentCode = oDoctorDal.GetLastCode();
            }
            return agentCode;
        }
    }

}