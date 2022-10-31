﻿// <auto-generated />
using System;
using AddressRegistry.Projections.Syndication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AddressRegistry.Projections.Syndication.Migrations
{
    [DbContext(typeof(SyndicationContext))]
    [Migration("20221031094128_AddStreetNamePersistentLocalIdIndex")]
    partial class AddStreetNamePersistentLocalIdIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.AddressLink.AddressLinkSyndicationItem", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BoxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("PersistentLocalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StreetNameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("AddressId"));

                    b.HasIndex("IsComplete", "IsRemoved");

                    b.ToTable("AddressLinksExtract_Addresses", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.BuildingUnit.AddressBuildingUnitLinkExtractItem", b =>
                {
                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressPersistentLocalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingUnitPersistentLocalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("DbaseRecord")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsAddressLinkRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBuildingComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBuildingUnitComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBuildingUnitRemoved")
                        .HasColumnType("bit");

                    b.HasKey("AddressId", "BuildingUnitId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("AddressId", "BuildingUnitId"), false);

                    b.HasIndex("AddressId");

                    b.HasIndex("AddressPersistentLocalId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("AddressPersistentLocalId"));

                    b.HasIndex("BuildingId");

                    b.HasIndex("BuildingUnitId");

                    b.HasIndex("IsAddressLinkRemoved", "IsBuildingUnitComplete", "IsBuildingUnitRemoved", "IsBuildingComplete");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("IsAddressLinkRemoved", "IsBuildingUnitComplete", "IsBuildingUnitRemoved", "IsBuildingComplete"), new[] { "AddressId", "BuildingUnitId" });

                    b.ToTable("AddressBuildingUnitLinksExtract", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.BuildingUnit.BuildingUnitAddressMatchLatestItem", b =>
                {
                    b.Property<Guid>("BuildingUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingUnitPersistentLocalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBuildingComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.HasKey("BuildingUnitId", "AddressId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("BuildingUnitId", "AddressId"), false);

                    b.HasIndex("AddressId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("AddressId"));

                    b.HasIndex("BuildingId");

                    b.HasIndex("IsComplete", "IsRemoved", "IsBuildingComplete");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("IsComplete", "IsRemoved", "IsBuildingComplete"), new[] { "AddressId", "BuildingUnitId" });

                    b.ToTable("BuildingUnitAddressMatchLatestItemSyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalityBosaItem", b =>
                {
                    b.Property<Guid>("MunicipalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFlemishRegion")
                        .HasColumnType("bit");

                    b.Property<string>("NameDutch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameDutchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglishSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameFrench")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFrenchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameGerman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameGermanSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<int?>("PrimaryLanguage")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MunicipalityId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("MunicipalityId"), false);

                    b.HasIndex("IsFlemishRegion");

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("NisCode"));

                    b.HasIndex("Position");

                    b.HasIndex("Version");

                    b.ToTable("MunicipalityBosa", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalityLatestItem", b =>
                {
                    b.Property<Guid>("MunicipalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameDutch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameDutchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglishSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameFrench")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFrenchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameGerman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameGermanSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<int?>("PrimaryLanguage")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MunicipalityId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("MunicipalityId"), false);

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("NisCode"));

                    b.HasIndex("Position");

                    b.ToTable("MunicipalityLatestSyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalitySyndicationItem", b =>
                {
                    b.Property<Guid>("MunicipalityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<string>("NameDutch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFrench")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameGerman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OfficialLanguagesAsString")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OfficialLanguages");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MunicipalityId", "Position");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("MunicipalityId", "Position"), false);

                    b.HasIndex("NisCode");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("NisCode"));

                    b.HasIndex("Position");

                    b.HasIndex("Version");

                    b.ToTable("MunicipalitySyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Parcel.AddressParcelLinkExtractItem", b =>
                {
                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParcelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressPersistentLocalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("DbaseRecord")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsAddressLinkRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsParcelRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("ParcelPersistentLocalId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId", "ParcelId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("AddressId", "ParcelId"), false);

                    b.HasIndex("AddressId");

                    b.HasIndex("AddressPersistentLocalId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("AddressPersistentLocalId"));

                    b.HasIndex("ParcelId");

                    b.HasIndex("IsParcelRemoved", "IsAddressLinkRemoved");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("IsParcelRemoved", "IsAddressLinkRemoved"), new[] { "AddressId", "ParcelId" });

                    b.ToTable("AddressParcelLinksExtract", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Parcel.ParcelAddressMatchLatestItem", b =>
                {
                    b.Property<Guid>("ParcelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("ParcelPersistentLocalId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParcelId", "AddressId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("ParcelId", "AddressId"), false);

                    b.HasIndex("AddressId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("AddressId"));

                    b.HasIndex("IsRemoved");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("IsRemoved"), new[] { "AddressId", "ParcelId" });

                    b.HasIndex("ParcelId");

                    b.ToTable("ParcelAddressMatchLatestItemSyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoLatestItem", b =>
                {
                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostalCode");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("PostalCode"), false);

                    b.HasIndex("NisCode");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("NisCode"));

                    b.ToTable("PostalInfoLatestSyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoPostalName", b =>
                {
                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostalName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.HasKey("PostalCode", "PostalName");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("PostalCode", "PostalName"), false);

                    b.HasIndex("PostalName");

                    b.ToTable("PostalInfoPostalNamesLatestSyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameBosaItem", b =>
                {
                    b.Property<Guid>("StreetNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HomonymAdditionDutch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomonymAdditionEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomonymAdditionFrench")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomonymAdditionGerman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<string>("NameDutch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameDutchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglishSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameFrench")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFrenchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameGerman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameGermanSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PersistentLocalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StreetNameId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("StreetNameId"), false);

                    b.HasIndex("IsComplete");

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode");

                    b.HasIndex("Version");

                    b.ToTable("StreetNameBosa", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameLatestItem", b =>
                {
                    b.Property<Guid>("StreetNameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HomonymAdditionDutch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomonymAdditionEnglish")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomonymAdditionFrench")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomonymAdditionGerman")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("NameDutch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameDutchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameEnglishSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameFrench")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameFrenchSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameGerman")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameGermanSearch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PersistentLocalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StreetNameId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("StreetNameId"));

                    b.HasIndex("HomonymAdditionDutch");

                    b.HasIndex("HomonymAdditionEnglish");

                    b.HasIndex("HomonymAdditionFrench");

                    b.HasIndex("HomonymAdditionGerman");

                    b.HasIndex("NameDutch");

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglish");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrench");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGerman");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode");

                    b.HasIndex("PersistentLocalId");

                    b.HasIndex("IsComplete", "IsRemoved");

                    b.ToTable("StreetNameLatestSyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameSyndicationItem", b =>
                {
                    b.Property<Guid>("StreetNameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.Property<string>("HomonymAdditionDutch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomonymAdditionEnglish")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomonymAdditionFrench")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomonymAdditionGerman")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameDutch")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameEnglish")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameFrench")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NameGerman")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NisCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersistentLocalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StreetNameId", "Position");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("StreetNameId", "Position"), false);

                    b.HasIndex("HomonymAdditionDutch");

                    b.HasIndex("HomonymAdditionEnglish");

                    b.HasIndex("HomonymAdditionFrench");

                    b.HasIndex("HomonymAdditionGerman");

                    b.HasIndex("NameDutch");

                    b.HasIndex("NameEnglish");

                    b.HasIndex("NameFrench");

                    b.HasIndex("NameGerman");

                    b.ToTable("StreetNameSyndication", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("Be.Vlaanderen.Basisregisters.ProjectionHandling.Runner.ProjectionStates.ProjectionStateItem", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DesiredState")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DesiredStateChangedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Position")
                        .HasColumnType("bigint");

                    b.HasKey("Name");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Name"));

                    b.ToTable("ProjectionStates", "AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoPostalName", b =>
                {
                    b.HasOne("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoLatestItem", null)
                        .WithMany("PostalNames")
                        .HasForeignKey("PostalCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.PostalInfo.PostalInfoLatestItem", b =>
                {
                    b.Navigation("PostalNames");
                });
#pragma warning restore 612, 618
        }
    }
}
