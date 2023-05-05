using HotelManagement.DTOs;
using HotelManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Model.Services
{
    public class BillService
    {
        public BillService() { }
        private static BillService _ins;
        public static BillService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }
        

        //public async Task<List<BillDTO>> GetBillsByRentalContracts(List<string> rentalContractIds)
        //{
        //    try
        //    {
        //        using (var context = new HotelManagementEntities())
        //        {
        //            List<BillDTO> listBillDTO = new List<BillDTO>();
        //            foreach (var item in rentalContractIds)
        //            {
        //                var itemBillDTO = await (from r in context.Rooms
        //                                         join c in context.RentalContracts
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
