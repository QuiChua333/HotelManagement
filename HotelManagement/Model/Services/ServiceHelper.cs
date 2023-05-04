using HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Model.Services
{
    public class ServiceHelper
    {
        private ServiceHelper() { }
        private static ServiceHelper _ins;
        public static ServiceHelper Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new ServiceHelper();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<(bool, string, List<ServiceDTO>, List<ServiceDTO>)> GetAllService()
        {
            try
            {
                List<ServiceDTO> listProduct = new List<ServiceDTO>();
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    listProduct = await (
                        from p in db.Services
                        select new ServiceDTO
                        {
                            ServiceId = p.ServiceId,
                            ServiceName = p.ServiceName,
                            ServiceAvatarData = p.ServiceAvatar,
                            ServiceType = p.ServiceType,
                            Quantity = (int)p.GoodsStorage.QuantityService,
                            ServicePrice = (double)p.ServicePrice,
                        }
                    ).ToListAsync();
                    listProduct.ForEach(item => item.SetAvatar());
                }

                List<ServiceDTO> cleanService = listProduct.Where(item => item.ServiceType == "Vệ sinh").ToList();

                return (true, "Thành công", listProduct, cleanService);
            }
            catch (EntityException e)
            {
                return (false, "Mất kết nối cơ sở dữ liệu", null, null);
            }
            catch (Exception ex)
            {
                return (false, "Lỗi hệ thống", null, null);
            }
        }

        public async Task<(bool, string)> SaveEditProduct(ServiceDTO productSelected)
        {
            try
            {
                using(HotelManagementEntities db = new HotelManagementEntities())
                {
                    Service service = await db.Services.FirstOrDefaultAsync(item => item.ServiceId == productSelected.ServiceId);

                    if (service == null)
                        return (false, "Không tìm thấy sản phẩm trong cơ sở dữ liệu");

                    service.ServiceAvatar = productSelected.ServiceAvatarData;
                    service.ServiceName = productSelected.ServiceName;
                    service.ServiceType = productSelected.ServiceType;
                    service.ServicePrice = productSelected.ServicePrice;

                    await db.SaveChangesAsync();

                    return (true, "Cập nhật sản phẩm thành công");
                }    
            }
            catch(EntityException ex)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch(Exception e)
            {
                return (false, "Lỗi hệ thống");
            }
        }

        public async Task<(bool, string)> AddProduct(ServiceDTO productSelected)
        {
            try
            {
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    Service service = await db.Services.FirstOrDefaultAsync(item => item.ServiceName == productSelected.ServiceName && item.ServiceType == productSelected.ServiceType);

                    if (service != null)
                        return (false, "Đã có sản phẩm trong cơ sở dữ liệu");
    
                    int ID = db.Services.ToList().Count();
                    string mainID;
                    if (ID == 0)
                        mainID = "0001";
                    else
                    {
                        ID = int.Parse(db.Services.ToList().Max(item => item.ServiceId));
                        mainID = getID(++ID);
                    }    

                    Service newService = new Service
                    {
                        ServiceId = mainID,
                        ServiceName = productSelected.ServiceName,
                        ServiceType = productSelected.ServiceType,
                        ServicePrice = productSelected.ServicePrice,
                        ServiceAvatar = productSelected.ServiceAvatarData,
                    };

                    GoodsStorage newGoodStorage = new GoodsStorage
                    {
                        ServiceId = mainID,
                        QuantityService = 0
                    };

                    db.Services.Add(newService);
                    db.GoodsStorages.Add(newGoodStorage);
                    await db.SaveChangesAsync();

                    productSelected.ServiceId = mainID;
                    return (true, "Thêm sản phẩm thành công");
                }
            }
            catch (EntityException ex)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống");
            }
        }

        public async Task<(bool, string)> DeleteService(ServiceDTO serviceSelected)
        {
            try
            {
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    Service service = await db.Services.FirstOrDefaultAsync(item => item.ServiceId == serviceSelected.ServiceId);
                    if (service == null)
                        return (false, "Không tìm thấy sản phẩm trong cơ sở dữ liệu");

                    db.GoodsStorages.Remove(service.GoodsStorage);
                    db.Services.Remove(service);

                    await db.SaveChangesAsync();
                    return (true, "Xóa sản phẩm thành công");
                }
            }
            catch (EntityException e)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống");
            }
        }
        public string getID(int id)
        {
            if (id < 10)
                return "000" + id;
            if (id < 100)
                return "00" + id;
            if (id < 1000)
                return "0" + id;

            return id.ToString();
        }

        public async Task<(bool, string)> ImportService(ServiceDTO serviceSelected, double importPrice, int importQuantity)
        {
            try
            {
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    Service service = await db.Services.FirstOrDefaultAsync(item => item.ServiceId == serviceSelected.ServiceId);
                    if (service == null)
                        return (false, "Không tìm thấy sản phẩm trong cơ sở dữ liệu");

                    int ID = db.GoodsReceipts.ToList().Count();
                    string mainID;
                    if (ID == 0)
                        mainID = "0001";
                    else
                    {
                        ID = int.Parse(db.GoodsReceipts.ToList().Max(item => item.GoodsReceiptId));
                        mainID = getID(++ID);
                    }

                    GoodsReceipt goodReceipt = new GoodsReceipt
                    {
                        GoodsReceiptId = mainID,
                        ServiceId = serviceSelected.ServiceId,
                        ImportPrice = importPrice,
                        Quantity = importQuantity,
                        CreateAt = DateTime.Now,
                    };
                    db.GoodsReceipts.Add(goodReceipt);
                    service.GoodsStorage.QuantityService = service.GoodsStorage.QuantityService + importQuantity;
                    await db.SaveChangesAsync();
                    return (true, "Nhập sản phẩm thành công");
                }
            }
            catch (EntityException e)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống");
            }
        }
        public List<ServiceDTO> GetAllServiceByType(string typeSelection, ObservableCollection<ServiceDTO> allService)
        {
            try
            {
                return allService.Where(item => item.ServiceType == typeSelection).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ServiceDTO> GetCleaningService()
        {
            try
            {
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    var service = await db.Services.Select(x => new ServiceDTO
                    {
                        ServiceId = x.ServiceId,
                        ServiceType = x.ServiceType,
                        ServiceName = x.ServiceName,
                        ServicePrice = (double)x.ServicePrice,
                    }).FirstOrDefaultAsync(x => x.ServiceName == "Dọn dẹp");
                    return service;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ServiceDTO> GetLaundryService()
        {
            try
            {
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    var service = await db.Services.Select(x => new ServiceDTO
                    {
                        ServiceId = x.ServiceId,
                        ServiceType = x.ServiceType,
                        ServiceName = x.ServiceName,
                        ServicePrice = (double)x.ServicePrice,
                    }).FirstOrDefaultAsync(x => x.ServiceName == "Giặt sấy");
                    return service;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
