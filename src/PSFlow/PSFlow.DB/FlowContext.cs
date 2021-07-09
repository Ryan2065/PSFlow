using Microsoft.EntityFrameworkCore;
using PSFlow.DB.Models;
using System;

namespace PSFlow.DB
{
    public class FlowContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlowVersion>().HasOne(p => p.Flow).WithMany(many => many.Versions).HasForeignKey(key => key.FlowId).HasPrincipalKey(pkey => pkey.FlowId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Variable> Variables { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowVersion> FlowVersions { get; set; }
    }

    public class FlowContextSQL : FlowContext
    {
        private string _conString;
        public FlowContextSQL()
        {
            Console.WriteLine("test");
            _conString = Environment.GetEnvironmentVariable("PSFlow_SQLConnectionString");
        }
        public FlowContextSQL(string connectionString)
        {
            _conString = connectionString;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine(_conString);
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
