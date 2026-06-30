using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;


namespace DAL
{
    public class DALExtrasApartements
    {
        public List<ExtrasApartement> GetExtrasApartements()
        {
            RacheliEntities db = new RacheliEntities();
            
                return db.ExtrasApartements.ToList();

            
        }

        public bool AddExtrasApartements(ExtrasApartement extraApartement )
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                db.ExtrasApartements.Add(extraApartement);
                db.SaveChanges();
                return true;
            }
        }
        public ExtrasApartement GetById(string id)
        {
            using (RacheliEntities db = new RacheliEntities())
            {
                return db.ExtrasApartements.Find(id);
            }
        }
        public bool Update(ExtrasApartement entity)
        {
            RacheliEntities db = new RacheliEntities();
            try
            {
                db.ExtrasApartements.Attach(entity);
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
}
