using Microsoft.AspNetCore.Mvc;

namespace StateMgt.Controllers.Web
{
    public class CookiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WriteCookies(string setting,
                   string settingValue, bool isPersistent)
        {
            //If persistent checkbox is unchecked, add the cookie expiration period = 1 day,
            //else don't add any expiration period

            if (isPersistent == false)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append(setting, settingValue, options);
            }
            else
            {
                Response.Cookies.Append(setting, settingValue);
            }
            ViewBag.Message = "Cookie written successfully!";
            return View("Index");
        }
        public IActionResult ReadCookies()
        {
            ViewBag.FontName = Request.Cookies["fontName"];
            ViewBag.FontSize = Request.Cookies["fontSize"];
            ViewBag.Color = Request.Cookies["color"];

            if (string.IsNullOrEmpty(ViewBag.FontName))
            {
                ViewBag.FontName = "Arial";
            }
            if (string.IsNullOrEmpty(ViewBag.FontSize))
            {
                ViewBag.FontSize = "22px";
            }
            if (string.IsNullOrEmpty(ViewBag.Color))
            {
                ViewBag.Color = "Blue";
            }
            return View();
        }
    }
}
