using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxControlToolkit;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.DAL
{
    public class AccountDAL
    {
        public User GetUserInfo(string username, string password)
        {
          
                using (var db  = new Entities())
                {
                    User user = null;
                        user = (db.Users.Where(u => u.username == username && u.Password == password)).FirstOrDefault();

                    if (user == null) throw  new Exception("Username or password doesn't match!");
                    
                    if((bool) !user.IsAuthorised) throw new Exception("Your account is not activated by Admin! Please contact with admin");

                    return user;
                }
           
        }

        public string GetUserNameByUserId(int userId)
        {
            try
            {
                using (var db = new Entities())
                {
                    return db.Users.Where(u => u.Id == userId).Select(u => u.Name).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DAL:"+ex.Message);
            }
        }
    }
}