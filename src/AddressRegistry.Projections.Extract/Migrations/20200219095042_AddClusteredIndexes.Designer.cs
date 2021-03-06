﻿// <auto-generated />
using System;
using AddressRegistry.Projections.Extract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AddressRegistry.Projections.Extract.Migrations
{
    [DbContext(typeof(ExtractContext))]
    [Migration("20200219095042_AddClusteredIndexes")]
    partial class AddClusteredIndexes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AddressRegistry.Projections.Extract.AddressExtract.AddressExtractItem", b =>
                {
                    b.Property<Guid?>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressPersistentLocalId")
                        .HasColumnType("int");

                    b.Property<bool>("Complete")
                        .HasColumnType("bit");

                    b.Property<byte[]>("DbaseRecord")
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("MaximumX")
                        .HasColumnType("float");

                    b.Property<double>("MaximumY")
                        .HasColumnType("float");

                    b.Property<double>("MinimumX")
                        .HasColumnType("float");

                    b.Property<double>("MinimumY")
                        .HasColumnType("float");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("ShapeRecordContent")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("ShapeRecordContentLength")
                        .HasColumnType("int");

                    b.Property<Guid>("StreetNameId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AddressId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("AddressPersistentLocalId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("Complete");

                    b.HasIndex("NisCode");

                    b.HasIndex("StreetNameId");

                    b.ToTable("Address","AddressRegistryExtract");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Extract.AddressLinkExtract.AddressLinkExtractItem", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Complete")
                        .HasColumnType("bit");

                    b.Property<byte[]>("DbaseRecord")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersistentLocalId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StreetNameId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AddressId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("PersistentLocalId")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("AddressLinks","AddressRegistryExtract");
                });

            modelBuilder.Entity("Be.Vlaanderen.Basisregisters.ProjectionHandling.Runner.ProjectionStates.ProjectionStateItem", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DesiredState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DesiredStateChangedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.HasKey("Name")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("ProjectionStates","AddressRegistryExtract");
                });
#pragma warning restore 612, 618
        }
    }
}
