﻿// <auto-generated />
using DDD.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDD.Infrastructure.Migrations
{
    [DbContext(typeof(DomainContext))]
    partial class DomainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DDD.Domain.OrderAggregate.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("TotalCount")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("DDD.Domain.UserAggregate.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("DDD.Domain.OrderAggregate.Order", b =>
                {
                    b.OwnsOne("DDD.Domain.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("varchar(30)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("varchar(30)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("varchar(30)");

                            b1.HasKey("OrderId");

                            b1.ToTable("address", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
