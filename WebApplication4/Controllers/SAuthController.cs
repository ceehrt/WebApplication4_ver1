using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Context;
using WebApplication4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Collections.Specialized.BitVector32;


namespace YourNamespace.Controllers
{
    public class SAuthController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userlist = await _context.SUser.ToListAsync();
            return View(userlist);
        }

        // GET: SProducts/Details/5

        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new SAuthViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Id,Name,LastName,UserEmail,Password,Role")] SAuthViewModel suser)
        {
            var user =  await _context.SUser.Where(x => x.UserEmail == suser.UserEmail 
            && x.Password==suser.Password).FirstOrDefaultAsync();

            if(user == null)
            {
                //lOGIN FAILED
                ViewBag.ErrorMessage = "Your username (Email) cannot be found!";
                return View(new SAuthViewModel());
            }
            else
            {
                //lOGIN SUCCESSFUL
                //HttpContext.Session.SetString("UserName", user.Name);

                //Session("") = user.Name;
                if (user.Role == "AdminRole")
                {
                    return RedirectToAction("Index", "SProducts");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            
            }


        }

         public IActionResult Register1()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register1([Bind("Id,Name,LastName,UserEmail,Password,Role")] SAuthViewModel sProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sProduct);
                await _context.SaveChangesAsync();

                ViewBag.Message = $"Welcome {sProduct.Name}";
                return RedirectToAction(nameof(Index));
            }
            return View(sProduct);
        }


        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear(); // Optional: Clear other session data
            await HttpContext.SignOutAsync(); // Sign out the user

            return RedirectToAction("Index", "Home"); // Redirect to home page
        }

    }
}
