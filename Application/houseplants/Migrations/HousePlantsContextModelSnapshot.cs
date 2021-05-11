﻿// <auto-generated />
using System;
using HousePlants.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KrnankSoft.HousePlants.Migrations
{
    [DbContext(typeof(HousePlantsContext))]
    partial class HousePlantsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HousePlants.Domain.Models.Classification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(20000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GenusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.HasIndex("GenusId");

                    b.ToTable("Classification");
                });

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

                    b.Property<Guid?>("ClassificationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommonName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(20000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LatinName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("LegendStatus")
                        .HasColumnType("int");

                    b.Property<short?>("LightRequirement")
                        .HasColumnType("smallint");

                    b.Property<int>("MinimumTemperature")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<short?>("SoilRequirement")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("Toxic")
                        .HasColumnType("bit");

                    b.Property<short?>("WaterRequirement")
                        .HasColumnType("smallint");

                    b.Property<int>("WateringTechnique")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassificationId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("HousePlants.Domain.Models.PlantGroup", b =>
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

                    b.ToTable("PlantGroup");
                });

            modelBuilder.Entity("PlantPlantGroup", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlantsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupsId", "PlantsId");

                    b.HasIndex("PlantsId");

                    b.ToTable("PlantPlantGroup");
                });

            modelBuilder.Entity("HousePlants.Domain.Models.Classification", b =>
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

            modelBuilder.Entity("HousePlants.Domain.Models.Plant", b =>
                {
                    b.HasOne("HousePlants.Domain.Models.Classification", "Classification")
                        .WithMany()
                        .HasForeignKey("ClassificationId");

                    b.Navigation("Classification");
                });

            modelBuilder.Entity("PlantPlantGroup", b =>
                {
                    b.HasOne("HousePlants.Domain.Models.PlantGroup", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HousePlants.Domain.Models.Plant", null)
                        .WithMany()
                        .HasForeignKey("PlantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
