using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Contact_Manager.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        //private fields
        private readonly IPersonServices _personServices;
        private readonly ICountriesService _countriesService;

        //contructor
        public PersonsController(IPersonServices personServices, ICountriesService countriesService)
        {
            _personServices = personServices;
            _countriesService = countriesService;
        }


        //Url: persons/index
        [Route("[action]")]
        //Url: persons/
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrderOptions = SortOrderOptions.ASC)
        {
            //Searching
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName), "Person Name"},
                {nameof(PersonResponse.Email), "Email"},
                {nameof(PersonResponse.DOB), "Date of Birth"},
                {nameof(PersonResponse.Gender), "Gender"},
                {nameof(PersonResponse.Address), "Address"},
                {nameof(PersonResponse.CountryID), "Country"}
            };
            List<PersonResponse> persons = _personServices.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sort
            List<PersonResponse> sortedPerson = _personServices.GetSortedPersons(persons, sortBy, sortOrderOptions);
            ViewBag.CurrentSortBy = sortBy.ToString();
            ViewBag.CurrentSortOrderOptions = sortOrderOptions.ToString();
            return View(sortedPerson);
        }

        //Url: persons/create
        [Route("[action]")]
        [HttpGet] //indicates that the action recieves only get requests
        public IActionResult Create()
        {
            List<CountryResponse> countries = _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>
            new SelectListItem()
            {
                Text=temp.CountryName, Value=temp.CountryID.ToString(),
            });

            return View();
        } 

        //Url: persons/create
        [Route("[action]")]
        [HttpPost] //indicates that the action recieves only get requests
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = _countriesService.GetAllCountries();
                ViewBag.Countries = countries;

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View(countries);
            }

            //call the service method
            PersonResponse personResponse = _personServices.AddPerson(personAddRequest);

            //navigate to Index() action method (it make another get request to "persons/index")
            return RedirectToAction("Index", "Persons");
        }

       
        [HttpGet]
        [Route("[action]/{personID}")] //Url: person/edit/1
        public IActionResult Edit(Guid personID)
        {
            return View();
        }
    }
}
