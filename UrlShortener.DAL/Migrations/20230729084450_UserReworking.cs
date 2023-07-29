using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserReworking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlRecords_Users_UserId",
                table: "UrlRecords");

            migrationBuilder.DropIndex(
                name: "IX_UrlRecords_UserId",
                table: "UrlRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UrlRecords");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UrlRecordsCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UsersUrlRecords",
                columns: table => new
                {
                    UrlRecordsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersUrlRecords", x => new { x.UrlRecordsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UsersUrlRecords_UrlRecords_UrlRecordsId",
                        column: x => x.UrlRecordsId,
                        principalTable: "UrlRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersUrlRecords_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersUrlRecords_UsersId",
                table: "UsersUrlRecords",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersUrlRecords");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UrlRecordsCount",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UrlRecords",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrlRecords_UserId",
                table: "UrlRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlRecords_Users_UserId",
                table: "UrlRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
