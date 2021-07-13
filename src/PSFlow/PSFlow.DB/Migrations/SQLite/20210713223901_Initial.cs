using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSFlow.DB.Migrations.SQLite
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    JobStatusId = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.JobStatusId);
                });

            migrationBuilder.CreateTable(
                name: "JobStreamDataTypes",
                columns: table => new
                {
                    JobStreamDataTypeId = table.Column<short>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStreamDataTypes", x => x.JobStreamDataTypeId);
                });

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

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FlowScriptId = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StatusId = table.Column<short>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Jobs_FlowScripts_FlowScriptId",
                        column: x => x.FlowScriptId,
                        principalTable: "FlowScripts",
                        principalColumn: "FlowScriptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_JobStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JobStatus",
                        principalColumn: "JobStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobStreamData",
                columns: table => new
                {
                    StreamDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    JobId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Recorded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ErrorRecord = table.Column<string>(type: "TEXT", nullable: true),
                    JobStreamDataTypeId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStreamData", x => x.StreamDataId);
                    table.ForeignKey(
                        name: "FK_JobStreamData_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobStreamData_JobStreamDataTypes_JobStreamDataTypeId",
                        column: x => x.JobStreamDataTypeId,
                        principalTable: "JobStreamDataTypes",
                        principalColumn: "JobStreamDataTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "JobStatusId", "Name" },
                values: new object[] { (short)1, "Created" });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "JobStatusId", "Name" },
                values: new object[] { (short)2, "New" });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "JobStatusId", "Name" },
                values: new object[] { (short)5, "InProgress" });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "JobStatusId", "Name" },
                values: new object[] { (short)6, "Waiting" });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "JobStatusId", "Name" },
                values: new object[] { (short)10, "Complete" });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "JobStatusId", "Name" },
                values: new object[] { (short)15, "Error" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)1, "Output" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)2, "Error" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)3, "Warning" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)4, "Verbose" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)5, "Debug" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)6, "Information" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)7, "Exception" });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[] { (short)8, "Progress" });

            migrationBuilder.CreateIndex(
                name: "IX_Flows_ActiveScriptId",
                table: "Flows",
                column: "ActiveScriptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlowScripts_FlowId",
                table: "FlowScripts",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_FlowScriptId",
                table: "Jobs",
                column: "FlowScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_StatusId",
                table: "Jobs",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStreamData_JobId",
                table: "JobStreamData",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStreamData_JobStreamDataTypeId",
                table: "JobStreamData",
                column: "JobStreamDataTypeId");

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
                name: "JobStreamData");

            migrationBuilder.DropTable(
                name: "Variables");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "JobStreamDataTypes");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.DropTable(
                name: "FlowScripts");

            migrationBuilder.DropTable(
                name: "Flows");
        }
    }
}
