using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorJsFastDataExchangerDemo
{
    public static class LocalFunctions
    {
        public static string GetShortenedItem(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                if (item.Length > 100)
                {
                    return item.Substring(0, 100);
                }
                else
                {
                    return item;
                }

            }
            else
            {
                return "empty";
            }
        }
    }
}
