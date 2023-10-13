using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using ServiceContracts.Enums;

namespace Services
{
    public class PersonService : IPersonServices
    {
        //private field
        private readonly List<Person> _persons;
       private readonly ICountriesService _countriesService;

        public PersonService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }

        //convert the Person object into PersonResponse type
        private PersonResponse ConvertPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.
                GetCountryByCountryID(personResponse.CountryID)?.CountryName;

            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
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
            _persons.Add(person);

            //convert the Person object into PersonResponse type
         return ConvertPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(person => person.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

           Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);
            if(person == null)
                return null;


            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPerson = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPerson;

            switch(searchBy)
            {
                case nameof(Person.PersonName): matchingPerson = allPersons.Where(temp =>
                (!string.IsNullOrEmpty(temp.PersonName)? temp.PersonName.Contains(
                    searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(Person.Email):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(Person.DOB):
                    matchingPerson = allPersons.Where(temp =>
                    (temp.DOB != null) ?
                    (temp.DOB.Value.ToString("dd MMMM yyyy").Contains(
                        searchString, StringComparison.OrdinalIgnoreCase)) : true).ToList();

                    break;


                case nameof(Person.Gender):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(Person.CountryID):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(Person.Address):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                default: matchingPerson = allPersons; break;
            }
            return matchingPerson;
        }

        public List<PersonResponse> GetSortedPersons
            (List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrderOptions)
        {
            throw new NotImplementedException();
        }
    }
}
