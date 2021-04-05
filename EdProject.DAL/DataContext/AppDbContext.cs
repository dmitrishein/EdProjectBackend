﻿using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EdProject.DAL.DataContext
{
    public class AppDbContext : IdentityDbContext<User,Role,long>
    {
        #region Config Options
        public class OptionsBuild
        {
            private AppConfig settings { get; set; }
            public OptionsBuild()
            {
                settings = new AppConfig();
                optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(settings.sqlConnectionString);
                dbOptions = optionsBuilder.Options;
            }
            public DbContextOptionsBuilder<AppDbContext> optionsBuilder { get; set; }
            public DbContextOptions<AppDbContext> dbOptions { get; set; }
          
        }
        public OptionsBuild ops = new OptionsBuild();
        #endregion

        #region Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Создание связи многие-ко-многим через AuthorInEdition
            modelBuilder.Entity<AuthorInEditions>()
                .HasKey(ba => new { ba.EditionId, ba.AuthorId });
            modelBuilder.Entity<AuthorInEditions>()
                .HasOne(ba => ba.Edition)
                .WithMany(ba => ba.Authors)
                .HasForeignKey(ba => ba.EditionId);
            modelBuilder.Entity<AuthorInEditions>()
                .HasOne(ba => ba.Author)
                .WithMany(ba => ba.Editions)
                .HasForeignKey(ba => ba.AuthorId);
            #endregion

            #region один-ко-одному связь между Edition and OrderItems
            modelBuilder.Entity<OrderItems>()
                .HasOne(e => e.Edition)
                .WithOne(o => o.OrderItem)
                .HasForeignKey<OrderItems>(o => o.EditionId);
            #endregion

            #region связь между orders and ordersItems
            modelBuilder.Entity<Orders>().HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
            #endregion

            #region Настройка преобразователя значений enum в string
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
                v => (EditionStatusTypes)Enum.Parse(typeof(EditionStatusTypes), v)
                );
            modelBuilder
                .Entity<Edition>()
                .Property(e => e.Type)
                .HasConversion(
                v => v.ToString(),
                v => (EditionTypes)Enum.Parse(typeof(EditionTypes), v)
                );
            #endregion

            #region отношения между Payments and Orders
            modelBuilder.Entity<Orders>()
                .HasOne(p => p.Payment)
                .WithOne(o => o.Order)
                .HasForeignKey<Orders>(o => o.PaymentId);
            #endregion

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        DbSet<Author> Authors { set; get; }
        DbSet<AuthorInEditions> AuthorInEditions { get; set; }
        DbSet<Edition> Editions { get; set; }
        DbSet<OrderItems> OrderItems { get; set; }
        DbSet<Orders> Orders { get; set; }
        DbSet<Payments> Payments { get; set; }
        

    }
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "William Shakespare"},
                new Author { Id = 2, Name = "Stephen King" }
                );
            modelBuilder.Entity<Edition>().HasData(
                new Edition { Id = 1, Title = "Hamlet", Desciption = "Classic Printing Edition", Price = 1444.9M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.Available, Type = EditionTypes.Book },
                new Edition { Id = 2, Title = "Othello", Desciption = "Classic Printing Edition", Price = 1200.9M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.Available, Type = EditionTypes.Book},
                new Edition { Id = 3, Title = "Pet Graveyard", Desciption = "Classic Printing Edition", Price = 1300.9M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.Available, Type = EditionTypes.Book },
                new Edition { Id = 4, Title = "Confrontation", Desciption = "Classic Printing Edition", Price = 1140.6M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.NotAvailable, Type= EditionTypes.Book},
                new Edition { Id = 5, Title = "Something Weird", Desciption = "Featuring Willy Shakespare", Price = 120.23M , Currency = CurrencyTypes.EUR, Status = EditionStatusTypes.NotAvailable, Type = EditionTypes.Magazine}
                );
            modelBuilder.Entity<AuthorInEditions>().HasData(
                new AuthorInEditions { AuthorId = 1, EditionId = 1},
                new AuthorInEditions { AuthorId = 1, EditionId = 2 },
                new AuthorInEditions { AuthorId = 2, EditionId = 3 },
                new AuthorInEditions { AuthorId = 2, EditionId = 4 },
                new AuthorInEditions { AuthorId = 1, EditionId = 5 },
                new AuthorInEditions { AuthorId = 2, EditionId = 5 }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "admin", RolesType = Enums.UserRolesType.Admin},
                new Role { Id = 2, Name = "client-user", RolesType = Enums.UserRolesType.Client}
                );

        }
    }
}
