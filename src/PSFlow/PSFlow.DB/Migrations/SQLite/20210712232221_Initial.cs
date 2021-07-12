using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSFlow.DB.Migrations.SQLite
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Variables",
                columns: table => new
                {
                    VariableId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Environment = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variables", x => x.VariableId);
                });

            migrationBuilder.CreateTable(
                name: "FlowScripts",
                columns: table => new
                {
                    FlowScriptId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlowId = table.Column<int>(type: "INTEGER", nullable: false),
                    Script = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowScripts", x => x.FlowScriptId);
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    FlowId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ActiveScriptId = table.Column<int>(type: "INTEGER", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                unique: true);

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
