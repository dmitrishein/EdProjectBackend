using EdProject.DAL.Entities;
using EdProject.DAL.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EdProject.DAL.DataContext
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,long>
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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region relations Author and Edition (many-to-many)
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

            #region relations Edition and OrderItems(one-to-one) 
            modelBuilder.Entity<OrderItems>()
                .HasOne(e => e.Edition)
                .WithOne(o => o.OrderItem)
                .HasForeignKey<OrderItems>(o => o.EditionId);
            #endregion

            #region relations orders and ordersItems(one-to-many)
            modelBuilder.Entity<Orders>().HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
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

            #region Настройка преобразователя значений enum.Currency в string
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

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        #region Tables
        public DbSet<Author> Authors { set; get; }
        public DbSet<AuthorInEditions> AuthorInEditions { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        #endregion

    }

    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<AppUser>();

            _ = modelBuilder.Entity<AppUser>().HasData(
                new AppUser
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
                new AppUser
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
           
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = 1, Name = "admin", RolesType = Enums.UserRolesType.Admin, NormalizedName = "admin" },
                new AppRole { Id = 2, Name = "client-user", RolesType = Enums.UserRolesType.Client, NormalizedName = "client" }
                );
            modelBuilder.Entity<IdentityUserRole<long>>().HasData(
                new IdentityUserRole<long> {  UserId = 1, RoleId = 1},
                new IdentityUserRole<long> {  UserId = 1, RoleId = 2},
                new IdentityUserRole<long> { UserId = 2 , RoleId = 2}
                );
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "William Shakespare"},
                new Author { Id = 2, Name = "Stephen King" }
                );
            modelBuilder.Entity<Edition>().HasData(
                new Edition { Id = 1, Title = "Hamlet", Description = "Classic Printing Edition", Price = 1444.9M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.Available, Type = EditionTypes.Book },
                new Edition { Id = 2, Title = "Othello", Description = "Classic Printing Edition", Price = 1200.9M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.Available, Type = EditionTypes.Book},
                new Edition { Id = 3, Title = "Pet Graveyard", Description = "Classic Printing Edition", Price = 1300.9M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.Available, Type = EditionTypes.Book },
                new Edition { Id = 4, Title = "Confrontation", Description = "Classic Printing Edition", Price = 1140.6M, Currency = CurrencyTypes.UAH, Status = EditionStatusTypes.NotAvailable, Type= EditionTypes.Book},
                new Edition { Id = 5, Title = "Something Weird", Description = "Featuring Willy Shakespare", Price = 120.23M , Currency = CurrencyTypes.EUR, Status = EditionStatusTypes.NotAvailable, Type = EditionTypes.Magazine}
                );
            modelBuilder.Entity<AuthorInEditions>().HasData(
                new AuthorInEditions { AuthorId = 1, EditionId = 1},
                new AuthorInEditions { AuthorId = 1, EditionId = 2 },
                new AuthorInEditions { AuthorId = 2, EditionId = 3 },
                new AuthorInEditions { AuthorId = 2, EditionId = 4 },
                new AuthorInEditions { AuthorId = 1, EditionId = 5 },
                new AuthorInEditions { AuthorId = 2, EditionId = 5 }
                );
        }
    }
}
