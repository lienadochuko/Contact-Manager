using ServiceContracts;
using Entities;
using Services;
using ServiceContracts.DTO;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCoreMock;

namespace TestProject1
{
    public class CountryServiceTest
    {
        private readonly ICountriesService _countriesService;
        //constructor
        public CountryServiceTest()
        {
            var countriesInitialData = new List<Country>() { };

            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>( new DbContextOptionsBuilder<ApplicationDbContext>().Options);

            ApplicationDbContext dbContext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);

            _countriesService = new CountriesService(dbContext);
        }

        #region AddCountry
        //when CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
                await _countriesService.AddCountry(request);
            });
        }

        //when the CountryName is null throw argumentException

        [Fact]
        public async Task AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest()
            {
                CountryName = null
            };

            //Assert
           await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                //Act
               await _countriesService.AddCountry(request);
            });
        }

        //When the CountryName is duplicate throw ArgumentException
        [Fact]
        public async Task AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            CountryAddRequest request2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                //Act
                await _countriesService.AddCountry(request);
                await _countriesService.AddCountry(request2);
            });
        }

        //when you supply proper countryName it should imsert it to the list if countries
        [Fact]
        public async Task AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest()
            {
                CountryName = "JAPAN"
            };

            //Act
            CountryResponse response = await _countriesService.AddCountry(request);

            List<CountryResponse> countries_from_GetAllCountries = await _countriesService.GetAllCountries();

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }
        #endregion

        #region GetAllCountries
        [Fact]
        //The list should be empty by default(before adding any countries)
        public async Task GetAllCountries_EmptyList()
        {
            //Acts
            List<CountryResponse> actual_country_response_list = await _countriesService.GetAllCountries();

            //Assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]

        public async Task GetAllCountry_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest(){ CountryName = "USA"},
                new CountryAddRequest(){ CountryName = "SINGAPORE"},
                new CountryAddRequest(){ CountryName = "ALGERIA"}
            };

            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();

            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(await _countriesService.AddCountry(country_request));
            }

           List<CountryResponse> actualCountryResponse = await _countriesService.GetAllCountries();


            //Assert
            foreach(CountryResponse expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponse);
            }
        }

        #endregion


        #region GetCountryID
        [Fact]
        //if we supply null as CountryID, it should return null as CountryResponse 
        public async Task GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countryID = null;

            //Act
            CountryResponse? country_Reponse_from_get_method = 
               await _countriesService.GetCountryByCountryID(countryID);

            //Assert
            Assert.Null(country_Reponse_from_get_method);
        }

        [Fact]
        //if we supply a valid countryID it should return the matching
        //country details as CountryResponse object
        public async Task GetCountryByCountryID_ValidCountryID()
        {
            //Arrange
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "China"
            };

           CountryResponse country_Response_from_add_request = 
                await _countriesService.AddCountry(countryAddRequest);

            //Act
            CountryResponse? country_Response_from_get = 
               await _countriesService.GetCountryByCountryID(country_Response_from_add_request.CountryID);

            //Assert
            Assert.Equal(country_Response_from_add_request, country_Response_from_get);
        }

        #endregion
    }
}
