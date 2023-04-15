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

namespace HotelManagement.View.Admin.ServiceManagement
{
    /// <summary>
    /// Interaction logic for ServiceManagementPage.xaml
    /// </summary>
    public partial class ServiceManagementPage : Page
    {
        public class Service
        {
            public string name;
            public string type;
            public string quantity;
            public Service() { }
            public Service(string name, string type, string quantity)
            {
                this.name = name;
                this.type = type;
                this.quantity = quantity;
            }
        }
        public List<Service> furnitures;

        public ServiceManagementPage()
        {
            InitializeComponent();
            furnitures = new List<Service>();
            Service f;
            for (int i = 0; i < 9; i++)
            {
                f = new Service("Giuong Rolex Diamond " + i.ToString(), "Giuong ngu", "38" + i.ToString());
                furnitures.Add(f);
            }

            ListViewProducts.ItemsSource = furnitures;
        }
    }
}
