using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Housing.API.Migrations
{
    /// <inheritdoc />
    public partial class dataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToltalFloors",
                table: "Properties",
                newName: "TotalFloors");

            migrationBuilder.RenameColumn(
                name: "Maintanance",
                table: "Properties",
                newName: "Maintenance");

            migrationBuilder.RenameColumn(
                name: "MainEnterance",
                table: "Properties",
                newName: "MainEntrance");

            migrationBuilder.AddColumn<string>(
                name: "PasswordKey",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordKey",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TotalFloors",
                table: "Properties",
                newName: "ToltalFloors");

            migrationBuilder.RenameColumn(
                name: "Maintenance",
                table: "Properties",
                newName: "Maintanance");

            migrationBuilder.RenameColumn(
                name: "MainEntrance",
                table: "Properties",
                newName: "MainEnterance");
        }
    }
}
