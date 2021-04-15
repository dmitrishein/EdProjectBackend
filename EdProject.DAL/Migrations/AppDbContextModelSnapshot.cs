﻿// <auto-generated />
using System;
using EdProject.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EdProject.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EdProject.DAL.Entities.AppRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("RolesType")
                        .HasColumnType("int");

                    b.Property<bool>("isRemoved")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ConcurrencyStamp = "90ba6ae6-c94d-4175-ab28-66fb87149ab2",
                            Name = "admin",
                            NormalizedName = "admin",
                            RolesType = 1,
                            isRemoved = false
                        },
                        new
                        {
                            Id = 2L,
                            ConcurrencyStamp = "bfcb641b-f284-4d1f-a38b-e071eeee9db6",
                            Name = "client-user",
                            NormalizedName = "client",
                            RolesType = 2,
                            isRemoved = false
                        });
                });

            modelBuilder.Entity("EdProject.DAL.Entities.AppUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("isRemoved")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c6dd3d5c-ce35-49ee-8902-67145a50d38e",
                            Email = "adminex@sample.te",
                            EmailConfirmed = true,
                            FirstName = "Admin",
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMINEX@SAMPLE.TE",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEJbSuJcr/OcGeVbCDq5fP+81ZlBnJnNZTdvAlNSU6XX4EA8ts9HJ79l6F/eJWA2yFg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d02ef817-4f60-47bc-8323-3bc88c4b440c",
                            TwoFactorEnabled = false,
                            UserName = "admin",
                            isRemoved = false
                        },
                        new
                        {
                            Id = 2L,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e8798e86-de31-417a-ae75-c2ef12e3bb36",
                            Email = "userex@sample.te",
                            EmailConfirmed = true,
                            FirstName = "Client",
                            LastName = "User",
                            LockoutEnabled = false,
                            NormalizedEmail = "USEREX@SAMPLE.TE",
                            NormalizedUserName = "CLIENT",
                            PasswordHash = "AQAAAAEAACcQAAAAEA8Ewhl2NFF8SvU3TM0UnzIbBGvqeQL+SmY9iUKfnHgmN/9eP5JHN+gNzrK4rJ1uJQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fcd73f62-119d-413a-9d46-12f1e6c234c0",
                            TwoFactorEnabled = false,
                            UserName = "client",
                            isRemoved = false
                        });
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            IsRemoved = false,
                            Name = "William Shakespare"
                        },
                        new
                        {
                            Id = 2L,
                            IsRemoved = false,
                            Name = "Stephen King"
                        });
                });

            modelBuilder.Entity("EdProject.DAL.Entities.AuthorInEditions", b =>
                {
                    b.Property<long>("EditionId")
                        .HasColumnType("bigint");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("EditionId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AuthorInEditions");

                    b.HasData(
                        new
                        {
                            EditionId = 1L,
                            AuthorId = 1L,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EditionId = 2L,
                            AuthorId = 1L,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EditionId = 3L,
                            AuthorId = 2L,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EditionId = 4L,
                            AuthorId = 2L,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EditionId = 5L,
                            AuthorId = 1L,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EditionId = 5L,
                            AuthorId = 2L,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Edition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Editions");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Currency = "UAH",
                            Desciption = "Classic Printing Edition",
                            IsRemoved = false,
                            Price = 1444.9m,
                            Status = "Available",
                            Title = "Hamlet",
                            Type = "Book"
                        },
                        new
                        {
                            Id = 2L,
                            Currency = "UAH",
                            Desciption = "Classic Printing Edition",
                            IsRemoved = false,
                            Price = 1200.9m,
                            Status = "Available",
                            Title = "Othello",
                            Type = "Book"
                        },
                        new
                        {
                            Id = 3L,
                            Currency = "UAH",
                            Desciption = "Classic Printing Edition",
                            IsRemoved = false,
                            Price = 1300.9m,
                            Status = "Available",
                            Title = "Pet Graveyard",
                            Type = "Book"
                        },
                        new
                        {
                            Id = 4L,
                            Currency = "UAH",
                            Desciption = "Classic Printing Edition",
                            IsRemoved = false,
                            Price = 1140.6m,
                            Status = "NotAvailable",
                            Title = "Confrontation",
                            Type = "Book"
                        },
                        new
                        {
                            Id = 5L,
                            Currency = "EUR",
                            Desciption = "Featuring Willy Shakespare",
                            IsRemoved = false,
                            Price = 120.23m,
                            Status = "NotAvailable",
                            Title = "Something Weird",
                            Type = "Magazine"
                        });
                });

            modelBuilder.Entity("EdProject.DAL.Entities.OrderItems", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<long>("EditionId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EditionId")
                        .IsUnique();

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Orders", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<int>("MyProperty")
                        .HasColumnType("int");

                    b.Property<long>("PaymentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Payments", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            RoleId = 1L
                        },
                        new
                        {
                            UserId = 1L,
                            RoleId = 2L
                        },
                        new
                        {
                            UserId = 2L,
                            RoleId = 2L
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.AuthorInEditions", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.Author", "Author")
                        .WithMany("Editions")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EdProject.DAL.Entities.Edition", "Edition")
                        .WithMany("Authors")
                        .HasForeignKey("EditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Edition");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.OrderItems", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.Edition", "Edition")
                        .WithOne("OrderItem")
                        .HasForeignKey("EdProject.DAL.Entities.OrderItems", "EditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EdProject.DAL.Entities.Orders", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Edition");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Orders", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.Payments", "Payment")
                        .WithOne("Order")
                        .HasForeignKey("EdProject.DAL.Entities.Orders", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EdProject.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("EdProject.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Author", b =>
                {
                    b.Navigation("Editions");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Edition", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Orders", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("EdProject.DAL.Entities.Payments", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
