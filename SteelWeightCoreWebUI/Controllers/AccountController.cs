using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Abstract;
using SteelWeightCoreWebUI.Models.Entities;

namespace SteelWeightCoreWebUI.Controllers {
    public class AccountController : Controller {
        public AccountController(IUserRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user, string returnUrl) {
            if (ModelState.IsValid) {
                string priv = _repository.Authenticate(user.user_name, user.user_pwd);
                if (priv != null) {
                    var claims = new[] { new Claim("name", user.user_name), new Claim(ClaimTypes.Role, priv) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationHttpContextExtensions.SignInAsync(HttpContext, new ClaimsPrincipal(identity));
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                TempData["Failure"] = "아이디 또는 비밀번호가 유효하지 않습니다";
            }
            return View();
        }

        [HttpGet]
        public RedirectResult Logout() {
            AuthenticationHttpContextExtensions.SignOutAsync(HttpContext); 
            return Redirect(Url.Action("Login", "Account"));
        }

        private readonly IUserRepository _repository;
    }
}