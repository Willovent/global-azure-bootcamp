
using Microsoft.ApplicationInsights;

namespace Todo.WebJob
{
    public class TodoTelemetryService
    {
        private readonly TelemetryClient _telemetryClient;

        public TodoTelemetryService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void SetOperationId(string correlationId)
        {
            _telemetryClient.Context.Operation.ParentId = correlationId;
        }
    }
}
