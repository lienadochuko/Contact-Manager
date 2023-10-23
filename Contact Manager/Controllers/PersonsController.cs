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
        public IActionResult Index()
        {
            List<PersonResponse> person = _personServices.GetAllPersons();
            return View(person);
        }
    }
}
