using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;

namespace DAL
{
    public class DALApartments
    {
        public List<Apartment> GetApartment()
        {
            RacheliEntities db = new RacheliEntities();

            return db.Apartments.ToList();

        }

        public bool AddApartments(Apartment apartment)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.Apartments.Add(apartment);
                db.SaveChanges();
                return true;
            }
        }

        public Apartment GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Apartments.Find(id);
            }
        }
        public bool Update(Apartment entity)
        {
            RacheliEntities db = new RacheliEntities();
            try
            {
                db.Apartments.Attach(entity);
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
        public bool Delete(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                Apartment ent = db.Apartments.Find(id);
                if (ent == null)
                    return false;
                db.Apartments.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }


    }
}



