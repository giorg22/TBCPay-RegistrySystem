using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{

    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<PersonRelationship> Relationships { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRelationship>()
                .HasOne(r => r.Person)
                .WithMany(p => p.RelatedPersons)
                .HasForeignKey(r => r.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PersonRelationship>()
                .HasOne(r => r.RelatedPerson)
                .WithMany()
                .HasForeignKey(r => r.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-V7MNVLC\\MSSQLSERVER01;Initial Catalog=TBCPayRegistrySystemDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
        }
    }
}
