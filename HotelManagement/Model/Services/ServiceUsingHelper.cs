using HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Model.Services
{
    public class ServiceUsingHelper
    {
        private static ServiceUsingHelper _ins;
        public static ServiceUsingHelper Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ServiceUsingHelper();
                }
                return _ins;
            }
            private set { _ins = value; }
        }
        public ServiceUsingHelper() { }

        public async Task<(bool, string)> SaveService(ServiceUsingDTO serviceUsingDTO)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    ServiceUsing serviceUsing = new ServiceUsing
                    {
                        RentalContractId = serviceUsingDTO.RentalContractId,
                        ServiceId = serviceUsingDTO.ServiceId,
                        UnitPrice = serviceUsingDTO.UnitPrice,
                        Quantity = serviceUsingDTO.Quantity,
                    };
                    context.ServiceUsings.Add(serviceUsing);
                    await context.SaveChangesAsync();
                    return (true, "Gọi dịch vụ thành công!");
                }
            }
            catch (Exception ex)
            {
                return (false, "Lỗi hệ thống!");
            }
        }

    }
}
