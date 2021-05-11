﻿// <auto-generated />
using System;
using HousePlants.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KrnankSoft.HousePlants.Migrations
{
    [DbContext(typeof(HousePlantsContext))]
    [Migration("20210327222941_AddedLegendStatus")]
    partial class AddedLegendStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HousePlants.Domain.Models.Family", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(20000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Family");
                });

            modelBuilder.Entity("HousePlants.Domain.Models.Genus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(20000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Genus");
                });

            modelBuilder.Entity("HousePlants.Domain.Models.Plant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AquiredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CommonName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(20000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GenusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LatinName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("LegendStatus")
                        .HasColumnType("int");

                    b.Property<short>("LightRequirement")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<short>("SoilRequirement")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<short>("WaterRequirement")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.HasIndex("GenusId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("HousePlants.Domain.Models.Plant", b =>
                {
                    b.HasOne("HousePlants.Domain.Models.Family", "Family")
                        .WithMany()
                        .HasForeignKey("FamilyId");

                    b.HasOne("HousePlants.Domain.Models.Genus", "Genus")
                        .WithMany()
                        .HasForeignKey("GenusId");

                    b.Navigation("Family");

                    b.Navigation("Genus");
                });
#pragma warning restore 612, 618
        }
    }
}
