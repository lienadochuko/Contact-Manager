using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Entities
{
    public class PersonsDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set;}

        public DbSet<Country> Countries { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<Person>().ToTable("Persons");

            //sead to Countries
            string countriesJson = System.IO.File.ReadAllText("countries.json");

            List<Country> countries = System.Text.Json.JsonSerializer.Deserialize
                <List<Country>>(countriesJson);

            foreach (Country country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }
            
            //sead to Persons
            
            string personJson = System.IO.File.ReadAllText("persons.json");
            List<Person> persons = System.Text.Json.JsonSerializer.Deserialize
                <List<Person>>(personJson);
            foreach (Person person in persons)
            {
                modelBuilder.Entity<Person>().HasData(person);

            }

        }
    }
}
