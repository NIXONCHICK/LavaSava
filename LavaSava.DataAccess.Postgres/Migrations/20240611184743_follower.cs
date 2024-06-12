using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LavaSava.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class follower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FollowerId",
                table: "Followings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Users_FollowerId",
                table: "Followings");

            migrationBuilder.DropIndex(
                name: "IX_Followings_FollowerId",
                table: "Followings");

            migrationBuilder.DropColumn(
                name: "FollowerId",
                table: "Followings");
        }
    }
}
