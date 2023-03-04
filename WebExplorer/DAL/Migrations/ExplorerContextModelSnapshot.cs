﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ExplorerContext))]
    partial class ExplorerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EntryPointId")
                        .HasColumnType("int");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EntryPointId");

                    b.HasIndex("ParentId");

                    b.ToTable("Catalogs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EntryPointId = 1,
                            IsProcessed = true,
                            Name = "Creating Digital Images"
                        },
                        new
                        {
                            Id = 2,
                            IsProcessed = true,
                            Name = "Resources",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            IsProcessed = true,
                            Name = "Evidence",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 4,
                            IsProcessed = true,
                            Name = "Graphic products",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 5,
                            IsProcessed = true,
                            Name = "Primary Sources",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 6,
                            IsProcessed = true,
                            Name = "Secondary Sources",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 7,
                            IsProcessed = true,
                            Name = "Process",
                            ParentId = 4
                        },
                        new
                        {
                            Id = 8,
                            IsProcessed = true,
                            Name = "Final Product",
                            ParentId = 4
                        });
                });

            modelBuilder.Entity("Core.EntryPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EntryPoints");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "db"
                        });
                });

            modelBuilder.Entity("Core.Catalog", b =>
                {
                    b.HasOne("Core.EntryPoint", "EntryPoint")
                        .WithMany("Catalogs")
                        .HasForeignKey("EntryPointId");

                    b.HasOne("Core.Catalog", "Parent")
                        .WithMany("Catalogs")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("EntryPoint");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Core.Catalog", b =>
                {
                    b.Navigation("Catalogs");
                });

            modelBuilder.Entity("Core.EntryPoint", b =>
                {
                    b.Navigation("Catalogs");
                });
#pragma warning restore 612, 618
        }
    }
}
