using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Data;

namespace Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("WatherStationId");

                    b.HasKey("Id");

                    b.HasIndex("WatherStationId");

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
                    b.HasOne("Data.WatherStation", "WatherStation")
                        .WithMany()
                        .HasForeignKey("WatherStationId");
                });
        }
    }
}
