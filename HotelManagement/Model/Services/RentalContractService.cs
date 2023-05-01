using HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Model.Services
{
    public class RentalContractService
    {
        private static RentalContractService _ins;
        public static RentalContractService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new RentalContractService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        private RentalContractService() { }
        public async Task<List<RentalContractDTO>> GetAllRentalContracts()
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    var rentalContractList = await (from r in context.RentalContracts
                                         
                                          select new RentalContractDTO
                                          {
                                             RentalContractId = r.RentalContractId,
                                             StartDate= r.StartDate,
                                             StartTime= r.StartTime,
                                             CheckOutDate= r.CheckOutDate,
                                             CustomerId = r.CustomerId,
                                             RoomId= r.RoomId,
                                             Validated=r.Validated,
                                          }
                                          ).ToListAsync();
                    
                    return rentalContractList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<RentalContractDTO>> GetRentalContractsNow()
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    var rentalContractList = await (from r in context.RentalContracts

                                                    select new RentalContractDTO
                                                    {
                                                        RentalContractId = r.RentalContractId,
                                                        StartDate = r.StartDate,
                                                        StartTime = r.StartTime,
                                                        CheckOutDate = r.CheckOutDate,
                                                        CustomerId = r.CustomerId,
                                                        RoomId = r.RoomId,
                                                        Validated = r.Validated,
                                                    }
                                          ).ToListAsync();
                    rentalContractList = rentalContractList.Where(x => x.CheckOutDate + x.StartTime > DateTime.Today + DateTime.Now.TimeOfDay && x.StartDate + x.StartTime <= DateTime.Today + DateTime.Now.TimeOfDay).ToList();

                    return rentalContractList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
