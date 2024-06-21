using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_MemberEventRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Users_CreatedById",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Users_ModifiedById",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ModifiedById",
                table: "Events",
                newName: "IX_Events_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Event_CreatedById",
                table: "Events",
                newName: "IX_Events_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GuestEventRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<long>(type: "bigint", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestEventRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestEventRecords_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestEventRecords_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestEventRecords_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GuestEventRecords_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MemberEventRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberEventRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberEventRecords_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberEventRecords_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberEventRecords_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MemberEventRecords_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestEventRecords_CreatedById",
                table: "GuestEventRecords",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GuestEventRecords_EventId",
                table: "GuestEventRecords",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestEventRecords_GuestId",
                table: "GuestEventRecords",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestEventRecords_ModifiedById",
                table: "GuestEventRecords",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MemberEventRecords_CreatedById",
                table: "MemberEventRecords",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MemberEventRecords_EventId",
                table: "MemberEventRecords",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberEventRecords_MemberId",
                table: "MemberEventRecords",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberEventRecords_ModifiedById",
                table: "MemberEventRecords",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_CreatedById",
                table: "Events",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_ModifiedById",
                table: "Events",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_CreatedById",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_ModifiedById",
                table: "Events");

            migrationBuilder.DropTable(
                name: "GuestEventRecords");

            migrationBuilder.DropTable(
                name: "MemberEventRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ModifiedById",
                table: "Event",
                newName: "IX_Event_ModifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CreatedById",
                table: "Event",
                newName: "IX_Event_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Users_CreatedById",
                table: "Event",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Users_ModifiedById",
                table: "Event",
                column: "ModifiedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
