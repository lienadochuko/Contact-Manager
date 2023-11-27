using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonStoredProcedure : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string sp_UpdatePerson = @"
CREATE PROCEDURE [dbo].[UpdatePerson]
(@PersonID uniqueidentifier, @PersonName nvarchar(40), @Email nvarchar(40), @DOB datetime2(7), @Gender nvarchar(10), @Address nvarchar(200), @CountryID uniqueidentifier, @RecieveNewsLetter bit, @NIN nvarchar(max))
AS BEGIN
UPDATE [dbo].[Persons] SET PersonName = @PersonName, Email = @Email, DOB = @DOB, Gender = @Gender, Address = @Address, CountryID = @CountryID, RecieveNewsLetter = @RecieveNewsLetter, NIN = @NIN
WHERE PersonID = @PersonID
END
";

			migrationBuilder.Sql(sp_UpdatePerson);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string sp_UpdatePerson = @"
DROP PROCEDURE [dbo].[UpdatePerson]";
			migrationBuilder.Sql(sp_UpdatePerson);
		}
	}
}
