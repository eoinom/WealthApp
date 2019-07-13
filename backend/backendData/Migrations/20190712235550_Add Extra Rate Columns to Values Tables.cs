using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class AddExtraRateColumnstoValuesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RateToUserCurrency",
                table: "LoanValues",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueUserCurrency",
                table: "LoanValues",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateToUserCurrency",
                table: "CryptoAccountValues",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueUserCurrency",
                table: "CryptoAccountValues",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateToUserCurrency",
                table: "CreditCardValues",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueUserCurrency",
                table: "CreditCardValues",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateToUserCurrency",
                table: "AssetValues",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueUserCurrency",
                table: "AssetValues",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RateToUserCurrency",
                table: "AccountValues",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueUserCurrency",
                table: "AccountValues",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateToUserCurrency",
                table: "LoanValues");

            migrationBuilder.DropColumn(
                name: "ValueUserCurrency",
                table: "LoanValues");

            migrationBuilder.DropColumn(
                name: "RateToUserCurrency",
                table: "CryptoAccountValues");

            migrationBuilder.DropColumn(
                name: "ValueUserCurrency",
                table: "CryptoAccountValues");

            migrationBuilder.DropColumn(
                name: "RateToUserCurrency",
                table: "CreditCardValues");

            migrationBuilder.DropColumn(
                name: "ValueUserCurrency",
                table: "CreditCardValues");

            migrationBuilder.DropColumn(
                name: "RateToUserCurrency",
                table: "AssetValues");

            migrationBuilder.DropColumn(
                name: "ValueUserCurrency",
                table: "AssetValues");

            migrationBuilder.DropColumn(
                name: "RateToUserCurrency",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "ValueUserCurrency",
                table: "AccountValues");
        }
    }
}
