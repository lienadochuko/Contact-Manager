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
    public class PersonUpdaterService : IPersonUpdaterServices
    {
        //private field
        //private readonly List<Person> _persons;
        private readonly IPersonRepository _personRepository;
        private readonly ICountriesRepository _countriesRepository;
        private readonly ICountriesService _countriesService;
        //private readonly ILogger<PersonUpdaterService> _logger;

        public PersonUpdaterService(IPersonRepository personRepository,
            ICountriesRepository countriesRepository)
        {
            //_persons = new List<Person>();
            //ILogger<PersonUpdaterService> logger

            _personRepository = personRepository;
            _countriesRepository = countriesRepository;
            //_logger = logger;
        }

        public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(personUpdateRequest));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            if (personUpdateRequest.PersonID == new Guid())
                throw new ArgumentException(nameof(personUpdateRequest.PersonID));

            Person? matchingPerson = await _personRepository.GetPersonByPersonID(personUpdateRequest.PersonID);
            if (matchingPerson == null)
            {
                throw new InvalidPersonIDException("Given person id doesn't exist");
            }

            //update all details
            await _personRepository.UpdatePerson(personUpdateRequest.ToPerson());

            return matchingPerson.ToPersonResponse();
        }
    }
}
