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
    //[Authorize]
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
        public async Task<IActionResult> AddPickItem(MyItemsVM pick)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(MyItems), pick);
            }

            if (!(await accountRepos.AddPick(pick.NewPick)))
            {
                pick.Message = "Error!, we couldn't add your item";
                return View(pick);
            }

            return RedirectToAction(nameof(MyItems));
        }

        [HttpGet]
        public async Task<IActionResult> MyItems()
        {
            MyItemsVM myItems = await accountRepos.GetMushrooms();
            myItems.MyMarketItems = await accountRepos.GetMarketItems();

            return View(myItems);
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
            MarketPickVM marketItemVM = new MarketPickVM {
                MarketItems = picks
            };
            return View(marketItemVM);
           
        }
        
    }


}
