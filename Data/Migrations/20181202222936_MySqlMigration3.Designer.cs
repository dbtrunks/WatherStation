﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20181202222936_MySqlMigration3")]
    partial class MySqlMigration3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065");

            modelBuilder.Entity("Data.ReadLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Details");

                    b.HasKey("Id");

                    b.ToTable("ReadLog");
                });

            modelBuilder.Entity("Data.TemperatureMeasurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(9,4)");

                    b.Property<int?>("WeatherStationId");

                    b.HasKey("Id");

                    b.HasIndex("WeatherStationId");

                    b.ToTable("TemperatureMeasurement");
                });

            modelBuilder.Entity("Data.WeatherStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExternalKey")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("WeatherStation");
                });

            modelBuilder.Entity("Data.TemperatureMeasurement", b =>
                {
                    b.HasOne("Data.WeatherStation", "WeatherStation")
                        .WithMany()
                        .HasForeignKey("WeatherStationId");
                });
#pragma warning restore 612, 618
        }
    }
}