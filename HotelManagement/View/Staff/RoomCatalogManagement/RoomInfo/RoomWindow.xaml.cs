using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace HotelManagement.View.Staff.RoomCatalogManagement.RoomInfo
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>

    public partial class RoomWindow : Window
    {
        public class DV
        {
            public string Dichvu { get; set; }
            public int Soluong { get; set; }
            public int Dongia { get; set; }
            public int Thanhtien { get; set; }
        }
        public string qui { get; set; }
        public ObservableCollection<DV> dssv = new ObservableCollection<DV>() { new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
        new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
        new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20, Thanhtien = 300 },
         new DV { Dichvu = "COCA", Soluong = 200, Dongia = 20000, Thanhtien = 301000 },
        };
        public ObservableCollection<DV> DSDV
        {
            get { return dssv; }
            set { dssv = value; }
        }

        public RoomWindow()
        {
            InitializeComponent();
            this.DataContext = this;
          

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("OK");
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("OK");
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
