using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace futArabicom.Migrations
{
    /// <inheritdoc />
    public partial class addPlayerAttr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defending",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Dribbling",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pace",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Passing",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Physical",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Shooting",
                table: "Players",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defending",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Dribbling",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Pace",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Passing",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Physical",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Shooting",
                table: "Players");
        }
    }
}
