using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Data
{
    internal class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;database=demo_efcore;user=root;password=formation;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        // Creation de la migration
        // 1 - Add-Migration initial
        // 2 - Update-Database
    }
}
