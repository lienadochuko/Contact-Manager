using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;


namespace Contact_Manager.Controllers
{
    public class PersonsController : Controller
    {
        //private fields
        private readonly IPersonServices _personServices;
        private readonly ICountriesService _countriesService;

        //contructor
        public PersonsController(IPersonServices personServices, ICountriesService countriesService)
        {
            _personServices = personServices;
            //_countriesService = countriesService;
        }

        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName), "Person Name"},
                {nameof(PersonResponse.Email), "Email"},
                {nameof(PersonResponse.DOB), "Date of Birth"},
                {nameof(PersonResponse.Gender), "Gender"},
                {nameof(PersonResponse.Address), "Address"},
                {nameof(PersonResponse.CountryID), "Country"}
            };
            List<PersonResponse> person = _personServices.GetFilteredPersons(searchBy, searchString);
            return View(person);
        }
    }
}
