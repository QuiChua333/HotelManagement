using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.StaffVM.TroubleReportVM
{
    public class TroubleReportVM : BaseVM
    {
        public ICommand FirstLoadCM { get; set; }
        public TroubleReportVM()
        {
            FirstLoadCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {

            });

        }
    }
}
