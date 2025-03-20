using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PersonSearchCriteria
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
    }
}
