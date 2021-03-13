using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWishlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "RoleDescription" },
                values: new object[] { 1, new DateTime(2021, 3, 11, 7, 36, 51, 74, DateTimeKind.Utc).AddTicks(247), "Admin", new DateTime(2021, 3, 11, 7, 36, 51, 74, DateTimeKind.Utc).AddTicks(1235), "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "RoleDescription" },
                values: new object[] { 2, new DateTime(2021, 3, 11, 7, 36, 51, 74, DateTimeKind.Utc).AddTicks(2169), "Admin", new DateTime(2021, 3, 11, 7, 36, 51, 74, DateTimeKind.Utc).AddTicks(2173), "Admin", "User" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Address", "CreatedAt", "CreatedBy", "Email", "LastUpdatedAt", "LastUpdatedBy", "Name", "PasswordHash", "PasswordSalt", "RoleId" },
                values: new object[] { 1, "Dhaka", new DateTime(2021, 3, 11, 7, 36, 51, 81, DateTimeKind.Utc).AddTicks(3380), "Admin", "sudiptakumar.shuvo@gmail.com", new DateTime(2021, 3, 11, 7, 36, 51, 81, DateTimeKind.Utc).AddTicks(4338), "Admin", "Sudipta Kumar Bhowmick", new byte[] { 45, 220, 105, 184, 117, 191, 166, 101, 95, 222, 191, 111, 86, 196, 213, 246, 69, 209, 156, 200, 30, 132, 249, 104, 50, 64, 199, 196, 200, 118, 53, 8, 12, 181, 79, 226, 58, 213, 17, 8, 205, 210, 54, 166, 23, 178, 95, 77, 56, 22, 117, 22, 171, 164, 242, 130, 44, 194, 193, 123, 104, 58, 222, 11 }, new byte[] { 198, 21, 229, 211, 197, 27, 42, 204, 45, 185, 27, 84, 202, 96, 128, 152, 241, 251, 56, 44, 153, 25, 151, 22, 55, 14, 20, 45, 29, 179, 104, 123, 122, 225, 20, 145, 204, 181, 48, 66, 158, 206, 127, 181, 195, 142, 167, 14, 129, 204, 139, 18, 143, 127, 181, 251, 165, 205, 21, 220, 101, 200, 157, 222, 32, 29, 67, 250, 122, 68, 58, 78, 51, 243, 57, 144, 61, 62, 95, 196, 160, 54, 75, 221, 198, 77, 78, 37, 179, 161, 202, 175, 218, 111, 87, 142, 56, 113, 109, 128, 212, 249, 82, 223, 120, 73, 10, 203, 4, 86, 112, 119, 185, 243, 252, 205, 2, 245, 49, 151, 230, 156, 129, 3, 42, 114, 5, 1 }, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_RoleId",
                table: "Admins",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWishlists_UserId",
                table: "UserWishlists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "UserWishlists");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
