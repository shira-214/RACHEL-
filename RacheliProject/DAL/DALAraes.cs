using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;


namespace DAL
{
    public class DALAraes
    {
        public List<Area> GetAraes()
        {
            RacheliEntities db = new RacheliEntities();
            
                return db.Areas.ToList();

            
        }
        public bool AddAraes(Area area)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.Areas.Add(area);
                db.SaveChanges();
                return true;
            }

        }
        public Area GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.Areas.Find(id);
            }
        }
        public bool Update(Area entity)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                try
                {
                    db.Areas.Attach(entity);
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
                Area ent = db.Areas.Find(id);
                if (ent == null)
                    return false;
                db.Areas.Remove(ent);
                db.SaveChanges();
                return true;
            }
        }


    }
}