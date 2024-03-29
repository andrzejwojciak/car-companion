﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using carcompanion.Data;

namespace carcompanion.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201214134512_add-used-attribute-to-refreshtoken")]
    partial class addusedattributetorefreshtoken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Mileage")
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

                    b.Property<Guid?>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndOfDateInterval")
                        .HasColumnType("datetime2");

                    b.Property<int>("MileageInterval")
                        .HasColumnType("int");

                    b.HasKey("ExpenseId");

                    b.HasIndex("CarId");

                    b.ToTable("Expenses");
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

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("carcompanion.Models.UserCar", b =>
                {
                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CarId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCars");
                });

            modelBuilder.Entity("carcompanion.Models.Expense", b =>
                {
                    b.HasOne("carcompanion.Models.Car", "Car")
                        .WithMany("Expenses")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("carcompanion.Models.UserCar", b =>
                {
                    b.HasOne("carcompanion.Models.Car", "Car")
                        .WithMany("UserCars")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
