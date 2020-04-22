using Microsoft.AspNetCore.Mvc;
using Zodiac.PersonList;


namespace Zodiac.Controllers
{
    public class FilterOrderController : Controller
    {
        private readonly PersonListSingleton _personListSingleton;

        public FilterOrderController(PersonListSingleton personListSingleton)
        {
            _personListSingleton = personListSingleton;
        }

        public IActionResult Process(int id)
        {
            string filter = HttpContext.Request.Form[$"Filter{id}"];
            string order = HttpContext.Request.Form[$"Order{id}"];

            var people = _personListSingleton.ProcessList(id, filter, order);

            return View("~/Views/Users/Index.cshtml", people);
        }
    }
}