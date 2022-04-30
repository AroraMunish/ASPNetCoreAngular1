using AuthEx.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthEx.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signinManager;

        public AuthController(SignInManager<IdentityUser> signinManager)
        {
            _signinManager = signinManager;
        }

        // GET: /<controller>/
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
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
                        return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }


        public IActionResult UnAuthorized()
        {
            return View();
        }

    }

}
