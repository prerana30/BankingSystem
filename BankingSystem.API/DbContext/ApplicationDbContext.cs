using BankingSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

public class ApplicationDbContext : DbContext
{
    //Defining Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
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
        modelBuilder.Entity<KycDocument>().Ignore(k=> k.UserImagePath);
        modelBuilder.Entity<KycDocument>().Ignore(k => k.CitizenshipImagePath);

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

        modelBuilder.Entity<Users>()
            .HasData(
                new Users()
                {
                    UserId = Guid.NewGuid(),
                    Username = "subs",
                    Fullname = "Subriti Aryal",
                    Address = "Gothatar, Kathmandu",
                    Email = "subritiaryal13@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Subriti123"),
                    CreatedAt = DateTime.UtcNow.ToUniversalTime(),
                    DateOfBirth = dateOfBirth,
                    ModifiedAt = DateTime.UtcNow.ToUniversalTime(),
                    UserType = Roles.AccountHolder
                },
                new Users()
                {
                    UserId = Guid.NewGuid(),
                    Username = "admin",
                    Fullname = "Teller Person",
                    Address = "Kathmandu",
                    Email = "teller@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Teller123"),
                    CreatedAt = DateTime.UtcNow.ToUniversalTime(),
                    DateOfBirth = dateOfBirth1,
                    ModifiedAt = DateTime.UtcNow.ToUniversalTime(),
                    UserType = Roles.TellerPerson
                },
                new Users()
                {
                    UserId = Guid.NewGuid(),
                    Username = "user",
                    Fullname = "Account Holder",
                    Address = "Kathmandu",
                    Email = "accountUser@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("User123"),
                    CreatedAt = DateTime.UtcNow.ToUniversalTime(),
                    DateOfBirth = dateOfBirth1,
                    ModifiedAt = DateTime.UtcNow.ToUniversalTime(),
                    UserType = Roles.AccountHolder
                }
            );

        base.OnModelCreating(modelBuilder);
    }

}