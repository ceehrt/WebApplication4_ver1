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

namespace WebApplication4.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult AdLogin()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult AdLogin()
        {
            return View(new AdminViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AdLogin([Bind("Id,AdUsername,AdPassword")] AdminViewModel suser)
        {
            var user = await _context.Admin.Where(x => x.AdUsername == suser.AdUsername).FirstOrDefaultAsync();

            if (user == null)
            {
                //lOGIN FAILED
                ViewBag.ErrorMessage = "Your username cannot be found!";
                return View(new AdminViewModel());
            }
            else
            {
                //lOGIN SUCCESSFUL
                // HttpContext.Session.SetString("UserName", user.Name);

                //Session("") = user.Name;

                return RedirectToAction("Index", "SProducts");
            }


        }
    }
}
