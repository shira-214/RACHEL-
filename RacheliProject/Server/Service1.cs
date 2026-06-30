using BL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using System.Text;


namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public bool UpdateArae(DTOAreas areas)
        {
            return new BLAraes().UpdateArea(areas);

        }

        public bool UpdateApartments(DTOApartments apartments)
        {
            return new BLApartments().UpdateApartments(apartments);

        }
        public bool UpdateraesCitiesStreet(DTOAraesCitiesStreet araesCitiesStreet)
        {
            return new BLAraesCitiesStreet().UpdateraesCitiesStreet(araesCitiesStreet);

        }
        public bool UpdateCities(DTOCities cities)
        {
            return new BLCities().UpdateCities(cities);

        }

        public bool Updatextras(DTOExtras extra)
        {
            return new BLExtras().Updatextras(extra);

        }

        public bool UpdateExtrasApartements(DTOExtrasApartements extrasApartements)
        {
            return new BLExtrasApartements().UpdateExtrasApartements(extrasApartements);

        }

        public bool UpdateHirers(DTOHirers hirer)
        {
            return new BLHirers().UpdateHirers(hirer);

        }

        public bool UpdateImages(DTOImages images)
        {
            return new BLImages().UpdateImages(images);

        }

        public bool UpdateRentings(DTORentings rentings)
        {
            return new BLRentings().UpdateRentings(rentings);

        }

        public bool UpdateStreetsNames(DTOStreetsNames streetsNames)
        {
            return new BLStreetsNames().UpdateStreetsNames(streetsNames);

        }

        public List<DTOApartments> GetApartments()
        {
            return new BLApartments().GetAllApartments();
        }

        public bool AddApartments(DTOApartments apartments)
        {
            try
            {
                return new BLApartments().AddApartment(apartments);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOAreas> GetAreas()
        {
            return new BLAraes().GetAreas();
        }

        public bool AddArea(DTOAreas areas)
        {
            try
            {
                return new BLAraes().AddAraes(areas);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOAraesCitiesStreet> GetTOAraesCitiesStreets()
        {
            return new BLAraesCitiesStreet().GetAraesCitiesStreets();
        }

        public bool AddAraesCitiesStreet(DTOAraesCitiesStreet araesCitiesStreet)
        {
            try
            {
                return new BLAraesCitiesStreet().AddraesCitiesStreet(araesCitiesStreet);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOCities> GetTOCities()
        {
            return new BLCities().GetCities();
        }

        public bool AddCities(DTOCities cities)
        {
            try
            {
                return new BLCities().AddCities(cities);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOExtras> GetExtras()
        {
            return new BLExtras().GetExtras();
        }

        public bool AddExtras(DTOExtras extras)
        {
            try
            {
                return new BLExtras().AddExtras(extras);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOExtrasApartements> GetTOExtrasApartements()
        {
            return new BLExtrasApartements().GetTOExtrasApartements();
        }

        public bool AddExtrasApartements(DTOExtrasApartements extrasApartements)
        {
            try
            {
                return new BLExtrasApartements().AddExtrasApartements(extrasApartements);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOHirers> GetTOHirers()
        {
            return new BLHirers().GetHirers();
        }

        public bool AddHirers(DTOHirers hirers)
        {
            try
            {
                return new BLHirers().AddHirers(hirers);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOImages> GetImages()
        {
            return new BLImages().GetImages();
        }

        public bool AddImages(DTOImages images)
        {
            try
            {
                return new BLImages().AddImages(images);
            }
            catch
            {
                return false;
            }
        }

        public List<DTORentings> GetTORentings()
        {
            return new BLRentings().GetRentings();
        }

        public bool Addentings(DTORentings rentings)
        {
            try
            {
                return new BLRentings().AddRentings(rentings);
            }
            catch
            {
                return false;
            }
        }

        public List<DTOStreetsNames> GetStreetsNames()
        {
            return new BLStreetsNames().GetStreetsNames();
        }

        public bool AddStreetsNames(DTOStreetsNames streetsNames)
        {
            try
            {
                return new BLStreetsNames().AddStreetsNames(streetsNames);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteApartment(int id)
        {
            return new BLApartments().DeleteApartment(id);
        }

        public bool DeleteArea(int id)
        {
            return new BLAraes().DeleteArea(id);
        }

        public bool DeleteCity(int id)
        {
            return new BLCities().DeleteCity(id);
        }

        public bool DeleteExtra(int id)
        {
            return new BLExtras().DeleteExtra(id);
        }

        public bool DeleteStreetsName(int id)
        {
            return new BLStreetsNames().DeleteStreetsName(id);
        }

        public bool DeleteHirer(string id)
        {
            return new BLHirers().DeleteHirer(id);
        }

        public bool DeleteRenting(int id)
        {
            return new BLRentings().DeleteRenting(id);
        }

        public bool DeleteAraesCitiesStreet(int idStreet, int idCities, int idArea)
        {
            return new BLAraesCitiesStreet().DeleteAraesCitiesStreet(idStreet, idCities, idArea);
        }

        public bool DeleteExtrasApartement(int idExtra, int idApartment)
        {
            return new BLExtrasApartements().DeleteExtrasApartement(idExtra, idApartment);
        }

        public bool DeleteImage(int idApartment, int numImage)
        {
            return new BLImages().DeleteImage(idApartment, numImage);
        }


        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
