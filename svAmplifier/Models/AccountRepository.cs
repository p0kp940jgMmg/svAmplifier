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
using Microsoft.AspNetCore.Mvc.Rendering;

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

        internal async Task<UserIndexLayoutVM> GetUserIndexLayoutVM()
        {
            UserIndexLayoutVM userIndexLayout = new UserIndexLayoutVM();

            var ltMarketItems = await GetUserMarketItems();
            var myPicks = await GetUserPicks();

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

        public async Task<bool> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                //TODO
                return false;
            }
        }

        public async Task<bool> RegisterUser(RegisterUserVM registerUserVM)
        {
            //skapa egen användare i tabell(AspUser)
            var user = new IdentityUser(registerUserVM.UserName)
            {
                //lägger till email
                Email = registerUserVM.Email
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
                    //City = registerUserVM.City,
                    //Street = registerUserVM.Street,
                    //Zipcode = registerUserVM.Zipcode,
                    //Phonenumber = registerUserVM.TelephoneNumber,
                    //Firstname = registerUserVM.FirstName,
                    //Lastname = registerUserVM.LastName,
                    Username = registerUserVM.UserName
                });

                await context.SaveChangesAsync();
            }
            return result.Succeeded;
        }

        public async Task<bool> UpdateUser(EditUserInfoVM updatedUser)
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
                userToUpdate.Firstname = updatedUser.Firstname;
                userToUpdate.Lastname = updatedUser.Lastname;
                userToUpdate.Street = updatedUser.Street;
                userToUpdate.City = updatedUser.City;
                userToUpdate.Zipcode = updatedUser.Zipcode;
                userToUpdate.Phonenumber = updatedUser.Phonenumber;

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

                //sätter PickID till nyvarande användarens UserID
                pickItem.UserId = GetCurrentUserId();
                pickItem.Username = GetCurrentUser().Username;
                var mushroom = await GetMushroom(Convert.ToInt32(pickItem.MushroomName));
                pickItem.MushroomName = mushroom.MushroomName;
                pickItem.MushroomPicUrl = mushroom.MushroomPicUrl;

                var region = await GetRegionCode(Convert.ToInt32(pickItem.Region));
                string regionTrim = region.RegionId.Trim();
                pickItem.Region = regionTrim;

                //var trimmedSum = Convert.ToDecimal(pickItem.Price.ToString().Trim('0'));

                //pickItem.Price = trimmedSum;

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

        public async Task<SelectListItem[]> GetMushrooms()
        {

            var mushrooms = await context.Mushrooms
            .Select(m => new SelectListItem { Text = m.MushroomName, Value = m.Id.ToString() })
            .ToArrayAsync();

            return mushrooms;
        }

        public async Task<SelectListItem[]> GetRegions()
        {

            var regions = await context.Regions
            .Select(m => new SelectListItem { Text = m.Region, Value = m.Id.ToString() })
            .ToArrayAsync();

            return regions;
        }

        //Hämtar de 20 senaste Market Items
        public async Task<Pick[]> GetLatestMarketItems()
        {
            return await context.Pick
                .Where(w => w.SalesItem == true)
                .OrderByDescending(o => o.DatePicked)
                .Take(20)
                .ToArrayAsync();
        }

        //Hämtar alla User Market Items
        public async Task<Pick[]> GetUserMarketItems()
        {
            return await context.Pick
                .Where(w => w.SalesItem == true && w.UserId == GetCurrentUserId())
                .OrderByDescending(o => o.Id)
                .ToArrayAsync();
        }

        //Hämtar alla User picks
        public async Task<Pick[]> GetUserPicks()
        {
            return await context.Pick
                .Where(w => w.SalesItem == false && w.UserId == GetCurrentUserId())
                .OrderByDescending(o => o.DatePicked)
                .ToArrayAsync();
        }

        public async Task<Pick[]> GetMarketItemsForRegion(string region)
        {
            return await context.Pick
                .Where(w => w.SalesItem == true && w.Region == region)
                .OrderByDescending(o => o.DatePicked)
                .ToArrayAsync();
        }

        public int GetCurrentUserId()
        {
            var aspUserId = userManager.GetUserId(contextAccessor.HttpContext.User);

            //Får inte vara Async, User blir null
            return context.User
                .FirstOrDefault(w => w.AspNetId == aspUserId).Id;
        }

        public User GetCurrentUser()
        {
            var aspUserId = userManager.GetUserId(contextAccessor.HttpContext.User);

            //Får inte vara Async, User blir null
            return context.User
                .FirstOrDefault(w => w.AspNetId == aspUserId);
        }

        public bool SetSessionUsername()
        {
            try
            {
                var userName = GetCurrentUser().Username;

                contextAccessor.HttpContext.Session.SetString("Username", userName);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string GetSessionUsername()
        {
            return contextAccessor.HttpContext.Session.GetString("Username");
        }

        private Task<Mushrooms> GetMushroom(int id)
        {
            return context.Mushrooms
                .FirstOrDefaultAsync(w => w.Id == id);
        }


        private Task<Regions> GetRegionCode(int code)
        {
            return context.Regions
                .FirstOrDefaultAsync(f => f.Id == code);
        }
    }
}
