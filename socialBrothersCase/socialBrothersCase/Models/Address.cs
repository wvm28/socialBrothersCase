using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace socialBrothersCase.Models
{
    public class Address
    {
        public Guid Id { get; init; }
        [Required]
        public string Street { get; init; }
        [Required]
        public int HouseNumber { get; init; }
        [Required]
        [RegularExpression(@"^[1-9][0-9]{3}[ ]{0,1}(?!SA|SD|SS)[A-Z]{2}$")]
        public string PostalCode { get; init; }
        [Required]
        public string Location { get; init; }
        [Required]
        public string Country { get; init; }
           
    }
}
