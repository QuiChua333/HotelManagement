using CinemaManagementProject.Utilities;
using HotelManagement.ViewModel.AdminVM.FurnitureManagementVM;
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
            set { _currentView = value; }
        }
        public ICommand FirstLoadCM { get; set; }
        public StaffVM()
        {

        }
    }
}
