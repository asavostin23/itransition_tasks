using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Task6.Models;

namespace Task6
{
    public partial class task6dbContext : DbContext
    {
        public task6dbContext(DbContextOptions<task6dbContext> options) : base(options)
        {
        }

        public DbSet<Firstname> NamesBy { get; set; } = null!;
        public DbSet<Settlement> SettlementsBy { get; set; } = null!;
        public DbSet<Surname> SurnamesBy { get; set; } = null!;
        public DbSet<Street> StreetsBy { get; set; } = null!;
        public DbSet<Patronymic> PatronymicsBy { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firstname>(entity =>
            {
                entity.ToTable("Names_by");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Settlement>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("Settlements_by");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Region).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.District).HasMaxLength(255);
            });

            modelBuilder.Entity<Surname>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("Surnames_by");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Patronymic>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("Patronymics_by");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.ToTable("Streets_by");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
