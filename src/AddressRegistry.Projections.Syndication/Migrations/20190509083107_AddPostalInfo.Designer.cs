﻿// <auto-generated />
using System;
using AddressRegistry.Projections.Syndication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AddressRegistry.Projections.Syndication.Migrations
{
    [DbContext(typeof(SyndicationContext))]
    [Migration("20190509083107_AddPostalInfo")]
    partial class AddPostalInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalityBosaItem", b =>
                {
                    b.Property<Guid>("MunicipalityId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsFlemishRegion");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameDutchSearch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameEnglishSearch");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameFrenchSearch");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NameGermanSearch");

                    b.Property<string>("NisCode");

                    b.Property<long>("Position");

                    b.Property<int?>("PrimaryLanguage");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("MunicipalityId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("Position");

                    b.ToTable("MunicipalityBosa","AddressRegistryLegacy");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalityLatestItem", b =>
                {
                    b.Property<Guid>("MunicipalityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameDutchSearch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameEnglishSearch");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameFrenchSearch");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NameGermanSearch");

                    b.Property<string>("NisCode");

                    b.Property<long>("Position");

                    b.Property<int?>("PrimaryLanguage");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("MunicipalityId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("Position");

                    b.ToTable("MunicipalityLatestSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalitySyndicationItem", b =>
                {
                    b.Property<Guid>("MunicipalityId");

                    b.Property<long>("Position");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NisCode");

                    b.Property<string>("OfficialLanguagesAsString")
                        .HasColumnName("OfficialLanguages");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("MunicipalityId", "Position")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("NisCode")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("Position");

                    b.HasIndex("Version");

                    b.ToTable("MunicipalitySyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoLatestItem", b =>
                {
                    b.Property<string>("PostalCode")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NisCode");

                    b.Property<long>("Position");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("PostalCode")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("NisCode");

                    b.ToTable("PostalInfoLatestSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoPostalName", b =>
                {
                    b.Property<string>("PostalCode");

                    b.Property<string>("PostalName");

                    b.Property<int>("Language");

                    b.HasKey("PostalCode", "PostalName")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("PostalName");

                    b.ToTable("PostalInfoPostalNamesLatestSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameBosaItem", b =>
                {
                    b.Property<Guid>("StreetNameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HomonymAdditionDutch");

                    b.Property<string>("HomonymAdditionEnglish");

                    b.Property<string>("HomonymAdditionFrench");

                    b.Property<string>("HomonymAdditionGerman");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameDutchSearch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameEnglishSearch");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameFrenchSearch");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NameGermanSearch");

                    b.Property<string>("NisCode");

                    b.Property<string>("OsloId");

                    b.Property<long>("Position");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("StreetNameId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("IsComplete");

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode");

                    b.ToTable("StreetNameBosa","AddressRegistryLegacy");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameLatestItem", b =>
                {
                    b.Property<Guid>("StreetNameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HomonymAdditionDutch");

                    b.Property<string>("HomonymAdditionEnglish");

                    b.Property<string>("HomonymAdditionFrench");

                    b.Property<string>("HomonymAdditionGerman");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NisCode");

                    b.Property<string>("OsloId");

                    b.Property<long>("Position");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("StreetNameId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("HomonymAdditionDutch");

                    b.HasIndex("HomonymAdditionEnglish");

                    b.HasIndex("HomonymAdditionFrench");

                    b.HasIndex("HomonymAdditionGerman");

                    b.HasIndex("IsComplete");

                    b.HasIndex("NameDutch");

                    b.HasIndex("NameEnglish");

                    b.HasIndex("NameFrench");

                    b.HasIndex("NameGerman");

                    b.HasIndex("NisCode");

                    b.ToTable("StreetNameLatestSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameSyndicationItem", b =>
                {
                    b.Property<Guid>("StreetNameId");

                    b.Property<long>("Position");

                    b.Property<string>("HomonymAdditionDutch");

                    b.Property<string>("HomonymAdditionEnglish");

                    b.Property<string>("HomonymAdditionFrench");

                    b.Property<string>("HomonymAdditionGerman");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NisCode");

                    b.Property<string>("OsloId");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("StreetNameId", "Position")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("HomonymAdditionDutch");

                    b.HasIndex("HomonymAdditionEnglish");

                    b.HasIndex("HomonymAdditionFrench");

                    b.HasIndex("HomonymAdditionGerman");

                    b.HasIndex("NameDutch");

                    b.HasIndex("NameEnglish");

                    b.HasIndex("NameFrench");

                    b.HasIndex("NameGerman");

                    b.ToTable("StreetNameSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("Be.Vlaanderen.Basisregisters.ProjectionHandling.Runner.ProjectionStates.ProjectionStateItem", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Position");

                    b.HasKey("Name")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("ProjectionStates","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoPostalName", b =>
                {
                    b.HasOne("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoLatestItem")
                        .WithMany("PostalNames")
                        .HasForeignKey("PostalCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
