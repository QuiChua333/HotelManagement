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
using System.Windows.Shapes;

namespace HotelManagement.View.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public class Number
        {
            public string item;
            public void setValueITem(string item2)
            {
                item = item2;
            }
        }
        public AdminWindow()
        {
            InitializeComponent();
            source = new List<Number>(9);
            Number nb;
            for (int i = 0; i < 9; i++)
            {
                nb = new Number();
                nb.setValueITem(i.ToString());
                source.Add(nb);
            }    
            this.DataContext = this;
            lb = new ListBox();
            lb.ItemsSource = source;
        }
        public List<Number> source;
        public ListBox lb;
    }
}
