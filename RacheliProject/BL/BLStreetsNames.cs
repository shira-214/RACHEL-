using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.DALApartments;

namespace BL
{
    public class BLStreetsNames
    {
        public DTOStreetsNames ConvertToDTO(StreetsName strtnm)
        {
            DTOStreetsNames strtnmDTO = new DTOStreetsNames();
            strtnmDTO.IdStreet = strtnm.IdStreet;
            strtnmDTO.StreetName = strtnm.StreetName;
            return strtnmDTO;

        }

        public StreetsName ConvertToDAL(DTOStreetsNames strtnmDTO)
        {
            StreetsName strtnmEF = new StreetsName();
            strtnmEF.IdStreet = strtnmDTO.IdStreet;
            strtnmEF.StreetName = strtnmDTO.StreetName;
            return strtnmEF;


        }
        public bool AddStreetsNames(DTOStreetsNames StreetsNames)
        {
            return new DALStreetsNames().AddStreetsNames(ConvertToDAL(StreetsNames));
        }

        public List<DTOStreetsNames> GetStreetsNames()
        {
            //ליצור ליסט שיחזור
            List<DTOStreetsNames> streetsNames = new List<DTOStreetsNames>();
            //המרה בלולאה

            foreach (StreetsName cStreetsName in new DALStreetsNames().GetStreetsNames())
            {
                //והוספה לליסט המרה
                streetsNames.Add(ConvertToDTO(cStreetsName));
            }
            return streetsNames;
            //להפעיל את הפעולות שבדאל

        }
        public bool UpdateStreetsNames(DTOStreetsNames streetsName)
        {
            return new DALStreetsNames().Update(ConvertToDAL(streetsName));

        }

        public bool DeleteStreetsName(int id)
        {
            return new DALStreetsNames().Delete(id);
        }

    }
}

