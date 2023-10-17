using ImenikApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ImenikApp.Data
{
    public class ImenikContext : DbContext
    {
        public ImenikContext(DbContextOptions<ImenikContext> opcije)
            : base(opcije) { }
        public DbSet<Kontakt> Kontakt { get; set; }
        public DbSet<Email> Email { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //modelBuilder.Entity<Kontakt>().HasOne(k => k.Email);
        //}
    }


}