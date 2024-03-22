using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Data.Helpers
{
    public class Md5Hash
    {
        public static string GetHash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
