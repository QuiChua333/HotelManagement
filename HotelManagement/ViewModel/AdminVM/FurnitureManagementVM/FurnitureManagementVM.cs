using CinemaManagementProject.Utilities;
using HotelManagement.Components.Search;
using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using HotelManagement.View.Admin;
using HotelManagement.View.Admin.FurnitureManagement;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HotelManagement.ViewModel.AdminVM.FurnitureManagementVM
{
    public partial class FurnitureManagementVM : BaseVM
    {
        private bool isChanged;
        public bool IsChanged { get { return isChanged; } set { isChanged = value; OnPropertyChanged(); } }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; OnPropertyChanged(); }
        }

        private ObservableCollection<FurnitureDTO> furnitureList;
        public ObservableCollection<FurnitureDTO> FurnitureList
        {
            get { return furnitureList; }
            set { furnitureList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<FurnitureDTO> allFurniture;
        public ObservableCollection<FurnitureDTO> AllFurniture
        {
            get { return allFurniture; }
            set { allFurniture = value; OnPropertyChanged(); }
        }

        private FurnitureDTO selectedFurniture;
        public FurnitureDTO SelectedFurniture
        {
            get { return selectedFurniture; }
            set { selectedFurniture = value; OnPropertyChanged(); }
        }
        public FurnitureDTO SelectedFurnitureCacheData { get; set; }

        private ObservableCollection<string> currentListFurnitureType { get; set; }
        public ObservableCollection<string> CurrentListFurnitureType
        {
            get { return currentListFurnitureType; }
            set { currentListFurnitureType = value; OnPropertyChanged(); }
        }

        private string _selectedItemFilter;
        public string SelectedItemFilter
        {
            get => _selectedItemFilter;
            set
            {
                _selectedItemFilter = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> AllListFurniture { get; set; }

        public ICommand FirstLoadCM { get; set; }
        public ICommand ClickCM { get; set; }
        public ICommand CloseCM { get; set; }
        public ICommand OpenEditFurnitureCM { get; set; }
        public ICommand CloseEditFurnitureCM { get; set; }
        public ICommand SelectionFilterChangeCM { get; set; }



        public FurnitureManagementVM()
        {
            AdminWindow tk = System.Windows.Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            

            FirstLoadCM = new RelayCommand<System.Windows.Controls.Page>((p) => { return true; }, async (p) =>
            {
                IsChanged = false;

                IsLoading = true;
                (bool isSuccess, string messageReturn, List<FurnitureDTO> listFurniture) = await Task.Run(() => FurnitureService.Ins.GetAllFurniture());
                CurrentListFurnitureType = new ObservableCollection<string>(GetAllCurrentFurnitureType(listFurniture));
                IsLoading = false;

                if (isSuccess)
                {
                    AllFurniture = new ObservableCollection<FurnitureDTO>(listFurniture);
                    FurnitureList = new ObservableCollection<FurnitureDTO>(AllFurniture);
                }
                else
                    CustomMessageBox.ShowOk(messageReturn, "Lỗi" , "OK");
            });

            OpenEditFurnitureCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedFurniture !=null)
                {
                    SelectedFurnitureCacheData = SelectedFurniture;
                    FurnitureEditWindow furnitureEditWD = new FurnitureEditWindow();
                    tk.MaskOverSideBar.Visibility = Visibility.Visible;
                    furnitureEditWD.ShowDialog();
                }
            });

            CloseEditFurnitureCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                tk.MaskOverSideBar.Visibility = Visibility.Collapsed;
                SelectedFurnitureCacheData = null;
            });

            SelectionFilterChangeCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedItemFilter != null)
                {
                    if (SelectedItemFilter == "Tất cả")
                        FurnitureList = AllFurniture;
                    else
                    FurnitureList = new ObservableCollection<FurnitureDTO>(FurnitureService.Ins.GetAllFurnitureByType(SelectedItemFilter, AllFurniture));
                }    
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

        public List<string> GetAllCurrentFurnitureType(List<FurnitureDTO> listFurniture)
        {
            List<string> result = new List<string>();
            int furnitureCount = listFurniture.Count();
            result.Add("Tất cả");
            SelectedItemFilter = result[0];
            for(int i = 0; i < furnitureCount; i++)
            {
                if (result.Contains(listFurniture[i].FurnitureType))
                    continue;
                result.Add(listFurniture[i].FurnitureType);
            }
            AllFurnitureType = new List<string>(result);
            AllFurnitureType.Remove(AllFurnitureType[0]);
            return result;
        }
    }
}
