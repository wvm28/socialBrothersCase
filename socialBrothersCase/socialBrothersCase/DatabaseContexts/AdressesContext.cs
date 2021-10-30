using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using socialBrothersCase.Models;

namespace socialBrothersCase.DatabaseContexts
{
    public class AdressesContext : DbContext
    {
        public DbSet<Adress> Adresses { get; set; }
        public AdressesContext(DbContextOptions<AdressesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adress>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e => e.Street).HasMaxLength(250);
                entity.Property(e => e.HouseNumber).HasMaxLength(250); ;
                entity.Property(e => e.PostalCode).HasMaxLength(250); ;
                entity.Property(e => e.Location).HasMaxLength(250); ;
                entity.Property(e => e.Country).HasMaxLength(250); ;

                entity.HasData(new Adress
                {
                    Id = Guid.NewGuid(),
                    Street = "Europalaan",
                    HouseNumber = 100,
                    PostalCode = "3526 KS",
                    Location = "Utrecht",
                    Country = "The Netherlands"                
                });
            });
        }
    }
}
