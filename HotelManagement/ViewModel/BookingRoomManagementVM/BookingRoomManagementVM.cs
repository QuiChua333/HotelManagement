using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement.ViewModel.BookingRoomManagementVM
{
    public partial class BookingRoomManagementVM:BaseVM
    {
        public ICommand CloseCM { get; set; }
        public BookingRoomManagementVM() 
        {
            CloseCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
