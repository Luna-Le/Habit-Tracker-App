using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyHabitTrackerApp.Context;
using DotNetEnv;
using MyHabitTrackerApp.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddScoped<IHabitRepository, HabitRepository>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<HabitDb>(options =>
        options.UseInMemoryDatabase("Item"));
}
else
{
    var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__HabitContext");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'HabitContext' not found.");
    }

    builder.Services.AddDbContext<HabitDb>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "HabitTracker API", 
        Description = "Keep track of your habits", 
        Version = "v1" 
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "HabitTracker API V1");
   });
}
else
{
    
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    

}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();