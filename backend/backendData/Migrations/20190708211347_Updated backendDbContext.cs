using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class UpdatedbackendDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCardValue_CreditCards_CreditCardId",
                table: "CreditCardValue");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanValue_Loans_LoanId",
                table: "LoanValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanValue",
                table: "LoanValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditCardValue",
                table: "CreditCardValue");

            migrationBuilder.RenameTable(
                name: "LoanValue",
                newName: "LoanValues");

            migrationBuilder.RenameTable(
                name: "CreditCardValue",
                newName: "CreditCardValues");

            migrationBuilder.RenameIndex(
                name: "IX_LoanValue_LoanId",
                table: "LoanValues",
                newName: "IX_LoanValues_LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCardValue_CreditCardId",
                table: "CreditCardValues",
                newName: "IX_CreditCardValues_CreditCardId");

            migrationBuilder.AlterColumn<int>(
                name: "LoanId",
                table: "LoanValues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "CreditCardValues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanValues",
                table: "LoanValues",
                column: "LoanValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditCardValues",
                table: "CreditCardValues",
                column: "CreditCardValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCardValues_CreditCards_CreditCardId",
                table: "CreditCardValues",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanValues_Loans_LoanId",
                table: "LoanValues",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditCardValues_CreditCards_CreditCardId",
                table: "CreditCardValues");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanValues_Loans_LoanId",
                table: "LoanValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanValues",
                table: "LoanValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CreditCardValues",
                table: "CreditCardValues");

            migrationBuilder.RenameTable(
                name: "LoanValues",
                newName: "LoanValue");

            migrationBuilder.RenameTable(
                name: "CreditCardValues",
                newName: "CreditCardValue");

            migrationBuilder.RenameIndex(
                name: "IX_LoanValues_LoanId",
                table: "LoanValue",
                newName: "IX_LoanValue_LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_CreditCardValues_CreditCardId",
                table: "CreditCardValue",
                newName: "IX_CreditCardValue_CreditCardId");

            migrationBuilder.AlterColumn<int>(
                name: "LoanId",
                table: "LoanValue",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "CreditCardValue",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanValue",
                table: "LoanValue",
                column: "LoanValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreditCardValue",
                table: "CreditCardValue",
                column: "CreditCardValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCardValue_CreditCards_CreditCardId",
                table: "CreditCardValue",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanValue_Loans_LoanId",
                table: "LoanValue",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
