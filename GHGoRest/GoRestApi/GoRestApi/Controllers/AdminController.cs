using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using GoRestApi.Models;
using GoRestApi.Entities;
using User = GoRestApi.Entities.User;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace GoRestApi.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;

        public AdminController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model) 
        { 
            if(ModelState.IsValid)
            {
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassWord = model.Password + md5Salt;
                string hashedPassword = saltedPassWord.MD5();
                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() 
                && x.Password == hashedPassword);

                if (user != null)
                {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Id", user.Id.ToString()));
                claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");

                }


                else
                {
                    ModelState.AddModelError("", "Kullanıcı veya şifre Doğru değil.");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();
                User user = new()
                {
                    Username = model.Username,
                    Password = hashedPassword
                };

            _databaseContext.Users.Add(user);
             int affectedRow = _databaseContext.SaveChanges();
                if(affectedRow == 0)
                {
                    ModelState.AddModelError("", "Kullanıcı oluşturulamadı..");
                }
                else
                {
                    RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

    }
}
