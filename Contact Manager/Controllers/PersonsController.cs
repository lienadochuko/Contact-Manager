using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
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
        private readonly ILogger<PersonsController> _logger;

        //contructor
        public PersonsController(IPersonServices personServices, ICountriesService countriesService, ILogger<PersonsController> logger)
        {
            _personServices = personServices;
            _countriesService = countriesService;
            _logger = logger;
        }


        //Url: persons/index
        [Route("[action]")]
        //Url: persons/
        [Route("/")]
        public async Task<IActionResult> Index(string searchBy, string? searchString, 
            string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrderOptions = SortOrderOptions.ASC)
        {
            _logger.LogInformation("Index Action for personController");
            _logger.LogDebug($"searhBy: {searchBy}, searchString: {searchString}, " +
                $"sortBy: {sortBy}, sortOrderOptions: {sortOrderOptions}");

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
            List<PersonResponse> persons = await _personServices.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sort
            List<PersonResponse> sortedPerson = await _personServices.GetSortedPersons(persons, sortBy, sortOrderOptions);
            ViewBag.CurrentSortBy = sortBy.ToString();
            ViewBag.CurrentSortOrderOptions = sortOrderOptions.ToString();
            return View(sortedPerson);
        }

        //Url: persons/create
        [Route("[action]")]
        [HttpGet] //indicates that the action recieves only get requests
        public async Task<IActionResult> Create()
        {
            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>
            new SelectListItem()
            {
                Text = temp.CountryName,
                Value = temp.CountryID.ToString(),
            });

            return View();
        }

        //Url: persons/create
        [Route("[action]")]
        [HttpPost] //indicates that the action recieves only post requests
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries;

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View(countries);
            }

            //call the service method
            PersonResponse personResponse = await _personServices.AddPerson(personAddRequest);

            //navigate to Index() action method (it make another get request to "persons/index")
            return RedirectToAction("Index", "Persons");
        }


        [HttpGet]
        [Route("[action]/{personID}")] //Url: person/edit/1
        public async Task<IActionResult> Edit(Guid personID)
        {
            PersonResponse? personResponse = await _personServices.GetPersonByPersonID(personID);
            if (personResponse == null)
            {
                return RedirectToAction("Index", "Persons");
            }

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>
            new SelectListItem()
            {
                Text = temp.CountryName,
                Value = temp.CountryID.ToString(),
            });

            return View(personUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personServices.GetPersonByPersonID(personUpdateRequest.PersonID);

            if (personResponse == null)
            {
                return RedirectToAction("Index", "Persons");
            }

            if (ModelState.IsValid)
            {
                PersonResponse updatedPerson = await _personServices.UpdatePerson(personUpdateRequest);
                return RedirectToAction("Index", "Persons");
            }
            else
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries;

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
        }


        [HttpGet]
        [Route("[action]/{personID}")] //Url: person/delete/1
        public async Task<IActionResult> Delete(Guid personID)
        {
            PersonResponse? personResponse = await _personServices.GetPersonByPersonID(personID);
            if (personResponse == null)
            {
                return RedirectToAction("Index", "Persons");
            }
            //_personServices.DeletePerson(personID);

            return View(personResponse);
        }


        [HttpPost]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Delete(PersonResponse personResponse)
        {
            if (personResponse == null)
                return RedirectToAction("Index");

            await _personServices.DeletePerson(personResponse.PersonID);
            return RedirectToAction("Index");
        }

        [Route("PersonsPDF")]
        public async Task<IActionResult> PersonsPDF()
        {
            //Get list of persons
            List<PersonResponse> personList = await _personServices.GetAllPersons();

            //Return View as PDF
            return new ViewAsPdf("PersonsPDF", personList, ViewData)
            {
                FileName = "MyPdfFile.pdf",
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape //it is by default Potrait
            };
        }
        
        [Route("PersonalPDF")]
        public async Task<IActionResult> PersonalPDF()
        {
            //Get list of persons
            List<PersonResponse> personList = await _personServices.GetAllPersons();

            //Return View as PDF
            return new ViewAsPdf("PersonalPDF", personList, ViewData)
            {
                FileName = "MyPdfFile.pdf",
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape //it is by default Potrait
            };
        }

        [Route("PersonsCSV")]
        public async Task<IActionResult> PersonsCSV()
        {
           MemoryStream memoryStream = await _personServices.GetPersonCSV();

            return File(memoryStream, "application/octet-stream", "persons.csv");
        }

        [Route("PersonsExcel")]
        public async Task<IActionResult> PersonsExcel()
        {
            MemoryStream memoryStream = await _personServices.GetPersonExcel();

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "persons.xlsx");
        }
    }
}
