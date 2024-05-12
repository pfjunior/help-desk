using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HD.Users.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department_Code",
                table: "Users",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Department_Name",
                table: "Users",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department_Code",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Department_Name",
                table: "Users");
        }
    }
}
