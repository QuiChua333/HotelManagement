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

namespace HotelManagement.View.Admin.RoomFurnitureManagement
{
    /// <summary>
    /// Interaction logic for RoomFurnitureManagementPage.xaml
    /// </summary>
    public partial class RoomFurnitureManagementPage : Page
    {
        public List<Furniture> furnitures;
        public RoomFurnitureManagementPage()
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
