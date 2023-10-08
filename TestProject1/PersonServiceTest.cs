using System;
using System.Collections.Generic;
using Xunit;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;
using Entities;

namespace TestProject1
{
    public class PersonServiceTest
    {
        //private fields
        private readonly IPersonServices _personService;

        private readonly ICountriesService _countriesService;

        private readonly ITestOutputHelper _testOutputHelper;

        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService();

            _countriesService = new CountriesService();

            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson
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


        #region GetPersonByPersonID

        //If we supply null as PersonID, It should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullPerson()
        {
            //Arrange
            Guid? personID = null;

            //Act
            PersonResponse? person_response_from_get = _personService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(person_response_from_get);
        }

        //If we supply a valid person id, it should return the valid person
        //details as PersonResponse object
        [Fact]
        public void GetPersonByPerson_ValidPersonID()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Canada"
            };
            CountryResponse country_response = _countriesService.AddCountry(countryAddRequest);

            //Act
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Person Name",
                Email = "email@sample.com",
                Address = "my address",
                DOB = DateTime.Parse("2000-07-09"),
                Gender = GenderOptions.Male,
                CountryID = country_response.CountryID,
                RecieveNewsLetter = false

            };
            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            PersonResponse? personResponse_from_Get = _personService.GetPersonByPersonID(personResponse.PersonID);


            //Assert
            Assert.Equal(personResponse_from_Get, personResponse);
        }
        #endregion

        #region GetAllPerson

        //The GetAllPerson() should return an empty list by default
        [Fact]
        public void GetAllPerson_EmptyList()
        {
            //Act
            List<PersonResponse> person_from_get = _personService.GetAllPersons();

            //Assert
            Assert.Empty(person_from_get);
        }

        //After adding a few persons, we should get the same persons when we call the GetAllPerson()
        [Fact]
        public void GetAllPerson_AddFewPersons()
        {

            //Arrange
            CountryAddRequest countryAddRequest1 = new CountryAddRequest()
            {
                CountryName = "Bolivia"
            };

            CountryAddRequest countryAddRequest2 = new CountryAddRequest()
            {
                CountryName = "India"
            };

            CountryAddRequest countryAddRequest3 = new CountryAddRequest()
            {
                CountryName = "Morocco"
            };

            CountryResponse country_response1 = _countriesService.AddCountry(countryAddRequest1);
            CountryResponse country_response2 = _countriesService.AddCountry(countryAddRequest2);
            CountryResponse country_response3 = _countriesService.AddCountry(countryAddRequest3);


            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Boli",
                Email = "boli@sample.com",
                Address = "my address boli",
                DOB = DateTime.Parse("2000-08-09"),
                Gender = GenderOptions.Male,
                CountryID = country_response1.CountryID,
                RecieveNewsLetter = false

            };


            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "India",
                Email = "India@sample.com",
                Address = "my address India",
                DOB = DateTime.Parse("2000-07-09"),
                Gender = GenderOptions.Female,
                CountryID = country_response2.CountryID,
                RecieveNewsLetter = false

            };


            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "Moro",
                Email = "Moro@sample.com",
                Address = "my address",
                DOB = DateTime.Parse("2000-06-09"),
                Gender = GenderOptions.Others,
                CountryID = country_response3.CountryID,
                RecieveNewsLetter = false

            };


            //PersonResponse personResponse1 = _personService.AddPerson(personAddRequest1);
            //PersonResponse personResponse2 = _personService.AddPerson(personAddRequest2);
            //PersonResponse personResponse3 = _personService.AddPerson(personAddRequest3);
            //instead

            List<PersonAddRequest> personAdd_request = new List<PersonAddRequest>()
            {
                personAddRequest1,
                personAddRequest2,
                personAddRequest3
            };

            List<PersonResponse> person_reponse_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest indexer in personAdd_request)
            {
                PersonResponse person_response = _personService.AddPerson(indexer);
                person_reponse_list_from_add.Add(person_response);
            }

            //Act
            List<PersonResponse> person_list_from_get = _personService.GetAllPersons();

            //Assert
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_get);
            }

            //print both the expected and actual value
            _testOutputHelper.WriteLine("Expected");
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            _testOutputHelper.WriteLine("Actual");
            foreach (PersonResponse person_response_from_add in person_list_from_get)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
        }

        #endregion

        #region GetFilteredPersons
        //if the search text is empty and search by it "PersonName",
        //it should return all persons
        [Fact]
        public void GetFilteredPersons_NullSearch()
        {
            //Arrange
            CountryAddRequest countryAddRequest1 = new CountryAddRequest()
            {
                CountryName = "Bolivia"
            };

            CountryAddRequest countryAddRequest2 = new CountryAddRequest()
            {
                CountryName = "India"
            };

            CountryAddRequest countryAddRequest3 = new CountryAddRequest()
            {
                CountryName = "Morocco"
            };

            CountryResponse country_response1 = _countriesService.AddCountry(countryAddRequest1);
            CountryResponse country_response2 = _countriesService.AddCountry(countryAddRequest2);
            CountryResponse country_response3 = _countriesService.AddCountry(countryAddRequest3);


            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Boli",
                Email = "boli@sample.com",
                Address = "my address boli",
                DOB = DateTime.Parse("2000-08-09"),
                Gender = GenderOptions.Male,
                CountryID = country_response1.CountryID,
                RecieveNewsLetter = false

            };


            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "India",
                Email = "India@sample.com",
                Address = "my address India",
                DOB = DateTime.Parse("2000-07-09"),
                Gender = GenderOptions.Female,
                CountryID = country_response2.CountryID,
                RecieveNewsLetter = false

            };


            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "Moro",
                Email = "Moro@sample.com",
                Address = "my address",
                DOB = DateTime.Parse("2000-06-09"),
                Gender = GenderOptions.Others,
                CountryID = country_response3.CountryID,
                RecieveNewsLetter = false

            };


            //PersonResponse personResponse1 = _personService.AddPerson(personAddRequest1);
            //PersonResponse personResponse2 = _personService.AddPerson(personAddRequest2);
            //PersonResponse personResponse3 = _personService.AddPerson(personAddRequest3);
            //instead

            List<PersonAddRequest> personAdd_request = new List<PersonAddRequest>()
            {
                personAddRequest1,
                personAddRequest2,
                personAddRequest3
            };

            List<PersonResponse> person_reponse_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest indexer in personAdd_request)
            {
                PersonResponse person_response = _personService.AddPerson(indexer);
                person_reponse_list_from_add.Add(person_response);
            }

            //Act
            List<PersonResponse> person_list_from_search =
                 _personService.GetFilteredPersons(nameof(Person.PersonName), "");

            //Assert
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_search);
            }

            //print both the expected and actual value
            _testOutputHelper.WriteLine("Expected");
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            _testOutputHelper.WriteLine("Actual");
            foreach (PersonResponse person_response_from_add in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
        }


        //First we will add a few persons; and then we will search based on person name
        //with some search string. It should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            //Arrange
            CountryAddRequest countryAddRequest1 = new CountryAddRequest()
            {
                CountryName = "Bolivia"
            };

            CountryAddRequest countryAddRequest2 = new CountryAddRequest()
            {
                CountryName = "India"
            };

            CountryAddRequest countryAddRequest3 = new CountryAddRequest()
            {
                CountryName = "Morocco"
            };

            CountryResponse country_response1 = _countriesService.AddCountry(countryAddRequest1);
            CountryResponse country_response2 = _countriesService.AddCountry(countryAddRequest2);
            CountryResponse country_response3 = _countriesService.AddCountry(countryAddRequest3);


            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Boli",
                Email = "boli@sample.com",
                Address = "my address boli",
                DOB = DateTime.Parse("2000-08-09"),
                Gender = GenderOptions.Male,
                CountryID = country_response1.CountryID,
                RecieveNewsLetter = false

            };


            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "India",
                Email = "India@sample.com",
                Address = "my address India",
                DOB = DateTime.Parse("2000-07-09"),
                Gender = GenderOptions.Female,
                CountryID = country_response2.CountryID,
                RecieveNewsLetter = false

            };


            PersonAddRequest personAddRequest3 = new PersonAddRequest()
            {
                PersonName = "Moro",
                Email = "Moro@sample.com",
                Address = "my address",
                DOB = DateTime.Parse("2000-06-09"),
                Gender = GenderOptions.Others,
                CountryID = country_response3.CountryID,
                RecieveNewsLetter = false

            };


            //PersonResponse personResponse1 = _personService.AddPerson(personAddRequest1);
            //PersonResponse personResponse2 = _personService.AddPerson(personAddRequest2);
            //PersonResponse personResponse3 = _personService.AddPerson(personAddRequest3);
            //instead

            List<PersonAddRequest> personAdd_request = new List<PersonAddRequest>()
            {
                personAddRequest1,
                personAddRequest2,
                personAddRequest3
            };

            List<PersonResponse> person_reponse_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest indexer in personAdd_request)
            {
                PersonResponse person_response = _personService.AddPerson(indexer);
                person_reponse_list_from_add.Add(person_response);
            }

            //Act
            List<PersonResponse> person_list_from_search =
                 _personService.GetFilteredPersons(nameof(Person.PersonName), "Ma");

            //Assert
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_search);
            }

            //print both the expected and actual value
            _testOutputHelper.WriteLine("Expected");
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                if (person_response_from_add.PersonName != null)
                {
                    if (person_response_from_add.PersonName.Contains("ma",
                    StringComparison.OrdinalIgnoreCase))
                    {
                        _testOutputHelper.WriteLine(person_response_from_add.ToString());
                    }
                }
            }
            _testOutputHelper.WriteLine("Actual");
            foreach (PersonResponse person_response_from_add in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
        }
        #endregion
    }
}
