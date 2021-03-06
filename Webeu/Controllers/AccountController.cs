﻿using System;
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
                    return Error(ex.InnerException.Message);
                    //return RedirectToAction(nameof(ExceptionPageController.Index), "ExceptionPage", new {Err } );
                    //return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                TempData["registerComplete"] = "Registration success!";
                return RedirectToAction(nameof(Login));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _db.Users.FirstOrDefaultAsync(a => a.Email == model.Email && a.Password == model.Password);
                if(user == null)
                {
                    TempData["loginFailed"] = "Username/password does not match.";
                    return View();
                    //return Error("User not found");
                }
                return Error("You found it");
            }

            return View(model);
        }
      
        [HttpPost]
        public IActionResult Error(string msg)
        {
            return RedirectToAction(nameof(ExceptionPageController.Index), "ExceptionPage", new ExceptionPageModel { ErrorMessage = msg});
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
