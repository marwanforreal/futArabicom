using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace futArabicom.Migrations
{
    /// <inheritdoc />
    public partial class addCommentsToPlayer1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PlayerId",
                table: "Comments",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Players_PlayerId",
                table: "Comments",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Players_PlayerId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PlayerId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Comments");
        }
    }
}
