using HotelManagement.DTOs;
using HotelManagement.Utils;
using MaterialDesignThemes.Wpf;
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
        public async Task AutoUpdateDb()
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    var roomRentingList = await context.Rooms.Where(x=> x.RoomStatus == ROOM_STATUS.RENTING).Select(x=> x.RoomId).ToListAsync();


                    var list1 = await context.RentalContracts.ToListAsync();
                    var rentalContractListId = list1.Where(x => x.CheckOutDate + x.StartTime <= DateTime.Today + DateTime.Now.TimeOfDay
                    && !roomRentingList.Contains(x.RoomId)).Select(x=> x.RentalContractId).ToList();
                    string t = "";
                    for (int i = 0; i < rentalContractListId.Count; i++)
                    {

                        t += $@"'{rentalContractListId[i]}'";
                        if (i != rentalContractListId.Count - 1)
                        {
                            t += ",";
                        }
                    }
                    var sql1 = $@"Update [RentalContract] SET Validated = 0  WHERE RentalContractId IN ({t})";
                    await context.Database.ExecuteSqlCommandAsync(sql1);


                    list1 = await context.RentalContracts.ToListAsync();
                    var roomListId = list1.Where(x => x.CheckOutDate + x.StartTime <= DateTime.Today + DateTime.Now.TimeOfDay && !roomRentingList.Contains(x.RoomId)).Select(x => x.RoomId).ToList();
                    t = "";
                    for (int i = 0; i < roomListId.Count; i++)
                    {

                        t += $@"'{roomListId[i]}'";
                        if (i != roomListId.Count - 1)
                        {
                            t += ",";
                        }
                    }
                    var sql2 = $@"Update [Room] SET RoomStatus = N'{ROOM_STATUS.READY}'  WHERE RoomID  IN ({t})";
                    await context.Database.ExecuteSqlCommandAsync(sql2);

                    list1 = await context.RentalContracts.ToListAsync();
                    roomListId = list1.Where(x => x.CheckOutDate + x.StartTime > DateTime.Today + DateTime.Now.TimeOfDay && x.StartDate + x.StartTime <= DateTime.Today + DateTime.Now.TimeOfDay && !roomRentingList.Contains(x.RoomId)).Select(x => x.RoomId).ToList();
                    t = "";
                    for (int i = 0; i < roomListId.Count; i++)
                    {

                        t += $@"'{roomListId[i]}'";
                        if (i != roomListId.Count - 1)
                        {
                            t += ",";
                        }
                    }
                    sql2 = $@"Update [Room] SET RoomStatus = N'{ROOM_STATUS.BOOKED}'  WHERE RoomID  IN ({t})";
                    await context.Database.ExecuteSqlCommandAsync(sql2);

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
                    int roomNumber =  context.Rooms.Select(x => x.RoomTypeId == RoomTypeId).Count();
                    

                    var roomList = await (from r in context.Rooms join c in context.RentalContracts
                                          on r.RoomId equals c.RoomId into ps from c in ps.DefaultIfEmpty()
                                          //join cu in context.Customers
                                          //on c.CustomerId equals cu.CustomerId 
                                          where r.RoomTypeId == RoomTypeId
                                          orderby r.RoomId 
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
                    List<RoomSettingDTO> roomList2 = new List<RoomSettingDTO>();
                    var t = DateTime.Today + DateTime.Now.TimeOfDay;
                    Dictionary<string, List<RoomSettingDTO>> dic = new Dictionary<string, List<RoomSettingDTO>>();
                    foreach (var item in roomList)
                    {
                        if (!dic.Keys.Contains(item.RoomId))
                        {
                            dic.Add(item.RoomId, new List<RoomSettingDTO>());
                        }
                    }
                    foreach (var item in dic.Keys)
                    {
                        foreach (var room in roomList)
                        {
                            if (room.RoomId == item)
                            {
                                dic[item].Add(room);
                            }
                        }
                    }
                    foreach (var item in dic.Values)
                    {
                        if (item.Count > 1)
                        {
                            item.Sort();
                        }
                    }
                    foreach (var item in dic.Values)
                    {
                        if (item.Count > 1)
                        {
                            bool flat = false;
                            foreach (var item2 in item)
                            {
                                if (item2.StartDate + item2.StartTime <= t && t < item2.CheckOutDate + item2.StartTime)
                                {
                                    roomList2.Add(item2);
                                    flat= true;
                                    break;
                                }
                                
                            }
                            if (flat==false) roomList2.Add(item[0]);


                        }
                        else roomList2.Add(item[0]);
                    }
                    
                    return roomList2;
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
        public int GetPersonNumber(string rentalContractId)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    int number =  context.RoomCustomers.Where(x => x.RentalContractId == rentalContractId).Select(x => x.CustomerId).Count();
                    return number;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
