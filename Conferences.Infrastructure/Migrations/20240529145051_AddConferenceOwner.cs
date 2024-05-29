using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conferences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConferenceOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Conferences",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("update Conferences set OwnerId = (select top 1 Id from AspNetUsers)");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_OwnerId",
                table: "Conferences",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conferences_AspNetUsers_OwnerId",
                table: "Conferences",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conferences_AspNetUsers_OwnerId",
                table: "Conferences");

            migrationBuilder.DropIndex(
                name: "IX_Conferences_OwnerId",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Conferences");
        }
    }
}
