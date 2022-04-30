using GreatWorld.Data;
using GreatWorld.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreatWorld.Controllers.Web
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signinManager;

        public AuthController(SignInManager<WorldUser> signinManager)
        {
            _signinManager = signinManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signinManager.PasswordSignInAsync(vm.UserName,
                                                                            vm.Password,
                                                                            isPersistent: true,
                                                                            lockoutOnFailure: false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            ViewBag.Message = "Please enter valid username and password!";
            return View();
        }

        //we have implemented this as async as _signInManager methods are async
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signinManager.SignOutAsync();
            }
            return RedirectToAction("Trips", "App");
        }

    }
}
