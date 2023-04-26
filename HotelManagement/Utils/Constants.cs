using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Utils
{
    public class Constants
    {
        public enum Operation
        {
            CREATE,
            READ,
            UPDATE,
            DELETE,
            UPDATE_PASSWORD,
            UPDATE_PROD_QUANTITY
        }
        public class STATUS_ROOM
        {
            public static readonly string IN_PROGRESS_REPAIR = "Đang sửa chữa";
            public static readonly string IN_PROGRESS_CLEAN_UP= "Đang dọn dẹp";
            public static readonly string DONE = "Đã dọn dẹp";
        }
    }
}
