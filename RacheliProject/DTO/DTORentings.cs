using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTORentings
    {
        public int IdRenting { get; set; }
        public DTOHirers IdHirer { get; set; }
        public DTOApartments KodHapartment { get; set; }
        public System.DateTime Date { get; set; }
        public int SumPayment { get; set; }
        public int SumBeds { get; set; }


    }
}
