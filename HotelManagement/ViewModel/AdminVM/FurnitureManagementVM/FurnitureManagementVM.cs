using CinemaManagementProject.Utilities;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Input;

namespace HotelManagement.ViewModel.AdminVM.FurnitureManagementVM
{
    public class Furniture
    {
        public string name;
        public string type;
        public string quantity;
        public Furniture() { }
        public Furniture(string name, string type, string quantity)
        {
            this.name = name;
            this.type = type;
            this.quantity = quantity;
        }
    }
    public class FurnitureManagementVM : BaseVM
    {
        public ICommand FirstLoadCM { get; set; }
        public FurnitureManagementVM()
        {
            FirstLoadCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
            });

        }
    }
}
