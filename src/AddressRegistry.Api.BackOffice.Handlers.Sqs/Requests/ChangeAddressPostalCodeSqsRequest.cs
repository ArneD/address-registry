namespace AddressRegistry.Api.BackOffice.Handlers.Sqs.Requests
{
    using Abstractions.Requests;
    using Be.Vlaanderen.Basisregisters.Sqs.Requests;

    public sealed class ChangeAddressPostalCodeSqsRequest : SqsRequest, IHasBackOfficeRequest<ChangeAddressPostalCodeBackOfficeRequest>
    {
        public int PersistentLocalId { get; set; }

        public ChangeAddressPostalCodeBackOfficeRequest Request { get; init; }
    }
}