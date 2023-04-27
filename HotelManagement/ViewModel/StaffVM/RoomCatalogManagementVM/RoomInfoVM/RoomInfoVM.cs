using HotelManagement.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagement.ViewModel.StaffVM.RoomCatalogManagementVM
{
    public partial class RoomCatalogManagementVM : BaseVM
    {
       

        private ComboBoxItem _SelectedRoomCleaningStatus;
        public ComboBoxItem SelectedRoomCleaningStatus
        {
            get { return _SelectedRoomCleaningStatus; }
            set { _SelectedRoomCleaningStatus = value; OnPropertyChanged(); }
        }


    }
}
