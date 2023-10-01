using System;
using ServiceContracts.Enums;
using Entities;

namespace ServiceContracts.DTO
{
    public class PersonAddRequest
    {
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public GenderOptions? Gender { get; set; }
        public string? Address { get; set; }
        public Guid? CountryID { get; set; }
        public bool RecieveNewsLetter { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PersonName = PersonName,
                Email = Email,
                DOB = DOB,
                Gender = Gender.ToString(),
                Address = Address,
                CountryID = CountryID,
                RecieveNewsLetter = RecieveNewsLetter
            };
        }
    }
}
