using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public static class LocalExtensions
    {
        public static string Repeat(this char value, int count)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count; i++)
                builder.Append(value);
            return builder.ToString();
        }
    }
}
