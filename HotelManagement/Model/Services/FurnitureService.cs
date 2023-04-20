using HotelManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;
using System.Runtime.Remoting.Contexts;
using System.Collections.ObjectModel;

namespace HotelManagement.Model.Services
{
    public class FurnitureService
    {
        private FurnitureService() { }
        private static FurnitureService _ins;
        public static FurnitureService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new FurnitureService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<(bool , string ,List<FurnitureDTO>)> GetAllFurniture()
        {
            try
            {
                List<FurnitureDTO> listFurniture = new List<FurnitureDTO>();
                using (HotelManagementEntities db = new HotelManagementEntities())
                {
                    listFurniture = await (
                        from p in db.Furnitures
                        select new FurnitureDTO
                        {
                            FurnitureAvatarData = p.FurnitureAvatar,
                            FurnitureName = p.FurnitureName,
                            FurnitureType = p.FurnitureType,
                            Quantity = (int)p.FurnitureStorage.QuantityFurniture,
                        }
                    ).ToListAsync();
                }
                for (int i = 0; i < listFurniture.Count; i++)
                    listFurniture[i].SetAvatar();
                return (true, "", listFurniture);
            }
            catch(EntityException e)
            {
                return (false, "Mất kết nối cơ sở dữ liệu", null);
            }
            catch(Exception ex)
            {
                return (false, "Lỗi hệ thống", null);
            }
        }

        public List<FurnitureDTO> GetAllFurnitureByType(string typeSelection, ObservableCollection<FurnitureDTO> allFurniture)
        {
            try
            {
                return allFurniture.Where(item => item.FurnitureType == typeSelection).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ImageSource LoadImage(string ImagePath)
        {
            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.CacheOption = BitmapCacheOption.None;
            _image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            _image.CacheOption = BitmapCacheOption.OnLoad;
            _image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            _image.UriSource = new Uri(ImagePath, UriKind.RelativeOrAbsolute);
            _image.EndInit();
            return _image;
        }

        public BitmapImage LoadAvatarImage(byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Position = 0;

            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
