﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Data;

namespace Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20170606203122_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int?>("WatherStationIdId");

                    b.HasKey("Id");

                    b.HasIndex("WatherStationIdId");

                    b.ToTable("TemperatureMeasurement");
                });

            modelBuilder.Entity("Data.WatherStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ExternalKey");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("WatherStation");
                });

            modelBuilder.Entity("Data.TemperatureMeasurement", b =>
                {
                    b.HasOne("Data.WatherStation", "WatherStationId")
                        .WithMany()
                        .HasForeignKey("WatherStationIdId");
                });
        }
    }
}
