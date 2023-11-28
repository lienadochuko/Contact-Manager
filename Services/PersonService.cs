using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PersonService : IPersonServices
    {
        //private field
        //private readonly List<Person> _persons;
        private readonly PersonsDbContext _db;
        private readonly ICountriesService _countriesService;

        public PersonService(PersonsDbContext personsDbContext, ICountriesService countriesService)
        {
            //_persons = new List<Person>();

            _db = personsDbContext;
            _countriesService = countriesService;

        }

        public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
        {
            //Validate personAddRequest
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model Validate PersonName
            ValidationHelper.ModelValidation(personAddRequest);


            //Convert personAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            //generate personID
            person.PersonID = Guid.NewGuid();

            //add person to person list
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            //_db.sp_InsertPerson(person);

            //convert the Person object into PersonResponse type
            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetAllPersons()
        {
            var persons = await _db.Persons.Include("country").ToListAsync();
            //SELECT * from Persons
            return persons.
                Select(person => person.ToPersonResponse()).ToList();


            //using storeProcedure
            //return _db.sp_GetAllPersons().Select(person =>  person.ToPersonResponse()).ToList();
        }

        public async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = await _db.Persons.Include("country").FirstOrDefaultAsync(temp => temp.PersonID == personID);
            if (person == null)
                return null;


            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = await GetAllPersons();
            List<PersonResponse> matchingPerson = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPerson;

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    matchingPerson = allPersons.Where(temp =>
                (!string.IsNullOrEmpty(temp.PersonName) ? temp.PersonName.Contains(
                    searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(PersonResponse.Email):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(PersonResponse.DOB):
                    matchingPerson = allPersons.Where(temp =>
                    (temp.DOB != null) ?
                    (temp.DOB.Value.ToString("dd MMMM yyyy").Contains(
                        searchString, StringComparison.OrdinalIgnoreCase)) : true).ToList();

                    break;


                case nameof(PersonResponse.Gender):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(PersonResponse.CountryID):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(PersonResponse.Address):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                default: matchingPerson = allPersons; break;
            }
            return matchingPerson;
        }

        public async Task<List<PersonResponse>> GetSortedPersons
            (List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonResponse> sortedPerson = (sortBy, sortOrder)
                switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DOB), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.DOB).ToList(),

                (nameof(PersonResponse.DOB), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.DOB).ToList(),


                (nameof(PersonResponse.Age), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.RecieveNewsLetter), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.RecieveNewsLetter).ToList(),

                (nameof(PersonResponse.RecieveNewsLetter), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.RecieveNewsLetter).ToList(),


                _ => allPersons
            };

            return sortedPerson;
        }

        public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            if (personUpdateRequest.PersonID == new Guid())
                throw new ArgumentException(nameof(personUpdateRequest.PersonID));

            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(temp => temp.PersonID == personUpdateRequest.PersonID);
            if (matchingPerson == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DOB = personUpdateRequest.DOB;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.NIN = personUpdateRequest.NIN;
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.RecieveNewsLetter = personUpdateRequest.RecieveNewsLetter;

            //Console.WriteLine(personUpdateRequest.NIN);

            await _db.SaveChangesAsync();
            //_db.sp_UpdatePerson(matchingPerson);

            return matchingPerson.ToPersonResponse();
        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));


            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(temp => temp.PersonID == personID);
            if (matchingPerson == null)
            {
                return false;
            }

            _db.Persons.Remove(await _db.Persons.FirstAsync(temp => temp.PersonID == personID));
            await _db.SaveChangesAsync();
            //_db.sp_DeletePerson(matchingPerson);


            return true;
        }
    }
}
