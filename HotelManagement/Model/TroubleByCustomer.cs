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
    
    public partial class TroubleByCustomer
    {
        public int TroubleByCustomerId { get; set; }
        public string TroubleId { get; set; }
        public string RentalContractId { get; set; }
        public Nullable<double> PredictedPrice { get; set; }
    
        public virtual RentalContract RentalContract { get; set; }
        public virtual Trouble Trouble { get; set; }
    }
}
