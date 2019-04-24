using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace Todo.Initializer
{
    public class CorrelationIdTelemetryInitializer : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CorrelationIdTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            var headers = this.httpContextAccessor?.HttpContext?.Request?.Headers;
            string operationId = (headers != null && headers.ContainsKey("Request-Id")) ? headers["Request-Id"].ToString() : null;
            if (operationId != null)
            {
                telemetry.Context.Operation.ParentId = operationId;
            }
        }
    }
}
