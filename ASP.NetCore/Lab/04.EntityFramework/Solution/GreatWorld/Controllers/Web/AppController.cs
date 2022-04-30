//using GreatWorld.Data;
using GreatWorld.Models;
using GreatWorld.Repository;
using GreatWorld.Services;
using GreatWorld.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace GreatWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfiguration _config;
        private IGreatWorldRepository _repository;

        //private GreatWorldWithEFContext _context;
        private TestClass _testClass;

        public AppController(IMailService mailService,
                              IConfiguration config,
            IGreatWorldRepository repository,
            TestClass testClass)
        {
            _mailService = mailService;
            _config = config;
            _repository= repository;
            _testClass = testClass;
            Debug.WriteLine("AppController: Using mailService class id::" + _mailService.Id);
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Home Page";
            List<Trip> trips = _repository.GetAllTripsWithStops().ToList();
            //List<Trip> trips = _context.Trips
            //                      .Include(t => t.Stops)
            //                      .OrderBy(t => t.Name)
            //                      .ToList();

            return View(trips);
        }


        public IActionResult About()
        {
            ViewBag.Title = "About Page";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Page";
            return View();
        }

        //Post
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            try
            {
                Debug.WriteLine("Contact Method: Using mailService class id::" + _mailService.Id);
                _testClass.TestMsg();

                var domainEmail = _config["Appsettings:SiteEmailAddress"];
                if (_mailService.SendMail(model.Email, domainEmail,
                    $"Contact Page from {domainEmail}", model.Message))
                {
                    ViewBag.Message = $"Mail sent Successfully from {domainEmail} to {model.Email}!! Thanks";
                    ModelState.Clear();
                }
                ViewBag.Title = "Contact Page";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Mail could not be sent Successfully to {model.Email} because of the exception {ex.Message}";
                return View();
            }
        }
    }
}
