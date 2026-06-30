using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<DTOApartments> GetApartments();

        [OperationContract]
        bool AddApartments(DTOApartments apartments);

        [OperationContract]
        List<DTOAreas> GetAreas();

        [OperationContract]
        bool AddArea(DTOAreas areas);

        [OperationContract]
        List<DTOAraesCitiesStreet> GetTOAraesCitiesStreets();

        [OperationContract]
        bool AddAraesCitiesStreet(DTOAraesCitiesStreet araesCitiesStreet);

        [OperationContract]
        List<DTOCities> GetTOCities();

        [OperationContract]
        bool AddCities(DTOCities cities);

        [OperationContract]
        List<DTOExtras> GetExtras();

        [OperationContract]
        bool AddExtras(DTOExtras extras);

        [OperationContract]
        List<DTOExtrasApartements> GetTOExtrasApartements();

        [OperationContract]
        bool AddExtrasApartements(DTOExtrasApartements extrasApartements);

        [OperationContract]
        List<DTOHirers> GetTOHirers();

        [OperationContract]
        bool AddHirers(DTOHirers hirers);

        [OperationContract]
        List<DTOImages> GetImages();

        [OperationContract]
        bool AddImages(DTOImages images);

        [OperationContract]
        List<DTORentings> GetTORentings();

        [OperationContract]
        bool Addentings(DTORentings rentings);

        [OperationContract]
        List<DTOStreetsNames> GetStreetsNames();

        [OperationContract]
        bool AddStreetsNames(DTOStreetsNames streetsNames);
        [OperationContract]
        bool UpdateArae(DTOAreas areas);

        [OperationContract]
        bool UpdateApartments(DTOApartments apartment);

        [OperationContract]
        bool UpdateraesCitiesStreet(DTOAraesCitiesStreet raesCitiesStreet);

        [OperationContract]
        bool UpdateCities(DTOCities city);

        [OperationContract]
        bool Updatextras(DTOExtras extra);

        [OperationContract]
        bool UpdateExtrasApartements(DTOExtrasApartements ExtrasApartements);

        [OperationContract]
        bool UpdateHirers(DTOHirers hirer);

        [OperationContract]
        bool UpdateImages(DTOImages image);

        [OperationContract]
        bool UpdateRentings(DTORentings rentings);

        [OperationContract]
        bool UpdateStreetsNames(DTOStreetsNames streetsName);

        [OperationContract]
        bool DeleteApartment(int id);

        [OperationContract]
        bool DeleteArea(int id);

        [OperationContract]
        bool DeleteCity(int id);

        [OperationContract]
        bool DeleteExtra(int id);

        [OperationContract]
        bool DeleteStreetsName(int id);

        [OperationContract]
        bool DeleteHirer(string id);

        [OperationContract]
        bool DeleteRenting(int id);

        [OperationContract]
        bool DeleteAraesCitiesStreet(int idStreet, int idCities, int idArea);

        [OperationContract]
        bool DeleteExtrasApartement(int idExtra, int idApartment);

        [OperationContract]
        bool DeleteImage(int idApartment, int numImage);

    }
}

