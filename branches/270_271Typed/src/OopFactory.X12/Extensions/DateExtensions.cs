using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Extensions
{
    public static class DateExtensions
    {
        public static string ToX12String(this DateTime? dt)
        {
            return ToX12String(dt.GetValueOrDefault());
        }

        public static string ToX12String(this DateTime dt)
        {
            if (default(DateTime).Year == dt.Year)
            {
                return null;
            }
            else
                return dt.ToString("yyyyMMdd");
        }
    }
}
