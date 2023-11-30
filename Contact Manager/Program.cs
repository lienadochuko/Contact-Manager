using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add services into IoC container
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonServices, PersonService>();

builder.Services.AddDbContext<PersonsDbContext>
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

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.UseRotativa();

app.Run();
