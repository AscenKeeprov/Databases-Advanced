﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase.Data.Migrations
{
    [DbContext(typeof(SalesContext))]
    [Migration("20180707185652_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("P03_SalesDatabase.Data.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreditCardNumber");

                    b.Property<string>("Email")
                        .HasColumnType("VARCHAR(80)")
                        .HasMaxLength(80)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(100)")
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("P03_SalesDatabase.Data.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<decimal>("Price")
                        .HasColumnType("SMALLMONEY");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new { ProductId = 1, Name = "NVidia Science Mouse", Price = 267.57m, Quantity = 1m },
                        new { ProductId = 2, Name = "Samsung Business Mouse", Price = 223.18m, Quantity = 8m },
                        new { ProductId = 3, Name = "Intel Business Processor", Price = 591.32m, Quantity = 8m },
                        new { ProductId = 4, Name = "NVidia Business Processor", Price = 354.38m, Quantity = 4m },
                        new { ProductId = 5, Name = "Cooler Master Gaming Keyboard", Price = 115.03m, Quantity = 10m },
                        new { ProductId = 6, Name = "MSI Science Monitor", Price = 487.8m, Quantity = 4m },
                        new { ProductId = 7, Name = "Gigabyte Gaming Chassis", Price = 333.23m, Quantity = 6m },
                        new { ProductId = 8, Name = "NVidia Gaming Mouse", Price = 558.38m, Quantity = 3m },
                        new { ProductId = 9, Name = "Gigabyte Business Mouse", Price = 648.63m, Quantity = 8m },
                        new { ProductId = 10, Name = "NVidia Business Chassis", Price = 435.89m, Quantity = 10m },
                        new { ProductId = 11, Name = "Cooler Master Science Monitor", Price = 229.36m, Quantity = 3m },
                        new { ProductId = 12, Name = "AMD Business Monitor", Price = 573.67m, Quantity = 1m },
                        new { ProductId = 13, Name = "ASUS Business Monitor", Price = 109.72m, Quantity = 4m }
                    );
                });

            modelBuilder.Entity("P03_SalesDatabase.Data.Models.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATETIME2");

                    b.Property<int>("ProductId");

                    b.Property<int>("StoreId");

                    b.HasKey("SaleId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("P03_SalesDatabase.Data.Models.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("NVARCHAR(80)")
                        .HasMaxLength(80)
                        .IsUnicode(true);

                    b.HasKey("StoreId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("P03_SalesDatabase.Data.Models.Sale", b =>
                {
                    b.HasOne("P03_SalesDatabase.Data.Models.Customer", "Customer")
                        .WithMany("Sales")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P03_SalesDatabase.Data.Models.Product", "Product")
                        .WithMany("Sales")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P03_SalesDatabase.Data.Models.Store", "Store")
                        .WithMany("Sales")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}