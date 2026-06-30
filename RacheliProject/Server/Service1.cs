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
            new BLApartments().AddApartment(apartments);
            return true;
        }

        public List<DTOAreas> GetAreas()
        {
            return new BLAraes().GetAreas();
        }

        public bool AddArea(DTOAreas areas)
        {
            new BLAraes().AddAraes(areas);
            return true;
        }

        public List<DTOAraesCitiesStreet> GetTOAraesCitiesStreets()
        {
            return new BLAraesCitiesStreet().GetAraesCitiesStreets();
        }

        public bool AddAraesCitiesStreet(DTOAraesCitiesStreet araesCitiesStreet)
        {
            new BLAraesCitiesStreet().AddraesCitiesStreet(araesCitiesStreet);
            return true;
        }

        public List<DTOCities> GetTOCities()
        {
            return new BLCities().GetCities();
        }

        public bool AddCities(DTOCities cities)
        {
            new BLCities().AddCities(cities);
            return true;
        }

        public List<DTOExtras> GetExtras()
        {
            return new BLExtras().GetExtras();
        }

        public bool AddExtras(DTOExtras extras)
        {
            new BLExtras().AddExtras(extras);
            return true;
        }

        public List<DTOExtrasApartements> GetTOExtrasApartements()
        {
            return new BLExtrasApartements().GetTOExtrasApartements();
        }

        public bool AddExtrasApartements(DTOExtrasApartements extrasApartements)
        {
            new BLExtrasApartements().AddExtrasApartements(extrasApartements);
            return true;
        }

        public List<DTOHirers> GetTOHirers()
        {
            return new BLHirers().GetHirers();
        }

        public bool AddHirers(DTOHirers hirers)
        {
            new BLHirers().AddHirers(hirers);
            return true;
        }

        public List<DTOImages> GetImages()
        {
            return new BLImages().GetImages();
        }

        public bool AddImages(DTOImages images)
        {
            new BLImages().AddImages(images);
            return true;
        }

        public List<DTORentings> GetTORentings()
        {
            return new BLRentings().GetRentings();
        }

        public bool Addentings(DTORentings rentings)
        {
            new BLRentings().AddRentings(rentings);
            return true;
        }

        public List<DTOStreetsNames> GetStreetsNames()
        {
            return new BLStreetsNames().GetStreetsNames();
        }

        public bool AddStreetsNames(DTOStreetsNames streetsNames)
        {
            new BLStreetsNames().AddStreetsNames(streetsNames);
            return true;
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
