using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonRepository
    {
        Task AddRelationshipAsync(int personId, int relatedPersonId, RelationType relationshipType);
        Task RemoveRelationshipAsync(int personId, int relatedPersonId);
        Task<IEnumerable<Person>> SearchAsync(string? firstName, string? lastName, string? personalNumber, int page, int pageSize);
        Task<IEnumerable<Person>> AdvancedSearchAsync(PersonSearchCriteria criteria, int page, int pageSize);
        Task<Person?> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        void Update(Person person);
        void Delete(Person person);
    }
}
