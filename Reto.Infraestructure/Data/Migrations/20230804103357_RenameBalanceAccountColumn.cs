using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reto.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameBalanceAccountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Account",
                newName: "InitialBalance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialBalance",
                table: "Account",
                newName: "Balance");
        }
    }
}
