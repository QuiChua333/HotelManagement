using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace HotelManagement
{
    public enum Operation
    {
        CREATE,
        READ,
        UPDATE,
        DELETE,
        UPDATE_PASSWORD,
        UPDATE_PROD_QUANTITY,
        UPDATECLEAN
    }
    public static class Number
    {
        public static bool IsNumeric(string value)
        {
            return value.All(char.IsNumber) || 
                 ((value.Substring(1, value.Length - 1).All(char.IsNumber) && value[0] == '-'));
        }
        public static bool IsPositive(string value)
        {
            return value[0] != '-' && value != "0";
        }

    }

    public class Helper
    {
        public static string FormatVNMoney(float money)
        {
            if (money == 0)
            {
                return "0 ₫";
            }
            return String.Format(CultureInfo.InvariantCulture,
                                "{0:#,#} ₫", money);
        }

    }
}
