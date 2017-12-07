using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using svAmplifier.Models.VM;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace svAmplifier.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            //return View(new UserIndexLayoutVM());
            return Content("logged in as.." + User.Identity.Name);
        }

        //TEST
        public IActionResult Test()
        {

            return View(new UserIndexLayoutVM());
        }
    }
}
