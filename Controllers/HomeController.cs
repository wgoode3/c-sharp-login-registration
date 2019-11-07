using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unicorns.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;


namespace Unicorns.Controllers
{
    public class HomeController : Controller
    {

        private MyContext context;
        
        public HomeController(MyContext mc)
        {
            context = mc;
        }
 
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                User userMatchingEmail = context.Users
                    .FirstOrDefault(u => u.Email == user.Email);
                if(userMatchingEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    context.Users.Add(user);
                    context.SaveChanges();
                    HttpContext.Session.SetInt32("userid", user.UserId);
                    return Redirect("/success");
                }
            }
            return View("Index");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                User userMatchingEmail = context.Users
                    .FirstOrDefault(u => u.Email == user.LoginEmail);
                if(userMatchingEmail == null)
                {
                    ModelState.AddModelError("LoginEmail", "Unknown Email!");
                }
                else
                {
                    PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                    var result = Hasher.VerifyHashedPassword(user, userMatchingEmail.Password, user.LoginPassword);
                    if(result == 0)
                    {
                        ModelState.AddModelError("LoginPassword", "Incorrect Password!");
                    } else {
                        HttpContext.Session.SetInt32("userid", userMatchingEmail.UserId);
                        return Redirect("/success");
                    }
                }
            }
            return View("Index");
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid == null)
            {
                return Redirect("/");
            }
            ViewBag.UserId = (int) userid;
            return View();
        }
    }
}
