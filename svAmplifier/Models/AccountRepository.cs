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

        internal async Task<UserIndexLayoutVM> GetUserIndexVM()
        {
            UserIndexLayoutVM userIndexLayout = new UserIndexLayoutVM();

            var ltMarketItems = await context.Pick
                .Where(w => w.SalesItem == true)
                .OrderByDescending(o => o.DatePicked)
                .Take(5)
                .ToArrayAsync();

            var myPicks = await context.Pick
                .Where(w => w.SalesItem == false)
                .OrderByDescending(o => o.DatePicked)
                .ToArrayAsync();

            userIndexLayout.LatestMarketItems = ltMarketItems;
            userIndexLayout.Picks = myPicks;

            return userIndexLayout;
        }

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
            var user = new IdentityUser(registerUserVM.UserName)
            {
                //lägger till email+Phonenumber i AspUser
                Email = registerUserVM.Email,
                PhoneNumber = registerUserVM.TelephoneNumber
            };

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
                pickItem.DatePicked = new DateTime(2017, 12, 08);
                pickItem.MushroomName = "MushroomTest";
                pickItem.MushroomPicUrl = "test";

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
