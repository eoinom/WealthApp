using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class InitialCreateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Iso2Code = table.Column<string>(nullable: false),
                    Iso3Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Continent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Iso2Code);
                });

            migrationBuilder.CreateTable(
                name: "Cryptocurrencies",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    NameShort = table.Column<string>(nullable: true),
                    NameLong = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    NewsletterSub = table.Column<bool>(nullable: false),
                    DisplayCurrencyCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Currencies_DisplayCurrencyCode",
                        column: x => x.DisplayCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Institution = table.Column<string>(nullable: false),
                    AccountType = table.Column<string>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
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
                    Type = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
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
                name: "CreditCards",
                columns: table => new
                {
                    CreditCardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditCardName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CreditCardId);
                    table.ForeignKey(
                        name: "FK_CreditCards_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CryptoAccounts",
                columns: table => new
                {
                    CryptoAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAccounts", x => x.CryptoAccountId);
                    table.ForeignKey(
                        name: "FK_CryptoAccounts_Currencies_QuotedCurrencyCode",
                        column: x => x.QuotedCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CryptoAccounts_Users_UserId",
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
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TermInMonths = table.Column<int>(nullable: false),
                    AprRate = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
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
                    PropertyName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
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
                name: "CryptoAccountValues",
                columns: table => new
                {
                    CryptoAccountValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CryptoAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAccountValues", x => x.CryptoAccountValueId);
                    table.ForeignKey(
                        name: "FK_CryptoAccountValues_CryptoAccounts_CryptoAccountId",
                        column: x => x.CryptoAccountId,
                        principalTable: "CryptoAccounts",
                        principalColumn: "CryptoAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MortgageAccounts",
                columns: table => new
                {
                    MortgageAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TermInMonths = table.Column<int>(nullable: false),
                    AprRate = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    QuotedCurrencyCode = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MortgagedPropertyPropertyId = table.Column<int>(nullable: true)
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
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Source = table.Column<string>(nullable: true),
                    PropertyId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "CryptoValues",
                columns: table => new
                {
                    CryptoValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    CryptocurrencyCode = table.Column<string>(nullable: true),
                    QuotedCurrency = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    NoCoins = table.Column<double>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CryptoAccountValueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoValues", x => x.CryptoValueId);
                    table.ForeignKey(
                        name: "FK_CryptoValues_CryptoAccountValues_CryptoAccountValueId",
                        column: x => x.CryptoAccountValueId,
                        principalTable: "CryptoAccountValues",
                        principalColumn: "CryptoAccountValueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CryptoValues_Cryptocurrencies_CryptocurrencyCode",
                        column: x => x.CryptocurrencyCode,
                        principalTable: "Cryptocurrencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountValues",
                columns: table => new
                {
                    AccountValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BankAccountId = table.Column<int>(nullable: true),
                    CashAccountId = table.Column<int>(nullable: true),
                    LoanAccountId = table.Column<int>(nullable: true),
                    MortgageAccountId = table.Column<int>(nullable: true),
                    CreditCardId = table.Column<int>(nullable: true),
                    CryptoAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountValues", x => x.AccountValueId);
                    table.ForeignKey(
                        name: "FK_AccountValues_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountValues_CashAccounts_CashAccountId",
                        column: x => x.CashAccountId,
                        principalTable: "CashAccounts",
                        principalColumn: "CashAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountValues_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountValues_CryptoAccounts_CryptoAccountId",
                        column: x => x.CryptoAccountId,
                        principalTable: "CryptoAccounts",
                        principalColumn: "CryptoAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountValues_LoanAccounts_LoanAccountId",
                        column: x => x.LoanAccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "LoanAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountValues_MortgageAccounts_MortgageAccountId",
                        column: x => x.MortgageAccountId,
                        principalTable: "MortgageAccounts",
                        principalColumn: "MortgageAccountId",
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
                name: "IX_AccountValues_CreditCardId",
                table: "AccountValues",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_CryptoAccountId",
                table: "AccountValues",
                column: "CryptoAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_LoanAccountId",
                table: "AccountValues",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountValues_MortgageAccountId",
                table: "AccountValues",
                column: "MortgageAccountId");

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
                name: "IX_CreditCards_QuotedCurrencyCode",
                table: "CreditCards",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_UserId",
                table: "CreditCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAccounts_QuotedCurrencyCode",
                table: "CryptoAccounts",
                column: "QuotedCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAccounts_UserId",
                table: "CryptoAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAccountValues_CryptoAccountId",
                table: "CryptoAccountValues",
                column: "CryptoAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoValues_CryptoAccountValueId",
                table: "CryptoValues",
                column: "CryptoAccountValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoValues_CryptocurrencyCode",
                table: "CryptoValues",
                column: "CryptocurrencyCode");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_DisplayCurrencyCode",
                table: "Users",
                column: "DisplayCurrencyCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountValues");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "CryptoValues");

            migrationBuilder.DropTable(
                name: "PropertyValues");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CashAccounts");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "LoanAccounts");

            migrationBuilder.DropTable(
                name: "MortgageAccounts");

            migrationBuilder.DropTable(
                name: "CryptoAccountValues");

            migrationBuilder.DropTable(
                name: "Cryptocurrencies");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "CryptoAccounts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
