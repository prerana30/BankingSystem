using BankingSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

public class ApplicationDbContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid>
{
    //Defining Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    //Define Databases
    public DbSet<Users> Users { get; set; }
    //Define Kyc Database
    public DbSet<KycDocument> KycDocument { get; set; }

    public DbSet<Accounts> Accounts { get; set; }

    public DbSet<Transaction> Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //seed database

        //ignore attributes
       /* modelBuilder.Entity<KycDocument>().Ignore(k=> k.UserImageFile);
        modelBuilder.Entity<KycDocument>().Ignore(k => k.CitizenshipImageFile);*/


        modelBuilder.Entity<Users>()
        .HasIndex(u => u.Email)
        .IsUnique();

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        //define foreign key relations

        /*modelBuilder.Entity<Transaction>()
              .HasData(
                  new Transaction()
                  {
                      TransactionId = Guid.NewGuid(),
                      AccountId = Guid.NewGuid(),
                      TransactionType = TransactionType.Deposit,
                      Amount = 20.29,
                      TransactionTime = DateTime.UtcNow.ToUniversalTime(),
                      TransactionRemarks = "Personal Use"
                  },
                  new Transaction()
                  {
                      TransactionId = Guid.NewGuid(),
                      AccountId = Guid.NewGuid(),
                      TransactionType = TransactionType.Withdraw,
                      Amount = 10.09,
                      TransactionTime = DateTime.UtcNow.ToUniversalTime(),
                      TransactionRemarks = "Personal Use WWW"
                  },

                  new Transaction()
                  {
                      TransactionId = Guid.NewGuid(),
                      AccountId = Guid.NewGuid(),
                      TransactionType = TransactionType.Deposit,
                      Amount = 30.29,
                      TransactionTime = DateTime.UtcNow.ToUniversalTime(),
                      TransactionRemarks = "Personalwqq Use"
                  }
              );*/

        DateTime dateOfBirth = DateTime.SpecifyKind(DateTime.ParseExact("2002-08-20T11:13:25.342Z", "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture), DateTimeKind.Utc);

        DateTime dateOfBirth1 = DateTime.SpecifyKind(DateTime.ParseExact("2000-03-23T11:13:25.342Z", "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture), DateTimeKind.Utc);

        /*modelBuilder.Entity<Accounts>().HasData(new Accounts()
        {
            AccountId= Guid.NewGuid(),
            AccountNumber= 1234627838293832,
            AtmCardNum=18462873844833,
            AtmCardPin=1242,
            Id= Guid.NewGuid(),
            AccountCreatedAt = dateOfBirth1,    
            
        });*/
/*
        modelBuilder.Entity<Users>()
            .HasData(
                new Users()
                {
                     Id= Guid.NewGuid(),
                    Username = "subs",
                    Fullname = "Subriti Aryal",
                    Address = "Gothatar, Kathmandu",
                    Email = "subritiaryal13@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Subriti123"),
                    PhoneNumber="9843346520",
                    CreatedAt = DateTime.UtcNow.ToUniversalTime(),
                    DateOfBirth = dateOfBirth,
                    ModifiedAt = DateTime.UtcNow.ToUniversalTime(),
                    UserType = UserRoles.AccountHolder
                },
                new Users()
                {
                     Id= Guid.NewGuid(),
                    Username = "admin",
                    Fullname = "Teller Person",
                    Address = "Kathmandu",
                    Email = "teller@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Teller123"),
                    PhoneNumber = "9826274833",
                    CreatedAt = DateTime.UtcNow.ToUniversalTime(),
                    DateOfBirth = dateOfBirth1,
                    ModifiedAt = DateTime.UtcNow.ToUniversalTime(),
                    UserType = UserRoles.TellerPerson
                },
                new Users()
                {
                     Id= Guid.NewGuid(),
                    Username = "user",
                    Fullname = "Account Holder",
                    Address = "Kathmandu",
                    Email = "accountUser@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("User123"),
                    PhoneNumber = "9830274849",
                    CreatedAt = DateTime.UtcNow.ToUniversalTime(),
                    DateOfBirth = dateOfBirth1,
                    ModifiedAt = DateTime.UtcNow.ToUniversalTime(),
                    UserType = UserRoles.AccountHolder
                }
            );
*/
        base.OnModelCreating(modelBuilder);
    }

}