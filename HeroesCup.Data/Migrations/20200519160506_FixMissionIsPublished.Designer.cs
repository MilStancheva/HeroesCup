﻿// <auto-generated />
using System;
using HeroesCup.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HeroesCup.Data.Migrations
{
    [DbContext(typeof(HeroesCupDbContext))]
    [Migration("20200519160506_FixMissionIsPublished")]
    partial class FixMissionIsPublished
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HeroesCup.Data.Models.Club", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OrganizationType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("HeroesCup.Data.Models.ClubImage", b =>
                {
                    b.Property<Guid?>("ClubId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("char(36)");

                    b.HasKey("ClubId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("ClubImages");
                });

            modelBuilder.Entity("HeroesCup.Data.Models.Hero", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ClubId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsCoordinator")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("HeroesCup.Data.Models.HeroMission", b =>
                {
                    b.Property<Guid>("HeroId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MissionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("HeroId", "MissionId");

                    b.HasIndex("MissionId");

                    b.ToTable("HeroMission");
                });

            modelBuilder.Entity("HeroesCup.Data.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Bytes")
                        .HasColumnType("longblob");

                    b.Property<string>("ContentType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Filename")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("HeroesCup.Data.Models.Mission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ClubId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("EndDate")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("SchoolYear")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<long>("StartDate")
                        .HasColumnType("bigint");

                    b.Property<string>("TimeheroesUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("HeroesCup.Data.Models.MissionImage", b =>
                {
                    b.Property<Guid?>("MissionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("char(36)");

                    b.HasKey("MissionId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("MissionImages");
                });

            modelBuilder.Entity("HeroesCup.Data.Models.ClubImage", b =>
                {
                    b.HasOne("HeroesCup.Data.Models.Club", "Club")
                        .WithMany("ClubImages")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeroesCup.Data.Models.Image", "Image")
                        .WithMany("ClubImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HeroesCup.Data.Models.Hero", b =>
                {
                    b.HasOne("HeroesCup.Data.Models.Club", "Club")
                        .WithMany("Heroes")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HeroesCup.Data.Models.HeroMission", b =>
                {
                    b.HasOne("HeroesCup.Data.Models.Hero", "Hero")
                        .WithMany("HeroMissions")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeroesCup.Data.Models.Mission", "Mission")
                        .WithMany("HeroMissions")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HeroesCup.Data.Models.Mission", b =>
                {
                    b.HasOne("HeroesCup.Data.Models.Club", "Club")
                        .WithMany("Missions")
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HeroesCup.Data.Models.MissionImage", b =>
                {
                    b.HasOne("HeroesCup.Data.Models.Image", "Image")
                        .WithMany("MissionImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HeroesCup.Data.Models.Mission", "Mission")
                        .WithMany("MissionImages")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
