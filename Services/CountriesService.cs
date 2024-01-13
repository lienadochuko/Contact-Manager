using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ServiceContracts;
using ServiceContracts.DTO;
using OfficeOpenXml;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private readonly ApplicationDbContext _db;
        //private readonly List<Country> _countries;

        //Contructor to inialize the field
        public CountriesService(ApplicationDbContext personsDbContext)
        {
            //_countries = new List<Country>();
            _db = personsDbContext;

        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(country => country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            //throw new NotImplementedException();

            //Validation: countryAddRequest parameter can't be null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            //Validation: CountryName can't be null
            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest.CountryName));
            }

            //Validation: CountryName can't be dupliacte
            if(await _db.Countries.CountAsync(temp => temp.CountryName == countryAddRequest.CountryName) > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            //Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            //generate CountryID
            country.CountryID = Guid.NewGuid();

            //Add country object into _countries
           await _db.Countries.AddAsync(country);
           await _db.SaveChangesAsync();

            return country.ToCountryResponse();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if(countryID == null)
                return null;
            
           
           Country? country_from_list =  
                await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryID == countryID);
            if (country_from_list == null)
                return null;

            return country_from_list.ToCountryResponse();
        }

        public async Task<int> UploadCountriesFromExcelFile(IFormFile formFile)
        {
            MemoryStream memoryStream = new();
            await formFile.CopyToAsync(memoryStream);

            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Countries"];

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string? cellValue = Convert.ToString(worksheet.Cells[row, 1].Value);
                }

                return rowCount;
            }
        }
    }
}