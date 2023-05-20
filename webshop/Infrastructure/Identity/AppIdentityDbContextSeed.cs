using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var roles = new string[] { "Customer", "Admin" };


            if (!roleManager.Roles.Any())
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            if (!userManager.Users.Any())
            {
                var customer = new AppUser
                {
                    DisplayName = "TestCustomer",
                    Email = "testcustomer@gmail.com",
                    UserName = "testcustomer",
                    FirstName = "Test",
                    LastName = "Customer",
                    Address = new Address
                    {
                        Street = "Street test",
                        City = "City test",
                        State = "State test",
                        ZipCode = "11111"
                    }
                };

                await userManager.CreateAsync(customer, "Test!23");
                await userManager.AddToRoleAsync(customer, roles[0]);

                var admin = new AppUser
                {
                    DisplayName = "TestAdmin",
                    Email = "testadmin@gmail.com",
                    UserName = "testadmin",
                    FirstName = "Test",
                    LastName = "Admin",
                    Address = new Address
                    {
                        Street = "Street test",
                        City = "City test",
                        State = "State test",
                        ZipCode = "11111"
                    }
                };

                await userManager.CreateAsync(admin, "Test!23");
                await userManager.AddToRoleAsync(admin, roles[1]);
            }
        }
    }
}
