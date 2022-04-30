using Microsoft.AspNetCore.Mvc;
using StateMgt.ViewModel;
using Newtonsoft.Json;

namespace StateMgt.Controllers.Web
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult Index(Person person)
        {
            string key = "person1";
            var data = JsonConvert.SerializeObject(person);
            HttpContext.Session.SetString(key, data);

            return RedirectToAction("Details");
        }

        [HttpGet]
        public IActionResult Details()
        {
            var key = HttpContext.Session.GetString("person1");
            Person person = JsonConvert.DeserializeObject<Person>(key);

            ViewBag.Details = person.Name + " is " + person.Age + " years old";
            return View();
        }
    }
}
