using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulman.WinRT.Helpers
{
    public static class StringHelper
    {
        private static readonly Random _rng = new Random();
        private static string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwzxy123456789";

        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

        public static string RemoveDiacritics(this String s)
        {
            byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(s);
            return Encoding.UTF8.GetString(tempBytes, 0, tempBytes.Length);
        }
    }
}
