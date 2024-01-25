using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PersonsRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Person> AddPerson(Person person)
        {
            _db.Persons.Add(person);
            await _db.SaveChangesAsync();

            return person;
        }

        public async Task<bool> DeletePersonByPersonID(Guid personID)
        {
            _db.Persons.Remove(await _db.Persons.FirstAsync(temp => temp.PersonID == personID));
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Person>> GetAllPerson()
        {
            List<Person> persons = await _db.Persons.Include("Country").ToListAsync();

            return persons;
        }

        public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            List<Person> persons = await _db.Persons.Where(predicate).
                Include("Country").ToListAsync();

            return persons;
        }

        public async Task<Person?> GetPersonByPersonID(Guid personID)
        {
           Person? person = await _db.Persons.Include("Country").FirstOrDefaultAsync(temp => temp.PersonID == personID);

            return person;
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(temp => temp.PersonID == person.PersonID);

            if (matchingPerson == null)
            return person;

            matchingPerson.PersonName = person.PersonName;
            matchingPerson.Email = person.Email;
            matchingPerson.DOB = person.DOB;
            matchingPerson.CountryID = person.CountryID;
            matchingPerson.NIN = person.NIN;
            matchingPerson.Gender = person.Gender;
            matchingPerson.Address = person.Address;
            matchingPerson.RecieveNewsLetter = person.RecieveNewsLetter;

            _db.Update(matchingPerson);
            await _db.SaveChangesAsync();

            return matchingPerson;
        }
    }
}
