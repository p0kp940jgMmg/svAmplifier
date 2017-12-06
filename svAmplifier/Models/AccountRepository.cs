using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using svAmplifier.Models.Entities;
using svAmplifier.Models.VM;

namespace svAmplifier.Models
{
    public class AccountRepository
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
       
        public AccountRepository(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager/*IdentityDbContext identityContext*/)
        {
            //identityContext.Database.EnsureCreated();
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        static List<User> users = new List<User>
        {
            new User {
                UserName = "Danne",
                Email = "danne@danne.com",
                Password = "password",
                Address = new Address
                {
                    City = "Sthlm",
                    Street = "Hornstull",
                    Zipcode = "12344"
                }
            },
            new User {
                UserName = "Pontus",
                Email = "pontus@pontus.com",
                Password = "password",
                Address = new Address
                {
                    City = "Sthlm",
                    Street = "Kistagatan",
                    Zipcode = "16533"
                }
            },
            new User {
                UserName = "Håkan",
                Email = "hakan@hakan.com",
                Password = "password",
                Address = new Address
                {
                    City = "Sollentuna",
                    Street = "Sollentunagatan",
                    Zipcode = "12452"
                }
            },
            new User {
                UserName = "Cristian",
                Email = "cristian@cristian.com",
                Password = "password",
                Address = new Address
                {
                    City = "Solna",
                    Street = "Solnagatan",
                    Zipcode = "1113"
                }
            },
            new User {
                UserName = "Patrick",
                Email = "patrik@patrik.com",
                Password = "password",
                Address = new Address
                {
                    City = "Malmö",
                    Street = "Malmögatan",
                    Zipcode = "22344"
                }
            }
        };


        //public async Task<bool> AddUser(RegisterUserVM regVm)
        //{
        //    //var result =
        //    //    await userManager.CreateAsync(
        //    //        new IdentityUser(/*regVm.UserName*/
        //    //                         /* ), regVm.Password*/));
        //    //nu ska vi skapa en user i vår tabell
        //    //return result.Succeeded;err
        //    return true;
        //}

        //signinmanager
        public async Task<bool> Login(LoginVM loginVM)
        {
            var result = 
                await signInManager.PasswordSignInAsync(
                loginVM.Email, loginVM.Password, false, false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(RegisterUserVM registerUserVM)
        {
            var result = await userManager.CreateAsync(new IdentityUser(registerUserVM.UserName), registerUserVM.Password);
            if (result.Succeeded)
                await signInManager.PasswordSignInAsync(registerUserVM.UserName, registerUserVM.Password, false, false);
            //skapa egen användare i tabell
            return result.Succeeded;
        }

        public User[] GetAllUsers()
        {
            return users.ToArray();
        }
    }
}
