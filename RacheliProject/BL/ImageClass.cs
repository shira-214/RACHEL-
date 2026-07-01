using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BL
{
    public class ImageClass
    {
        public static string GetProjectRoot()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 10 && !string.IsNullOrEmpty(dir); i++)
            {
                if (Directory.Exists(Path.Combine(dir, "WpfRentingApartementRacheli")) &&
                    Directory.Exists(Path.Combine(dir, "RacheliProject")))
                    return dir;

                if (File.Exists(Path.Combine(dir, "README.md")) &&
                    Directory.Exists(Path.Combine(dir, "Pictures")))
                    return dir;

                dir = Directory.GetParent(dir)?.FullName;
            }

            dir = AppDomain.CurrentDomain.BaseDirectory;
            string outermostWithPictures = null;
            for (int i = 0; i < 10 && !string.IsNullOrEmpty(dir); i++)
            {
                if (Directory.Exists(Path.Combine(dir, "Pictures")))
                    outermostWithPictures = dir;
                dir = Directory.GetParent(dir)?.FullName;
            }

            return outermostWithPictures ?? AppDomain.CurrentDomain.BaseDirectory;
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
            int max = Directory.GetFiles(path, "image*.jpg")
                .Select(f => Path.GetFileNameWithoutExtension(f))
                .Select(name =>
                {
                    if (name.StartsWith("image") && int.TryParse(name.Substring(5), out int n))
                        return n;
                    return 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return "image" + (max + 1) + ".jpg";
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
                img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return fileName;
        }

        public static byte[] GetImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            string dir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 10 && !string.IsNullOrEmpty(dir); i++)
            {
                string path = Path.Combine(dir, "Pictures", fileName);
                if (File.Exists(path))
                    return File.ReadAllBytes(path);
                dir = Directory.GetParent(dir)?.FullName;
            }

            return null;
        }
    }
}
