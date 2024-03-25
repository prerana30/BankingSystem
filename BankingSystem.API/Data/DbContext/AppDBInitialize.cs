using BankingSystem.API.Entities;
using Microsoft.AspNetCore.Identity;

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

