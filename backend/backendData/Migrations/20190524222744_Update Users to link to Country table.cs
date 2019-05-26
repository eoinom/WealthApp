using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class UpdateUserstolinktoCountrytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Users",
                newName: "CountryIso2Code");

            migrationBuilder.AlterColumn<string>(
                name: "CountryIso2Code",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryIso2Code",
                table: "Users",
                column: "CountryIso2Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Countries_CountryIso2Code",
                table: "Users",
                column: "CountryIso2Code",
                principalTable: "Countries",
                principalColumn: "Iso2Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Countries_CountryIso2Code",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CountryIso2Code",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CountryIso2Code",
                table: "Users",
                newName: "Country");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
