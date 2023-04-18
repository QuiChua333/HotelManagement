using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.AdminVM.RoomManagementVM
{
    public class RoomManagementVM : BaseVM
    {
        public ICommand FirstLoadCM { get; set; }
        public RoomManagementVM() 
        {
            FirstLoadCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {

            });
        }
    }
}
