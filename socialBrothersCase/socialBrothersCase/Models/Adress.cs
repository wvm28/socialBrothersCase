using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialBrothersCase.Models
{
    public class Adress
    {
        public Guid Id { get; init; }
        public string Street { get; init; }
        public int HouseNumber { get; init; }
        public string PostalCode { get; init; }
        public string Location { get; init; }
        public string Country { get; init; }
           
    }
}
