using Common.ViewModels;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcaFinance.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool existUser = (await _userManager.FindByEmailAsync(model.Email) != null || await _userManager.FindByNameAsync(model.Email) != null);

            if (!existUser)
            {
                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("PendingPage", "Account", new { area = "Admin" });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "The Email already exists please use another email!");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Finance", "Home");
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            User user = new User();
            try
            {
                user = await _userManager.FindByNameAsync(model.Email) ?? await _userManager.FindByEmailAsync(model.Email);
            }
            catch (Exception ex)
            {
                string ex1 = ex.ToString();
            }

            if (user != null)
            {
                if (user.Deleted)
                {
                    ModelState.AddModelError("", "Account is deleted.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = new Microsoft.AspNetCore.Identity.SignInResult();
            try
            {
                result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
            }
            catch (Exception ec)
            {
                string s = ec.ToString();
            }

            if (result.Succeeded)
            {
                try
                {
                    if (model.ReturnUrl != null && model.ReturnUrl != "/admin")
                        return Redirect(model.ReturnUrl);
                }
                catch (Exception ex)
                {
                }
                return RedirectToAction("Finance", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Finance", "Home", new { area = "" });
        }
    }
}
