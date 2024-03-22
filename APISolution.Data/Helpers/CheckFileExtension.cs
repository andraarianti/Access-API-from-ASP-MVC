using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISolution.Data.Helpers
{
    public class CheckFileExtension
    {
        public static bool IsImageFile(string filePath)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string extension = Path.GetExtension(filePath);
            return imageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
