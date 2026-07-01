using System;
using System.Drawing;
using System.IO;

namespace BL
{
    public class ImageClass
    {
        public static string GetProjectRoot()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 6 && !string.IsNullOrEmpty(dir); i++)
            {
                if (Directory.Exists(Path.Combine(dir, "Pictures")))
                    return dir;
                dir = Directory.GetParent(dir)?.FullName;
            }

            return @"C:\Users\1\Downloads\פרוייקט";
        }

        public static string GetCurrentPath()
        {
            string root = GetProjectRoot();
            return root.EndsWith("\\") ? root : root + "\\";
        }

        public static string GetPicturesPath()
        {
            string path = Path.Combine(GetProjectRoot(), "Pictures");
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
            string path = Path.Combine(GetPicturesPath(), fileName);
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

            string path = Path.Combine(GetPicturesPath(), fileName);
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            return null;
        }
    }
}
