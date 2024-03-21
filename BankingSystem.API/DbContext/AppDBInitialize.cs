using AutoMapper;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingSystem.API.DbContext
{
    public static class AppDBInitialize
    {
        public static async Task SeedUsersAndUserRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Users>>();

                // Seed Roles
                await SeedRoleAsync(roleManager, UserRoles.AccountHolder);
                await SeedRoleAsync(roleManager, UserRoles.TellerPerson);

                // Seed Users
                await SeedUserAsync(userManager, "admin@gmail.com", "admin", "Teller Person", "Kathmandu", "9826274833", "2002-08-20T11:13:25.342Z", UserRoles.TellerPerson);
                await SeedUserAsync(userManager, "subritiaryal13@gmail.com", "subs", "Subriti Aryal", "Gothatar, Kathmandu", "9843346520", "2002-08-20T11:13:25.342Z", UserRoles.AccountHolder);
                await SeedUserAsync(userManager, "accountUser@gmail.com", "user", "Account Holder", "Kathmandu", "9830274849", "2000-03-23T11:13:25.342Z", UserRoles.AccountHolder);

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

        private static async Task SeedUserAsync(UserManager<Users> userManager, string email, string username, string fullname, string address, string phoneNumber, string dateOfBirth, string role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var newUser = new Users
                {
                    Username = username,
                    Fullname = fullname,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    DateOfBirth = DateTime.SpecifyKind(DateTime.ParseExact(dateOfBirth, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture), DateTimeKind.Utc),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, $"{username}123");
                try
                {
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(newUser, role);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new Exception(nameof(ex));

                }
            }
        }
    }
}
