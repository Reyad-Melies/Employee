using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationCasuals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<int>(nullable: false),
                    Balance = table.Column<int>(nullable: false),
                    Used = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationCasuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationCasuals_Emp_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Emp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacationSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<int>(nullable: false),
                    Balance = table.Column<int>(nullable: false),
                    Used = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationSchedules_Emp_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Emp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "All",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmppId = table.Column<int>(nullable: true),
                    VacationCasualId = table.Column<int>(nullable: true),
                    VacationScheduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_All", x => x.Id);
                    table.ForeignKey(
                        name: "FK_All_Emp_EmppId",
                        column: x => x.EmppId,
                        principalTable: "Emp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_All_VacationCasuals_VacationCasualId",
                        column: x => x.VacationCasualId,
                        principalTable: "VacationCasuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_All_VacationSchedules_VacationScheduleId",
                        column: x => x.VacationScheduleId,
                        principalTable: "VacationSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_All_EmppId",
                table: "All",
                column: "EmppId");

            migrationBuilder.CreateIndex(
                name: "IX_All_VacationCasualId",
                table: "All",
                column: "VacationCasualId");

            migrationBuilder.CreateIndex(
                name: "IX_All_VacationScheduleId",
                table: "All",
                column: "VacationScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationCasuals_EmpId",
                table: "VacationCasuals",
                column: "EmpId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VacationSchedules_EmpId",
                table: "VacationSchedules",
                column: "EmpId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "All");

            migrationBuilder.DropTable(
                name: "VacationCasuals");

            migrationBuilder.DropTable(
                name: "VacationSchedules");

            migrationBuilder.DropTable(
                name: "Emp");
        }
    }
}
