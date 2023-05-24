using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal_Finance_Manager.Migrations
{
    /// <inheritdoc />
    public partial class DropFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "transactions_ibfk_1",
            table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
