﻿using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using EdProject.DAL.Extension;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EdProject.DAL.DataContext
{
    public class AppDbContext : IdentityDbContext<User,Role,long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region relations between Orders and Editions (many-to-many) 
            modelBuilder.Entity<Edition>()
                .HasMany(o => o.Orders)
                .WithMany(e => e.Editions)
                .UsingEntity(j => j.ToTable("OrderItems"));
            #endregion

            #region relations between Authors and Editions (many-to-many)
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Editions)
                .WithMany(e => e.Authors)
                .UsingEntity(j => j.ToTable("AuthorInEditions"));
            #endregion

            #region relations User and Orders(one-to-many)
            modelBuilder.Entity<Orders>().HasOne(o => o.User)
                .WithMany(oi => oi.Orders)
                .HasForeignKey(oi => oi.UserId);
            #endregion

            #region relations Payments and Orders(one-to-one)
            modelBuilder.Entity<Orders>()
                .HasOne(p => p.Payment)
                .WithOne(o => o.Order)
                .HasForeignKey<Orders>(o => o.PaymentId);
            #endregion

            #region Convert enum.Currency into string
            modelBuilder
                .Entity<Edition>()
                .Property(e => e.Currency)
                .HasConversion(
                v => v.ToString(),
                v => (CurrencyTypes)Enum.Parse(typeof(CurrencyTypes), v)
                );
            modelBuilder
                .Entity<Edition>()
                .Property(e => e.Status)
                .HasConversion(
                v => v.ToString(),
                v => (AvailableStatusType)Enum.Parse(typeof(AvailableStatusType), v)
                );
            modelBuilder
                .Entity<Edition>()
                .Property(e => e.Type)
                .HasConversion(
                v => v.ToString(),
                v => (EditionTypes)Enum.Parse(typeof(EditionTypes), v)
                );
            #endregion

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Edition>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");
        }     

        #region Tables
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Author> Authors { set; get; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        #endregion
        
    }

}
