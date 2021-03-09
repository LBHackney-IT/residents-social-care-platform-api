namespace ResidentsSocialCarePlatformApi.V1.Boundary.Responses
{
    public class HealthCheckResponse
    {
        private readonly bool _success;
        private readonly string _message;

        public HealthCheckResponse(bool success, string message)
        {
            _success = success;
            _message = message;
        }

        public string Message => _message;
        public bool Success => _success;
    }
}
