using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTM_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class mig02CorrectedTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalJobs_DbData_JobProgress_DbData_JobProgressid",
                table: "ApprovalJobs_DbData");

            migrationBuilder.RenameColumn(
                name: "JobProgressid",
                table: "JobProgress_DbData",
                newName: "JobProgressId");

            migrationBuilder.RenameColumn(
                name: "JobProgressid",
                table: "ApprovalJobs_DbData",
                newName: "JobProgressId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalJobs_DbData_JobProgressid",
                table: "ApprovalJobs_DbData",
                newName: "IX_ApprovalJobs_DbData_JobProgressId");

            migrationBuilder.AlterColumn<int>(
                name: "JobProgressId",
                table: "ApprovalJobs_DbData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalJobs_DbData_JobProgress_DbData_JobProgressId",
                table: "ApprovalJobs_DbData",
                column: "JobProgressId",
                principalTable: "JobProgress_DbData",
                principalColumn: "JobProgressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalJobs_DbData_JobProgress_DbData_JobProgressId",
                table: "ApprovalJobs_DbData");

            migrationBuilder.RenameColumn(
                name: "JobProgressId",
                table: "JobProgress_DbData",
                newName: "JobProgressid");

            migrationBuilder.RenameColumn(
                name: "JobProgressId",
                table: "ApprovalJobs_DbData",
                newName: "JobProgressid");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalJobs_DbData_JobProgressId",
                table: "ApprovalJobs_DbData",
                newName: "IX_ApprovalJobs_DbData_JobProgressid");

            migrationBuilder.AlterColumn<int>(
                name: "JobProgressid",
                table: "ApprovalJobs_DbData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalJobs_DbData_JobProgress_DbData_JobProgressid",
                table: "ApprovalJobs_DbData",
                column: "JobProgressid",
                principalTable: "JobProgress_DbData",
                principalColumn: "JobProgressid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
