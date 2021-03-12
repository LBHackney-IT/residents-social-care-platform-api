using System;
using System.Linq;
using ResidentsSocialCarePlatformApi.V1.Factories;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.Gateways;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.UseCase
{
    public class GetAllResidentsUseCase : IGetAllResidentsUseCase
    {
        private ISocialCareGateway _socialCareGateway;
        private IValidatePostcode _validatePostcode;
        public GetAllResidentsUseCase(ISocialCareGateway socialCareGateway, IValidatePostcode validatePostcode)
        {
            _socialCareGateway = socialCareGateway;
            _validatePostcode = validatePostcode;
        }

        public ResidentInformationList Execute(ResidentQueryParam rqp, int cursor, int limit)
        {
            CheckPostcodeValid(rqp);
            limit = limit < 10 ? 10 : limit;
            limit = limit > 100 ? 100 : limit;
            var residents = _socialCareGateway.GetAllResidents(cursor: cursor, limit: limit, rqp.MosaicId, rqp.FirstName, rqp.LastName,
                rqp.DateOfBirth, rqp.Postcode, rqp.Address, rqp.ContextFlag);

            var nextCursor = residents.Count == limit ? residents.Max(r => Int64.Parse(r.MosaicId)).ToString() : "";
            return new ResidentInformationList
            {
                Residents = residents.ToResponse(),
                NextCursor = nextCursor
            };
        }

        private void CheckPostcodeValid(ResidentQueryParam rqp)
        {
            if (string.IsNullOrWhiteSpace(rqp.Postcode)) return;
            var validPostcode = _validatePostcode.Execute(rqp.Postcode);
            if (!validPostcode) throw new InvalidQueryParameterException("The Postcode given does not have a valid format");
        }
    }
}
