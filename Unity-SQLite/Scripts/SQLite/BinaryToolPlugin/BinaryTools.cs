using System;

namespace SQLite
{
    public static class BinaryTools
    {
        public static string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }

        public static byte[] StringToBytes(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);
        }
    }
}