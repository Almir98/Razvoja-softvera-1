﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using System;

namespace RS1_PrakticniDioIspita_2017_01_24.Migrations
{
    [DbContext(typeof(MojContext))]
    [Migration("20200109195721_inicijalna")]
    partial class inicijalna
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.Angazovan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("NatavnikID");

                    b.Property<int?>("OdjeljenjeID");

                    b.Property<int?>("PredmetID");

                    b.HasKey("Id");

                    b.HasIndex("NatavnikID");

                    b.HasIndex("OdjeljenjeID");

                    b.HasIndex("PredmetID");

                    b.ToTable("Angazovan");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.Nastavnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ime");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Nastavnik");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.Odjeljenje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("NastavnikID");

                    b.Property<string>("Oznaka");

                    b.Property<int>("Razred");

                    b.HasKey("Id");

                    b.HasIndex("NastavnikID");

                    b.ToTable("Odjeljenje");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.OdrzaniCas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AngazovanID");

                    b.Property<DateTime>("datum");

                    b.HasKey("Id");

                    b.HasIndex("AngazovanID");

                    b.ToTable("OdrzaniCas");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.OdrzaniCasDetalj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Ocjena");

                    b.Property<bool>("Odsutan");

                    b.Property<bool?>("OpravdanoOdsutan");

                    b.Property<int>("UpisUOdjeljenjeID");

                    b.HasKey("Id");

                    b.HasIndex("UpisUOdjeljenjeID");

                    b.ToTable("OdrzaniCasDetalj");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.Predmet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("Id");

                    b.ToTable("Predmet");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.Ucenik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ime");

                    b.HasKey("Id");

                    b.ToTable("Ucenik");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.UpisUOdjeljenje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrojUDnevniku");

                    b.Property<int>("OdjeljenjeID");

                    b.Property<int>("UcenikID");

                    b.HasKey("Id");

                    b.HasIndex("OdjeljenjeID");

                    b.HasIndex("UcenikID");

                    b.ToTable("UpisUOdjeljenje");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.Angazovan", b =>
                {
                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.Nastavnik", "Natavnik")
                        .WithMany()
                        .HasForeignKey("NatavnikID");

                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.Odjeljenje", "Odjeljenje")
                        .WithMany()
                        .HasForeignKey("OdjeljenjeID");

                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.Predmet", "Predmet")
                        .WithMany()
                        .HasForeignKey("PredmetID");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.Odjeljenje", b =>
                {
                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.Nastavnik", "Nastavnik")
                        .WithMany()
                        .HasForeignKey("NastavnikID");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.OdrzaniCas", b =>
                {
                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.Angazovan", "Angazovan")
                        .WithMany()
                        .HasForeignKey("AngazovanID");
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.OdrzaniCasDetalj", b =>
                {
                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.UpisUOdjeljenje", "UpisUOdjeljenje")
                        .WithMany()
                        .HasForeignKey("UpisUOdjeljenjeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RS1_PrakticniDioIspita_2017_01_24.Models.UpisUOdjeljenje", b =>
                {
                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.Odjeljenje", "Odjeljenje")
                        .WithMany()
                        .HasForeignKey("OdjeljenjeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RS1_PrakticniDioIspita_2017_01_24.Models.Ucenik", "Ucenik")
                        .WithMany()
                        .HasForeignKey("UcenikID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
