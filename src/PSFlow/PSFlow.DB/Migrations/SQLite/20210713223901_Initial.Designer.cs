﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSFlow.DB;

namespace PSFlow.DB.Migrations.SQLite
{
    [DbContext(typeof(FlowContextSqlite))]
    [Migration("20210713223901_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("PSFlow.Models.Flow", b =>
                {
                    b.Property<int>("FlowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ActiveScriptId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("FlowId");

                    b.HasIndex("ActiveScriptId")
                        .IsUnique();

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("PSFlow.Models.FlowScript", b =>
                {
                    b.Property<int>("FlowScriptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlowId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Script")
                        .HasColumnType("TEXT");

                    b.HasKey("FlowScriptId");

                    b.HasIndex("FlowId");

                    b.ToTable("FlowScripts");
                });

            modelBuilder.Entity("PSFlow.Models.Job", b =>
                {
                    b.Property<Guid>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("FlowScriptId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<short>("StatusId")
                        .HasColumnType("INTEGER");

                    b.HasKey("JobId");

                    b.HasIndex("FlowScriptId");

                    b.HasIndex("StatusId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("PSFlow.Models.JobStatus", b =>
                {
                    b.Property<short>("JobStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

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
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorRecord")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("JobId")
                        .HasColumnType("TEXT");

                    b.Property<short>("JobStreamDataTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Recorded")
                        .HasColumnType("TEXT");

                    b.HasKey("StreamDataId");

                    b.HasIndex("JobId");

                    b.HasIndex("JobStreamDataTypeId");

                    b.ToTable("JobStreamData");
                });

            modelBuilder.Entity("PSFlow.Models.JobStreamDataType", b =>
                {
                    b.Property<short>("JobStreamDataTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

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
                        .HasColumnType("INTEGER");

                    b.Property<string>("Environment")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

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
