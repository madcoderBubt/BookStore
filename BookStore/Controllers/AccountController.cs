using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel (){ ReturnUrl = returnUrl});
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (login == null) throw new NullReferenceException();

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByNameAsync(login.UserName).ConfigureAwait(false);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(login.ReturnUrl))
                        return RedirectToAction("Index","Home", new { area = "Dashboard" });
                    return Redirect(login.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "User Name or Password is invalid!");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (register == null)
                throw new NullReferenceException();

            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = register.UserName, Email = register.Email };
                var result = await _userManager.CreateAsync(user, register.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(result.Errors.FirstOrDefault().Code, result.Errors.FirstOrDefault().Description);
                }
            }
            return View(register);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return RedirectToAction("Index", "Home");
        }

        //public IActionResult Login(Uri returnUrl)
        //{
        //    throw new NotImplementedException();
        //}
    }
}