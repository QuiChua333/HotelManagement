using HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<List<ServiceUsingDTO>> GetListUsingService(string rentalContractId)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                   
                    var listUsingService = await context.ServiceUsings.Where(x=> x.RentalContractId ==rentalContractId).Select(x=> new ServiceUsingDTO 
                    {
                        RentalContractId= x.RentalContractId,
                        ServiceId= x.ServiceId,
                        ServiceName = x.Service.ServiceName,
                        ServiceType = x.Service.ServiceType,
                        UnitPrice = x.Service.ServicePrice,
                        Quantity = x.Quantity,
                    }).ToListAsync();

                    var listUsingService2 = listUsingService.Where(x => x.ServiceName != "Giặt sấy")
                                                            .GroupBy(x => x.ServiceId)
                                                            .Select(t => new ServiceUsingDTO
                                                            {
                                                                RentalContractId = t.First().RentalContractId,
                                                                ServiceId = t.First().ServiceId,
                                                                ServiceName = t.First().ServiceName,
                                                                ServiceType = t.First().ServiceType,
                                                                UnitPrice = t.First().UnitPrice,
                                                                Quantity = t.Sum(g => g.Quantity)
                                                            }).ToList();
                    foreach (var item in listUsingService)
                    {
                        if (item.ServiceName == "Giặt sấy")
                        {
                            listUsingService2.Insert(0,item);
                        }
                    }
                    return listUsingService2;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
