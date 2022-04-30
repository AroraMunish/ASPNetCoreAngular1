using BasicApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace BasicApp.Controllers
{
  public class Home: Controller
  {
    public IActionResult About()
    {
      CompanyInfo objCompany = new CompanyInfo();
      objCompany.Name = "My Company";
      objCompany.Location = "New Delhi";
      List<string> SoftwareDevelopers = new List<string> { "Peter", "Suzy", "Ian Mayes" };
      objCompany.Developers = SoftwareDevelopers;
      return View(objCompany);
    }

    public IActionResult Index()
    {
      ViewBag.Title = "Company Info";
      ViewBag.Company = "Best Infosystems";
      ViewData["HeadQuarter"] = "New Delhi";
      ViewBag.Locations = new List<string> { "India", "USA", "UK" };
      ViewData["UKLocations"] = new List<string> { "London", "Manchester", "Birmingham" };
      return View();
    }
  }
}
