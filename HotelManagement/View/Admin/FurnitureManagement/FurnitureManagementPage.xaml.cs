using CinemaManagementProject.Utilities;
using HotelManagement.ViewModel.AdminVM.FurnitureManagementVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.View.Admin.FurnitureManagement
{
    /// <summary>
    /// Interaction logic for FurnitureManagementPage.xaml
    /// </summary>
    public partial class FurnitureManagementPage : Page
    {
        public List<Furniture> furnitures;

               
        public FurnitureManagementPage()
        {
            InitializeComponent();
            furnitures = new List<Furniture>();
            Furniture f;
            for (int i = 0; i < 9; i++)
            {
                f = new Furniture("Giuong Rolex Diamond " + i.ToString(), "Giuong ngu", "38" + i.ToString());
                furnitures.Add(f);
            }

            ListViewProducts.ItemsSource = furnitures;
        }
    }
}
