namespace AddressRegistry.Consumer.Read.Municipality
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
    using Be.Vlaanderen.Basisregisters.MessageHandling.Kafka.Simple;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;
    using Projections;

    public class MunicipalityLatestItemConsumer
    {
        private readonly ILifetimeScope _container;
        private readonly KafkaOptions _options;
        private readonly MunicipalityConsumerOptions _municipalityConsumerOptions;

        public MunicipalityLatestItemConsumer(
            ILifetimeScope container,
            KafkaOptions options,
            MunicipalityConsumerOptions municipalityConsumerOptions)
        {
            _container = container;
            _options = options;
            _municipalityConsumerOptions = municipalityConsumerOptions;
        }

        public Task Start(CancellationToken cancellationToken = default)
        {
            var projector = new ConnectedProjector<MunicipalityConsumerContext>(Resolve.WhenEqualToHandlerMessageType(new MunicipalityLatestItemProjections().Handlers));

            var consumerGroupId = $"{nameof(AddressRegistry)}.{nameof(MunicipalityLatestItemConsumer)}.{_municipalityConsumerOptions.Topic}{_municipalityConsumerOptions.ConsumerGroupSuffix}";
            return KafkaConsumer.Consume(
                new KafkaConsumerOptions(
                    _options.BootstrapServers,
                    _options.SaslUserName,
                    _options.SaslPassword,
                    consumerGroupId,
                    _municipalityConsumerOptions.Topic,
                    async message =>
                    {
                        Console.WriteLine($"consuming...");
                        var municipalityConsumerContext = _container.Resolve<MunicipalityConsumerContext>();
                        await projector.ProjectAsync(municipalityConsumerContext, message, CancellationToken.None);
                        await municipalityConsumerContext.SaveChangesAsync(CancellationToken.None);
                    },
                    noMessageFoundDelay: 300,
                    offset: null,
                    _options.JsonSerializerSettings),
                cancellationToken);
        }
    }
}
