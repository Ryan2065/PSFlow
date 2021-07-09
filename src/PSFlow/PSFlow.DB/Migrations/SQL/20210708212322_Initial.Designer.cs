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
    [Migration("20210708212322_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PSFlow.DB.Models.Flow", b =>
                {
                    b.Property<int>("FlowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int?>("VersionId")
                        .HasColumnType("int");

                    b.HasKey("FlowId");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("PSFlow.DB.Models.FlowVersion", b =>
                {
                    b.Property<int>("FlowVersionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanRun")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlowId")
                        .HasColumnType("int");

                    b.Property<string>("Script")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlowVersionId");

                    b.HasIndex("FlowId");

                    b.ToTable("FlowVersions");
                });

            modelBuilder.Entity("PSFlow.DB.Models.Variable", b =>
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

            modelBuilder.Entity("PSFlow.DB.Models.FlowVersion", b =>
                {
                    b.HasOne("PSFlow.DB.Models.Flow", "Flow")
                        .WithMany("Versions")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("PSFlow.DB.Models.Flow", b =>
                {
                    b.Navigation("Versions");
                });
#pragma warning restore 612, 618
        }
    }
}
