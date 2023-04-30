using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.AdminVM.RoomFurnitureManagementVM
{
    public partial class RoomFurnitureManagementVM
    {

        private ObservableCollection<FurnitureDTO> allFurnitureInRoom;
        public ObservableCollection<FurnitureDTO> AllFurnitureInRoom
        {
            get { return allFurnitureInRoom; }
            set { allFurnitureInRoom = value; OnPropertyChanged(); }
        }

        private FurnitureDTO selectedFurnitureInRoom;
        public FurnitureDTO SelectedFurnitureInRoom
        {
            get { return selectedFurnitureInRoom; }
            set { selectedFurnitureInRoom = value; OnPropertyChanged(); }
        }
        private FurnitureDTO furnitureCache;
        public FurnitureDTO FurnitureCache
        {
            get { return furnitureCache; }
            set { furnitureCache = value; OnPropertyChanged(); }
        }

        public ICommand FirstLoadInfoWindowCM { get; set; }
        public ICommand OpenImportFurnitureRoomCM { get; set; }

        public async Task LoadFurniture()
        {
            (bool isSuccess, string messageReturn, List<FurnitureDTO> listFurnituresRoomReturn) = await Task.Run(() => FurnituresRoomService.Ins.GetAllFurnituresIn(FurnituresRoomCache));

            if (isSuccess)
            {
                FurnituresRoomCache.ListFurnitureRoom = new ObservableCollection<FurnitureDTO>(listFurnituresRoomReturn);
                FurnituresRoomCache.SetQuantityAndStringTypeFurniture();
            }
            else
            {
                CustomMessageBox.ShowOk(messageReturn, "Lỗi", "OK", View.CustomMessageBoxWindow.CustomMessageBoxImage.Error);
            }
        }
    }
}
