namespace AddressRegistry.StreetName.Events
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Be.Vlaanderen.Basisregisters.GrAr.Common;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using Newtonsoft.Json;

    [EventTags(EventTag.For.Edit, EventTag.For.Sync)]
    [EventName(EventName)]
    [EventDescription("Het adres werd goedgekeurd.")]
    public class AddressWasApproved : IStreetNameEvent, IHasAddressPersistentLocalId
    {
        public const string EventName = "AddressWasApproved"; // BE CAREFUL CHANGING THIS!!

        public int StreetNamePersistentLocalId { get; }
        public int AddressPersistentLocalId { get; }
        public ProvenanceData Provenance { get; private set; }

        public AddressWasApproved(
            StreetNamePersistentLocalId streetNamePersistentLocalId,
            AddressPersistentLocalId addressPersistentLocalId)
        {
            AddressPersistentLocalId = addressPersistentLocalId;
            StreetNamePersistentLocalId = streetNamePersistentLocalId;
        }

        [JsonConstructor]
        private AddressWasApproved(
            int streetNamePersistentLocalId,
            int addressPersistentLocalId,
            ProvenanceData provenance)
            : this(
                new StreetNamePersistentLocalId(streetNamePersistentLocalId),
                new AddressPersistentLocalId(addressPersistentLocalId))
            => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);

        public IEnumerable<string> GetHashFields()
        {
            var fields = Provenance.GetHashFields().ToList();
            fields.Add(StreetNamePersistentLocalId.ToString(CultureInfo.InvariantCulture));
            fields.Add(AddressPersistentLocalId.ToString(CultureInfo.InvariantCulture));
            return fields;
        }

        public string GetHash() => this.ToEventHash(EventName);
    }
}
