using HotelManagement.DTOs;
using HotelManagement.Model;
using HotelManagement.Model.Services;
using HotelManagement.Utils;
using HotelManagement.View.CustomMessageBoxWindow;
using HotelManagement.View.Staff.RoomCatalogManagement.RoomInfo;
using IronXL.Formatting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HotelManagement.ViewModel.StaffVM.RoomCatalogManagementVM
{
    
    public partial class RoomCatalogManagementVM : BaseVM
    {
        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { _SelectedDate = value; OnPropertyChanged(); }
        }
        private DateTime _SelectedTime;
        public DateTime SelectedTime
        {
            get { return _SelectedTime; }
            set { _SelectedTime = value; OnPropertyChanged(); }
        }

        private RadioButton _radioButtonRoomType;
        public RadioButton radioButtonRoomType
        {
            get { return _radioButtonRoomType; }
            set { _radioButtonRoomType = value; OnPropertyChanged(); }
        }
        private RadioButton _radioButtonRoomStatus;
        public RadioButton radioButtonRoomStatus
        {
            get { return _radioButtonRoomStatus; }
            set { _radioButtonRoomStatus = value; OnPropertyChanged(); }
        }
        private RadioButton _radioButtonRoomCleaningStatus;
        public RadioButton radioButtonRoomCleaningStatus
        {
            get { return _radioButtonRoomCleaningStatus; }
            set { _radioButtonRoomCleaningStatus = value; OnPropertyChanged(); }
        }
        Page MainPage;
        public string RoomTypeA
        {
            get
            {
                return ROOM_TYPE.ROOM_TYPE_A;
            }
        }
        public string RoomTypeB
        {
            get
            {
                return ROOM_TYPE.ROOM_TYPE_B;
            }
        }
        public string RoomTypeC
        {
            get
            {
                return ROOM_TYPE.ROOM_TYPE_C;
            }
        }
       

        private List<RoomTypeDTO> _RoomTypes;
        public List<RoomTypeDTO> RoomTypes 
        { 
            get { return _RoomTypes; }
            set { _RoomTypes = value; OnPropertyChanged();}
        }
        private List<RoomDTO> _ListRooms;
        public List<RoomDTO> ListRooms
        {
            get { return _ListRooms; }
            set { _ListRooms = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType1;
        public List<RoomSettingDTO> ListRoomType1
        {
            get { return _ListRoomType1; }
            set { _ListRoomType1 = value; OnPropertyChanged(); }
        }
        private  List<RoomSettingDTO> _ListRoomType1Mini;
        public List<RoomSettingDTO> ListRoomType1Mini
        {
            get { return _ListRoomType1Mini; }
            set { _ListRoomType1Mini = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType2Mini;
        public List<RoomSettingDTO> ListRoomType2Mini
        {
            get { return _ListRoomType2Mini; }
            set { _ListRoomType2Mini = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType3Mini;
        public List<RoomSettingDTO> ListRoomType3Mini
        {
            get { return _ListRoomType3Mini; }
            set { _ListRoomType3Mini = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType1ChangeMini;
        public List<RoomSettingDTO> ListRoomType1ChangeMini
        {
            get { return _ListRoomType1ChangeMini; }
            set { _ListRoomType1ChangeMini = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType2ChangeMini;
        public List<RoomSettingDTO> ListRoomType2ChangeMini
        {
            get { return _ListRoomType2ChangeMini; }
            set { _ListRoomType2ChangeMini = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType3ChangeMini;
        public List<RoomSettingDTO> ListRoomType3ChangeMini
        {
            get { return _ListRoomType3ChangeMini; }
            set { _ListRoomType3ChangeMini = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType2;
        public List<RoomSettingDTO> ListRoomType2
        {
            get { return _ListRoomType2; }
            set { _ListRoomType2 = value; OnPropertyChanged(); }
        }
        private List<RoomSettingDTO> _ListRoomType3;
        public List<RoomSettingDTO> ListRoomType3
        {
            get { return _ListRoomType3; }
            set { _ListRoomType3 = value; OnPropertyChanged(); }
        }

        private RoomSettingDTO _SelectedRoom;
        public RoomSettingDTO SelectedRoom
        {
            get { return _SelectedRoom; }
            set { _SelectedRoom = value; OnPropertyChanged(); }
        }
        Label lbRoomTypeA;
        Label lbRoomTypeB;
        Label lbRoomTypeC;
        private bool TimeChange = false;
        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadRoomInfoCM { get; set; }
        public ICommand ChangeViewCM { get; set; }
        public ICommand SelectedDateTimeCM { get; set; }
        public ICommand OpenRoomWindowCM { get; set; }
        public ICommand FirstLoadRoomWindowCM { get; set; }
        public ICommand RefreshCM { get; set; }
        public RoomCatalogManagementVM()
        {
            Color color = new Color();
            FormatStringDate();
            FirstLoadCM = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                PageSetting(p);
                await AutoUpdateDb();
                await GenerateRoom();
            });
            LoadRoomInfoCM = new RelayCommand<Grid>((p) => { return true; },  (p) => 
            {

                Border border = p.Children.OfType<Border>().FirstOrDefault();
                TextBlock textBlockRoomStatus = (TextBlock)p.FindName("RoomStatus");
                TextBlock textBlockRoomCleaningStatus = (TextBlock)p.FindName("RoomCleaningStatus");
                MaterialDesignThemes.Wpf.PackIcon packIcon = (MaterialDesignThemes.Wpf.PackIcon)p.FindName("icon");

                if (textBlockRoomStatus != null)
                {
                    if (textBlockRoomStatus.Text == ROOM_STATUS.READY)
                    {
                        color = (Color)ColorConverter.ConvertFromString("#59D66D");
                        border.Background = new SolidColorBrush(color);
                    }
                    else if (textBlockRoomStatus.Text == ROOM_STATUS.BOOKED)
                    {
                        color = (Color)ColorConverter.ConvertFromString("#BDBDBA");
                        border.Background = new SolidColorBrush(color);
                    }
                    else
                    {
                        color = (Color)ColorConverter.ConvertFromString("#72B6DC");
                        border.Background = new SolidColorBrush(color);
                    }
                }

                if (textBlockRoomCleaningStatus != null)
                {
                    if (textBlockRoomCleaningStatus.Text == ROOM_CLEANING_STATUS.CLEANED)
                    {
                        packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
                    }
                    else if (textBlockRoomCleaningStatus.Text == ROOM_CLEANING_STATUS.NOT_CLEANING_YET)
                    {
                        packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                    }
                    else
                    {
                        packIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.HammerWrench;
                    }
                }


            });
            ChangeViewCM = new RelayCommand<RadioButton>((p) => { return true; },   (p) =>
            {
                if (p.GroupName.ToString() == "RoomType") radioButtonRoomType = p;
                else if (p.GroupName.ToString() == "RoomStatus") radioButtonRoomStatus= p;
                else if (p.GroupName.ToString() == "RoomCleaningStatus") radioButtonRoomCleaningStatus= p;

                if (TimeChange == false)
                {
                    ListRoomType1 = new List<RoomSettingDTO>(ListRoomType1Mini);
                    ListRoomType2 = new List<RoomSettingDTO>(ListRoomType2Mini);
                    ListRoomType3 = new List<RoomSettingDTO>(ListRoomType3Mini);
                }
                else
                {
                    ListRoomType1 = new List<RoomSettingDTO>(ListRoomType1ChangeMini);
                    ListRoomType2 = new List<RoomSettingDTO>(ListRoomType2ChangeMini);
                    ListRoomType3 = new List<RoomSettingDTO>(ListRoomType3ChangeMini);
                }
               


                ChangView();

                
            });
            SelectedDateTimeCM = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                TimeChange = true;
                ListRoomType1 = ListRoomType1Mini.Select(r => new RoomSettingDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomTypeName.Trim(),
                    RoomStatus = r.RoomStatus.Trim(),
                    RoomCleaningStatus = r.RoomCleaningStatus.Trim(),
                    StartDate = r.StartDate,
                    StartTime = r.StartTime,
                    CheckOutDate = r.CheckOutDate,
                    Validated = r.Validated,
                }).ToList();
               
                ListRoomType2 = ListRoomType2Mini.Select(r => new RoomSettingDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomTypeName.Trim(),
                    RoomStatus = r.RoomStatus.Trim(),
                    RoomCleaningStatus = r.RoomCleaningStatus.Trim(),
                    StartDate = r.StartDate,
                    StartTime = r.StartTime,
                    CheckOutDate = r.CheckOutDate,
                    Validated = r.Validated,
                }).ToList();
                
                ListRoomType3 = ListRoomType3Mini.Select(r => new RoomSettingDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomTypeName.Trim(),
                    RoomStatus = r.RoomStatus.Trim(),
                    RoomCleaningStatus = r.RoomCleaningStatus.Trim(),
                    StartDate = r.StartDate,
                    StartTime = r.StartTime,
                    CheckOutDate = r.CheckOutDate,
                    Validated = r.Validated,
                }).ToList();
               
                radioButtonRoomType = (RadioButton)p.FindName("rdbAllRoomType");
                radioButtonRoomStatus = (RadioButton)p.FindName("rdbAllRoomStatus");
                radioButtonRoomCleaningStatus = (RadioButton)p.FindName("rdbAllRoomCleaningStatus");
                radioButtonRoomType.IsChecked = true;
                radioButtonRoomStatus.IsChecked = true;
                radioButtonRoomCleaningStatus.IsChecked = true;

                ChangView();


                List<string> listRentalContractId = (await RentalContractService.Ins.GetRentalContracts()).Where(x => x.CheckOutDate + x.StartTime > SelectedDate + SelectedTime.TimeOfDay && x.StartDate + x.StartTime <= SelectedDate  + SelectedTime.TimeOfDay).Select(x => x.RoomId).ToList();
              
                foreach (var item in ListRoomType1)
                {
                   
                    if (listRentalContractId.Contains(item.RoomId))
                    {
                        if (item.RoomStatus == ROOM_STATUS.READY)
                        {
                            if (item.StartDate + item.StartTime <= SelectedDate + SelectedTime.TimeOfDay) item.RoomStatus = ROOM_STATUS.BOOKED;
                        }
                    }
                    else item.RoomStatus = ROOM_STATUS.READY;

                }
                foreach (var item in ListRoomType2)
                {
                    if (listRentalContractId.Contains(item.RoomId))
                    {
                        if (item.RoomStatus == ROOM_STATUS.READY)
                        {
                            if (item.StartDate + item.StartTime <= SelectedDate + SelectedTime.TimeOfDay) item.RoomStatus = ROOM_STATUS.BOOKED;
                        }
                    }
                    else item.RoomStatus = ROOM_STATUS.READY;

                }
                foreach (var item in ListRoomType3)
                {
                    if (listRentalContractId.Contains(item.RoomId))
                    {
                        if (item.RoomStatus == ROOM_STATUS.READY)
                        {
                            if (item.StartDate + item.StartTime <= SelectedDate + SelectedTime.TimeOfDay) item.RoomStatus = ROOM_STATUS.BOOKED;
                        }
                    }
                    else item.RoomStatus = ROOM_STATUS.READY;
                }
                ListRoomType1ChangeMini = ListRoomType1.Select(r => new RoomSettingDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomTypeName.Trim(),
                    RoomStatus = r.RoomStatus.Trim(),
                    RoomCleaningStatus = r.RoomCleaningStatus.Trim(),
                    StartDate = r.StartDate,
                    StartTime = r.StartTime,
                    CheckOutDate = r.CheckOutDate,
                    Validated = r.Validated,
                }).ToList();
                ListRoomType2ChangeMini = ListRoomType2.Select(r => new RoomSettingDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomTypeName.Trim(),
                    RoomStatus = r.RoomStatus.Trim(),
                    RoomCleaningStatus = r.RoomCleaningStatus.Trim(),
                    StartDate = r.StartDate,
                    StartTime = r.StartTime,
                    CheckOutDate = r.CheckOutDate,
                    Validated = r.Validated,
                }).ToList();
                ListRoomType3ChangeMini = ListRoomType3.Select(r => new RoomSettingDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomTypeId = r.RoomTypeId,
                    RoomTypeName = r.RoomTypeName.Trim(),
                    RoomStatus = r.RoomStatus.Trim(),
                    RoomCleaningStatus = r.RoomCleaningStatus.Trim(),
                    StartDate = r.StartDate,
                    StartTime = r.StartTime,
                    CheckOutDate = r.CheckOutDate,
                    Validated = r.Validated,
                }).ToList();

            });
            OpenRoomWindowCM = new RelayCommand<object>((p) => { return true; },  (p) =>
            {
                if (SelectedRoom != null)
                {
                    try
                    {
                        RoomWindow wd = new RoomWindow();
                        wd.ShowDialog();


                    }
                    catch(Exception ex)
                    {
                        CustomMessageBox.ShowOk("Lỗi hệ thống!", "Lỗi", "Ok", CustomMessageBoxImage.Error);
                    }
                }
            });
            FirstLoadRoomWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                PersonNumber = RoomService.Ins.GetPersonNumber(SelectedRoom.RentalContractId);
            });
            RefreshCM = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                SelectedDate = DateTime.Today;
                SelectedTime = DateTime.Now;
                radioButtonRoomType = (RadioButton)p.FindName("rdbAllRoomType");
                radioButtonRoomStatus = (RadioButton)p.FindName("rdbAllRoomStatus");
                radioButtonRoomCleaningStatus = (RadioButton)p.FindName("rdbAllRoomCleaningStatus");
                radioButtonRoomType.IsChecked = true;
                radioButtonRoomStatus.IsChecked = true;
                radioButtonRoomCleaningStatus.IsChecked = true;
                await AutoUpdateDb();
                await GenerateRoom();
              
           
            
               
            });
        }

       
        public async Task GenerateRoom()
        {
            RoomTypes = new List<RoomTypeDTO>(await RoomService.Ins.GetRoomTypes());
            ListRoomType1 = new List<RoomSettingDTO>(await RoomService.Ins.GetRoomsByRoomType(RoomTypes[0].RoomTypeId));
            ListRoomType1Mini = new List<RoomSettingDTO>(ListRoomType1);
            ListRoomType2 = new List<RoomSettingDTO>(await RoomService.Ins.GetRoomsByRoomType(RoomTypes[1].RoomTypeId));
            ListRoomType2Mini = new List<RoomSettingDTO>(ListRoomType2);
            ListRoomType3 = new List<RoomSettingDTO>(await RoomService.Ins.GetRoomsByRoomType(RoomTypes[2].RoomTypeId));
            ListRoomType3Mini = new List<RoomSettingDTO>(ListRoomType3);
            
            //ListRooms = new List<RoomDTO>(await RoomService.Ins.GetRooms());

        }
        public async Task AutoUpdateDb()
        {
            await RoomService.Ins.AutoUpdateDb();
        }
        public void FormatStringDate()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }
        private void PageSetting(Page p)
        {
            SelectedDate = DateTime.Today;
            SelectedTime = DateTime.Now;
            radioButtonRoomType = (RadioButton)p.FindName("rdbAllRoomType");
            radioButtonRoomStatus = (RadioButton)p.FindName("rdbAllRoomStatus");
            radioButtonRoomCleaningStatus = (RadioButton)p.FindName("rdbAllRoomCleaningStatus");
            lbRoomTypeA = (Label)p.FindName("lbRoomTypeA");
            lbRoomTypeB = (Label)p.FindName("lbRoomTypeB");
            lbRoomTypeC = (Label)p.FindName("lbRoomTypeC");
            MainPage = (Page)p;
        }
      
     
        private Tuple<string,string,string> GetContentRadioButton(RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton3)
        {
            string res1, res2, res3;
            switch(radioButton1.Name.ToString())
            {
                case "rdbRoomTypeA":
                    res1 = ROOM_TYPE.ROOM_TYPE_A;
                    break;
                case "rdbRoomTypeB":
                    res1 = ROOM_TYPE.ROOM_TYPE_B;
                    break;
                case "rdbRoomTypeC":
                    res1 = ROOM_TYPE.ROOM_TYPE_C;
                    break;
                default:
                    res1 = "All";
                    break;
            }
            switch (radioButton2.Name.ToString())
            {
                case "rdbRoomStatusReady":
                    res2 = ROOM_STATUS.READY;
                    break;
                case "rdbRoomStatusBooked":
                    res2 = ROOM_STATUS.BOOKED;
                    break;
                case "rdbRoomStatusRenting":
                    res2 = ROOM_STATUS.RENTING;
                    break;
                default:
                    res2 = "All";
                    break;
            }
            switch (radioButton3.Name.ToString())
            {
                case "rdbRoomCleaningStatusCleaned":
                    res3 = ROOM_CLEANING_STATUS.CLEANED;
                    break;
                case "rdbRoomCleaningStatusNotCleanYet":
                    res3 = ROOM_CLEANING_STATUS.NOT_CLEANING_YET;
                    break;
                case "rdbRoomCleaningStatusRepairing":
                    res3 = ROOM_CLEANING_STATUS.REPAIRING;
                    break;
                default:
                    res3 = "All";
                    break;
            }
           var res = Tuple.Create(res1,res2,res3);
            return res;
        }
        private void ChangView()
        {

            (string roomType, string roomStatus, string roomCleaningStatus) =  GetContentRadioButton(radioButtonRoomType, radioButtonRoomStatus, radioButtonRoomCleaningStatus);
            lbRoomTypeA.Visibility = lbRoomTypeB.Visibility = lbRoomTypeC.Visibility = Visibility.Visible;
            if (roomType != "All")
            {

                if (roomType == ROOM_TYPE.ROOM_TYPE_A)
                {
                    lbRoomTypeB.Visibility = Visibility.Collapsed;
                    lbRoomTypeC.Visibility = Visibility.Collapsed;
                }
                else if (roomType == ROOM_TYPE.ROOM_TYPE_B)
                {
                    lbRoomTypeA.Visibility = Visibility.Collapsed;
                    lbRoomTypeC.Visibility = Visibility.Collapsed;
                }
                else if (roomType == ROOM_TYPE.ROOM_TYPE_C)
                {
                    lbRoomTypeA.Visibility = Visibility.Collapsed;
                    lbRoomTypeB.Visibility = Visibility.Collapsed;
                }
        
                    if (TimeChange == false)
                {
                    ListRoomType1 = new List<RoomSettingDTO>(ListRoomType1Mini.Where(r => r.RoomTypeName == roomType).ToList());
                    ListRoomType2 = new List<RoomSettingDTO>(ListRoomType2Mini.Where(r => r.RoomTypeName == roomType).ToList());
                    ListRoomType3 = new List<RoomSettingDTO>(ListRoomType3Mini.Where(r => r.RoomTypeName == roomType).ToList());
                }
                    else
                {
                    ListRoomType1 = new List<RoomSettingDTO>(ListRoomType1ChangeMini.Where(r => r.RoomTypeName == roomType).ToList());
                    ListRoomType2 = new List<RoomSettingDTO>(ListRoomType2ChangeMini.Where(r => r.RoomTypeName == roomType).ToList());
                    ListRoomType3 = new List<RoomSettingDTO>(ListRoomType3ChangeMini.Where(r => r.RoomTypeName == roomType).ToList());
                }
                   
              
              
            }

            if (roomStatus != "All")
            {
                ListRoomType1 = new List<RoomSettingDTO>(ListRoomType1.Where(r => r.RoomStatus == roomStatus).ToList());
                ListRoomType2 = new List<RoomSettingDTO>(ListRoomType2.Where(r => r.RoomStatus == roomStatus).ToList());
                ListRoomType3 = new List<RoomSettingDTO>(ListRoomType3.Where(r => r.RoomStatus == roomStatus).ToList());
            }

            if (roomCleaningStatus != "All")
            {
                ListRoomType1 = new List<RoomSettingDTO>(ListRoomType1.Where(r => r.RoomCleaningStatus == roomCleaningStatus).ToList());
                ListRoomType2 = new List<RoomSettingDTO>(ListRoomType2.Where(r => r.RoomCleaningStatus == roomCleaningStatus).ToList());
                ListRoomType3 = new List<RoomSettingDTO>(ListRoomType3.Where(r => r.RoomCleaningStatus == roomCleaningStatus).ToList());
            }
        }
    }
   
  
}
