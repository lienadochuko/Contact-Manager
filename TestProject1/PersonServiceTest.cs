﻿using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;
using Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            _countriesService = new CountriesService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));

            _personService = new PersonService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options), _countriesService);

            _testOutputHelper = testOutputHelper;
        }

        public async Task<List<PersonAddRequest>> CreatePersons()
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

            CountryResponse country_response1 = await _countriesService.AddCountry(countryAddRequest1);
            CountryResponse country_response2 = await _countriesService.AddCountry(countryAddRequest2);
            CountryResponse country_response3 = await _countriesService.AddCountry(countryAddRequest3);


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

            return personAdd_request.ToList();
        }

        #region AddPerson
        //when we supply a null value as PersonAddRequest,
        //it should throw ArgumentNullException
        [Fact]
        public async Task AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? PersonAddRequest = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _personService.AddPerson(PersonAddRequest);

            });
        }

        //when the supplied PersonName is null,
        //it should throw ArgumentException
        [Fact]
        public async Task AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? PersonAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };

            //Act
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _personService.AddPerson(PersonAddRequest);
            });
        }


        //when proper person details are supplied it should
        //insert the person into the Person list and
        //it should return an object of
        //PersonResponse, which should
        //include the newly generated personID
        [Fact]
        public async Task AddPerson_ProperPersonDetails()
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
                await _personService.AddPerson(personAddRequest);

            List<PersonResponse> person_response_to_GetAllPerson =
                await _personService.GetAllPersons();

            //Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, person_response_to_GetAllPerson);
        }
        #endregion


        #region GetPersonByPersonID

        //If we supply null as PersonID, It should return null as PersonResponse
        [Fact]
        public async Task GetPersonByPersonID_NullPerson()
        {
            //Arrange
            Guid? personID = null;

            //Act
            PersonResponse? person_response_from_get = await _personService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(person_response_from_get);
        }

        //If we supply a valid person id, it should return the valid person
        //details as PersonResponse object
        [Fact]
        public async Task GetPersonByPerson_ValidPersonID()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Canada"
            };
            CountryResponse country_response = await _countriesService.AddCountry(countryAddRequest);

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
            PersonResponse personResponse = await _personService.AddPerson(personAddRequest);

            PersonResponse? personResponse_from_Get = await _personService.GetPersonByPersonID(personResponse.PersonID);


            //Assert
            Assert.Equal(personResponse_from_Get, personResponse);
        }
        #endregion

        #region GetAllPerson

        //The GetAllPerson() should return an empty list by default
        [Fact]
        public async Task GetAllPerson_EmptyList()
        {
            //Act
            List<PersonResponse> person_from_get = await _personService.GetAllPersons();

            //Assert
            Assert.Empty(person_from_get);
        }

        //After adding a few persons, we should get the same persons when we call the GetAllPerson()
        [Fact]
        public async Task GetAllPerson_AddFewPersons()
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

            CountryResponse country_response1 = await _countriesService.AddCountry(countryAddRequest1);
            CountryResponse country_response2 = await _countriesService.AddCountry(countryAddRequest2);
            CountryResponse country_response3 = await _countriesService.AddCountry(countryAddRequest3);


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
                PersonResponse person_response = await _personService.AddPerson(indexer);
                person_reponse_list_from_add.Add(person_response);
            }

            //Act
            List<PersonResponse> person_list_from_get = await _personService.GetAllPersons();

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
        public async Task GetFilteredPersons_NullSearch()
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

            CountryResponse country_response1 = await _countriesService.AddCountry(countryAddRequest1);
            CountryResponse country_response2 = await _countriesService.AddCountry(countryAddRequest2);
            CountryResponse country_response3 = await _countriesService.AddCountry(countryAddRequest3);


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
                PersonResponse person_response = await _personService.AddPerson(indexer);
                person_reponse_list_from_add.Add(person_response);
            }



            //print both the expected and actual value
            _testOutputHelper.WriteLine("Expected");
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //Act
            List<PersonResponse> person_list_from_search =
                 await _personService.GetFilteredPersons(nameof(Person.PersonName), "");


            _testOutputHelper.WriteLine("Actual");
            foreach (PersonResponse person_response_from_add in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }
            //Assert
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                Assert.Contains(person_response_from_add, person_list_from_search);
            }
        }


        //First we will add a few persons; and then we will search based on person name
        //with some search string. It should return the matching persons
        [Fact]
        public async Task GetFilteredPersons_SearchByPersonName()
        {
            List<PersonAddRequest> personAdd_request = await CreatePersons();

            List<PersonResponse> person_reponse_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest indexer in personAdd_request)
            {
                PersonResponse person_response = await _personService.AddPerson(indexer);
                person_reponse_list_from_add.Add(person_response);
            }




            //print both the expected and actual value
            _testOutputHelper.WriteLine("Expected");
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //Act
            List<PersonResponse> person_list_from_search =
                 await _personService.GetFilteredPersons(nameof(Person.PersonName), "a");


            _testOutputHelper.WriteLine("Actual");
            foreach (PersonResponse person_response_from_add in person_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //Assert
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                if (person_response_from_add.PersonName != null)
                {
                    if (person_response_from_add.PersonName.Contains("a",
                    StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person_response_from_add, person_list_from_search);
                    }
                }
            }
        }
        #endregion'



        #region GetSortedPersons
        //when we sort based on the person name in DESC order
        //,it should return person list in descending order on PersonName
        [Fact]
        public async Task GetSortedPersons()
        {
            //Arrange
            List<PersonAddRequest> personAdd_request = await CreatePersons();

            List<PersonResponse> person_reponse_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest indexer in personAdd_request)
            {
                PersonResponse person_response = await _personService.AddPerson(indexer);
                person_reponse_list_from_add.Add(person_response);
            }

            //print both the expected and actual value
            _testOutputHelper.WriteLine("Expected");
            foreach (PersonResponse person_response_from_add in person_reponse_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            List<PersonResponse> allPersons = await _personService.GetAllPersons();

            //Act
            List<PersonResponse> person_list_from_sort =
                await _personService.GetSortedPersons(allPersons,
                nameof(Person.PersonName), SortOrderOptions.DESC);


            _testOutputHelper.WriteLine("Actual");
            foreach (PersonResponse person_response_from_get in person_list_from_sort)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            person_reponse_list_from_add = person_reponse_list_from_add.
                OrderByDescending(temp => temp.PersonName).ToList();

            //Assert
            for (int i = 0; i < person_reponse_list_from_add.Count; i++)
            {
                Assert.Equal(person_reponse_list_from_add[i], person_list_from_sort[i]);
            }
        }
        #endregion


        #region UpdatePerson
        //when we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public async Task UpdatePerson_NullPerson()
        {
            //Arrange
            PersonUpdateRequest? person_update_request = null;


            //Assert
           await Assert.ThrowsAsync<ArgumentNullException>(async () => {
                //Act
                await _personService.UpdatePerson(person_update_request);
            });
        }

        [Fact]
        //when invalid PersonID is supplied it should throw ArgumentExceptionError
        public async Task UpdatePerson_InvalidPersonID()
        {
            //Arrange
            PersonUpdateRequest? person_update_request = new PersonUpdateRequest()
            {
                PersonID = Guid.NewGuid()
            };


            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => {
                //Act
               await _personService.UpdatePerson(person_update_request);
            });
        }

        [Fact]
        //when invalid PersonName is null it should throw ArgumentException
        public async Task UpdatePerson_PersonNameIsNull()
        {

            //Arrange

            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Morocco"
            };

            CountryResponse country_response = await _countriesService.AddCountry(countryAddRequest);


            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Boli",
                Email = "boli.me@gmail.com",
                Address = "bolivar street, lampshire, England",
                Gender = GenderOptions.Male,
                DOB = DateTime.Parse("2000-01-09"),
                CountryID = country_response.CountryID,

            };
            PersonResponse person_reponse_from_add = await _personService.AddPerson(personAddRequest);

            PersonUpdateRequest person_update_reuqest = person_reponse_from_add.ToPersonUpdateRequest();
            person_update_reuqest.PersonName = null;


            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _personService.UpdatePerson(person_update_reuqest);
            });
        }

        [Fact]
        //when invalid PersonName is null it should throw ArgumentException
        public async Task UpdatePerson_PersonFullDetailsUpdate()
        {

            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Morocco"
            };

            CountryResponse country_response = await _countriesService.AddCountry(countryAddRequest);


            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Boli",
                Email = "boli.me@gmail.com",
                Address = "bolivar street, lampshire, England",
                Gender = GenderOptions.Male,
                DOB = DateTime.Parse("2000-01-09"),
                CountryID = country_response.CountryID,
                RecieveNewsLetter = true,

            };


            PersonResponse person_reponse_from_add = await _personService.AddPerson(personAddRequest);

            _testOutputHelper.WriteLine("Origanal");
            _testOutputHelper.WriteLine(person_reponse_from_add.ToString());

            PersonUpdateRequest person_update_request = person_reponse_from_add.ToPersonUpdateRequest();
            person_update_request.PersonName = "Bolivar";
            person_update_request.Email = "bolivar.me@gmail.com";

            //Act
            PersonResponse person_reponse_from_update = await _personService.UpdatePerson(person_update_request);
            _testOutputHelper.WriteLine("Actual");
           _testOutputHelper.WriteLine(person_reponse_from_update.ToString());

            PersonResponse? person_response_from_getPerson =  await _personService.GetPersonByPersonID(person_update_request.PersonID);
            _testOutputHelper.WriteLine("Expected");
            _testOutputHelper.WriteLine(person_response_from_getPerson?.ToString());


            //Assert
            Assert.Equal(person_response_from_getPerson, person_reponse_from_update);
        }


        #endregion


        #region DeletPerson

        //when an valid PersonID is supplied it should return true
        [Fact]
        public async Task DeletePerson_ValidPerson()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "England"
            };

            CountryResponse country_response = await _countriesService.AddCountry(countryAddRequest);


            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Boli",
                Email = "boli.me@gmail.com",
                Address = "bolivar street, lampshire, England",
                Gender = GenderOptions.Male,
                DOB = DateTime.Parse("2000-01-09"),
                CountryID = country_response.CountryID,

            };
            PersonResponse person_reponse_from_add = await _personService.AddPerson(personAddRequest);

            //Act
            bool isDeleted = await _personService.DeletePerson(person_reponse_from_add.PersonID);

            //Assert
            Assert.True(isDeleted);
        }

        //when an invalid PersonID is supplied it should return false
        [Fact]
        public async Task DeletePerson_InvalidPerson()
        {            
            //Act
            bool isDeleted = await _personService.DeletePerson(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }
        #endregion
    }
}
