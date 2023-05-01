using HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace HotelManagement.ViewModel.BookingRoomManagementVM
{
    public partial class BookingRoomManagementVM : BaseVM
    {
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; OnPropertyChanged(); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }
        
        private string _CCCD;
        public string CCCD
        {
            get { return _CCCD; }
            set { _CCCD = value; OnPropertyChanged(); }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged(); }
        }
        
        private ComboBoxItem _Gender;
        public ComboBoxItem Gender
        {
            get { return _Gender; }
            set { _Gender = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _CustomerType;
        public ComboBoxItem CustomerType
        {
            get { return _CustomerType; }
            set { _CustomerType = value; OnPropertyChanged(); }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; OnPropertyChanged(); }
        }

        private DateTime _CheckoutDate;
        public DateTime CheckoutDate
        {
            get { return _CheckoutDate; }
            set { _CheckoutDate = value; OnPropertyChanged(); }
        }

        private DateTime _StartTime;
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _PersonNumber;
        public ComboBoxItem PersonNumber
        {
            get { return _PersonNumber; }
            set { _PersonNumber = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RoomDTO> _ListReadyRoom;
        public ObservableCollection<RoomDTO> ListReadyRoom
        {
            get => _ListReadyRoom;
            set
            {
                _ListReadyRoom = value;
                OnPropertyChanged();
            }
        }

    }
}
