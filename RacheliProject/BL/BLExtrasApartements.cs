using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BLExtrasApartements
    {
        public DTOExtrasApartements ConvertToDTO(ExtrasApartement ExtrAprtmnt)
        {
            DTOExtrasApartements ExtrAprtmntDTO = new DTOExtrasApartements();
            ExtrAprtmntDTO.IdAapartment =new BLApartments().ConvertToDTO( ExtrAprtmnt.Apartment);
            ExtrAprtmntDTO.IdExtra = new BLExtras().ConvertToDTO(ExtrAprtmnt.Extra );
            ExtrAprtmntDTO.Status = ExtrAprtmnt.Status;
            return ExtrAprtmntDTO;


        }
        

        public ExtrasApartement ConvertToDAL(DTOExtrasApartements ExtrAprtmntDTO)
        {
            ExtrasApartement ExtrAprtmntEF = new ExtrasApartement();
            ExtrAprtmntEF.IdAapartment = ExtrAprtmntDTO.IdAapartment.IdApartment;
            ExtrAprtmntEF.IdExtra = ExtrAprtmntDTO.IdExtra.IdExtra;
            ExtrAprtmntEF.Status = ExtrAprtmntDTO.Status;
            return ExtrAprtmntEF;


        }
        public bool AddExtrasApartements(DTOExtrasApartements extrasApartements)
        {
            return new DALExtrasApartements().AddExtrasApartements(ConvertToDAL(extrasApartements));
        }
        public List<DTOExtrasApartements> GetTOExtrasApartements()
        {
            //ליצור ליסט שיחזור
            List<DTOExtrasApartements> extrasApartements = new List<DTOExtrasApartements>();
            //המרה בלולאה

            foreach (ExtrasApartement cextrasApartement in new DALExtrasApartements().GetExtrasApartements())
            {
                //והוספה לליסט המרה
                extrasApartements.Add(ConvertToDTO(cextrasApartement));
            }
            return extrasApartements;
            //להפעיל את הפעולות שבדאל

        }

        public bool UpdateExtrasApartements(DTOExtrasApartements ExtrasApartements)
        {
            return new DALExtrasApartements().Update(ConvertToDAL(ExtrasApartements));

        }

    }
}
//public DTOExtras IdExtra { get; set; }

