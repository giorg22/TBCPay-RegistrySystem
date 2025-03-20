using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class PersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _repository;
        private readonly string _imageDirectory = "wwwroot/uploads";

        public PersonService(
            IUnitOfWork unitOfWork,
            IPersonRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task AddRelationshipAsync(int personId, int relatedPersonId, RelationType relationshipType)
        {
            await _repository.AddRelationshipAsync(personId, relatedPersonId, relationshipType);
        }
        public async Task RemoveRelationshipAsync(int personId, int relatedPersonId)
        {
            await _repository.RemoveRelationshipAsync(personId, relatedPersonId);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _unitOfWork.People.GetAllAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _unitOfWork.People.GetByIdAsync(id);
        }

        public async Task AddPersonAsync(Person person)
        {
            await _unitOfWork.People.AddAsync(person);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _unitOfWork.People.Update(person);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _unitOfWork.People.GetByIdAsync(id);
            if (person != null)
            {
                _unitOfWork.People.Delete(person);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task UploadProfilePictureAsync(int personId, IFormFile imageFile)
        {
            var person = await _unitOfWork.People.GetByIdAsync(personId);
            if (person == null)
            {
                throw new Exception("Person not found");
            }

            if (!Directory.Exists(_imageDirectory))
                Directory.CreateDirectory(_imageDirectory);

            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(_imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            person.ProfilePicture = $"/uploads/{fileName}";
            _unitOfWork.People.Update(person);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Person>> SearchAsync(string? firstName, string? lastName, string? personalNumber, int page, int pageSize)
        {
            return await _repository.SearchAsync(firstName, lastName, personalNumber, page, pageSize);
        }

        public async Task<IEnumerable<Person>> AdvancedSearchAsync(PersonSearchCriteria criteria, int page, int pageSize)
        {
            return await _repository.AdvancedSearchAsync(criteria, page, pageSize);
        }
    }
}
