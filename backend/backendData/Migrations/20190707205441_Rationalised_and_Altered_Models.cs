using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class Rationalised_and_Altered_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_BankAccounts_BankAccountId",
                table: "AccountValues");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_CashAccounts_CashAccountId",
                table: "AccountValues");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_LoanAccounts_LoanAccountId",
                table: "AccountValues");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_MortgageAccounts_MortgageAccountId",
                table: "AccountValues");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CashAccounts");

            migrationBuilder.DropTable(
                name: "LoanAccounts");

            migrationBuilder.DropTable(
                name: "MortgageAccounts");

            migrationBuilder.DropTable(
                name: "PropertyValues");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_AccountValues_BankAccountId",
                table: "AccountValues");

            migrationBuilder.DropIndex(
                name: "IX_AccountValues_CashAccountId",
                table: "AccountValues");

            migrationBuilder.DropIndex(
                name: "IX_AccountValues_LoanAccountId",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "CashAccountId",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "LoanAccountId",
                table: "AccountValues");

            migrationBuilder.RenameColumn(
                name: "MortgageAccountId",
                table: "AccountValues",
                newName: "LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountValues_MortgageAccountId",
                table: "AccountValues",
                newName: "IX_AccountValues_LoanId");

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "CreditCards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "AccountValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Institution = table.Column<string>(nullable: true),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AssetType = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_Assets_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetValues",
                columns: table => new
                {
                    AssetValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Source = table.Column<string>(nullable: true),
                    AssetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetValues", x => x.AssetValueId);
                    table.ForeignKey(
                        name: "FK_AssetValues_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoanName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    StartPrincipal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TermInMonths = table.Column<int>(nullable: false),
                    FixedTermInMonths = table.Column<int>(nullable: false),
                    RateType = table.Column<string>(nullable: true),
                    AprRate = table.Column<double>(nullable: false),
                    RepaymentFrequency = table.Column<string>(nullable: true),
                    RepaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    SecuredAssetAssetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loans_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Assets_SecuredAssetAssetId",
                        column: x => x.SecuredAssetAssetId,
                        principalTable: "Assets",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_AccountId",
                table: "AccountValues",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_QuotedCurrencyCode",
                table: "Accounts",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_QuotedCurrencyCode",
                table: "Assets",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UserId",
                table: "Assets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetValues_AssetId",
                table: "AssetValues",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_QuotedCurrencyCode",
                table: "Loans",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_SecuredAssetAssetId",
                table: "Loans",
                column: "SecuredAssetAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_Accounts_AccountId",
                table: "AccountValues",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_Loans_LoanId",
                table: "AccountValues",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "LoanId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_Accounts_AccountId",
                table: "AccountValues");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountValues_Loans_LoanId",
                table: "AccountValues");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AssetValues");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_AccountValues_AccountId",
                table: "AccountValues");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountValues");

            migrationBuilder.RenameColumn(
                name: "LoanId",
                table: "AccountValues",
                newName: "MortgageAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountValues_LoanId",
                table: "AccountValues",
                newName: "IX_AccountValues_MortgageAccountId");

            migrationBuilder.AddColumn<int>(
                name: "BankAccountId",
                table: "AccountValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CashAccountId",
                table: "AccountValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoanAccountId",
                table: "AccountValues",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashAccounts",
                columns: table => new
                {
                    CashAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashAccounts", x => x.CashAccountId);
                    table.ForeignKey(
                        name: "FK_CashAccounts_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanAccounts",
                columns: table => new
                {
                    LoanAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    AprRate = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TermInMonths = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAccounts", x => x.LoanAccountId);
                    table.ForeignKey(
                        name: "FK_LoanAccounts_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PropertyName = table.Column<string>(nullable: false),
                    PropertyType = table.Column<string>(nullable: true),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MortgageAccounts",
                columns: table => new
                {
                    MortgageAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    AprRate = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MortgagedPropertyPropertyId = table.Column<int>(nullable: true),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TermInMonths = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MortgageAccounts", x => x.MortgageAccountId);
                    table.ForeignKey(
                        name: "FK_MortgageAccounts_Properties_MortgagedPropertyPropertyId",
                        column: x => x.MortgagedPropertyPropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MortgageAccounts_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MortgageAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValues",
                columns: table => new
                {
                    PropertyValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    PropertyId = table.Column<int>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValues", x => x.PropertyValueId);
                    table.ForeignKey(
                        name: "FK_PropertyValues_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_BankAccountId",
                table: "AccountValues",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_CashAccountId",
                table: "AccountValues",
                column: "CashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_LoanAccountId",
                table: "AccountValues",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_QuotedCurrencyCode",
                table: "BankAccounts",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserId",
                table: "BankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CashAccounts_QuotedCurrencyCode",
                table: "CashAccounts",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_CashAccounts_UserId",
                table: "CashAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_QuotedCurrencyCode",
                table: "LoanAccounts",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_UserId",
                table: "LoanAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MortgageAccounts_MortgagedPropertyPropertyId",
                table: "MortgageAccounts",
                column: "MortgagedPropertyPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_MortgageAccounts_QuotedCurrencyCode",
                table: "MortgageAccounts",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_MortgageAccounts_UserId",
                table: "MortgageAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_QuotedCurrencyCode",
                table: "Properties",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValues_PropertyId",
                table: "PropertyValues",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_BankAccounts_BankAccountId",
                table: "AccountValues",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "BankAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_CashAccounts_CashAccountId",
                table: "AccountValues",
                column: "CashAccountId",
                principalTable: "CashAccounts",
                principalColumn: "CashAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_LoanAccounts_LoanAccountId",
                table: "AccountValues",
                column: "LoanAccountId",
                principalTable: "LoanAccounts",
                principalColumn: "LoanAccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountValues_MortgageAccounts_MortgageAccountId",
                table: "AccountValues",
                column: "MortgageAccountId",
                principalTable: "MortgageAccounts",
                principalColumn: "MortgageAccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
