using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CrmBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddAllWorkscopeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuildingAddress = table.Column<string>(type: "text", nullable: false),
                    BuildingTypeOfUnit = table.Column<string>(type: "text", nullable: false),
                    BuildingCondition = table.Column<string>(type: "text", nullable: false),
                    BuildingFloor = table.Column<string>(type: "text", nullable: false),
                    BuildingReconstruction = table.Column<bool>(type: "boolean", nullable: true),
                    IsOccupied = table.Column<bool>(type: "boolean", nullable: true),
                    BuildingMakaniMap = table.Column<string>(type: "text", nullable: false),
                    BuildingLatitude = table.Column<string>(type: "text", nullable: false),
                    BuildingLongitude = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                });

            migrationBuilder.CreateTable(
                name: "WorkScopes",
                columns: table => new
                {
                    WorkScopeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkScopeName = table.Column<string>(type: "text", nullable: false),
                    WorkScopeDescription = table.Column<string>(type: "text", nullable: false),
                    QuestionaireType = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkScopes", x => x.WorkScopeId);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                columns: table => new
                {
                    InquiryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryCode = table.Column<string>(type: "text", nullable: false),
                    InquiryName = table.Column<string>(type: "text", nullable: false),
                    InquiryDescription = table.Column<string>(type: "text", nullable: false),
                    InquiryStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InquiryDueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InquiryEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsMeasurementProvidedByCustomer = table.Column<bool>(type: "boolean", nullable: true),
                    IsDesignProvidedByCustomer = table.Column<bool>(type: "boolean", nullable: true),
                    MeasurementFees = table.Column<string>(type: "text", nullable: false),
                    QuotationAssignTo = table.Column<Guid>(type: "uuid", nullable: true),
                    QuotationScheduleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    QuotationAddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    BuildingId = table.Column<int>(type: "integer", nullable: true),
                    IsEscalationRequested = table.Column<bool>(type: "boolean", nullable: true),
                    EscalationRequestedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EscalationRequestedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    ManagedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    TempContractAssignedTo = table.Column<int>(type: "integer", nullable: true),
                    IsQuotationReschedule = table.Column<bool>(type: "boolean", nullable: true),
                    IsInquiryLocked = table.Column<bool>(type: "boolean", nullable: true),
                    IsExistingInquiry = table.Column<bool>(type: "boolean", nullable: true),
                    ExistingInquiryId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.InquiryId);
                    table.ForeignKey(
                        name: "FK_Inquiries_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId");
                    table.ForeignKey(
                        name: "FK_Inquiries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Inquiries_Inquiries_ExistingInquiryId",
                        column: x => x.ExistingInquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "InquiryId");
                    table.ForeignKey(
                        name: "FK_Inquiries_Users_EscalationRequestedBy",
                        column: x => x.EscalationRequestedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_Users_ManagedBy",
                        column: x => x.ManagedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inquiries_Users_QuotationAssignTo",
                        column: x => x.QuotationAssignTo,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InquiryWorkscopes",
                columns: table => new
                {
                    InquiryWorkscopeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryId = table.Column<int>(type: "integer", nullable: false),
                    WorkScopeId = table.Column<int>(type: "integer", nullable: false),
                    InquiryWorkscopeDetailName = table.Column<string>(type: "text", nullable: false),
                    IsMeasurementDrawing = table.Column<bool>(type: "boolean", nullable: true),
                    MeasurementAssignedTo = table.Column<Guid>(type: "uuid", nullable: true),
                    DesignAssignedTo = table.Column<Guid>(type: "uuid", nullable: true),
                    InquiryStatus = table.Column<int>(type: "integer", nullable: true),
                    MeasurementScheduleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MeasurementAddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DesignScheduleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DesignAddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDesignApproved = table.Column<bool>(type: "boolean", nullable: true),
                    IsDesignSentToCustomer = table.Column<bool>(type: "boolean", nullable: true),
                    FeedbackReaction = table.Column<int>(type: "integer", nullable: true),
                    IsMeasurementReschedule = table.Column<bool>(type: "boolean", nullable: true),
                    IsDesignReschedule = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryWorkscopes", x => x.InquiryWorkscopeId);
                    table.ForeignKey(
                        name: "FK_InquiryWorkscopes_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "InquiryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryWorkscopes_Users_DesignAssignedTo",
                        column: x => x.DesignAssignedTo,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryWorkscopes_Users_MeasurementAssignedTo",
                        column: x => x.MeasurementAssignedTo,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InquiryWorkscopes_WorkScopes_WorkScopeId",
                        column: x => x.WorkScopeId,
                        principalTable: "WorkScopes",
                        principalColumn: "WorkScopeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommentName = table.Column<string>(type: "text", nullable: false),
                    CommentDetail = table.Column<string>(type: "text", nullable: false),
                    InquiryId = table.Column<int>(type: "integer", nullable: true),
                    InquiryWorkscopeId = table.Column<int>(type: "integer", nullable: true),
                    InquiryStatus = table.Column<int>(type: "integer", nullable: true),
                    CommentAddedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CommentAddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CommentNextFollowup = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsFollowedUpRequired = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "InquiryId");
                    table.ForeignKey(
                        name: "FK_Comments_InquiryWorkscopes_InquiryWorkscopeId",
                        column: x => x.InquiryWorkscopeId,
                        principalTable: "InquiryWorkscopes",
                        principalColumn: "InquiryWorkscopeId");
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentAddedBy",
                        column: x => x.CommentAddedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkscopeQuotationDetails",
                columns: table => new
                {
                    WorkscopeQuotationDetailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryWorkscopeId = table.Column<int>(type: "integer", nullable: false),
                    QuotationAddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    QuotationAddedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    QuotationUpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    QuotationUpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkscopeQuotationPic = table.Column<string>(type: "text", nullable: false),
                    IsUrlGenerated = table.Column<bool>(type: "boolean", nullable: true),
                    UrlGeneratedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WoodenWorkAmount = table.Column<string>(type: "text", nullable: false),
                    CounterTopAmount = table.Column<string>(type: "text", nullable: false),
                    AppliancesAmount = table.Column<string>(type: "text", nullable: false),
                    LightningAmount = table.Column<string>(type: "text", nullable: false),
                    AccessoriesAmount = table.Column<string>(type: "text", nullable: false),
                    SpecialItemAmount = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkscopeQuotationDetails", x => x.WorkscopeQuotationDetailId);
                    table.ForeignKey(
                        name: "FK_WorkscopeQuotationDetails_InquiryWorkscopes_InquiryWorkscop~",
                        column: x => x.InquiryWorkscopeId,
                        principalTable: "InquiryWorkscopes",
                        principalColumn: "InquiryWorkscopeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentAddedBy",
                table: "Comments",
                column: "CommentAddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_InquiryId",
                table: "Comments",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_InquiryWorkscopeId",
                table: "Comments",
                column: "InquiryWorkscopeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_BranchId",
                table: "Inquiries",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_BuildingId",
                table: "Inquiries",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CustomerId",
                table: "Inquiries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_EscalationRequestedBy",
                table: "Inquiries",
                column: "EscalationRequestedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_ExistingInquiryId",
                table: "Inquiries",
                column: "ExistingInquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_ManagedBy",
                table: "Inquiries",
                column: "ManagedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_QuotationAssignTo",
                table: "Inquiries",
                column: "QuotationAssignTo");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryWorkscopes_DesignAssignedTo",
                table: "InquiryWorkscopes",
                column: "DesignAssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryWorkscopes_InquiryId",
                table: "InquiryWorkscopes",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryWorkscopes_MeasurementAssignedTo",
                table: "InquiryWorkscopes",
                column: "MeasurementAssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryWorkscopes_WorkScopeId",
                table: "InquiryWorkscopes",
                column: "WorkScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkscopeQuotationDetails_InquiryWorkscopeId",
                table: "WorkscopeQuotationDetails",
                column: "InquiryWorkscopeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "WorkscopeQuotationDetails");

            migrationBuilder.DropTable(
                name: "InquiryWorkscopes");

            migrationBuilder.DropTable(
                name: "Inquiries");

            migrationBuilder.DropTable(
                name: "WorkScopes");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
