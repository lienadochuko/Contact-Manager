using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using OfficeOpenXml.Drawing;

namespace Services
{
    public class PersonService : IPersonServices
    {
        //private field
        //private readonly List<Person> _persons;
        private readonly PersonsDbContext _db;
        private readonly ICountriesService _countriesService;

        public PersonService(PersonsDbContext personsDbContext, ICountriesService countriesService)
        {
            //_persons = new List<Person>();

            _db = personsDbContext;
            _countriesService = countriesService;

        }

        public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
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
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            //_db.sp_InsertPerson(person);

            //convert the Person object into PersonResponse type
            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetAllPersons()
        {
            var persons = await _db.Persons.Include("country").ToListAsync();
            //SELECT * from Persons
            return persons.
                Select(person => person.ToPersonResponse()).ToList();


            //using storeProcedure
            //return _db.sp_GetAllPersons().Select(person =>  person.ToPersonResponse()).ToList();
        }

        public async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = await _db.Persons.Include("country").FirstOrDefaultAsync(temp => temp.PersonID == personID);
            if (person == null)
                return null;


            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = await GetAllPersons();
            List<PersonResponse> matchingPerson = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPerson;

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    matchingPerson = allPersons.Where(temp =>
                (!string.IsNullOrEmpty(temp.PersonName) ? temp.PersonName.Contains(
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

        public async Task<List<PersonResponse>> GetSortedPersons
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


                _ => allPersons
            };

            return sortedPerson;
        }

        public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(Person));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            if (personUpdateRequest.PersonID == new Guid())
                throw new ArgumentException(nameof(personUpdateRequest.PersonID));

            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(temp => temp.PersonID == personUpdateRequest.PersonID);
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
            matchingPerson.NIN = personUpdateRequest.NIN;
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.RecieveNewsLetter = personUpdateRequest.RecieveNewsLetter;

            //Console.WriteLine(personUpdateRequest.NIN);

            await _db.SaveChangesAsync();
            //_db.sp_UpdatePerson(matchingPerson);

