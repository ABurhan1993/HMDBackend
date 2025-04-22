using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrmBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBuildingFieldsToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // استخدام كود SQL لتغيير النوع مع تحويل القيم تلقائيًا
            migrationBuilder.Sql(@"ALTER TABLE ""Buildings"" ALTER COLUMN ""BuildingTypeOfUnit"" TYPE integer USING ""BuildingTypeOfUnit""::integer;");
            migrationBuilder.Sql(@"ALTER TABLE ""Buildings"" ALTER COLUMN ""BuildingCondition"" TYPE integer USING ""BuildingCondition""::integer;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // في حال التراجع، نرجّع النوع إلى string (text)
            migrationBuilder.AlterColumn<string>(
                name: "BuildingTypeOfUnit",
                table: "Buildings",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "BuildingCondition",
                table: "Buildings",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
