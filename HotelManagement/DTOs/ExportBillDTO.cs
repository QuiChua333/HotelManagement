using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DTOs
{
    public class ExportBillDTO
    {
        public string BillId { get; set; }
        public string CusName { get; set; }
        public string PhoneNum { get; set; }
        public float Price { get; set; }
        public string StaffName { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
