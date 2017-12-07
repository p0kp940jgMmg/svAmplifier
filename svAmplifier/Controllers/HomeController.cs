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
        UserContext context;

        public HomeController(AccountRepository accountRepository, UserContext context)
        {
            this.accountRepository = accountRepository;
            this.context = context;
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        //i formulär: alltid en get och en post. en som visar... 
        [HttpGet]
        public async Task<IActionResult> Login()            
        {
            await accountRepository.RegisterUser(new RegisterUserVM { UserName = "danne", Password="Password_123"});
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

            return RedirectToAction(nameof(Success));

        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserVM registerUserVM)
        {
            if (!ModelState.IsValid)
                return View(registerUserVM);
           
            if (! await accountRepository.RegisterUser(registerUserVM))
                return View(registerUserVM);

            return RedirectToAction(nameof(UserController.Index), "User");
        }

    }
}
