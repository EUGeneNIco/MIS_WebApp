using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_MemberId_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Members",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Members",
                newName: "MemberId");
        }
    }
}
