using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using OfficeOpenXml.Drawing;
using Microsoft.Extensions.Logging;
using RepositoryContract_s_;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Metrics;
using Exceptions;

namespace Services
{
    public class PersonDeleterService : IPersonDeleterServices
    {
        //private field
        //private readonly List<Person> _persons;
        private readonly IPersonRepository _personRepository;
        private readonly ICountriesRepository _countriesRepository;
        private readonly ICountriesService _countriesService;
        //private readonly ILogger<PersonDeleterService> _logger;

        public PersonDeleterService(IPersonRepository personRepository,
            ICountriesRepository countriesRepository)
        {
            //_persons = new List<Person>();
            //ILogger<PersonDeleterService> logger

            _personRepository = personRepository;
            _countriesRepository = countriesRepository;
            //_logger = logger;
        }


        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));


            Person? matchingPerson = await _personRepository.GetPersonByPersonID(personID.Value);
            if (matchingPerson == null)
            {
                return false;
            }

            await _personRepository.DeletePersonByPersonID(matchingPerson.PersonID);
            return true;
        }

    }
}
