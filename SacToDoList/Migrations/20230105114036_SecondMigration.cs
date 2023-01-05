using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SacToDoList.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Activity");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Activity",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Activity");

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "Activity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
