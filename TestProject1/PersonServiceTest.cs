using System;
using System.Collections.Generic;
using Xunit;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace TestProject1
{
    public class PersonServiceTest
    {
        //private fields
        private readonly IPersonServices _personService;

        public PersonServiceTest()
        {
            _personService = new PersonService();
        }

        #region
        //when we supply a null value as PersonAddRequest,
        //it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? PersonAddRequest = null;
                        
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _personService.AddPerson(PersonAddRequest);

            });
        }

        //when the supplied PersonName is null,
        //it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? PersonAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _personService.AddPerson(PersonAddRequest);
            });
        }


        //when proper person details are supplied it should
        //insert the person into the Person list and
        //it should return an object of
        //PersonResponse, which should
        //include the newly generated personID
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Khalid",
                Email = "khalid@gmail.com",
                Address = ".gfgfgfgfg",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DOB = DateTime.Parse("2003-03-03"),
                RecieveNewsLetter = true,
            };

            //Act
            PersonResponse person_response_from_add = 
                _personService.AddPerson(personAddRequest);

            List<PersonResponse> person_response_to_GetAllPerson = 
                _personService.GetAllPersons();

            //Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, person_response_to_GetAllPerson);
        }
        #endregion

    }
}
