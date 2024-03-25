using BankingSystem.API.Entities;
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
            try
            {
                var existingUser = await userManager.FindByEmailAsync(email);
                if (existingUser == null)
                {
                    var newUser = new Users
                    {
                        //Username = username,
                        UserName = username,
                        Fullname = fullname,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Address = address,
                        DateOfBirth = DateTime.SpecifyKind(DateTime.ParseExact(dateOfBirth, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture), DateTimeKind.Utc),
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        //Password = $"{username}123",
                        //UserType = role
                    };

                    var result = await userManager.CreateAsync(newUser, $"{username}123");
                    if (result.Succeeded)
                    {

                        await userManager.AddToRoleAsync(newUser, role);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(nameof(ex));

            }
        }
    }
}

