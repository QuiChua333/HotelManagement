using HotelManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.StaffVM.RoomCatalogManagementVM
{
    public partial class RoomCatalogManagementVM : BaseVM
    {
        public string RoomCleaningStatusCleaned
        {
            get
            {
                return ROOM_CLEANING_STATUS.CLEANED;
            }
        }
        public string RoomCleaningStatusNotCleaningYet
        {
            get
            {
                return ROOM_CLEANING_STATUS.NOT_CLEANING_YET;
            }
        }
        public string RoomCleaningStatusRepairing
        {
            get
            {
                return ROOM_CLEANING_STATUS.REPAIRING;
            }
        }

        private int _PersonNumber;
        public int PersonNumber
        {
            get { return _PersonNumber; }
            set { _PersonNumber = value; OnPropertyChanged(); }
        }

        
    }
}
