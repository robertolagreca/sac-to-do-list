using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SacToDoList.Migrations
{
    /// <inheritdoc />
    public partial class TagsSystemMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTag_Activity_activitiesActivityId",
                table: "ActivityTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTag_Tag_TagsId",
                table: "ActivityTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTag",
                table: "ActivityTag");

            migrationBuilder.DropIndex(
                name: "IX_ActivityTag_activitiesActivityId",
                table: "ActivityTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameColumn(
                name: "activitiesActivityId",
                table: "ActivityTag",
                newName: "ActivitiesActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTag",
                table: "ActivityTag",
                columns: new[] { "ActivitiesActivityId", "TagsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTag_TagsId",
                table: "ActivityTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTag_Activity_ActivitiesActivityId",
                table: "ActivityTag",
                column: "ActivitiesActivityId",
                principalTable: "Activity",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTag_Tags_TagsId",
                table: "ActivityTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTag_Activity_ActivitiesActivityId",
                table: "ActivityTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityTag_Tags_TagsId",
                table: "ActivityTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityTag",
                table: "ActivityTag");

            migrationBuilder.DropIndex(
                name: "IX_ActivityTag_TagsId",
                table: "ActivityTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameColumn(
                name: "ActivitiesActivityId",
                table: "ActivityTag",
                newName: "activitiesActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityTag",
                table: "ActivityTag",
                columns: new[] { "TagsId", "activitiesActivityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTag_activitiesActivityId",
                table: "ActivityTag",
                column: "activitiesActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTag_Activity_activitiesActivityId",
                table: "ActivityTag",
                column: "activitiesActivityId",
                principalTable: "Activity",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityTag_Tag_TagsId",
                table: "ActivityTag",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
