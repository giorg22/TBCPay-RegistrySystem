using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Gender Gender { get; set; }
        public required string PersonalNumber { get; set; }
        public required DateTime BirthDate { get; set; }
        public string? ProfilePicture { get; set; }
        public int CityId { get; set; }
        public ICollection<PersonRelationship> RelatedPersons { get; set; } = new List<PersonRelationship>();
    }
}
