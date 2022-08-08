namespace AddressRegistry.Tests.ProjectionTests.Legacy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoFixture;
    using Be.Vlaanderen.Basisregisters.GrAr.Common.Pipes;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.SqlStreamStore;
    using FluentAssertions;
    using global::AutoFixture;
    using Projections.Legacy.AddressListV2;
    using StreetName;
    using StreetName.Events;
    using Xunit;

    public class AddressListProjectionsV2Tests : AddressLegacyProjectionTest<AddressListProjectionsV2>
    {
        private readonly Fixture? _fixture;

        public AddressListProjectionsV2Tests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new InfrastructureCustomization());
            _fixture.Customize(new WithFixedStreetNamePersistentLocalId());
            _fixture.Customize(new WithFixedAddressPersistentLocalId());
        }

        [Fact]
        public async Task WhenAddressWasMigratedToStreetName()
        {
            var addressWasMigratedToStreetName = _fixture.Create<AddressWasMigratedToStreetName>();

            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasMigratedToStreetName.GetHash() }
            };

            await Sut
                .Given(new Envelope<AddressWasMigratedToStreetName>(new Envelope(addressWasMigratedToStreetName, metadata)))
                .Then(async ct =>
                {
                    var expectedListItem = (await ct.AddressListV2.FindAsync(addressWasMigratedToStreetName.AddressPersistentLocalId));
                    expectedListItem.Should().NotBeNull();
                    expectedListItem.StreetNamePersistentLocalId.Should().Be(addressWasMigratedToStreetName.StreetNamePersistentLocalId);
                    expectedListItem.HouseNumber.Should().Be(addressWasMigratedToStreetName.HouseNumber);
                    expectedListItem.BoxNumber.Should().Be(addressWasMigratedToStreetName.BoxNumber);
                    expectedListItem.PostalCode.Should().Be(addressWasMigratedToStreetName.PostalCode);
                    expectedListItem.Status.Should().Be(addressWasMigratedToStreetName.Status);
                    expectedListItem.Removed.Should().Be(addressWasMigratedToStreetName.IsRemoved);
                    expectedListItem.VersionTimestamp.Should().Be(addressWasMigratedToStreetName.Provenance.Timestamp);
                    expectedListItem.LastEventHash.Should().Be(addressWasMigratedToStreetName.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasProposedV2()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            await Sut
                .Given(new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)))
                .Then(async ct =>
                {
                    var expectedListItem = (await ct.AddressListV2.FindAsync(addressWasProposedV2.AddressPersistentLocalId));
                    expectedListItem.Should().NotBeNull();
                    expectedListItem.StreetNamePersistentLocalId.Should().Be(addressWasProposedV2.StreetNamePersistentLocalId);
                    expectedListItem.HouseNumber.Should().Be(addressWasProposedV2.HouseNumber);
                    expectedListItem.BoxNumber.Should().Be(addressWasProposedV2.BoxNumber);
                    expectedListItem.PostalCode.Should().Be(addressWasProposedV2.PostalCode);
                    expectedListItem.Status.Should().Be(AddressStatus.Proposed);
                    expectedListItem.Removed.Should().BeFalse();
                    expectedListItem.VersionTimestamp.Should().Be(addressWasProposedV2.Provenance.Timestamp);
                    expectedListItem.LastEventHash.Should().Be(addressWasProposedV2.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasApproved()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            var addressWasApproved = _fixture.Create<AddressWasApproved>();
            var metadata2 = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasApproved.GetHash() }
            };

            await Sut
                .Given(
                    new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)),
                    new Envelope<AddressWasApproved>(new Envelope(addressWasApproved, metadata2)))
                .Then(async ct =>
                {
                    var addressListItemV2 = (await ct.AddressListV2.FindAsync(addressWasApproved.AddressPersistentLocalId));
                    addressListItemV2.Should().NotBeNull();
                    addressListItemV2.Status.Should().Be(AddressStatus.Current);
                    addressListItemV2.VersionTimestamp.Should().Be(addressWasApproved.Provenance.Timestamp);
                    addressListItemV2.LastEventHash.Should().Be(addressWasApproved.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasRejected()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            var addressWasRejected = _fixture.Create<AddressWasRejected>();
            var metadata2 = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasRejected.GetHash() }
            };

            await Sut
                .Given(
                    new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)),
                    new Envelope<AddressWasRejected>(new Envelope(addressWasRejected, metadata2)))
                .Then(async ct =>
                {
                    var addressListItemV2 = (await ct.AddressListV2.FindAsync(addressWasRejected.AddressPersistentLocalId));
                    addressListItemV2.Should().NotBeNull();
                    addressListItemV2.Status.Should().Be(AddressStatus.Rejected);
                    addressListItemV2.VersionTimestamp.Should().Be(addressWasRejected.Provenance.Timestamp);
                    addressListItemV2.LastEventHash.Should().Be(addressWasRejected.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasRejectedBecauseHouseNumberWasRejected()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            var addressWasRejected = _fixture.Create<AddressWasRejectedBecauseHouseNumberWasRejected>();
            var metadata2 = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasRejected.GetHash() }
            };

            await Sut
                .Given(
                    new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)),
                    new Envelope<AddressWasRejectedBecauseHouseNumberWasRejected>(new Envelope(addressWasRejected, metadata2)))
                .Then(async ct =>
                {
                    var addressListItemV2 = (await ct.AddressListV2.FindAsync(addressWasRejected.AddressPersistentLocalId));
                    addressListItemV2.Should().NotBeNull();
                    addressListItemV2.Status.Should().Be(AddressStatus.Rejected);
                    addressListItemV2.VersionTimestamp.Should().Be(addressWasRejected.Provenance.Timestamp);
                    addressListItemV2.LastEventHash.Should().Be(addressWasRejected.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasRejectedBecauseStreetNameWasRetired()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            var addressWasRejected = _fixture.Create<AddressWasRejectedBecauseStreetNameWasRetired>();
            var metadata2 = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasRejected.GetHash() }
            };

            await Sut
                .Given(
                    new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)),
                    new Envelope<AddressWasRejectedBecauseStreetNameWasRetired>(new Envelope(addressWasRejected, metadata2)))
                .Then(async ct =>
                {
                    var addressListItemV2 = (await ct.AddressListV2.FindAsync(addressWasRejected.AddressPersistentLocalId));
                    addressListItemV2.Should().NotBeNull();
                    addressListItemV2.Status.Should().Be(AddressStatus.Rejected);
                    addressListItemV2.VersionTimestamp.Should().Be(addressWasRejected.Provenance.Timestamp);
                    addressListItemV2.LastEventHash.Should().Be(addressWasRejected.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasRetiredV2()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            var addressWasApproved = _fixture.Create<AddressWasApproved>();
            var approveMetadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasApproved.GetHash() }
            };

            var addressWasRetiredV2 = _fixture.Create<AddressWasRetiredV2>();
            var retireMetadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasRetiredV2.GetHash() }
            };

            await Sut
                .Given(
                    new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)),
                    new Envelope<AddressWasApproved>(new Envelope(addressWasApproved, approveMetadata)),
                    new Envelope<AddressWasRetiredV2>(new Envelope(addressWasRetiredV2, retireMetadata)))
                .Then(async ct =>
                {
                    var addressListItemV2 = (await ct.AddressListV2.FindAsync(addressWasRetiredV2.AddressPersistentLocalId));
                    addressListItemV2.Should().NotBeNull();
                    addressListItemV2.Status.Should().Be(AddressStatus.Retired);
                    addressListItemV2.VersionTimestamp.Should().Be(addressWasRetiredV2.Provenance.Timestamp);
                    addressListItemV2.LastEventHash.Should().Be(addressWasRetiredV2.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasRetiredBecauseHouseNumberWasRetired()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            var addressWasApproved = _fixture.Create<AddressWasApproved>();
            var approveMetadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasApproved.GetHash() }
            };

            var addressWasRetiredBecauseHouseNumberWasRetired = _fixture.Create<AddressWasRetiredBecauseHouseNumberWasRetired>();
            var retireMetadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasRetiredBecauseHouseNumberWasRetired.GetHash() }
            };

            await Sut
                .Given(
                    new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)),
                    new Envelope<AddressWasApproved>(new Envelope(addressWasApproved, approveMetadata)),
                    new Envelope<AddressWasRetiredBecauseHouseNumberWasRetired>(new Envelope(addressWasRetiredBecauseHouseNumberWasRetired, retireMetadata)))
                .Then(async ct =>
                {
                    var addressListItemV2 = (await ct.AddressListV2.FindAsync(addressWasRetiredBecauseHouseNumberWasRetired.AddressPersistentLocalId));
                    addressListItemV2.Should().NotBeNull();
                    addressListItemV2.Status.Should().Be(AddressStatus.Retired);
                    addressListItemV2.VersionTimestamp.Should().Be(addressWasRetiredBecauseHouseNumberWasRetired.Provenance.Timestamp);
                    addressListItemV2.LastEventHash.Should().Be(addressWasRetiredBecauseHouseNumberWasRetired.GetHash());
                });
        }

        [Fact]
        public async Task WhenAddressWasRetiredBecauseStreetNameWasRetired()
        {
            var addressWasProposedV2 = _fixture.Create<AddressWasProposedV2>();
            var metadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasProposedV2.GetHash() }
            };

            var addressWasApproved = _fixture.Create<AddressWasApproved>();
            var approveMetadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasApproved.GetHash() }
            };

            var addressWasRetiredBecauseHouseNumberWasRetired = _fixture.Create<AddressWasRetiredBecauseStreetNameWasRetired>();
            var retireMetadata = new Dictionary<string, object>
            {
                { AddEventHashPipe.HashMetadataKey, addressWasRetiredBecauseHouseNumberWasRetired.GetHash() }
            };

            await Sut
                .Given(
                    new Envelope<AddressWasProposedV2>(new Envelope(addressWasProposedV2, metadata)),
                    new Envelope<AddressWasApproved>(new Envelope(addressWasApproved, approveMetadata)),
                    new Envelope<AddressWasRetiredBecauseStreetNameWasRetired>(new Envelope(addressWasRetiredBecauseHouseNumberWasRetired, retireMetadata)))
                .Then(async ct =>
                {
                    var addressListItemV2 = (await ct.AddressListV2.FindAsync(addressWasRetiredBecauseHouseNumberWasRetired.AddressPersistentLocalId));
                    addressListItemV2.Should().NotBeNull();
                    addressListItemV2.Status.Should().Be(AddressStatus.Retired);
                    addressListItemV2.VersionTimestamp.Should().Be(addressWasRetiredBecauseHouseNumberWasRetired.Provenance.Timestamp);
                    addressListItemV2.LastEventHash.Should().Be(addressWasRetiredBecauseHouseNumberWasRetired.GetHash());
                });
        }

        protected override AddressListProjectionsV2 CreateProjection()
            => new AddressListProjectionsV2();
    }
}
