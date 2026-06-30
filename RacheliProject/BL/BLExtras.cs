using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BL
{
    public class BLExtras
    {
        public DTOExtras ConvertToDTO(Extra extr)
        {

            DTOExtras extrDTO = new DTOExtras();
            extrDTO.IdExtra = extr.IdExtra;
            extrDTO.NameExtra = extr.NameExtra;
            return extrDTO;

        }
        public Extra ConvertToDAL(DTOExtras extrDTO)
        {
            Extra extrEF = new Extra();
            extrEF.IdExtra = extrDTO.IdExtra;
            extrEF.NameExtra = extrDTO.NameExtra;
            return extrEF;


        }
        public bool AddExtras(DTOExtras extras)
        {
            return new DALExtras().AddExtras(ConvertToDAL(extras));
        }
        public List<DTOExtras> GetExtras()
        {
            //ליצור ליסט שיחזור
            List<DTOExtras> extras = new List<DTOExtras>();
            //המרה בלולאה

            foreach (Extra cextra in new DALExtras().GetExtra())
            {
                //והוספה לליסט המרה
                extras.Add(ConvertToDTO(cextra));
            }
            return extras;
            //להפעיל את הפעולות שבדאל

        }
        public bool Updatextras(DTOExtras extra)
        {
            return new DALExtras().Update(ConvertToDAL(extra));

        }

        public bool DeleteExtra(int id)
        {
            return new DALExtras().Delete(id);
        }

    }
}
