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
    [Migration("20180707190757_ProductsAddColumnDescription")]
    partial class ProductsAddColumnDescription
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

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NVARCHAR(250)")
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasDefaultValue("No description");

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
                        new { ProductId = 1, Name = "AMD Gaming Sound Card", Price = 233.96m, Quantity = 2m },
                        new { ProductId = 2, Name = "NVidia Science Mouse", Price = 474.95m, Quantity = 9m },
                        new { ProductId = 3, Name = "Samsung Business Processor", Price = 326.77m, Quantity = 9m },
                        new { ProductId = 4, Name = "MSI Science Monitor", Price = 641.42m, Quantity = 8m },
                        new { ProductId = 5, Name = "Cooler Master Science Processor", Price = 300.76m, Quantity = 7m }
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
