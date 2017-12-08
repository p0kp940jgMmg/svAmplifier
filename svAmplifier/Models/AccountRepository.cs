using svAmplifier.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using svAmplifier.Models.Entities;
using svAmplifier.Models.VM;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace svAmplifier.Models
{
    public class AccountRepository
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        IHttpContextAccessor contextAccessor;
        UserContext context;

        public AccountRepository(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, /*IdentityDbContext identityContext*/
            IHttpContextAccessor contextAccessor,
            UserContext context)
        {
            //identityContext.Database.EnsureCreated();
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.contextAccessor = contextAccessor;
            this.context = context;
        }
        //static List<User> users = new List<User>
        //{
        //    new User {
        //        UserName = "Danne",
        //        Email = "danne@danne.com",
        //        Password = "password",
        //        Address = new Address
        //        {
        //            City = "Sthlm",
        //            Street = "Hornstull",
        //            Zipcode = "12344"
        //        }
        //    },
        //    new User {
        //        UserName = "Pontus",
        //        Email = "pontus@pontus.com",
        //        Password = "password",
        //        Address = new Address
        //        {
        //            City = "Sthlm",
        //            Street = "Kistagatan",
        //            Zipcode = "16533"
        //        }
        //    },
        //    new User {
        //        UserName = "Håkan",
        //        Email = "hakan@hakan.com",
        //        Password = "password",
        //        Address = new Address
        //        {
        //            City = "Sollentuna",
        //            Street = "Sollentunagatan",
        //            Zipcode = "12452"
        //        }
        //    },
        //    new User {
        //        UserName = "Cristian",
        //        Email = "cristian@cristian.com",
        //        Password = "password",
        //        Address = new Address
        //        {
        //            City = "Solna",
        //            Street = "Solnagatan",
        //            Zipcode = "1113"
        //        }
        //    },
        //    new User {
        //        UserName = "Patrick",
        //        Email = "patrik@patrik.com",
        //        Password = "password",
        //        Address = new Address
        //        {
        //            City = "Malmö",
        //            Street = "Malmögatan",
        //            Zipcode = "22344"
        //        }
        //    }
        //}


        public async Task<bool> Login(LoginVM loginVM)
        {
            var result =
                await signInManager.PasswordSignInAsync(
                loginVM.Email, loginVM.Password, false, false);

            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(RegisterUserVM registerUserVM)
        {
            //skapa egen användare i tabell(AspUser)
            var user = new IdentityUser(registerUserVM.UserName);

            //lägger till email+Phonenumber i AspUser
            user.Email = registerUserVM.Email;
            user.PhoneNumber = registerUserVM.TelephoneNumber;

            var result = await userManager.CreateAsync(user, registerUserVM.Password);

            //om lyckad
            if (result.Succeeded)
            {
                //loggar vi in användaren
                await signInManager.PasswordSignInAsync(registerUserVM.UserName, registerUserVM.Password, false, false);

                //skapar en ny User med primarykey=AspNetID
                var userId = await userManager.GetUserIdAsync(user);
                await context.User.AddAsync(new User
                {
                    AspNetId = userId,
                    City = registerUserVM.City,
                    Street = registerUserVM.Street,
                    Zipcode = registerUserVM.Zipcode,
                    Phonenumber = registerUserVM.TelephoneNumber,
                    Firstname = registerUserVM.FirstName,
                    Lastname = registerUserVM.LastName
                });

                await context.SaveChangesAsync();
            }
            return result.Succeeded;
        }

        public async Task<bool> UpdateUser(RegisterUserVM updatedUser)
        {
            //Hämtar nuvarande användaren
            var currentUserId = userManager.GetUserId(contextAccessor.HttpContext.User);
            IdentityUser user = await userManager.FindByIdAsync(currentUserId);

            //Hämtar User för nuvarande användaren med AspId
            var userToUpdate = await context.User
               .SingleOrDefaultAsync(s => s.AspNetId == currentUserId);

            if (userToUpdate == null)
            {
                return false;
            }

            try
            {
                userToUpdate.Firstname = updatedUser.FirstName;
                userToUpdate.Lastname = updatedUser.LastName;
                userToUpdate.Street = updatedUser.Street;
                userToUpdate.City = updatedUser.City;
                userToUpdate.Zipcode = updatedUser.Zipcode;
                userToUpdate.Phonenumber = updatedUser.TelephoneNumber;

                await context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                //TODO
                return false;
            }
        }

        public async Task<bool> AddPick(Pick pickItem)
        {
            try
            {
                //Hämtar nuvarande användarens AspID
                var userAspId = userManager.GetUserId(contextAccessor.HttpContext.User);

                //Hämtar nuvarande användarens User med AspID
                User user = context.User
                    .FirstOrDefault(u => u.AspNetId == userAspId);

                //sätter PickID till nyvarande användarens UserID
                pickItem.UserId = user.Id;

                await context.Pick.AddAsync(pickItem);
                await context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException e)
            {
                //TO DO
                return false;
            }

        }

        public async Task<bool> RemovePick(int pickItemID)
        {

            var pickToRemove = await context.Pick
                .SingleOrDefaultAsync(u => u.Id == pickItemID);

            if (pickToRemove == null)
            {
                return false;
            }

            try
            {
                context.Pick.Remove(pickToRemove);
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                //TO DO log error
                return false;
            }

        }

        public async Task<bool> UpdatePick(Pick updatedPick)
        {

            var pickToUpdate = await context.Pick
                .SingleOrDefaultAsync(s => s.Id == updatedPick.Id);

            if (pickToUpdate == null)
            {
                return false;
            }

            try
            {
                pickToUpdate = updatedPick;
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                //TODO
                return false;
            }
        }

    }
}


        //public User[] GetAllUsers()
        //{
        //    return users.ToArray();
        //}
    }
}
