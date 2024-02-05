using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using Rotativa.AspNetCore;
using RepositoryContract_s_;
using Repository_s_;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//logging
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventLog();
});

//add services into IoC container
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonServices, PersonService>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IPersonRepository, PersonsRepository>();

builder.Services.AddDbContext<ApplicationDbContext>
    (
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
//Rotativa.AspNetCore.RotativaConfiguration.Setup(builder.Environment.WebRootPath, "Rotativa");
//RotativaConfiguration.Setup(builder.Environment.WebRootPath, "Rotativa");
//app.Logger.LogDebug("Debug Message");
//app.Logger.LogInformation("Information Message");
//app.Logger.LogWarning("Warning Message");
//app.Logger.LogError("Error Message");
//app.Logger.LogCritical("Critical Message");1
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.UseRotativa();

app.Run();
