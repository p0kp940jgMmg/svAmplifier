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
    
    public class HomeController : Controller
    {
        AccountRepository accountRepository;

        public HomeController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reg()
        {
            return View();
        }

        //i formulär: alltid en get och en post. en som visar... 
        [HttpGet]
        public IActionResult Login()            
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var temp = await accountRepository.Login(loginVM);

            if (!temp)
            {
                return View(loginVM);
            }
            else
            {
                //accountRepository.SetSessionUsername();
            }

            return RedirectToAction(nameof(UserController.Index), "User");

        }

        [HttpPost]
        public async Task<IActionResult> Reg(RegisterUserVM registerUserVM)
        {
            if (!ModelState.IsValid)
                return View(registerUserVM);
           
            if (!await accountRepository.RegisterUser(registerUserVM))
            {
                ModelState.AddModelError(nameof(RegisterUserVM.UserName), "Registration failed...");
                return View(registerUserVM);
            }

            return RedirectToAction(nameof(UserController.Index), "User");
        }

    }
}
