//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public string BillId { get; set; }
        public string RentalContractId { get; set; }
        public string StaffId { get; set; }
        public Nullable<int> NumberOfRentalDays { get; set; }
        public Nullable<double> ServicePrice { get; set; }
        public Nullable<double> TroublePrice { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        public virtual RentalContract RentalContract { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
