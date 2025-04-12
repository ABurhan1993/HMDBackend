using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CrmBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "text", nullable: false),
                    CustomerEmail = table.Column<string>(type: "text", nullable: true),
                    CustomerContact = table.Column<string>(type: "text", nullable: true),
                    CustomerWhatsapp = table.Column<string>(type: "text", nullable: true),
                    CustomerAddress = table.Column<string>(type: "text", nullable: true),
                    CustomerCity = table.Column<string>(type: "text", nullable: true),
                    CustomerCountry = table.Column<string>(type: "text", nullable: true),
                    CustomerNationality = table.Column<string>(type: "text", nullable: true),
                    CustomerNationalId = table.Column<string>(type: "text", nullable: true),
                    CustomerNotes = table.Column<string>(type: "text", nullable: true),
                    CustomerNextMeetingDate = table.Column<string>(type: "text", nullable: true),
                    ContactStatus = table.Column<int>(type: "integer", nullable: false),
                    IsVisitedShowroom = table.Column<bool>(type: "boolean", nullable: true),
                    CustomerTimeSpent = table.Column<int>(type: "integer", nullable: true),
                    WayOfContact = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomerAssignedTo = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomerAssignedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomerAssignedDate = table.Column<string>(type: "text", nullable: true),
                    IsEscalationRequested = table.Column<bool>(type: "boolean", nullable: true),
                    EscalationRequestedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    EscalationRequestedOn = table.Column<string>(type: "text", nullable: true),
                    EscalatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    EscalatedOn = table.Column<string>(type: "text", nullable: true),
                    IsReturnedNotRespondingCustomer = table.Column<bool>(type: "boolean", nullable: true),
                    IsReturnedNotRespondingCustomerDate = table.Column<string>(type: "text", nullable: true),
                    IsReturnedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CustomerAssignedBy",
                        column: x => x.CustomerAssignedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_CustomerAssignedTo",
                        column: x => x.CustomerAssignedTo,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_EscalatedBy",
                        column: x => x.EscalatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_EscalationRequestedBy",
                        column: x => x.EscalationRequestedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_IsReturnedBy",
                        column: x => x.IsReturnedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerBranch",
                columns: table => new
                {
                    CustomerBranchId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBranch", x => x.CustomerBranchId);
                    table.ForeignKey(
                        name: "FK_CustomerBranch_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerBranch_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerComment",
                columns: table => new
                {
                    CustomerCommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerCommentDetail = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CommentAddedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentAddedOn = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedDate = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActve = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerComment", x => x.CustomerCommentId);
                    table.ForeignKey(
                        name: "FK_CustomerComment_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerComment_Users_CommentAddedBy",
                        column: x => x.CommentAddedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBranch_BranchId",
                table: "CustomerBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBranch_CustomerId",
                table: "CustomerBranch",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerComment_CommentAddedBy",
                table: "CustomerComment",
                column: "CommentAddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerComment_CustomerId",
                table: "CustomerComment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BranchId",
                table: "Customers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy",
                table: "Customers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerAssignedBy",
                table: "Customers",
                column: "CustomerAssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerAssignedTo",
                table: "Customers",
                column: "CustomerAssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EscalatedBy",
                table: "Customers",
                column: "EscalatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EscalationRequestedBy",
                table: "Customers",
                column: "EscalationRequestedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IsReturnedBy",
                table: "Customers",
                column: "IsReturnedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UpdatedBy",
                table: "Customers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                table: "Users",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerBranch");

            migrationBuilder.DropTable(
                name: "CustomerComment");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
