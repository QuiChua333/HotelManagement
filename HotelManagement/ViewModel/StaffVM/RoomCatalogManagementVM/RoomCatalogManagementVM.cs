using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagement.ViewModel.StaffVM.RoomCatalogManagementVM
{
    public partial class RoomCatalogManagementVM:BaseVM
    {
        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { _SelectedDate = value;  OnPropertyChanged(); }
        }
        private DateTime _SelectedTime;
        public DateTime SelectedTime
        {
            get { return _SelectedTime; }
            set { _SelectedTime = value; OnPropertyChanged(); }
        }
        public RadioButton radioButtonRoomType { get; set; }  
        public RadioButton radioButtonRoomStatus { get; set; }  
        public RadioButton radioButtonRoomCleaningStatus { get; set; }

        private ObservableCollection<Object> _ListRoomType1;
        public ObservableCollection<Object> ListRoomType1
        {
            get { return _ListRoomType1; }
            set { _ListRoomType1 = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Object> _ListRoomType2;
        public ObservableCollection<Object> ListRoomType2
        {
            get { return _ListRoomType2; }
            set { _ListRoomType2 = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Object> _ListRoomType3;
        public ObservableCollection<Object> ListRoomType3
        {
            get { return _ListRoomType3; }
            set { _ListRoomType3 = value; OnPropertyChanged(); }
        }

        private Object _SelectedRoom;
        public Object SelectedRoom
        {
            get { return _SelectedRoom; }
            set { _SelectedRoom = value; OnPropertyChanged(); }
        }
        public ICommand FirstLoadCM { get; set; }
        public RoomCatalogManagementVM()
        {
            FirstLoadCM = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                SelectedDate = DateTime.Today;
                SelectedTime = DateTime.Now;
                radioButtonRoomType = (RadioButton)p.FindName("rdbAllRoomType");
                radioButtonRoomStatus = (RadioButton)p.FindName("rdbAllRoomStatus");
                radioButtonRoomCleaningStatus = (RadioButton)p.FindName("rdbAllRoomCleaningStatus");
            });
        }
    }
}
