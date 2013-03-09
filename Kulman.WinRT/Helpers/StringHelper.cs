using System;
using System.Text;

namespace Kulman.WinRT.Helpers
{
    public static class StringHelper
    {
        private static readonly Random Rng = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwzxy123456789";

        public static string RandomString(int size)
        {
            var buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = Chars[Rng.Next(Chars.Length)];
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
