using BankingSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class ApplicationDbContext : DbContext
{
    //Defining Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    //Define Databases
    public DbSet<Users> Users { get; set; }

    public DbSet<Accounts> Accounts { get; set; }

    public DbSet<Transaction> Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //seed database

        //ignore attributes

        //define foreign key relations

        modelBuilder.Entity<Transaction>()
              .HasData(
                  new Transaction()
                  {
                      TransactionId = 1,
                      AccountId = 1,
                      TransactionType = TransactionType.Deposit,
                      Amount = 20.29,
                      TransactionTime = DateTime.UtcNow,
                      TransactionRemarks = "Personal Use"
                  },
                  new Transaction()
                  {
                      TransactionId = 2,
                      AccountId = 1,
                      TransactionType = TransactionType.Withdraw,
                      Amount = 10.09,
                      TransactionTime = DateTime.UtcNow,
                      TransactionRemarks = "Personal Use WWW"
                  },

                  new Transaction()
                  {
                      TransactionId = 3,
                      AccountId = 1,
                      TransactionType = TransactionType.Deposit,
                      Amount = 30.29,
                      TransactionTime = DateTime.UtcNow,
                      TransactionRemarks = "Personalwqq Use"
                  }
              );


        base.OnModelCreating(modelBuilder);
    }
}