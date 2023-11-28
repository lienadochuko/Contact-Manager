using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class InsertPersonStoredProcedure : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string sp_InsertPerson = @"
CREATE PROCEDURE [dbo].[InsertPerson]
(@PersonID uniqueidentifier, @PersonName nvarchar(40), @Email nvarchar(40), @DOB datetime2(7), @Gender nvarchar(10), @Address nvarchar(200), @CountryID uniqueidentifier, @RecieveNewsLetter bit, @NIN varchar(10))
AS BEGIN
INSERT INTO [dbo].[Persons](PersonID, PersonName, Email, DOB, Gender, Address, CountryID, RecieveNewsLetter, NIN) 
VALUES (@PersonID, @PersonName, @Email, @DOB, @Gender, @Address, @CountryID, @RecieveNewsLetter, @NIN)
END
";

			migrationBuilder.Sql(sp_InsertPerson);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string sp_InsertPerson = @"
DROP PROCEDURE [dbo].[InsertPerson]";
			migrationBuilder.Sql(sp_InsertPerson);
		}
	}
}
