using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DTOs
{
    public class BillDTO
    {
        public string BillId { get; set; }
        public string RentalContractId { get; set; }
        public Nullable<int> NumberOfRentalDays { get; set; }
        public Nullable<double> ServicePrice { get; set; }
        public Nullable<double> TroublePrice { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<double> Price { get; set; }
        
    }
}
