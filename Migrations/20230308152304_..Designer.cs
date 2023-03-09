﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MockProject.Data;

#nullable disable

namespace MockProject.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230308152304_.")]
    partial class _
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MockProject.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RemainingQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("MockProject.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RemainingQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Product 1",
                            Price = 100.0,
                            RemainingQuantity = 10,
                            Review = "Good"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Product 2",
                            Price = 200.0,
                            RemainingQuantity = 20,
                            Review = "Excellent"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Product 3",
                            Price = 50.0,
                            RemainingQuantity = 5,
                            Review = "Average"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Product 4",
                            Price = 80.0,
                            RemainingQuantity = 15,
                            Review = "Good"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Product 5",
                            Price = 150.0,
                            RemainingQuantity = 25,
                            Review = "Excellent"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Product 6",
                            Price = 90.0,
                            RemainingQuantity = 10,
                            Review = "Good"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Product 7",
                            Price = 60.0,
                            RemainingQuantity = 8,
                            Review = "Average"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Product 8",
                            Price = 70.0,
                            RemainingQuantity = 12,
                            Review = "Good"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Product 9",
                            Price = 250.0,
                            RemainingQuantity = 30,
                            Review = "Excellent"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Product 10",
                            Price = 120.0,
                            RemainingQuantity = 18,
                            Review = "Good"
                        });
                });

            modelBuilder.Entity("MockProject.Models.User.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocalUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "nguyentunglam2410@gmail.com",
                            Name = "Nguyen Tung Lam",
                            Password = "lam55526",
                            Role = "Admin",
                            UserName = "tunglam24"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
