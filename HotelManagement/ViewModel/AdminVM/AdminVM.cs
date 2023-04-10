using CinemaManagementProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.AdminVM
{
    public class AdminVM : BaseVM
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; }
        }
        public ICommand FirstLoadCM { get; set; }
        private void Furniture(object obj) => CurrentView = new FurnitureManagementVM.FurnitureManagementVM();
        
        public ICommand FurnitureCommand { get; set; }
        public AdminVM()
        {
            _currentView = new FurnitureManagementVM.FurnitureManagementVM();

            FurnitureCommand = new RelayCommand(Furniture);
        }

    }
}
