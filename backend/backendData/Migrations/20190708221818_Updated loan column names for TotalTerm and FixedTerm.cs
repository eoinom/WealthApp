using Microsoft.EntityFrameworkCore.Migrations;

namespace backendData.Migrations
{
    public partial class UpdatedloancolumnnamesforTotalTermandFixedTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TermInMonths",
                table: "Loans",
                newName: "TotalTerm");

            migrationBuilder.RenameColumn(
                name: "FixedTermInMonths",
                table: "Loans",
                newName: "FixedTerm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalTerm",
                table: "Loans",
                newName: "TermInMonths");

            migrationBuilder.RenameColumn(
                name: "FixedTerm",
                table: "Loans",
                newName: "FixedTermInMonths");
        }
    }
}