            return matchingPerson.ToPersonResponse();
        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));


            Person? matchingPerson = await _db.Persons.FirstOrDefaultAsync(temp => temp.PersonID == personID);
            if (matchingPerson == null)
            {
                return false;
            }

            _db.Persons.Remove(await _db.Persons.FirstAsync(temp => temp.PersonID == personID));
            await _db.SaveChangesAsync();
            //_db.sp_DeletePerson(matchingPerson);


            return true;
        }

        public async Task<MemoryStream> GetPersonCSV()
        {
            MemoryStream memoryStream = new ();
            StreamWriter streamWriter = new (memoryStream);
           

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration, leaveOpen: true);

            //csvWriter.WriteHeader<PersonResponse>(); //using the model fields as heads such as PersonID, PersonName ...... 

            //await csvWriter.WriteRecordsAsync(persons);
            ////1, dan, dan@gmail.com ........

            csvWriter.WriteField(nameof(PersonResponse.PersonName));
            csvWriter.WriteField(nameof(PersonResponse.Email));
            csvWriter.WriteField(nameof(PersonResponse.DOB));
            csvWriter.WriteField(nameof(PersonResponse.Age));
            csvWriter.WriteField(nameof(PersonResponse.Gender));
            csvWriter.WriteField(nameof(PersonResponse.Address));
            csvWriter.WriteField(nameof(PersonResponse.Country));
            csvWriter.WriteField(nameof(PersonResponse.NIN));
            csvWriter.WriteField(nameof(PersonResponse.RecieveNewsLetter));
            csvWriter.NextRecord(); //goes to the next line (\n)

            List<PersonResponse> persons = await _db.Persons
                .Include("country")
                .Select(temp => temp.ToPersonResponse()).ToListAsync();

            foreach( PersonResponse person in persons )
            {
                csvWriter.WriteField(person.PersonName);
                csvWriter.WriteField(person.Email);
                csvWriter.WriteField(person.DOB.HasValue ? person.DOB.Value.ToString("yyyy-MM-dd") : "");
                csvWriter.WriteField(person.Age);
                csvWriter.WriteField(person.Gender);
                csvWriter.WriteField(person.Address);
                csvWriter.WriteField(person.Country);
                csvWriter.WriteField(person.NIN);
                csvWriter.WriteField(person.RecieveNewsLetter);
                csvWriter.NextRecord(); //goes to the next line (\n)
            }


            await csvWriter.FlushAsync(); //when the buffer in the stremwriter gets filled up it flushes to stream, At the end of writing,
                                          //you need to flush the writer so anything in the buffer gets written to the stream ensuring there is no missing record

            memoryStream.Position = 0;

            return memoryStream;
        }

        public async Task<MemoryStream> GetPersonExcel()
        {
           MemoryStream memoryStream = new ();

           using (ExcelPackage excelpackage = new (memoryStream))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "logoi.png");
                ExcelWorksheet workSheet = excelpackage.Workbook.Worksheets.Add("PersonsSheet");
                if (File.Exists(imagePath))
                {
                    // Add the image to the Excel file
                    FileInfo image = new (imagePath);
                    ExcelPicture picture = workSheet.Drawings.AddPicture("ImageName", image);
                    picture.SetSize(50);
                    picture.SetPosition(0, 0, 0, 0); // Adjust the position as needed
                } else
                {
                    // Handle the case when the image file does not exist
                    // You can log a message, throw an exception, etc.
                }

                using (ExcelRange excelRange = workSheet.Cells["A1:H1"])
                {
                    excelRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                }

                //Set Headings
                workSheet.Cells["A1"].Value = "Person Name";
                workSheet.Cells["B1"].Value = "Email";
                workSheet.Cells["C1"].Value = "Gender";
                workSheet.Cells["D1"].Value = "Date of Birth";
                workSheet.Cells["E1"].Value = "Age";
                workSheet.Cells["F1"].Value = "Address";
                workSheet.Cells["G1"].Value = "Country";
                workSheet.Cells["H1"].Value = "Recieve NewsLetter";


                //Align Headings
                workSheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["E1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["H1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //Bold
                workSheet.Cells["A1"].Style.Font.Bold = true;
                workSheet.Cells["B1"].Style.Font.Bold = true;
                workSheet.Cells["C1"].Style.Font.Bold = true;
                workSheet.Cells["D1"].Style.Font.Bold = true;
                workSheet.Cells["E1"].Style.Font.Bold = true;
                workSheet.Cells["F1"].Style.Font.Bold = true;
                workSheet.Cells["G1"].Style.Font.Bold = true;
                workSheet.Cells["H1"].Style.Font.Bold = true;
                //Set Pattern and BackgroundColor 
                workSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);

                workSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.Gray);


                int row = 2;

                List<PersonResponse> personResponses = await _db.Persons
                .Include("country")
                .Select(temp => temp.ToPersonResponse()).ToListAsync();

                foreach (PersonResponse person in personResponses)
                {
                    workSheet.Cells[row, 1].Value = person.PersonName;
                    workSheet.Cells[row, 2].Value = person.Email;
                    workSheet.Cells[row, 3].Value = person.Gender;
                    workSheet.Cells[row, 4].Value = person.DOB.HasValue ? person.DOB.Value.ToString("yyyy-MM-dd") : "";
                    workSheet.Cells[row, 5].Value = person.Age;
                    workSheet.Cells[row, 6].Value = person.Address;
                    workSheet.Cells[row, 7].Value = person.Country;
                    workSheet.Cells[row, 8].Value = person.RecieveNewsLetter;

                    workSheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    workSheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    workSheet.Cells[row, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    workSheet.Cells[row, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    workSheet.Cells[row, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    workSheet.Cells[row, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    workSheet.Cells[row, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right; ;
                    workSheet.Cells[row, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    row++;
                }

                workSheet.Cells[$"A1:H{row}"].AutoFitColumns();

                await excelpackage.SaveAsync();

                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }
}
