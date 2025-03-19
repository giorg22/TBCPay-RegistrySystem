using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PersonRelationship
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
        public required RelationType RelationType { get; set; }
        public Person? Person { get; set; }
        public Person? RelatedPerson { get; set; }
    }
}
