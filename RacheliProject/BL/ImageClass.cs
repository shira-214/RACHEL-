using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ImageClass
    {
        private const string BasePath = @"C:\Users\1\Downloads\פרוייקט\";

        public static string GetCurrentPath()
        {
            return BasePath;
        }

        private static string GetPicturesPath()
        {
            string path = BasePath + @"Pictures";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string NextName()
        {
            string path = GetPicturesPath();
            string[] files = Directory.GetFiles(path);
            return "image" + (files.Length + 1) + ".jpg";
        }

        public static string SaveImage(byte[] imageArray)
        {
            if (imageArray == null)
                return null;

            string fileName = NextName();
            string path = GetPicturesPath() + @"\" + fileName;
            using (var stream = new MemoryStream(imageArray))
            using (Image img = Image.FromStream(stream))
            {
                img.Save(path);
            }
            return fileName;
        }

        public static byte[] GetImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            string path = GetPicturesPath() + @"\" + fileName;
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            return null;
        }
    }
}
