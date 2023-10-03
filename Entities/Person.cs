using System;

namespace Entities
{
    public class Person
    {
        /// <summary>
        /// Person domain Model class
        /// </summary>
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public Guid? CountryID { get; set; }
        public bool RecieveNewsLetter { get; set; }
    }
}
