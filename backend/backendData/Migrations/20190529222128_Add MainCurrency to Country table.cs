using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class AddMainCurrencytoCountrytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainCurrencyCode",
                table: "Countries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_MainCurrencyCode",
                table: "Countries",
                column: "MainCurrencyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Currencies_MainCurrencyCode",
                table: "Countries",
                column: "MainCurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Currencies_MainCurrencyCode",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_MainCurrencyCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "MainCurrencyCode",
                table: "Countries");
        }
    }
}
