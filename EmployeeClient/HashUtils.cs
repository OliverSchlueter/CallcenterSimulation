using System;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeClient
{
    public class HashUtils
    {
        private static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static string Sha512(string message)
        {
            byte[] resultData = SHA512Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(message));
            return ByteArrayToString(resultData);
        }

        public static string Sha256(string message)
        {
            byte[] resultData = SHA256Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(message));
            return ByteArrayToString(resultData);
        }

        public static string Md5(string message)
        {
            byte[] resultData = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(message));
            return ByteArrayToString(resultData);
        }
    }
}