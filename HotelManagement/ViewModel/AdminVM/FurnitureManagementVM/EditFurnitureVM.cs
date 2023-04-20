using BitMiracle.LibTiff.Classic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel.AdminVM.FurnitureManagementVM
{
    public partial class FurnitureManagementVM: BaseVM
    {
        public List<string> AllFurnitureType { get; set; }
        public void GetAllFurnitureType()
        {
            AllFurnitureType = new List<string>();
            int furnitureCount = AllFurniture.Count;
            for (int i = 0; i < furnitureCount; i++)
            {
                if (AllFurnitureType.Contains(AllFurniture[i].FurnitureType))
                    continue;
                AllFurnitureType.Add(AllFurniture[i].FurnitureType);
            }

        }

    }
}
