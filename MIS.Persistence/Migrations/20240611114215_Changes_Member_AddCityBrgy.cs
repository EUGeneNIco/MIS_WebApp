using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Changes_Member_AddCityBrgy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barangay",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barangay",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Members");
        }
    }
}
