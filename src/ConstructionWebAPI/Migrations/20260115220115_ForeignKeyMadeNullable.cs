using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyMadeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Buildings_BuildingId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Users_OwnerId",
                table: "Buildings");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Buildings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Buildings_BuildingId",
                table: "Assignments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Users_OwnerId",
                table: "Buildings",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Buildings_BuildingId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Users_OwnerId",
                table: "Buildings");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Buildings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Buildings_BuildingId",
                table: "Assignments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Users_OwnerId",
                table: "Buildings",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
