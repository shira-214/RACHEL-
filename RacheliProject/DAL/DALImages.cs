using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;


namespace DAL
{
    public class DALImages
    {
        public List<Image> GetImages()
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Images
                    .Include(x => x.Apartment)
                    .Include(x => x.Apartment.StreetsName)
                    .Include(x => x.Apartment.City)
                    .ToList();
            }
        }
        public bool AddImages(Image image)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.Images.Add(image);
                db.SaveChanges();
                return true;
            }
        }

        public Image GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Images.Find(id);
            }
        }
        public bool Update(Image entity)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                try
                {
                    db.Images.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                        Debug.WriteLine(ex.InnerException.Message);
                    return false;
                }
            }
        }

        public bool Delete(int idApartment, int numImage)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                Image ent = db.Images.Find(idApartment, numImage);
                if (ent == null)
                    return false;
                db.Images.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }

    }
}
