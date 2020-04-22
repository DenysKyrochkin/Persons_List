using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zodiac.Models;
using Zodiac.PersonList;
using Zodiac.Exceptions;


namespace Zodiac.Controllers
{
    public class UsersController : Controller
    {
        private readonly PersonListSingleton _personListSingleton;

        public UsersController(PersonListSingleton personListSingleton)
        {
            _personListSingleton = personListSingleton;
        }

        public IActionResult Index()
        {
            return View(_personListSingleton.PersonList);
        }

        public IActionResult New()
        {
            return View("EditPerson");
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id)
        {
            try
            {
            string name = HttpContext.Request.Form["Name"];
            string surname = HttpContext.Request.Form["Surname"];
            string email = HttpContext.Request.Form["Email"];
            string date = HttpContext.Request.Form["Birthday"];
            
            Person person = new Person(name, surname, email, DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None));


            if (id == 0)
            {
                _personListSingleton.PersonList.Insert(0, person);
            }
            else
            {
                var personFromHelperList = _personListSingleton.HelperList[id - 1];
                var personFromPersonList = _personListSingleton.PersonList.First(p => p == personFromHelperList);

                personFromHelperList.Name = person.Name;
                personFromHelperList.Surname = person.Surname;
                personFromHelperList.Email = person.Email;
                personFromHelperList.Birthday = person.Birthday;
                personFromHelperList.IsAdult = person.IsAdult;
                personFromHelperList.SunSign = person.SunSign;
                personFromHelperList.ChineseSign = person.ChineseSign;
                personFromHelperList.IsBirthday = person.IsBirthday;

                personFromPersonList = personFromHelperList;

            }

            await _personListSingleton.Serialize();
            return View("Index", _personListSingleton.HelperList);
            }
            catch (NotBirthException)
            {
                return Redirect("/Users/NotBirth");
            }
            catch (OldBirthException)
            {
                return Redirect("/Users/OldBirth");
            }
            catch (InvalidEmailException)
            {
                return Redirect("/Users/InvalidEmail");
            }

        }

        public IActionResult Edit(int id)
        {
            var person = _personListSingleton.HelperList[id - 1];
            person.Id = id;

            return View("EditPerson", person);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var person = _personListSingleton.HelperList[id - 1];
            _personListSingleton.HelperList.Remove(person);
            _personListSingleton.PersonList.Remove(person);

            await _personListSingleton.Serialize();
            return View("Index", _personListSingleton.HelperList);
        }

        public IActionResult NotBirth()
        {
            return View();
        }

        public IActionResult OldBirth()
        {
            return View();
        }

        public IActionResult InvalidEmail()
        {
            return View();
        }
    }
}