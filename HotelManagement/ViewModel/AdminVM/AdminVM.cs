using CinemaManagementProject.Utilities;
using HotelManagement.DTOs;
using HotelManagement.View.Admin;
using HotelManagement.View.Admin.StatisticalManagement;
using HotelManagement.View.CustomMessageBoxWindow;
using HotelManagement.View.Login;
using HotelManagement.View.Staff;
using HotelManagement.ViewModel.StaffVM.RoomCatalogManagementVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagement.ViewModel.AdminVM
{
    public class AdminVM : BaseVM
    {

        public static StaffDTO CurrentStaff;

        private string _staffname;
        public string StaffName
        {
            get { return _staffname; }
            set { _staffname = value; OnPropertyChanged(); }
        }
        public string _avatarName { get; set; }
        public string AvatarName
        {
            get { return _avatarName; }
            set { _avatarName = value; OnPropertyChanged(); }
        }

        private ImageSource _imageSource { get; set; }
        public ImageSource AvatarSource
        {
            get { return _imageSource; }
            set { _imageSource = value; OnPropertyChanged(); }
        }

        private string _StaffId;
        public string StaffId
        {
            get { return _StaffId; }
            set { _StaffId = value; OnPropertyChanged(); }
        }
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
        private void BookingRoom(object obj) => CurrentView = new BookingRoomManagementVM.BookingRoomManagementVM();
        private void Room(object obj) => CurrentView = new RoomManagementVM.RoomManagementVM();
        private void RoomType(object obj) => CurrentView = new RoomTypeManagementVM.RoomTypeManagementVM();
        private void Statiscal(object obj) => CurrentView = new StatisticalManagementVM.StatisticalManagementVM();
        private void HelpScreen(object obj) => CurrentView = new HelpScreenVM.HelpScreenVM();
        private void Customer(object obj) => CurrentView = new CustomerManagementVM.CustomerManagementVM();
        private void Staff(object obj) => CurrentView = new StaffManagementVM.StaffManagementVM();
        private void History(object obj) => CurrentView = new HistoryManagementVM.HistoryManagementVM();
        private void Trouble(object obj) => CurrentView = new TroubleManagementVM.TroubleManagementVM();
        private void RoomCatalog(object obj) => CurrentView = new RoomCatalogManagementVM();

        public ICommand FurnitureCommand { get; set; }
        public ICommand ServiceCommand { get; set; }
        public ICommand RoomFurnitureCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        public ICommand BookingRoomCommand { get; set; }
        public ICommand RoomCommand { get; set; }
        public ICommand RoomTypeCommand { get; set; }
        public ICommand StatiscalCommand { get; set; }
        public ICommand HelpScreenCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand StaffCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand TroubleCommand { get; set; }
        public ICommand RoomCatalogCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public AdminVM()
        {
            _currentView = new RoomFurnitureManagementVM.RoomFurnitureManagementVM();

            FurnitureCommand = new RelayCommand(Furniture);
            ServiceCommand = new RelayCommand(Service);
            RoomFurnitureCommand = new RelayCommand(RoomFurniture);
            SettingCommand = new RelayCommand(Setting);
            BookingRoomCommand = new RelayCommand(BookingRoom);
            RoomCommand = new RelayCommand(Room);
            RoomTypeCommand = new RelayCommand(RoomType);
            StatiscalCommand=new RelayCommand(Statiscal);
            HelpScreenCommand = new RelayCommand(HelpScreen);
            CustomerCommand=new RelayCommand(Customer);
            StaffCommand=new RelayCommand(Staff);
            HistoryCommand=new RelayCommand(History);
            TroubleCommand=new RelayCommand(Trouble);
            RoomCatalogCommand = new RelayCommand(RoomCatalog);
            FirstLoadCM = new RelayCommand<Rectangle>((p) => { return true; }, (p) =>
            {
                StaffName = CurrentStaff.StaffName;
                SetAvatarName(StaffName);
                StaffId = CurrentStaff.StaffId;
                if (CurrentStaff.Avatar != null)
                    AvatarSource = LoadAvatarImage(CurrentStaff.Avatar);
                else
                    AvatarSource = null;
                p.Fill = (SolidColorBrush)new BrushConverter().ConvertFrom(Properties.Settings.Default.MainAppColor);
            });
            LogOutCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (CustomMessageBox.ShowOkCancel("Bạn thật sự muốn đăng xuất không?","Cảnh báo", "Đăng xuất", "Không", CustomMessageBoxImage.Warning) == CustomMessageBoxResult.OK)
                {
                    LoginWindow loginwd = new LoginWindow();
                    AdminWindow st = Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
                    st.Close();
                    loginwd.Show();
                }
            });
        }
        public void SetAvatarName(string staffName)
        {
            string[] trimNames = staffName.Split(' ');
            AvatarName = trimNames[trimNames.Length - 1][0].ToString() + trimNames[0][0].ToString();
        }
        public BitmapImage LoadAvatarImage(byte[] data)
        {
            MemoryStream strm = new MemoryStream();
            strm.Write(data, 0, data.Length);
            strm.Position = 0;
            System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }
    }
}
