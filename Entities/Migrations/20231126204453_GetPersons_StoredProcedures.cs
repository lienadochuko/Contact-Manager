﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class GetPersonsStoredProcedures : Migration
    {
			/// <inheritdoc />
			protected override void Up(MigrationBuilder migrationBuilder)
			{
				string sp_GetAllPersons = @"
			CREATE PROCEDURE [dbo].[GetAllPersons]
			AS BEGIN
			SELECT PersonID, PersonName, Email, DOB, Gender, Address, CountryID, RecieveNewsLetter, NIN FROM [dbo].[Persons]
			END";

				migrationBuilder.Sql(sp_GetAllPersons);

			}

			/// <inheritdoc />
			protected override void Down(MigrationBuilder migrationBuilder)
			{
				string sp_GetAllPersons = @"
			DROP PROCEDURE [dbo].[GetAllPersons]";

				migrationBuilder.Sql(sp_GetAllPersons);
			}
	}
}
