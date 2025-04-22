using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToCustomerContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerContact",
                table: "Customers",
                column: "CustomerContact",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerContact",
                table: "Customers");
        }
    }
}
