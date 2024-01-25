using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContract_s_;

namespace Repository_s_
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CountriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Country> AddCountry(Country country)
        {
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();

            return country;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            List<Country> country = await _db.Countries.ToListAsync();

            return country;
        }

        public async Task<Country?> GetCountryByCountryID(Guid countryID)
        {
            Country? countries = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryID == countryID);

            return countries;
        }

        public async Task<Country?> GetCountryByCountryName(string countryName)
        {
            Country? countries = await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryName == countryName);

            return countries;
        }
    }
}
