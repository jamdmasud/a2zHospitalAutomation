using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtoZHosptalAutometion.Models
{
    public class VmMedicineHistory
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string EmployeeName { get; set; }
        public decimal Sold { get; set; }
        public decimal Purchased { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Category { get; set; }
    }
}