using CinemaManagementProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.StaffVM
{
    public class StaffVM : BaseVM
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private void RoomCatalog(object obj) => CurrentView = new RoomCatalogManagementVM.RoomCatalogManagementVM();
        private void TroubleRp(object obj) => CurrentView = new TroubleReportVM.TroubleReportVM();

        public ICommand FirstLoadCM { get; set; }
        public ICommand RoomCatalogCommand { get; set; }
        public ICommand TroubleRpCommand { get; set; }

        public StaffVM()
        {
            _currentView = new RoomCatalogManagementVM.RoomCatalogManagementVM();
            RoomCatalogCommand = new RelayCommand(RoomCatalog);
            TroubleRpCommand = new RelayCommand(TroubleRp);

        }
    }
}
