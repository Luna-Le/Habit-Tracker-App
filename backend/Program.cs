using MyHabitTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyHabitTrackerApp.Context;
using MyHabitTrackerApp.Repositories;
using Microsoft.AspNetCore.Identity;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext before building the app
var environment = builder.Environment;
var useSqlServer = false;

// database injection
if (environment.IsDevelopment() && !useSqlServer)
{
    builder.Services.AddDbContext<HabitContext>(options =>
        options.UseInMemoryDatabase("Habit"));
}
else
{   var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__HabitContext");
     if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'HabitContext' not found.");
    }
    builder.Services.AddDbContext<HabitContext>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddScoped<IHabitRepository, HabitRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
       
        options.User.RequireUniqueEmail = true; // Ensure email addresses are unique
       
    })
    .AddEntityFrameworkStores<HabitContext>()
    .AddDefaultTokenProviders();

     


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    
    var securityScheme = new OpenApiSecurityScheme //allows swagger to create api in which only authenticated user can get access to their habits
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            new string[] {}
        }
    };
    c.AddSecurityRequirement(securityRequirement);
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();