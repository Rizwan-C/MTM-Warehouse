using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTM_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class ItemTable_price_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Item_price_per_unit",
                table: "WarehouseItems_DbData",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Item_total_cost",
                table: "WarehouseItems_DbData",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item_price_per_unit",
                table: "WarehouseItems_DbData");

            migrationBuilder.DropColumn(
                name: "Item_total_cost",
                table: "WarehouseItems_DbData");
        }
    }
}
