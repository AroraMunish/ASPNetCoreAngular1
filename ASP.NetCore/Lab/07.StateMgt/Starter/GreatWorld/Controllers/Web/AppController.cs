//using GreatWorld.Data;
using GreatWorld.Models;
using GreatWorld.Repository;
using GreatWorld.Services;
using GreatWorld.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using GreatWorld.Data;

namespace GreatWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfiguration _config;
        private IGreatWorldRepository _repository;


        //private GreatWorldWithEFContext _context;
        private TestClass _testClass;
        private UserManager<WorldUser> _userManager;

        public AppController(   IMailService mailService,
                                IConfiguration config,
                                IGreatWorldRepository repository,
                                UserManager<WorldUser> userManager,
                                TestClass testClass)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _userManager = userManager;
            _testClass = testClass;
            Debug.WriteLine("AppController: Using mailService class id::" + _mailService.Id);
        }

        [HttpGet]

        public IActionResult Register()
        {
            ViewBag.Title = "Register";
            return View();
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewBag.Title = "Register";
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    //Check Email
                    var isUniqueEmail = (await _userManager.FindByEmailAsync(model.Email) == null);
                    if (isUniqueEmail == false)
                    {
                        ViewBag.Message = "Pls. enter unique Email";
                        return View();
                    }

                    //add the user
                    var newUser = new WorldUser()
                    {
                        UserName = model.UserName,
                        Email = model.Email
                    };

                    var identityResult = await _userManager.CreateAsync(newUser, model.Password);
                    if (identityResult.Succeeded)
                    {
                        ModelState.Clear();
                        ViewBag.Message = string.Format("User {0} Created Successfully", model.UserName);
                    }
                    else
                    {
                        IEnumerable<IdentityError> allErrors = identityResult.Errors;

                        string errors = "";
                        foreach (IdentityError error in identityResult.Errors)
                        {

                            errors = (errors == "") ? error.Description : errors + ";" + error.Description;
                        }
                        ViewBag.Message = "User Could not be created. Error:" + errors;

                        //ViewBag.Message = "User Could not be created. Error:" + identityResult.Errors.FirstOrDefault();
                    }
                }
                else
                {
                    ViewBag.Message = string.Format("User with User Name {0} already exists.", model.UserName);
                }
            }
            return View();
        }


        public IActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            List<Trip> trips = _repository.GetAllTripsWithStops().ToList();
            ViewBag.Title = "Trips";
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
