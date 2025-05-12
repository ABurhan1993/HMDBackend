using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Migrations
{
    /// <inheritdoc />
    public partial class RenameTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskDescriptionTemp",
                table: "InquiryTasks",
                newName: "TaskDescription");

            migrationBuilder.RenameColumn(
                name: "TaskCommentTemp",
                table: "InquiryTasks",
                newName: "TaskComment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "InquiryTasks",
                newName: "TaskDescriptionTemp");

            migrationBuilder.RenameColumn(
                name: "TaskComment",
                table: "InquiryTasks",
                newName: "TaskCommentTemp");
        }
    }
}
