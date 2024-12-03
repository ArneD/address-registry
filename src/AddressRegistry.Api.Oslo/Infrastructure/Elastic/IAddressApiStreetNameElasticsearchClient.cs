﻿namespace AddressRegistry.Api.Oslo.Infrastructure.Elastic
{
    using System.Threading.Tasks;
    using Consumer.Read.StreetName.Projections;

    public interface IAddressApiStreetNameElasticsearchClient
    {
        Task<StreetNameSearchResult> SearchStreetNames(
            string query,
            string? nisCode,
            StreetNameStatus? status,
            int size = 10);
    }
}
