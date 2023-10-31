using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private readonly List<Country> _countries;

        //Contructor to inialize the field
        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if(initialize )
            {
                _countries.AddRange(new List<Country>() { 
                new Country()
                {
                    CountryID = Guid.Parse("EE69E6B7-641C-4ABB-80B6-1D3971EDC904"),
                    CountryName = "ENGLAND"
                },
                new Country()
                {
                    CountryID = Guid.Parse("A5386467-0411-44CA-90C8-E3A26B655B94"),
                    CountryName = "AUSTRALIA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("84312DB0-4672-407D-B212-87E550C74428"),
                    CountryName = "EIGHT-MAN-EMPIRE"
                },
                new Country()
                {
                    CountryID = Guid.Parse("BF9830C7-8E3D-485C-8FB1-9C7F9652FC75"),
                    CountryName = "BULGARIA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("1E00DAEA-F817-4375-9EC9-A6E6BD48BACE"),
                    CountryName = "ONARIA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("09508EB3-57DB-49CE-B368-0A00FFB75828"),
                    CountryName = "ZERIOBIA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("40DB2E52-37AB-43C3-8B22-EB24A2084DED"),
                    CountryName = "ONSLOW"
                },
                new Country()
                {
                    CountryID = Guid.Parse("E801E3C0-7835-4760-9E0B-27078011A2E5"),
                    CountryName = "ETHOPIA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("6E0A41B7-BAF4-4F2B-BACE-A654E87C664B"),
                    CountryName = "HOLDFAST"
                },
                new Country()
                {
                    CountryID = Guid.Parse("7680241D-34B8-4F39-B494-87C27831866C"),
                    CountryName = "ISREAL"
                },
                new Country()
                {
                    CountryID = Guid.Parse("A6939D16-EA43-442C-ADEE-68738A2B39CB"),
                    CountryName = "BENIN"
                },
                new Country()
                {
                    CountryID = Guid.Parse("9CE71166-8A69-481D-9C5C-2EA5AAC9E73B"),
                    CountryName = "ALGERIA"
                },
                new Country()
                {
                    CountryID = Guid.Parse("C3EE9030-141F-4B92-B1AC-DF45A84A9046"),
                    CountryName = "BELGIUM"
                }
            });
            }
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
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
            if(_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            //Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            //generate CountryID
            country.CountryID = Guid.NewGuid();

            //Add country object into _countries
           _countries.Add(country);

            return country.ToCountryResponse();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if(countryID == null)
                return null;
            
           
           Country? country_from_list =  
                _countries.FirstOrDefault(temp => temp.CountryID == countryID);
            if (country_from_list == null)
                return null;

            return country_from_list.ToCountryResponse();
        }

    }
}