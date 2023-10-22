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
                    PersonID = Guid.Parse(""),
                    PersonName = "Oliver Duigan",
                    Email = "oduigan1o@plala.or.jp",
                    Address = "933 Kennedy Hill",
                    Gender = "Male",
                    DOB = DateTime.Parse("2000-02-16"),
                    CountryID = Guid.Parse(""),
                    RecieveNewsLetter = true,
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse(""),
                    PersonName = "Oliver Duigan",
                    Email = "oduigan1o@plala.or.jp",
                    Address = "933 Kennedy Hill",
                    Gender = "Male",
                    DOB = DateTime.Parse("2000-02-16"),
                    CountryID = Guid.Parse(""),
                    RecieveNewsLetter = true,
                });
                /*
Blaine Algar,balgar1r@altervista.org,00098 Hanson Hill,Male,1995-01-07,true
Maureen Grotty,mgrotty1s@zdnet.com,4 Bultman Junction,Female,1992-12-05,true
Lethia Laurenceau,llaurenceau1t@cafepress.com,84 Barnett Avenue,Female,1993-07-15,true
Clarie Pentelo,cpentelo1u@posterous.com,86 Rowland Avenue,Female,2001-01-22,false
Chandler Hutchason,chutchason1v@theguardian.com,05043 Katie Parkway,Male,1991-05-02,true
Keen Matisse,kmatisse1w@a8.net,892 Nova Place,Male,1998-02-24,false
Mickey Czajkowski,mczajkowski1x@ning.com,60021 Westend Junction,Male,1992-08-05,false
Cirstoforo Wroughton,cwroughton1y@mail.ru,33 Calypso Crossing,Male,1995-05-25,false
Licha Atcock,latcock1z@sphinn.com,25 Iowa Drive,Female,1993-01-30,true
Nicolais Hymus,nhymus20@pinterest.com,77732 Northland Road,Male,1995-09-09,true
Letizia Itzig,litzig21@unblog.fr,910 Stang Hill,Female,1997-10-21,true
Cesar Lias,clias22@wufoo.com,53 Hauk Crossing,Male,1996-09-09,false
Siusan Inchan,sinchan23@weibo.com,342 Maple Wood Crossing,Female,1993-09-23,true
Schuyler Greenall,sgreenall24@amazon.co.uk,62929 American Crossing,Male,1998-08-11,false
Fabio Ainslie,fainslie25@ovh.net,720 Esch Drive,Male,2002-02-27,true
Bendicty Whibley,bwhibley26@ebay.com,534 Macpherson Pass,Male,1991-08-09,true
Mathias Jeste,mjeste27@mail.ru,50 Dottie Junction,Male,1996-11-07,true
Bride Beechcraft,bbeechcraft28@mac.com,61 Hintze Junction,Female,1995-11-03,true
Sherlock Mantz,smantz29@cnbc.com,5 Beilfuss Parkway,Male,1996-12-10,false
Belinda Muffitt,bmuffitt2a@4shared.com,63 Prairieview Point,Female,1999-09-27,false
Kristofer Joska,kjoska2b@biglobe.ne.jp,49 Trailsway Street,Male,2000-09-24,true
Lurleen Simpkins,lsimpkins2c@illinois.edu,2474 Hintze Street,Female,1993-10-28,false
Ludwig Erangy,lerangy2d@foxnews.com,73 Onsgard Trail,Male,1998-08-16,true
Maurie Rumney,mrumney2e@newyorker.com,81402 Mallard Hill,Male,2001-09-12,false
Killy Tunnacliffe,ktunnacliffe2f@surveymonkey.com,4 Redwing Parkway,Male,2000-10-27,true
Doti Sparshutt,dsparshutt2g@wikipedia.org,16 Canary Way,Female,2000-08-05,false
Sissy Skate,sskate2h@yellowbook.com,3 Susan Junction,Female,1994-08-25,true
Blayne Leishman,bleishman2i@rambler.ru,94 Corben Trail,Male,1997-08-18,false
Mal Grimsley,mgrimsley2j@last.fm,85423 Gulseth Hill,Male,1996-09-12,false
Aylmar Dunnet,adunnet2k@yolasite.com,112 Leroy Hill,Male,1996-12-21,true
Debor Lampe,dlampe2l@geocities.com,7563 Londonderry Way,Female,1993-12-11,false
Dalston Maggi,dmaggi2m@blogspot.com,263 Porter Way,Male,1999-01-05,true
Wilma Paxeford,wpaxeford2n@disqus.com,79568 Calypso Street,Female,1995-08-17,false
Ginnifer Marcinkus,gmarcinkus2o@shop-pro.jp,469 David Place,Female,1992-06-26,true
Pauly Duckerin,pduckerin2p@ucla.edu,1941 Bunker Hill Center,Male,1992-09-22,false
Morganica De Witt,mde2q@mashable.com,21 Glacier Hill Parkway,Female,1992-06-28,true
Calhoun Hansom,chansom2r@hugedomains.com,87545 Village Green Hill,Male,2000-03-10,true
                 */
                // 
                // 
                //
                // 0887293C-1D55-46FD-B7FF-6033B62BE031
                // 0F8128D1-9F2A-4656-803E-79BF6DE10818
                // 1DD4E94B-BE3B-4AD4-9A1E-C15FFA625CBB
                // 30DA7678-7CCC-4E26-96E6-F3F51F4561F5
                // BC4B49CC-8826-4781-A8FA-D4340B506E2B
                // 8FA6B3E5-5DC4-484C-A6AA-77A95B3BD00F
                // B34AD391-ADB2-4837-9293-68BCCB99B199
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
                case nameof(Person.PersonName): matchingPerson = allPersons.Where(temp =>
                (!string.IsNullOrEmpty(temp.PersonName)? temp.PersonName.Contains(
                    searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(Person.Email):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(Person.DOB):
                    matchingPerson = allPersons.Where(temp =>
                    (temp.DOB != null) ?
                    (temp.DOB.Value.ToString("dd MMMM yyyy").Contains(
                        searchString, StringComparison.OrdinalIgnoreCase)) : true).ToList();

                    break;


                case nameof(Person.Gender):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;

                case nameof(Person.CountryID):
                    matchingPerson = allPersons.Where(temp =>
            (!string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(
                searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();

                    break;


                case nameof(Person.Address):
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
