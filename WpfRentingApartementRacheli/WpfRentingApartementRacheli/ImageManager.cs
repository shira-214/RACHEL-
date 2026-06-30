using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfRentingApartementRacheli
{
    public class ImageManager
    {
        public static byte[] UploadImage_Dlg()
        {
            string filename = null;
            // יצירת אוביקט שיודע לפתוח חלון 
            OpenFileDialog dlg = new OpenFileDialog();
            // קביעת מסנן לבחירת קובץ רק סיומות אלו יוכלו להיבחר 
            dlg.Filter = "All Images | *.jpg;*.jpeg;*.tif;*.tiff;*.bmp;*.png" +
                "|JPEG Files (*.jpeg)|*.jpeg" +
                "|PNG Files (*.png)|*.png" +
                "|JPG Files (*.jpg)|*.jpg" +
                "|GIF Files (*.gif)|*.gif";
            //פותח חלונית בחירת תמונה ומחזיר האם נבחרה תמונה 
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filename = dlg.FileName;
                byte[] imgArray = File.ReadAllBytes(filename);
                return imgArray;
            }
            return null;
        }
        public static BitmapFrame GetImage(byte[] imageArr)
        {
            if (imageArr == null)
                return null;
            using (var ms = new MemoryStream(imageArr))
            {
                return BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            }
        }

    }
}
