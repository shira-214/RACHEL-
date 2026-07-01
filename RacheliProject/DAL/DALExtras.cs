using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;


namespace DAL
{
    public class DALExtras
    {
        public List<Extra> GetExtra()
        {
            RacheliEntities db = new RacheliEntities();
            
                return db.Extras.ToList();

            
        }
        public bool AddExtras(Extra extra)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.Extras.Add(extra);
                db.SaveChanges();
                return true;
            }
        }
        public Extra GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Extras.Find(id);
            }
        }
        public bool Update(Extra entity)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                try
                {
                    db.Extras.Attach(entity);
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
                Extra ent = db.Extras.Find(id);
                if (ent == null)
                    return false;
                db.Extras.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }


    }
}
