namespace AddressRegistry.Api.BackOffice.Abstractions.Requests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using MediatR;
    using Newtonsoft.Json;
    using Responses;
    using StreetName;
    using StreetName.Commands;
    using Swashbuckle.AspNetCore.Filters;

    [DataContract(Name = "CorrigerenHuisnummerAdres", Namespace = "")]
    public class AddressCorrectHouseNumberRequest : IRequest<ETagResponse>
    {
        /// <summary>
        /// De unieke en persistente identificator van het adres.
        /// </summary>
        [JsonIgnore]
        public int PersistentLocalId { get; set; }

        /// <summary>
        /// Het huisnummer van het adres.
        /// </summary>
        [DataMember(Name = "Huisnummer", Order = 0)]
        [JsonProperty(Required = Required.Always)]
        public string Huisnummer { get; set; }

        [JsonIgnore]
        public IDictionary<string, object> Metadata { get; set; }

        /// <summary>
        /// Map to CorrectAddressHouseNumber command
        /// </summary>
        /// <returns>CorrectAddressHouseNumber.</returns>
        public CorrectAddressHouseNumber ToCommand(
            StreetNamePersistentLocalId streetNamePersistentLocalId,
            Provenance provenance)
        {
            return new CorrectAddressHouseNumber(
                streetNamePersistentLocalId,
                new AddressPersistentLocalId(PersistentLocalId),
                HouseNumber.Create(Huisnummer),
                provenance);
        }
    }

    public class AddressCorrectHouseNumberRequestExamples : IExamplesProvider<AddressCorrectHouseNumberRequest>
    {
        public AddressCorrectHouseNumberRequest GetExamples()
        {
            return new AddressCorrectHouseNumberRequest
            {
                Huisnummer = "11",
            };
        }
    }
}
