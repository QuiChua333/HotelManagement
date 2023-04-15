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

namespace HotelManagement.View.Admin.HistoryManagement
{
    /// <summary>
    /// Interaction logic for ExportManagementPage.xaml
    /// </summary>
    public partial class ExportManagementPage : Page
    {
        public ExportManagementPage()
        {
            InitializeComponent();
        }

        private void cbb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (bodermonth != null && bodertime != null)
            {
                switch (comboBox.SelectedIndex)
                {
                    case 0:
                        {
                            bodertime.Visibility = Visibility.Hidden;
                            bodermonth.Visibility = Visibility.Hidden;
                            break;
                        }
                    case 1:
                        {
                            bodertime.Visibility = Visibility.Visible;
                            bodermonth.Visibility = Visibility.Hidden;
                            break;
                        }
                    case 2:
                        {
                            bodertime.Visibility = Visibility.Hidden;
                            bodermonth.Visibility = Visibility.Visible;
                            break;
                        }
                }
            }
        }
    }
}
