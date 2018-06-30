using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webeu.Data;
using Webeu.Models;
using Webeu.Models.ExceptionPage;

namespace Webeu.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null  )
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };
                try
                {
                    _db.Users.Add(applicationUser);
                    await _db.SaveChangesAsync();
                }
               catch(Exception ex)
                {
                    return Error();
                    //return RedirectToAction(nameof(ExceptionPageController.Index), "ExceptionPage", new {Err } );
                    //return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                return RedirectToAction(nameof(Register));
            }
            return View(model);
        }

      
        [HttpPost]
        public IActionResult Error()
        {
            return RedirectToAction(nameof(ExceptionPageController.Index), "ExceptionPage", new ExceptionPageModel { ErrorMessage ="Hello World!"});
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
 

}
