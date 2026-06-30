using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;


namespace BL
{
    public class BLAraesCitiesStreet
    {
        public DTOAraesCitiesStreet ConvertToDTO(AraesCitiesStreet arcty)
        {
            DTOAraesCitiesStreet arctyDTO = new DTOAraesCitiesStreet();
            arctyDTO.IdStreetDTo = new BLStreetsNames().ConvertToDTO(arcty.StreetsName);
            arctyDTO.IdCities = new BLCities().ConvertToDTO(arcty.City);
            arctyDTO.IdArea = new BLAraes().ConvertToDTO(arcty.Area);
            return arctyDTO;


        }

        public AraesCitiesStreet ConvertToDAL(DTOAraesCitiesStreet arctyDTO)
        {
            AraesCitiesStreet arctyEF = new AraesCitiesStreet();
            if (arctyDTO.IdStreetDTo != null)
                arctyEF.IdStreet = arctyDTO.IdStreetDTo.IdStreet;
            if (arctyDTO.IdCities != null)
                arctyEF.IdCities = arctyDTO.IdCities.IdCity;
            if (arctyDTO.IdArea != null)
                arctyEF.IdArea = arctyDTO.IdArea.IdArea;

            return arctyEF;
        }
        public bool AddraesCitiesStreet(DTOAraesCitiesStreet araesCitiesStreet)
        {
            return new DALAraesCitiesStreet().AddAraesCitiesStreet(ConvertToDAL(araesCitiesStreet));
        }


        public List<DTOAraesCitiesStreet> GetAraesCitiesStreets()
        {
            //ליצור ליסט שיחזור
            List<DTOAraesCitiesStreet> caraesCitiesStreets = new List<DTOAraesCitiesStreet>();
            //המרה בלולאה

            foreach (AraesCitiesStreet citiesStreet in new DALAraesCitiesStreet().GetAraesCitiesStreet())
            {
                //והוספה לליסט המרה
                caraesCitiesStreets.Add(ConvertToDTO(citiesStreet));
            }
            return caraesCitiesStreets;
            //להפעיל את הפעולות שבדאל

        }
        public bool UpdateraesCitiesStreet(DTOAraesCitiesStreet raesCitiesStreet)
        {
            return new DALAraesCitiesStreet().Update(ConvertToDAL(raesCitiesStreet));

        }

    }

}

