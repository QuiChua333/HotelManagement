using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DTOs
{
    public class ServiceUsingDTO
    {
        public int ServiceUsingId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
        public string RentalContractId { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}
