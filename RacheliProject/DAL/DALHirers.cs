using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;


namespace DAL
{
    public class DALHirers
    {
        public List<Hirer> GetHirer()
        {
            RacheliEntities db = new RacheliEntities();
            
                return db.Hirers.ToList();
           
        }
        public bool AddHirers(Hirer hirer)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.Hirers.Add(hirer);
                db.SaveChanges();
                return true;
            }
        }
        public Hirer GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Hirers.Find(id);
            }
        }
        public bool Update(Hirer entity)
        {
            RacheliEntities db = new RacheliEntities();
            try
            {
                db.Hirers.Attach(entity);
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
                Hirer ent = db.Hirers.Find(id);
                if (ent == null)
                    return false;
                db.Hirers.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }

    }
}
