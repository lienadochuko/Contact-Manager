using System;
using ServiceContracts.DTO;

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
    }
}
