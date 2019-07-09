using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class AmendedrelationshiptoValuestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_CreditCards_CreditCardId",
                table: "AccountValues");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_Loans_LoanId",
                table: "AccountValues");

            migrationBuilder.DropIndex(
                name: "IX_AccountValues_CreditCardId",
                table: "AccountValues");

            migrationBuilder.DropIndex(
                name: "IX_AccountValues_LoanId",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "CreditCardId",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "AccountValues");

            migrationBuilder.CreateTable(
                name: "CreditCardValue",
                columns: table => new
                {
                    CreditCardValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardValue", x => x.CreditCardValueId);
                    table.ForeignKey(
                        name: "FK_CreditCardValue_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanValue",
                columns: table => new
                {
                    LoanValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanValue", x => x.LoanValueId);
                    table.ForeignKey(
                        name: "FK_LoanValue_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "LoanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardValue_CreditCardId",
                table: "CreditCardValue",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanValue_LoanId",
                table: "LoanValue",
                column: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCardValue");

            migrationBuilder.DropTable(
                name: "LoanValue");

            migrationBuilder.AddColumn<int>(
                name: "CreditCardId",
                table: "AccountValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "AccountValues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_CreditCardId",
                table: "AccountValues",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_LoanId",
                table: "AccountValues",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_CreditCards_CreditCardId",
                table: "AccountValues",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_Loans_LoanId",
                table: "AccountValues",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
