﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using carcompanion.Data;

namespace carcompanion.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("carcompanion.Models.Car", b =>
                {
                    b.Property<Guid>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Generation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Plate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("carcompanion.Models.Expense", b =>
                {
                    b.Property<Guid>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndOfDateInterval")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MileageInterval")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExpenseId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("carcompanion.Models.ExpenseCategory", b =>
                {
                    b.Property<string>("ExpenseCategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseCategoryId");

                    b.ToTable("ExpenseCategories");

                    b.HasData(
                        new
                        {
                            ExpenseCategoryId = "other"
                        },
                        new
                        {
                            ExpenseCategoryId = "insurance"
                        },
                        new
                        {
                            ExpenseCategoryId = "repair"
                        },
                        new
                        {
                            ExpenseCategoryId = "fuel"
                        },
                        new
                        {
                            ExpenseCategoryId = "utilization"
                        });
                });

            modelBuilder.Entity("carcompanion.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("carcompanion.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccessTokenJti")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Used")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RefreshTokenId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("carcompanion.Models.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = "user"
                        },
                        new
                        {
                            RoleId = "superUser"
                        },
                        new
                        {
                            RoleId = "admin"
                        });
                });

            modelBuilder.Entity("carcompanion.Models.ShareKey", b =>
                {
                    b.Property<Guid>("ShareKeyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IssuerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Used")
                        .HasColumnType("bit");

                    b.Property<string>("UserCarRoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ShareKeyId");

                    b.HasIndex("CarId");

                    b.HasIndex("IssuerId");

                    b.HasIndex("UserCarRoleId");

                    b.ToTable("ShareKeys");
                });

            modelBuilder.Entity("carcompanion.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("018bc3cc-24a1-45ac-bdd8-4ee8da30ff81"),
                            AddedDate = new DateTime(2021, 1, 18, 17, 39, 26, 69, DateTimeKind.Local).AddTicks(7707),
                            Email = "admin@example.com",
                            EmailConfirmed = false,
                            Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=",
                            RoleId = "admin"
                        },
                        new
                        {
                            UserId = new Guid("9162e011-93d5-4bab-8599-46c17dfd0aa7"),
                            AddedDate = new DateTime(2021, 1, 18, 17, 39, 26, 75, DateTimeKind.Local).AddTicks(9411),
                            Email = "superuser@example.com",
                            EmailConfirmed = false,
                            Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=",
                            RoleId = "superUser"
                        },
                        new
                        {
                            UserId = new Guid("00894133-c04f-4064-8d82-d7b54018f456"),
                            AddedDate = new DateTime(2021, 1, 18, 17, 39, 26, 75, DateTimeKind.Local).AddTicks(9572),
                            Email = "firstuser@example.com",
                            EmailConfirmed = false,
                            Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=",
                            RoleId = "user"
                        },
                        new
                        {
                            UserId = new Guid("5a8e3d5b-3f60-4b4f-bdba-fa8564a34bc0"),
                            AddedDate = new DateTime(2021, 1, 18, 17, 39, 26, 75, DateTimeKind.Local).AddTicks(9587),
                            Email = "secounduser@example.com",
                            EmailConfirmed = false,
                            Password = "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=",
                            RoleId = "user"
                        });
                });

            modelBuilder.Entity("carcompanion.Models.UserCar", b =>
                {
                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserCarRoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CarId", "UserId");

                    b.HasIndex("UserCarRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCars");
                });

            modelBuilder.Entity("carcompanion.Models.UserCarRole", b =>
                {
                    b.Property<string>("UserCarRoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserCarRoleId");

                    b.ToTable("UserCarRoles");

                    b.HasData(
                        new
                        {
                            UserCarRoleId = "owner"
                        },
                        new
                        {
                            UserCarRoleId = "editor"
                        },
                        new
                        {
                            UserCarRoleId = "viewer"
                        });
                });

            modelBuilder.Entity("carcompanion.Models.Expense", b =>
                {
                    b.HasOne("carcompanion.Models.Car", "Car")
                        .WithMany("Expenses")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("carcompanion.Models.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("carcompanion.Models.ShareKey", b =>
                {
                    b.HasOne("carcompanion.Models.Car", "Car")
                        .WithMany("ShareKeys")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("carcompanion.Models.User", "Issuer")
                        .WithMany("ShareKeys")
                        .HasForeignKey("IssuerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("carcompanion.Models.UserCarRole", "UserCarRole")
                        .WithMany("ShareKeys")
                        .HasForeignKey("UserCarRoleId");
                });

            modelBuilder.Entity("carcompanion.Models.User", b =>
                {
                    b.HasOne("carcompanion.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("carcompanion.Models.UserCar", b =>
                {
                    b.HasOne("carcompanion.Models.Car", "Car")
                        .WithMany("UserCars")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("carcompanion.Models.UserCarRole", "UserCarRole")
                        .WithMany("UserCars")
                        .HasForeignKey("UserCarRoleId");

                    b.HasOne("carcompanion.Models.User", "User")
                        .WithMany("UserCars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
