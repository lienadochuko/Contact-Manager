﻿using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonServices
    {
        /// <summary>
        /// Adds a new person into the list of persons
        /// </summary>
        /// <param name="personAddRequest"></param>
        /// <returns>Returns the same person details along with newly
        /// generated PersonId</returns>
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        /// <summary>
        /// Returns all person
        /// </summary>
        /// <returns>Returns a list of object of PersonResponse type</returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Returns the person object based on the given person id
        /// </summary>
        /// <param name="personID"></param>
        /// <returns>Return matching person object </returns>
        PersonResponse? GetPersonByPersonID(Guid? personID);

        /// <summary>
        /// Return the list of all person object that
        /// matches the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Return the list of all person object that
        /// matches the given search field and search string</returns>
        List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);


        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">Represents the list of persons to sort</param>
        /// <param name="sortBy">Name of the property (key), based on which the 
        /// list of persons should be sorted</param>
        /// <param name="sortOrderOptions">ASC or DESC</param>
        /// <returns>Returns sorted list of persons as PersonResponse list</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons,
            string sortBy, SortOrderOptions sortOrderOptions);
    }
}
