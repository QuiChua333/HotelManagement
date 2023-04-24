using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DTOs
{
    public class RentalContractDTO
    {
        public string RentalContractId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public string StaffId { get; set; }
        public string CustomerId { get; set; }
        public string RoomId { get; set; }
        public Nullable<bool> Validated { get; set; }
    }
}
