using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace futArabicom.Migrations
{
    /// <inheritdoc />
    public partial class addTimeAndSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Claims",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "claimDate",
                table: "Claims",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "timeStamp",
                table: "Claims",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "claimDate",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "timeStamp",
                table: "Claims");
        }
    }
}
