using HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelManagement.Model.Services
{
    public class RoomService
    {
        private static RoomService _ins;
        public static RoomService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new RoomService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        private RoomService() { }
        public async Task<List<RoomDTO>> GetRooms()
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    var roomList = await (from r in context.Rooms
                                          join t in context.RoomTypes
                                          on r.RoomTypeId equals t.RoomTypeId
                                          select new RoomDTO
                                          {
                                              RoomId = r.RoomId,
                                              RoomNumber = r.RoomNumber,
                                              RoomTypeId = r.RoomTypeId,
                                              Note= r.Note,
                                              RoomStatus = r.RoomStatus,
                                              RoomCleaningStatus = r.RoomCleaningStatus,
                                              Price = t.Price,
                                          }
                                          ).ToListAsync();
                    return roomList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<RoomSettingDTO>> GetRoomsByRoomType(string RoomTypeId)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    RoomType rt = await context.RoomTypes.FindAsync(RoomTypeId);
                    var roomList = await (from r in context.Rooms join c in context.RentalContracts
                                          on r.RoomId equals c.RoomId into ps from c in ps.DefaultIfEmpty()
                                          //join cu in context.Customers
                                          //on c.CustomerId equals cu.CustomerId 
                                          where r.RoomTypeId == RoomTypeId
                                          select new RoomSettingDTO
                                          {
                                              RoomId = r.RoomId,
                                              RoomNumber = r.RoomNumber,
                                              RoomTypeId = r.RoomTypeId,
                                              RoomTypeName = rt.RoomTypeName.Trim(),
                                              RoomStatus = r.RoomStatus.Trim(),
                                              RoomCleaningStatus = r.RoomCleaningStatus.Trim(),
                                              StartDate=  c.StartDate,
                                              StartTime=c.StartTime,
                                              CheckOutDate=c.CheckOutDate,
                                              Validated = c.Validated,
                                              //CustomerId = cu.CustomerId,
                                              //CustomerName = cu.CustomerName,

                                          }
                                          ).ToListAsync();
                    return roomList;
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            } 

        }
        public async Task<List<RoomTypeDTO>> GetRoomTypes()
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    var roomTypeList = await (from r in context.RoomTypes
                                          select new RoomTypeDTO
                                          {
                                              RoomTypeId= r.RoomTypeId,
                                              RoomTypeName= r.RoomTypeName.Trim(),
                                              Price= r.Price,
                                          }
                                          ).ToListAsync();
                    return roomTypeList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
