using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddRelationshipAsync(int personId, int relatedPersonId, RelationType relationshipType)
        {
            var relationship = new PersonRelationship
            {
                PersonId = personId,
                RelatedPersonId = relatedPersonId,
                RelationType = relationshipType
            };

            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRelationshipAsync(int personId, int relatedPersonId)
        {
            var relationship = await _context.Relationships
                .FirstOrDefaultAsync(r => r.PersonId == personId && r.RelatedPersonId == relatedPersonId);

            if (relationship != null)
            {
                _context.Relationships.Remove(relationship);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Person>> SearchAsync(string? firstName, string? lastName, string? personalNumber, int page, int pageSize)
        {
            var query = _context.People.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{firstName}%"));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(p => EF.Functions.Like(p.LastName, $"%{lastName}%"));

            if (!string.IsNullOrWhiteSpace(personalNumber))
                query = query.Where(p => EF.Functions.Like(p.PersonalNumber, $"%{personalNumber}%"));

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> AdvancedSearchAsync(PersonSearchCriteria criteria, int page, int pageSize)
        {
            var query = _context.People.AsQueryable();

            if (!string.IsNullOrWhiteSpace(criteria.FirstName))
                query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{criteria.FirstName}%"));

            if (!string.IsNullOrWhiteSpace(criteria.LastName))
                query = query.Where(p => EF.Functions.Like(p.LastName, $"%{criteria.LastName}%"));

            if (!string.IsNullOrWhiteSpace(criteria.PersonalNumber))
                query = query.Where(p => EF.Functions.Like(p.PersonalNumber, $"%{criteria.PersonalNumber}%"));

            if (criteria.BirthDate != null)
                query = query.Where(p => p.BirthDate == criteria.BirthDate);

            if (criteria.CityId.HasValue)
                query = query.Where(p => p.CityId == criteria.CityId.Value);

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task AddAsync(Person person)
        {
            await _context.People.AddAsync(person);
        }

        public void Update(Person person)
        {
            _context.People.Update(person);
        }

        public void Delete(Person person)
        {
            _context.People.Remove(person);
        }
    }
}
