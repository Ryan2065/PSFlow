﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSFlow.DB;

namespace PSFlow.DB.Migrations.SQL
{
    [DbContext(typeof(FlowContextSQL))]
    [Migration("20210713223858_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PSFlow.Models.Flow", b =>
                {
                    b.Property<int>("FlowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ActiveScriptId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("FlowId");

                    b.HasIndex("ActiveScriptId")
                        .IsUnique()
                        .HasFilter("[ActiveScriptId] IS NOT NULL");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("PSFlow.Models.FlowScript", b =>
                {
                    b.Property<int>("FlowScriptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("FlowId")
                        .HasColumnType("int");

                    b.Property<string>("Script")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlowScriptId");

                    b.HasIndex("FlowId");

                    b.ToTable("FlowScripts");
                });

            modelBuilder.Entity("PSFlow.Models.Job", b =>
                {
                    b.Property<Guid>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlowScriptId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.HasKey("JobId");

                    b.HasIndex("FlowScriptId");

                    b.HasIndex("StatusId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("PSFlow.Models.JobStatus", b =>
                {
                    b.Property<short>("JobStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("JobStatusId");

                    b.ToTable("JobStatus");

                    b.HasData(
                        new
                        {
                            JobStatusId = (short)1,
                            Name = "Created"
                        },
                        new
                        {
                            JobStatusId = (short)2,
                            Name = "New"
                        },
                        new
                        {
                            JobStatusId = (short)5,
                            Name = "InProgress"
                        },
                        new
                        {
                            JobStatusId = (short)6,
                            Name = "Waiting"
                        },
                        new
                        {
                            JobStatusId = (short)10,
                            Name = "Complete"
                        },
                        new
                        {
                            JobStatusId = (short)15,
                            Name = "Error"
                        });
                });

            modelBuilder.Entity("PSFlow.Models.JobStreamData", b =>
                {
                    b.Property<Guid>("StreamDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ErrorRecord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("JobStreamDataTypeId")
                        .HasColumnType("smallint");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Recorded")
                        .HasColumnType("datetime2");

                    b.HasKey("StreamDataId");

                    b.HasIndex("JobId");

                    b.HasIndex("JobStreamDataTypeId");

                    b.ToTable("JobStreamData");
                });

            modelBuilder.Entity("PSFlow.Models.JobStreamDataType", b =>
                {
                    b.Property<short>("JobStreamDataTypeId")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobStreamDataTypeId");

                    b.ToTable("JobStreamDataTypes");

                    b.HasData(
                        new
                        {
                            JobStreamDataTypeId = (short)1,
                            Name = "Output"
                        },
                        new
                        {
                            JobStreamDataTypeId = (short)2,
                            Name = "Error"
                        },
                        new
                        {
                            JobStreamDataTypeId = (short)3,
                            Name = "Warning"
                        },
                        new
                        {
                            JobStreamDataTypeId = (short)4,
                            Name = "Verbose"
                        },
                        new
                        {
                            JobStreamDataTypeId = (short)5,
                            Name = "Debug"
                        },
                        new
                        {
                            JobStreamDataTypeId = (short)6,
                            Name = "Information"
                        },
                        new
                        {
                            JobStreamDataTypeId = (short)7,
                            Name = "Exception"
                        },
                        new
                        {
                            JobStreamDataTypeId = (short)8,
                            Name = "Progress"
                        });
                });

            modelBuilder.Entity("PSFlow.Models.Variable", b =>
                {
                    b.Property<int>("VariableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Environment")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VariableId");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("PSFlow.Models.Flow", b =>
                {
                    b.HasOne("PSFlow.Models.FlowScript", "ActiveScript")
                        .WithOne()
                        .HasForeignKey("PSFlow.Models.Flow", "ActiveScriptId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ActiveScript");
                });

            modelBuilder.Entity("PSFlow.Models.FlowScript", b =>
                {
                    b.HasOne("PSFlow.Models.Flow", "Flow")
                        .WithMany("Scripts")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("PSFlow.Models.Job", b =>
                {
                    b.HasOne("PSFlow.Models.FlowScript", "Script")
                        .WithMany("Jobs")
                        .HasForeignKey("FlowScriptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSFlow.Models.JobStatus", "Status")
                        .WithMany("Jobs")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Script");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PSFlow.Models.JobStreamData", b =>
                {
                    b.HasOne("PSFlow.Models.Job", "PSJob")
                        .WithMany("StreamData")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSFlow.Models.JobStreamDataType", "JobStreamDataType")
                        .WithMany("JobStreamData")
                        .HasForeignKey("JobStreamDataTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("JobStreamDataType");

                    b.Navigation("PSJob");
                });

            modelBuilder.Entity("PSFlow.Models.Flow", b =>
                {
                    b.Navigation("Scripts");
                });

            modelBuilder.Entity("PSFlow.Models.FlowScript", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("PSFlow.Models.Job", b =>
                {
                    b.Navigation("StreamData");
                });

            modelBuilder.Entity("PSFlow.Models.JobStatus", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("PSFlow.Models.JobStreamDataType", b =>
                {
                    b.Navigation("JobStreamData");
                });
#pragma warning restore 612, 618
        }
    }
}
