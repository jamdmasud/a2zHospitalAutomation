using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtoZHosptalAutometion.Models
{
    public class MedicineDetails
    {
        public string GroupName { get; set; }
        public string Volume { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? UpdatedBy { get; set; }  
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}