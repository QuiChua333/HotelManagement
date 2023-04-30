﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HotelManagement.View.Admin.RoomFurnitureManagement
{
    /// <summary>
    /// Interaction logic for RoomFurnitureInfoWindow.xaml
    /// </summary>
    public partial class RoomFurnitureInfoWindow : Window
    {
        public RoomFurnitureInfoWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 150);
            timer.Tick += ((sender, e) =>
            {

                if (scrollNote.VerticalOffset < scrollNote.ScrollableHeight)
                {
                    scrollNote.ScrollToVerticalOffset(scrollNote.VerticalOffset + 1);
                }
                else
                {
                    scrollNote.ScrollToTop();
                    timer.Stop();
                }    

            });
            timer.Start();
            
            timer2.Interval = new TimeSpan(0, 0, 1);
            timer2.Tick += ((s, se) =>
            {
                SetDeleteControl();
                timer2.Stop();
            });

        }
        DispatcherTimer timer2 = new DispatcherTimer();

        private void RoomFurnitureInfoWD_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void RoundBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                timer2.Start();
                Grid grid = sender as Grid;
                Rectangle rec = (Rectangle)grid.FindName("Mask");
                rec.Opacity = 0.15;
            }
        }
        public void SetDeleteControl()
        {
            ListRadio.Visibility = Visibility.Visible;
            TopDeleteControl.Visibility = Visibility.Visible;
            BottomDeleteControl.Visibility = Visibility.Visible;
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListRadio.Visibility = Visibility.Collapsed;
            TopDeleteControl.Visibility = Visibility.Collapsed;
            BottomDeleteControl.Visibility = Visibility.Collapsed;
        }

        private void RoundBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            timer2.Stop();
            Grid grid = sender as Grid;
            Rectangle rec = (Rectangle)grid.FindName("Mask");
            rec.Opacity = 0;
        }
    }
}
