using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace svAmplifier.Models.Entities
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Pick> Pick { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebApplication1DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                //entity.Property(idNr =>
                //{

                //});
                entity.Property(e => e.Email);

                entity.Property(e => e.UserName);

                entity.Property(e => e.Password);

                entity.Property(e => e.Address);
            });

            modelBuilder.Entity<Pick>(entity =>
            {
                entity.ToTable("User");
                entity.Property(e => e.Mushroom);

                entity.Property(e => e.User);

                entity.Property(e => e.Location);
            });
        }
    }
}
