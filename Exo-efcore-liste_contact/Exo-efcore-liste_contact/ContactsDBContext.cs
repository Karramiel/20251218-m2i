using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Exo_efcore_liste_contact
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Remplace par ta vraie chaîne de connexion si nécessaire
            optionsBuilder.UseMySql("server=localhost;database=contactdb;user=root;password=formation",
                new MySqlServerVersion(new Version(8, 0, 31)));
        }
    }
}