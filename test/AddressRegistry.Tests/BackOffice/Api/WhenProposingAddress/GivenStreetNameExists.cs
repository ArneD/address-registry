namespace AddressRegistry.Tests.BackOffice.Api.WhenProposingAddress
{
    using System.Linq;
    using System.Threading.Tasks;
    using Address;
    using AddressRegistry.Api.BackOffice.Address;
    using AddressRegistry.Api.BackOffice.Address.Requests;
    using Autofac;
    using Be.Vlaanderen.Basisregisters.Api.ETag;
    using Be.Vlaanderen.Basisregisters.CommandHandling.Idempotency;
    using FluentAssertions;
    using global::AutoFixture;
    using Infrastructure;
    using Moq;
    using SqlStreamStore;
    using SqlStreamStore.Streams;
    using StreetName;
    using Xunit;
    using Xunit.Abstractions;
    using StreetNameId = StreetName.StreetNameId;

    public class GivenStreetNameExists : AddressRegistryBackOfficeTest
    {
        private readonly AddressController _controller;
        private readonly TestBackOfficeContext _backOfficeContext;
        private readonly IdempotencyContext _idempotencyContext;
        private readonly TestConsumerContext _consumerContext;

        public GivenStreetNameExists(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _controller = CreateApiBusControllerWithUser<AddressController>("John Doe");
            _idempotencyContext = new FakeIdempotencyContextFactory().CreateDbContext();
            _consumerContext = new FakeConsumerContextFactory().CreateDbContext();
            _backOfficeContext = new FakeBackOfficeContextFactory().CreateDbContext();
        }

        [Fact]
        public async Task ThenTheAddressIsProposed()
        {
            const int expectedLocation = 5;
            var streetNameId = Fixture.Create<StreetNameId>();
            var streetNamePersistentId = Fixture.Create<StreetNamePersistentLocalId>();

            //Arrange
            var consumerItem = _consumerContext
                .AddStreetNameConsumerItemFixtureWithPersistentLocalIdAndStreetNameId(streetNameId, streetNamePersistentId);
            var mockPersistentLocalIdGenerator = new Mock<IPersistentLocalIdGenerator>();
            mockPersistentLocalIdGenerator
                .Setup(x => x.GenerateNextPersistentLocalId())
                .Returns(new PersistentLocalId(expectedLocation));

            var streamVersion = 0;

            ImportMigratedStreetName(streetNameId, streetNamePersistentId);
            streamVersion++;

            var body = new AddressProposeRequest
            {
                StraatNaamId = $"https://data.vlaanderen.be/id/straatnaam/{consumerItem.PersistentLocalId}",
                PostInfoId = $"https://data.vlaanderen.be/id/postinfo/8200",
                HouseNumber = Fixture.Create<string>(),
            };

            //Act
            var result = (CreatedWithLastObservedPositionAsETagResult)await _controller.Propose(
                ResponseOptions,
                _idempotencyContext,
                _backOfficeContext,
                mockPersistentLocalIdGenerator.Object,
                //new AddressProposeRequestValidator(_consumerContext),
                Container.Resolve<IStreetNames>(),
                body);

            //Assert
            result.Location.Should().Be(string.Format(DetailUrl, expectedLocation));
            result.LastObservedPositionAsETag.Length.Should().Be(128);

            var stream = await Container.Resolve<IStreamStore>().ReadStreamBackwards(new StreamId(new StreetNameStreamId(streetNamePersistentId)), streamVersion, 1); //1 = version of stream (zero based)
            stream.Messages.First().JsonMetadata.Should().Contain(result.LastObservedPositionAsETag);

            var municipalityIdByPersistentLocalId = await _backOfficeContext.AddressPersistentIdStreetNamePersistentIds.FindAsync(expectedLocation);
            municipalityIdByPersistentLocalId.Should().NotBeNull();
            municipalityIdByPersistentLocalId.StreetNamePersistentLocalId.Should().Be(consumerItem.PersistentLocalId);
        }
    }
}