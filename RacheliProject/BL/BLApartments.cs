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
            //להפעיל את הפעולות שבדאל

      
        }

        //public DTOApartments GetCityByCode(int? code)
        //{
        //    DALApartments = new DALApartments();
        //    Apartment result = DALApartments.GetById(code);
        //    return convertToDTO(result);
        //}
        //public bool UpdateCity(DTOApartments dto)
        //{
        //    DALApartments dal = new DALApartments();
        //    //ממירים את האוביקט שקיבלנו לשכבת הנתונים דטה
        //    Apartment data = convertToData(dto);
        //    return dal.Update(data);
        //}
        //public bool DeleteCity(int code)
        //{
        //    DALApartments dal = new DALApartments();
        //    return dal.Delete(code);
        //}
    }

    //public DTOApartments GetApartmentById(string id)
    //{
    //    DALApartments dal = new DALApartments();
    //    Apartment result = dal.GetById(id);
    //    return ConvertToDTO(result);
    //}

    //public bool UpdateApartments(DTOApartments dto)
    //{
    //    DALApartments dal = new DALApartments();
    //    //ממירים את האוביקט שקיבלנו לשכבת הנתונים דטה
    //    Apartment data = conver(dto);
    //    return dal.Update(data);
    //}

    ////public bool DeleteUser(string id)
    ////{
    ////    UserDAL dal = new UserDAL();
    //    return dal.Delete(id);
    //}


}




