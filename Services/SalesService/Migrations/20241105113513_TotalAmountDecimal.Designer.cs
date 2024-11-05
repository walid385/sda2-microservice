﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalesService.Data;

#nullable disable

namespace SalesService.Migrations
{
    [DbContext(typeof(SalesContext))]
    [Migration("20241105113513_TotalAmountDecimal")]
    partial class TotalAmountDecimal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("SalesService.Models.RegistersTable", b =>
                {
                    b.Property<int>("RegisterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CloseTime")
                        .HasColumnType("TEXT");

                    b.Property<float>("CloseTotal")
                        .HasColumnType("REAL");

                    b.Property<float>("DropTotal")
                        .HasColumnType("REAL");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OpenEmpId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OpenTime")
                        .HasColumnType("TEXT");

                    b.Property<float>("OpenTotal")
                        .HasColumnType("REAL");

                    b.HasKey("RegisterId");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("SalesService.Models.TicketSystem", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("CartPurchase")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("Cash")
                        .HasColumnType("REAL");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float?>("Cost")
                        .HasColumnType("REAL");

                    b.Property<float?>("Credit")
                        .HasColumnType("REAL");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<float?>("Discount")
                        .HasColumnType("REAL");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("TEXT");

                    b.Property<float?>("Tax")
                        .HasColumnType("REAL");

                    b.Property<float?>("TaxRate")
                        .HasColumnType("REAL");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.HasKey("TicketId");

                    b.ToTable("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
