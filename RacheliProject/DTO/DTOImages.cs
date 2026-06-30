using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOImages
    {
        public DTOApartments IdApartement { get; set; }
        public int NumImage { get; set; }
        public byte[] Image1 { get; set; }
        public bool Stataus { get; set; }


    }
}
