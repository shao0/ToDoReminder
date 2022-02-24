using System.Security.Cryptography;
using System.Text;

namespace ToDoReminder.Server.Extensions
{
    public static class Helpers
    {
        public static string ToMD5(this string source)=> Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Default.GetBytes(source)));
    }
}
