using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HotelManagement.ViewModel.AdminVM.StatisticalManagementVM
{
    public partial class StatisticalManagementVM : BaseVM
    {
        private List<RoomTypeDTO> _ListRoomTypeRevenue;
        public List<RoomTypeDTO> ListRoomTypeRevenue
        {
            get { return _ListRoomTypeRevenue; }
            set { _ListRoomTypeRevenue = value; OnPropertyChanged(); }
        }
        private ComboBoxItem _SelectedPeriod;
        public ComboBoxItem SelectedPeriod
        {
            get { return _SelectedPeriod; }
            set { _SelectedPeriod = value; OnPropertyChanged(); }
        }
        private string _SelectedTime;
        public string SelectedTime
        {
            get { return _SelectedTime; }
            set { _SelectedTime = value; OnPropertyChanged(); }
        }
        private ComboBoxItem _SelectedPeriod2;
        public ComboBoxItem SelectedPeriod2
        {
            get { return _SelectedPeriod2; }
            set { _SelectedPeriod2 = value; OnPropertyChanged(); }
        }
        private string _SelectedTime2;
        public string SelectedTime2
        {
            get { return _SelectedTime2; }
            set { _SelectedTime2 = value; OnPropertyChanged(); }
        }
        private List<ServiceTypeDTO> _ListServiceTypeRevenue;
        public List<ServiceTypeDTO> ListServiceTypeRevenue
        {
            get { return _ListServiceTypeRevenue; }
            set { _ListServiceTypeRevenue = value; OnPropertyChanged(); }
        }
        public async Task ChangeRoomTypeRevenue()
        {
            ListRoomTypeRevenue = await OverviewStatisticService.Ins.GetListRoomTypeRevenue(SelectedPeriod.Content.ToString(), SelectedTime);
        }
        
            public async Task ChangeServiceTypeRevenue()
        {
            ListServiceTypeRevenue = await OverviewStatisticService.Ins.GetListServiceTypeRevenue(SelectedPeriod2.Content.ToString(), SelectedTime2);
        }
    }
}
