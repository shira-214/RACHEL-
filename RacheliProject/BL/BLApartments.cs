using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using static DAL.DALApartments;


namespace BL
{
    public class BLApartments
    {
        public DTOApartments ConvertToDTO(Apartment aprtmnt)
        {
            DTOApartments aprtmntDTO = new DTOApartments();
            aprtmntDTO.IdApartment = aprtmnt.IdApartment;
            aprtmntDTO.IdStreet = new BLStreetsNames().ConvertToDTO(aprtmnt.StreetsName);
            aprtmntDTO.NameOwner = aprtmnt.NameOwner;
            aprtmntDTO.NumberHouse = aprtmnt.NumberHouse;
            aprtmntDTO.NumberBeds = aprtmnt.NumberBeds;
            aprtmntDTO.Floor = aprtmnt.Floor;
            aprtmntDTO.NumberRooms = aprtmnt.NumberRooms;
            aprtmntDTO.MinimumPrice = aprtmnt.MinimumPrice;
            aprtmntDTO.ExtraForBed = aprtmnt.ExtraForBed;
            aprtmntDTO.Status = aprtmnt.Status;
            aprtmntDTO.note = aprtmnt.note;
            aprtmntDTO.PhoneOwner = aprtmnt.PhoneOwner;
            aprtmntDTO.IdCities = new BLCities().ConvertToDTO(aprtmnt.City);
            return aprtmntDTO;


        }

        public Apartment ConvertToDAL(DTOApartments aprtmntDTO)
        {
            Apartment aprtmntEF = new Apartment();
            aprtmntEF.IdApartment = aprtmntDTO.IdApartment;
            aprtmntEF.NameOwner = aprtmntDTO.NameOwner;
            aprtmntEF.NumberHouse = aprtmntDTO.NumberHouse;
            aprtmntEF.Floor = aprtmntDTO.Floor;
            aprtmntEF.NumberRooms = aprtmntDTO.NumberRooms;
            aprtmntEF.NumberBeds = aprtmntDTO.NumberBeds;
            aprtmntEF.MinimumPrice = aprtmntDTO.MinimumPrice;
            aprtmntEF.ExtraForBed = aprtmntDTO.ExtraForBed;
            aprtmntEF.Status = aprtmntDTO.Status;
            aprtmntEF.note = aprtmntDTO.note;
            aprtmntEF.PhoneOwner = aprtmntDTO.PhoneOwner;
            if (aprtmntDTO.IdStreet != null)
                aprtmntEF.IdStreet = aprtmntDTO.IdStreet.IdStreet;
            if (aprtmntDTO.IdCities != null)
                aprtmntEF.IdCities = aprtmntDTO.IdCities.IdCity;
            return aprtmntEF;
        }
        public bool UpdateApartments(DTOApartments apartment)
        {
            return new DALApartments().Update(ConvertToDAL(apartment));

        }

        public bool AddApartment(DTOApartments apartments)
        {
            return new DALApartments().AddApartments(ConvertToDAL(apartments));
        }

        public bool DeleteApartment(int id)
        {
            return new DALApartments().Delete(id);
        }
        public List<DTOApartments> GetAllApartments()
        {
            //ליצור ליסט שיחזור
            List<DTOApartments> Apartments = new List<DTOApartments>();
            //המרה בלולאה

            foreach (Apartment cApartments in new DALApartments().GetApartment())
            {
                //והוספה לליסט המרה
                Apartments.Add(ConvertToDTO(cApartments));
            }
            return Apartments;
        }
    }
}
