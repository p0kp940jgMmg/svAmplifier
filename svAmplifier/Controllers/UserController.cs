﻿using System;
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
        public IActionResult Index()
        {
            return View();
            //return Content("logged in as.." + User.Identity.Name);
        }

        //TEST
        public IActionResult Test()
        {

            return View(new UserIndexLayoutVM());
        }

        public async Task<IActionResult> AddPickItem(Pick pick)
        {
            pick.DatePicked = new DateTime();

            await accountRepos.AddPick(pick);

            return RedirectToAction(nameof(Index));
        }
    }
}
