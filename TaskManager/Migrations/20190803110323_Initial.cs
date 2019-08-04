using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Perfomers = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    PlannedLeadTime = table.Column<double>(nullable: false),
                    ActualLeadTime = table.Column<double>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    IsMainTask = table.Column<bool>(nullable: false),
                    TaskID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Task_Task_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Task",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskID",
                table: "Task",
                column: "TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
