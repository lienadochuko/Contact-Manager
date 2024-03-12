using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using Rotativa.AspNetCore;
using RepositoryContract_s_;
using Repository_s_;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//Serilog
builder.Host.UseSerilog((HostBuilderContext context,
    IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
    .ReadFrom.Services(services); //read out current app's services and make them available to serilog
});


//add services into IoC container
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonServices, PersonService>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IPersonRepository, PersonsRepository>();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = 
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties |
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders;
});

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

app.UseHttpLogging();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.UseRotativa();

app.Run();
