using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtoZHosptalAutometion.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Roles { get; set; }     
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}