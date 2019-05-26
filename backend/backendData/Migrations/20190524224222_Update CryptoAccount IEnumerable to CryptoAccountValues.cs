using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class UpdateCryptoAccountIEnumerabletoCryptoAccountValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_CryptoAccounts_CryptoAccountId",
                table: "AccountValues");

            migrationBuilder.DropIndex(
                name: "IX_AccountValues_CryptoAccountId",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "CryptoAccountId",
                table: "AccountValues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CryptoAccountId",
                table: "AccountValues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_CryptoAccountId",
                table: "AccountValues",
                column: "CryptoAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_CryptoAccounts_CryptoAccountId",
                table: "AccountValues",
                column: "CryptoAccountId",
                principalTable: "CryptoAccounts",
                principalColumn: "CryptoAccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
