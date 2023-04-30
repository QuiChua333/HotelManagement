using Google.Apis.Util;
using HotelManagement.DTOs;
using IronXL.Formatting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.Model.Services
{
    public class RoomService
    {
        public RoomService() { }
        private static RoomService _ins;
        public static RoomService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new RoomService();
                return _ins;
            }
            private set { _ins = value; }
        }
        public async Task<List<RoomDTO>> GetAllRoom()
        {
            try
            {
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    List<RoomDTO> RoomDTOs = await (
                        from r in db.Rooms join temp in db.RoomTypes
                        on r.RoomTypeId equals temp.RoomTypeId into gj
                        from d in gj.DefaultIfEmpty()
                        select new RoomDTO
                        {
                            // DTO = db
                            RoomID = r.RoomId,
                            RoomNumber = (int)r.RoomNumber,
                            RoomTypeName = d.RoomTypeName,
                            RoomTypeId = d.RoomTypeId,
                            Note = r.Note,
                            RoomCleaningStatus = r.RoomCleaningStatus,
                            RoomStatus = r.RoomStatus,
                        }
                    ).ToListAsync();
                    RoomDTOs.Reverse();
                    return RoomDTOs;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private string CreateNextRoomCode(string maxCode)
        {
            if (maxCode == "")
            {
                return "PH001";
            }
            int index = (int.Parse(maxCode.Substring(2)) + 1);
            string CodeID = index.ToString();
            while (CodeID.Length < 3) CodeID = "0" + CodeID;

            return "PH" + CodeID;
        }
        public async Task<(bool, string, RoomDTO)> AddRoom(RoomDTO newRoom)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    Room r = context.Rooms.Where((Room Room) => Room.RoomNumber == newRoom.RoomNumber).FirstOrDefault();

                    if (r != null)
                    {
                        if (r.IsDeleted == false)
                        {
                            return (false, "Phòng này đã tồn tại", null);
                        }
                        //Khi loại phòng đã bị xóa nhưng được add lại với cùng tên => update lại loại phòng đã xóa đó với thông tin là 
                        // loại phòng mới thêm thay vì add thêm
                        r.RoomNumber = newRoom.RoomNumber;
                        r.RoomStatus = newRoom.RoomStatus;
                        r.RoomCleaningStatus = newRoom.RoomCleaningStatus;
                        r.RoomTypeId = newRoom.RoomTypeId;
                        r.Note = newRoom.Note;
                        r.IsDeleted = false;

                        await context.SaveChangesAsync();
                        newRoom.RoomID = r.RoomId;
                    }
                    else
                    {
                        var listid = await context.Rooms.Select(s => s.RoomId).ToListAsync();
                        string maxId = "";

                        if (listid.Count > 0)
                            maxId = listid[listid.Count - 1];
                        string id = CreateNextRoomCode(maxId);
                        Room room = new Room
                        {
                            RoomId = id,
                            RoomNumber = newRoom.RoomNumber,
                            RoomTypeId = newRoom.RoomTypeId,
                            Note = newRoom.Note,
                            RoomStatus = newRoom.RoomStatus,
                            RoomCleaningStatus = newRoom.RoomCleaningStatus,
                            IsDeleted = false,
                        };
                        context.Rooms.Add(room);
                        await context.SaveChangesAsync();
                        newRoom.RoomID = room.RoomId;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return (false, "DbEntityValidationException", null);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (false, $"Error Server {e}", null);
            }
            return (true, "Thêm phòng thành công", newRoom);
        }

        public async Task<(bool, string)> DeleteRoom(string Id)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    Room room = await (from p in context.Rooms
                                               where p.RoomId == Id && p.IsDeleted == false
                                               select p).FirstOrDefaultAsync();
                    if (room == null)
                    {
                        return (false, "Loại phòng này không tồn tại!");
                    }
                    room.IsDeleted = true;
                    context.Rooms.Remove(room);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return (false, "Phòng này đã áp dụng trước đây và đã có khách đặt. Không thể xóa!");
            }
            return (true, "Xóa phòng thành công");
        }

        public async Task<(bool, string)> UpdateRoom(RoomDTO updatedRoom)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    Room room = context.Rooms.Find(updatedRoom.RoomID);

                    if (room is null)
                    {
                        return (false, "Phòng này không tồn tại!");
                    }

                    // ở dưới phải đợi thêm r fix, code coment ở dưới tức là khi
                    // phòng đã có người đặt hoặc đang thuê thì không thể chỉnh sửa

                    //bool IsExistRoomNumber = context.Rooms.Any((Room r) => r.RoomId != room.RoomTypeId && r.RoomNumber == updatedRoom.RoomNumber);
                    //if (IsExistRoomNumber)
                    //{
                    //    return (false, "Phòng đang được sử dụng không thể chỉnh sửa!");
                    //}

                    room.RoomId = updatedRoom.RoomID;
                    room.RoomNumber = updatedRoom.RoomNumber;
                    room.RoomStatus = updatedRoom.RoomStatus;
                    room.Note = updatedRoom.Note;
                    room.RoomCleaningStatus = updatedRoom.RoomCleaningStatus;
                    room.RoomTypeId = updatedRoom.RoomTypeId;
                   
                    await context.SaveChangesAsync();
                    return (true, "Cập nhật thành công");
                }
            }
            catch (DbEntityValidationException)
            {
                return (false, "DbEntityValidationException");
            }
            catch (DbUpdateException e)
            {
                return (false, $"DbUpdateException: {e.Message}");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống");
            }
        }
    }
}
