using Microsoft.EntityFrameworkCore;
using System;
using PSFlow.Models;

namespace PSFlow.DB
{
    public class FlowContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlowScript>().HasOne(p => p.Flow).WithMany(many => many.Scripts).HasForeignKey(key => key.FlowId).HasPrincipalKey(pkey => pkey.FlowId);
            modelBuilder.Entity<Flow>().HasOne(p => p.ActiveScript).WithOne().HasForeignKey<Flow>(p => p.ActiveScriptId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Variable> Variables { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<FlowScript> FlowScripts { get; set; }
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
