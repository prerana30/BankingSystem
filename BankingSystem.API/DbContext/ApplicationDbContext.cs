using BankingSystem.API.Models;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //seed database

        //ignore attributes
        modelBuilder.Entity<KycDocument>().Ignore(k=> k.UserImagePath);
        modelBuilder.Entity<KycDocument>().Ignore(k => k.CitizenshipImagePath);

        //define foreign key relations

        base.OnModelCreating(modelBuilder);
    }

}