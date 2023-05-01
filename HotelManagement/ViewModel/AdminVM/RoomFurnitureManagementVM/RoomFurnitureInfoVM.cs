using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using HotelManagement.View.CustomMessageBoxWindow;
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

        private ObservableCollection<FurnitureDTO> listFurnitureNeedDelete;
        public ObservableCollection<FurnitureDTO> ListFurnitureNeedDelete
        {
            get { return listFurnitureNeedDelete; }
            set { listFurnitureNeedDelete = value; OnPropertyChanged(); }
        }

        private FurnitureDTO furnitureCache;
        public FurnitureDTO FurnitureCache
        {
            get { return furnitureCache; }
            set { furnitureCache = value; OnPropertyChanged(); }
        }



        public ICommand FirstLoadInfoWindowCM { get; set; }
        public ICommand OpenImportFurnitureRoomCM { get; set; }
        public ICommand ChooseItemToListNeedDelete { get; set; }
        public ICommand RemoveItemToListNeedDelete { get; set; }
        public ICommand ChooseAllFurnitureToDeleteCM { get; set; }
        public ICommand DeleteListFurnitureCM { get; set; }
        public ICommand CloseDeleteControlCM { get; set; }

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
        public async Task DeleteListFurniture()
        {
            if (ListFurnitureNeedDelete.Count() == 0)
                return;

            if (CustomMessageBox.ShowOkCancel("Bạn có muốn xóa những tiện nghi được chọn ra khỏi phòng không?", "Cảnh báo", "Có", "Không", CustomMessageBoxImage.Warning)
                == CustomMessageBoxResult.OK)
            {
                (bool isSuccess, string messageReturn) = await Task.Run(() => FurnituresRoomService.Ins.DeleteListFurnitureRoom(furnituresRoomCache.RoomId, ListFurnitureNeedDelete));
                if (isSuccess)
                {
                    CustomMessageBox.ShowOk(messageReturn, "Thành công", "OK", CustomMessageBoxImage.Success);
                    FurnituresRoomCache.DeleteListFurniture(ListFurnitureNeedDelete);
                    FurnituresRoomCache.SetQuantityAndStringTypeFurniture();
                    ListFurnitureNeedDelete.Clear();
                }
                else
                {
                    CustomMessageBox.ShowOk(messageReturn, "Lỗi", "OK", CustomMessageBoxImage.Error);
                }
            }
        }

    }
}
