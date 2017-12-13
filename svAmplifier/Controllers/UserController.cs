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
        public async Task<IActionResult> Index()
        {
            var model = await accountRepos.GetUserIndexLayoutVM();

            model.MyItems.Mushrooms = await accountRepos.GetMushrooms();
            model.MyItems.Regions = await accountRepos.GetRegions();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPickItem(UserIndexLayoutVM pick)
        {
            pick.MyItems.NewPick.SalesItem = false;

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), pick);
            }

            if (pick.Cordinates != null)
            {
                var cord = pick.Cordinates;
                cord = cord.Replace('(', ' ');
                cord = cord.Replace(')', ' ');

                string[] cordArr = cord.Split(',');
                pick.MyItems.NewPick.Latitude = cordArr[0];
                pick.MyItems.NewPick.Longitude = cordArr[1];
            }

            if (!(await accountRepos.AddPick(pick.MyItems.NewPick)))
            {
                string msg = "Error!, we couldn't add your item.";

                return RedirectToAction(nameof(Error), "User", msg);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddMarketItem(UserIndexLayoutVM pick)
        {
            pick.MyItems.NewPick.SalesItem = true;

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), pick);
            }

            if (!(await accountRepos.AddPick(pick.MyItems.NewPick)))
            {
                string msg = "Error!, we couldn't add your item.";

                return RedirectToAction(nameof(Error), "User", msg);
            }

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SwedenMap()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult SwedenMap(string id)
        //{
        //    return RedirectToAction(nameof("market"), id);
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Market(string id)
        {
            var picks = await accountRepos.GetMarketItemsForRegion(id);
            MarketPickVM marketItemVM = new MarketPickVM
            {
                MarketItems = picks
            };
            return View(marketItemVM);

        }

        public IActionResult Error(string msg)
        {
            return View(msg);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult UpdateUserInfo()
        {
            return View();
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult UpdateUserInfo()
        //{
        //    return Content("Success");
        //}

    }


}
