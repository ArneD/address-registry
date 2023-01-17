namespace AddressRegistry.Tests.AggregateTests.WhenCorrectingStreetNameNames
{
    using System.Collections.Generic;
    using Api.BackOffice.Abstractions;
    using StreetName;
    using StreetName.Commands;
    using StreetName.Events;
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Snapshotting;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using FluentAssertions;
    using global::AutoFixture;
    using Xunit;
    using Xunit.Abstractions;

    public class GivenStreetName : AddressRegistryTest
    {
        private readonly StreetNameStreamId _streamId;

        public GivenStreetName(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture.Customize(new InfrastructureCustomization());
            Fixture.Customize(new WithFixedStreetNamePersistentLocalId());
            _streamId = Fixture.Create<StreetNameStreamId>();
        }

        [Fact]
        public void ThenStreetNameNamesWereCorrected()
        {
            var streetNamePersistentLocalId = Fixture.Create<StreetNamePersistentLocalId>();

            var command = Fixture.Create<CorrectStreetNameNames>();

            var streetNameWasImported = new StreetNameWasImported(
                streetNamePersistentLocalId,
                Fixture.Create<MunicipalityId>(),
                StreetNameStatus.Retired);
            ((ISetProvenance)streetNameWasImported).SetProvenance(Fixture.Create<Provenance>());

            Assert(new Scenario()
                .Given(_streamId, streetNameWasImported)
                .When(command)
                .Then(
                    new Fact(new StreetNameStreamId(command.PersistentLocalId),
                        new StreetNameNamesWereCorrected(streetNamePersistentLocalId, command.StreetNameNames, new List<AddressPersistentLocalId>()))));
        }

        [Fact]
        public void StateCheck()
        {
            // Arrange
            var streetNamePersistentLocalId = Fixture.Create<StreetNamePersistentLocalId>();

            var migratedStreetNameWasImported = new MigratedStreetNameWasImported(
                Fixture.Create<StreetNameId>(),
                streetNamePersistentLocalId,
                Fixture.Create<MunicipalityId>(),
                Fixture.Create<NisCode>(),
                StreetNameStatus.Current);
            ((ISetProvenance)migratedStreetNameWasImported).SetProvenance(Fixture.Create<Provenance>());

            var addressWasProposedV2 = new AddressWasProposedV2(
                Fixture.Create<StreetNamePersistentLocalId>(),
                Fixture.Create<AddressPersistentLocalId>(),
                parentPersistentLocalId: null,
                Fixture.Create<PostalCode>(),
                Fixture.Create<HouseNumber>(),
                boxNumber: null,
                GeometryMethod.AppointedByAdministrator,
                GeometrySpecification.Lot,
                GeometryHelpers.GmlPointGeometry.ToExtendedWkbGeometry());
            ((ISetProvenance)addressWasProposedV2).SetProvenance(Fixture.Create<Provenance>());

            var streetNameNamesWereCorrected = new StreetNameNamesWereCorrected(
                new StreetNamePersistentLocalId(migratedStreetNameWasImported.StreetNamePersistentLocalId),
                Fixture.Create<Dictionary<string, string>>(),
                new List<AddressPersistentLocalId>
                    { new AddressPersistentLocalId(addressWasProposedV2.AddressPersistentLocalId) });
            ((ISetProvenance)streetNameNamesWereCorrected).SetProvenance(Fixture.Create<Provenance>());

            var sut = new StreetNameFactory(NoSnapshotStrategy.Instance).Create();

            // Act
            sut.Initialize(new List<object>
            {
                migratedStreetNameWasImported,
                addressWasProposedV2,
                streetNameNamesWereCorrected
            });

            // Assert
            foreach (var streetNameAddress in sut.StreetNameAddresses)
            {
                streetNameAddress.LastEventHash.Should().Be(streetNameNamesWereCorrected.GetHash());
            }
        }
    }
}
