using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTM_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class Db_type_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Item_SpaceAccuired",
                table: "WarehouseItems_DbData",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Item_SpaceAccuired",
                table: "WarehouseItems_DbData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
