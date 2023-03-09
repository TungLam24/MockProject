using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockProject.Migrations
{
    /// <inheritdoc />
    public partial class addadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LocalUser",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "UserName" },
                values: new object[] { 1, "nguyentunglam2410@gmail.com", "Nguyen Tung Lam", "lam55526", "Admin", "tunglam24" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocalUser",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
