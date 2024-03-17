using BankingSystem.API.IRepository;
using BankingSystem.API.Services;
using Microsoft.EntityFrameworkCore;
using RESTful_API__ASP.NET_Core.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registering the service
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //searches for all profiles automatically

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
