using CinemaManagementProject.Utilities;
using HotelManagement.View.Admin.StatisticalManagement;
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
        private void BookingRoom(object obj) => CurrentView = new BookingRoomManagementVM.BookingRoomManagementVM();
        private void Room(object obj) => CurrentView = new RoomManagementVM.RoomManagementVM();
        private void RoomType(object obj) => CurrentView = new RoomTypeManagementVM.RoomTypeManagementVM();
        private void Statiscal(object obj) => CurrentView = new StatisticalManagementVM.StatisticalManagementVM();
        private void HelpScreen(object obj) => CurrentView = new HelpScreenVM.HelpScreenVM();
        public ICommand FurnitureCommand { get; set; }
        public ICommand ServiceCommand { get; set; }
        public ICommand BookingRoomCommand { get; set; }
        public ICommand RoomCommand { get; set; }
        public ICommand RoomTypeCommand { get; set; }
        public ICommand StatiscalCommand { get; set; }
        public ICommand HelpScreenCommand { get; set; }

        
        public AdminVM()
        {
            _currentView = new ServiceManagementVM.ServiceManagementVM(); 

            FurnitureCommand = new RelayCommand(Furniture);
            ServiceCommand = new RelayCommand(Service);
            BookingRoomCommand = new RelayCommand(BookingRoom);
            RoomCommand = new RelayCommand(Room);
            RoomTypeCommand = new RelayCommand(RoomType);
            StatiscalCommand=new RelayCommand(Statiscal);
            HelpScreenCommand = new RelayCommand(HelpScreen);
        }
    }
}
