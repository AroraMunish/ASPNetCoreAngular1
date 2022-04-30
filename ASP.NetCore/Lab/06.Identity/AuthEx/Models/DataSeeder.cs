using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthEx.Models
{
    public class DataSeeder
    {
        private IServiceProvider _serviceProvider;
        private AuthExDBContext _context;

        public DataSeeder(IServiceProvider serviceProvider, AuthExDBContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public async Task CreateRolesAsync()
        {
            _context.Database.EnsureCreated();
            //initializing custom roles   
            var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "User", "HR" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("adminuser@gmail.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "adminuser@gmail.com",
                    Email = "adminuser@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");


            IdentityUser user1 = await UserManager.FindByEmailAsync("generaluser@gmail.com");

            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "generaluser@gmail.com",
                    Email = "generaluser@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123");
            }
            await UserManager.AddToRoleAsync(user1, "User");

            IdentityUser user2 = await UserManager.FindByEmailAsync("HRuser@gmail.com");

            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = "HRuser@gmail.com",
                    Email = "HRuser@gmail.com",
                };
                await UserManager.CreateAsync(user2, "Test@123");
            }
            await UserManager.AddToRoleAsync(user2, "HR");

        }

    }
}
