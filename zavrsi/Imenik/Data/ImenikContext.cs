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
       
        
    }


}