using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.BLL
{
    public class AccountBll
    {
        public User GetUserInfo(string username, string password)
        {
            AccountDAL oAccountDal = new AccountDAL();
            return oAccountDal.GetUserInfo(username, password);
        }

        public string GetUserNameByUserId(int userId)
        {
            AccountDAL oAccountDal = new AccountDAL();
            return oAccountDal.GetUserNameByUserId(userId);
        }
    }
}