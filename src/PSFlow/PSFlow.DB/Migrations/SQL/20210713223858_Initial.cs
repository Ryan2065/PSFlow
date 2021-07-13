using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSFlow.DB.Migrations.SQL
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    JobStatusId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.JobStatusId);
                });

            migrationBuilder.CreateTable(
                name: "JobStreamDataTypes",
                columns: table => new
                {
                    JobStreamDataTypeId = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStreamDataTypes", x => x.JobStreamDataTypeId);
                });

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

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlowScriptId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    StreamDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErrorRecord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobStreamDataTypeId = table.Column<short>(type: "smallint", nullable: false)
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
                values: new object[,]
                {
                    { (short)1, "Created" },
                    { (short)2, "New" },
                    { (short)5, "InProgress" },
                    { (short)6, "Waiting" },
                    { (short)10, "Complete" },
                    { (short)15, "Error" }
                });

            migrationBuilder.InsertData(
                table: "JobStreamDataTypes",
                columns: new[] { "JobStreamDataTypeId", "Name" },
                values: new object[,]
                {
                    { (short)1, "Output" },
                    { (short)2, "Error" },
                    { (short)3, "Warning" },
                    { (short)4, "Verbose" },
                    { (short)5, "Debug" },
                    { (short)6, "Information" },
                    { (short)7, "Exception" },
                    { (short)8, "Progress" }
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
