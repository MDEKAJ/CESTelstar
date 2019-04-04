using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TelstarCES.Data.Models;

namespace TelstarCES.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<Connection> Connections { get; set; }

        public DbSet<ParcelType> ParcelTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>()
                .HasMany<Connection>(c => c.Connections)
                .WithOne();

            base.OnModelCreating(builder);
        }
    }
}
