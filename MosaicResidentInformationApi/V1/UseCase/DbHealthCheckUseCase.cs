using Microsoft.Extensions.HealthChecks;
using MosaicResidentInformationApi.V1.Boundary;
using MosaicResidentInformationApi.V1.Boundary.Responses;

namespace MosaicResidentInformationApi.V1.UseCase
{
    public class DbHealthCheckUseCase
    {
        private readonly IHealthCheckService _healthCheckService;

        public DbHealthCheckUseCase(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        public HealthCheckResponse Execute()
        {
            var result = _healthCheckService.CheckHealthAsync().Result;

            var success = result.CheckStatus == CheckStatus.Healthy;
            return new HealthCheckResponse(success, result.Description);
        }
    }

}
