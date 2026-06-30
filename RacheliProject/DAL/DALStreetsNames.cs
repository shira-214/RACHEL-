using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;



namespace DAL
{
    public class DALStreetsNames
    {
        public List<StreetsName> GetStreetsNames()
        {
           RacheliEntities db = new RacheliEntities();
            
                return db.StreetsNames.ToList();

           
        }
        public bool AddStreetsNames(StreetsName streetsName)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.StreetsNames.Add(streetsName);
                db.SaveChanges();
                return true;
            }
        }

        public StreetsName GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.StreetsNames.Find(id);
            }
        }
        public bool Update(StreetsName entity)
        {
            RacheliEntities db = new RacheliEntities();
            try
            {
                db.StreetsNames.Attach(entity);
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

        public bool Delete(int id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                StreetsName ent = db.StreetsNames.Find(id);
                if (ent == null)
                    return false;
                db.StreetsNames.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }

    }
}
