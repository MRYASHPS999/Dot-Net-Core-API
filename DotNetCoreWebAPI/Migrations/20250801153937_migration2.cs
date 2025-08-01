using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDetailsUserId",
                table: "manager",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserDetailsUserId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_manager_UserDetailsUserId",
                table: "manager",
                column: "UserDetailsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_UserDetailsUserId",
                table: "employee",
                column: "UserDetailsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_users_UserDetailsUserId",
                table: "employee",
                column: "UserDetailsUserId",
                principalTable: "users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_manager_users_UserDetailsUserId",
                table: "manager",
                column: "UserDetailsUserId",
                principalTable: "users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_users_UserDetailsUserId",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_manager_users_UserDetailsUserId",
                table: "manager");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_manager_UserDetailsUserId",
                table: "manager");

            migrationBuilder.DropIndex(
                name: "IX_employee_UserDetailsUserId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "UserDetailsUserId",
                table: "manager");

            migrationBuilder.DropColumn(
                name: "UserDetailsUserId",
                table: "employee");
        }
    }
}
