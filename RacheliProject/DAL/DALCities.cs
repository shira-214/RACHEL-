using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;


namespace DAL
{
    public class DALCities
    {
        public List<City> GetCity()
        {
            RacheliEntities db = new RacheliEntities();
            
                return db.Cities.ToList();

            
        }
        public bool AddCities(City city)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.Cities.Add(city);
                db.SaveChanges();
                return true;
            }
        }
        public City GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Cities.Find(id);
            }
        }
        public bool Update(City entity)
        {
            RacheliEntities db = new RacheliEntities();
            try
            {
                db.Cities.Attach(entity);
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
                City ent = db.Cities.Find(id);
                if (ent == null)
                    return false;
                db.Cities.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }



    }
}
