using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BL
{
    public class BLCities
    {
        public DTOCities ConvertToDTO(City cty)
        {
            DTOCities ctyDTO = new DTOCities();
            ctyDTO.IdCity = cty.IdCity;
            ctyDTO.NameCity = cty.NameCity;
            return ctyDTO;


        }


        public City ConvertToDAL(DTOCities ctyDTO)
        {
            City ctyEF = new City();
            ctyEF.IdCity = ctyDTO.IdCity;
            ctyEF.NameCity = ctyDTO.NameCity;
            return ctyEF;


        }

        public bool AddCities(DTOCities cities)
        {
            return new DALCities().AddCities(ConvertToDAL(cities));
        }
        public List<DTOCities> GetCities()
        {
            List<DTOCities> cities = new List<DTOCities>();

            foreach (City ccity in new DALCities().GetCity())
            {
                cities.Add(ConvertToDTO(ccity));
            }
            return cities;

        }
        public bool UpdateCities(DTOCities city)
        {
            return new DALCities().Update(ConvertToDAL(city));

        }

        public bool DeleteCity(int id)
        {
            return new DALCities().Delete(id);
        }

    }
}
