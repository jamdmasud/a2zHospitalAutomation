//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AtoZHosptalAutometion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string fatherOhusbandName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string Religion { get; set; }
        public string presentAddress { get; set; }
        public string PermenantAddress { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> RefencedBy { get; set; }
        public Nullable<System.DateTime> AddmissionDate { get; set; }
        public Nullable<int> AgentsId { get; set; }
        public Nullable<System.DateTime> ReleasedDate { get; set; }
        public string Age { get; set; }
    }
}
