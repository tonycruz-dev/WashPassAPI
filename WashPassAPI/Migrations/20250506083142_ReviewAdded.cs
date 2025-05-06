using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashPassAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingService_Bookings_BookingId",
                table: "BookingService");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingService_Services_ServiceId",
                table: "BookingService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingService",
                table: "BookingService");

            migrationBuilder.RenameTable(
                name: "BookingService",
                newName: "BookingServices");

            migrationBuilder.RenameIndex(
                name: "IX_BookingService_ServiceId",
                table: "BookingServices",
                newName: "IX_BookingServices_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingServices",
                table: "BookingServices",
                columns: new[] { "BookingId", "ServiceId" });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Bookings_BookingId",
                table: "BookingServices",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Services_ServiceId",
                table: "BookingServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Bookings_BookingId",
                table: "BookingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Services_ServiceId",
                table: "BookingServices");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingServices",
                table: "BookingServices");

            migrationBuilder.RenameTable(
                name: "BookingServices",
                newName: "BookingService");

            migrationBuilder.RenameIndex(
                name: "IX_BookingServices_ServiceId",
                table: "BookingService",
                newName: "IX_BookingService_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingService",
                table: "BookingService",
                columns: new[] { "BookingId", "ServiceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingService_Bookings_BookingId",
                table: "BookingService",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingService_Services_ServiceId",
                table: "BookingService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
