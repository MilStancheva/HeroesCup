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
    [Migration("20200513100929_SchoolClubToClub")]
    partial class SchoolClubToClub
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

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OrganizationType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clubs");
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

                    b.HasKey("HeroId", "MissionId");

                    b.HasIndex("MissionId");

                    b.ToTable("HeroMission");
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

                    b.Property<byte[]>("Image")
                        .HasColumnType("longblob");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("SchoolYear")
                        .HasColumnType("int");

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
#pragma warning restore 612, 618
        }
    }
}
