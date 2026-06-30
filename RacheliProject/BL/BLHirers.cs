using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLHirers
    {
        public DTOHirers ConvertToDTO(Hirer hrr)
        {
            DTOHirers hrrDTO = new DTOHirers();
            hrrDTO.C_IDHirer = hrr.C_IDHirer;
            hrrDTO.NameHirer = hrr.NameHirer;
            hrrDTO.FhoneNumberHirer= hrr.FhoneNumberHirer;
            return hrrDTO;


        }


        public Hirer ConvertToDAL(DTOHirers hrrDTO)
        {
            Hirer hrrEF = new Hirer();
            hrrEF.C_IDHirer = hrrDTO.C_IDHirer;
            hrrEF.NameHirer = hrrDTO.NameHirer;
            hrrEF.FhoneNumberHirer = hrrDTO.FhoneNumberHirer;
            return hrrEF;


        }
        public bool AddHirers(DTOHirers hirers)
        {
            return new DALHirers().AddHirers(ConvertToDAL(hirers));
        }

        public List<DTOHirers> GetHirers()
        {
            //ליצור ליסט שיחזור
            List<DTOHirers> hirers = new List<DTOHirers>();
            //המרה בלולאה

            foreach (Hirer chirer in new DALHirers().GetHirer())
            {
                //והוספה לליסט המרה
                hirers.Add(ConvertToDTO(chirer));
            }
            return hirers;
            //להפעיל את הפעולות שבדאל

        }
        public bool UpdateHirers(DTOHirers hirer)
        {
            return new DALHirers().Update(ConvertToDAL(hirer));

        }

    }
}

