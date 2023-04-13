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

namespace HotelManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Tg_Btn_Click(object sender, RoutedEventArgs e)
        {
            fullsidebar.Visibility= Visibility.Visible;
        }

        private void Tg_Btn2_Click(object sender, RoutedEventArgs e)
        {
            fullsidebar.Visibility = Visibility.Hidden;
        }

        private void mainFrame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            fullsidebar.Visibility = Visibility.Hidden;
        }


    }
}
