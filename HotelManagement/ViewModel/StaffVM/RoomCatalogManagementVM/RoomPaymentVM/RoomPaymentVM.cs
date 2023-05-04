using HotelManagement.DTOs;
using HotelManagement.Model.Services;
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
        public async Task PayMent()
        {
            var listRentalContractByCustomer = await BillService.Ins.GetRentalContractByCustomer(SelectedRoom.CustomerId);
            if (listRentalContractByCustomer!=null)
            {
                if (listRentalContractByCustomer.Count > 1)
                {
                    OptionPayment wd = new OptionPayment();
                    wd.lbCustomerName.Text = SelectedRoom.CustomerName;

                }
            }
        }
    }
}
