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
            RacheliEntities db = new RacheliEntities();
            
                return db.AraesCitiesStreets.ToList();

            
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
            RacheliEntities db = new RacheliEntities();
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
        //public bool Delete(string id)
        //{
        //    using (RacheliEntities db = new RacheliEntities())
        //    {
        //        AraesCitiesStreet ent = db.AraesCitiesStreets.Find(id);
        //        if (ent == null)
        //            return false;
        //        db.Areas.Remove(ent);
        //        db.SaveChanges();
        //        return true;
        //    }
        //}

    }
}
