using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ViewModel.AdminVM.TroubleManagement
{
    public class TroubleManagementVM
    {
        public class Trouble
        {
            public string Image { get; set; }

            public string TroubleTitle { get; set; }
            public string TroubleDescription { get; set; }
            public float RepairCost { get; set; }
            public DateTime ReportDate { get; set; }
            public DateTime RepairDate { get; set; }
            public DateTime FinishDate { get; set; }


            public string TroubleReason { get; set; }
            public string TroubleLevel { get; set; }
            public string TroubleStatus { get; set; }
            public string StaffName { get; set; }

            
        }
    }
}
