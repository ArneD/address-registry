namespace AddressRegistry.Api.BackOffice
{
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions.Requests;
    using Abstractions.SqsRequests;
    using Abstractions.Validation;
    using Be.Vlaanderen.Basisregisters.AcmIdm;
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using Be.Vlaanderen.Basisregisters.Api.ETag;
    using Be.Vlaanderen.Basisregisters.Api.Exceptions;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using Be.Vlaanderen.Basisregisters.Sqs.Exceptions;
    using Infrastructure;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StreetName;
    using StreetName.Exceptions;
    using Swashbuckle.AspNetCore.Filters;

    public partial class AddressController
    {
        /// <summary>
        /// Hef een adres op.
        /// </summary>
        /// <param name="request"></param>l
        /// <param name="ifMatchHeaderValidator"></param>
        /// <param name="ifMatchHeaderValue"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="202">Aanvraag tot opheffing wordt reeds verwerkt.</response>
        /// <response code="400">Als de adres status niet 'ingebruik' is.</response>
        /// <response code="412">Als de If-Match header niet overeenkomt met de laatste ETag.</response>
        /// <returns></returns>
        [HttpPost("{persistentLocalId}/acties/opheffen")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestResponseExamples))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorResponseExamples))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyNames.Adres.DecentraleBijwerker)]
        public async Task<IActionResult> Retire(
            [FromServices] IIfMatchHeaderValidator ifMatchHeaderValidator,
            [FromRoute] RetireAddressRequest request,
            [FromHeader(Name = "If-Match")] string? ifMatchHeaderValue,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Check if user provided ETag is equal to the current Entity Tag
                if (!await ifMatchHeaderValidator.IsValid(ifMatchHeaderValue, new AddressPersistentLocalId(request.PersistentLocalId), cancellationToken))
                {
                    return new PreconditionFailedResult();
                }

                var sqsRequest = new RetireAddressSqsRequest
                {
                    Request = request,
                    IfMatchHeaderValue = ifMatchHeaderValue,
                    Metadata = GetMetadata(),
                    ProvenanceData = new ProvenanceData(CreateFakeProvenance())
                };
                var sqsResult = await _mediator.Send(sqsRequest, cancellationToken);

                return Accepted(sqsResult);
            }
            catch (AggregateIdIsNotFoundException)
            {
                throw new ApiException(ValidationErrors.Common.AddressNotFound.Message, StatusCodes.Status404NotFound);
            }
            catch (AggregateNotFoundException)
            {
                throw new ApiException(ValidationErrors.Common.AddressNotFound.Message, StatusCodes.Status404NotFound);
            }
            catch (AddressIsNotFoundException)
            {
                throw new ApiException(ValidationErrors.Common.AddressNotFound.Message, StatusCodes.Status404NotFound);
            }
        }
    }
}
