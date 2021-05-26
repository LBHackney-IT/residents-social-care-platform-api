using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ResidentsSocialCarePlatformApi.V1.Boundary.Requests
{
    public class GetRelationshipsRequest
    {
        [FromRoute]
        public long PersonId { get; set; }
    }

    public class GetRelationshipsRequestValidator : AbstractValidator<GetRelationshipsRequest>
    {
        public GetRelationshipsRequestValidator()
        {
            RuleFor(getRelationshipsRequest => getRelationshipsRequest.PersonId)
                .NotNull().WithMessage("Person ID must be provided")
                .GreaterThan(0).WithMessage("Person ID must be greater than 0");
        }
    }
}
