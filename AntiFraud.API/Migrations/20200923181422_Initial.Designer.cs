﻿// <auto-generated />
using System;
using AntiFraud.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AntiFraud.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200923181422_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("AntiFraud.API.Models.Purchase", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Products")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("AntiFraud.API.Models.Purchase", b =>
                {
                    b.OwnsOne("AntiFraud.API.Models.Address", "Address", b1 =>
                        {
                            b1.Property<string>("PurchaseId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Country")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Street")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Zipcode")
                                .HasColumnType("TEXT");

                            b1.HasKey("PurchaseId");

                            b1.ToTable("Purchases");

                            b1.WithOwner()
                                .HasForeignKey("PurchaseId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
