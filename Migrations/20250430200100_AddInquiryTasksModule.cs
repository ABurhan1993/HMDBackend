using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CrmBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddInquiryTasksModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InquiryTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryId = table.Column<int>(type: "integer", nullable: false),
                    InquiryWorkscopeId = table.Column<int>(type: "integer", nullable: true),
                    TaskType = table.Column<int>(type: "integer", nullable: false),
                    TaskName = table.Column<string>(type: "text", nullable: false),
                    TaskDescription = table.Column<string>(type: "text", nullable: false),
                    TaskComment = table.Column<string>(type: "text", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AssignedToUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsRescheduled = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InquiryTasks_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "InquiryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryTasks_InquiryWorkscopes_InquiryWorkscopeId",
                        column: x => x.InquiryWorkscopeId,
                        principalTable: "InquiryWorkscopes",
                        principalColumn: "InquiryWorkscopeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InquiryTasks_Users_ApprovedByUserId",
                        column: x => x.ApprovedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InquiryTasks_Users_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Designs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryTaskId = table.Column<int>(type: "integer", nullable: false),
                    DesignCustomerReviewDate = table.Column<string>(type: "text", nullable: false),
                    TotalHoursConsumed = table.Column<string>(type: "text", nullable: false),
                    IsTimerStarted = table.Column<bool>(type: "boolean", nullable: true),
                    TimerStartedOn = table.Column<string>(type: "text", nullable: false),
                    TimerEndsOn = table.Column<string>(type: "text", nullable: false),
                    SoftwareUsed = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designs_InquiryTasks_InquiryTaskId",
                        column: x => x.InquiryTaskId,
                        principalTable: "InquiryTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryTaskId = table.Column<int>(type: "integer", nullable: false),
                    IsMeasurementApproved = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_InquiryTasks_InquiryTaskId",
                        column: x => x.InquiryTaskId,
                        principalTable: "InquiryTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryTaskId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskFiles_InquiryTasks_InquiryTaskId",
                        column: x => x.InquiryTaskId,
                        principalTable: "InquiryTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designs_InquiryTaskId",
                table: "Designs",
                column: "InquiryTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InquiryTasks_ApprovedByUserId",
                table: "InquiryTasks",
                column: "ApprovedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryTasks_AssignedToUserId",
                table: "InquiryTasks",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryTasks_InquiryId",
                table: "InquiryTasks",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryTasks_InquiryWorkscopeId",
                table: "InquiryTasks",
                column: "InquiryWorkscopeId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_InquiryTaskId",
                table: "Measurements",
                column: "InquiryTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskFiles_InquiryTaskId",
                table: "TaskFiles",
                column: "InquiryTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Designs");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "TaskFiles");

            migrationBuilder.DropTable(
                name: "InquiryTasks");
        }
    }
}
