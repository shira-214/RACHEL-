using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOApartments
    {
        public int IdApartment { get; set; }
        public string NameOwner { get; set; }
        public DTOStreetsNames IdStreet { get; set; }
        public string NumberHouse { get; set; }
        public int Floor { get; set; }
        public int NumberRooms { get; set; }
        public int NumberBeds { get; set; }
        public int MinimumPrice { get; set; }
        public int ExtraForBed { get; set; }
        public bool Status { get; set; }
        public string note { get; set; }
        public DTOCities IdCities { get; set; }
        public string PhoneOwner { get; set; }


    }
}
