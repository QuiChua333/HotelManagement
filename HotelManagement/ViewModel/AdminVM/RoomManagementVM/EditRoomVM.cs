using HotelManagement.DTOs;
using HotelManagement.Model.Services;
using HotelManagement.Model;
using HotelManagement.View.Admin.RoomManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelManagement.Utils.Constants;
using System.Windows.Input;
using System.Windows;

namespace HotelManagement.ViewModel.AdminVM.RoomManagementVM
{
    public partial class RoomManagementVM : BaseVM
    {

        public ICommand LoadEditRoomCM { get; set; }

        public void LoadEditRoom(EditRoom w1)
        {
            RoomID = SelectedItem.RoomID;
            RoomNumber = SelectedItem.RoomNumber;
            RoomNote = SelectedItem.Note;

            if (SelectedItem.RoomTypeId == "LP001")
            {
                w1.loaiphong.SelectedIndex = 0;
            }
            else if (SelectedItem.RoomTypeId == "LP002")
            {
                w1.loaiphong.SelectedIndex = 1;
            }
            else w1.loaiphong.SelectedIndex = 2;

            w1.trangthaiphong.Text = SelectedItem.RoomStatus;
            w1.tinhtrangphong.Text = SelectedItem.RoomCleaningStatus;
        }
        
        public async Task UpdateRoomFunc(System.Windows.Window p)
        {
            string rtn = CbRoomType.Tag.ToString();
            string rti = await RoomTypeService.Ins.GetRoomTypeID(rtn);

            if (RoomID != null && IsValidData())
            {
                RoomDTO room = new RoomDTO
                {
                    RoomID = RoomID,
                    Note = RoomNote,
                    RoomNumber = RoomNumber,
                    RoomTypeId = rti,
                    RoomCleaningStatus = CbRoomTinhTrang.Tag.ToString(),
                    RoomTypeName = CbRoomType.Tag.ToString(),
                    RoomStatus = CbRoomStatus.Tag.ToString(),
                };

                (bool successUpdateRoom, string messageFromUpdateRoom) = await RoomService.Ins.UpdateRoom(room);

                if (successUpdateRoom)
                {
                    isSaving = false;
                    CustomMessageBox.ShowOk(messageFromUpdateRoom, "Thông báo", "OK", View.CustomMessageBoxWindow.CustomMessageBoxImage.Success);
                    LoadRoomListView(Operation.UPDATE, room);
                    p.Close();
                }
                else
                {
                    CustomMessageBox.ShowOk(messageFromUpdateRoom, "Lỗi", "OK", View.CustomMessageBoxWindow.CustomMessageBoxImage.Error);
                }
            }
            else
            {
                CustomMessageBox.ShowOk("Vui lòng nhập đủ thông tin!", "Cảnh báo", "OK", View.CustomMessageBoxWindow.CustomMessageBoxImage.Warning);
            }
        }
    }
}
