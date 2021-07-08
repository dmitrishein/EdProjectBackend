using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace EdProject.DAL.Extension
{

    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();
            
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Admin756"),
                    EmailConfirmed = true,
                    Email = "adminex@sample.te",
                    NormalizedEmail = "ADMINEX@SAMPLE.TE",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                },
                new User
                {
                    Id = 2,
                    UserName = "client",
                    NormalizedUserName = "CLIENT",
                    FirstName = "Client",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "123User"),
                    EmailConfirmed = true,
                    Email = "userex@sample.te",
                    NormalizedEmail = "USEREX@SAMPLE.TE",
                    SecurityStamp = Guid.NewGuid().ToString("d")
                }
                );

            modelBuilder.Entity<IdentityRole<long>>().HasData(
                new IdentityRole<long> { Id = 1, Name = "admin",ConcurrencyStamp ="1", NormalizedName = "admin" },
                new IdentityRole<long> { Id = 2, Name = "client",ConcurrencyStamp ="2" ,NormalizedName = "client" }
                );
            modelBuilder.Entity<IdentityUserRole<long>>().HasData(
                new IdentityUserRole<long> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<long> { UserId = 1, RoleId = 2 },
                new IdentityUserRole<long> { UserId = 2, RoleId = 2 }
                );

            modelBuilder.Entity<Edition>().HasData(
                new Edition{ Id = 1, Title = "Hamlet", Description = "Classic Printing Edition", Price = 1444.9M, Currency = CurrencyTypes.USD, Status = AvailableStatusType.Available, Type = EditionTypes.Book },
                new Edition { Id = 2, Title = "Othello", Description = "Classic Printing Edition", Price = 1200.9M, Currency = CurrencyTypes.USD, Status = AvailableStatusType.Available, Type = EditionTypes.Book },
                new Edition { Id = 3, Title = "Pet Graveyard", Description = "Classic Printing Edition", Price = 1300.9M, Currency = CurrencyTypes.USD, Status = AvailableStatusType.Available, Type = EditionTypes.Book },
                new Edition { Id = 4, Title = "Confrontation", Description = "Classic Printing Edition", Price = 1140.6M, Currency = CurrencyTypes.USD, Status = AvailableStatusType.NotAvailable, Type = EditionTypes.Book },
                new Edition { Id = 5, Title = "Something Weird", Description = "Featuring Willy Shakespare", Price = 120.23M, Currency = CurrencyTypes.USD, Status = AvailableStatusType.NotAvailable, Type = EditionTypes.Magazine }
                );
            modelBuilder.Entity<Author>().HasData(
              new Author { Id = 1, Name = "William Shakespare" },
              new Author { Id = 2, Name = "Stephen King", });

            modelBuilder.Entity<Edition>()
                .HasMany(a => a.Authors)
                .WithMany(e => e.Editions)
                .UsingEntity(j => j.HasData(
                    new { AuthorsId = (long)1, EditionsId = (long)1 },
                    new { AuthorsId = (long)1, EditionsId = (long)2 },
                    new { AuthorsId = (long)2, EditionsId = (long)3 },
                    new { AuthorsId = (long)2, EditionsId = (long)4 },
                    new { AuthorsId = (long)1, EditionsId = (long)5 },
                    new { AuthorsId = (long)2, EditionsId = (long)5 }
                ));

        }
    }
}
