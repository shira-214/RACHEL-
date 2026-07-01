using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;


namespace DAL
{
    public class DALRentings
    {
        public List<Renting> GetRentings()
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Rentings
                    .Include(r => r.Hirer)
                    .Include(r => r.Apartment)
                    .Include(r => r.Apartment.StreetsName)
                    .Include(r => r.Apartment.City)
                    .ToList();
            }
        }
        public bool AddRentings(Renting renting)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.Rentings.Add(renting);
                db.SaveChanges();
                return true;
            }
        }
        public Renting GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Rentings.Find(id);
            }
        }
        public bool Update(Renting entity)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                try
                {
                    db.Rentings.Attach(entity);
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

        public bool Delete(int id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                Renting ent = db.Rentings.Find(id);
                if (ent == null)
                    return false;
                db.Rentings.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }

    }
}
