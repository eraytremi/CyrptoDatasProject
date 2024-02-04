﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(CyrptoContext))]
    [Migration("20240203150149_1")]
    partial class _1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Model.Bakiye", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ParaMiktarı")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ParaTipiId")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParaTipiId");

                    b.HasIndex("UserId");

                    b.ToTable("Bakiye");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ParaMiktarı = 10000000m,
                            ParaTipiId = 1,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 2,
                            ParaMiktarı = 10000000m,
                            ParaTipiId = 2,
                            UserId = 1L
                        },
                        new
                        {
                            Id = 3,
                            ParaMiktarı = 10000000m,
                            ParaTipiId = 1,
                            UserId = 2L
                        },
                        new
                        {
                            Id = 4,
                            ParaMiktarı = 10000000m,
                            ParaTipiId = 2,
                            UserId = 2L
                        });
                });

            modelBuilder.Entity("Model.CoinList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Count")
                        .HasColumnType("float");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CoinList");
                });

            modelBuilder.Entity("Model.ParaTipi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DövizTipi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParaTipi");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DövizTipi = "TL"
                        },
                        new
                        {
                            Id = 2,
                            DövizTipi = "USDT"
                        },
                        new
                        {
                            Id = 3,
                            DövizTipi = "BTC"
                        },
                        new
                        {
                            Id = 4,
                            DövizTipi = "ETH"
                        });
                });

            modelBuilder.Entity("Model.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Count")
                        .HasColumnType("float");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("WaitingTrades")
                        .HasColumnType("bit");

                    b.Property<bool>("isBuy")
                        .HasColumnType("bit");

                    b.Property<bool>("isSell")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "eray@mail.com",
                            FirstName = "Eray",
                            LastName = "Türemiş",
                            Password = "1"
                        },
                        new
                        {
                            Id = 2L,
                            Email = "erdal@mail.com",
                            FirstName = "Erdal",
                            LastName = "Türemiş",
                            Password = "2"
                        });
                });

            modelBuilder.Entity("Model.Bakiye", b =>
                {
                    b.HasOne("Model.ParaTipi", "ParaTipi")
                        .WithMany("UserBakiyeler")
                        .HasForeignKey("ParaTipiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.User", "User")
                        .WithMany("UserBakiyeler")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParaTipi");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.CoinList", b =>
                {
                    b.HasOne("Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.Trade", b =>
                {
                    b.HasOne("Model.User", "User")
                        .WithMany("Trade")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.ParaTipi", b =>
                {
                    b.Navigation("UserBakiyeler");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Navigation("Trade");

                    b.Navigation("UserBakiyeler");
                });
#pragma warning restore 612, 618
        }
    }
}
