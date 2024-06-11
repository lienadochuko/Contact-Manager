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
    public interface IPersonUpdaterServices
    {

        /// <summary>
        /// Update the specified person details based on the given person ID
        /// </summary>
        /// <param name="personUpdateRequest">Person Details to be updated</param>
        /// <returns>Retuns Person object after updating</returns>
        Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest);
    }
}
