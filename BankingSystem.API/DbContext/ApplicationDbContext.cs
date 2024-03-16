using BankingSystem.API.Models;
using Microsoft.EntityFrameworkCore;

internal class ApplicationDbContext : DbContext
{
    //Defining Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    //Define Databases
    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //seed database

        //ignore attributes

        //define foreign key relations

        base.OnModelCreating(modelBuilder);
    }
}