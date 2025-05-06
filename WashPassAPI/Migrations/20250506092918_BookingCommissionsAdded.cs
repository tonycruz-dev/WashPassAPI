using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashPassAPI.Migrations
{
    /// <inheritdoc />
    public partial class BookingCommissionsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingCommissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CommissionPercent = table.Column<decimal>(type: "money", nullable: false),
                    CommissionAmount = table.Column<decimal>(type: "money", nullable: false),
                    PaidToAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCommissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingCommissions_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCommissions_BookingId",
                table: "BookingCommissions",
                column: "BookingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCommissions");
        }
    }
}
