using ServiceContracts;
using Entities;
using Services;
using ServiceContracts.DTO;
using System;
using Xunit;

namespace TestProject1
{
    public class CountryServiceTest
    {
        private readonly ICountriesService _countriesService;
        //constructor
        public CountryServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        //when CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        //when the CountryName is null throw argumentException

        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest()
            {
                CountryName = null
            };

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        //When the CountryName is duplicate throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
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
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
                _countriesService.AddCountry(request2);
            });
        }

        //when you supply proper countryName it should imsert it to the list if countries
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest()
            {
                CountryName = "JAPAN"
            };

            //Act
            CountryResponse response = _countriesService.AddCountry(request);

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
        }
        #endregion

        #region GetAllCountries
        [Fact]
        //The list should be empty by default
        public void GetAllCountries_EmptyList()
        {

        }

        #endregion
    }
}
