using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using HotelManagement.View.BookingRoomManagement;
using HotelManagement.View.CustomMessageBoxWindow;
using HotelManagement.ViewModel.AdminVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagement.ViewModel.BookingRoomManagementVM
{
    public partial class BookingRoomManagementVM:BaseVM
    {
        public StaffDTO currentStaff;
       
        private string _staffName { get; set; }
        public string StaffName
        {
            set
            {
                _staffName = value;
                OnPropertyChanged();
            }
            get { return _staffName; }
        }

        private string _StaffId { get; set; }
        public string StaffId
        {
            set
            {
                _StaffId = value;
                OnPropertyChanged();
            }
            get { return _StaffId; }
        }


        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; OnPropertyChanged(); }
        }
        private string CustomerId;
        private bool IsSucceedSavingCustomer;


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

       

        private DateTime _DayOfBirth;
        public DateTime DayOfBirth
        {
            get { return _DayOfBirth; }
            set { _DayOfBirth = value; OnPropertyChanged(); }
        }

        private DateTime _CheckoutDate;
        public DateTime CheckoutDate
        {
            get { return _CheckoutDate; }
            set { _CheckoutDate = value; OnPropertyChanged(); }
        }

       

        private string _RentalContractId;
        public string RentalContractId
        {
            get { return _RentalContractId; }
            set { _RentalContractId = value; OnPropertyChanged(); }
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

        private ObservableCollection<RentalContractDTO> _BookingRoomList;
        public ObservableCollection<RentalContractDTO> BookingRoomList
        {
            get => _BookingRoomList;
            set
            {
                _BookingRoomList = value;
                OnPropertyChanged();
            }
        }
        
        private RoomDTO _SelectedRoom;
        public RoomDTO SelectedRoom
        {
            get => _SelectedRoom;
            set
            {
                _SelectedRoom = value;
                OnPropertyChanged();
            }
        }

        private RentalContractDTO _SelectedItem;
        public RentalContractDTO SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }


        public ICommand CloseCM { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadBookingCM { get; set; }
        public ICommand ConfirmBookingRoomCM { get; set; }
        
        public BookingRoomManagementVM() 
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            FirstLoadCM = new RelayCommand<Window>((p) => { return true; },async (p) =>
            {
                StartDate = DateTime.Today;
                CheckoutDate = DateTime.Today.AddDays(1);
                StartTime = DateTime.Now;
                DayOfBirth = DateTime.Now;
                SelectedRoom = null;


                //currentStaff = AdminVM.AdminVM.CurrentStaff;
                //StaffName = currentStaff.StaffName;
                //StaffId = currentStaff.StaffId;

                IsSucceedSavingCustomer = false;
                await  LoadReadyRoom();

                await LoadBookingRoom();

            });

            LoadBookingCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
               Booking booking = new Booking();
               booking.ShowDialog();
            });

            ConfirmBookingRoomCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                (bool isvalid, string error) = ValidateBooking();
                if (isvalid)
                {
                    await SaveCustomer();
                    if (!IsSucceedSavingCustomer)
                    {
                        return;
                    }
                    await SaveRentalContract(p);
                }
                else
                {
                    CustomMessageBox.ShowOk(error, "Cảnh báo", "OK", CustomMessageBoxImage.Warning);
                }
            });

            CloseCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
        public async Task LoadBookingRoom()
        {
            BookingRoomList = new ObservableCollection<RentalContractDTO>(await BookingRoomService.Ins.GetBookingList());
        }
    }
}
