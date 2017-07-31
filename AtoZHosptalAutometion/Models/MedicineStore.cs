using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtoZHosptalAutometion.Models
{
    public class MedicineStore
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string GroupName { get; set; }
        public string Company { get; set; }
        public int Balance { get; set; }
        public int OpeningQuantity { get; set; }
        public int PurchasingQty { get; set; }
        public int SalingQty { get; set; }  
    }
}