using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLRentings
    {
        public DTORentings ConvertToDTO(Renting rntn)
        {
            DTORentings rntnDTO = new DTORentings();
            rntnDTO.IdRenting = rntn.IdRenting;
            rntnDTO.IdHirer = new BLHirers().ConvertToDTO(rntn.Hirer);
            rntnDTO.KodHapartment = new BLApartments().ConvertToDTO(rntn.Apartment);
            rntnDTO.Date = rntn.Date;
            rntnDTO.SumPayment = rntn.SumPayment;
            rntnDTO.SumBeds = rntn.SumBeds;

            return rntnDTO;

        }

        public Renting ConvertToDAL(DTORentings rntnDTO)
        {
            Renting rntnEF = new Renting();
            rntnEF.IdRenting = rntnDTO.IdRenting;
            rntnEF.IdHirer = rntnDTO.IdHirer.C_IDHirer;
            rntnEF.KodHapartment = rntnDTO.KodHapartment.IdApartment;
            rntnEF.Date = rntnDTO.Date;
            rntnEF.SumPayment = rntnDTO.SumPayment;
            rntnEF.SumBeds = rntnDTO.SumBeds;

            return rntnEF;


        }

        public bool AddRentings(DTORentings Rentings)
        {
            return new DALRentings().AddRentings(ConvertToDAL(Rentings));
        }

        public List<DTORentings> GetRentings()
        {
            //ליצור ליסט שיחזור
            List<DTORentings> rentings = new List<DTORentings>();
            //המרה בלולאה

            foreach (Renting crentings in new DALRentings().GetRentings())
            {
                //והוספה לליסט המרה
                rentings.Add(ConvertToDTO(crentings));
            }
            return rentings;
            //להפעיל את הפעולות שבדאל

        }

        public bool UpdateRentings(DTORentings rentings)
        {
            return new DALRentings().Update(ConvertToDAL(rentings));

        }

    }
}
