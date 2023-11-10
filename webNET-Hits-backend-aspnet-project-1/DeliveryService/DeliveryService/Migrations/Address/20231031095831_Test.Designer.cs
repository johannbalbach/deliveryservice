﻿// <auto-generated />
using System;
using DeliveryService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliveryService.Migrations.Address
{
    [DbContext(typeof(AddressContext))]
    [Migration("20231031095831_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DeliveryService.Models.Address.AddressElement", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<int>("IsActive")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ObjectGuid")
                        .HasColumnType("uuid");

                    b.Property<long>("Objectid")
                        .HasColumnType("bigint");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DeliveryService.Models.Address.Hierarchy", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<long>("ObjectId")
                        .HasColumnType("bigint");

                    b.Property<long>("ParentObjectId")
                        .HasColumnType("bigint");

                    b.Property<int>("isActive")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("Hierarchies");
                });

            modelBuilder.Entity("DeliveryService.Models.Address.House", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("Addnum1")
                        .HasColumnType("text");

                    b.Property<string>("Addnum2")
                        .HasColumnType("text");

                    b.Property<int>("Addtype1")
                        .HasColumnType("integer");

                    b.Property<int>("Addtype2")
                        .HasColumnType("integer");

                    b.Property<string>("Housenum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IsActive")
                        .HasColumnType("integer");

                    b.Property<Guid>("ObjectGuid")
                        .HasColumnType("uuid");

                    b.Property<long>("Objectid")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("Houses");
                });
#pragma warning restore 612, 618
        }
    }
}
