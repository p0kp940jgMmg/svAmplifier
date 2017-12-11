using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using svAmplifier.Models;
using svAmplifier.Models.Entities;
using svAmplifier.Models.VM;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace svAmplifier.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        AccountRepository accountRepos;
        public UserController(AccountRepository accountRepos)
        {
            this.accountRepos = accountRepos;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await accountRepos.GetUserIndexVM();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPickItem(Pick pick)
        {
            pick.DatePicked = new DateTime();

            if (!ModelState.IsValid)
            {
                return View(pick);
            }

            if (!(await accountRepos.AddPick(pick)))
            {
                return View("error, couldnt add your item", pick);
            }
            //ändra index sen
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> MyItems()
        {
            MyItemsVM myItems = new MyItemsVM
            {
                Mushrooms = await accountRepos.GetMushrooms()
            };

            return View(myItems);
        }
    }


}
