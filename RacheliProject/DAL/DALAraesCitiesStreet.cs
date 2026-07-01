using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;

namespace DAL
{
    public class DALAraesCitiesStreet
    {
        public List<AraesCitiesStreet> GetAraesCitiesStreet()
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.AraesCitiesStreets
                    .Include(x => x.StreetsName)
                    .Include(x => x.City)
                    .Include(x => x.Area)
                    .ToList();
            }
        }
        public bool AddAraesCitiesStreet(AraesCitiesStreet araesCitiesStreet)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.AraesCitiesStreets.Add(araesCitiesStreet);
                db.SaveChanges();
                return true;
            }
        }

        public AraesCitiesStreet GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.AraesCitiesStreets.Find(id);
            }
        }
        public bool Update(AraesCitiesStreet entity)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                try
                {
                    db.AraesCitiesStreets.Attach(entity);
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

        public bool Delete(int idStreet, int idCities, int idArea)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                AraesCitiesStreet ent = db.AraesCitiesStreets.FirstOrDefault(x =>
                    x.IdStreet == idStreet && x.IdCities == idCities && x.IdArea == idArea);
                if (ent == null)
                    return false;
                db.AraesCitiesStreets.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }

    }
}
