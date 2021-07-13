using Microsoft.EntityFrameworkCore;
using System;
using PSFlow.Models;
using System.Collections.Generic;

namespace PSFlow.DB
{
    public class FlowContext : DbContext
    {
        public DbSet<Variable> Variables { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowScript> FlowScripts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobStatus> JobStatus { get; set; }
        public DbSet<JobStreamData> JobStreamData { get; set; }
        public DbSet<JobStreamDataType> JobStreamDataTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlowScript>()
                .HasOne(p => p.Flow)
                .WithMany(many => many.Scripts)
                .HasForeignKey(key => key.FlowId)
                .HasPrincipalKey(pkey => pkey.FlowId);
            modelBuilder.Entity<Flow>()
                .HasOne(p => p.ActiveScript)
                .WithOne()
                .HasForeignKey<Flow>(p => p.ActiveScriptId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Job>()
                .HasOne(p => p.Status)
                .WithMany(many => many.Jobs)
                .HasPrincipalKey(pkey => pkey.JobStatusId)
                .HasForeignKey(fkey => fkey.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Job>()
                .HasOne(p => p.Script)
                .WithMany(many => many.Jobs)
                .HasPrincipalKey(pkey => pkey.FlowScriptId)
                .HasForeignKey(fkey => fkey.FlowScriptId)
                .OnDelete(DeleteBehavior.Cascade);
            List<JobStatus> jobStatuses = new List<JobStatus>();
            foreach(JobStatusEnum jsEnum in Enum.GetValues(typeof(JobStatusEnum)))
            {
                jobStatuses.Add(new Models.JobStatus()
                {
                    JobStatusId = ((short)jsEnum),
                    Name = jsEnum.ToString()
                });
            }
            modelBuilder.Entity<JobStatus>()
                .HasData(jobStatuses.ToArray());

            modelBuilder.Entity<JobStreamData>()
                .HasOne(p => p.PSJob)
                .WithMany(many => many.StreamData)
                .HasForeignKey(key => key.JobId)
                .HasPrincipalKey(pkey => pkey.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobStreamData>()
                .HasOne(p => p.JobStreamDataType)
                .WithMany(many => many.JobStreamData)
                .HasPrincipalKey(pkey => pkey.JobStreamDataTypeId)
                .HasForeignKey(fkey => fkey.JobStreamDataTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            List<JobStreamDataType> jobStreamDataTypes = new List<JobStreamDataType>();
            foreach (JobStreamDataTypeEnum jsEnum in Enum.GetValues(typeof(JobStreamDataTypeEnum)))
            {
                jobStreamDataTypes.Add(new Models.JobStreamDataType()
                {
                    JobStreamDataTypeId = ((short)jsEnum),
                    Name = jsEnum.ToString()
                });
            }

            modelBuilder.Entity<JobStreamDataType>()
                .HasData(jobStreamDataTypes.ToArray());

            base.OnModelCreating(modelBuilder);
        }
        
    }

    public class FlowContextSQL : FlowContext
    {
        private string _conString;
        public FlowContextSQL()
        {
            _conString = Environment.GetEnvironmentVariable("PSFlow_SQLConnectionString");
        }
        public FlowContextSQL(string connectionString)
        {
            _conString = connectionString;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conString);
            base.OnConfiguring(optionsBuilder);
        }
    }
    public class FlowContextSqlite :FlowContext
    {
        private string _conString;
        public FlowContextSqlite()
        {
            _conString = Environment.GetEnvironmentVariable("PSFlow_SQLiteConnectionString");
        }
        public FlowContextSqlite(string connectionString)
        {
            _conString = connectionString;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_conString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
