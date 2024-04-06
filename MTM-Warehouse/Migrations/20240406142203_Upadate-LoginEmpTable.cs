using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTM_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class UpadateLoginEmpTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "loginEmps_DbData");

            migrationBuilder.RenameColumn(
                name: "AccessRights",
                table: "loginEmps_DbData",
                newName: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "loginEmps_DbData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "loginEmps_DbData",
                newName: "AccessRights");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "loginEmps_DbData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "loginEmps_DbData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
