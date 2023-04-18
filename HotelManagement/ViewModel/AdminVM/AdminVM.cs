using CinemaManagementProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement.ViewModel.AdminVM
{
    public class AdminVM : BaseVM
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public ICommand FirstLoadCM { get; set; }
        private void Furniture(object obj) => CurrentView = new FurnitureManagementVM.FurnitureManagementVM();
        private void Service(object obj) => CurrentView = new ServiceManagementVM.ServiceManagementVM();
        private void RoomFurniture(object obj) => CurrentView = new RoomFurnitureManagementVM.RoomFurnitureManagementVM();
        private void Setting(object obj) => CurrentView = new SettingVM.SettingVM();


        public ICommand FurnitureCommand { get; set; }
        public ICommand ServiceCommand { get; set; }
        public ICommand RoomFurnitureCommand { get; set; }
        public ICommand SettingCommand { get; set; }

        public AdminVM()
        {
            _currentView = new ServiceManagementVM.ServiceManagementVM();

            FurnitureCommand = new RelayCommand(Furniture);
            ServiceCommand = new RelayCommand(Service);
            RoomFurnitureCommand = new RelayCommand(RoomFurniture);
            SettingCommand = new RelayCommand(Setting);
        }
    }
}
