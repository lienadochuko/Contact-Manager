using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;

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
            throw new NotImplementedException();
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
    }
}
