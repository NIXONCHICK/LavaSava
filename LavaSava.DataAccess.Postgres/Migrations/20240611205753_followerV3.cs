using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LavaSava.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class followerV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Users_FollowerId",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Users_UserId",
                table: "Followings");

            migrationBuilder.DropIndex(
                name: "IX_Followings_FollowerId",
                table: "Followings");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "Followings",
                newName: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Users_UserId",
                table: "Followings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Users_UserId",
                table: "Followings");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "Followings",
                newName: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Followings_FollowerId",
                table: "Followings",
                column: "FollowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Users_FollowerId",
                table: "Followings",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Users_UserId",
                table: "Followings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
