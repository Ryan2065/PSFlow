using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSFlow.DB.Migrations.SQL
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    VariableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.VariableId);
                });

            migrationBuilder.CreateTable(
                name: "FlowScripts",
                columns: table => new
                {
                    FlowScriptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<int>(type: "int", nullable: false),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowScripts", x => x.FlowScriptId);
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    FlowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveScriptId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.FlowId);
                    table.ForeignKey(
                        name: "FK_Flows_FlowScripts_ActiveScriptId",
                        column: x => x.ActiveScriptId,
                        principalTable: "FlowScripts",
                        principalColumn: "FlowScriptId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flows_ActiveScriptId",
                table: "Flows",
                column: "ActiveScriptId",
                unique: true,
                filter: "[ActiveScriptId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FlowScripts_FlowId",
                table: "FlowScripts",
                column: "FlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowScripts_Flows_FlowId",
                table: "FlowScripts",
                column: "FlowId",
                principalTable: "Flows",
                principalColumn: "FlowId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flows_FlowScripts_ActiveScriptId",
                table: "Flows");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "FlowScripts");

            migrationBuilder.DropTable(
                name: "Flows");
        }
    }
}
