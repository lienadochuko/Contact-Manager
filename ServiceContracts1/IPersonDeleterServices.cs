using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for manipulating Person entity
    /// </summary>
    public interface IPersonDeleterServices
    {

        /// <summary>
        /// Deletes a person based on the provided person id
        /// </summary>
        /// <param name="personID">personID to delete</param>
        /// <returns>Returns true, if deletion is successful else false</returns>
        Task<bool> DeletePerson(Guid? personID);
    }
}
