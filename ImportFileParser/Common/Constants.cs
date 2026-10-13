using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFileParser.Common
{
    public static class Constants
    {
        public const string CONST_ALL_VALUE = "ALL";
        public const string CONST_EMPTY_VALUE = "NUL";
        public const string CONST_DEFAULT_VALUE = "DFL";

        public const string CONST_RUR = "RUR";
        public const string CONST_EUR = "EUR";
        public const string CONST_USD = "USD";

        public const string CONST_SHOP = "SHP";

        public const string CONST_CATEGORY_INT = "INT";

        public static bool IsEmpty(string p_Value)
        {
            if (string.IsNullOrEmpty(p_Value)) return true;
            if (p_Value == CONST_EMPTY_VALUE) return true;

            return false;
        }
    }
}
