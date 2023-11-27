﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class DeletePersonStoredProcedure : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			string sp_DeletePerson = @"
CREATE PROCEDURE [dbo].[DeletePerson]
(@PersonID uniqueidentifier)
AS BEGIN
DELETE FROM [dbo].[Persons] WHERE PersonID = @PersonID 
END
";

			migrationBuilder.Sql(sp_DeletePerson);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			string sp_DeletePerson = @"
DROP PROCEDURE [dbo].[DeletePerson]";
			migrationBuilder.Sql(sp_DeletePerson);
		}
	}
}
