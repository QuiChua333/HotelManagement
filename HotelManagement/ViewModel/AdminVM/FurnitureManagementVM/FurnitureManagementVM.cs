using CinemaManagementProject.Utilities;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private bool isChanged;
        public bool IsChanged { get { return isChanged; } set { isChanged = value; OnPropertyChanged(); } }
        public ICommand FirstLoadCM { get; set; }
        public ICommand ClickCM { get; set; }
        public ICommand CloseCM { get; set; }


        public FurnitureManagementVM()
        {
            FirstLoadCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
                IsChanged = false;
            });

            ClickCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
                IsChanged = true ;
            });
            CloseCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, (p) =>
            {
                IsChanged = false;
            });

        }
    }
}
