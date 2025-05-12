using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Migrations
{
    /// <inheritdoc />
    public partial class RenameTaskDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskComment",
                table: "InquiryTasks");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "InquiryTasks");

            migrationBuilder.AddColumn<string>(
                name: "TaskCommentTemp",
                table: "InquiryTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskDescriptionTemp",
                table: "InquiryTasks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskCommentTemp",
                table: "InquiryTasks");

            migrationBuilder.DropColumn(
                name: "TaskDescriptionTemp",
                table: "InquiryTasks");

            migrationBuilder.AddColumn<string>(
                name: "TaskComment",
                table: "InquiryTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "InquiryTasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
