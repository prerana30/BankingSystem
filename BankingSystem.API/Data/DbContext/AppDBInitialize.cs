using BankingSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace BankingSystem.API.Data.DbContext
{
    public static class AppDBInitialize
    {
        public static async Task SeedConstantsAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Users>>();

                // Seed Roles
                await SeedRoleAsync(roleManager, UserRoles.AccountHolder.ToString());
                await SeedRoleAsync(roleManager, UserRoles.TellerPerson.ToString());

                // Seed Users
                await SeedUserAsync(userManager, "admin@gmail.com", "admin", "Teller Person", "Kathmandu", "9826274833", "2002-08-20T11:13:25.342Z", UserRoles.TellerPerson.ToString());
                await SeedUserAsync(userManager, "subritiaryal13@gmail.com", "subs", "Subriti Aryal", "Gothatar, Kathmandu", "9843346520", "2002-08-20T11:13:25.342Z", UserRoles.AccountHolder.ToString());
                await SeedUserAsync(userManager, "accountUser@gmail.com", "user", "Account Holder", "Kathmandu", "9830274849", "2000-03-23T11:13:25.342Z", UserRoles.AccountHolder.ToString());

                // Save Changes
               // Save Changes
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedRoleAsync(RoleManager<IdentityRole<Guid>> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }

    }
}

