using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.AdminVM.CustomerManagementVM
{
    public class CustomerManagementVM : BaseVM
    {
        public ICommand FirstLoadCM { get; set; }
        public CustomerManagementVM()
        {
            FirstLoadCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
            });

        }
    }
}
