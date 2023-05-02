using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using HotelManagement.View.CustomMessageBoxWindow;
using HotelManagement.View.Staff.RoomCatalogManagement.RoomOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.ViewModel.StaffVM.RoomCatalogManagementVM
{
    public partial class RoomCatalogManagementVM : BaseVM
    {
        private ServiceDTO _CleaningServices;
        public ServiceDTO CleaningService
        {
            get { return _CleaningServices; }
            set { _CleaningServices = value; OnPropertyChanged(); }
        }
        private ServiceDTO _LaundryService;
        public ServiceDTO LaundryService
        {
            get { return _LaundryService; }
            set { _LaundryService = value; OnPropertyChanged(); }
        }

        public async Task SaveUsingCleaningService(Window p)
        {
            ServiceUsingDTO serviceUsingDTO = new ServiceUsingDTO
            {
                 RentalContractId = SelectedRoom.RentalContractId,
                 ServiceId = CleaningService.ServiceId,
                 UnitPrice = CleaningService.ServicePrice,
                 Quantity = 1,
            };
            (bool isSucceed, string message) = await ServiceUsingHelper.Ins.SaveService(serviceUsingDTO);
            if (isSucceed)
            {
                CustomMessageBox.ShowOk(message, "Thông báo", "Ok", CustomMessageBoxImage.Success);
                p.Close();
            }
            else
            {
                CustomMessageBox.ShowOk(message, "Thông báo", "Ok", CustomMessageBoxImage.Error);

            }
        }
        public async Task SaveUsingLaundryService(RoomOrderLaundry p)
        {
            ServiceUsingDTO serviceUsingDTO = new ServiceUsingDTO
            {
                RentalContractId = SelectedRoom.RentalContractId,
                ServiceId = LaundryService.ServiceId,
                UnitPrice = LaundryService.ServicePrice,
                Quantity = int.Parse(p.tbKg.Text),
            };
            (bool isSucceed, string message) = await ServiceUsingHelper.Ins.SaveService(serviceUsingDTO);
            if (isSucceed)
            {
                CustomMessageBox.ShowOk(message, "Thông báo", "Ok", CustomMessageBoxImage.Success);
                p.Close();
            }
            else
            {
                CustomMessageBox.ShowOk(message, "Thông báo", "Ok", CustomMessageBoxImage.Error);

            }
        }
    }
}
