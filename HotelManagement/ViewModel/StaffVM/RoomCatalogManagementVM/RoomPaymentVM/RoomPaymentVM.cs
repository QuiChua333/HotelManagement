using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using HotelManagement.Utilities;
using HotelManagement.View.Staff.RoomCatalogManagement.RoomPayment;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.ViewModel.StaffVM.RoomCatalogManagementVM
{
    public partial class RoomCatalogManagementVM : BaseVM
    {


        private List<BillDTO> _ListBill;
        public List<BillDTO> ListBill
        {
            get { return _ListBill; }
            set { _ListBill = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<string> _ListRoomByCustomer;
        public ObservableCollection<string> ListRoomByCustomer
        {
            get { return _ListRoomByCustomer; }
            set { _ListRoomByCustomer = value; OnPropertyChanged(); }
        }
        private List<string> _ListPaymentRoomId;
        public List<string> ListPaymentRoomId
        {
            get { return _ListPaymentRoomId; }
            set { _ListPaymentRoomId = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ServiceUsingDTO> _ListServicePayment;
        public ObservableCollection<ServiceUsingDTO> ListServicePayment
        {
            get { return _ListServicePayment; }
            set { _ListServicePayment = value; OnPropertyChanged(); }
        }
        private ObservableCollection<RentalContractDTO> _ListRentalContractByCustomer;
        public ObservableCollection<RentalContractDTO> ListRentalContractByCustomer
        {
            get { return _ListRentalContractByCustomer; }
            set { _ListRentalContractByCustomer = value; OnPropertyChanged(); }
        }
        private RentalContractDTO _RentalContractPayment;
        public RentalContractDTO RentalContractPayment
        {
            get { return _RentalContractPayment; }
            set { _RentalContractPayment = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TroubleByCustomerDTO> _ListTroubleByCustomer;
        public ObservableCollection<TroubleByCustomerDTO> ListTroubleByCustomer
        {
            get { return _ListTroubleByCustomer; }
            set { _ListTroubleByCustomer = value; OnPropertyChanged(); }
        }
        public string CreateDateStr
        {
            get
            {
                return DateTime.Today.ToString("dd/MM/yyyy");
            }
        }
        private double _TotalMoneyPayment;
        public double TotalMoneyPayment
        {
            get { return _TotalMoneyPayment; }
            set { _TotalMoneyPayment = value; OnPropertyChanged(); }
        }
        private string _TotalMoneyPaymentStr;
        public string TotalMoneyPaymentStr
        {
            get { return _TotalMoneyPaymentStr; }
            set { _TotalMoneyPaymentStr = value; OnPropertyChanged(); }
        }
        public async Task Payment()
        {
            ListRentalContractByCustomer = new ObservableCollection<RentalContractDTO> (await RentalContractService.Ins.GetRentalContractByCustomer(SelectedRoom.CustomerId));
            ListPaymentRoomId = new List<string>();
            if (ListRentalContractByCustomer != null)
            {
                if (ListRentalContractByCustomer.Count > 1)
                {
                    OptionPayment wd = new OptionPayment();
                    wd.lbCustomerName.Text = SelectedRoom.CustomerName;
                    ListRoomByCustomer = new ObservableCollection<string>(ListRentalContractByCustomer.Select(x => "Phòng " + x.RoomNumber).ToList());
                    wd.ShowDialog();
                }
                else
                {
                    RoomBill wd = new RoomBill();
                    RentalContractPayment = ListRentalContractByCustomer[0];
                    ListTroubleByCustomer = new ObservableCollection<TroubleByCustomerDTO>(await TroubleService.Ins.GetListTroubleByCustomer(RentalContractPayment.RentalContractId));
                    if (ListTroubleByCustomer.Count == 0)
                    {
                        wd.wrapperTrouble.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    TotalMoneyPayment = 0;
                    wd.ShowDialog();
                }
            }
        }
        public async Task LoadRoomBillFunc()
        {

            ListServicePayment = new ObservableCollection<ServiceUsingDTO>(await ServiceUsingHelper.Ins.GetListUsingService(RentalContractPayment.RentalContractId));
            ListServicePayment.Insert(0, new ServiceUsingDTO
                {
                    ServiceName = RentalContractPayment.RoomName,
                    UnitPrice = RentalContractPayment.RoomPrice,
                    Quantity = RentalContractPayment.DayNumber,
                });
            foreach (var item in ListServicePayment)
            {
                TotalMoneyPayment += item.TotalMoney;
            }
            foreach (var item in ListTroubleByCustomer)
            {
                TotalMoneyPayment += (double)item.PredictedPrice;
            }
            FormatMoney();
            
        }
        private void FormatMoney()
        {
            TotalMoneyPaymentStr = Helper.FormatVNMoney2(TotalMoneyPayment);
        }
    }
}
