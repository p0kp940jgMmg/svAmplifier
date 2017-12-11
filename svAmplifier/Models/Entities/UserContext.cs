using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace svAmplifier.Models.Entities
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<Mushrooms> Mushrooms { get; set; }
        public virtual DbSet<Pick> Pick { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=dottnett.database.windows.net;Initial Catalog=Room;Integrated Security=False;User ID=danielsoderberg;Password=T0matS()ppa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mushrooms>(entity =>
            {
                entity.ToTable("Mushrooms", "dco");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MushroomName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MushroomPicUrl).IsRequired();
            });

            modelBuilder.Entity<Pick>(entity =>
            {
                entity.ToTable("Pick", "dco");

                entity.Property(e => e.City)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.DatePicked).HasColumnType("datetime");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.MushroomName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Street)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pick)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Pick__UserId__60A75C0F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "dco");

                entity.HasIndex(e => e.AspNetId)
                    .HasName("UQ__User__9C3F232AD5155025")
                    .IsUnique();

                entity.Property(e => e.AspNetId).IsRequired();

                entity.Property(e => e.City)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });
        }
    }
}
