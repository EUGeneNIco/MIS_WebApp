using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Changes_Member_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Members",
                newName: "BirthDate");

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Members",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Members_CreatedById",
                table: "Members",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_CreatedById",
                table: "Members",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_CreatedById",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_CreatedById",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Members",
                newName: "Birthdate");
        }
    }
}
