using System;
using System.Security.Cryptography;
using System.Text;

namespace HL.Core.Application.Commons
{
    public static class Functions
    {
        public static string GetPasswordHash(string userName, string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password));

            var computeHash = md5.ComputeHash(bytes);
            return BitConverter.ToString(computeHash).Replace("-", "");
        }
    }
}
