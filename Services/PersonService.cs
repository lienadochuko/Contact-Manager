using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using ServiceContracts.Enums;

namespace Services
{
    public class PersonService : IPersonServices
    {
        //private field
        private readonly List<Person> _persons;
       private readonly ICountriesService _countriesService;

        public PersonService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();

            if (initialize)
            {
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("F461A9C2-B695-4E47-9113-DEEC98C91830"),
                    PersonName = "Grace Davids",
                    Email = "davids.Grace@gmail.com",
                    Address = "324 DavidsViews",
                    Gender = "Male",
                    DOB = DateTime.Parse("2023-12-03"),
                    CountryID = Guid.Parse("EE69E6B7-641C-4ABB-80B6-1D3971EDC904"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("6EBDEF38-3035-4499-AC23-690F9641F448"),
                    PersonName = "Wheeler Hucknall",
                    Email = "whucknall1n@google.it",
                    Address = "2 Washington Trail",
                    Gender = "Male",
                    DOB = DateTime.Parse("2001-05-12"),
                    CountryID = Guid.Parse("A5386467-0411-44CA-90C8-E3A26B655B94"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("6EBDEF38-3035-4499-AC23-690F9641F448"),
                    PersonName = "Oliver Duigan",
                    Email = "oduigan1o@plala.or.jp",
                    Address = "933 Kennedy Hill",
                    Gender = "Male",
                    DOB = DateTime.Parse("2000-02-16"),
                    CountryID = Guid.Parse("84312DB0-4672-407D-B212-87E550C74428"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("C9B583CA-EBA3-428A-BDCC-C692D086EA61"),
                    PersonName = "Kessiah Cowwell",
                    Email = "kcowwell1p@mediafire.com",
                    Address = "0958 Bashford Park",
                    Gender = "Female",
                    DOB = DateTime.Parse("1992-04-12"),
                    CountryID = Guid.Parse("BF9830C7-8E3D-485C-8FB1-9C7F9652FC75"),
                    RecieveNewsLetter = false,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("852E5F08-D85D-43DC-A485-88A7AD445D8B"),
                    PersonName = "Rosalyn Dawidowitz",
                    Email = "rdawidowitz1q@goo.gl",
                    Address = "20 Johnson Point",
                    Gender = "Female",
                    DOB = DateTime.Parse("1999-01-06"),
                    CountryID = Guid.Parse("1E00DAEA-F817-4375-9EC9-A6E6BD48BACE"),
                    RecieveNewsLetter = false,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("0887293C-1D55-46FD-B7FF-6033B62BE031"),
                    PersonName = "Blaine Algar",
                    Email = "balgar1r@altervista.org",
                    Address = "00098 Hanson Hill",
                    Gender = "Male",
                    DOB = DateTime.Parse("1995-01-07"),
                    CountryID = Guid.Parse("09508EB3-57DB-49CE-B368-0A00FFB75828"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("0F8128D1-9F2A-4656-803E-79BF6DE10818"),
                    PersonName = "Maureen Grotty",
                    Email = "mgrotty1s@zdnet.com",
                    Address = "4 Bultman Junction",
                    Gender = "Female",
                    DOB = DateTime.Parse("1992-12-05"),
                    CountryID = Guid.Parse("40DB2E52-37AB-43C3-8B22-EB24A2084DED"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("1DD4E94B-BE3B-4AD4-9A1E-C15FFA625CBB"),
                    PersonName = "Lethia Laurenceau",
                    Email = "llaurenceau1t@cafepress.com",
                    Address = "84 Barnett Avenue",
                    Gender = "Female",
                    DOB = DateTime.Parse("1993-07-15"),
                    CountryID = Guid.Parse("E801E3C0-7835-4760-9E0B-27078011A2E5"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("30DA7678-7CCC-4E26-96E6-F3F51F4561F5"),
                    PersonName = "Clarie Pentelo",
                    Email = "cpentelo1u@posterous.com",
                    Address = "86 Rowland Avenue",
                    Gender = "Female",
                    DOB = DateTime.Parse("2001-01-22"),
                    CountryID = Guid.Parse("6E0A41B7-BAF4-4F2B-BACE-A654E87C664B"),
                    RecieveNewsLetter = false,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("BC4B49CC-8826-4781-A8FA-D4340B506E2B"),
                    PersonName = "Chandler Hutchason",
                    Email = "chutchason1v@theguardian.com",
                    Address = "05043 Katie Parkway",
                    Gender = "Male",
                    DOB = DateTime.Parse("1991-05-02"),
                    CountryID = Guid.Parse("7680241D-34B8-4F39-B494-87C27831866C"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("8FA6B3E5-5DC4-484C-A6AA-77A95B3BD00F"),
                    PersonName = "Keen Matisse",
                    Email = "kmatisse1w@a8.net",
                    Address = "892 Nova Place",
                    Gender = "Male",
                    DOB = DateTime.Parse("1998-02-24"),
                    CountryID = Guid.Parse("A6939D16-EA43-442C-ADEE-68738A2B39CB"),
                    RecieveNewsLetter = false,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("B34AD391-ADB2-4837-9293-68BCCB99B199"),
                    PersonName = "Mickey Czajkowski",
                    Email = "mczajkowski1x@ning.com",
                    Address = "60021 Westend Junction",
                    Gender = "Male",
                    DOB = DateTime.Parse("1992-08-05"),
                    CountryID = Guid.Parse("9CE71166-8A69-481D-9C5C-2EA5AAC9E73B"),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("0078BAA8-586B-4EC6-985D-78C2A80D47CC"),
                    PersonName = "Calhoun Hansom",
                    Email = "chansom2r@hugedomains.com",
                    Address = "87545 Village Green Hill",
                    Gender = "Male",
                    DOB = DateTime.Parse("2000-03-10"),
                    CountryID = Guid.Parse("C3EE9030-141F-4B92-B1AC-DF45A84A9046"),
                    RecieveNewsLetter = true,
                });
            }
        }

        //convert the Person object into PersonResponse type
        private PersonResponse ConvertPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.
                GetCountryByCountryID(personResponse.CountryID)?.CountryName;

            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //Validate personAddRequest
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            //Model Validate PersonName
            ValidationHelper.ModelValidation(personAddRequest);


            //Convert personAddRequest into Person type
           Person person = personAddRequest.ToPerson();

            //generate personID
            person.PersonID = Guid.NewGuid();

            //add person to person list
            _persons.Add(person);

            //convert the Person object into PersonResponse type
         return ConvertPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(person => person.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

           Person? person = _persons.FirstOrDefault(temp => temp.PersonID == personID);
            if(person == null)
                return null;


            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPerson = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPerson;

            switch(searchBy)
            {
                case nameof(PersonResponse.PersonName): matchingPerson = allPersons.Where(temp =>
                (!string.IsNullOrEmpty(temp.PersonName)? temp.PersonName.Contains(
                    searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(PersonResponse.Email):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(PersonResponse.DOB):
                    matchingPerson = allPersons.Where(temp =>
                    (temp.DOB != null) ?
                    (temp.DOB.Value.ToString("dd MMMM yyyy").Contains(
                        searchString, StringComparison.OrdinalIgnoreCase)) : true).ToList();

                    break;


                case nameof(PersonResponse.Gender):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(PersonResponse.CountryID):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(PersonResponse.Address):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                default: matchingPerson = allPersons; break;
            }
            return matchingPerson;
        }

        public List<PersonResponse> GetSortedPersons
            (List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonResponse> sortedPerson = (sortBy, sortOrder)
                switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DOB), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.DOB).ToList(),

                (nameof(PersonResponse.DOB), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.DOB).ToList(),


                (nameof(PersonResponse.Age), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.RecieveNewsLetter), SortOrderOptions.ASC)
               => allPersons.OrderBy(temp => temp.RecieveNewsLetter).ToList(),

                (nameof(PersonResponse.RecieveNewsLetter), SortOrderOptions.DESC)
               => allPersons.OrderByDescending(temp => temp.RecieveNewsLetter).ToList(),


               _=> allPersons
            };

            return sortedPerson;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);


            if (personUpdateRequest.PersonID == new Guid())
                throw new ArgumentException(nameof(personUpdateRequest.PersonID));
                
            
            Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonID);
            if (matchingPerson == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DOB = personUpdateRequest.DOB;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.RecieveNewsLetter = personUpdateRequest.RecieveNewsLetter;

            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));


            Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonID == personID);
            if (matchingPerson == null)
            {
                return false;
            }

            _persons.RemoveAll(temp => temp.PersonID == personID);

            return true;
        }
    }
}
