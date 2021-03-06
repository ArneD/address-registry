namespace AddressRegistry.Api.Legacy.Tests.LegacyTesting.Assert
{
    using System.Collections.Generic;
    using AddressMatch.Responses;
    using FluentAssertions;
    using Newtonsoft.Json;

    public class AdresMatchCollectieAssertions : Assertions<AddressMatchCollection, AdresMatchCollectieAssertions>
    {
        public AdresMatchCollectieAssertions(AddressMatchCollection subject) : base(subject)
        {
        }

        public AndWhichConstraint<AdresMatchCollectieAssertions, List<AdresMatchItem>> HaveMatches(int matchCount)
        {
            AssertingThat($"[{matchCount}] match(es) were found");

            Subject.AdresMatches.Should().HaveCount(matchCount);

            return AndWhich(Subject.AdresMatches);
        }

        internal AndConstraint<AdresMatchCollectieAssertions> ContainWarning(string warningMessagePart)
        {
            AssertingThat($"a warning containing [{warningMessagePart}] was present");

            Subject.Warnings.Should().Contain(w => w.Message.Contains(warningMessagePart));

            return And();
        }

        internal void BeEquivalentTo(AddressMatchCollection addressMatchCollection)
        {
            JsonConvert.SerializeObject((object) Subject).Should().Be(JsonConvert.SerializeObject(addressMatchCollection));
        }
    }
}
