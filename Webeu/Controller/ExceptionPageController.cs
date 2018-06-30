using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webeu.Models.ExceptionPage;
namespace Webeu.Controllers
{
    public class ExceptionPageController: Controller
    {
        
        [HttpGet]
        public IActionResult Index(string errMsg)
        {
            ExceptionPageModel model = new ExceptionPageModel { ErrorMessage = errMsg};
            return View(model);
        }
    }
}
