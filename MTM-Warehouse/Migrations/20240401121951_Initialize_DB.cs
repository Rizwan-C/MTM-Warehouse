using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTM_Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class Initialize_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobProgress_DbData",
                columns: table => new
                {
                    JobProgressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProgress_DbData", x => x.JobProgressId);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInfo_DbData",
                columns: table => new
                {
                    WarehouseInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    W_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    W_Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    W_PinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    W_TotalCapacity = table.Column<double>(type: "float", nullable: false),
                    W_SpaceAvailable = table.Column<double>(type: "float", nullable: true),
                    W_PercentFull = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInfo_DbData", x => x.WarehouseInfoId);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalJobs_DbData",
                columns: table => new
                {
                    ApprovalJobsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item_Space = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From_Warehouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To_Warehouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item_Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approval_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobProgressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalJobs_DbData", x => x.ApprovalJobsId);
                    table.ForeignKey(
                        name: "FK_ApprovalJobs_DbData_JobProgress_DbData_JobProgressId",
                        column: x => x.JobProgressId,
                        principalTable: "JobProgress_DbData",
                        principalColumn: "JobProgressId");
                });

            migrationBuilder.CreateTable(
                name: "EmpData_DbData",
                columns: table => new
                {
                    EmpDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<double>(type: "float", nullable: false),
                    Email = table.Column<double>(type: "float", nullable: false),
                    Phone = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<double>(type: "float", nullable: false),
                    PinCode = table.Column<double>(type: "float", nullable: false),
                    FirstDay = table.Column<double>(type: "float", nullable: false),
                    LastDay = table.Column<double>(type: "float", nullable: false),
                    WarehouseInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpData_DbData", x => x.EmpDataId);
                    table.ForeignKey(
                        name: "FK_EmpData_DbData_WarehouseInfo_DbData_WarehouseInfoId",
                        column: x => x.WarehouseInfoId,
                        principalTable: "WarehouseInfo_DbData",
                        principalColumn: "WarehouseInfoId");
                });

            migrationBuilder.CreateTable(
                name: "loginEmps_DbData",
                columns: table => new
                {
                    LoginEmpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessRights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loginEmps_DbData", x => x.LoginEmpId);
                    table.ForeignKey(
                        name: "FK_loginEmps_DbData_WarehouseInfo_DbData_WarehouseInfoId",
                        column: x => x.WarehouseInfoId,
                        principalTable: "WarehouseInfo_DbData",
                        principalColumn: "WarehouseInfoId");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseItems_DbData",
                columns: table => new
                {
                    WarehouseItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item_Unit_Quant = table.Column<double>(type: "float", nullable: false),
                    Item_Capacity_Quant = table.Column<double>(type: "float", nullable: false),
                    Item_SpaceAccuired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseItems_DbData", x => x.WarehouseItemsId);
                    table.ForeignKey(
                        name: "FK_WarehouseItems_DbData_WarehouseInfo_DbData_WarehouseInfoId",
                        column: x => x.WarehouseInfoId,
                        principalTable: "WarehouseInfo_DbData",
                        principalColumn: "WarehouseInfoId");
                });

            migrationBuilder.CreateTable(
                name: "Approvals_DbData",
                columns: table => new
                {
                    ApprovalsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approval_Job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginEmpId = table.Column<int>(type: "int", nullable: false),
                    ApprovalJobsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals_DbData", x => x.ApprovalsId);
                    table.ForeignKey(
                        name: "FK_Approvals_DbData_ApprovalJobs_DbData_ApprovalJobsId",
                        column: x => x.ApprovalJobsId,
                        principalTable: "ApprovalJobs_DbData",
                        principalColumn: "ApprovalJobsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Approvals_DbData_loginEmps_DbData_LoginEmpId",
                        column: x => x.LoginEmpId,
                        principalTable: "loginEmps_DbData",
                        principalColumn: "LoginEmpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalJobs_DbData_JobProgressId",
                table: "ApprovalJobs_DbData",
                column: "JobProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_DbData_ApprovalJobsId",
                table: "Approvals_DbData",
                column: "ApprovalJobsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_DbData_LoginEmpId",
                table: "Approvals_DbData",
                column: "LoginEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpData_DbData_WarehouseInfoId",
                table: "EmpData_DbData",
                column: "WarehouseInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_loginEmps_DbData_WarehouseInfoId",
                table: "loginEmps_DbData",
                column: "WarehouseInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItems_DbData_WarehouseInfoId",
                table: "WarehouseItems_DbData",
                column: "WarehouseInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approvals_DbData");

            migrationBuilder.DropTable(
                name: "EmpData_DbData");

            migrationBuilder.DropTable(
                name: "WarehouseItems_DbData");

            migrationBuilder.DropTable(
                name: "ApprovalJobs_DbData");

            migrationBuilder.DropTable(
                name: "loginEmps_DbData");

            migrationBuilder.DropTable(
                name: "JobProgress_DbData");

            migrationBuilder.DropTable(
                name: "WarehouseInfo_DbData");
        }
    }
}
