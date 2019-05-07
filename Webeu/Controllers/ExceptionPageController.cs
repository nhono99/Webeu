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
        
       
        public IActionResult Index(string errorMessage)
        {
            ExceptionPageModel model = new ExceptionPageModel { ErrorMessage = errorMessage};
            //model.ErrorMessage = errorMessage;
            return View(model);
        }
    }
}
