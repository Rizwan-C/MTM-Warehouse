using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTM_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class DatatypeChangeINEmpData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstDay",
                table: "EmpData_DbData");

            migrationBuilder.DropColumn(
                name: "LastDay",
                table: "EmpData_DbData");

            migrationBuilder.AlterColumn<string>(
                name: "PinCode",
                table: "EmpData_DbData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "EmpData_DbData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmpData_DbData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "EmpData_DbData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "EmpData_DbData",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PinCode",
                table: "EmpData_DbData",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Phone",
                table: "EmpData_DbData",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Name",
                table: "EmpData_DbData",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Email",
                table: "EmpData_DbData",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "Address",
                table: "EmpData_DbData",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "FirstDay",
                table: "EmpData_DbData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LastDay",
                table: "EmpData_DbData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
