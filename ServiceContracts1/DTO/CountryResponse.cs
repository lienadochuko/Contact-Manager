﻿using System;
using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO Class used a return type for most of CountriesServices method
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }

        public string? CountryName { get; set; }


        //It compares the current oject to another object of
        //CountryResponse type and returns true if both values are same otherwiase returns false
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(CountryResponse)) return false;

            CountryResponse country_to_comapare = (CountryResponse)obj;

            return this.CountryID == country_to_comapare.CountryID && 
                this.CountryName == country_to_comapare.CountryName;
        }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }
}
