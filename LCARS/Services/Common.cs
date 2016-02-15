using System;
using System.Text;

namespace LCARS.Services
{
    public static class Common
    {
        public static string GetEncodedCredentials(string username, string password)
        {
            var mergedCredentials = $"{username}:{password}";
            var byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }
}