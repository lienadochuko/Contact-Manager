using Microsoft.AspNetCore.Mvc;

namespace Contact_Manager.Controllers
{
    public class PersonsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
