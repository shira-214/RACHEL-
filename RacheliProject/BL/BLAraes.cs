using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using static DAL.DALApartments;

namespace BL
{
    public class BLAraes

    {
        public bool UpdateArea(DTOAreas area)
        {
            return new DALAraes().Update(ConvertToDAL(area));

        }

        public DTOAreas ConvertToDTO(Area ar)
        {
            DTOAreas arDTO = new DTOAreas();
            arDTO.IdArea = ar.IdArea;
            arDTO.NameArea = ar.NameArea;
            return arDTO;

        }

        public Area ConvertToDAL(DTOAreas arDTO)
        {
            Area arEF = new Area();
            arEF.IdArea = arDTO.IdArea;
            arEF.NameArea = arDTO.NameArea;
            return arEF;


        }
        public bool AddAraes(DTOAreas areas)
        {
            return new DALAraes().AddAraes(ConvertToDAL(areas));
        }
        public List<DTOAreas> GetAreas()
        {
            //ליצור ליסט שיחזור
            List<DTOAreas> Araes = new List<DTOAreas>();
            //המרה בלולאה

            foreach (Area carea in new DALAraes().GetAraes())
            {
                //והוספה לליסט המרה
                Araes.Add(ConvertToDTO(carea));
            }
            return Araes;
            //להפעיל את הפעולות שבדאל

        }

        public bool DeleteArea(int id)
        {
            return new DALAraes().Delete(id);
        }
    }

}





