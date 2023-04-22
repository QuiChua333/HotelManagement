using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HotelManagement.DTOs
{
    public class FurnitureDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public byte[] FurnitureAvatarData { get; set; }
        private ImageSource furnitureAvatar;
        public ImageSource FurnitureAvatar
        {
            get { return furnitureAvatar; }
            set { SetField(ref furnitureAvatar, value, "FurnitureAvatar"); }
        } 
        public string FurnitureID { get; set; }
        public string FurnitureName { get; set; }
        public string FurnitureType { get; set; }
        public int Quantity { get; set; }
        public FurnitureDTO()
        {
            FurnitureName = "";
            FurnitureType = "";
            Quantity = 0;
        }
        public void SetAvatar()
        {
            FurnitureAvatar = LoadAvatarImage(FurnitureAvatarData);
        }
        public BitmapImage LoadAvatarImage(byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Position = 0;

            Image img = Image.FromStream(stream);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            bitmapImage.Freeze();
            return bitmapImage;
        }
    }
}
