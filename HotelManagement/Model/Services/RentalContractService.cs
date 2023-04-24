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
        public async Task<List<RentalContractDTO>> GetRentalContracts()
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
    }
}
