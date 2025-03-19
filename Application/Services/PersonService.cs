using Application.Interfaces;
using Domain.Entities;
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

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
