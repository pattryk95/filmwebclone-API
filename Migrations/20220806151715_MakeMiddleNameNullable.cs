using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmwebclone_API.Migrations
{
    public partial class MakeMiddleNameNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Actors",
                type: "varchar(120)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(120)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Actors",
                type: "varchar(120)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldNullable: true);
        }
    }
}
