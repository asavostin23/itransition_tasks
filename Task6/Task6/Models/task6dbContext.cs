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

        public DbSet<FirstnameBy> NamesBy { get; set; } = null!;
        public DbSet<FirstnamePl> NamesPl { get; set; } = null!;
        public DbSet<FirstnameUk> NamesUk { get; set; } = null!;
        public DbSet<SettlementBy> SettlementsBy { get; set; } = null!;
        public DbSet<SettlementPl> SettlementsPl{ get; set; } = null!;
        public DbSet<SettlementUk> SettlementsUk { get; set; } = null!;
        public DbSet<SurnameBy> SurnamesBy { get; set; } = null!;
        public DbSet<SurnamePl> SurnamesPl { get; set; } = null!;
        public DbSet<SurnameUk> SurnamesUk { get; set; } = null!;
        public DbSet<StreetBy> StreetsBy { get; set; } = null!;
        public DbSet<StreetPl> StreetsPl { get; set; } = null!;
        public DbSet<StreetUk> StreetsUk { get; set; } = null!;
        public DbSet<PatronymicBy> PatronymicsBy { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirstnameBy>(entity =>
            {
                entity.ToTable("Names_by");
                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<FirstnamePl>(entity =>
            {
                entity.ToTable("Names_pl");
                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<FirstnameUk>(entity =>
            {
                entity.ToTable("Names_uk");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SettlementBy>(entity =>
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
            modelBuilder.Entity<SettlementPl>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Settlements_pl");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Type).HasMaxLength(255);
            });
            modelBuilder.Entity<SettlementUk>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Settlements_uk");
                entity.Property(e => e.Name).HasMaxLength(255);
            });
            modelBuilder.Entity<SurnameBy>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Surnames_by");
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<SurnamePl>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Surnames_pl");
                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<SurnameUk>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Surnames_uk");
                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<PatronymicBy>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Patronymics_by");
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<StreetBy>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Streets_by");
                //entity.Property(e => e.Id)
                //    .ValueGeneratedOnAdd()
                //    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<StreetPl>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Streets_pl");
                entity.Property(e => e.Name).HasMaxLength(255);
            });
            modelBuilder.Entity<StreetUk>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.ToTable("Streets_uk");
                entity.Property(e => e.Name).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
