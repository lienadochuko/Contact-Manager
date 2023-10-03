using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;

namespace Services
{
    public class PersonService : IPersonServices
    {
        //private field
        private readonly List<Person> _persons;
       private readonly ICountriesService _countries;

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //Validate personAddRequest
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Validate PersonName
            if (personAddRequest.PersonName == null)
            {
                throw new ArgumentException(nameof(personAddRequest.PersonName));
            }

            //Convert personAddRequest into Person type
           Person person = personAddRequest.ToPerson();

            //generate personID
            person.PersonID = Guid.NewGuid();

            //add person to person list
            _persons.Add(person);

            //convert the Person object into PersonResponse type
            PersonResponse personResponse = person.ToPersonResponse();

            personResponse.Country = 
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
