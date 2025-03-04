using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitedHealth.Common.Migrations
{
    /// <inheritdoc />
    public partial class Specialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Users");
        }
    }
}
