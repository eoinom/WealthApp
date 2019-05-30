using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class UpdatestoBankAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "Institution",
                table: "BankAccounts",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Institution",
                table: "BankAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "BankAccounts",
                nullable: false,
                defaultValue: "");
        }
    }
}
