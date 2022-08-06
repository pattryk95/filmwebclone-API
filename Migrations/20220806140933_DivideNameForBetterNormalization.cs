using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmwebclone_API.Migrations
{
    public partial class DivideNameForBetterNormalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Actors",
                newName: "MiddleName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Actors",
                type: "varchar(120)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Actors",
                type: "varchar(120)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Actors");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Actors",
                newName: "Name");
        }
    }
}
