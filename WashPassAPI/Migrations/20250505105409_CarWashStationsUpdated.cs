using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashPassAPI.Migrations
{
    /// <inheritdoc />
    public partial class CarWashStationsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminId",
                table: "CarWashStations");

            migrationBuilder.DropForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminUserId",
                table: "CarWashStations");

            migrationBuilder.DropIndex(
                name: "IX_CarWashStations_AdminId",
                table: "CarWashStations");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "CarWashStations");

            migrationBuilder.AlterColumn<int>(
                name: "AdminUserId",
                table: "CarWashStations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminUserId1",
                table: "CarWashStations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarWashStations_AdminUserId1",
                table: "CarWashStations",
                column: "AdminUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminUserId",
                table: "CarWashStations",
                column: "AdminUserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminUserId1",
                table: "CarWashStations",
                column: "AdminUserId1",
                principalTable: "AdminUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminUserId",
                table: "CarWashStations");

            migrationBuilder.DropForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminUserId1",
                table: "CarWashStations");

            migrationBuilder.DropIndex(
                name: "IX_CarWashStations_AdminUserId1",
                table: "CarWashStations");

            migrationBuilder.DropColumn(
                name: "AdminUserId1",
                table: "CarWashStations");

            migrationBuilder.AlterColumn<int>(
                name: "AdminUserId",
                table: "CarWashStations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "CarWashStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarWashStations_AdminId",
                table: "CarWashStations",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminId",
                table: "CarWashStations",
                column: "AdminId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarWashStations_AdminUsers_AdminUserId",
                table: "CarWashStations",
                column: "AdminUserId",
                principalTable: "AdminUsers",
                principalColumn: "Id");
        }
    }
}
