namespace AddressRegistry.Tests.AggregateTests.WhenReaddressing.GivenDestinationAddressDoesNotExist
{
    using System.Collections.Generic;
    using Api.BackOffice.Abstractions;
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using Builders;
    using FluentAssertions;
    using global::AutoFixture;
    using StreetName;
    using StreetName.Commands;
    using StreetName.DataStructures;
    using StreetName.Events;
    using Xunit;
    using Xunit.Abstractions;

    public class SourceAddressHasBoxNumbers : AddressRegistryTest
    {
        private readonly StreetNameStreamId _streamId;
        private readonly StreetNamePersistentLocalId _streetNamePersistentLocalId;

        public SourceAddressHasBoxNumbers(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            Fixture.Customize(new WithFixedStreetNamePersistentLocalId());

            _streamId = Fixture.Create<StreetNameStreamId>();
            _streetNamePersistentLocalId = Fixture.Create<StreetNamePersistentLocalId>();
        }

        [Fact]
        public void ThenDestinationAddressHasBoxNumbers()
        {
            var sourceAddressPersistentLocalId = new AddressPersistentLocalId(100);
            var proposedBoxNumberAddressPersistentLocalId = new AddressPersistentLocalId(101);
            var currentBoxNumberAddressPersistentLocalId = new AddressPersistentLocalId(102);

            var destinationAddressPersistentLocalId = new AddressPersistentLocalId(1); // FakePersistentLocalIdGenerator starts with id 1
            var destinationProposedBoxNumberAddressPersistentLocalId = new AddressPersistentLocalId(2);
            var destinationCurrentBoxNumberAddressPersistentLocalId = new AddressPersistentLocalId(3);

            var sourceHouseNumber = new HouseNumber("11");
            var destinationHouseNumber = new HouseNumber("13");

            var sourceAddressWasMigrated = new AddressWasMigratedToStreetNameBuilder(Fixture)
                .WithAddressPersistentLocalId(sourceAddressPersistentLocalId)
                .WithHouseNumber(sourceHouseNumber)
                .WithAddressGeometry(new AddressGeometry(
                    GeometryMethod.AppointedByAdministrator,
                    GeometrySpecification.Entry,
                    GeometryHelpers.GmlPointGeometry.ToExtendedWkbGeometry()))
                .Build();

            var proposedBoxNumberAddressWasMigrated = new AddressWasMigratedToStreetNameBuilder(Fixture, AddressStatus.Proposed)
                .WithAddressPersistentLocalId(proposedBoxNumberAddressPersistentLocalId)
                .WithHouseNumber(sourceHouseNumber)
                .WithAddressGeometry(new AddressGeometry(
                    GeometryMethod.AppointedByAdministrator,
                    GeometrySpecification.Entry,
                    GeometryHelpers.GmlPointGeometry.ToExtendedWkbGeometry()))
                .WithBoxNumber(new BoxNumber("A1"), sourceAddressPersistentLocalId)
                .Build();

            var currentBoxNumberAddressWasMigrated = new AddressWasMigratedToStreetNameBuilder(Fixture, AddressStatus.Current)
                .WithAddressPersistentLocalId(currentBoxNumberAddressPersistentLocalId)
                .WithHouseNumber(sourceHouseNumber)
                .WithAddressGeometry(new AddressGeometry(
                    GeometryMethod.AppointedByAdministrator,
                    GeometrySpecification.Entry,
                    GeometryHelpers.GmlPointGeometry.ToExtendedWkbGeometry()))
                .WithBoxNumber(new BoxNumber("A2"), sourceAddressPersistentLocalId)
                .Build();

            var command = new Readdress(
                _streetNamePersistentLocalId,
                new List<ReaddressAddressItem>
                {
                    new ReaddressAddressItem(_streetNamePersistentLocalId, sourceAddressPersistentLocalId , destinationHouseNumber)
                },
                new List<RetireAddressItem>(),
                Fixture.Create<Provenance>());

            Assert(new Scenario()
                .Given(_streamId,
                    Fixture.Create<StreetNameWasImported>(),
                    sourceAddressWasMigrated,
                    proposedBoxNumberAddressWasMigrated,
                    currentBoxNumberAddressWasMigrated)
                .When(command)
                .Then(new[]
                {
                    new Fact(_streamId,
                        new AddressWasProposedBecauseOfReaddress(
                            _streetNamePersistentLocalId,
                            destinationAddressPersistentLocalId,
                            sourceAddressPersistentLocalId,
                            null,
                            new PostalCode(sourceAddressWasMigrated.PostalCode!),
                            destinationHouseNumber,
                            null,
                            sourceAddressWasMigrated.GeometryMethod,
                            sourceAddressWasMigrated.GeometrySpecification,
                            new ExtendedWkbGeometry(sourceAddressWasMigrated.ExtendedWkbGeometry))),
                    new Fact(_streamId,
                        new AddressWasProposedBecauseOfReaddress(
                            _streetNamePersistentLocalId,
                            destinationProposedBoxNumberAddressPersistentLocalId,
                            proposedBoxNumberAddressPersistentLocalId,
                            destinationAddressPersistentLocalId,
                            new PostalCode(sourceAddressWasMigrated.PostalCode!),
                            destinationHouseNumber,
                            new BoxNumber(proposedBoxNumberAddressWasMigrated.BoxNumber!),
                            proposedBoxNumberAddressWasMigrated.GeometryMethod,
                            proposedBoxNumberAddressWasMigrated.GeometrySpecification,
                            new ExtendedWkbGeometry(proposedBoxNumberAddressWasMigrated.ExtendedWkbGeometry))),
                    new Fact(_streamId,
                        new AddressWasProposedBecauseOfReaddress(
                            _streetNamePersistentLocalId,
                            destinationCurrentBoxNumberAddressPersistentLocalId,
                            currentBoxNumberAddressPersistentLocalId,
                            destinationAddressPersistentLocalId,
                            new PostalCode(sourceAddressWasMigrated.PostalCode!),
                            destinationHouseNumber,
                            new BoxNumber(currentBoxNumberAddressWasMigrated.BoxNumber!),
                            currentBoxNumberAddressWasMigrated.GeometryMethod,
                            currentBoxNumberAddressWasMigrated.GeometrySpecification,
                            new ExtendedWkbGeometry(currentBoxNumberAddressWasMigrated.ExtendedWkbGeometry))),
                    new Fact(_streamId,
                        new AddressHouseNumberWasReaddressed(
                            _streetNamePersistentLocalId,
                            destinationAddressPersistentLocalId,
                            readdressedHouseNumber: new ReaddressedAddressData(
                                sourceAddressPersistentLocalId,
                                destinationAddressPersistentLocalId,
                                isDestinationNewlyProposed: true,
                                sourceAddressWasMigrated.Status,
                                destinationHouseNumber,
                                boxNumber: null,
                                new PostalCode(sourceAddressWasMigrated.PostalCode!),
                                new AddressGeometry(
                                    sourceAddressWasMigrated.GeometryMethod,
                                    sourceAddressWasMigrated.GeometrySpecification,
                                    new ExtendedWkbGeometry(sourceAddressWasMigrated.ExtendedWkbGeometry)),
                                sourceAddressWasMigrated.OfficiallyAssigned),
                            readdressedBoxNumbers: new List<ReaddressedAddressData>
                            {
                                new ReaddressedAddressData(
                                    proposedBoxNumberAddressPersistentLocalId,
                                    destinationProposedBoxNumberAddressPersistentLocalId,
                                    isDestinationNewlyProposed: true,
                                    proposedBoxNumberAddressWasMigrated.Status,
                                    destinationHouseNumber,
                                    boxNumber: new BoxNumber(proposedBoxNumberAddressWasMigrated.BoxNumber!),
                                    new PostalCode(sourceAddressWasMigrated.PostalCode!),
                                    new AddressGeometry(
                                        proposedBoxNumberAddressWasMigrated.GeometryMethod,
                                        proposedBoxNumberAddressWasMigrated.GeometrySpecification,
                                        new ExtendedWkbGeometry(proposedBoxNumberAddressWasMigrated.ExtendedWkbGeometry)),
                                    proposedBoxNumberAddressWasMigrated.OfficiallyAssigned),
                                new ReaddressedAddressData(
                                    currentBoxNumberAddressPersistentLocalId,
                                    destinationCurrentBoxNumberAddressPersistentLocalId,
                                    isDestinationNewlyProposed: true,
                                    currentBoxNumberAddressWasMigrated.Status,
                                    destinationHouseNumber,
                                    boxNumber: new BoxNumber(currentBoxNumberAddressWasMigrated.BoxNumber!),
                                    new PostalCode(sourceAddressWasMigrated.PostalCode!),
                                    new AddressGeometry(
                                        currentBoxNumberAddressWasMigrated.GeometryMethod,
                                        currentBoxNumberAddressWasMigrated.GeometrySpecification,
                                        new ExtendedWkbGeometry(currentBoxNumberAddressWasMigrated.ExtendedWkbGeometry)),
                                    currentBoxNumberAddressWasMigrated.OfficiallyAssigned)
                            }))
                }));

            command.ExecutionContext.AddressesAdded.Should().HaveCount(3);
            command.ExecutionContext.AddressesAdded.Should()
                .ContainSingle(x =>
                    x.streetNamePersistentLocalId == _streetNamePersistentLocalId
                    && x.addressPersistentLocalId == destinationAddressPersistentLocalId);
            command.ExecutionContext.AddressesAdded.Should()
                .ContainSingle(x =>
                    x.streetNamePersistentLocalId == _streetNamePersistentLocalId
                    && x.addressPersistentLocalId == destinationProposedBoxNumberAddressPersistentLocalId);
            command.ExecutionContext.AddressesAdded.Should()
                .ContainSingle(x =>
                    x.streetNamePersistentLocalId == _streetNamePersistentLocalId
                    && x.addressPersistentLocalId == destinationCurrentBoxNumberAddressPersistentLocalId);

            command.ExecutionContext.AddressesUpdated.Should().HaveCount(1);
            command.ExecutionContext.AddressesUpdated.Should()
                .ContainSingle(x =>
                    x.streetNamePersistentLocalId == _streetNamePersistentLocalId
                    && x.addressPersistentLocalId == destinationAddressPersistentLocalId);
        }
    }
}
