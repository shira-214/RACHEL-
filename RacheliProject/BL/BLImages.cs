using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLImages
    {
        public DTOImages ConvertToDTO(Image img)
        {
            DTOImages imgDTO = new DTOImages();
            imgDTO.NumImage = img.NumImage;
            imgDTO.Image1 = ImageClass.GetImage(img.Image1);
            imgDTO.Stataus = img.Stataus;
            imgDTO.IdApartement = new BLApartments().ConvertToDTO(img.Apartment); ;
            return imgDTO;

        }

        public Image ConvertToDAL(DTOImages imDTO)
        {
            Image imEF = new Image();
            imEF.NumImage = imDTO.NumImage;
            imEF.Image1 = ImageClass.SaveImage(imDTO.Image1);
            imEF.Stataus = imDTO.Stataus;
            imEF.IdApartement = imDTO.IdApartement.IdApartment;
            return imEF;


        }
        public bool AddImages(DTOImages images)
        {
            return new DALImages().AddImages(ConvertToDAL(images));
        }

        public List<DTOImages> GetImages()
        {
            //ליצור ליסט שיחזור
            List<DTOImages> images = new List<DTOImages>();
            //המרה בלולאה

            foreach (Image cimage in new DALImages().GetImages())
            {
                //והוספה לליסט המרה
                images.Add(ConvertToDTO(cimage));
            }
            return images;
            //להפעיל את הפעולות שבדאל

        }
        public bool UpdateImages(DTOImages image)
        {
            return new DALImages().Update(ConvertToDAL(image));

        }

    }
}

