using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Todo.WebJob
{
    public class OperationIdTelemetryInitializer : ITelemetryInitializer
    {
        private readonly TodoTelemetryService _todoTelemetryService;

        public OperationIdTelemetryInitializer(TodoTelemetryService todoTelemetryService)
        {
            this._todoTelemetryService = todoTelemetryService;
        }

        public void Initialize(ITelemetry telemetry)
        {
            string operationId = _todoTelemetryService.GetOperationId();
            if (!string.IsNullOrEmpty(operationId))
            {
                telemetry.Context.GlobalProperties["Operation Id"] = operationId;
            }
        }
    }
}
