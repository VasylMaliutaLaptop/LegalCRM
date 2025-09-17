using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalCRM.Data.Migrations
{
    /// <inheritdoc />
    public partial class addStatusForClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Clients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clients");
        }
    }
}
