using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{

    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PersonalNumber { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
    }
}
