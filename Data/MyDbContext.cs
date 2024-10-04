using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.HasKey(i => i.IncidentName);
                entity.Property(i => i.IncidentName)
                      .ValueGeneratedOnAdd();

                entity.HasOne(i => i.Account)
                      .WithMany(a => a.Incidents)
                      .HasForeignKey(i => i.AccountId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.AccountId);

                entity.HasIndex(a => a.Name)
                      .IsUnique();

                entity.HasOne(a => a.Contact)
                      .WithMany(c => c.Accounts)
                      .HasForeignKey(a => a.ContactId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.ContactId);

                entity.HasIndex(c => c.Email)
                      .IsUnique();
            });
        }
    }
}
