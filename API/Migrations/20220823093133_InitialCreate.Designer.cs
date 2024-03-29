﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220823093133_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Database.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Database.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("ProductType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4646),
                            Name = "Boiled"
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4667),
                            Name = "Chewy"
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4672),
                            Name = "BubbleGum"
                        },
                        new
                        {
                            Id = 4,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4673),
                            Name = "Fizzy"
                        },
                        new
                        {
                            Id = 5,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4675),
                            Name = "Marshmallow"
                        },
                        new
                        {
                            Id = 6,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4677),
                            Name = "Jellies"
                        },
                        new
                        {
                            Id = 7,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4679),
                            Name = "Liquorice"
                        },
                        new
                        {
                            Id = 8,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4680),
                            Name = "Lollipops"
                        },
                        new
                        {
                            Id = 9,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4682),
                            Name = "Mints"
                        },
                        new
                        {
                            Id = 10,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4683),
                            Name = "Sherbet"
                        },
                        new
                        {
                            Id = 11,
                            CreationDate = new DateTime(2022, 8, 23, 9, 31, 33, 73, DateTimeKind.Utc).AddTicks(4684),
                            Name = "Chocolate"
                        });
                });

            modelBuilder.Entity("Database.Models.Product", b =>
                {
                    b.HasOne("Database.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductType");
                });
#pragma warning restore 612, 618
        }
    }
}
