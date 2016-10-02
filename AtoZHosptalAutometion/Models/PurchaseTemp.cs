using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtoZHosptalAutometion.Models
{
    [Serializable]
    public class PurchaseTemp
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}